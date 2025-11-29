Imports MySqlConnector
Imports System.Data

Public Class OrderPayment

    Private Sub OrderPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPayments()
        UpdateTotal()
    End Sub

    ' =================================================
    ' LOAD PAYMENTS INTO DATAGRIDVIEW
    ' =================================================
    Private Sub LoadPayments(Optional condition As String = "")
        Try
            Dim query As String =
            "SELECT 
                PaymentID,
                OrderID,
                PaymentDate,
                PaymentMethod,
                PaymentStatus,
                AmountPaid,
                PaymentSource,
                TransactionID,
                Notes
             FROM payments"

            If condition <> "" Then
                query &= " WHERE " & condition
            End If

            LoadToDGV(query, Order, "")
            FormatGrid()

        Catch ex As Exception
            MessageBox.Show("Error loading payments: " & ex.Message)
        End Try
    End Sub

    ' Dummy wrapper to call modDB loader
    Private Sub LoadToDGV(query As String, dgv As DataGridView, filter As String)
        modDB.LoadToDGV(query, dgv, filter)
    End Sub

    ' =================================================
    ' FORMAT GRID + HIDE INTERNAL COLUMNS
    ' =================================================
    Private Sub FormatGrid()
        If Order.Columns.Count = 0 Then Exit Sub

        Dim hideCols() As String = {
            "PaymentID",
            "OrderID",
            "TransactionID"
        }

        For Each colName In hideCols
            If Order.Columns.Contains(colName) Then
                Order.Columns(colName).Visible = False
            End If
        Next

        ' Formatting
        Order.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Order.RowHeadersVisible = False
        Order.DefaultCellStyle.Font = New Font("Segoe UI", 10)
        Order.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 10)

        ' Format column headers with spaces
        If Order.Columns.Contains("PaymentDate") Then
            Order.Columns("PaymentDate").HeaderText = "Payment Date"
        End If

        If Order.Columns.Contains("PaymentMethod") Then
            Order.Columns("PaymentMethod").HeaderText = "Payment Method"
        End If

        If Order.Columns.Contains("PaymentStatus") Then
            Order.Columns("PaymentStatus").HeaderText = "Payment Status"
        End If

        If Order.Columns.Contains("AmountPaid") Then
            Order.Columns("AmountPaid").HeaderText = "Amount Paid"
            Order.Columns("AmountPaid").DefaultCellStyle.Format = "₱ #,##0.00"
            Order.Columns("AmountPaid").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If

        If Order.Columns.Contains("PaymentSource") Then
            Order.Columns("PaymentSource").HeaderText = "Payment Source"
        End If
    End Sub

    ' =================================================
    ' ENSURE COLUMNS STAY HIDDEN AFTER RELOAD
    ' =================================================
    Private Sub Order_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Order.DataBindingComplete
        FormatGrid()
    End Sub

    ' =================================================
    ' SEARCH
    ' =================================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim keyword As String = txtSearch.Text.Trim()

        If keyword = "" Then
            LoadPayments()
        Else
            LoadPayments(
                $"OrderID LIKE '%{keyword}%'
                  OR PaymentID LIKE '%{keyword}%'
                  OR PaymentStatus LIKE '%{keyword}%'
                  OR PaymentMethod LIKE '%{keyword}%'")
        End If

        UpdateTotal()
    End Sub

    ' =================================================
    ' REFRESH BUTTON
    ' =================================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Clear()
        LoadPayments()
        UpdateTotal()
    End Sub

    ' =================================================
    ' UPDATE TOTAL COUNT
    ' =================================================
    Private Sub UpdateTotal()
        lblTotalRecords.Text = "Total: " & Order.Rows.Count.ToString()
    End Sub

    ' =============================================================
    ' UPDATE PAYMENT STATUS - Allows changing status to Completed, Refunded, or Failed
    ' =============================================================
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Try
            ' Check if a row is selected
            If Order.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a payment record to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get the selected row
            Dim selectedRow As DataGridViewRow = Order.SelectedRows(0)
            Dim paymentID As String = selectedRow.Cells("PaymentID").Value.ToString()
            Dim orderID As String = selectedRow.Cells("OrderID").Value.ToString()
            Dim currentStatus As String = selectedRow.Cells("PaymentStatus").Value.ToString()

            ' ============= STATUS SELECTION DIALOG =============
            Dim statusForm As New Form()
            statusForm.Text = "Update Payment Status"
            statusForm.Size = New Size(400, 250)
            statusForm.StartPosition = FormStartPosition.CenterParent
            statusForm.FormBorderStyle = FormBorderStyle.FixedDialog
            statusForm.MaximizeBox = False
            statusForm.MinimizeBox = False

            Dim lblInfo As New Label()
            lblInfo.Text =
            $"Payment ID: {paymentID}" & vbCrLf &
            $"Order ID: {orderID}" & vbCrLf &
            $"Current Status: {currentStatus}" & vbCrLf & vbCrLf &
            "Select new status:"
            lblInfo.Location = New Point(20, 20)
            lblInfo.Size = New Size(350, 80)
            lblInfo.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(lblInfo)

            ' Radio buttons
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

            Dim btnOK As New Button()
            btnOK.Text = "Update"
            btnOK.Location = New Point(200, 160)
            btnOK.Size = New Size(80, 35)
            btnOK.DialogResult = DialogResult.OK
            statusForm.Controls.Add(btnOK)

            Dim btnCancel As New Button()
            btnCancel.Text = "Cancel"
            btnCancel.Location = New Point(290, 160)
            btnCancel.Size = New Size(80, 35)
            btnCancel.DialogResult = DialogResult.Cancel
            statusForm.Controls.Add(btnCancel)

            statusForm.AcceptButton = btnOK
            statusForm.CancelButton = btnCancel

            ' Show dialog
            If statusForm.ShowDialog() = DialogResult.OK Then

                Dim newStatus As String = ""
                If rbCompleted.Checked Then
                    newStatus = "Completed"
                ElseIf rbRefunded.Checked Then
                    newStatus = "Refunded"
                ElseIf rbFailed.Checked Then
                    newStatus = "Failed"
                End If

                ' Prevent status not changing
                If newStatus.ToLower() = currentStatus.ToLower() Then
                    MessageBox.Show($"Payment status is already '{currentStatus}'.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' Perform UPDATE
                Dim updateQuery As String =
                $"UPDATE payments 
                  SET PaymentStatus = '{newStatus}', 
                      PaymentDate = NOW()
                  WHERE PaymentID = '{paymentID}'"

                modDB.readQuery(updateQuery)

                MessageBox.Show($"Payment status updated to '{newStatus}' successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LoadPayments()
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
            If Order.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a payment record to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedRow As DataGridViewRow = Order.SelectedRows(0)
            Dim paymentID As String = selectedRow.Cells("PaymentID").Value.ToString()
            Dim orderID As String = selectedRow.Cells("OrderID").Value.ToString()
            Dim amountPaid As Decimal = Convert.ToDecimal(selectedRow.Cells("AmountPaid").Value)

            Dim result As DialogResult = MessageBox.Show(
                $"Are you sure you want to delete this payment?" & vbCrLf &
                $"Payment ID: {paymentID}" & vbCrLf &
                $"Order ID: {orderID}" & vbCrLf &
                $"Amount: ₱{amountPaid:N2}" & vbCrLf & vbCrLf &
                "This action cannot be undone!",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then

                Dim deleteQuery As String =
                    $"DELETE FROM payments WHERE PaymentID = '{paymentID}'"

                modDB.readQuery(deleteQuery)

                MessageBox.Show("Payment record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LoadPayments()
                UpdateTotal()
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting payment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Class