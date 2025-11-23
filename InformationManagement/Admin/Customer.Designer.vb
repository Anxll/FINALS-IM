<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Customer
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
    'It can be modified using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.CustomerID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Email = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContactNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomerType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FeedbackCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalOrdersCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReservationCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastTransactionDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastLoginDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreatedDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AccountStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SatisfactionRating = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DeleteBtn = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.SuspendBtn = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(14, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(296, 20)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Track and manage customer accounts"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(175, 36)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Customers"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1453, 105)
        Me.Splitter1.TabIndex = 36
        Me.Splitter1.TabStop = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToResizeColumns = True
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeight = 40
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CustomerID, Me.FirstName, Me.LastName, Me.Email, Me.ContactNumber, Me.CustomerType, Me.FeedbackCount, Me.TotalOrdersCount, Me.ReservationCount, Me.LastTransactionDate, Me.LastLoginDate, Me.CreatedDate, Me.AccountStatus, Me.SatisfactionRating, Me.DeleteBtn, Me.SuspendBtn})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.EnableHeadersVisualStyles = False
        Me.DataGridView1.Location = New System.Drawing.Point(0, 105)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.TabIndex = 42
        '
        'CustomerID
        '
        Me.CustomerID.DataPropertyName = "CustomerID"
        Me.CustomerID.Frozen = True
        Me.CustomerID.HeaderText = "ID"
        Me.CustomerID.MinimumWidth = 50
        Me.CustomerID.Name = "CustomerID"
        Me.CustomerID.ReadOnly = True
        Me.CustomerID.Width = 50
        '
        'FirstName
        '
        Me.FirstName.DataPropertyName = "FirstName"
        Me.FirstName.HeaderText = "First Name"
        Me.FirstName.MinimumWidth = 100
        Me.FirstName.Name = "FirstName"
        Me.FirstName.ReadOnly = True
        '
        'LastName
        '
        Me.LastName.DataPropertyName = "LastName"
        Me.LastName.HeaderText = "Last Name"
        Me.LastName.MinimumWidth = 100
        Me.LastName.Name = "LastName"
        Me.LastName.ReadOnly = True
        '
        'Email
        '
        Me.Email.DataPropertyName = "Email"
        Me.Email.HeaderText = "Email"
        Me.Email.MinimumWidth = 150
        Me.Email.Name = "Email"
        Me.Email.ReadOnly = True
        '
        'ContactNumber
        '
        Me.ContactNumber.DataPropertyName = "ContactNumber"
        Me.ContactNumber.HeaderText = "Contact"
        Me.ContactNumber.MinimumWidth = 110
        Me.ContactNumber.Name = "ContactNumber"
        Me.ContactNumber.ReadOnly = True
        '
        'CustomerType
        '
        Me.CustomerType.DataPropertyName = "CustomerType"
        Me.CustomerType.HeaderText = "Type"
        Me.CustomerType.MinimumWidth = 90
        Me.CustomerType.Name = "CustomerType"
        Me.CustomerType.ReadOnly = True
        '
        'FeedbackCount
        '
        Me.FeedbackCount.DataPropertyName = "FeedbackCount"
        Me.FeedbackCount.HeaderText = "Feedback"
        Me.FeedbackCount.MinimumWidth = 80
        Me.FeedbackCount.Name = "FeedbackCount"
        Me.FeedbackCount.ReadOnly = True
        '
        'TotalOrdersCount
        '
        Me.TotalOrdersCount.DataPropertyName = "TotalOrdersCount"
        Me.TotalOrdersCount.HeaderText = "Orders"
        Me.TotalOrdersCount.MinimumWidth = 70
        Me.TotalOrdersCount.Name = "TotalOrdersCount"
        Me.TotalOrdersCount.ReadOnly = True
        '
        'ReservationCount
        '
        Me.ReservationCount.DataPropertyName = "ReservationCount"
        Me.ReservationCount.HeaderText = "Reservations"
        Me.ReservationCount.MinimumWidth = 100
        Me.ReservationCount.Name = "ReservationCount"
        Me.ReservationCount.ReadOnly = True
        '
        'LastTransactionDate
        '
        Me.LastTransactionDate.DataPropertyName = "LastTransactionDate"
        Me.LastTransactionDate.HeaderText = "Last Transaction"
        Me.LastTransactionDate.MinimumWidth = 130
        Me.LastTransactionDate.Name = "LastTransactionDate"
        Me.LastTransactionDate.ReadOnly = True
        '
        'LastLoginDate
        '
        Me.LastLoginDate.DataPropertyName = "LastLoginDate"
        Me.LastLoginDate.HeaderText = "Last Login"
        Me.LastLoginDate.MinimumWidth = 130
        Me.LastLoginDate.Name = "LastLoginDate"
        Me.LastLoginDate.ReadOnly = True
        '
        'CreatedDate
        '
        Me.CreatedDate.DataPropertyName = "CreatedDate"
        Me.CreatedDate.HeaderText = "Created"
        Me.CreatedDate.MinimumWidth = 130
        Me.CreatedDate.Name = "CreatedDate"
        Me.CreatedDate.ReadOnly = True
        '
        'AccountStatus
        '
        Me.AccountStatus.DataPropertyName = "AccountStatus"
        Me.AccountStatus.HeaderText = "Status"
        Me.AccountStatus.MinimumWidth = 90
        Me.AccountStatus.Name = "AccountStatus"
        Me.AccountStatus.ReadOnly = True
        '
        'SatisfactionRating
        '
        Me.SatisfactionRating.DataPropertyName = "SatisfactionRating"
        Me.SatisfactionRating.HeaderText = "Rating"
        Me.SatisfactionRating.MinimumWidth = 70
        Me.SatisfactionRating.Name = "SatisfactionRating"
        Me.SatisfactionRating.ReadOnly = True
        '
        'DeleteBtn
        '
        Me.DeleteBtn.HeaderText = "Delete"
        Me.DeleteBtn.MinimumWidth = 70
        Me.DeleteBtn.Name = "DeleteBtn"
        Me.DeleteBtn.ReadOnly = True
        Me.DeleteBtn.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DeleteBtn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DeleteBtn.Text = "Delete"
        Me.DeleteBtn.UseColumnTextForButtonValue = True
        Me.DeleteBtn.Width = 70
        '
        'SuspendBtn
        '
        Me.SuspendBtn.HeaderText = "Suspend"
        Me.SuspendBtn.MinimumWidth = 80
        Me.SuspendBtn.Name = "SuspendBtn"
        Me.SuspendBtn.ReadOnly = True
        Me.SuspendBtn.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.SuspendBtn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.SuspendBtn.Text = "Suspend"
        Me.SuspendBtn.UseColumnTextForButtonValue = True
        Me.SuspendBtn.Width = 80
        '
        'Customer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1453, 864)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Splitter1)
        Me.Name = "Customer"
        Me.Text = "Customer Management"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents CustomerID As DataGridViewTextBoxColumn
    Friend WithEvents FirstName As DataGridViewTextBoxColumn
    Friend WithEvents LastName As DataGridViewTextBoxColumn
    Friend WithEvents Email As DataGridViewTextBoxColumn
    Friend WithEvents ContactNumber As DataGridViewTextBoxColumn
    Friend WithEvents CustomerType As DataGridViewTextBoxColumn
    Friend WithEvents FeedbackCount As DataGridViewTextBoxColumn
    Friend WithEvents TotalOrdersCount As DataGridViewTextBoxColumn
    Friend WithEvents ReservationCount As DataGridViewTextBoxColumn
    Friend WithEvents LastTransactionDate As DataGridViewTextBoxColumn
    Friend WithEvents LastLoginDate As DataGridViewTextBoxColumn
    Friend WithEvents CreatedDate As DataGridViewTextBoxColumn
    Friend WithEvents AccountStatus As DataGridViewTextBoxColumn
    Friend WithEvents SatisfactionRating As DataGridViewTextBoxColumn
    Friend WithEvents DeleteBtn As DataGridViewButtonColumn
    Friend WithEvents SuspendBtn As DataGridViewButtonColumn
End Class