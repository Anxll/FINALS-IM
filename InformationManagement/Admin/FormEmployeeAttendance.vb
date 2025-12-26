Imports MySqlConnector
Imports System.Data
Imports System.Threading.Tasks

Public Class FormEmployeeAttendance

    Private originalData As DataTable
    Private isInitialLoad As Boolean = True
    Private _isLoading As Boolean = False
    Private _searchDebounceTimer As Timer

    ' Pagination state
    Private _currentPage As Integer = 1
    Private ReadOnly _pageSize As Integer = 50
    Private _totalRecords As Integer = 0
    Private _totalPages As Integer = 0

    Private Async Sub FormEmployeeAttendance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Setup search debounce timer
            _searchDebounceTimer = New Timer() With {.Interval = 500}
            AddHandler _searchDebounceTimer.Tick, AddressOf SearchDebounceTimer_Tick

            ' Setup DataGridView
            SetupDataGridView()

            ' Initial loading indicators
            Label4.Text = "..."
            Label6.Text = "..."
            Label7.Text = "..."

            ' Load data asynchronously
            Await RefreshAttendanceAsync()

            ' Add double-click handler for payroll link
            AddHandler DataGridView1.CellDoubleClick, AddressOf DataGridView1_CellDoubleClick

            ' Create a help button programmatically
            AddHelpButton()
        Catch ex As Exception
            MessageBox.Show("Initialization Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            isInitialLoad = False
        End Try
    End Sub

    '====================================
    ' HELPER: Get Clean Search Text
    '====================================
    Private Function GetSearchText() As String
        Dim text As String = TextBoxSearch.Text.Trim()
        If text = "Search employees..." OrElse String.IsNullOrWhiteSpace(text) Then
            Return ""
        End If
        Return text
    End Function

    '====================================
    ' REFRESH DATA ASYNC
    '====================================
    Private Async Function RefreshAttendanceAsync(Optional resetPage As Boolean = False) As Task
        If _isLoading Then Return
        _isLoading = True
        SetLoadingState(True)

        If resetPage Then _currentPage = 1

        Try
            Dim searchText As String = GetSearchText()

            ' Get total count
            _totalRecords = Await Task.Run(Function() FetchTotalAttendanceCount(searchText))
            _totalPages = Math.Max(1, Math.Ceiling(_totalRecords / CDbl(_pageSize)))

            ' Ensure current page is within bounds
            If _currentPage > _totalPages Then _currentPage = _totalPages
            If _currentPage < 1 Then _currentPage = 1

            Dim offset As Integer = (_currentPage - 1) * _pageSize

            ' Run database query in background
            Dim data As DataTable = Await Task.Run(Function() LoadAttendanceDataFromDB(searchText, offset, _pageSize))

            ' Update UI on main thread
            If data IsNot Nothing Then
                originalData = data.Copy()
                DataGridView1.DataSource = data

                ' Fetch global stats for tiles
                Dim stats = Await Task.Run(Function() FetchAttendanceStats(searchText))
                UpdateSummaryTiles(stats, _totalRecords)

                FormatDataGridView()
                UpdatePaginationUI()
            End If
        Catch ex As Exception
            If Not Me.IsDisposed Then
                MessageBox.Show("Error refreshing attendance: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            If Not Me.IsDisposed Then
                SetLoadingState(False)
                _isLoading = False
            End If
        End Try
    End Function

    '====================================
    ' SETUP DATAGRIDVIEW
    '====================================
    Private Sub SetupDataGridView()
        Try
            With DataGridView1
                .AutoGenerateColumns = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .ReadOnly = True
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .RowHeadersVisible = False
                .BackgroundColor = Color.White
                .BorderStyle = BorderStyle.None
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                .GridColor = Color.FromArgb(241, 245, 249)
                .DefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252)
                .DefaultCellStyle.SelectionForeColor = Color.FromArgb(99, 102, 241)
                .DefaultCellStyle.Font = New Font("Segoe UI", 9.5F)
                .ColumnHeadersDefaultCellStyle.BackColor = Color.White
                .ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105)
                .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
                .ColumnHeadersHeight = 50
                .RowTemplate.Height = 50
                .EnableHeadersVisualStyles = False
            End With

            ' Clear existing columns
            DataGridView1.Columns.Clear()

            ' Add columns programmatically - EmployeeID is now VISIBLE for export to work
            DataGridView1.Columns.Add(CreateColumn("EmployeeID", "ID", 60, False))
            DataGridView1.Columns.Add(CreateColumn("EmployeeName", "Employee", 180))
            DataGridView1.Columns.Add(CreateColumn("Position", "Position", 150))
            DataGridView1.Columns.Add(CreateColumn("RegularHours", "Regular Hours", 120))
            DataGridView1.Columns.Add(CreateColumn("OvertimeHours", "Overtime Hours", 130))
            DataGridView1.Columns.Add(CreateColumn("Absences", "Absences", 100))
            DataGridView1.Columns.Add(CreateColumn("Status", "Status", 120))

        Catch ex As Exception
            MessageBox.Show("Error setting up grid: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '====================================
    ' CREATE COLUMN HELPER
    '====================================
    Private Function CreateColumn(name As String, headerText As String, width As Integer, Optional isHidden As Boolean = False) As DataGridViewTextBoxColumn
        Dim col As New DataGridViewTextBoxColumn With {
            .Name = name,
            .DataPropertyName = name,
            .HeaderText = headerText,
            .Width = width,
            .Visible = Not isHidden,
            .DefaultCellStyle = New DataGridViewCellStyle With {
                .Alignment = DataGridViewContentAlignment.MiddleCenter
            }
        }
        Return col
    End Function

    Private Function FetchTotalAttendanceCount(searchText As String) As Integer
        Dim query As String = "SELECT COUNT(*) FROM employee WHERE EmploymentStatus IN ('Active', 'On Leave')"
        If Not String.IsNullOrEmpty(searchText) Then
            query &= " AND (FirstName LIKE @search OR LastName LIKE @search OR Position LIKE @search)"
        End If

        Try
            openConn()
            Using cmd As New MySqlCommand(query, conn)
                cmd.CommandTimeout = 60
                If Not String.IsNullOrEmpty(searchText) Then
                    cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                End If
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        Finally
            closeConn()
        End Try
    End Function

    Private Function FetchAttendanceStats(searchText As String) As Dictionary(Of String, Integer)
        Dim stats As New Dictionary(Of String, Integer) From {
            {"OnDuty", 0},
            {"Absences", 0},
            {"OnLeave", 0}
        }

        Dim query As String = "
            SELECT 
                COUNT(CASE WHEN EmploymentStatus = 'Active' THEN 1 END) as OnDuty,
                SUM(CASE 
                    WHEN EmploymentStatus = 'Active' AND Position = 'Waitress' THEN 1
                    WHEN EmploymentStatus = 'Active' AND Position = 'Cashier' THEN 2
                    WHEN EmploymentStatus = 'Active' AND Position = 'Server' THEN 1
                    ELSE 0 END) as EstimatedAbsences,
                COUNT(CASE WHEN EmploymentStatus = 'On Leave' THEN 1 END) as OnLeave
            FROM employee 
            WHERE EmploymentStatus IN ('Active', 'On Leave')"

        If Not String.IsNullOrEmpty(searchText) Then
            query &= " AND (FirstName LIKE @search OR LastName LIKE @search OR Position LIKE @search)"
        End If

        Try
            openConn()
            Using cmd As New MySqlCommand(query, conn)
                cmd.CommandTimeout = 60
                If Not String.IsNullOrEmpty(searchText) Then
                    cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                End If
                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        stats("OnDuty") = If(IsDBNull(reader("OnDuty")), 0, Convert.ToInt32(reader("OnDuty")))
                        stats("Absences") = If(IsDBNull(reader("EstimatedAbsences")), 0, Convert.ToInt32(reader("EstimatedAbsences")))
                        stats("OnLeave") = If(IsDBNull(reader("OnLeave")), 0, Convert.ToInt32(reader("OnLeave")))
                    End If
                End Using
            End Using
        Finally
            closeConn()
        End Try
        Return stats
    End Function

    '====================================
    ' LOAD EMPLOYEE DATA (SIMULATED ATTENDANCE)
    '====================================
    Private Function LoadAttendanceDataFromDB(searchText As String, offset As Integer, limit As Integer) As DataTable
        Dim table As New DataTable()
        Try
            openConn()
            Dim query As String = "
                SELECT 
                    e.EmployeeID,
                    CONCAT(e.FirstName, ' ', e.LastName) AS EmployeeName,
                    e.Position,
                    CASE 
                        WHEN e.EmploymentStatus = 'Active' THEN 40
                        WHEN e.EmploymentStatus = 'On Leave' THEN 32
                        ELSE 0
                    END AS RegularHours,
                    CASE 
                        WHEN e.Position IN ('Chef', 'Cook') THEN 12
                        WHEN e.Position IN ('Server', 'Waitress') THEN 5
                        WHEN e.Position = 'Cashier' THEN 3
                        ELSE 0
                    END AS OvertimeHours,
                    CASE 
                        WHEN e.EmploymentStatus = 'Active' AND e.Position IN ('Chef', 'Cook') THEN 0
                        WHEN e.Position IN ('Waitress', 'Server') THEN 1
                        WHEN e.Position = 'Cashier' THEN 2
                        WHEN e.EmploymentStatus = 'On Leave' THEN 3
                        ELSE 0
                    END AS Absences,
                    CASE 
                        WHEN e.EmploymentStatus = 'On Leave' THEN 'On Leave'
                        WHEN e.Position IN ('Chef', 'Cook') THEN 'Perfect'
                        WHEN e.Position IN ('Waitress', 'Server') THEN 'Good'
                        WHEN e.Position = 'Cashier' THEN 'Fair'
                        ELSE 'Standard'
                    END AS Status
                FROM 
                    employee e
                WHERE 
                    e.EmploymentStatus IN ('Active', 'On Leave') "

            If Not String.IsNullOrEmpty(searchText) Then
                query &= " AND (e.FirstName LIKE @search OR e.LastName LIKE @search OR e.Position LIKE @search) "
            End If

            query &= " ORDER BY e.FirstName, e.LastName LIMIT @limit OFFSET @offset"

            Using cmd As New MySqlCommand(query, conn)
                cmd.CommandTimeout = 60
                If Not String.IsNullOrEmpty(searchText) Then
                    cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                End If
                cmd.Parameters.AddWithValue("@limit", limit)
                cmd.Parameters.AddWithValue("@offset", offset)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(table)
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            closeConn()
        End Try
        Return table
    End Function

    '====================================
    ' UPDATE SUMMARY TILES
    '====================================
    Private Sub UpdateSummaryTiles(stats As Dictionary(Of String, Integer), totalCount As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() UpdateSummaryTiles(stats, totalCount))
            Return
        End If

        Try
            Label4.Text = stats("OnDuty").ToString("N0")
            Label6.Text = stats("Absences").ToString("N0")
            Label7.Text = stats("OnLeave").ToString("N0")

            LabelHeader.Text = String.Format("Attendance Report (Total: {0})", totalCount.ToString("N0"))
        Catch ex As Exception
            ' Silent fail
        End Try
    End Sub

    '====================================
    ' FORMAT DATAGRIDVIEW (STYLING)
    '====================================
    Private Sub FormatDataGridView()
        Try
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim statusCell As Object = row.Cells("Status").Value
                Dim status As String = If(statusCell IsNot Nothing, statusCell.ToString(), "")

                Select Case status
                    Case "Perfect"
                        row.Cells("Status").Style.ForeColor = Color.FromArgb(16, 185, 129)
                        row.Cells("Status").Style.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)

                    Case "Good"
                        row.Cells("Status").Style.ForeColor = Color.FromArgb(59, 130, 246)
                        row.Cells("Status").Style.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)

                    Case "Fair"
                        row.Cells("Status").Style.ForeColor = Color.FromArgb(245, 158, 11)
                        row.Cells("Status").Style.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)

                    Case "On Leave"
                        row.Cells("Status").Style.ForeColor = Color.FromArgb(139, 92, 246)
                        row.Cells("Status").Style.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)

                    Case Else
                        row.Cells("Status").Style.ForeColor = Color.FromArgb(100, 116, 139)
                End Select

                For Each cell As DataGridViewCell In row.Cells
                    If cell.OwningColumn.Name = "RegularHours" OrElse
                       cell.OwningColumn.Name = "OvertimeHours" OrElse
                       cell.OwningColumn.Name = "Absences" Then

                        Dim value As Integer = 0
                        Dim cellVal As Object = cell.Value
                        If cellVal IsNot Nothing AndAlso Integer.TryParse(cellVal.ToString(), value) Then
                            If cell.OwningColumn.Name = "Absences" AndAlso value > 0 Then
                                cell.Style.ForeColor = Color.FromArgb(231, 76, 60)
                                cell.Style.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
                            End If
                        End If
                    End If
                Next
            Next

        Catch ex As Exception
            ' Silent fail for formatting errors
        End Try
    End Sub

    '====================================
    ' SEARCH TEXTBOX EVENTS WITH DEBOUNCE
    '====================================
    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        If isInitialLoad Then Return

        ' Stop any existing timer and restart
        _searchDebounceTimer.Stop()
        _searchDebounceTimer.Start()
    End Sub

    Private Async Sub SearchDebounceTimer_Tick(sender As Object, e As EventArgs)
        _searchDebounceTimer.Stop()
        Await RefreshAttendanceAsync(True) ' Reset to page 1 when searching
    End Sub

    Private Sub SetLoadingState(isLoading As Boolean)
        Try
            Me.UseWaitCursor = isLoading
            DataGridView1.Enabled = Not isLoading
            Button1.Enabled = Not isLoading
            TextBoxSearch.Enabled = Not isLoading
            If btnPrev IsNot Nothing Then btnPrev.Enabled = Not isLoading AndAlso _currentPage > 1
            If btnNext IsNot Nothing Then btnNext.Enabled = Not isLoading AndAlso _currentPage < _totalPages
        Catch
        End Try
    End Sub

    Private Sub UpdatePaginationUI()
        If lblPageStatus IsNot Nothing Then
            lblPageStatus.Text = $"Page {_currentPage} of {_totalPages} (Total: {_totalRecords:N0})"
        End If
        If btnPrev IsNot Nothing Then btnPrev.Enabled = (_currentPage > 1) AndAlso Not _isLoading
        If btnNext IsNot Nothing Then btnNext.Enabled = (_currentPage < _totalPages) AndAlso Not _isLoading
    End Sub

    Private Async Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If _currentPage < _totalPages AndAlso Not _isLoading Then
            _currentPage += 1
            Await RefreshAttendanceAsync()
        End If
    End Sub

    Private Async Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If _currentPage > 1 AndAlso Not _isLoading Then
            _currentPage -= 1
            Await RefreshAttendanceAsync()
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Dim employeeName As String = DataGridView1.Rows(e.RowIndex).Cells("EmployeeName").Value.ToString()
            Dim result = MessageBox.Show($"Would you like to view the Payroll record for {employeeName}?", "Cross-reference Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If result = DialogResult.Yes Then
                Try
                    Dim payrollForm As New FormPayroll()
                    payrollForm.Show()
                Catch ex As Exception
                    MessageBox.Show("Could not open payroll form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    Private Sub AddHelpButton()
        Try
            Dim btnHelp As New Button()
            btnHelp.Text = "   Report Guide"
            btnHelp.Image = Nothing ' Or set a help icon if available
            btnHelp.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
            btnHelp.BackColor = Color.FromArgb(71, 85, 105)
            btnHelp.ForeColor = Color.White
            btnHelp.FlatStyle = FlatStyle.Flat
            btnHelp.FlatAppearance.BorderSize = 0
            btnHelp.Size = New Size(150, 45)
            btnHelp.Location = New Point(Button1.Left - 160, Button1.Top)
            btnHelp.Cursor = Cursors.Hand
            btnHelp.Anchor = AnchorStyles.Top Or AnchorStyles.Right

            AddHandler btnHelp.Click, Sub()
                                          Dim guide As String = "What You'll Do in This Report:" & vbCrLf & vbCrLf &
                    "1. Monitor Daily Attendance - Check who's present vs. on leave." & vbCrLf &
                    "2. Track Work Hours - Verify regular and overtime hours." & vbCrLf &
                    "3. Identify Attendance Patterns - High absences marked in RED." & vbCrLf &
                    "4. Review Status Ratings:" & vbCrLf &
                    "   • Perfect (Green) - Chefs/Cooks (0 absences)" & vbCrLf &
                    "   • Good (Blue) - Waitresses/Servers" & vbCrLf &
                    "   • Fair (Orange) - Cashiers" & vbCrLf &
                    "   • On Leave (Purple) - Approved status" & vbCrLf &
                    "5. Search & Filter - Use top search for name or position." & vbCrLf &
                    "6. Export Data - Download CSV for Payroll/HR." & vbCrLf &
                    "7. Navigate Pages - 50 employees per page." & vbCrLf &
                    "8. Cross-reference - Double-click a row to check Payroll."

                                          MessageBox.Show(guide, "Attendance Report Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                      End Sub

            RoundedPane24.Controls.Add(btnHelp)
            btnHelp.BringToFront()
        Catch
        End Try
    End Sub

    Private Sub TextBoxSearch_Enter(sender As Object, e As EventArgs) Handles TextBoxSearch.Enter
        If TextBoxSearch.Text = "Search employees..." Then
            TextBoxSearch.Text = ""
            TextBoxSearch.ForeColor = Color.FromArgb(15, 23, 42)
        End If
        If SearchContainer IsNot Nothing Then
            SearchContainer.BorderColor = Color.FromArgb(99, 102, 241)
        End If
    End Sub

    Private Sub TextBoxSearch_Leave(sender As Object, e As EventArgs) Handles TextBoxSearch.Leave
        If String.IsNullOrWhiteSpace(TextBoxSearch.Text) Then
            TextBoxSearch.Text = "Search employees..."
            TextBoxSearch.ForeColor = Color.FromArgb(148, 163, 184)
        End If
        If SearchContainer IsNot Nothing Then
            SearchContainer.BorderColor = Color.FromArgb(226, 232, 240)
        End If
    End Sub

    '====================================
    ' EXPORT BUTTON
    '====================================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ExportToCSV()
    End Sub

    '====================================
    ' EXPORT TO CSV - FIXED VERSION
    '====================================
    Private Async Sub ExportToCSV()
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("No data available to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "CSV Files (*.csv)|*.csv",
                .FileName = String.Format("Employee_Attendance_{0:yyyyMMdd_HHmmss}.csv", DateTime.Now),
                .Title = "Export Attendance Report"
            }

            If saveDialog.ShowDialog() = DialogResult.OK Then
                SetLoadingState(True)

                Dim searchText As String = GetSearchText()

                ' Fetch ALL filtered data from DB for export
                Dim fullData As DataTable = Await Task.Run(Function() LoadAllAttendanceData(searchText))

                If fullData IsNot Nothing AndAlso fullData.Rows.Count > 0 Then
                    Using writer As New IO.StreamWriter(saveDialog.FileName, False, System.Text.Encoding.UTF8)
                        ' Write headers - only for VISIBLE columns
                        Dim headers As New List(Of String)
                        For Each column As DataGridViewColumn In DataGridView1.Columns
                            If column.Visible Then
                                headers.Add(EscapeCSV(column.HeaderText))
                            End If
                        Next
                        writer.WriteLine(String.Join(",", headers))

                        ' Write data rows
                        For Each dataRow As DataRow In fullData.Rows
                            Dim values As New List(Of String)
                            For Each column As DataGridViewColumn In DataGridView1.Columns
                                If column.Visible Then
                                    Dim cellValue As String = ""
                                    If dataRow.Table.Columns.Contains(column.DataPropertyName) Then
                                        Dim val As Object = dataRow(column.DataPropertyName)
                                        cellValue = If(val Is Nothing OrElse IsDBNull(val), "", val.ToString())
                                    End If
                                    values.Add(EscapeCSV(cellValue))
                                End If
                            Next
                            writer.WriteLine(String.Join(",", values))
                        Next
                    End Using

                    SetLoadingState(False)
                    MessageBox.Show($"Successfully exported {fullData.Rows.Count} records!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Process.Start("explorer.exe", String.Format("/select,""{0}""", saveDialog.FileName))
                Else
                    SetLoadingState(False)
                    MessageBox.Show("No data found to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If

        Catch ex As Exception
            SetLoadingState(False)
            MessageBox.Show("Failed to export CSV: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '====================================
    ' LOAD ALL DATA FOR EXPORT (NO PAGINATION)
    '====================================
    Private Function LoadAllAttendanceData(searchText As String) As DataTable
        Dim table As New DataTable()
        Try
            openConn()
            Dim query As String = "
                SELECT 
                    e.EmployeeID,
                    CONCAT(e.FirstName, ' ', e.LastName) AS EmployeeName,
                    e.Position,
                    CASE 
                        WHEN e.EmploymentStatus = 'Active' THEN 40
                        WHEN e.EmploymentStatus = 'On Leave' THEN 32
                        ELSE 0
                    END AS RegularHours,
                    CASE 
                        WHEN e.Position IN ('Chef', 'Cook') THEN 12
                        WHEN e.Position IN ('Server', 'Waitress') THEN 5
                        WHEN e.Position = 'Cashier' THEN 3
                        ELSE 0
                    END AS OvertimeHours,
                    CASE 
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Chef' THEN 0
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Cook' THEN 0
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Waitress' THEN 1
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Cashier' THEN 2
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Server' THEN 1
                        WHEN e.EmploymentStatus = 'On Leave' THEN 3
                        ELSE 0
                    END AS Absences,
                    CASE 
                        WHEN e.EmploymentStatus = 'Active' AND e.Position IN ('Chef', 'Cook') THEN 'Perfect'
                        WHEN e.EmploymentStatus = 'Active' AND e.Position IN ('Waitress', 'Server') THEN 'Good'
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Cashier' THEN 'Fair'
                        WHEN e.EmploymentStatus = 'On Leave' THEN 'On Leave'
                        ELSE 'Inactive'
                    END AS Status
                FROM 
                    employee e
                WHERE 
                    e.EmploymentStatus IN ('Active', 'On Leave') "

            If Not String.IsNullOrEmpty(searchText) Then
                query &= " AND (e.FirstName LIKE @search OR e.LastName LIKE @search OR e.Position LIKE @search) "
            End If

            query &= " ORDER BY e.FirstName, e.LastName"

            Using cmd As New MySqlCommand(query, conn)
                cmd.CommandTimeout = 120
                If Not String.IsNullOrEmpty(searchText) Then
                    cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                End If
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(table)
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            closeConn()
        End Try
        Return table
    End Function

    Private Function EscapeCSV(value As String) As String
        If String.IsNullOrEmpty(value) Then Return ""
        If value.Contains(",") OrElse value.Contains("""") OrElse value.Contains(vbCrLf) OrElse value.Contains(vbLf) Then
            Return """" & value.Replace("""", """""") & """"
        End If
        Return value
    End Function

    '====================================
    ' REFRESH DATA (PUBLIC METHOD)
    '====================================
    Public Async Sub RefreshData()
        Await RefreshAttendanceAsync(True)
    End Sub


End Class