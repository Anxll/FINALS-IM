Imports MySqlConnector
Imports System.Data
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Drawing.Drawing2D

Public Class FormOrders
    Private ordersData As New DataTable()
    Private currentFilter As String = "All"
    Private searchText As String = ""

    ' =======================================================================
    ' FORM LOAD
    ' =======================================================================
    Private Sub FormOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            InitializeDataGridView()
            InitializeFilters()

            LoadOrdersData()
            UpdateStatisticsFromDatabase()
            InitializeCharts()
            LoadOrdersTrendChart()
            LoadCategoriesChart()

        Catch ex As Exception
            MessageBox.Show($"Form Load Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' UPDATE STATISTICS FROM DATABASE
    ' =======================================================================
    Private Sub UpdateStatisticsFromDatabase()
        Try
            If conn Is Nothing OrElse conn.State <> ConnectionState.Open Then
                openConn()
            End If

            ' Get period filter from Reports form
            Dim periodFilter As String = ""
            Dim selectedYear As Integer = Reports.SelectedYear
            Dim selectedMonth As Integer = Reports.SelectedMonth

            Select Case Reports.SelectedPeriod
                Case "Daily"
                    If selectedYear = DateTime.Now.Year Then
                        periodFilter = " WHERE DATE(OrderDate) = CURDATE() "
                    Else
                        periodFilter = $" WHERE DATE(OrderDate) = '{selectedYear}-12-31' "
                    End If
                Case "Weekly"
                    If selectedYear = DateTime.Now.Year Then
                        periodFilter = " WHERE YEARWEEK(OrderDate, 1) = YEARWEEK(CURDATE(), 1) "
                    Else
                        periodFilter = $" WHERE YEAR(OrderDate) = {selectedYear} AND WEEK(OrderDate, 1) = 52 "
                    End If
                Case "Monthly"
                    If selectedMonth = 0 Then
                        periodFilter = $" WHERE YEAR(OrderDate) = {selectedYear} "
                    Else
                        periodFilter = $" WHERE YEAR(OrderDate) = {selectedYear} AND MONTH(OrderDate) = {selectedMonth} "
                    End If
                Case "Yearly"
                    periodFilter = $" WHERE YEAR(OrderDate) = {selectedYear} "
                Case Else
                    periodFilter = "" ' All time
            End Select

            ' Query to get statistics
            Dim statsQuery As String = $"
                SELECT 
                    COUNT(*) AS TotalOrders,
                    COALESCE(SUM(TotalAmount), 0) AS TotalRevenue,
                    COALESCE(AVG(TotalAmount), 0) AS AvgOrderValue,
                    COUNT(CASE WHEN OrderStatus = 'Pending' THEN 1 END) AS PendingCount,
                    COUNT(CASE WHEN OrderStatus = 'Confirmed' THEN 1 END) AS ConfirmedCount,
                    COUNT(CASE WHEN OrderStatus = 'Completed' THEN 1 END) AS CompletedCount,
                    COUNT(CASE WHEN OrderStatus = 'Cancelled' THEN 1 END) AS CancelledCount
                FROM orders
                {periodFilter}
            "

            Using cmd As New MySqlCommand(statsQuery, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' Update Total Orders Card (Label4)
                        Dim totalOrders As Integer = Convert.ToInt32(reader("TotalOrders"))
                        Label4.Text = totalOrders.ToString("N0")

                        ' Update Total Revenue Card (Label6)
                        Dim totalRevenue As Decimal = Convert.ToDecimal(reader("TotalRevenue"))
                        Label6.Text = totalRevenue.ToString("₱#,##0.00")

                        ' Update Average Order Value Card (Label7)
                        Dim avgOrderValue As Decimal = Convert.ToDecimal(reader("AvgOrderValue"))
                        Label7.Text = avgOrderValue.ToString("₱#,##0.00")

                        ' Optional: Store counts for future use
                        Dim pendingCount As Integer = Convert.ToInt32(reader("PendingCount"))
                        Dim confirmedCount As Integer = Convert.ToInt32(reader("ConfirmedCount"))
                        Dim completedCount As Integer = Convert.ToInt32(reader("CompletedCount"))
                        Dim cancelledCount As Integer = Convert.ToInt32(reader("CancelledCount"))
                    End If
                End Using
            End Using

        Catch ex As Exception
            ' If database fails, show default values
            Label4.Text = "0"
            Label6.Text = "₱0.00"
            Label7.Text = "₱0.00"

            MessageBox.Show($"Error loading statistics: {ex.Message}", "Warning",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ' =======================================================================
    ' LOAD ORDERS DATA FROM DATABASE
    ' =======================================================================
    Private Sub LoadOrdersData(Optional filterStatus As String = "All", Optional search As String = "")
        Try
            If conn Is Nothing Then
                MessageBox.Show("Database connection not initialized.", "Connection Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Return
            End If

            If conn.State <> ConnectionState.Open Then
                openConn()
            End If

            ' Check if orders table exists
            If Not TableExists("orders") Then
                MessageBox.Show("Orders table not found. Loading sample data.", "Table Missing",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Return
            End If

            ' Build query with filters
            Dim sql As String = BuildOrdersQuery(filterStatus, search)

            Using cmd As New MySqlCommand(sql, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    ordersData.Clear()
                    adapter.Fill(ordersData)
                End Using
            End Using

            ' Update DataGridView if it exists
            If DataGridView1 IsNot Nothing Then
                ' Reset DataSource and Columns to prevent duplicates
                DataGridView1.DataSource = Nothing
                DataGridView1.Columns.Clear()
                DataGridView1.AutoGenerateColumns = True

                DataGridView1.DataSource = ordersData
                FormatOrdersDataGridView()
            End If

        Catch ex As MySqlException
            MessageBox.Show($"Database Error: {ex.Message}{vbCrLf}Loading sample data instead.",
                          "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            MessageBox.Show($"Error loading orders: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    ' =======================================================================
    ' FORMAT ORDERS DATAGRIDVIEW
    ' =======================================================================
    Private Sub FormatOrdersDataGridView()
        Try
            With DataGridView1
                .ReadOnly = True
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = False
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .RowHeadersVisible = False
                .RowTemplate.Height = 45
                .ColumnHeadersHeight = 45
                .EnableHeadersVisualStyles = False
                .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250)
                .ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(73, 80, 87)
                .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
                .DefaultCellStyle.Font = New Font("Segoe UI", 9.5F)
                .DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 241, 255)
                .DefaultCellStyle.SelectionForeColor = Color.Black
                .BackgroundColor = Color.White
                .BorderStyle = BorderStyle.None
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                .GridColor = Color.FromArgb(233, 236, 239)

                ' Format Columns
                If .Columns.Contains("OrderID") Then
                    .Columns("OrderID").HeaderText = "Order ID"
                    .Columns("OrderID").Width = 100
                    .Columns("OrderID").DefaultCellStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
                End If

                If .Columns.Contains("OrderDateTime") Then
                    .Columns("OrderDateTime").HeaderText = "Date & Time"
                    .Columns("OrderDateTime").Width = 160
                End If

                If .Columns.Contains("OrderType") Then
                    .Columns("OrderType").HeaderText = "Type"
                    .Columns("OrderType").Width = 100
                    .Columns("OrderType").DefaultCellStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
                End If

                If .Columns.Contains("Items") Then
                    .Columns("Items").HeaderText = "Items"
                    .Columns("Items").FillWeight = 200
                End If

                If .Columns.Contains("TotalAmount") Then
                    .Columns("TotalAmount").HeaderText = "Total"
                    .Columns("TotalAmount").DefaultCellStyle.Format = "₱#,##0"
                    .Columns("TotalAmount").DefaultCellStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
                    .Columns("TotalAmount").Width = 100
                End If

                If .Columns.Contains("PaymentMethod") Then
                    .Columns("PaymentMethod").HeaderText = "Payment"
                    .Columns("PaymentMethod").Width = 110
                End If

                If .Columns.Contains("OrderStatus") Then
                    .Columns("OrderStatus").HeaderText = "Status"
                    .Columns("OrderStatus").Width = 120
                End If
            End With
        Catch ex As Exception
            ' Handle formatting errors silently
        End Try
    End Sub

    ' =======================================================================
    ' DATAGRIDVIEW CELL FORMATTING - FOR BADGES AND COLORS
    ' =======================================================================
    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Try
            If e.Value Is Nothing Then Return

            ' Status Badges
            If DataGridView1.Columns(e.ColumnIndex).Name = "OrderStatus" Then
                Dim status As String = e.Value.ToString()
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

                Select Case status
                    Case "Completed"
                        e.CellStyle.BackColor = Color.FromArgb(0, 200, 83) ' Bright Green
                        e.CellStyle.ForeColor = Color.White
                    Case "Cancelled"
                        e.CellStyle.BackColor = Color.FromArgb(255, 140, 0) ' Orange
                        e.CellStyle.ForeColor = Color.White
                    Case "Refunded"
                        e.CellStyle.BackColor = Color.FromArgb(255, 193, 7) ' Yellow/Gold
                        e.CellStyle.ForeColor = Color.White
                    Case "Pending"
                        e.CellStyle.BackColor = Color.FromArgb(108, 117, 125) ' Grey
                        e.CellStyle.ForeColor = Color.White
                    Case Else
                        e.CellStyle.BackColor = Color.FromArgb(23, 162, 184) ' Cyan
                        e.CellStyle.ForeColor = Color.White
                End Select
            End If

            ' Payment Method Badges
            If DataGridView1.Columns(e.ColumnIndex).Name = "PaymentMethod" Then
                Dim payment As String = e.Value.ToString()
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

                Select Case payment
                    Case "Cash"
                        e.CellStyle.BackColor = Color.FromArgb(220, 255, 230) ' Light Green
                        e.CellStyle.ForeColor = Color.FromArgb(40, 167, 69)
                    Case "Card", "Credit Card"
                        e.CellStyle.BackColor = Color.FromArgb(255, 235, 238) ' Light Red
                        e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69)
                    Case "GCash", "E-Wallet"
                        e.CellStyle.BackColor = Color.FromArgb(232, 240, 254) ' Light Blue
                        e.CellStyle.ForeColor = Color.FromArgb(26, 115, 232)
                End Select
            End If

            ' Order ID Color
            If DataGridView1.Columns(e.ColumnIndex).Name = "OrderID" Then
                e.CellStyle.ForeColor = Color.FromArgb(26, 115, 232) ' Blue

                ' Add Hash prefix if missing
                If Not e.Value.ToString().StartsWith("#") Then
                    e.Value = "#" & e.Value.ToString()
                    e.FormattingApplied = True
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    ' =======================================================================
    ' BUILD ORDERS QUERY
    ' =======================================================================
    Private Function BuildOrdersQuery(filterStatus As String, search As String) As String
        ' Get period filter from Reports form
        Dim periodFilter As String = ""
        Dim selectedYear As Integer = Reports.SelectedYear
        Dim selectedMonth As Integer = Reports.SelectedMonth

        Select Case Reports.SelectedPeriod
            Case "Daily"
                If selectedYear = DateTime.Now.Year Then
                    periodFilter = " AND DATE(o.OrderDate) = CURDATE() "
                Else
                    periodFilter = $" AND DATE(o.OrderDate) = '{selectedYear}-12-31' "
                End If
            Case "Weekly"
                If selectedYear = DateTime.Now.Year Then
                    periodFilter = " AND YEARWEEK(o.OrderDate, 1) = YEARWEEK(CURDATE(), 1) "
                Else
                    periodFilter = $" AND YEAR(o.OrderDate) = {selectedYear} AND WEEK(o.OrderDate, 1) = 52 "
                End If
            Case "Monthly"
                If selectedMonth = 0 Then
                    periodFilter = $" AND YEAR(o.OrderDate) = {selectedYear} "
                Else
                    periodFilter = $" AND YEAR(o.OrderDate) = {selectedYear} AND MONTH(o.OrderDate) = {selectedMonth} "
                End If
            Case "Yearly"
                periodFilter = $" AND YEAR(o.OrderDate) = {selectedYear} "
        End Select

        ' Simplified query - match the design from the image
        Dim sql As String = "
            SELECT 
                o.OrderID,
                CONCAT(DATE_FORMAT(o.OrderDate, '%Y-%m-%d'), ' ', TIME_FORMAT(o.OrderTime, '%h:%i %p')) AS OrderDateTime,
                o.OrderType,
                (
                    SELECT GROUP_CONCAT(ProductName SEPARATOR ', ')
                    FROM order_items
                    WHERE OrderID = o.OrderID
                ) AS Items,
                o.TotalAmount,
                COALESCE(p.PaymentMethod, 'Cash') AS PaymentMethod,
                o.OrderStatus
            FROM orders o
            LEFT JOIN payments p ON o.OrderID = p.OrderID
            WHERE 1=1
        "

        sql &= periodFilter

        If filterStatus <> "All" AndAlso Not String.IsNullOrEmpty(filterStatus) Then
            sql &= $" AND o.OrderStatus = '{filterStatus}'"
        End If

        If Not String.IsNullOrEmpty(search) Then
            sql &= $" AND (o.OrderID LIKE '%{search}%' OR o.OrderStatus LIKE '%{search}%' OR o.OrderType LIKE '%{search}%')"
        End If

        sql &= " ORDER BY o.OrderDate DESC, o.OrderTime DESC LIMIT 100"

        Return sql
    End Function

    ' =======================================================================
    ' REFRESH DATA - Public method to refresh all data
    ' =======================================================================
    Public Sub RefreshData()
        Try
            ' Reload statistics
            UpdateStatisticsFromDatabase()


            ' Reload grid
            LoadOrdersData(currentFilter, searchText)

            ' Reload charts
            LoadOrdersTrendChart()
            LoadCategoriesChart()
        Catch ex As Exception
            MessageBox.Show($"Error refreshing data: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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
    ' INITIALIZE DATAGRIDVIEW
    ' =======================================================================
    Private Sub InitializeDataGridView()
        ' DataGridView is already defined in the designer
        ' Just set basic properties if needed
        If DataGridView1 IsNot Nothing Then
            DataGridView1.AutoGenerateColumns = True
            DataGridView1.AllowUserToAddRows = False
            DataGridView1.AllowUserToDeleteRows = False
            DataGridView1.ReadOnly = True
        End If
    End Sub

    ' =======================================================================
    ' INITIALIZE FILTERS
    ' =======================================================================
    Private Sub InitializeFilters()
        ' Add filter initialization if needed
    End Sub

    ' =======================================================================
    ' INITIALIZE CHARTS
    ' =======================================================================
    Private Sub InitializeCharts()
        Try
            ' Configure Monthly Chart (Trends)
            With MonthlyChartOrder
                .Series.Clear()
                .ChartAreas(0).AxisX.MajorGrid.Enabled = False
                .ChartAreas(0).AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240)
                .ChartAreas(0).AxisX.LabelStyle.Font = New Font("Segoe UI", 8)
                .ChartAreas(0).AxisY.LabelStyle.Font = New Font("Segoe UI", 8)

                Dim seriesTrend As New Series("Orders")
                seriesTrend.ChartType = SeriesChartType.SplineArea
                seriesTrend.Color = Color.FromArgb(100, 78, 115, 223) ' Semi-transparent blue
                seriesTrend.BorderWidth = 3
                seriesTrend.BorderColor = Color.FromArgb(78, 115, 223)
                seriesTrend.MarkerStyle = MarkerStyle.Circle
                seriesTrend.MarkerSize = 8
                seriesTrend.MarkerColor = Color.White
                seriesTrend.MarkerBorderColor = Color.FromArgb(78, 115, 223)
                seriesTrend.MarkerBorderWidth = 2

                .Series.Add(seriesTrend)
            End With

            ' Configure Categories Chart (Pie)
            With OrderCategoriesGraph
                .Series.Clear()
                .ChartAreas(0).BackColor = Color.Transparent

                Dim seriesPie As New Series("Categories")
                seriesPie.ChartType = SeriesChartType.Doughnut
                seriesPie("PieLabelStyle") = "Outside"
                seriesPie("PieStartAngle") = "270"
                seriesPie("DoughnutRadius") = "40"

                .Series.Add(seriesPie)
                .Legends(0).Enabled = True
                .Legends(0).Docking = Docking.Right
            End With

        Catch ex As Exception
            ' Handle silently or show basic warning
        End Try
    End Sub

    ' =======================================================================
    ' LOAD ORDERS TREND CHART
    ' =======================================================================
    Private Sub LoadOrdersTrendChart()
        Try
            If conn.State <> ConnectionState.Open Then openConn()

            ' Use shared date filter logic
            Dim dateGrouping As String = ""
            Dim periodValue As String = ""

            Select Case Reports.SelectedPeriod
                Case "Daily"
                    dateGrouping = "DATE_FORMAT(OrderDate, '%m/%d')"
                Case "Weekly"
                    dateGrouping = "CONCAT('Wk ', WEEK(OrderDate))"
                Case "Monthly"
                    dateGrouping = "DATE_FORMAT(OrderDate, '%b')"
                Case "Yearly"
                    dateGrouping = "YEAR(OrderDate)"
                Case Else
                    dateGrouping = "DATE_FORMAT(OrderDate, '%m/%d')"
            End Select

            ' Get period filter from Reports form
            Dim periodFilter As String = ""
            Dim selectedYear As Integer = Reports.SelectedYear
            Dim selectedMonth As Integer = Reports.SelectedMonth

            Select Case Reports.SelectedPeriod
                Case "Daily"
                    If selectedYear = DateTime.Now.Year Then
                        periodFilter = " WHERE DATE(OrderDate) = CURDATE() "
                    Else
                        periodFilter = $" WHERE DATE(OrderDate) = '{selectedYear}-12-31' "
                    End If
                Case "Weekly"
                    If selectedYear = DateTime.Now.Year Then
                        periodFilter = " WHERE YEARWEEK(OrderDate, 1) = YEARWEEK(CURDATE(), 1) "
                    Else
                        periodFilter = $" WHERE YEAR(OrderDate) = {selectedYear} AND WEEK(OrderDate, 1) = 52 "
                    End If
                Case "Monthly"
                    If selectedMonth = 0 Then
                        periodFilter = $" WHERE YEAR(OrderDate) = {selectedYear} "
                    Else
                        periodFilter = $" WHERE YEAR(OrderDate) = {selectedYear} AND MONTH(OrderDate) = {selectedMonth} "
                    End If
                Case "Yearly"
                    periodFilter = $" WHERE YEAR(OrderDate) = {selectedYear} "
            End Select

            Dim sql As String = $"
                SELECT {dateGrouping} AS Period, COUNT(*) AS OrderCount
                FROM orders
                {periodFilter}
                GROUP BY {dateGrouping}
                ORDER BY MIN(OrderDate)
            "

            MonthlyChartOrder.Series("Orders").Points.Clear()

            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        MonthlyChartOrder.Series("Orders").Points.AddXY(reader("Period").ToString(), reader("OrderCount"))
                    End While
                End Using
            End Using

        Catch ex As Exception
            ' Load sample points if db fails
            MonthlyChartOrder.Series("Orders").Points.AddXY("Mon", 10)
            MonthlyChartOrder.Series("Orders").Points.AddXY("Tue", 15)
            MonthlyChartOrder.Series("Orders").Points.AddXY("Wed", 12)
        End Try
    End Sub

    ' =======================================================================
    ' LOAD CATEGORIES CHART
    ' =======================================================================
    Private Sub LoadCategoriesChart()
        Try
            If conn.State <> ConnectionState.Open Then openConn()

            ' Get period filter from Reports form
            Dim periodFilter As String = ""
            Dim selectedYear As Integer = Reports.SelectedYear
            Dim selectedMonth As Integer = Reports.SelectedMonth

            Select Case Reports.SelectedPeriod
                Case "Daily"
                    If selectedYear = DateTime.Now.Year Then
                        periodFilter = " AND DATE(o.OrderDate) = CURDATE() "
                    Else
                        periodFilter = $" AND DATE(o.OrderDate) = '{selectedYear}-12-31' "
                    End If
                Case "Weekly"
                    If selectedYear = DateTime.Now.Year Then
                        periodFilter = " AND YEARWEEK(o.OrderDate, 1) = YEARWEEK(CURDATE(), 1) "
                    Else
                        periodFilter = $" AND YEAR(o.OrderDate) = {selectedYear} AND WEEK(o.OrderDate, 1) = 52 "
                    End If
                Case "Monthly"
                    If selectedMonth = 0 Then
                        periodFilter = $" AND YEAR(o.OrderDate) = {selectedYear} "
                    Else
                        periodFilter = $" AND YEAR(o.OrderDate) = {selectedYear} AND MONTH(o.OrderDate) = {selectedMonth} "
                    End If
                Case "Yearly"
                    periodFilter = $" AND YEAR(o.OrderDate) = {selectedYear} "
            End Select

            Dim sql As String = $"
                SELECT COALESCE(p.Category, 'Other') AS Category, COUNT(*) AS Count
                FROM order_items oi
                JOIN orders o ON oi.OrderID = o.OrderID
                LEFT JOIN products p ON oi.ProductName = p.ProductName
                WHERE 1=1 {periodFilter}
                GROUP BY p.Category
            "

            OrderCategoriesGraph.Series("Categories").Points.Clear()

            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim p As DataPoint = OrderCategoriesGraph.Series("Categories").Points.Add(Convert.ToDouble(reader("Count")))
                        p.LegendText = reader("Category").ToString()
                        p.Label = reader("Count").ToString()
                    End While
                End Using
            End Using

        Catch ex As Exception
            ' Load sample points
            OrderCategoriesGraph.Series("Categories").Points.AddXY("Dine In", 45)
            OrderCategoriesGraph.Series("Categories").Points.AddXY("Take Out", 35)
            OrderCategoriesGraph.Series("Categories").Points.AddXY("Delivery", 20)
        End Try
    End Sub
    Private Sub btnExportPdf_Click(sender As Object, e As EventArgs) Handles btnExportPdf.Click
        If Reports.Instance IsNot Nothing Then
            Reports.Instance.ExportCurrentReport()
        Else
            MessageBox.Show("Please open the Reports screen to export.", "PDF Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class