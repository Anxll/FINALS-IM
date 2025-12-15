<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Feedback
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
        Me.btnExport = New ReaLTaiizor.Controls.Button()
        Me.btnDelete = New ReaLTaiizor.Controls.Button()
        Me.btnViewDetails = New ReaLTaiizor.Controls.Button()
        Me.btnRefresh = New ReaLTaiizor.Controls.Button()
        Me.txtSearch = New ReaLTaiizor.Controls.BigTextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnViewAll = New ReaLTaiizor.Controls.Button()
        Me.btnViewRejected = New ReaLTaiizor.Controls.Button()
        Me.btnViewApproved = New ReaLTaiizor.Controls.Button()
        Me.btnViewPending = New ReaLTaiizor.Controls.Button()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblTotalReviews = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel1.Controls.Add(Me.lblTitle)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1113, 79)
        Me.Panel1.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(22, 21)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(447, 40)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Customer Review Management"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel2.Controls.Add(Me.btnExport)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnViewDetails)
        Me.Panel2.Controls.Add(Me.btnRefresh)
        Me.Panel2.Controls.Add(Me.txtSearch)
        Me.Panel2.Controls.Add(Me.lblSearch)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 79)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(9)
        Me.Panel2.Size = New System.Drawing.Size(1113, 63)
        Me.Panel2.TabIndex = 1
        '
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.Color.Transparent
        Me.btnExport.BorderColor = System.Drawing.Color.Transparent
        Me.btnExport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExport.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnExport.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnExport.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnExport.Image = Nothing
        Me.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExport.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExport.Location = New System.Drawing.Point(885, 23)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnExport.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnExport.Size = New System.Drawing.Size(109, 36)
        Me.btnExport.TabIndex = 11
        Me.btnExport.Text = "Export to CSV"
        Me.btnExport.TextAlignment = System.Drawing.StringAlignment.Center
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
        Me.btnDelete.Location = New System.Drawing.Point(1000, 23)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnDelete.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnDelete.Size = New System.Drawing.Size(69, 36)
        Me.btnDelete.TabIndex = 9
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnViewDetails
        '
        Me.btnViewDetails.BackColor = System.Drawing.Color.Transparent
        Me.btnViewDetails.BorderColor = System.Drawing.Color.Transparent
        Me.btnViewDetails.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnViewDetails.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewDetails.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewDetails.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewDetails.Image = Nothing
        Me.btnViewDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewDetails.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnViewDetails.Location = New System.Drawing.Point(775, 23)
        Me.btnViewDetails.Name = "btnViewDetails"
        Me.btnViewDetails.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewDetails.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewDetails.Size = New System.Drawing.Size(104, 36)
        Me.btnViewDetails.TabIndex = 10
        Me.btnViewDetails.Text = "View Details"
        Me.btnViewDetails.TextAlignment = System.Drawing.StringAlignment.Center
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
        Me.btnRefresh.Location = New System.Drawing.Point(665, 22)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnRefresh.Size = New System.Drawing.Size(104, 36)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.Transparent
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.Image = Nothing
        Me.txtSearch.Location = New System.Drawing.Point(39, 21)
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
        Me.lblSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblSearch.Location = New System.Drawing.Point(41, 4)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(46, 15)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search:"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.btnViewAll)
        Me.Panel3.Controls.Add(Me.btnViewRejected)
        Me.Panel3.Controls.Add(Me.btnViewApproved)
        Me.Panel3.Controls.Add(Me.btnViewPending)
        Me.Panel3.Controls.Add(Me.lblFilter)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 142)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(9)
        Me.Panel3.Size = New System.Drawing.Size(1113, 64)
        Me.Panel3.TabIndex = 2
        '
        'btnViewAll
        '
        Me.btnViewAll.BackColor = System.Drawing.Color.Transparent
        Me.btnViewAll.BorderColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewAll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnViewAll.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewAll.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewAll.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewAll.Image = Nothing
        Me.btnViewAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewAll.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.btnViewAll.Location = New System.Drawing.Point(368, 21)
        Me.btnViewAll.Name = "btnViewAll"
        Me.btnViewAll.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewAll.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewAll.Size = New System.Drawing.Size(68, 36)
        Me.btnViewAll.TabIndex = 12
        Me.btnViewAll.Text = "All"
        Me.btnViewAll.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnViewRejected
        '
        Me.btnViewRejected.BackColor = System.Drawing.Color.Transparent
        Me.btnViewRejected.BorderColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewRejected.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnViewRejected.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewRejected.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewRejected.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewRejected.Image = Nothing
        Me.btnViewRejected.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewRejected.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.btnViewRejected.Location = New System.Drawing.Point(258, 21)
        Me.btnViewRejected.Name = "btnViewRejected"
        Me.btnViewRejected.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewRejected.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewRejected.Size = New System.Drawing.Size(104, 36)
        Me.btnViewRejected.TabIndex = 13
        Me.btnViewRejected.Text = "Rejected"
        Me.btnViewRejected.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnViewApproved
        '
        Me.btnViewApproved.BackColor = System.Drawing.Color.Transparent
        Me.btnViewApproved.BorderColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewApproved.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnViewApproved.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewApproved.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewApproved.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewApproved.Image = Nothing
        Me.btnViewApproved.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewApproved.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.btnViewApproved.Location = New System.Drawing.Point(149, 21)
        Me.btnViewApproved.Name = "btnViewApproved"
        Me.btnViewApproved.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewApproved.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewApproved.Size = New System.Drawing.Size(104, 36)
        Me.btnViewApproved.TabIndex = 14
        Me.btnViewApproved.Text = "Approved"
        Me.btnViewApproved.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btnViewPending
        '
        Me.btnViewPending.BackColor = System.Drawing.Color.Transparent
        Me.btnViewPending.BorderColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewPending.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnViewPending.EnteredBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewPending.EnteredColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewPending.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewPending.Image = Nothing
        Me.btnViewPending.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewPending.InactiveColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.btnViewPending.Location = New System.Drawing.Point(39, 21)
        Me.btnViewPending.Name = "btnViewPending"
        Me.btnViewPending.PressedBorderColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewPending.PressedColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnViewPending.Size = New System.Drawing.Size(104, 36)
        Me.btnViewPending.TabIndex = 15
        Me.btnViewPending.Text = "Pending"
        Me.btnViewPending.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblFilter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblFilter.Location = New System.Drawing.Point(41, 2)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(72, 15)
        Me.lblFilter.TabIndex = 0
        Me.lblFilter.Text = "Filter Status:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(39, 212)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 25
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1030, 468)
        Me.DataGridView1.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel4.Controls.Add(Me.lblTotalReviews)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 686)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1113, 26)
        Me.Panel4.TabIndex = 4
        '
        'lblTotalReviews
        '
        Me.lblTotalReviews.AutoSize = True
        Me.lblTotalReviews.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalReviews.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTotalReviews.Location = New System.Drawing.Point(38, 5)
        Me.lblTotalReviews.Name = "lblTotalReviews"
        Me.lblTotalReviews.Size = New System.Drawing.Size(97, 15)
        Me.lblTotalReviews.TabIndex = 0
        Me.lblTotalReviews.Text = "Total Reviews: 0"
        '
        'Feedback
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1113, 712)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Feedback"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customer Review Management - Tabeya"
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
    Friend WithEvents lblTotalReviews As Label
    Friend WithEvents txtSearch As ReaLTaiizor.Controls.BigTextBox
    Friend WithEvents btnRefresh As ReaLTaiizor.Controls.Button
    Friend WithEvents btnDelete As ReaLTaiizor.Controls.Button
    Friend WithEvents btnExport As ReaLTaiizor.Controls.Button
    Friend WithEvents btnViewDetails As ReaLTaiizor.Controls.Button
    Friend WithEvents btnViewAll As ReaLTaiizor.Controls.Button
    Friend WithEvents btnViewRejected As ReaLTaiizor.Controls.Button
    Friend WithEvents btnViewApproved As ReaLTaiizor.Controls.Button
    Friend WithEvents btnViewPending As ReaLTaiizor.Controls.Button
End Class