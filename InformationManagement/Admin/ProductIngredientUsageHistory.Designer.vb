Partial Class ProductIngredientUsageHistory
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.dgvUsageHistory = New System.Windows.Forms.DataGridView()
        Me.pnlPagination = New System.Windows.Forms.Panel()
        Me.btnFirstPage = New System.Windows.Forms.Button()
        Me.btnPreviousPage = New System.Windows.Forms.Button()
        Me.lblPageInfo = New System.Windows.Forms.Label()
        Me.btnNextPage = New System.Windows.Forms.Button()
        Me.btnLastPage = New System.Windows.Forms.Button()
        Me.lblPageSize = New System.Windows.Forms.Label()
        Me.cmbPageSize = New System.Windows.Forms.ComboBox()
        Me.pnlFilters = New System.Windows.Forms.Panel()
        Me.grpFilters = New System.Windows.Forms.GroupBox()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.lblEndDate = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.lblSource = New System.Windows.Forms.Label()
        Me.cmbSource = New System.Windows.Forms.ComboBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnApplyFilters = New System.Windows.Forms.Button()
        Me.btnResetFilters = New System.Windows.Forms.Button()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnClearHistory = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.dgvUsageHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPagination.SuspendLayout()
        Me.pnlFilters.SuspendLayout()
        Me.grpFilters.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.GhostWhite
        Me.pnlMain.Controls.Add(Me.pnlGrid)
        Me.pnlMain.Controls.Add(Me.pnlPagination)
        Me.pnlMain.Controls.Add(Me.pnlFilters)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Controls.Add(Me.pnlActions)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlMain.Size = New System.Drawing.Size(1200, 700)
        Me.pnlMain.TabIndex = 0
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.White
        Me.pnlGrid.Controls.Add(Me.dgvUsageHistory)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(15, 175)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlGrid.Size = New System.Drawing.Size(1170, 390)
        Me.pnlGrid.TabIndex = 0
        '
        'dgvUsageHistory
        '
        Me.dgvUsageHistory.AllowUserToAddRows = False
        Me.dgvUsageHistory.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgvUsageHistory.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvUsageHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvUsageHistory.BackgroundColor = System.Drawing.Color.White
        Me.dgvUsageHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvUsageHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvUsageHistory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Padding = New System.Windows.Forms.Padding(8)
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUsageHistory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvUsageHistory.ColumnHeadersHeight = 38
        Me.dgvUsageHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUsageHistory.EnableHeadersVisualStyles = False
        Me.dgvUsageHistory.GridColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvUsageHistory.Location = New System.Drawing.Point(10, 10)
        Me.dgvUsageHistory.Name = "dgvUsageHistory"
        Me.dgvUsageHistory.ReadOnly = True
        Me.dgvUsageHistory.RowHeadersVisible = False
        Me.dgvUsageHistory.RowTemplate.Height = 35
        Me.dgvUsageHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUsageHistory.Size = New System.Drawing.Size(1150, 370)
        Me.dgvUsageHistory.TabIndex = 0
        '
        'pnlPagination
        '
        Me.pnlPagination.BackColor = System.Drawing.Color.White
        Me.pnlPagination.Controls.Add(Me.btnFirstPage)
        Me.pnlPagination.Controls.Add(Me.btnPreviousPage)
        Me.pnlPagination.Controls.Add(Me.lblPageInfo)
        Me.pnlPagination.Controls.Add(Me.btnNextPage)
        Me.pnlPagination.Controls.Add(Me.btnLastPage)
        Me.pnlPagination.Controls.Add(Me.lblPageSize)
        Me.pnlPagination.Controls.Add(Me.cmbPageSize)
        Me.pnlPagination.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPagination.Location = New System.Drawing.Point(15, 565)
        Me.pnlPagination.Name = "pnlPagination"
        Me.pnlPagination.Padding = New System.Windows.Forms.Padding(10, 8, 10, 8)
        Me.pnlPagination.Size = New System.Drawing.Size(1170, 60)
        Me.pnlPagination.TabIndex = 4
        '
        'btnFirstPage
        '
        Me.btnFirstPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnFirstPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFirstPage.FlatAppearance.BorderSize = 0
        Me.btnFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFirstPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnFirstPage.ForeColor = System.Drawing.Color.White
        Me.btnFirstPage.Location = New System.Drawing.Point(10, 11)
        Me.btnFirstPage.Name = "btnFirstPage"
        Me.btnFirstPage.Size = New System.Drawing.Size(45, 38)
        Me.btnFirstPage.TabIndex = 0
        Me.btnFirstPage.Text = "<<"
        Me.btnFirstPage.UseVisualStyleBackColor = False
        '
        'btnPreviousPage
        '
        Me.btnPreviousPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnPreviousPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPreviousPage.FlatAppearance.BorderSize = 0
        Me.btnPreviousPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreviousPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreviousPage.ForeColor = System.Drawing.Color.White
        Me.btnPreviousPage.Location = New System.Drawing.Point(60, 11)
        Me.btnPreviousPage.Name = "btnPreviousPage"
        Me.btnPreviousPage.Size = New System.Drawing.Size(45, 38)
        Me.btnPreviousPage.TabIndex = 1
        Me.btnPreviousPage.Text = "<"
        Me.btnPreviousPage.UseVisualStyleBackColor = False
        '
        'lblPageInfo
        '
        Me.lblPageInfo.AutoSize = True
        Me.lblPageInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblPageInfo.Location = New System.Drawing.Point(115, 21)
        Me.lblPageInfo.Name = "lblPageInfo"
        Me.lblPageInfo.Size = New System.Drawing.Size(80, 15)
        Me.lblPageInfo.TabIndex = 2
        Me.lblPageInfo.Text = "Page 1 of 10"
        Me.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnNextPage
        '
        Me.btnNextPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNextPage.FlatAppearance.BorderSize = 0
        Me.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNextPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnNextPage.ForeColor = System.Drawing.Color.White
        Me.btnNextPage.Location = New System.Drawing.Point(205, 11)
        Me.btnNextPage.Name = "btnNextPage"
        Me.btnNextPage.Size = New System.Drawing.Size(45, 38)
        Me.btnNextPage.TabIndex = 3
        Me.btnNextPage.Text = ">"
        Me.btnNextPage.UseVisualStyleBackColor = False
        '
        'btnLastPage
        '
        Me.btnLastPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnLastPage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLastPage.FlatAppearance.BorderSize = 0
        Me.btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLastPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnLastPage.ForeColor = System.Drawing.Color.White
        Me.btnLastPage.Location = New System.Drawing.Point(255, 11)
        Me.btnLastPage.Name = "btnLastPage"
        Me.btnLastPage.Size = New System.Drawing.Size(45, 38)
        Me.btnLastPage.TabIndex = 4
        Me.btnLastPage.Text = ">>"
        Me.btnLastPage.UseVisualStyleBackColor = False
        '
        'lblPageSize
        '
        Me.lblPageSize.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblPageSize.AutoSize = True
        Me.lblPageSize.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPageSize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblPageSize.Location = New System.Drawing.Point(950, 21)
        Me.lblPageSize.Name = "lblPageSize"
        Me.lblPageSize.Size = New System.Drawing.Size(89, 15)
        Me.lblPageSize.TabIndex = 5
        Me.lblPageSize.Text = "Records/Page:"
        '
        'cmbPageSize
        '
        Me.cmbPageSize.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPageSize.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cmbPageSize.FormattingEnabled = True
        Me.cmbPageSize.Location = New System.Drawing.Point(1045, 17)
        Me.cmbPageSize.Name = "cmbPageSize"
        Me.cmbPageSize.Size = New System.Drawing.Size(115, 23)
        Me.cmbPageSize.TabIndex = 6
        '
        'pnlFilters
        '
        Me.pnlFilters.BackColor = System.Drawing.Color.White
        Me.pnlFilters.Controls.Add(Me.grpFilters)
        Me.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilters.Location = New System.Drawing.Point(15, 70)
        Me.pnlFilters.Name = "pnlFilters"
        Me.pnlFilters.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlFilters.Size = New System.Drawing.Size(1170, 105)
        Me.pnlFilters.TabIndex = 1
        '
        'grpFilters
        '
        Me.grpFilters.Controls.Add(Me.lblStartDate)
        Me.grpFilters.Controls.Add(Me.dtpStartDate)
        Me.grpFilters.Controls.Add(Me.lblEndDate)
        Me.grpFilters.Controls.Add(Me.dtpEndDate)
        Me.grpFilters.Controls.Add(Me.lblSource)
        Me.grpFilters.Controls.Add(Me.cmbSource)
        Me.grpFilters.Controls.Add(Me.lblSearch)
        Me.grpFilters.Controls.Add(Me.txtSearch)
        Me.grpFilters.Controls.Add(Me.btnApplyFilters)
        Me.grpFilters.Controls.Add(Me.btnResetFilters)
        Me.grpFilters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpFilters.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.grpFilters.Location = New System.Drawing.Point(10, 10)
        Me.grpFilters.Name = "grpFilters"
        Me.grpFilters.Size = New System.Drawing.Size(1150, 85)
        Me.grpFilters.TabIndex = 0
        Me.grpFilters.TabStop = False
        Me.grpFilters.Text = "Filter Options"
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(10, 22)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(61, 15)
        Me.lblStartDate.TabIndex = 0
        Me.lblStartDate.Text = "Start Date:"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStartDate.Location = New System.Drawing.Point(10, 42)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(120, 23)
        Me.dtpStartDate.TabIndex = 1
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Location = New System.Drawing.Point(140, 22)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(57, 15)
        Me.lblEndDate.TabIndex = 2
        Me.lblEndDate.Text = "End Date:"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndDate.Location = New System.Drawing.Point(140, 42)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(120, 23)
        Me.dtpEndDate.TabIndex = 3
        '
        'lblSource
        '
        Me.lblSource.AutoSize = True
        Me.lblSource.Location = New System.Drawing.Point(270, 22)
        Me.lblSource.Name = "lblSource"
        Me.lblSource.Size = New System.Drawing.Size(46, 15)
        Me.lblSource.TabIndex = 4
        Me.lblSource.Text = "Source:"
        '
        'cmbSource
        '
        Me.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSource.Location = New System.Drawing.Point(270, 42)
        Me.cmbSource.Name = "cmbSource"
        Me.cmbSource.Size = New System.Drawing.Size(120, 23)
        Me.cmbSource.TabIndex = 5
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(400, 22)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(102, 15)
        Me.lblSearch.TabIndex = 6
        Me.lblSearch.Text = "Search Ingredient:"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(400, 42)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(160, 23)
        Me.txtSearch.TabIndex = 7
        '
        'btnApplyFilters
        '
        Me.btnApplyFilters.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnApplyFilters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnApplyFilters.FlatAppearance.BorderSize = 0
        Me.btnApplyFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApplyFilters.ForeColor = System.Drawing.Color.White
        Me.btnApplyFilters.Location = New System.Drawing.Point(575, 38)
        Me.btnApplyFilters.Name = "btnApplyFilters"
        Me.btnApplyFilters.Size = New System.Drawing.Size(100, 30)
        Me.btnApplyFilters.TabIndex = 8
        Me.btnApplyFilters.Text = "Apply"
        Me.btnApplyFilters.UseVisualStyleBackColor = False
        '
        'btnResetFilters
        '
        Me.btnResetFilters.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnResetFilters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnResetFilters.FlatAppearance.BorderSize = 0
        Me.btnResetFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetFilters.ForeColor = System.Drawing.Color.White
        Me.btnResetFilters.Location = New System.Drawing.Point(685, 38)
        Me.btnResetFilters.Name = "btnResetFilters"
        Me.btnResetFilters.Size = New System.Drawing.Size(100, 30)
        Me.btnResetFilters.TabIndex = 9
        Me.btnResetFilters.Text = "Reset"
        Me.btnResetFilters.UseVisualStyleBackColor = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.GhostWhite
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblSubtitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(15, 15)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(15, 10, 15, 10)
        Me.pnlHeader.Size = New System.Drawing.Size(1170, 55)
        Me.pnlHeader.TabIndex = 2
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(10, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(236, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Ingredient Usage History"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSubtitle.Location = New System.Drawing.Point(12, 33)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(313, 15)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "Track which ingredients were used by products and orders"
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.White
        Me.pnlActions.Controls.Add(Me.btnClearHistory)
        Me.pnlActions.Controls.Add(Me.btnRefresh)
        Me.pnlActions.Controls.Add(Me.btnClose)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(15, 625)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlActions.Size = New System.Drawing.Size(1170, 60)
        Me.pnlActions.TabIndex = 3
        '
        'btnClearHistory
        '
        Me.btnClearHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnClearHistory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearHistory.FlatAppearance.BorderSize = 0
        Me.btnClearHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearHistory.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClearHistory.ForeColor = System.Drawing.Color.White
        Me.btnClearHistory.Location = New System.Drawing.Point(10, 10)
        Me.btnClearHistory.Name = "btnClearHistory"
        Me.btnClearHistory.Size = New System.Drawing.Size(130, 38)
        Me.btnClearHistory.TabIndex = 0
        Me.btnClearHistory.Text = "🗑️ Clear History"
        Me.btnClearHistory.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(150, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(110, 38)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "🔄 Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(1050, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(110, 38)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'ProductIngredientUsageHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ProductIngredientUsageHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ingredient Usage History"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.dgvUsageHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPagination.ResumeLayout(False)
        Me.pnlPagination.PerformLayout()
        Me.pnlFilters.ResumeLayout(False)
        Me.grpFilters.ResumeLayout(False)
        Me.grpFilters.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlActions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents pnlFilters As Panel
    Friend WithEvents grpFilters As GroupBox
    Friend WithEvents lblStartDate As Label
    Friend WithEvents dtpStartDate As DateTimePicker
    Friend WithEvents lblEndDate As Label
    Friend WithEvents dtpEndDate As DateTimePicker
    Friend WithEvents lblSource As Label
    Friend WithEvents cmbSource As ComboBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnApplyFilters As Button
    Friend WithEvents btnResetFilters As Button
    Friend WithEvents pnlGrid As Panel
    Friend WithEvents dgvUsageHistory As DataGridView
    Friend WithEvents pnlPagination As Panel
    Friend WithEvents btnFirstPage As Button
    Friend WithEvents btnPreviousPage As Button
    Friend WithEvents lblPageInfo As Label
    Friend WithEvents btnNextPage As Button
    Friend WithEvents btnLastPage As Button
    Friend WithEvents lblPageSize As Label
    Friend WithEvents cmbPageSize As ComboBox
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnClearHistory As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnClose As Button
End Class