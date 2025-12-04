Imports System.Drawing.Drawing2D
Imports System.Net.Mime.MediaTypeNames
Imports MySqlConnector

Public Class FormAddUser

    Private Sub FormAddUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        ' Validate all required fields
        If txtFullName.Text.Trim() = "" Then
            MessageBox.Show("Please enter username/full name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFullName.Focus()
            Return
        End If

        ' txtPhone is used for Password based on designer analysis
        If txtPhone.Text.Trim() = "" Then
            MessageBox.Show("Please enter password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPhone.Focus()
            Return
        End If

        If cmbRole.SelectedIndex = -1 Then
            MessageBox.Show("Please select a role.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbRole.Focus()
            Return
        End If

        If cmbStatus.SelectedIndex = -1 Then
            MessageBox.Show("Please select a status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbStatus.Focus()
            Return
        End If

        Try
            openConn()

            ' Check if username exists in user_accounts
            Dim checkSql As String = "SELECT COUNT(*) FROM user_accounts WHERE username = @user"
            Dim checkCmd As New MySqlCommand(checkSql, conn)
            checkCmd.Parameters.AddWithValue("@user", txtFullName.Text.Trim())
            Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

            If count > 0 Then
                MessageBox.Show("Username already exists. Please choose another.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                closeConn()
                Return
            End If

            Dim selectedRole As String = cmbRole.SelectedItem.ToString().ToLower()
            Dim selectedStatus As String = cmbStatus.SelectedItem.ToString().Trim()
            Dim encryptedPassword As String = Encrypt(txtPhone.Text.Trim())
            
            ' Parse name into FirstName and LastName
            Dim nameParts As String() = txtFullName.Text.Trim().Split(" "c)
            Dim firstName As String = nameParts(0)
            Dim lastName As String = If(nameParts.Length > 1, String.Join(" ", nameParts.Skip(1)), "")

            ' Determine role type for user_accounts table
            Dim userType As Integer = 0
            If selectedRole.Contains("admin") OrElse selectedRole.Contains("staff") Then
                userType = 1 ' Admin/Staff
            Else
                userType = 2 ' Employee/Customer
            End If

            ' Insert into user_accounts table
            Dim userQuery As String = "INSERT INTO user_accounts (name, username, password, type, position, created_at) VALUES (@name, @user, @pass, @type, @position, NOW())"
            Dim userCmd As New MySqlCommand(userQuery, conn)
            userCmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim())
            userCmd.Parameters.AddWithValue("@user", txtFullName.Text.Trim())
            userCmd.Parameters.AddWithValue("@pass", encryptedPassword)
            userCmd.Parameters.AddWithValue("@type", userType)
            userCmd.Parameters.AddWithValue("@position", cmbRole.SelectedItem.ToString())
            userCmd.ExecuteNonQuery()

            ' Insert into appropriate table based on role
            If selectedRole.Contains("customer") Then
                ' Insert into customers table
                Dim custQuery As String = "INSERT INTO customers (FirstName, LastName, Email, PasswordHash, CustomerType, AccountStatus, CreatedDate) VALUES (@fname, @lname, @email, @pass, 'Online', @status, NOW())"
                Dim custCmd As New MySqlCommand(custQuery, conn)
                custCmd.Parameters.AddWithValue("@fname", firstName)
                custCmd.Parameters.AddWithValue("@lname", lastName)
                custCmd.Parameters.AddWithValue("@email", txtFullName.Text.Trim().Replace(" ", "").ToLower() & "@customer.com") ' Generate email
                custCmd.Parameters.AddWithValue("@pass", encryptedPassword)
                custCmd.Parameters.AddWithValue("@status", selectedStatus)
                custCmd.ExecuteNonQuery()

            ElseIf selectedRole.Contains("employee") Then
                ' Insert into employee table
                Dim empQuery As String = "INSERT INTO employee (FirstName, LastName, Email, Position, HireDate, EmploymentStatus, EmploymentType, Salary) VALUES (@fname, @lname, @email, @position, NOW(), @status, 'Full-time', 0)"
                Dim empCmd As New MySqlCommand(empQuery, conn)
                empCmd.Parameters.AddWithValue("@fname", firstName)
                empCmd.Parameters.AddWithValue("@lname", lastName)
                empCmd.Parameters.AddWithValue("@email", txtFullName.Text.Trim().Replace(" ", "").ToLower() & "@company.com")
                empCmd.Parameters.AddWithValue("@position", cmbRole.SelectedItem.ToString())
                empCmd.Parameters.AddWithValue("@status", selectedStatus)
                empCmd.ExecuteNonQuery()
            End If

            closeConn()

            MessageBox.Show("User added successfully!" & vbCrLf & "Data saved to user_accounts and " & If(selectedRole.Contains("customer"), "customers", If(selectedRole.Contains("employee"), "employee", "user_accounts only")) & " table.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh Grid - Use fully qualified name to avoid namespace conflict
            If System.Windows.Forms.Application.OpenForms().OfType(Of UsersAccounts).Any() Then
                System.Windows.Forms.Application.OpenForms().OfType(Of UsersAccounts)().First().LoadUsers()
            End If

            ' Clear fields
            txtFullName.Text = ""
            txtPhone.Text = ""
            cmbRole.SelectedIndex = -1
            cmbStatus.SelectedIndex = -1

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error adding user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ComboBox_DrawItem(sender As Object, e As DrawItemEventArgs) _
        Handles cmbRole.DrawItem, cmbStatus.DrawItem

        If e.Index < 0 Then Return
        Dim cmb As ComboBox = DirectCast(sender, ComboBox)
        e.DrawBackground()
        e.Graphics.DrawString(cmb.Items(e.Index).ToString(), cmb.Font, Brushes.Black, e.Bounds)
        e.DrawFocusRectangle()
    End Sub

    Private Sub FormAddUser_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close()
    End Sub



End Class