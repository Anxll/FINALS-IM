Imports MySqlConnector
Imports System.Data
Imports System.Threading.Tasks

Public Class Orders

    ' Pagination variables
    Private CurrentPage As Integer = 1
    Private RecordsPerPage As Integer = 100
    Private TotalRecords As Integer = 0
    Private CurrentCondition As String = ""

    ' Performance optimization
    Private searchDebounceTimer As Timer
    Private lastSearchText As String = ""

    Private Sub Orders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeSearchDebounce()
        LoadOrdersAsync()
        lblFilter.Text = "Showing: All Orders"

        With DataGridView2
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ReadOnly = True
            .BorderStyle = BorderStyle.None
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
            .DefaultCellStyle.WrapMode = DataGridViewTriState.False
            .VirtualMode = False ' Keep false for simplicity, but consider VirtualMode for 100k+ records
        End With

        ' Enable double buffering to reduce flicker
        EnableDoubleBuffering(DataGridView2)

        SetupPaginationControls()
    End Sub

    ' ============================================================
    ' ENABLE DOUBLE BUFFERING (Reduces flicker)
    ' ============================================================
    Private Sub EnableDoubleBuffering(dgv As DataGridView)
        Try
            Dim dgvType As Type = dgv.[GetType]()
            Dim pi As Reflection.PropertyInfo = dgvType.GetProperty("DoubleBuffered",
                Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
            If pi IsNot Nothing Then
                pi.SetValue(dgv, True, Nothing)
            End If
        Catch ex As Exception
            ' Silently fail if reflection is not available
        End Try
    End Sub

    ' ============================================================
    ' SEARCH DEBOUNCE INITIALIZATION
    ' ============================================================
    Private Sub InitializeSearchDebounce()
        searchDebounceTimer = New Timer()
        searchDebounceTimer.Interval = 500 ' 500ms delay
        AddHandler searchDebounceTimer.Tick, AddressOf SearchDebounceTimer_Tick
    End Sub

    Private Sub SearchDebounceTimer_Tick(sender As Object, e As EventArgs)
        searchDebounceTimer.Stop()
        PerformSearch(lastSearchText)
    End Sub

    ' ============================================================
    ' SETUP PAGINATION CONTROLS
    ' ============================================================
    Private Sub SetupPaginationControls()
        If Me.Controls.Find("cboRecordsPerPage", True).Length > 0 Then
            Dim cbo As ComboBox = CType(Me.Controls.Find("cboRecordsPerPage", True)(0), ComboBox)
            cbo.Items.Clear()
            cbo.Items.AddRange(New Object() {50, 100, 200, 500, 1000})
            cbo.SelectedIndex = 1 ' Default to 100
        End If

        UpdatePaginationButtons()
    End Sub

    ' ============================================================
    ' ASYNC LOAD ORDERS (NON-BLOCKING UI)
    ' ============================================================
    Private Async Sub LoadOrdersAsync(Optional condition As String = "")
        Try
            ' Show loading indicator
            Cursor = Cursors.WaitCursor
            DataGridView2.Enabled = False

            Await Task.Run(Sub() LoadOrders(condition))

        Catch ex As Exception
            MessageBox.Show("Error loading orders: " & ex.Message, "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DataGridView2.Enabled = True
            Cursor = Cursors.Default
        End Try
    End Sub

    ' ============================================================
    ' LOAD ORDERS WITH OPTIMIZATIONS FOR LARGE DATASETS
    ' ============================================================
    Private Sub LoadOrders(Optional condition As String = "")
        Try
            CurrentCondition = condition

            ' Get total record count with optimized query
            Dim countQuery As String = "SELECT COUNT(DISTINCT o.OrderID) FROM orders o"
            If condition <> "" AndAlso Not condition.Contains("c.") Then
                ' If condition doesn't reference customers table, don't join it
                countQuery &= " WHERE " & condition
            ElseIf condition <> "" Then
                countQuery &= " LEFT JOIN customers c ON o.CustomerID = c.CustomerID WHERE " & condition
            End If

            TotalRecords = GetRecordCount(countQuery)

            ' Calculate offset for pagination
            Dim offset As Integer = (CurrentPage - 1) * RecordsPerPage

            ' HIGHLY OPTIMIZED QUERY for large datasets
            ' Uses covering index strategy and efficient JOINs
            Dim query As String =
            "SELECT 
                o.OrderID,
                o.CustomerID,
                COALESCE(c.FirstName, '') AS FirstName,
                COALESCE(c.LastName, '') AS LastName,
                COALESCE(c.Email, '') AS Email,
                COALESCE(c.ContactNumber, '') AS CustomerContact,
                o.EmployeeID,
                o.OrderType,
                o.OrderSource,
                o.WebsiteStatus,
                o.ReceiptNumber,
                o.NumberOfDiners,
                o.OrderDate,
                o.OrderTime,
                o.ItemsOrderedCount,
                o.TotalAmount,
                o.OrderStatus,
                o.Remarks,
                o.SpecialRequestFlag,
                o.CreatedDate,
                o.UpdatedDate,
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
             LEFT JOIN customers c ON o.CustomerID = c.CustomerID"

            If condition <> "" Then
                query &= " WHERE " & condition
            End If

            ' Optimized ordering - use indexed columns
            query &= " ORDER BY o.OrderID DESC"
            query &= $" LIMIT {RecordsPerPage} OFFSET {offset}"

            ' Load data with optimized method
            LoadToDGVOptimized(query, DataGridView2)

            ' Invoke UI updates on UI thread
            If DataGridView2.InvokeRequired Then
                DataGridView2.Invoke(Sub()
                                         FormatDataGridView()
                                         FormatCustomerData()
                                         UpdatePaginationInfo()
                                     End Sub)
            Else
                FormatDataGridView()
                FormatCustomerData()
                UpdatePaginationInfo()
            End If

        Catch ex As Exception
            If DataGridView2.InvokeRequired Then
                DataGridView2.Invoke(Sub() MessageBox.Show("Error loading orders: " & ex.Message))
            Else
                MessageBox.Show("Error loading orders: " & ex.Message)
            End If
        End Try
    End Sub

    ' ============================================================
    ' OPTIMIZED DATA LOADING METHOD
    ' ============================================================
    Private Sub LoadToDGVOptimized(query As String, dgv As DataGridView)
        Try
            Using conn As New MySqlConnection("Server=127.0.0.1;User=root;Password=;Database=tabeya_system")
                conn.Open()

                Using cmd As New MySqlCommand(query, conn)
                    ' Optimize command for large datasets
                    cmd.CommandTimeout = 60 ' 60 seconds timeout

                    Using da As New MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()

                        ' Optimize DataTable loading
                        dt.BeginLoadData()
                        da.Fill(dt)
                        dt.EndLoadData()

                        ' Update UI on UI thread
                        If dgv.InvokeRequired Then
                            dgv.Invoke(Sub()
                                           dgv.DataSource = dt

                                           ' Ensure newest orders appear at the top
                                           If dgv.Columns.Contains("OrderID") Then
                                               Try
                                                   dgv.Columns("OrderID").SortMode = DataGridViewColumnSortMode.Automatic
                                                   dgv.Sort(dgv.Columns("OrderID"), ComponentModel.ListSortDirection.Descending)
                                               Catch
                                                   ' Best-effort: SQL ordering still applies if bound sorting isn't supported.
                                               End Try
                                           End If
                                           If dgv.Rows.Count > 0 Then
                                               dgv.FirstDisplayedScrollingRowIndex = 0
                                           End If
                                       End Sub)
                        Else
                            dgv.DataSource = dt

                            ' Ensure newest orders appear at the top
                            If dgv.Columns.Contains("OrderID") Then
                                Try
                                    dgv.Columns("OrderID").SortMode = DataGridViewColumnSortMode.Automatic
                                    dgv.Sort(dgv.Columns("OrderID"), ComponentModel.ListSortDirection.Descending)
                                Catch
                                End Try
                            End If
                            If dgv.Rows.Count > 0 Then
                                dgv.FirstDisplayedScrollingRowIndex = 0
                            End If
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Database Error: " & ex.Message, ex)
        End Try
    End Sub

    ' ============================================================
    ' FORMAT DATAGRIDVIEW (Separated for better performance)
    ' ============================================================
    Private Sub FormatDataGridView()
        Try
            With DataGridView2
                .SuspendLayout() ' Suspend layout during bulk changes

                ' Hide ID columns
                If .Columns.Contains("OrderID") Then .Columns("OrderID").Visible = False
                If .Columns.Contains("CustomerID") Then .Columns("CustomerID").Visible = False
                If .Columns.Contains("EmployeeID") Then .Columns("EmployeeID").Visible = False
                If .Columns.Contains("CreatedDate") Then .Columns("CreatedDate").Visible = False
                If .Columns.Contains("UpdatedDate") Then .Columns("UpdatedDate").Visible = False

                ' Customer Information Columns
                If .Columns.Contains("FirstName") Then
                    .Columns("FirstName").HeaderText = "First Name"
                    .Columns("FirstName").Width = 120
                    .Columns("FirstName").DisplayIndex = 0
                End If

                If .Columns.Contains("LastName") Then
                    .Columns("LastName").HeaderText = "Last Name"
                    .Columns("LastName").Width = 120
                    .Columns("LastName").DisplayIndex = 1
                End If

                If .Columns.Contains("Email") Then
                    .Columns("Email").HeaderText = "Email"
                    .Columns("Email").Width = 180
                    .Columns("Email").DisplayIndex = 2
                End If

                If .Columns.Contains("CustomerContact") Then
                    .Columns("CustomerContact").HeaderText = "Contact Number"
                    .Columns("CustomerContact").Width = 120
                    .Columns("CustomerContact").DisplayIndex = 3
                End If

                ' Order Information Columns
                If .Columns.Contains("ReceiptNumber") Then
                    .Columns("ReceiptNumber").HeaderText = "Receipt Number"
                    .Columns("ReceiptNumber").Width = 120
                    .Columns("ReceiptNumber").DisplayIndex = 4
                End If

                If .Columns.Contains("OrderType") Then
                    .Columns("OrderType").HeaderText = "Order Type"
                    .Columns("OrderType").Width = 100
                    .Columns("OrderType").DisplayIndex = 5
                End If

                If .Columns.Contains("OrderSource") Then
                    .Columns("OrderSource").HeaderText = "Order Source"
                    .Columns("OrderSource").Width = 100
                    .Columns("OrderSource").DisplayIndex = 6
                End If

                If .Columns.Contains("WebsiteStatus") Then
                    .Columns("WebsiteStatus").HeaderText = "Website Status"
                    .Columns("WebsiteStatus").Width = 120
                    .Columns("WebsiteStatus").DisplayIndex = 7
                End If

                If .Columns.Contains("NumberOfDiners") Then
                    .Columns("NumberOfDiners").HeaderText = "Diners"
                    .Columns("NumberOfDiners").Width = 70
                    .Columns("NumberOfDiners").DisplayIndex = 8
                End If

                If .Columns.Contains("OrderDate") Then
                    .Columns("OrderDate").HeaderText = "Order Date"
                    .Columns("OrderDate").Width = 100
                    .Columns("OrderDate").DefaultCellStyle.Format = "MM/dd/yyyy"
                    .Columns("OrderDate").DisplayIndex = 9
                End If

                If .Columns.Contains("OrderTime") Then
                    .Columns("OrderTime").HeaderText = "Order Time"
                    .Columns("OrderTime").Width = 90
                    .Columns("OrderTime").DisplayIndex = 10
                End If

                If .Columns.Contains("ItemsOrderedCount") Then
                    .Columns("ItemsOrderedCount").HeaderText = "Items"
                    .Columns("ItemsOrderedCount").Width = 70
                    .Columns("ItemsOrderedCount").DisplayIndex = 11
                End If

                If .Columns.Contains("OrderedProducts") Then
                    .Columns("OrderedProducts").HeaderText = "Ordered Products"
                    .Columns("OrderedProducts").Width = 250
                    .Columns("OrderedProducts").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("OrderedProducts").DisplayIndex = 12
                End If

                If .Columns.Contains("TotalAmount") Then
                    .Columns("TotalAmount").HeaderText = "Total Amount"
                    .Columns("TotalAmount").Width = 120
                    .Columns("TotalAmount").DefaultCellStyle.Format = "₱#,##0.00"
                    .Columns("TotalAmount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("TotalAmount").DisplayIndex = 13
                End If

                If .Columns.Contains("OrderStatus") Then
                    .Columns("OrderStatus").HeaderText = "Status"
                    .Columns("OrderStatus").Width = 100
                    .Columns("OrderStatus").DisplayIndex = 14
                End If

                If .Columns.Contains("SpecialRequestFlag") Then
                    .Columns("SpecialRequestFlag").HeaderText = "Special Request"
                    .Columns("SpecialRequestFlag").Width = 110
                    .Columns("SpecialRequestFlag").DisplayIndex = 15
                End If

                If .Columns.Contains("Remarks") Then
                    .Columns("Remarks").HeaderText = "Remarks"
                    .Columns("Remarks").Width = 150
                    .Columns("Remarks").DisplayIndex = 16
                End If

                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                .ScrollBars = ScrollBars.Both
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
                .RowHeadersVisible = False

                .RowTemplate.Height = 35
                .ColumnHeadersHeight = 40
                .AllowUserToResizeColumns = True
                .AllowUserToResizeRows = False

                .EnableHeadersVisualStyles = False
                .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94)
                .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
                .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                .ResumeLayout() ' Resume layout after changes
            End With
        Catch ex As Exception
            ' Handle silently
        End Try
    End Sub

    ' ============================================================
    ' GET RECORD COUNT (Optimized)
    ' ============================================================
    Private Function GetRecordCount(query As String) As Integer
        Try
            Using conn As New MySqlConnection("Server=127.0.0.1;User=root;Password=;Database=tabeya_system")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.CommandTimeout = 30
                    Dim result = cmd.ExecuteScalar()
                    Return If(result IsNot Nothing AndAlso Not IsDBNull(result), CInt(result), 0)
                End Using
            End Using
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ' ============================================================
    ' UPDATE PAGINATION INFO
    ' ============================================================
    Private Sub UpdatePaginationInfo()
        Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)
        Dim startRecord As Integer = If(TotalRecords > 0, (CurrentPage - 1) * RecordsPerPage + 1, 0)
        Dim endRecord As Integer = Math.Min(CurrentPage * RecordsPerPage, TotalRecords)

        lblTotalOrders.Text = $"Showing {startRecord:N0} to {endRecord:N0} of {TotalRecords:N0} orders (Page {CurrentPage} of {totalPages})"

        If Me.Controls.Find("lblPageInfo", True).Length > 0 Then
            Dim lblPage As Label = CType(Me.Controls.Find("lblPageInfo", True)(0), Label)
            lblPage.Text = $"Page {CurrentPage} / {totalPages}"
        End If

        UpdatePaginationButtons()
    End Sub

    ' ============================================================
    ' UPDATE PAGINATION BUTTONS
    ' ============================================================
    Private Sub UpdatePaginationButtons()
        Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)

        If Me.Controls.Find("btnFirstPage", True).Length > 0 Then
            Dim btn As Button = CType(Me.Controls.Find("btnFirstPage", True)(0), Button)
            btn.Enabled = CurrentPage > 1
        End If

        If Me.Controls.Find("btnPrevPage", True).Length > 0 Then
            Dim btn As Button = CType(Me.Controls.Find("btnPrevPage", True)(0), Button)
            btn.Enabled = CurrentPage > 1
        End If

        If Me.Controls.Find("btnNextPage", True).Length > 0 Then
            Dim btn As Button = CType(Me.Controls.Find("btnNextPage", True)(0), Button)
            btn.Enabled = CurrentPage < totalPages
        End If

        If Me.Controls.Find("btnLastPage", True).Length > 0 Then
            Dim btn As Button = CType(Me.Controls.Find("btnLastPage", True)(0), Button)
            btn.Enabled = CurrentPage < totalPages
        End If
    End Sub

    ' ============================================================
    ' FORMAT CUSTOMER DATA
    ' ============================================================
    Private Sub FormatCustomerData()
        Try
            DataGridView2.SuspendLayout()

            For Each row As DataGridViewRow In DataGridView2.Rows
                If row.IsNewRow Then Continue For

                Dim firstName As String = If(row.Cells("FirstName").Value?.ToString(), "")
                Dim lastName As String = If(row.Cells("LastName").Value?.ToString(), "")

                If String.IsNullOrEmpty(firstName) AndAlso String.IsNullOrEmpty(lastName) Then
                    row.Cells("FirstName").Value = "Walk-in"
                    row.Cells("LastName").Value = "Customer"
                    row.Cells("Email").Value = "N/A"
                    row.Cells("CustomerContact").Value = "N/A"

                    row.Cells("FirstName").Style.ForeColor = Color.Gray
                    row.Cells("LastName").Style.ForeColor = Color.Gray
                    row.Cells("Email").Style.ForeColor = Color.Gray
                    row.Cells("CustomerContact").Style.ForeColor = Color.Gray
                End If

                If row.Cells("OrderedProducts").Value Is Nothing OrElse
                   String.IsNullOrEmpty(row.Cells("OrderedProducts").Value.ToString()) Then
                    row.Cells("OrderedProducts").Value = "No items"
                    row.Cells("OrderedProducts").Style.ForeColor = Color.Gray
                End If
            Next

            DataGridView2.ResumeLayout()
        Catch ex As Exception
            ' Silently handle formatting errors
        End Try
    End Sub

    Private Sub LoadToDGV(query As String, dgv As DataGridView)
        LoadToDGVOptimized(query, dgv)
    End Sub

    ' ============================================================
    ' GET CUSTOMER NAME
    ' ============================================================
    Private Function GetCustomerName(row As DataGridViewRow) As String
        Try
            Dim firstName As String = If(row.Cells("FirstName").Value?.ToString(), "")
            Dim lastName As String = If(row.Cells("LastName").Value?.ToString(), "")

            If firstName = "Walk-in" AndAlso lastName = "Customer" Then
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

    ' ============================================================
    ' UPDATE ORDER STATUS
    ' ============================================================
    Private Sub UpdateOrderStatus(orderID As Integer, newStatus As String, Optional websiteStatus As String = Nothing)
        Try
            Using conn As New MySqlConnection("Server=127.0.0.1;User=root;Password=;Database=tabeya_system")
                conn.Open()
                Dim query As String
                If websiteStatus Is Nothing Then
                    query = "UPDATE orders SET OrderStatus = @status, UpdatedDate = NOW() WHERE OrderID = @orderID"
                Else
                    query = "UPDATE orders SET OrderStatus = @status, WebsiteStatus = @websiteStatus, UpdatedDate = NOW() WHERE OrderID = @orderID"
                End If
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@status", newStatus)
                    If websiteStatus IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@websiteStatus", websiteStatus)
                    End If
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

                ' Use transaction for data integrity
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim deleteItemsQuery As String = "DELETE FROM order_items WHERE OrderID = @orderID"
                        Using cmd As New MySqlCommand(deleteItemsQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@orderID", orderID)
                            cmd.ExecuteNonQuery()
                        End Using

                        Dim query As String = "DELETE FROM orders WHERE OrderID = @orderID"
                        Using cmd As New MySqlCommand(query, conn, transaction)
                            cmd.Parameters.AddWithValue("@orderID", orderID)
                            cmd.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            MessageBox.Show("Order deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadOrdersAsync(CurrentCondition)
        Catch ex As Exception
            MessageBox.Show("Delete Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' CONTEXT MENU
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

            Dim contextMenu As New ContextMenuStrip()
            contextMenu.Font = New Font("Segoe UI", 9)

            If status = "Preparing" Then
                If orderSource.ToLower() = "website" Then
                    Dim completeItem As New ToolStripMenuItem("Complete Order")
                    AddHandler completeItem.Click, Sub() CompleteOrder(orderID)
                    contextMenu.Items.Add(completeItem)
                Else
                    Dim serveItem As New ToolStripMenuItem("Serve Order")
                    AddHandler serveItem.Click, Sub() ServeOrder(orderID)
                    contextMenu.Items.Add(serveItem)
                End If

                Dim cancelItem As New ToolStripMenuItem("Cancel Order")
                AddHandler cancelItem.Click, Sub() CancelOrder(orderID)
                contextMenu.Items.Add(cancelItem)
                contextMenu.Items.Add(New ToolStripSeparator())
            ElseIf status = "Served" Then
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

            Dim mousePos As Point = DataGridView2.PointToClient(Cursor.Position)
            contextMenu.Show(DataGridView2, mousePos)
        End If
    End Sub

    Private Sub CompleteOrder(orderID As Integer)
        If MessageBox.Show($"Mark Order #{orderID} as Completed?",
                          "Complete Order", MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question) = DialogResult.Yes Then
            UpdateOrderStatus(orderID, "Completed")
            LoadOrdersAsync(CurrentCondition)
        End If
    End Sub

    Private Sub ServeOrder(orderID As Integer)
        If MessageBox.Show($"Mark Order #{orderID} as Served?",
                          "Serve Order", MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question) = DialogResult.Yes Then
            UpdateOrderStatus(orderID, "Served")
            LoadOrdersAsync(CurrentCondition)
        End If
    End Sub

    Private Sub ConfirmOrder(orderID As Integer)
        If MessageBox.Show($"Confirm Order #{orderID}?",
                          "Confirm Order", MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question) = DialogResult.Yes Then
            UpdateOrderStatus(orderID, "Confirmed")
            LoadOrdersAsync(CurrentCondition)
        End If
    End Sub

    Private Sub CancelOrder(orderID As Integer)
        If MessageBox.Show($"Cancel Order #{orderID}?",
                          "Cancel Order", MessageBoxButtons.YesNo,
                          MessageBoxIcon.Warning) = DialogResult.Yes Then
            UpdateOrderStatus(orderID, "Cancelled")
            LoadOrdersAsync(CurrentCondition)
        End If
    End Sub

    Private Sub ViewOrderDetails(orderID As Integer)
        Try
            Dim row As DataGridViewRow = DataGridView2.SelectedRows(0)
            Dim customerName As String = GetCustomerName(row)
            Dim email As String = If(row.Cells("Email").Value?.ToString(), "N/A")
            Dim contact As String = If(row.Cells("CustomerContact").Value?.ToString(), "N/A")
            Dim orderedProducts As String = If(row.Cells("OrderedProducts").Value?.ToString(), "No items")

            Dim details As String = $"Order Details:" & vbCrLf & vbCrLf &
                                   $"Order ID: {orderID}" & vbCrLf &
                                   $"Customer: {customerName}" & vbCrLf

            If customerName <> "Walk-in Customer" Then
                details &= $"Email: {email}" & vbCrLf &
                          $"Contact: {contact}" & vbCrLf
            End If

            details &= $"Receipt Number: {row.Cells("ReceiptNumber").Value}" & vbCrLf &
                      $"Order Type: {row.Cells("OrderType").Value}" & vbCrLf &
                      $"Order Source: {row.Cells("OrderSource").Value}" & vbCrLf &
                      $"Number of Diners: {row.Cells("NumberOfDiners").Value}" & vbCrLf &
                      $"Order Date: {row.Cells("OrderDate").Value}" & vbCrLf &
                      $"Order Time: {row.Cells("OrderTime").Value}" & vbCrLf &
                      $"Items Ordered: {row.Cells("ItemsOrderedCount").Value}" & vbCrLf &
                      $"Ordered Products: {orderedProducts}" & vbCrLf &
                      $"Total Amount: ₱{CDec(row.Cells("TotalAmount").Value):N2}" & vbCrLf &
                      $"Status: {row.Cells("OrderStatus").Value}" & vbCrLf &
                      $"Remarks: {row.Cells("Remarks").Value}"

            MessageBox.Show(details, "Order Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error viewing details: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' FILTER BUTTONS
    ' ============================================================
    Private Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        CurrentPage = 1
        LoadOrdersAsync()
        lblFilter.Text = "Showing: All Orders"
    End Sub

    Private Sub btnViewPending_Click(sender As Object, e As EventArgs) Handles btnViewPending.Click
        CurrentPage = 1
        LoadOrdersAsync("o.OrderStatus = 'Preparing'")
        lblFilter.Text = "Showing: Preparing Orders"
    End Sub

    Private Sub btnViewCompleted_Click(sender As Object, e As EventArgs) Handles btnViewCompleted.Click
        LoadOrders("o.OrderStatus = 'Completed'")
        lblFilter.Text = "Showing: Completed Orders"
    End Sub

    Private Sub btnViewCancelled_Click(sender As Object, e As EventArgs) Handles btnViewCancelled.Click
        CurrentPage = 1
        LoadOrdersAsync("o.OrderStatus = 'Cancelled'")
        lblFilter.Text = "Showing: Cancelled Orders"
    End Sub

    ' ============================================================
    ' SEARCH WITH DEBOUNCE (Optimized for performance)
    ' ============================================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        lastSearchText = txtSearch.Text.Trim()

        ' Reset and restart timer
        searchDebounceTimer.Stop()
        searchDebounceTimer.Start()
    End Sub

    Private Sub PerformSearch(search As String)
        Try
            CurrentPage = 1

            If search = "" Then
                LoadOrdersAsync()
                lblFilter.Text = "Showing: All Orders"
                Exit Sub
            End If

            ' Use parameterized-like approach for better performance
            ' Escape special characters to prevent SQL injection
            search = search.Replace("'", "''")

            ' Optimized search query - using indexed columns first
            Dim condition As String = $"(o.OrderID LIKE '%{search}%'
                        OR o.ReceiptNumber LIKE '%{search}%'
                        OR o.OrderStatus LIKE '%{search}%'
                        OR c.FirstName LIKE '%{search}%'
                        OR c.LastName LIKE '%{search}%'
                        OR c.Email LIKE '%{search}%'
                        OR o.CustomerID LIKE '%{search}%')"

            LoadOrdersAsync(condition)
            lblFilter.Text = "Search Results"
        Catch ex As Exception
            MessageBox.Show("Search Error: " & ex.Message)
        End Try
    End Sub

    ' ============================================================
    ' REFRESH
    ' ============================================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        CurrentPage = 1
        txtSearch.Text = ""
        lastSearchText = ""
        LoadOrdersAsync(CurrentCondition)
    End Sub

    ' ============================================================
    ' btnConfirm - Handle order confirmation/completion
    ' ============================================================
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Try
            If DataGridView2.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select an order to confirm.", "No Selection",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedRow As DataGridViewRow = DataGridView2.SelectedRows(0)
            Dim orderID As Integer = CInt(selectedRow.Cells("OrderID").Value)
            Dim currentStatus As String = selectedRow.Cells("OrderStatus").Value.ToString()
            Dim orderSource As String = If(selectedRow.Cells("OrderSource").Value IsNot Nothing,
                                      selectedRow.Cells("OrderSource").Value.ToString(), "")
            Dim currentWebsiteStatus As String = ""
            If selectedRow.Cells("WebsiteStatus") IsNot Nothing AndAlso
           selectedRow.Cells("WebsiteStatus").Value IsNot Nothing Then
                currentWebsiteStatus = selectedRow.Cells("WebsiteStatus").Value.ToString()
            End If
            Dim customerName As String = GetCustomerName(selectedRow)

            ' Create status update form
            Dim statusForm As New Form()
            statusForm.Text = "Update Order Status"
            Dim isWebsiteOrder As Boolean = orderSource.Trim().ToLower() = "website"
            statusForm.Size = If(isWebsiteOrder, New Size(450, 380), New Size(450, 310))
            statusForm.StartPosition = FormStartPosition.CenterParent
            statusForm.FormBorderStyle = FormBorderStyle.FixedDialog
            statusForm.MaximizeBox = False
            statusForm.MinimizeBox = False

            ' Status label and ComboBox
            Dim lblStatus As New Label()
            lblStatus.Text = "New Status:"
            lblStatus.Location = New Point(20, 20)
            lblStatus.Size = New Size(100, 23)
            lblStatus.Font = New Font("Segoe UI", 9)
            statusForm.Controls.Add(lblStatus)

            Dim cboStatus As New ComboBox()
            cboStatus.Location = New Point(20, 45)
            cboStatus.Size = New Size(390, 23)
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList
            cboStatus.Font = New Font("Segoe UI", 9)

            ' Add appropriate status options based on current status and order source
            Select Case currentStatus
                Case "Preparing"
                    If orderSource.ToLower() = "website" Then
                        cboStatus.Items.AddRange({"Preparing", "Completed", "Cancelled"})
                    Else
                        cboStatus.Items.AddRange({"Preparing", "Served", "Cancelled"})
                    End If
                Case "Served"
                    cboStatus.Items.AddRange({"Preparing", "Served", "Completed"})
                Case Else
                    cboStatus.Items.AddRange({"Preparing", "Served", "Completed", "Cancelled"})
            End Select

            ' Set "Preparing" as default
            If cboStatus.Items.Contains("Preparing") Then
                cboStatus.SelectedItem = "Preparing"
            ElseIf cboStatus.Items.Count > 0 Then
                cboStatus.SelectedIndex = 0
            End If

            statusForm.Controls.Add(cboStatus)

            ' Order info label
            Dim lblInfo As New Label()
            lblInfo.Text = $"Order ID: {orderID}" & vbCrLf &
                      $"Customer: {customerName}" & vbCrLf &
                      $"Current Status: {currentStatus}"
            lblInfo.Location = New Point(20, 80)
            lblInfo.Size = New Size(390, 70)
            lblInfo.Font = New Font("Segoe UI", 9)
            lblInfo.BorderStyle = BorderStyle.FixedSingle
            lblInfo.Padding = New Padding(5)
            lblInfo.BackColor = Color.FromArgb(245, 245, 245)
            statusForm.Controls.Add(lblInfo)

            ' Website status (only for website orders)
            Dim cboWebsiteStatus As ComboBox = Nothing
            If isWebsiteOrder Then
                Dim lblWebsiteStatus As New Label()
                lblWebsiteStatus.Text = "Website Status:"
                lblWebsiteStatus.Location = New Point(20, 160)
                lblWebsiteStatus.Size = New Size(120, 23)
                lblWebsiteStatus.Font = New Font("Segoe UI", 9)
                statusForm.Controls.Add(lblWebsiteStatus)

                cboWebsiteStatus = New ComboBox()
                cboWebsiteStatus.Location = New Point(20, 185)
                cboWebsiteStatus.Size = New Size(390, 23)
                cboWebsiteStatus.DropDownStyle = ComboBoxStyle.DropDownList
                cboWebsiteStatus.Font = New Font("Segoe UI", 9)
                cboWebsiteStatus.Items.AddRange(New Object() {"Pending", "Confirmed", "Cancelled"})

                If Not String.IsNullOrEmpty(currentWebsiteStatus) AndAlso
               cboWebsiteStatus.Items.Contains(currentWebsiteStatus) Then
                    cboWebsiteStatus.SelectedItem = currentWebsiteStatus
                Else
                    cboWebsiteStatus.SelectedIndex = 0
                End If

                statusForm.Controls.Add(cboWebsiteStatus)
            End If

            ' Buttons
            Dim btnUpdate As New Button()
            btnUpdate.Text = "Update Status"
            Dim buttonY As Integer = If(isWebsiteOrder, 230, 170)
            btnUpdate.Location = New Point(150, buttonY)
            btnUpdate.Size = New Size(120, 35)
            btnUpdate.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            btnUpdate.BackColor = Color.FromArgb(52, 152, 219)
            btnUpdate.ForeColor = Color.White
            btnUpdate.FlatStyle = FlatStyle.Flat
            btnUpdate.Cursor = Cursors.Hand

            AddHandler btnUpdate.Click, Sub()
                                            Dim newStatus As String = cboStatus.SelectedItem.ToString()
                                            Dim newWebsiteStatus As String = Nothing
                                            If isWebsiteOrder AndAlso cboWebsiteStatus IsNot Nothing AndAlso
                                           cboWebsiteStatus.SelectedItem IsNot Nothing Then
                                                newWebsiteStatus = cboWebsiteStatus.SelectedItem.ToString()
                                            End If

                                            UpdateOrderStatus(orderID, newStatus, newWebsiteStatus)
                                            statusForm.Close()
                                            LoadOrdersAsync(CurrentCondition)
                                        End Sub
            statusForm.Controls.Add(btnUpdate)

            Dim btnCancel As New Button()
            btnCancel.Text = "Cancel"
            btnCancel.Location = New Point(280, buttonY)
            btnCancel.Size = New Size(100, 35)
            btnCancel.Font = New Font("Segoe UI", 9)
            btnCancel.DialogResult = DialogResult.Cancel
            btnCancel.Cursor = Cursors.Hand
            statusForm.Controls.Add(btnCancel)

            statusForm.AcceptButton = btnUpdate
            statusForm.CancelButton = btnCancel
            statusForm.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ============================================================
    ' CLEANUP
    ' ============================================================
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If searchDebounceTimer IsNot Nothing Then
            searchDebounceTimer.Stop()
            searchDebounceTimer.Dispose()
        End If
        MyBase.OnFormClosing(e)
    End Sub

    ' ============================================================
    ' PAGINATION BUTTON EVENTS
    ' ============================================================
    Private Sub btnFirstPage_Click(sender As Object, e As EventArgs) Handles btnFirstPage.Click
        CurrentPage = 1
        LoadOrdersAsync(CurrentCondition)
    End Sub

    Private Sub btnPrevPage_Click(sender As Object, e As EventArgs) Handles btnPrevPage.Click
        If CurrentPage > 1 Then
            CurrentPage -= 1
            LoadOrdersAsync(CurrentCondition)
        End If
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)
        If CurrentPage < totalPages Then
            CurrentPage += 1
            LoadOrdersAsync(CurrentCondition)
        End If
    End Sub

    Private Sub btnLastPage_Click(sender As Object, e As EventArgs) Handles btnLastPage.Click
        Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)
        CurrentPage = totalPages
        LoadOrdersAsync(CurrentCondition)
    End Sub

    Private Sub cboRecordsPerPage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRecordsPerPage.SelectedIndexChanged
        If cboRecordsPerPage.SelectedItem IsNot Nothing Then
            RecordsPerPage = CInt(cboRecordsPerPage.SelectedItem)
            CurrentPage = 1 ' Reset to first page when changing page size
            LoadOrdersAsync(CurrentCondition)
        End If
    End Sub

End Class
