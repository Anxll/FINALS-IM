<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InventoryMovementHistory
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.dgvMovements = New System.Windows.Forms.DataGridView()
        Me.pnlStats = New System.Windows.Forms.Panel()
        Me.lblStatsTitle = New System.Windows.Forms.Label()
        Me.pnlStatCards = New System.Windows.Forms.Panel()
        Me.pnlTotalCard = New System.Windows.Forms.Panel()
        Me.lblTotalValue = New System.Windows.Forms.Label()
        Me.lblTotalLabel = New System.Windows.Forms.Label()
        Me.pnlPOSCard = New System.Windows.Forms.Panel()
        Me.lblPOSValue = New System.Windows.Forms.Label()
        Me.lblPOSLabel = New System.Windows.Forms.Label()
        Me.pnlWebCard = New System.Windows.Forms.Panel()
        Me.lblWebValue = New System.Windows.Forms.Label()
        Me.lblWebLabel = New System.Windows.Forms.Label()
        Me.pnlAdminCard = New System.Windows.Forms.Panel()
        Me.lblAdminValue = New System.Windows.Forms.Label()
        Me.lblAdminLabel = New System.Windows.Forms.Label()
        Me.pnlFilters = New System.Windows.Forms.Panel()
        Me.lblFilterTitle = New System.Windows.Forms.Label()
        Me.grpDateRange = New System.Windows.Forms.GroupBox()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.lblEndDate = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.grpCategories = New System.Windows.Forms.GroupBox()
        Me.lblSource = New System.Windows.Forms.Label()
        Me.cmbSource = New System.Windows.Forms.ComboBox()
        Me.lblChangeType = New System.Windows.Forms.Label()
        Me.cmbChangeType = New System.Windows.Forms.ComboBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.pnlFilterButtons = New System.Windows.Forms.Panel()
        Me.btnApplyFilters = New System.Windows.Forms.Button()
        Me.btnResetFilters = New System.Windows.Forms.Button()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.lblOverallTotalCost = New System.Windows.Forms.Label()
        Me.btnclear = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.dgvMovements, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStats.SuspendLayout()
        Me.pnlStatCards.SuspendLayout()
        Me.pnlTotalCard.SuspendLayout()
        Me.pnlPOSCard.SuspendLayout()
        Me.pnlWebCard.SuspendLayout()
        Me.pnlAdminCard.SuspendLayout()
        Me.pnlFilters.SuspendLayout()
        Me.grpDateRange.SuspendLayout()
        Me.grpCategories.SuspendLayout()
        Me.pnlFilterButtons.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.pnlGrid)
        Me.pnlMain.Controls.Add(Me.pnlStats)
        Me.pnlMain.Controls.Add(Me.pnlFilters)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Controls.Add(Me.pnlActions)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlMain.Size = New System.Drawing.Size(1316, 800)
        Me.pnlMain.TabIndex = 0
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.White
        Me.pnlGrid.Controls.Add(Me.dgvMovements)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(20, 440)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlGrid.Size = New System.Drawing.Size(1276, 270)
        Me.pnlGrid.TabIndex = 0
        '
        'dgvMovements
        '
        Me.dgvMovements.AllowUserToAddRows = False
        Me.dgvMovements.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgvMovements.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvMovements.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvMovements.BackgroundColor = System.Drawing.Color.White
        Me.dgvMovements.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvMovements.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvMovements.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(87, Byte), Integer))
        DataGridViewCellStyle5.Padding = New System.Windows.Forms.Padding(10)
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvMovements.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvMovements.ColumnHeadersHeight = 45
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        DataGridViewCellStyle6.Padding = New System.Windows.Forms.Padding(10, 5, 10, 5)
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvMovements.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvMovements.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMovements.EnableHeadersVisualStyles = False
        Me.dgvMovements.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dgvMovements.GridColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvMovements.Location = New System.Drawing.Point(20, 20)
        Me.dgvMovements.Name = "dgvMovements"
        Me.dgvMovements.ReadOnly = True
        Me.dgvMovements.RowHeadersVisible = False
        Me.dgvMovements.RowTemplate.Height = 35
        Me.dgvMovements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMovements.Size = New System.Drawing.Size(1236, 230)
        Me.dgvMovements.TabIndex = 0
        '
        'pnlStats
        '
        Me.pnlStats.BackColor = System.Drawing.Color.Transparent
        Me.pnlStats.Controls.Add(Me.lblStatsTitle)
        Me.pnlStats.Controls.Add(Me.pnlStatCards)
        Me.pnlStats.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlStats.Location = New System.Drawing.Point(20, 300)
        Me.pnlStats.Name = "pnlStats"
        Me.pnlStats.Padding = New System.Windows.Forms.Padding(0, 15, 0, 15)
        Me.pnlStats.Size = New System.Drawing.Size(1276, 140)
        Me.pnlStats.TabIndex = 1
        '
        'lblStatsTitle
        '
        Me.lblStatsTitle.AutoSize = True
        Me.lblStatsTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblStatsTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.lblStatsTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblStatsTitle.Name = "lblStatsTitle"
        Me.lblStatsTitle.Size = New System.Drawing.Size(132, 20)
        Me.lblStatsTitle.TabIndex = 0
        Me.lblStatsTitle.Text = "Statistics Overview"
        '
        'pnlStatCards
        '
        Me.pnlStatCards.Controls.Add(Me.pnlTotalCard)
        Me.pnlStatCards.Controls.Add(Me.pnlPOSCard)
        Me.pnlStatCards.Controls.Add(Me.pnlWebCard)
        Me.pnlStatCards.Controls.Add(Me.pnlAdminCard)
        Me.pnlStatCards.Location = New System.Drawing.Point(0, 30)
        Me.pnlStatCards.Name = "pnlStatCards"
        Me.pnlStatCards.Size = New System.Drawing.Size(1240, 90)
        Me.pnlStatCards.TabIndex = 1
        '
        'pnlTotalCard
        '
        Me.pnlTotalCard.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlTotalCard.Controls.Add(Me.lblTotalValue)
        Me.pnlTotalCard.Controls.Add(Me.lblTotalLabel)
        Me.pnlTotalCard.Location = New System.Drawing.Point(0, 0)
        Me.pnlTotalCard.Name = "pnlTotalCard"
        Me.pnlTotalCard.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlTotalCard.Size = New System.Drawing.Size(295, 90)
        Me.pnlTotalCard.TabIndex = 0
        '
        'lblTotalValue
        '
        Me.lblTotalValue.AutoSize = True
        Me.lblTotalValue.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalValue.ForeColor = System.Drawing.Color.White
        Me.lblTotalValue.Location = New System.Drawing.Point(20, 15)
        Me.lblTotalValue.Name = "lblTotalValue"
        Me.lblTotalValue.Size = New System.Drawing.Size(38, 45)
        Me.lblTotalValue.TabIndex = 0
        Me.lblTotalValue.Text = "0"
        '
        'lblTotalLabel
        '
        Me.lblTotalLabel.AutoSize = True
        Me.lblTotalLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTotalLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblTotalLabel.Location = New System.Drawing.Point(20, 55)
        Me.lblTotalLabel.Name = "lblTotalLabel"
        Me.lblTotalLabel.Size = New System.Drawing.Size(99, 15)
        Me.lblTotalLabel.TabIndex = 1
        Me.lblTotalLabel.Text = "Total Movements"
        '
        'pnlPOSCard
        '
        Me.pnlPOSCard.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.pnlPOSCard.Controls.Add(Me.lblPOSValue)
        Me.pnlPOSCard.Controls.Add(Me.lblPOSLabel)
        Me.pnlPOSCard.Location = New System.Drawing.Point(310, 0)
        Me.pnlPOSCard.Name = "pnlPOSCard"
        Me.pnlPOSCard.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlPOSCard.Size = New System.Drawing.Size(295, 90)
        Me.pnlPOSCard.TabIndex = 1
        '
        'lblPOSValue
        '
        Me.lblPOSValue.AutoSize = True
        Me.lblPOSValue.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.lblPOSValue.ForeColor = System.Drawing.Color.White
        Me.lblPOSValue.Location = New System.Drawing.Point(20, 15)
        Me.lblPOSValue.Name = "lblPOSValue"
        Me.lblPOSValue.Size = New System.Drawing.Size(38, 45)
        Me.lblPOSValue.TabIndex = 0
        Me.lblPOSValue.Text = "0"
        '
        'lblPOSLabel
        '
        Me.lblPOSLabel.AutoSize = True
        Me.lblPOSLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblPOSLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.lblPOSLabel.Location = New System.Drawing.Point(20, 55)
        Me.lblPOSLabel.Name = "lblPOSLabel"
        Me.lblPOSLabel.Size = New System.Drawing.Size(98, 15)
        Me.lblPOSLabel.TabIndex = 1
        Me.lblPOSLabel.Text = "POS Transactions"
        '
        'pnlWebCard
        '
        Me.pnlWebCard.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.pnlWebCard.Controls.Add(Me.lblWebValue)
        Me.pnlWebCard.Controls.Add(Me.lblWebLabel)
        Me.pnlWebCard.Location = New System.Drawing.Point(620, 0)
        Me.pnlWebCard.Name = "pnlWebCard"
        Me.pnlWebCard.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlWebCard.Size = New System.Drawing.Size(295, 90)
        Me.pnlWebCard.TabIndex = 2
        '
        'lblWebValue
        '
        Me.lblWebValue.AutoSize = True
        Me.lblWebValue.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.lblWebValue.ForeColor = System.Drawing.Color.White
        Me.lblWebValue.Location = New System.Drawing.Point(20, 15)
        Me.lblWebValue.Name = "lblWebValue"
        Me.lblWebValue.Size = New System.Drawing.Size(38, 45)
        Me.lblWebValue.TabIndex = 0
        Me.lblWebValue.Text = "0"
        '
        'lblWebLabel
        '
        Me.lblWebLabel.AutoSize = True
        Me.lblWebLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblWebLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.lblWebLabel.Location = New System.Drawing.Point(20, 55)
        Me.lblWebLabel.Name = "lblWebLabel"
        Me.lblWebLabel.Size = New System.Drawing.Size(87, 15)
        Me.lblWebLabel.TabIndex = 1
        Me.lblWebLabel.Text = "Website Orders"
        '
        'pnlAdminCard
        '
        Me.pnlAdminCard.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.pnlAdminCard.Controls.Add(Me.lblAdminValue)
        Me.pnlAdminCard.Controls.Add(Me.lblAdminLabel)
        Me.pnlAdminCard.Location = New System.Drawing.Point(930, 0)
        Me.pnlAdminCard.Name = "pnlAdminCard"
        Me.pnlAdminCard.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlAdminCard.Size = New System.Drawing.Size(295, 90)
        Me.pnlAdminCard.TabIndex = 3
        '
        'lblAdminValue
        '
        Me.lblAdminValue.AutoSize = True
        Me.lblAdminValue.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.lblAdminValue.ForeColor = System.Drawing.Color.White
        Me.lblAdminValue.Location = New System.Drawing.Point(20, 15)
        Me.lblAdminValue.Name = "lblAdminValue"
        Me.lblAdminValue.Size = New System.Drawing.Size(38, 45)
        Me.lblAdminValue.TabIndex = 0
        Me.lblAdminValue.Text = "0"
        '
        'lblAdminLabel
        '
        Me.lblAdminLabel.AutoSize = True
        Me.lblAdminLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblAdminLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.lblAdminLabel.Location = New System.Drawing.Point(20, 55)
        Me.lblAdminLabel.Name = "lblAdminLabel"
        Me.lblAdminLabel.Size = New System.Drawing.Size(92, 15)
        Me.lblAdminLabel.TabIndex = 1
        Me.lblAdminLabel.Text = "Admin Changes"
        '
        'pnlFilters
        '
        Me.pnlFilters.BackColor = System.Drawing.Color.White
        Me.pnlFilters.Controls.Add(Me.lblFilterTitle)
        Me.pnlFilters.Controls.Add(Me.grpDateRange)
        Me.pnlFilters.Controls.Add(Me.grpCategories)
        Me.pnlFilters.Controls.Add(Me.pnlFilterButtons)
        Me.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilters.Location = New System.Drawing.Point(20, 100)
        Me.pnlFilters.Margin = New System.Windows.Forms.Padding(0, 15, 0, 0)
        Me.pnlFilters.Name = "pnlFilters"
        Me.pnlFilters.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlFilters.Size = New System.Drawing.Size(1276, 200)
        Me.pnlFilters.TabIndex = 2
        '
        'lblFilterTitle
        '
        Me.lblFilterTitle.AutoSize = True
        Me.lblFilterTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblFilterTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.lblFilterTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblFilterTitle.Name = "lblFilterTitle"
        Me.lblFilterTitle.Size = New System.Drawing.Size(48, 20)
        Me.lblFilterTitle.TabIndex = 0
        Me.lblFilterTitle.Text = "Filters"
        '
        'grpDateRange
        '
        Me.grpDateRange.Controls.Add(Me.lblStartDate)
        Me.grpDateRange.Controls.Add(Me.dtpStartDate)
        Me.grpDateRange.Controls.Add(Me.lblEndDate)
        Me.grpDateRange.Controls.Add(Me.dtpEndDate)
        Me.grpDateRange.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.grpDateRange.Location = New System.Drawing.Point(20, 45)
        Me.grpDateRange.Name = "grpDateRange"
        Me.grpDateRange.Size = New System.Drawing.Size(460, 100)
        Me.grpDateRange.TabIndex = 1
        Me.grpDateRange.TabStop = False
        Me.grpDateRange.Text = "Date Range"
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(15, 25)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(61, 15)
        Me.lblStartDate.TabIndex = 0
        Me.lblStartDate.Text = "Start Date:"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStartDate.Location = New System.Drawing.Point(15, 48)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(200, 23)
        Me.dtpStartDate.TabIndex = 1
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Location = New System.Drawing.Point(240, 25)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(57, 15)
        Me.lblEndDate.TabIndex = 2
        Me.lblEndDate.Text = "End Date:"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndDate.Location = New System.Drawing.Point(240, 48)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(200, 23)
        Me.dtpEndDate.TabIndex = 3
        '
        'grpCategories
        '
        Me.grpCategories.Controls.Add(Me.lblSource)
        Me.grpCategories.Controls.Add(Me.cmbSource)
        Me.grpCategories.Controls.Add(Me.lblChangeType)
        Me.grpCategories.Controls.Add(Me.cmbChangeType)
        Me.grpCategories.Controls.Add(Me.lblSearch)
        Me.grpCategories.Controls.Add(Me.txtSearch)
        Me.grpCategories.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.grpCategories.Location = New System.Drawing.Point(500, 45)
        Me.grpCategories.Name = "grpCategories"
        Me.grpCategories.Size = New System.Drawing.Size(540, 100)
        Me.grpCategories.TabIndex = 2
        Me.grpCategories.TabStop = False
        Me.grpCategories.Text = "Categories & Search"
        '
        'lblSource
        '
        Me.lblSource.AutoSize = True
        Me.lblSource.Location = New System.Drawing.Point(15, 25)
        Me.lblSource.Name = "lblSource"
        Me.lblSource.Size = New System.Drawing.Size(46, 15)
        Me.lblSource.TabIndex = 0
        Me.lblSource.Text = "Source:"
        '
        'cmbSource
        '
        Me.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSource.Location = New System.Drawing.Point(15, 48)
        Me.cmbSource.Name = "cmbSource"
        Me.cmbSource.Size = New System.Drawing.Size(150, 23)
        Me.cmbSource.TabIndex = 1
        '
        'lblChangeType
        '
        Me.lblChangeType.AutoSize = True
        Me.lblChangeType.Location = New System.Drawing.Point(185, 25)
        Me.lblChangeType.Name = "lblChangeType"
        Me.lblChangeType.Size = New System.Drawing.Size(79, 15)
        Me.lblChangeType.TabIndex = 2
        Me.lblChangeType.Text = "Change Type:"
        '
        'cmbChangeType
        '
        Me.cmbChangeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChangeType.Location = New System.Drawing.Point(185, 48)
        Me.cmbChangeType.Name = "cmbChangeType"
        Me.cmbChangeType.Size = New System.Drawing.Size(150, 23)
        Me.cmbChangeType.TabIndex = 3
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(355, 25)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(45, 15)
        Me.lblSearch.TabIndex = 4
        Me.lblSearch.Text = "Search:"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(355, 48)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(170, 23)
        Me.txtSearch.TabIndex = 5
        '
        'pnlFilterButtons
        '
        Me.pnlFilterButtons.Controls.Add(Me.btnApplyFilters)
        Me.pnlFilterButtons.Controls.Add(Me.btnResetFilters)
        Me.pnlFilterButtons.Location = New System.Drawing.Point(1060, 60)
        Me.pnlFilterButtons.Name = "pnlFilterButtons"
        Me.pnlFilterButtons.Size = New System.Drawing.Size(180, 85)
        Me.pnlFilterButtons.TabIndex = 3
        '
        'btnApplyFilters
        '
        Me.btnApplyFilters.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnApplyFilters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnApplyFilters.FlatAppearance.BorderSize = 0
        Me.btnApplyFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApplyFilters.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnApplyFilters.ForeColor = System.Drawing.Color.White
        Me.btnApplyFilters.Location = New System.Drawing.Point(10, 10)
        Me.btnApplyFilters.Name = "btnApplyFilters"
        Me.btnApplyFilters.Size = New System.Drawing.Size(160, 35)
        Me.btnApplyFilters.TabIndex = 0
        Me.btnApplyFilters.Text = "Apply Filters"
        Me.btnApplyFilters.UseVisualStyleBackColor = False
        '
        'btnResetFilters
        '
        Me.btnResetFilters.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnResetFilters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnResetFilters.FlatAppearance.BorderSize = 0
        Me.btnResetFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetFilters.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnResetFilters.ForeColor = System.Drawing.Color.White
        Me.btnResetFilters.Location = New System.Drawing.Point(10, 50)
        Me.btnResetFilters.Name = "btnResetFilters"
        Me.btnResetFilters.Size = New System.Drawing.Size(160, 35)
        Me.btnResetFilters.TabIndex = 1
        Me.btnResetFilters.Text = "Reset Filters"
        Me.btnResetFilters.UseVisualStyleBackColor = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.White
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblSubtitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(20, 20)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(20, 15, 20, 15)
        Me.pnlHeader.Size = New System.Drawing.Size(1276, 80)
        Me.pnlHeader.TabIndex = 3
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(41, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(347, 32)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Inventory Movement History"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSubtitle.Location = New System.Drawing.Point(20, 48)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(318, 15)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "Track and analyze all inventory changes across your system"
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.White
        Me.pnlActions.Controls.Add(Me.lblOverallTotalCost)
        Me.pnlActions.Controls.Add(Me.btnclear)
        Me.pnlActions.Controls.Add(Me.btnExport)
        Me.pnlActions.Controls.Add(Me.btnRefresh)
        Me.pnlActions.Controls.Add(Me.btnClose)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(20, 710)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Padding = New System.Windows.Forms.Padding(20, 15, 20, 15)
        Me.pnlActions.Size = New System.Drawing.Size(1276, 70)
        Me.pnlActions.TabIndex = 4
        '
        'lblOverallTotalCost
        '
        Me.lblOverallTotalCost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOverallTotalCost.AutoSize = True
        Me.lblOverallTotalCost.Font = New System.Drawing.Font("Segoe UI", 14.0!)
        Me.lblOverallTotalCost.Location = New System.Drawing.Point(841, 30)
        Me.lblOverallTotalCost.Name = "lblOverallTotalCost"
        Me.lblOverallTotalCost.Size = New System.Drawing.Size(213, 25)
        Me.lblOverallTotalCost.TabIndex = 4
        Me.lblOverallTotalCost.Text = "Overall Total Cost: ₱0.00"
        Me.lblOverallTotalCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnclear
        '
        Me.btnclear.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnclear.FlatAppearance.BorderSize = 0
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.Location = New System.Drawing.Point(320, 15)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(140, 40)
        Me.btnclear.TabIndex = 3
        Me.btnclear.Text = "CLear History"
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnExport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExport.FlatAppearance.BorderSize = 0
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnExport.ForeColor = System.Drawing.Color.White
        Me.btnExport.Location = New System.Drawing.Point(20, 15)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(140, 40)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "📊 Export CSV"
        Me.btnExport.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(170, 15)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(140, 40)
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
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(1117, 15)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(140, 40)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'InventoryMovementHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1316, 800)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(1024, 600)
        Me.Name = "InventoryMovementHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inventory Movement History"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.dgvMovements, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStats.ResumeLayout(False)
        Me.pnlStats.PerformLayout()
        Me.pnlStatCards.ResumeLayout(False)
        Me.pnlTotalCard.ResumeLayout(False)
        Me.pnlTotalCard.PerformLayout()
        Me.pnlPOSCard.ResumeLayout(False)
        Me.pnlPOSCard.PerformLayout()
        Me.pnlWebCard.ResumeLayout(False)
        Me.pnlWebCard.PerformLayout()
        Me.pnlAdminCard.ResumeLayout(False)
        Me.pnlAdminCard.PerformLayout()
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlFilters.PerformLayout()
        Me.grpDateRange.ResumeLayout(False)
        Me.grpDateRange.PerformLayout()
        Me.grpCategories.ResumeLayout(False)
        Me.grpCategories.PerformLayout()
        Me.pnlFilterButtons.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlActions.ResumeLayout(False)
        Me.pnlActions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    '==== Declare controls ====
    Friend WithEvents pnlMain As Panel

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label

    Friend WithEvents pnlFilters As Panel
    Friend WithEvents lblFilterTitle As Label
    Friend WithEvents grpDateRange As GroupBox
    Friend WithEvents lblStartDate As Label
    Friend WithEvents dtpStartDate As DateTimePicker
    Friend WithEvents lblEndDate As Label
    Friend WithEvents dtpEndDate As DateTimePicker
    Friend WithEvents grpCategories As GroupBox
    Friend WithEvents lblSource As Label
    Friend WithEvents cmbSource As ComboBox
    Friend WithEvents lblChangeType As Label
    Friend WithEvents cmbChangeType As ComboBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents pnlFilterButtons As Panel
    Friend WithEvents btnApplyFilters As Button
    Friend WithEvents btnResetFilters As Button

    Friend WithEvents pnlStats As Panel
    Friend WithEvents lblStatsTitle As Label
    Friend WithEvents pnlStatCards As Panel
    Friend WithEvents pnlTotalCard As Panel
    Friend WithEvents lblTotalValue As Label
    Friend WithEvents lblTotalLabel As Label
    Friend WithEvents pnlPOSCard As Panel
    Friend WithEvents lblPOSValue As Label
    Friend WithEvents lblPOSLabel As Label
    Friend WithEvents pnlWebCard As Panel
    Friend WithEvents lblWebValue As Label
    Friend WithEvents lblWebLabel As Label
    Friend WithEvents pnlAdminCard As Panel
    Friend WithEvents lblAdminValue As Label
    Friend WithEvents lblAdminLabel As Label

    Friend WithEvents pnlGrid As Panel
    Friend WithEvents dgvMovements As DataGridView

    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnExport As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnclear As Button
    Friend WithEvents lblOverallTotalCost As Label
End Class