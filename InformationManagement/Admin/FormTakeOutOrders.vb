Imports MySqlConnector
Imports System.Data
Imports System.Threading.Tasks

Public Class FormTakeOutOrders
    Private ReadOnly connectionString As String = modDB.strConnection
    Private originalData As DataTable
    Private isInitialLoad As Boolean = True
    Private _isLoading As Boolean = False
    
    ' Pagination state
    Private _currentPage As Integer = 1
    Private ReadOnly _pageSize As Integer = 50
    Private _totalRecords As Integer = 0
    Private _totalPages As Integer = 0

    Private Async Sub FormTakeOutOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set initial loading state
        Label4.Text = "..."
        Label6.Text = "..."
        Label7.Text = "..."
        
        Await RefreshOrdersAsync()
        isInitialLoad = False
    End Sub

    Public Async Sub RefreshData()
        _currentPage = 1
        Await RefreshOrdersAsync()
    End Sub

    Private Async Function RefreshOrdersAsync(Optional resetPage As Boolean = False) As Task
        If _isLoading Then Return
        _isLoading = True
        SetLoadingState(True)

        If resetPage Then _currentPage = 1

        Try
            Dim searchText As String = TextBoxSearch.Text.Trim()
            If searchText = "Search orders..." Then searchText = ""

            ' Get total count
            _totalRecords = Await Task.Run(Function() FetchTotalTakeOutCount(searchText))
            _totalPages = Math.Ceiling(_totalRecords / _pageSize)
            If _totalPages = 0 Then _totalPages = 1
            If _currentPage > _totalPages Then _currentPage = _totalPages

            Dim offset As Integer = (_currentPage - 1) * _pageSize
            Dim dt As DataTable = Await Task.Run(Function() LoadOrdersDataFromDB(searchText, offset, _pageSize))
            
            originalData = dt
            
            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return

            ' Update UI on UI thread
            Me.Invoke(Sub()
                          DataGridView1.DataSource = dt
                          FormatGrid()
                          UpdatePaginationUI()
                          Label10.Text = "Recent Take-Out Orders"
                      End Sub)

            ' Update summary with total stats (non-paginated)
            Await UpdateTotalSummaryAsync(searchText)
                      
        Catch ex As Exception
            If Not Me.IsDisposed Then
                MessageBox.Show("Error refreshing orders: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            If Not Me.IsDisposed Then
                SetLoadingState(False)
                _isLoading = False
            End If
        End Try
    End Function

    Private Function FetchTotalTakeOutCount(searchText As String) As Integer
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
            Case Else
                periodFilter = "" ' All Time
        End Select

        Dim query As String = "SELECT COUNT(*) FROM orders WHERE OrderType = 'Takeout' " & periodFilter & " AND (OrderID LIKE @search OR OrderStatus LIKE @search)"
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Function LoadOrdersDataFromDB(searchText As String, offset As Integer, limit As Integer) As DataTable
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
            Case Else
                periodFilter = "" ' All Time
        End Select

        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                ' Note: Some schemas use ItemsOrderedCount, others use subqueries. 
                ' Standardizing to o.OrderDate and o.OrderTime
                Dim query As String =
                    "SELECT " &
                    "o.OrderID, " &
                    "CONCAT('#', o.OrderID) AS OrderNumber, " &
                    "COALESCE(o.ItemsOrderedCount, (SELECT SUM(Quantity) FROM order_items WHERE OrderID = o.OrderID)) AS Items, " &
                    "o.TotalAmount AS Amount, " &
                    "o.OrderStatus AS Status, " &
                    "DATE_FORMAT(CONCAT(o.OrderDate, ' ', o.OrderTime), '%Y-%m-%d %H:%i') AS Time " &
                    "FROM orders o " &
                    "WHERE o.OrderType = 'Takeout' " & periodFilter & " AND (o.OrderID LIKE @search OR o.OrderStatus LIKE @search) " &
                    "ORDER BY o.OrderDate DESC, o.OrderTime DESC " &
                    "LIMIT @limit OFFSET @offset"
                
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                    cmd.Parameters.AddWithValue("@limit", limit)
                    cmd.Parameters.AddWithValue("@offset", offset)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    Private Async Function UpdateTotalSummaryAsync(searchText As String) As Task
        Dim totalCount As Integer = 0
        Dim totalRevenue As Decimal = 0
        Dim avgValue As Decimal = 0

        Try
            Await Task.Run(Sub()
                               ' Re-use the same period filter logic
                               Dim periodFilter As String = ""
                               Select Case Reports.SelectedPeriod
                                   Case "Daily" : periodFilter = " AND DATE(OrderDate) = CURDATE() "
                                   Case "Weekly" : periodFilter = " AND YEARWEEK(OrderDate, 1) = YEARWEEK(CURDATE(), 1) "
                                   Case "Monthly" : periodFilter = " AND MONTH(OrderDate) = MONTH(CURDATE()) AND YEAR(OrderDate) = YEAR(CURDATE()) "
                                   Case "Yearly" : periodFilter = " AND YEAR(OrderDate) = YEAR(CURDATE()) "
                                   Case Else : periodFilter = "" ' All Time
                               End Select

                               Using conn As New MySqlConnection(connectionString)
                                   conn.Open()
                                   Dim sql = "SELECT COUNT(*), COALESCE(SUM(TotalAmount), 0) FROM orders WHERE OrderType = 'Takeout' " & periodFilter & " AND (OrderID LIKE @search OR OrderStatus LIKE @search)"
                                   Using cmd As New MySqlCommand(sql, conn)
                                       cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                                       Using reader = cmd.ExecuteReader()
                                           If reader.Read() Then
                                               totalCount = reader.GetInt32(0)
                                               totalRevenue = reader.GetDecimal(1)
                                           End If
                                       End Using
                                   End Using
                               End Using
                           End Sub)

            If totalCount > 0 Then avgValue = totalRevenue / totalCount

            ' Update UI labels
            Me.Invoke(Sub()
                          Label4.Text = totalCount.ToString("N0")
                          Label6.Text = "₱" & totalRevenue.ToString("N2")
                          Label7.Text = "₱" & avgValue.ToString("N2")
                      End Sub)
        Catch
            ' Silent fail
        End Try
    End Function

    Private Sub UpdateSummaryTiles(dt As DataTable)
        Try
            Dim totalOrders As Integer = dt.Rows.Count
            Dim totalRevenue As Decimal = 0
            Dim avgOrderValue As Decimal = 0

            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("Amount")) Then
                    totalRevenue += Convert.ToDecimal(row("Amount"))
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
    Private Async Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        If isInitialLoad Then Return
        Await RefreshOrdersAsync(True) ' Reset to page 1 on search
    End Sub

    Private Sub SetLoadingState(isLoading As Boolean)
        Try
            Me.UseWaitCursor = isLoading
            DataGridView1.Enabled = Not isLoading
            Button1.Enabled = Not isLoading
            If btnPrev IsNot Nothing Then btnPrev.Enabled = Not isLoading AndAlso _currentPage > 1
            If btnNext IsNot Nothing Then btnNext.Enabled = Not isLoading AndAlso _currentPage < _totalPages
        Catch
        End Try
    End Sub

    Private Sub UpdatePaginationUI()
        If lblPageStatus IsNot Nothing Then
            lblPageStatus.Text = $"Page {_currentPage} of {_totalPages} (Total Records: {_totalRecords:N0})"
        End If
        If btnPrev IsNot Nothing Then btnPrev.Enabled = (_currentPage > 1)
        If btnNext IsNot Nothing Then btnNext.Enabled = (_currentPage < _totalPages)
    End Sub

    Private Async Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If _currentPage < _totalPages Then
            _currentPage += 1
            Await RefreshOrdersAsync()
        End If
    End Sub

    Private Async Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If _currentPage > 1 Then
            _currentPage -= 1
            Await RefreshOrdersAsync()
        End If
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


    ' =============================
    ' FORMAT GRID
    ' =============================
    Private Sub FormatGrid()
        With DataGridView1
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .RowHeadersVisible = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .GridColor = Color.FromArgb(241, 245, 249)
            .RowTemplate.Height = 50
            .ColumnHeadersHeight = 50
            .EnableHeadersVisualStyles = False
            
            ' Header Styling
            .ColumnHeadersDefaultCellStyle.BackColor = Color.White
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105)
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 10)
            .ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White
            
            ' Cell Styling
            .DefaultCellStyle.Font = New Font("Segoe UI", 10)
            .DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59)
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252)
            .DefaultCellStyle.SelectionForeColor = Color.FromArgb(99, 102, 241)
        
            If .Columns.Contains("OrderID") Then .Columns("OrderID").Visible = False
            
            If .Columns.Contains("OrderNumber") Then
                With .Columns("OrderNumber")
                    .HeaderText = "Order #"
                    .FillWeight = 80
                End With
            End If

            If .Columns.Contains("Items") Then
                With .Columns("Items")
                    .HeaderText = "Items"
                    .FillWeight = 60
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End With
            End If

            If .Columns.Contains("Amount") Then
                With .Columns("Amount")
                    .HeaderText = "Total Amount"
                    .DefaultCellStyle.Format = "₱#,##0.00"
                    .DefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42)
                    .DefaultCellStyle.Font = New Font("Segoe UI Semibold", 10)
                End With
            End If

            If .Columns.Contains("Time") Then
                With .Columns("Time")
                    .HeaderText = "Date & Time"
                    .FillWeight = 120
                End With
            End If
        End With

        ' Apply status colors
        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim statusCell = row.Cells("Status")
            If statusCell.Value IsNot Nothing Then
                Dim status = statusCell.Value.ToString().ToLower()
                Select Case status
                    Case "paid", "completed"
                        statusCell.Style.ForeColor = Color.FromArgb(16, 185, 129) ' Success Green
                    Case "pending"
                        statusCell.Style.ForeColor = Color.FromArgb(245, 158, 11) ' Warning Amber
                    Case "cancelled"
                        statusCell.Style.ForeColor = Color.FromArgb(239, 68, 68) ' Danger Red
                End Select
            End If
        Next
    End Sub

    ' =============================
    ' PREVENT ERROR POPUPS
    ' =============================
    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) _
        Handles DataGridView1.DataError
        e.ThrowException = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ExportToCSV()
    End Sub

    Private Sub ExportToCSV()
        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "CSV Files (*.csv)|*.csv",
                .FileName = String.Format("Takeout_Orders_{0:yyyyMMdd_HHmmss}.csv", DateTime.Now),
                .Title = "Export Takeout Orders Report"
            }

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Using writer As New IO.StreamWriter(saveDialog.FileName)
                    ' Write headers
                    Dim headers As New List(Of String)
                    For Each column As DataGridViewColumn In DataGridView1.Columns
                        If column.Visible Then
                            headers.Add(column.HeaderText)
                        End If
                    Next
                    writer.WriteLine(String.Join(",", headers))

                    ' Write data rows
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If Not row.IsNewRow Then
                            Dim values As New List(Of String)
                            For Each column As DataGridViewColumn In DataGridView1.Columns
                                If column.Visible Then
                                    Dim cellVal As Object = row.Cells(column.Index).Value
                                    Dim cellValue As String = If(cellVal IsNot Nothing, cellVal.ToString(), "")

                                    ' Remove currency symbols and format properly
                                    cellValue = cellValue.Replace("₱", "").Trim()

                                    ' Escape commas and quotes
                                    If cellValue.Contains(",") OrElse cellValue.Contains("""") Then
                                        cellValue = """" & cellValue.Replace("""", """""") & """"
                                    End If
                                    values.Add(cellValue)
                                End If
                            Next
                            writer.WriteLine(String.Join(",", values))
                        End If
                    Next
                End Using

                MessageBox.Show("Takeout orders report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Open file location
                Process.Start("explorer.exe", String.Format("/select,""{0}""", saveDialog.FileName))
            End If

        Catch ex As Exception
            MessageBox.Show("Failed to export CSV: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
