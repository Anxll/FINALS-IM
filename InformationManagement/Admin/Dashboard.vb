Imports System.Windows.Forms.DataVisualization.Charting
Imports MySqlConnector

Public Class Dashboard
    Private WithEvents refreshTimer As New Timer()

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = ColorTranslator.FromHtml("#F7F8FA")
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New Size(0, 1200)

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
        ' Refresh only dynamic data (orders, reservations)
        LoadPendingOrders()
        LoadRecentReservations()
        LoadTotalOrders()
    End Sub

    Private Sub LoadDashboardData()
        Try
            ' Load metrics
            LoadTotalRevenue()
            LoadTotalOrders()
            LoadActiveReservations()

            ' Load charts and lists
            LoadSalesByChannel()
            LoadTopMenuItems()
            LoadRecentReservations()
            LoadPendingOrders()
            LoadQuickStats()

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
    ' SALES BY CHANNEL PIE CHART
    ' ============================================

    Private Sub LoadSalesByChannel()
        Try
            openConn()
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

            ' Add Catering/Reservation revenue
            cmd = New MySqlCommand("
                SELECT COALESCE(SUM(AmountPaid), 0) as CateringRevenue
                FROM reservation_payments 
                WHERE PaymentStatus = 'Completed'", conn)
            Dim cateringRevenue As Decimal = Convert.ToDecimal(cmd.ExecuteScalar())

            closeConn()

            ' Calculate total and percentages
            Dim totalSales As Decimal = dineInSales + takeoutSales + cateringRevenue

            If totalSales > 0 Then
                Dim dineInPercent As Decimal = (dineInSales / totalSales) * 100
                Dim takeoutPercent As Decimal = (takeoutSales / totalSales) * 100
                Dim cateringPercent As Decimal = (cateringRevenue / totalSales) * 100

                ' Update chart
                Chart2.Series(0).Points.Clear()
                Chart2.Series(0).Points.AddXY("Dine-in", dineInPercent)
                Chart2.Series(0).Points.AddXY("Takeout", takeoutPercent)
                Chart2.Series(0).Points.AddXY("Catering", cateringPercent)

                ' Update legend labels
                lblPercentDineIn.Text = Math.Round(dineInPercent, 0).ToString() & "%"
                lblValueDinein.Text = "₱" & dineInSales.ToString("N2")

                lblPercentTakeout.Text = Math.Round(takeoutPercent, 0).ToString() & "%"
                lblValueTakeout.Text = "₱" & takeoutSales.ToString("N2")

                lblPercentCatering.Text = Math.Round(cateringPercent, 0).ToString() & "%"
                lblValueCatering.Text = "₱" & cateringRevenue.ToString("N2")
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading sales by channel: " & ex.Message)
            closeConn()
        End Try
    End Sub

    ' ============================================
    ' TOP MENU ITEMS - FIXED (Using OrderCount from Products table)
    ' ============================================

    Private Sub LoadTopMenuItems()
        Try
            ' Clear existing items except the header
            For i = RoundedPane25.Controls.Count - 1 To 0 Step -1
                If TypeOf RoundedPane25.Controls(i) Is RoundedPane2 AndAlso
                   RoundedPane25.Controls(i).Name <> "RoundedPane214" Then
                    RoundedPane25.Controls.RemoveAt(i)
                End If
            Next

            openConn()
            ' Use OrderCount field from products table and calculate estimated revenue
            cmd = New MySqlCommand("
                SELECT 
                    ProductID,
                    ProductName,
                    OrderCount,
                    (Price * OrderCount) as TotalRevenue
                FROM products
                WHERE OrderCount > 0
                ORDER BY OrderCount DESC
                LIMIT 5", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            Dim yPosition As Integer = 61
            Dim itemCount As Integer = 0

            While reader.Read() AndAlso itemCount < 5
                Dim itemPanel As New RoundedPane2 With {
                    .BorderColor = Color.LightGray,
                    .BorderThickness = 1,
                    .CornerRadius = 15,
                    .FillColor = Color.White,
                    .Size = New Size(456, 67),
                    .Location = New Point(20, yPosition),
                    .Name = "itemPanel" & itemCount
                }

                ' Icon
                Dim icon As New PictureBox With {
                    .BackColor = Color.Transparent,
                    .Image = My.Resources.fork_and_knife,
                    .Location = New Point(21, 25),
                    .Size = New Size(20, 17),
                    .SizeMode = PictureBoxSizeMode.StretchImage
                }

                ' Product name
                Dim lblName As New Label With {
                    .AutoSize = True,
                    .BackColor = Color.Transparent,
                    .Font = New Font("Segoe UI Semibold", 11.25!, FontStyle.Bold),
                    .Location = New Point(53, 15),
                    .Text = reader("ProductName").ToString()
                }

                ' Order count
                Dim lblOrders As New Label With {
                    .AutoSize = True,
                    .BackColor = Color.Transparent,
                    .Font = New Font("Segoe UI", 9.75!),
                    .ForeColor = SystemColors.ControlDarkDark,
                    .Location = New Point(54, 35),
                    .Text = reader("OrderCount").ToString() & " orders"
                }

                ' Revenue
                Dim lblRevenue As New Label With {
                    .AutoSize = True,
                    .BackColor = Color.Transparent,
                    .Font = New Font("Segoe UI", 11.25!, FontStyle.Bold),
                    .Location = New Point(320, 25),
                    .Text = "₱" & Convert.ToDecimal(reader("TotalRevenue")).ToString("N2")
                }

                itemPanel.Controls.AddRange({icon, lblName, lblOrders, lblRevenue})
                RoundedPane25.Controls.Add(itemPanel)
                itemPanel.BringToFront()

                yPosition += 83
                itemCount += 1
            End While

            reader.Close()
            closeConn()

            ' If no items found, show a message
            If itemCount = 0 Then
                Dim noDataLabel As New Label With {
                    .Text = "No order data available",
                    .Font = New Font("Segoe UI", 10),
                    .ForeColor = Color.Gray,
                    .Location = New Point(20, 61),
                    .AutoSize = True,
                    .BackColor = Color.Transparent
                }
                RoundedPane25.Controls.Add(noDataLabel)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading top menu items: " & ex.Message)
            closeConn()
        End Try
    End Sub

    ' ============================================
    ' RECENT RESERVATIONS - FIXED
    ' ============================================


    Private Sub LoadRecentReservations()
        Try
            openConn()
            cmd = New MySqlCommand("
                SELECT 
                    r.EventType,
                    r.EventDate,
                    r.NumberOfGuests,
                    r.ReservationStatus
                FROM reservations r
                WHERE r.ReservationStatus IN ('Pending', 'Confirmed')
                ORDER BY r.EventDate DESC
                LIMIT 2", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            ' First reservation (Wedding)
            If reader.Read() Then
                lblEvent.Text = reader("EventType").ToString()
                lblDate.Text = Convert.ToDateTime(reader("EventDate")).ToString("yyyy-MM-dd")
                lblGuests.Text = reader("NumberOfGuests").ToString() & " Guests"
                lblStatus.Text = reader("ReservationStatus").ToString()
                lblStatus.BackColor = If(reader("ReservationStatus").ToString() = "Confirmed", Color.Black, Color.LightGray)
            End If


            reader.Close()
            closeConn()

        Catch ex As Exception
            MessageBox.Show("Error loading recent reservations: " & ex.Message)
            closeConn()
        End Try
    End Sub


    ' ============================================
    ' PENDING ORDERS - FIXED
    ' ============================================

    Private Sub LoadPendingOrders()
        Try
            ' Clear existing order panels except the template
            For i = flpOrders.Controls.Count - 1 To 0 Step -1
                If TypeOf flpOrders.Controls(i) Is Panel AndAlso
                   flpOrders.Controls(i).Name <> "pnlOrders" Then
                    flpOrders.Controls.RemoveAt(i)
                End If
            Next

            openConn()
            cmd = New MySqlCommand("
                SELECT 
                    o.OrderID,
                    o.ReceiptNumber,
                    o.OrderType,
                    o.TotalAmount,
                    o.OrderDate,
                    o.OrderTime,
                    TIMESTAMPDIFF(MINUTE, CONCAT(o.OrderDate, ' ', o.OrderTime), NOW()) as MinutesAgo,
                    o.OrderSource
                FROM orders o
                WHERE o.OrderStatus = 'Preparing'
                ORDER BY o.OrderDate DESC, o.OrderTime DESC
                LIMIT 5", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            Dim yPosition As Integer = 62
            Dim itemCount As Integer = 0

            While reader.Read() AndAlso itemCount < 5
                Dim orderPanel As New Panel With {
                    .BackColor = Color.FromArgb(255, 250, 240),
                    .Size = New Size(456, 58),
                    .Location = New Point(18, yPosition),
                    .Name = "orderPanel" & itemCount
                }

                ' Order ID
                Dim lblOrderId As New Label With {
                    .AutoSize = True,
                    .BackColor = Color.Transparent,
                    .Font = New Font("Segoe UI Semibold", 11.25!, FontStyle.Bold),
                    .Location = New Point(17, 9),
                    .Text = reader("ReceiptNumber").ToString()
                }

                ' Order Type
                Dim lblOrderType As New Label With {
                    .AutoSize = True,
                    .BackColor = Color.Transparent,
                    .Font = New Font("Segoe UI", 9.75!),
                    .ForeColor = SystemColors.ControlDarkDark,
                    .Location = New Point(20, 29),
                    .Text = reader("OrderType").ToString() & " •"
                }

                ' Time ago
                Dim minutesAgo As Integer = If(IsDBNull(reader("MinutesAgo")), 0, Convert.ToInt32(reader("MinutesAgo")))
                Dim timeText As String
                If minutesAgo < 60 Then
                    timeText = minutesAgo.ToString() & " mins ago"
                Else
                    Dim hours As Integer = minutesAgo \ 60
                    timeText = hours.ToString() & " hour" & If(hours > 1, "s", "") & " ago"
                End If

                Dim lblOrderTime As New Label With {
                    .AutoSize = True,
                    .BackColor = Color.Transparent,
                    .Font = New Font("Segoe UI", 9.75!),
                    .ForeColor = SystemColors.ControlDarkDark,
                    .Location = New Point(110, 29),
                    .Text = timeText
                }

                ' Price
                Dim lblPrice As New Label With {
                    .AutoSize = True,
                    .BackColor = Color.Transparent,
                    .Font = New Font("Segoe UI", 11.25!, FontStyle.Bold),
                    .Location = New Point(350, 17),
                    .Text = "₱" & Convert.ToDecimal(reader("TotalAmount")).ToString("N2")
                }

                orderPanel.Controls.AddRange({lblOrderId, lblOrderType, lblOrderTime, lblPrice})
                flpOrders.Controls.Add(orderPanel)
                orderPanel.BringToFront()

                yPosition += 73
                itemCount += 1
            End While

            reader.Close()
            closeConn()

            ' If no pending orders
            If itemCount = 0 Then
                Dim noDataLabel As New Label With {
                    .Text = "No pending orders",
                    .Font = New Font("Segoe UI", 10),
                    .ForeColor = Color.Gray,
                    .Location = New Point(18, 62),
                    .AutoSize = True,
                    .BackColor = Color.Transparent
                }
                flpOrders.Controls.Add(noDataLabel)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading pending orders: " & ex.Message)
            closeConn()
        End Try
    End Sub

    ' ============================================
    ' QUICK STATS
    ' ============================================

    Private Sub LoadQuickStats()
        Try
            openConn()

            ' Active Staff
            cmd = New MySqlCommand("SELECT COUNT(*) FROM employee WHERE EmploymentStatus = 'Active'", conn)
            Label39.Text = cmd.ExecuteScalar().ToString()

            ' Menu Items
            cmd = New MySqlCommand("SELECT COUNT(*) FROM products WHERE Availability = 'Available'", conn)
            Label38.Text = cmd.ExecuteScalar().ToString()

            ' Tables Available (hardcoded for now - you can create a tables table later)
            Label37.Text = "12/20"

            ' Average Order Value
            cmd = New MySqlCommand("
                SELECT COALESCE(AVG(TotalAmount), 0) 
                FROM orders 
                WHERE OrderStatus = 'Completed'", conn)

            Dim avgValue As Decimal = Convert.ToDecimal(cmd.ExecuteScalar())
            Label36.Text = "₱" & avgValue.ToString("N2")

            closeConn()

        Catch ex As Exception
            MessageBox.Show("Error loading quick stats: " & ex.Message)
            closeConn()
        End Try
    End Sub

    ' ============================================
    ' REFRESH DATA METHOD
    ' ============================================

    Public Sub RefreshDashboard()
        LoadDashboardData()
    End Sub

End Class