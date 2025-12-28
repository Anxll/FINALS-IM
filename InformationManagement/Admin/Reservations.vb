Imports MySqlConnector
Imports System.Data
Imports System.IO
Imports System.Net

Public Class Reservations
    ' ==========================================
    ' PAGINATION VARIABLES - OPTIMIZED
    ' ==========================================
    Private CurrentPage As Integer = 1
    Private RecordsPerPage As Integer = 20  ' Changed from 50 to 20 for faster loading
    Private TotalRecords As Integer = 0
    Private CurrentCondition As String = ""

    ' Configuration for proof of payment
    Private Const WEB_BASE_URL As String = "http://localhost/TrialWeb/TrialWorkIM/Tabeya/"

    Private Sub Reservations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize pagination dropdown with 20 as default
        cboRecordsPerPage.Items.Clear()
        cboRecordsPerPage.Items.AddRange(New Object() {10, 20, 50, 100})
        cboRecordsPerPage.SelectedItem = 20  ' Changed from 50 to 20

        LoadReservations()
    End Sub

    ' ==========================================
    ' UPDATE STATUS BUTTON (Form Button)
    ' ==========================================
    Private Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        Try
            ' Check if a row is selected
            If Reservation.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a reservation to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get selected reservation details
            Dim selectedRow As DataGridViewRow = Reservation.SelectedRows(0)
            Dim reservationID As Integer = Convert.ToInt32(selectedRow.Cells("ReservationID").Value)
            Dim currentStatus As String = selectedRow.Cells("ReservationStatus").Value.ToString().Trim()

            ' Show dialog to select new status
            Dim statusForm As New Form()
            statusForm.Text = "Update Reservation Status"
            statusForm.Size = New Size(400, 250)
            statusForm.StartPosition = FormStartPosition.CenterParent
            statusForm.FormBorderStyle = FormBorderStyle.FixedDialog
            statusForm.MaximizeBox = False
            statusForm.MinimizeBox = False

            ' Label
            Dim lblInfo As New Label()
            lblInfo.Text = $"Reservation ID: {reservationID}" & vbCrLf &
                          $"Current Status: {currentStatus}" & vbCrLf & vbCrLf &
                          "Select new status:"
            lblInfo.Location = New Point(20, 20)
            lblInfo.Size = New Size(350, 70)
            lblInfo.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(lblInfo)

            ' Radio buttons for status options
            Dim rbPending As New RadioButton()
            rbPending.Text = "Pending"
            rbPending.Location = New Point(30, 100)
            rbPending.Size = New Size(100, 25)
            rbPending.Font = New Font("Segoe UI", 10)
            rbPending.Checked = (currentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
            statusForm.Controls.Add(rbPending)

            Dim rbConfirmed As New RadioButton()
            rbConfirmed.Text = "Confirmed"
            rbConfirmed.Location = New Point(140, 100)
            rbConfirmed.Size = New Size(110, 25)
            rbConfirmed.Font = New Font("Segoe UI", 10)
            rbConfirmed.Checked = (currentStatus.Equals("Confirmed", StringComparison.OrdinalIgnoreCase))
            statusForm.Controls.Add(rbConfirmed)

            Dim rbCancelled As New RadioButton()
            rbCancelled.Text = "Cancelled"
            rbCancelled.Location = New Point(260, 100)
            rbCancelled.Size = New Size(110, 25)
            rbCancelled.Font = New Font("Segoe UI", 10)
            rbCancelled.Checked = (currentStatus.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
            statusForm.Controls.Add(rbCancelled)

            Dim rbCompleted As New RadioButton()
            rbCompleted.Text = "Completed"
            rbCompleted.Location = New Point(30, 130)
            rbCompleted.Size = New Size(120, 25)
            rbCompleted.Font = New Font("Segoe UI", 10)
            rbCompleted.Checked = (currentStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase))
            statusForm.Controls.Add(rbCompleted)

            ' Ensure at least one is checked if none match
            If Not (rbPending.Checked Or rbConfirmed.Checked Or rbCancelled.Checked Or rbCompleted.Checked) Then
                rbPending.Checked = True
            End If

            ' Buttons
            Dim btnOK As New Button()
            btnOK.Text = "Update"
            btnOK.Location = New Point(200, 160)
            btnOK.Size = New Size(80, 35)
            btnOK.DialogResult = DialogResult.OK
            btnOK.Font = New Font("Segoe UI", 9)
            statusForm.Controls.Add(btnOK)

            Dim btnCancel As New Button()
            btnCancel.Text = "Cancel"
            btnCancel.Location = New Point(290, 160)
            btnCancel.Size = New Size(80, 35)
            btnCancel.DialogResult = DialogResult.Cancel
            btnCancel.Font = New Font("Segoe UI", 9)
            statusForm.Controls.Add(btnCancel)

            statusForm.AcceptButton = btnOK
            statusForm.CancelButton = btnCancel

            ' Show the dialog
            If statusForm.ShowDialog() = DialogResult.OK Then
                Dim newStatus As String = ""

                If rbPending.Checked Then
                    newStatus = "Pending"
                ElseIf rbConfirmed.Checked Then
                    newStatus = "Confirmed"
                ElseIf rbCancelled.Checked Then
                    newStatus = "Cancelled"
                ElseIf rbCompleted.Checked Then
                    newStatus = "Completed"
                End If

                ' Check if status is actually changing
                If newStatus.Equals(currentStatus, StringComparison.OrdinalIgnoreCase) Then
                    MessageBox.Show($"Reservation status is already '{currentStatus}'.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' Update the reservation status
                UpdateReservationStatus(reservationID, newStatus)
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating reservation status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==========================================
    ' UPDATE RESERVATION STATUS WITH AUTO PAYMENT UPDATE
    ' ==========================================
    Private Sub UpdateReservationStatus(reservationID As Integer, newStatus As String)
        Try
            openConn()

            ' Start transaction for data integrity
            Dim transaction = conn.BeginTransaction()

            Try
                ' Update reservation status
                Dim query As String = "UPDATE reservations SET ReservationStatus = @status, UpdatedDate = @updated WHERE ReservationID = @id"
                Using cmd As New MySqlCommand(query, conn, transaction)
                    cmd.Parameters.AddWithValue("@status", newStatus)
                    cmd.Parameters.AddWithValue("@updated", DateTime.Now)
                    cmd.Parameters.AddWithValue("@id", reservationID)

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected = 0 Then
                        transaction.Rollback()
                        MessageBox.Show("No reservation was updated. Please check if the reservation exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        closeConn()
                        Return
                    End If
                End Using

                ' If status is changed to "Confirmed", auto-update payment status
                If newStatus = "Confirmed" Then
                    ' Check if payment record exists
                    Dim checkQuery As String = "SELECT COUNT(*) FROM payments WHERE ReservationID = @reservationID"
                    Dim paymentExists As Boolean = False

                    Using checkCmd As New MySqlCommand(checkQuery, conn, transaction)
                        checkCmd.Parameters.AddWithValue("@reservationID", reservationID)
                        Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                        paymentExists = (count > 0)
                    End Using

                    If paymentExists Then
                        ' Update existing payment record
                        Dim updatePaymentQuery As String = "UPDATE payments SET PaymentStatus = 'Completed', PaymentDate = NOW() WHERE ReservationID = @reservationID"
                        Using cmd As New MySqlCommand(updatePaymentQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@reservationID", reservationID)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                End If
                ' If status is changed to "Cancelled", auto-update payment status to "Refunded"
                If newStatus = "Cancelled" Then
                    ' Check if payment record exists
                    Dim checkQuery As String = "SELECT COUNT(*) FROM payments WHERE ReservationID = @reservationID"
                    Dim paymentExists As Boolean = False

                    Using checkCmd As New MySqlCommand(checkQuery, conn, transaction)
                        checkCmd.Parameters.AddWithValue("@reservationID", reservationID)
                        Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                        paymentExists = (count > 0)
                    End Using

                    If paymentExists Then
                        ' Update existing payment record to Refunded
                        Dim updatePaymentQuery As String = "UPDATE payments SET PaymentStatus = 'Refunded', PaymentDate = NOW() WHERE ReservationID = @reservationID"
                        Using cmd As New MySqlCommand(updatePaymentQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@reservationID", reservationID)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                End If

                transaction.Commit()

                Dim message As String = $"Reservation #{reservationID} has been updated to '{newStatus}'."
                If newStatus = "Confirmed" Then
                    message &= vbCrLf & "Payment status automatically set to 'Completed'."
                ElseIf newStatus = "Cancelled" Then
                    message &= vbCrLf & "Payment status automatically set to 'Refunded'."
                End If
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                transaction.Rollback()
                Throw
            End Try

        Catch ex As MySqlException
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error updating reservation: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
            LoadReservations(CurrentCondition)
        End Try
    End Sub

    ' ==========================================
    ' LOAD RESERVATIONS WITH PAGINATION - OPTIMIZED
    ' ==========================================
    Private Sub LoadReservations(Optional condition As String = "")
        Try
            CurrentCondition = condition

            ' Get total count first
            Dim countQuery As String = "SELECT COUNT(*) FROM reservations r LEFT JOIN customers c ON r.CustomerID = c.CustomerID"
            If condition <> "" Then
                countQuery &= " WHERE " & condition
            End If

            openConn()
            Dim countCmd As New MySqlCommand(countQuery, conn)
            TotalRecords = Convert.ToInt32(countCmd.ExecuteScalar())
            closeConn()

            ' Build main query with pagination - OPTIMIZED with payment info
            ' Build main query with pagination - OPTIMIZED with payment info
            Dim query As String =
                "SELECT 
                    r.ReservationID,
                    r.CustomerID,
                    COALESCE(r.FullName, CONCAT(COALESCE(c.FirstName, ''), ' ', COALESCE(c.LastName, ''))) AS CustomerName,
                    COALESCE(r.ContactNumber, c.ContactNumber) AS ContactNumber,
                    r.ReservationType,
                    r.EventType,
                    r.EventDate,
                    r.EventTime,
                    r.NumberOfGuests,
                    r.ProductSelection,
                    r.SpecialRequests,
                    r.ReservationStatus,
                    r.ReservationDate,
                    r.DeliveryAddress,
                    r.DeliveryOption,
                    r.UpdatedDate,
                    COALESCE(p.PaymentMethod, '') AS PaymentMethod,
                    COALESCE(p.PaymentStatus, 'Pending') AS PaymentStatus,
                    COALESCE(p.ProofOfPayment, '') AS ProofOfPayment,
                    COALESCE(p.ReceiptFileName, '') AS ReceiptFileName,
                    COALESCE(
                        (SELECT SUM(ri.TotalPrice) 
                         FROM reservation_items ri 
                         WHERE ri.ReservationID = r.ReservationID), 
                        0.00
                    ) AS TotalAmount
                 FROM reservations r
                 LEFT JOIN customers c ON r.CustomerID = c.CustomerID
                 LEFT JOIN payments p ON r.ReservationID = p.ReservationID"
            If condition <> "" Then
                query &= " WHERE " & condition
            End If

            query &= " ORDER BY r.ReservationID DESC"

            ' Add pagination
            Dim offset As Integer = (CurrentPage - 1) * RecordsPerPage
            query &= $" LIMIT {RecordsPerPage} OFFSET {offset}"

            ' Load results into DGV
            LoadToDGV(query, Reservation)

            ' Ensure newest reservations appear at the top
            If Reservation.Columns.Contains("ReservationID") Then
                Try
                    Reservation.Sort(Reservation.Columns("ReservationID"), ComponentModel.ListSortDirection.Descending)
                Catch
                End Try
            End If
            If Reservation.Rows.Count > 0 Then
                Reservation.FirstDisplayedScrollingRowIndex = 0
            End If

            ' CRITICAL FIX: Call methods in the correct order
            ' 1. Format columns FIRST
            FormatReservationColumns()

            ' 2. Format data SECOND
            FormatReservationData()

            ' 3. Add button column LAST (after all other columns are positioned)
            AddViewProofButtonColumn()

            ' 4. Update pagination info
            UpdatePaginationInfo()

        Catch ex As Exception
            MessageBox.Show("Error loading reservations: " & ex.Message)
        End Try
    End Sub


    ' ==========================================
    ' UPDATE PAGINATION INFO
    ' ==========================================
    Private Sub UpdatePaginationInfo()
        Try
            Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)

            ' Update label with current info
            lblTotalReservations.Text = $"Total: {TotalRecords} | Page {CurrentPage} of {totalPages}"

            ' Enable/disable navigation buttons
            btnFirstPage.Enabled = (CurrentPage > 1)
            btnPrevPage.Enabled = (CurrentPage > 1)
            btnNextPage.Enabled = (CurrentPage < totalPages)
            btnLastPage.Enabled = (CurrentPage < totalPages)

        Catch ex As Exception
            ' Silently handle errors
        End Try
    End Sub

    ' ==========================================
    ' FORMAT DATAGRIDVIEW COLUMNS - UPDATED
    ' ==========================================
    Private Sub FormatReservationColumns()
        Try
            With Reservation
                .SuspendLayout()

                ' Hide ID columns
                If .Columns.Contains("ReservationID") Then .Columns("ReservationID").Visible = False
                If .Columns.Contains("CustomerID") Then .Columns("CustomerID").Visible = False
                If .Columns.Contains("ProofOfPayment") Then .Columns("ProofOfPayment").Visible = False
                If .Columns.Contains("ReceiptFileName") Then .Columns("ReceiptFileName").Visible = False

                ' Customer Name - CONSOLIDATED
                If .Columns.Contains("CustomerName") Then
                    .Columns("CustomerName").HeaderText = "Customer Name"
                    .Columns("CustomerName").Width = 180
                    .Columns("CustomerName").DisplayIndex = 0
                End If

                ' Contact Number - SINGLE COLUMN
                If .Columns.Contains("ContactNumber") Then
                    .Columns("ContactNumber").HeaderText = "Contact Number"
                    .Columns("ContactNumber").Width = 130
                    .Columns("ContactNumber").DisplayIndex = 1
                End If

                ' Reservation Details
                If .Columns.Contains("ReservationType") Then
                    .Columns("ReservationType").HeaderText = "Type"
                    .Columns("ReservationType").Width = 90
                    .Columns("ReservationType").DisplayIndex = 2
                End If

                If .Columns.Contains("EventType") Then
                    .Columns("EventType").HeaderText = "Event"
                    .Columns("EventType").Width = 120
                    .Columns("EventType").DisplayIndex = 3
                End If

                If .Columns.Contains("EventDate") Then
                    .Columns("EventDate").HeaderText = "Event Date"
                    .Columns("EventDate").Width = 100
                    .Columns("EventDate").DefaultCellStyle.Format = "MM/dd/yyyy"
                    .Columns("EventDate").DisplayIndex = 4
                End If

                If .Columns.Contains("EventTime") Then
                    .Columns("EventTime").HeaderText = "Time"
                    .Columns("EventTime").Width = 80
                    .Columns("EventTime").DisplayIndex = 5
                End If

                If .Columns.Contains("NumberOfGuests") Then
                    .Columns("NumberOfGuests").HeaderText = "Guests"
                    .Columns("NumberOfGuests").Width = 70
                    .Columns("NumberOfGuests").DisplayIndex = 6
                End If

                If .Columns.Contains("ProductSelection") Then
                    .Columns("ProductSelection").HeaderText = "Products"
                    .Columns("ProductSelection").Width = 150
                    .Columns("ProductSelection").DisplayIndex = 7
                End If

                ' FIXED: Changed from "Delivery" to "Delivery Option"
                If .Columns.Contains("DeliveryOption") Then
                    .Columns("DeliveryOption").HeaderText = "Delivery Option"
                    .Columns("DeliveryOption").Width = 120
                    .Columns("DeliveryOption").DisplayIndex = 8
                End If

                If .Columns.Contains("DeliveryAddress") Then
                    .Columns("DeliveryAddress").HeaderText = "Address"
                    .Columns("DeliveryAddress").Width = 180
                    .Columns("DeliveryAddress").DisplayIndex = 9
                End If

                If .Columns.Contains("ReservationStatus") Then
                    .Columns("ReservationStatus").HeaderText = "Status"
                    .Columns("ReservationStatus").Width = 90
                    .Columns("ReservationStatus").DisplayIndex = 10
                End If

                If .Columns.Contains("PaymentMethod") Then
                    .Columns("PaymentMethod").HeaderText = "Payment Method"
                    .Columns("PaymentMethod").Width = 120
                    .Columns("PaymentMethod").DisplayIndex = 11
                End If

                ' Payment Status
                If .Columns.Contains("PaymentStatus") Then
                    .Columns("PaymentStatus").HeaderText = "Payment Status"
                    .Columns("PaymentStatus").Width = 120
                    .Columns("PaymentStatus").DisplayIndex = 11
                End If


                ' Total Amount - WITH PROPER PADDING
                If .Columns.Contains("TotalAmount") Then
                    .Columns("TotalAmount").HeaderText = "Total Amount"
                    .Columns("TotalAmount").Width = 130  ' Increased width
                    .Columns("TotalAmount").DefaultCellStyle.Format = "₱#,##0.00"
                    .Columns("TotalAmount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("TotalAmount").DefaultCellStyle.Padding = New Padding(5, 0, 10, 0)  ' Add padding
                    .Columns("TotalAmount").DefaultCellStyle.WrapMode = DataGridViewTriState.False
                    .Columns("TotalAmount").DisplayIndex = 12
                End If

                ' Reservation Status - MOVED AFTER TotalAmount
                If .Columns.Contains("ReservationStatus") Then
                    .Columns("ReservationStatus").HeaderText = "Status"
                    .Columns("ReservationStatus").Width = 90
                    .Columns("ReservationStatus").DisplayIndex = 13
                End If

                ' Special Requests
                If .Columns.Contains("SpecialRequests") Then
                    .Columns("SpecialRequests").HeaderText = "Special Requests"
                    .Columns("SpecialRequests").Width = 150
                    .Columns("SpecialRequests").DisplayIndex = 14
                End If

                ' Reservation Date
                If .Columns.Contains("ReservationDate") Then
                    .Columns("ReservationDate").HeaderText = "Reserved On"
                    .Columns("ReservationDate").Width = 100
                    .Columns("ReservationDate").DefaultCellStyle.Format = "MM/dd/yyyy"
                    .Columns("ReservationDate").DisplayIndex = 15
                End If

                ' Updated Date
                If .Columns.Contains("UpdatedDate") Then
                    .Columns("UpdatedDate").HeaderText = "Last Updated"
                    .Columns("UpdatedDate").Width = 100
                    .Columns("UpdatedDate").DefaultCellStyle.Format = "MM/dd/yyyy"
                    .Columns("UpdatedDate").DisplayIndex = 16
                End If
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                .ScrollBars = ScrollBars.Both

                .ResumeLayout()
            End With

        Catch ex As Exception
            ' Silently handle formatting errors
        End Try
    End Sub


    ' ==========================================
    ' FORMAT RESERVATION DATA - NEW
    ' ==========================================
    Private Sub FormatReservationData()
        Try
            Reservation.SuspendLayout()

            For Each row As DataGridViewRow In Reservation.Rows
                If row.IsNewRow Then Continue For

                ' Handle empty customer name
                Dim customerName As String = If(row.Cells("CustomerName").Value?.ToString(), "").Trim()
                If String.IsNullOrWhiteSpace(customerName) Then
                    row.Cells("CustomerName").Value = "Walk-in Customer"
                    row.Cells("CustomerName").Style.ForeColor = Color.Gray
                End If

                ' Handle empty contact
                If String.IsNullOrWhiteSpace(row.Cells("ContactNumber").Value?.ToString()) Then
                    row.Cells("ContactNumber").Value = "N/A"
                    row.Cells("ContactNumber").Style.ForeColor = Color.Gray
                End If

                ' Handle empty payment method
                If String.IsNullOrWhiteSpace(row.Cells("PaymentMethod").Value?.ToString()) Then
                    row.Cells("PaymentMethod").Value = "N/A"
                    row.Cells("PaymentMethod").Style.ForeColor = Color.Gray
                End If

                ' Handle payment status
                If String.IsNullOrWhiteSpace(row.Cells("PaymentStatus").Value?.ToString()) Then
                    row.Cells("PaymentStatus").Value = "Pending"
                    row.Cells("PaymentStatus").Style.ForeColor = Color.DarkOrange
                End If

                ' Handle empty special requests
                If String.IsNullOrWhiteSpace(row.Cells("SpecialRequests").Value?.ToString()) Then
                    row.Cells("SpecialRequests").Value = "N/A"
                    row.Cells("SpecialRequests").Style.ForeColor = Color.Gray
                End If

                ' Handle empty delivery address
                If String.IsNullOrWhiteSpace(row.Cells("DeliveryAddress").Value?.ToString()) Then
                    row.Cells("DeliveryAddress").Value = "N/A"
                    row.Cells("DeliveryAddress").Style.ForeColor = Color.Gray
                End If
            Next

            Reservation.ResumeLayout()
        Catch ex As Exception
            ' Silently handle errors
        End Try
    End Sub

    ' ==========================================
    ' ADD VIEW PROOF BUTTON COLUMN
    ' ==========================================
    Private Sub AddViewProofButtonColumn()
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
            btnCol.UseColumnTextForButtonValue = False
            btnCol.Width = 120
            btnCol.DefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255)
            btnCol.DefaultCellStyle.ForeColor = Color.White
            btnCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 105, 217)
            btnCol.DefaultCellStyle.SelectionForeColor = Color.White
            btnCol.FlatStyle = FlatStyle.Flat

            ' Add column at the end
            Reservation.Columns.Add(btnCol)
            btnCol.DisplayIndex = Reservation.Columns.Count - 1

            ' Set button text based on proof availability
            For Each row As DataGridViewRow In Reservation.Rows
                If row.IsNewRow Then Continue For

                Dim proofPath As String = If(row.Cells("ProofOfPayment").Value?.ToString(), "")

                If Not String.IsNullOrEmpty(proofPath) Then
                    row.Cells("ViewProof").Value = "View"
                    row.Cells("ViewProof").Style.BackColor = Color.FromArgb(0, 123, 255)
                Else
                    row.Cells("ViewProof").Value = "N/A"
                    row.Cells("ViewProof").Style.BackColor = Color.Gray
                    row.Cells("ViewProof").ReadOnly = True
                End If
            Next

        Catch ex As Exception
            ' Handle silently
        End Try
    End Sub

    ' ==========================================
    ' HANDLE VIEW PROOF BUTTON CLICK
    ' ==========================================
    Private Sub Reservation_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Reservation.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            If Reservation.Columns(e.ColumnIndex).Name = "ViewProof" Then
                Dim row As DataGridViewRow = Reservation.Rows(e.RowIndex)
                Dim proofPath As String = If(row.Cells("ProofOfPayment").Value?.ToString(), "")
                Dim receiptFileName As String = If(row.Cells("ReceiptFileName").Value?.ToString(), "")

                If String.IsNullOrEmpty(proofPath) Then
                    MessageBox.Show("No proof of payment available for this reservation.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' Show the image in fullscreen
                ShowProofOfPayment(proofPath, receiptFileName)
            End If
        End If
    End Sub

    ' ==========================================
    ' SHOW PROOF OF PAYMENT IN FULLSCREEN
    ' ==========================================
    Private Sub ShowProofOfPayment(imagePath As String, fileName As String)
        Try
            Dim imageForm As New Form()
            imageForm.Text = "Proof of Payment - " & fileName
            imageForm.WindowState = FormWindowState.Maximized
            imageForm.BackColor = Color.Black
            imageForm.FormBorderStyle = FormBorderStyle.None
            imageForm.StartPosition = FormStartPosition.CenterScreen
            imageForm.KeyPreview = True

            Dim pictureBox As New PictureBox()
            pictureBox.Dock = DockStyle.Fill
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom
            pictureBox.BackColor = Color.Black

            Dim controlPanel As New Panel()
            controlPanel.Dock = DockStyle.Top
            controlPanel.Height = 50
            controlPanel.BackColor = Color.FromArgb(200, 30, 30, 30)

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

            Dim lblFileName As New Label()
            lblFileName.Text = fileName
            lblFileName.Location = New Point(150, 15)
            lblFileName.AutoSize = True
            lblFileName.ForeColor = Color.White
            lblFileName.Font = New Font("Segoe UI", 11, FontStyle.Bold)

            controlPanel.Controls.Add(btnClose)
            controlPanel.Controls.Add(lblFileName)

            imageForm.Controls.Add(pictureBox)
            imageForm.Controls.Add(controlPanel)

            AddHandler imageForm.KeyDown, Sub(s, e)
                                              If e.KeyCode = Keys.Escape Then
                                                  imageForm.Close()
                                              End If
                                          End Sub

            Dim finalUrl As String = ConvertToWebUrl(imagePath)

            Try
                Dim webClient As New WebClient()
                Dim imageBytes() As Byte = webClient.DownloadData(finalUrl)
                Using ms As New MemoryStream(imageBytes)
                    pictureBox.Image = Image.FromStream(ms)
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading image from server." & vbCrLf & vbCrLf &
                              "URL: " & finalUrl & vbCrLf & vbCrLf &
                              "Error: " & ex.Message,
                              "Error Loading Image", MessageBoxButtons.OK, MessageBoxIcon.Error)
                imageForm.Close()
                Return
            End Try

            imageForm.ShowDialog()

            If pictureBox.Image IsNot Nothing Then
                pictureBox.Image.Dispose()
            End If

        Catch ex As Exception
            MessageBox.Show("Error displaying proof of payment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==========================================
    ' CONVERT FILE PATH TO WEB URL
    ' ==========================================
    Private Function ConvertToWebUrl(imagePath As String) As String
        If imagePath.StartsWith("http://") OrElse imagePath.StartsWith("https://") Then
            Return imagePath
        End If

        If imagePath.Contains(":\") AndAlso imagePath.ToLower().Contains("htdocs") Then
            Dim htdocsIndex As Integer = imagePath.ToLower().IndexOf("htdocs")
            If htdocsIndex > 0 Then
                Dim webPath As String = imagePath.Substring(htdocsIndex + 7)
                webPath = webPath.Replace("\", "/")
                Return "http://localhost/" & webPath
            End If
        End If

        Dim cleanPath As String = imagePath.Replace("\", "/")
        If cleanPath.StartsWith("/") Then
            cleanPath = cleanPath.Substring(1)
        End If

        Return WEB_BASE_URL & cleanPath
    End Function

    ' ==========================================
    ' GENERIC LOAD FUNCTION
    ' ==========================================
    Private Sub LoadToDGV(query As String, dgv As DataGridView)
        Try
            openConn()

            Dim cmd As New MySqlCommand(query, conn)
            cmd.CommandTimeout = 120  ' Increased timeout
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()

            adapter.Fill(dt)
            dgv.DataSource = dt

            closeConn()

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
            closeConn()
        End Try
    End Sub

    ' ==========================================
    ' VIEW ALL
    ' ==========================================
    Private Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        CurrentPage = 1
        LoadReservations()
        lblFilter.Text = "Showing: All Reservations"
    End Sub

    ' ==========================================
    ' VIEW PENDING
    ' ==========================================
    Private Sub btnViewPending_Click(sender As Object, e As EventArgs) Handles btnViewPending.Click
        CurrentPage = 1
        LoadReservations("r.ReservationStatus = 'Pending'")
        lblFilter.Text = "Showing: Pending"
    End Sub

    ' ==========================================
    ' VIEW CONFIRMED
    ' ==========================================
    Private Sub btnViewConfirmed_Click(sender As Object, e As EventArgs) Handles btnViewConfirmed.Click
        CurrentPage = 1
        LoadReservations("r.ReservationStatus = 'Confirmed'")
        lblFilter.Text = "Showing: Confirmed"
    End Sub

    ' ==========================================
    ' VIEW CANCELLED
    ' ==========================================
    Private Sub btnViewCancelled_Click(sender As Object, e As EventArgs) Handles btnViewCancelled.Click
        CurrentPage = 1
        LoadReservations("r.ReservationStatus = 'Cancelled'")
        lblFilter.Text = "Showing: Cancelled"
    End Sub

    ' ==========================================
    ' REFRESH
    ' ==========================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        CurrentPage = 1
        LoadReservations(CurrentCondition)
        If CurrentCondition = "" Then
            lblFilter.Text = "Showing: All Reservations"
        End If
    End Sub

    ' ==========================================
    ' SEARCH BAR - UPDATED
    ' ==========================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim keyword As String = txtSearch.Text.Trim()

        If keyword = "" Then
            CurrentPage = 1
            LoadReservations()
            Exit Sub
        End If

        CurrentPage = 1
        SearchReservations(keyword)
    End Sub

    ' ==========================================
    ' SEARCH RESERVATIONS - UPDATED
    ' ==========================================
    Private Sub SearchReservations(keyword As String)
        Try
            ' Get total count for search results
            Dim countQuery As String = "SELECT COUNT(*) FROM reservations r " &
                                      "LEFT JOIN customers c ON r.CustomerID = c.CustomerID " &
                                      "WHERE CAST(r.ReservationID AS CHAR) LIKE @keyword " &
                                      "OR r.FullName LIKE @keyword " &
                                      "OR c.FirstName LIKE @keyword " &
                                      "OR c.LastName LIKE @keyword " &
                                      "OR r.EventType LIKE @keyword " &
                                      "OR r.ReservationStatus LIKE @keyword"

            openConn()
            Dim countCmd As New MySqlCommand(countQuery, conn)
            countCmd.Parameters.AddWithValue("@keyword", "%" & keyword & "%")
            TotalRecords = Convert.ToInt32(countCmd.ExecuteScalar())
            closeConn()

            ' Build main search query
            ' Build main search query
            Dim query As String =
                "SELECT 
                    r.ReservationID,
                    r.CustomerID,
                    COALESCE(r.FullName, CONCAT(COALESCE(c.FirstName, ''), ' ', COALESCE(c.LastName, ''))) AS CustomerName,
                    COALESCE(r.ContactNumber, c.ContactNumber) AS ContactNumber,
                    r.ReservationType,
                    r.EventType,
                    r.EventDate,
                    r.EventTime,
                    r.NumberOfGuests,
                    r.ProductSelection,
                    r.SpecialRequests,
                    r.ReservationStatus,
                    r.ReservationDate,
                    r.DeliveryAddress,
                    r.DeliveryOption,
                    r.UpdatedDate,
                    COALESCE(p.PaymentMethod, '') AS PaymentMethod,
                    COALESCE(p.PaymentStatus, 'Pending') AS PaymentStatus,
                    COALESCE(p.ProofOfPayment, '') AS ProofOfPayment,
                    COALESCE(p.ReceiptFileName, '') AS ReceiptFileName,
                    COALESCE(
                        (SELECT SUM(ri.TotalPrice) 
                         FROM reservation_items ri 
                         WHERE ri.ReservationID = r.ReservationID), 
                        0.00
                    ) AS TotalAmount
                 FROM reservations r
                 LEFT JOIN customers c ON r.CustomerID = c.CustomerID
                 LEFT JOIN payments p ON r.ReservationID = p.ReservationID
                 WHERE CAST(r.ReservationID AS CHAR) LIKE @keyword 
                 OR r.FullName LIKE @keyword 
                 OR c.FirstName LIKE @keyword 
                 OR c.LastName LIKE @keyword 
                 OR r.EventType LIKE @keyword 
                 OR r.ReservationStatus LIKE @keyword
                 ORDER BY r.ReservationID DESC"

            ' Add pagination
            Dim offset As Integer = (CurrentPage - 1) * RecordsPerPage
            query &= $" LIMIT {RecordsPerPage} OFFSET {offset}"

            ' Load with parameters
            openConn()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.CommandTimeout = 120
            cmd.Parameters.AddWithValue("@keyword", "%" & keyword & "%")

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Reservation.DataSource = dt
            closeConn()

            If Reservation.Columns.Contains("ReservationID") Then
                Try
                    Reservation.Sort(Reservation.Columns("ReservationID"), ComponentModel.ListSortDirection.Descending)
                Catch
                End Try
            End If
            If Reservation.Rows.Count > 0 Then
                Reservation.FirstDisplayedScrollingRowIndex = 0
            End If

            ' CRITICAL FIX: Same order here too
            FormatReservationColumns()
            FormatReservationData()
            AddViewProofButtonColumn()
            UpdatePaginationInfo()

            lblFilter.Text = $"Search results for: {keyword}"

        Catch ex As Exception
            MessageBox.Show("Error searching reservations: " & ex.Message)
            closeConn()
        End Try
    End Sub

    ' ==========================================
    ' DELETE RESERVATION
    ' ==========================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If Reservation.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a reservation to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim id As Integer = Reservation.SelectedRows(0).Cells("ReservationID").Value

        If MessageBox.Show("Are you sure to delete Reservation #" & id & "?",
                           "Confirm Delete",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning) = DialogResult.No Then Exit Sub

        Try
            openConn()

            Dim cmd As New MySqlCommand("DELETE FROM reservations WHERE ReservationID=@id", conn)
            cmd.Parameters.AddWithValue("@id", id)
            cmd.ExecuteNonQuery()

            closeConn()

            MessageBox.Show("Reservation deleted successfully.")
            LoadReservations(CurrentCondition)

        Catch ex As Exception
            MessageBox.Show("Error deleting reservation: " & ex.Message)
            closeConn()
        End Try

    End Sub

    ' ============================================================
    ' PAGINATION BUTTON EVENTS
    ' ============================================================
    Private Sub btnFirstPage_Click(sender As Object, e As EventArgs) Handles btnFirstPage.Click
        CurrentPage = 1

        If txtSearch.Text.Trim() <> "" Then
            SearchReservations(txtSearch.Text.Trim())
        Else
            LoadReservations(CurrentCondition)
        End If
    End Sub

    Private Sub btnPrevPage_Click(sender As Object, e As EventArgs) Handles btnPrevPage.Click
        If CurrentPage > 1 Then
            CurrentPage -= 1

            If txtSearch.Text.Trim() <> "" Then
                SearchReservations(txtSearch.Text.Trim())
            Else
                LoadReservations(CurrentCondition)
            End If
        End If
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)
        If CurrentPage < totalPages Then
            CurrentPage += 1

            If txtSearch.Text.Trim() <> "" Then
                SearchReservations(txtSearch.Text.Trim())
            Else
                LoadReservations(CurrentCondition)
            End If
        End If
    End Sub

    Private Sub btnLastPage_Click(sender As Object, e As EventArgs) Handles btnLastPage.Click
        Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)
        CurrentPage = totalPages

        If txtSearch.Text.Trim() <> "" Then
            SearchReservations(txtSearch.Text.Trim())
        Else
            LoadReservations(CurrentCondition)
        End If
    End Sub

    Private Sub cboRecordsPerPage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRecordsPerPage.SelectedIndexChanged
        If cboRecordsPerPage.SelectedItem IsNot Nothing Then
            RecordsPerPage = CInt(cboRecordsPerPage.SelectedItem)
            CurrentPage = 1

            If txtSearch.Text.Trim() <> "" Then
                SearchReservations(txtSearch.Text.Trim())
            Else
                LoadReservations(CurrentCondition)
            End If
        End If
    End Sub

    ' ============================================================
    ' PAGE INFO LABEL CLICK
    ' ============================================================
    Private Sub lblPageInfo_Click(sender As Object, e As EventArgs) Handles lblPageInfo.Click
        Try
            Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)

            Dim input As String = InputBox($"Enter page number (1 to {totalPages}):", "Go to Page", CurrentPage.ToString())

            If String.IsNullOrWhiteSpace(input) Then
                Return
            End If

            Dim pageNumber As Integer
            If Integer.TryParse(input, pageNumber) Then
                If pageNumber >= 1 AndAlso pageNumber <= totalPages Then
                    CurrentPage = pageNumber

                    If txtSearch.Text.Trim() <> "" Then
                        SearchReservations(txtSearch.Text.Trim())
                    Else
                        LoadReservations(CurrentCondition)
                    End If
                Else
                    MessageBox.Show($"Please enter a valid page number between 1 and {totalPages}.", "Invalid Page", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                MessageBox.Show("Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error navigating to page: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnViewCalendar_Click(sender As Object, e As EventArgs) Handles btnViewCalendar.Click
        Try
            Dim calendarForm As New ReservationCalendar()
            calendarForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Error opening calendar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class