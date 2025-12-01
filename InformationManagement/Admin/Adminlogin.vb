
Imports MySqlConnector   ' ✔ Correct library for your modDB module

Public Class Adminlogin

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        ' Optional
    End Sub

    Private Sub Back1_Click(sender As Object, e As EventArgs)
        ' Close the application when Back is clicked
        Application.Exit()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        ' Optional
    End Sub

    Private Sub Adminlogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize database tables
        CheckAndCreateTables()
    End Sub


    ' 🔐 ADMIN LOGIN BUTTON
    Private Sub adminlog_Click(sender As Object, e As EventArgs) Handles adminlog.Click
        ' ---- VALIDATION ----
        If txtUsername.Text.Trim() = "" Then
            MessageBox.Show("Please enter your username.", "Missing Field",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUsername.Focus()
            Exit Sub
        End If

        If txtPassword.Text.Trim() = "" Then
            MessageBox.Show("Please enter your password.", "Missing Field",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPassword.Focus()
            Exit Sub
        End If

        Dim user As String = txtUsername.Text.Trim()
        Dim pass As String = txtPassword.Text.Trim()

        ' Encrypt typed password
        Dim encryptedPass As String = Encrypt(pass)

        ' Query using original schema (lowercase column names)
        Dim query As String = "SELECT * FROM user_accounts WHERE username=@user AND password=@pass LIMIT 1"

        Try
            openConn()

            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@user", user)
            cmd.Parameters.AddWithValue("@pass", encryptedPass)

            Dim reader = cmd.ExecuteReader()

            If reader.Read() Then
                ' Store logged user using original schema columns
                CurrentLoggedUser.id = reader("id")
                CurrentLoggedUser.name = reader("name").ToString()
                CurrentLoggedUser.username = reader("username").ToString()
                CurrentLoggedUser.type = reader("type")

                reader.Close()
                conn.Close()

                Logs("Admin logged in", "Login")

                Dim dashboard As New AdminDashboard()
                dashboard.StartPosition = FormStartPosition.CenterScreen
                dashboard.WindowState = FormWindowState.Maximized
                dashboard.Show()
                Me.Hide()
            Else
                MessageBox.Show("Invalid username or password.",
                                "Login Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub
End Class
