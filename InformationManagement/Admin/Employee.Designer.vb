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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.EditEmployee = New ReaLTaiizor.Controls.Button()
        Me.btnRefresh = New ReaLTaiizor.Controls.Button()
        Me.AddEmployee = New ReaLTaiizor.Controls.Button()
        Me.btnDelete = New ReaLTaiizor.Controls.Button()
        Me.txtSearch = New ReaLTaiizor.Controls.BigTextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnViewAll = New ReaLTaiizor.Controls.Button()
        Me.btnViewInactive = New ReaLTaiizor.Controls.Button()
        Me.btnViewActive = New ReaLTaiizor.Controls.Button()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblTotalEmployees = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1113, 63)
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
        Me.Panel2.Controls.Add(Me.EditEmployee)
        Me.Panel2.Controls.Add(Me.btnRefresh)
        Me.Panel2.Controls.Add(Me.AddEmployee)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.txtSearch)
        Me.Panel2.Controls.Add(Me.lblSearch)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 63)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(8, 9, 8, 9)
        Me.Panel2.Size = New System.Drawing.Size(1113, 81)
        Me.Panel2.TabIndex = 1
        '
        'EditEmployee
        '
        Me.EditEmployee.BackColor = System.Drawing.Color.Transparent
        Me.EditEmployee.BorderColor = System.Drawing.Color.Transparent
        Me.EditEmployee.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditEmployee.EnteredBorderColor = System.Drawing.Color.Transparent
        Me.EditEmployee.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.EditEmployee.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.EditEmployee.Image = Nothing
        Me.EditEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditEmployee.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.EditEmployee.Location = New System.Drawing.Point(859, 33)
        Me.EditEmployee.Name = "EditEmployee"
        Me.EditEmployee.PressedBorderColor = System.Drawing.Color.Transparent
        Me.EditEmployee.PressedColor = System.Drawing.Color.Green
        Me.EditEmployee.Size = New System.Drawing.Size(131, 36)
        Me.EditEmployee.TabIndex = 10
        Me.EditEmployee.Text = "Edit Employee"
        Me.EditEmployee.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.Transparent
        Me.btnRefresh.BorderColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Image = Nothing
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.btnRefresh.Location = New System.Drawing.Point(749, 33)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.Size = New System.Drawing.Size(104, 36)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'AddEmployee
        '
        Me.AddEmployee.BackColor = System.Drawing.Color.Transparent
        Me.AddEmployee.BorderColor = System.Drawing.Color.Transparent
        Me.AddEmployee.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AddEmployee.EnteredBorderColor = System.Drawing.Color.Transparent
        Me.AddEmployee.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.AddEmployee.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.AddEmployee.Image = Nothing
        Me.AddEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AddEmployee.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.AddEmployee.Location = New System.Drawing.Point(612, 33)
        Me.AddEmployee.Name = "AddEmployee"
        Me.AddEmployee.PressedBorderColor = System.Drawing.Color.Transparent
        Me.AddEmployee.PressedColor = System.Drawing.Color.Navy
        Me.AddEmployee.Size = New System.Drawing.Size(131, 36)
        Me.AddEmployee.TabIndex = 9
        Me.AddEmployee.Text = "➕ Add Employee"
        Me.AddEmployee.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.Transparent
        Me.btnDelete.BorderColor = System.Drawing.Color.Transparent
        Me.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDelete.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnDelete.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnDelete.Image = Nothing
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnDelete.Location = New System.Drawing.Point(996, 33)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnDelete.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnDelete.Size = New System.Drawing.Size(69, 36)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.Transparent
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.Image = Nothing
        Me.txtSearch.Location = New System.Drawing.Point(29, 31)
        Me.txtSearch.MaxLength = 32767
        Me.txtSearch.Multiline = False
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.ReadOnly = False
        Me.txtSearch.Size = New System.Drawing.Size(488, 38)
        Me.txtSearch.TabIndex = 6
        Me.txtSearch.Text = "Search..."
        Me.txtSearch.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSearch.UseSystemPasswordChar = False
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(28, 9)
        Me.lblSearch.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(51, 17)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search:"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.btnViewAll)
        Me.Panel3.Controls.Add(Me.btnViewInactive)
        Me.Panel3.Controls.Add(Me.btnViewActive)
        Me.Panel3.Controls.Add(Me.lblFilter)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 144)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(8, 9, 8, 9)
        Me.Panel3.Size = New System.Drawing.Size(1113, 48)
        Me.Panel3.TabIndex = 2
        '
        'btnViewAll
        '
        Me.btnViewAll.BackColor = System.Drawing.Color.Transparent
        Me.btnViewAll.BorderColor = System.Drawing.Color.Transparent
        Me.btnViewAll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnViewAll.EnteredBorderColor = System.Drawing.Color.Transparent
        Me.btnViewAll.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewAll.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewAll.Image = Nothing
        Me.btnViewAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewAll.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnViewAll.Location = New System.Drawing.Point(250, 5)
        Me.btnViewAll.Name = "btnViewAll"
        Me.btnViewAll.PressedBorderColor = System.Drawing.Color.Transparent
        Me.btnViewAll.PressedColor = System.Drawing.Color.Gray
        Me.btnViewAll.Size = New System.Drawing.Size(104, 36)
        Me.btnViewAll.TabIndex = 9
        Me.btnViewAll.Text = "All"
        Me.btnViewAll.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnViewInactive
        '
        Me.btnViewInactive.BackColor = System.Drawing.Color.Transparent
        Me.btnViewInactive.BorderColor = System.Drawing.Color.Transparent
        Me.btnViewInactive.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnViewInactive.EnteredBorderColor = System.Drawing.Color.Transparent
        Me.btnViewInactive.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewInactive.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewInactive.Image = Nothing
        Me.btnViewInactive.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewInactive.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnViewInactive.Location = New System.Drawing.Point(139, 5)
        Me.btnViewInactive.Name = "btnViewInactive"
        Me.btnViewInactive.PressedBorderColor = System.Drawing.Color.Transparent
        Me.btnViewInactive.PressedColor = System.Drawing.Color.Red
        Me.btnViewInactive.Size = New System.Drawing.Size(104, 36)
        Me.btnViewInactive.TabIndex = 10
        Me.btnViewInactive.Text = "InActive"
        Me.btnViewInactive.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnViewActive
        '
        Me.btnViewActive.BackColor = System.Drawing.Color.Transparent
        Me.btnViewActive.BorderColor = System.Drawing.Color.Transparent
        Me.btnViewActive.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnViewActive.EnteredBorderColor = System.Drawing.Color.Transparent
        Me.btnViewActive.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewActive.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewActive.Image = Nothing
        Me.btnViewActive.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewActive.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnViewActive.Location = New System.Drawing.Point(29, 5)
        Me.btnViewActive.Name = "btnViewActive"
        Me.btnViewActive.PressedBorderColor = System.Drawing.Color.Transparent
        Me.btnViewActive.PressedColor = System.Drawing.Color.Lime
        Me.btnViewActive.Size = New System.Drawing.Size(104, 36)
        Me.btnViewActive.TabIndex = 11
        Me.btnViewActive.Text = "Active"
        Me.btnViewActive.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblFilter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblFilter.Location = New System.Drawing.Point(828, 13)
        Me.lblFilter.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(83, 17)
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
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.EnableHeadersVisualStyles = False
        Me.DataGridView1.Location = New System.Drawing.Point(29, 196)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 35
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1036, 486)
        Me.DataGridView1.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel4.Controls.Add(Me.lblTotalEmployees)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 686)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1113, 26)
        Me.Panel4.TabIndex = 4
        '
        'lblTotalEmployees
        '
        Me.lblTotalEmployees.AutoSize = True
        Me.lblTotalEmployees.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalEmployees.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTotalEmployees.Location = New System.Drawing.Point(28, 6)
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
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1113, 712)
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
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblSearch As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lblFilter As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lblTotalEmployees As Label
    Friend WithEvents txtSearch As ReaLTaiizor.Controls.BigTextBox
    Friend WithEvents btnRefresh As ReaLTaiizor.Controls.Button
    Friend WithEvents btnDelete As ReaLTaiizor.Controls.Button
    Friend WithEvents btnViewAll As ReaLTaiizor.Controls.Button
    Friend WithEvents btnViewInactive As ReaLTaiizor.Controls.Button
    Friend WithEvents btnViewActive As ReaLTaiizor.Controls.Button
    Friend WithEvents EditEmployee As ReaLTaiizor.Controls.Button
    Friend WithEvents AddEmployee As ReaLTaiizor.Controls.Button
End Class