Imports MySqlConnector
Imports System.Data

Public Class ReservationPayment

    Private Sub ReservationPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadReservationPayments()
        UpdateTotal()
    End Sub

    ' =============================================================
    ' LOAD RESERVATION PAYMENTS
    ' =============================================================
    Private Sub LoadReservationPayments(Optional condition As String = "")
        Try
            Dim query As String =
            "SELECT 
                ReservationPaymentID,
                ReservationID,
                PaymentDate,
                PaymentMethod,
                PaymentStatus,
                AmountPaid,
                PaymentSource,
                ProofOfPayment,
                ReceiptFileName,
                TransactionID,
                UpdatedDate
            FROM reservation_payments"

            If condition <> "" Then
                query &= " WHERE " & condition
            End If

            LoadToDGV(query, Reservation, "")

            FormatGrid()

        Catch ex As Exception
            MessageBox.Show("Error loading reservation payments: " & ex.Message)
        End Try
    End Sub

    ' Dummy wrapper to call modDB.LoadToDGV
    Private Sub LoadToDGV(query As String, dgv As DataGridView, filter As String)
        modDB.LoadToDGV(query, dgv, filter)
    End Sub

    ' =============================================================
    ' FORMAT GRID + HIDE COLUMNS
    ' =============================================================
    Private Sub FormatGrid()
        If Reservation.Columns.Count = 0 Then Exit Sub

        Dim hideCols() As String = {
            "ReservationPaymentID",
            "ReservationID",
            "ProofOfPayment",
            "ReceiptFileName",
            "TransactionID"
        }

        For Each colName In hideCols
            If Reservation.Columns.Contains(colName) Then
                Reservation.Columns(colName).Visible = False
            End If
        Next

        ' Optional formatting
        Reservation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Reservation.RowHeadersVisible = False
        Reservation.DefaultCellStyle.Font = New Font("Segoe UI", 10)
        Reservation.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 10)
        Reservation.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 60, 85)
        Reservation.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        Reservation.EnableHeadersVisualStyles = False

        ' Format column headers with spaces
        If Reservation.Columns.Contains("PaymentDate") Then
            Reservation.Columns("PaymentDate").HeaderText = "Payment Date"
        End If

        If Reservation.Columns.Contains("PaymentMethod") Then
            Reservation.Columns("PaymentMethod").HeaderText = "Payment Method"
        End If

        If Reservation.Columns.Contains("PaymentStatus") Then
            Reservation.Columns("PaymentStatus").HeaderText = "Payment Status"
        End If

        If Reservation.Columns.Contains("AmountPaid") Then
            Reservation.Columns("AmountPaid").HeaderText = "Amount Paid"
            Reservation.Columns("AmountPaid").DefaultCellStyle.Format = "₱ #,##0.00"
            Reservation.Columns("AmountPaid").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If

        If Reservation.Columns.Contains("PaymentSource") Then
            Reservation.Columns("PaymentSource").HeaderText = "Payment Source"
        End If

        If Reservation.Columns.Contains("UpdatedDate") Then
            Reservation.Columns("UpdatedDate").HeaderText = "Updated Date"
        End If
    End Sub

    ' =============================================================
    ' DATA BIND COMPLETE (ensures hidden columns stay hidden)
    ' =============================================================
    Private Sub Reservation_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Reservation.DataBindingComplete
        FormatGrid()
    End Sub

    ' =============================================================
    ' SEARCH
    ' =============================================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim keyword As String = txtSearch.Text.Trim()

        If keyword = "" Then
            LoadReservationPayments()
        Else
            LoadReservationPayments(
                $"ReservationID LIKE '%{keyword}%' 
                  OR ReservationPaymentID LIKE '%{keyword}%' 
                  OR PaymentStatus LIKE '%{keyword}%'")
        End If

        UpdateTotal()
    End Sub

    ' =============================================================
    ' REFRESH
    ' =============================================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        LoadReservationPayments()
        UpdateTotal()
    End Sub

    ' =============================================================
    ' UPDATE TOTAL COUNT
    ' =============================================================
    Private Sub UpdateTotal()
        lblTotalRecords.Text = "Total: " & Reservation.Rows.Count.ToString()
    End Sub

    ' =============================================================
    ' UPDATE PAYMENT STATUS - Allows changing status to Completed, Refunded, or Failed
    ' =============================================================
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Try
            ' Check if a row is selected
            If Reservation.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a payment record to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get the selected row
            Dim selectedRow As DataGridViewRow = Reservation.SelectedRows(0)
            Dim paymentID As String = selectedRow.Cells("ReservationPaymentID").Value.ToString()
            Dim currentStatus As String = selectedRow.Cells("PaymentStatus").Value.ToString()
            Dim reservationID As String = selectedRow.Cells("ReservationID").Value.ToString()

            ' Show dialog to select new status
            Dim statusForm As New Form()
            statusForm.Text = "Update Payment Status"
            statusForm.Size = New Size(400, 250)
            statusForm.StartPosition = FormStartPosition.CenterParent
            statusForm.FormBorderStyle = FormBorderStyle.FixedDialog
            statusForm.MaximizeBox = False
            statusForm.MinimizeBox = False

            ' Label
            Dim lblInfo As New Label()
            lblInfo.Text = $"Payment ID: {paymentID}" & vbCrLf &
                          $"Reservation ID: {reservationID}" & vbCrLf &
                          $"Current Status: {currentStatus}" & vbCrLf & vbCrLf &
                          "Select new status:"
            lblInfo.Location = New Point(20, 20)
            lblInfo.Size = New Size(350, 80)
            lblInfo.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(lblInfo)

            ' Radio buttons for status options
            Dim rbCompleted As New RadioButton()
            rbCompleted.Text = "Completed"
            rbCompleted.Location = New Point(30, 110)
            rbCompleted.Size = New Size(120, 25)
            rbCompleted.Font = New Font("Segoe UI", 10)
            rbCompleted.Checked = True
            statusForm.Controls.Add(rbCompleted)

            Dim rbRefunded As New RadioButton()
            rbRefunded.Text = "Refunded"
            rbRefunded.Location = New Point(160, 110)
            rbRefunded.Size = New Size(120, 25)
            rbRefunded.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(rbRefunded)

            Dim rbFailed As New RadioButton()
            rbFailed.Text = "Failed"
            rbFailed.Location = New Point(290, 110)
            rbFailed.Size = New Size(100, 25)
            rbFailed.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(rbFailed)

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

                If rbCompleted.Checked Then
                    newStatus = "Completed"
                ElseIf rbRefunded.Checked Then
                    newStatus = "Refunded"
                ElseIf rbFailed.Checked Then
                    newStatus = "Failed"
                End If

                ' Check if status is actually changing
                If newStatus.ToLower() = currentStatus.ToLower() Then
                    MessageBox.Show($"Payment status is already '{currentStatus}'.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' Update the payment status
                Dim updateQuery As String = $"UPDATE reservation_payments 
                                             SET PaymentStatus = '{newStatus}', 
                                                 UpdatedDate = NOW() 
                                             WHERE ReservationPaymentID = '{paymentID}'"

                modDB.readQuery(updateQuery)

                MessageBox.Show($"Payment status updated to '{newStatus}' successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Reload the grid
                LoadReservationPayments()
                UpdateTotal()
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating payment status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =============================================================
    ' DELETE PAYMENT - Removes payment record from database
    ' =============================================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            ' Check if a row is selected
            If Reservation.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a payment record to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get the selected row
            Dim selectedRow As DataGridViewRow = Reservation.SelectedRows(0)
            Dim paymentID As String = selectedRow.Cells("ReservationPaymentID").Value.ToString()
            Dim reservationID As String = selectedRow.Cells("ReservationID").Value.ToString()
            Dim amountPaid As Decimal = Convert.ToDecimal(selectedRow.Cells("AmountPaid").Value)

            ' Confirm deletion
            Dim result As DialogResult = MessageBox.Show(
                $"Are you sure you want to delete this payment record?" & vbCrLf &
                $"Payment ID: {paymentID}" & vbCrLf &
                $"Reservation ID: {reservationID}" & vbCrLf &
                $"Amount: ₱{amountPaid:N2}" & vbCrLf & vbCrLf &
                "This action cannot be undone!",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                ' Delete the payment record
                Dim deleteQuery As String = $"DELETE FROM reservation_payments 
                                             WHERE ReservationPaymentID = '{paymentID}'"

                modDB.readQuery(deleteQuery)

                MessageBox.Show("Payment record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Reload the grid
                LoadReservationPayments()
                UpdateTotal()
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting payment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class