Imports System.IO
Imports System.Text
Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Tls

Public Class ConfigurationPage
    Private configFolderPath As String = Path.Combine(Application.StartupPath, "Config")
    Private mainServerConfigPath As String = ""

    Private Sub ConfigurationPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize configuration path
        InitializeConfigPath()

        ' Load existing configuration if available
        LoadConfiguration()

        ' Set default values if fields are empty
        If String.IsNullOrWhiteSpace(txtServer.Text) Then
            SetDefaultValues()
        End If
        ' Optional (for true fullscreen
    End Sub

    Private Sub InitializeConfigPath()
        ' Create Config folder if it doesn't exist
        If Not Directory.Exists(configFolderPath) Then
            Directory.CreateDirectory(configFolderPath)
        End If

        ' Set configuration file path
        mainServerConfigPath = Path.Combine(configFolderPath, "MainServer.config")
    End Sub

    Private Sub SetDefaultValues()
        ' Set default XAMPP values
        txtServer.Text = "localhost"
        txtPort.Text = "3306"
        txtUsername.Text = "root"
        txtPassword.Text = ""
        txtDatabasename.Text = ""
    End Sub

    Private Sub LoadConfiguration()
        ' Load from file if exists
        If File.Exists(mainServerConfigPath) Then
            Try
                Dim lines As String() = File.ReadAllLines(mainServerConfigPath)
                For Each line As String In lines
                    If line.Contains("=") Then
                        Dim parts As String() = line.Split("="c)
                        If parts.Length = 2 Then
                            Dim key As String = parts(0).Trim()
                            Dim value As String = parts(1).Trim()

                            Select Case key.ToUpper()
                                Case "SERVER", "IP"
                                    txtServer.Text = value
                                Case "PORT"
                                    txtPort.Text = value
                                Case "DATABASE"
                                    txtDatabasename.Text = value
                                Case "USERNAME"
                                    txtUsername.Text = value
                                Case "PASSWORD"
                                    txtPassword.Text = DecryptPassword(value)
                            End Select
                        End If
                    End If
                Next

                ' Update status label
                lblServerStatus.Text = "Configuration loaded successfully"
                lblServerStatus.ForeColor = Color.Green
            Catch ex As Exception
                MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                SetDefaultValues()
                lblServerStatus.Text = "Failed to load configuration"
                lblServerStatus.ForeColor = Color.Red
            End Try
        Else
            lblServerStatus.Text = "No saved configuration found"
            lblServerStatus.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub SaveConfiguration()
        ' Validate fields
        If Not ValidateFields() Then
            Return
        End If

        Try
            ' Build configuration content
            Dim sb As New StringBuilder()
            sb.AppendLine($"CONNECTION_TYPE=DATABASE")
            sb.AppendLine($"SERVER={txtServer.Text}")
            sb.AppendLine($"PORT={txtPort.Text}")
            sb.AppendLine($"DATABASE={txtDatabasename.Text}")
            sb.AppendLine($"USERNAME={txtUsername.Text}")
            sb.AppendLine($"PASSWORD={EncryptPassword(txtPassword.Text)}")
            sb.AppendLine($"SAVED_DATE={DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}")

            ' Save to file
            File.WriteAllText(mainServerConfigPath, sb.ToString())

            lblServerStatus.Text = "Configuration saved successfully ✓"
            lblServerStatus.ForeColor = Color.Green

            MessageBox.Show("Main Server configuration saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            lblServerStatus.Text = "Failed to save configuration"
            lblServerStatus.ForeColor = Color.Red
            MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateFields() As Boolean
        ' Validate Server IP
        If String.IsNullOrWhiteSpace(txtServer.Text) Then
            MessageBox.Show("Please enter Server IP address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtServer.Focus()
            Return False
        End If

        ' Validate Port
        If String.IsNullOrWhiteSpace(txtPort.Text) Then
            MessageBox.Show("Please enter Port number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPort.Focus()
            Return False
        End If

        ' Validate Port is numeric
        Dim portNumber As Integer
        If Not Integer.TryParse(txtPort.Text, portNumber) Then
            MessageBox.Show("Port must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPort.Focus()
            Return False
        End If

        ' Validate Port range
        If portNumber < 1 OrElse portNumber > 65535 Then
            MessageBox.Show("Port must be between 1 and 65535.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPort.Focus()
            Return False
        End If

        ' Validate Database Name
        If String.IsNullOrWhiteSpace(txtDatabasename.Text) Then
            MessageBox.Show("Please enter Database name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDatabasename.Focus()
            Return False
        End If

        ' Validate Username
        If String.IsNullOrWhiteSpace(txtUsername.Text) Then
            MessageBox.Show("Please enter Username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUsername.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub ClearFields()
        txtServer.Text = ""
        txtPort.Text = "3306"
        txtDatabasename.Text = ""
        txtUsername.Text = "root"
        txtPassword.Text = ""
        lblServerStatus.Text = ""
    End Sub

    ' Simple encryption/decryption (use stronger encryption in production)
    Private Function EncryptPassword(password As String) As String
        If String.IsNullOrEmpty(password) Then Return ""
        Try
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Return Convert.ToBase64String(bytes)
        Catch
            Return password
        End Try
    End Function

    Private Function DecryptPassword(encryptedPassword As String) As String
        If String.IsNullOrEmpty(encryptedPassword) Then Return ""
        Try
            Dim bytes As Byte() = Convert.FromBase64String(encryptedPassword)
            Return Encoding.UTF8.GetString(bytes)
        Catch
            Return encryptedPassword
        End Try
    End Function

    ' Test database connection
    Private Sub TestConnection()
        ' Validate fields first
        If Not ValidateFields() Then
            Return
        End If

        Dim connectionString As String = BuildConnectionString()

        Try
            Using conn As New MySqlConnection(connectionString)
                Me.Cursor = Cursors.WaitCursor
                lblServerStatus.Text = "Testing connection..."
                lblServerStatus.ForeColor = Color.Orange
                Application.DoEvents()

                conn.Open()

                Me.Cursor = Cursors.Default
                lblServerStatus.Text = "Connection successful ✓"
                lblServerStatus.ForeColor = Color.Green

                MessageBox.Show("Connection to Main Server successful!" & vbCrLf & vbCrLf &
                              $"Server: {txtServer.Text}" & vbCrLf &
                              $"Database: {txtDatabasename.Text}",
                              "Connection Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

                conn.Close()
            End Using
        Catch ex As MySqlException
            Me.Cursor = Cursors.Default
            lblServerStatus.Text = "Connection failed ✗"
            lblServerStatus.ForeColor = Color.Red

            MessageBox.Show($"Connection failed!" & vbCrLf & vbCrLf &
                          $"Error: {ex.Message}" & vbCrLf & vbCrLf &
                          "Please check your server settings.",
                          "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            lblServerStatus.Text = "Connection failed ✗"
            lblServerStatus.ForeColor = Color.Red

            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function BuildConnectionString() As String
        Return $"Server={txtServer.Text};Port={txtPort.Text};Database={txtDatabasename.Text};Uid={txtUsername.Text};Pwd={txtPassword.Text};"
    End Function

    ' Button Event Handlers
    Private Sub btnTestConnection_Click(sender As Object, e As EventArgs) Handles btnTestConnection.Click
        TestConnection()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs)
        SaveConfiguration()
    End Sub

    Private Sub btnSaveAndContinue_Click(sender As Object, e As EventArgs) Handles btnSaveAndContinue.Click
        SaveConfiguration()

        If File.Exists(mainServerConfigPath) Then
            Dim loginForm As New Adminlogin()
            loginForm.Show()
            Me.Hide()
            ' Prevent app from closing
        End If
    End Sub


    ' Optional: Add keyboard shortcuts
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.S) Then
            SaveConfiguration()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.T) Then
            TestConnection()
            Return True
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    ' Show configuration info in status label on focus
    Private Sub txtServer_Enter(sender As Object, e As EventArgs) Handles txtServer.Enter
        lblServerStatus.Text = "Enter the IP address or hostname of your MySQL server"
        lblServerStatus.ForeColor = Color.Gray
    End Sub

    Private Sub txtPort_Enter(sender As Object, e As EventArgs) Handles txtPort.Enter
        lblServerStatus.Text = "Default MySQL port is 3306"
        lblServerStatus.ForeColor = Color.Gray
    End Sub

    Private Sub txtDatabasename_Enter(sender As Object, e As EventArgs) Handles txtDatabasename.Enter
        lblServerStatus.Text = "Enter the name of your database"
        lblServerStatus.ForeColor = Color.Gray
    End Sub

    Private Sub txtUsername_Enter(sender As Object, e As EventArgs) Handles txtUsername.Enter
        lblServerStatus.Text = "Enter the database username (default: root)"
        lblServerStatus.ForeColor = Color.Gray
    End Sub

    Private Sub txtPassword_Enter(sender As Object, e As EventArgs) Handles txtPassword.Enter
        lblServerStatus.Text = "Enter the database password (leave empty for no password)"
        lblServerStatus.ForeColor = Color.Gray
    End Sub



End Class