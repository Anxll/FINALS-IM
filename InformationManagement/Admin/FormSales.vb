Imports System.Windows.Forms.DataVisualization.Charting
Imports MySqlConnector

Public Class FormSales

    ' Shared filter state (synced with Reports)
    Public Shared SelectedPeriod As String = "Monthly" ' Daily, Weekly, Monthly, Yearly
    Public Shared SelectedDateRange As String = "This Year" ' Today, Yesterday, This Week, etc.
    Public Shared CustomStartDate As DateTime = DateTime.Now.Date
    Public Shared CustomEndDate As DateTime = DateTime.Now.Date

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
    ' HELPER FUNCTIONS (MATCHING REPORTS EXACTLY)
    ' =======================================================================
    Public Shared Function GetDateGrouping(dateColumn As String) As String
        Select Case SelectedPeriod
            Case "Daily"
                Return $"DATE({dateColumn})"
            Case "Weekly"
                Return $"YEARWEEK({dateColumn}, 1)"
            Case "Monthly"
                Return $"DATE_FORMAT({dateColumn}, '%Y-%m')"
            Case "Yearly"
                Return $"YEAR({dateColumn})"
            Case Else
                Return $"DATE({dateColumn})"
        End Select
    End Function

    Public Shared Function GetDateDisplayFormat(dateValue As Object) As String
        Select Case SelectedPeriod
            Case "Daily"
                Return Convert.ToDateTime(dateValue).ToString("MMM dd, yyyy")
            Case "Weekly"
                Return $"Week {dateValue}"
            Case "Monthly"
                Return Convert.ToDateTime(dateValue & "-01").ToString("MMM yyyy")
            Case "Yearly"
                Return dateValue.ToString()
            Case Else
                Return dateValue.ToString()
        End Select
    End Function

    Public Shared Function GetDateFilterCondition(dateColumn As String) As String
        Select Case SelectedDateRange
            Case "Today"
                Return $"DATE({dateColumn}) = CURDATE()"
            Case "Yesterday"
                Return $"DATE({dateColumn}) = DATE_SUB(CURDATE(), INTERVAL 1 DAY)"
            Case "This Week"
                Return $"YEARWEEK({dateColumn}, 1) = YEARWEEK(CURDATE(), 1)"
            Case "Last Week"
                Return $"YEARWEEK({dateColumn}, 1) = YEARWEEK(DATE_SUB(CURDATE(), INTERVAL 1 WEEK), 1)"
            Case "This Month"
                Return $"YEAR({dateColumn}) = YEAR(CURDATE()) AND MONTH({dateColumn}) = MONTH(CURDATE())"
            Case "Last Month"
                Return $"YEAR({dateColumn}) = YEAR(DATE_SUB(CURDATE(), INTERVAL 1 MONTH)) AND MONTH({dateColumn}) = MONTH(DATE_SUB(CURDATE(), INTERVAL 1 MONTH))"
            Case "This Year"
                Return $"YEAR({dateColumn}) = YEAR(CURDATE())"
            Case "Last Year"
                Return $"YEAR({dateColumn}) = YEAR(DATE_SUB(CURDATE(), INTERVAL 1 YEAR))"
            Case "Custom Range"
                Return $"{dateColumn} BETWEEN '{CustomStartDate:yyyy-MM-dd}' AND '{CustomEndDate:yyyy-MM-dd}'"
            Case "All Time"
                Return "1=1"
            Case Else
                Return $"YEAR({dateColumn}) = YEAR(CURDATE())"
        End Select
    End Function

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

            EnsureOrderItemPriceSnapshotInfrastructure()

            If Not TablesExist() Then
                MessageBox.Show("Required tables not found. Showing sample data.")
                LoadSampleData()
                Return
            End If

            Dim sql As String = BuildSalesQuery()

            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    ClearChartData()
                    salesData.Clear()

                    Dim hasRows As Boolean = False

                    While reader.Read()
                        hasRows = True

                        Dim periodKey As Object = reader("PeriodKey")
                        Dim displayLabel As String = GetDateDisplayFormat(periodKey)

                        Dim revenue As Decimal = If(IsDBNull(reader("TotalRevenue")), 0D, reader("TotalRevenue"))
                        Dim expenses As Decimal = If(IsDBNull(reader("TotalExpenses")), 0D, reader("TotalExpenses"))
                        Dim profit As Decimal = revenue - expenses

                        salesData(displayLabel) = (revenue, expenses, profit)

                        Chart1.Series("Revenue").Points.AddXY(displayLabel, revenue)
                        Chart1.Series("Expenses").Points.AddXY(displayLabel, expenses)
                        Chart1.Series("NetProfit").Points.AddXY(displayLabel, profit)
                    End While

                    If Not hasRows Then
                        MessageBox.Show("No sales data found for this period.")
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading sales data: " & ex.Message & vbCrLf & ex.StackTrace)
            LoadSampleData()
        End Try
    End Sub

    ' =======================================================================
    ' TABLE CHECKER
    ' =======================================================================
    Private Function TablesExist() As Boolean
        Return TableExists("orders") OrElse
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
    ' SALES QUERY BUILDER (USING REPORTS FILTERS)
    ' =======================================================================
    Private Function BuildSalesQuery() As String
        Dim q As New List(Of String)
        Dim dateGrouping As String = GetDateGrouping("DateColumn") ' Placeholder, will be replaced

        ' Revenue from Orders
        If TableExists("orders") Then
            Dim orderFilter As String = GetDateFilterCondition("OrderDate")
            Dim orderGrouping As String = GetDateGrouping("OrderDate")
            q.Add($"
                SELECT {orderGrouping} AS PeriodKey, TotalAmount AS Amount, 'Revenue' AS Type
                FROM orders
                WHERE OrderStatus = 'Completed'
                AND {orderFilter}
            ")
        End If

        ' Revenue from Reservation Payments
        If TableExists("reservation_payments") Then
            Dim paymentFilter As String = GetDateFilterCondition("PaymentDate")
            Dim paymentGrouping As String = GetDateGrouping("PaymentDate")
            q.Add($"
                SELECT {paymentGrouping} AS PeriodKey, AmountPaid AS Amount, 'Revenue' AS Type
                FROM reservation_payments
                WHERE PaymentStatus IN ('Paid','Completed')
                AND {paymentFilter}
            ")
        End If

        ' Legacy payments table
        If TableExists("payments") Then
            Dim paymentFilter As String = GetDateFilterCondition("PaymentDate")
            Dim paymentGrouping As String = GetDateGrouping("PaymentDate")
            q.Add($"
                SELECT {paymentGrouping} AS PeriodKey, AmountPaid AS Amount, 'Revenue' AS Type
                FROM payments
                WHERE PaymentStatus IN ('Paid','Completed')
                AND {paymentFilter}
            ")
        End If

        ' Legacy sales table
        If TableExists("sales") Then
            Dim salesFilter As String = GetDateFilterCondition("sales_date")
            Dim salesGrouping As String = GetDateGrouping("sales_date")
            q.Add($"
                SELECT {salesGrouping} AS PeriodKey, revenue AS Amount, 'Revenue' AS Type
                FROM sales
                WHERE {salesFilter}
            ")

            q.Add($"
                SELECT {salesGrouping} AS PeriodKey, expenses AS Amount, 'Expenses' AS Type
                FROM sales
                WHERE {salesFilter}
            ")
        End If

        ' Expenses from inventory batches
        If TableExists("inventory_batches") Then
            Dim purchaseFilter As String = GetDateFilterCondition("PurchaseDate")
            Dim purchaseGrouping As String = GetDateGrouping("PurchaseDate")
            q.Add($"
                SELECT {purchaseGrouping} AS PeriodKey, TotalCost AS Amount, 'Expenses' AS Type
                FROM inventory_batches
                WHERE BatchStatus = 'Active'
                AND {purchaseFilter}
            ")
        End If

        If q.Count = 0 Then Throw New Exception("No valid tables found.")

        Return "
            SELECT 
                PeriodKey,
                SUM(CASE WHEN Type='Revenue' THEN Amount ELSE 0 END) AS TotalRevenue,
                SUM(CASE WHEN Type='Expenses' THEN Amount ELSE 0 END) AS TotalExpenses
            FROM (" & String.Join(" UNION ALL ", q) & ") AS c
            GROUP BY PeriodKey ORDER BY PeriodKey
        "
    End Function

    ' =======================================================================
    ' CLEAR CHART DATA
    ' =======================================================================
    Private Sub ClearChartData()
        Chart1.Series("Revenue").Points.Clear()
        Chart1.Series("Expenses").Points.Clear()
        Chart1.Series("NetProfit").Points.Clear()
    End Sub

    ' =======================================================================
    ' SAMPLE DATA (if DB fails)
    ' =======================================================================
    Private Sub LoadSampleData()
        ClearChartData()
        salesData.Clear()

        Dim sample = New Dictionary(Of String, (Decimal, Decimal)) From {
            {"Jan 2024", (2250000, 1600000)},
            {"Feb 2024", (2600000, 1750000)},
            {"Mar 2024", (2400000, 1650000)},
            {"Apr 2024", (3050000, 1900000)},
            {"May 2024", (2750000, 1800000)},
            {"Jun 2024", (3350000, 2050000)}
        }

        For Each kv In sample
            Dim revenue = kv.Value.Item1
            Dim expenses = kv.Value.Item2
            Dim profit = revenue - expenses

            salesData(kv.Key) = (revenue, expenses, profit)

            Chart1.Series("Revenue").Points.AddXY(kv.Key, revenue)
            Chart1.Series("Expenses").Points.AddXY(kv.Key, expenses)
            Chart1.Series("NetProfit").Points.AddXY(kv.Key, profit)
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

            Dim purchaseFilter As String = GetDateFilterCondition("PurchaseDate")

            Dim sql As String = $"
                SELECT COALESCE(SUM(TotalCost), 0) AS TotalCost
                FROM inventory_batches
                WHERE BatchStatus = 'Active'
                AND {purchaseFilter}
            "

            Using cmd As New MySqlCommand(sql, conn)
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
    ' REFRESH DATA (PUBLIC - Called from Reports)
    ' =======================================================================
    Public Sub RefreshData()
        LoadAndDisplaySalesData()
        UpdateSummaryCards()
        UpdateLabel()
    End Sub

    ' =======================================================================
    ' UPDATE LABEL TO SHOW CURRENT FILTER
    ' =======================================================================
    Private Sub UpdateLabel()
        Label1.Text = $"Financial Overview - {SelectedPeriod} ({SelectedDateRange})"
    End Sub

    ' =======================================================================
    ' PUBLIC METHODS TO UPDATE FILTERS (Called from Reports Form)
    ' =======================================================================
    Public Sub SetFilters(period As String, dateRange As String, Optional customStart As DateTime = Nothing, Optional customEnd As DateTime = Nothing)
        SelectedPeriod = period
        SelectedDateRange = dateRange

        If dateRange = "Custom Range" Then
            If customStart <> Nothing Then CustomStartDate = customStart
            If customEnd <> Nothing Then CustomEndDate = customEnd
        End If

        RefreshData()
    End Sub

    ' =======================================================================
    ' PRICE SNAPSHOT
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