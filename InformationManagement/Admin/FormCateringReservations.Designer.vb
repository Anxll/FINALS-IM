<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCateringReservations
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCateringReservations))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Export = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Period = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Reservations = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalGuests = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RoundedPane21 = New InformationManagement.RoundedPane2()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.RoundedPane22 = New InformationManagement.RoundedPane2()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.RoundedPane23 = New InformationManagement.RoundedPane2()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.RoundedPane24 = New InformationManagement.RoundedPane2()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RoundedPane21.SuspendLayout()
        Me.RoundedPane22.SuspendLayout()
        Me.RoundedPane23.SuspendLayout()
        Me.RoundedPane24.SuspendLayout()
        Me.SuspendLayout()
        '
        'Export
        '
        Me.Export.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Export.Image = CType(resources.GetObject("Export.Image"), System.Drawing.Image)
        Me.Export.Location = New System.Drawing.Point(1215, 15)
        Me.Export.Margin = New System.Windows.Forms.Padding(4)
        Me.Export.Name = "Export"
        Me.Export.Size = New System.Drawing.Size(139, 37)
        Me.Export.TabIndex = 7
        Me.Export.Text = "   Export"
        Me.Export.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Export.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Export.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeight = 40
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Period, Me.Reservations, Me.TotalGuests, Me.TotalAmount})
        Me.DataGridView1.Location = New System.Drawing.Point(29, 265)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 40
        Me.DataGridView1.Size = New System.Drawing.Size(1341, 185)
        Me.DataGridView1.TabIndex = 6
        '
        'Period
        '
        Me.Period.HeaderText = "Period"
        Me.Period.MinimumWidth = 6
        Me.Period.Name = "Period"
        Me.Period.Width = 200
        '
        'Reservations
        '
        Me.Reservations.HeaderText = "Reservations"
        Me.Reservations.MinimumWidth = 6
        Me.Reservations.Name = "Reservations"
        Me.Reservations.Width = 200
        '
        'TotalGuests
        '
        Me.TotalGuests.HeaderText = "Total Guests"
        Me.TotalGuests.MinimumWidth = 6
        Me.TotalGuests.Name = "TotalGuests"
        Me.TotalGuests.Width = 300
        '
        'TotalAmount
        '
        Me.TotalAmount.HeaderText = "Total Amount"
        Me.TotalAmount.MinimumWidth = 6
        Me.TotalAmount.Name = "TotalAmount"
        Me.TotalAmount.Width = 300
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 38)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Total Reservations"
        '
        'ComboBox1
        '
        Me.ComboBox1.AutoCompleteCustomSource.AddRange(New String() {"Daily", "Weekly", "Monthly"})
        Me.ComboBox1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.ComboBox1.DisplayMember = "Daily"
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(1011, 23)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(160, 24)
        Me.ComboBox1.Sorted = True
        Me.ComboBox1.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(25, 30)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(293, 25)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Catering Reservations Breakdown"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(29, 38)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(168, 23)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Average Event Value"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 38)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Total Events"
        '
        'RoundedPane21
        '
        Me.RoundedPane21.BackColor = System.Drawing.Color.Transparent
        Me.RoundedPane21.BorderColor = System.Drawing.Color.LightGray
        Me.RoundedPane21.BorderThickness = 1
        Me.RoundedPane21.Controls.Add(Me.Label5)
        Me.RoundedPane21.Controls.Add(Me.Label1)
        Me.RoundedPane21.CornerRadius = 15
        Me.RoundedPane21.FillColor = System.Drawing.Color.White
        Me.RoundedPane21.Location = New System.Drawing.Point(45, 73)
        Me.RoundedPane21.Margin = New System.Windows.Forms.Padding(4)
        Me.RoundedPane21.Name = "RoundedPane21"
        Me.RoundedPane21.Size = New System.Drawing.Size(411, 170)
        Me.RoundedPane21.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(24, 102)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(199, 37)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "₱8,200,000.00"
        '
        'RoundedPane22
        '
        Me.RoundedPane22.BackColor = System.Drawing.Color.Transparent
        Me.RoundedPane22.BorderColor = System.Drawing.Color.LightGray
        Me.RoundedPane22.BorderThickness = 1
        Me.RoundedPane22.Controls.Add(Me.Label6)
        Me.RoundedPane22.Controls.Add(Me.Label2)
        Me.RoundedPane22.CornerRadius = 15
        Me.RoundedPane22.FillColor = System.Drawing.Color.White
        Me.RoundedPane22.Location = New System.Drawing.Point(496, 73)
        Me.RoundedPane22.Margin = New System.Windows.Forms.Padding(4)
        Me.RoundedPane22.Name = "RoundedPane22"
        Me.RoundedPane22.Size = New System.Drawing.Size(411, 170)
        Me.RoundedPane22.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(37, 102)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 37)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "68"
        '
        'RoundedPane23
        '
        Me.RoundedPane23.BackColor = System.Drawing.Color.Transparent
        Me.RoundedPane23.BorderColor = System.Drawing.Color.LightGray
        Me.RoundedPane23.BorderThickness = 1
        Me.RoundedPane23.Controls.Add(Me.Label7)
        Me.RoundedPane23.Controls.Add(Me.Label3)
        Me.RoundedPane23.CornerRadius = 15
        Me.RoundedPane23.FillColor = System.Drawing.Color.White
        Me.RoundedPane23.Location = New System.Drawing.Point(943, 73)
        Me.RoundedPane23.Margin = New System.Windows.Forms.Padding(4)
        Me.RoundedPane23.Name = "RoundedPane23"
        Me.RoundedPane23.Size = New System.Drawing.Size(411, 170)
        Me.RoundedPane23.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(27, 102)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(176, 37)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "₱120,588.00"
        '
        'RoundedPane24
        '
        Me.RoundedPane24.BorderColor = System.Drawing.Color.LightGray
        Me.RoundedPane24.BorderThickness = 1
        Me.RoundedPane24.Controls.Add(Me.DataGridView1)
        Me.RoundedPane24.Controls.Add(Me.Export)
        Me.RoundedPane24.Controls.Add(Me.RoundedPane23)
        Me.RoundedPane24.Controls.Add(Me.Label4)
        Me.RoundedPane24.Controls.Add(Me.ComboBox1)
        Me.RoundedPane24.Controls.Add(Me.RoundedPane22)
        Me.RoundedPane24.Controls.Add(Me.RoundedPane21)
        Me.RoundedPane24.CornerRadius = 15
        Me.RoundedPane24.FillColor = System.Drawing.Color.White
        Me.RoundedPane24.Location = New System.Drawing.Point(44, 15)
        Me.RoundedPane24.Margin = New System.Windows.Forms.Padding(4)
        Me.RoundedPane24.Name = "RoundedPane24"
        Me.RoundedPane24.Size = New System.Drawing.Size(1393, 548)
        Me.RoundedPane24.TabIndex = 4
        '
        'FormCateringReservations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.GhostWhite
        Me.ClientSize = New System.Drawing.Size(1564, 721)
        Me.Controls.Add(Me.RoundedPane24)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormCateringReservations"
        Me.Text = "FormCateringReservations"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RoundedPane21.ResumeLayout(False)
        Me.RoundedPane21.PerformLayout()
        Me.RoundedPane22.ResumeLayout(False)
        Me.RoundedPane22.PerformLayout()
        Me.RoundedPane23.ResumeLayout(False)
        Me.RoundedPane23.PerformLayout()
        Me.RoundedPane24.ResumeLayout(False)
        Me.RoundedPane24.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Period As DataGridViewTextBoxColumn
    Friend WithEvents Reservations As DataGridViewTextBoxColumn
    Friend WithEvents TotalGuests As DataGridViewTextBoxColumn
    Friend WithEvents TotalAmount As DataGridViewTextBoxColumn
    Friend WithEvents Export As Button
    Friend WithEvents RoundedPane21 As RoundedPane2
    Friend WithEvents RoundedPane22 As RoundedPane2
    Friend WithEvents RoundedPane23 As RoundedPane2
    Friend WithEvents RoundedPane24 As RoundedPane2
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class
