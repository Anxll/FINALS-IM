Imports System.Drawing.Drawing2D
Imports MySqlConnector

Public Class FormAddUser

    Private Sub FormAddUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Only show Staff as an option since we're only managing staff
        cmbRole.Items.Clear()
        cmbRole.Items.Add("Staff")
        cmbRole.Items.Add("Senior Staff")
        cmbRole.Items.Add("Junior Staff")
        cmbRole.Items.Add("Administrative Staff")
        cmbRole.SelectedIndex = 0
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
            Dim tx As MySqlTransaction = conn.BeginTransaction()

            ' Check if username exists in user_accounts
            Dim checkSql As String = "SELECT COUNT(*) FROM user_accounts WHERE username = @user"
            Dim checkCmd As New MySqlCommand(checkSql, conn, tx)
            checkCmd.Parameters.AddWithValue("@user", txtFullName.Text.Trim())
            Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

            If count > 0 Then
                MessageBox.Show("Username already exists. Please choose another.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tx.Rollback()
                closeConn()
                Return
            End If

            Dim selectedRole As String = cmbRole.SelectedItem.ToString()
            Dim selectedStatus As String = cmbStatus.SelectedItem.ToString().Trim()
            Dim encryptedPassword As String = Encrypt(txtPhone.Text.Trim())

            ' Parse name into FirstName and LastName
            Dim nameParts As String() = txtFullName.Text.Trim().Split(" "c)
            Dim firstName As String = nameParts(0)
            Dim lastName As String = If(nameParts.Length > 1, String.Join(" ", nameParts.Skip(1)), "")

            ' Insert into employee table (all staff are employees)
            Dim empQuery As String = "INSERT INTO employee (FirstName, LastName, Email, Position, HireDate, EmploymentStatus, EmploymentType, Salary) VALUES (@fname, @lname, @email, @position, NOW(), @status, 'Full-time', 0)"
            Dim empCmd As New MySqlCommand(empQuery, conn, tx)
            empCmd.Parameters.AddWithValue("@fname", firstName)
            empCmd.Parameters.AddWithValue("@lname", lastName)
            empCmd.Parameters.AddWithValue("@email", txtFullName.Text.Trim().Replace(" ", "").ToLower() & "@company.com")
            empCmd.Parameters.AddWithValue("@position", selectedRole)
            empCmd.Parameters.AddWithValue("@status", selectedStatus)
            empCmd.ExecuteNonQuery()
            Dim newEmployeeId As Integer = CInt(empCmd.LastInsertedId)

            ' Ensure employee_id column exists (older DB)
            Try
                Dim colCheckSql As String = "SELECT COUNT(*) FROM information_schema.COLUMNS " &
                                            "WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'user_accounts' AND COLUMN_NAME = 'employee_id'"
                Using colCheckCmd As New MySqlCommand(colCheckSql, conn, tx)
                    Dim colCount As Integer = Convert.ToInt32(colCheckCmd.ExecuteScalar())
                    If colCount = 0 Then
                        Using alterCmd As New MySqlCommand("ALTER TABLE user_accounts ADD COLUMN employee_id INT NULL", conn, tx)
                            alterCmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            Catch
                ' ignore
            End Try

            ' Insert into user_accounts table (Staff = type 1) and LINK employee_id
            Dim userQuery As String = "INSERT INTO user_accounts (employee_id, name, username, password, type, position, created_at) " &
                                      "VALUES (@eid, @name, @user, @pass, 1, @position, NOW())"
            Dim userCmd As New MySqlCommand(userQuery, conn, tx)
            userCmd.Parameters.AddWithValue("@eid", newEmployeeId)
            userCmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim())
            userCmd.Parameters.AddWithValue("@user", txtFullName.Text.Trim())
            userCmd.Parameters.AddWithValue("@pass", encryptedPassword)
            userCmd.Parameters.AddWithValue("@position", selectedRole)
            userCmd.ExecuteNonQuery()

            tx.Commit()

            closeConn()

            MessageBox.Show("Staff member added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh Grid using the LoadUsers method
            For Each frm As Form In System.Windows.Forms.Application.OpenForms
                If TypeOf frm Is UsersAccounts Then
                    DirectCast(frm, UsersAccounts).LoadUsers()
                    Exit For
                End If
            Next

            ' Clear fields
            txtFullName.Text = ""
            txtPhone.Text = ""
            cmbRole.SelectedIndex = 0
            cmbStatus.SelectedIndex = -1

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error adding staff member: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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