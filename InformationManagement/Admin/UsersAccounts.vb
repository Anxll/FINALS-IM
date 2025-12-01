Imports System.Security.Policy
Imports MySqlConnector

Public Class UsersAccounts
    Private Sub UsersAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUsers()
        UpdateUserCounts()
    End Sub

    Public Sub LoadUsers()
        Try
            openConn()
            ' UNION query with FirstName, LastName separate and proper date fields
            Dim query As String = "
                SELECT 
                    CustomerID as ID,
                    FirstName COLLATE utf8mb4_general_ci as FirstName,
                    LastName COLLATE utf8mb4_general_ci as LastName,
                    'Customer' COLLATE utf8mb4_general_ci as Role,
                    AccountStatus COLLATE utf8mb4_general_ci as Status,
                    CreatedDate as DateCreated
                FROM customers
                UNION ALL
                SELECT 
                    EmployeeID as ID,
                    FirstName COLLATE utf8mb4_general_ci as FirstName,
                    LastName COLLATE utf8mb4_general_ci as LastName,
                    Position COLLATE utf8mb4_general_ci as Role,
                    EmploymentStatus COLLATE utf8mb4_general_ci as Status,
                    HireDate as DateCreated
                FROM employee
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
                
                ' Use correct column names from Designer: txtName and colJoinDate
                newRow.Cells("txtName").Value = fullName
                newRow.Cells("colRole").Value = If(row("Role") IsNot DBNull.Value, row("Role").ToString(), "")
                newRow.Cells("colStatus").Value = If(row("Status") IsNot DBNull.Value, row("Status").ToString(), "")
                newRow.Cells("colJoinDate").Value = If(row("DateCreated") IsNot DBNull.Value, Convert.ToDateTime(row("DateCreated")).ToString("MMMM dd, yyyy"), "")
                
                ' Store ID and Role type for edit/delete operations
                newRow.Tag = New With {.ID = row("ID"), .Role = row("Role").ToString()}
            Next

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
                MessageBox.Show("Edit functionality coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

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
                        Dim query As String = ""
                        
                        ' Determine which table to delete from based on role
                        If userRole.ToLower() = "customer" Then
                            query = "DELETE FROM customers WHERE CustomerID = @id"
                        Else
                            query = "DELETE FROM employee WHERE EmployeeID = @id"
                        End If
                        
                        Dim cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@id", userID)
                        cmd.ExecuteNonQuery()
                        closeConn()
                        
                        LoadUsers()
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

    Private Sub SetActiveButton(activeBtn As Button)
        Dim buttons() As Button = {AllUsersbtn, Staffbtn, Employeesbtn, Customerbtn}

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
            If row.Cells("colRole").Value IsNot Nothing Then
                Dim role As String = row.Cells("colRole").Value.ToString().ToLower()
                row.Visible = (Not role = "customer")
            End If
        Next
    End Sub

    Private Sub Customerbtn_Click(sender As Object, e As EventArgs) Handles Customerbtn.Click
        SetActiveButton(Customerbtn)
        For Each row As DataGridViewRow In UsersAccountData.Rows
            If row.Cells("colRole").Value IsNot Nothing Then
                Dim role As String = row.Cells("colRole").Value.ToString().ToLower()
                row.Visible = (role = "customer")
            End If
        Next
    End Sub

    Private Sub UpdateUserCounts()
        Dim totalUsers As Integer = UsersAccountData.Rows.Count
        Dim staffCount As Integer = 0
        Dim employeeCount As Integer = 0
        Dim customerCount As Integer = 0

        For Each row As DataGridViewRow In UsersAccountData.Rows
            If Not row.IsNewRow AndAlso row.Cells("colRole").Value IsNot Nothing Then
                Dim role As String = row.Cells("colRole").Value.ToString().ToLower()

                If role.Contains("staff") Then
                    staffCount += 1
                ElseIf role = "customer" Then
                    customerCount += 1
                Else
                    employeeCount += 1
                End If
            End If
        Next

        lblTotalUsers.Text = totalUsers.ToString()
        lblStaffs.Text = staffCount.ToString()
        lblEmployees.Text = employeeCount.ToString()
        lblCustomers.Text = customerCount.ToString()
    End Sub

    Private Sub UsersAccountData_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles UsersAccountData.RowsAdded
        UpdateUserCounts()
    End Sub

    Private Sub UsersAccountData_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles UsersAccountData.RowsRemoved
        UpdateUserCounts()
    End Sub

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
        RoundButton(Customerbtn)
        SetActiveButton(AllUsersbtn)
        RoundButton(Adduserbtn)
    End Sub

    Private Sub Adduserbtn_Click(sender As Object, e As EventArgs) Handles Adduserbtn.Click
        With FormAddUser
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            .BringToFront()
        End With
    End Sub

End Class