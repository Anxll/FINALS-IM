Imports System.IO
Imports System.Text
Imports MySqlConnector

Public Class DatabaseConfigManager
    ' Configuration paths
    Private Shared ReadOnly ConfigFolder As String = Path.Combine(Application.StartupPath, "Config")
    Private Shared ReadOnly MainServerConfig As String = Path.Combine(ConfigFolder, "MainServer.config")
    Private Shared ReadOnly ClientServerConfig As String = Path.Combine(ConfigFolder, "ClientServer.config")
    Private Shared ReadOnly CustomerServerConfig As String = Path.Combine(ConfigFolder, "CustomerServer.config")

    ' Server configuration structure
    Public Structure ServerConfig
        Public ServerIP As String
        Public Port As String
        Public DatabaseName As String
        Public Username As String
        Public Password As String
        Public SavedDate As DateTime

        Public Function IsValid() As Boolean
            Return Not String.IsNullOrWhiteSpace(ServerIP) AndAlso
                   Not String.IsNullOrWhiteSpace(Port) AndAlso
                   Not String.IsNullOrWhiteSpace(DatabaseName) AndAlso
                   Not String.IsNullOrWhiteSpace(Username)
        End Function

        Public Function GetConnectionString() As String
            Return $"Server={ServerIP};Port={Port};Database={DatabaseName};Uid={Username};Pwd={Password};"
        End Function
    End Structure

    ' Check if all configurations exist
    Public Shared Function AreAllConfigurationsSet() As Boolean
        Return File.Exists(MainServerConfig) AndAlso
               File.Exists(ClientServerConfig) AndAlso
               File.Exists(CustomerServerConfig)
    End Function

    ' Get Main Server Configuration
    Public Shared Function GetMainServerConfig() As ServerConfig
        Return LoadConfigFromFile(MainServerConfig)
    End Function

    ' Get Client Server Configuration
    Public Shared Function GetClientServerConfig() As ServerConfig
        Return LoadConfigFromFile(ClientServerConfig)
    End Function

    ' Get Customer Server Configuration
    Public Shared Function GetCustomerServerConfig() As ServerConfig
        Return LoadConfigFromFile(CustomerServerConfig)
    End Function

    ' Get connection string for Main Server
    Public Shared Function GetMainServerConnectionString() As String
        Dim config As ServerConfig = GetMainServerConfig()
        If Not config.IsValid() Then
            Throw New Exception("Main Server configuration is not valid or missing.")
        End If
        Return config.GetConnectionString()
    End Function

    ' Get connection string for Client Server
    Public Shared Function GetClientServerConnectionString() As String
        Dim config As ServerConfig = GetClientServerConfig()
        If Not config.IsValid() Then
            Throw New Exception("Client Server configuration is not valid or missing.")
        End If
        Return config.GetConnectionString()
    End Function

    ' Get connection string for Customer Server
    Public Shared Function GetCustomerServerConnectionString() As String
        Dim config As ServerConfig = GetCustomerServerConfig()
        If Not config.IsValid() Then
            Throw New Exception("Customer Server configuration is not valid or missing.")
        End If
        Return config.GetConnectionString()
    End Function

    ' Load configuration from file
    Private Shared Function LoadConfigFromFile(filePath As String) As ServerConfig
        Dim config As New ServerConfig()

        If Not File.Exists(filePath) Then
            Return config
        End If

        Try
            Dim lines As String() = File.ReadAllLines(filePath)
            For Each line As String In lines
                If line.Contains("=") Then
                    Dim parts As String() = line.Split("="c)
                    If parts.Length = 2 Then
                        Dim key As String = parts(0).Trim().ToUpper()
                        Dim value As String = parts(1).Trim()

                        Select Case key
                            Case "SERVER", "IP"
                                config.ServerIP = value
                            Case "PORT"
                                config.Port = value
                            Case "DATABASE"
                                config.DatabaseName = value
                            Case "USERNAME"
                                config.Username = value
                            Case "PASSWORD"
                                config.Password = DecryptPassword(value)
                            Case "SAVED_DATE"
                                DateTime.TryParse(value, config.SavedDate)
                        End Select
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception($"Error loading configuration from {Path.GetFileName(filePath)}: {ex.Message}")
        End Try

        Return config
    End Function

    ' Save configuration to file
    Public Shared Sub SaveConfigToFile(filePath As String, config As ServerConfig)
        Try
            ' Ensure config folder exists
            If Not Directory.Exists(ConfigFolder) Then
                Directory.CreateDirectory(ConfigFolder)
            End If

            Dim sb As New StringBuilder()
            sb.AppendLine($"SERVER={config.ServerIP}")
            sb.AppendLine($"PORT={config.Port}")
            sb.AppendLine($"DATABASE={config.DatabaseName}")
            sb.AppendLine($"USERNAME={config.Username}")
            sb.AppendLine($"PASSWORD={EncryptPassword(config.Password)}")
            sb.AppendLine($"SAVED_DATE={DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}")

            File.WriteAllText(filePath, sb.ToString())
        Catch ex As Exception
            Throw New Exception($"Error saving configuration to {Path.GetFileName(filePath)}: {ex.Message}")
        End Try
    End Sub

    ' Test connection with configuration
    Public Shared Function TestConnection(config As ServerConfig, ByRef errorMessage As String) As Boolean
        Try
            Using conn As New MySqlConnection(config.GetConnectionString())
                conn.Open()
                conn.Close()
                errorMessage = ""
                Return True
            End Using
        Catch ex As MySqlException
            errorMessage = $"MySQL Error: {ex.Message}"
            Return False
        Catch ex As Exception
            errorMessage = $"Connection Error: {ex.Message}"
            Return False
        End Try
    End Function

    ' Simple password encryption (use stronger encryption in production)
    Private Shared Function EncryptPassword(password As String) As String
        If String.IsNullOrEmpty(password) Then Return ""
        Try
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Return Convert.ToBase64String(bytes)
        Catch
            Return password
        End Try
    End Function

    ' Simple password decryption
    Private Shared Function DecryptPassword(encryptedPassword As String) As String
        If String.IsNullOrEmpty(encryptedPassword) Then Return ""
        Try
            Dim bytes As Byte() = Convert.FromBase64String(encryptedPassword)
            Return Encoding.UTF8.GetString(bytes)
        Catch
            Return encryptedPassword
        End Try
    End Function

    ' Get database connection for specific server
    Public Shared Function GetMainServerConnection() As MySqlConnection
        Return New MySqlConnection(GetMainServerConnectionString())
    End Function

    Public Shared Function GetClientServerConnection() As MySqlConnection
        Return New MySqlConnection(GetClientServerConnectionString())
    End Function

    Public Shared Function GetCustomerServerConnection() As MySqlConnection
        Return New MySqlConnection(GetCustomerServerConnectionString())
    End Function

    ' Check if specific configuration exists
    Public Shared Function MainServerConfigExists() As Boolean
        Return File.Exists(MainServerConfig)
    End Function

    Public Shared Function ClientServerConfigExists() As Boolean
        Return File.Exists(ClientServerConfig)
    End Function

    Public Shared Function CustomerServerConfigExists() As Boolean
        Return File.Exists(CustomerServerConfig)
    End Function

    ' Delete configuration files (for testing or reset)
    Public Shared Sub DeleteAllConfigurations()
        Try
            If File.Exists(MainServerConfig) Then File.Delete(MainServerConfig)
            If File.Exists(ClientServerConfig) Then File.Delete(ClientServerConfig)
            If File.Exists(CustomerServerConfig) Then File.Delete(CustomerServerConfig)
        Catch ex As Exception
            Throw New Exception($"Error deleting configurations: {ex.Message}")
        End Try
    End Sub

    ' Export configuration to string (for backup)
    Public Shared Function ExportConfiguration() As String
        Dim sb As New StringBuilder()
        sb.AppendLine("=== Main Server Configuration ===")
        If File.Exists(MainServerConfig) Then
            sb.AppendLine(File.ReadAllText(MainServerConfig))
        Else
            sb.AppendLine("Not configured")
        End If
        sb.AppendLine()

        sb.AppendLine("=== Client Server Configuration ===")
        If File.Exists(ClientServerConfig) Then
            sb.AppendLine(File.ReadAllText(ClientServerConfig))
        Else
            sb.AppendLine("Not configured")
        End If
        sb.AppendLine()

        sb.AppendLine("=== Customer Server Configuration ===")
        If File.Exists(CustomerServerConfig) Then
            sb.AppendLine(File.ReadAllText(CustomerServerConfig))
        Else
            sb.AppendLine("Not configured")
        End If

        Return sb.ToString()
    End Function
End Class