<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MenuItems
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnToggleAvailability = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.AddMenuItemsbtn = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Category = New System.Windows.Forms.ComboBox()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.DataGridMenu = New System.Windows.Forms.DataGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblTotalItems = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DataGridMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1219, 61)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(17, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(347, 37)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Menu Items Management"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.Controls.Add(Me.btnExport)
        Me.Panel2.Controls.Add(Me.btnToggleAvailability)
        Me.Panel2.Controls.Add(Me.btnRefresh)
        Me.Panel2.Controls.Add(Me.AddMenuItemsbtn)
        Me.Panel2.Controls.Add(Me.txtSearch)
        Me.Panel2.Controls.Add(Me.lblSearch)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 61)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(9)
        Me.Panel2.Size = New System.Drawing.Size(1219, 61)
        Me.Panel2.TabIndex = 1
        '
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnExport.ForeColor = System.Drawing.Color.White
        Me.btnExport.Location = New System.Drawing.Point(1243, 16)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(111, 30)
        Me.btnExport.TabIndex = 5
        Me.btnExport.Text = "📊 Export CSV"
        Me.btnExport.UseVisualStyleBackColor = False
        '
        'btnToggleAvailability
        '
        Me.btnToggleAvailability.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnToggleAvailability.FlatAppearance.BorderSize = 0
        Me.btnToggleAvailability.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnToggleAvailability.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnToggleAvailability.ForeColor = System.Drawing.Color.Black
        Me.btnToggleAvailability.Location = New System.Drawing.Point(1089, 16)
        Me.btnToggleAvailability.Name = "btnToggleAvailability"
        Me.btnToggleAvailability.Size = New System.Drawing.Size(146, 30)
        Me.btnToggleAvailability.TabIndex = 4
        Me.btnToggleAvailability.Text = "🔄 Toggle Status"
        Me.btnToggleAvailability.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(986, 16)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(94, 30)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "🔄 Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'AddMenuItemsbtn
        '
        Me.AddMenuItemsbtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.AddMenuItemsbtn.FlatAppearance.BorderSize = 0
        Me.AddMenuItemsbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddMenuItemsbtn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.AddMenuItemsbtn.ForeColor = System.Drawing.Color.White
        Me.AddMenuItemsbtn.Location = New System.Drawing.Point(823, 16)
        Me.AddMenuItemsbtn.Name = "AddMenuItemsbtn"
        Me.AddMenuItemsbtn.Size = New System.Drawing.Size(154, 30)
        Me.AddMenuItemsbtn.TabIndex = 2
        Me.AddMenuItemsbtn.Text = "➕ Add Menu Item"
        Me.AddMenuItemsbtn.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtSearch.ForeColor = System.Drawing.Color.Gray
        Me.txtSearch.Location = New System.Drawing.Point(77, 19)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(729, 27)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Text = "Search menu items..."
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblSearch.Location = New System.Drawing.Point(13, 22)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(59, 20)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "Search:"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.Category)
        Me.Panel3.Controls.Add(Me.lblFilter)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 122)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(9)
        Me.Panel3.Size = New System.Drawing.Size(1219, 48)
        Me.Panel3.TabIndex = 2
        '
        'Category
        '
        Me.Category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Category.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Category.FormattingEnabled = True
        Me.Category.Location = New System.Drawing.Point(136, 11)
        Me.Category.Name = "Category"
        Me.Category.Size = New System.Drawing.Size(258, 25)
        Me.Category.TabIndex = 1
        '
        'lblFilter
        '
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblFilter.Location = New System.Drawing.Point(13, 13)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(117, 20)
        Me.lblFilter.TabIndex = 0
        Me.lblFilter.Text = "Filter Category:"
        '
        'DataGridMenu
        '
        Me.DataGridMenu.AllowUserToAddRows = False
        Me.DataGridMenu.AllowUserToDeleteRows = False
        Me.DataGridMenu.BackgroundColor = System.Drawing.Color.White
        Me.DataGridMenu.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridMenu.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DataGridMenu.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridMenu.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridMenu.ColumnHeadersHeight = 50
        Me.DataGridMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridMenu.EnableHeadersVisualStyles = False
        Me.DataGridMenu.Location = New System.Drawing.Point(0, 170)
        Me.DataGridMenu.MultiSelect = False
        Me.DataGridMenu.Name = "DataGridMenu"
        Me.DataGridMenu.ReadOnly = True
        Me.DataGridMenu.RowHeadersVisible = False
        Me.DataGridMenu.RowHeadersWidth = 51
        Me.DataGridMenu.RowTemplate.Height = 40
        Me.DataGridMenu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridMenu.Size = New System.Drawing.Size(1219, 444)
        Me.DataGridMenu.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel4.Controls.Add(Me.lblTotalItems)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 614)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1219, 35)
        Me.Panel4.TabIndex = 4
        '
        'lblTotalItems
        '
        Me.lblTotalItems.AutoSize = True
        Me.lblTotalItems.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalItems.Location = New System.Drawing.Point(13, 9)
        Me.lblTotalItems.Name = "lblTotalItems"
        Me.lblTotalItems.Size = New System.Drawing.Size(98, 19)
        Me.lblTotalItems.TabIndex = 0
        Me.lblTotalItems.Text = "Total Items: 0"
        '
        'MenuItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.GhostWhite
        Me.ClientSize = New System.Drawing.Size(1219, 649)
        Me.Controls.Add(Me.DataGridMenu)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "MenuItems"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Menu Items Management - Tabeya"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.DataGridMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents AddMenuItemsbtn As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnToggleAvailability As Button
    Friend WithEvents btnExport As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lblFilter As Label
    Friend WithEvents Category As ComboBox
    Friend WithEvents DataGridMenu As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lblTotalItems As Label
End Class