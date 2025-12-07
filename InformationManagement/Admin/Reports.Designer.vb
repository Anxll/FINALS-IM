<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Reports
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnSales = New System.Windows.Forms.Button()
        Me.btnOrders = New System.Windows.Forms.Button()
        Me.btnPayroll = New System.Windows.Forms.Button()
        Me.btnCatering = New System.Windows.Forms.Button()
        Me.btnStatus = New System.Windows.Forms.Button()
        Me.btnDineIn = New System.Windows.Forms.Button()
        Me.btnTakeout = New System.Windows.Forms.Button()
        Me.btnCustomerHistory = New System.Windows.Forms.Button()
        Me.btnEmployeeAttendance = New System.Windows.Forms.Button()
        Me.btnProductsPerformance = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.reportPeriod = New System.Windows.Forms.ComboBox()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(30, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(324, 40)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Reports  and Analytics"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.AutoSize = True
        Me.Panel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Panel1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(-4, 166)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1268, 787)
        Me.Panel1.TabIndex = 3
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AccessibleRole = System.Windows.Forms.AccessibleRole.PageTabList
        Me.FlowLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.LightGray
        Me.FlowLayoutPanel1.Controls.Add(Me.btnSales)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnOrders)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnPayroll)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnCatering)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnStatus)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnDineIn)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnTakeout)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnCustomerHistory)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnEmployeeAttendance)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnProductsPerformance)
        Me.FlowLayoutPanel1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(12, 85)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(10, 5, 10, 5)
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(2840, 50)
        Me.FlowLayoutPanel1.TabIndex = 4
        Me.FlowLayoutPanel1.WrapContents = False
        '
        'btnSales
        '
        Me.btnSales.AutoSize = True
        Me.btnSales.BackColor = System.Drawing.Color.Transparent
        Me.btnSales.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSales.FlatAppearance.BorderSize = 0
        Me.btnSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSales.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSales.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSales.Location = New System.Drawing.Point(16, 5)
        Me.btnSales.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnSales.Name = "btnSales"
        Me.btnSales.Padding = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.btnSales.Size = New System.Drawing.Size(121, 45)
        Me.btnSales.TabIndex = 0
        Me.btnSales.Text = "Sales"
        Me.btnSales.UseVisualStyleBackColor = False
        '
        'btnOrders
        '
        Me.btnOrders.AutoSize = True
        Me.btnOrders.BackColor = System.Drawing.Color.Transparent
        Me.btnOrders.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOrders.FlatAppearance.BorderSize = 0
        Me.btnOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrders.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnOrders.Location = New System.Drawing.Point(149, 5)
        Me.btnOrders.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnOrders.Name = "btnOrders"
        Me.btnOrders.Padding = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.btnOrders.Size = New System.Drawing.Size(121, 45)
        Me.btnOrders.TabIndex = 1
        Me.btnOrders.Text = "Orders"
        Me.btnOrders.UseVisualStyleBackColor = False
        '
        'btnPayroll
        '
        Me.btnPayroll.AutoSize = True
        Me.btnPayroll.BackColor = System.Drawing.Color.Transparent
        Me.btnPayroll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPayroll.FlatAppearance.BorderSize = 0
        Me.btnPayroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPayroll.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnPayroll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnPayroll.Location = New System.Drawing.Point(282, 5)
        Me.btnPayroll.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnPayroll.Name = "btnPayroll"
        Me.btnPayroll.Padding = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.btnPayroll.Size = New System.Drawing.Size(121, 45)
        Me.btnPayroll.TabIndex = 2
        Me.btnPayroll.Text = "Payroll"
        Me.btnPayroll.UseVisualStyleBackColor = False
        '
        'btnCatering
        '
        Me.btnCatering.AutoSize = True
        Me.btnCatering.BackColor = System.Drawing.Color.Transparent
        Me.btnCatering.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCatering.FlatAppearance.BorderSize = 0
        Me.btnCatering.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatering.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnCatering.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnCatering.Location = New System.Drawing.Point(415, 5)
        Me.btnCatering.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnCatering.Name = "btnCatering"
        Me.btnCatering.Padding = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.btnCatering.Size = New System.Drawing.Size(219, 45)
        Me.btnCatering.TabIndex = 3
        Me.btnCatering.Text = "Catering Reservations"
        Me.btnCatering.UseVisualStyleBackColor = False
        '
        'btnStatus
        '
        Me.btnStatus.AutoSize = True
        Me.btnStatus.BackColor = System.Drawing.Color.Transparent
        Me.btnStatus.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStatus.FlatAppearance.BorderSize = 0
        Me.btnStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnStatus.Location = New System.Drawing.Point(646, 5)
        Me.btnStatus.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnStatus.Name = "btnStatus"
        Me.btnStatus.Padding = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.btnStatus.Size = New System.Drawing.Size(193, 45)
        Me.btnStatus.TabIndex = 4
        Me.btnStatus.Text = "Reservation Status"
        Me.btnStatus.UseVisualStyleBackColor = False
        '
        'btnDineIn
        '
        Me.btnDineIn.AutoSize = True
        Me.btnDineIn.BackColor = System.Drawing.Color.Transparent
        Me.btnDineIn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDineIn.FlatAppearance.BorderSize = 0
        Me.btnDineIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDineIn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDineIn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnDineIn.Location = New System.Drawing.Point(851, 5)
        Me.btnDineIn.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnDineIn.Name = "btnDineIn"
        Me.btnDineIn.Padding = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.btnDineIn.Size = New System.Drawing.Size(162, 45)
        Me.btnDineIn.TabIndex = 5
        Me.btnDineIn.Text = "Dine-in Orders"
        Me.btnDineIn.UseVisualStyleBackColor = False
        '
        'btnTakeout
        '
        Me.btnTakeout.AutoSize = True
        Me.btnTakeout.BackColor = System.Drawing.Color.Transparent
        Me.btnTakeout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTakeout.FlatAppearance.BorderSize = 0
        Me.btnTakeout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTakeout.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnTakeout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnTakeout.Location = New System.Drawing.Point(1025, 5)
        Me.btnTakeout.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnTakeout.Name = "btnTakeout"
        Me.btnTakeout.Padding = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.btnTakeout.Size = New System.Drawing.Size(167, 45)
        Me.btnTakeout.TabIndex = 6
        Me.btnTakeout.Text = "Takeout Orders"
        Me.btnTakeout.UseVisualStyleBackColor = False
        '
        'btnCustomerHistory
        '
        Me.btnCustomerHistory.AutoSize = True
        Me.btnCustomerHistory.BackColor = System.Drawing.Color.Transparent
        Me.btnCustomerHistory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCustomerHistory.FlatAppearance.BorderSize = 0
        Me.btnCustomerHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerHistory.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnCustomerHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnCustomerHistory.Location = New System.Drawing.Point(1204, 5)
        Me.btnCustomerHistory.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnCustomerHistory.Name = "btnCustomerHistory"
        Me.btnCustomerHistory.Padding = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.btnCustomerHistory.Size = New System.Drawing.Size(185, 45)
        Me.btnCustomerHistory.TabIndex = 8
        Me.btnCustomerHistory.Text = "Customer History"
        Me.btnCustomerHistory.UseVisualStyleBackColor = False
        '
        'btnEmployeeAttendance
        '
        Me.btnEmployeeAttendance.AutoSize = True
        Me.btnEmployeeAttendance.BackColor = System.Drawing.Color.Transparent
        Me.btnEmployeeAttendance.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEmployeeAttendance.FlatAppearance.BorderSize = 0
        Me.btnEmployeeAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEmployeeAttendance.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnEmployeeAttendance.ForeColor = System.Drawing.Color.Black
        Me.btnEmployeeAttendance.Location = New System.Drawing.Point(1401, 5)
        Me.btnEmployeeAttendance.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnEmployeeAttendance.Name = "btnEmployeeAttendance"
        Me.btnEmployeeAttendance.Padding = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.btnEmployeeAttendance.Size = New System.Drawing.Size(219, 45)
        Me.btnEmployeeAttendance.TabIndex = 7
        Me.btnEmployeeAttendance.Text = "Employee Attendance"
        Me.btnEmployeeAttendance.UseVisualStyleBackColor = False
        '
        'btnProductsPerformance
        '
        Me.btnProductsPerformance.AutoSize = True
        Me.btnProductsPerformance.BackColor = System.Drawing.Color.Transparent
        Me.btnProductsPerformance.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnProductsPerformance.FlatAppearance.BorderSize = 0
        Me.btnProductsPerformance.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProductsPerformance.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnProductsPerformance.ForeColor = System.Drawing.Color.Black
        Me.btnProductsPerformance.Location = New System.Drawing.Point(1632, 5)
        Me.btnProductsPerformance.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.btnProductsPerformance.Name = "btnProductsPerformance"
        Me.btnProductsPerformance.Padding = New System.Windows.Forms.Padding(12, 6, 12, 6)
        Me.btnProductsPerformance.Size = New System.Drawing.Size(220, 45)
        Me.btnProductsPerformance.TabIndex = 7
        Me.btnProductsPerformance.Text = "Products Performance"
        Me.btnProductsPerformance.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(254, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(1147, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Period :"
        '
        'reportPeriod
        '
        Me.reportPeriod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.reportPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.reportPeriod.DropDownWidth = 115
        Me.reportPeriod.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.reportPeriod.FormattingEnabled = True
        Me.reportPeriod.ItemHeight = 20
        Me.reportPeriod.Items.AddRange(New Object() {"Daily", "Weekly", "Montly", "Yearly"})
        Me.reportPeriod.Location = New System.Drawing.Point(1220, 20)
        Me.reportPeriod.Name = "reportPeriod"
        Me.reportPeriod.Size = New System.Drawing.Size(121, 26)
        Me.reportPeriod.TabIndex = 6
        '
        'Reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.GhostWhite
        Me.ClientSize = New System.Drawing.Size(1370, 609)
        Me.Controls.Add(Me.reportPeriod)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MinimizeBox = False
        Me.Name = "Reports"
        Me.Text = "Reports"
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents btnSales As Button
    Friend WithEvents btnOrders As Button
    Friend WithEvents btnPayroll As Button
    Friend WithEvents btnCatering As Button
    Friend WithEvents btnStatus As Button
    Friend WithEvents btnDineIn As Button
    Friend WithEvents btnTakeout As Button
    Friend WithEvents btnCustomerHistory As Button
    Friend WithEvents btnEmployeeAttendance As Button
    Friend WithEvents btnProductsPerformance As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents reportPeriod As ComboBox
End Class
