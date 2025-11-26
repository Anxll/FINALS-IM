Imports MySqlConnector
Imports System.Data
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Drawing.Drawing2D

Public Class FormOrders

    Private ordersData As New DataTable()
    Private currentFilter As String = "All"
    Private searchText As String = ""

    ' ===========================
    ' FORM LOAD
    ' ===========================
    Private Sub FormOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            InitializeDataGridView()
            InitializeFilters()
            InitializeCharts()
            LoadOrdersData()
            UpdateStatistics()
            LoadOrdersTrendChart()
            LoadCategoriesChart()
        Catch ex As Exception
            MessageBox.Show($"Form Load Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ===========================
    ' INITIALIZE CHARTS
    ' ===========================
    Private Sub InitializeCharts()
        ' --- Monthly Orders Chart ---
        If MonthlyChartOrder IsNot Nothing Then
            Try
                MonthlyChartOrder.Series.Clear()
                MonthlyChartOrder.ChartAreas.Clear()
                MonthlyChartOrder.Legends.Clear()

                Dim chartArea As New ChartArea("MainArea")
                chartArea.AxisX.Title = "Period"
                chartArea.AxisY.Title = "Total Orders"
                chartArea.AxisX.MajorGrid.Enabled = True
                chartArea.AxisY.MajorGrid.Enabled = True
                MonthlyChartOrder.ChartAreas.Add(chartArea)

                Dim series As New Series("Orders") With {
                    .ChartType = SeriesChartType.Line,
                    .BorderWidth = 3,
                    .Color = Color.FromArgb(99, 102, 241),
                    .MarkerStyle = MarkerStyle.Circle,
                    .MarkerSize = 8,
                    .Legend = "Default"
                }
                MonthlyChartOrder.Series.Add(series)

            Catch ex As Exception
                MessageBox.Show($"Error initializing monthly chart: {ex.Message}", "Chart Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If

        ' --- Categories Chart ---
        If OrderCategoriesGraph IsNot Nothing Then
            Try
                OrderCategoriesGraph.Series.Clear()
                OrderCategoriesGraph.ChartAreas.Clear()
                OrderCategoriesGraph.Legends.Clear()

                Dim chartArea As New ChartArea("MainArea")
                OrderCategoriesGraph.ChartAreas.Add(chartArea)

                Dim legend As New Legend("Legend1") With {
                    .Docking = Docking.Right,
                    .Alignment = StringAlignment.Center
                }
                OrderCategoriesGraph.Legends.Add(legend)

                Dim series As New Series("Categories") With {
                    .ChartType = SeriesChartType.Pie,
                    .Label = "#PERCENT{P1}",
                    .Legend = "Legend1",
                    .IsVisibleInLegend = True
                }
                OrderCategoriesGraph.Series.Add(series)

            Catch ex As Exception
                MessageBox.Show($"Error initializing categories chart: {ex.Message}", "Chart Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
    End Sub

    ' ===========================
    ' LOAD MONTHLY ORDERS CHART
    ' ===========================
    Private Sub LoadOrdersTrendChart()
        Try
            If MonthlyChartOrder Is Nothing Then Return
            If MonthlyChartOrder.Series.Count = 0 Then InitializeCharts()

            Dim trendData As New DataTable()
            Dim hasData As Boolean = False

            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                Dim sql As String = ""
                Dim xLabels As New List(Of String)
                Dim yMax As Integer = 100
                Dim yInterval As Integer = 20

                Select Case Reports.SelectedPeriod
                    Case "Daily"
                        sql = "
                    SELECT DATE(OrderDate) AS Period, COUNT(*) AS OrderCount
                    FROM orders
                    WHERE OrderDate >= CURDATE() - INTERVAL 6 DAY
                    GROUP BY DATE(OrderDate)
                    ORDER BY DATE(OrderDate)
                "
                        For i As Integer = 6 To 0 Step -1
                            xLabels.Add(Format(Date.Today.AddDays(-i), "ddd"))
                        Next
                        yMax = 80
                        yInterval = 20

                    Case "Weekly"
                        sql = "
                    SELECT WEEK(OrderDate,1) AS WeekNum, COUNT(*) AS OrderCount
                    FROM orders
                    WHERE MONTH(OrderDate) = MONTH(CURDATE()) AND YEAR(OrderDate) = YEAR(CURDATE())
                    GROUP BY WEEK(OrderDate,1)
                    ORDER BY WEEK(OrderDate,1)
                "
                        For i As Integer = 1 To 6
                            xLabels.Add($"Week {i}")
                        Next
                        yMax = 380
                        yInterval = 95

                    Case "Monthly"
                        sql = "
                    SELECT DATE_FORMAT(OrderDate,'%Y-%m') AS Period, COUNT(*) AS OrderCount
                    FROM orders
                    WHERE OrderDate >= DATE_SUB(CURDATE(), INTERVAL 4 MONTH)
                    GROUP BY DATE_FORMAT(OrderDate,'%Y-%m')
                    ORDER BY DATE_FORMAT(OrderDate,'%Y-%m')
                "
                        For i As Integer = 4 To 0 Step -1
                            xLabels.Add(Format(Date.Today.AddMonths(-i), "MMM"))
                        Next
                        yMax = 600
                        yInterval = 150

                    Case "Yearly"
                        sql = "
                    SELECT YEAR(OrderDate) AS YearNum, COUNT(*) AS OrderCount
                    FROM orders
                    WHERE OrderDate >= DATE_SUB(CURDATE(), INTERVAL 5 YEAR)
                    GROUP BY YEAR(OrderDate)
                    ORDER BY YEAR(OrderDate)
                "
                        For i As Integer = 5 To 0 Step -1
                            xLabels.Add((Date.Today.Year - i).ToString())
                        Next
                        yMax = 8000
                        yInterval = 2000

                    Case Else
                        Return
                End Select

                ' Fetch data
                Using cmd As New MySqlCommand(sql, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(trendData)
                    End Using
                End Using

                ' Check if we have any real data
                hasData = trendData.Rows.Count > 0

                ' If no data, use sample data instead
                If Not hasData Then
                    LoadSampleMonthlyData()
                    Return
                End If

                ' Clear previous points and setup series
                Dim seriesName As String = $"{Reports.SelectedPeriod} Orders"
                Dim chartSeries As Series

                If MonthlyChartOrder.Series.IndexOf(seriesName) >= 0 Then
                    chartSeries = MonthlyChartOrder.Series(seriesName)
                    chartSeries.Points.Clear()
                Else
                    MonthlyChartOrder.Series.Clear()
                    chartSeries = MonthlyChartOrder.Series.Add(seriesName)
                    chartSeries.ChartType = SeriesChartType.Line
                    chartSeries.BorderWidth = 3
                    chartSeries.Color = Color.FromArgb(99, 102, 241)
                    chartSeries.MarkerStyle = MarkerStyle.Circle
                    chartSeries.MarkerSize = 8
                End If

                MonthlyChartOrder.Titles.Clear()
                MonthlyChartOrder.Titles.Add(seriesName)

                ' Set X-axis
                MonthlyChartOrder.ChartAreas(0).AxisX.Interval = 1
                MonthlyChartOrder.ChartAreas(0).AxisX.MajorGrid.Enabled = True
                MonthlyChartOrder.ChartAreas(0).AxisX.MajorGrid.LineColor = Color.LightGray

                ' Set Y-axis
                MonthlyChartOrder.ChartAreas(0).AxisY.Minimum = 0
                MonthlyChartOrder.ChartAreas(0).AxisY.Maximum = yMax
                MonthlyChartOrder.ChartAreas(0).AxisY.Interval = yInterval
                MonthlyChartOrder.ChartAreas(0).AxisY.MajorGrid.Enabled = True
                MonthlyChartOrder.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.LightGray

                ' Add points to series
                Dim labelIndex As Integer = 0
                For Each xLabel As String In xLabels
                    Dim yValue As Integer = 0

                    Select Case Reports.SelectedPeriod
                        Case "Daily"
                            Dim row = trendData.AsEnumerable().FirstOrDefault(Function(r) Format(CDate(r("Period")), "ddd") = xLabel)
                            If row IsNot Nothing Then yValue = Convert.ToInt32(row("OrderCount"))

                        Case "Weekly"
                            Dim weekNum As Integer = labelIndex + 1
                            Dim row = trendData.AsEnumerable().FirstOrDefault(Function(r) Convert.ToInt32(r("WeekNum")) = weekNum)
                            If row IsNot Nothing Then yValue = Convert.ToInt32(row("OrderCount"))

                        Case "Monthly"
                            Dim row = trendData.AsEnumerable().FirstOrDefault(Function(r)
                                                                                  Dim periodDate = DateTime.ParseExact(r("Period").ToString(), "yyyy-MM", Nothing)
                                                                                  Return Format(periodDate, "MMM") = xLabel
                                                                              End Function)
                            If row IsNot Nothing Then yValue = Convert.ToInt32(row("OrderCount"))

                        Case "Yearly"
                            Dim yearNum As Integer = CInt(xLabel)
                            Dim row = trendData.AsEnumerable().FirstOrDefault(Function(r) Convert.ToInt32(r("YearNum")) = yearNum)
                            If row IsNot Nothing Then yValue = Convert.ToInt32(row("OrderCount"))
                    End Select

                    chartSeries.Points.AddXY(xLabel, yValue)
                    labelIndex += 1
                Next

            Else
                ' No database connection - use sample data
                LoadSampleMonthlyData()
            End If

        Catch ex As Exception
            ' Any error - use sample data
            Try
                LoadSampleMonthlyData()
            Catch
            End Try
        End Try
    End Sub

    Private Sub LoadSampleMonthlyData()
        MonthlyChartOrder.Series.Clear()

        Dim seriesName As String = $"{Reports.SelectedPeriod} Orders"
        Dim chartSeries As Series = MonthlyChartOrder.Series.Add(seriesName)
        chartSeries.ChartType = SeriesChartType.Line
        chartSeries.BorderWidth = 3
        chartSeries.Color = Color.FromArgb(99, 102, 241)
        chartSeries.MarkerStyle = MarkerStyle.Circle
        chartSeries.MarkerSize = 8

        MonthlyChartOrder.Titles.Clear()
        MonthlyChartOrder.Titles.Add(seriesName)

        Dim periods() As String
        Dim values() As Integer

        Select Case Reports.SelectedPeriod
            Case "Daily"
                periods = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"}
                values = {40, 55, 48, 60, 70, 65, 50}
                ' Set Y-axis for Daily
                MonthlyChartOrder.ChartAreas(0).AxisY.Minimum = 0
                MonthlyChartOrder.ChartAreas(0).AxisY.Maximum = 80
                MonthlyChartOrder.ChartAreas(0).AxisY.Interval = 20

            Case "Weekly"
                periods = {"Week 1", "Week 2", "Week 3", "Week 4"}
                values = {200, 250, 230, 280}
                MonthlyChartOrder.ChartAreas(0).AxisY.Minimum = 0
                MonthlyChartOrder.ChartAreas(0).AxisY.Maximum = 380
                MonthlyChartOrder.ChartAreas(0).AxisY.Interval = 95

            Case "Monthly"
                periods = {
                DateTime.Now.AddMonths(-4).ToString("MMM"),
                DateTime.Now.AddMonths(-3).ToString("MMM"),
                DateTime.Now.AddMonths(-2).ToString("MMM"),
                DateTime.Now.AddMonths(-1).ToString("MMM"),
                DateTime.Now.ToString("MMM")
            }
                values = {300, 350, 400, 450, 500}
                MonthlyChartOrder.ChartAreas(0).AxisY.Minimum = 0
                MonthlyChartOrder.ChartAreas(0).AxisY.Maximum = 600
                MonthlyChartOrder.ChartAreas(0).AxisY.Interval = 150

            Case "Yearly"
                periods = {"2021", "2022", "2023", "2024", "2025"}
                values = {2500, 2800, 3000, 3200, 3400}
                MonthlyChartOrder.ChartAreas(0).AxisY.Minimum = 0
                MonthlyChartOrder.ChartAreas(0).AxisY.Maximum = 8000
                MonthlyChartOrder.ChartAreas(0).AxisY.Interval = 2000
        End Select

        ' Configure X-axis
        MonthlyChartOrder.ChartAreas(0).AxisX.Interval = 1
        MonthlyChartOrder.ChartAreas(0).AxisX.MajorGrid.Enabled = True
        MonthlyChartOrder.ChartAreas(0).AxisX.MajorGrid.LineColor = Color.LightGray
        MonthlyChartOrder.ChartAreas(0).AxisY.MajorGrid.Enabled = True
        MonthlyChartOrder.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.LightGray

        For i As Integer = 0 To periods.Length - 1
            chartSeries.Points.AddXY(periods(i), values(i))
        Next
    End Sub

    ' ===========================
    ' LOAD CATEGORIES CHART
    ' ===========================
    Private Sub LoadCategoriesChart()
        Try
            If OrderCategoriesGraph Is Nothing Then Return
            If OrderCategoriesGraph.Series.Count = 0 Then InitializeCharts()

            Dim categoriesData As New DataTable()

            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                ' Filter by selected period
                Dim periodFilter As String = ""
                Select Case Reports.SelectedPeriod
                    Case "Daily" : periodFilter = "WHERE DATE(oi.OrderDate) = CURDATE()"
                    Case "Weekly" : periodFilter = "WHERE YEARWEEK(oi.OrderDate,1) = YEARWEEK(CURDATE(),1)"
                    Case "Monthly" : periodFilter = "WHERE DATE_FORMAT(oi.OrderDate,'%Y-%m') = DATE_FORMAT(CURDATE(),'%Y-%m')"
                    Case "Yearly" : periodFilter = "WHERE YEAR(oi.OrderDate) = YEAR(CURDATE())"
                End Select

                Dim sql As String = $"
                SELECT COALESCE(c.CategoryName,'Uncategorized') AS Category,
                       COUNT(DISTINCT oi.OrderID) AS OrderCount
                FROM order_items oi
                LEFT JOIN products p ON oi.ProductID = p.ProductID
                LEFT JOIN categories c ON p.CategoryID = c.CategoryID
                {periodFilter}
                GROUP BY c.CategoryName
                ORDER BY OrderCount DESC
                LIMIT 10
            "

                Try
                    Using cmd As New MySqlCommand(sql, conn)
                        Using adapter As New MySqlDataAdapter(cmd)
                            adapter.Fill(categoriesData)
                        End Using
                    End Using
                Catch
                    LoadSampleCategoriesData()
                    Return
                End Try
            End If

            ' Clear previous points
            Dim seriesName As String = "Categories"
            If OrderCategoriesGraph.Series.IndexOf(seriesName) >= 0 Then
                OrderCategoriesGraph.Series(seriesName).Points.Clear()

                If categoriesData.Rows.Count > 0 Then
                    Dim colors() As Color = {
                    Color.FromArgb(147, 112, 219), Color.FromArgb(255, 165, 0),
                    Color.FromArgb(144, 238, 144), Color.FromArgb(255, 193, 37),
                    Color.FromArgb(100, 149, 237), Color.FromArgb(255, 105, 180),
                    Color.FromArgb(60, 179, 113), Color.FromArgb(255, 140, 0),
                    Color.FromArgb(138, 43, 226), Color.FromArgb(218, 165, 32)
                }

                    Dim colorIndex As Integer = 0
                    For Each row As DataRow In categoriesData.Rows
                        Dim point As New DataPoint()
                        point.SetValueXY(row("Category").ToString(), Convert.ToInt32(row("OrderCount")))
                        point.Color = colors(colorIndex Mod colors.Length)
                        point.LegendText = row("Category").ToString()
                        OrderCategoriesGraph.Series(seriesName).Points.Add(point)
                        colorIndex += 1
                    Next
                Else
                    LoadSampleCategoriesData()
                End If
            End If

        Catch
            Try
                LoadSampleCategoriesData()
            Catch
            End Try
        End Try
    End Sub



    Private Sub LoadSampleCategoriesData()
        Try
            If OrderCategoriesGraph.Series.Count = 0 Then InitializeCharts()
            Dim seriesName As String = "Categories"
            Dim chartSeries As Series

            ' Clear or create series
            If OrderCategoriesGraph.Series.IndexOf(seriesName) >= 0 Then
                chartSeries = OrderCategoriesGraph.Series(seriesName)
                chartSeries.Points.Clear()
            Else
                chartSeries = OrderCategoriesGraph.Series.Add(seriesName)
                chartSeries.ChartType = SeriesChartType.Pie
                chartSeries.Label = "#PERCENT{P1}"
                chartSeries.Legend = "Legend1"
            End If

            ' Sample data by period
            Dim categories As New List(Of String)
            Dim values As New List(Of Integer)

            Select Case Reports.SelectedPeriod
                Case "Daily"
                    categories.AddRange({"Main Courses", "Beverages", "Desserts", "Appetizers"})
                    values.AddRange({50, 30, 40, 20})
                Case "Weekly"
                    categories.AddRange({"Main Courses", "Beverages", "Desserts", "Appetizers"})
                    values.AddRange({200, 150, 180, 120})
                Case "Monthly"
                    categories.AddRange({"Main Courses", "Beverages", "Desserts", "Appetizers"})
                    values.AddRange({450, 280, 320, 180})
                Case "Yearly"
                    categories.AddRange({"Main Courses", "Beverages", "Desserts", "Appetizers"})
                    values.AddRange({3000, 2500, 2700, 2000})
            End Select

            Dim colors() As Color = {
            Color.FromArgb(147, 112, 219), Color.FromArgb(255, 165, 0),
            Color.FromArgb(144, 238, 144), Color.FromArgb(255, 193, 37)
        }

            For i As Integer = 0 To categories.Count - 1
                Dim point As New DataPoint()
                point.SetValueXY(categories(i), values(i))
                point.Color = colors(i Mod colors.Length)
                point.LegendText = categories(i)
                chartSeries.Points.Add(point)
            Next

        Catch ex As Exception
            MessageBox.Show($"Error loading sample categories data: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' INITIALIZE DATAGRIDVIEW
    ' =======================================================================
    Private Sub InitializeDataGridView()
        ' Check if DataGridView exists on your form
        ' If not, you'll need to add one in the designer
        ' This is a template - adjust based on your actual controls

        ' Example if you have a DataGridView named dgvOrders:
        ' With dgvOrders
        '     .AutoGenerateColumns = False
        '     .AllowUserToAddRows = False
        '     .AllowUserToDeleteRows = False
        '     .ReadOnly = True
        '     .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        '     .MultiSelect = False
        '     .RowHeadersVisible = False
        '     .BackgroundColor = Color.White
        '     .BorderStyle = BorderStyle.None
        '     .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(99, 102, 241)
        '     .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        '     .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        '     .DefaultCellStyle.Font = New Font("Segoe UI", 9)
        '     .RowTemplate.Height = 35
        ' End With
    End Sub
    ' =======================================================================
    ' INITIALIZE FILTERS (ComboBox, etc.)
    ' =======================================================================
    Private Sub InitializeFilters()
        ' Example if you have a ComboBox for status filter
        ' cmbStatusFilter.Items.Clear()
        ' cmbStatusFilter.Items.AddRange(New String() {"All", "Pending", "Processing", "Completed", "Cancelled"})
        ' cmbStatusFilter.SelectedIndex = 0
    End Sub
    ' =======================================================================
    ' LOAD ORDERS DATA FROM DATABASE
    ' =======================================================================
    Private Sub LoadOrdersData(Optional filterStatus As String = "All", Optional search As String = "")
        Try
            If conn Is Nothing Then
                MessageBox.Show("Database connection not initialized.", "Connection Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                LoadSampleData()
                Return
            End If

            If conn.State <> ConnectionState.Open Then
                openConn()
            End If

            ' Check if orders table exists
            If Not TableExists("orders") Then
                MessageBox.Show("Orders table not found. Loading sample data.", "Table Missing",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                LoadSampleData()
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

            ' Update display (adjust based on your controls)
            DisplayOrders()

        Catch ex As MySqlException
            MessageBox.Show($"Database Error: {ex.Message}{vbCrLf}Loading sample data instead.",
                          "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LoadSampleData()
        Catch ex As Exception
            MessageBox.Show($"Error loading orders: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            LoadSampleData()
        End Try
    End Sub

    ' =======================================================================
    ' BUILD ORDERS QUERY
    ' =======================================================================
    Private Function BuildOrdersQuery(filterStatus As String, search As String) As String
        ' Build query based on actual table structure
        Dim sql As String = "
            SELECT 
                o.OrderID,
                o.CustomerID,
                o.EmployeeID,
                o.OrderType,
                o.OrderSource,
                o.ReceiptNumber,
                o.NumberOfDiners,
                o.OrderDate,
                o.OrderTime,
                o.ItemsOrderedCount,
                o.TotalAmount,
                o.OrderStatus,
                o.Remarks
            FROM orders o
            WHERE 1=1
        "

        If filterStatus <> "All" AndAlso Not String.IsNullOrEmpty(filterStatus) Then
            sql &= $" AND o.OrderStatus = '{filterStatus}'"
        End If

        If Not String.IsNullOrEmpty(search) Then
            sql &= $" AND (o.OrderID LIKE '%{search}%' OR o.ReceiptNumber LIKE '%{search}%' OR o.OrderStatus LIKE '%{search}%')"
        End If

        sql &= " ORDER BY o.OrderDate DESC, o.OrderTime DESC LIMIT 1000"

        Return sql
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
    ' DISPLAY ORDERS (Update your UI here)
    ' =======================================================================
    Private Sub DisplayOrders()
        ' This depends on how you want to display orders
        ' Examples:

        ' If using DataGridView:
        ' dgvOrders.DataSource = ordersData

        ' If using ListBox:
        ' lstOrders.Items.Clear()
        ' For Each row As DataRow In ordersData.Rows
        '     lstOrders.Items.Add($"Order #{row("OrderID")} - {row("CustomerName")} - ₱{Convert.ToDecimal(row("TotalAmount")):N2}")
        ' Next

        ' If using FlowLayoutPanel with custom controls:
        ' flowPanelOrders.Controls.Clear()
        ' For Each row As DataRow In ordersData.Rows
        '     Dim orderCard As New OrderCard(row)
        '     flowPanelOrders.Controls.Add(orderCard)
        ' Next
    End Sub

    ' =======================================================================
    ' LOAD SAMPLE DATA FOR TESTING
    ' =======================================================================
    Private Sub LoadSampleData()
        Try
            ordersData.Clear()

            ' Create columns
            If ordersData.Columns.Count = 0 Then
                ordersData.Columns.Add("OrderID", GetType(Integer))
                ordersData.Columns.Add("CustomerID", GetType(Integer))
                ordersData.Columns.Add("CustomerName", GetType(String))
                ordersData.Columns.Add("OrderDate", GetType(DateTime))
                ordersData.Columns.Add("TotalAmount", GetType(Decimal))
                ordersData.Columns.Add("OrderStatus", GetType(String))
                ordersData.Columns.Add("PaymentMethod", GetType(String))
                ordersData.Columns.Add("DeliveryAddress", GetType(String))
                ordersData.Columns.Add("ItemCount", GetType(Integer))
            End If

            ' Add sample rows
            ordersData.Rows.Add(1001, 1, "Juan Dela Cruz", DateTime.Now.AddDays(-1), 1250.5D, "Pending", "GCash", "123 Main St, Manila", 3)
            ordersData.Rows.Add(1002, 2, "Maria Santos", DateTime.Now.AddDays(-2), 2340D, "Processing", "COD", "456 Oak Ave, Quezon City", 5)
            ordersData.Rows.Add(1003, 3, "Jose Reyes", DateTime.Now.AddDays(-3), 890.75D, "Completed", "Credit Card", "789 Pine Rd, Makati", 2)
            ordersData.Rows.Add(1004, 4, "Ana Garcia", DateTime.Now.AddDays(-4), 1580.25D, "Completed", "GCash", "321 Elm St, Pasig", 4)
            ordersData.Rows.Add(1005, 5, "Pedro Martinez", DateTime.Now.AddDays(-5), 3200D, "Cancelled", "Bank Transfer", "654 Maple Dr, Taguig", 6)

            DisplayOrders()

        Catch ex As Exception
            MessageBox.Show($"Error loading sample data: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' UPDATE STATISTICS
    ' =======================================================================
    Private Sub UpdateStatistics()
        Try
            If ordersData.Rows.Count = 0 Then
                SetDefaultStatistics()
                Return
            End If

            Dim totalOrders As Integer = ordersData.Rows.Count
            Dim totalRevenue As Decimal = 0
            Dim pendingCount As Integer = 0
            Dim completedCount As Integer = 0
            Dim cancelledCount As Integer = 0

            For Each row As DataRow In ordersData.Rows
                totalRevenue += Convert.ToDecimal(row("TotalAmount"))

                Dim status As String = row("OrderStatus").ToString()
                Select Case status
                    Case "Pending"
                        pendingCount += 1
                    Case "Completed"
                        completedCount += 1
                    Case "Cancelled"
                        cancelledCount += 1
                End Select
            Next

            ' Update labels (adjust based on your actual label names)
            ' lblTotalOrders.Text = totalOrders.ToString()
            ' lblTotalRevenue.Text = $"₱{totalRevenue:N2}"
            ' lblPending.Text = pendingCount.ToString()
            ' lblCompleted.Text = completedCount.ToString()
            ' lblCancelled.Text = cancelledCount.ToString()

        Catch ex As Exception
            MessageBox.Show($"Error updating statistics: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' SET DEFAULT STATISTICS
    ' =======================================================================
    Private Sub SetDefaultStatistics()
        ' Set all statistics to 0 or default values
        ' lblTotalOrders.Text = "0"
        ' lblTotalRevenue.Text = "₱0.00"
        ' lblPending.Text = "0"
        ' lblCompleted.Text = "0"
        ' lblCancelled.Text = "0"
    End Sub

    ' =======================================================================
    ' GET ORDER DETAILS
    ' =======================================================================
    Public Function GetOrderDetails(orderId As Integer) As DataTable
        Dim orderDetails As New DataTable()

        Try
            If conn Is Nothing OrElse conn.State <> ConnectionState.Open Then
                openConn()
            End If

            Dim sql As String = $"
                SELECT 
                    oi.OrderItemID,
                    oi.ProductID,
                    p.ProductName,
                    oi.Quantity,
                    oi.UnitPrice,
                    (oi.Quantity * oi.UnitPrice) AS Subtotal
                FROM order_items oi
                LEFT JOIN products p ON oi.ProductID = p.ProductID
                WHERE oi.OrderID = {orderId}
            "

            Using cmd As New MySqlCommand(sql, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(orderDetails)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error loading order details: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return orderDetails
    End Function

    ' =======================================================================
    ' UPDATE ORDER STATUS
    ' =======================================================================
    Public Function UpdateOrderStatus(orderId As Integer, newStatus As String) As Boolean
        Try
            If conn Is Nothing OrElse conn.State <> ConnectionState.Open Then
                openConn()
            End If

            Dim sql As String = $"
                UPDATE orders 
                SET OrderStatus = @status,
                    UpdatedDate = NOW()
                WHERE OrderID = @orderId
            "

            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@status", newStatus)
                cmd.Parameters.AddWithValue("@orderId", orderId)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    LoadOrdersData(currentFilter, searchText)
                    UpdateStatistics()
                    Return True
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error updating order status: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return False
    End Function

    ' =======================================================================
    ' SEARCH ORDERS
    ' =======================================================================
    Public Sub SearchOrders(searchTerm As String)
        searchText = searchTerm
        LoadOrdersData(currentFilter, searchText)
        UpdateStatistics()
    End Sub

    ' =======================================================================
    ' FILTER ORDERS BY STATUS
    ' =======================================================================
    Public Sub FilterOrdersByStatus(status As String)
        currentFilter = status
        LoadOrdersData(currentFilter, searchText)
        UpdateStatistics()
    End Sub

    ' =======================================================================
    ' REFRESH DATA
    ' =======================================================================
    Public Sub RefreshData()
        LoadOrdersData(currentFilter, searchText)
        UpdateStatistics()
    End Sub

    ' =======================================================================
    ' EXPORT ORDERS TO CSV
    ' =======================================================================
    Public Sub ExportToCSV()
        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "CSV File|*.csv",
                .Title = "Export Orders",
                .FileName = $"Orders_Export_{DateTime.Now:yyyy-MM-dd}"
            }

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Using writer As New IO.StreamWriter(saveDialog.FileName)
                    ' Write headers
                    Dim headers As New List(Of String)
                    For Each column As DataColumn In ordersData.Columns
                        headers.Add(column.ColumnName)
                    Next
                    writer.WriteLine(String.Join(",", headers))

                    ' Write data
                    For Each row As DataRow In ordersData.Rows
                        Dim values As New List(Of String)
                        For Each item In row.ItemArray
                            values.Add($"""{item}""")
                        Next
                        writer.WriteLine(String.Join(",", values))
                    Next
                End Using

                MessageBox.Show("Orders exported successfully!", "Export Complete",
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show($"Export Error: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' GET ORDER STATISTICS
    ' =======================================================================
    Public Function GetOrderStatistics(Optional dateFrom As DateTime = Nothing,
                                       Optional dateTo As DateTime = Nothing) As Dictionary(Of String, Object)
        Dim stats As New Dictionary(Of String, Object)

        Try
            If conn Is Nothing OrElse conn.State <> ConnectionState.Open Then
                openConn()
            End If

            Dim dateFilter As String = ""
            If dateFrom <> Nothing AndAlso dateTo <> Nothing Then
                dateFilter = $"WHERE OrderDate BETWEEN '{dateFrom:yyyy-MM-dd}' AND '{dateTo:yyyy-MM-dd}'"
            End If

            Dim sql As String = $"
                SELECT 
                    COUNT(*) AS TotalOrders,
                    COALESCE(SUM(TotalAmount), 0) AS TotalRevenue,
                    COALESCE(AVG(TotalAmount), 0) AS AvgOrderValue,
                    COUNT(CASE WHEN OrderStatus = 'Pending' THEN 1 END) AS PendingOrders,
                    COUNT(CASE WHEN OrderStatus = 'Processing' THEN 1 END) AS ProcessingOrders,
                    COUNT(CASE WHEN OrderStatus = 'Completed' THEN 1 END) AS CompletedOrders,
                    COUNT(CASE WHEN OrderStatus = 'Cancelled' THEN 1 END) AS CancelledOrders
                FROM orders
                {dateFilter}
            "

            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        stats("TotalOrders") = Convert.ToInt32(reader("TotalOrders"))
                        stats("TotalRevenue") = Convert.ToDecimal(reader("TotalRevenue"))
                        stats("AvgOrderValue") = Convert.ToDecimal(reader("AvgOrderValue"))
                        stats("PendingOrders") = Convert.ToInt32(reader("PendingOrders"))
                        stats("ProcessingOrders") = Convert.ToInt32(reader("ProcessingOrders"))
                        stats("CompletedOrders") = Convert.ToInt32(reader("CompletedOrders"))
                        stats("CancelledOrders") = Convert.ToInt32(reader("CancelledOrders"))
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error getting statistics: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return stats
    End Function

    ' =======================================================================
    ' GENERATE ORDERS REPORT
    ' =======================================================================
    Public Function GenerateOrdersReport() As String
        Dim report As New Text.StringBuilder()
        Dim stats = GetOrderStatistics()

        report.AppendLine("═══════════════════════════════════════════════════════")
        report.AppendLine("                  ORDERS REPORT")
        report.AppendLine("═══════════════════════════════════════════════════════")
        report.AppendLine()

        report.AppendLine("SUMMARY:")
        report.AppendLine($"  Total Orders:       {stats("TotalOrders"):N0}")
        report.AppendLine($"  Total Revenue:      ₱{stats("TotalRevenue"):N2}")
        report.AppendLine($"  Average Order:      ₱{stats("AvgOrderValue"):N2}")
        report.AppendLine()

        report.AppendLine("ORDER STATUS BREAKDOWN:")
        report.AppendLine($"  Pending:       {stats("PendingOrders"),5}")
        report.AppendLine($"  Processing:    {stats("ProcessingOrders"),5}")
        report.AppendLine($"  Completed:     {stats("CompletedOrders"),5}")
        report.AppendLine($"  Cancelled:     {stats("CancelledOrders"),5}")
        report.AppendLine()

        report.AppendLine($"Report Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
        report.AppendLine("═══════════════════════════════════════════════════════")

        Return report.ToString()
    End Function

    ' =======================================================================
    ' CLEANUP
    ' =======================================================================
    Private Sub FormOrders_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Catch
        End Try
    End Sub

    Private Sub ExportReportToImage()
        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp",
                .Title = "Export Orders Report",
                .FileName = $"OrdersReport_{DateTime.Now:yyyyMMdd_HHmmss}",
                .DefaultExt = "png",
                .AddExtension = True
            }

            If saveDialog.ShowDialog() = DialogResult.OK Then
                ' Create a panel to hold the complete report
                Dim reportWidth As Integer = 850
                Dim reportHeight As Integer = 1100

                Dim reportPanel As New Panel With {
                    .Width = reportWidth,
                    .Height = reportHeight,
                    .BackColor = Color.White
                }

                ' ===== HEADER SECTION =====
                ' Main Title
                Dim lblMainTitle As New Label With {
                    .Text = "Restaurant & Catering" & vbCrLf & "Management System",
                    .Font = New Font("Segoe UI", 24, FontStyle.Bold),
                    .ForeColor = Color.FromArgb(30, 64, 175),
                    .AutoSize = False,
                    .Width = reportWidth,
                    .Height = 100,
                    .Location = New Point(0, 80),
                    .TextAlign = ContentAlignment.TopCenter
                }

                ' Subtitle
                Dim lblSubtitle As New Label With {
                    .Text = "Orders Report",
                    .Font = New Font("Segoe UI", 16, FontStyle.Regular),
                    .ForeColor = Color.FromArgb(71, 85, 105),
                    .AutoSize = False,
                    .Width = reportWidth,
                    .Height = 30,
                    .Location = New Point(0, 190),
                    .TextAlign = ContentAlignment.MiddleCenter
                }

                ' Generated Date
                Dim lblGenerated As New Label With {
                    .Text = $"Generated on {DateTime.Now:MMMM dd, yyyy}",
                    .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                    .ForeColor = Color.FromArgb(100, 116, 139),
                    .AutoSize = False,
                    .Width = reportWidth,
                    .Height = 25,
                    .Location = New Point(0, 225),
                    .TextAlign = ContentAlignment.MiddleCenter
                }

                ' Separator line
                Dim separator As New Panel With {
                    .Width = 400,
                    .Height = 2,
                    .BackColor = Color.FromArgb(30, 64, 175),
                    .Location = New Point((reportWidth - 400) / 2, 265)
                }

                ' Add header controls
                reportPanel.Controls.Add(lblMainTitle)
                reportPanel.Controls.Add(lblSubtitle)
                reportPanel.Controls.Add(lblGenerated)
                reportPanel.Controls.Add(separator)

                ' ===== CHARTS SECTION =====
                ' Create container for charts side by side
                Dim chartsPanel As New Panel With {
                    .Width = reportWidth - 80,
                    .Height = 550,
                    .BackColor = Color.White,
                    .Location = New Point(40, 300)
                }

                ' Left side - Order Trends Chart
                Dim leftChartPanel As New Panel With {
                    .Width = (chartsPanel.Width / 2) - 20,
                    .Height = chartsPanel.Height,
                    .BackColor = Color.White,
                    .Location = New Point(0, 0)
                }

                ' Order Trends Title
                Dim lblOrderTrends As New Label With {
                    .Text = "Order Trends",
                    .Font = New Font("Segoe UI", 14, FontStyle.Bold),
                    .ForeColor = Color.FromArgb(30, 64, 175),
                    .AutoSize = False,
                    .Width = leftChartPanel.Width,
                    .Height = 30,
                    .Location = New Point(0, 0),
                    .TextAlign = ContentAlignment.MiddleLeft
                }
                leftChartPanel.Controls.Add(lblOrderTrends)

                ' Capture Monthly Chart
                If MonthlyChartOrder IsNot Nothing Then
                    Dim chartBmp As New Bitmap(MonthlyChartOrder.Width, MonthlyChartOrder.Height)
                    MonthlyChartOrder.DrawToBitmap(chartBmp, New Rectangle(0, 0, MonthlyChartOrder.Width, MonthlyChartOrder.Height))

                    Dim chartPicture As New PictureBox With {
                        .Image = chartBmp,
                        .Width = leftChartPanel.Width,
                        .Height = leftChartPanel.Height - 40,
                        .Location = New Point(0, 40),
                        .SizeMode = PictureBoxSizeMode.Zoom,
                        .BackColor = Color.White
                    }
                    leftChartPanel.Controls.Add(chartPicture)
                End If

                ' Right side - Popular Categories Chart
                Dim rightChartPanel As New Panel With {
                    .Width = (chartsPanel.Width / 2) - 20,
                    .Height = chartsPanel.Height,
                    .BackColor = Color.White,
                    .Location = New Point((chartsPanel.Width / 2) + 20, 0)
                }

                ' Popular Categories Title
                Dim lblCategories As New Label With {
                    .Text = "Popular Categories",
                    .Font = New Font("Segoe UI", 14, FontStyle.Bold),
                    .ForeColor = Color.FromArgb(30, 64, 175),
                    .AutoSize = False,
                    .Width = rightChartPanel.Width,
                    .Height = 30,
                    .Location = New Point(0, 0),
                    .TextAlign = ContentAlignment.MiddleLeft
                }
                rightChartPanel.Controls.Add(lblCategories)

                ' Capture Categories Chart
                If OrderCategoriesGraph IsNot Nothing Then
                    Dim chartBmp As New Bitmap(OrderCategoriesGraph.Width, OrderCategoriesGraph.Height)
                    OrderCategoriesGraph.DrawToBitmap(chartBmp, New Rectangle(0, 0, OrderCategoriesGraph.Width, OrderCategoriesGraph.Height))

                    Dim chartPicture As New PictureBox With {
                        .Image = chartBmp,
                        .Width = rightChartPanel.Width,
                        .Height = rightChartPanel.Height - 40,
                        .Location = New Point(0, 40),
                        .SizeMode = PictureBoxSizeMode.Zoom,
                        .BackColor = Color.White
                    }
                    rightChartPanel.Controls.Add(chartPicture)
                End If

                ' Add chart panels to charts container
                chartsPanel.Controls.Add(leftChartPanel)
                chartsPanel.Controls.Add(rightChartPanel)
                reportPanel.Controls.Add(chartsPanel)

                ' ===== CREATE FINAL BITMAP =====
                Dim finalBmp As New Bitmap(reportWidth, reportHeight)

                Using g As Graphics = Graphics.FromImage(finalBmp)
                    ' Set high quality rendering
                    g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality

                    ' Draw white background
                    g.Clear(Color.White)

                    ' Draw the report panel
                    reportPanel.DrawToBitmap(finalBmp, New Rectangle(0, 0, reportWidth, reportHeight))
                End Using

                ' Determine format
                Dim format As Imaging.ImageFormat = Imaging.ImageFormat.Png
                Select Case System.IO.Path.GetExtension(saveDialog.FileName).ToLower()
                    Case ".jpg", ".jpeg"
                        format = Imaging.ImageFormat.Jpeg
                    Case ".bmp"
                        format = Imaging.ImageFormat.Bmp
                End Select

                ' Save the image
                finalBmp.Save(saveDialog.FileName, format)

                ' Clean up
                finalBmp.Dispose()
                reportPanel.Dispose()

                MessageBox.Show("Report exported successfully!", "Export Complete",
                              MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Optional: Open file location

            End If

        Catch ex As Exception
            MessageBox.Show($"Export Error: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExportReport_Click(sender As Object, e As EventArgs) Handles btnExportReport.Click
        ExportReportToImage()
    End Sub
End Class
