Imports System.Windows.Forms.DataVisualization.Charting
Imports MySqlConnector

Public Class FormSales

    Private currentYear As Integer = DateTime.Now.Year
    Private salesData As New Dictionary(Of String, (Revenue As Decimal, Expenses As Decimal, Profit As Decimal))

    ' =======================================================================
    ' FORM LOAD
    ' =======================================================================
    Private Sub FormSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Hide panel if it's blocking controls
            If Panel1 IsNot Nothing Then
                Panel1.Visible = False
                Panel1.SendToBack()
            End If

            ' Bring controls to front
            RoundedPane21.BringToFront()
            RoundedPane22.BringToFront()
            RoundedPane23.BringToFront()
            RoundedPane24.BringToFront()

            ' Configure and load
            ConfigureChart()
            LoadAndDisplaySalesData()
            UpdateSummaryCards()

        Catch ex As Exception
            MessageBox.Show($"Form Load Error: {ex.Message}{vbCrLf}{vbCrLf}Stack Trace:{vbCrLf}{ex.StackTrace}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' CONFIGURE CHART
    ' =======================================================================
    Private Sub ConfigureChart()
        Try
            With Chart1
                .ChartAreas(0).AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230)
                .ChartAreas(0).AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot
                .ChartAreas(0).AxisX.LabelStyle.Font = New Font("Segoe UI", 9)

                .ChartAreas(0).AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230)
                .ChartAreas(0).AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot
                .ChartAreas(0).AxisY.LabelStyle.Format = "₱{0:N0}"
                .ChartAreas(0).AxisY.LabelStyle.Font = New Font("Segoe UI", 9)

                .Series("Revenue").Color = Color.FromArgb(99, 102, 241)
                .Series("Expenses").Color = Color.FromArgb(239, 68, 68)
                .Series("NetProfit").Color = Color.FromArgb(34, 197, 94)

                For Each series As Series In .Series
                    series.ChartType = SeriesChartType.Column
                    series.BorderWidth = 0
                    series("PointWidth") = "0.6"
                Next

                .Legends(0).Font = New Font("Segoe UI", 9)
                .Legends(0).Docking = Docking.Bottom

                For Each series As Series In .Series
                    series.ToolTip = "#VALX: ₱#VALY{N2}"
                Next
            End With
        Catch ex As Exception
            MessageBox.Show($"Chart Config Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' LOAD DATA - STEP BY STEP WITH DEBUGGING
    ' =======================================================================
    Private Sub LoadAndDisplaySalesData()
        Try
            ' Step 1: Check connection
            If conn Is Nothing Then
                MessageBox.Show("Connection object is Nothing. Please initialize database connection first.",
                              "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                LoadSampleData()
                Return
            End If

            ' Step 2: Open connection
            If conn.State <> ConnectionState.Open Then
                Try
                    openConn()
                Catch connEx As Exception
                    MessageBox.Show($"Cannot open connection: {connEx.Message}",
                                  "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    LoadSampleData()
                    Return
                End Try
            End If

            ' Step 3: Check if tables exist
            If Not TablesExist() Then
                MessageBox.Show("Required tables (payments or reservation_payments) not found. Loading sample data.",
                              "Tables Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                LoadSampleData()
                Return
            End If

            ' Step 4: Build and execute query
            Dim sql As String = BuildSalesQuery()

            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    InitializeChartData()

                    Dim dataFound As Boolean = False
                    While reader.Read()
                        dataFound = True
                        Dim monthNum As Integer = Convert.ToInt32(reader("MonthNum"))
                        Dim monthName As String = New DateTime(currentYear, monthNum, 1).ToString("MMM")

                        Dim revenue As Decimal = If(IsDBNull(reader("TotalRevenue")), 0D, Convert.ToDecimal(reader("TotalRevenue")))
                        Dim expenses As Decimal = If(IsDBNull(reader("TotalExpenses")), 0D, Convert.ToDecimal(reader("TotalExpenses")))
                        Dim profit As Decimal = revenue - expenses

                        salesData(monthName) = (revenue, expenses, profit)

                        Dim pointIndex As Integer = monthNum - 1
                        Chart1.Series("Revenue").Points(pointIndex).YValues(0) = revenue
                        Chart1.Series("Expenses").Points(pointIndex).YValues(0) = expenses
                        Chart1.Series("NetProfit").Points(pointIndex).YValues(0) = profit
                    End While

                    If Not dataFound Then
                        MessageBox.Show($"No payment data found for year {currentYear}. Showing empty chart.",
                                      "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show($"Database Error: {ex.Message}{vbCrLf}Error Code: {ex.Number}{vbCrLf}{vbCrLf}Loading sample data instead.",
                          "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LoadSampleData()
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}{vbCrLf}{vbCrLf}{ex.StackTrace}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LoadSampleData()
        End Try
    End Sub

    ' =======================================================================
    ' CHECK IF REQUIRED TABLES EXIST
    ' =======================================================================
    Private Function TablesExist() As Boolean
        Try
            Dim sql As String = "
                SELECT COUNT(*) AS TableCount
                FROM information_schema.tables 
                WHERE table_schema = DATABASE()
                  AND table_name IN ('payments', 'reservation_payments')
            "

            Using cmd As New MySqlCommand(sql, conn)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' =======================================================================
    ' BUILD SALES QUERY DYNAMICALLY
    ' =======================================================================
    Private Function BuildSalesQuery() As String
        Dim queries As New List(Of String)

        ' Check and add payments table
        If TableExists("payments") Then
            queries.Add($"
                SELECT 
                    MONTH(PaymentDate) AS MonthNum, 
                    AmountPaid AS Amount, 
                    'Revenue' AS Type
                FROM payments 
                WHERE PaymentStatus = 'Completed' 
                  AND YEAR(PaymentDate) = {currentYear}
                  AND AmountPaid IS NOT NULL
                  AND AmountPaid > 0
            ")
        End If

        ' Check and add reservation_payments table
        If TableExists("reservation_payments") Then
            queries.Add($"
                SELECT 
                    MONTH(PaymentDate) AS MonthNum, 
                    AmountPaid AS Amount, 
                    'Revenue' AS Type
                FROM reservation_payments
                WHERE PaymentStatus = 'Completed'
                  AND YEAR(PaymentDate) = {currentYear}
                  AND AmountPaid IS NOT NULL
                  AND AmountPaid > 0
            ")
        End If

        ' Check and add sales table
        If TableExists("sales") Then
            queries.Add($"
                SELECT 
                    MONTH(sales_date) AS MonthNum, 
                    revenue AS Amount, 
                    'Revenue' AS Type
                FROM sales
                WHERE YEAR(sales_date) = {currentYear}
                  AND revenue IS NOT NULL
                  AND revenue > 0
            ")

            queries.Add($"
                SELECT 
                    MONTH(sales_date) AS MonthNum, 
                    expenses AS Amount, 
                    'Expenses' AS Type
                FROM sales
                WHERE YEAR(sales_date) = {currentYear}
                  AND expenses IS NOT NULL
                  AND expenses > 0
            ")
        End If

        If queries.Count = 0 Then
            Throw New Exception("No valid payment tables found")
        End If

        ' Combine queries
        Dim combinedQuery As String = $"
            SELECT 
                MonthNum,
                COALESCE(SUM(CASE WHEN Type='Revenue' THEN Amount ELSE 0 END), 0) AS TotalRevenue,
                COALESCE(SUM(CASE WHEN Type='Expenses' THEN Amount ELSE 0 END), 0) AS TotalExpenses
            FROM (
                {String.Join(" UNION ALL ", queries)}
            ) AS combined
            GROUP BY MonthNum
            ORDER BY MonthNum
        "

        Return combinedQuery
    End Function

    ' =======================================================================
    ' CHECK IF TABLE EXISTS
    ' =======================================================================
    Private Function TableExists(tableName As String) As Boolean
        Try
            Dim sql As String = $"
                SELECT COUNT(*) 
                FROM information_schema.tables 
                WHERE table_schema = DATABASE()
                  AND table_name = '{tableName}'
            "

            Using cmd As New MySqlCommand(sql, conn)
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        Catch
            Return False
        End Try
    End Function

    ' =======================================================================
    ' INITIALIZE CHART WITH EMPTY DATA
    ' =======================================================================
    Private Sub InitializeChartData()
        Chart1.Series("Revenue").Points.Clear()
        Chart1.Series("Expenses").Points.Clear()
        Chart1.Series("NetProfit").Points.Clear()
        salesData.Clear()

        For month As Integer = 1 To 12
            Dim monthName As String = New DateTime(currentYear, month, 1).ToString("MMM")
            salesData(monthName) = (0D, 0D, 0D)
            Chart1.Series("Revenue").Points.AddXY(monthName, 0)
            Chart1.Series("Expenses").Points.AddXY(monthName, 0)
            Chart1.Series("NetProfit").Points.AddXY(monthName, 0)
        Next
    End Sub

    ' =======================================================================
    ' LOAD SAMPLE DATA FOR TESTING
    ' =======================================================================
    Private Sub LoadSampleData()
        Try
            InitializeChartData()

            ' Sample data for demonstration
            Dim sampleData As New Dictionary(Of Integer, (Revenue As Decimal, Expenses As Decimal)) From {
                {1, (2250000D, 1600000D)},
                {2, (2600000D, 1750000D)},
                {3, (2400000D, 1650000D)},
                {4, (3050000D, 1900000D)},
                {5, (2750000D, 1800000D)},
                {6, (3350000D, 2050000D)}
            }

            For Each kvp In sampleData
                Dim monthName As String = New DateTime(currentYear, kvp.Key, 1).ToString("MMM")
                Dim revenue As Decimal = kvp.Value.Revenue
                Dim expenses As Decimal = kvp.Value.Expenses
                Dim profit As Decimal = revenue - expenses

                salesData(monthName) = (revenue, expenses, profit)

                Dim pointIndex As Integer = kvp.Key - 1
                Chart1.Series("Revenue").Points(pointIndex).YValues(0) = revenue
                Chart1.Series("Expenses").Points(pointIndex).YValues(0) = expenses
                Chart1.Series("NetProfit").Points(pointIndex).YValues(0) = profit
            Next

        Catch ex As Exception
            MessageBox.Show($"Error loading sample data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' UPDATE SUMMARY CARDS
    ' =======================================================================
    Private Sub UpdateSummaryCards()
        Try
            Dim totalRevenue As Decimal = 0
            Dim totalExpenses As Decimal = 0
            Dim totalProfit As Decimal = 0

            For Each kvp In salesData.Values
                totalRevenue += kvp.Revenue
                totalExpenses += kvp.Expenses
                totalProfit += kvp.Profit
            Next

            lblTotalRevenue.Text = $"₱{totalRevenue:N2}"
            Label11.Text = $"₱{totalExpenses:N2}"
            Label14.Text = $"₱{totalProfit:N2}"

            UpdateTrendIndicator(PictureBox1, totalRevenue)
            UpdateTrendIndicator(PictureBox7, totalExpenses)
            UpdateTrendIndicator(PictureBox9, totalProfit)

        Catch ex As Exception
            MessageBox.Show($"Error updating summary cards: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' UPDATE TREND INDICATOR
    ' =======================================================================
    Private Sub UpdateTrendIndicator(pictureBox As PictureBox, currentValue As Decimal)
        If currentValue > 0 Then
            pictureBox.BackColor = Color.FromArgb(220, 252, 231)
        Else
            pictureBox.BackColor = Color.FromArgb(254, 226, 226)
        End If
    End Sub

    ' =======================================================================
    ' EXPORT CHART
    ' =======================================================================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "PNG Image|*.png|JPEG Image|*.jpg",
                .Title = "Export Chart",
                .FileName = $"Sales_Report_{DateTime.Now:yyyy-MM-dd}"
            }

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Dim bmp As New Bitmap(Chart1.Width, Chart1.Height)
                Chart1.DrawToBitmap(bmp, New Rectangle(0, 0, Chart1.Width, Chart1.Height))
                bmp.Save(saveDialog.FileName)
                bmp.Dispose()

                MessageBox.Show("Chart exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show($"Export Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' REFRESH DATA
    ' =======================================================================
    Public Sub RefreshData()
        LoadAndDisplaySalesData()
        UpdateSummaryCards()
    End Sub

    ' =======================================================================
    ' SET YEAR
    ' =======================================================================
    Public Sub SetYear(year As Integer)
        currentYear = year
        Label1.Text = $"Financial Overview - Monthly ({year})"
        RefreshData()
    End Sub

    ' =======================================================================
    ' CLEANUP
    ' =======================================================================
    Private Sub FormSales_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Catch
        End Try
    End Sub

End Class