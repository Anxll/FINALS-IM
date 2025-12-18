Imports System.Windows.Forms.DataVisualization.Charting
Imports MySqlConnector

Public Class FormSales

    Private currentYear As Integer = DateTime.Now.Year
    Private salesData As New Dictionary(Of String, (Revenue As Decimal, Expenses As Decimal, Profit As Decimal))
    Private currentPeriod As String = "Daily"

    ' =======================================================================
    ' FORM LOAD
    ' =======================================================================
    Private Sub FormSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Panel1 IsNot Nothing Then
                Panel1.Visible = False
                Panel1.SendToBack()
            End If

            RoundedPane21.BringToFront()
            RoundedPane22.BringToFront()
            RoundedPane23.BringToFront()
            RoundedPane24.BringToFront()

            ' Get the selected period from Reports form
            currentPeriod = Reports.SelectedPeriod

            ConfigureChart()
            LoadAndDisplaySalesData()
            UpdateSummaryCards()

            ' Update label to show current period
            UpdateHeaderLabel()

        Catch ex As Exception
            MessageBox.Show($"Form Load Error: {ex.Message}{vbCrLf}{ex.StackTrace}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' UPDATE HEADER LABEL
    ' =======================================================================
    Private Sub UpdateHeaderLabel()
        Try
            If Label1 IsNot Nothing Then
                Label1.Text = $"Financial Overview - {currentPeriod} ({currentYear})"
            End If
        Catch ex As Exception
            ' Silently handle if Label1 doesn't exist
        End Try
    End Sub

    ' =======================================================================
    ' CHART CONFIG
    ' =======================================================================
    Private Sub ConfigureChart()
        Try
            With Chart1
                .ChartAreas(0).AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230)
                .ChartAreas(0).AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot
                .ChartAreas(0).AxisX.LabelStyle.Font = New Font("Segoe UI", 9)
                .ChartAreas(0).AxisX.Interval = 1

                ' Angle labels for better readability based on period
                If currentPeriod = "Daily" Then
                    .ChartAreas(0).AxisX.LabelStyle.Angle = -45
                Else
                    .ChartAreas(0).AxisX.LabelStyle.Angle = 0
                End If

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
                    series.ToolTip = "#VALX: ₱#VALY{N2}"
                Next

                .Legends(0).Font = New Font("Segoe UI", 9)
                .Legends(0).Docking = Docking.Bottom
            End With

        Catch ex As Exception
            MessageBox.Show($"Chart Config Error: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' MAIN LOAD FUNCTION
    ' =======================================================================
    Private Sub LoadAndDisplaySalesData()
        Try
            If conn Is Nothing Then
                MessageBox.Show("Database connection is missing.", "Connection Error")
                LoadSampleData()
                Return
            End If

            If conn.State <> ConnectionState.Open Then
                Try
                    openConn()
                Catch
                    MessageBox.Show("Unable to open DB connection.")
                    LoadSampleData()
                    Return
                End Try
            End If

            ' EnsureOrderItemPriceSnapshotInfrastructure() - Removed to avoid creating tables

            If Not TablesExist() Then
                MessageBox.Show("Required tables not found. Showing sample data.")
                LoadSampleData()
                Return
            End If

            Dim sql As String = BuildSalesQuery()

            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@Year", currentYear)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    InitializeChartData()

                    Dim hasRows As Boolean = False

                    While reader.Read()
                        hasRows = True

                        Dim periodValue As Object = reader("PeriodGroup")
                        Dim periodLabel As String = GetPeriodLabel(periodValue)
                        Dim revenue As Decimal = If(IsDBNull(reader("TotalRevenue")), 0D, Convert.ToDecimal(reader("TotalRevenue")))
                        Dim expenses As Decimal = If(IsDBNull(reader("TotalExpenses")), 0D, Convert.ToDecimal(reader("TotalExpenses")))
                        Dim profit As Decimal = revenue - expenses

                        salesData(periodLabel) = (revenue, expenses, profit)

                        ' Add data points to chart
                        Chart1.Series("Revenue").Points.AddXY(periodLabel, revenue)
                        Chart1.Series("Expenses").Points.AddXY(periodLabel, expenses)
                        Chart1.Series("NetProfit").Points.AddXY(periodLabel, profit)
                    End While

                    If Not hasRows Then
                        ' MessageBox.Show($"No sales data found for {currentPeriod} period in {currentYear}.")
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading sales data: " & ex.Message & vbCrLf & ex.StackTrace)
            LoadSampleData()
        End Try
    End Sub

    ' =======================================================================
    ' GET PERIOD LABEL FOR DISPLAY
    ' =======================================================================
    Private Function GetPeriodLabel(periodValue As Object) As String
        If IsDBNull(periodValue) OrElse periodValue Is Nothing Then Return "N/A"

        Try
            Select Case currentPeriod
                Case "Daily"
                    ' Format: "Jan 15"
                    Return Convert.ToDateTime(periodValue).ToString("MMM dd")

                Case "Weekly"
                    ' Format: "Week 52"
                    Return $"Week {periodValue}"

                Case "Monthly"
                    ' periodValue will be month number (1-12)
                    Dim monthNum As Integer = Convert.ToInt32(periodValue)
                    Return New DateTime(currentYear, monthNum, 1).ToString("MMM")

                Case "Yearly"
                    ' Format: "2025"
                    Return periodValue.ToString()

                Case Else
                    Return periodValue.ToString()
            End Select
        Catch ex As Exception
            Return "Invalid"
        End Try
    End Function

    ' =======================================================================
    ' TABLE CHECKER
    ' =======================================================================
    Private Function TablesExist() As Boolean
        ' We primarily rely on orders and reservation_payments now, which should exist.
        Return TableExists("orders") OrElse
               TableExists("reservation_payments")
    End Function

    Private Function TableExists(tableName As String) As Boolean
        Try
            Dim sql = "
                SELECT COUNT(*) FROM information_schema.tables
                WHERE table_schema = DATABASE()
                AND LOWER(table_name) = LOWER(@TableName)
            "

            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TableName", tableName)
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using

        Catch
            Return False
        End Try
    End Function

    ' =======================================================================
    ' SALES QUERY BUILDER - WITH PERIOD FILTER
    ' =======================================================================
    Private Function BuildSalesQuery() As String
        Dim q As New List(Of String)

        ' ORDERS TABLE - Revenue (Matches Dashboard Logic)
        Dim orderDateGrouping As String = GetDateGroupingForColumn("OrderDate")
        q.Add($"
            SELECT {orderDateGrouping} AS PeriodGroup, TotalAmount AS Amount, 'Revenue' AS Type
            FROM orders
            WHERE OrderStatus = 'Completed'
            AND YEAR(OrderDate) = @Year
        ")

        ' RESERVATION_PAYMENTS TABLE - Revenue
        If TableExists("reservation_payments") Then
            Dim resDateGrouping As String = GetDateGroupingForColumn("PaymentDate")
            q.Add($"
                SELECT {resDateGrouping} AS PeriodGroup, AmountPaid AS Amount, 'Revenue' AS Type
                FROM reservation_payments
                WHERE PaymentStatus IN ('Paid','Completed')
                AND YEAR(PaymentDate) = @Year
            ")
        End If

        ' INVENTORY_BATCHES TABLE - Expenses
        If TableExists("inventory_batches") Then
            Dim purchaseDateGrouping As String = GetDateGroupingForColumn("PurchaseDate")

            q.Add($"
                SELECT {purchaseDateGrouping} AS PeriodGroup, TotalCost AS Amount, 'Expenses' AS Type
                FROM inventory_batches
                WHERE BatchStatus = 'Active'
                AND YEAR(PurchaseDate) = @Year
            ")
        End If

        If q.Count = 0 Then Throw New Exception("No valid tables found.")

        Return $"
            SELECT 
                PeriodGroup,
                SUM(CASE WHEN Type='Revenue' THEN Amount ELSE 0 END) AS TotalRevenue,
                SUM(CASE WHEN Type='Expenses' THEN Amount ELSE 0 END) AS TotalExpenses
            FROM ({String.Join(" UNION ALL ", q)}) AS c
            WHERE PeriodGroup IS NOT NULL
            GROUP BY PeriodGroup 
            ORDER BY PeriodGroup
        "
    End Function

    ' =======================================================================
    ' GET DATE GROUPING FOR SQL COLUMN
    ' =======================================================================
    Private Function GetDateGroupingForColumn(columnName As String) As String
        Select Case currentPeriod
            Case "Daily"
                Return $"DATE({columnName})"

            Case "Weekly"
                Return $"YEARWEEK({columnName}, 1)"

            Case "Monthly"
                Return $"MONTH({columnName})"

            Case "Yearly"
                Return $"YEAR({columnName})"

            Case Else
                Return $"DATE({columnName})"
        End Select
    End Function

    ' =======================================================================
    ' INITIAL EMPTY CHART
    ' =======================================================================
    Private Sub InitializeChartData()
        Chart1.Series("Revenue").Points.Clear()
        Chart1.Series("Expenses").Points.Clear()
        Chart1.Series("NetProfit").Points.Clear()
        salesData.Clear()
    End Sub

    ' =======================================================================
    ' SAMPLE DATA (if DB fails)
    ' =======================================================================
    Private Sub LoadSampleData()
        InitializeChartData()

        Select Case currentPeriod
            Case "Monthly"
                ' Monthly sample data
                Dim sample = New Dictionary(Of Integer, (Decimal, Decimal)) From {
                    {1, (2250000, 1600000)},
                    {2, (2600000, 1750000)},
                    {3, (2400000, 1650000)},
                    {4, (3050000, 1900000)},
                    {5, (2750000, 1800000)},
                    {6, (3350000, 2050000)}
                }

                For Each kv In sample
                    Dim name As String = New DateTime(currentYear, kv.Key, 1).ToString("MMM")
                    Dim revenue = kv.Value.Item1
                    Dim expenses = kv.Value.Item2
                    Dim profit = revenue - expenses

                    salesData(name) = (revenue, expenses, profit)
                    Chart1.Series("Revenue").Points.AddXY(name, revenue)
                    Chart1.Series("Expenses").Points.AddXY(name, expenses)
                    Chart1.Series("NetProfit").Points.AddXY(name, profit)
                Next

            Case "Daily"
                ' Show last 7 days sample
                For i As Integer = 6 To 0 Step -1
                    Dim dt As DateTime = DateTime.Now.AddDays(-i)
                    Dim name As String = dt.ToString("MMM dd")
                    Dim revenue As Decimal = 50000 + (i * 5000)
                    Dim expenses As Decimal = 35000 + (i * 3000)
                    Dim profit As Decimal = revenue - expenses

                    salesData(name) = (revenue, expenses, profit)
                    Chart1.Series("Revenue").Points.AddXY(name, revenue)
                    Chart1.Series("Expenses").Points.AddXY(name, expenses)
                    Chart1.Series("NetProfit").Points.AddXY(name, profit)
                Next

            Case "Weekly"
                ' Show last 8 weeks sample
                For i As Integer = 1 To 8
                    Dim name As String = $"Week {i}"
                    Dim revenue As Decimal = 300000 + (i * 20000)
                    Dim expenses As Decimal = 200000 + (i * 15000)
                    Dim profit As Decimal = revenue - expenses

                    salesData(name) = (revenue, expenses, profit)
                    Chart1.Series("Revenue").Points.AddXY(name, revenue)
                    Chart1.Series("Expenses").Points.AddXY(name, expenses)
                    Chart1.Series("NetProfit").Points.AddXY(name, profit)
                Next

            Case "Yearly"
                ' Show last 5 years sample
                For i As Integer = 4 To 0 Step -1
                    Dim year As Integer = currentYear - i
                    Dim name As String = year.ToString()
                    Dim revenue As Decimal = 20000000 + (i * 2000000)
                    Dim expenses As Decimal = 15000000 + (i * 1500000)
                    Dim profit As Decimal = revenue - expenses

                    salesData(name) = (revenue, expenses, profit)
                    Chart1.Series("Revenue").Points.AddXY(name, revenue)
                    Chart1.Series("Expenses").Points.AddXY(name, expenses)
                    Chart1.Series("NetProfit").Points.AddXY(name, profit)
                Next
        End Select
    End Sub

    ' =======================================================================
    ' SUMMARY CARDS - WITH PERIOD FILTER APPLIED TO ALL DATA
    ' =======================================================================
    Private Sub UpdateSummaryCards()
        Try
            ' Get filtered revenue and expenses from database
            Dim filteredRevenue As Decimal = GetFilteredRevenue()
            Dim filteredExpenses As Decimal = GetFilteredExpenses()
            Dim filteredProfit As Decimal = filteredRevenue - filteredExpenses

            ' Update labels if they exist
            If lblTotalRevenue IsNot Nothing Then
                lblTotalRevenue.Text = $"₱{filteredRevenue:N2}"
            End If

            If Label11 IsNot Nothing Then
                Label11.Text = $"₱{filteredExpenses:N2}"
            End If

            If Label14 IsNot Nothing Then
                Label14.Text = $"₱{filteredProfit:N2}"
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating summary cards: " & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub


    ' =======================================================================
    ' GET FILTERED REVENUE - ALL SOURCES WITH PERIOD FILTER
    ' =======================================================================
    Private Function GetFilteredRevenue() As Decimal
        Try
            If conn Is Nothing OrElse conn.State <> ConnectionState.Open Then
                Return 0D
            End If

            Dim q As New List(Of String)

            ' ORDERS TABLE - Revenue (Matches Dashboard)
            Dim whereClauseOrders As String = GetPeriodWhereClause("OrderDate")
            q.Add($"
                SELECT COALESCE(SUM(TotalAmount), 0) AS Amount
                FROM orders
                WHERE OrderStatus = 'Completed'
                {whereClauseOrders}
            ")

            ' RESERVATION_PAYMENTS TABLE - Revenue
            If TableExists("reservation_payments") Then
                Dim whereClause As String = GetPeriodWhereClause("PaymentDate")
                q.Add($"
                    SELECT COALESCE(SUM(AmountPaid), 0) AS Amount
                    FROM reservation_payments
                    WHERE PaymentStatus IN ('Paid','Completed')
                    {whereClause}
                ")
            End If

            If q.Count = 0 Then Return 0D

            Dim unionQuery As String = String.Join(" UNION ALL ", q)
            Dim finalQuery As String = $"SELECT SUM(Amount) AS TotalRevenue FROM ({unionQuery}) AS revenue_sources"

            Using cmd As New MySqlCommand(finalQuery, conn)
                AddPeriodParameters(cmd)
                Dim result = cmd.ExecuteScalar()
                Return If(IsDBNull(result) OrElse result Is Nothing, 0D, Convert.ToDecimal(result))
            End Using

        Catch ex As Exception
            MessageBox.Show("Error calculating filtered revenue: " & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return 0D
        End Try
    End Function

    ' =======================================================================
    ' GET FILTERED EXPENSES - ALL SOURCES WITH PERIOD FILTER
    ' =======================================================================
    Private Function GetFilteredExpenses() As Decimal
        Try
            If conn Is Nothing OrElse conn.State <> ConnectionState.Open Then
                Return 0D
            End If

            Dim q As New List(Of String)

            ' INVENTORY_BATCHES TABLE - Expenses
            If TableExists("inventory_batches") Then
                Dim whereClause As String = GetPeriodWhereClause("PurchaseDate")
                q.Add($"
                    SELECT COALESCE(SUM(TotalCost), 0) AS Amount
                    FROM inventory_batches
                    WHERE BatchStatus = 'Active'
                    {whereClause}
                ")
            End If

            If q.Count = 0 Then Return 0D

            Dim unionQuery As String = String.Join(" UNION ALL ", q)
            Dim finalQuery As String = $"SELECT SUM(Amount) AS TotalExpenses FROM ({unionQuery}) AS expense_sources"

            Using cmd As New MySqlCommand(finalQuery, conn)
                AddPeriodParameters(cmd)
                Dim result = cmd.ExecuteScalar()
                Return If(IsDBNull(result) OrElse result Is Nothing, 0D, Convert.ToDecimal(result))
            End Using

        Catch ex As Exception
            MessageBox.Show("Error calculating filtered expenses: " & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return 0D
        End Try
    End Function

    ' =======================================================================
    ' GET PERIOD WHERE CLAUSE FOR FILTERING
    ' =======================================================================
    Private Function GetPeriodWhereClause(dateColumn As String) As String
        Select Case currentPeriod
            Case "Daily"
                Return $"AND YEAR({dateColumn}) = @Year AND DATE({dateColumn}) = CURDATE()"

            Case "Weekly"
                Return $"AND YEAR({dateColumn}) = @Year AND YEARWEEK({dateColumn}, 1) = YEARWEEK(CURDATE(), 1)"

            Case "Monthly"
                Return $"AND YEAR({dateColumn}) = @Year AND MONTH({dateColumn}) = MONTH(CURDATE())"

            Case "Yearly"
                Return $"AND YEAR({dateColumn}) = @Year"

            Case Else
                Return $"AND YEAR({dateColumn}) = @Year"
        End Select
    End Function

    ' =======================================================================
    ' ADD PERIOD PARAMETERS TO COMMAND
    ' =======================================================================
    Private Sub AddPeriodParameters(cmd As MySqlCommand)
        If Not cmd.Parameters.Contains("@Year") Then
            cmd.Parameters.AddWithValue("@Year", currentYear)
        End If
    End Sub



    ' =======================================================================
    ' EXPORT CHART
    ' =======================================================================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim dlg As New SaveFileDialog With {
                .Filter = "PNG|*.png|JPEG|*.jpg",
                .FileName = $"Sales_Report_{currentPeriod}_{DateTime.Now.ToString("yyyy-MM-dd")}"
            }

            If dlg.ShowDialog() = DialogResult.OK Then
                Dim bmp As New Bitmap(Chart1.Width, Chart1.Height)
                Chart1.DrawToBitmap(bmp, Chart1.ClientRectangle)
                bmp.Save(dlg.FileName)
                bmp.Dispose()

                MessageBox.Show("Chart exported successfully!")
            End If

        Catch ex As Exception
            MessageBox.Show("Export Error: " & ex.Message)
        End Try
    End Sub

    ' =======================================================================
    ' REFRESH DATA
    ' =======================================================================
    Public Sub RefreshData()
        currentPeriod = Reports.SelectedPeriod
        ConfigureChart()
        LoadAndDisplaySalesData()
        UpdateSummaryCards()
        UpdateHeaderLabel()
    End Sub

    ' =======================================================================
    ' CHANGE YEAR
    ' =======================================================================
    Public Sub SetYear(year As Integer)
        currentYear = year
        UpdateHeaderLabel()
        RefreshData()
    End Sub

    ' =======================================================================
    ' FORM CLOSING
    ' =======================================================================
    Private Sub FormSales_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then conn.Close()
        Catch
        End Try
    End Sub

End Class