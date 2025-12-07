<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AddEmployee
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.WorkShift = New InformationManagement.RoundedTextBox()
        Me.MaritalStatus = New InformationManagement.RoundedTextBox()
        Me.EmploymentType = New InformationManagement.RoundedTextBox()
        Me.Address = New InformationManagement.RoundedTextBox()
        Me.Salary = New InformationManagement.RoundedTextBox()
        Me.EmploymentStatus = New InformationManagement.RoundedTextBox()
        Me.EmergencyContact = New InformationManagement.RoundedTextBox()
        Me.Position = New InformationManagement.RoundedTextBox()
        Me.LastName = New InformationManagement.RoundedTextBox()
        Me.Gender = New InformationManagement.RoundedTextBox()
        Me.ContactNumber = New InformationManagement.RoundedTextBox()
        Me.Email = New InformationManagement.RoundedTextBox()
        Me.FirstName = New InformationManagement.RoundedTextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.HireDate = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DateOfBirth = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.EmployeeID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 100)
        Me.Panel1.TabIndex = 3
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(1011, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(40, 40)
        Me.btnClose.TabIndex = 25
        Me.btnClose.Text = "✕"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(21, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(285, 40)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Add New Employee"
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnAdd)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.WorkShift)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.MaritalStatus)
        Me.Panel2.Controls.Add(Me.EmploymentType)
        Me.Panel2.Controls.Add(Me.Address)
        Me.Panel2.Controls.Add(Me.Salary)
        Me.Panel2.Controls.Add(Me.EmploymentStatus)
        Me.Panel2.Controls.Add(Me.EmergencyContact)
        Me.Panel2.Controls.Add(Me.Position)
        Me.Panel2.Controls.Add(Me.LastName)
        Me.Panel2.Controls.Add(Me.Gender)
        Me.Panel2.Controls.Add(Me.ContactNumber)
        Me.Panel2.Controls.Add(Me.Email)
        Me.Panel2.Controls.Add(Me.FirstName)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.HireDate)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.DateOfBirth)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.EmployeeID)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(20)
        Me.Panel2.Size = New System.Drawing.Size(696, 749)
        Me.Panel2.TabIndex = 1
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnAdd.ForeColor = System.Drawing.Color.White
        Me.btnAdd.Location = New System.Drawing.Point(537, 667)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(105, 45)
        Me.btnAdd.TabIndex = 17
        Me.btnAdd.Text = "➕ Add"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(28, 152)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 17)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "First Name"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(391, 667)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(110, 45)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'WorkShift
        '
        Me.WorkShift.BackColor = System.Drawing.Color.Transparent
        Me.WorkShift.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.WorkShift.Location = New System.Drawing.Point(368, 546)
        Me.WorkShift.MaxLength = 32767
        Me.WorkShift.MinimumSize = New System.Drawing.Size(50, 20)
        Me.WorkShift.Multiline = False
        Me.WorkShift.Name = "WorkShift"
        Me.WorkShift.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.WorkShift.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.WorkShift.ReadOnly = False
        Me.WorkShift.Size = New System.Drawing.Size(286, 34)
        Me.WorkShift.TabIndex = 82
        Me.WorkShift.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.WorkShift.TextColor = System.Drawing.Color.Black
        Me.WorkShift.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'MaritalStatus
        '
        Me.MaritalStatus.BackColor = System.Drawing.Color.Transparent
        Me.MaritalStatus.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.MaritalStatus.Location = New System.Drawing.Point(368, 371)
        Me.MaritalStatus.MaxLength = 32767
        Me.MaritalStatus.MinimumSize = New System.Drawing.Size(50, 20)
        Me.MaritalStatus.Multiline = False
        Me.MaritalStatus.Name = "MaritalStatus"
        Me.MaritalStatus.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.MaritalStatus.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.MaritalStatus.ReadOnly = False
        Me.MaritalStatus.Size = New System.Drawing.Size(286, 34)
        Me.MaritalStatus.TabIndex = 82
        Me.MaritalStatus.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.MaritalStatus.TextColor = System.Drawing.Color.Black
        Me.MaritalStatus.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'EmploymentType
        '
        Me.EmploymentType.BackColor = System.Drawing.Color.Transparent
        Me.EmploymentType.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.EmploymentType.Location = New System.Drawing.Point(368, 612)
        Me.EmploymentType.MaxLength = 32767
        Me.EmploymentType.MinimumSize = New System.Drawing.Size(50, 20)
        Me.EmploymentType.Multiline = False
        Me.EmploymentType.Name = "EmploymentType"
        Me.EmploymentType.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.EmploymentType.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.EmploymentType.ReadOnly = False
        Me.EmploymentType.Size = New System.Drawing.Size(286, 34)
        Me.EmploymentType.TabIndex = 84
        Me.EmploymentType.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.EmploymentType.TextColor = System.Drawing.Color.Black
        Me.EmploymentType.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'Address
        '
        Me.Address.BackColor = System.Drawing.Color.Transparent
        Me.Address.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.Address.Location = New System.Drawing.Point(31, 371)
        Me.Address.MaxLength = 32767
        Me.Address.MinimumSize = New System.Drawing.Size(50, 20)
        Me.Address.Multiline = True
        Me.Address.Name = "Address"
        Me.Address.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Address.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Address.ReadOnly = False
        Me.Address.Size = New System.Drawing.Size(288, 71)
        Me.Address.TabIndex = 83
        Me.Address.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.Address.TextColor = System.Drawing.Color.Black
        Me.Address.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'Salary
        '
        Me.Salary.BackColor = System.Drawing.Color.Transparent
        Me.Salary.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.Salary.Location = New System.Drawing.Point(33, 612)
        Me.Salary.MaxLength = 32767
        Me.Salary.MinimumSize = New System.Drawing.Size(50, 20)
        Me.Salary.Multiline = False
        Me.Salary.Name = "Salary"
        Me.Salary.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Salary.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Salary.ReadOnly = False
        Me.Salary.Size = New System.Drawing.Size(286, 34)
        Me.Salary.TabIndex = 82
        Me.Salary.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.Salary.TextColor = System.Drawing.Color.Black
        Me.Salary.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'EmploymentStatus
        '
        Me.EmploymentStatus.BackColor = System.Drawing.Color.Transparent
        Me.EmploymentStatus.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.EmploymentStatus.Location = New System.Drawing.Point(35, 678)
        Me.EmploymentStatus.MaxLength = 32767
        Me.EmploymentStatus.MinimumSize = New System.Drawing.Size(50, 20)
        Me.EmploymentStatus.Multiline = False
        Me.EmploymentStatus.Name = "EmploymentStatus"
        Me.EmploymentStatus.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.EmploymentStatus.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.EmploymentStatus.ReadOnly = False
        Me.EmploymentStatus.Size = New System.Drawing.Size(286, 34)
        Me.EmploymentStatus.TabIndex = 82
        Me.EmploymentStatus.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.EmploymentStatus.TextColor = System.Drawing.Color.Black
        Me.EmploymentStatus.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'EmergencyContact
        '
        Me.EmergencyContact.BackColor = System.Drawing.Color.Transparent
        Me.EmergencyContact.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.EmergencyContact.Location = New System.Drawing.Point(33, 546)
        Me.EmergencyContact.MaxLength = 32767
        Me.EmergencyContact.MinimumSize = New System.Drawing.Size(50, 20)
        Me.EmergencyContact.Multiline = False
        Me.EmergencyContact.Name = "EmergencyContact"
        Me.EmergencyContact.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.EmergencyContact.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.EmergencyContact.ReadOnly = False
        Me.EmergencyContact.Size = New System.Drawing.Size(286, 34)
        Me.EmergencyContact.TabIndex = 82
        Me.EmergencyContact.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.EmergencyContact.TextColor = System.Drawing.Color.Black
        Me.EmergencyContact.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'Position
        '
        Me.Position.BackColor = System.Drawing.Color.Transparent
        Me.Position.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.Position.Location = New System.Drawing.Point(34, 477)
        Me.Position.MaxLength = 32767
        Me.Position.MinimumSize = New System.Drawing.Size(50, 20)
        Me.Position.Multiline = False
        Me.Position.Name = "Position"
        Me.Position.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Position.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Position.ReadOnly = False
        Me.Position.Size = New System.Drawing.Size(286, 34)
        Me.Position.TabIndex = 81
        Me.Position.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.Position.TextColor = System.Drawing.Color.Black
        Me.Position.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'LastName
        '
        Me.LastName.BackColor = System.Drawing.Color.Transparent
        Me.LastName.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.LastName.Location = New System.Drawing.Point(32, 239)
        Me.LastName.MaxLength = 32767
        Me.LastName.MinimumSize = New System.Drawing.Size(50, 20)
        Me.LastName.Multiline = False
        Me.LastName.Name = "LastName"
        Me.LastName.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.LastName.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.LastName.ReadOnly = False
        Me.LastName.Size = New System.Drawing.Size(286, 34)
        Me.LastName.TabIndex = 79
        Me.LastName.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.LastName.TextColor = System.Drawing.Color.Black
        Me.LastName.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'Gender
        '
        Me.Gender.BackColor = System.Drawing.Color.Transparent
        Me.Gender.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.Gender.Location = New System.Drawing.Point(368, 305)
        Me.Gender.MaxLength = 32767
        Me.Gender.MinimumSize = New System.Drawing.Size(50, 20)
        Me.Gender.Multiline = False
        Me.Gender.Name = "Gender"
        Me.Gender.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Gender.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Gender.ReadOnly = False
        Me.Gender.Size = New System.Drawing.Size(286, 34)
        Me.Gender.TabIndex = 79
        Me.Gender.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.Gender.TextColor = System.Drawing.Color.Black
        Me.Gender.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'ContactNumber
        '
        Me.ContactNumber.BackColor = System.Drawing.Color.Transparent
        Me.ContactNumber.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.ContactNumber.Location = New System.Drawing.Point(33, 303)
        Me.ContactNumber.MaxLength = 32767
        Me.ContactNumber.MinimumSize = New System.Drawing.Size(50, 20)
        Me.ContactNumber.Multiline = False
        Me.ContactNumber.Name = "ContactNumber"
        Me.ContactNumber.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ContactNumber.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.ContactNumber.ReadOnly = False
        Me.ContactNumber.Size = New System.Drawing.Size(286, 34)
        Me.ContactNumber.TabIndex = 80
        Me.ContactNumber.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.ContactNumber.TextColor = System.Drawing.Color.Black
        Me.ContactNumber.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'Email
        '
        Me.Email.BackColor = System.Drawing.Color.Transparent
        Me.Email.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.Email.Location = New System.Drawing.Point(368, 239)
        Me.Email.MaxLength = 32767
        Me.Email.MinimumSize = New System.Drawing.Size(50, 20)
        Me.Email.Multiline = False
        Me.Email.Name = "Email"
        Me.Email.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Email.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Email.ReadOnly = False
        Me.Email.Size = New System.Drawing.Size(286, 34)
        Me.Email.TabIndex = 79
        Me.Email.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.Email.TextColor = System.Drawing.Color.Black
        Me.Email.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'FirstName
        '
        Me.FirstName.BackColor = System.Drawing.Color.Transparent
        Me.FirstName.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.FirstName.Location = New System.Drawing.Point(31, 172)
        Me.FirstName.MaxLength = 32767
        Me.FirstName.MinimumSize = New System.Drawing.Size(50, 20)
        Me.FirstName.Multiline = False
        Me.FirstName.Name = "FirstName"
        Me.FirstName.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.FirstName.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.FirstName.ReadOnly = False
        Me.FirstName.Size = New System.Drawing.Size(286, 34)
        Me.FirstName.TabIndex = 78
        Me.FirstName.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.FirstName.TextColor = System.Drawing.Color.Black
        Me.FirstName.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label15.Location = New System.Drawing.Point(365, 592)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(36, 17)
        Me.Label15.TabIndex = 77
        Me.Label15.Text = "Type"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label19.Location = New System.Drawing.Point(32, 658)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(46, 17)
        Me.Label19.TabIndex = 76
        Me.Label19.Text = "Status"
        '
        'HireDate
        '
        Me.HireDate.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.HireDate.Location = New System.Drawing.Point(368, 477)
        Me.HireDate.Name = "HireDate"
        Me.HireDate.Size = New System.Drawing.Size(286, 25)
        Me.HireDate.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(365, 457)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 17)
        Me.Label12.TabIndex = 69
        Me.Label12.Text = "Hire Date"
        '
        'DateOfBirth
        '
        Me.DateOfBirth.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.DateOfBirth.Location = New System.Drawing.Point(368, 177)
        Me.DateOfBirth.Name = "DateOfBirth"
        Me.DateOfBirth.Size = New System.Drawing.Size(286, 25)
        Me.DateOfBirth.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(365, 152)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 17)
        Me.Label9.TabIndex = 67
        Me.Label9.Text = "Date of Birth"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label16.Location = New System.Drawing.Point(365, 523)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(72, 17)
        Me.Label16.TabIndex = 64
        Me.Label16.Text = "Work Shift"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label17.Location = New System.Drawing.Point(32, 592)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(45, 17)
        Me.Label17.TabIndex = 62
        Me.Label17.Text = "Salary"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label18.Location = New System.Drawing.Point(31, 523)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(126, 17)
        Me.Label18.TabIndex = 60
        Me.Label18.Text = "Emergency Contact"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label14.Location = New System.Drawing.Point(31, 457)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(57, 17)
        Me.Label14.TabIndex = 58
        Me.Label14.Text = "Position"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(365, 351)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 17)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "Marital Status"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label13.Location = New System.Drawing.Point(29, 351)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(57, 17)
        Me.Label13.TabIndex = 54
        Me.Label13.Text = "Address"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(365, 219)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 17)
        Me.Label8.TabIndex = 49
        Me.Label8.Text = "Email"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(29, 283)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(109, 17)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "Contact Number"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(28, 219)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 17)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Last Name"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(365, 283)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 17)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "Gender"
        '
        'EmployeeID
        '
        Me.EmployeeID.BackColor = System.Drawing.Color.LightGray
        Me.EmployeeID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.EmployeeID.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.EmployeeID.ForeColor = System.Drawing.Color.Gray
        Me.EmployeeID.Location = New System.Drawing.Point(28, 115)
        Me.EmployeeID.Name = "EmployeeID"
        Me.EmployeeID.ReadOnly = True
        Me.EmployeeID.Size = New System.Drawing.Size(289, 22)
        Me.EmployeeID.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(25, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(155, 17)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Employee ID (Auto-Gen)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Label2.ForeColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(25, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(270, 17)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Enter the details of the new employee below."
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(200, 100)
        Me.Panel3.TabIndex = 2
        '
        'AddEmployee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(696, 749)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddEmployee"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents EmployeeID As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents DateOfBirth As DateTimePicker
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents HireDate As DateTimePicker
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents Label15 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents FirstName As RoundedTextBox
    Friend WithEvents LastName As RoundedTextBox
    Friend WithEvents Gender As RoundedTextBox
    Friend WithEvents ContactNumber As RoundedTextBox
    Friend WithEvents Email As RoundedTextBox
    Friend WithEvents EmergencyContact As RoundedTextBox
    Friend WithEvents Position As RoundedTextBox
    Friend WithEvents Salary As RoundedTextBox
    Friend WithEvents EmploymentStatus As RoundedTextBox
    Friend WithEvents WorkShift As RoundedTextBox
    Friend WithEvents MaritalStatus As RoundedTextBox
    Friend WithEvents EmploymentType As RoundedTextBox
    Friend WithEvents Address As RoundedTextBox
End Class