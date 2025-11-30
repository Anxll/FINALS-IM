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
            If Panel1 IsNot Nothing Then
                Panel1.Visible = False
                Panel1.SendToBack()
            End If

            RoundedPane21.BringToFront()
            RoundedPane22.BringToFront()
            RoundedPane23.BringToFront()
            RoundedPane24.BringToFront()

            ConfigureChart()
            LoadAndDisplaySalesData()
            UpdateSummaryCards()

        Catch ex As Exception
            MessageBox.Show($"Form Load Error: {ex.Message}{vbCrLf}{ex.StackTrace}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                Try : openConn()
                Catch
                    MessageBox.Show("Unable to open DB connection.")
                    LoadSampleData()
                    Return
                End Try
            End If

            ' *** FIXED: Create snapshot table AFTER connection is confirmed open ***
            EnsureOrderItemPriceSnapshotInfrastructure()

            If Not TablesExist() Then
                MessageBox.Show("Required tables not found. Showing sample data.")
                LoadSampleData()
                Return
            End If

            Dim sql As String = BuildSalesQuery()

            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    InitializeChartData()

                    Dim hasRows As Boolean = False

                    While reader.Read()
                        hasRows = True

                        Dim monthNum As Integer = Convert.ToInt32(reader("MonthNum"))
                        Dim monthName As String = New DateTime(currentYear, monthNum, 1).ToString("MMM")

                        Dim revenue As Decimal = If(IsDBNull(reader("TotalRevenue")), 0D, reader("TotalRevenue"))
                        Dim expenses As Decimal = If(IsDBNull(reader("TotalExpenses")), 0D, reader("TotalExpenses"))
                        Dim profit As Decimal = revenue - expenses

                        salesData(monthName) = (revenue, expenses, profit)

                        Dim i As Integer = monthNum - 1
                        Chart1.Series("Revenue").Points(i).YValues(0) = revenue
                        Chart1.Series("Expenses").Points(i).YValues(0) = expenses
                        Chart1.Series("NetProfit").Points(i).YValues(0) = profit
                    End While

                    If Not hasRows Then
                        MessageBox.Show("No sales data found for this year.")
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading sales data: " & ex.Message)
            LoadSampleData()
        End Try
    End Sub

    ' =======================================================================
    ' TABLE CHECKER
    ' =======================================================================
    Private Function TablesExist() As Boolean
        Return TableExists("payments") OrElse
               TableExists("reservation_payments") OrElse
               TableExists("sales")
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
    ' SALES QUERY BUILDER
    ' =======================================================================
    Private Function BuildSalesQuery() As String
        Dim q As New List(Of String)

        If TableExists("payments") Then
            q.Add("
                SELECT MONTH(PaymentDate) AS MonthNum, AmountPaid AS Amount, 'Revenue' AS Type
                FROM payments
                WHERE PaymentStatus IN ('Paid','Completed')
                AND YEAR(PaymentDate) = " & currentYear & "
            ")
        End If

        If TableExists("reservation_payments") Then
            q.Add("
                SELECT MONTH(PaymentDate) AS MonthNum, AmountPaid AS Amount, 'Revenue' AS Type
                FROM reservation_payments
                WHERE PaymentStatus IN ('Paid','Completed')
                AND YEAR(PaymentDate) = " & currentYear & "
            ")
        End If

        If TableExists("sales") Then
            q.Add("
                SELECT MONTH(sales_date) AS MonthNum, revenue AS Amount, 'Revenue' AS Type
                FROM sales
                WHERE YEAR(sales_date) = " & currentYear
            )

            q.Add("
                SELECT MONTH(sales_date) AS MonthNum, expenses AS Amount, 'Expenses' AS Type
                FROM sales
                WHERE YEAR(sales_date) = " & currentYear
            )
        End If

        ' Add inventory batches as expenses
        If TableExists("inventory_batches") Then
            q.Add("
                SELECT MONTH(PurchaseDate) AS MonthNum, TotalCost AS Amount, 'Expenses' AS Type
                FROM inventory_batches
                WHERE BatchStatus = 'Active'
                AND YEAR(PurchaseDate) = " & currentYear & "
            ")
        End If

        If q.Count = 0 Then Throw New Exception("No valid tables found.")

        Return "
            SELECT 
                MonthNum,
                SUM(CASE WHEN Type='Revenue' THEN Amount ELSE 0 END) AS TotalRevenue,
                SUM(CASE WHEN Type='Expenses' THEN Amount ELSE 0 END) AS TotalExpenses
            FROM (" & String.Join(" UNION ALL ", q) & ") AS c
            GROUP BY MonthNum ORDER BY MonthNum
        "
    End Function

    ' =======================================================================
    ' INITIAL EMPTY CHART
    ' =======================================================================
    Private Sub InitializeChartData()
        Chart1.Series("Revenue").Points.Clear()
        Chart1.Series("Expenses").Points.Clear()
        Chart1.Series("NetProfit").Points.Clear()
        salesData.Clear()

        For month As Integer = 1 To 12
            Dim name As String = New DateTime(currentYear, month, 1).ToString("MMM")
            salesData(name) = (0, 0, 0)
            Chart1.Series("Revenue").Points.AddXY(name, 0)
            Chart1.Series("Expenses").Points.AddXY(name, 0)
            Chart1.Series("NetProfit").Points.AddXY(name, 0)
        Next
    End Sub

    ' =======================================================================
    ' SAMPLE DATA (if DB fails)
    ' =======================================================================
    Private Sub LoadSampleData()
        InitializeChartData()

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

            Chart1.Series("Revenue").Points(kv.Key - 1).YValues(0) = revenue
            Chart1.Series("Expenses").Points(kv.Key - 1).YValues(0) = expenses
            Chart1.Series("NetProfit").Points(kv.Key - 1).YValues(0) = profit
        Next
    End Sub

    ' =======================================================================
    ' SUMMARY CARDS
    ' =======================================================================
    Private Sub UpdateSummaryCards()
        Dim tRev As Decimal = 0
        Dim tExp As Decimal = 0
        Dim tPro As Decimal = 0

        For Each v In salesData.Values
            tRev += v.Revenue
            tExp += v.Expenses
            tPro += v.Profit
        Next

        ' Calculate total cost from inventory_batches
        Dim totalInventoryCost As Decimal = GetTotalInventoryCost()

        lblTotalRevenue.Text = $"₱{tRev:N2}"
        Label11.Text = $"₱{totalInventoryCost:N2}"
        Label14.Text = $"₱{tPro:N2}"


    End Sub



    ' =======================================================================
    ' GET TOTAL INVENTORY COST
    ' =======================================================================
    Private Function GetTotalInventoryCost() As Decimal
        Try
            If conn Is Nothing OrElse conn.State <> ConnectionState.Open Then
                Return 0D
            End If

            If Not TableExists("inventory_batches") Then
                Return 0D
            End If

            ' Calculate: TotalCost = Sum of (TotalCost) for all active batches
            Dim sql As String = "
                SELECT COALESCE(SUM(TotalCost), 0) AS TotalCost
                FROM inventory_batches
                WHERE BatchStatus = 'Active'
                AND YEAR(PurchaseDate) = @Year
            "

            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@Year", currentYear)
                Dim result = cmd.ExecuteScalar()
                Return If(IsDBNull(result), 0D, Convert.ToDecimal(result))
            End Using

        Catch ex As Exception
            MessageBox.Show("Error calculating inventory cost: " & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return 0D
        End Try
    End Function

    ' =======================================================================
    ' EXPORT CHART
    ' =======================================================================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim dlg As New SaveFileDialog With {
                .Filter = "PNG|*.png|JPEG|*.jpg",
                .FileName = "Sales_Report_" & DateTime.Now.ToString("yyyy-MM-dd")
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
        LoadAndDisplaySalesData()
        UpdateSummaryCards()
    End Sub

    ' =======================================================================
    ' CHANGE YEAR
    ' =======================================================================
    Public Sub SetYear(year As Integer)
        currentYear = year
        Label1.Text = $"Financial Overview - Monthly ({year})"
        RefreshData()
    End Sub

    ' =======================================================================
    ' PRICE SNAPSHOT (ENSURES YOU SAVE OLD PRICES)
    ' =======================================================================
    Private Sub EnsureOrderItemPriceSnapshotInfrastructure()
        Try
            Dim sql As String =
"
CREATE TABLE IF NOT EXISTS order_item_price_snapshot (
    snapshot_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    price_at_order DECIMAL(10,2) NOT NULL,
    quantity INT NOT NULL,
    date_recorded DATETIME DEFAULT CURRENT_TIMESTAMP
);
"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MessageBox.Show("Snapshot Table Error: " & ex.Message)
        End Try
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