Imports MySqlConnector
Imports System.Data
Imports System.Threading.Tasks
Imports System.Drawing.Drawing2D

Public Class FormDineInOrders
    Private ReadOnly connectionString As String = modDB.strConnection
    Private _isLoading As Boolean = False
    Private _baseTitle As String = ""
    Private _dataCache As DataTable = Nothing
    Private _lastRefresh As DateTime = DateTime.MinValue
    Private ReadOnly _cacheTimeout As TimeSpan = TimeSpan.FromSeconds(30)

    ' Pagination state
    Private _currentPage As Integer = 1
    Private ReadOnly _pageSize As Integer = 50
    Private _totalRecords As Integer = 0
    Private _totalPages As Integer = 0

    Private originalData As DataTable
    Private isInitialLoad As Boolean = True

    Private Async Sub FormDineInOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set initial loading state
        Label4.Text = "..."
        Label6.Text = "..."
        Label7.Text = "..."

        _baseTitle = LabelHeader.Text
        _currentPage = 1
        Await BeginLoadDineInOrders()
        isInitialLoad = False
    End Sub

    Private Sub InitializeModernUI()
        ' Enhanced form appearance
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint, True)

        ' modern DataGridView styling
        With DataGridView1
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .BackgroundColor = Color.White
            .GridColor = Color.FromArgb(241, 245, 249)
            .RowTemplate.Height = 50
            .EnableHeadersVisualStyles = False
            .AllowUserToResizeRows = False

            ' modern header style
            .ColumnHeadersDefaultCellStyle.BackColor = Color.White
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105)
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 10)
            .ColumnHeadersDefaultCellStyle.Padding = New Padding(5)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .ColumnHeadersHeight = 50

            ' modern row style
            .DefaultCellStyle.Font = New Font("Segoe UI", 10)
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252)
            .DefaultCellStyle.SelectionForeColor = Color.FromArgb(99, 102, 241)
            .DefaultCellStyle.BackColor = Color.White
            .DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59)
            .DefaultCellStyle.Padding = New Padding(5, 8, 5, 8)

            ' Alternating row colors removed for premium clean look
            .AlternatingRowsDefaultCellStyle.BackColor = Color.White
        End With

        ' Style the export button
        StyleButton(Button1)

        ' Style the label
        Label2.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        Label2.ForeColor = Color.FromArgb(44, 62, 80)
    End Sub

    Private Sub StyleButton(btn As Button)
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.BackColor = Color.FromArgb(46, 204, 113)
        btn.ForeColor = Color.White
        btn.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btn.Cursor = Cursors.Hand
        btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96)
        btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(34, 153, 84)
    End Sub

    Private Async Function BeginLoadDineInOrders() As Task
        If _isLoading Then Return

        _isLoading = True
        SetLoadingState(True)

        Try
            Dim searchText As String = TextBoxSearch.Text.Trim()
            If searchText = "Search orders..." Then searchText = ""

            ' Get total count with filter
            _totalRecords = Await Task.Run(Function() FetchTotalDineInCount(searchText))
            _totalPages = Math.Ceiling(_totalRecords / _pageSize)
            If _totalPages = 0 Then _totalPages = 1
            If _currentPage > _totalPages Then _currentPage = _totalPages

            Dim offset As Integer = (_currentPage - 1) * _pageSize
            Dim table As DataTable = Await Task.Run(Function() FetchDineInOrdersTable(searchText, offset, _pageSize))

            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return

            _dataCache = table
            _lastRefresh = DateTime.Now

            DataGridView1.DataSource = table
            ConfigureGrid()
            ApplyStatusColors()
            ' UpdatePaginationUI() ' This function is not yet defined, will be added later
            UpdateSummaryTiles(table) ' We'll update summary based on current page or we might need another query for total stats.
            ' For now, keeping it simple with current page stats or total if needed.
        Catch ex As Exception
            If Not Me.IsDisposed Then
                MessageBox.Show("Error refreshing dine-in orders: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            If Not Me.IsDisposed Then SetLoadingState(False)
            _isLoading = False
        End Try
    End Function

    Private Function FetchTotalDineInCount(searchText As String) As Integer
        ' Get period filter from Reports form
        Dim periodFilter As String = ""
        Select Case Reports.SelectedPeriod
            Case "Daily"
                periodFilter = " AND DATE(OrderDate) = CURDATE() "
            Case "Weekly"
                periodFilter = " AND YEARWEEK(OrderDate, 1) = YEARWEEK(CURDATE(), 1) "
            Case "Monthly"
                periodFilter = " AND MONTH(OrderDate) = MONTH(CURDATE()) AND YEAR(OrderDate) = YEAR(CURDATE()) "
            Case "Yearly"
                periodFilter = " AND YEAR(OrderDate) = YEAR(CURDATE()) "
        End Select

        Dim query As String = "SELECT COUNT(*) FROM orders WHERE OrderType = 'Dine-in' " & periodFilter & " AND (OrderID LIKE @search OR OrderStatus LIKE @search)"
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Function FetchDineInOrdersTable(searchText As String, offset As Integer, limit As Integer) As DataTable
        ' Get period filter from Reports form
        Dim periodFilter As String = ""
        Select Case Reports.SelectedPeriod
            Case "Daily"
                periodFilter = " AND DATE(o.OrderDate) = CURDATE() "
            Case "Weekly"
                periodFilter = " AND YEARWEEK(o.OrderDate, 1) = YEARWEEK(CURDATE(), 1) "
            Case "Monthly"
                periodFilter = " AND MONTH(o.OrderDate) = MONTH(CURDATE()) AND YEAR(o.OrderDate) = YEAR(CURDATE()) "
            Case "Yearly"
                periodFilter = " AND YEAR(o.OrderDate) = YEAR(CURDATE()) "
        End Select

        ' Build query with LIMIT, OFFSET and search
        Dim query As String =
            "SELECT " &
            "o.OrderID, " &
            "CONCAT('#', o.OrderID) AS OrderNumber, " &
            "(SELECT GROUP_CONCAT(CONCAT(oi2.Quantity, 'x ', oi2.ProductName) SEPARATOR ', ') " &
            "   FROM order_items oi2 " &
            "   WHERE oi2.OrderID = o.OrderID " &
            "   LIMIT 10) AS ItemsOrdered, " &
            "o.TotalAmount, " &
            "o.OrderStatus AS Status, " &
            "DATE_FORMAT(CONCAT(o.OrderDate, ' ', o.OrderTime), '%Y-%m-%d %H:%i') AS OrderDateTime " &
            "FROM orders o " &
            "WHERE o.OrderType = 'Dine-in' " & periodFilter & " AND (o.OrderID LIKE @search OR o.OrderStatus LIKE @search) " &
            "ORDER BY o.OrderDate DESC, o.OrderTime DESC " &
            "LIMIT @limit OFFSET @offset"

        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                cmd.Parameters.AddWithValue("@limit", limit)
                cmd.Parameters.AddWithValue("@offset", offset)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Private Sub UpdateSummaryTiles(dt As DataTable)
        Try
            Dim totalOrders As Integer = dt.Rows.Count
            Dim totalRevenue As Decimal = 0
            Dim avgOrderValue As Decimal = 0

            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("TotalAmount")) Then
                    totalRevenue += Convert.ToDecimal(row("TotalAmount"))
                End If
            Next

            If totalOrders > 0 Then
                avgOrderValue = totalRevenue / totalOrders
            End If

            ' Safe UI updates
            Label4.Text = totalOrders.ToString("N0")
            Label6.Text = "₱" & totalRevenue.ToString("N2")
            Label7.Text = "₱" & avgOrderValue.ToString("N2")

        Catch ex As Exception
            ' Silent fail for stats
        End Try
    End Sub

    ' =============================
    ' SEARCH FUNCTIONALITY
    ' =============================
    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        If isInitialLoad OrElse originalData Is Nothing Then Return

        Dim filter = TextBoxSearch.Text.Trim()
        If filter = "Search orders..." OrElse String.IsNullOrEmpty(filter) Then
            DataGridView1.DataSource = originalData
        Else
            Try
                Dim dv As New DataView(originalData)
                dv.RowFilter = String.Format("OrderID = {0} OR Status LIKE '%{1}%'", If(IsNumeric(filter), filter, -1), filter)
                DataGridView1.DataSource = dv
            Catch
                ' Fallback
            End Try
        End If

        Try
            Dim dataSource = DataGridView1.DataSource
            If TypeOf dataSource Is DataView Then
                UpdateSummaryTiles(CType(dataSource, DataView).ToTable())
            ElseIf TypeOf dataSource Is DataTable Then
                UpdateSummaryTiles(CType(dataSource, DataTable))
            End If
        Catch
        End Try
        ApplyStatusColors()
    End Sub

    Private Sub TextBoxSearch_Enter(sender As Object, e As EventArgs) Handles TextBoxSearch.Enter
        If TextBoxSearch.Text = "Search orders..." Then
            TextBoxSearch.Text = ""
            TextBoxSearch.ForeColor = Color.FromArgb(15, 23, 42)
            SearchContainer.BorderColor = Color.FromArgb(99, 102, 241)
        End If
    End Sub

    Private Sub TextBoxSearch_Leave(sender As Object, e As EventArgs) Handles TextBoxSearch.Leave
        If String.IsNullOrWhiteSpace(TextBoxSearch.Text) Then
            TextBoxSearch.Text = "Search orders..."
            TextBoxSearch.ForeColor = Color.FromArgb(148, 163, 184)
            SearchContainer.BorderColor = Color.FromArgb(226, 232, 240)
        End If
    End Sub



    ' FIXED: Improved status color application
    Private Sub ApplyStatusColors()
        Try
            ' Add visual indicators for order status
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow AndAlso row.Cells("Status").Value IsNot Nothing Then
                    Dim status As String = row.Cells("Status").Value.ToString().Trim().ToLower()

                    ' Reset to default first
                    row.Cells("Status").Style.ForeColor = Color.FromArgb(44, 62, 80)
                    row.Cells("Status").Style.Font = New Font("Segoe UI", 9, FontStyle.Regular)

                    ' Apply status-specific colors
                    Select Case status
                        Case "completed", "paid"
                            row.Cells("Status").Style.ForeColor = Color.FromArgb(16, 185, 129)
                        Case "pending", "preparing"
                            row.Cells("Status").Style.ForeColor = Color.FromArgb(245, 158, 11)
                        Case "cancelled", "canceled"
                            row.Cells("Status").Style.ForeColor = Color.FromArgb(239, 68, 68)
                    End Select
                End If
            Next

            ' Force redraw
            DataGridView1.InvalidateColumn(DataGridView1.Columns("Status").Index)
        Catch ex As Exception
            ' Silently handle errors in color application
            Debug.WriteLine("Error applying status colors: " & ex.Message)
        End Try
    End Sub

    ' ALTERNATIVE: Use CellFormatting event for automatic color application
    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Try
            If DataGridView1.Columns(e.ColumnIndex).Name = "Status" AndAlso e.Value IsNot Nothing Then
                Dim status As String = e.Value.ToString().Trim().ToLower()

                Select Case status
                    Case "completed", "paid"
                        e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96)
                        e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    Case "pending", "preparing"
                        e.CellStyle.ForeColor = Color.FromArgb(241, 196, 15)
                        e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    Case "cancelled", "canceled"
                        e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60)
                        e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                End Select
            End If
        Catch ex As Exception
            ' Silently handle formatting errors
        End Try
    End Sub

    Private Sub SetLoadingState(isLoading As Boolean)
        Try
            Me.UseWaitCursor = isLoading
            DataGridView1.Enabled = Not isLoading
            Button1.Enabled = Not isLoading
            If btnPrev IsNot Nothing Then btnPrev.Enabled = Not isLoading AndAlso _currentPage > 1
            If btnNext IsNot Nothing Then btnNext.Enabled = Not isLoading AndAlso _currentPage < _totalPages
            LabelHeader.Text = If(isLoading, _baseTitle & " (Updating...)", _baseTitle)

            If isLoading Then
                Button1.Text = "   Loading..."
            Else
                Button1.Text = "   Export"
            End If
        Catch
        End Try
    End Sub

    Private Sub ConfigureGrid()
        With DataGridView1
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AllowUserToOrderColumns = False
            .AllowUserToAddRows = False
        End With

        ' Optimized column configuration
        If DataGridView1.Columns.Contains("OrderID") Then
            With DataGridView1.Columns("OrderID")
                .HeaderText = "Order #"
                .Width = 100
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
            End With
        End If

        If DataGridView1.Columns.Contains("ItemsOrdered") Then
            With DataGridView1.Columns("ItemsOrdered")
                .HeaderText = "Items Ordered"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .MinimumWidth = 300
                .DefaultCellStyle.WrapMode = DataGridViewTriState.False
            End With
        End If

        If DataGridView1.Columns.Contains("TotalAmount") Then
            With DataGridView1.Columns("TotalAmount")
                .HeaderText = "Amount"
                .Width = 130
                .DefaultCellStyle.Format = "₱#,##0.00"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                .DefaultCellStyle.ForeColor = Color.FromArgb(52, 73, 94)
            End With
        End If

        If DataGridView1.Columns.Contains("Status") Then
            With DataGridView1.Columns("Status")
                .HeaderText = "Status"
                .Width = 120
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End If

        If DataGridView1.Columns.Contains("OrderDateTime") Then
            With DataGridView1.Columns("OrderDateTime")
                .HeaderText = "Date & Time"
                .Width = 150
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If _currentPage > 1 Then
            _currentPage -= 1
            BeginLoadDineInOrders()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ExportToCSV()
    End Sub

    Private Sub ExportToCSV()
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "CSV Files (*.csv)|*.csv",
                .FileName = String.Format("DineIn_Orders_{0:yyyyMMdd_HHmmss}.csv", DateTime.Now),
                .Title = "Export Dine-In Orders Report"
            }

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Button1.Enabled = False
                Button1.Text = "   Exporting..."

                Using writer As New IO.StreamWriter(saveDialog.FileName, False, System.Text.Encoding.UTF8)
                    ' Write headers
                    Dim headers As New List(Of String)
                    For Each column As DataGridViewColumn In DataGridView1.Columns
                        If column.Visible Then
                            headers.Add(EscapeCSV(column.HeaderText))
                        End If
                    Next
                    writer.WriteLine(String.Join(",", headers))

                    ' Write data rows (optimized)
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If Not row.IsNewRow Then
                            Dim values As New List(Of String)
                            For Each column As DataGridViewColumn In DataGridView1.Columns
                                If column.Visible Then
                                    Dim cellValue As String = GetCellValueAsString(row.Cells(column.Index))
                                    values.Add(EscapeCSV(cellValue))
                                End If
                            Next
                            writer.WriteLine(String.Join(",", values))
                        End If
                    Next
                End Using

                MessageBox.Show("Export completed successfully!" & vbCrLf &
                              DataGridView1.Rows.Count & " orders exported.",
                              "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Process.Start("explorer.exe", String.Format("/select,""{0}""", saveDialog.FileName))
            End If

        Catch ex As Exception
            MessageBox.Show("Export failed: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Button1.Enabled = True
            Button1.Text = "   Export"
        End Try
    End Sub

    Private Function GetCellValueAsString(cell As DataGridViewCell) As String
        If cell.Value Is Nothing Then Return ""
        Dim value As String = cell.Value.ToString()
        Return value.Replace("₱", "").Trim()
    End Function

    Private Function EscapeCSV(value As String) As String
        If String.IsNullOrEmpty(value) Then Return ""
        If value.Contains(",") OrElse value.Contains("""") OrElse value.Contains(vbCrLf) Then
            Return """" & value.Replace("""", """""") & """"
        End If
        Return value
    End Function

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        _dataCache?.Dispose()
        MyBase.OnFormClosing(e)
    End Sub
End Class