<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Reservations
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnUpdateStatus = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnViewAll = New System.Windows.Forms.Button()
        Me.btnViewCancelled = New System.Windows.Forms.Button()
        Me.btnViewConfirmed = New System.Windows.Forms.Button()
        Me.btnViewPending = New System.Windows.Forms.Button()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.Reservation = New System.Windows.Forms.DataGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblTotalReservations = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.Reservation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lblTitle)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1028, 52)
        Me.Panel1.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(10, 11)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(318, 32)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Reservations Management"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.Controls.Add(Me.btnUpdateStatus)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Controls.Add(Me.btnRefresh)
        Me.Panel2.Controls.Add(Me.txtSearch)
        Me.Panel2.Controls.Add(Me.lblSearch)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 52)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(11, 11, 11, 11)
        Me.Panel2.Size = New System.Drawing.Size(1028, 64)
        Me.Panel2.TabIndex = 1
        '
        'btnUpdateStatus
        '
        Me.btnUpdateStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateStatus.BackColor = System.Drawing.Color.Green
        Me.btnUpdateStatus.FlatAppearance.BorderSize = 0
        Me.btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnUpdateStatus.ForeColor = System.Drawing.Color.White
        Me.btnUpdateStatus.Location = New System.Drawing.Point(368, 13)
        Me.btnUpdateStatus.Name = "btnUpdateStatus"
        Me.btnUpdateStatus.Size = New System.Drawing.Size(118, 30)
        Me.btnUpdateStatus.TabIndex = 9
        Me.btnUpdateStatus.Text = "Update Status"
        Me.btnUpdateStatus.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnDelete.ForeColor = System.Drawing.Color.White
        Me.btnDelete.Location = New System.Drawing.Point(497, 13)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 30)
        Me.btnDelete.TabIndex = 4
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
        Me.btnRefresh.Location = New System.Drawing.Point(257, 13)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 30)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtSearch.Location = New System.Drawing.Point(68, 15)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(172, 25)
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
        Me.Panel3.Controls.Add(Me.btnViewCancelled)
        Me.Panel3.Controls.Add(Me.btnViewConfirmed)
        Me.Panel3.Controls.Add(Me.btnViewPending)
        Me.Panel3.Controls.Add(Me.lblFilter)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 116)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(11, 11, 11, 11)
        Me.Panel3.Size = New System.Drawing.Size(1028, 53)
        Me.Panel3.TabIndex = 2
        '
        'btnViewAll
        '
        Me.btnViewAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnViewAll.FlatAppearance.BorderSize = 0
        Me.btnViewAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewAll.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnViewAll.ForeColor = System.Drawing.Color.White
        Me.btnViewAll.Location = New System.Drawing.Point(495, 10)
        Me.btnViewAll.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnViewAll.Name = "btnViewAll"
        Me.btnViewAll.Size = New System.Drawing.Size(100, 30)
        Me.btnViewAll.TabIndex = 4
        Me.btnViewAll.Text = "All"
        Me.btnViewAll.UseVisualStyleBackColor = False
        '
        'btnViewCancelled
        '
        Me.btnViewCancelled.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnViewCancelled.FlatAppearance.BorderSize = 0
        Me.btnViewCancelled.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewCancelled.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnViewCancelled.ForeColor = System.Drawing.Color.White
        Me.btnViewCancelled.Location = New System.Drawing.Point(387, 10)
        Me.btnViewCancelled.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnViewCancelled.Name = "btnViewCancelled"
        Me.btnViewCancelled.Size = New System.Drawing.Size(100, 30)
        Me.btnViewCancelled.TabIndex = 3
        Me.btnViewCancelled.Text = "Cancelled"
        Me.btnViewCancelled.UseVisualStyleBackColor = False
        '
        'btnViewConfirmed
        '
        Me.btnViewConfirmed.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnViewConfirmed.FlatAppearance.BorderSize = 0
        Me.btnViewConfirmed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewConfirmed.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnViewConfirmed.ForeColor = System.Drawing.Color.White
        Me.btnViewConfirmed.Location = New System.Drawing.Point(279, 10)
        Me.btnViewConfirmed.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnViewConfirmed.Name = "btnViewConfirmed"
        Me.btnViewConfirmed.Size = New System.Drawing.Size(100, 30)
        Me.btnViewConfirmed.TabIndex = 2
        Me.btnViewConfirmed.Text = "Confirmed"
        Me.btnViewConfirmed.UseVisualStyleBackColor = False
        '
        'btnViewPending
        '
        Me.btnViewPending.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnViewPending.FlatAppearance.BorderSize = 0
        Me.btnViewPending.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewPending.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewPending.ForeColor = System.Drawing.Color.White
        Me.btnViewPending.Location = New System.Drawing.Point(171, 10)
        Me.btnViewPending.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnViewPending.Name = "btnViewPending"
        Me.btnViewPending.Size = New System.Drawing.Size(100, 30)
        Me.btnViewPending.TabIndex = 1
        Me.btnViewPending.Text = "Pending"
        Me.btnViewPending.UseVisualStyleBackColor = False
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
        'Reservation
        '
        Me.Reservation.AllowUserToAddRows = False
        Me.Reservation.AllowUserToDeleteRows = False
        Me.Reservation.AllowUserToResizeRows = False
        Me.Reservation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.Reservation.BackgroundColor = System.Drawing.Color.White
        Me.Reservation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Reservation.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.Reservation.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Reservation.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Reservation.ColumnHeadersHeight = 40
        Me.Reservation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.Reservation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Reservation.EnableHeadersVisualStyles = False
        Me.Reservation.Location = New System.Drawing.Point(0, 169)
        Me.Reservation.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Reservation.Name = "Reservation"
        Me.Reservation.ReadOnly = True
        Me.Reservation.RowHeadersVisible = False
        Me.Reservation.RowHeadersWidth = 51
        Me.Reservation.RowTemplate.Height = 35
        Me.Reservation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Reservation.Size = New System.Drawing.Size(1028, 377)
        Me.Reservation.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel4.Controls.Add(Me.lblTotalReservations)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 546)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1028, 26)
        Me.Panel4.TabIndex = 4
        '
        'lblTotalReservations
        '
        Me.lblTotalReservations.AutoSize = True
        Me.lblTotalReservations.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalReservations.Location = New System.Drawing.Point(10, 6)
        Me.lblTotalReservations.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTotalReservations.Name = "lblTotalReservations"
        Me.lblTotalReservations.Size = New System.Drawing.Size(122, 15)
        Me.lblTotalReservations.TabIndex = 0
        Me.lblTotalReservations.Text = "Total Reservations: 0"
        '
        'Reservations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 572)
        Me.Controls.Add(Me.Reservation)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MinimumSize = New System.Drawing.Size(687, 523)
        Me.Name = "Reservations"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reservations Management - Tabeya"
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
    Friend WithEvents btnDelete As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lblFilter As Label
    Friend WithEvents btnViewPending As Button
    Friend WithEvents btnViewConfirmed As Button
    Friend WithEvents btnViewCancelled As Button
    Friend WithEvents btnViewAll As Button
    Friend WithEvents Reservation As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lblTotalReservations As Label
    Friend WithEvents btnUpdateStatus As Button
End Class