Imports System.Drawing.Drawing2D

Public Class Reports

    ' === SHARED PROPERTY FOR PERIOD SELECTION ===
    Public Shared Property SelectedPeriod As String = "Daily"

    ' === Load Form into Panel1 ===
    Private Sub LoadFormIntoPanel(childForm As Form)
        Panel1.Controls.Clear()

        childForm.TopLevel = False
        childForm.FormBorderStyle = FormBorderStyle.None
        childForm.AutoScroll = False
        childForm.AutoSize = True
        childForm.AutoSizeMode = AutoSizeMode.GrowAndShrink
        childForm.Dock = DockStyle.Fill

        ' Add to panel and show
        Panel1.Controls.Add(childForm)
        childForm.Location = New Point(0, 0)
        childForm.Show()

        ' Force layout update
        childForm.PerformLayout()
        Application.DoEvents()

        ' Set AutoScrollMinSize based on child form's actual size
        Panel1.AutoScrollMinSize = New Size(childForm.Width, childForm.Height)
    End Sub


    Private Sub Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New Size(Me.Width, Me.Height)
        Panel1.AutoSize = False
        Panel1.AutoScroll = True
        Panel1.BorderStyle = BorderStyle.None

        ' === INITIALIZE COMBOBOX ===
        reportPeriod.Items.Clear()
        reportPeriod.Items.AddRange(New String() {"Daily", "Weekly", "Monthly", "Yearly"})
        reportPeriod.SelectedIndex = 0 ' Default to "Daily"
        reportPeriod.DropDownStyle = ComboBoxStyle.DropDownList

        ' === FLOWLAYOUTPANEL SETTINGS ===
        FlowLayoutPanel1.AutoScroll = True
        FlowLayoutPanel1.WrapContents = False
        FlowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight
        FlowLayoutPanel1.Padding = New Padding(8)
        FlowLayoutPanel1.Margin = New Padding(0)
        FlowLayoutPanel1.BackColor = Color.FromArgb(240, 240, 240)
        FlowLayoutPanel1.Height = 50
        FlowLayoutPanel1.Top = 80   'Adjust below your label
        FlowLayoutPanel1.Left = 20
        FlowLayoutPanel1.Width = Me.ClientSize.Width - 320

        FlowLayoutPanel1.Height = 70

        FlowLayoutPanel1.AutoSize = False

        ' === APPLY ROUNDED CORNERS TO FLOWLAYOUTPANEL ===
        ApplyRoundedCorners(FlowLayoutPanel1, 35)

        ' === MOVE EXISTING BUTTONS TO FLOWLAYOUTPANEL ===
        Dim toMove As New List(Of Control)
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is Button AndAlso ctrl.Parent Is Me Then
                toMove.Add(ctrl)
            End If
        Next

        For Each ctrl As Control In toMove
            FlowLayoutPanel1.Controls.Add(ctrl)
        Next

        ' Bring FlowLayoutPanel forward so buttons are visible
        FlowLayoutPanel1.BringToFront()
        LoadFormIntoPanel(New FormSales())
        HighlightActiveButton(btnSales)

    End Sub



    Public Sub ResetToDefault()
        LoadFormIntoPanel(New FormSales())
        HighlightActiveButton(btnSales)
        reportPeriod.SelectedIndex = 0 ' Reset to Daily
    End Sub
    ' === APPLY ROUNDED CORNERS TO CONTROL ===
    Private Sub ApplyRoundedCorners(ctrl As Control, radius As Integer)
        Dim gp As New GraphicsPath()
        gp.AddArc(0, 0, radius, radius, 180, 90)
        gp.AddArc(ctrl.Width - radius, 0, radius, radius, 270, 90)
        gp.AddArc(ctrl.Width - radius, ctrl.Height - radius, radius, radius, 0, 90)
        gp.AddArc(0, ctrl.Height - radius, radius, radius, 90, 90)
        gp.CloseFigure()
        ctrl.Region = New Region(gp)
    End Sub

    ' === BUTTON CLICKS ===
    Private Sub Button_Click(sender As Object, e As EventArgs) _
        Handles btnSales.Click, btnOrders.Click, btnPayroll.Click, btnCatering.Click, btnStatus.Click,
                btnDineIn.Click, btnTakeout.Click, btnCustomerHistory.Click, btnEmployeeAttendance.Click, btnProductsPerformance.Click

        Dim clickedBtn As Button = CType(sender, Button)
        HighlightActiveButton(CType(sender, Button))

        Select Case clickedBtn.Name
            Case "btnSales" : LoadFormIntoPanel(New FormSales())
            Case "btnOrders" : LoadFormIntoPanel(New FormOrders())
            Case "btnPayroll" : LoadFormIntoPanel(New FormPayroll())
            Case "btnCatering" : LoadFormIntoPanel(New FormCateringReservations())
            Case "btnStatus" : LoadFormIntoPanel(New FormReservationStatus())
            Case "btnDineIn" : LoadFormIntoPanel(New FormDineInOrders())
            Case "btnTakeout" : LoadFormIntoPanel(New FormTakeOutOrders())
            Case "btnCustomerHistory" : LoadFormIntoPanel(New FormCustomerHistory())
            Case "btnEmployeeAttendance" : LoadFormIntoPanel(New FormEmployeeAttendance())
            Case "btnProductsPerformance" : LoadFormIntoPanel(New FormProductPerformance())
        End Select
    End Sub

    ' === HIGHLIGHT ACTIVE BUTTON WITH PILL SHAPE ===
    Private Sub HighlightActiveButton(activeBtn As Button)
        ' Reset all buttons first
        For Each ctrl As Control In FlowLayoutPanel1.Controls
            If TypeOf ctrl Is Button Then
                Dim btn As Button = CType(ctrl, Button)
                btn.BackColor = Color.FromArgb(240, 240, 240) ' Light gray default
                btn.ForeColor = Color.Black
                btn.FlatAppearance.MouseOverBackColor = btn.BackColor
                btn.Region = Nothing
            End If
        Next

        ' Apply white color to the active (clicked) button
        activeBtn.BackColor = Color.White
        activeBtn.ForeColor = Color.Black
        activeBtn.FlatAppearance.MouseOverBackColor = Color.White

        ' Create pill-shaped rounded corners (fully rounded ends)
        Dim radius As Integer = activeBtn.Height ' Use height as radius for pill shape
        Dim gp As New GraphicsPath()

        ' Left semi-circle
        gp.AddArc(0, 0, radius, radius, 90, 180)
        ' Right semi-circle
        gp.AddArc(activeBtn.Width - radius, 0, radius, radius, 270, 180)

        gp.CloseFigure()
        activeBtn.Region = New Region(gp)
    End Sub

    Private Sub ComboBox_DrawItem(sender As Object, e As DrawItemEventArgs)


        If e.Index < 0 Then Return
        Dim cmb As ComboBox = DirectCast(sender, ComboBox)
        e.DrawBackground()
        e.Graphics.DrawString(cmb.Items(e.Index).ToString(), cmb.Font, Brushes.Black, e.Bounds)
        e.DrawFocusRectangle()
    End Sub


    ' === PERIOD SELECTION CHANGED ===
    Private Sub reportPeriod_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' Update the shared property
        SelectedPeriod = reportPeriod.SelectedItem.ToString()

        ' Reload the current form to apply the new period
        Dim currentForm As Form = Nothing
        If Panel1.Controls.Count > 0 Then
            currentForm = TryCast(Panel1.Controls(0), Form)
        End If

        ' Reload based on the active button
        For Each ctrl As Control In FlowLayoutPanel1.Controls
            If TypeOf ctrl Is Button Then
                Dim btn As Button = CType(ctrl, Button)
                If btn.BackColor = Color.White Then ' Active button
                    Select Case btn.Name
                        Case "btnSales" : LoadFormIntoPanel(New FormSales())
                        Case "btnOrders" : LoadFormIntoPanel(New FormOrders())
                        Case "btnPayroll" : LoadFormIntoPanel(New FormPayroll())
                        Case "btnCatering" : LoadFormIntoPanel(New FormCateringReservations())
                        Case "btnStatus" : LoadFormIntoPanel(New FormReservationStatus())
                        Case "btnDineIn" : LoadFormIntoPanel(New FormDineInOrders())
                        Case "btnTakeout" : LoadFormIntoPanel(New FormTakeOutOrders())
                        Case "btnCustomerHistory" : LoadFormIntoPanel(New FormCustomerHistory())
                        Case "btnEmployeeAttendance" : LoadFormIntoPanel(New FormEmployeeAttendance())
                        Case "btnProductsPerformance" : LoadFormIntoPanel(New FormProductPerformance())
                    End Select
                    Exit For
                End If
            End If
        Next
    End Sub

    ' === HELPER FUNCTION TO GET SQL DATE GROUPING ===
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

    ' === HELPER FUNCTION TO GET DISPLAY FORMAT ===
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
    Private Sub LoadDefaultForm()
        LoadFormIntoPanel(New FormSales())
        HighlightActiveButton(btnSales)
    End Sub

    ' === PUBLIC METHOD TO LOAD CATERING RESERVATIONS FROM EXTERNAL CALL ===
    ' ============================================
    ' PUBLIC METHOD - Load Catering Reservations Report from Dashboard
    ' ============================================

    ' === PUBLIC METHOD TO LOAD CATERING RESERVATIONS FROM EXTERNAL CALL ===
    Public Sub LoadCateringReservationReport()
        Try
            ' Load FormCateringReservations into Panel1
            LoadFormIntoPanel(New FormReservationStatus())

            ' Highlight the Catering Reservations button as active
            HighlightActiveButton(btnStatus)

            ' Scroll to top of the panel
            Panel1.AutoScrollPosition = New Point(0, 0)

            ' Bring Reports form to front and focus
            Me.BringToFront()
            Me.Activate()
            Me.Focus()

            ' Optional: Set the period dropdown to Daily
            If reportPeriod.Items.Contains("Daily") Then
                reportPeriod.SelectedItem = "Daily"
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading catering reservation report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub LoadOrderTrends()
        Try
            ' Load FormCateringReservations into Panel1
            LoadFormIntoPanel(New FormOrders())

            ' Highlight the Catering Reservations button as active
            HighlightActiveButton(btnOrders)

            ' Scroll to top of the panel
            Panel1.AutoScrollPosition = New Point(0, 0)

            ' Bring Reports form to front and focus
            Me.BringToFront()
            Me.Activate()
            Me.Focus()

            ' Optional: Set the period dropdown to Daily
            If reportPeriod.Items.Contains("Daily") Then
                reportPeriod.SelectedItem = "Daily"
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading catering reservation report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LoadSalesReport()
        Try
            ' Load FormCateringReservations into Panel1
            LoadFormIntoPanel(New FormSales())

            ' Highlight the Catering Reservations button as active
            HighlightActiveButton(btnSales)

            ' Scroll to top of the panel
            Panel1.AutoScrollPosition = New Point(0, 0)

            ' Bring Reports form to front and focus
            Me.BringToFront()
            Me.Activate()
            Me.Focus()
        Catch ex As Exception
            MessageBox.Show("Error loading catering reservation report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub LoadProductPerformanceReport()
        Try
            ' Load FormCateringReservations into Panel1
            LoadFormIntoPanel(New FormProductPerformance())

            ' Highlight the Catering Reservations button as active
            HighlightActiveButton(btnProductsPerformance)

            ' Scroll to top of the panel
            Panel1.AutoScrollPosition = New Point(0, 0)

            ' Bring Reports form to front and focus
            Me.BringToFront()
            Me.Activate()
            Me.Focus()
        Catch ex As Exception
            MessageBox.Show("Error loading catering reservation report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class