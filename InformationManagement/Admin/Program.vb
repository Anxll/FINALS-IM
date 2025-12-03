Imports System.IO

Module Program
    Public ConfigPath As String = Path.Combine(Application.StartupPath, "config.ini")

    <STAThread>
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Check if config exists
        If Not File.Exists(ConfigPath) Then
            ' First run → show configuration form
            Application.Run(New ConfigurationPage())
        Else
            ' Config exists → show login form
            Application.Run(New ConfigurationPage())
        End If
    End Sub
End Module
