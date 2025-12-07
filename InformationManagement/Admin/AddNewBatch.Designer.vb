<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddNewBatch
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.lblIngredientCaption = New System.Windows.Forms.Label()
        Me.lblIngredientName = New System.Windows.Forms.Label()
        Me.lblQuantity = New System.Windows.Forms.Label()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.lblCostPerUnit = New System.Windows.Forms.Label()
        Me.txtCostPerUnit = New System.Windows.Forms.TextBox()
        Me.lblPurchaseDate = New System.Windows.Forms.Label()
        Me.dtpPurchaseDate = New System.Windows.Forms.DateTimePicker()
        Me.lblExpirationDate = New System.Windows.Forms.Label()
        Me.dtpExpirationDate = New System.Windows.Forms.DateTimePicker()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblStorageLocation = New System.Windows.Forms.Label()
        Me.cmbStorageLocation = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(24, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(174, 30)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Add New Batch"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSubtitle.ForeColor = System.Drawing.Color.DimGray
        Me.lblSubtitle.Location = New System.Drawing.Point(26, 39)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(246, 15)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "Create an additional batch for this ingredient."
        '
        'lblIngredientCaption
        '
        Me.lblIngredientCaption.AutoSize = True
        Me.lblIngredientCaption.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblIngredientCaption.Location = New System.Drawing.Point(26, 84)
        Me.lblIngredientCaption.Name = "lblIngredientCaption"
        Me.lblIngredientCaption.Size = New System.Drawing.Size(72, 15)
        Me.lblIngredientCaption.TabIndex = 2
        Me.lblIngredientCaption.Text = "Ingredient: "
        '
        'lblIngredientName
        '
        Me.lblIngredientName.AutoSize = True
        Me.lblIngredientName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblIngredientName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblIngredientName.Location = New System.Drawing.Point(108, 84)
        Me.lblIngredientName.Name = "lblIngredientName"
        Me.lblIngredientName.Size = New System.Drawing.Size(110, 15)
        Me.lblIngredientName.TabIndex = 3
        Me.lblIngredientName.Text = "[Ingredient Name]"
        '
        'lblQuantity
        '
        Me.lblQuantity.AutoSize = True
        Me.lblQuantity.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblQuantity.Location = New System.Drawing.Point(26, 125)
        Me.lblQuantity.Name = "lblQuantity"
        Me.lblQuantity.Size = New System.Drawing.Size(55, 15)
        Me.lblQuantity.TabIndex = 4
        Me.lblQuantity.Text = "Quantity"
        '
        'txtQuantity
        '
        Me.txtQuantity.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtQuantity.Location = New System.Drawing.Point(26, 143)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(150, 23)
        Me.txtQuantity.TabIndex = 5
        '
        'lblUnit
        '
        Me.lblUnit.AutoSize = True
        Me.lblUnit.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblUnit.Location = New System.Drawing.Point(196, 125)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(31, 15)
        Me.lblUnit.TabIndex = 6
        Me.lblUnit.Text = "Unit"
        '
        'txtUnit
        '
        Me.txtUnit.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtUnit.Location = New System.Drawing.Point(196, 143)
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.Size = New System.Drawing.Size(120, 23)
        Me.txtUnit.TabIndex = 7
        '
        'lblCostPerUnit
        '
        Me.lblCostPerUnit.AutoSize = True
        Me.lblCostPerUnit.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCostPerUnit.Location = New System.Drawing.Point(340, 125)
        Me.lblCostPerUnit.Name = "lblCostPerUnit"
        Me.lblCostPerUnit.Size = New System.Drawing.Size(80, 15)
        Me.lblCostPerUnit.TabIndex = 8
        Me.lblCostPerUnit.Text = "Cost per Unit"
        '
        'txtCostPerUnit
        '
        Me.txtCostPerUnit.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtCostPerUnit.Location = New System.Drawing.Point(340, 143)
        Me.txtCostPerUnit.Name = "txtCostPerUnit"
        Me.txtCostPerUnit.Size = New System.Drawing.Size(150, 23)
        Me.txtCostPerUnit.TabIndex = 9
        '
        'lblPurchaseDate
        '
        Me.lblPurchaseDate.AutoSize = True
        Me.lblPurchaseDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPurchaseDate.Location = New System.Drawing.Point(26, 185)
        Me.lblPurchaseDate.Name = "lblPurchaseDate"
        Me.lblPurchaseDate.Size = New System.Drawing.Size(87, 15)
        Me.lblPurchaseDate.TabIndex = 10
        Me.lblPurchaseDate.Text = "Purchase Date"
        '
        'dtpPurchaseDate
        '
        Me.dtpPurchaseDate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpPurchaseDate.Location = New System.Drawing.Point(26, 203)
        Me.dtpPurchaseDate.Name = "dtpPurchaseDate"
        Me.dtpPurchaseDate.Size = New System.Drawing.Size(150, 23)
        Me.dtpPurchaseDate.TabIndex = 11
        '
        'lblExpirationDate
        '
        Me.lblExpirationDate.AutoSize = True
        Me.lblExpirationDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblExpirationDate.Location = New System.Drawing.Point(196, 185)
        Me.lblExpirationDate.Name = "lblExpirationDate"
        Me.lblExpirationDate.Size = New System.Drawing.Size(93, 15)
        Me.lblExpirationDate.TabIndex = 12
        Me.lblExpirationDate.Text = "Expiration Date"
        '
        'dtpExpirationDate
        '
        Me.dtpExpirationDate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpExpirationDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpExpirationDate.Location = New System.Drawing.Point(196, 203)
        Me.dtpExpirationDate.Name = "dtpExpirationDate"
        Me.dtpExpirationDate.Size = New System.Drawing.Size(150, 23)
        Me.dtpExpirationDate.TabIndex = 13
        '
        'lblNotes
        '
        Me.lblNotes.AutoSize = True
        Me.lblNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNotes.Location = New System.Drawing.Point(26, 305)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(40, 15)
        Me.lblNotes.TabIndex = 16
        Me.lblNotes.Text = "Notes"
        '
        'txtNotes
        '
        Me.txtNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtNotes.Location = New System.Drawing.Point(26, 323)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(464, 120)
        Me.txtNotes.TabIndex = 17
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(320, 451)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(85, 34)
        Me.btnSave.TabIndex = 18
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(423, 449)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(85, 34)
        Me.btnCancel.TabIndex = 19
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'lblStorageLocation
        '
        Me.lblStorageLocation.AutoSize = True
        Me.lblStorageLocation.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblStorageLocation.Location = New System.Drawing.Point(26, 245)
        Me.lblStorageLocation.Name = "lblStorageLocation"
        Me.lblStorageLocation.Size = New System.Drawing.Size(101, 15)
        Me.lblStorageLocation.TabIndex = 20
        Me.lblStorageLocation.Text = "Storage Location"
        '
        'cmbStorageLocation
        '
        Me.cmbStorageLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStorageLocation.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cmbStorageLocation.FormattingEnabled = True
        Me.cmbStorageLocation.Items.AddRange(New Object() {"Freezer-Meat", "Freezer-Seafood", "Freezer-Processed", "Refrigerator-Dairy", "Refrigerator-Vegetables", "Refrigerator-Condiments", "Pantry-Dry-Goods", "Pantry-Canned", "Pantry-Condiments", "Pantry-Spices", "Pantry-Beverages"})
        Me.cmbStorageLocation.Location = New System.Drawing.Point(26, 263)
        Me.cmbStorageLocation.Name = "cmbStorageLocation"
        Me.cmbStorageLocation.Size = New System.Drawing.Size(230, 23)
        Me.cmbStorageLocation.TabIndex = 15
        '
        'AddNewBatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(520, 497)
        Me.Controls.Add(Me.cmbStorageLocation)
        Me.Controls.Add(Me.lblStorageLocation)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.lblNotes)
        Me.Controls.Add(Me.dtpExpirationDate)
        Me.Controls.Add(Me.lblExpirationDate)
        Me.Controls.Add(Me.dtpPurchaseDate)
        Me.Controls.Add(Me.lblPurchaseDate)
        Me.Controls.Add(Me.txtCostPerUnit)
        Me.Controls.Add(Me.lblCostPerUnit)
        Me.Controls.Add(Me.txtUnit)
        Me.Controls.Add(Me.lblUnit)
        Me.Controls.Add(Me.txtQuantity)
        Me.Controls.Add(Me.lblQuantity)
        Me.Controls.Add(Me.lblIngredientName)
        Me.Controls.Add(Me.lblIngredientCaption)
        Me.Controls.Add(Me.lblSubtitle)
        Me.Controls.Add(Me.lblTitle)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddNewBatch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblSubtitle As System.Windows.Forms.Label
    Friend WithEvents lblIngredientCaption As System.Windows.Forms.Label
    Friend WithEvents lblIngredientName As System.Windows.Forms.Label
    Friend WithEvents lblQuantity As System.Windows.Forms.Label
    Friend WithEvents txtQuantity As System.Windows.Forms.TextBox
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents lblCostPerUnit As System.Windows.Forms.Label
    Friend WithEvents txtCostPerUnit As System.Windows.Forms.TextBox
    Friend WithEvents lblPurchaseDate As System.Windows.Forms.Label
    Friend WithEvents dtpPurchaseDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblExpirationDate As System.Windows.Forms.Label
    Friend WithEvents dtpExpirationDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblStorageLocation As Label
    Friend WithEvents cmbStorageLocation As ComboBox
End Class
