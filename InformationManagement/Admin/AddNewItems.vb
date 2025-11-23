Imports MySqlConnector

Public Class AddNewItems

    Private Sub AddNewItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadIngredientsList()
        LoadCategories()
        SetDefaultValues()
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

    ' Load existing ingredients into a dropdown
    Private Sub LoadIngredientsList()
        Try
            openConn()

            Dim sql As String = "
                SELECT 
                    IngredientID,
                    CONCAT(IngredientName, ' (', UnitType, ')') AS DisplayName
                FROM ingredients
                WHERE IsActive = 1
                ORDER BY IngredientName
            "

            Dim cmd As New MySqlCommand(sql, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Dim ingredientList As New Dictionary(Of Integer, String)

            While reader.Read()
                ingredientList.Add(reader.GetInt32("IngredientID"), reader.GetString("DisplayName"))
            End While

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading ingredients: " & ex.Message)
        Finally
            closeConn()
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
            MessageBox.Show("Error loading categories: " & ex.Message)
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
        If Unit.Items.Count > 0 Then
            Unit.SelectedIndex = 0 ' Default to first unit
        End If
    End Sub

    ' Add Item Button Click
    Private Sub AddItem_Click(sender As Object, e As EventArgs) Handles AddItem.Click
        If ValidateInputs() Then
            AddNewInventoryBatch()
        End If
    End Sub

    ' Validate all inputs
    Private Function ValidateInputs() As Boolean
        ' Item Name
        If String.IsNullOrWhiteSpace(txtFullName.Text) Then
            MessageBox.Show("Please enter an item name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFullName.Focus()
            Return False
        End If

        ' Category
        If Category.SelectedIndex <= 0 Then
            MessageBox.Show("Please select a category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Category.Focus()
            Return False
        End If

        ' Quantity
        If String.IsNullOrWhiteSpace(Quantity.Text) OrElse Not IsNumeric(Quantity.Text) Then
            MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Quantity.Focus()
            Return False
        End If

        If Convert.ToDecimal(Quantity.Text) <= 0 Then
            MessageBox.Show("Quantity must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    ' Add new inventory batch to database
    Private Sub AddNewInventoryBatch()
        Try
            openConn()

            ' Start transaction
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
                            MinStockLevel, MaxStockLevel, IsActive
                        ) VALUES (
                            @name, @category, @unit,
                            @minStock, @maxStock, 1
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
                Dim batchID As Integer
                Dim batchNumber As String = ""

                Dim cmdBatch As New MySqlCommand("AddInventoryBatch", conn, transaction)
                cmdBatch.CommandType = CommandType.StoredProcedure

                cmdBatch.Parameters.AddWithValue("@p_ingredient_id", ingredientID)
                cmdBatch.Parameters.AddWithValue("@p_quantity", Convert.ToDecimal(Quantity.Text))
                cmdBatch.Parameters.AddWithValue("@p_unit_type", Unit.Text)
                cmdBatch.Parameters.AddWithValue("@p_cost_per_unit", Convert.ToDecimal(RoundedTextBox1.Text))
                cmdBatch.Parameters.AddWithValue("@p_expiration_date", DateTimePicker2.Value.Date)
                cmdBatch.Parameters.AddWithValue("@p_market_source", "Owner Purchase")
                cmdBatch.Parameters.AddWithValue("@p_notes", "Inventory added on " & DateTimePicker1.Value.ToShortDateString())

                ' Output parameters
                Dim paramBatchID As New MySqlParameter("@p_batch_id", MySqlDbType.Int32)
                paramBatchID.Direction = ParameterDirection.Output
                cmdBatch.Parameters.Add(paramBatchID)

                Dim paramBatchNumber As New MySqlParameter("@p_batch_number", MySqlDbType.VarChar, 50)
                paramBatchNumber.Direction = ParameterDirection.Output
                cmdBatch.Parameters.Add(paramBatchNumber)

                cmdBatch.ExecuteNonQuery()

                batchID = Convert.ToInt32(paramBatchID.Value)
                batchNumber = paramBatchNumber.Value.ToString()

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
            MessageBox.Show("Error adding inventory batch: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                ' You can display this in a label if you add one to the form
                ' lblTotalCost.Text = "Total: ₱" & total.ToString("#,##0.00")
            End If
        Catch ex As Exception
            ' Ignore calculation errors during typing
        End Try
    End Sub

End Class