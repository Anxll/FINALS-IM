Imports MySqlConnector

Public Class FormEditIngredients

    Private currentProductId As Integer = 0
    Private currentProductName As String = ""

    Public Sub LoadProductData(productId As Integer)
        currentProductId = productId

        Try
            openConn()

            Dim cmdProduct As New MySqlCommand("SELECT ProductName FROM products WHERE ProductID = @id", conn)
            cmdProduct.Parameters.AddWithValue("@id", productId)
            currentProductName = cmdProduct.ExecuteScalar()?.ToString()

            conn.Close()

            InitializeUI()
            LoadCurrentIngredients()
            LoadAvailableIngredients()

        Catch ex As Exception
            MessageBox.Show("Error loading product data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub InitializeUI()
        Me.Text = "Edit Ingredients - " & currentProductName
        Me.Size = New Size(900, 700)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(245, 245, 245)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        Me.Controls.Clear()

        Dim mainPanel As New Panel()
        mainPanel.Dock = DockStyle.Fill
        mainPanel.Padding = New Padding(20)
        Me.Controls.Add(mainPanel)

        Dim lblTitle As New Label()
        lblTitle.Text = "✏️ Edit Ingredients for: " & currentProductName
        lblTitle.Font = New Font("Segoe UI", 16, FontStyle.Bold)
        lblTitle.Location = New Point(20, 20)
        lblTitle.Size = New Size(860, 40)
        lblTitle.ForeColor = Color.FromArgb(52, 73, 94)
        mainPanel.Controls.Add(lblTitle)

        Dim leftPanel As New Panel()
        leftPanel.Location = New Point(20, 70)
        leftPanel.Size = New Size(420, 500)
        leftPanel.BackColor = Color.White
        leftPanel.BorderStyle = BorderStyle.FixedSingle
        mainPanel.Controls.Add(leftPanel)

        Dim lblCurrent As New Label()
        lblCurrent.Text = "📋 Current Ingredients"
        lblCurrent.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblCurrent.Location = New Point(10, 10)
        lblCurrent.Size = New Size(400, 30)
        lblCurrent.ForeColor = Color.FromArgb(41, 128, 185)
        leftPanel.Controls.Add(lblCurrent)

        Dim dgvCurrent As New DataGridView()
        dgvCurrent.Name = "dgvCurrentIngredients"
        dgvCurrent.Location = New Point(10, 45)
        dgvCurrent.Size = New Size(400, 400)
        dgvCurrent.AllowUserToAddRows = False
        dgvCurrent.ReadOnly = False
        dgvCurrent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCurrent.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCurrent.MultiSelect = False
        dgvCurrent.BackgroundColor = Color.White
        dgvCurrent.BorderStyle = BorderStyle.None
        leftPanel.Controls.Add(dgvCurrent)

        Dim btnRemove As New Button()
        btnRemove.Name = "btnRemove"
        btnRemove.Text = "🗑️ Remove Selected"
        btnRemove.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnRemove.Location = New Point(10, 455)
        btnRemove.Size = New Size(400, 35)
        btnRemove.BackColor = Color.FromArgb(231, 76, 60)
        btnRemove.ForeColor = Color.White
        btnRemove.FlatStyle = FlatStyle.Flat
        btnRemove.FlatAppearance.BorderSize = 0
        btnRemove.Cursor = Cursors.Hand
        AddHandler btnRemove.Click, AddressOf RemoveIngredient_Click
        leftPanel.Controls.Add(btnRemove)

        Dim rightPanel As New Panel()
        rightPanel.Location = New Point(460, 70)
        rightPanel.Size = New Size(420, 500)
        rightPanel.BackColor = Color.White
        rightPanel.BorderStyle = BorderStyle.FixedSingle
        mainPanel.Controls.Add(rightPanel)

        Dim lblAdd As New Label()
        lblAdd.Text = "➕ Add Ingredient to Product"
        lblAdd.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblAdd.Location = New Point(10, 10)
        lblAdd.Size = New Size(400, 30)
        lblAdd.ForeColor = Color.FromArgb(39, 174, 96)
        rightPanel.Controls.Add(lblAdd)

        Dim rbExisting As New RadioButton()
        rbExisting.Name = "rbExisting"
        rbExisting.Text = "Select from existing ingredients"
        rbExisting.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        rbExisting.Location = New Point(10, 45)
        rbExisting.Size = New Size(250, 20)
        rbExisting.Checked = True
        AddHandler rbExisting.CheckedChanged, AddressOf RadioButton_CheckedChanged
        rightPanel.Controls.Add(rbExisting)

        Dim rbNewIngredient As New RadioButton()
        rbNewIngredient.Name = "rbNewIngredient"
        rbNewIngredient.Text = "Create new ingredient"
        rbNewIngredient.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        rbNewIngredient.Location = New Point(270, 45)
        rbNewIngredient.Size = New Size(150, 20)
        AddHandler rbNewIngredient.CheckedChanged, AddressOf RadioButton_CheckedChanged
        rightPanel.Controls.Add(rbNewIngredient)

        Dim pnlExisting As New Panel()
        pnlExisting.Name = "pnlExisting"
        pnlExisting.Location = New Point(10, 75)
        pnlExisting.Size = New Size(400, 180)
        pnlExisting.BackColor = Color.FromArgb(250, 250, 250)
        pnlExisting.BorderStyle = BorderStyle.FixedSingle
        rightPanel.Controls.Add(pnlExisting)

        Dim lblSelectIngredient As New Label()
        lblSelectIngredient.Text = "Select Ingredient:"
        lblSelectIngredient.Font = New Font("Segoe UI", 10)
        lblSelectIngredient.Location = New Point(10, 10)
        lblSelectIngredient.Size = New Size(150, 25)
        pnlExisting.Controls.Add(lblSelectIngredient)

        Dim cmbIngredients As New ComboBox()
        cmbIngredients.Name = "cmbIngredients"
        cmbIngredients.Font = New Font("Segoe UI", 10)
        cmbIngredients.Location = New Point(10, 40)
        cmbIngredients.Size = New Size(380, 30)
        cmbIngredients.DropDownStyle = ComboBoxStyle.DropDownList
        pnlExisting.Controls.Add(cmbIngredients)

        Dim lblQuantityExist As New Label()
        lblQuantityExist.Text = "Quantity:"
        lblQuantityExist.Font = New Font("Segoe UI", 10)
        lblQuantityExist.Location = New Point(10, 80)
        lblQuantityExist.Size = New Size(150, 25)
        pnlExisting.Controls.Add(lblQuantityExist)

        Dim txtQuantityExist As New TextBox()
        txtQuantityExist.Name = "txtQuantityExist"
        txtQuantityExist.Font = New Font("Segoe UI", 10)
        txtQuantityExist.Location = New Point(10, 110)
        txtQuantityExist.Size = New Size(180, 30)
        pnlExisting.Controls.Add(txtQuantityExist)

        Dim lblUnitExist As New Label()
        lblUnitExist.Text = "Unit:"
        lblUnitExist.Font = New Font("Segoe UI", 10)
        lblUnitExist.Location = New Point(210, 80)
        lblUnitExist.Size = New Size(150, 25)
        pnlExisting.Controls.Add(lblUnitExist)

        ' DROPDOWN for Unit
        Dim cmbUnitExist As New ComboBox()
        cmbUnitExist.Name = "cmbUnitExist"
        cmbUnitExist.Font = New Font("Segoe UI", 10)
        cmbUnitExist.Location = New Point(210, 110)
        cmbUnitExist.Size = New Size(180, 30)
        cmbUnitExist.DropDownStyle = ComboBoxStyle.DropDown
        cmbUnitExist.Items.AddRange(New String() {"g", "kg", "ml", "L", "pcs", "tbsp", "tsp", "cup", "oz", "lb", "bottle", "can", "pack", "bundle"})
        pnlExisting.Controls.Add(cmbUnitExist)

        Dim pnlNewIngredient As New Panel()
        pnlNewIngredient.Name = "pnlNewIngredient"
        pnlNewIngredient.Location = New Point(10, 75)
        pnlNewIngredient.Size = New Size(400, 300)
        pnlNewIngredient.BackColor = Color.FromArgb(250, 250, 250)
        pnlNewIngredient.BorderStyle = BorderStyle.FixedSingle
        pnlNewIngredient.Visible = False
        rightPanel.Controls.Add(pnlNewIngredient)

        Dim lblNewName As New Label()
        lblNewName.Text = "Ingredient Name:"
        lblNewName.Font = New Font("Segoe UI", 10)
        lblNewName.Location = New Point(10, 10)
        lblNewName.Size = New Size(150, 25)
        pnlNewIngredient.Controls.Add(lblNewName)

        Dim txtNewName As New TextBox()
        txtNewName.Name = "txtNewName"
        txtNewName.Font = New Font("Segoe UI", 10)
        txtNewName.Location = New Point(10, 40)
        txtNewName.Size = New Size(380, 30)
        pnlNewIngredient.Controls.Add(txtNewName)

        Dim lblStockUnit As New Label()
        lblStockUnit.Text = "Stock Unit (for inventory):"
        lblStockUnit.Font = New Font("Segoe UI", 10)
        lblStockUnit.Location = New Point(10, 80)
        lblStockUnit.Size = New Size(200, 25)
        pnlNewIngredient.Controls.Add(lblStockUnit)

        Dim cmbStockUnit As New ComboBox()
        cmbStockUnit.Name = "cmbStockUnit"
        cmbStockUnit.Font = New Font("Segoe UI", 10)
        cmbStockUnit.Location = New Point(10, 110)
        cmbStockUnit.Size = New Size(180, 30)
        cmbStockUnit.DropDownStyle = ComboBoxStyle.DropDown
        cmbStockUnit.Items.AddRange(New String() {"kg", "g", "L", "ml", "pack", "bottle", "can", "bundle", "tray", "jar"})
        cmbStockUnit.Text = "kg"
        pnlNewIngredient.Controls.Add(cmbStockUnit)

        Dim chkPerishable As New CheckBox()
        chkPerishable.Name = "chkPerishable"
        chkPerishable.Text = "Perishable"
        chkPerishable.Font = New Font("Segoe UI", 9)
        chkPerishable.Location = New Point(210, 110)
        chkPerishable.Size = New Size(180, 30)
        chkPerishable.Checked = True
        pnlNewIngredient.Controls.Add(chkPerishable)

        Dim lblQuantityNew As New Label()
        lblQuantityNew.Text = "Quantity Used (in recipe):"
        lblQuantityNew.Font = New Font("Segoe UI", 10)
        lblQuantityNew.Location = New Point(10, 150)
        lblQuantityNew.Size = New Size(200, 25)
        pnlNewIngredient.Controls.Add(lblQuantityNew)

        Dim txtQuantityNew As New TextBox()
        txtQuantityNew.Name = "txtQuantityNew"
        txtQuantityNew.Font = New Font("Segoe UI", 10)
        txtQuantityNew.Location = New Point(10, 180)
        txtQuantityNew.Size = New Size(180, 30)
        pnlNewIngredient.Controls.Add(txtQuantityNew)

        Dim lblUnitNew As New Label()
        lblUnitNew.Text = "Unit (recipe):"
        lblUnitNew.Font = New Font("Segoe UI", 10)
        lblUnitNew.Location = New Point(210, 150)
        lblUnitNew.Size = New Size(150, 25)
        pnlNewIngredient.Controls.Add(lblUnitNew)

        Dim cmbUnitNew As New ComboBox()
        cmbUnitNew.Name = "cmbUnitNew"
        cmbUnitNew.Font = New Font("Segoe UI", 10)
        cmbUnitNew.Location = New Point(210, 180)
        cmbUnitNew.Size = New Size(180, 30)
        cmbUnitNew.DropDownStyle = ComboBoxStyle.DropDown
        cmbUnitNew.Items.AddRange(New String() {"g", "kg", "ml", "L", "pcs", "tbsp", "tsp", "cup", "oz", "lb"})
        pnlNewIngredient.Controls.Add(cmbUnitNew)

        Dim lblHelper As New Label()
        lblHelper.Text = "💡 Tip: Stock unit is for inventory (e.g., kg)," & vbCrLf &
                        "Recipe unit is for cooking (e.g., g, ml)"
        lblHelper.Font = New Font("Segoe UI", 8, FontStyle.Italic)
        lblHelper.Location = New Point(10, 220)
        lblHelper.Size = New Size(380, 40)
        lblHelper.ForeColor = Color.Gray
        pnlNewIngredient.Controls.Add(lblHelper)

        Dim btnAdd As New Button()
        btnAdd.Name = "btnAdd"
        btnAdd.Text = "➕ Add to Product"
        btnAdd.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnAdd.Location = New Point(10, 395)
        btnAdd.Size = New Size(400, 40)
        btnAdd.BackColor = Color.FromArgb(39, 174, 96)
        btnAdd.ForeColor = Color.White
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.Cursor = Cursors.Hand
        AddHandler btnAdd.Click, AddressOf AddIngredient_Click
        rightPanel.Controls.Add(btnAdd)

        Dim instructionsPanel As New Panel()
        instructionsPanel.Location = New Point(10, 445)
        instructionsPanel.Size = New Size(400, 45)
        instructionsPanel.BackColor = Color.FromArgb(250, 250, 250)
        instructionsPanel.BorderStyle = BorderStyle.FixedSingle
        rightPanel.Controls.Add(instructionsPanel)

        Dim lblInstructions As New Label()
        lblInstructions.Text = "📝 Choose to add from existing ingredients" & vbCrLf &
                              "or create a new ingredient and add it to this product."
        lblInstructions.Font = New Font("Segoe UI", 9)
        lblInstructions.Location = New Point(10, 10)
        lblInstructions.Size = New Size(380, 30)
        lblInstructions.ForeColor = Color.FromArgb(127, 140, 141)
        instructionsPanel.Controls.Add(lblInstructions)

        Dim btnSave As New Button()
        btnSave.Text = "💾 Save Changes"
        btnSave.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        btnSave.Location = New Point(20, 580)
        btnSave.Size = New Size(200, 40)
        btnSave.BackColor = Color.FromArgb(39, 174, 96)
        btnSave.ForeColor = Color.White
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.Cursor = Cursors.Hand
        AddHandler btnSave.Click, AddressOf SaveChanges_Click
        mainPanel.Controls.Add(btnSave)

        Dim btnCancel As New Button()
        btnCancel.Text = "✕ Cancel"
        btnCancel.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        btnCancel.Location = New Point(680, 580)
        btnCancel.Size = New Size(200, 40)
        btnCancel.BackColor = Color.FromArgb(149, 165, 166)
        btnCancel.ForeColor = Color.White
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.Cursor = Cursors.Hand
        AddHandler btnCancel.Click, Sub()
                                        Me.DialogResult = DialogResult.Cancel
                                        Me.Close()
                                    End Sub
        mainPanel.Controls.Add(btnCancel)
    End Sub

    ' =======================================================
    ' LOAD CURRENT INGREDIENTS - USING DATATAG TO STORE ID
    ' =======================================================
    Private Sub LoadCurrentIngredients()
        Dim dgv As DataGridView = CType(Me.Controls.Find("dgvCurrentIngredients", True)(0), DataGridView)

        Try
            openConn()

            Dim query As String = "
                SELECT 
                    pi.ProductIngredientID,
                    i.IngredientName,
                    pi.QuantityUsed,
                    pi.UnitType
                FROM product_ingredients pi
                INNER JOIN ingredients i ON pi.IngredientID = i.IngredientID
                WHERE pi.ProductID = @productId
                ORDER BY i.IngredientName ASC
            "

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@productId", currentProductId)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            ' Clear DataGridView
            dgv.DataSource = Nothing
            dgv.Rows.Clear()
            dgv.Columns.Clear()

            ' Create columns
            dgv.Columns.Add("Ingredient", "Ingredient")
            dgv.Columns.Add("Quantity", "Quantity")

            ' Unit column as ComboBox
            Dim unitColumn As New DataGridViewComboBoxColumn()
            unitColumn.Name = "Unit"
            unitColumn.HeaderText = "Unit"
            unitColumn.Items.AddRange(New String() {"g", "kg", "ml", "L", "pcs", "tbsp", "tsp", "cup", "oz", "lb", "bottle", "can", "pack", "bundle"})
            dgv.Columns.Add(unitColumn)

            ' Set properties
            dgv.Columns("Ingredient").ReadOnly = True
            dgv.Columns("Quantity").ReadOnly = False
            dgv.Columns("Unit").ReadOnly = False

            ' Add rows and store ProductIngredientID in Tag
            For Each row As DataRow In dt.Rows
                Dim rowIndex As Integer = dgv.Rows.Add(
                    row("IngredientName"),
                    row("QuantityUsed"),
                    row("UnitType")
                )

                ' CRITICAL: Store ProductIngredientID in the row's Tag property
                dgv.Rows(rowIndex).Tag = Convert.ToInt32(row("ProductIngredientID"))
            Next

        Catch ex As Exception
            MessageBox.Show("Error loading ingredients: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub LoadAvailableIngredients()
        Dim cmb As ComboBox = CType(Me.Controls.Find("cmbIngredients", True)(0), ComboBox)
        cmb.Items.Clear()

        Try
            openConn()

            Dim query As String = "SELECT IngredientID, IngredientName FROM ingredients WHERE IsActive = 1 ORDER BY IngredientName ASC"
            Dim cmd As New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim item As New IngredientItem()
                item.Id = Convert.ToInt32(reader("IngredientID"))
                item.Name = reader("IngredientName").ToString()
                cmb.Items.Add(item)
            End While

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading ingredients: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs)
        Dim rb As RadioButton = CType(sender, RadioButton)
        If Not rb.Checked Then Return

        Dim pnlExisting As Panel = CType(Me.Controls.Find("pnlExisting", True)(0), Panel)
        Dim pnlNewIngredient As Panel = CType(Me.Controls.Find("pnlNewIngredient", True)(0), Panel)

        If rb.Name = "rbExisting" Then
            pnlExisting.Visible = True
            pnlNewIngredient.Visible = False
        ElseIf rb.Name = "rbNewIngredient" Then
            pnlExisting.Visible = False
            pnlNewIngredient.Visible = True
        End If
    End Sub

    Private Sub AddIngredient_Click(sender As Object, e As EventArgs)
        Try
            Dim rbExisting As RadioButton = CType(Me.Controls.Find("rbExisting", True)(0), RadioButton)

            If rbExisting.Checked Then
                AddExistingIngredient()
            Else
                CreateAndAddNewIngredient()
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddExistingIngredient()
        Dim cmbIngredients As ComboBox = CType(Me.Controls.Find("cmbIngredients", True)(0), ComboBox)
        Dim txtQuantityExist As TextBox = CType(Me.Controls.Find("txtQuantityExist", True)(0), TextBox)
        Dim cmbUnitExist As ComboBox = CType(Me.Controls.Find("cmbUnitExist", True)(0), ComboBox)

        If cmbIngredients.SelectedItem Is Nothing Then
            MessageBox.Show("Please select an ingredient.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim quantity As Decimal
        If Not Decimal.TryParse(txtQuantityExist.Text, quantity) OrElse quantity <= 0 Then
            MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(cmbUnitExist.Text) Then
            MessageBox.Show("Please select or enter a unit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedIngredient As IngredientItem = CType(cmbIngredients.SelectedItem, IngredientItem)

        Try
            openConn()

            Dim checkQuery As String = "SELECT COUNT(*) FROM product_ingredients WHERE ProductID = @productId AND IngredientID = @ingredientId"
            Dim checkCmd As New MySqlCommand(checkQuery, conn)
            checkCmd.Parameters.AddWithValue("@productId", currentProductId)
            checkCmd.Parameters.AddWithValue("@ingredientId", selectedIngredient.Id)

            If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                MessageBox.Show("This ingredient is already added.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                conn.Close()
                Return
            End If

            Dim insertQuery As String = "INSERT INTO product_ingredients (ProductID, IngredientID, QuantityUsed, UnitType, CreatedDate, UpdatedDate) VALUES (@productId, @ingredientId, @quantity, @unit, NOW(), NOW())"
            Dim insertCmd As New MySqlCommand(insertQuery, conn)
            insertCmd.Parameters.AddWithValue("@productId", currentProductId)
            insertCmd.Parameters.AddWithValue("@ingredientId", selectedIngredient.Id)
            insertCmd.Parameters.AddWithValue("@quantity", quantity)
            insertCmd.Parameters.AddWithValue("@unit", cmbUnitExist.Text.Trim())

            insertCmd.ExecuteNonQuery()

            MessageBox.Show("✓ Ingredient added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            cmbIngredients.SelectedIndex = -1
            txtQuantityExist.Clear()
            cmbUnitExist.Text = ""

            LoadCurrentIngredients()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub CreateAndAddNewIngredient()
        Dim txtNewName As TextBox = CType(Me.Controls.Find("txtNewName", True)(0), TextBox)
        Dim cmbStockUnit As ComboBox = CType(Me.Controls.Find("cmbStockUnit", True)(0), ComboBox)
        Dim chkPerishable As CheckBox = CType(Me.Controls.Find("chkPerishable", True)(0), CheckBox)
        Dim txtQuantityNew As TextBox = CType(Me.Controls.Find("txtQuantityNew", True)(0), TextBox)
        Dim cmbUnitNew As ComboBox = CType(Me.Controls.Find("cmbUnitNew", True)(0), ComboBox)

        If String.IsNullOrWhiteSpace(txtNewName.Text) Then
            MessageBox.Show("Please enter an ingredient name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(cmbStockUnit.Text) Then
            MessageBox.Show("Please select or enter a stock unit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(cmbUnitNew.Text) Then
            MessageBox.Show("Please select or enter a recipe unit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim quantity As Decimal
        If Not Decimal.TryParse(txtQuantityNew.Text, quantity) OrElse quantity <= 0 Then
            MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            openConn()

            ' Check if ingredient already exists
            Dim checkQuery As String = "SELECT IngredientID FROM ingredients WHERE IngredientName = @name"
            Dim checkCmd As New MySqlCommand(checkQuery, conn)
            checkCmd.Parameters.AddWithValue("@name", txtNewName.Text.Trim())
            Dim existingId As Object = checkCmd.ExecuteScalar()

            Dim ingredientId As Integer

            If existingId IsNot Nothing Then
                ' Ingredient already exists, use existing ID
                ingredientId = Convert.ToInt32(existingId)
            Else
                ' Get the next available IngredientID manually
                Dim getMaxIdQuery As String = "SELECT COALESCE(MAX(IngredientID), 0) + 1 FROM ingredients"
                Dim getMaxIdCmd As New MySqlCommand(getMaxIdQuery, conn)
                Dim nextId As Integer = Convert.ToInt32(getMaxIdCmd.ExecuteScalar())

                ' Insert new ingredient with explicit ID
                Dim insertIngredientQuery As String = "INSERT INTO ingredients (IngredientID, IngredientName, UnitType, StockQuantity, IsActive, IsPerishable, LastRestockedDate, MinStockLevel, MaxStockLevel) VALUES (@id, @name, @unitType, 0, 1, @isPerishable, NOW(), 5.00, 100.00)"
                Dim insertIngredientCmd As New MySqlCommand(insertIngredientQuery, conn)
                insertIngredientCmd.Parameters.AddWithValue("@id", nextId)
                insertIngredientCmd.Parameters.AddWithValue("@name", txtNewName.Text.Trim())
                insertIngredientCmd.Parameters.AddWithValue("@unitType", cmbStockUnit.Text.Trim())
                insertIngredientCmd.Parameters.AddWithValue("@isPerishable", If(chkPerishable.Checked, 1, 0))

                insertIngredientCmd.ExecuteNonQuery()
                ingredientId = nextId
            End If

            ' Check if this ingredient is already linked to the product
            Dim checkProductIngredientQuery As String = "SELECT COUNT(*) FROM product_ingredients WHERE ProductID = @productId AND IngredientID = @ingredientId"
            Dim checkProductCmd As New MySqlCommand(checkProductIngredientQuery, conn)
            checkProductCmd.Parameters.AddWithValue("@productId", currentProductId)
            checkProductCmd.Parameters.AddWithValue("@ingredientId", ingredientId)

            If Convert.ToInt32(checkProductCmd.ExecuteScalar()) > 0 Then
                MessageBox.Show("This ingredient is already added to this product.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                conn.Close()
                Return
            End If

            ' Get next ProductIngredientID
            Dim getMaxPIIdQuery As String = "SELECT COALESCE(MAX(ProductIngredientID), 0) + 1 FROM product_ingredients"
            Dim getMaxPIIdCmd As New MySqlCommand(getMaxPIIdQuery, conn)
            Dim nextPIId As Integer = Convert.ToInt32(getMaxPIIdCmd.ExecuteScalar())

            ' Insert into product_ingredients with explicit ID
            Dim insertProductIngredientQuery As String = "INSERT INTO product_ingredients (ProductIngredientID, ProductID, IngredientID, QuantityUsed, UnitType, CreatedDate, UpdatedDate) VALUES (@piid, @productId, @ingredientId, @quantity, @unit, NOW(), NOW())"
            Dim insertProductCmd As New MySqlCommand(insertProductIngredientQuery, conn)
            insertProductCmd.Parameters.AddWithValue("@piid", nextPIId)
            insertProductCmd.Parameters.AddWithValue("@productId", currentProductId)
            insertProductCmd.Parameters.AddWithValue("@ingredientId", ingredientId)
            insertProductCmd.Parameters.AddWithValue("@quantity", quantity)
            insertProductCmd.Parameters.AddWithValue("@unit", cmbUnitNew.Text.Trim())

            insertProductCmd.ExecuteNonQuery()

            MessageBox.Show("✓ Ingredient created and added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            txtNewName.Clear()
            txtQuantityNew.Clear()
            cmbUnitNew.Text = ""

            LoadCurrentIngredients()
            LoadAvailableIngredients()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub
    ' =======================================================
    ' REMOVE INGREDIENT - USING ROW.TAG FOR ID
    ' =======================================================
    Private Sub RemoveIngredient_Click(sender As Object, e As EventArgs)
        Dim dgv As DataGridView = CType(Me.Controls.Find("dgvCurrentIngredients", True)(0), DataGridView)

        If dgv.SelectedRows.Count = 0 AndAlso dgv.CurrentRow Is Nothing Then
            MessageBox.Show("Please select an ingredient to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedRow As DataGridViewRow = If(dgv.SelectedRows.Count > 0, dgv.SelectedRows(0), dgv.CurrentRow)

        If selectedRow Is Nothing OrElse selectedRow.Tag Is Nothing Then
            MessageBox.Show("Invalid row selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Get ProductIngredientID from Row.Tag
        Dim productIngredientId As Integer = Convert.ToInt32(selectedRow.Tag)
        Dim ingredientName As String = selectedRow.Cells("Ingredient").Value.ToString()

        If MessageBox.Show($"Remove '{ingredientName}'?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Try
            openConn()

            Dim deleteQuery As String = "DELETE FROM product_ingredients WHERE ProductIngredientID = @piid AND ProductID = @pid LIMIT 1"
            Dim cmd As New MySqlCommand(deleteQuery, conn)
            cmd.Parameters.AddWithValue("@piid", productIngredientId)
            cmd.Parameters.AddWithValue("@pid", currentProductId)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected = 1 Then
                MessageBox.Show($"✓ Removed '{ingredientName}'!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadCurrentIngredients()
            Else
                MessageBox.Show("Could not delete ingredient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub SaveChanges_Click(sender As Object, e As EventArgs)
        Dim dgv As DataGridView = CType(Me.Controls.Find("dgvCurrentIngredients", True)(0), DataGridView)

        Try
            openConn()

            For Each row As DataGridViewRow In dgv.Rows
                If row.IsNewRow OrElse row.Tag Is Nothing Then Continue For

                Dim productIngredientId As Integer = Convert.ToInt32(row.Tag)
                Dim quantity As Decimal = Convert.ToDecimal(row.Cells("Quantity").Value)
                Dim unit As String = row.Cells("Unit").Value.ToString()

                Dim updateQuery As String = "UPDATE product_ingredients SET QuantityUsed = @quantity, UnitType = @unit, UpdatedDate = NOW() WHERE ProductIngredientID = @id AND ProductID = @pid"
                Dim cmd As New MySqlCommand(updateQuery, conn)
                cmd.Parameters.AddWithValue("@quantity", quantity)
                cmd.Parameters.AddWithValue("@unit", unit)
                cmd.Parameters.AddWithValue("@id", productIngredientId)
                cmd.Parameters.AddWithValue("@pid", currentProductId)
                cmd.ExecuteNonQuery()
            Next

            MessageBox.Show("✓ Changes saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Class IngredientItem
        Public Property Id As Integer
        Public Property Name As String

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

End Class