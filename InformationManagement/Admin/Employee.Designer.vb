<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Employee
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.EditEmployee = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnUpdateStatus = New System.Windows.Forms.Button()
        Me.AddEmployee = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnViewAll = New System.Windows.Forms.Button()
        Me.btnViewInactive = New System.Windows.Forms.Button()
        Me.btnViewOnLeave = New System.Windows.Forms.Button()
        Me.btnViewActive = New System.Windows.Forms.Button()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.PaginationContainer = New System.Windows.Forms.Panel()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.lblPageStatus = New System.Windows.Forms.Label()
        Me.lblTotalEmployees = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.PaginationContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.lblTitle)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1080, 79)
        Me.Panel1.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(22, 21)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(343, 40)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Employee Management"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.EditEmployee)
        Me.Panel2.Controls.Add(Me.btnRefresh)
        Me.Panel2.Controls.Add(Me.btnUpdateStatus)
        Me.Panel2.Controls.Add(Me.AddEmployee)
        Me.Panel2.Controls.Add(Me.txtSearch)
        Me.Panel2.Controls.Add(Me.lblSearch)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 79)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(8, 9, 8, 9)
        Me.Panel2.Size = New System.Drawing.Size(1080, 63)
        Me.Panel2.TabIndex = 1
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnDelete.ForeColor = System.Drawing.Color.White
        Me.btnDelete.Location = New System.Drawing.Point(995, 13)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 30)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'EditEmployee
        '
        Me.EditEmployee.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EditEmployee.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.EditEmployee.FlatAppearance.BorderSize = 0
        Me.EditEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EditEmployee.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.EditEmployee.ForeColor = System.Drawing.Color.White
        Me.EditEmployee.Location = New System.Drawing.Point(885, 13)
        Me.EditEmployee.Margin = New System.Windows.Forms.Padding(2)
        Me.EditEmployee.Name = "EditEmployee"
        Me.EditEmployee.Size = New System.Drawing.Size(103, 30)
        Me.EditEmployee.TabIndex = 4
        Me.EditEmployee.Text = "Edit Employee"
        Me.EditEmployee.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(777, 13)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 30)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnUpdateStatus
        '
        Me.btnUpdateStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
        Me.btnUpdateStatus.FlatAppearance.BorderSize = 0
        Me.btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnUpdateStatus.ForeColor = System.Drawing.Color.White
        Me.btnUpdateStatus.Location = New System.Drawing.Point(523, 13)
        Me.btnUpdateStatus.Margin = New System.Windows.Forms.Padding(2)
        Me.btnUpdateStatus.Name = "btnUpdateStatus"
        Me.btnUpdateStatus.Size = New System.Drawing.Size(120, 30)
        Me.btnUpdateStatus.TabIndex = 7
        Me.btnUpdateStatus.Text = "Update Status"
        Me.btnUpdateStatus.UseVisualStyleBackColor = False
        '
        'AddEmployee
        '
        Me.AddEmployee.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddEmployee.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.AddEmployee.FlatAppearance.BorderSize = 0
        Me.AddEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddEmployee.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.AddEmployee.ForeColor = System.Drawing.Color.White
        Me.AddEmployee.Location = New System.Drawing.Point(651, 13)
        Me.AddEmployee.Margin = New System.Windows.Forms.Padding(2)
        Me.AddEmployee.Name = "AddEmployee"
        Me.AddEmployee.Size = New System.Drawing.Size(117, 30)
        Me.AddEmployee.TabIndex = 2
        Me.AddEmployee.Text = "➕ Add Employee"
        Me.AddEmployee.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtSearch.Location = New System.Drawing.Point(68, 15)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(567, 25)
        Me.txtSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSearch.Location = New System.Drawing.Point(11, 17)
        Me.lblSearch.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(58, 19)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search:"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.btnViewAll)
        Me.Panel3.Controls.Add(Me.btnViewInactive)
        Me.Panel3.Controls.Add(Me.btnViewOnLeave)
        Me.Panel3.Controls.Add(Me.btnViewActive)
        Me.Panel3.Controls.Add(Me.lblFilter)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 142)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(8, 9, 8, 9)
        Me.Panel3.Size = New System.Drawing.Size(1080, 48)
        Me.Panel3.TabIndex = 2
        '
        'btnViewAll
        '
        Me.btnViewAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnViewAll.FlatAppearance.BorderSize = 0
        Me.btnViewAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewAll.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnViewAll.ForeColor = System.Drawing.Color.White
        Me.btnViewAll.Location = New System.Drawing.Point(514, 9)
        Me.btnViewAll.Margin = New System.Windows.Forms.Padding(2)
        Me.btnViewAll.Name = "btnViewAll"
        Me.btnViewAll.Size = New System.Drawing.Size(100, 30)
        Me.btnViewAll.TabIndex = 3
        Me.btnViewAll.Text = "All"
        Me.btnViewAll.UseVisualStyleBackColor = False
        '
        'btnViewInactive
        '
        Me.btnViewInactive.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnViewInactive.FlatAppearance.BorderSize = 0
        Me.btnViewInactive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewInactive.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnViewInactive.ForeColor = System.Drawing.Color.White
        Me.btnViewInactive.Location = New System.Drawing.Point(410, 9)
        Me.btnViewInactive.Margin = New System.Windows.Forms.Padding(2)
        Me.btnViewInactive.Name = "btnViewInactive"
        Me.btnViewInactive.Size = New System.Drawing.Size(100, 30)
        Me.btnViewInactive.TabIndex = 2
        Me.btnViewInactive.Text = "Resigned"
        Me.btnViewInactive.UseVisualStyleBackColor = False
        '
        'btnViewOnLeave
        '
        Me.btnViewOnLeave.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
        Me.btnViewOnLeave.FlatAppearance.BorderSize = 0
        Me.btnViewOnLeave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewOnLeave.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnViewOnLeave.ForeColor = System.Drawing.Color.White
        Me.btnViewOnLeave.Location = New System.Drawing.Point(306, 9)
        Me.btnViewOnLeave.Margin = New System.Windows.Forms.Padding(2)
        Me.btnViewOnLeave.Name = "btnViewOnLeave"
        Me.btnViewOnLeave.Size = New System.Drawing.Size(100, 30)
        Me.btnViewOnLeave.TabIndex = 4
        Me.btnViewOnLeave.Text = "On Leave"
        Me.btnViewOnLeave.UseVisualStyleBackColor = False
        '
        'btnViewActive
        '
        Me.btnViewActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnViewActive.FlatAppearance.BorderSize = 0
        Me.btnViewActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewActive.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewActive.ForeColor = System.Drawing.Color.White
        Me.btnViewActive.Location = New System.Drawing.Point(202, 9)
        Me.btnViewActive.Margin = New System.Windows.Forms.Padding(2)
        Me.btnViewActive.Name = "btnViewActive"
        Me.btnViewActive.Size = New System.Drawing.Size(100, 30)
        Me.btnViewActive.TabIndex = 1
        Me.btnViewActive.Text = "Active"
        Me.btnViewActive.UseVisualStyleBackColor = False
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblFilter.Location = New System.Drawing.Point(11, 13)
        Me.lblFilter.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(91, 19)
        Me.lblFilter.TabIndex = 0
        Me.lblFilter.Text = "Filter Status:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeight = 40
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.EnableHeadersVisualStyles = False
        Me.DataGridView1.Location = New System.Drawing.Point(0, 190)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 35
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1080, 342)
        Me.DataGridView1.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel4.Controls.Add(Me.PaginationContainer)
        Me.Panel4.Controls.Add(Me.lblTotalEmployees)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 532)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1080, 40)
        Me.Panel4.TabIndex = 4
        '
        'PaginationContainer
        '
        Me.PaginationContainer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PaginationContainer.Controls.Add(Me.btnNext)
        Me.PaginationContainer.Controls.Add(Me.btnPrev)
        Me.PaginationContainer.Controls.Add(Me.lblPageStatus)
        Me.PaginationContainer.Location = New System.Drawing.Point(750, 2)
        Me.PaginationContainer.Name = "PaginationContainer"
        Me.PaginationContainer.Size = New System.Drawing.Size(320, 35)
        Me.PaginationContainer.TabIndex = 1
        '
        'btnNext
        '
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnNext.Location = New System.Drawing.Point(235, 2)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(80, 28)
        Me.btnNext.TabIndex = 2
        Me.btnNext.Text = "Next →"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnPrev
        '
        Me.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrev.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnPrev.Location = New System.Drawing.Point(150, 2)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(80, 28)
        Me.btnPrev.TabIndex = 1
        Me.btnPrev.Text = "← Prev"
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'lblPageStatus
        '
        Me.lblPageStatus.AutoSize = True
        Me.lblPageStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPageStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lblPageStatus.Location = New System.Drawing.Point(10, 8)
        Me.lblPageStatus.Name = "lblPageStatus"
        Me.lblPageStatus.Size = New System.Drawing.Size(65, 15)
        Me.lblPageStatus.TabIndex = 0
        Me.lblPageStatus.Text = "Page 1 of 1"
        '
        'lblTotalEmployees
        '
        Me.lblTotalEmployees.AutoSize = True
        Me.lblTotalEmployees.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalEmployees.Location = New System.Drawing.Point(10, 12)
        Me.lblTotalEmployees.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTotalEmployees.Name = "lblTotalEmployees"
        Me.lblTotalEmployees.Size = New System.Drawing.Size(109, 15)
        Me.lblTotalEmployees.TabIndex = 0
        Me.lblTotalEmployees.Text = "Total Employees: 0"
        '
        'Employee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1080, 572)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(688, 525)
        Me.Name = "Employee"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Employee Management - Tabeya"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.PaginationContainer.ResumeLayout(False)
        Me.PaginationContainer.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents AddEmployee As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents EditEmployee As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnUpdateStatus As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lblFilter As Label
    Friend WithEvents btnViewActive As Button
    Friend WithEvents btnViewOnLeave As Button
    Friend WithEvents btnViewInactive As Button
    Friend WithEvents btnViewAll As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lblTotalEmployees As Label
    Friend WithEvents PaginationContainer As Panel
    Friend WithEvents btnNext As Button
    Friend WithEvents btnPrev As Button
    Friend WithEvents lblPageStatus As Label
End Class