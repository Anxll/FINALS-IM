Imports MySqlConnector
Imports System.IO
Imports System.Drawing.Imaging

Public Class FormAddNewmenuItem

    ' ========== VARIABLE DECLARATIONS ==========
    Private Const UPLOAD_FOLDER As String = "C:\xampp\htdocs\TrialWeb\TrialWorkIM\Tabeya\uploads\products\"
    Private Const WEB_BASE_PATH As String = "uploads/products/"
    Private Const UPLOAD_DIR As String = "C:\xampp\htdocs\TrialWeb\TrialWorkIM\Tabeya\uploads\products\"
    Private Const WEB_URL As String = "http://localhost/TrialWeb/TrialWorkIM/Tabeya/uploads/products/"

    Private SelectedImagePath As String = Nothing
    Private SelectedImageBytes As Byte() = Nothing

    Private Sub FormAddNewmenuItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeForm()
        EnsureUploadFolderExists()
    End Sub

    ' ================================
    ' ENSURE UPLOAD FOLDER EXISTS
    ' ================================
    Private Sub EnsureUploadFolderExists()
        Try
            If Not Directory.Exists(UPLOAD_FOLDER) Then
                Directory.CreateDirectory(UPLOAD_FOLDER)
                MessageBox.Show("Upload folder created at: " & UPLOAD_FOLDER, "Info",
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Warning: Could not create upload folder." & vbCrLf & ex.Message,
                          "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ' ================================
    ' FORM INIT
    ' ================================
    Private Sub InitializeForm()
        Availability.Items.Clear()
        Availability.Items.Add("Available")
        Availability.Items.Add("Unavailable")
        Availability.SelectedIndex = 0

        cmbCategory.Items.Clear()
        cmbCategory.Items.AddRange({"SPAGHETTI MEAL", "DESSERT", "DRINKS & BEVERAGES", "PLATTER", "RICE MEAL", "RICE", "Bilao", "Snacks"})
        cmbCategory.SelectedIndex = -1

        cmbMealTime.Items.Clear()
        cmbMealTime.Items.AddRange({"All Day", "Breakfast", "Lunch", "Dinner"})
        cmbMealTime.SelectedIndex = 0

        numericPrice.Value = 0
        numericPrice.DecimalPlaces = 2
        numericPrice.Maximum = 999999

        ProductID.Text = GenerateNextProductID()
        ProductID.ReadOnly = True
        ProductID.BackColor = Color.LightGray

        DateTimePicker1.Value = DateTime.Now
        DateTimePicker1.Enabled = False

        OrderCount.Text = "0"
        OrderCount.ReadOnly = True
        OrderCount.BackColor = Color.LightGray

        PictureBox1.Image = Nothing
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

        SelectedImagePath = Nothing
        SelectedImageBytes = Nothing
    End Sub

    ' ================================
    ' GENERATE NEXT PRODUCT ID
    ' ================================
    Private Function GenerateNextProductID() As String
        Try
            openConn()
            Dim query As String = "SELECT COALESCE(MAX(ProductID), 0) + 1 AS NextID FROM products"
            Dim cmd As New MySqlCommand(query, conn)
            Return cmd.ExecuteScalar().ToString()
        Catch ex As Exception
            Return "1"
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Function

    ' ================================
    ' VALIDATE FORM
    ' ================================
    Private Function ValidateForm() As Boolean
        If txtProductName.Text.Trim() = "" Then
            Return ShowError(txtProductName, "Please enter a product name.")
        End If

        If cmbCategory.SelectedIndex = -1 Then
            Return ShowError(cmbCategory, "Please select a category.")
        End If

        If Description.Text.Trim() = "" Then
            Return ShowError(Description, "Please enter a description.")
        End If

        If numericPrice.Value <= 0 Then
            Return ShowError(numericPrice, "Price must be greater than 0.")
        End If

        If Availability.SelectedIndex = -1 Then
            Return ShowError(Availability, "Please select availability.")
        End If

        If ServingSize.Text.Trim() = "" Then
            Return ShowError(ServingSize, "Please enter serving size.")
        End If

        If PrepTime.Text.Trim() = "" Then
            Return ShowError(PrepTime, "Please enter preparation time.")
        End If

        If cmbMealTime.SelectedIndex = -1 Then
            Return ShowError(cmbMealTime, "Please select meal time.")
        End If

        Return True
    End Function

    Private Function ShowError(ctrl As Control, msg As String) As Boolean
        MessageBox.Show(msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ctrl.Focus()
        Return False
    End Function

    ' ================================
    ' BROWSE AND LOAD IMAGE
    ' ================================
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        BrowseAndLoadImage()
    End Sub

    Private Sub BrowseAndLoadImage()
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        ofd.Title = "Select Product Image"

        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                SelectedImagePath = ofd.FileName
                SelectedImageBytes = File.ReadAllBytes(ofd.FileName)

                Using ms As New MemoryStream(SelectedImageBytes)
                    PictureBox1.Image = Image.FromStream(ms)
                End Using

                MessageBox.Show("✓ Image selected successfully!",
                              "Image Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show("Error loading image: " & ex.Message,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                SelectedImagePath = Nothing
                SelectedImageBytes = Nothing
            End Try
        End If
    End Sub

    ' ================================
    ' SAVE IMAGE TO FILE SYSTEM
    ' ================================
    Private Function SaveImageToFileSystem(productId As String) As String
        If SelectedImageBytes Is Nothing Then Return Nothing

        Try
            Dim timestamp As String = DateTime.Now.ToString("yyyyMMddHHmmss")
            Dim ext As String = Path.GetExtension(SelectedImagePath)

            Dim filename As String = $"product_{productId}_{timestamp}{ext}"
            Dim fullPath As String = Path.Combine(UPLOAD_DIR, filename)

            File.WriteAllBytes(fullPath, SelectedImageBytes)

            ' Return relative path for database
            Return WEB_BASE_PATH & filename

        Catch ex As Exception
            MessageBox.Show("Error saving image: " & ex.Message, "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    ' ================================
    ' SAVE IMAGE TO FOLDER (ALTERNATIVE METHOD)
    ' ================================
    Private Function SaveImageToFolder() As String
        If SelectedImageBytes Is Nothing Then Return Nothing

        Try
            Dim timestamp As String = DateTime.Now.ToString("yyyyMMddHHmmss")
            Dim ext As String = If(String.IsNullOrEmpty(SelectedImagePath), ".jpg", Path.GetExtension(SelectedImagePath))

            Dim filename As String = $"product_{timestamp}{ext}"
            Dim fullPath As String = Path.Combine(UPLOAD_FOLDER, filename)

            File.WriteAllBytes(fullPath, SelectedImageBytes)

            Return WEB_BASE_PATH & filename

        Catch ex As Exception
            MessageBox.Show("Error saving image: " & ex.Message, "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    ' ================================
    ' LOAD PRODUCT IMAGE FROM URL
    ' ================================
    Private Sub LoadProductImage(imagePath As String)
        Try
            If String.IsNullOrEmpty(imagePath) Then
                PictureBox1.Image = Nothing
                Return
            End If

            ' Construct full URL
            Dim fullUrl As String = "http://localhost/TrialWeb/TrialWorkIM/Tabeya/" & imagePath
            Dim webClient As New System.Net.WebClient()
            Dim imageBytes() As Byte = webClient.DownloadData(fullUrl)

            Using ms As New MemoryStream(imageBytes)
                PictureBox1.Image = Image.FromStream(ms)
            End Using

        Catch ex As Exception
            Console.WriteLine($"Failed to load image: {ex.Message}")
            PictureBox1.Image = Nothing
        End Try
    End Sub

    ' ================================
    ' ADD ITEM BUTTON CLICK
    ' ================================
    Private Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click

        If Not ValidateForm() Then
            Exit Sub
        End If

        Try
            openConn()

            ' Insert product without image first
            Dim sql As String = "INSERT INTO products " &
                "(ProductName, Category, Description, Price, Availability, ServingSize, " &
                "DateAdded, LastUpdated, ProductCode, OrderCount, PrepTime, MealTime) " &
                "VALUES " &
                "(@ProductName, @Category, @Description, @Price, @Availability, @ServingSize, " &
                "NOW(), NOW(), @ProductCode, 0, @PrepTime, @MealTime)"

            Dim cmd As New MySqlCommand(sql, conn)

            cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim())
            cmd.Parameters.AddWithValue("@Category", GetDatabaseCategory(cmbCategory.Text.Trim()))
            cmd.Parameters.AddWithValue("@Description", Description.Text.Trim())
            cmd.Parameters.AddWithValue("@Price", numericPrice.Value)
            cmd.Parameters.AddWithValue("@Availability", Availability.Text)
            cmd.Parameters.AddWithValue("@ServingSize", ServingSize.Text.Trim())

            If ProductCode.Text.Trim() = "" Then
                cmd.Parameters.AddWithValue("@ProductCode", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@ProductCode", ProductCode.Text.Trim())
            End If

            cmd.Parameters.AddWithValue("@PrepTime", PrepTime.Text.Trim())
            cmd.Parameters.AddWithValue("@MealTime", cmbMealTime.Text)

            cmd.ExecuteNonQuery()

            ' Get the inserted product ID
            Dim insertedId As Long = cmd.LastInsertedId

            ' Save image if provided
            Dim savedImageUrl As String = Nothing
            If SelectedImageBytes IsNot Nothing Then
                savedImageUrl = SaveImageToFileSystem(insertedId.ToString())

                If savedImageUrl IsNot Nothing Then
                    ' Update product with image path
                    Dim updateSql As String = "UPDATE products SET Image = @Image WHERE ProductID = @ProductID"
                    Dim updateCmd As New MySqlCommand(updateSql, conn)
                    updateCmd.Parameters.AddWithValue("@Image", savedImageUrl)
                    updateCmd.Parameters.AddWithValue("@ProductID", insertedId)
                    updateCmd.ExecuteNonQuery()

                    ' Load back into PictureBox
                    LoadProductImage(savedImageUrl)
                End If
            End If

            MessageBox.Show("✓ Menu item saved successfully!" & vbCrLf & vbCrLf &
                          "Product: " & txtProductName.Text & vbCrLf &
                          "Product ID: " & insertedId,
                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Clear form for next entry
            ClearForm()

            ' Set DialogResult so parent form can refresh
            Me.DialogResult = DialogResult.OK

        Catch ex As Exception
            MessageBox.Show("❌ Error adding item: " & ex.Message & vbCrLf & vbCrLf &
                          "Stack Trace: " & ex.StackTrace,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    ' ================================
    ' GET DATABASE CATEGORY
    ' ================================
    Private Function GetDatabaseCategory(displayCategory As String) As String
        If displayCategory = "Bilao" Then
            Return "NOODLES & PASTA"
        End If
        Return displayCategory
    End Function

    ' ================================
    ' CLEAR FORM
    ' ================================
    Private Sub ClearForm()
        txtProductName.Text = ""
        cmbCategory.SelectedIndex = -1
        Description.Text = ""
        numericPrice.Value = 0
        Availability.SelectedIndex = 0
        ServingSize.Text = ""
        ProductCode.Text = ""
        PrepTime.Text = ""
        cmbMealTime.SelectedIndex = 0
        PictureBox1.Image = Nothing
        SelectedImageBytes = Nothing
        SelectedImagePath = Nothing
        ProductID.Text = GenerateNextProductID()
        txtProductName.Focus()
    End Sub

    ' ================================
    ' CLOSE BUTTON
    ' ================================
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Dim result As DialogResult = MessageBox.Show(
            "Are you sure you want to close without saving?",
            "Confirm Close",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        )

        If result = DialogResult.Yes Then
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    ' ================================
    ' FORM CLOSING EVENT
    ' ================================
    Private Sub FormAddNewmenuItem_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If PictureBox1 IsNot Nothing AndAlso PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
            PictureBox1.Image = Nothing
        End If
    End Sub

End Class