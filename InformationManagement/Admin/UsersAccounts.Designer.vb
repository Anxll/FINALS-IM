<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UsersAccounts
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UsersAccounts))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UsersAccountData = New System.Windows.Forms.DataGridView()
        Me.txtName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUsername = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRole = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colJoinDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEdit = New System.Windows.Forms.DataGridViewImageColumn()
        Me.colDelete = New System.Windows.Forms.DataGridViewImageColumn()
        Me.RoundedPane22 = New InformationManagement.RoundedPane2()
        Me.lblStaffs = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Adduserbtn = New System.Windows.Forms.Button()
        Me.PaginationPanel = New System.Windows.Forms.Panel()
        Me.btnFirstPage = New System.Windows.Forms.Button()
        Me.btnPreviousPage = New System.Windows.Forms.Button()
        Me.lblPageInfo = New System.Windows.Forms.Label()
        Me.btnNextPage = New System.Windows.Forms.Button()
        Me.btnLastPage = New System.Windows.Forms.Button()
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewImageColumn2 = New System.Windows.Forms.DataGridViewImageColumn()
        CType(Me.UsersAccountData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RoundedPane22.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PaginationPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(29, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(226, 41)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Staff Members"
        '
        'UsersAccountData
        '
        Me.UsersAccountData.AllowUserToAddRows = False
        Me.UsersAccountData.AllowUserToDeleteRows = False
        Me.UsersAccountData.AllowUserToResizeColumns = False
        Me.UsersAccountData.AllowUserToResizeRows = False
        Me.UsersAccountData.BackgroundColor = System.Drawing.Color.White
        Me.UsersAccountData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.UsersAccountData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.UsersAccountData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.UsersAccountData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.UsersAccountData.ColumnHeadersHeight = 45
        Me.UsersAccountData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.UsersAccountData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.txtName, Me.colUsername, Me.colRole, Me.colStatus, Me.colJoinDate, Me.colEdit, Me.colDelete})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.UsersAccountData.DefaultCellStyle = DataGridViewCellStyle2
        Me.UsersAccountData.EnableHeadersVisualStyles = False
        Me.UsersAccountData.GridColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.UsersAccountData.Location = New System.Drawing.Point(28, 223)
        Me.UsersAccountData.Name = "UsersAccountData"
        Me.UsersAccountData.ReadOnly = True
        Me.UsersAccountData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.UsersAccountData.RowHeadersVisible = False
        Me.UsersAccountData.RowHeadersWidth = 55
        Me.UsersAccountData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(5)
        Me.UsersAccountData.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.UsersAccountData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White
        Me.UsersAccountData.RowTemplate.Height = 40
        Me.UsersAccountData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.UsersAccountData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.UsersAccountData.Size = New System.Drawing.Size(1051, 250)
        Me.UsersAccountData.TabIndex = 9
        '
        'txtName
        '
        Me.txtName.HeaderText = "Name"
        Me.txtName.MinimumWidth = 6
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = True
        Me.txtName.Width = 200
        '
        'colUsername
        '
        Me.colUsername.HeaderText = "Username"
        Me.colUsername.MinimumWidth = 6
        Me.colUsername.Name = "colUsername"
        Me.colUsername.ReadOnly = True
        Me.colUsername.Width = 180
        '
        'colRole
        '
        Me.colRole.HeaderText = "Role"
        Me.colRole.MinimumWidth = 6
        Me.colRole.Name = "colRole"
        Me.colRole.ReadOnly = True
        Me.colRole.Width = 180
        '
        'colStatus
        '
        Me.colStatus.HeaderText = "Status"
        Me.colStatus.MinimumWidth = 6
        Me.colStatus.Name = "colStatus"
        Me.colStatus.ReadOnly = True
        Me.colStatus.Width = 150
        '
        'colJoinDate
        '
        Me.colJoinDate.HeaderText = "Join Date"
        Me.colJoinDate.MinimumWidth = 6
        Me.colJoinDate.Name = "colJoinDate"
        Me.colJoinDate.ReadOnly = True
        Me.colJoinDate.Width = 180
        '
        'colEdit
        '
        Me.colEdit.HeaderText = "Edit"
        Me.colEdit.Image = Global.InformationManagement.My.Resources.Resources.edit
        Me.colEdit.MinimumWidth = 6
        Me.colEdit.Name = "colEdit"
        Me.colEdit.ReadOnly = True
        Me.colEdit.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colEdit.Width = 80
        '
        'colDelete
        '
        Me.colDelete.HeaderText = "Delete"
        Me.colDelete.Image = Global.InformationManagement.My.Resources.Resources.delete
        Me.colDelete.MinimumWidth = 6
        Me.colDelete.Name = "colDelete"
        Me.colDelete.ReadOnly = True
        Me.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colDelete.Width = 80
        '
        'RoundedPane22
        '
        Me.RoundedPane22.BorderColor = System.Drawing.Color.LightGray
        Me.RoundedPane22.BorderThickness = 1
        Me.RoundedPane22.Controls.Add(Me.lblStaffs)
        Me.RoundedPane22.Controls.Add(Me.Label6)
        Me.RoundedPane22.Controls.Add(Me.PictureBox5)
        Me.RoundedPane22.CornerRadius = 15
        Me.RoundedPane22.FillColor = System.Drawing.Color.White
        Me.RoundedPane22.Location = New System.Drawing.Point(28, 80)
        Me.RoundedPane22.Name = "RoundedPane22"
        Me.RoundedPane22.Size = New System.Drawing.Size(280, 90)
        Me.RoundedPane22.TabIndex = 12
        '
        'lblStaffs
        '
        Me.lblStaffs.AutoSize = True
        Me.lblStaffs.BackColor = System.Drawing.Color.Transparent
        Me.lblStaffs.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStaffs.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblStaffs.Location = New System.Drawing.Point(23, 44)
        Me.lblStaffs.Name = "lblStaffs"
        Me.lblStaffs.Size = New System.Drawing.Size(40, 46)
        Me.lblStaffs.TabIndex = 3
        Me.lblStaffs.Text = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(14, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(162, 25)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Total Staff Count"
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(229, 28)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(35, 35)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 1
        Me.PictureBox5.TabStop = False
        '
        'Adduserbtn
        '
        Me.Adduserbtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.Adduserbtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Adduserbtn.FlatAppearance.BorderSize = 0
        Me.Adduserbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Adduserbtn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Adduserbtn.ForeColor = System.Drawing.Color.White
        Me.Adduserbtn.Location = New System.Drawing.Point(1014, 140)
        Me.Adduserbtn.Name = "Adduserbtn"
        Me.Adduserbtn.Size = New System.Drawing.Size(65, 45)
        Me.Adduserbtn.TabIndex = 14
        Me.Adduserbtn.Text = "+"
        Me.Adduserbtn.UseVisualStyleBackColor = False
        '
        'PaginationPanel
        '
        Me.PaginationPanel.BackColor = System.Drawing.Color.White
        Me.PaginationPanel.Controls.Add(Me.btnFirstPage)
        Me.PaginationPanel.Controls.Add(Me.btnPreviousPage)
        Me.PaginationPanel.Controls.Add(Me.lblPageInfo)
        Me.PaginationPanel.Controls.Add(Me.btnNextPage)
        Me.PaginationPanel.Controls.Add(Me.btnLastPage)
        Me.PaginationPanel.Location = New System.Drawing.Point(28, 460)
        Me.PaginationPanel.Name = "PaginationPanel"
        Me.PaginationPanel.Size = New System.Drawing.Size(1051, 50)
        Me.PaginationPanel.TabIndex = 15
        '
        'btnFirstPage
        '
        Me.btnFirstPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.btnFirstPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFirstPage.FlatAppearance.BorderSize = 0
        Me.btnFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFirstPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFirstPage.Location = New System.Drawing.Point(340, 10)
        Me.btnFirstPage.Name = "btnFirstPage"
        Me.btnFirstPage.Size = New System.Drawing.Size(70, 30)
        Me.btnFirstPage.TabIndex = 0
        Me.btnFirstPage.Text = "First"
        Me.btnFirstPage.UseVisualStyleBackColor = False
        '
        'btnPreviousPage
        '
        Me.btnPreviousPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.btnPreviousPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPreviousPage.FlatAppearance.BorderSize = 0
        Me.btnPreviousPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreviousPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreviousPage.Location = New System.Drawing.Point(420, 10)
        Me.btnPreviousPage.Name = "btnPreviousPage"
        Me.btnPreviousPage.Size = New System.Drawing.Size(70, 30)
        Me.btnPreviousPage.TabIndex = 1
        Me.btnPreviousPage.Text = "Prev"
        Me.btnPreviousPage.UseVisualStyleBackColor = False
        '
        'lblPageInfo
        '
        Me.lblPageInfo.AutoSize = True
        Me.lblPageInfo.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPageInfo.Location = New System.Drawing.Point(500, 16)
        Me.lblPageInfo.MinimumSize = New System.Drawing.Size(80, 0)
        Me.lblPageInfo.Name = "lblPageInfo"
        Me.lblPageInfo.Size = New System.Drawing.Size(94, 21)
        Me.lblPageInfo.TabIndex = 2
        Me.lblPageInfo.Text = "Page 1 of 1"
        Me.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnNextPage
        '
        Me.btnNextPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNextPage.FlatAppearance.BorderSize = 0
        Me.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNextPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNextPage.Location = New System.Drawing.Point(590, 10)
        Me.btnNextPage.Name = "btnNextPage"
        Me.btnNextPage.Size = New System.Drawing.Size(70, 30)
        Me.btnNextPage.TabIndex = 3
        Me.btnNextPage.Text = "Next"
        Me.btnNextPage.UseVisualStyleBackColor = False
        '
        'btnLastPage
        '
        Me.btnLastPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.btnLastPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLastPage.FlatAppearance.BorderSize = 0
        Me.btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLastPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLastPage.Location = New System.Drawing.Point(670, 10)
        Me.btnLastPage.Name = "btnLastPage"
        Me.btnLastPage.Size = New System.Drawing.Size(70, 30)
        Me.btnLastPage.TabIndex = 4
        Me.btnLastPage.Text = "Last"
        Me.btnLastPage.UseVisualStyleBackColor = False
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.HeaderText = "Edit"
        Me.DataGridViewImageColumn1.Image = Global.InformationManagement.My.Resources.Resources.edit
        Me.DataGridViewImageColumn1.MinimumWidth = 6
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        Me.DataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewImageColumn1.Width = 80
        '
        'DataGridViewImageColumn2
        '
        Me.DataGridViewImageColumn2.HeaderText = "Delete"
        Me.DataGridViewImageColumn2.Image = Global.InformationManagement.My.Resources.Resources.delete
        Me.DataGridViewImageColumn2.MinimumWidth = 6
        Me.DataGridViewImageColumn2.Name = "DataGridViewImageColumn2"
        Me.DataGridViewImageColumn2.ReadOnly = True
        Me.DataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewImageColumn2.Width = 80
        '
        'UsersAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1121, 553)
        Me.Controls.Add(Me.PaginationPanel)
        Me.Controls.Add(Me.Adduserbtn)
        Me.Controls.Add(Me.RoundedPane22)
        Me.Controls.Add(Me.UsersAccountData)
        Me.Controls.Add(Me.Label1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "UsersAccounts"
        Me.Text = "Staff Management"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.UsersAccountData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RoundedPane22.ResumeLayout(False)
        Me.RoundedPane22.PerformLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PaginationPanel.ResumeLayout(False)
        Me.PaginationPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents UsersAccountData As DataGridView
    Friend WithEvents txtName As DataGridViewTextBoxColumn
    Friend WithEvents colUsername As DataGridViewTextBoxColumn
    Friend WithEvents colRole As DataGridViewTextBoxColumn
    Friend WithEvents colStatus As DataGridViewTextBoxColumn
    Friend WithEvents colJoinDate As DataGridViewTextBoxColumn
    Friend WithEvents colEdit As DataGridViewImageColumn
    Friend WithEvents colDelete As DataGridViewImageColumn
    Friend WithEvents RoundedPane22 As RoundedPane2
    Friend WithEvents Label6 As Label
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents lblStaffs As Label
    Friend WithEvents Adduserbtn As Button
    Friend WithEvents PaginationPanel As Panel
    Friend WithEvents btnFirstPage As Button
    Friend WithEvents btnPreviousPage As Button
    Friend WithEvents lblPageInfo As Label
    Friend WithEvents btnNextPage As Button
    Friend WithEvents btnLastPage As Button
    Friend WithEvents DataGridViewImageColumn1 As DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn2 As DataGridViewImageColumn
End Class