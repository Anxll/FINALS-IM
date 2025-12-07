Imports MySqlConnector
Imports System.Data

Public Class Employee

    Private Sub Employee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEmployees()

    End Sub

    '====================================
    ' MAIN LOADER
    '====================================
    Public Sub LoadEmployees(Optional condition As String = "")
        Try
            Dim query As String =
                "SELECT EmployeeID, FirstName, LastName, Gender, DateOfBirth, ContactNumber, Email, Address, HireDate, Position, MaritalStatus, EmploymentStatus, EmploymentType, EmergencyContact, WorkShift, Salary FROM employee"

            If condition <> "" Then
                query &= " WHERE " & condition
            End If

            LoadToDGV(query, DataGridView1)

            lblTotalEmployees.Text = "Total: " & DataGridView1.Rows.Count.ToString()

        Catch ex As Exception
            MessageBox.Show("Error loading employees: " & ex.Message)
        End Try
    End Sub

    '====================================
    ' UNIVERSAL LOADER FOR DATAGRIDVIEW
    '====================================
    Private Sub LoadToDGV(query As String, dgv As DataGridView)
        Try
            openConn()

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable()

            adapter.Fill(table)
            dgv.DataSource = table

            ' ✅ HIDE EMPLOYEE ID COLUMN
            If dgv.Columns.Contains("EmployeeID") Then
                dgv.Columns("EmployeeID").Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading table: " & ex.Message)
        Finally
            closeConn()
        End Try
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
        LoadEmployees()
        lblFilter.Text = "Showing: All Employees"
    End Sub

    Private Sub btnViewActive_Click(sender As Object, e As EventArgs) Handles btnViewActive.Click
        LoadEmployees("EmploymentStatus = 'Active'")
        lblFilter.Text = "Showing: Active Employees"
    End Sub

    Private Sub btnViewInactive_Click(sender As Object, e As EventArgs) Handles btnViewInactive.Click
        LoadEmployees("EmploymentStatus = 'Resigned'")
        lblFilter.Text = "Showing: Inactive Employees"
    End Sub

    '====================================
    ' REFRESH LIST
    '====================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadEmployees()
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
            LoadEmployees()

        Catch ex As Exception
            MessageBox.Show("Error deleting employee: " & ex.Message)
        End Try
    End Sub


End Class