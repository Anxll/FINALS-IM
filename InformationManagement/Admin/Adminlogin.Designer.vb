
'Admin Login Form - Modern Design
'
' DESIGN IMPROVEMENTS:
' 1. Modern gradient color scheme with professional blue tones
' 2. Enhanced spacing and visual hierarchy
' 3. Improved button styling with better contrast
' 4. Refined typography and layout
' 5. Professional appearance with modern aesthetics

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
        Me.RoundedPanel1 = New InformationManagement.RoundedPanel()
        Me.chkShowPassword = New System.Windows.Forms.CheckBox()
        Me.txtPassword = New InformationManagement.RoundedTextBox()
        Me.txtUsername = New InformationManagement.RoundedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.adminlog = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.RoundedPanel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RoundedPanel1
        '
        Me.RoundedPanel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.RoundedPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.RoundedPanel1.Controls.Add(Me.PictureBox1)
        Me.RoundedPanel1.Controls.Add(Me.chkShowPassword)
        Me.RoundedPanel1.Controls.Add(Me.txtPassword)
        Me.RoundedPanel1.Controls.Add(Me.txtUsername)
        Me.RoundedPanel1.Controls.Add(Me.Label1)
        Me.RoundedPanel1.Controls.Add(Me.adminlog)
        Me.RoundedPanel1.Controls.Add(Me.Label2)
        Me.RoundedPanel1.Controls.Add(Me.Label3)
        Me.RoundedPanel1.CornerRadius = 20
        Me.RoundedPanel1.Location = New System.Drawing.Point(281, 138)
        Me.RoundedPanel1.Name = "RoundedPanel1"
        Me.RoundedPanel1.Size = New System.Drawing.Size(366, 412)
        Me.RoundedPanel1.TabIndex = 10
        '
        'chkShowPassword
        '
        Me.chkShowPassword.AutoSize = True
        Me.chkShowPassword.BackColor = System.Drawing.Color.Transparent
        Me.chkShowPassword.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkShowPassword.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowPassword.ForeColor = System.Drawing.Color.White
        Me.chkShowPassword.Location = New System.Drawing.Point(54, 281)
        Me.chkShowPassword.Margin = New System.Windows.Forms.Padding(2)
        Me.chkShowPassword.Name = "chkShowPassword"
        Me.chkShowPassword.Size = New System.Drawing.Size(119, 21)
        Me.chkShowPassword.TabIndex = 11
        Me.chkShowPassword.Text = "Show password"
        Me.chkShowPassword.UseVisualStyleBackColor = False
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.Color.Transparent
        Me.txtPassword.BorderRadius = 10
        Me.txtPassword.FocusBorderColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.txtPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.txtPassword.Location = New System.Drawing.Point(58, 239)
        Me.txtPassword.MaxLength = 32767
        Me.txtPassword.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtPassword.Multiline = False
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(199, Byte), Integer))
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtPassword.ReadOnly = False
        Me.txtPassword.Size = New System.Drawing.Size(248, 37)
        Me.txtPassword.TabIndex = 10
        Me.txtPassword.TextBoxBackColor = System.Drawing.Color.White
        Me.txtPassword.TextColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.txtPassword.TextFont = New System.Drawing.Font("Segoe UI", 11.0!)
        '
        'txtUsername
        '
        Me.txtUsername.BackColor = System.Drawing.Color.Transparent
        Me.txtUsername.BorderRadius = 10
        Me.txtUsername.FocusBorderColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.txtUsername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.txtUsername.Location = New System.Drawing.Point(56, 165)
        Me.txtUsername.MaxLength = 32767
        Me.txtUsername.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtUsername.Multiline = False
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(199, Byte), Integer))
        Me.txtUsername.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtUsername.ReadOnly = False
        Me.txtUsername.Size = New System.Drawing.Size(248, 37)
        Me.txtUsername.TabIndex = 9
        Me.txtUsername.TextBoxBackColor = System.Drawing.Color.White
        Me.txtUsername.TextColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.txtUsername.TextFont = New System.Drawing.Font("Segoe UI", 11.0!)
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(86, 74)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(199, 41)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Admin Login"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'adminlog
        '
        Me.adminlog.BackColor = System.Drawing.Color.Gray
        Me.adminlog.Cursor = System.Windows.Forms.Cursors.Hand
        Me.adminlog.FlatAppearance.BorderSize = 0
        Me.adminlog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue
        Me.adminlog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.adminlog.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adminlog.ForeColor = System.Drawing.Color.White
        Me.adminlog.Location = New System.Drawing.Point(58, 327)
        Me.adminlog.Margin = New System.Windows.Forms.Padding(2)
        Me.adminlog.Name = "adminlog"
        Me.adminlog.Size = New System.Drawing.Size(248, 41)
        Me.adminlog.TabIndex = 8
        Me.adminlog.Text = "LOGIN"
        Me.adminlog.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(56, 140)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Username"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(56, 214)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Password"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(126, 33)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(105, 46)
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = False
        '
        'Adminlogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.GhostWhite
        Me.BackgroundImage = Global.InformationManagement.My.Resources.Resources.Background
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(895, 705)
        Me.Controls.Add(Me.RoundedPanel1)
        Me.DoubleBuffered = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Adminlogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Admin"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.RoundedPanel1.ResumeLayout(False)
        Me.RoundedPanel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    ' Event handler to toggle password visibility
    Private Sub chkShowPassword_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowPassword.CheckedChanged
        Try
            If Me.chkShowPassword.Checked Then
                ' Show the password text
                Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
            Else
                ' Mask the password with '●' for modern appearance
                Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
            End If
        Catch
            ' Silently ignore designer-time exceptions
        End Try
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents adminlog As Button
    Friend WithEvents RoundedPanel1 As RoundedPanel
    Friend WithEvents txtPassword As RoundedTextBox
    Friend WithEvents txtUsername As RoundedTextBox
    Friend WithEvents chkShowPassword As CheckBox
    Friend WithEvents PictureBox1 As PictureBox
End Class
