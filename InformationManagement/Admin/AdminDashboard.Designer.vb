<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminDashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminDashboard))
        Me.SidePanel = New System.Windows.Forms.Panel()
        Me.ReservationPayment = New System.Windows.Forms.Button()
        Me.OrderPayment = New System.Windows.Forms.Button()
        Me.Feedback = New System.Windows.Forms.Button()
        Me.Customer = New System.Windows.Forms.Button()
        Me.Employee = New System.Windows.Forms.Button()
        Me.Inventory = New System.Windows.Forms.Button()
        Me.btnReports = New System.Windows.Forms.Button()
        Me.btnPayroll = New System.Windows.Forms.Button()
        Me.btnOrders = New System.Windows.Forms.Button()
        Me.btnReservations = New System.Windows.Forms.Button()
        Me.btnMenuItems = New System.Windows.Forms.Button()
        Me.btnUserAccounts = New System.Windows.Forms.Button()
        Me.btnDashboard = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SidePanel.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SidePanel
        '
        Me.SidePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.SidePanel.Controls.Add(Me.ReservationPayment)
        Me.SidePanel.Controls.Add(Me.OrderPayment)
        Me.SidePanel.Controls.Add(Me.Feedback)
        Me.SidePanel.Controls.Add(Me.Customer)
        Me.SidePanel.Controls.Add(Me.Employee)
        Me.SidePanel.Controls.Add(Me.Inventory)
        Me.SidePanel.Controls.Add(Me.btnReports)
        Me.SidePanel.Controls.Add(Me.btnPayroll)
        Me.SidePanel.Controls.Add(Me.btnOrders)
        Me.SidePanel.Controls.Add(Me.btnReservations)
        Me.SidePanel.Controls.Add(Me.btnMenuItems)
        Me.SidePanel.Controls.Add(Me.btnUserAccounts)
        Me.SidePanel.Controls.Add(Me.btnDashboard)
        Me.SidePanel.Controls.Add(Me.Panel3)
        Me.SidePanel.Controls.Add(Me.Panel2)
        Me.SidePanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.SidePanel.Location = New System.Drawing.Point(0, 0)
        Me.SidePanel.Margin = New System.Windows.Forms.Padding(4)
        Me.SidePanel.Name = "SidePanel"
        Me.SidePanel.Size = New System.Drawing.Size(348, 877)
        Me.SidePanel.TabIndex = 0
        '
        'ReservationPayment
        '
        Me.ReservationPayment.FlatAppearance.BorderSize = 0
        Me.ReservationPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ReservationPayment.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReservationPayment.ForeColor = System.Drawing.Color.White
        Me.ReservationPayment.Image = CType(resources.GetObject("ReservationPayment.Image"), System.Drawing.Image)
        Me.ReservationPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ReservationPayment.Location = New System.Drawing.Point(26, 754)
        Me.ReservationPayment.Margin = New System.Windows.Forms.Padding(4)
        Me.ReservationPayment.Name = "ReservationPayment"
        Me.ReservationPayment.Size = New System.Drawing.Size(253, 34)
        Me.ReservationPayment.TabIndex = 14
        Me.ReservationPayment.Text = "     Reservation Payment"
        Me.ReservationPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ReservationPayment.UseVisualStyleBackColor = True
        '
        'OrderPayment
        '
        Me.OrderPayment.FlatAppearance.BorderSize = 0
        Me.OrderPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OrderPayment.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OrderPayment.ForeColor = System.Drawing.Color.White
        Me.OrderPayment.Image = CType(resources.GetObject("OrderPayment.Image"), System.Drawing.Image)
        Me.OrderPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OrderPayment.Location = New System.Drawing.Point(26, 702)
        Me.OrderPayment.Margin = New System.Windows.Forms.Padding(4)
        Me.OrderPayment.Name = "OrderPayment"
        Me.OrderPayment.Size = New System.Drawing.Size(253, 34)
        Me.OrderPayment.TabIndex = 13
        Me.OrderPayment.Text = "     Order Payement"
        Me.OrderPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.OrderPayment.UseVisualStyleBackColor = True
        '
        'Feedback
        '
        Me.Feedback.FlatAppearance.BorderSize = 0
        Me.Feedback.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Feedback.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Feedback.ForeColor = System.Drawing.Color.White
        Me.Feedback.Image = CType(resources.GetObject("Feedback.Image"), System.Drawing.Image)
        Me.Feedback.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Feedback.Location = New System.Drawing.Point(26, 651)
        Me.Feedback.Margin = New System.Windows.Forms.Padding(4)
        Me.Feedback.Name = "Feedback"
        Me.Feedback.Size = New System.Drawing.Size(253, 34)
        Me.Feedback.TabIndex = 12
        Me.Feedback.Text = "     Feedback"
        Me.Feedback.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Feedback.UseVisualStyleBackColor = True
        '
        'Customer
        '
        Me.Customer.FlatAppearance.BorderSize = 0
        Me.Customer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Customer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Customer.ForeColor = System.Drawing.Color.White
        Me.Customer.Image = CType(resources.GetObject("Customer.Image"), System.Drawing.Image)
        Me.Customer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Customer.Location = New System.Drawing.Point(26, 549)
        Me.Customer.Margin = New System.Windows.Forms.Padding(4)
        Me.Customer.Name = "Customer"
        Me.Customer.Size = New System.Drawing.Size(253, 34)
        Me.Customer.TabIndex = 11
        Me.Customer.Text = "    Customer"
        Me.Customer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Customer.UseVisualStyleBackColor = True
        '
        'Employee
        '
        Me.Employee.FlatAppearance.BorderSize = 0
        Me.Employee.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Employee.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Employee.ForeColor = System.Drawing.Color.White
        Me.Employee.Image = CType(resources.GetObject("Employee.Image"), System.Drawing.Image)
        Me.Employee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Employee.Location = New System.Drawing.Point(27, 498)
        Me.Employee.Margin = New System.Windows.Forms.Padding(4)
        Me.Employee.Name = "Employee"
        Me.Employee.Size = New System.Drawing.Size(253, 34)
        Me.Employee.TabIndex = 10
        Me.Employee.Text = "    Employee"
        Me.Employee.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Employee.UseVisualStyleBackColor = True
        '
        'Inventory
        '
        Me.Inventory.FlatAppearance.BorderSize = 0
        Me.Inventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Inventory.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Inventory.ForeColor = System.Drawing.Color.White
        Me.Inventory.Image = CType(resources.GetObject("Inventory.Image"), System.Drawing.Image)
        Me.Inventory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Inventory.Location = New System.Drawing.Point(26, 447)
        Me.Inventory.Margin = New System.Windows.Forms.Padding(4)
        Me.Inventory.Name = "Inventory"
        Me.Inventory.Size = New System.Drawing.Size(253, 34)
        Me.Inventory.TabIndex = 9
        Me.Inventory.Text = "     Inventory"
        Me.Inventory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Inventory.UseVisualStyleBackColor = True
        '
        'btnReports
        '
        Me.btnReports.FlatAppearance.BorderSize = 0
        Me.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReports.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReports.ForeColor = System.Drawing.Color.White
        Me.btnReports.Image = CType(resources.GetObject("btnReports.Image"), System.Drawing.Image)
        Me.btnReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReports.Location = New System.Drawing.Point(26, 600)
        Me.btnReports.Margin = New System.Windows.Forms.Padding(4)
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(253, 34)
        Me.btnReports.TabIndex = 7
        Me.btnReports.Text = "     Reports"
        Me.btnReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnReports.UseVisualStyleBackColor = True
        '
        'btnPayroll
        '
        Me.btnPayroll.FlatAppearance.BorderSize = 0
        Me.btnPayroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPayroll.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPayroll.ForeColor = System.Drawing.Color.White
        Me.btnPayroll.Image = CType(resources.GetObject("btnPayroll.Image"), System.Drawing.Image)
        Me.btnPayroll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPayroll.Location = New System.Drawing.Point(27, 393)
        Me.btnPayroll.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPayroll.Name = "btnPayroll"
        Me.btnPayroll.Size = New System.Drawing.Size(253, 34)
        Me.btnPayroll.TabIndex = 6
        Me.btnPayroll.Text = "     Payroll"
        Me.btnPayroll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPayroll.UseVisualStyleBackColor = True
        '
        'btnOrders
        '
        Me.btnOrders.FlatAppearance.BorderSize = 0
        Me.btnOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrders.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOrders.ForeColor = System.Drawing.Color.White
        Me.btnOrders.Image = CType(resources.GetObject("btnOrders.Image"), System.Drawing.Image)
        Me.btnOrders.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOrders.Location = New System.Drawing.Point(27, 337)
        Me.btnOrders.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOrders.Name = "btnOrders"
        Me.btnOrders.Size = New System.Drawing.Size(253, 34)
        Me.btnOrders.TabIndex = 5
        Me.btnOrders.Text = "     Orders"
        Me.btnOrders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnOrders.UseVisualStyleBackColor = True
        '
        'btnReservations
        '
        Me.btnReservations.FlatAppearance.BorderSize = 0
        Me.btnReservations.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReservations.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReservations.ForeColor = System.Drawing.Color.White
        Me.btnReservations.Image = CType(resources.GetObject("btnReservations.Image"), System.Drawing.Image)
        Me.btnReservations.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReservations.Location = New System.Drawing.Point(27, 278)
        Me.btnReservations.Margin = New System.Windows.Forms.Padding(4)
        Me.btnReservations.Name = "btnReservations"
        Me.btnReservations.Size = New System.Drawing.Size(253, 34)
        Me.btnReservations.TabIndex = 4
        Me.btnReservations.Text = "     Reservations"
        Me.btnReservations.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnReservations.UseVisualStyleBackColor = True
        '
        'btnMenuItems
        '
        Me.btnMenuItems.FlatAppearance.BorderSize = 0
        Me.btnMenuItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMenuItems.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMenuItems.ForeColor = System.Drawing.Color.White
        Me.btnMenuItems.Image = CType(resources.GetObject("btnMenuItems.Image"), System.Drawing.Image)
        Me.btnMenuItems.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMenuItems.Location = New System.Drawing.Point(27, 223)
        Me.btnMenuItems.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMenuItems.Name = "btnMenuItems"
        Me.btnMenuItems.Size = New System.Drawing.Size(253, 34)
        Me.btnMenuItems.TabIndex = 3
        Me.btnMenuItems.Text = "     Menu Items"
        Me.btnMenuItems.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnMenuItems.UseVisualStyleBackColor = True
        '
        'btnUserAccounts
        '
        Me.btnUserAccounts.FlatAppearance.BorderSize = 0
        Me.btnUserAccounts.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUserAccounts.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUserAccounts.ForeColor = System.Drawing.Color.White
        Me.btnUserAccounts.Image = CType(resources.GetObject("btnUserAccounts.Image"), System.Drawing.Image)
        Me.btnUserAccounts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUserAccounts.Location = New System.Drawing.Point(27, 175)
        Me.btnUserAccounts.Margin = New System.Windows.Forms.Padding(4)
        Me.btnUserAccounts.Name = "btnUserAccounts"
        Me.btnUserAccounts.Size = New System.Drawing.Size(253, 34)
        Me.btnUserAccounts.TabIndex = 2
        Me.btnUserAccounts.Text = "     User Accounts"
        Me.btnUserAccounts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnUserAccounts.UseVisualStyleBackColor = True
        '
        'btnDashboard
        '
        Me.btnDashboard.FlatAppearance.BorderSize = 0
        Me.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDashboard.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDashboard.ForeColor = System.Drawing.Color.White
        Me.btnDashboard.Image = Global.InformationManagement.My.Resources.Resources.dashboard__1_
        Me.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDashboard.Location = New System.Drawing.Point(27, 119)
        Me.btnDashboard.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDashboard.Name = "btnDashboard"
        Me.btnDashboard.Size = New System.Drawing.Size(253, 34)
        Me.btnDashboard.TabIndex = 0
        Me.btnDashboard.Text = "     Dashboard"
        Me.btnDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnDashboard.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnLogout)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 824)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(348, 53)
        Me.Panel3.TabIndex = 1
        '
        'btnLogout
        '
        Me.btnLogout.FlatAppearance.BorderSize = 0
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Image = CType(resources.GetObject("btnLogout.Image"), System.Drawing.Image)
        Me.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogout.Location = New System.Drawing.Point(25, 12)
        Me.btnLogout.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(253, 34)
        Me.btnLogout.TabIndex = 8
        Me.btnLogout.Text = "     Logout"
        Me.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLogout.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(348, 80)
        Me.Panel2.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(31, 22)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(249, 37)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Admin Dashboard"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(348, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(719, 877)
        Me.Panel1.TabIndex = 1
        '
        'AdminDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1067, 877)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.SidePanel)
        Me.DoubleBuffered = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "AdminDashboard"
        Me.Text = "AdminDashboard"
        Me.SidePanel.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SidePanel As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnDashboard As Button
    Friend WithEvents btnUserAccounts As Button
    Friend WithEvents btnReports As Button
    Friend WithEvents btnPayroll As Button
    Friend WithEvents btnOrders As Button
    Friend WithEvents btnReservations As Button
    Friend WithEvents btnMenuItems As Button
    Friend WithEvents btnLogout As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Inventory As Button
    Friend WithEvents Employee As Button
    Friend WithEvents Customer As Button
    Friend WithEvents Feedback As Button
    Friend WithEvents ReservationPayment As Button
    Friend WithEvents OrderPayment As Button
End Class
