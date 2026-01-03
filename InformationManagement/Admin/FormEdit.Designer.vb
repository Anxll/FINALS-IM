<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEdit
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.LabelName = New System.Windows.Forms.Label()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.LabelUser = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.LabelCurrentPass = New System.Windows.Forms.Label()
        Me.txtCurrentPassword = New System.Windows.Forms.TextBox()
        Me.LabelNewPass = New System.Windows.Forms.Label()
        Me.txtNewPassword = New System.Windows.Forms.TextBox()
        Me.chkShowPass = New System.Windows.Forms.CheckBox()
        Me.btnAddUser = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LabelTitle
        '
        Me.LabelTitle.AutoSize = True
        Me.LabelTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTitle.Location = New System.Drawing.Point(24, 15)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(90, 25)
        Me.LabelTitle.TabIndex = 0
        Me.LabelTitle.Text = "Edit User"
        '
        'LabelName
        '
        Me.LabelName.AutoSize = True
        Me.LabelName.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelName.Location = New System.Drawing.Point(26, 60)
        Me.LabelName.Name = "LabelName"
        Me.LabelName.Size = New System.Drawing.Size(71, 19)
        Me.LabelName.TabIndex = 1
        Me.LabelName.Text = "Full Name"
        '
        'txtFullName
        '
        Me.txtFullName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFullName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullName.Location = New System.Drawing.Point(30, 82)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.ReadOnly = True
        Me.txtFullName.Size = New System.Drawing.Size(300, 25)
        Me.txtFullName.TabIndex = 2
        '
        'LabelUser
        '
        Me.LabelUser.AutoSize = True
        Me.LabelUser.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelUser.Location = New System.Drawing.Point(26, 120)
        Me.LabelUser.Name = "LabelUser"
        Me.LabelUser.Size = New System.Drawing.Size(71, 19)
        Me.LabelUser.TabIndex = 3
        Me.LabelUser.Text = "Username"
        '
        'txtUsername
        '
        Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.Location = New System.Drawing.Point(30, 142)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(300, 25)
        Me.txtUsername.TabIndex = 4
        '
        'LabelCurrentPass
        '
        Me.LabelCurrentPass.AutoSize = True
        Me.LabelCurrentPass.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCurrentPass.Location = New System.Drawing.Point(26, 180)
        Me.LabelCurrentPass.Name = "LabelCurrentPass"
        Me.LabelCurrentPass.Size = New System.Drawing.Size(118, 19)
        Me.LabelCurrentPass.TabIndex = 5
        Me.LabelCurrentPass.Text = "Current Password"
        '
        'txtCurrentPassword
        '
        Me.txtCurrentPassword.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCurrentPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrentPassword.Location = New System.Drawing.Point(30, 202)
        Me.txtCurrentPassword.Name = "txtCurrentPassword"
        Me.txtCurrentPassword.PasswordChar = "*"c
        Me.txtCurrentPassword.ReadOnly = True
        Me.txtCurrentPassword.Size = New System.Drawing.Size(300, 25)
        Me.txtCurrentPassword.TabIndex = 6
        '
        'LabelNewPass
        '
        Me.LabelNewPass.AutoSize = True
        Me.LabelNewPass.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelNewPass.Location = New System.Drawing.Point(26, 240)
        Me.LabelNewPass.Name = "LabelNewPass"
        Me.LabelNewPass.Size = New System.Drawing.Size(100, 19)
        Me.LabelNewPass.TabIndex = 7
        Me.LabelNewPass.Text = "New Password"
        '
        'txtNewPassword
        '
        Me.txtNewPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPassword.Location = New System.Drawing.Point(30, 262)
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.PasswordChar = "*"c
        Me.txtNewPassword.Size = New System.Drawing.Size(300, 25)
        Me.txtNewPassword.TabIndex = 8
        '
        'chkShowPass
        '
        Me.chkShowPass.AutoSize = True
        Me.chkShowPass.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowPass.Location = New System.Drawing.Point(30, 298)
        Me.chkShowPass.Name = "chkShowPass"
        Me.chkShowPass.Size = New System.Drawing.Size(108, 19)
        Me.chkShowPass.TabIndex = 9
        Me.chkShowPass.Text = "Show Password"
        Me.chkShowPass.UseVisualStyleBackColor = True
        '
        'btnAddUser
        '
        Me.btnAddUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.btnAddUser.FlatAppearance.BorderSize = 0
        Me.btnAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddUser.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddUser.ForeColor = System.Drawing.Color.White
        Me.btnAddUser.Location = New System.Drawing.Point(190, 340)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(140, 40)
        Me.btnAddUser.TabIndex = 10
        Me.btnAddUser.Text = "Update"
        Me.btnAddUser.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Location = New System.Drawing.Point(30, 340)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(140, 40)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'FormEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(360, 410)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAddUser)
        Me.Controls.Add(Me.chkShowPass)
        Me.Controls.Add(Me.txtNewPassword)
        Me.Controls.Add(Me.LabelNewPass)
        Me.Controls.Add(Me.txtCurrentPassword)
        Me.Controls.Add(Me.LabelCurrentPass)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.LabelUser)
        Me.Controls.Add(Me.txtFullName)
        Me.Controls.Add(Me.LabelName)
        Me.Controls.Add(Me.LabelTitle)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit User"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelTitle As Label
    Friend WithEvents LabelName As Label
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents LabelUser As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents LabelCurrentPass As Label
    Friend WithEvents txtCurrentPassword As TextBox
    Friend WithEvents LabelNewPass As Label
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents chkShowPass As CheckBox
    Friend WithEvents btnAddUser As Button
    Friend WithEvents btnCancel As Button
End Class
