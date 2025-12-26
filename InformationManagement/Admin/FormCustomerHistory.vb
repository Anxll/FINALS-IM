Imports MySqlConnector
Imports System.Text
Imports System.Data
Imports System.Threading.Tasks

Public Class FormCustomerHistory
    Private ReadOnly connectionString As String = modDB.strConnection
    Private _isLoading As Boolean = False
    Private _baseTitle As String = ""

    ' Pagination state
    Private _currentPage As Integer = 1
    Private ReadOnly _pageSize As Integer = 50
    Private _totalRecords As Integer = 0
    Private _totalPages As Integer = 0

    Private Sub FormCustomerHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _baseTitle = Label1.Text
        ConfigureGrid()
        _currentPage = 1
        BeginLoadCustomerHistory()
    End Sub

    Private Async Sub BeginLoadCustomerHistory()
        If _isLoading Then Return
        _isLoading = True
        SetLoadingState(True)

        Try
            Dim searchText As String = txtSearch.Text.Trim()
            ' Get total count with search filter
            _totalRecords = Await Task.Run(Function() FetchTotalHistoryCount(searchText))
            _totalPages = Math.Ceiling(_totalRecords / _pageSize)
            If _totalPages = 0 Then _totalPages = 1
            If _currentPage > _totalPages Then _currentPage = _totalPages

            Dim offset As Integer = (_currentPage - 1) * _pageSize
            Dim table As DataTable = Await Task.Run(Function() FetchCustomerHistoryTable(searchText, offset, _pageSize))

            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return

            DataGridView1.DataSource = table
            UpdatePaginationUI()
        Catch ex As Exception
            If Not Me.IsDisposed Then
                MessageBox.Show("Error loading customer history:" & vbCrLf & ex.Message,
                                "Database Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
            End If
        Finally
            If Not Me.IsDisposed Then SetLoadingState(False)
            _isLoading = False
        End Try
    End Sub

    Private Function FetchTotalHistoryCount(searchText As String) As Integer
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

        Dim query As String = "SELECT COUNT(*) FROM orders WHERE (OrderID LIKE @search OR OrderType LIKE @search OR OrderStatus LIKE @search)" & periodFilter
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Function FetchCustomerHistoryTable(searchText As String, offset As Integer, limit As Integer) As DataTable
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

        ' Optimized query targeting only necessary columns and using paging with search
        Dim query As String =
            "SELECT " &
            "  o.OrderDate, " &
            "  o.OrderID, " &
            "  o.OrderType, " &
            "  (SELECT GROUP_CONCAT(CONCAT(ProductName, ' (', Quantity, ')') SEPARATOR ', ') " &
            "   FROM order_items WHERE OrderID = o.OrderID) AS Items, " &
            "  IFNULL(o.TotalAmount, 0) AS TotalAmount, " &
            "  o.OrderStatus " &
            "FROM orders o " &
            "WHERE (o.OrderID LIKE @search OR o.OrderType LIKE @search OR o.OrderStatus LIKE @search) " & periodFilter & " " &
            "ORDER BY o.OrderDate DESC, o.OrderID DESC " &
            "LIMIT @limit OFFSET @offset;"

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                cmd.Parameters.AddWithValue("@limit", limit)
                cmd.Parameters.AddWithValue("@offset", offset)
                cmd.CommandTimeout = 120
                Using adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)
                    Return table
                End Using
            End Using
        End Using
    End Function

    Private Sub ConfigureGrid()
        ' Use the designer-defined columns; bind to a DataTable for faster loading.
        DataGridView1.AutoGenerateColumns = False

        dateid.DataPropertyName = "OrderDate"
        Orderid.DataPropertyName = "OrderID"
        Type.DataPropertyName = "OrderType"
        Items.DataPropertyName = "Items"
        Amount.DataPropertyName = "TotalAmount"
        Status.DataPropertyName = "OrderStatus"

        dateid.DefaultCellStyle.Format = "yyyy-MM-dd"
        Amount.DefaultCellStyle.Format = "₱#,##0.00"

        DataGridView1.ReadOnly = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
    End Sub

    Private Sub SetLoadingState(isLoading As Boolean)
        Try
            Me.UseWaitCursor = isLoading
            DataGridView1.Enabled = Not isLoading
            Export.Enabled = Not isLoading
            If btnPrev IsNot Nothing Then btnPrev.Enabled = Not isLoading AndAlso _currentPage > 1
            If btnNext IsNot Nothing Then btnNext.Enabled = Not isLoading AndAlso _currentPage < _totalPages

            Label1.Text = If(isLoading, _baseTitle & " (Loading...)", _baseTitle)
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

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If _currentPage < _totalPages Then
            _currentPage += 1
            BeginLoadCustomerHistory()
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If _currentPage > 1 Then
            _currentPage -= 1
            BeginLoadCustomerHistory()
        End If
    End Sub

    ' Search Events
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        _currentPage = 1 ' Reset to first page on search
        BeginLoadCustomerHistory()
    End Sub

    Private Sub txtSearch_Enter(sender As Object, e As EventArgs) Handles txtSearch.Enter
        SearchContainer.BorderColor = Color.FromArgb(99, 102, 241) ' Indigo focus
    End Sub

    Private Sub txtSearch_Leave(sender As Object, e As EventArgs) Handles txtSearch.Leave
        SearchContainer.BorderColor = Color.FromArgb(226, 232, 240)
    End Sub

    Private Sub Export_Click(sender As Object, e As EventArgs) Handles Export.Click
        ExportToCSV()
    End Sub

    Private Sub ExportToCSV()
        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "CSV Files (*.csv)|*.csv",
                .FileName = String.Format("Customer_History_{0:yyyyMMdd_HHmmss}.csv", DateTime.Now),
                .Title = "Export Customer History Report"
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

                MessageBox.Show("Customer history report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Open file location
                Process.Start("explorer.exe", String.Format("/select,""{0}""", saveDialog.FileName))
            End If

        Catch ex As Exception
            MessageBox.Show("Failed to export CSV: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class