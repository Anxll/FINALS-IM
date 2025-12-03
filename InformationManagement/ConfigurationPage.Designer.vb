<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigurationPage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSMTPPort = New InformationManagement.RoundedTextBox()
        Me.txtSMTPHost = New InformationManagement.RoundedTextBox()
        Me.txtPassword = New InformationManagement.RoundedTextBox()
        Me.txtUser = New InformationManagement.RoundedTextBox()
        Me.txtDBName = New InformationManagement.RoundedTextBox()
        Me.txtServerIP = New InformationManagement.RoundedTextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(58, 38)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(199, 21)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server Name / IP Address *"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(51, 78)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 21)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Port Number *"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(51, 127)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 21)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Database Name *"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 173)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(160, 21)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Database Username *"
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(181, 481)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(165, 38)
        Me.btnTest.TabIndex = 11
        Me.btnTest.Text = "Test Connection"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(463, 396)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(143, 63)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "Save Configuration"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(485, 217)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(76, 23)
        Me.btnReset.TabIndex = 13
        Me.btnReset.Text = "Reset to Defaults"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(485, 254)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(121, 39)
        Me.btnCancel.TabIndex = 14
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(80, 324)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(55, 21)
        Me.lblStatus.TabIndex = 15
        Me.lblStatus.Text = "Status:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(38, 255)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(144, 21)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Database Password"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(464, 36)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(323, 37)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Database Configuration"
        '
        'txtSMTPPort
        '
        Me.txtSMTPPort.BackColor = System.Drawing.Color.Transparent
        Me.txtSMTPPort.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtSMTPPort.Location = New System.Drawing.Point(257, 372)
        Me.txtSMTPPort.MaxLength = 32767
        Me.txtSMTPPort.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtSMTPPort.Multiline = False
        Me.txtSMTPPort.Name = "txtSMTPPort"
        Me.txtSMTPPort.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtSMTPPort.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtSMTPPort.ReadOnly = False
        Me.txtSMTPPort.Size = New System.Drawing.Size(200, 30)
        Me.txtSMTPPort.TabIndex = 9
        Me.txtSMTPPort.TextBoxBackColor = System.Drawing.Color.White
        Me.txtSMTPPort.TextColor = System.Drawing.Color.Black
        Me.txtSMTPPort.TextFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'txtSMTPHost
        '
        Me.txtSMTPHost.BackColor = System.Drawing.Color.Transparent
        Me.txtSMTPHost.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtSMTPHost.Location = New System.Drawing.Point(257, 315)
        Me.txtSMTPHost.MaxLength = 32767
        Me.txtSMTPHost.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtSMTPHost.Multiline = False
        Me.txtSMTPHost.Name = "txtSMTPHost"
        Me.txtSMTPHost.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtSMTPHost.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtSMTPHost.ReadOnly = False
        Me.txtSMTPHost.Size = New System.Drawing.Size(200, 30)
        Me.txtSMTPHost.TabIndex = 18
        Me.txtSMTPHost.TextBoxBackColor = System.Drawing.Color.White
        Me.txtSMTPHost.TextColor = System.Drawing.Color.Black
        Me.txtSMTPHost.TextFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.Color.Transparent
        Me.txtPassword.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtPassword.Location = New System.Drawing.Point(257, 247)
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
        Me.txtPassword.TextFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'txtUser
        '
        Me.txtUser.BackColor = System.Drawing.Color.Transparent
        Me.txtUser.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtUser.Location = New System.Drawing.Point(257, 187)
        Me.txtUser.MaxLength = 32767
        Me.txtUser.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtUser.Multiline = False
        Me.txtUser.Name = "txtUser"
        Me.txtUser.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtUser.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtUser.ReadOnly = False
        Me.txtUser.Size = New System.Drawing.Size(200, 30)
        Me.txtUser.TabIndex = 7
        Me.txtUser.TextBoxBackColor = System.Drawing.Color.White
        Me.txtUser.TextColor = System.Drawing.Color.Black
        Me.txtUser.TextFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'txtDBName
        '
        Me.txtDBName.BackColor = System.Drawing.Color.Transparent
        Me.txtDBName.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtDBName.Location = New System.Drawing.Point(257, 127)
        Me.txtDBName.MaxLength = 32767
        Me.txtDBName.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtDBName.Multiline = False
        Me.txtDBName.Name = "txtDBName"
        Me.txtDBName.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtDBName.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtDBName.ReadOnly = False
        Me.txtDBName.Size = New System.Drawing.Size(200, 30)
        Me.txtDBName.TabIndex = 6
        Me.txtDBName.TextBoxBackColor = System.Drawing.Color.White
        Me.txtDBName.TextColor = System.Drawing.Color.Black
        Me.txtDBName.TextFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'txtServerIP
        '
        Me.txtServerIP.BackColor = System.Drawing.Color.Transparent
        Me.txtServerIP.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtServerIP.Location = New System.Drawing.Point(257, 78)
        Me.txtServerIP.MaxLength = 32767
        Me.txtServerIP.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtServerIP.Multiline = False
        Me.txtServerIP.Name = "txtServerIP"
        Me.txtServerIP.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtServerIP.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtServerIP.ReadOnly = False
        Me.txtServerIP.Size = New System.Drawing.Size(200, 30)
        Me.txtServerIP.TabIndex = 4
        Me.txtServerIP.TextBoxBackColor = System.Drawing.Color.White
        Me.txtServerIP.TextColor = System.Drawing.Color.Black
        Me.txtServerIP.TextFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'ConfigurationPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(761, 727)
        Me.Controls.Add(Me.txtSMTPPort)
        Me.Controls.Add(Me.txtSMTPHost)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.txtDBName)
        Me.Controls.Add(Me.txtServerIP)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "ConfigurationPage"
        Me.Text = "ConfigurationPage"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtServerIP As RoundedTextBox
    Friend WithEvents txtDBName As RoundedTextBox
    Friend WithEvents txtUser As RoundedTextBox
    Friend WithEvents txtPassword As RoundedTextBox
    Friend WithEvents btnTest As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnReset As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtSMTPHost As RoundedTextBox
    Friend WithEvents txtSMTPPort As RoundedTextBox
End Class
