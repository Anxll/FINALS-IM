Imports MySqlConnector

Public Class EditBatch

    Private _batchID As Integer
    Private _ingredientName As String
    Private _ingredientID As Integer

    Public Sub New(batchID As Integer, ingredientName As String)
        InitializeComponent()
        _batchID = batchID
        _ingredientName = ingredientName
    End Sub

    Private Sub EditBatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureFormLayout()
        LoadBatchData()
    End Sub

    ' Configure form layout and styling
    Private Sub ConfigureFormLayout()
        Try
            Me.BackColor = Color.White
            Me.Font = New Font("Segoe UI", 10)

            ' Header styling
            Label1.Font = New Font("Segoe UI", 18, FontStyle.Bold)
            Label1.ForeColor = Color.FromArgb(26, 38, 50)

            Label2.Font = New Font("Segoe UI", 11, FontStyle.Regular)
            Label2.ForeColor = Color.FromArgb(108, 117, 125)

            ' Label styling
            For Each ctrl In Me.Controls.OfType(Of Label)()
                If ctrl.Name.StartsWith("Label") AndAlso ctrl.Name <> "Label1" AndAlso ctrl.Name <> "Label2" Then
                    ctrl.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                    ctrl.ForeColor = Color.FromArgb(26, 38, 50)
                End If
            Next

            ' Button styling
            SaveChanges.BackColor = Color.FromArgb(40, 167, 69)
            SaveChanges.ForeColor = Color.White
            SaveChanges.Font = New Font("Segoe UI", 11, FontStyle.Bold)
            SaveChanges.FlatStyle = FlatStyle.Flat
            SaveChanges.FlatAppearance.BorderSize = 0
            SaveChanges.Cursor = Cursors.Hand

            Cancel.BackColor = Color.FromArgb(108, 117, 125)
            Cancel.ForeColor = Color.White
            Cancel.Font = New Font("Segoe UI", 11, FontStyle.Bold)
            Cancel.FlatStyle = FlatStyle.Flat
            Cancel.FlatAppearance.BorderSize = 0
            Cancel.Cursor = Cursors.Hand

            ' Make identifying fields read-only
            txtItemName.ReadOnly = True
            txtBatchNumber.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("Error configuring form: " & ex.Message)
        End Try
    End Sub

    ' Load existing batch data
    Private Sub LoadBatchData()
        Try
            openConn()

            Dim sql As String = "
                SELECT 
                    ib.IngredientID,
                    i.IngredientName,
                    ib.BatchNumber,
                    ib.StockQuantity,
                    ib.OriginalQuantity,
                    ib.UnitType,
                    ib.CostPerUnit,
                    ib.PurchaseDate,
                    ib.ExpirationDate,
                    i.MinStockLevel,
                    i.MaxStockLevel
                FROM inventory_batches ib
                JOIN ingredients i ON ib.IngredientID = i.IngredientID
                WHERE ib.BatchID = @batchID
            "

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@batchID", _batchID)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                _ingredientID = reader.GetInt32("IngredientID")

                ' Fill in the form
                txtItemName.Text = reader.GetString("IngredientName")
                txtBatchNumber.Text = reader.GetString("BatchNumber")
                Quantity.Text = reader.GetDecimal("StockQuantity").ToString()
                txtOriginalQty.Text = reader.GetDecimal("OriginalQuantity").ToString()
                Unit.Text = reader.GetString("UnitType")
                RoundedTextBox1.Text = reader.GetDecimal("CostPerUnit").ToString()
                DateTimePicker1.Value = reader.GetDateTime("PurchaseDate")

                If Not reader.IsDBNull(reader.GetOrdinal("ExpirationDate")) Then
                    DateTimePicker2.Value = reader.GetDateTime("ExpirationDate")
                Else
                    DateTimePicker2.Value = Date.Now.AddDays(30)
                End If

                NumericUpDown1.Value = reader.GetDecimal("MinStockLevel")
                NumericUpDown2.Value = reader.GetDecimal("MaxStockLevel")
            End If

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading batch data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    ' Save Changes Button Click
    Private Sub SaveChanges_Click(sender As Object, e As EventArgs) Handles SaveChanges.Click
        If ValidateInputs() Then
            UpdateBatch()
        End If
    End Sub

    ' Validate all inputs
    Private Function ValidateInputs() As Boolean
        ' Quantity
        If String.IsNullOrWhiteSpace(Quantity.Text) OrElse Not IsNumeric(Quantity.Text) Then
            MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Quantity.Focus()
            Return False
        End If

        If Convert.ToDecimal(Quantity.Text) < 0 Then
            MessageBox.Show("Quantity cannot be negative.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Quantity.Focus()
            Return False
        End If

        ' Check if current stock is greater than original quantity
        Dim originalQty As Decimal = Convert.ToDecimal(txtOriginalQty.Text)
        Dim currentQty As Decimal = Convert.ToDecimal(Quantity.Text)

        If currentQty > originalQty Then
            MessageBox.Show("Current stock quantity cannot exceed original quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Quantity.Focus()
            Return False
        End If

        ' Unit
        If Unit.SelectedIndex < 0 Then
            MessageBox.Show("Please select a unit type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Unit.Focus()
            Return False
        End If

        ' Cost per Unit
        If String.IsNullOrWhiteSpace(RoundedTextBox1.Text) OrElse Not IsNumeric(RoundedTextBox1.Text) Then
            MessageBox.Show("Please enter a valid cost per unit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            RoundedTextBox1.Focus()
            Return False
        End If

        If Convert.ToDecimal(RoundedTextBox1.Text) < 0 Then
            MessageBox.Show("Cost per unit cannot be negative.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            RoundedTextBox1.Focus()
            Return False
        End If

        ' Stock Levels
        If NumericUpDown1.Value <= 0 Then
            MessageBox.Show("Minimum stock level must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NumericUpDown1.Focus()
            Return False
        End If

        If NumericUpDown2.Value <= NumericUpDown1.Value Then
            MessageBox.Show("Maximum stock level must be greater than minimum stock level.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NumericUpDown2.Focus()
            Return False
        End If

        ' Expiration Date
        If DateTimePicker2.Value < Date.Now.Date Then
            Dim result As DialogResult = MessageBox.Show(
                "The expiration date is in the past. Are you sure you want to continue?",
                "Expired Item Warning",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If result = DialogResult.No Then
                DateTimePicker2.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    ' Update batch in database
    Private Sub UpdateBatch()
    Try
        openConn()

        Dim transaction As MySqlTransaction = conn.BeginTransaction()

        Try
            ' STEP 1: Get current batch data BEFORE making any changes
            Dim sqlGetCurrent As String = "
            SELECT 
                ib.StockQuantity,
                ib.BatchNumber,
                ib.UnitType,
                i.IngredientName
            FROM inventory_batches ib
            JOIN ingredients i ON ib.IngredientID = i.IngredientID
            WHERE ib.BatchID = @id
        "

            Dim cmdGetCurrent As New MySqlCommand(sqlGetCurrent, conn, transaction)
            cmdGetCurrent.Parameters.AddWithValue("@id", _batchID)

            Dim reader As MySqlDataReader = cmdGetCurrent.ExecuteReader()

            Dim oldQuantity As Decimal = 0
            Dim batchNumber As String = ""
            Dim oldUnit As String = ""
            Dim ingredientName As String = ""

            If reader.Read() Then
                oldQuantity = reader.GetDecimal("StockQuantity")
                batchNumber = reader.GetString("BatchNumber")
                oldUnit = reader.GetString("UnitType")
                ingredientName = reader.GetString("IngredientName")
            End If

            reader.Close()

            ' New values
            Dim newQuantity As Decimal = Convert.ToDecimal(Quantity.Text)
            Dim newUnit As String = Unit.Text
            Dim quantityChanged As Decimal = newQuantity - oldQuantity

            ' Update batch
            Dim sqlUpdateBatch As String = "
            UPDATE inventory_batches
            SET StockQuantity = @quantity,
                UnitType = @unit,
                CostPerUnit = @cost,
                PurchaseDate = @purchaseDate,
                ExpirationDate = @expirationDate,
                UpdatedDate = NOW()
            WHERE BatchID = @id
        "

            Dim cmdUpdate As New MySqlCommand(sqlUpdateBatch, conn, transaction)
            cmdUpdate.Parameters.AddWithValue("@id", _batchID)
            cmdUpdate.Parameters.AddWithValue("@quantity", newQuantity)
            cmdUpdate.Parameters.AddWithValue("@unit", newUnit)
            cmdUpdate.Parameters.AddWithValue("@cost", Convert.ToDecimal(RoundedTextBox1.Text))
            cmdUpdate.Parameters.AddWithValue("@purchaseDate", DateTimePicker1.Value.Date)
            cmdUpdate.Parameters.AddWithValue("@expirationDate", DateTimePicker2.Value.Date)
            cmdUpdate.ExecuteNonQuery()

            ' Update ingredient stock
            Dim sqlUpdateIngredient As String = "
            UPDATE ingredients
            SET MinStockLevel = @minStock,
                MaxStockLevel = @maxStock,
                UnitType = @unit,
                StockQuantity = (
                    SELECT COALESCE(SUM(StockQuantity), 0)
                    FROM inventory_batches
                    WHERE IngredientID = @id
                    AND BatchStatus = 'Active'
                )
            WHERE IngredientID = @id
        "

            Dim cmdUpdateIng As New MySqlCommand(sqlUpdateIngredient, conn, transaction)
            cmdUpdateIng.Parameters.AddWithValue("@id", _ingredientID)
            cmdUpdateIng.Parameters.AddWithValue("@minStock", NumericUpDown1.Value)
            cmdUpdateIng.Parameters.AddWithValue("@maxStock", NumericUpDown2.Value)
            cmdUpdateIng.Parameters.AddWithValue("@unit", newUnit)
            cmdUpdateIng.ExecuteNonQuery()

            ' Log in batch_transactions (existing system)
            If oldQuantity <> newQuantity Then
                Dim sqlLog As String = "
                INSERT INTO batch_transactions (
                    BatchID, TransactionType, QuantityChanged,
                    StockBefore, StockAfter, Reason, Notes, TransactionDate
                ) VALUES (
                    @batch, 'Adjustment', @qtyChange,
                    @before, @after, 'Manual Edit', @notes, NOW()
                )
            "
                Dim cmdLog As New MySqlCommand(sqlLog, conn, transaction)
                cmdLog.Parameters.AddWithValue("@batch", _batchID)
                cmdLog.Parameters.AddWithValue("@qtyChange", quantityChanged)
                cmdLog.Parameters.AddWithValue("@before", oldQuantity)
                cmdLog.Parameters.AddWithValue("@after", newQuantity)
                cmdLog.Parameters.AddWithValue("@notes", "Batch edited by user")
                cmdLog.ExecuteNonQuery()
            End If


            ' ===========================================
            ' ⭐ NEW: COMPUTE GLOBAL STOCK BEFORE & AFTER
            ' ===========================================
            Dim globalBefore As Decimal = 0

            Dim sqlGetGlobal As String = "
                SELECT StockAfter
                FROM inventory_movement_log
                WHERE IngredientID = @iid
                ORDER BY MovementID DESC
                LIMIT 1
            "

            Dim cmdGetGlobal As New MySqlCommand(sqlGetGlobal, conn, transaction)
            cmdGetGlobal.Parameters.AddWithValue("@iid", _ingredientID)

            Dim result As Object = cmdGetGlobal.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                globalBefore = Convert.ToDecimal(result)
            End If

            Dim globalAfter As Decimal = globalBefore + quantityChanged



            ' ===========================================
            ' NEW: Log in inventory_movement_log (CORRECTED)
            ' ===========================================
            If oldQuantity <> newQuantity Then
                Dim changeType As String = "ADJUST"

                Dim sqlMovementLog As String = "
                INSERT INTO inventory_movement_log (
                    IngredientID,
                    BatchID,
                    ChangeType,
                    QuantityChanged,
                    StockBefore,
                    StockAfter,
                    UnitType,
                    Reason,
                    Source,
                    SourceID,
                    SourceName,
                    OrderID,
                    ReservationID,
                    ReferenceNumber,
                    Notes,
                    MovementDate
                ) VALUES (
                    @ingredientID,
                    @batchID,
                    @changeType,
                    @quantityChanged,
                    @stockBefore,
                    @stockAfter,
                    @unitType,
                    @reason,
                    'ADMIN',
                    NULL,
                    'Admin User',
                    NULL,
                    NULL,
                    @referenceNumber,
                    @notes,
                    NOW()
                )
            "

                Dim cmdMovementLog As New MySqlCommand(sqlMovementLog, conn, transaction)
                cmdMovementLog.Parameters.AddWithValue("@ingredientID", _ingredientID)
                cmdMovementLog.Parameters.AddWithValue("@batchID", _batchID)
                cmdMovementLog.Parameters.AddWithValue("@changeType", changeType)
                cmdMovementLog.Parameters.AddWithValue("@quantityChanged", quantityChanged)

                ' ⭐ FIX: Replace batch stock with GLOBAL stock
                cmdMovementLog.Parameters.AddWithValue("@stockBefore", globalBefore)
                cmdMovementLog.Parameters.AddWithValue("@stockAfter", globalAfter)

                cmdMovementLog.Parameters.AddWithValue("@unitType", newUnit)
                cmdMovementLog.Parameters.AddWithValue("@reason", "Batch Edited")
                cmdMovementLog.Parameters.AddWithValue("@referenceNumber", batchNumber)

                Dim notesText As String =
                    $"Batch Edit: {ingredientName} | Batch: {batchNumber} | Previous: {oldQuantity} {oldUnit} | New: {newQuantity} {newUnit} | Change: {quantityChanged} {newUnit}"

                cmdMovementLog.Parameters.AddWithValue("@notes", notesText)
                cmdMovementLog.ExecuteNonQuery()
            End If

            transaction.Commit()

            MessageBox.Show(
            "Batch updated successfully!" & vbCrLf & vbCrLf &
            "Batch #: " & batchNumber & vbCrLf &
            "Previous Quantity: " & oldQuantity.ToString("N2") & " " & oldUnit & vbCrLf &
            "New Quantity: " & newQuantity.ToString("N2") & " " & newUnit & vbCrLf &
            "Change: " & quantityChanged.ToString("N2") & " " & newUnit,
            "Success",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            transaction.Rollback()
            Throw
        End Try

    Catch ex As Exception
        MessageBox.Show("Error updating batch: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Finally
        closeConn()
    End Try
End Sub

    ' Cancel Button
    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' Auto-calculate total cost when quantity or cost per unit changes
    Private Sub Quantity_TextChanged(sender As Object, e As EventArgs) Handles Quantity.TextChanged
        UpdateTotalCost()
    End Sub

    Private Sub RoundedTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RoundedTextBox1.TextChanged
        UpdateTotalCost()
    End Sub

    Private Sub UpdateTotalCost()
        Try
            If IsNumeric(Quantity.Text) AndAlso IsNumeric(RoundedTextBox1.Text) Then
                Dim qty As Decimal = Convert.ToDecimal(Quantity.Text)
                Dim cost As Decimal = Convert.ToDecimal(RoundedTextBox1.Text)
                Dim total As Decimal = qty * cost
                ' Display in form title or add a label if needed
                Me.Text = "Edit Batch - Total: ₱" & total.ToString("#,##0.00")
            End If
        Catch ex As Exception
            ' Ignore calculation errors during typing
        End Try
    End Sub
    Private Sub ComboBox_DrawItem(sender As Object, e As DrawItemEventArgs) _
      Handles Unit.DrawItem

        If e.Index < 0 Then Return
        Dim cmb As ComboBox = DirectCast(sender, ComboBox)
        e.DrawBackground()
        e.Graphics.DrawString(cmb.Items(e.Index).ToString(), cmb.Font, Brushes.Black, e.Bounds)
        e.DrawFocusRectangle()
    End Sub
End Class