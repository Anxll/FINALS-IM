Imports MySqlConnector
Imports System.Data
Imports System.Threading.Tasks
Imports System.IO
Imports System.Net
Public Class Orders

    ' Pagination variables
    Private CurrentPage As Integer = 1
    Private RecordsPerPage As Integer = 20
    Private TotalRecords As Integer = 0
    Private CurrentCondition As String = ""

    ' Performance optimization
    Private searchDebounceTimer As Timer
    Private lastSearchText As String = ""
    Private Const WEB_BASE_URL As String = "http://localhost/TrialWeb/TrialWorkIM/Tabeya/"
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
            cbo.Items.AddRange(New Object() {20, 50, 100, 200, 500})  ' Added 20 as first option
            cbo.SelectedIndex = 0 ' Changed from 1 to 0 to select 20 as default
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
                    o.DeliveryOption,
                    o.ReceiptNumber,
                    o.NumberOfDiners,
                    o.OrderDate,
                    o.OrderTime,
                    o.ItemsOrderedCount,
                    o.TotalAmount,
                    o.OrderStatus,
                    COALESCE(o.SpecialRequests, '') AS SpecialRequests,
                    COALESCE(o.DeliveryAddress, '') AS DeliveryAddress,
                    o.CreatedDate,
                    o.UpdatedDate,
                    COALESCE(p.PaymentMethod, '') AS PaymentMethod,
                    COALESCE(p.ProofOfPayment, '') AS ProofOfPayment,
                    COALESCE(p.ReceiptFileName, '') AS ReceiptFileName,
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
                 LEFT JOIN customers c ON o.CustomerID = c.CustomerID
                 LEFT JOIN payments p ON o.OrderID = p.OrderID"

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
                    cmd.CommandTimeout = 120 ' 60 seconds timeout

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

                If .Columns.Contains("DeliveryOption") Then
                    .Columns("DeliveryOption").HeaderText = "Delivery Option"
                    .Columns("DeliveryOption").Width = 120
                    .Columns("DeliveryOption").DisplayIndex = 7
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

                If .Columns.Contains("SpecialRequests") Then
                    .Columns("SpecialRequests").HeaderText = "Special Requests"
                    .Columns("SpecialRequests").Width = 200
                    .Columns("SpecialRequests").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("SpecialRequests").DisplayIndex = 15
                End If

                If .Columns.Contains("DeliveryAddress") Then
                    .Columns("DeliveryAddress").HeaderText = "Delivery Address"
                    .Columns("DeliveryAddress").Width = 200
                    .Columns("DeliveryAddress").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("DeliveryAddress").DisplayIndex = 16
                End If
                If .Columns.Contains("PaymentMethod") Then
                    .Columns("PaymentMethod").HeaderText = "Payment Method"
                    .Columns("PaymentMethod").Width = 120
                    .Columns("PaymentMethod").DisplayIndex = 15
                End If

                ' Hide ProofOfPayment and ReceiptFileName columns - they are for internal use
                If .Columns.Contains("ProofOfPayment") Then .Columns("ProofOfPayment").Visible = False
                If .Columns.Contains("ReceiptFileName") Then .Columns("ReceiptFileName").Visible = False

                ' Update the SpecialRequests DisplayIndex to 16
                If .Columns.Contains("SpecialRequests") Then
                    .Columns("SpecialRequests").HeaderText = "Special Requests"
                    .Columns("SpecialRequests").Width = 200
                    .Columns("SpecialRequests").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("SpecialRequests").DisplayIndex = 16
                End If

                ' Update the DeliveryAddress DisplayIndex to 17
                If .Columns.Contains("DeliveryAddress") Then
                    .Columns("DeliveryAddress").HeaderText = "Delivery Address"
                    .Columns("DeliveryAddress").Width = 200
                    .Columns("DeliveryAddress").DefaultCellStyle.WrapMode = DataGridViewTriState.True
                    .Columns("DeliveryAddress").DisplayIndex = 17
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
                AddViewProofButtonColumn()
                .ResumeLayout() ' Resume layout after changes
            End With
        Catch ex As Exception
            ' Handle silently
        End Try
    End Sub
    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        ' Check if the clicked cell is the View Proof button
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            If DataGridView2.Columns(e.ColumnIndex).Name = "ViewProof" Then
                Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Dim orderSource As String = If(row.Cells("OrderSource").Value?.ToString(), "")
                Dim proofPath As String = If(row.Cells("ProofOfPayment").Value?.ToString(), "")
                Dim receiptFileName As String = If(row.Cells("ReceiptFileName").Value?.ToString(), "")

                ' Only allow viewing for Website orders with proof
                If orderSource.ToLower() <> "website" Then
                    MessageBox.Show("Proof of payment is only available for Website orders.", "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                If String.IsNullOrEmpty(proofPath) Then
                    MessageBox.Show("No proof of payment available for this order.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' Show the image in fullscreen
                ShowProofOfPayment(proofPath, receiptFileName)
            End If
        End If
    End Sub
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
    Private Sub AddViewProofButtonColumn()
        Try
            ' Remove existing button column if it exists
            If DataGridView2.Columns.Contains("ViewProof") Then
                DataGridView2.Columns.Remove("ViewProof")
            End If

            ' Create button column
            Dim btnCol As New DataGridViewButtonColumn()
            btnCol.Name = "ViewProof"
            btnCol.HeaderText = "Proof of Payment"
            btnCol.Text = "View"
            btnCol.UseColumnTextForButtonValue = False ' Important: We'll set text per cell
            btnCol.Width = 120
            btnCol.DefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255)
            btnCol.DefaultCellStyle.ForeColor = Color.White
            btnCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 105, 217)
            btnCol.DefaultCellStyle.SelectionForeColor = Color.White
            btnCol.FlatStyle = FlatStyle.Flat

            ' Add column at the end
            DataGridView2.Columns.Add(btnCol)
            btnCol.DisplayIndex = DataGridView2.Columns.Count - 1

            ' Set button text based on order source and proof availability
            For Each row As DataGridViewRow In DataGridView2.Rows
                If row.IsNewRow Then Continue For

                Dim orderSource As String = If(row.Cells("OrderSource").Value?.ToString(), "")
                Dim proofPath As String = If(row.Cells("ProofOfPayment").Value?.ToString(), "")

                ' Only show button for Website orders with proof
                If orderSource.ToLower() = "website" AndAlso Not String.IsNullOrEmpty(proofPath) Then
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

                ' Handle Walk-in customers
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

                ' Handle empty email
                If String.IsNullOrWhiteSpace(row.Cells("Email").Value?.ToString()) Then
                    row.Cells("Email").Value = "N/A"
                    row.Cells("Email").Style.ForeColor = Color.Gray
                End If

                ' Handle empty contact
                If String.IsNullOrWhiteSpace(row.Cells("CustomerContact").Value?.ToString()) Then
                    row.Cells("CustomerContact").Value = "N/A"
                    row.Cells("CustomerContact").Style.ForeColor = Color.Gray
                End If

                ' Handle empty delivery option (add this after the DeliveryAddress check)
                If row.Cells("DeliveryOption").Value Is Nothing OrElse
                    String.IsNullOrWhiteSpace(row.Cells("DeliveryOption").Value.ToString()) OrElse
                    IsDBNull(row.Cells("DeliveryOption").Value) Then
                    row.Cells("DeliveryOption").Value = "N/A"
                    row.Cells("DeliveryOption").Style.ForeColor = Color.Gray
                End If

                ' Handle empty ordered products
                If row.Cells("OrderedProducts").Value Is Nothing OrElse
                   String.IsNullOrWhiteSpace(row.Cells("OrderedProducts").Value.ToString()) Then
                    row.Cells("OrderedProducts").Value = "N/A"
                    row.Cells("OrderedProducts").Style.ForeColor = Color.Gray
                End If

                ' Handle empty special requests
                If row.Cells("SpecialRequests").Value Is Nothing OrElse
                   String.IsNullOrWhiteSpace(row.Cells("SpecialRequests").Value.ToString()) Then
                    row.Cells("SpecialRequests").Value = "N/A"
                    row.Cells("SpecialRequests").Style.ForeColor = Color.Gray
                End If

                ' Handle empty delivery address
                If row.Cells("DeliveryAddress").Value Is Nothing OrElse
                   String.IsNullOrWhiteSpace(row.Cells("DeliveryAddress").Value.ToString()) Then
                    row.Cells("DeliveryAddress").Value = "N/A"
                    row.Cells("DeliveryAddress").Style.ForeColor = Color.Gray
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
    Private Sub UpdateOrderStatus(orderID As Integer, newStatus As String)
        Try
            Using conn As New MySqlConnection("Server=127.0.0.1;User=root;Password=;Database=tabeya_system")
                conn.Open()
                Dim query As String = "UPDATE orders SET OrderStatus = @status, UpdatedDate = NOW() WHERE OrderID = @orderID"
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

            If status = "Pending" Then
                Dim confirmItem As New ToolStripMenuItem("Confirm Order")
                AddHandler confirmItem.Click, Sub() ConfirmOrder(orderID)
                contextMenu.Items.Add(confirmItem)

                Dim cancelItem As New ToolStripMenuItem("Cancel Order")
                AddHandler cancelItem.Click, Sub() CancelOrder(orderID)
                contextMenu.Items.Add(cancelItem)
                contextMenu.Items.Add(New ToolStripSeparator())
            ElseIf status = "Confirmed" Then
                If orderSource.ToLower() = "website" Then
                    Dim completeItem As New ToolStripMenuItem("Complete Order")
                    AddHandler completeItem.Click, Sub() CompleteOrder(orderID)
                    contextMenu.Items.Add(completeItem)
                Else
                    Dim serveItem As New ToolStripMenuItem("Serve Order (Complete)")
                    AddHandler serveItem.Click, Sub() CompleteOrder(orderID)
                    contextMenu.Items.Add(serveItem)
                End If

                Dim cancelItem As New ToolStripMenuItem("Cancel Order")
                AddHandler cancelItem.Click, Sub() CancelOrder(orderID)
                contextMenu.Items.Add(cancelItem)
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
            Dim orderedProducts As String = If(row.Cells("OrderedProducts").Value?.ToString(), "N/A")
            Dim specialRequests As String = If(row.Cells("SpecialRequests").Value?.ToString(), "N/A")
            Dim deliveryAddress As String = If(row.Cells("DeliveryAddress").Value?.ToString(), "N/A")
            Dim deliveryOption As String = If(row.Cells("DeliveryOption").Value?.ToString(), "N/A")
            Dim paymentMethod As String = If(row.Cells("PaymentMethod").Value?.ToString(), "N/A")
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
                      $"Delivery Option: {deliveryOption}" & vbCrLf &
                      $"Payment Method: {paymentMethod}" & vbCrLf &
                      $"Number of Diners: {row.Cells("NumberOfDiners").Value}" & vbCrLf &
                      $"Order Date: {row.Cells("OrderDate").Value}" & vbCrLf &
                      $"Order Time: {row.Cells("OrderTime").Value}" & vbCrLf &
                      $"Items Ordered: {row.Cells("ItemsOrderedCount").Value}" & vbCrLf &
                      $"Ordered Products: {orderedProducts}" & vbCrLf &
                      $"Total Amount: ₱{CDec(row.Cells("TotalAmount").Value):N2}" & vbCrLf &
                      $"Status: {row.Cells("OrderStatus").Value}" & vbCrLf &
                      $"Special Requests: {specialRequests}" & vbCrLf &
                      $"Delivery Address: {deliveryAddress}"

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
        LoadOrdersAsync("o.OrderStatus = 'Pending'")
        lblFilter.Text = "Showing: Pending Orders"
    End Sub

    Private Sub btnViewConfirmed_Click(sender As Object, e As EventArgs) Handles btnViewConfirmed.Click
        CurrentPage = 1
        LoadOrdersAsync("o.OrderStatus = 'Confirmed'")
        lblFilter.Text = "Showing: Confirmed Orders"
    End Sub

    Private Sub btnViewCompleted_Click(sender As Object, e As EventArgs) Handles btnViewCompleted.Click
        CurrentPage = 1
        LoadOrdersAsync("o.OrderStatus = 'Completed'")
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
    ' btnConfirm - Handle order status update
    ' ============================================================
    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Try
            If DataGridView2.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select an order to update.", "No Selection",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedRow As DataGridViewRow = DataGridView2.SelectedRows(0)
            Dim orderID As Integer = CInt(selectedRow.Cells("OrderID").Value)
            Dim currentStatus As String = selectedRow.Cells("OrderStatus").Value.ToString()
            Dim orderSource As String = If(selectedRow.Cells("OrderSource").Value IsNot Nothing,
                                          selectedRow.Cells("OrderSource").Value.ToString(), "")
            Dim customerName As String = GetCustomerName(selectedRow)

            ' Create status update form
            Dim statusForm As New Form()
            statusForm.Text = "Update Order Status"
            statusForm.Size = New Size(450, 310)
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
                Case "Pending"
                    cboStatus.Items.AddRange({"Confirmed", "Cancelled"})
                Case "Confirmed"
                    cboStatus.Items.AddRange({"Completed", "Cancelled"})
                Case Else
                    cboStatus.Items.AddRange({"Pending", "Confirmed", "Completed", "Cancelled"})
            End Select

            If cboStatus.Items.Count > 0 Then cboStatus.SelectedIndex = 0
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

            ' Buttons
            Dim btnUpdate As New Button()
            btnUpdate.Text = "Update Status"
            btnUpdate.Location = New Point(150, 170)
            btnUpdate.Size = New Size(120, 35)
            btnUpdate.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            btnUpdate.BackColor = Color.FromArgb(52, 152, 219)
            btnUpdate.ForeColor = Color.White
            btnUpdate.FlatStyle = FlatStyle.Flat
            btnUpdate.Cursor = Cursors.Hand

            AddHandler btnUpdate.Click, Sub()
                                            Dim newStatus As String = cboStatus.SelectedItem.ToString()
                                            UpdateOrderStatus(orderID, newStatus)
                                            statusForm.Close()
                                            LoadOrdersAsync(CurrentCondition)
                                        End Sub
            statusForm.Controls.Add(btnUpdate)

            Dim btnCancel As New Button()
            btnCancel.Text = "Cancel"
            btnCancel.Location = New Point(280, 170)
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