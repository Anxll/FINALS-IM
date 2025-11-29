<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Inventory
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Inventory))
        Me.InventoryGrid = New System.Windows.Forms.DataGridView()
        Me.ItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InventoryID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CostUnit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LostStock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpirationDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Actions = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Category = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.RoundedPane21 = New InformationManagement.RoundedPane2()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RoundedPane22 = New InformationManagement.RoundedPane2()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        CType(Me.InventoryGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RoundedPane21.SuspendLayout()
        Me.RoundedPane22.SuspendLayout()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'InventoryGrid
        '
        Me.InventoryGrid.AllowUserToResizeColumns = False
        Me.InventoryGrid.AllowUserToResizeRows = False
        Me.InventoryGrid.BackgroundColor = System.Drawing.Color.White
        Me.InventoryGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.InventoryGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.InventoryGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.InventoryGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.InventoryGrid.ColumnHeadersHeight = 40
        Me.InventoryGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ItemName, Me.InventoryID, Me.ProductID, Me.Quantity, Me.Status, Me.CostUnit, Me.TotalValue, Me.LostStock, Me.Type, Me.ExpirationDate, Me.Actions})
        Me.InventoryGrid.EnableHeadersVisualStyles = False
        Me.InventoryGrid.Location = New System.Drawing.Point(35, 408)
        Me.InventoryGrid.Name = "InventoryGrid"
        Me.InventoryGrid.ReadOnly = True
        Me.InventoryGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.InventoryGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.InventoryGrid.RowHeadersVisible = False
        Me.InventoryGrid.RowHeadersWidth = 51
        Me.InventoryGrid.Size = New System.Drawing.Size(1137, 147)
        Me.InventoryGrid.TabIndex = 40
        '
        'ItemName
        '
        Me.ItemName.DataPropertyName = "colItemName"
        Me.ItemName.Frozen = True
        Me.ItemName.HeaderText = "Item Name"
        Me.ItemName.MinimumWidth = 6
        Me.ItemName.Name = "ItemName"
        Me.ItemName.ReadOnly = True
        Me.ItemName.Width = 150
        '
        'InventoryID
        '
        Me.InventoryID.Frozen = True
        Me.InventoryID.HeaderText = "Inventory ID"
        Me.InventoryID.MinimumWidth = 6
        Me.InventoryID.Name = "InventoryID"
        Me.InventoryID.ReadOnly = True
        Me.InventoryID.Width = 125
        '
        'ProductID
        '
        Me.ProductID.Frozen = True
        Me.ProductID.HeaderText = "Product ID"
        Me.ProductID.MinimumWidth = 6
        Me.ProductID.Name = "ProductID"
        Me.ProductID.ReadOnly = True
        Me.ProductID.Width = 125
        '
        'Quantity
        '
        Me.Quantity.DataPropertyName = "colQuantity"
        Me.Quantity.Frozen = True
        Me.Quantity.HeaderText = "Quantity"
        Me.Quantity.MinimumWidth = 6
        Me.Quantity.Name = "Quantity"
        Me.Quantity.ReadOnly = True
        Me.Quantity.Width = 120
        '
        'Status
        '
        Me.Status.DataPropertyName = "colStatus"
        Me.Status.Frozen = True
        Me.Status.HeaderText = "Status"
        Me.Status.MinimumWidth = 6
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Width = 120
        '
        'CostUnit
        '
        Me.CostUnit.DataPropertyName = "colCostUnit"
        Me.CostUnit.Frozen = True
        Me.CostUnit.HeaderText = "Cost/Unit"
        Me.CostUnit.MinimumWidth = 6
        Me.CostUnit.Name = "CostUnit"
        Me.CostUnit.ReadOnly = True
        Me.CostUnit.Width = 125
        '
        'TotalValue
        '
        Me.TotalValue.DataPropertyName = "colTotalValue"
        Me.TotalValue.Frozen = True
        Me.TotalValue.HeaderText = "Total Value"
        Me.TotalValue.MinimumWidth = 6
        Me.TotalValue.Name = "TotalValue"
        Me.TotalValue.ReadOnly = True
        Me.TotalValue.Width = 120
        '
        'LostStock
        '
        Me.LostStock.DataPropertyName = "colLostStock"
        Me.LostStock.Frozen = True
        Me.LostStock.HeaderText = "Last Stock"
        Me.LostStock.MinimumWidth = 6
        Me.LostStock.Name = "LostStock"
        Me.LostStock.ReadOnly = True
        Me.LostStock.Width = 120
        '
        'Type
        '
        Me.Type.DataPropertyName = "colType"
        Me.Type.HeaderText = "Unit Type"
        Me.Type.MinimumWidth = 6
        Me.Type.Name = "Type"
        Me.Type.ReadOnly = True
        Me.Type.Width = 120
        '
        'ExpirationDate
        '
        Me.ExpirationDate.DataPropertyName = "colExpirationDate"
        Me.ExpirationDate.HeaderText = "Expiration Date"
        Me.ExpirationDate.MinimumWidth = 6
        Me.ExpirationDate.Name = "ExpirationDate"
        Me.ExpirationDate.ReadOnly = True
        Me.ExpirationDate.Width = 120
        '
        'Actions
        '
        Me.Actions.DataPropertyName = "colActions"
        Me.Actions.HeaderText = "Actions"
        Me.Actions.MinimumWidth = 6
        Me.Actions.Name = "Actions"
        Me.Actions.ReadOnly = True
        Me.Actions.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Actions.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Actions.UseColumnTextForButtonValue = True
        Me.Actions.Width = 120
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(768, 345)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 17)
        Me.Label7.TabIndex = 39
        Me.Label7.Text = "Category :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(32, 345)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 17)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Search Item :"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(35, 366)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(728, 20)
        Me.TextBox1.TabIndex = 38
        '
        'Category
        '
        Me.Category.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Category.FormattingEnabled = True
        Me.Category.Items.AddRange(New Object() {"Meat & Poultry", "Vegetables", "Dairy", "Dry Goods", "Oils & Condiments", "Herbs & Spices", "Beverages", "Seafood"})
        Me.Category.Location = New System.Drawing.Point(767, 366)
        Me.Category.Margin = New System.Windows.Forms.Padding(2)
        Me.Category.Name = "Category"
        Me.Category.Size = New System.Drawing.Size(188, 21)
        Me.Category.TabIndex = 37
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(9, 51)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(325, 17)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Track and manage your restaurant supplies"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 22)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(342, 29)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Restaurant Inventory System"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Margin = New System.Windows.Forms.Padding(2)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1108, 85)
        Me.Splitter1.TabIndex = 31
        Me.Splitter1.TabStop = False
        '
        'RoundedPane21
        '
        Me.RoundedPane21.BackColor = System.Drawing.Color.Transparent
        Me.RoundedPane21.BorderColor = System.Drawing.Color.LightGray
        Me.RoundedPane21.BorderThickness = 1
        Me.RoundedPane21.Controls.Add(Me.Label5)
        Me.RoundedPane21.Controls.Add(Me.Label3)
        Me.RoundedPane21.CornerRadius = 15
        Me.RoundedPane21.FillColor = System.Drawing.Color.White
        Me.RoundedPane21.Location = New System.Drawing.Point(35, 145)
        Me.RoundedPane21.Name = "RoundedPane21"
        Me.RoundedPane21.Size = New System.Drawing.Size(308, 138)
        Me.RoundedPane21.TabIndex = 35
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(18, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(25, 30)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "8"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Total Items :"
        '
        'RoundedPane22
        '
        Me.RoundedPane22.BorderColor = System.Drawing.Color.LightGray
        Me.RoundedPane22.BorderThickness = 1
        Me.RoundedPane22.Controls.Add(Me.Label4)
        Me.RoundedPane22.Controls.Add(Me.Label10)
        Me.RoundedPane22.Controls.Add(Me.Label11)
        Me.RoundedPane22.Controls.Add(Me.PictureBox8)
        Me.RoundedPane22.CornerRadius = 15
        Me.RoundedPane22.FillColor = System.Drawing.Color.White
        Me.RoundedPane22.Location = New System.Drawing.Point(391, 145)
        Me.RoundedPane22.Name = "RoundedPane22"
        Me.RoundedPane22.Size = New System.Drawing.Size(335, 138)
        Me.RoundedPane22.TabIndex = 36
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label4.Location = New System.Drawing.Point(25, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 13)
        Me.Label4.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label10.Location = New System.Drawing.Point(25, 26)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 17)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Total Value"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(54, 83)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(125, 23)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "10,750,000.00"
        '
        'PictureBox8
        '
        Me.PictureBox8.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(104, 20)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(28, 28)
        Me.PictureBox8.TabIndex = 4
        Me.PictureBox8.TabStop = False
        '
        'Inventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1108, 756)
        Me.Controls.Add(Me.RoundedPane21)
        Me.Controls.Add(Me.RoundedPane22)
        Me.Controls.Add(Me.InventoryGrid)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Category)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Splitter1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Inventory"
        Me.Text = "Inventory"
        CType(Me.InventoryGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RoundedPane21.ResumeLayout(False)
        Me.RoundedPane21.PerformLayout()
        Me.RoundedPane22.ResumeLayout(False)
        Me.RoundedPane22.PerformLayout()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents RoundedPane21 As RoundedPane2
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents RoundedPane22 As RoundedPane2
    Friend WithEvents InventoryGrid As DataGridView
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Category As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents ItemName As DataGridViewTextBoxColumn
    Friend WithEvents InventoryID As DataGridViewTextBoxColumn
    Friend WithEvents ProductID As DataGridViewTextBoxColumn
    Friend WithEvents Quantity As DataGridViewTextBoxColumn
    Friend WithEvents Status As DataGridViewTextBoxColumn
    Friend WithEvents CostUnit As DataGridViewTextBoxColumn
    Friend WithEvents TotalValue As DataGridViewTextBoxColumn
    Friend WithEvents LostStock As DataGridViewTextBoxColumn
    Friend WithEvents Type As DataGridViewTextBoxColumn
    Friend WithEvents ExpirationDate As DataGridViewTextBoxColumn
    Friend WithEvents Actions As DataGridViewButtonColumn
End Class
