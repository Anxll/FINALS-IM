<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddNewItems
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddNewItems))
        Me.AddItem = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.RoundedTextBox1 = New InformationManagement.RoundedTextBox()
        Me.Unit = New System.Windows.Forms.ComboBox()
        Me.Quantity = New InformationManagement.RoundedTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtFullName = New InformationManagement.RoundedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Category = New System.Windows.Forms.ComboBox()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AddItem
        '
        Me.AddItem.BackColor = System.Drawing.Color.DarkRed
        Me.AddItem.FlatAppearance.BorderSize = 0
        Me.AddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.AddItem.Image = CType(resources.GetObject("AddItem.Image"), System.Drawing.Image)
        Me.AddItem.Location = New System.Drawing.Point(388, 473)
        Me.AddItem.Margin = New System.Windows.Forms.Padding(2)
        Me.AddItem.Name = "AddItem"
        Me.AddItem.Size = New System.Drawing.Size(129, 38)
        Me.AddItem.TabIndex = 61
        Me.AddItem.Text = " Add Item"
        Me.AddItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.AddItem.UseVisualStyleBackColor = False
        '
        'Cancel
        '
        Me.Cancel.BackColor = System.Drawing.SystemColors.Highlight
        Me.Cancel.FlatAppearance.BorderSize = 0
        Me.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Cancel.Location = New System.Drawing.Point(283, 473)
        Me.Cancel.Margin = New System.Windows.Forms.Padding(2)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(88, 38)
        Me.Cancel.TabIndex = 60
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.DateTimePicker1.Location = New System.Drawing.Point(296, 347)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(204, 29)
        Me.DateTimePicker1.TabIndex = 54
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label8.Location = New System.Drawing.Point(296, 318)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(120, 21)
        Me.Label8.TabIndex = 53
        Me.Label8.Text = "Last Restocked"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label7.Location = New System.Drawing.Point(25, 318)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 21)
        Me.Label7.TabIndex = 52
        Me.Label7.Text = "Cost per Unit"
        '
        'RoundedTextBox1
        '
        Me.RoundedTextBox1.BackColor = System.Drawing.Color.Transparent
        Me.RoundedTextBox1.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.RoundedTextBox1.Location = New System.Drawing.Point(26, 340)
        Me.RoundedTextBox1.MaxLength = 32767
        Me.RoundedTextBox1.MinimumSize = New System.Drawing.Size(50, 20)
        Me.RoundedTextBox1.Multiline = False
        Me.RoundedTextBox1.Name = "RoundedTextBox1"
        Me.RoundedTextBox1.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.RoundedTextBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.RoundedTextBox1.ReadOnly = False
        Me.RoundedTextBox1.Size = New System.Drawing.Size(230, 36)
        Me.RoundedTextBox1.TabIndex = 48
        Me.RoundedTextBox1.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.RoundedTextBox1.TextColor = System.Drawing.Color.Black
        Me.RoundedTextBox1.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'Unit
        '
        Me.Unit.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Unit.FormattingEnabled = True
        Me.Unit.Items.AddRange(New Object() {"kg", "liters", "pieces", "boxes", "grams", "bottles"})
        Me.Unit.Location = New System.Drawing.Point(296, 203)
        Me.Unit.Margin = New System.Windows.Forms.Padding(2)
        Me.Unit.Name = "Unit"
        Me.Unit.Size = New System.Drawing.Size(188, 29)
        Me.Unit.TabIndex = 51
        '
        'Quantity
        '
        Me.Quantity.BackColor = System.Drawing.Color.Transparent
        Me.Quantity.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.Quantity.Location = New System.Drawing.Point(29, 196)
        Me.Quantity.MaxLength = 32767
        Me.Quantity.MinimumSize = New System.Drawing.Size(50, 20)
        Me.Quantity.Multiline = False
        Me.Quantity.Name = "Quantity"
        Me.Quantity.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Quantity.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Quantity.ReadOnly = False
        Me.Quantity.Size = New System.Drawing.Size(230, 36)
        Me.Quantity.TabIndex = 45
        Me.Quantity.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.Quantity.TextColor = System.Drawing.Color.Black
        Me.Quantity.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Location = New System.Drawing.Point(25, 171)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(121, 21)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "Stock Quantity "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label5.Location = New System.Drawing.Point(296, 171)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 21)
        Me.Label5.TabIndex = 49
        Me.Label5.Text = "Unit"
        '
        'txtFullName
        '
        Me.txtFullName.BackColor = System.Drawing.Color.Transparent
        Me.txtFullName.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtFullName.Location = New System.Drawing.Point(29, 117)
        Me.txtFullName.MaxLength = 32767
        Me.txtFullName.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtFullName.Multiline = False
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtFullName.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtFullName.ReadOnly = False
        Me.txtFullName.Size = New System.Drawing.Size(230, 36)
        Me.txtFullName.TabIndex = 43
        Me.txtFullName.TextBoxBackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFullName.TextColor = System.Drawing.Color.Black
        Me.txtFullName.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label3.Location = New System.Drawing.Point(25, 90)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 21)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Item Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(23, 54)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(321, 15)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Enter the details of the new inventory item below."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(20, 19)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(267, 26)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Add New Inventory Item"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DateTimePicker2.CalendarTitleForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DateTimePicker2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.DateTimePicker2.Location = New System.Drawing.Point(296, 411)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(204, 29)
        Me.DateTimePicker2.TabIndex = 63
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label11.Location = New System.Drawing.Point(296, 388)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(125, 21)
        Me.Label11.TabIndex = 62
        Me.Label11.Text = "Expiration Date "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label9.Location = New System.Drawing.Point(25, 248)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(129, 21)
        Me.Label9.TabIndex = 55
        Me.Label9.Text = "Min Stock Level "
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label10.Location = New System.Drawing.Point(296, 248)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(128, 21)
        Me.Label10.TabIndex = 56
        Me.Label10.Text = "Max Stock Level"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NumericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NumericUpDown1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumericUpDown1.Location = New System.Drawing.Point(29, 273)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(180, 25)
        Me.NumericUpDown1.TabIndex = 57
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NumericUpDown2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NumericUpDown2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumericUpDown2.Location = New System.Drawing.Point(296, 273)
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(180, 25)
        Me.NumericUpDown2.TabIndex = 58
        Me.NumericUpDown2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Location = New System.Drawing.Point(292, 90)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 21)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Catergory "
        '
        'Category
        '
        Me.Category.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Category.FormattingEnabled = True
        Me.Category.Items.AddRange(New Object() {"Meat & Poultry", "Vegetables", "Dairy", "Dry Goods", "Oils & Condiments", "Herbs & Spices", "Beverages", "Seafood"})
        Me.Category.Location = New System.Drawing.Point(296, 117)
        Me.Category.Margin = New System.Windows.Forms.Padding(2)
        Me.Category.Name = "Category"
        Me.Category.Size = New System.Drawing.Size(188, 29)
        Me.Category.TabIndex = 47
        '
        'AddNewItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(548, 519)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.AddItem)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.NumericUpDown2)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.RoundedTextBox1)
        Me.Controls.Add(Me.Unit)
        Me.Controls.Add(Me.Quantity)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Category)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtFullName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "AddNewItems"
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AddItem As Button
    Friend WithEvents Cancel As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents RoundedTextBox1 As RoundedTextBox
    Friend WithEvents Unit As ComboBox
    Friend WithEvents Quantity As RoundedTextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtFullName As RoundedTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents Category As ComboBox
End Class
