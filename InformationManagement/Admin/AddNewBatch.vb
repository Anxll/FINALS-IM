Imports MySqlConnector
Public Class AddNewBatch
    Private ReadOnly _ingredientID As Integer
    Private ReadOnly _ingredientName As String
    Private ReadOnly _unitType As String
    ' Constructor to receive context from BatchManagement
    Public Sub New(ingredientID As Integer, ingredientName As String, unitType As String)
        ' This call is required by the designer.
        InitializeComponent()
        _ingredientID = ingredientID
        _ingredientName = ingredientName
        _unitType = unitType
    End Sub
    Private Sub AddNewBatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Basic form text/context
            Me.Text = "Add New Batch - " & _ingredientName
            ' If you have a label for the ingredient name, set it here
            If Me.Controls.ContainsKey("lblIngredientName") Then
                Dim lbl = TryCast(Me.Controls("lblIngredientName"), Label)
                If lbl IsNot Nothing Then
                    lbl.Text = _ingredientName
                End If
            End If
            ' Default values for controls if they exist
            If Me.Controls.ContainsKey("dtpPurchaseDate") Then
                Dim dtp = TryCast(Me.Controls("dtpPurchaseDate"), DateTimePicker)
                If dtp IsNot Nothing Then
                    dtp.Value = Date.Now
                End If
            End If
            If Me.Controls.ContainsKey("dtpExpirationDate") Then
                Dim dtp = TryCast(Me.Controls("dtpExpirationDate"), DateTimePicker)
                If dtp IsNot Nothing Then
                    dtp.Value = Date.Now.AddDays(30)
                End If
            End If
            ' If you have a unit textbox/combobox, pre-fill with the unit from BatchManagement
            If Not String.IsNullOrWhiteSpace(_unitType) Then
                If Me.Controls.ContainsKey("cmbUnit") Then
                    Dim cmb As ComboBox = TryCast(Me.Controls("cmbUnit"), ComboBox)
                    If cmb IsNot Nothing Then
                        If cmb.Items.Contains(_unitType) Then
                            cmb.SelectedItem = _unitType
                        Else
                            cmb.Items.Add(_unitType)
                            cmb.SelectedItem = _unitType
                        End If
                    End If
                End If
            End If
            ' Default storage location selection if available
            If Me.Controls.ContainsKey("cmbStorageLocation") Then
                Dim cmbStorage As ComboBox = TryCast(Me.Controls("cmbStorageLocation"), ComboBox)
                If cmbStorage IsNot Nothing AndAlso cmbStorage.Items.Count > 0 AndAlso cmbStorage.SelectedIndex = -1 Then
                    cmbStorage.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading Add New Batch form: " & ex.Message,
            "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ' Save / Add batch button click
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateInputs() Then
            AddBatchToDatabase()
        End If
    End Sub
    ' Cancel button
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    ' Validate user inputs
    Private Function ValidateInputs() As Boolean
        ' Quantity textbox name assumed: txtQuantity
        Dim quantityText As String = ""
        If Me.Controls.ContainsKey("txtQuantity") Then
            Dim txt As TextBox = TryCast(Me.Controls("txtQuantity"), TextBox)
            If txt IsNot Nothing Then
                quantityText = txt.Text.Trim()
            End If
        End If
        If String.IsNullOrWhiteSpace(quantityText) OrElse Not IsNumeric(quantityText) Then
            MessageBox.Show("Please enter a valid quantity.", "Validation Error",
            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            If Me.Controls.ContainsKey("txtQuantity") Then
                Me.Controls("txtQuantity").Focus()
            End If
            Return False
        End If
        If Convert.ToDecimal(quantityText) <= 0D Then
            MessageBox.Show("Quantity must be greater than zero.", "Validation Error",
            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            If Me.Controls.ContainsKey("txtQuantity") Then
                Me.Controls("txtQuantity").Focus()
            End If
            Return False
        End If
        ' Cost per unit textbox name assumed: txtCostPerUnit
        Dim costText As String = ""
        If Me.Controls.ContainsKey("txtCostPerUnit") Then
            Dim txt As TextBox = TryCast(Me.Controls("txtCostPerUnit"), TextBox)
            If txt IsNot Nothing Then
                costText = txt.Text.Trim()
            End If
        End If
        If String.IsNullOrWhiteSpace(costText) OrElse Not IsNumeric(costText) Then
            MessageBox.Show("Please enter a valid cost per unit.", "Validation Error",
            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            If Me.Controls.ContainsKey("txtCostPerUnit") Then
                Me.Controls("txtCostPerUnit").Focus()
            End If
            Return False
        End If
        If Convert.ToDecimal(costText) < 0D Then
            MessageBox.Show("Cost per unit cannot be negative.", "Validation Error",
            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            If Me.Controls.ContainsKey("txtCostPerUnit") Then
                Me.Controls("txtCostPerUnit").Focus()
            End If
            Return False
        End If
        ' Expiration date picker assumed: dtpExpirationDate
        Dim expiryDate As Date = Date.Now
        If Me.Controls.ContainsKey("dtpExpirationDate") Then
            Dim dtp = TryCast(Me.Controls("dtpExpirationDate"), DateTimePicker)
            If dtp IsNot Nothing Then
                expiryDate = dtp.Value.Date
            End If
        End If
        If expiryDate < Date.Now.Date Then
            Dim result As DialogResult = MessageBox.Show(
            "The expiration date is in the past. Are you sure you want to continue?",
            "Expired Batch Warning",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)
            If result = DialogResult.No Then
                If Me.Controls.ContainsKey("dtpExpirationDate") Then
                    Me.Controls("dtpExpirationDate").Focus()
                End If
                Return False
            End If
        End If
        ' Unit (text box or combo box)
        Dim unit As String = ""
        If Me.Controls.ContainsKey("cmbUnit") Then
            Dim cmb As ComboBox = TryCast(Me.Controls("cmbUnit"), ComboBox)
            If cmb IsNot Nothing Then
                If cmb.SelectedItem Is Nothing Then
                     MessageBox.Show("Please select a unit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                     Return False
                End If
            End If
        End If


        ' Storage location
        If Me.Controls.ContainsKey("cmbStorageLocation") Then
            Dim cmbStorage As ComboBox = TryCast(Me.Controls("cmbStorageLocation"), ComboBox)
            If cmbStorage IsNot Nothing AndAlso cmbStorage.SelectedIndex = -1 Then
                MessageBox.Show("Please select a storage location.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbStorage.Focus()
                Return False
            End If
        End If
        Return True
    End Function
    ' Add the batch using the same stored procedure pattern as AddNewItems
    Private Sub AddBatchToDatabase()
        Dim quantity As Decimal = 0D
        Dim costPerUnit As Decimal = 0D
        Dim expirationDate As Date = Date.Now
        Dim unit As String = ""
        Dim storageLocation As String = "Pantry-Dry-Goods"
        Dim notes As String = ""
        Dim purchaseDate As Date = Date.Now
        ' Safely read values again from controls
        If Me.Controls.ContainsKey("txtQuantity") Then
            Dim txt As TextBox = TryCast(Me.Controls("txtQuantity"), TextBox)
            If txt IsNot Nothing AndAlso IsNumeric(txt.Text) Then
                quantity = Convert.ToDecimal(txt.Text)
            End If
        End If
        If Me.Controls.ContainsKey("txtCostPerUnit") Then
            Dim txt As TextBox = TryCast(Me.Controls("txtCostPerUnit"), TextBox)
            If txt IsNot Nothing AndAlso IsNumeric(txt.Text) Then
                costPerUnit = Convert.ToDecimal(txt.Text)
            End If
        End If
        If Me.Controls.ContainsKey("dtpExpirationDate") Then
            Dim dtp = TryCast(Me.Controls("dtpExpirationDate"), DateTimePicker)
            If dtp IsNot Nothing Then
                expirationDate = dtp.Value.Date
            End If
        End If
        If Me.Controls.ContainsKey("dtpPurchaseDate") Then
            Dim dtp = TryCast(Me.Controls("dtpPurchaseDate"), DateTimePicker)
            If dtp IsNot Nothing Then
                purchaseDate = dtp.Value.Date
            End If
        End If
        If Me.Controls.ContainsKey("txtNotes") Then
            Dim txt As TextBox = TryCast(Me.Controls("txtNotes"), TextBox)
            If txt IsNot Nothing Then
                notes = txt.Text.Trim()
            End If
        End If
        If Me.Controls.ContainsKey("cmbUnit") Then
            Dim cmb As ComboBox = TryCast(Me.Controls("cmbUnit"), ComboBox)
            If cmb IsNot Nothing AndAlso cmb.SelectedItem IsNot Nothing Then
                unit = cmb.SelectedItem.ToString()
            End If
        Else
            unit = _unitType
        End If
        If Me.Controls.ContainsKey("cmbStorageLocation") Then
            Dim cmbStorage As ComboBox = TryCast(Me.Controls("cmbStorageLocation"), ComboBox)
            If cmbStorage IsNot Nothing AndAlso cmbStorage.SelectedItem IsNot Nothing Then
                storageLocation = cmbStorage.SelectedItem.ToString()
            End If
        End If
        Try
            openConn()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()
            Try
                Dim cmdBatch As New MySqlCommand("AddInventoryBatch", conn, transaction)
                cmdBatch.CommandType = CommandType.StoredProcedure
                cmdBatch.Parameters.AddWithValue("@p_ingredient_id", _ingredientID)
                cmdBatch.Parameters.AddWithValue("@p_quantity", quantity)
                cmdBatch.Parameters.AddWithValue("@p_unit_type", unit)
                cmdBatch.Parameters.AddWithValue("@p_cost_per_unit", costPerUnit)
                cmdBatch.Parameters.AddWithValue("@p_expiration_date", expirationDate)
                cmdBatch.Parameters.AddWithValue("@p_storage_location", storageLocation)
                Dim fullNotes As String = notes
                If String.IsNullOrWhiteSpace(fullNotes) Then
                    fullNotes = "Additional batch added on " & purchaseDate.ToString("yyyy-MM-dd")
                End If
                cmdBatch.Parameters.AddWithValue("@p_notes", fullNotes)
                ' Output parameters, same pattern as AddNewItems
                Dim paramBatchID As New MySqlParameter("@p_batch_id", MySqlDbType.Int32)
                paramBatchID.Direction = ParameterDirection.Output
                cmdBatch.Parameters.Add(paramBatchID)
                Dim paramBatchNumber As New MySqlParameter("@p_batch_number", MySqlDbType.VarChar, 50)
                paramBatchNumber.Direction = ParameterDirection.Output
                cmdBatch.Parameters.Add(paramBatchNumber)
                cmdBatch.ExecuteNonQuery()
                Dim batchID As Integer = Convert.ToInt32(paramBatchID.Value)
                Dim batchNumber As String = paramBatchNumber.Value.ToString()
                transaction.Commit()
                MessageBox.Show(
                "Batch added successfully!" & vbCrLf & vbCrLf &
                "Ingredient: " & _ingredientName & vbCrLf &
                "Batch #: " & batchNumber & vbCrLf &
                "Quantity: " & quantity.ToString("#,##0.##") & " " & unit & vbCrLf &
                "Total Cost: ?" & (quantity * costPerUnit).ToString("#,##0.00"),
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

                ' Log Activity
                ActivityLogger.LogUserActivity("New Batch Added", "Inventory", $"Added Batch {batchNumber} to Ingredient ID {_ingredientID} (Qty: {quantity})", "Admin Panel")

                Me.DialogResult = DialogResult.OK
                Me.Close()
            Catch ex As Exception
                transaction.Rollback()
                Throw
            End Try
        Catch ex As Exception
            MessageBox.Show("Error adding batch: " & ex.Message,
            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub
    ' Draw Border
    Private Sub Form_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Dim borderColor As Color = Color.LightGray
        Dim borderThickness As Integer = 1
        ControlPaint.DrawBorder(e.Graphics, Me.ClientRectangle,
                                borderColor, borderThickness, ButtonBorderStyle.Solid,
                                borderColor, borderThickness, ButtonBorderStyle.Solid,
                                borderColor, borderThickness, ButtonBorderStyle.Solid,
                                borderColor, borderThickness, ButtonBorderStyle.Solid)
    End Sub
End Class
