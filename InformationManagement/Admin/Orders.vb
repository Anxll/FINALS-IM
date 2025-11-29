Imports MySqlConnector
Imports System.Data

Public Class Orders

    Private Sub Orders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadOrders()
        lblFilter.Text = "Showing: All Orders"

        With DataGridView2
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ReadOnly = True
            .BorderStyle = BorderStyle.None
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
            .DefaultCellStyle.WrapMode = DataGridViewTriState.False
        End With
    End Sub

    ' ============================================================
    ' LOAD ORDERS
    ' ============================================================
    Private Sub LoadOrders(Optional condition As String = "")
        Try
            Dim query As String =
            "SELECT OrderID, CustomerID, EmployeeID, OrderType, OrderSource,
                    ReceiptNumber, NumberOfDiners, OrderDate, OrderTime,
                    ItemsOrderedCount, TotalAmount, OrderStatus, Remarks,
                    OrderPriority, PreparationTimeEstimate, SpecialRequestFlag,
                    CreatedDate, UpdatedDate
             FROM orders"

            If condition <> "" Then
                query &= " WHERE " & condition
            End If

            LoadToDGV(query, DataGridView2)

            ' FORMAT
            With DataGridView2

                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
                .RowHeadersVisible = False

                ' Hide ID columns
                If .Columns.Contains("OrderID") Then .Columns("OrderID").Visible = False
                If .Columns.Contains("CustomerID") Then .Columns("CustomerID").Visible = False
                If .Columns.Contains("EmployeeID") Then .Columns("EmployeeID").Visible = False
                If .Columns.Contains("CreatedDate") Then .Columns("CreatedDate").Visible = False
                If .Columns.Contains("UpdatedDate") Then .Columns("UpdatedDate").Visible = False

                ' Set column headers with proper spacing
                If .Columns.Contains("ReceiptNumber") Then
                    .Columns("ReceiptNumber").HeaderText = "Receipt Number"
                    .Columns("ReceiptNumber").FillWeight = 100
                    .Columns("ReceiptNumber").MinimumWidth = 120
                End If

                If .Columns.Contains("NumberOfDiners") Then
                    .Columns("NumberOfDiners").HeaderText = "Number Of Diners"
                    .Columns("NumberOfDiners").FillWeight = 70
                    .Columns("NumberOfDiners").MinimumWidth = 80
                End If

                If .Columns.Contains("OrderType") Then
                    .Columns("OrderType").HeaderText = "Order Type"
                    .Columns("OrderType").FillWeight = 80
                    .Columns("OrderType").MinimumWidth = 100
                End If

                If .Columns.Contains("OrderSource") Then
                    .Columns("OrderSource").HeaderText = "Order Source"
                    .Columns("OrderSource").FillWeight = 90
                    .Columns("OrderSource").MinimumWidth = 120
                End If

                If .Columns.Contains("ItemsOrderedCount") Then
                    .Columns("ItemsOrderedCount").HeaderText = "Items Ordered"
                    .Columns("ItemsOrderedCount").FillWeight = 70
                    .Columns("ItemsOrderedCount").MinimumWidth = 80
                End If

                If .Columns.Contains("TotalAmount") Then
                    .Columns("TotalAmount").HeaderText = "Total Amount"
                    .Columns("TotalAmount").FillWeight = 90
                    .Columns("TotalAmount").MinimumWidth = 120
                    .Columns("TotalAmount").DefaultCellStyle.Format = "₱#,##0.00"
                End If

                If .Columns.Contains("OrderDate") Then
                    .Columns("OrderDate").HeaderText = "Order Date"
                    .Columns("OrderDate").FillWeight = 90
                    .Columns("OrderDate").MinimumWidth = 120
                End If

                If .Columns.Contains("OrderTime") Then
                    .Columns("OrderTime").HeaderText = "Order Time"
                    .Columns("OrderTime").FillWeight = 80
                    .Columns("OrderTime").MinimumWidth = 100
                End If

                If .Columns.Contains("OrderStatus") Then
                    .Columns("OrderStatus").HeaderText = "Order Status"
                    .Columns("OrderStatus").FillWeight = 90
                    .Columns("OrderStatus").MinimumWidth = 120
                End If

                If .Columns.Contains("Remarks") Then
                    .Columns("Remarks").HeaderText = "Remarks"
                    .Columns("Remarks").FillWeight = 120
                    .Columns("Remarks").MinimumWidth = 160
                End If

                If .Columns.Contains("OrderPriority") Then
                    .Columns("OrderPriority").HeaderText = "Order Priority"
                    .Columns("OrderPriority").FillWeight = 80
                    .Columns("OrderPriority").MinimumWidth = 100
                End If

                If .Columns.Contains("PreparationTimeEstimate") Then
                    .Columns("PreparationTimeEstimate").HeaderText = "Preparation Time"
                    .Columns("PreparationTimeEstimate").FillWeight = 100
                    .Columns("PreparationTimeEstimate").MinimumWidth = 120
                End If

                If .Columns.Contains("SpecialRequestFlag") Then
                    .Columns("SpecialRequestFlag").HeaderText = "Special Request"
                    .Columns("SpecialRequestFlag").FillWeight = 90
                    .Columns("SpecialRequestFlag").MinimumWidth = 120
                End If

                .RowTemplate.Height = 35
                .ColumnHeadersHeight = 40
                .AllowUserToResizeColumns = True
                .AllowUserToResizeRows = False

                ' Style header
                .EnableHeadersVisualStyles = False
                .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94)
                .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
                .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            End With

            lblTotalOrders.Text = "Total Orders: " & DataGridView2.Rows.Count

        Catch ex As Exception
            MessageBox.Show("Error loading orders: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadToDGV(query As String, dgv As DataGridView)
        Try
            Using conn As New MySqlConnection("Server=127.0.0.1;User=root;Password=;Database=tabeya_system")
                conn.Open()

                Using cmd As New MySqlCommand(query, conn)
                    Using da As New MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        dgv.DataSource = dt
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Database Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' UPDATE ORDER STATUS
    ' ============================================================
    Private Sub UpdateOrderStatus(orderID As Integer, newStatus As String)
        Try
            Using conn As New MySqlConnection("Server=127.0.0.1;User=root;Password=;Database=tabeya_system")
                conn.Open()

                Dim query As String =
                    "UPDATE orders SET OrderStatus = @status, UpdatedDate = NOW()
                     WHERE OrderID = @orderID"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@status", newStatus)
                    cmd.Parameters.AddWithValue("@orderID", orderID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show($"Order #{orderID} status updated to '{newStatus}' successfully!",
                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error updating status: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' DELETE ORDER
    ' ============================================================
    Private Sub DeleteOrder(orderID As Integer)
        Try
            If MessageBox.Show($"Are you sure you want to DELETE Order #{orderID}?",
                               "Confirm Delete", MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning) = DialogResult.No Then Exit Sub

            Using conn As New MySqlConnection("Server=127.0.0.1;User=root;Password=;Database=tabeya_system")
                conn.Open()

                Dim query As String = "DELETE FROM orders WHERE OrderID = @orderID"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@orderID", orderID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Order deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadOrders()

        Catch ex As Exception
            MessageBox.Show("Delete Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' CONTEXT MENU FOR ROW ACTIONS
    ' ============================================================
    Private Sub DataGridView2_MouseDown(sender As Object, e As MouseEventArgs) Handles DataGridView2.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim hti As DataGridView.HitTestInfo = DataGridView2.HitTest(e.X, e.Y)
            If hti.RowIndex >= 0 Then
                DataGridView2.ClearSelection()
                DataGridView2.Rows(hti.RowIndex).Selected = True
            End If
        End If
    End Sub

    Private Sub DataGridView2_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
            DataGridView2.ClearSelection()
            DataGridView2.Rows(e.RowIndex).Selected = True

            Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
            Dim orderID As Integer = CInt(row.Cells("OrderID").Value)
            Dim status As String = row.Cells("OrderStatus").Value.ToString()
            Dim orderSource As String = If(row.Cells("OrderSource").Value IsNot Nothing,
                                          row.Cells("OrderSource").Value.ToString(), "")

            ' Create context menu
            Dim contextMenu As New ContextMenuStrip()
            contextMenu.Font = New Font("Segoe UI", 9)

            ' Add options based on status
            If status = "Preparing" Then
                ' For Website orders, show "Complete Order"
                If orderSource.ToLower() = "website" Then
                    Dim completeItem As New ToolStripMenuItem("Complete Order")
                    AddHandler completeItem.Click, Sub() CompleteOrder(orderID)
                    contextMenu.Items.Add(completeItem)
                Else
                    ' For POS/Dine-in orders, show "Serve Order"
                    Dim serveItem As New ToolStripMenuItem("Serve Order")
                    AddHandler serveItem.Click, Sub() ServeOrder(orderID)
                    contextMenu.Items.Add(serveItem)
                End If

                Dim cancelItem As New ToolStripMenuItem("Cancel Order")
                AddHandler cancelItem.Click, Sub() CancelOrder(orderID)
                contextMenu.Items.Add(cancelItem)

                contextMenu.Items.Add(New ToolStripSeparator())
            ElseIf status = "Served" Then
                ' Allow completing served orders
                Dim completeItem As New ToolStripMenuItem("Complete Order")
                AddHandler completeItem.Click, Sub() CompleteOrder(orderID)
                contextMenu.Items.Add(completeItem)

                contextMenu.Items.Add(New ToolStripSeparator())
            End If

            Dim deleteItem As New ToolStripMenuItem("Delete Order")
            deleteItem.ForeColor = Color.DarkRed
            AddHandler deleteItem.Click, Sub() DeleteOrder(orderID)
            contextMenu.Items.Add(deleteItem)

            contextMenu.Items.Add(New ToolStripSeparator())

            Dim viewDetailsItem As New ToolStripMenuItem("View Order Details")
            AddHandler viewDetailsItem.Click, Sub() ViewOrderDetails(orderID)
            contextMenu.Items.Add(viewDetailsItem)

            ' Show menu at cursor position
            Dim mousePos As Point = DataGridView2.PointToClient(Cursor.Position)
            contextMenu.Show(DataGridView2, mousePos)
        End If
    End Sub

    Private Sub CompleteOrder(orderID As Integer)
        If MessageBox.Show($"Mark Order #{orderID} as Completed?",
                          "Complete Order", MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question) = DialogResult.Yes Then
            UpdateOrderStatus(orderID, "Completed")
            LoadOrders()
        End If
    End Sub

    Private Sub ServeOrder(orderID As Integer)
        If MessageBox.Show($"Mark Order #{orderID} as Served?",
                          "Serve Order", MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question) = DialogResult.Yes Then
            UpdateOrderStatus(orderID, "Served")
            LoadOrders()
        End If
    End Sub

    Private Sub ConfirmOrder(orderID As Integer)
        If MessageBox.Show($"Confirm Order #{orderID}?",
                          "Confirm Order", MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question) = DialogResult.Yes Then
            UpdateOrderStatus(orderID, "Confirmed")
            LoadOrders()
        End If
    End Sub

    Private Sub CancelOrder(orderID As Integer)
        If MessageBox.Show($"Cancel Order #{orderID}?",
                          "Cancel Order", MessageBoxButtons.YesNo,
                          MessageBoxIcon.Warning) = DialogResult.Yes Then
            UpdateOrderStatus(orderID, "Cancelled")
            LoadOrders()
        End If
    End Sub

    Private Sub ViewOrderDetails(orderID As Integer)
        Try
            Dim row As DataGridViewRow = DataGridView2.SelectedRows(0)

            Dim details As String = $"Order Details:" & vbCrLf & vbCrLf &
                                   $"Order ID: {orderID}" & vbCrLf &
                                   $"Receipt Number: {row.Cells("ReceiptNumber").Value}" & vbCrLf &
                                   $"Order Type: {row.Cells("OrderType").Value}" & vbCrLf &
                                   $"Order Source: {row.Cells("OrderSource").Value}" & vbCrLf &
                                   $"Number of Diners: {row.Cells("NumberOfDiners").Value}" & vbCrLf &
                                   $"Order Date: {row.Cells("OrderDate").Value}" & vbCrLf &
                                   $"Order Time: {row.Cells("OrderTime").Value}" & vbCrLf &
                                   $"Items Ordered: {row.Cells("ItemsOrderedCount").Value}" & vbCrLf &
                                   $"Total Amount: ₱{CDec(row.Cells("TotalAmount").Value):N2}" & vbCrLf &
                                   $"Status: {row.Cells("OrderStatus").Value}" & vbCrLf &
                                   $"Priority: {row.Cells("OrderPriority").Value}" & vbCrLf &
                                   $"Remarks: {row.Cells("Remarks").Value}"

            MessageBox.Show(details, "Order Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error viewing details: " & ex.Message)
        End Try
    End Sub

    ' FILTER BUTTONS
    Private Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        LoadOrders()
        lblFilter.Text = "Showing: All Orders"
    End Sub

    Private Sub btnViewPending_Click(sender As Object, e As EventArgs) Handles btnViewPending.Click
        LoadOrders("OrderStatus = 'Preparing'")
        lblFilter.Text = "Showing: Preparing Orders"
    End Sub


    Private Sub btnViewCompleted_Click(sender As Object, e As EventArgs) Handles btnViewCompleted.Click
        LoadOrders("OrderStatus = 'Completed'")
        lblFilter.Text = "Showing: Completed Orders"
    End Sub

    Private Sub btnViewCancelled_Click(sender As Object, e As EventArgs) Handles btnViewCancelled.Click
        LoadOrders("OrderStatus = 'Cancelled'")
        lblFilter.Text = "Showing: Cancelled Orders"
    End Sub

    ' SEARCH
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim search As String = txtSearch.Text.Trim()

        If search = "" Then
            LoadOrders()
            lblFilter.Text = "Showing: All Orders"
            Exit Sub
        End If

        LoadOrders($"OrderID LIKE '%{search}%'
                    OR CustomerID LIKE '%{search}%'
                    OR OrderStatus LIKE '%{search}%'
                    OR ReceiptNumber LIKE '%{search}%'")

        lblFilter.Text = "Search Results"
    End Sub

    ' REFRESH
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadOrders()
        txtSearch.Text = ""
        lblFilter.Text = "Showing: All Orders"
    End Sub

    ' ============================================================
    ' btnConfirm - Handle order confirmation/completion
    ' ============================================================
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Try
            ' Check if a row is selected
            If DataGridView2.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select an order to confirm.", "No Selection",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get the selected row
            Dim selectedRow As DataGridViewRow = DataGridView2.SelectedRows(0)
            Dim orderID As Integer = CInt(selectedRow.Cells("OrderID").Value)
            Dim currentStatus As String = selectedRow.Cells("OrderStatus").Value.ToString()
            Dim orderSource As String = If(selectedRow.Cells("OrderSource").Value IsNot Nothing,
                                          selectedRow.Cells("OrderSource").Value.ToString(), "")

            ' Show dialog to select new status
            Dim statusForm As New Form()
            statusForm.Text = "Update Order Status"
            statusForm.Size = New Size(450, 280)
            statusForm.StartPosition = FormStartPosition.CenterParent
            statusForm.FormBorderStyle = FormBorderStyle.FixedDialog
            statusForm.MaximizeBox = False
            statusForm.MinimizeBox = False

            ' Label
            Dim lblInfo As New Label()
            lblInfo.Text = $"Order ID: {orderID}" & vbCrLf &
                          $"Order Source: {orderSource}" & vbCrLf &
                          $"Current Status: {currentStatus}" & vbCrLf & vbCrLf &
                          "Select new status:"
            lblInfo.Location = New Point(20, 20)
            lblInfo.Size = New Size(400, 80)
            lblInfo.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(lblInfo)

            ' Radio buttons for status options
            Dim rbPreparing As New RadioButton()
            rbPreparing.Text = "Preparing"
            rbPreparing.Location = New Point(30, 110)
            rbPreparing.Size = New Size(100, 25)
            rbPreparing.Font = New Font("Segoe UI", 10)
            rbPreparing.Checked = (currentStatus = "Preparing")
            statusForm.Controls.Add(rbPreparing)

            Dim rbServed As New RadioButton()
            rbServed.Text = "Served"
            rbServed.Location = New Point(140, 110)
            rbServed.Size = New Size(100, 25)
            rbServed.Font = New Font("Segoe UI", 10)
            rbServed.Checked = (currentStatus = "Served")
            statusForm.Controls.Add(rbServed)

            Dim rbCompleted As New RadioButton()
            rbCompleted.Text = "Completed"
            rbCompleted.Location = New Point(250, 110)
            rbCompleted.Size = New Size(100, 25)
            rbCompleted.Font = New Font("Segoe UI", 10)
            rbCompleted.Checked = (currentStatus = "Completed")
            statusForm.Controls.Add(rbCompleted)

            Dim rbCancelled As New RadioButton()
            rbCancelled.Text = "Cancelled"
            rbCancelled.Location = New Point(30, 145)
            rbCancelled.Size = New Size(100, 25)
            rbCancelled.Font = New Font("Segoe UI", 10)
            rbCancelled.Checked = (currentStatus = "Cancelled")
            statusForm.Controls.Add(rbCancelled)

            ' Buttons
            Dim btnOK As New Button()
            btnOK.Text = "Update"
            btnOK.Location = New Point(250, 195)
            btnOK.Size = New Size(80, 35)
            btnOK.DialogResult = DialogResult.OK
            btnOK.Font = New Font("Segoe UI", 9)
            statusForm.Controls.Add(btnOK)

            Dim btnCancel As New Button()
            btnCancel.Text = "Cancel"
            btnCancel.Location = New Point(340, 195)
            btnCancel.Size = New Size(80, 35)
            btnCancel.DialogResult = DialogResult.Cancel
            btnCancel.Font = New Font("Segoe UI", 9)
            statusForm.Controls.Add(btnCancel)

            statusForm.AcceptButton = btnOK
            statusForm.CancelButton = btnCancel

            ' Show the dialog
            If statusForm.ShowDialog() = DialogResult.OK Then
                Dim newStatus As String = ""

                If rbPreparing.Checked Then
                    newStatus = "Preparing"
                ElseIf rbServed.Checked Then
                    newStatus = "Served"
                ElseIf rbCompleted.Checked Then
                    newStatus = "Completed"
                ElseIf rbCancelled.Checked Then
                    newStatus = "Cancelled"
                End If

                ' Check if status is actually changing
                If newStatus.ToLower() = currentStatus.ToLower() Then
                    MessageBox.Show($"Order status is already '{currentStatus}'.", "No Change",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' Update the order status
                UpdateOrderStatus(orderID, newStatus)
                LoadOrders()
            End If

        Catch ex As Exception
            MessageBox.Show("Error confirming order: " & ex.Message, "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ============================================================
    ' btnDelete - Delete selected order
    ' ============================================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            ' Check if a row is selected
            If DataGridView2.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select an order to delete.", "No Selection",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get the selected row
            Dim selectedRow As DataGridViewRow = DataGridView2.SelectedRows(0)
            Dim orderID As Integer = CInt(selectedRow.Cells("OrderID").Value)
            Dim receiptNumber As String = selectedRow.Cells("ReceiptNumber").Value.ToString()
            Dim totalAmount As Decimal = CDec(selectedRow.Cells("TotalAmount").Value)
            Dim orderStatus As String = selectedRow.Cells("OrderStatus").Value.ToString()

            ' Confirm deletion with detailed info
            Dim result As DialogResult = MessageBox.Show(
                $"Are you sure you want to DELETE this order?" & vbCrLf & vbCrLf &
                $"Order ID: {orderID}" & vbCrLf &
                $"Receipt Number: {receiptNumber}" & vbCrLf &
                $"Status: {orderStatus}" & vbCrLf &
                $"Total Amount: ₱{totalAmount:N2}" & vbCrLf & vbCrLf &
                "This action cannot be undone!",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                DeleteOrder(orderID)
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting order: " & ex.Message, "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class