Imports System.IO
Imports System.Text
Imports MySql.Data.MySqlClient

Public Class ConfigurationPage
    Private ReadOnly configFolderPath As String = Path.Combine(Application.StartupPath, "Config")
    Private ReadOnly mainServerConfigPath As String = Path.Combine(Application.StartupPath, "Config", "MainServer.config")

    ' ==============================
    ' FORM LOAD
    ' ==============================
    Private Sub ConfigurationPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeConfigPath()
        LoadConfiguration()
    End Sub

    ' ==============================
    ' INITIALIZE CONFIG FOLDER
    ' ==============================
    Private Sub InitializeConfigPath()
        Try
            If Not Directory.Exists(configFolderPath) Then
                Directory.CreateDirectory(configFolderPath)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error creating config folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==============================
    ' LOAD CONFIGURATION FROM FILE
    ' ==============================
    Private Sub LoadConfiguration()
        If Not File.Exists(mainServerConfigPath) Then
            Return ' No saved config, leave fields empty
        End If

        Try
            Dim lines As String() = File.ReadAllLines(mainServerConfigPath)
            For Each line As String In lines
                If String.IsNullOrWhiteSpace(line) OrElse Not line.Contains("=") Then
                    Continue For
                End If

                Dim parts As String() = line.Split(New Char() {"="c}, 2)
                If parts.Length <> 2 Then Continue For

                Dim key As String = parts(0).Trim().ToUpper()
                Dim value As String = parts(1).Trim()

                Select Case key
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
            Next

            MessageBox.Show("Configuration loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==============================
    ' SAVE CONFIGURATION TO FILE
    ' ==============================
    Private Sub SaveConfiguration()
        If Not ValidateFields() Then
            Return
        End If

        Try
            Dim sb As New StringBuilder()
            sb.AppendLine("CONNECTION_TYPE=DATABASE")
            sb.AppendLine($"SERVER={txtServer.Text.Trim()}")
            sb.AppendLine($"PORT={txtPort.Text.Trim()}")
            sb.AppendLine($"DATABASE={txtDatabasename.Text.Trim()}")
            sb.AppendLine($"USERNAME={txtUsername.Text.Trim()}")
            sb.AppendLine($"PASSWORD={EncryptPassword(txtPassword.Text)}")
            sb.AppendLine($"SAVED_DATE={DateTime.Now:yyyy-MM-dd HH:mm:ss}")

            File.WriteAllText(mainServerConfigPath, sb.ToString())

            MessageBox.Show("Configuration saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==============================
    ' VALIDATE INPUT FIELDS
    ' ==============================
    Private Function ValidateFields() As Boolean
        ' Validate Server
        If String.IsNullOrWhiteSpace(txtServer.Text) Then
            MessageBox.Show("Please enter Server IP address or hostname.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtServer.Focus()
            Return False
        End If

        ' Validate Port
        If String.IsNullOrWhiteSpace(txtPort.Text) Then
            MessageBox.Show("Please enter Port number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPort.Focus()
            Return False
        End If

        Dim portNumber As Integer
        If Not Integer.TryParse(txtPort.Text, portNumber) Then
            MessageBox.Show("Port must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPort.Focus()
            Return False
        End If

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

    ' ==============================
    ' PASSWORD ENCRYPTION/DECRYPTION
    ' ==============================
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

    ' ==============================
    ' TEST DATABASE CONNECTION
    ' ==============================
    Private Sub TestConnection()
        If Not ValidateFields() Then
            Return
        End If

        Dim connectionString As String = BuildConnectionString()

        Try
            Me.Cursor = Cursors.WaitCursor

            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Me.Cursor = Cursors.Default

                MessageBox.Show("Connection to database successful!" & vbCrLf & vbCrLf &
                              $"Server: {txtServer.Text}" & vbCrLf &
                              $"Port: {txtPort.Text}" & vbCrLf &
                              $"Database: {txtDatabasename.Text}" & vbCrLf &
                              $"Username: {txtUsername.Text}",
                              "Connection Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using

        Catch ex As MySqlException
            Me.Cursor = Cursors.Default

            Dim errorMessage As String = "Connection failed!" & vbCrLf & vbCrLf

            Select Case ex.Number
                Case 0
                    errorMessage &= "Cannot connect to MySQL server." & vbCrLf &
                                  "Please check if MySQL is running and the server address is correct."
                Case 1045
                    errorMessage &= "Access denied." & vbCrLf &
                                  "Please check your username and password."
                Case 1049
                    errorMessage &= "Unknown database." & vbCrLf &
                                  $"Database '{txtDatabasename.Text}' does not exist."
                Case Else
                    errorMessage &= $"Error Code: {ex.Number}" & vbCrLf &
                                  $"Error: {ex.Message}"
            End Select

            MessageBox.Show(errorMessage, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==============================
    ' BUILD CONNECTION STRING
    ' ==============================
    Private Function BuildConnectionString() As String
        Return $"Server={txtServer.Text.Trim()};Port={txtPort.Text.Trim()};Database={txtDatabasename.Text.Trim()};Uid={txtUsername.Text.Trim()};Pwd={txtPassword.Text};"
    End Function

    ' ==============================
    ' BUTTON: TEST CONNECTION
    ' ==============================
    Private Sub btnTestConnection_Click(sender As Object, e As EventArgs) Handles btnTestConnection.Click
        TestConnection()
    End Sub

    ' ==============================
    ' BUTTON: SAVE AND CONTINUE
    ' ==============================
    Private Sub btnSaveAndContinue_Click(sender As Object, e As EventArgs) Handles btnSaveAndContinue.Click
        SaveConfiguration()

        If File.Exists(mainServerConfigPath) Then
            Dim loginForm As New Adminlogin()
            loginForm.Show()
            Me.Hide()
        End If
    End Sub

    ' ==============================
    ' KEYBOARD SHORTCUTS
    ' ==============================
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.S) Then
            SaveConfiguration()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.T) Then
            TestConnection()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

End Class