
'RoundedPanel1
'
' PSEUDOCODE / PLAN:
' 1. Set the main rounded panel background color to red so the panel is visually red.
' 2. Preserve layout, controls and existing event wiring.
' 3. Keep password masking and Show password checkbox behavior intact.
' 4. Ensure only the panel color change is applied to minimize side effects.

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class Adminlogin
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Adminlogin))
            Me.Back1 = New System.Windows.Forms.Button()
            Me.RoundedPanel1 = New InformationManagement.RoundedPanel()
            Me.txtPassword = New InformationManagement.RoundedTextBox()
            Me.txtUsername = New InformationManagement.RoundedTextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.adminlog = New System.Windows.Forms.Button()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.chkShowPassword = New System.Windows.Forms.CheckBox()
            Me.RoundedPanel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Back1
            '
            Me.Back1.BackColor = System.Drawing.Color.White
            Me.Back1.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Back1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            Me.Back1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Back1.ForeColor = System.Drawing.Color.Red
            Me.Back1.Location = New System.Drawing.Point(15, 14)
            Me.Back1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.Back1.Name = "Back1"
            Me.Back1.Size = New System.Drawing.Size(99, 34)
            Me.Back1.TabIndex = 9
            Me.Back1.Text = "Back"
            Me.Back1.UseVisualStyleBackColor = False
            '
            'RoundedPanel1
            '
            Me.RoundedPanel1.Anchor = System.Windows.Forms.AnchorStyles.None
            ' Make the panel color red as requested
            Me.RoundedPanel1.BackColor = System.Drawing.Color.Red
            Me.RoundedPanel1.Controls.Add(Me.chkShowPassword)
            Me.RoundedPanel1.Controls.Add(Me.txtPassword)
            Me.RoundedPanel1.Controls.Add(Me.txtUsername)
            Me.RoundedPanel1.Controls.Add(Me.Label1)
            Me.RoundedPanel1.Controls.Add(Me.adminlog)
            Me.RoundedPanel1.Controls.Add(Me.Label2)
            Me.RoundedPanel1.Controls.Add(Me.Label3)
            Me.RoundedPanel1.CornerRadius = 12
            ' Centered and slightly smaller for a cleaner look
            Me.RoundedPanel1.Location = New System.Drawing.Point(391, 140)
            Me.RoundedPanel1.Margin = New System.Windows.Forms.Padding(4)
            Me.RoundedPanel1.Name = "RoundedPanel1"
            Me.RoundedPanel1.Size = New System.Drawing.Size(391, 300)
            Me.RoundedPanel1.TabIndex = 10
            '
            'txtPassword
            '
            Me.txtPassword.BackColor = System.Drawing.Color.Transparent
            Me.txtPassword.BorderRadius = 6
            Me.txtPassword.FocusBorderColor = System.Drawing.Color.Red
            Me.txtPassword.Location = New System.Drawing.Point(80, 165)
            Me.txtPassword.Margin = New System.Windows.Forms.Padding(4)
            Me.txtPassword.MaxLength = 32767
            Me.txtPassword.MinimumSize = New System.Drawing.Size(67, 25)
            Me.txtPassword.Multiline = False
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.NormalBorderColor = System.Drawing.Color.FromArgb(CType(200, Byte), CType(200, Byte), CType(200, Byte))
            ' Start masked using '*' for a standard appearance
            Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.txtPassword.Size = New System.Drawing.Size(231, 34)
            Me.txtPassword.TabIndex = 10
            Me.txtPassword.TextBoxBackColor = System.Drawing.Color.White
            Me.txtPassword.TextColor = System.Drawing.Color.Black
            Me.txtPassword.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
            '
            'txtUsername
            '
            Me.txtUsername.BackColor = System.Drawing.Color.Transparent
            Me.txtUsername.BorderRadius = 6
            Me.txtUsername.FocusBorderColor = System.Drawing.Color.Red
            Me.txtUsername.Location = New System.Drawing.Point(80, 100)
            Me.txtUsername.Margin = New System.Windows.Forms.Padding(4)
            Me.txtUsername.MaxLength = 32767
            Me.txtUsername.MinimumSize = New System.Drawing.Size(67, 25)
            Me.txtUsername.Multiline = False
            Me.txtUsername.Name = "txtUsername"
            Me.txtUsername.NormalBorderColor = System.Drawing.Color.FromArgb(CType(200, Byte), CType(200, Byte), CType(200, Byte))
            Me.txtUsername.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
            Me.txtUsername.Size = New System.Drawing.Size(231, 34)
            Me.txtUsername.TabIndex = 9
            Me.txtUsername.TextBoxBackColor = System.Drawing.Color.White
            Me.txtUsername.TextColor = System.Drawing.Color.Black
            Me.txtUsername.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = System.Drawing.Color.Transparent
            Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.ForeColor = System.Drawing.Color.Black
            Me.Label1.Location = New System.Drawing.Point(160, 16)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(74, 32)
            Me.Label1.TabIndex = 3
            Me.Label1.Text = "Admin"
            '
            'adminlog
            '
            Me.adminlog.BackColor = System.Drawing.Color.White
            Me.adminlog.FlatAppearance.BorderSize = 1
            Me.adminlog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.adminlog.Font = New System.Drawing.Font("Microsoft New Tai Lue", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.adminlog.ForeColor = System.Drawing.Color.Black
            Me.adminlog.Location = New System.Drawing.Point(130, 230)
            Me.adminlog.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.adminlog.Name = "adminlog"
            Me.adminlog.Size = New System.Drawing.Size(131, 33)
            Me.adminlog.TabIndex = 8
            Me.adminlog.Text = "Login"
            Me.adminlog.UseVisualStyleBackColor = False
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.BackColor = System.Drawing.Color.Transparent
            Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.ForeColor = System.Drawing.Color.Black
            Me.Label2.Location = New System.Drawing.Point(76, 74)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(92, 23)
            Me.Label2.TabIndex = 6
            Me.Label2.Text = "Username :"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.ForeColor = System.Drawing.Color.Black
            Me.Label3.Location = New System.Drawing.Point(76, 139)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(86, 23)
            Me.Label3.TabIndex = 7
            Me.Label3.Text = "Password :"
            '
            'chkShowPassword
            '
            Me.chkShowPassword.AutoSize = True
            Me.chkShowPassword.BackColor = System.Drawing.Color.Transparent
            Me.chkShowPassword.ForeColor = System.Drawing.Color.Black
            Me.chkShowPassword.Location = New System.Drawing.Point(80, 205)
            Me.chkShowPassword.Name = "chkShowPassword"
            Me.chkShowPassword.Size = New System.Drawing.Size(130, 21)
            Me.chkShowPassword.TabIndex = 11
            Me.chkShowPassword.Text = "Show password"
            Me.chkShowPassword.UseVisualStyleBackColor = False
            AddHandler Me.chkShowPassword.CheckedChanged, AddressOf Me.chkShowPassword_CheckedChanged
            '
            'Adminlogin
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            ' Remove background image and use plain white background
            Me.BackColor = System.Drawing.Color.White
            Me.BackgroundImage = Nothing
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.CancelButton = Me.Back1
            Me.ClientSize = New System.Drawing.Size(1193, 550)
            Me.Controls.Add(Me.RoundedPanel1)
            Me.Controls.Add(Me.Back1)
            Me.DoubleBuffered = True
            Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.Name = "Adminlogin"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Admin"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            Me.RoundedPanel1.ResumeLayout(False)
            Me.RoundedPanel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        ' Event handler to toggle password visibility
        Private Sub chkShowPassword_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Me.chkShowPassword.Checked Then
                    ' Show the password text
                    Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
                Else
                    ' Mask the password with '*'
                    Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
                End If
            Catch
                ' Silently ignore designer-time exceptions
            End Try
        End Sub

        Friend WithEvents Label1 As Label
        Friend WithEvents Label2 As Label
        Friend WithEvents Label3 As Label
        Friend WithEvents adminlog As Button
        Friend WithEvents Back1 As Button
        Friend WithEvents RoundedPanel1 As RoundedPanel
        Friend WithEvents txtPassword As RoundedTextBox
        Friend WithEvents txtUsername As RoundedTextBox
        Friend WithEvents chkShowPassword As CheckBox
    End Class
    