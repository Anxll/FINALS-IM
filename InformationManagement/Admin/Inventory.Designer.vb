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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Inventory))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RoundedPane21 = New InformationManagement.RoundedPane2()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.RoundedPane22 = New InformationManagement.RoundedPane2()
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
        Me.AddItem = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.RoundedPane21.SuspendLayout()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RoundedPane22.SuspendLayout()
        CType(Me.InventoryGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(27, 38)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 23)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Total Items :"
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
        Me.RoundedPane21.Location = New System.Drawing.Point(47, 178)
        Me.RoundedPane21.Margin = New System.Windows.Forms.Padding(4)
        Me.RoundedPane21.Name = "RoundedPane21"
        Me.RoundedPane21.Size = New System.Drawing.Size(411, 170)
        Me.RoundedPane21.TabIndex = 35
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(24, 102)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 37)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "8"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label4.Location = New System.Drawing.Point(33, 121)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 19)
        Me.Label4.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label10.Location = New System.Drawing.Point(33, 32)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(93, 23)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Total Value"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PictureBox7
        '
        Me.PictureBox7.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(41, 107)
        Me.PictureBox7.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(27, 25)
        Me.PictureBox7.TabIndex = 2
        Me.PictureBox7.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(72, 102)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(161, 30)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "10,750,000.00"
        '
        'PictureBox8
        '
        Me.PictureBox8.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox8.Image = CType(resources.GetObject("PictureBox8.Image"), System.Drawing.Image)
        Me.PictureBox8.Location = New System.Drawing.Point(387, 17)
        Me.PictureBox8.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(37, 34)
        Me.PictureBox8.TabIndex = 4
        Me.PictureBox8.TabStop = False
        '
        'RoundedPane22
        '
        Me.RoundedPane22.BorderColor = System.Drawing.Color.LightGray
        Me.RoundedPane22.BorderThickness = 1
        Me.RoundedPane22.Controls.Add(Me.Label4)
        Me.RoundedPane22.Controls.Add(Me.Label10)
        Me.RoundedPane22.Controls.Add(Me.PictureBox7)
        Me.RoundedPane22.Controls.Add(Me.Label11)
        Me.RoundedPane22.Controls.Add(Me.PictureBox8)
        Me.RoundedPane22.CornerRadius = 15
        Me.RoundedPane22.FillColor = System.Drawing.Color.White
        Me.RoundedPane22.Location = New System.Drawing.Point(521, 178)
        Me.RoundedPane22.Margin = New System.Windows.Forms.Padding(4)
        Me.RoundedPane22.Name = "RoundedPane22"
        Me.RoundedPane22.Size = New System.Drawing.Size(447, 170)
        Me.RoundedPane22.TabIndex = 36
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
        Me.InventoryGrid.Location = New System.Drawing.Point(47, 502)
        Me.InventoryGrid.Margin = New System.Windows.Forms.Padding(4)
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
        Me.InventoryGrid.Size = New System.Drawing.Size(1516, 181)
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
        Me.Label7.Location = New System.Drawing.Point(1024, 425)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 23)
        Me.Label7.TabIndex = 39
        Me.Label7.Text = "Category :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(43, 425)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 23)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Search Item :"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(47, 451)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(970, 22)
        Me.TextBox1.TabIndex = 38
        '
        'Category
        '
        Me.Category.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Category.FormattingEnabled = True
        Me.Category.Items.AddRange(New Object() {"Meat & Poultry", "Vegetables", "Dairy", "Dry Goods", "Oils & Condiments", "Herbs & Spices", "Beverages", "Seafood"})
        Me.Category.Location = New System.Drawing.Point(1023, 451)
        Me.Category.Name = "Category"
        Me.Category.Size = New System.Drawing.Size(250, 24)
        Me.Category.TabIndex = 37
        '
        'AddItem
        '
        Me.AddItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.AddItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.AddItem.Location = New System.Drawing.Point(1191, 27)
        Me.AddItem.Name = "AddItem"
        Me.AddItem.Size = New System.Drawing.Size(165, 56)
        Me.AddItem.TabIndex = 34
        Me.AddItem.Text = "+      Add Item"
        Me.AddItem.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(12, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(373, 20)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Track and manage your restaurant supplies"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(423, 36)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Restaurant Inventory System"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1478, 105)
        Me.Splitter1.TabIndex = 31
        Me.Splitter1.TabStop = False
        '
        'Inventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1478, 931)
        Me.Controls.Add(Me.RoundedPane21)
        Me.Controls.Add(Me.RoundedPane22)
        Me.Controls.Add(Me.InventoryGrid)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Category)
        Me.Controls.Add(Me.AddItem)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "Inventory"
        Me.Text = "Inventory"
        Me.RoundedPane21.ResumeLayout(False)
        Me.RoundedPane21.PerformLayout()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RoundedPane22.ResumeLayout(False)
        Me.RoundedPane22.PerformLayout()
        CType(Me.InventoryGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents RoundedPane21 As RoundedPane2
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents PictureBox7 As PictureBox
    Friend WithEvents Label11 As Label
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents RoundedPane22 As RoundedPane2
    Friend WithEvents InventoryGrid As DataGridView
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Category As ComboBox
    Friend WithEvents AddItem As Button
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
