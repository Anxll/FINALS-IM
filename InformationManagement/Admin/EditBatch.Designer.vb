Partial Class EditBatch
    Inherits System.Windows.Forms.Form

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

    Private Sub InitializeComponent()
        Me.SaveChanges = New System.Windows.Forms.Button()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.RoundedTextBox1 = New InformationManagement.RoundedTextBox()
        Me.Unit = New System.Windows.Forms.ComboBox()
        Me.Quantity = New InformationManagement.RoundedTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtItemName = New InformationManagement.RoundedTextBox()
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
        Me.txtBatchNumber = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtOriginalQty = New System.Windows.Forms.TextBox()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SaveChanges
        '
        Me.SaveChanges.BackColor = System.Drawing.Color.DarkRed
        Me.SaveChanges.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveChanges.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.SaveChanges.Location = New System.Drawing.Point(446, 520)
        Me.SaveChanges.Margin = New System.Windows.Forms.Padding(2)
        Me.SaveChanges.Name = "SaveChanges"
        Me.SaveChanges.Size = New System.Drawing.Size(91, 38)
        Me.SaveChanges.TabIndex = 61
        Me.SaveChanges.Text = "Save"
        Me.SaveChanges.UseVisualStyleBackColor = False
        '
        'Cancel
        '
        Me.Cancel.BackColor = System.Drawing.SystemColors.Highlight
        Me.Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Cancel.Location = New System.Drawing.Point(352, 520)
        Me.Cancel.Margin = New System.Windows.Forms.Padding(2)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(88, 38)
        Me.Cancel.TabIndex = 60
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CalendarFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.DateTimePicker1.Location = New System.Drawing.Point(296, 363)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(2)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(204, 29)
        Me.DateTimePicker1.TabIndex = 54
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(292, 340)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(117, 21)
        Me.Label8.TabIndex = 53
        Me.Label8.Text = "Purchase Date "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(18, 340)
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
        Me.RoundedTextBox1.Location = New System.Drawing.Point(22, 364)
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
        Me.Unit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.Unit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Unit.FormattingEnabled = True
        Me.Unit.ItemHeight = 26
        Me.Unit.Items.AddRange(New Object() {"kg", "liters", "pieces", "boxes", "grams", "bottles"})
        Me.Unit.Location = New System.Drawing.Point(296, 203)
        Me.Unit.Margin = New System.Windows.Forms.Padding(2)
        Me.Unit.Name = "Unit"
        Me.Unit.Size = New System.Drawing.Size(188, 32)
        Me.Unit.TabIndex = 51
        '
        'Quantity
        '
        Me.Quantity.BackColor = System.Drawing.Color.Transparent
        Me.Quantity.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.Quantity.Location = New System.Drawing.Point(22, 203)
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
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(18, 176)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(140, 21)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "Current Stock Qty"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(292, 176)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 21)
        Me.Label5.TabIndex = 49
        Me.Label5.Text = "Unit"
        '
        'txtItemName
        '
        Me.txtItemName.BackColor = System.Drawing.Color.Transparent
        Me.txtItemName.FocusBorderColor = System.Drawing.Color.DarkGray
        Me.txtItemName.Location = New System.Drawing.Point(22, 122)
        Me.txtItemName.MaxLength = 32767
        Me.txtItemName.MinimumSize = New System.Drawing.Size(50, 20)
        Me.txtItemName.Multiline = False
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.NormalBorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtItemName.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtItemName.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(230, 36)
        Me.txtItemName.TabIndex = 43
        Me.txtItemName.TextBoxBackColor = System.Drawing.Color.Gainsboro
        Me.txtItemName.TextColor = System.Drawing.Color.Black
        Me.txtItemName.TextFont = New System.Drawing.Font("Segoe UI", 10.0!)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(18, 89)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 21)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Item Name "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(20, 41)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(281, 15)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Update the details of the batch item below."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(17, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(247, 32)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Edit Inventory Batch"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.DateTimePicker2.Location = New System.Drawing.Point(296, 444)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(2)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(204, 29)
        Me.DateTimePicker2.TabIndex = 63
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(292, 420)
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
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(18, 257)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(125, 21)
        Me.Label9.TabIndex = 55
        Me.Label9.Text = "Min Stock Level"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(292, 257)
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
        Me.NumericUpDown1.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.NumericUpDown1.Location = New System.Drawing.Point(22, 292)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(180, 27)
        Me.NumericUpDown1.TabIndex = 57
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NumericUpDown2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NumericUpDown2.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.NumericUpDown2.Location = New System.Drawing.Point(296, 292)
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(180, 27)
        Me.NumericUpDown2.TabIndex = 58
        Me.NumericUpDown2.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(292, 89)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 21)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Batch Number"
        '
        'txtBatchNumber
        '
        Me.txtBatchNumber.BackColor = System.Drawing.Color.Gainsboro
        Me.txtBatchNumber.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBatchNumber.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtBatchNumber.Location = New System.Drawing.Point(296, 122)
        Me.txtBatchNumber.Multiline = True
        Me.txtBatchNumber.Name = "txtBatchNumber"
        Me.txtBatchNumber.ReadOnly = True
        Me.txtBatchNumber.Size = New System.Drawing.Size(188, 36)
        Me.txtBatchNumber.TabIndex = 64
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(18, 420)
        Me.Label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(133, 21)
        Me.Label12.TabIndex = 65
        Me.Label12.Text = "Original Quantity"
        '
        'txtOriginalQty
        '
        Me.txtOriginalQty.BackColor = System.Drawing.Color.Gainsboro
        Me.txtOriginalQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOriginalQty.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtOriginalQty.Location = New System.Drawing.Point(22, 444)
        Me.txtOriginalQty.Multiline = True
        Me.txtOriginalQty.Name = "txtOriginalQty"
        Me.txtOriginalQty.ReadOnly = True
        Me.txtOriginalQty.Size = New System.Drawing.Size(230, 36)
        Me.txtOriginalQty.TabIndex = 66
        '
        'EditBatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(536, 580)
        Me.Controls.Add(Me.txtOriginalQty)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtBatchNumber)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.SaveChanges)
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
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtItemName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "EditBatch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SaveChanges As Button
    Friend WithEvents Cancel As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents RoundedTextBox1 As RoundedTextBox
    Friend WithEvents Unit As ComboBox
    Friend WithEvents Quantity As RoundedTextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtItemName As RoundedTextBox
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
    Friend WithEvents txtBatchNumber As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtOriginalQty As TextBox
End Class