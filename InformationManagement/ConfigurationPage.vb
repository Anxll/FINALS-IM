Imports System.Data.Common

Public Class ConfigurationPage
    Private Sub FormConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Prefill fields if config exists
        If ConfigManager.ConfigExists() Then
            txtServerIP.Text = ConfigManager.GetValue("DATABASE", "ServerIP")
            txtDBName.Text = ConfigManager.GetValue("DATABASE", "DBName")
            txtUser.Text = ConfigManager.GetValue("DATABASE", "DBUser")
            txtPassword.Text = ConfigManager.GetValue("DATABASE", "DBPass")
            txtSMTPHost.Text = ConfigManager.GetValue("SMTP", "Host")
            txtSMTPPort.Text = ConfigManager.GetValue("SMTP", "Port")
        End If
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        If modDB.TestConnection() Then
            MsgBox("Connection successful!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Validate required fields
        If txtServerIP.Text = "" Or txtDBName.Text = "" Or txtUser.Text = "" Then
            MsgBox("Please fill all database fields.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' Prepare dictionary to save
        Dim config As New Dictionary(Of String, String) From {
            {"ServerIP", txtServerIP.Text},
            {"DBName", txtDBName.Text},
            {"DBUser", txtUser.Text},
            {"DBPass", txtPassword.Text},
            {"SMTPHost", txtSMTPHost.Text},
            {"SMTPPort", txtSMTPPort.Text}
        }

        ' Save config
        ConfigManager.SaveConfig(config)
        MsgBox("Configuration saved successfully!", MsgBoxStyle.Information)

        ' Optionally test DB
        modDB.TestConnection()

        ' Close form and go to login if first run
        Me.Close()
        Dim loginForm As New Login()
        loginForm.Show()
    End Sub
End Class
