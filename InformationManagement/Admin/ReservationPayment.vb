Imports MySqlConnector
Imports System.Data
Imports System.IO
Imports System.Net

Public Class ReservationPayment

    ' Pagination variables
    Private CurrentPage As Integer = 1
    Private RecordsPerPage As Integer = 50
    Private TotalRecords As Integer = 0
    Private CurrentCondition As String = ""

    Private Sub ReservationPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Initialize pagination controls
            SetupPaginationControls()
            LoadReservationPayments()
            UpdateTotal()
        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================================
    ' LOAD RESERVATION PAYMENTS WITH CUSTOMER INFO
    ' =============================================================
    Private Sub LoadReservationPayments(Optional condition As String = "")
        Try
            CurrentCondition = condition

            ' Check if DataGridView is initialized
            If Reservation Is Nothing Then
                MessageBox.Show("DataGridView is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim query As String =
        "SELECT 
            rp.PaymentID,
            rp.ReservationID,
            r.CustomerID,
            c.FirstName,
            c.LastName,
            c.Email,
            c.ContactNumber AS CustomerContact,
            r.ContactNumber AS ReservationContact,
            r.EventType,
            r.EventDate,
            rp.PaymentDate,
            rp.PaymentMethod,
            rp.PaymentStatus,
            rp.AmountPaid,
            rp.PaymentSource,
            rp.ProofOfPayment,
            rp.ReceiptFileName,
            rp.TransactionID,
            rp.UpdatedDate
        FROM payments rp
        INNER JOIN reservations r ON rp.ReservationID = r.ReservationID
        INNER JOIN customers c ON r.CustomerID = c.CustomerID"

            ' Build count query for pagination
            Dim countQuery As String = "SELECT COUNT(*) FROM payments rp " &
                                   "INNER JOIN reservations r ON rp.ReservationID = r.ReservationID " &
                                   "INNER JOIN customers c ON r.CustomerID = c.CustomerID"

            If condition <> "" Then
                query &= " WHERE " & condition
                countQuery &= " WHERE " & condition
            End If

            ' Get total records
            Try
                openConn()
                Using countCmd As New MySqlCommand(countQuery, conn)
                    TotalRecords = Convert.ToInt32(countCmd.ExecuteScalar())
                End Using
            Finally
                closeConn()
            End Try

            ' Add pagination
            Dim offset As Integer = (CurrentPage - 1) * RecordsPerPage
            ' FIXED: Sort by PaymentDate first, then PaymentID to ensure newest is always on top
            query &= " ORDER BY rp.PaymentDate DESC, rp.PaymentID DESC"
            query &= $" LIMIT {RecordsPerPage} OFFSET {offset}"

            LoadToDGV(query, Reservation, "")

            ' Only format if data was loaded successfully
            If Reservation.Columns.Count > 0 Then
                FormatGrid()
                AddViewButtonColumn()
            End If

            UpdatePaginationInfo()

        Catch ex As Exception
            MessageBox.Show("Error loading reservation payments: " & ex.Message & vbCrLf & vbCrLf &
                      "Stack Trace: " & ex.StackTrace, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Dummy wrapper to call modDB.LoadToDGV
    Private Sub LoadToDGV(query As String, dgv As DataGridView, filter As String)
        Try
            modDB.LoadToDGV(query, dgv, filter)
        Catch ex As Exception
            Throw New Exception("Database connection error: " & ex.Message, ex)
        End Try
    End Sub

    ' =============================================================
    ' ADD VIEW BUTTON COLUMN FOR PROOF OF PAYMENT
    ' =============================================================
    Private Sub AddViewButtonColumn()
        Try
            ' Remove existing button column if it exists
            If Reservation.Columns.Contains("ViewProof") Then
                Reservation.Columns.Remove("ViewProof")
            End If

            ' Create button column
            Dim btnCol As New DataGridViewButtonColumn()
            btnCol.Name = "ViewProof"
            btnCol.HeaderText = "Proof of Payment"
            btnCol.Text = "View"
            btnCol.UseColumnTextForButtonValue = True
            btnCol.Width = 120
            btnCol.DefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255)
            btnCol.DefaultCellStyle.ForeColor = Color.White
            btnCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 105, 217)
            btnCol.DefaultCellStyle.SelectionForeColor = Color.White
            btnCol.FlatStyle = FlatStyle.Flat

            ' Add column at the end
            Reservation.Columns.Add(btnCol)
            btnCol.DisplayIndex = Reservation.Columns.Count - 1
        Catch ex As Exception
            ' Silently handle if column already exists
        End Try
    End Sub

    ' =============================================================
    ' FORMAT GRID + HIDE COLUMNS + SET WIDTHS
    ' =============================================================
    Private Sub FormatGrid()
        Try
            If Reservation.Columns.Count = 0 Then Exit Sub

            ' Hide ID and file columns
            Dim hideCols() As String = {
                "PaymentID",
                "ReservationID",
                "CustomerID",
                "ProofOfPayment",
                "ReceiptFileName",
                "TransactionID"
            }

            For Each colName In hideCols
                If Reservation.Columns.Contains(colName) Then
                    Reservation.Columns(colName).Visible = False
                End If
            Next

            ' Set fixed column widths and order
            If Reservation.Columns.Contains("FirstName") Then
                Reservation.Columns("FirstName").HeaderText = "First Name"
                Reservation.Columns("FirstName").Width = 120
                Reservation.Columns("FirstName").DisplayIndex = 0
            End If

            If Reservation.Columns.Contains("LastName") Then
                Reservation.Columns("LastName").HeaderText = "Last Name"
                Reservation.Columns("LastName").Width = 120
                Reservation.Columns("LastName").DisplayIndex = 1
            End If

            If Reservation.Columns.Contains("Email") Then
                Reservation.Columns("Email").HeaderText = "Email"
                Reservation.Columns("Email").Width = 180
                Reservation.Columns("Email").DisplayIndex = 2
            End If

            If Reservation.Columns.Contains("CustomerContact") Then
                Reservation.Columns("CustomerContact").HeaderText = "Customer Phone"
                Reservation.Columns("CustomerContact").Width = 120
                Reservation.Columns("CustomerContact").DisplayIndex = 3
            End If

            If Reservation.Columns.Contains("ReservationContact") Then
                Reservation.Columns("ReservationContact").HeaderText = "Reservation Phone"
                Reservation.Columns("ReservationContact").Width = 130
                Reservation.Columns("ReservationContact").DisplayIndex = 4
            End If

            If Reservation.Columns.Contains("EventType") Then
                Reservation.Columns("EventType").HeaderText = "Event Type"
                Reservation.Columns("EventType").Width = 120
                Reservation.Columns("EventType").DisplayIndex = 5
            End If

            If Reservation.Columns.Contains("EventDate") Then
                Reservation.Columns("EventDate").HeaderText = "Event Date"
                Reservation.Columns("EventDate").Width = 100
                Reservation.Columns("EventDate").DefaultCellStyle.Format = "MM/dd/yyyy"
                Reservation.Columns("EventDate").DisplayIndex = 6
            End If

            If Reservation.Columns.Contains("PaymentDate") Then
                Reservation.Columns("PaymentDate").HeaderText = "Payment Date"
                Reservation.Columns("PaymentDate").Width = 110
                Reservation.Columns("PaymentDate").DefaultCellStyle.Format = "MM/dd/yyyy"
                Reservation.Columns("PaymentDate").DisplayIndex = 7
            End If

            If Reservation.Columns.Contains("PaymentMethod") Then
                Reservation.Columns("PaymentMethod").HeaderText = "Payment Method"
                Reservation.Columns("PaymentMethod").Width = 120
                Reservation.Columns("PaymentMethod").DisplayIndex = 8
            End If

            If Reservation.Columns.Contains("PaymentStatus") Then
                Reservation.Columns("PaymentStatus").HeaderText = "Payment Status"
                Reservation.Columns("PaymentStatus").Width = 110
                Reservation.Columns("PaymentStatus").DisplayIndex = 9
            End If

            If Reservation.Columns.Contains("AmountPaid") Then
                Reservation.Columns("AmountPaid").HeaderText = "Amount Paid"
                Reservation.Columns("AmountPaid").Width = 120
                Reservation.Columns("AmountPaid").DefaultCellStyle.Format = "₱ #,##0.00"
                Reservation.Columns("AmountPaid").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Reservation.Columns("AmountPaid").DisplayIndex = 10
            End If

            If Reservation.Columns.Contains("PaymentSource") Then
                Reservation.Columns("PaymentSource").HeaderText = "Payment Source"
                Reservation.Columns("PaymentSource").Width = 120
                Reservation.Columns("PaymentSource").DisplayIndex = 11
            End If

            If Reservation.Columns.Contains("UpdatedDate") Then
                Reservation.Columns("UpdatedDate").HeaderText = "Updated Date"
                Reservation.Columns("UpdatedDate").Width = 110
                Reservation.Columns("UpdatedDate").DefaultCellStyle.Format = "MM/dd/yyyy"
                Reservation.Columns("UpdatedDate").DisplayIndex = 12
            End If

            ' Disable auto-sizing to keep fixed widths
            Reservation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            Reservation.ScrollBars = ScrollBars.Both

            ' Other formatting
            Reservation.RowHeadersVisible = False
            Reservation.DefaultCellStyle.Font = New Font("Segoe UI", 10)
            Reservation.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 10)
            Reservation.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 60, 85)
            Reservation.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            Reservation.EnableHeadersVisualStyles = False

        Catch ex As Exception
            MessageBox.Show("Error formatting grid: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================================
    ' HANDLE BUTTON CLICK FOR VIEWING PROOF OF PAYMENT
    ' =============================================================
    Private Sub Reservation_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Reservation.CellContentClick
        Try
            ' Check if the clicked cell is the View button
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
                If Reservation.Columns(e.ColumnIndex).Name = "ViewProof" Then
                    Dim row As DataGridViewRow = Reservation.Rows(e.RowIndex)
                    Dim proofPath As String = If(row.Cells("ProofOfPayment").Value?.ToString(), "")
                    Dim receiptFileName As String = If(row.Cells("ReceiptFileName").Value?.ToString(), "")

                    If String.IsNullOrEmpty(proofPath) Then
                        MessageBox.Show("No proof of payment available for this record.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If

                    ' Show the image in fullscreen
                    ShowProofOfPayment(proofPath, receiptFileName)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error handling cell click: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================================
    ' CONFIGURATION: Set your XAMPP htdocs path and localhost URL
    ' =============================================================
    Private Const WEB_BASE_URL As String = "http://localhost/TrialWeb/TrialWorkIM/Tabeya/"

    ' =============================================================
    ' SHOW PROOF OF PAYMENT IN FULLSCREEN
    ' =============================================================
    Private Sub ShowProofOfPayment(imagePath As String, fileName As String)
        Try
            ' Create fullscreen form
            Dim imageForm As New Form()
            imageForm.Text = "Proof of Payment - " & fileName
            imageForm.WindowState = FormWindowState.Maximized
            imageForm.BackColor = Color.Black
            imageForm.FormBorderStyle = FormBorderStyle.None
            imageForm.StartPosition = FormStartPosition.CenterScreen
            imageForm.KeyPreview = True

            ' Create PictureBox to display image
            Dim pictureBox As New PictureBox()
            pictureBox.Dock = DockStyle.Fill
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom
            pictureBox.BackColor = Color.Black

            ' Create panel for controls
            Dim controlPanel As New Panel()
            controlPanel.Dock = DockStyle.Top
            controlPanel.Height = 50
            controlPanel.BackColor = Color.FromArgb(200, 30, 30, 30)

            ' Create close button
            Dim btnClose As New Button()
            btnClose.Text = "✕ Close (ESC)"
            btnClose.Location = New Point(10, 10)
            btnClose.Size = New Size(120, 30)
            btnClose.BackColor = Color.FromArgb(220, 53, 69)
            btnClose.ForeColor = Color.White
            btnClose.FlatStyle = FlatStyle.Flat
            btnClose.FlatAppearance.BorderSize = 0
            btnClose.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            AddHandler btnClose.Click, Sub() imageForm.Close()

            ' Create label for filename
            Dim lblFileName As New Label()
            lblFileName.Text = fileName
            lblFileName.Location = New Point(150, 15)
            lblFileName.AutoSize = True
            lblFileName.ForeColor = Color.White
            lblFileName.Font = New Font("Segoe UI", 11, FontStyle.Bold)

            controlPanel.Controls.Add(btnClose)
            controlPanel.Controls.Add(lblFileName)

            ' Add controls to form
            imageForm.Controls.Add(pictureBox)
            imageForm.Controls.Add(controlPanel)

            ' Handle ESC key to close
            AddHandler imageForm.KeyDown, Sub(s, e)
                                              If e.KeyCode = Keys.Escape Then
                                                  imageForm.Close()
                                              End If
                                          End Sub

            ' Convert path to URL
            Dim finalUrl As String = ConvertToWebUrl(imagePath)

            ' Load image from URL
            Try
                Dim webClient As New WebClient()
                Dim imageBytes() As Byte = webClient.DownloadData(finalUrl)
                Using ms As New MemoryStream(imageBytes)
                    pictureBox.Image = Image.FromStream(ms)
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading image from server." & vbCrLf & vbCrLf &
                              "URL: " & finalUrl & vbCrLf & vbCrLf &
                              "Error: " & ex.Message & vbCrLf & vbCrLf &
                              "Please ensure:" & vbCrLf &
                              "1. XAMPP Apache is running" & vbCrLf &
                              "2. The file exists at: D:\XAMPP\htdocs\TrialWeb\TrialWorkIM\Tabeya\" & imagePath,
                              "Error Loading Image", MessageBoxButtons.OK, MessageBoxIcon.Error)
                imageForm.Close()
                Return
            End Try

            ' Show the form
            imageForm.ShowDialog()

            ' Dispose image after closing
            If pictureBox.Image IsNot Nothing Then
                pictureBox.Image.Dispose()
            End If

        Catch ex As Exception
            MessageBox.Show("Error displaying proof of payment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================================
    ' CONVERT FILE PATH TO WEB URL
    ' =============================================================
    Private Function ConvertToWebUrl(imagePath As String) As String
        ' If already a URL, return as-is
        If imagePath.StartsWith("http://") OrElse imagePath.StartsWith("https://") Then
            Return imagePath
        End If

        ' If path contains full system path with htdocs
        If imagePath.Contains(":\") AndAlso imagePath.ToLower().Contains("htdocs") Then
            Dim htdocsIndex As Integer = imagePath.ToLower().IndexOf("htdocs")
            If htdocsIndex > 0 Then
                Dim webPath As String = imagePath.Substring(htdocsIndex + 7) ' Skip "htdocs\"
                webPath = webPath.Replace("\", "/")
                Return "http://localhost/" & webPath
            End If
        End If

        ' If relative path (like "uploads/gcash_receipts/...")
        ' Combine with base URL
        Dim cleanPath As String = imagePath.Replace("\", "/")
        If cleanPath.StartsWith("/") Then
            cleanPath = cleanPath.Substring(1)
        End If

        Return WEB_BASE_URL & cleanPath
    End Function

    ' =============================================================
    ' DATA BIND COMPLETE (ensures hidden columns stay hidden)
    ' =============================================================
    Private Sub Reservation_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Reservation.DataBindingComplete
        Try
            FormatGrid()
            AddViewButtonColumn()
        Catch ex As Exception
            ' Silently handle errors during data binding
        End Try
    End Sub

    ' =============================================================
    ' SEARCH - Updated to include customer name and email
    ' =============================================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            Dim keyword As String = txtSearch.Text.Trim()
            CurrentPage = 1

            If keyword = "" Then
                LoadReservationPayments()
            Else
                LoadReservationPayments(
                    $"rp.PaymentID LIKE '%{keyword}%' 
                      OR rp.ReservationID LIKE '%{keyword}%' 
                      OR rp.PaymentStatus LIKE '%{keyword}%'
                      OR c.FirstName LIKE '%{keyword}%'
                      OR c.LastName LIKE '%{keyword}%'
                      OR c.Email LIKE '%{keyword}%'
                      OR r.EventType LIKE '%{keyword}%'")
            End If

            UpdateTotal()
        Catch ex As Exception
            MessageBox.Show("Error searching: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================================
    ' REFRESH
    ' =============================================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            txtSearch.Clear()
            CurrentPage = 1
            LoadReservationPayments()
            UpdateTotal()
        Catch ex As Exception
            MessageBox.Show("Error refreshing: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================================
    ' UPDATE TOTAL COUNT
    ' =============================================================
    Private Sub UpdateTotal()
        Try
            If lblTotalRecords IsNot Nothing AndAlso Reservation IsNot Nothing Then
                lblTotalRecords.Text = "Total: " & Reservation.Rows.Count.ToString()
            End If
        Catch ex As Exception
            ' Silently handle if label doesn't exist
        End Try
    End Sub

    ' =============================================================
    ' UPDATE PAYMENT STATUS
    ' =============================================================
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Try
            If Reservation.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a payment record to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedRow As DataGridViewRow = Reservation.SelectedRows(0)
            Dim paymentID As String = selectedRow.Cells("ReservationPaymentID").Value.ToString()
            Dim currentStatus As String = selectedRow.Cells("PaymentStatus").Value.ToString()
            Dim reservationID As String = selectedRow.Cells("ReservationID").Value.ToString()
            Dim customerName As String = selectedRow.Cells("FirstName").Value.ToString() & " " & selectedRow.Cells("LastName").Value.ToString()

            Dim statusForm As New Form()
            statusForm.Text = "Update Payment Status"
            statusForm.Size = New Size(400, 280)
            statusForm.StartPosition = FormStartPosition.CenterParent
            statusForm.FormBorderStyle = FormBorderStyle.FixedDialog
            statusForm.MaximizeBox = False
            statusForm.MinimizeBox = False

            Dim lblInfo As New Label()
            lblInfo.Text = $"Payment ID: {paymentID}" & vbCrLf &
                          $"Reservation ID: {reservationID}" & vbCrLf &
                          $"Customer: {customerName}" & vbCrLf &
                          $"Current Status: {currentStatus}" & vbCrLf & vbCrLf &
                          "Select new status:"
            lblInfo.Location = New Point(20, 20)
            lblInfo.Size = New Size(350, 100)
            lblInfo.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(lblInfo)

            Dim rbCompleted As New RadioButton()
            rbCompleted.Text = "Completed"
            rbCompleted.Location = New Point(30, 130)
            rbCompleted.Size = New Size(120, 25)
            rbCompleted.Font = New Font("Segoe UI", 10)
            rbCompleted.Checked = True
            statusForm.Controls.Add(rbCompleted)

            Dim rbRefunded As New RadioButton()
            rbRefunded.Text = "Refunded"
            rbRefunded.Location = New Point(160, 130)
            rbRefunded.Size = New Size(120, 25)
            rbRefunded.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(rbRefunded)

            Dim rbFailed As New RadioButton()
            rbFailed.Text = "Failed"
            rbFailed.Location = New Point(290, 130)
            rbFailed.Size = New Size(100, 25)
            rbFailed.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(rbFailed)

            Dim btnOK As New Button()
            btnOK.Text = "Update"
            btnOK.Location = New Point(200, 180)
            btnOK.Size = New Size(80, 35)
            btnOK.DialogResult = DialogResult.OK
            btnOK.Font = New Font("Segoe UI", 9)
            statusForm.Controls.Add(btnOK)

            Dim btnCancel As New Button()
            btnCancel.Text = "Cancel"
            btnCancel.Location = New Point(290, 180)
            btnCancel.Size = New Size(80, 35)
            btnCancel.DialogResult = DialogResult.Cancel
            btnCancel.Font = New Font("Segoe UI", 9)
            statusForm.Controls.Add(btnCancel)

            statusForm.AcceptButton = btnOK
            statusForm.CancelButton = btnCancel

            If statusForm.ShowDialog() = DialogResult.OK Then
                Dim newStatus As String = ""

                If rbCompleted.Checked Then
                    newStatus = "Completed"
                ElseIf rbRefunded.Checked Then
                    newStatus = "Refunded"
                ElseIf rbFailed.Checked Then
                    newStatus = "Failed"
                End If

                If newStatus.ToLower() = currentStatus.ToLower() Then
                    MessageBox.Show($"Payment status is already '{currentStatus}'.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                Dim updateQuery As String = $"UPDATE payments 
                                             SET PaymentStatus = '{newStatus}', 
                                                 UpdatedDate = NOW() 
                                             WHERE PaymentID = '{paymentID}'"

                modDB.readQuery(updateQuery)

                MessageBox.Show($"Payment status updated to '{newStatus}' successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LoadReservationPayments()
                UpdateTotal()
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating payment status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================================
    ' DELETE PAYMENT
    ' =============================================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Reservation.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a payment record to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedRow As DataGridViewRow = Reservation.SelectedRows(0)
            Dim paymentID As String = selectedRow.Cells("ReservationPaymentID").Value.ToString()
            Dim reservationID As String = selectedRow.Cells("ReservationID").Value.ToString()
            Dim amountPaid As Decimal = Convert.ToDecimal(selectedRow.Cells("AmountPaid").Value)
            Dim customerName As String = selectedRow.Cells("FirstName").Value.ToString() & " " & selectedRow.Cells("LastName").Value.ToString()

            Dim result As DialogResult = MessageBox.Show(
                $"Are you sure you want to delete this payment record?" & vbCrLf &
                $"Payment ID: {paymentID}" & vbCrLf &
                $"Reservation ID: {reservationID}" & vbCrLf &
                $"Customer: {customerName}" & vbCrLf &
                $"Amount: ₱{amountPaid:N2}" & vbCrLf & vbCrLf &
                "This action cannot be undone!",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                Dim deleteQuery As String = $"DELETE FROM payments 
                                             WHERE PaymentID = '{paymentID}'"

                modDB.readQuery(deleteQuery)

                MessageBox.Show("Payment record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LoadReservationPayments()
                UpdateTotal()
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting payment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================================
    ' PAGINATION HELPERS
    ' =============================================================
    Private Sub SetupPaginationControls()
        Try
            If cboRecordsPerPage IsNot Nothing Then
                cboRecordsPerPage.Items.Clear()
                cboRecordsPerPage.Items.AddRange(New Object() {10, 25, 50, 100})
                cboRecordsPerPage.SelectedItem = 50
                RecordsPerPage = CInt(cboRecordsPerPage.SelectedItem)
            End If
            UpdatePaginationButtons()
        Catch ex As Exception
            ' Ignore UI init errors
        End Try
    End Sub

    Private Sub UpdatePaginationInfo()
        Try
            Dim totalPages As Integer = If(TotalRecords > 0, CInt(Math.Ceiling(TotalRecords / RecordsPerPage)), 1)
            Dim startRecord As Integer = If(TotalRecords > 0, (CurrentPage - 1) * RecordsPerPage + 1, 0)
            Dim endRecord As Integer = Math.Min(CurrentPage * RecordsPerPage, TotalRecords)

            If lblTotalRecords IsNot Nothing Then
                lblTotalRecords.Text = $"Total: {TotalRecords} | Showing {startRecord} - {endRecord}"
            End If

            If lblPageInfo IsNot Nothing Then
                lblPageInfo.Text = $"Page {CurrentPage} of {totalPages}"
            End If

            UpdatePaginationButtons()
        Catch ex As Exception
            ' Ignore UI update errors
        End Try
    End Sub

    Private Sub UpdatePaginationButtons()
        Try
            Dim totalPages As Integer = If(TotalRecords > 0, CInt(Math.Ceiling(TotalRecords / RecordsPerPage)), 1)

            If btnFirstPage IsNot Nothing Then btnFirstPage.Enabled = (CurrentPage > 1)
            If btnPrevPage IsNot Nothing Then btnPrevPage.Enabled = (CurrentPage > 1)
            If btnNextPage IsNot Nothing Then btnNextPage.Enabled = (CurrentPage < totalPages)
            If btnLastPage IsNot Nothing Then btnLastPage.Enabled = (CurrentPage < totalPages)
        Catch ex As Exception
            ' Ignore UI update errors
        End Try
    End Sub

    Private Sub btnFirstPage_Click(sender As Object, e As EventArgs) Handles btnFirstPage.Click
        CurrentPage = 1
        LoadReservationPayments(CurrentCondition)
    End Sub

    Private Sub btnPrevPage_Click(sender As Object, e As EventArgs) Handles btnPrevPage.Click
        If CurrentPage > 1 Then
            CurrentPage -= 1
            LoadReservationPayments(CurrentCondition)
        End If
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        Dim totalPages As Integer = If(TotalRecords > 0, CInt(Math.Ceiling(TotalRecords / RecordsPerPage)), 1)
        If CurrentPage < totalPages Then
            CurrentPage += 1
            LoadReservationPayments(CurrentCondition)
        End If
    End Sub

    Private Sub btnLastPage_Click(sender As Object, e As EventArgs) Handles btnLastPage.Click
        Dim totalPages As Integer = If(TotalRecords > 0, CInt(Math.Ceiling(TotalRecords / RecordsPerPage)), 1)
        CurrentPage = totalPages
        LoadReservationPayments(CurrentCondition)
    End Sub

    Private Sub cboRecordsPerPage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRecordsPerPage.SelectedIndexChanged
        If cboRecordsPerPage.SelectedItem IsNot Nothing Then
            RecordsPerPage = CInt(cboRecordsPerPage.SelectedItem)
            CurrentPage = 1
            LoadReservationPayments(CurrentCondition)
        End If
    End Sub

End Class