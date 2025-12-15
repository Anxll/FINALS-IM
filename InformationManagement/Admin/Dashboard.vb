Imports System.Windows.Forms.DataVisualization.Charting
Imports MySqlConnector

Public Class Dashboard
    Private WithEvents refreshTimer As New Timer()

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = ColorTranslator.FromHtml("#F7F8FA")

        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or
            ControlStyles.AllPaintingInWmPaint Or
            ControlStyles.UserPaint, True)
        Me.UpdateStyles()

        ' Load all dashboard data
        LoadDashboardData()

        ' Setup auto-refresh timer (refresh every 30 seconds)
        refreshTimer.Interval = 30000 ' 30 seconds
        refreshTimer.Start()
    End Sub

    Private Sub refreshTimer_Tick(sender As Object, e As EventArgs) Handles refreshTimer.Tick


        LoadTotalOrders()
        LoadReservationChart()
    End Sub

    Private Sub LoadDashboardData()
        Try
            ' Load metrics
            LoadTotalRevenue()
            LoadTotalOrders()
            LoadActiveReservations()

            ' Load charts and lists
            LoadSalesByChannel()

            LoadReservationChart()
            LoadOrdersTrendChart() ' Add this line
            LoadSalesChart()
            ConfigureChart2Clickable()
            ConfigureMonthlyChartClickable()
            ConfigureSalesChartClickable()
            LoadProductPerformanceChart()

        Catch ex As Exception
            MessageBox.Show("Error loading dashboard: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ============================================
    ' TOP METRICS
    ' ============================================

    Private Sub LoadTotalRevenue()
        Try
            openConn()
            ' Calculate from both Orders and Reservation Payments
            cmd = New MySqlCommand("
                SELECT COALESCE(
                    (SELECT SUM(TotalAmount) FROM orders WHERE OrderStatus = 'Completed'),
                    0
                ) + COALESCE(
                    (SELECT SUM(AmountPaid) FROM reservation_payments WHERE PaymentStatus = 'Completed'),
                    0
                ) as TotalRevenue", conn)

            Dim revenue As Decimal = Convert.ToDecimal(cmd.ExecuteScalar())
            lblTotalRevenue.Text = "₱" & revenue.ToString("N2")
            closeConn()
        Catch ex As Exception
            lblTotalRevenue.Text = "₱0.00"
            closeConn()
        End Try
    End Sub

    Private Sub LoadTotalOrders()
        Try
            openConn()
            ' Count both POS and Website orders
            cmd = New MySqlCommand("
                SELECT COUNT(*) FROM orders 
                WHERE OrderSource IN ('POS', 'Website')", conn)

            Dim totalOrders As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Label14.Text = totalOrders.ToString("#,##0")
            closeConn()
        Catch ex As Exception
            Label14.Text = "0"
            closeConn()
        End Try
    End Sub

    Private Sub LoadActiveReservations()
        Try
            openConn()
            ' Count reservations that are Pending or Confirmed
            cmd = New MySqlCommand("
                SELECT COUNT(*) FROM reservations 
                WHERE ReservationStatus IN ('Pending', 'Confirmed')
                AND EventDate >= CURDATE()", conn)

            Dim activeReservations As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Label16.Text = activeReservations.ToString()
            closeConn()
        Catch ex As Exception
            Label16.Text = "0"
            closeConn()
        End Try
    End Sub

    ' ============================================
    ' FIXED: SALES BY CHANNEL - Now captures actual prices from orders
    ' ============================================

    Private Sub LoadSalesByChannel()
        Try
            openConn()

            ' Get sales from completed orders grouped by type
            cmd = New MySqlCommand("
            SELECT 
                OrderType,
                COALESCE(SUM(TotalAmount), 0) as TotalSales
            FROM orders 
            WHERE OrderStatus = 'Completed'
            GROUP BY OrderType", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Dim dineInSales As Decimal = 0
            Dim takeoutSales As Decimal = 0
            Dim onlineSales As Decimal = 0

            While reader.Read()
                Dim orderType As String = reader("OrderType").ToString()
                Dim sales As Decimal = Convert.ToDecimal(reader("TotalSales"))

                Select Case orderType
                    Case "Dine-in"
                        dineInSales = sales
                    Case "Takeout"
                        takeoutSales = sales
                    Case "Online"
                        onlineSales = sales
                End Select
            End While
            reader.Close()

            ' Get Catering/Reservation revenue from completed payments
            cmd = New MySqlCommand("
            SELECT COALESCE(SUM(AmountPaid), 0) as CateringRevenue
            FROM reservation_payments 
            WHERE PaymentStatus = 'Completed'", conn)
            Dim cateringRevenue As Decimal = Convert.ToDecimal(cmd.ExecuteScalar())

            closeConn()

            ' Calculate total and percentages
            Dim totalSales As Decimal = dineInSales + takeoutSales + onlineSales + cateringRevenue

            If totalSales > 0 Then
                Dim dineInPercent As Decimal = (dineInSales / totalSales) * 100
                Dim takeoutPercent As Decimal = ((takeoutSales + onlineSales) / totalSales) * 100
                Dim cateringPercent As Decimal = (cateringRevenue / totalSales) * 100

                ' Clear and update chart with actual values (not percentages)
                Chart2.Series(0).Points.Clear()

                Dim point1 As New DataVisualization.Charting.DataPoint()
                point1.SetValueXY("Dine-in", dineInSales)
                point1.Color = Color.FromArgb(165, 149, 233)
                point1.BorderColor = Color.White
                point1.BorderWidth = 2
                point1.Label = Math.Round(dineInPercent, 0).ToString() & "%"
                point1.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                point1.LabelForeColor = Color.White
                Chart2.Series(0).Points.Add(point1)

                Dim point2 As New DataVisualization.Charting.DataPoint()
                point2.SetValueXY("Takeout", takeoutSales + onlineSales)
                point2.Color = Color.FromArgb(144, 213, 169)
                point2.BorderColor = Color.White
                point2.BorderWidth = 2
                point2.Label = Math.Round(takeoutPercent, 0).ToString() & "%"
                point2.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                point2.LabelForeColor = Color.White
                Chart2.Series(0).Points.Add(point2)

                Dim point3 As New DataVisualization.Charting.DataPoint()
                point3.SetValueXY("Catering", cateringRevenue)
                point3.Color = Color.FromArgb(251, 203, 119)
                point3.BorderColor = Color.White
                point3.BorderWidth = 2
                point3.Label = Math.Round(cateringPercent, 0).ToString() & "%"
                point3.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                point3.LabelForeColor = Color.White
                Chart2.Series(0).Points.Add(point3)

                ' Update legend labels
                lblPercentDineIn.Text = Math.Round(dineInPercent, 0).ToString() & "%"
                lblValueDinein.Text = "₱" & dineInSales.ToString("N2")

                ' Combine takeout and online
                lblPercentTakeout.Text = Math.Round(takeoutPercent, 0).ToString() & "%"
                lblValueTakeout.Text = "₱" & (takeoutSales + onlineSales).ToString("N2")

                lblPercentCatering.Text = Math.Round(cateringPercent, 0).ToString() & "%"
                lblValueCatering.Text = "₱" & cateringRevenue.ToString("N2")
            Else
                ' No data yet - show zeros and add "No data" label
                Chart2.Series(0).Points.Clear()

                ' Add "No Data Available" text annotation to chart
                Chart2.Annotations.Clear()
                Dim noDataAnnotation As New TextAnnotation()
                noDataAnnotation.Text = "No Sales Data Available"
                noDataAnnotation.Font = New Font("Segoe UI", 12, FontStyle.Bold)
                noDataAnnotation.ForeColor = Color.Gray
                noDataAnnotation.X = 50
                noDataAnnotation.Y = 50
                noDataAnnotation.Alignment = ContentAlignment.MiddleCenter
                Chart2.Annotations.Add(noDataAnnotation)

                lblPercentDineIn.Text = "0%"
                lblValueDinein.Text = "₱0.00"

                lblPercentTakeout.Text = "0%"
                lblValueTakeout.Text = "₱0.00"

                lblPercentCatering.Text = "0%"
                lblValueCatering.Text = "₱0.00"
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading sales by channel: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            closeConn()
        End Try
    End Sub

    ' ============================================


    ' ============================================
    ' SIMPLIFIED: Call FormReservationStatus chart rendering directly
    ' Add this to Dashboard.vb, replacing LoadReservationChart method
    ' ============================================

    Private Sub LoadReservationChart()
        Try
            ' Simple approach: Reuse FormReservationStatus chart rendering logic
            Dim reservationStatusData = GetReservationStatusData("Monthly")
            RenderReservationChart(ChartReservations, reservationStatusData)

        Catch ex As Exception
            MessageBox.Show("Error loading reservation chart: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            closeConn()
        End Try
    End Sub

    ' ============================================
    ' SHARED: Get Reservation Status Data
    ' This returns the same data structure used by FormReservationStatus
    ' ============================================
    Private Function GetReservationStatusData(period As String) As Dictionary(Of String, Integer)
        Dim data As New Dictionary(Of String, Integer) From {
        {"Pending", 0},
        {"Confirmed", 0},
        {"Cancelled", 0},
        {"Completed", 0}
    }

        Try
            openConn()

            ' Use same query logic as FormReservationStatus
            Dim dateFilter As String = "MONTH(ReservationDate) = MONTH(CURDATE()) AND YEAR(ReservationDate) = YEAR(CURDATE())"

            cmd = New MySqlCommand($"
            SELECT 
                ReservationStatus,
                COUNT(*) AS StatusCount
            FROM reservations
            WHERE {dateFilter}
            GROUP BY ReservationStatus", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim status As String = reader("ReservationStatus").ToString()
                Dim count As Integer = Convert.ToInt32(reader("StatusCount"))
                If data.ContainsKey(status) Then
                    data(status) = count
                End If
            End While

            reader.Close()
            closeConn()

        Catch ex As Exception
            closeConn()
            Throw
        End Try

        Return data
    End Function

    ' ============================================
    ' SHARED: Render Reservation Chart
    ' This uses the EXACT same rendering logic as FormReservationStatus
    ' ============================================
    Private Sub RenderReservationChart(chart As DataVisualization.Charting.Chart, data As Dictionary(Of String, Integer))
        Try
            ' Clear chart
            chart.Series("ReservationStatus").Points.Clear()
            chart.Annotations.Clear()

            Dim totalCount As Integer = data.Values.Sum()

            If totalCount > 0 Then
                ' Add Pending (Orange) - Same as FormReservationStatus
                If data("Pending") > 0 Then
                    Dim point As New DataVisualization.Charting.DataPoint()
                    point.SetValueXY("Pending", data("Pending"))
                    point.Color = Color.FromArgb(255, 165, 0)
                    point.BorderColor = Color.White
                    point.BorderWidth = 2
                    point.Label = data("Pending").ToString()
                    point.LegendText = $"Pending ({data("Pending")})"
                    point.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                    point.LabelForeColor = Color.White
                    chart.Series("ReservationStatus").Points.Add(point)
                End If

                ' Add Confirmed (Green)
                If data("Confirmed") > 0 Then
                    Dim point As New DataVisualization.Charting.DataPoint()
                    point.SetValueXY("Confirmed", data("Confirmed"))
                    point.Color = Color.FromArgb(34, 197, 94)
                    point.BorderColor = Color.White
                    point.BorderWidth = 2
                    point.Label = data("Confirmed").ToString()
                    point.LegendText = $"Confirmed ({data("Confirmed")})"
                    point.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                    point.LabelForeColor = Color.White
                    chart.Series("ReservationStatus").Points.Add(point)
                End If

                ' Add Cancelled (Red)
                If data("Cancelled") > 0 Then
                    Dim point As New DataVisualization.Charting.DataPoint()
                    point.SetValueXY("Cancelled", data("Cancelled"))
                    point.Color = Color.FromArgb(239, 68, 68)
                    point.BorderColor = Color.White
                    point.BorderWidth = 2
                    point.Label = data("Cancelled").ToString()
                    point.LegendText = $"Cancelled ({data("Cancelled")})"
                    point.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                    point.LabelForeColor = Color.White
                    chart.Series("ReservationStatus").Points.Add(point)
                End If

                ' Add Completed (Blue)
                If data("Completed") > 0 Then
                    Dim point As New DataVisualization.Charting.DataPoint()
                    point.SetValueXY("Completed", data("Completed"))
                    point.Color = Color.FromArgb(59, 130, 246)
                    point.BorderColor = Color.White
                    point.BorderWidth = 2
                    point.Label = data("Completed").ToString()
                    point.LegendText = $"Completed ({data("Completed")})"
                    point.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                    point.LabelForeColor = Color.White
                    chart.Series("ReservationStatus").Points.Add(point)
                End If

                ' Configure legend
                chart.Legends(0).Enabled = True
                chart.Legends(0).Docking = Docking.Right
                chart.Legends(0).Font = New Font("Segoe UI", 9)
                chart.Legends(0).BackColor = Color.Transparent
            Else
                ' No data message
                Dim noDataAnnotation As New TextAnnotation()
                noDataAnnotation.Text = "No Reservation Data"
                noDataAnnotation.Font = New Font("Segoe UI", 12, FontStyle.Bold)
                noDataAnnotation.ForeColor = Color.Gray
                noDataAnnotation.X = 50
                noDataAnnotation.Y = 50
                noDataAnnotation.Alignment = ContentAlignment.MiddleCenter
                chart.Annotations.Add(noDataAnnotation)
                chart.Legends(0).Enabled = False
            End If

            ' Configure 3D effect
            chart.ChartAreas(0).Area3DStyle.Enable3D = True
            chart.ChartAreas(0).Area3DStyle.Inclination = 15
            chart.ChartAreas(0).Area3DStyle.Rotation = 10
            chart.Series("ReservationStatus")("PieLabelStyle") = "Inside"
            chart.Series("ReservationStatus").IsValueShownAsLabel = True

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ' ============================================
    ' CHART CLICK EVENT - Navigate to Reservation Reports
    ' ============================================
    Private Sub ConfigureChart2Clickable()
        ' Make the chart cursor indicate it's clickable
        ChartReservations.Cursor = Cursors.Hand
        ' Add tooltip to indicate it's clickable
        Dim tooltip As New ToolTip()
        tooltip.SetToolTip(Chart2, "Click to view detailed Catering Reservations report")
    End Sub
    Private Sub ConfigureMonthlyChartClickable()
        ' Make the Monthly Orders chart clickable
        MonthlyChartOrder.Cursor = Cursors.Hand

        ' Add tooltip
        Dim tooltip As New ToolTip()
        tooltip.SetToolTip(MonthlyChartOrder, "Click to view detailed Orders report")
    End Sub
    Private Sub ChartReservations_Click(sender As Object, e As EventArgs) Handles ChartReservations.Click
        Try
            ' Get reference to AdminDashboard (parent form)
            Dim adminDashboard As AdminDashboard = TryCast(Me.ParentForm, AdminDashboard)

            If adminDashboard IsNot Nothing Then
                ' First, load the Reports form in AdminDashboard
                adminDashboard.btnReports.PerformClick()

                ' Give UI time to load
                Application.DoEvents()

                ' Then load the Catering Reservations report
                If Reports IsNot Nothing Then
                    Reports.LoadCateringReservationReport()
                End If
            Else
                MessageBox.Show("Unable to navigate to Reports section.", "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error navigating to catering reservations: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub LoadOrdersTrendChart()
        Try
            If MonthlyChartOrder Is Nothing Then Return

            openConn()

            ' Get last 6 months of order data
            cmd = New MySqlCommand("
                SELECT 
                    DATE_FORMAT(OrderDate,'%Y-%m') AS Period,
                    DATE_FORMAT(OrderDate,'%b') AS MonthLabel,
                    COUNT(*) AS OrderCount
                FROM orders
                WHERE OrderDate >= DATE_SUB(CURDATE(), INTERVAL 5 MONTH)
                GROUP BY DATE_FORMAT(OrderDate,'%Y-%m'), DATE_FORMAT(OrderDate,'%b')
                ORDER BY DATE_FORMAT(OrderDate,'%Y-%m')", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            ' Clear existing data
            MonthlyChartOrder.Series(0).Points.Clear()

            Dim hasData As Boolean = False
            While reader.Read()
                hasData = True
                Dim monthLabel As String = reader("MonthLabel").ToString()
                Dim orderCount As Integer = Convert.ToInt32(reader("OrderCount"))

                Dim point As New DataVisualization.Charting.DataPoint()
                point.SetValueXY(monthLabel, orderCount)
                point.Color = Color.FromArgb(99, 102, 241)
                point.MarkerStyle = MarkerStyle.Circle
                point.MarkerSize = 8
                MonthlyChartOrder.Series(0).Points.Add(point)
            End While

            reader.Close()
            closeConn()


        Catch ex As Exception
            closeConn()
        End Try
    End Sub
    Private Sub MonthlyChartOrder_Click(sender As Object, e As EventArgs) Handles MonthlyChartOrder.Click
        Try
            ' Get reference to AdminDashboard (parent form)
            Dim adminDashboard As AdminDashboard = TryCast(Me.ParentForm, AdminDashboard)

            If adminDashboard IsNot Nothing Then
                ' First, load the Reports form in AdminDashboard
                adminDashboard.btnReports.PerformClick()

                ' Give UI time to load
                Application.DoEvents()

                ' Then load the Catering Reservations report
                If Reports IsNot Nothing Then
                    Reports.LoadOrderTrends()
                End If
            Else
                MessageBox.Show("Unable to navigate to Reports section.", "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error navigating to orders: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ' Add visual feedback when hovering over the chart
    Private Sub Chart2_MouseEnter(sender As Object, e As EventArgs) Handles Chart2.MouseEnter
        Chart2.Cursor = Cursors.Hand
        RoundedPane28.BackColor = Color.FromArgb(248, 248, 250)
    End Sub

    Private Sub Chart2_MouseLeave(sender As Object, e As EventArgs) Handles Chart2.MouseLeave
        Chart2.Cursor = Cursors.Default
        RoundedPane28.BackColor = Color.White
    End Sub

    ' Add visual feedback when hovering over the chart


    ' Add visual feedback when hovering over the chart
    Private Sub ChartReservations_MouseEnter(sender As Object, e As EventArgs) Handles ChartReservations.MouseEnter
        Chart2.Cursor = Cursors.Hand
        ChartReservations.BackColor = Color.FromArgb(245, 245, 245)
    End Sub

    Private Sub ChartReservations_MouseLeave(sender As Object, e As EventArgs) Handles ChartReservations.MouseLeave
        Chart2.Cursor = Cursors.Default
        ChartReservations.BackColor = Color.White
    End Sub
    Private Sub RoundedPane21_Paint(sender As Object, e As PaintEventArgs) Handles RoundedPane21.Paint

    End Sub
    Private Sub MonthlyChartOrder_MouseEnter(sender As Object, e As EventArgs) Handles MonthlyChartOrder.MouseEnter
        MonthlyChartOrder.Cursor = Cursors.Hand
        RoundedPane26.BackColor = Color.FromArgb(248, 248, 250)
    End Sub

    Private Sub MonthlyChartOrder_MouseLeave(sender As Object, e As EventArgs) Handles MonthlyChartOrder.MouseLeave
        MonthlyChartOrder.Cursor = Cursors.Default
        RoundedPane26.BackColor = Color.White
    End Sub
    Private Sub LoadSalesChart()
        Try
            ' Get sales data for current year
            Dim salesData = GetSalesData()

            ' Render the chart with the data
            RenderSalesChart(SalesChart, salesData)  ' Replace Chart1 with your actual sales chart name

        Catch ex As Exception
            MessageBox.Show("Error loading sales chart: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            closeConn()
        End Try
    End Sub

    ' ============================================
    ' GET SALES DATA - Same logic as FormSales
    ' ============================================
    Private Function GetSalesData() As Dictionary(Of String, (Revenue As Decimal, Expenses As Decimal, Profit As Decimal))
        Dim salesData As New Dictionary(Of String, (Revenue As Decimal, Expenses As Decimal, Profit As Decimal))
        Dim currentYear As Integer = DateTime.Now.Year

        ' Initialize all 12 months with zero values
        For month As Integer = 1 To 12
            Dim monthName As String = New DateTime(currentYear, month, 1).ToString("MMM")
            salesData(monthName) = (0D, 0D, 0D)
        Next

        Try
            openConn()

            ' Build the sales query using the same logic as FormSales
            Dim sql As String = BuildSalesQueryForDashboard(currentYear)

            cmd = New MySqlCommand(sql, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim monthNum As Integer = Convert.ToInt32(reader("MonthNum"))
                Dim monthName As String = New DateTime(currentYear, monthNum, 1).ToString("MMM")

                Dim revenue As Decimal = If(IsDBNull(reader("TotalRevenue")), 0D, Convert.ToDecimal(reader("TotalRevenue")))
                Dim expenses As Decimal = If(IsDBNull(reader("TotalExpenses")), 0D, Convert.ToDecimal(reader("TotalExpenses")))
                Dim profit As Decimal = revenue - expenses

                salesData(monthName) = (revenue, expenses, profit)
            End While

            reader.Close()
            closeConn()

        Catch ex As Exception
            closeConn()
            Throw New Exception($"Error fetching sales data: {ex.Message}", ex)
        End Try

        Return salesData
    End Function


    ' ============================================
    ' BUILD SALES QUERY - Exact same as FormSales
    ' ============================================
    Private Function BuildSalesQueryForDashboard(currentYear As Integer) As String
        Dim queries As New List(Of String)

        ' Check and add payments table
        If TableExists("payments") Then
            queries.Add($"
            SELECT MONTH(PaymentDate) AS MonthNum, AmountPaid AS Amount, 'Revenue' AS Type
            FROM payments
            WHERE PaymentStatus IN ('Paid','Completed')
            AND YEAR(PaymentDate) = {currentYear}
        ")
        End If

        ' Check and add reservation_payments table
        If TableExists("reservation_payments") Then
            queries.Add($"
            SELECT MONTH(PaymentDate) AS MonthNum, AmountPaid AS Amount, 'Revenue' AS Type
            FROM reservation_payments
            WHERE PaymentStatus IN ('Paid','Completed')
            AND YEAR(PaymentDate) = {currentYear}
        ")
        End If

        ' Check and add sales table
        If TableExists("sales") Then
            queries.Add($"
            SELECT MONTH(sales_date) AS MonthNum, revenue AS Amount, 'Revenue' AS Type
            FROM sales
            WHERE YEAR(sales_date) = {currentYear}
        ")

            queries.Add($"
            SELECT MONTH(sales_date) AS MonthNum, expenses AS Amount, 'Expenses' AS Type
            FROM sales
            WHERE YEAR(sales_date) = {currentYear}
        ")
        End If

        ' Add inventory batches as expenses
        If TableExists("inventory_batches") Then
            queries.Add($"
            SELECT MONTH(PurchaseDate) AS MonthNum, TotalCost AS Amount, 'Expenses' AS Type
            FROM inventory_batches
            WHERE BatchStatus = 'Active'
            AND YEAR(PurchaseDate) = {currentYear}
        ")
        End If

        If queries.Count = 0 Then
            Throw New Exception("No valid sales tables found.")
        End If

        Return $"
        SELECT 
            MonthNum,
            SUM(CASE WHEN Type='Revenue' THEN Amount ELSE 0 END) AS TotalRevenue,
            SUM(CASE WHEN Type='Expenses' THEN Amount ELSE 0 END) AS TotalExpenses
        FROM ({String.Join(" UNION ALL ", queries)}) AS combined
        GROUP BY MonthNum 
        ORDER BY MonthNum
    "
    End Function

    ' ============================================
    ' RENDER SALES CHART - Same styling as FormSales
    ' ============================================
    Private Sub RenderSalesChart(chart As DataVisualization.Charting.Chart, salesData As Dictionary(Of String, (Revenue As Decimal, Expenses As Decimal, Profit As Decimal)))
        Try
            ' Clear existing series if they exist
            chart.Series.Clear()

            ' Create the three series
            Dim revenueSeries As New DataVisualization.Charting.Series("Revenue")
            Dim expensesSeries As New DataVisualization.Charting.Series("Expenses")
            Dim profitSeries As New DataVisualization.Charting.Series("NetProfit")

            ' Configure series appearance - Same as FormSales
            revenueSeries.ChartType = SeriesChartType.Column
            revenueSeries.Color = Color.FromArgb(99, 102, 241)  ' Indigo
            revenueSeries.BorderWidth = 0
            revenueSeries("PointWidth") = "0.6"

            expensesSeries.ChartType = SeriesChartType.Column
            expensesSeries.Color = Color.FromArgb(239, 68, 68)  ' Red
            expensesSeries.BorderWidth = 0
            expensesSeries("PointWidth") = "0.6"

            profitSeries.ChartType = SeriesChartType.Column
            profitSeries.Color = Color.FromArgb(34, 197, 94)  ' Green
            profitSeries.BorderWidth = 0
            profitSeries("PointWidth") = "0.6"

            ' Add data points for all 12 months
            Dim currentYear As Integer = DateTime.Now.Year
            For month As Integer = 1 To 12
                Dim monthName As String = New DateTime(currentYear, month, 1).ToString("MMM")

                If salesData.ContainsKey(monthName) Then
                    Dim data = salesData(monthName)

                    revenueSeries.Points.AddXY(monthName, data.Revenue)
                    expensesSeries.Points.AddXY(monthName, data.Expenses)
                    profitSeries.Points.AddXY(monthName, data.Profit)
                Else
                    revenueSeries.Points.AddXY(monthName, 0)
                    expensesSeries.Points.AddXY(monthName, 0)
                    profitSeries.Points.AddXY(monthName, 0)
                End If
            Next

            ' Add series to chart
            chart.Series.Add(revenueSeries)
            chart.Series.Add(expensesSeries)
            chart.Series.Add(profitSeries)

            ' Configure chart area styling - Same as FormSales
            If chart.ChartAreas.Count > 0 Then
                With chart.ChartAreas(0)
                    ' X-axis styling
                    .AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230)
                    .AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot
                    .AxisX.LabelStyle.Font = New Font("Segoe UI", 9)

                    ' Y-axis styling
                    .AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230)
                    .AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot
                    .AxisY.LabelStyle.Format = "₱{0:N0}"
                    .AxisY.LabelStyle.Font = New Font("Segoe UI", 9)
                End With
            End If

            ' Configure tooltips
            For Each series As DataVisualization.Charting.Series In chart.Series
                series.ToolTip = "#VALX: ₱#VALY{N2}"
            Next

            ' Configure legend
            If chart.Legends.Count > 0 Then
                chart.Legends(0).Font = New Font("Segoe UI", 9)
                chart.Legends(0).Docking = Docking.Bottom
                chart.Legends(0).BackColor = Color.Transparent
            End If

            ' Check if there's any data
            Dim hasData As Boolean = salesData.Values.Any(Function(d) d.Revenue > 0 OrElse d.Expenses > 0)

            If Not hasData Then
                ' Show "No Data" message
                chart.Annotations.Clear()
                Dim noDataAnnotation As New TextAnnotation()
                noDataAnnotation.Text = "No Sales Data Available"
                noDataAnnotation.Font = New Font("Segoe UI", 12, FontStyle.Bold)
                noDataAnnotation.ForeColor = Color.Gray
                noDataAnnotation.X = 50
                noDataAnnotation.Y = 50
                noDataAnnotation.Alignment = ContentAlignment.MiddleCenter
                chart.Annotations.Add(noDataAnnotation)
            End If

        Catch ex As Exception
            Throw New Exception($"Error rendering sales chart: {ex.Message}", ex)
        End Try
    End Sub

    ' ============================================
    ' TABLE EXISTS HELPER (if not already in Dashboard)
    ' ============================================
    Private Function TableExists(tableName As String) As Boolean
        Try
            If conn Is Nothing Then Return False

            If conn.State <> ConnectionState.Open Then
                openConn()
            End If

            Dim sql As String = "
            SELECT COUNT(*) 
            FROM information_schema.tables
            WHERE table_schema = DATABASE()
            AND LOWER(table_name) = LOWER(@TableName)
        "

            Dim checkCmd As New MySqlCommand(sql, conn)
            checkCmd.Parameters.AddWithValue("@TableName", tableName)
            Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

            Return count > 0

        Catch ex As Exception
            Return False
        End Try
    End Function

    ' ============================================
    ' MAKE SALES CHART CLICKABLE (Optional)
    ' ============================================
    Private Sub ConfigureSalesChartClickable()
        ' Make the chart cursor indicate it's clickable
        SalesChart.Cursor = Cursors.Hand  ' Replace Chart1 with your actual chart name

        ' Add tooltip
        Dim tooltip As New ToolTip()
        tooltip.SetToolTip(SalesChart, "Click to view detailed Sales report")
    End Sub

    Private Sub SalesChart_Click(sender As Object, e As EventArgs) Handles SalesChart.Click
        Try
            ' Get reference to AdminDashboard (parent form)
            Dim adminDashboard As AdminDashboard = TryCast(Me.ParentForm, AdminDashboard)

            If adminDashboard IsNot Nothing Then
                ' First, load the Reports form in AdminDashboard
                adminDashboard.btnReports.PerformClick()

                ' Give UI time to load
                Application.DoEvents()

                ' Then load the Sales report (btnSales should be clicked)
                If Reports IsNot Nothing Then
                    ' Assuming Reports has access to btnSales
                    ' You may need to add a public method in Reports.vb to handle this
                    Reports.LoadSalesReport()  ' Add this method to Reports.vb
                End If
            Else
                MessageBox.Show("Unable to navigate to Reports section.", "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error navigating to sales report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ============================================
    ' VISUAL FEEDBACK FOR SALES CHART HOVER
    ' ============================================
    Private Sub SalesChart_MouseEnter(sender As Object, e As EventArgs) Handles SalesChart.MouseEnter
        SalesChart.Cursor = Cursors.Hand
        ' If your chart is inside a RoundedPane, change its background:
        ' RoundedPaneXX.BackColor = Color.FromArgb(248, 248, 250)
    End Sub

    Private Sub SalesChart_MouseLeave(sender As Object, e As EventArgs) Handles SalesChart.MouseLeave
        SalesChart.Cursor = Cursors.Default
        ' RoundedPaneXX.BackColor = Color.White
    End Sub
    ' Add this method to your Reports.vb file
    Private Sub ProductPerformance_Click(sender As Object, e As EventArgs) Handles ProductPerformance.Click
        Try
            ' Get reference to AdminDashboard (parent form)
            Dim adminDashboard As AdminDashboard = TryCast(Me.ParentForm, AdminDashboard)

            If adminDashboard IsNot Nothing Then
                ' First, load the Reports form in AdminDashboard
                adminDashboard.btnReports.PerformClick()

                ' Give UI time to load
                Application.DoEvents()

                ' Then load the Catering Reservations report
                If Reports IsNot Nothing Then
                    Reports.LoadProductPerformanceReport()
                End If
            Else
                MessageBox.Show("Unable to navigate to Reports section.", "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error navigating to catering reservations: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub LoadProductPerformanceChart()
        Try
            Dim query As String =
"SELECT ProductName,
        SUM(Quantity) AS TotalOrders,
        SUM(TotalPrice) AS Revenue
 FROM (
        SELECT ri.ProductName,
               ri.Quantity,
               ri.TotalPrice
        FROM reservation_items ri
        INNER JOIN reservations r ON ri.ReservationID = r.ReservationID
        WHERE r.ReservationStatus IN ('Confirmed', 'Served')

        UNION ALL
        
        SELECT oi.ProductName,
               oi.Quantity,
               (oi.Quantity * oi.UnitPrice) AS TotalPrice
        FROM order_items oi
        INNER JOIN orders o ON oi.OrderID = o.OrderID
        WHERE o.OrderStatus IN ('Served', 'Completed')
      ) AS combined
 GROUP BY ProductName
 ORDER BY Revenue DESC;"

            ProductPerformance.Series("Revenue").Points.Clear()

            Using conn As New MySqlConnection(strConnection)
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            ProductPerformance.Series("Revenue").Points.AddXY(
                            reader("ProductName").ToString(),
                            Convert.ToDecimal(reader("Revenue"))
                        )
                        End While
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub ProductPerformance_MouseEnter(sender As Object, e As EventArgs) Handles ProductPerformance.MouseEnter
        ProductPerformance.Cursor = Cursors.Hand
        ' If your chart is inside a RoundedPane, change its background:
        ' RoundedPaneXX.BackColor = Color.FromArgb(248, 248, 250)
    End Sub

    Private Sub ProductPerformance_MouseLeave(sender As Object, e As EventArgs) Handles ProductPerformance.MouseLeave
        ProductPerformance.Cursor = Cursors.Default
        ' RoundedPaneXX.BackCo
    End Sub
End Class