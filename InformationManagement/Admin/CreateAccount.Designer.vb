<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreateAccount
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CreateAccount))
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.LabelName = New System.Windows.Forms.Label()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.LabelUser = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.LabelPass = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.LabelRole = New System.Windows.Forms.Label()
        Me.cmbRole = New System.Windows.Forms.ComboBox()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LabelTitle
        '
        Me.LabelTitle.AutoSize = True
        Me.LabelTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTitle.Location = New System.Drawing.Point(24, 20)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(148, 25)
        Me.LabelTitle.TabIndex = 0
        Me.LabelTitle.Text = "Create Account"
        '
        'LabelName
        '
        Me.LabelName.AutoSize = True
        Me.LabelName.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelName.Location = New System.Drawing.Point(26, 70)
        Me.LabelName.Name = "LabelName"
        Me.LabelName.Size = New System.Drawing.Size(71, 19)
        Me.LabelName.TabIndex = 1
        Me.LabelName.Text = "Full Name"
        '
        'txtFullName
        '
        Me.txtFullName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullName.Location = New System.Drawing.Point(30, 92)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(300, 25)
        Me.txtFullName.TabIndex = 2
        '
        'LabelUser
        '
        Me.LabelUser.AutoSize = True
        Me.LabelUser.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelUser.Location = New System.Drawing.Point(26, 130)
        Me.LabelUser.Name = "LabelUser"
        Me.LabelUser.Size = New System.Drawing.Size(71, 19)
        Me.LabelUser.TabIndex = 3
        Me.LabelUser.Text = "Username"
        '
        'txtUsername
        '
        Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.Location = New System.Drawing.Point(30, 152)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(300, 25)
        Me.txtUsername.TabIndex = 4
        '
        'LabelPass
        '
        Me.LabelPass.AutoSize = True
        Me.LabelPass.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPass.Location = New System.Drawing.Point(26, 190)
        Me.LabelPass.Name = "LabelPass"
        Me.LabelPass.Size = New System.Drawing.Size(68, 19)
        Me.LabelPass.TabIndex = 5
        Me.LabelPass.Text = "Password"
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(30, 212)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = "*"c
        Me.txtPassword.Size = New System.Drawing.Size(300, 25)
        Me.txtPassword.TabIndex = 6
        '
        'LabelRole
        '
        Me.LabelRole.AutoSize = True
        Me.LabelRole.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelRole.Location = New System.Drawing.Point(26, 250)
        Me.LabelRole.Name = "LabelRole"
        Me.LabelRole.Size = New System.Drawing.Size(36, 19)
        Me.LabelRole.TabIndex = 7
        Me.LabelRole.Text = "Role"
        '
        'cmbRole
        '
        Me.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRole.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRole.FormattingEnabled = True
        Me.cmbRole.Items.AddRange(New Object() {"Staff", "Employee"})
        Me.cmbRole.Location = New System.Drawing.Point(30, 272)
        Me.cmbRole.Name = "cmbRole"
        Me.cmbRole.Size = New System.Drawing.Size(300, 25)
        Me.cmbRole.TabIndex = 8
        '
        'btnCreate
        '
        Me.btnCreate.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.btnCreate.FlatAppearance.BorderSize = 0
        Me.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreate.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreate.ForeColor = System.Drawing.Color.White
        Me.btnCreate.Location = New System.Drawing.Point(190, 330)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(140, 40)
        Me.btnCreate.TabIndex = 9
        Me.btnCreate.Text = "Create"
        Me.btnCreate.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Location = New System.Drawing.Point(30, 330)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(140, 40)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'chkShowPass
        '
        Me.chkShowPass = New System.Windows.Forms.CheckBox()
        Me.chkShowPass.AutoSize = True
        Me.chkShowPass.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowPass.Location = New System.Drawing.Point(30, 298) 
        ' Note: Role combo is at 272, height 25 -> Y=297+. So Y=300 is good.
        ' Wait, previously button was at 330. So I have space.
        Me.chkShowPass.Name = "chkShowPass"
        Me.chkShowPass.Size = New System.Drawing.Size(108, 19)
        Me.chkShowPass.TabIndex = 11
        Me.chkShowPass.Text = "Show Password"
        Me.chkShowPass.UseVisualStyleBackColor = True
        
        '
        'CreateAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(360, 400)
        Me.Controls.Add(Me.chkShowPass)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.cmbRole)
        Me.Controls.Add(Me.LabelRole)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.LabelPass)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.LabelUser)
        Me.Controls.Add(Me.txtFullName)
        Me.Controls.Add(Me.LabelName)
        Me.Controls.Add(Me.LabelTitle)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CreateAccount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create Account"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelTitle As Label
    Friend WithEvents LabelName As Label
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents LabelUser As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents LabelPass As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents LabelRole As Label
    Friend WithEvents cmbRole As ComboBox
    Friend WithEvents btnCreate As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents chkShowPass As CheckBox
End Class
