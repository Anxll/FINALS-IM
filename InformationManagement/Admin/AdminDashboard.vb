Imports Org.BouncyCastle.Math.EC

Public Class AdminDashboard
    Public Sub New()
        InitializeComponent()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or
                    ControlStyles.AllPaintingInWmPaint, True)
        Me.UpdateStyles()
    End Sub


    Private Sub MakeRoundedButton(btn As Button, radius As Integer)
        Dim path As New Drawing2D.GraphicsPath()
        Dim rect As New Rectangle(0, 0, btn.Width, btn.Height)

        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
        path.CloseAllFigures()

        btn.Region = New Region(path)
    End Sub
    Private currentButton As Button = Nothing

    Private Sub HighlightButton(clickedButton As Button)
        If currentButton IsNot Nothing Then
            currentButton.BackColor = Color.FromArgb(26, 38, 50)
        End If
        clickedButton.BackColor = Color.FromArgb(110, 120, 135)
        currentButton = clickedButton
    End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        HighlightButton(btnDashboard)
        With Dashboard
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill
            Panel1.Controls.Clear()
            Panel1.Controls.Add(Dashboard)
            .BringToFront()
            .Show()
        End With

    End Sub

    Private Sub btnMenuItems_Click(sender As Object, e As EventArgs) Handles btnMenuItems.Click
        HighlightButton(btnMenuItems)
        With MenuItems
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill
            Panel1.Controls.Clear()
            Panel1.Controls.Add(MenuItems)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnReservations_Click(sender As Object, e As EventArgs) Handles btnReservations.Click
        HighlightButton(btnReservations)
        With Reservations
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill
            Panel1.Controls.Clear()
            Panel1.Controls.Add(Reservations)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnUserAccounts_Click(sender As Object, e As EventArgs) Handles btnUserAccounts.Click
        HighlightButton(btnUserAccounts)
        With UsersAccounts
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill
            Panel1.Controls.Clear()
            Panel1.Controls.Add(UsersAccounts)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub btnOrders_Click(sender As Object, e As EventArgs) Handles btnOrders.Click
        HighlightButton(btnOrders)
        With Orders
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill
            Panel1.Controls.Clear()
            Panel1.Controls.Add(Orders)
            .BringToFront()
            .Show()
        End With

    End Sub

    Private Sub btnPayroll_Click(sender As Object, e As EventArgs) Handles btnPayroll.Click
        HighlightButton(btnPayroll)
        With Payroll
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill
            Panel1.Controls.Clear()
            Panel1.Controls.Add(Payroll)
            .BringToFront()
            .Show()
        End With

    End Sub

    Private Sub btnReports_Click(sender As Object, e As EventArgs) Handles btnReports.Click
        HighlightButton(btnReports)
        With Reports
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill
            Panel1.Controls.Clear()
            Panel1.Controls.Add(Reports)
            .BringToFront()
            .Show()
        End With

    End Sub

    Private Sub Inventory_Click(sender As Object, e As EventArgs) Handles Inventory.Click
        HighlightButton(Inventory)

        ' Create an instance of your Inventory form
        Dim inventoryForm As New Inventory() ' Make sure "Inventory" is your FORM, not the button name

        With inventoryForm
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill
            Panel1.Controls.Clear()
            Panel1.Controls.Add(inventoryForm) ' ✅ Correct: add the FORM, not the button
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Employee_Click(sender As Object, e As EventArgs) Handles Employee.Click
        HighlightButton(Employee)

        ' Create an instance of your Employee form
        Dim employeeForm As New Employee() ' Correct variable name

        With employeeForm
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill

            Panel1.Controls.Clear()
            Panel1.Controls.Add(employeeForm)

            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Customer_Click(sender As Object, e As EventArgs) Handles Customer.Click
        HighlightButton(Customer)

        ' Create an instance of your Customer form
        Dim customerForm As New Customer()

        With customerForm
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill

            Panel1.Controls.Clear()
            Panel1.Controls.Add(customerForm)

            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Feedback_Click(sender As Object, e As EventArgs) Handles Feedback.Click
        HighlightButton(Feedback) ' ← Correct button to highlight

        ' Load Feedback form inside Panel1
        Dim feedbackForm As New Feedback() ' If this is really your Feedback form

        With feedbackForm
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill

            Panel1.Controls.Clear()
            Panel1.Controls.Add(feedbackForm)

            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub OrderPayment_Click(sender As Object, e As EventArgs) Handles OrderPayment.Click
        HighlightButton(OrderPayment) ' ← Correct button to highlight

        ' Load Feedback form inside Panel1
        Dim orderpaymentForm As New OrderPayment() ' If this is really your Feedback form

        With orderpaymentForm
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill

            Panel1.Controls.Clear()
            Panel1.Controls.Add(orderpaymentForm)

            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub ReservationPayment_Click(sender As Object, e As EventArgs) Handles ReservationPayment.Click
        HighlightButton(ReservationPayment) ' ← Correct button to highlight

        ' Load Feedback form inside Panel1
        Dim reservationpaymentForm As New ReservationPayment() ' If this is really your Feedback form

        With reservationpaymentForm
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill

            Panel1.Controls.Clear()
            Panel1.Controls.Add(reservationpaymentForm)

            .BringToFront()
            .Show()
        End With
    End Sub
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Panel1.Controls.Clear()

        End If
    End Sub

    Private Sub AdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        MakeRoundedButton(btnDashboard, 15)
        MakeRoundedButton(btnMenuItems, 15)
        MakeRoundedButton(btnUserAccounts, 15)
        MakeRoundedButton(btnReservations, 15)
        MakeRoundedButton(btnOrders, 15)
        MakeRoundedButton(btnPayroll, 15)
        MakeRoundedButton(btnReports, 15)
        HighlightButton(btnDashboard)
        With Dashboard
            .TopLevel = False
            .FormBorderStyle = FormBorderStyle.None
            .Dock = DockStyle.Fill
            Panel1.Controls.Clear()
            Panel1.Controls.Add(Dashboard)
            .BringToFront()
            .Show()
        End With
    End Sub


End Class