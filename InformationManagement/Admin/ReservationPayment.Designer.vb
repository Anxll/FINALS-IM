<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReservationPayment
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnConfirm = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnLastPage = New System.Windows.Forms.Button()
        Me.btnNextPage = New System.Windows.Forms.Button()
        Me.btnPrevPage = New System.Windows.Forms.Button()
        Me.btnFirstPage = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboRecordsPerPage = New System.Windows.Forms.ComboBox()
        Me.Reservation = New System.Windows.Forms.DataGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblPageInfo = New System.Windows.Forms.Label()
        Me.lblTotalRecords = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.Reservation, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(1028, 79)
        Me.Panel1.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(22, 21)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(309, 40)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Reservation Payment"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel2.Controls.Add(Me.btnConfirm)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnRefresh)
        Me.Panel2.Controls.Add(Me.txtSearch)
        Me.Panel2.Controls.Add(Me.lblSearch)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 79)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel2.Size = New System.Drawing.Size(1028, 61)
        Me.Panel2.TabIndex = 1
        '
        'btnConfirm
        '
        Me.btnConfirm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfirm.BackColor = System.Drawing.Color.Green
        Me.btnConfirm.FlatAppearance.BorderSize = 0
        Me.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfirm.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnConfirm.ForeColor = System.Drawing.Color.White
        Me.btnConfirm.Location = New System.Drawing.Point(821, 14)
        Me.btnConfirm.Margin = New System.Windows.Forms.Padding(2)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(100, 30)
        Me.btnConfirm.TabIndex = 7
        Me.btnConfirm.Text = "Update Status"
        Me.btnConfirm.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnDelete.ForeColor = System.Drawing.Color.White
        Me.btnDelete.Location = New System.Drawing.Point(925, 14)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 30)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(716, 14)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 30)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txtSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.Location = New System.Drawing.Point(90, 18)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(400, 25)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Text = ""
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(28, 21)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(51, 17)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search:"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.btnLastPage)
        Me.Panel3.Controls.Add(Me.btnNextPage)
        Me.Panel3.Controls.Add(Me.btnPrevPage)
        Me.Panel3.Controls.Add(Me.btnFirstPage)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.cboRecordsPerPage)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 140)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel3.Size = New System.Drawing.Size(1028, 50)
        Me.Panel3.TabIndex = 2
        '
        'btnLastPage
        '
        Me.btnLastPage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnLastPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnLastPage.FlatAppearance.BorderSize = 0
        Me.btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLastPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnLastPage.ForeColor = System.Drawing.Color.White
        Me.btnLastPage.Location = New System.Drawing.Point(470, 10)
        Me.btnLastPage.Name = "btnLastPage"
        Me.btnLastPage.Size = New System.Drawing.Size(80, 30)
        Me.btnLastPage.TabIndex = 5
        Me.btnLastPage.Text = "Last »"
        Me.btnLastPage.UseVisualStyleBackColor = False
        '
        'btnNextPage
        '
        Me.btnNextPage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnNextPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnNextPage.FlatAppearance.BorderSize = 0
        Me.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNextPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnNextPage.ForeColor = System.Drawing.Color.White
        Me.btnNextPage.Location = New System.Drawing.Point(384, 10)
        Me.btnNextPage.Name = "btnNextPage"
        Me.btnNextPage.Size = New System.Drawing.Size(80, 30)
        Me.btnNextPage.TabIndex = 4
        Me.btnNextPage.Text = "Next ›"
        Me.btnNextPage.UseVisualStyleBackColor = False
        '
        'btnPrevPage
        '
        Me.btnPrevPage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnPrevPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnPrevPage.FlatAppearance.BorderSize = 0
        Me.btnPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrevPage.ForeColor = System.Drawing.Color.White
        Me.btnPrevPage.Location = New System.Drawing.Point(298, 10)
        Me.btnPrevPage.Name = "btnPrevPage"
        Me.btnPrevPage.Size = New System.Drawing.Size(80, 30)
        Me.btnPrevPage.TabIndex = 3
        Me.btnPrevPage.Text = "‹ Prev"
        Me.btnPrevPage.UseVisualStyleBackColor = False
        '
        'btnFirstPage
        '
        Me.btnFirstPage.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnFirstPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnFirstPage.FlatAppearance.BorderSize = 0
        Me.btnFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFirstPage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnFirstPage.ForeColor = System.Drawing.Color.White
        Me.btnFirstPage.Location = New System.Drawing.Point(212, 10)
        Me.btnFirstPage.Name = "btnFirstPage"
        Me.btnFirstPage.Size = New System.Drawing.Size(80, 30)
        Me.btnFirstPage.TabIndex = 2
        Me.btnFirstPage.Text = "« First"
        Me.btnFirstPage.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(680, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Records per page:"
        '
        'cboRecordsPerPage
        '
        Me.cboRecordsPerPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboRecordsPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRecordsPerPage.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cboRecordsPerPage.FormattingEnabled = True
        Me.cboRecordsPerPage.Location = New System.Drawing.Point(792, 12)
        Me.cboRecordsPerPage.Name = "cboRecordsPerPage"
        Me.cboRecordsPerPage.Size = New System.Drawing.Size(90, 23)
        Me.cboRecordsPerPage.TabIndex = 0
        '
        'Reservation
        '
        Me.Reservation.AllowUserToAddRows = False
        Me.Reservation.AllowUserToDeleteRows = False
        Me.Reservation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Reservation.BackgroundColor = System.Drawing.Color.White
        Me.Reservation.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Reservation.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Reservation.ColumnHeadersHeight = 30
        Me.Reservation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.Reservation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Reservation.EnableHeadersVisualStyles = False
        Me.Reservation.Location = New System.Drawing.Point(0, 190)
        Me.Reservation.Name = "Reservation"
        Me.Reservation.ReadOnly = True
        Me.Reservation.RowHeadersVisible = False
        Me.Reservation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Reservation.Size = New System.Drawing.Size(1028, 370)
        Me.Reservation.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel4.Controls.Add(Me.lblPageInfo)
        Me.Panel4.Controls.Add(Me.lblTotalRecords)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 560)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1028, 40)
        Me.Panel4.TabIndex = 3
        '
        'lblPageInfo
        '
        Me.lblPageInfo.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblPageInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPageInfo.Location = New System.Drawing.Point(760, 10)
        Me.lblPageInfo.Name = "lblPageInfo"
        Me.lblPageInfo.Size = New System.Drawing.Size(240, 20)
        Me.lblPageInfo.TabIndex = 1
        Me.lblPageInfo.Text = "Page 1 of 1"
        Me.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalRecords
        '
        Me.lblTotalRecords.AutoSize = True
        Me.lblTotalRecords.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblTotalRecords.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTotalRecords.Location = New System.Drawing.Point(28, 12)
        Me.lblTotalRecords.Name = "lblTotalRecords"
        Me.lblTotalRecords.Size = New System.Drawing.Size(105, 15)
        Me.lblTotalRecords.TabIndex = 0
        Me.lblTotalRecords.Text = "Total Records: 0"
        '
        'ReservationPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 600)
        Me.Controls.Add(Me.Reservation)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ReservationPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reservation Payment"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.Reservation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents Reservation As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lblTotalRecords As Label
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnConfirm As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnLastPage As Button
    Friend WithEvents btnNextPage As Button
    Friend WithEvents btnPrevPage As Button
    Friend WithEvents btnFirstPage As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cboRecordsPerPage As ComboBox
    Friend WithEvents lblPageInfo As Label
End Class