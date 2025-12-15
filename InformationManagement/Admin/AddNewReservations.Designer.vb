<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PanelCreateReservation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PanelCreateReservation))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnCreateReservation = New System.Windows.Forms.Button()
        Me.cmbEventType = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtNote = New InformationManagement.RoundedTextBox()
        Me.txtPhone = New InformationManagement.RoundedTextBox()
        Me.txtEmail = New InformationManagement.RoundedTextBox()
        Me.txtFullName = New InformationManagement.RoundedTextBox()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(224, 25)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Create New Reservation"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Full Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Email"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(21, 182)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 17)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Phone"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 250)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 17)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Guests"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(267, 250)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 17)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Event Type"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(267, 317)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 17)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Time"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(21, 317)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 17)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Date"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(21, 383)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 17)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Note"
        '
        'btnCancel
        '
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(187, 498)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(87, 33)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnCreateReservation
        '
        Me.btnCreateReservation.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.btnCreateReservation.FlatAppearance.BorderSize = 0
        Me.btnCreateReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateReservation.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateReservation.ForeColor = System.Drawing.Color.White
        Me.btnCreateReservation.Location = New System.Drawing.Point(299, 498)
        Me.btnCreateReservation.Name = "btnCreateReservation"
        Me.btnCreateReservation.Size = New System.Drawing.Size(173, 33)
        Me.btnCreateReservation.TabIndex = 12
        Me.btnCreateReservation.Text = "Create Reservation"
        Me.btnCreateReservation.UseVisualStyleBackColor = True
        '
        'cmbEventType
        '
        Me.cmbEventType.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmbEventType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEventType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbEventType.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEventType.FormattingEnabled = True
        Me.cmbEventType.ItemHeight = 30
        Me.cmbEventType.Location = New System.Drawing.Point(270, 279)
        Me.cmbEventType.Name = "cmbEventType"
        Me.cmbEventType.Size = New System.Drawing.Size(192, 36)
        Me.cmbEventType.TabIndex = 16
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke
        Me.DateTimePicker1.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(22, 346)
        Me.DateTimePicker1.MaxDate = New Date(2025, 10, 31, 0, 0, 0, 0)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(182, 25)
        Me.DateTimePicker1.TabIndex = 17
        Me.DateTimePicker1.Value = New Date(2025, 10, 31, 0, 0, 0, 0)
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NumericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NumericUpDown1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumericUpDown1.Location = New System.Drawing.Point(24, 279)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(180, 21)
        Me.NumericUpDown1.TabIndex = 18
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke
        Me.DateTimePicker2.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePicker2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePicker2.Location = New System.Drawing.Point(270, 346)
        Me.DateTimePicker2.MaxDate = New Date(2025, 10, 31, 0, 0, 0, 0)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(192, 25)
        Me.DateTimePicker2.TabIndex = 20
        Me.DateTimePicker2.Value = New Date(2025, 10, 31, 0, 0, 0, 0)
        '
        'btnClose
        '
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(433, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(29, 27)
        Me.btnClose.TabIndex = 21
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtNote
        '
        Me.txtNote.BackColor = System.Drawing.Color.Transparent
        Me.txtNote.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtNote.Location = New System.Drawing.Point(24, 401)
        Me.txtNote.MaxLength = 32767
        Me.txtNote.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtNote.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtNote.Size = New System.Drawing.Size(438, 91)
        Me.txtNote.TabIndex = 25
        Me.txtNote.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.txtNote.TextColor = System.Drawing.Color.Black
        Me.txtNote.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'txtPhone
        '
        Me.txtPhone.BackColor = System.Drawing.Color.Transparent
        Me.txtPhone.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtPhone.Location = New System.Drawing.Point(24, 202)
        Me.txtPhone.MaxLength = 32767
        Me.txtPhone.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtPhone.Multiline = False
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtPhone.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtPhone.Size = New System.Drawing.Size(438, 40)
        Me.txtPhone.TabIndex = 24
        Me.txtPhone.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPhone.TextColor = System.Drawing.Color.Black
        Me.txtPhone.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'txtEmail
        '
        Me.txtEmail.BackColor = System.Drawing.Color.Transparent
        Me.txtEmail.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtEmail.Location = New System.Drawing.Point(24, 135)
        Me.txtEmail.MaxLength = 32767
        Me.txtEmail.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtEmail.Multiline = False
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtEmail.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtEmail.Size = New System.Drawing.Size(438, 40)
        Me.txtEmail.TabIndex = 23
        Me.txtEmail.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.txtEmail.TextColor = System.Drawing.Color.Black
        Me.txtEmail.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'txtFullName
        '
        Me.txtFullName.BackColor = System.Drawing.Color.Transparent
        Me.txtFullName.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtFullName.Location = New System.Drawing.Point(24, 66)
        Me.txtFullName.MaxLength = 32767
        Me.txtFullName.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtFullName.Multiline = False
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtFullName.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtFullName.Size = New System.Drawing.Size(438, 40)
        Me.txtFullName.TabIndex = 22
        Me.txtFullName.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFullName.TextColor = System.Drawing.Color.Black
        Me.txtFullName.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'PanelCreateReservation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(484, 541)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtFullName)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.cmbEventType)
        Me.Controls.Add(Me.btnCreateReservation)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "PanelCreateReservation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AddNewReservations"
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnCreateReservation As Button
    Friend WithEvents cmbEventType As ComboBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents btnClose As Button
    Friend WithEvents txtFullName As RoundedTextBox
    Friend WithEvents txtEmail As RoundedTextBox
    Friend WithEvents txtPhone As RoundedTextBox
    Friend WithEvents txtNote As RoundedTextBox
End Class
