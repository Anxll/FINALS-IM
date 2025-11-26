<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEmployeeAttendance
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEmployeeAttendance))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Employee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Position = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RegularHours = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OvertimeHours = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Absences = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RoundedPane21 = New InformationManagement.RoundedPane2()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RoundedPane21.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeight = 40
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Employee, Me.Position, Me.RegularHours, Me.OvertimeHours, Me.Absences, Me.Status})
        Me.DataGridView1.Location = New System.Drawing.Point(25, 118)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(997, 150)
        Me.DataGridView1.TabIndex = 10
        '
        'Employee
        '
        Me.Employee.HeaderText = "Employee"
        Me.Employee.Name = "Employee"
        Me.Employee.Width = 150
        '
        'Position
        '
        Me.Position.HeaderText = "Position"
        Me.Position.Name = "Position"
        Me.Position.Width = 150
        '
        'RegularHours
        '
        Me.RegularHours.HeaderText = "Regular Hours"
        Me.RegularHours.Name = "RegularHours"
        Me.RegularHours.Width = 150
        '
        'OvertimeHours
        '
        Me.OvertimeHours.HeaderText = "Overtime Hours"
        Me.OvertimeHours.Name = "OvertimeHours"
        Me.OvertimeHours.Width = 150
        '
        'Absences
        '
        Me.Absences.HeaderText = "Absences"
        Me.Absences.Name = "Absences"
        '
        'Status
        '
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.Width = 150
        '
        'RoundedPane21
        '
        Me.RoundedPane21.BorderColor = System.Drawing.Color.LightGray
        Me.RoundedPane21.BorderThickness = 1
        Me.RoundedPane21.Controls.Add(Me.DataGridView1)
        Me.RoundedPane21.Controls.Add(Me.Button1)
        Me.RoundedPane21.Controls.Add(Me.Label1)
        Me.RoundedPane21.CornerRadius = 15
        Me.RoundedPane21.FillColor = System.Drawing.Color.White
        Me.RoundedPane21.Location = New System.Drawing.Point(31, 22)
        Me.RoundedPane21.Name = "RoundedPane21"
        Me.RoundedPane21.Size = New System.Drawing.Size(1045, 306)
        Me.RoundedPane21.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(918, 18)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(104, 30)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "   Export"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(204, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Employee Attendance Report"
        '
        'FormEmployeeAttendance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.GhostWhite
        Me.ClientSize = New System.Drawing.Size(1151, 749)
        Me.Controls.Add(Me.RoundedPane21)
        Me.Name = "FormEmployeeAttendance"
        Me.Text = "FormEmployeeAttendance"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RoundedPane21.ResumeLayout(False)
        Me.RoundedPane21.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RoundedPane21 As RoundedPane2
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Employee As DataGridViewTextBoxColumn
    Friend WithEvents Position As DataGridViewTextBoxColumn
    Friend WithEvents RegularHours As DataGridViewTextBoxColumn
    Friend WithEvents OvertimeHours As DataGridViewTextBoxColumn
    Friend WithEvents Absences As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents DataGridView1 As DataGridView
End Class
