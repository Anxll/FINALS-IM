Imports MySqlConnector
Imports System.Data
Imports System.IO
Imports System.Net

Public Class OrderPayment

    ' =============================================================
    ' CONFIGURATION: Set your XAMPP htdocs path and localhost URL
    ' =============================================================
    Private Const WEB_BASE_URL As String = "http://localhost/TrialWeb/TrialWorkIM/Tabeya/"

    Private Sub OrderPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPayments()
        UpdateTotal()
    End Sub

    ' =================================================
    ' LOAD PAYMENTS WITH CUSTOMER INFO AND PROOF OF PAYMENT
    ' =================================================
    Private Sub LoadPayments(Optional condition As String = "")
        Try
            Dim query As String =
            "SELECT 
                p.PaymentID,
                p.OrderID,
                o.CustomerID,
                IFNULL(c.FirstName, '') AS FirstName,
                IFNULL(c.LastName, '') AS LastName,
                IFNULL(c.Email, '') AS Email,
                IFNULL(c.ContactNumber, '') AS CustomerContact,
                o.ReceiptNumber,
                o.OrderType,
                o.TotalAmount AS OrderAmount,
                p.PaymentDate,
                p.PaymentMethod,
                p.PaymentStatus,
                p.AmountPaid,
                p.PaymentSource,
                p.ProofOfPayment,
                p.ReceiptFileName,
                p.TransactionID,
                p.Notes
             FROM payments p
             INNER JOIN orders o ON p.OrderID = o.OrderID
             LEFT JOIN customers c ON o.CustomerID = c.CustomerID"

            If condition <> "" Then
                query &= " WHERE " & condition
            End If

            query &= " ORDER BY p.PaymentDate DESC"

            LoadToDGV(query, Order, "")
            FormatGrid()
            AddViewButtonColumn()

        Catch ex As Exception
            MessageBox.Show("Error loading payments: " & ex.Message)
        End Try
    End Sub

    ' Dummy wrapper to call modDB loader
    Private Sub LoadToDGV(query As String, dgv As DataGridView, filter As String)
        modDB.LoadToDGV(query, dgv, filter)
    End Sub

    ' =============================================================
    ' ADD VIEW BUTTON COLUMN FOR PROOF OF PAYMENT
    ' =============================================================
    Private Sub AddViewButtonColumn()
        ' Remove existing button column if it exists
        If Order.Columns.Contains("ViewProof") Then
            Order.Columns.Remove("ViewProof")
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
        Order.Columns.Add(btnCol)
        btnCol.DisplayIndex = Order.Columns.Count - 1
    End Sub

    ' =================================================
    ' FORMAT GRID + HIDE INTERNAL COLUMNS + SET WIDTHS
    ' =================================================
    Private Sub FormatGrid()
        If Order.Columns.Count = 0 Then Exit Sub

        ' Hide ID columns
        Dim hideCols() As String = {
            "PaymentID",
            "OrderID",
            "CustomerID",
            "TransactionID",
            "ProofOfPayment",
            "ReceiptFileName"
        }

        For Each colName In hideCols
            If Order.Columns.Contains(colName) Then
                Order.Columns(colName).Visible = False
            End If
        Next

        ' Customer Information Columns
        If Order.Columns.Contains("FirstName") Then
            Order.Columns("FirstName").HeaderText = "First Name"
            Order.Columns("FirstName").Width = 120
            Order.Columns("FirstName").DisplayIndex = 0
        End If

        If Order.Columns.Contains("LastName") Then
            Order.Columns("LastName").HeaderText = "Last Name"
            Order.Columns("LastName").Width = 120
            Order.Columns("LastName").DisplayIndex = 1
        End If

        If Order.Columns.Contains("Email") Then
            Order.Columns("Email").HeaderText = "Email"
            Order.Columns("Email").Width = 180
            Order.Columns("Email").DisplayIndex = 2
        End If

        If Order.Columns.Contains("CustomerContact") Then
            Order.Columns("CustomerContact").HeaderText = "Contact Number"
            Order.Columns("CustomerContact").Width = 120
            Order.Columns("CustomerContact").DisplayIndex = 3
        End If

        ' Order Information
        If Order.Columns.Contains("ReceiptNumber") Then
            Order.Columns("ReceiptNumber").HeaderText = "Receipt Number"
            Order.Columns("ReceiptNumber").Width = 120
            Order.Columns("ReceiptNumber").DisplayIndex = 4
        End If

        If Order.Columns.Contains("OrderType") Then
            Order.Columns("OrderType").HeaderText = "Order Type"
            Order.Columns("OrderType").Width = 100
            Order.Columns("OrderType").DisplayIndex = 5
        End If

        If Order.Columns.Contains("OrderAmount") Then
            Order.Columns("OrderAmount").HeaderText = "Order Amount"
            Order.Columns("OrderAmount").Width = 110
            Order.Columns("OrderAmount").DefaultCellStyle.Format = "₱ #,##0.00"
            Order.Columns("OrderAmount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Order.Columns("OrderAmount").DisplayIndex = 6
        End If

        ' Payment Information
        If Order.Columns.Contains("PaymentDate") Then
            Order.Columns("PaymentDate").HeaderText = "Payment Date"
            Order.Columns("PaymentDate").Width = 110
            Order.Columns("PaymentDate").DefaultCellStyle.Format = "MM/dd/yyyy"
            Order.Columns("PaymentDate").DisplayIndex = 7
        End If

        If Order.Columns.Contains("PaymentMethod") Then
            Order.Columns("PaymentMethod").HeaderText = "Payment Method"
            Order.Columns("PaymentMethod").Width = 120
            Order.Columns("PaymentMethod").DisplayIndex = 8
        End If

        If Order.Columns.Contains("PaymentStatus") Then
            Order.Columns("PaymentStatus").HeaderText = "Payment Status"
            Order.Columns("PaymentStatus").Width = 110
            Order.Columns("PaymentStatus").DisplayIndex = 9
        End If

        If Order.Columns.Contains("AmountPaid") Then
            Order.Columns("AmountPaid").HeaderText = "Amount Paid"
            Order.Columns("AmountPaid").Width = 120
            Order.Columns("AmountPaid").DefaultCellStyle.Format = "₱ #,##0.00"
            Order.Columns("AmountPaid").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Order.Columns("AmountPaid").DisplayIndex = 10
        End If

        If Order.Columns.Contains("PaymentSource") Then
            Order.Columns("PaymentSource").HeaderText = "Payment Source"
            Order.Columns("PaymentSource").Width = 120
            Order.Columns("PaymentSource").DisplayIndex = 11
        End If

        If Order.Columns.Contains("Notes") Then
            Order.Columns("Notes").HeaderText = "Notes"
            Order.Columns("Notes").Width = 150
            Order.Columns("Notes").DisplayIndex = 12
        End If

        ' Disable auto-sizing
        Order.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        Order.ScrollBars = ScrollBars.Both
        Order.RowHeadersVisible = False
        Order.DefaultCellStyle.Font = New Font("Segoe UI", 8.5)
        Order.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Bold", 9)

        ' Format customer data for walk-in customers
        FormatCustomerData()
    End Sub

    ' =============================================================
    ' HANDLE BUTTON CLICK FOR VIEWING PROOF OF PAYMENT
    ' =============================================================
    Private Sub Order_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Order.CellContentClick
        ' Check if the clicked cell is the View button
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            If Order.Columns(e.ColumnIndex).Name = "ViewProof" Then
                Dim row As DataGridViewRow = Order.Rows(e.RowIndex)
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
    End Sub

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

        ' If relative path (like "uploads/order_receipts/...")
        ' Combine with base URL
        Dim cleanPath As String = imagePath.Replace("\", "/")
        If cleanPath.StartsWith("/") Then
            cleanPath = cleanPath.Substring(1)
        End If

        Return WEB_BASE_URL & cleanPath
    End Function

    ' =================================================
    ' FORMAT CUSTOMER DATA - Show only when CustomerID matches
    ' =================================================
    Private Sub FormatCustomerData()
        Try
            For Each row As DataGridViewRow In Order.Rows
                If row.IsNewRow Then Continue For

                ' Check if customer info is empty (no match)
                Dim firstName As String = If(row.Cells("FirstName").Value?.ToString(), "")
                Dim lastName As String = If(row.Cells("LastName").Value?.ToString(), "")

                ' If all customer fields are empty, show "Walk-in" or "N/A"
                If String.IsNullOrEmpty(firstName) And String.IsNullOrEmpty(lastName) Then
                    row.Cells("FirstName").Value = "Walk-in"
                    row.Cells("LastName").Value = "Customer"
                    row.Cells("Email").Value = "N/A"
                    row.Cells("CustomerContact").Value = "N/A"

                    ' Optional: Style walk-in customers differently
                    row.Cells("FirstName").Style.ForeColor = Color.Gray
                    row.Cells("LastName").Style.ForeColor = Color.Gray
                    row.Cells("Email").Style.ForeColor = Color.Gray
                    row.Cells("CustomerContact").Style.ForeColor = Color.Gray
                End If
            Next
        Catch ex As Exception
            ' Silently handle formatting errors
        End Try
    End Sub

    ' =================================================
    ' GET CUSTOMER NAME - Helper function
    ' =================================================
    Private Function GetCustomerName(row As DataGridViewRow) As String
        Try
            Dim firstName As String = If(row.Cells("FirstName").Value?.ToString(), "")
            Dim lastName As String = If(row.Cells("LastName").Value?.ToString(), "")

            ' Check if this is actual customer data or walk-in
            If firstName = "Walk-in" And lastName = "Customer" Then
                Return "Walk-in Customer"
            ElseIf Not String.IsNullOrEmpty(firstName) OrElse Not String.IsNullOrEmpty(lastName) Then
                Return $"{firstName} {lastName}".Trim()
            Else
                Return "Walk-in Customer"
            End If
        Catch ex As Exception
            Return "Unknown"
        End Try
    End Function

    ' =================================================
    ' ENSURE COLUMNS STAY HIDDEN AFTER RELOAD
    ' =================================================
    Private Sub Order_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Order.DataBindingComplete
        FormatGrid()
        AddViewButtonColumn()
    End Sub

    ' =================================================
    ' SEARCH - Updated to include customer info
    ' =================================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim keyword As String = txtSearch.Text.Trim()

        If keyword = "" Then
            LoadPayments()
        Else
            LoadPayments(
                $"p.PaymentID LIKE '%{keyword}%'
                  OR p.OrderID LIKE '%{keyword}%'
                  OR p.PaymentStatus LIKE '%{keyword}%'
                  OR p.PaymentMethod LIKE '%{keyword}%'
                  OR o.ReceiptNumber LIKE '%{keyword}%'
                  OR c.FirstName LIKE '%{keyword}%'
                  OR c.LastName LIKE '%{keyword}%'
                  OR c.Email LIKE '%{keyword}%'")
        End If

        UpdateTotal()
    End Sub

    ' =================================================
    ' REFRESH BUTTON
    ' =================================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Text = ""
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
            Dim customerName As String = GetCustomerName(selectedRow)
            Dim receiptNumber As String = If(selectedRow.Cells("ReceiptNumber").Value?.ToString(), "N/A")

            ' ============= STATUS SELECTION DIALOG =============
            Dim statusForm As New Form()
            statusForm.Text = "Update Payment Status"
            statusForm.Size = New Size(400, 280)
            statusForm.StartPosition = FormStartPosition.CenterParent
            statusForm.FormBorderStyle = FormBorderStyle.FixedDialog
            statusForm.MaximizeBox = False
            statusForm.MinimizeBox = False

            Dim lblInfo As New Label()
            lblInfo.Text =
            $"Payment ID: {paymentID}" & vbCrLf &
            $"Order ID: {orderID}" & vbCrLf &
            $"Customer: {customerName}" & vbCrLf &
            $"Receipt: {receiptNumber}" & vbCrLf &
            $"Current Status: {currentStatus}" & vbCrLf & vbCrLf &
            "Select new status:"
            lblInfo.Location = New Point(20, 20)
            lblInfo.Size = New Size(350, 110)
            lblInfo.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(lblInfo)

            ' Radio buttons
            Dim rbCompleted As New RadioButton()
            rbCompleted.Text = "Completed"
            rbCompleted.Location = New Point(30, 140)
            rbCompleted.Size = New Size(120, 25)
            rbCompleted.Font = New Font("Segoe UI", 10)
            rbCompleted.Checked = True
            statusForm.Controls.Add(rbCompleted)

            Dim rbRefunded As New RadioButton()
            rbRefunded.Text = "Refunded"
            rbRefunded.Location = New Point(160, 140)
            rbRefunded.Size = New Size(120, 25)
            rbRefunded.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(rbRefunded)

            Dim rbFailed As New RadioButton()
            rbFailed.Text = "Failed"
            rbFailed.Location = New Point(290, 140)
            rbFailed.Size = New Size(100, 25)
            rbFailed.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(rbFailed)

            Dim btnOK As New Button()
            btnOK.Text = "Update"
            btnOK.Location = New Point(200, 190)
            btnOK.Size = New Size(80, 35)
            btnOK.DialogResult = DialogResult.OK
            statusForm.Controls.Add(btnOK)

            Dim btnCancel As New Button()
            btnCancel.Text = "Cancel"
            btnCancel.Location = New Point(290, 190)
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
            Dim customerName As String = GetCustomerName(selectedRow)
            Dim receiptNumber As String = If(selectedRow.Cells("ReceiptNumber").Value?.ToString(), "N/A")

            Dim result As DialogResult = MessageBox.Show(
                $"Are you sure you want to delete this payment?" & vbCrLf & vbCrLf &
                $"Payment ID: {paymentID}" & vbCrLf &
                $"Order ID: {orderID}" & vbCrLf &
                $"Customer: {customerName}" & vbCrLf &
                $"Receipt: {receiptNumber}" & vbCrLf &
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