Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Linq

Public Class Reports
    Public Shared Property Instance As Reports

    ' === SHARED PROPERTY FOR PERIOD SELECTION ===
    Public Shared Property SelectedPeriod As String = "Daily"
    Public Shared Property SelectedYear As Integer = DateTime.Now.Year
    Public Shared Property SelectedMonth As Integer = DateTime.Now.Month

    ' === PDF EXPORT PRIVATE VARIABLES ===
    Private WithEvents prnDoc As New PrintDocument()
    Private activeGrids As New List(Of DataGridView)
    Private activeCharts As New List(Of System.Windows.Forms.DataVisualization.Charting.Chart)
    Private reportTitle As String = ""

    ' === Load Form into Panel1 ===
    Private Sub LoadFormIntoPanel(childForm As Form)
        Panel1.Controls.Clear()

        childForm.TopLevel = False
        childForm.FormBorderStyle = FormBorderStyle.None
        childForm.AutoScroll = False
        childForm.AutoSize = True
        childForm.AutoSizeMode = AutoSizeMode.GrowAndShrink
        childForm.Dock = DockStyle.None

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
        Instance = Me
        InitializeFilters()
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New Size(Me.Width, Me.Height)
        Panel1.AutoSize = False
        Panel1.AutoScroll = True
        Panel1.BorderStyle = BorderStyle.None

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
        FlowLayoutPanel1.Width = Me.ClientSize.Width - 30
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

    Private Sub InitializeFilters()
        ' === INITIALIZE COMBOBOX ===
        reportPeriod.Items.Clear()
        reportPeriod.Items.AddRange(New String() {"Daily", "Weekly", "Monthly", "Yearly"})
        reportPeriod.SelectedIndex = 0 ' Default to "Daily"
        reportPeriod.DropDownStyle = ComboBoxStyle.DropDownList

        ' === INITIALIZE YEAR COMBOBOX ===
        cmbYear.Items.Clear()
        Dim currentYear As Integer = DateTime.Now.Year
        For y As Integer = currentYear - 2 To currentYear + 1
            cmbYear.Items.Add(y)
        Next
        cmbYear.SelectedItem = currentYear
        SelectedYear = currentYear

        ' === INITIALIZE MONTH COMBOBOX ===
        cmbMonth.Items.Clear()
        cmbMonth.Items.Add("All Months") ' Index 0
        Dim monthNames As String() = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
        For i As Integer = 0 To 11
            If Not String.IsNullOrEmpty(monthNames(i)) Then
                cmbMonth.Items.Add(monthNames(i))
            End If
        Next
        cmbMonth.SelectedIndex = DateTime.Now.Month ' Select current month by default
        SelectedMonth = cmbMonth.SelectedIndex

        UpdateFilterVisibility()
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

    Private Sub ComboBox_DrawItem(sender As Object, e As DrawItemEventArgs) _
       Handles reportPeriod.DrawItem

        If e.Index < 0 Then Return
        Dim cmb As ComboBox = DirectCast(sender, ComboBox)
        e.DrawBackground()
        e.Graphics.DrawString(cmb.Items(e.Index).ToString(), cmb.Font, Brushes.Black, e.Bounds)
        e.DrawFocusRectangle()
    End Sub


    ' === PERIOD SELECTION CHANGED ===
    Private Sub reportPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles reportPeriod.SelectedIndexChanged
        ' Update the shared property
        SelectedPeriod = reportPeriod.SelectedItem.ToString()
        UpdateFilterVisibility()
        RefreshCurrentlyLoadedForm()
    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        If cmbYear.SelectedItem IsNot Nothing Then
            SelectedYear = Convert.ToInt32(cmbYear.SelectedItem)
            RefreshCurrentlyLoadedForm()
        End If
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonth.SelectedIndexChanged
        SelectedMonth = cmbMonth.SelectedIndex
        RefreshCurrentlyLoadedForm()
    End Sub

    Private Sub UpdateFilterVisibility()
        ' Month selection is only critical for "Monthly" and "Daily" (if we want to jump to a day in a month)
        ' For now, enable it only for Monthly
        Dim isMonthly As Boolean = (SelectedPeriod = "Monthly")
        cmbMonth.Enabled = isMonthly
        lblMonth.Enabled = isMonthly
    End Sub

    Private Sub RefreshCurrentlyLoadedForm()
        ' Reload the current form data to apply the new filters
        If Panel1.Controls.Count > 0 Then
            Dim currentForm = Panel1.Controls(0)
            
            ' Try to call RefreshData method using reflection
            Try
                Dim mi = currentForm.GetType().GetMethod("RefreshData")
                If mi IsNot Nothing Then
                    mi.Invoke(currentForm, Nothing)
                    Return ' Successful refresh
                End If
            Catch ex As Exception
                ' Log error if needed
            End Try

            ' Fallback: If RefreshData is not available, we have to reload the form
            ' This is less efficient but ensures the data updates.
            For Each ctrl As Control In FlowLayoutPanel1.Controls
                If TypeOf ctrl Is Button Then
                    Dim btn As Button = CType(ctrl, Button)
                    ' White background indicates the active report button in our UI
                    If btn.BackColor = Color.White Then 
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
        End If
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
    ' ============================================
    ' ADD THIS TO Reports.vb to handle navigation
    ' ============================================
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
            MessageBox.Show("Error loading product performance report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' === PUBLIC METHOD FOR PDF EXPORT ===
    Public Sub ExportCurrentReport()
        If Panel1.Controls.Count = 0 Then Return

        Dim currentForm = Panel1.Controls(0)

        ' Set report title based on form type
        reportTitle = currentForm.Text
        If String.IsNullOrEmpty(reportTitle) OrElse reportTitle = "Form" Then
            If TypeOf currentForm Is FormSales Then reportTitle = "Sales Report"
            If TypeOf currentForm Is FormOrders Then reportTitle = "Orders Report"
            If TypeOf currentForm Is FormPayroll Then reportTitle = "Payroll Report"
            If TypeOf currentForm Is FormCateringReservations Then reportTitle = "Catering Report"
            If TypeOf currentForm Is FormReservationStatus Then reportTitle = "Reservation Status"
            If TypeOf currentForm Is FormDineInOrders Then reportTitle = "Dine-In Orders"
            If TypeOf currentForm Is FormTakeOutOrders Then reportTitle = "Take-Out Orders"
            If TypeOf currentForm Is FormCustomerHistory Then reportTitle = "Customer History"
            If TypeOf currentForm Is FormEmployeeAttendance Then reportTitle = "Employee Attendance"
            If TypeOf currentForm Is FormProductPerformance Then reportTitle = "Product Performance"
        End If

        ' Try to find ALL DataGridViews and Charts
        activeGrids.Clear()
        activeCharts.Clear()
        FindAllControls(Of DataGridView)(currentForm, activeGrids)
        FindAllControls(Of System.Windows.Forms.DataVisualization.Charting.Chart)(currentForm, activeCharts)

        If activeGrids.Count = 0 AndAlso activeCharts.Count = 0 Then
            MessageBox.Show("No exportable data (Table or Chart) found in the current report.", "Export Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim sfd As New SaveFileDialog()
            sfd.Filter = "PDF Files (*.pdf)|*.pdf"
            sfd.FileName = $"{reportTitle.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}"

            If sfd.ShowDialog() = DialogResult.OK Then
                ' Setup Print to PDF
                Dim pdfPrinterFound As Boolean = False
                For Each printer As String In PrinterSettings.InstalledPrinters
                    If printer.ToUpper().Contains("PDF") Then
                        prnDoc.PrinterSettings.PrinterName = printer
                        pdfPrinterFound = True
                        Exit For
                    End If
                Next

                If Not pdfPrinterFound Then
                    MessageBox.Show("No PDF printer found. Please install 'Microsoft Print to PDF'.", "Printer Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                prnDoc.PrinterSettings.PrintToFile = True
                prnDoc.PrinterSettings.PrintFileName = sfd.FileName

                ' Auto Landscape for wide tables
                If activeGrids.Count > 0 Then
                    Dim maxCols = activeGrids.Max(Function(g) g.Columns.Count)
                    prnDoc.DefaultPageSettings.Landscape = (maxCols > 6)
                Else
                    prnDoc.DefaultPageSettings.Landscape = False
                End If

                prnDoc.Print()

                ' Ensure application stays steady and focused
                Me.Activate()
                Me.Focus()

                If MessageBox.Show("Report exported successfully as PDF!" & vbCrLf & vbCrLf & "Would you like to open the file now?", "Export Successful", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    ' Open the file if requested
                    Process.Start(sfd.FileName)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error performing PDF export: " & ex.Message & vbCrLf & vbCrLf & "Please make sure the output file is not open in another application.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Helper to find controls recursively (ALL matches)
    Private Sub FindAllControls(Of T As Control)(parent As Control, ByRef list As List(Of T))
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is T Then list.Add(DirectCast(ctrl, T))
            FindAllControls(Of T)(ctrl, list)
        Next
    End Sub

    ' === PRINT PAGE HANDLER ===
    Private Sub prnDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles prnDoc.PrintPage
        Dim g As Graphics = e.Graphics
        Dim fontHeader As New Font("Segoe UI", 20, FontStyle.Bold)
        Dim fontSubHeader As New Font("Segoe UI", 11, FontStyle.Regular)
        Dim fontTable As New Font("Segoe UI", 9)
        Dim fontBold As New Font("Segoe UI", 9, FontStyle.Bold)

        Dim marginX As Integer = e.MarginBounds.Left
        Dim marginY As Integer = e.MarginBounds.Top
        Dim y As Integer = marginY

        ' Draw Store Logo / Name placeholder
        g.DrawString("TABEYA SYSTEM", fontBold, Brushes.DarkRed, marginX, y)
        y += 25

        ' Draw Header
        g.DrawString(reportTitle.ToUpper(), fontHeader, Brushes.Black, marginX, y)
        y += 45

        ' Draw Stats/Filter Info
        g.DrawString($"Period: {SelectedPeriod}  |  Year: {SelectedYear}  |  Generated: {DateTime.Now:MMM dd, yyyy HH:mm}", fontSubHeader, Brushes.Gray, marginX, y)
        y += 40

        g.DrawLine(New Pen(Color.LightGray, 1), marginX, y, e.MarginBounds.Right, y)
        y += 30

        ' Print all visible Charts
        For Each chart In activeCharts
            If Not chart.Visible Then Continue For
            Try
                Using bmp As New Bitmap(chart.Width, chart.Height)
                    chart.DrawToBitmap(bmp, New Rectangle(0, 0, chart.Width, chart.Height))

                    Dim targetWidth As Integer = e.MarginBounds.Width
                    Dim targetHeight As Integer = CInt(bmp.Height * (targetWidth / bmp.Width))

                    If y + targetHeight > e.MarginBounds.Bottom Then
                        ' Simplified: Don't overflow page
                        targetHeight = e.MarginBounds.Bottom - y - 10
                        targetWidth = CInt(bmp.Width * (targetHeight / bmp.Height))
                    End If

                    If targetWidth > 0 And targetHeight > 0 Then
                        g.DrawImage(bmp, marginX, y, targetWidth, targetHeight)
                        y += targetHeight + 40
                    End If
                End Using
            Catch
            End Try
        Next

        ' Print all visible DataGridViews
        For Each grid In activeGrids
            If Not grid.Visible Then Continue For

            Dim colWidths As New List(Of Integer)
            Dim totalGridWidth As Integer = 0

            For Each col As DataGridViewColumn In grid.Columns
                If col.Visible Then
                    colWidths.Add(col.Width)
                    totalGridWidth += col.Width
                End If
            Next

            If totalGridWidth > 0 Then
                Dim scaleFactor As Single = e.MarginBounds.Width / totalGridWidth
                Dim currentX As Integer = marginX

                ' Header
                Dim headerHeight As Integer = 35
                g.FillRectangle(New SolidBrush(Color.FromArgb(240, 240, 240)), New Rectangle(marginX, y, e.MarginBounds.Width, headerHeight))

                Dim colIndex As Integer = 0
                For i As Integer = 0 To grid.Columns.Count - 1
                    Dim col = grid.Columns(i)
                    If col.Visible Then
                        Dim w As Integer = CInt(colWidths(colIndex) * scaleFactor)
                        g.DrawString(col.HeaderText, fontBold, Brushes.Black, New RectangleF(currentX + 5, y + 8, w - 5, headerHeight - 8))
                        g.DrawRectangle(Pens.Gray, currentX, y, w, headerHeight)
                        currentX += w
                        colIndex += 1
                    End If
                Next
                y += headerHeight

                ' Rows
                For rowIdx As Integer = 0 To grid.Rows.Count - 1
                    Dim row = grid.Rows(rowIdx)
                    If row.IsNewRow Then Continue For
                    If y + 30 > e.MarginBounds.Bottom Then Exit For ' Simplified pagination

                    currentX = marginX
                    colIndex = 0
                    For i As Integer = 0 To grid.Columns.Count - 1
                        Dim col = grid.Columns(i)
                        If col.Visible Then
                            Dim w As Integer = CInt(colWidths(colIndex) * scaleFactor)
                            Dim val = If(row.Cells(i).Value IsNot Nothing, row.Cells(i).Value.ToString(), "")
                            g.DrawString(val, fontTable, Brushes.Black, New RectangleF(currentX + 5, y + 5, w - 10, 20))
                            g.DrawRectangle(Pens.LightGray, currentX, y, w, 25)
                            currentX += w
                            colIndex += 1
                        End If
                    Next
                    y += 25
                Next
                y += 40
            End If
        Next

        ' Footer
        Dim footerY As Integer = e.PageBounds.Height - 60
        g.DrawLine(Pens.LightGray, marginX, footerY, e.MarginBounds.Right, footerY)
        g.DrawString("Generated by Tabeya System - Official Report", fontSubHeader, Brushes.LightGray, marginX, footerY + 10)
    End Sub

    Private Sub Reports_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If FlowLayoutPanel1 IsNot Nothing Then
            FlowLayoutPanel1.Width = Me.ClientSize.Width - 40
            ApplyRoundedCorners(FlowLayoutPanel1, 35)
        End If
    End Sub
End Class