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
        ' Disable Update Status button initially
        btnUpdateStatus.Enabled = False
        
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
        lblFilter.Text = "Showing: Resigned Employees"
    End Sub

    Private Sub btnViewOnLeave_Click(sender As Object, e As EventArgs) Handles btnViewOnLeave.Click
        LoadEmployees(condition:="EmploymentStatus = 'On Leave'", resetPage:=True)
        lblFilter.Text = "Showing: Employees On Leave"
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

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        ' Enable/disable Update Status button based on selection
        If DataGridView1.SelectedRows.Count > 0 Then
            btnUpdateStatus.Enabled = True
        Else
            btnUpdateStatus.Enabled = False
        End If
    End Sub

    Private Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        ' Validate selection
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an employee to update status.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim employeeId As Integer = DataGridView1.SelectedRows(0).Cells("EmployeeID").Value
        Dim employeeName As String = DataGridView1.SelectedRows(0).Cells("FirstName").Value.ToString() & " " & DataGridView1.SelectedRows(0).Cells("LastName").Value.ToString()
        Dim currentStatus As String = DataGridView1.SelectedRows(0).Cells("EmploymentStatus").Value?.ToString()

        ' Show status selection dialog
        Dim statusForm As New Form()
        statusForm.Text = "Update Employment Status"
        statusForm.Size = New Size(400, 250)
        statusForm.StartPosition = FormStartPosition.CenterParent
        statusForm.FormBorderStyle = FormBorderStyle.FixedDialog
        statusForm.MaximizeBox = False
        statusForm.MinimizeBox = False

        Dim lblInfo As New Label()
        lblInfo.Text = $"Employee: {employeeName}{Environment.NewLine}Current Status: {currentStatus}{Environment.NewLine}{Environment.NewLine}Select new status:"
        lblInfo.Location = New Point(20, 20)
        lblInfo.Size = New Size(350, 60)
        lblInfo.Font = New Font("Segoe UI", 10)

        Dim cmbStatus As New ComboBox()
        cmbStatus.Items.AddRange(New Object() {"Active", "On Leave", "Resigned"})
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStatus.Location = New Point(20, 90)
        cmbStatus.Size = New Size(340, 30)
        cmbStatus.Font = New Font("Segoe UI", 11)

        ' Set current status
        Dim index As Integer = cmbStatus.FindStringExact(currentStatus)
        If index >= 0 Then
            cmbStatus.SelectedIndex = index
        Else
            cmbStatus.SelectedIndex = 0
        End If

        Dim btnOK As New Button()
        btnOK.Text = "Update"
        btnOK.Location = New Point(180, 140)
        btnOK.Size = New Size(90, 35)
        btnOK.DialogResult = DialogResult.OK
        btnOK.BackColor = Color.FromArgb(245, 158, 11)
        btnOK.ForeColor = Color.White
        btnOK.FlatStyle = FlatStyle.Flat

        Dim btnCancel As New Button()
        btnCancel.Text = "Cancel"
        btnCancel.Location = New Point(280, 140)
        btnCancel.Size = New Size(90, 35)
        btnCancel.DialogResult = DialogResult.Cancel
        btnCancel.BackColor = Color.Gray
        btnCancel.ForeColor = Color.White
        btnCancel.FlatStyle = FlatStyle.Flat

        statusForm.Controls.AddRange(New Control() {lblInfo, cmbStatus, btnOK, btnCancel})
        statusForm.AcceptButton = btnOK
        statusForm.CancelButton = btnCancel

        If statusForm.ShowDialog() = DialogResult.Cancel Then
            Return
        End If

        Dim newStatus As String = cmbStatus.SelectedItem?.ToString()
        If String.IsNullOrEmpty(newStatus) Then
            MessageBox.Show("Please select a status.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check if status is the same
        If currentStatus = newStatus Then
            MessageBox.Show($"Employee is already {currentStatus}. No changes made.", "No Changes", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            ' Update database
            openConn()
            Dim query As String = "UPDATE employee SET EmploymentStatus = @status WHERE EmployeeID = @id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@status", newStatus)
                cmd.Parameters.AddWithValue("@id", employeeId)
                cmd.ExecuteNonQuery()
            End Using
            closeConn()

            MessageBox.Show($"Employment status updated successfully to '{newStatus}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Log Activity
            ActivityLogger.LogUserActivity(
                action:="Employment Status Updated",
                actionCategory:="User Management",
                description:=$"Changed employment status for {employeeName} (ID: {employeeId}) from '{currentStatus}' to '{newStatus}'",
                sourceSystem:="Admin Panel",
                referenceID:=employeeId.ToString(),
                referenceTable:="employee",
                oldValue:=currentStatus,
                newValue:=newStatus
            )

            ' Refresh the grid
            LoadEmployees(resetPage:=False)

        Catch ex As Exception
            MessageBox.Show("Error updating employment status: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

End Class