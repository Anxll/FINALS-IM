<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReservationPayment
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
        Me.Reservation = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.Reservation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Reservation
        '
        Me.Reservation.AllowUserToAddRows = False
        Me.Reservation.AllowUserToDeleteRows = False
        Me.Reservation.AllowUserToResizeColumns = False
        Me.Reservation.AllowUserToResizeRows = False
        Me.Reservation.BackgroundColor = System.Drawing.Color.White
        Me.Reservation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Reservation.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.Reservation.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Me.Reservation.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Reservation.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Reservation.ColumnHeadersHeight = 40
        Me.Reservation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.Reservation.EnableHeadersVisualStyles = False
        Me.Reservation.Location = New System.Drawing.Point(19, 75)
        Me.Reservation.Margin = New System.Windows.Forms.Padding(4)
        Me.Reservation.Name = "Reservation"
        Me.Reservation.RowHeadersVisible = False
        Me.Reservation.RowHeadersWidth = 51
        Me.Reservation.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.Reservation.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.Reservation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.Reservation.Size = New System.Drawing.Size(1296, 241)
        Me.Reservation.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(254, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 23)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(293, 31)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Reservation Payment"
        '
        'ReservationPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1436, 692)
        Me.Controls.Add(Me.Reservation)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ReservationPayment"
        Me.Text = "ReservationPayment"
        CType(Me.Reservation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Reservation As DataGridView
    Friend WithEvents Label1 As Label
End Class
