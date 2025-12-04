<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ConfigurationPage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtServer = New InformationManagement.RoundedTextBox()
        Me.txtPort = New InformationManagement.RoundedTextBox()
        Me.txtUser = New InformationManagement.RoundedTextBox()
        Me.txtPassword = New InformationManagement.RoundedTextBox()
        Me.txtDatabase = New InformationManagement.RoundedTextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(75, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(75, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Label2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(75, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Label3"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(75, 147)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 16)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Label4"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(75, 192)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 16)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(75, 241)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 16)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Label6"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(75, 278)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 16)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Label7"
        '
        'txtServer
        '
        Me.txtServer.BackColor = System.Drawing.Color.Transparent
        Me.txtServer.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtServer.Location = New System.Drawing.Point(184, 32)
        Me.txtServer.MaxLength = 32767
        Me.txtServer.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtServer.Multiline = False
        Me.txtServer.Name = "txtServer"
        Me.txtServer.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtServer.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtServer.ReadOnly = False
        Me.txtServer.Size = New System.Drawing.Size(200, 30)
        Me.txtServer.TabIndex = 7
        Me.txtServer.TextBoxBackColor = System.Drawing.Color.White
        Me.txtServer.TextColor = System.Drawing.Color.Black
        Me.txtServer.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'txtPort
        '
        Me.txtPort.BackColor = System.Drawing.Color.Transparent
        Me.txtPort.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtPort.Location = New System.Drawing.Point(184, 91)
        Me.txtPort.MaxLength = 32767
        Me.txtPort.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtPort.Multiline = False
        Me.txtPort.Name = "txtPort"
        Me.txtPort.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtPort.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtPort.ReadOnly = False
        Me.txtPort.Size = New System.Drawing.Size(200, 30)
        Me.txtPort.TabIndex = 8
        Me.txtPort.TextBoxBackColor = System.Drawing.Color.White
        Me.txtPort.TextColor = System.Drawing.Color.Black
        Me.txtPort.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'txtUser
        '
        Me.txtUser.BackColor = System.Drawing.Color.Transparent
        Me.txtUser.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtUser.Location = New System.Drawing.Point(184, 161)
        Me.txtUser.MaxLength = 32767
        Me.txtUser.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtUser.Multiline = False
        Me.txtUser.Name = "txtUser"
        Me.txtUser.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtUser.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtUser.ReadOnly = False
        Me.txtUser.Size = New System.Drawing.Size(200, 30)
        Me.txtUser.TabIndex = 9
        Me.txtUser.TextBoxBackColor = System.Drawing.Color.White
        Me.txtUser.TextColor = System.Drawing.Color.Black
        Me.txtUser.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.Color.Transparent
        Me.txtPassword.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtPassword.Location = New System.Drawing.Point(184, 227)
        Me.txtPassword.MaxLength = 32767
        Me.txtPassword.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtPassword.Multiline = False
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtPassword.ReadOnly = False
        Me.txtPassword.Size = New System.Drawing.Size(200, 30)
        Me.txtPassword.TabIndex = 8
        Me.txtPassword.TextBoxBackColor = System.Drawing.Color.White
        Me.txtPassword.TextColor = System.Drawing.Color.Black
        Me.txtPassword.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'txtDatabase
        '
        Me.txtDatabase.BackColor = System.Drawing.Color.Transparent
        Me.txtDatabase.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtDatabase.Location = New System.Drawing.Point(184, 278)
        Me.txtDatabase.MaxLength = 32767
        Me.txtDatabase.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtDatabase.Multiline = False
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtDatabase.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtDatabase.ReadOnly = False
        Me.txtDatabase.Size = New System.Drawing.Size(200, 30)
        Me.txtDatabase.TabIndex = 8
        Me.txtDatabase.TextBoxBackColor = System.Drawing.Color.White
        Me.txtDatabase.TextColor = System.Drawing.Color.Black
        Me.txtDatabase.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'ConfigurationPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.GhostWhite
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.txtDatabase)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.txtServer)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ConfigurationPage"
        Me.Text = "ConfigurationPage"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtServer As RoundedTextBox
    Friend WithEvents txtPort As RoundedTextBox
    Friend WithEvents txtUser As RoundedTextBox
    Friend WithEvents txtPassword As RoundedTextBox
    Friend WithEvents txtDatabase As RoundedTextBox
End Class
