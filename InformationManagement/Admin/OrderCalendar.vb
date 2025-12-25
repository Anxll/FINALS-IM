Imports MySqlConnector
Imports System.Data

Public Class OrderCalendar
    Private currentMonth As Date
    Private orderData As List(Of OrderInfo)

    ' Class to hold order info
    Private Class OrderInfo
        Public Property OrderID As Integer
        Public Property CustomerName As String
        Public Property OrderType As String
        Public Property OrderDate As Date
        Public Property OrderTime As String
        Public Property TotalAmount As Decimal
        Public Property ItemsOrderedCount As Integer
        Public Property OrderedProducts As String
        Public Property ContactNumber As String
        Public Property SpecialRequests As String
        Public Property DeliveryAddress As String
        Public Property DeliveryOption As String
    End Class

    Private Sub OrderCalendar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        currentMonth = New Date(Date.Now.Year, Date.Now.Month, 1)
        LoadOrders()
        DisplayCalendar()
    End Sub

    ' ==========================================
    ' LOAD CONFIRMED ORDERS
    ' ==========================================
    Private Sub LoadOrders()
        Try
            orderData = New List(Of OrderInfo)

            ' Get start and end of current month
            Dim monthStart As Date = New Date(currentMonth.Year, currentMonth.Month, 1)
            Dim monthEnd As Date = monthStart.AddMonths(1).AddDays(-1)

            Dim query As String =
                "SELECT 
                    o.OrderID,
                    CONCAT(COALESCE(c.FirstName, ''), ' ', COALESCE(c.LastName, '')) AS CustomerName,
                    o.OrderType,
                    o.OrderDate,
                    o.OrderTime,
                    o.TotalAmount,
                    o.ItemsOrderedCount,
                    COALESCE(c.ContactNumber, '') AS ContactNumber,
                    COALESCE(o.SpecialRequests, '') AS SpecialRequests,
                    COALESCE(o.DeliveryAddress, '') AS DeliveryAddress,
                    COALESCE(o.DeliveryOption, '') AS DeliveryOption,
                    COALESCE(
                        (SELECT GROUP_CONCAT(
                            CONCAT(ProductName, ' (', Quantity, ')') 
                            ORDER BY OrderItemID 
                            SEPARATOR ', '
                        )
                        FROM order_items 
                        WHERE OrderID = o.OrderID
                        LIMIT 1000), 
                        ''
                    ) AS OrderedProducts
                 FROM orders o
                 LEFT JOIN customers c ON o.CustomerID = c.CustomerID
                 WHERE o.OrderStatus = 'Confirmed'
                 AND o.OrderDate >= @startDate
                 AND o.OrderDate <= @endDate
                 ORDER BY o.OrderDate, o.OrderTime"

            openConn()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@startDate", monthStart.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@endDate", monthEnd.ToString("yyyy-MM-dd"))

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim orderInfo As New OrderInfo With {
                            .OrderID = Convert.ToInt32(reader("OrderID")),
                            .CustomerName = If(reader("CustomerName").ToString().Trim() = "", "Walk-in Customer", reader("CustomerName").ToString()),
                            .OrderType = reader("OrderType").ToString(),
                            .OrderDate = Convert.ToDateTime(reader("OrderDate")),
                            .OrderTime = reader("OrderTime").ToString(),
                            .TotalAmount = Convert.ToDecimal(reader("TotalAmount")),
                            .ItemsOrderedCount = Convert.ToInt32(reader("ItemsOrderedCount")),
                            .OrderedProducts = If(IsDBNull(reader("OrderedProducts")), "N/A", reader("OrderedProducts").ToString()),
                            .ContactNumber = If(IsDBNull(reader("ContactNumber")), "N/A", reader("ContactNumber").ToString()),
                            .SpecialRequests = If(IsDBNull(reader("SpecialRequests")) OrElse String.IsNullOrWhiteSpace(reader("SpecialRequests").ToString()), "None", reader("SpecialRequests").ToString()),
                            .DeliveryAddress = If(IsDBNull(reader("DeliveryAddress")), "N/A", reader("DeliveryAddress").ToString()),
                            .DeliveryOption = If(IsDBNull(reader("DeliveryOption")), "N/A", reader("DeliveryOption").ToString())
                        }
                        orderData.Add(orderInfo)
                    End While
                End Using
            End Using
            closeConn()

        Catch ex As Exception
            MessageBox.Show("Error loading orders: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            closeConn()
        End Try
    End Sub

    ' ==========================================
    ' DISPLAY CALENDAR
    ' ==========================================
    Private Sub DisplayCalendar()
        Try
            ' Clear existing controls
            pnlCalendar.Controls.Clear()

            ' Update month label
            lblMonth.Text = currentMonth.ToString("MMMM yyyy")

            ' Get first day of month and number of days
            Dim firstDay As Date = New Date(currentMonth.Year, currentMonth.Month, 1)
            Dim daysInMonth As Integer = Date.DaysInMonth(currentMonth.Year, currentMonth.Month)
            Dim startDayOfWeek As Integer = firstDay.DayOfWeek ' 0 = Sunday

            ' CALCULATE CELL SIZE BASED ON PANEL WIDTH
            Dim panelWidth As Integer = pnlCalendar.Width - 20
            Dim cellWidth As Integer = CInt(panelWidth / 7)
            Dim cellHeight As Integer = 100

            Dim currentRow As Integer = 0
            Dim currentCol As Integer = startDayOfWeek

            ' Calculate total rows needed
            Dim totalCells As Integer = startDayOfWeek + daysInMonth
            Dim totalRows As Integer = CInt(Math.Ceiling(totalCells / 7))

            ' Set panel height
            pnlCalendar.AutoScrollMinSize = New Size(panelWidth, totalRows * cellHeight)

            ' Create calendar cells
            For day As Integer = 1 To daysInMonth
                Dim currentDate As Date = New Date(currentMonth.Year, currentMonth.Month, day)

                ' Create day panel
                Dim dayPanel As New Panel()
                dayPanel.Size = New Size(cellWidth - 2, cellHeight - 2)
                dayPanel.Location = New Point(currentCol * cellWidth, currentRow * cellHeight)
                dayPanel.BorderStyle = BorderStyle.FixedSingle
                dayPanel.BackColor = Color.White
                dayPanel.Tag = currentDate

                ' Check if date is in the past
                Dim isPastDate As Boolean = currentDate.Date < Date.Today

                If isPastDate Then
                    ' Gray out past dates
                    dayPanel.BackColor = Color.FromArgb(240, 240, 240)
                    dayPanel.Cursor = Cursors.No
                Else
                    ' Highlight today
                    If currentDate.Date = Date.Today Then
                        dayPanel.BackColor = Color.FromArgb(255, 248, 220)
                    End If
                    dayPanel.Cursor = Cursors.Hand
                End If

                ' Day number label
                Dim lblDay As New Label()
                lblDay.Text = day.ToString()
                lblDay.Location = New Point(5, 5)
                lblDay.Size = New Size(40, 25)
                lblDay.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                lblDay.ForeColor = If(isPastDate, Color.Gray, Color.Black)
                dayPanel.Controls.Add(lblDay)

                ' Get orders for this day
                Dim dayOrders = orderData.Where(Function(o) o.OrderDate.Date = currentDate.Date).ToList()

                If dayOrders.Count > 0 Then
                    ' Order count indicator (center of cell)
                    Dim lblIndicator As New Label()
                    lblIndicator.Text = $"🛒 {dayOrders.Count}" & vbCrLf & "Order" & If(dayOrders.Count > 1, "s", "")
                    lblIndicator.Location = New Point(10, 35)
                    lblIndicator.Size = New Size(cellWidth - 20, 50)
                    lblIndicator.TextAlign = ContentAlignment.MiddleCenter
                    lblIndicator.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    lblIndicator.ForeColor = If(isPastDate, Color.Gray, Color.FromArgb(0, 123, 255))
                    lblIndicator.BackColor = Color.Transparent
                    dayPanel.Controls.Add(lblIndicator)

                    ' Only make clickable if not past date
                    If Not isPastDate Then
                        AddHandler dayPanel.Click, Sub() ShowDayOrders(currentDate)
                        AddHandler lblDay.Click, Sub() ShowDayOrders(currentDate)
                        AddHandler lblIndicator.Click, Sub() ShowDayOrders(currentDate)
                    End If
                Else
                    ' No orders message
                    If Not isPastDate Then
                        Dim lblNoOrders As New Label()
                        lblNoOrders.Text = "No orders"
                        lblNoOrders.Location = New Point(10, 40)
                        lblNoOrders.Size = New Size(cellWidth - 20, 40)
                        lblNoOrders.TextAlign = ContentAlignment.MiddleCenter
                        lblNoOrders.Font = New Font("Segoe UI", 8)
                        lblNoOrders.ForeColor = Color.Gray
                        dayPanel.Controls.Add(lblNoOrders)
                    End If
                End If

                pnlCalendar.Controls.Add(dayPanel)

                ' Move to next position
                currentCol += 1
                If currentCol > 6 Then
                    currentCol = 0
                    currentRow += 1
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Error displaying calendar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==========================================
    ' SHOW ALL ORDERS FOR A SPECIFIC DAY IN POPUP
    ' ==========================================
    Private Sub ShowDayOrders(selectedDate As Date)
        Try
            ' Get all orders for this day
            Dim dayOrders = orderData.Where(Function(o) o.OrderDate.Date = selectedDate.Date).OrderBy(Function(o) o.OrderTime).ToList()

            If dayOrders.Count = 0 Then
                MessageBox.Show("No orders found for this date.", "No Orders", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Create popup form
            Dim popupForm As New Form()
            popupForm.Text = $"Orders for {selectedDate:MMMM dd, yyyy}"
            popupForm.Size = New Size(800, 550)
            popupForm.StartPosition = FormStartPosition.CenterParent
            popupForm.FormBorderStyle = FormBorderStyle.FixedDialog
            popupForm.MaximizeBox = False
            popupForm.MinimizeBox = False

            ' Header panel
            Dim headerPanel As New Panel()
            headerPanel.Dock = DockStyle.Top
            headerPanel.Height = 60
            headerPanel.BackColor = Color.FromArgb(52, 73, 94)

            Dim lblHeader As New Label()
            lblHeader.Text = $"🛒 {selectedDate:dddd, MMMM dd, yyyy}" & vbCrLf & $"{dayOrders.Count} Confirmed Order" & If(dayOrders.Count > 1, "s", "")
            lblHeader.Dock = DockStyle.Fill
            lblHeader.Font = New Font("Segoe UI", 12, FontStyle.Bold)
            lblHeader.ForeColor = Color.White
            lblHeader.TextAlign = ContentAlignment.MiddleCenter
            headerPanel.Controls.Add(lblHeader)

            ' Scrollable panel for orders list
            Dim scrollPanel As New Panel()
            scrollPanel.Dock = DockStyle.Fill
            scrollPanel.AutoScroll = True
            scrollPanel.Padding = New Padding(10)
            scrollPanel.BackColor = Color.White

            Dim yPos As Integer = 10

            For Each ord In dayOrders
                ' Order card panel
                Dim cardPanel As New Panel()
                cardPanel.Location = New Point(10, yPos)
                cardPanel.Size = New Size(740, 120)
                cardPanel.BorderStyle = BorderStyle.FixedSingle
                cardPanel.BackColor = Color.FromArgb(240, 248, 255)
                cardPanel.Padding = New Padding(10)

                ' Order time (large and prominent)
                Dim lblTime As New Label()
                lblTime.Text = ord.OrderTime.ToString()
                lblTime.Location = New Point(10, 10)
                lblTime.Size = New Size(80, 30)
                lblTime.Font = New Font("Segoe UI", 14, FontStyle.Bold)
                lblTime.ForeColor = Color.FromArgb(0, 123, 255)
                cardPanel.Controls.Add(lblTime)

                ' Order details
                Dim lblDetails As New Label()
                lblDetails.Text = $"Order #{ord.OrderID}   |   Customer: {ord.CustomerName}" & vbCrLf &
                                $"Type: {ord.OrderType}   |   Items: {ord.ItemsOrderedCount}   |   Total: ₱{ord.TotalAmount:N2}" & vbCrLf &
                                $"Delivery: {ord.DeliveryOption}   |   Contact: {ord.ContactNumber}"
                lblDetails.Location = New Point(100, 10)
                lblDetails.Size = New Size(620, 70)
                lblDetails.Font = New Font("Segoe UI", 9)
                lblDetails.ForeColor = Color.FromArgb(52, 73, 94)
                cardPanel.Controls.Add(lblDetails)

                ' Special requests (if any)
                If ord.SpecialRequests <> "None" AndAlso Not String.IsNullOrWhiteSpace(ord.SpecialRequests) Then
                    Dim lblSpecial As New Label()
                    lblSpecial.Text = "📝 " & ord.SpecialRequests
                    lblSpecial.Location = New Point(100, 85)
                    lblSpecial.Size = New Size(620, 25)
                    lblSpecial.Font = New Font("Segoe UI", 8, FontStyle.Italic)
                    lblSpecial.ForeColor = Color.FromArgb(149, 165, 166)
                    cardPanel.Controls.Add(lblSpecial)
                End If

                ' View details button
                Dim btnViewDetails As New Button()
                btnViewDetails.Text = "View Details"
                btnViewDetails.Location = New Point(10, 85)
                btnViewDetails.Size = New Size(80, 25)
                btnViewDetails.BackColor = Color.FromArgb(0, 123, 255)
                btnViewDetails.ForeColor = Color.White
                btnViewDetails.FlatStyle = FlatStyle.Flat
                btnViewDetails.FlatAppearance.BorderSize = 0
                btnViewDetails.Font = New Font("Segoe UI", 8)
                btnViewDetails.Cursor = Cursors.Hand

                Dim ordId As Integer = ord.OrderID
                AddHandler btnViewDetails.Click, Sub()
                                                     ShowOrderDetails(ordId)
                                                 End Sub
                cardPanel.Controls.Add(btnViewDetails)

                scrollPanel.Controls.Add(cardPanel)
                yPos += 130
            Next

            ' Close button at bottom
            Dim btnClose As New Button()
            btnClose.Text = "Close"
            btnClose.Dock = DockStyle.Bottom
            btnClose.Height = 40
            btnClose.BackColor = Color.FromArgb(108, 117, 125)
            btnClose.ForeColor = Color.White
            btnClose.FlatStyle = FlatStyle.Flat
            btnClose.FlatAppearance.BorderSize = 0
            btnClose.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            btnClose.Cursor = Cursors.Hand
            AddHandler btnClose.Click, Sub() popupForm.Close()

            ' Add controls to form
            popupForm.Controls.Add(scrollPanel)
            popupForm.Controls.Add(headerPanel)
            popupForm.Controls.Add(btnClose)

            ' Show popup
            popupForm.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Error showing orders: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==========================================
    ' SHOW ORDER DETAILS
    ' ==========================================
    Private Sub ShowOrderDetails(orderID As Integer)
        Try
            Dim ord = orderData.FirstOrDefault(Function(o) o.OrderID = orderID)

            If ord IsNot Nothing Then
                Dim details As String =
                    $"═══════════════════════════════════════" & vbCrLf &
                    $"            ORDER DETAILS" & vbCrLf &
                    $"═══════════════════════════════════════" & vbCrLf & vbCrLf &
                    $"Order ID: #{ord.OrderID}" & vbCrLf &
                    $"Status: CONFIRMED ✓" & vbCrLf & vbCrLf &
                    $"Customer Information:" & vbCrLf &
                    $"  • Name: {ord.CustomerName}" & vbCrLf &
                    $"  • Contact: {ord.ContactNumber}" & vbCrLf & vbCrLf &
                    $"Order Details:" & vbCrLf &
                    $"  • Type: {ord.OrderType}" & vbCrLf &
                    $"  • Date: {ord.OrderDate:MMMM dd, yyyy (dddd)}" & vbCrLf &
                    $"  • Time: {ord.OrderTime}" & vbCrLf &
                    $"  • Items Ordered: {ord.ItemsOrderedCount}" & vbCrLf &
                    $"  • Total Amount: ₱{ord.TotalAmount:N2}" & vbCrLf & vbCrLf &
                    $"Products:" & vbCrLf &
                    $"  {ord.OrderedProducts}" & vbCrLf & vbCrLf &
                    $"Delivery Information:" & vbCrLf &
                    $"  • Option: {ord.DeliveryOption}" & vbCrLf &
                    $"  • Address: {ord.DeliveryAddress}" & vbCrLf & vbCrLf &
                    $"Special Requests:" & vbCrLf &
                    $"  {ord.SpecialRequests}" & vbCrLf & vbCrLf &
                    $"═══════════════════════════════════════"

                MessageBox.Show(details, "Order Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error showing details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==========================================
    ' NAVIGATION BUTTONS
    ' ==========================================
    Private Sub btnPrevMonth_Click(sender As Object, e As EventArgs) Handles btnPrevMonth.Click
        currentMonth = currentMonth.AddMonths(-1)
        LoadOrders()
        DisplayCalendar()
    End Sub

    Private Sub btnNextMonth_Click(sender As Object, e As EventArgs) Handles btnNextMonth.Click
        currentMonth = currentMonth.AddMonths(1)
        LoadOrders()
        DisplayCalendar()
    End Sub

    Private Sub btnToday_Click(sender As Object, e As EventArgs) Handles btnToday.Click
        currentMonth = New Date(Date.Now.Year, Date.Now.Month, 1)
        LoadOrders()
        DisplayCalendar()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadOrders()
        DisplayCalendar()
        MessageBox.Show("Calendar refreshed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub OrderCalendar_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If orderData IsNot Nothing Then
            DisplayCalendar()
        End If
    End Sub
End Class