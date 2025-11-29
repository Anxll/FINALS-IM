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
                ' No data yet - show zeros
                Chart2.Series(0).Points.Clear()

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
    ' FIXED: TOP MENU ITEMS - Uses OrderCount from products table
    ' No order_items table needed - works with existing schema
    ' ============================================

    Private Sub LoadTopMenuItems()
        Try
            ' Clear existing items except the label
            Dim controlsToRemove As New List(Of Control)
            For Each ctrl As Control In PanelMenu.Controls
                If TypeOf ctrl Is RoundedPane2 AndAlso ctrl.Name.StartsWith("itemPanel") Then
                    controlsToRemove.Add(ctrl)
                End If
            Next
            For Each ctrl In controlsToRemove
                PanelMenu.Controls.Remove(ctrl)
                ctrl.Dispose()
            Next

            openConn()

            ' Get top products by OrderCount (manually maintained)
            ' Revenue calculated as: OrderCount × Current Price
            cmd = New MySqlCommand("
            SELECT 
                ProductID,
                ProductName,
                OrderCount,
                Price,
                (OrderCount * Price) as EstimatedRevenue
            FROM products
            WHERE OrderCount > 0
            AND Availability = 'Available'
            ORDER BY OrderCount DESC
            LIMIT 10", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            Dim yPosition As Integer = 61
            Dim itemCount As Integer = 0

            While reader.Read()
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
                Dim orderCount As Integer = Convert.ToInt32(reader("OrderCount"))
                Dim lblOrders As New Label With {
                .AutoSize = True,
                .BackColor = Color.Transparent,
                .Font = New Font("Segoe UI", 9.75!),
                .ForeColor = SystemColors.ControlDarkDark,
                .Location = New Point(54, 35),
                .Text = orderCount.ToString("#,##0") & " orders"
            }

                ' Estimated Revenue (OrderCount × Price)
                Dim revenue As Decimal = Convert.ToDecimal(reader("EstimatedRevenue"))
                Dim lblRevenue As New Label With {
                .AutoSize = True,
                .BackColor = Color.Transparent,
                .Font = New Font("Segoe UI", 11.25!, FontStyle.Bold),
                .Location = New Point(320, 25),
                .Text = "₱" & revenue.ToString("N2")
            }

                itemPanel.Controls.AddRange({icon, lblName, lblOrders, lblRevenue})
                PanelMenu.Controls.Add(itemPanel)
                itemPanel.BringToFront()

                yPosition += 83
                itemCount += 1
            End While

            reader.Close()
            closeConn()

            ' Adjust panel height to fit all items
            If itemCount > 0 Then
                PanelMenu.Height = yPosition + 30
            Else
                ' If no items found, show a message
                Dim noDataLabel As New Label With {
                .Text = "No order data available yet",
                .Font = New Font("Segoe UI", 10),
                .ForeColor = Color.Gray,
                .Location = New Point(20, 61),
                .AutoSize = True,
                .BackColor = Color.Transparent
            }
                PanelMenu.Controls.Add(noDataLabel)
                PanelMenu.Height = 150
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading top menu items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            closeConn()
        End Try
    End Sub

    ' ============================================
    ' HELPER: Update Product OrderCount (Optional - for maintenance)
    ' Call this after completing orders to keep OrderCount field updated
    ' ============================================

    Public Sub UpdateProductOrderCounts()
        Try
            openConn()

            ' Update OrderCount based on actual completed orders
            cmd = New MySqlCommand("
            UPDATE products p
            LEFT JOIN (
                SELECT 
                    oi.ProductID,
                    COUNT(DISTINCT oi.OrderID) as OrderCount
                FROM order_items oi
                INNER JOIN orders o ON oi.OrderID = o.OrderID
                WHERE o.OrderStatus = 'Completed'
                GROUP BY oi.ProductID
            ) counts ON p.ProductID = counts.ProductID
            SET p.OrderCount = COALESCE(counts.OrderCount, 0)", conn)

            cmd.ExecuteNonQuery()
            closeConn()

        Catch ex As Exception
            closeConn()
        End Try
    End Sub

    ' ============================================
    ' RECENT RESERVATIONS - FIXED TO SHOW ALL DATA
    ' ============================================


    Private Sub LoadRecentReservations()
        Try
            ' Clear existing reservation panels except the label
            Dim controlsToRemove As New List(Of Control)
            For Each ctrl As Control In PanelReservations.Controls
                If TypeOf ctrl Is RoundedPane2 AndAlso ctrl.Name.StartsWith("pnlReservation") Then
                    controlsToRemove.Add(ctrl)
                End If
            Next
            For Each ctrl In controlsToRemove
                PanelReservations.Controls.Remove(ctrl)
                ctrl.Dispose()
            Next

            openConn()
            ' FIXED: Show all reservations, ordered by most recent first
            cmd = New MySqlCommand("
                SELECT 
                    r.ReservationID,
                    r.EventType,
                    r.EventDate,
                    r.NumberOfGuests,
                    r.ReservationStatus,
                    r.ReservationType,
                    r.DeliveryOption,
                    c.FirstName,
                    c.LastName
                FROM reservations r
                LEFT JOIN customers c ON r.CustomerID = c.CustomerID
                ORDER BY r.EventDate DESC, r.ReservationID DESC
                LIMIT 20", conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            Dim yPosition As Integer = 61
            Dim itemCount As Integer = 0

            While reader.Read()
                Dim reservationPanel As New RoundedPane2 With {
                    .BorderColor = Color.LightGray,
                    .BorderThickness = 1,
                    .CornerRadius = 15,
                    .FillColor = Color.White,
                    .Size = New Size(456, 67),
                    .Location = New Point(29, yPosition),
                    .Name = "pnlReservation" & itemCount
                }

                ' Icon
                Dim icon As New PictureBox With {
                    .BackColor = Color.Transparent,
                    .Image = My.Resources.calendar_icon,
                    .Location = New Point(21, 25),
                    .Size = New Size(20, 17),
                    .SizeMode = PictureBoxSizeMode.StretchImage
                }

                ' Event Type (Title)
                Dim eventType As String = reader("EventType").ToString()
                Dim lblEvent As New Label With {
                    .AutoSize = True,
                    .BackColor = Color.Transparent,
                    .Font = New Font("Segoe UI Semibold", 11.25!, FontStyle.Bold),
                    .Location = New Point(53, 15),
                    .Text = eventType
                }

                ' Event Date
                Dim eventDate As DateTime = Convert.ToDateTime(reader("EventDate"))
                Dim lblDate As New Label With {
                    .AutoSize = True,
                    .BackColor = Color.Transparent,
                    .Font = New Font("Segoe UI", 9.75!),
                    .ForeColor = SystemColors.ControlDarkDark,
                    .Location = New Point(54, 35),
                    .Text = eventDate.ToString("yyyy-MM-dd")
                }

                ' Number of Guests
                Dim guests As Integer = Convert.ToInt32(reader("NumberOfGuests"))
                Dim lblGuests As New Label With {
                    .AutoSize = True,
                    .BackColor = Color.Transparent,
                    .Font = New Font("Segoe UI", 9.75!),
                    .ForeColor = SystemColors.ControlDarkDark,
                    .Location = New Point(154, 35),
                    .Text = " • " & guests.ToString() & " Guests"
                }

                ' Reservation Status Badge with dynamic colors
                Dim status As String = reader("ReservationStatus").ToString()
                Dim statusColor As Color

                Select Case status.ToLower()
                    Case "confirmed"
                        statusColor = Color.FromArgb(34, 197, 94) ' Green
                    Case "pending"
                        statusColor = Color.FromArgb(251, 146, 60) ' Orange
                    Case "completed"
                        statusColor = Color.FromArgb(59, 130, 246) ' Blue
                    Case "cancelled"
                        statusColor = Color.FromArgb(239, 68, 68) ' Red
                    Case Else
                        statusColor = Color.Gray
                End Select

                Dim lblStatus As New Label With {
                    .AutoSize = True,
                    .BackColor = statusColor,
                    .FlatStyle = FlatStyle.Flat,
                    .Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold),
                    .ForeColor = Color.White,
                    .Location = New Point(379, 25),
                    .Text = status,
                    .Padding = New Padding(8, 4, 8, 4)
                }

                reservationPanel.Controls.AddRange({icon, lblEvent, lblDate, lblGuests, lblStatus})
                PanelReservations.Controls.Add(reservationPanel)
                reservationPanel.BringToFront()

                yPosition += 83
                itemCount += 1
            End While

            reader.Close()
            closeConn()

            ' Adjust panel height to fit all items
            If itemCount > 0 Then
                PanelReservations.Height = yPosition + 30
                ' FIXED: Adjust Quick Stats position based on reservation panel height
                AdjustQuickStatsPosition()
            Else
                ' If no reservations found
                Dim noDataLabel As New Label With {
                    .Text = "No reservations found",
                    .Font = New Font("Segoe UI", 10),
                    .ForeColor = Color.Gray,
                    .Location = New Point(29, 61),
                    .AutoSize = True,
                    .BackColor = Color.Transparent
                }
                PanelReservations.Controls.Add(noDataLabel)
                PanelReservations.Height = 150
                AdjustQuickStatsPosition()
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading recent reservations: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            closeConn()
        End Try
    End Sub

    ' ============================================
    ' ADJUST QUICK STATS POSITION
    ' ============================================

    Private Sub AdjustQuickStatsPosition()
        Try
            ' Find the Quick Stats panel - searching more thoroughly
            Dim quickStatsPanel As Control = Nothing

            ' Method 1: Search in form controls
            quickStatsPanel = FindControlRecursive(Me, "quickstats")

            ' Method 2: If not found, try common variations
            If quickStatsPanel Is Nothing Then
                quickStatsPanel = FindControlRecursive(Me, "pnlquick")
            End If

            ' Method 3: Search for controls with Label39, Label38, Label37, Label36 (Quick Stats labels)
            If quickStatsPanel Is Nothing Then
                For Each ctrl As Control In Me.Controls
                    If ctrl.Controls.Contains(Label39) OrElse
                       ctrl.Controls.Contains(Label38) OrElse
                       ctrl.Controls.Contains(Label37) OrElse
                       ctrl.Controls.Contains(Label36) Then
                        quickStatsPanel = ctrl
                        Exit For
                    End If
                Next
            End If

            ' If Quick Stats panel is found, adjust its position
            If quickStatsPanel IsNot Nothing Then
                Dim newY As Integer = PanelReservations.Location.Y + PanelReservations.Height + 20
                quickStatsPanel.Location = New Point(quickStatsPanel.Location.X, newY)
            End If

        Catch ex As Exception
            ' Silently fail - this is just a positioning adjustment
        End Try
    End Sub

    ' Helper function to search for controls recursively
    Private Function FindControlRecursive(parent As Control, searchText As String) As Control
        For Each ctrl As Control In parent.Controls
            If ctrl.Name.ToLower().Contains(searchText.ToLower()) Then
                Return ctrl
            End If

            ' Search in child controls
            Dim found As Control = FindControlRecursive(ctrl, searchText)
            If found IsNot Nothing Then
                Return found
            End If
        Next
        Return Nothing
    End Function
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
                    .BackColor = Color.PeachPuff,
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