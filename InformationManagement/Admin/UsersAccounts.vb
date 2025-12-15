Imports System.Security.Policy
Imports MySqlConnector

Public Class UsersAccounts
    Private Sub UsersAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUsers()
        UpdateUserCounts()
    End Sub

    Public Sub LoadUsers()
        Try
            ' Suspend layout to prevent flickering and improve performance
            UsersAccountData.SuspendLayout()

            openConn()
            ' Query only employees (staff and admin)
            Dim query As String = "
                SELECT 
                    e.EmployeeID as ID,
                    e.FirstName COLLATE utf8mb4_general_ci as FirstName,
                    e.LastName COLLATE utf8mb4_general_ci as LastName,
                    e.Position COLLATE utf8mb4_general_ci as Role,
                    e.EmploymentStatus COLLATE utf8mb4_general_ci as Status,
                    e.HireDate as DateCreated
                FROM employee e
                WHERE LOWER(e.Position) LIKE '%staff%' OR LOWER(e.Position) = 'admin'
                ORDER BY DateCreated DESC"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            UsersAccountData.Rows.Clear()
            For Each row As DataRow In dt.Rows
                Dim rowIndex As Integer = UsersAccountData.Rows.Add()
                Dim newRow As DataGridViewRow = UsersAccountData.Rows(rowIndex)

                ' Combine FirstName and LastName for display
                Dim fullName As String = ""
                If row("FirstName") IsNot DBNull.Value Then
                    fullName = row("FirstName").ToString()
                End If
                If row("LastName") IsNot DBNull.Value Then
                    If fullName <> "" Then fullName &= " "
                    fullName &= row("LastName").ToString()
                End If

                newRow.Cells("txtName").Value = fullName
                newRow.Cells("colRole").Value = If(row("Role") IsNot DBNull.Value, row("Role").ToString(), "")
                newRow.Cells("colStatus").Value = If(row("Status") IsNot DBNull.Value, row("Status").ToString(), "")
                newRow.Cells("colJoinDate").Value = If(row("DateCreated") IsNot DBNull.Value, Convert.ToDateTime(row("DateCreated")).ToString("MMMM dd, yyyy"), "")

                ' Store ID and Role type for edit/delete operations
                newRow.Tag = New With {.ID = row("ID"), .Role = row("Role").ToString()}
            Next

            ' Resume layout
            UsersAccountData.ResumeLayout()

        Catch ex As Exception
            MessageBox.Show("Error loading users: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub UsersAccountData_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles UsersAccountData.CellContentClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = UsersAccountData.Rows(e.RowIndex)
            Dim username As String = If(selectedRow.Cells("txtName").Value IsNot Nothing, selectedRow.Cells("txtName").Value.ToString(), "Unknown")

            Dim userInfo As Object = selectedRow.Tag
            Dim userID As Integer = 0
            Dim userRole As String = ""

            If userInfo IsNot Nothing Then
                userID = userInfo.ID
                userRole = userInfo.Role.ToString()
            End If

            ' --- EDIT BUTTON ---
            If e.ColumnIndex = UsersAccountData.Columns("colEdit").Index Then
                ' Edit employee (Staff/Admin)
                Dim editForm As New FormEdit()
                editForm.LoadUserData(userID, username, userRole)
                editForm.StartPosition = FormStartPosition.CenterScreen

                If editForm.ShowDialog() = DialogResult.OK Then
                    ' Refresh the list after editing
                    LoadUsers()
                    UpdateUserCounts()
                End If

                ' --- DELETE BUTTON ---
            ElseIf e.ColumnIndex = UsersAccountData.Columns("colDelete").Index Then
                Dim result As DialogResult = MessageBox.Show(
                    "Are you sure you want to delete " & username & "?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                )

                If result = DialogResult.Yes Then
                    Try
                        openConn()

                        ' Delete from employee table
                        Dim cmdEmployee As New MySqlCommand("DELETE FROM employee WHERE EmployeeID = @id", conn)
                        cmdEmployee.Parameters.AddWithValue("@id", userID)
                        cmdEmployee.ExecuteNonQuery()

                        ' Also delete from user_accounts if exists
                        Dim cmdUser As New MySqlCommand("DELETE FROM user_accounts WHERE username IN (SELECT CONCAT(FirstName, LastName) FROM employee WHERE EmployeeID = @id)", conn)
                        cmdUser.Parameters.AddWithValue("@id", userID)
                        cmdUser.ExecuteNonQuery()

                        closeConn()

                        LoadUsers()
                        UpdateUserCounts()
                        MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show("Error deleting user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        closeConn()
                    End Try
                End If
            End If
        End If
    End Sub

    ' This method should be called from your employee edit form after saving
    Public Shared Sub SyncEmployeeToUserAccount(employeeID As Integer)
        Try
            Dim connLocal As MySqlConnection = Nothing
            Try
                connLocal = New MySqlConnection("your_connection_string_here") ' Use your connection string
                connLocal.Open()
            Catch
                ' If static connection method exists
                openConn()
                connLocal = conn
            End Try

            ' Get employee details
            Dim queryEmp As String = "SELECT FirstName, LastName, Email, Position FROM employee WHERE EmployeeID = @id"
            Dim cmdEmp As New MySqlCommand(queryEmp, connLocal)
            cmdEmp.Parameters.AddWithValue("@id", employeeID)
            Dim reader As MySqlDataReader = cmdEmp.ExecuteReader()

            If reader.Read() Then
                Dim firstName As String = reader("FirstName").ToString()
                Dim lastName As String = reader("LastName").ToString()
                Dim email As String = reader("Email").ToString()
                Dim position As String = reader("Position").ToString()
                reader.Close()

                ' Only create user account if position contains "staff" or is "admin"
                If position.ToLower().Contains("staff") OrElse position.ToLower() = "admin" Then
                    Dim username As String = firstName & lastName
                    Dim defaultPassword As String = "staff123" ' Default password

                    ' Check if user account already exists
                    Dim queryCheck As String = "SELECT COUNT(*) FROM user_accounts WHERE username = @username"
                    Dim cmdCheck As New MySqlCommand(queryCheck, connLocal)
                    cmdCheck.Parameters.AddWithValue("@username", username)
                    Dim exists As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                    If exists > 0 Then
                        ' Update existing user account
                        Dim queryUpdate As String = "UPDATE user_accounts SET position = @position WHERE username = @username"
                        Dim cmdUpdate As New MySqlCommand(queryUpdate, connLocal)
                        cmdUpdate.Parameters.AddWithValue("@position", position)
                        cmdUpdate.Parameters.AddWithValue("@username", username)
                        cmdUpdate.ExecuteNonQuery()
                    Else
                        ' Create new user account
                        Dim queryInsert As String = "INSERT INTO user_accounts (name, position, username, password, type, created_at) VALUES (@name, @position, @username, @password, @type, NOW())"
                        Dim cmdInsert As New MySqlCommand(queryInsert, connLocal)
                        cmdInsert.Parameters.AddWithValue("@name", firstName & " " & lastName)
                        cmdInsert.Parameters.AddWithValue("@position", position)
                        cmdInsert.Parameters.AddWithValue("@username", username)
                        cmdInsert.Parameters.AddWithValue("@password", defaultPassword)

                        ' Set type based on position
                        Dim userType As Integer = If(position.ToLower() = "admin", 1, 2) ' 1=Admin, 2=Staff
                        cmdInsert.Parameters.AddWithValue("@type", userType)

                        cmdInsert.ExecuteNonQuery()
                    End If
                End If
            Else
                reader.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error syncing employee to user account: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Try
                closeConn()
            Catch
            End Try
        End Try
    End Sub

    Private Sub SetActiveButton(activeBtn As Button)
        Dim buttons() As Button = {AllUsersbtn, Staffbtn, Employeesbtn}

        For Each btn As Button In buttons
            btn.BackColor = Color.White
            btn.ForeColor = Color.Black
            btn.FlatAppearance.BorderSize = 0
        Next

        activeBtn.BackColor = Color.FromArgb(25, 25, 35)
        activeBtn.ForeColor = Color.White
    End Sub

    Private Sub AllUsersbtn_Click(sender As Object, e As EventArgs) Handles AllUsersbtn.Click
        SetActiveButton(AllUsersbtn)
        For Each row As DataGridViewRow In UsersAccountData.Rows
            row.Visible = True
        Next
    End Sub

    Private Sub Staffbtn_Click(sender As Object, e As EventArgs) Handles Staffbtn.Click
        SetActiveButton(Staffbtn)
        For Each row As DataGridViewRow In UsersAccountData.Rows
            If row.Cells("colRole").Value IsNot Nothing Then
                Dim role As String = row.Cells("colRole").Value.ToString().ToLower()
                row.Visible = (role.Contains("staff"))
            End If
        Next
    End Sub

    Private Sub Employeesbtn_Click(sender As Object, e As EventArgs) Handles Employeesbtn.Click
        SetActiveButton(Employeesbtn)
        For Each row As DataGridViewRow In UsersAccountData.Rows
            row.Visible = True ' Show all employees (no customers to filter out)
        Next
    End Sub

    Private Sub UpdateUserCounts()
        Dim totalUsers As Integer = UsersAccountData.Rows.Count
        Dim staffCount As Integer = 0
        Dim employeeCount As Integer = 0

        For Each row As DataGridViewRow In UsersAccountData.Rows
            If Not row.IsNewRow AndAlso row.Cells("colRole").Value IsNot Nothing Then
                Dim role As String = row.Cells("colRole").Value.ToString().ToLower()

                If role.Contains("staff") Then
                    staffCount += 1
                Else
                    employeeCount += 1
                End If
            End If
        Next

        lblTotalUsers.Text = totalUsers.ToString()
        lblStaffs.Text = staffCount.ToString()
        lblEmployees.Text = employeeCount.ToString()
    End Sub

    ' Removed RowsAdded and RowsRemoved events to prevent infinite loops
    ' UpdateUserCounts is called manually after LoadUsers()

    Private Sub RoundButton(btn As Button)
        Dim radius As Integer = 12
        Dim path As New Drawing2D.GraphicsPath()
        path.StartFigure()
        path.AddArc(New Rectangle(0, 0, radius, radius), 180, 90)
        path.AddArc(New Rectangle(btn.Width - radius, 0, radius, radius), 270, 90)
        path.AddArc(New Rectangle(btn.Width - radius, btn.Height - radius, radius, radius), 0, 90)
        path.AddArc(New Rectangle(0, btn.Height - radius, radius, radius), 90, 90)
        path.CloseFigure()
        btn.Region = New Region(path)
    End Sub

    Private Sub FormDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RoundButton(AllUsersbtn)
        RoundButton(Staffbtn)
        RoundButton(Employeesbtn)
        SetActiveButton(AllUsersbtn)
        RoundButton(AddEdit)
    End Sub

    Private Sub AddEdit_Click(sender As Object, e As EventArgs) Handles AddEdit.Click
        Dim addEditForm As New FormEdit()
        addEditForm.StartPosition = FormStartPosition.CenterScreen

        If addEditForm.ShowDialog() = DialogResult.OK Then
            ' Refresh the list after adding/editing
            LoadUsers()
            UpdateUserCounts()
        End If
    End Sub

End Class