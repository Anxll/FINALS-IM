Imports MySqlConnector

Public Class AddNewItems
    Private ReadOnly _isEditMode As Boolean
    Private ReadOnly _ingredientId As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ingredientId As Integer)
        Me.New()
        _isEditMode = True
        _ingredientId = ingredientId
    End Sub

    Private Sub AddNewItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCategories()
        SetDefaultValues()

        If _isEditMode Then
            ConfigureEditModeUI()
            LoadIngredientDetailsForEdit()
        End If

        ConfigureFormLayout()
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
            AddItem.BackColor = Color.FromArgb(40, 167, 69)
            AddItem.ForeColor = Color.White
            AddItem.Font = New Font("Segoe UI", 11, FontStyle.Bold)
            AddItem.FlatStyle = FlatStyle.Flat
            AddItem.FlatAppearance.BorderSize = 0
            AddItem.Cursor = Cursors.Hand

            Cancel.BackColor = Color.FromArgb(108, 117, 125)
            Cancel.ForeColor = Color.White
            Cancel.Font = New Font("Segoe UI", 11, FontStyle.Bold)
            Cancel.FlatStyle = FlatStyle.Flat
            Cancel.FlatAppearance.BorderSize = 0
            Cancel.Cursor = Cursors.Hand

        Catch ex As Exception
            MessageBox.Show("Error configuring form: " & ex.Message)
        End Try
    End Sub

    Private Sub ConfigureEditModeUI()
        Try
            Label1.Text = "Edit Inventory Item"
            Label2.Text = "Update the details of this inventory item below."
            AddItem.Text = "Save Changes"

            Label6.Text = "Current Stock (read-only):"
            Label7.Text = "Avg Cost per Unit (read-only):"
            Label8.Text = "Last Restocked (read-only):"
            Label11.Text = "Next Expiration (read-only):"

            Quantity.ReadOnly = True
            Quantity.Enabled = False
            RoundedTextBox1.ReadOnly = True
            RoundedTextBox1.Enabled = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        Catch
            ' Styling failures can be ignored
        End Try
    End Sub

    ' Load categories into Category dropdown
    Private Sub LoadCategories()
        Try
            openConn()

            Category.Items.Clear()
            Category.Items.Add("-- Select Category --")

            Dim sql As String = "SELECT CategoryName FROM ingredient_categories ORDER BY CategoryName"
            Dim cmd As New MySqlCommand(sql, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Category.Items.Add(reader.GetString("CategoryName"))
            End While

            reader.Close()
            Category.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show("Error loading categories: " & ex.Message,
                          "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub LoadIngredientDetailsForEdit()
        Try
            openConn()

            Dim sql As String = "
                SELECT 
                    i.IngredientName,
                    i.UnitType,
                    i.MinStockLevel,
                    i.MaxStockLevel,
                    i.LastRestockedDate,
                    COALESCE(ic.CategoryName, '') AS CategoryName,
                    COALESCE(SUM(CASE WHEN ib.BatchStatus = 'Active' THEN ib.StockQuantity END), 0) AS TotalQuantity,
                    COALESCE(AVG(CASE WHEN ib.BatchStatus = 'Active' THEN ib.CostPerUnit END), 0) AS AvgCostPerUnit,
                    MAX(CASE WHEN ib.BatchStatus = 'Active' THEN ib.ExpirationDate END) AS LatestExpiration
                FROM ingredients i
                LEFT JOIN ingredient_categories ic ON ic.CategoryID = i.CategoryID
                LEFT JOIN inventory_batches ib ON ib.IngredientID = i.IngredientID
                WHERE i.IngredientID = @id
                GROUP BY i.IngredientName, i.UnitType, i.MinStockLevel, i.MaxStockLevel, i.LastRestockedDate, ic.CategoryName
            "

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", _ingredientId)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If Not reader.Read() Then
                reader.Close()
                Throw New Exception("Unable to load ingredient details.")
            End If

            txtFullName.Text = reader("IngredientName").ToString()

            Dim categoryName As String = reader("CategoryName").ToString()
            If Not String.IsNullOrWhiteSpace(categoryName) Then
                Dim categoryIndex As Integer = -1
                For i As Integer = 0 To Category.Items.Count - 1
                    If String.Equals(Category.Items(i).ToString(), categoryName, StringComparison.OrdinalIgnoreCase) Then
                        categoryIndex = i
                        Exit For
                    End If
                Next

                If categoryIndex >= 0 Then
                    Category.SelectedIndex = categoryIndex
                Else
                    Category.Items.Add(categoryName)
                    Category.SelectedItem = categoryName
                End If
            Else
                Category.SelectedIndex = 0
            End If

            Dim unitValue As String = reader("UnitType").ToString()
            If Not String.IsNullOrWhiteSpace(unitValue) Then
                Dim unitIndex As Integer = -1
                For i As Integer = 0 To Unit.Items.Count - 1
                    If String.Equals(Unit.Items(i).ToString(), unitValue, StringComparison.OrdinalIgnoreCase) Then
                        unitIndex = i
                        Exit For
                    End If
                Next

                If unitIndex >= 0 Then
                    Unit.SelectedIndex = unitIndex
                Else
                    Unit.Items.Add(unitValue)
                    Unit.SelectedItem = unitValue
                End If
            ElseIf Unit.Items.Count > 0 Then
                Unit.SelectedIndex = 0
            End If

            Dim minStock As Decimal = 0D
            If Not reader.IsDBNull(reader.GetOrdinal("MinStockLevel")) Then
                minStock = Convert.ToDecimal(reader("MinStockLevel"))
            End If

            Dim maxStock As Decimal = 0D
            If Not reader.IsDBNull(reader.GetOrdinal("MaxStockLevel")) Then
                maxStock = Convert.ToDecimal(reader("MaxStockLevel"))
            End If

            If minStock < NumericUpDown1.Minimum Then
                NumericUpDown1.Value = NumericUpDown1.Minimum
            ElseIf minStock > NumericUpDown1.Maximum Then
                NumericUpDown1.Value = NumericUpDown1.Maximum
            Else
                NumericUpDown1.Value = minStock
            End If

            If maxStock < NumericUpDown2.Minimum Then
                NumericUpDown2.Value = NumericUpDown2.Minimum
            ElseIf maxStock > NumericUpDown2.Maximum Then
                NumericUpDown2.Value = NumericUpDown2.Maximum
            Else
                NumericUpDown2.Value = maxStock
            End If

            Dim totalQuantity As Decimal = 0D
            If Not reader.IsDBNull(reader.GetOrdinal("TotalQuantity")) Then
                totalQuantity = Convert.ToDecimal(reader("TotalQuantity"))
            End If
            Quantity.Text = totalQuantity.ToString()

            Dim avgCost As Decimal = 0D
            If Not reader.IsDBNull(reader.GetOrdinal("AvgCostPerUnit")) Then
                avgCost = Convert.ToDecimal(reader("AvgCostPerUnit"))
            End If
            RoundedTextBox1.Text = avgCost.ToString()

            If Not reader.IsDBNull(reader.GetOrdinal("LastRestockedDate")) Then
                DateTimePicker1.Value = Convert.ToDateTime(reader("LastRestockedDate"))
            End If

            If Not reader.IsDBNull(reader.GetOrdinal("LatestExpiration")) Then
                DateTimePicker2.Value = Convert.ToDateTime(reader("LatestExpiration"))
            End If

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading ingredient details: " & ex.Message,
                            "Edit Item",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Finally
            closeConn()
        End Try
    End Sub

    ' Set default values
    Private Sub SetDefaultValues()
        DateTimePicker1.Value = Date.Now ' Purchase Date
        DateTimePicker2.Value = Date.Now.AddDays(30) ' Default expiration 30 days from now
        NumericUpDown1.Value = 5 ' Default min stock
        NumericUpDown2.Value = 100 ' Default max stock
        NumericUpDown1.Maximum = 10000
        NumericUpDown2.Maximum = 10000

        If Unit.Items.Count > 0 Then
            Unit.SelectedIndex = 0 ' Default to first unit
        End If
    End Sub

    ' Add Item Button Click
    Private Sub AddItem_Click(sender As Object, e As EventArgs) Handles AddItem.Click
        If _isEditMode Then
            If ValidateEditInputs() Then
                UpdateExistingIngredient()
            End If
        Else
            If ValidateAddInputs() Then
                AddNewInventoryBatch()
            End If
        End If
    End Sub

    ' Validate all inputs
    Private Function ValidateAddInputs() As Boolean
        ' Item Name
        If String.IsNullOrWhiteSpace(txtFullName.Text) Then
            MessageBox.Show("Please enter an item name.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFullName.Focus()
            Return False
        End If

        ' Category
        If Category.SelectedIndex <= 0 Then
            MessageBox.Show("Please select a category.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Category.Focus()
            Return False
        End If

        ' Quantity
        If String.IsNullOrWhiteSpace(Quantity.Text) OrElse Not IsNumeric(Quantity.Text) Then
            MessageBox.Show("Please enter a valid quantity.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Quantity.Focus()
            Return False
        End If

        If Convert.ToDecimal(Quantity.Text) <= 0 Then
            MessageBox.Show("Quantity must be greater than zero.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Quantity.Focus()
            Return False
        End If

        ' Unit
        If Unit.SelectedIndex < 0 Then
            MessageBox.Show("Please select a unit type.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Unit.Focus()
            Return False
        End If

        ' Cost per Unit
        If String.IsNullOrWhiteSpace(RoundedTextBox1.Text) OrElse Not IsNumeric(RoundedTextBox1.Text) Then
            MessageBox.Show("Please enter a valid cost per unit.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            RoundedTextBox1.Focus()
            Return False
        End If

        If Convert.ToDecimal(RoundedTextBox1.Text) < 0 Then
            MessageBox.Show("Cost per unit cannot be negative.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            RoundedTextBox1.Focus()
            Return False
        End If

        ' Stock Levels
        If NumericUpDown1.Value <= 0 Then
            MessageBox.Show("Minimum stock level must be greater than zero.",
                          "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NumericUpDown1.Focus()
            Return False
        End If

        If NumericUpDown2.Value <= NumericUpDown1.Value Then
            MessageBox.Show("Maximum stock level must be greater than minimum stock level.",
                          "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    Private Function ValidateEditInputs() As Boolean
        If String.IsNullOrWhiteSpace(txtFullName.Text) Then
            MessageBox.Show("Please enter an item name.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFullName.Focus()
            Return False
        End If

        If Category.SelectedIndex <= 0 Then
            MessageBox.Show("Please select a category.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Category.Focus()
            Return False
        End If

        If Unit.SelectedIndex < 0 OrElse String.IsNullOrWhiteSpace(Unit.Text) Then
            MessageBox.Show("Please select a unit type.", "Validation Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Unit.Focus()
            Return False
        End If

        If NumericUpDown1.Value <= 0 Then
            MessageBox.Show("Minimum stock level must be greater than zero.",
                          "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NumericUpDown1.Focus()
            Return False
        End If

        If NumericUpDown2.Value <= NumericUpDown1.Value Then
            MessageBox.Show("Maximum stock level must be greater than minimum stock level.",
                          "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NumericUpDown2.Focus()
            Return False
        End If

        Return True
    End Function

    ' Add new inventory batch to database
    Private Sub AddNewInventoryBatch()
        Try
            openConn()

            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                Dim ingredientID As Integer = 0
                Dim ingredientName As String = txtFullName.Text.Trim()
                Dim categoryID As Integer = GetCategoryID(Category.Text)

                ' Check if ingredient already exists
                Dim sqlCheck As String = "
                    SELECT IngredientID 
                    FROM ingredients 
                    WHERE LOWER(IngredientName) = LOWER(@name) AND IsActive = 1
                "
                Dim cmdCheck As New MySqlCommand(sqlCheck, conn, transaction)
                cmdCheck.Parameters.AddWithValue("@name", ingredientName)
                Dim existingID As Object = cmdCheck.ExecuteScalar()

                If existingID IsNot Nothing Then
                    ' Ingredient exists - just add new batch
                    ingredientID = Convert.ToInt32(existingID)

                    ' Update min/max levels if needed
                    Dim sqlUpdate As String = "
                        UPDATE ingredients
                        SET MinStockLevel = @minStock,
                            MaxStockLevel = @maxStock,
                            UnitType = @unit
                        WHERE IngredientID = @id
                    "
                    Dim cmdUpdate As New MySqlCommand(sqlUpdate, conn, transaction)
                    cmdUpdate.Parameters.AddWithValue("@minStock", NumericUpDown1.Value)
                    cmdUpdate.Parameters.AddWithValue("@maxStock", NumericUpDown2.Value)
                    cmdUpdate.Parameters.AddWithValue("@unit", Unit.Text)
                    cmdUpdate.Parameters.AddWithValue("@id", ingredientID)
                    cmdUpdate.ExecuteNonQuery()

                Else
                    ' New ingredient - insert it first
                    Dim sqlInsertIngredient As String = "
                        INSERT INTO ingredients (
                            IngredientName, CategoryID, UnitType,
                            StockQuantity, MinStockLevel, MaxStockLevel, 
                            IsActive, IsPerishable, LastRestockedDate
                        ) VALUES (
                            @name, @category, @unit,
                            0, @minStock, @maxStock, 
                            1, 1, NOW()
                        );
                        SELECT LAST_INSERT_ID();
                    "
                    Dim cmdInsert As New MySqlCommand(sqlInsertIngredient, conn, transaction)
                    cmdInsert.Parameters.AddWithValue("@name", ingredientName)
                    cmdInsert.Parameters.AddWithValue("@category", categoryID)
                    cmdInsert.Parameters.AddWithValue("@unit", Unit.Text)
                    cmdInsert.Parameters.AddWithValue("@minStock", NumericUpDown1.Value)
                    cmdInsert.Parameters.AddWithValue("@maxStock", NumericUpDown2.Value)
                    ingredientID = Convert.ToInt32(cmdInsert.ExecuteScalar())
                End If

                ' Now add the batch using stored procedure
                Dim cmdBatch As New MySqlCommand("AddInventoryBatch", conn, transaction)
                cmdBatch.CommandType = CommandType.StoredProcedure

                cmdBatch.Parameters.AddWithValue("@p_ingredient_id", ingredientID)
                cmdBatch.Parameters.AddWithValue("@p_quantity", Convert.ToDecimal(Quantity.Text))
                cmdBatch.Parameters.AddWithValue("@p_unit_type", Unit.Text)
                cmdBatch.Parameters.AddWithValue("@p_cost_per_unit", Convert.ToDecimal(RoundedTextBox1.Text))
                cmdBatch.Parameters.AddWithValue("@p_expiration_date", DateTimePicker2.Value.Date)

                ' ADD THIS LINE - Set storage location based on category or use default
                cmdBatch.Parameters.AddWithValue("@p_storage_location", DBNull.Value) ' Will use 'Main Storage' default

                cmdBatch.Parameters.AddWithValue("@p_notes",
                "Initial batch added on " & DateTimePicker1.Value.ToString("yyyy-MM-dd"))

                ' Output parameters
                Dim paramBatchID As New MySqlParameter("@p_batch_id", MySqlDbType.Int32)
                paramBatchID.Direction = ParameterDirection.Output
                cmdBatch.Parameters.Add(paramBatchID)

                Dim paramBatchNumber As New MySqlParameter("@p_batch_number", MySqlDbType.VarChar, 50)
                paramBatchNumber.Direction = ParameterDirection.Output
                cmdBatch.Parameters.Add(paramBatchNumber)

                cmdBatch.ExecuteNonQuery()

                Dim batchID As Integer = Convert.ToInt32(paramBatchID.Value)
                Dim batchNumber As String = paramBatchNumber.Value.ToString()

                ' Commit transaction
                transaction.Commit()

                ' Show success message
                MessageBox.Show(
                    "Inventory batch added successfully!" & vbCrLf & vbCrLf &
                    "Ingredient: " & ingredientName & vbCrLf &
                    "Batch #: " & batchNumber & vbCrLf &
                    "Quantity: " & Quantity.Text & " " & Unit.Text & vbCrLf &
                    "Total Cost: ₱" & (Convert.ToDecimal(Quantity.Text) * Convert.ToDecimal(RoundedTextBox1.Text)).ToString("#,##0.00"),
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information)

                ' Set dialog result and close
                Me.DialogResult = DialogResult.OK
                Me.Close()

            Catch ex As Exception
                transaction.Rollback()
                Throw
            End Try

        Catch ex As Exception
            MessageBox.Show("Error adding inventory batch: " & ex.Message,
                          "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub UpdateExistingIngredient()
        Try
            openConn()

            Dim sql As String = "
                UPDATE ingredients
                SET IngredientName = @name,
                    CategoryID = @category,
                    UnitType = @unit,
                    MinStockLevel = @minLevel,
                    MaxStockLevel = @maxLevel
                WHERE IngredientID = @id
            "

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim())
            cmd.Parameters.AddWithValue("@category", GetCategoryID(Category.Text))
            cmd.Parameters.AddWithValue("@unit", Unit.Text)
            cmd.Parameters.AddWithValue("@minLevel", NumericUpDown1.Value)
            cmd.Parameters.AddWithValue("@maxLevel", NumericUpDown2.Value)
            cmd.Parameters.AddWithValue("@id", _ingredientId)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Inventory item updated successfully.",
                            "Edit Item",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error updating inventory item: " & ex.Message,
                          "Edit Item",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    ' Get Category ID from name
    Private Function GetCategoryID(categoryName As String) As Integer
        Try
            Dim sql As String = "SELECT CategoryID FROM ingredient_categories WHERE CategoryName = @name"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@name", categoryName)

            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                Return Convert.ToInt32(result)
            End If

            Return 1 ' Default to first category if not found
        Catch ex As Exception
            Return 1
        End Try
    End Function

    ' Cancel Button
    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' Auto-calculate total cost
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
                ' Display total if you add a label: lblTotalCost.Text = "Total: ₱" & total.ToString("#,##0.00")
            End If
        Catch ex As Exception
            ' Ignore calculation errors during typing
        End Try
    End Sub

End Class