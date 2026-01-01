Imports MySqlConnector
Imports System.Data

Public Class Employee
    Private _isLoading As Boolean = False
    
    ' Pagination state
    Private _currentPage As Integer = 1
    Private ReadOnly _pageSize As Integer = 50
    Private _totalRecords As Integer = 0
    Private _totalPages As Integer = 0
    Private _currentCondition As String = ""

    Private Sub Employee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEmployees()

    End Sub

    '====================================
    ' MAIN LOADER
    '====================================
    Public Async Sub LoadEmployees(Optional condition As String = "", Optional searchText As String = "", Optional resetPage As Boolean = False)
        If _isLoading Then Return
        _isLoading = True
        SetLoadingState(True)

        If resetPage Then _currentPage = 1
        If condition <> "" OrElse (condition = "" AndAlso Not resetPage AndAlso _currentCondition <> "") Then
             If condition <> "" Then _currentCondition = condition
        Else
             _currentCondition = ""
        End If

        Try
            ' Get search text from UI if not provided
            If String.IsNullOrEmpty(searchText) AndAlso txtSearch IsNot Nothing Then
                searchText = txtSearch.Text.Trim()
            End If

            ' Get total count
            _totalRecords = Await Task.Run(Function() FetchTotalEmployeeCount(searchText, _currentCondition))
            _totalPages = Math.Ceiling(_totalRecords / _pageSize)
            If _totalPages = 0 Then _totalPages = 1
            If _currentPage > _totalPages Then _currentPage = _totalPages

            Dim offset As Integer = (_currentPage - 1) * _pageSize
            
            Dim query As String =
                "SELECT EmployeeID, FirstName, LastName, Gender, DateOfBirth, ContactNumber, Email, Address, HireDate, Position, MaritalStatus, EmploymentStatus, EmploymentType, EmergencyContact, WorkShift, Salary FROM employee"

            Dim finalCondition As String = ""
            If _currentCondition <> "" Then
                finalCondition = _currentCondition
            End If

            If searchText <> "" Then
                Dim searchPart As String = "(FirstName LIKE @search OR LastName LIKE @search OR Position LIKE @search)"
                If finalCondition <> "" Then
                    finalCondition &= " AND " & searchPart
                Else
                    finalCondition = searchPart
                End If
            End If

            If finalCondition <> "" Then
                query &= " WHERE " & finalCondition
            End If

            query &= " ORDER BY FirstName, LastName LIMIT @limit OFFSET @offset"

            Await Task.Run(Sub() LoadToDGV(query, DataGridView1, searchText, offset, _pageSize))

            lblTotalEmployees.Text = "Total: " & _totalRecords.ToString("N0")
            UpdatePaginationUI()

        Catch ex As Exception
            If Not Me.IsDisposed Then
                MessageBox.Show("Error loading employees: " & ex.Message)
            End If
        Finally
            If Not Me.IsDisposed Then
                SetLoadingState(False)
                _isLoading = False
            End If
        End Try
    End Sub

    Private Function FetchTotalEmployeeCount(searchText As String, condition As String) As Integer
        Dim query As String = "SELECT COUNT(*) FROM employee"
        Dim finalCondition As String = condition
        
        If searchText <> "" Then
            Dim searchPart As String = "(FirstName LIKE @search OR LastName LIKE @search OR Position LIKE @search)"
            If finalCondition <> "" Then
                finalCondition &= " AND " & searchPart
            Else
                finalCondition = searchPart
            End If
        End If

        If finalCondition <> "" Then
            query &= " WHERE " & finalCondition
        End If

        Try
            openConn()
            Using cmd As New MySqlCommand(query, conn)
                If searchText <> "" Then
                    cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                End If
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        Finally
            closeConn()
        End Try
    End Function

    '====================================
    ' UNIVERSAL LOADER FOR DATAGRIDVIEW
    '====================================
    Private Sub LoadToDGV(query As String, dgv As DataGridView, searchText As String, offset As Integer, limit As Integer)
        Try
            openConn()

            Using cmd As New MySqlCommand(query, conn)
                If searchText <> "" Then
                    cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                End If
                cmd.Parameters.AddWithValue("@limit", limit)
                cmd.Parameters.AddWithValue("@offset", offset)
                
                Using adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)
                    
                    Me.Invoke(Sub()
                                  dgv.DataSource = table
                                  ' ✅ HIDE EMPLOYEE ID COLUMN
                                  If dgv.Columns.Contains("EmployeeID") Then
                                      dgv.Columns("EmployeeID").Visible = False
                                  End If
                              End Sub)
                End Using
            End Using

        Catch ex As Exception
            If Not Me.IsDisposed Then
                Me.Invoke(Sub() MessageBox.Show("Error loading table: " & ex.Message))
            End If
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub SetLoadingState(isLoading As Boolean)
        Try
            Me.UseWaitCursor = isLoading
            DataGridView1.Enabled = Not isLoading
            btnRefresh.Enabled = Not isLoading
            AddEmployee.Enabled = Not isLoading
            EditEmployee.Enabled = Not isLoading
            btnDelete.Enabled = Not isLoading
            If btnPrev IsNot Nothing Then btnPrev.Enabled = Not isLoading AndAlso _currentPage > 1
            If btnNext IsNot Nothing Then btnNext.Enabled = Not isLoading AndAlso _currentPage < _totalPages
        Catch
        End Try
    End Sub

    Private Sub UpdatePaginationUI()
        If lblPageStatus IsNot Nothing Then
            lblPageStatus.Text = $"Page {_currentPage} of {_totalPages}"
        End If
        If btnPrev IsNot Nothing Then btnPrev.Enabled = (_currentPage > 1)
        If btnNext IsNot Nothing Then btnNext.Enabled = (_currentPage < _totalPages)
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If _currentPage < _totalPages Then
            _currentPage += 1
            LoadEmployees()
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If _currentPage > 1 Then
            _currentPage -= 1
            LoadEmployees()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadEmployees(resetPage:=True)
    End Sub

    '====================================
    ' ADD EMPLOYEE
    '====================================
    Private Sub AddEmployee_Click(sender As Object, e As EventArgs) Handles AddEmployee.Click
        Dim frm As New AddEmployee()

        frm.StartPosition = FormStartPosition.CenterScreen
        frm.Show()
        frm.BringToFront()
    End Sub

    '====================================
    ' EDIT EMPLOYEE
    '====================================
    Private Sub EditEmployee_Click(sender As Object, e As EventArgs) Handles EditEmployee.Click

        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Select an employee to edit.")
            Exit Sub
        End If

        Dim empID As Integer = DataGridView1.SelectedRows(0).Cells("EmployeeID").Value

        Dim frm As New EditEmployee()
        frm.EmployeeIDValue = empID     ' pass ID to edit form
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.Show()
        frm.BringToFront()

    End Sub

    '====================================
    ' FILTER BUTTONS
    '====================================
    Private Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        LoadEmployees(condition:="", resetPage:=True)
        lblFilter.Text = "Showing: All Employees"
    End Sub

    Private Sub btnViewActive_Click(sender As Object, e As EventArgs) Handles btnViewActive.Click
        LoadEmployees(condition:="EmploymentStatus = 'Active'", resetPage:=True)
        lblFilter.Text = "Showing: Active Employees"
    End Sub

    Private Sub btnViewInactive_Click(sender As Object, e As EventArgs) Handles btnViewInactive.Click
        LoadEmployees(condition:="EmploymentStatus = 'Resigned'", resetPage:=True)
        lblFilter.Text = "Showing: Inactive Employees"
    End Sub

    '====================================
    ' REFRESH LIST
    '====================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadEmployees(resetPage:=True)
        lblFilter.Text = "Showing: All Employees"
    End Sub

    '====================================
    ' DELETE EMPLOYEE
    '====================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Select an employee to delete.")
            Exit Sub
        End If

        Dim empID As Integer = DataGridView1.SelectedRows(0).Cells("EmployeeID").Value

        If MessageBox.Show("Delete Employee #" & empID & "?",
                           "Confirm Deletion",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning) = DialogResult.No Then Exit Sub

        Try
            openConn()

            Dim cmd As New MySqlCommand("DELETE FROM employee WHERE EmployeeID=@id", conn)
            cmd.Parameters.AddWithValue("@id", empID)
            cmd.ExecuteNonQuery()

            closeConn()

            MessageBox.Show("Employee deleted successfully.")

            ' Log Activity
            ActivityLogger.LogUserActivity(
                action:="Delete",
                actionCategory:="User Management",
                description:=$"Deleted Employee ID: {empID}",
                sourceSystem:="Admin Panel",
                referenceID:=empID.ToString(),
                referenceTable:="employee",
                oldValue:="Active",
                newValue:="Deleted"
            )
            LoadEmployees(resetPage:=True)

        Catch ex As Exception
            MessageBox.Show("Error deleting employee: " & ex.Message)
        End Try
    End Sub


End Class