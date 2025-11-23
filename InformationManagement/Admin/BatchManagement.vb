Imports MySqlConnector

Public Class BatchManagement
    Private _ingredientID As Integer
    Private _ingredientName As String

    ' Constructor
    Public Sub New(ingredientID As Integer, ingredientName As String)
        InitializeComponent()
        _ingredientID = ingredientID
        _ingredientName = ingredientName
    End Sub

    Private Sub BatchManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = "Batch Management - " & _ingredientName

            If Me.Controls.Contains(lblIngredientName) Then
                lblIngredientName.Text = _ingredientName
            End If

            LoadBatchData()
        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message,
                          "Load Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        End Try
    End Sub

    ' Load all batches for this ingredient
    Private Sub LoadBatchData()
        Try
            openConn()

            Dim sql As String = "
                SELECT 
                    BatchID AS 'Batch ID',
                    BatchNumber AS 'Batch Number',
                    StockQuantity AS 'Current Stock',
                    OriginalQuantity AS 'Original Qty',
                    UnitType AS 'Unit',
                    CostPerUnit AS 'Cost/Unit',
                    (StockQuantity * CostPerUnit) AS 'Total Cost',
                    PurchaseDate AS 'Purchase Date',
                    ExpirationDate AS 'Expiration',
                    CASE 
                        WHEN ExpirationDate IS NULL THEN NULL
                        ELSE DATEDIFF(ExpirationDate, CURDATE())
                    END AS 'Days Left',
                    CASE 
                        WHEN BatchStatus = 'Expired' THEN 'EXPIRED'
                        WHEN BatchStatus = 'Depleted' THEN 'Depleted'
                        WHEN ExpirationDate IS NULL THEN 'No Expiry'
                        WHEN ExpirationDate <= CURDATE() THEN 'EXPIRED'
                        WHEN DATEDIFF(ExpirationDate, CURDATE()) <= 3 THEN 'CRITICAL'
                        WHEN DATEDIFF(ExpirationDate, CURDATE()) <= 7 THEN 'WARNING'
                        WHEN DATEDIFF(ExpirationDate, CURDATE()) <= 14 THEN 'Monitor'
                        ELSE 'Fresh'
                    END AS 'Alert',
                    BatchStatus AS 'Status',
                    MarketSource AS 'Source',
                    ROUND((StockQuantity / OriginalQuantity) * 100, 1) AS 'Remaining %',
                    Notes
                FROM inventory_batches
                WHERE IngredientID = @ingredientID
                ORDER BY 
                    CASE BatchStatus
                        WHEN 'Active' THEN 1
                        WHEN 'Expired' THEN 2
                        WHEN 'Depleted' THEN 3
                        ELSE 4
                    END,
                    CASE WHEN ExpirationDate IS NULL THEN 1 ELSE 0 END,
                    ExpirationDate ASC,
                    PurchaseDate ASC
            "

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@ingredientID", _ingredientID)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            ' Setup DataGridView
            dgvBatches.DataSource = Nothing
            dgvBatches.Columns.Clear()
            dgvBatches.DataSource = dt

            FormatBatchGrid()
            ColorCodeBatches()

            ' Load summary statistics
            LoadBatchStatistics()

        Catch ex As Exception
            MessageBox.Show("Error loading batches: " & ex.Message,
                          "Database Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    ' Format the batch grid
    Private Sub FormatBatchGrid()
        Try
            With dgvBatches
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .RowTemplate.Height = 35
                .DefaultCellStyle.Font = New Font("Segoe UI", 9)
                .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250)
                .ReadOnly = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect

                ' Hide Batch ID and Notes
                If .Columns.Contains("Batch ID") Then
                    .Columns("Batch ID").Visible = False
                End If

                If .Columns.Contains("Notes") Then
                    .Columns("Notes").Visible = False
                End If

                ' Format currency columns
                If .Columns.Contains("Cost/Unit") Then
                    .Columns("Cost/Unit").DefaultCellStyle.Format = "₱#,##0.00"
                    .Columns("Cost/Unit").ReadOnly = True
                End If

                If .Columns.Contains("Total Cost") Then
                    .Columns("Total Cost").DefaultCellStyle.Format = "₱#,##0.00"
                    .Columns("Total Cost").ReadOnly = True
                End If

                ' Format date columns
                If .Columns.Contains("Purchase Date") Then
                    .Columns("Purchase Date").DefaultCellStyle.Format = "MMM dd, yyyy"
                    .Columns("Purchase Date").ReadOnly = True
                End If

                If .Columns.Contains("Expiration") Then
                    .Columns("Expiration").DefaultCellStyle.Format = "MMM dd, yyyy"
                    .Columns("Expiration").ReadOnly = True
                End If

                ' Center alignment
                Dim centerColumns() As String = {"Current Stock", "Unit", "Days Left", "Alert", "Status", "Remaining %"}
                For Each colName In centerColumns
                    If .Columns.Contains(colName) Then
                        .Columns(colName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .Columns(colName).ReadOnly = True
                    End If
                Next

                ' Bold important columns
                If .Columns.Contains("Alert") Then
                    .Columns("Alert").DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                End If

                If .Columns.Contains("Status") Then
                    .Columns("Status").DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                End If

                ' Set column widths
                If .Columns.Contains("Batch Number") Then
                    .Columns("Batch Number").Width = 150
                End If

                If .Columns.Contains("Current Stock") Then
                    .Columns("Current Stock").Width = 100
                End If
            End With

            ' Add action button
            If Not dgvBatches.Columns.Contains("btnDiscard") Then
                Dim btnDiscard As New DataGridViewButtonColumn()
                btnDiscard.Name = "btnDiscard"
                btnDiscard.HeaderText = "Actions"
                btnDiscard.Text = "Discard"
                btnDiscard.UseColumnTextForButtonValue = True
                btnDiscard.Width = 100
                btnDiscard.FlatStyle = FlatStyle.Flat
                dgvBatches.Columns.Add(btnDiscard)
            End If

        Catch ex As Exception
            MessageBox.Show("Error formatting grid: " & ex.Message)
        End Try
    End Sub

    ' Color code batches based on status
    Private Sub ColorCodeBatches()
        Try
            For Each row As DataGridViewRow In dgvBatches.Rows
                If Not row.IsNewRow Then
                    ' Color code Alert column
                    If row.Cells("Alert").Value IsNot Nothing Then
                        Dim alert As String = row.Cells("Alert").Value.ToString()

                        Select Case alert
                            Case "EXPIRED"
                                row.Cells("Alert").Style.BackColor = Color.FromArgb(139, 0, 0)
                                row.Cells("Alert").Style.ForeColor = Color.White
                                If row.Cells("Expiration").Value IsNot Nothing Then
                                    row.Cells("Expiration").Style.BackColor = Color.FromArgb(139, 0, 0)
                                    row.Cells("Expiration").Style.ForeColor = Color.White
                                End If
                            Case "CRITICAL"
                                row.Cells("Alert").Style.BackColor = Color.FromArgb(220, 53, 69)
                                row.Cells("Alert").Style.ForeColor = Color.White
                                If row.Cells("Expiration").Value IsNot Nothing Then
                                    row.Cells("Expiration").Style.BackColor = Color.FromArgb(220, 53, 69)
                                    row.Cells("Expiration").Style.ForeColor = Color.White
                                End If
                            Case "WARNING"
                                row.Cells("Alert").Style.BackColor = Color.FromArgb(255, 193, 7)
                                row.Cells("Alert").Style.ForeColor = Color.Black
                                If row.Cells("Expiration").Value IsNot Nothing Then
                                    row.Cells("Expiration").Style.BackColor = Color.FromArgb(255, 193, 7)
                                End If
                            Case "Monitor"
                                row.Cells("Alert").Style.BackColor = Color.FromArgb(255, 235, 59)
                                row.Cells("Alert").Style.ForeColor = Color.Black
                            Case "Fresh"
                                row.Cells("Alert").Style.BackColor = Color.FromArgb(40, 167, 69)
                                row.Cells("Alert").Style.ForeColor = Color.White
                        End Select
                    End If

                    ' Color code Status column
                    If row.Cells("Status").Value IsNot Nothing Then
                        Dim status As String = row.Cells("Status").Value.ToString()

                        Select Case status
                            Case "Active"
                                row.Cells("Status").Style.BackColor = Color.FromArgb(40, 167, 69)
                                row.Cells("Status").Style.ForeColor = Color.White
                            Case "Depleted"
                                row.Cells("Status").Style.BackColor = Color.Gray
                                row.Cells("Status").Style.ForeColor = Color.White
                            Case "Expired"
                                row.Cells("Status").Style.BackColor = Color.FromArgb(220, 53, 69)
                                row.Cells("Status").Style.ForeColor = Color.White
                            Case "Discarded"
                                row.Cells("Status").Style.BackColor = Color.DarkGray
                                row.Cells("Status").Style.ForeColor = Color.White
                        End Select
                    End If

                    ' Color code Remaining %
                    If row.Cells("Remaining %").Value IsNot Nothing AndAlso
                       Not IsDBNull(row.Cells("Remaining %").Value) Then
                        Dim remaining As Decimal = Convert.ToDecimal(row.Cells("Remaining %").Value)

                        If remaining <= 20 Then
                            row.Cells("Remaining %").Style.BackColor = Color.FromArgb(220, 53, 69)
                            row.Cells("Remaining %").Style.ForeColor = Color.White
                        ElseIf remaining <= 50 Then
                            row.Cells("Remaining %").Style.BackColor = Color.FromArgb(255, 193, 7)
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error color coding: " & ex.Message)
        End Try
    End Sub

    ' Load summary statistics
    Private Sub LoadBatchStatistics()
        Try
            openConn()

            ' Total Stock
            Dim sqlTotal As String = "
                SELECT COALESCE(SUM(StockQuantity), 0)
                FROM inventory_batches
                WHERE IngredientID = @id AND BatchStatus = 'Active'
            "
            Dim cmdTotal As New MySqlCommand(sqlTotal, conn)
            cmdTotal.Parameters.AddWithValue("@id", _ingredientID)
            Dim totalStock As Decimal = Convert.ToDecimal(cmdTotal.ExecuteScalar())

            If Me.Controls.Contains(lblTotalStock) Then
                lblTotalStock.Text = totalStock.ToString("#,##0.00")
            End If

            ' Active Batches
            Dim sqlActive As String = "
                SELECT COUNT(*)
                FROM inventory_batches
                WHERE IngredientID = @id AND BatchStatus = 'Active'
            "
            Dim cmdActive As New MySqlCommand(sqlActive, conn)
            cmdActive.Parameters.AddWithValue("@id", _ingredientID)
            Dim activeBatches As Integer = Convert.ToInt32(cmdActive.ExecuteScalar())

            If Me.Controls.Contains(lblActiveBatches) Then
                lblActiveBatches.Text = activeBatches.ToString()
            End If

            ' Total Value
            Dim sqlValue As String = "
                SELECT COALESCE(SUM(StockQuantity * CostPerUnit), 0)
                FROM inventory_batches
                WHERE IngredientID = @id AND BatchStatus = 'Active'
            "
            Dim cmdValue As New MySqlCommand(sqlValue, conn)
            cmdValue.Parameters.AddWithValue("@id", _ingredientID)
            Dim totalValue As Decimal = Convert.ToDecimal(cmdValue.ExecuteScalar())

            If Me.Controls.Contains(lblTotalValue) Then
                lblTotalValue.Text = "₱" & totalValue.ToString("#,##0.00")
            End If

            ' Expiring Soon Count
            Dim sqlExpiring As String = "
                SELECT COUNT(*)
                FROM inventory_batches
                WHERE IngredientID = @id 
                  AND BatchStatus = 'Active'
                  AND ExpirationDate IS NOT NULL
                  AND DATEDIFF(ExpirationDate, CURDATE()) <= 7
            "
            Dim cmdExpiring As New MySqlCommand(sqlExpiring, conn)
            cmdExpiring.Parameters.AddWithValue("@id", _ingredientID)
            Dim expiringCount As Integer = Convert.ToInt32(cmdExpiring.ExecuteScalar())

            If Me.Controls.Contains(lblExpiringCount) Then
                lblExpiringCount.Text = expiringCount.ToString()

                If expiringCount > 0 Then
                    lblExpiringCount.ForeColor = Color.Red
                Else
                    lblExpiringCount.ForeColor = Color.Green
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading statistics: " & ex.Message)
        Finally
            closeConn()
        End Try
    End Sub

    ' Handle button clicks in grid
    Private Sub dgvBatches_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBatches.CellContentClick
        Try
            If e.RowIndex >= 0 AndAlso e.ColumnIndex = dgvBatches.Columns("btnDiscard").Index Then
                Dim batchID As Integer = Convert.ToInt32(dgvBatches.Rows(e.RowIndex).Cells("Batch ID").Value)
                Dim batchNumber As String = dgvBatches.Rows(e.RowIndex).Cells("Batch Number").Value.ToString()
                Dim currentStock As Decimal = Convert.ToDecimal(dgvBatches.Rows(e.RowIndex).Cells("Current Stock").Value)
                Dim batchStatus As String = dgvBatches.Rows(e.RowIndex).Cells("Status").Value.ToString()

                ' Check if already discarded or depleted
                If batchStatus = "Discarded" OrElse batchStatus = "Depleted" Then
                    MessageBox.Show("This batch is already " & batchStatus & " and cannot be discarded again.",
                                  "Cannot Discard",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning)
                    Return
                End If

                ' Confirm discard
                Dim result As DialogResult = MessageBox.Show(
                    "Are you sure you want to discard batch " & batchNumber & "?" & vbCrLf &
                    "Current stock: " & currentStock & vbCrLf & vbCrLf &
                    "This will mark the batch as discarded and remove it from active inventory.",
                    "Confirm Discard",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning)

                If result = DialogResult.Yes Then
                    DiscardBatch(batchID, batchNumber, currentStock)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error processing action: " & ex.Message,
                          "Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        End Try
    End Sub

    ' Discard a batch
    Private Sub DiscardBatch(batchID As Integer, batchNumber As String, currentStock As Decimal)
        Try
            openConn()

            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                ' Update batch status
                Dim sqlUpdate As String = "
                    UPDATE inventory_batches
                    SET BatchStatus = 'Discarded',
                        StockQuantity = 0
                    WHERE BatchID = @id
                "
                Dim cmdUpdate As New MySqlCommand(sqlUpdate, conn, transaction)
                cmdUpdate.Parameters.AddWithValue("@id", batchID)
                cmdUpdate.ExecuteNonQuery()

                ' Log transaction
                Dim sqlLog As String = "
                    INSERT INTO batch_transactions (
                        BatchID, TransactionType, QuantityChanged,
                        StockBefore, StockAfter, Reason, Notes
                    ) VALUES (
                        @batch, 'Discard', @qty,
                        @before, 0, 'Manual Discard', @notes
                    )
                "
                Dim cmdLog As New MySqlCommand(sqlLog, conn, transaction)
                cmdLog.Parameters.AddWithValue("@batch", batchID)
                cmdLog.Parameters.AddWithValue("@qty", -currentStock)
                cmdLog.Parameters.AddWithValue("@before", currentStock)
                cmdLog.Parameters.AddWithValue("@notes", "Batch " & batchNumber & " discarded by user on " & DateTime.Now.ToString())
                cmdLog.ExecuteNonQuery()

                transaction.Commit()

                MessageBox.Show("Batch discarded successfully!",
                              "Success",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information)

                ' Refresh data
                LoadBatchData()

            Catch ex As Exception
                transaction.Rollback()
                Throw
            End Try

        Catch ex As Exception
            MessageBox.Show("Error discarding batch: " & ex.Message,
                          "Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    ' Add new batch
    Private Sub btnAddBatch_Click(sender As Object, e As EventArgs) Handles btnAddBatch.Click
        Try
            Dim addForm As New AddNewItems()
            addForm.StartPosition = FormStartPosition.CenterScreen

            If addForm.ShowDialog() = DialogResult.OK Then
                LoadBatchData()
            End If
        Catch ex As Exception
            MessageBox.Show("Error opening add form: " & ex.Message)
        End Try
    End Sub

    ' View transaction history
    Private Sub btnViewHistory_Click(sender As Object, e As EventArgs) Handles btnViewHistory.Click
        Try
            openConn()

            Dim sql As String = "
                SELECT 
                    bt.TransactionDate AS 'Date/Time',
                    bt.TransactionType AS 'Type',
                    ib.BatchNumber AS 'Batch #',
                    bt.QuantityChanged AS 'Qty Change',
                    bt.StockBefore AS 'Before',
                    bt.StockAfter AS 'After',
                    bt.ReferenceID AS 'Reference',
                    bt.PerformedBy AS 'Performed By',
                    bt.Notes
                FROM batch_transactions bt
                JOIN inventory_batches ib ON bt.BatchID = ib.BatchID
                WHERE ib.IngredientID = @id
                ORDER BY bt.TransactionDate DESC
                LIMIT 100
            "

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", _ingredientID)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                ' Create a simple form to show the history
                Dim historyForm As New Form()
                historyForm.Text = "Transaction History - " & _ingredientName
                historyForm.Size = New Size(1000, 600)
                historyForm.StartPosition = FormStartPosition.CenterParent

                Dim dgv As New DataGridView()
                dgv.Dock = DockStyle.Fill
                dgv.DataSource = dt
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                dgv.ReadOnly = True
                dgv.AllowUserToAddRows = False
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect

                historyForm.Controls.Add(dgv)
                historyForm.ShowDialog()
            Else
                MessageBox.Show("No transaction history found.",
                              "History",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading history: " & ex.Message,
                          "Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    ' Close button
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class