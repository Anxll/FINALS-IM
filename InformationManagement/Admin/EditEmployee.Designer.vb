<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EditEmployee
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.EmploymentType = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.EmploymentStatus = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.HireDate = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DateOfBirth = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.WorkShift = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Salary = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.EmergencyContact = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Position = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.MaritalStatus = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Address = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Email = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.FirstName = New System.Windows.Forms.TextBox()
        Me.ContactNumber = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.LastName = New System.Windows.Forms.TextBox()
        Me.Gender = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.EmployeeID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(23, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(161, 30)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Edit Employee"
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.EmploymentType)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.EmploymentStatus)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.HireDate)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.DateOfBirth)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.WorkShift)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Salary)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.EmergencyContact)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Position)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.MaritalStatus)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Address)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Email)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.FirstName)
        Me.Panel2.Controls.Add(Me.ContactNumber)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.LastName)
        Me.Panel2.Controls.Add(Me.Gender)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.EmployeeID)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(20, 20, 20, 20)
        Me.Panel2.Size = New System.Drawing.Size(1005, 529)
        Me.Panel2.TabIndex = 1
        '
        'EmploymentType
        '
        Me.EmploymentType.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EmploymentType.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.EmploymentType.Location = New System.Drawing.Point(517, 552)
        Me.EmploymentType.Name = "EmploymentType"
        Me.EmploymentType.Size = New System.Drawing.Size(432, 25)
        Me.EmploymentType.TabIndex = 71
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label15.Location = New System.Drawing.Point(517, 526)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(36, 17)
        Me.Label15.TabIndex = 73
        Me.Label15.Text = "Type"
        '
        'EmploymentStatus
        '
        Me.EmploymentStatus.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EmploymentStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.EmploymentStatus.Location = New System.Drawing.Point(25, 614)
        Me.EmploymentStatus.Name = "EmploymentStatus"
        Me.EmploymentStatus.Size = New System.Drawing.Size(432, 25)
        Me.EmploymentStatus.TabIndex = 70
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label19.Location = New System.Drawing.Point(25, 588)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(46, 17)
        Me.Label19.TabIndex = 72
        Me.Label19.Text = "Status"
        '
        'HireDate
        '
        Me.HireDate.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.HireDate.Location = New System.Drawing.Point(517, 418)
        Me.HireDate.Name = "HireDate"
        Me.HireDate.Size = New System.Drawing.Size(432, 25)
        Me.HireDate.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(517, 392)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 17)
        Me.Label12.TabIndex = 69
        Me.Label12.Text = "Hire Date"
        '
        'DateOfBirth
        '
        Me.DateOfBirth.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.DateOfBirth.Location = New System.Drawing.Point(506, 84)
        Me.DateOfBirth.Name = "DateOfBirth"
        Me.DateOfBirth.Size = New System.Drawing.Size(432, 25)
        Me.DateOfBirth.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(506, 58)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 17)
        Me.Label9.TabIndex = 67
        Me.Label9.Text = "Date of Birth"
        '
        'WorkShift
        '
        Me.WorkShift.BackColor = System.Drawing.Color.WhiteSmoke
        Me.WorkShift.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.WorkShift.Location = New System.Drawing.Point(517, 484)
        Me.WorkShift.Name = "WorkShift"
        Me.WorkShift.Size = New System.Drawing.Size(432, 25)
        Me.WorkShift.TabIndex = 16
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label16.Location = New System.Drawing.Point(517, 458)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(72, 17)
        Me.Label16.TabIndex = 64
        Me.Label16.Text = "Work Shift"
        '
        'Salary
        '
        Me.Salary.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Salary.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Salary.Location = New System.Drawing.Point(25, 552)
        Me.Salary.Name = "Salary"
        Me.Salary.Size = New System.Drawing.Size(432, 25)
        Me.Salary.TabIndex = 15
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label17.Location = New System.Drawing.Point(25, 526)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(45, 17)
        Me.Label17.TabIndex = 62
        Me.Label17.Text = "Salary"
        '
        'EmergencyContact
        '
        Me.EmergencyContact.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EmergencyContact.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.EmergencyContact.Location = New System.Drawing.Point(23, 484)
        Me.EmergencyContact.Name = "EmergencyContact"
        Me.EmergencyContact.Size = New System.Drawing.Size(432, 25)
        Me.EmergencyContact.TabIndex = 14
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label18.Location = New System.Drawing.Point(23, 458)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(126, 17)
        Me.Label18.TabIndex = 60
        Me.Label18.Text = "Emergency Contact"
        '
        'Position
        '
        Me.Position.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Position.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Position.Location = New System.Drawing.Point(23, 418)
        Me.Position.Name = "Position"
        Me.Position.Size = New System.Drawing.Size(432, 25)
        Me.Position.TabIndex = 12
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label14.Location = New System.Drawing.Point(23, 392)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(57, 17)
        Me.Label14.TabIndex = 58
        Me.Label14.Text = "Position"
        '
        'MaritalStatus
        '
        Me.MaritalStatus.BackColor = System.Drawing.Color.WhiteSmoke
        Me.MaritalStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.MaritalStatus.Location = New System.Drawing.Point(506, 285)
        Me.MaritalStatus.Name = "MaritalStatus"
        Me.MaritalStatus.Size = New System.Drawing.Size(432, 25)
        Me.MaritalStatus.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(506, 259)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 17)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "Marital Status"
        '
        'Address
        '
        Me.Address.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Address.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Address.Location = New System.Drawing.Point(25, 347)
        Me.Address.Multiline = True
        Me.Address.Name = "Address"
        Me.Address.Size = New System.Drawing.Size(921, 25)
        Me.Address.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label13.Location = New System.Drawing.Point(25, 325)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(57, 17)
        Me.Label13.TabIndex = 54
        Me.Label13.Text = "Address"
        '
        'Email
        '
        Me.Email.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Email.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Email.Location = New System.Drawing.Point(506, 151)
        Me.Email.Name = "Email"
        Me.Email.Size = New System.Drawing.Size(432, 25)
        Me.Email.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(506, 125)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 17)
        Me.Label8.TabIndex = 49
        Me.Label8.Text = "Email"
        '
        'FirstName
        '
        Me.FirstName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FirstName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FirstName.Location = New System.Drawing.Point(25, 151)
        Me.FirstName.Name = "FirstName"
        Me.FirstName.Size = New System.Drawing.Size(432, 25)
        Me.FirstName.TabIndex = 2
        '
        'ContactNumber
        '
        Me.ContactNumber.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ContactNumber.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.ContactNumber.Location = New System.Drawing.Point(25, 285)
        Me.ContactNumber.Name = "ContactNumber"
        Me.ContactNumber.Size = New System.Drawing.Size(432, 25)
        Me.ContactNumber.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(25, 125)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 17)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "First Name"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(25, 259)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(109, 17)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "Contact Number"
        '
        'LastName
        '
        Me.LastName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LastName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.LastName.Location = New System.Drawing.Point(25, 218)
        Me.LastName.Name = "LastName"
        Me.LastName.Size = New System.Drawing.Size(432, 25)
        Me.LastName.TabIndex = 5
        '
        'Gender
        '
        Me.Gender.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Gender.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Gender.Location = New System.Drawing.Point(506, 218)
        Me.Gender.Name = "Gender"
        Me.Gender.Size = New System.Drawing.Size(432, 25)
        Me.Gender.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(25, 192)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 17)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Last Name"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(506, 192)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 17)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "Gender"
        '
        'EmployeeID
        '
        Me.EmployeeID.BackColor = System.Drawing.Color.LightGray
        Me.EmployeeID.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.EmployeeID.ForeColor = System.Drawing.Color.Gray
        Me.EmployeeID.Location = New System.Drawing.Point(25, 84)
        Me.EmployeeID.Name = "EmployeeID"
        Me.EmployeeID.ReadOnly = True
        Me.EmployeeID.Size = New System.Drawing.Size(432, 25)
        Me.EmployeeID.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(25, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 17)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Employee ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Label2.ForeColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(25, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(247, 17)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Update the employee information below."
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Controls.Add(Me.btnUpdate)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 529)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(20, 20, 20, 20)
        Me.Panel3.Size = New System.Drawing.Size(1005, 80)
        Me.Panel3.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(744, 20)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(110, 45)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdate.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnUpdate.ForeColor = System.Drawing.Color.White
        Me.btnUpdate.Location = New System.Drawing.Point(860, 20)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(125, 45)
        Me.btnUpdate.TabIndex = 17
        Me.btnUpdate.Text = "💾 Update"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'EditEmployee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1005, 609)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EditEmployee"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents EmployeeID As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents FirstName As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents LastName As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Email As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents DateOfBirth As DateTimePicker
    Friend WithEvents Label10 As Label
    Friend WithEvents ContactNumber As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Gender As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents HireDate As DateTimePicker
    Friend WithEvents Label13 As Label
    Friend WithEvents Address As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Position As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents WorkShift As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Salary As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents EmergencyContact As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents MaritalStatus As TextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents EmploymentType As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents EmploymentStatus As TextBox
    Friend WithEvents Label19 As Label
End Class