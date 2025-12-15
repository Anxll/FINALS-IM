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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.dtpStartDate = New ReaLTaiizor.Controls.PoisonDateTime()
        Me.dgvUsageHistory = New System.Windows.Forms.DataGridView()
        Me.pnlFilters = New System.Windows.Forms.Panel()
        Me.cmbSource = New ReaLTaiizor.Controls.ComboBoxEdit()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.txtSearch = New ReaLTaiizor.Controls.BigTextBox()
        Me.lblEndDate = New System.Windows.Forms.Label()
        Me.lblSource = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnClearHistory = New ReaLTaiizor.Controls.Button()
        Me.btnRefresh = New ReaLTaiizor.Controls.Button()
        Me.btnClose = New ReaLTaiizor.Controls.Button()
        Me.btnResetFilters = New ReaLTaiizor.Controls.Button()
        Me.btnApplyFilters = New ReaLTaiizor.Controls.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpEndDate = New ReaLTaiizor.Controls.PoisonDateTime()
        Me.pnlMain.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.dgvUsageHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilters.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.BackColor = System.Drawing.Color.GhostWhite
        Me.pnlMain.Controls.Add(Me.pnlGrid)
        Me.pnlMain.Controls.Add(Me.pnlFilters)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Controls.Add(Me.pnlActions)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlMain.Size = New System.Drawing.Size(1084, 661)
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
        Me.pnlGrid.Size = New System.Drawing.Size(1054, 423)
        Me.pnlGrid.TabIndex = 0
        '
        'dtpStartDate
        '
        Me.dtpStartDate.FontSize = ReaLTaiizor.Extension.Poison.PoisonDateTimeSize.Medium
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStartDate.Location = New System.Drawing.Point(30, 57)
        Me.dtpStartDate.MinimumSize = New System.Drawing.Size(0, 29)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(200, 29)
        Me.dtpStartDate.TabIndex = 9
        '
        'dgvUsageHistory
        '
        Me.dgvUsageHistory.AllowUserToAddRows = False
        Me.dgvUsageHistory.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgvUsageHistory.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvUsageHistory.BackgroundColor = System.Drawing.Color.White
        Me.dgvUsageHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvUsageHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvUsageHistory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(8)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUsageHistory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvUsageHistory.ColumnHeadersHeight = 38
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUsageHistory.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvUsageHistory.EnableHeadersVisualStyles = False
        Me.dgvUsageHistory.GridColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvUsageHistory.Location = New System.Drawing.Point(30, 0)
        Me.dgvUsageHistory.Name = "dgvUsageHistory"
        Me.dgvUsageHistory.ReadOnly = True
        Me.dgvUsageHistory.RowHeadersVisible = False
        Me.dgvUsageHistory.RowTemplate.Height = 35
        Me.dgvUsageHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUsageHistory.Size = New System.Drawing.Size(991, 417)
        Me.dgvUsageHistory.TabIndex = 0
        '
        'pnlFilters
        '
        Me.pnlFilters.BackColor = System.Drawing.Color.White
        Me.pnlFilters.Controls.Add(Me.lblSource)
        Me.pnlFilters.Controls.Add(Me.lblSearch)
        Me.pnlFilters.Controls.Add(Me.txtSearch)
        Me.pnlFilters.Controls.Add(Me.dtpEndDate)
        Me.pnlFilters.Controls.Add(Me.Label1)
        Me.pnlFilters.Controls.Add(Me.btnResetFilters)
        Me.pnlFilters.Controls.Add(Me.btnApplyFilters)
        Me.pnlFilters.Controls.Add(Me.dtpStartDate)
        Me.pnlFilters.Controls.Add(Me.cmbSource)
        Me.pnlFilters.Controls.Add(Me.lblEndDate)
        Me.pnlFilters.Controls.Add(Me.lblStartDate)
        Me.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilters.Location = New System.Drawing.Point(15, 70)
        Me.pnlFilters.Name = "pnlFilters"
        Me.pnlFilters.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlFilters.Size = New System.Drawing.Size(1054, 105)
        Me.pnlFilters.TabIndex = 1
        '
        'cmbSource
        '
        Me.cmbSource.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.cmbSource.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmbSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSource.DropDownHeight = 100
        Me.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSource.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbSource.ForeColor = System.Drawing.Color.White
        Me.cmbSource.FormattingEnabled = True
        Me.cmbSource.HoverSelectionColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.cmbSource.IntegralHeight = False
        Me.cmbSource.ItemHeight = 26
        Me.cmbSource.Location = New System.Drawing.Point(470, 54)
        Me.cmbSource.Name = "cmbSource"
        Me.cmbSource.Size = New System.Drawing.Size(202, 32)
        Me.cmbSource.StartIndex = 0
        Me.cmbSource.TabIndex = 8
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(27, 42)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(61, 13)
        Me.lblStartDate.TabIndex = 0
        Me.lblStartDate.Text = "Start Date:"
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.Transparent
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.Image = Nothing
        Me.txtSearch.Location = New System.Drawing.Point(690, 52)
        Me.txtSearch.MaxLength = 32767
        Me.txtSearch.Multiline = False
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.ReadOnly = False
        Me.txtSearch.Size = New System.Drawing.Size(186, 38)
        Me.txtSearch.TabIndex = 6
        Me.txtSearch.Text = "Search..."
        Me.txtSearch.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSearch.UseSystemPasswordChar = False
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Location = New System.Drawing.Point(252, 41)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(57, 13)
        Me.lblEndDate.TabIndex = 2
        Me.lblEndDate.Text = "End Date:"
        '
        'lblSource
        '
        Me.lblSource.AutoSize = True
        Me.lblSource.Location = New System.Drawing.Point(467, 40)
        Me.lblSource.Name = "lblSource"
        Me.lblSource.Size = New System.Drawing.Size(45, 13)
        Me.lblSource.TabIndex = 4
        Me.lblSource.Text = "Source:"
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(687, 36)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(101, 13)
        Me.lblSearch.TabIndex = 6
        Me.lblSearch.Text = "Search Ingredient:"
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
        Me.pnlHeader.Size = New System.Drawing.Size(1054, 55)
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
        Me.pnlActions.Controls.Add(Me.btnClose)
        Me.pnlActions.Controls.Add(Me.btnRefresh)
        Me.pnlActions.Controls.Add(Me.btnClearHistory)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(15, 598)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlActions.Size = New System.Drawing.Size(1054, 48)
        Me.pnlActions.TabIndex = 3
        '
        'btnClearHistory
        '
        Me.btnClearHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearHistory.BackColor = System.Drawing.Color.Transparent
        Me.btnClearHistory.BorderColor = System.Drawing.Color.Transparent
        Me.btnClearHistory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearHistory.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnClearHistory.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnClearHistory.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClearHistory.Image = Nothing
        Me.btnClearHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearHistory.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnClearHistory.Location = New System.Drawing.Point(30, 9)
        Me.btnClearHistory.Name = "btnClearHistory"
        Me.btnClearHistory.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnClearHistory.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnClearHistory.Size = New System.Drawing.Size(124, 36)
        Me.btnClearHistory.TabIndex = 15
        Me.btnClearHistory.Text = "Clear History"
        Me.btnClearHistory.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.Transparent
        Me.btnRefresh.BorderColor = System.Drawing.Color.Transparent
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Image = Nothing
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnRefresh.Location = New System.Drawing.Point(769, 9)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.Size = New System.Drawing.Size(124, 36)
        Me.btnRefresh.TabIndex = 16
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.BorderColor = System.Drawing.Color.Transparent
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnClose.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.Image = Nothing
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(897, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnClose.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnClose.Size = New System.Drawing.Size(124, 36)
        Me.btnClose.TabIndex = 17
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnResetFilters
        '
        Me.btnResetFilters.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnResetFilters.BackColor = System.Drawing.Color.Transparent
        Me.btnResetFilters.BorderColor = System.Drawing.Color.Transparent
        Me.btnResetFilters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnResetFilters.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnResetFilters.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnResetFilters.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnResetFilters.Image = Nothing
        Me.btnResetFilters.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnResetFilters.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnResetFilters.Location = New System.Drawing.Point(897, 57)
        Me.btnResetFilters.Name = "btnResetFilters"
        Me.btnResetFilters.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnResetFilters.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnResetFilters.Size = New System.Drawing.Size(124, 36)
        Me.btnResetFilters.TabIndex = 18
        Me.btnResetFilters.Text = "Reset"
        Me.btnResetFilters.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnApplyFilters
        '
        Me.btnApplyFilters.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApplyFilters.BackColor = System.Drawing.Color.Transparent
        Me.btnApplyFilters.BorderColor = System.Drawing.Color.Transparent
        Me.btnApplyFilters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnApplyFilters.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnApplyFilters.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnApplyFilters.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnApplyFilters.Image = Nothing
        Me.btnApplyFilters.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnApplyFilters.InactiveColor = System.Drawing.Color.RoyalBlue
        Me.btnApplyFilters.Location = New System.Drawing.Point(897, 13)
        Me.btnApplyFilters.Name = "btnApplyFilters"
        Me.btnApplyFilters.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnApplyFilters.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnApplyFilters.Size = New System.Drawing.Size(124, 36)
        Me.btnApplyFilters.TabIndex = 19
        Me.btnApplyFilters.Text = "Apply"
        Me.btnApplyFilters.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Filter Options"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.FontSize = ReaLTaiizor.Extension.Poison.PoisonDateTimeSize.Medium
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndDate.Location = New System.Drawing.Point(255, 57)
        Me.dtpEndDate.MinimumSize = New System.Drawing.Size(0, 29)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(200, 29)
        Me.dtpEndDate.TabIndex = 11
        '
        'ProductIngredientUsageHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1084, 661)
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
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlFilters.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlActions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents pnlFilters As Panel
    Friend WithEvents lblStartDate As Label
    Friend WithEvents lblEndDate As Label
    Friend WithEvents lblSource As Label
    Friend WithEvents lblSearch As Label
    Friend WithEvents pnlGrid As Panel
    Friend WithEvents dgvUsageHistory As DataGridView
    Friend WithEvents pnlActions As Panel
    Friend WithEvents txtSearch As ReaLTaiizor.Controls.BigTextBox
    Friend WithEvents dtpStartDate As ReaLTaiizor.Controls.PoisonDateTime
    Friend WithEvents cmbSource As ReaLTaiizor.Controls.ComboBoxEdit
    Friend WithEvents btnApplyFilters As ReaLTaiizor.Controls.Button
    Friend WithEvents btnResetFilters As ReaLTaiizor.Controls.Button
    Friend WithEvents btnClose As ReaLTaiizor.Controls.Button
    Friend WithEvents btnRefresh As ReaLTaiizor.Controls.Button
    Friend WithEvents btnClearHistory As ReaLTaiizor.Controls.Button
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpEndDate As ReaLTaiizor.Controls.PoisonDateTime
End Class