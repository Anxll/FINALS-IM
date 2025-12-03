Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Module ConfigManager
    Public ConfigPath As String = Path.Combine(Application.StartupPath, "config.ini")
    Public BackupFolder As String = Path.Combine(Application.StartupPath, "backups")

    ' Check if config exists
    Public Function ConfigExists() As Boolean
        Return File.Exists(ConfigPath)
    End Function

    ' Get value from INI
    Public Function GetValue(section As String, key As String) As String
        Dim lines() As String = File.ReadAllLines(ConfigPath)
        Dim inSection As Boolean = False
        For Each line In lines
            Dim l = line.Trim()
            If l.StartsWith("[") AndAlso l.EndsWith("]") Then
                inSection = (l.Substring(1, l.Length - 2) = section)
            ElseIf inSection AndAlso l.StartsWith(key & "=") Then
                Return l.Substring(key.Length + 1)
            End If
        Next
        Return ""
    End Function

    ' Save config and create backup
    Public Sub SaveConfig(values As Dictionary(Of String, String))
        Try
            ' Create backup if exists
            If ConfigExists() Then
                If Not Directory.Exists(BackupFolder) Then Directory.CreateDirectory(BackupFolder)
                Dim backupFile = Path.Combine(BackupFolder, $"config_{DateTime.Now:yyyy-MM-dd_HHmm}.ini")
                File.Copy(ConfigPath, backupFile, True)
            End If

            ' Write new config
            Using sw As New StreamWriter(ConfigPath, False)
                sw.WriteLine("[DATABASE]")
                sw.WriteLine($"ServerIP={values("ServerIP")}")
                sw.WriteLine($"DBName={values("DBName")}")
                sw.WriteLine($"DBUser={values("DBUser")}")
                sw.WriteLine($"DBPass={values("DBPass")}")

                sw.WriteLine("[SMTP]")
                sw.WriteLine($"Host={values("SMTPHost")}")
                sw.WriteLine($"Port={values("SMTPPort")}")
            End Using

        Catch ex As Exception
            MsgBox("Error saving configuration: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

End Module
