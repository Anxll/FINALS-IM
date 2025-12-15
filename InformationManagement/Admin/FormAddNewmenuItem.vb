Imports MySqlConnector
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Net

Public Class FormAddNewmenuItem

    ' =======================================================
    ' CONFIGURATION: Match your MenuItems.vb settings
    ' =======================================================
    Private Const UPLOAD_FOLDER As String = "C:\xampp\htdocs\TrialWeb\TrialWorkIM\Tabeya\uploads\products\"
    Private Const WEB_BASE_PATH As String = "uploads/products/"

    ' Store the selected image file path
    Private SelectedImagePath As String = Nothing

    Private Sub FormAddNewmenuItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeForm()
        EnsureUploadFolderExists()
    End Sub

    ' =======================================================
    ' ENSURE UPLOAD FOLDER EXISTS
    ' =======================================================
    Private Sub EnsureUploadFolderExists()
        Try
            If Not Directory.Exists(UPLOAD_FOLDER) Then
                Directory.CreateDirectory(UPLOAD_FOLDER)
                MessageBox.Show("Upload folder created at: " & UPLOAD_FOLDER, "Info")
            End If
        Catch ex As Exception
            MessageBox.Show("Warning: Could not create upload folder." & vbCrLf & ex.Message, "Warning")
        End Try
    End Sub

    ' =======================================================
    ' INITIALIZE FORM
    ' =======================================================
    Private Sub InitializeForm()
        Availability.Items.Clear()
        Availability.Items.Add("Available")
        Availability.Items.Add("Unavailable")
        Availability.SelectedIndex = 0

        cmbCategory.Items.Clear()
        cmbCategory.Items.AddRange({
            "SPAGHETTI MEAL",
            "DESSERT",
            "DRINKS & BEVERAGES",
            "PLATTER",
            "RICE MEAL",
            "RICE",
            "Bilao",
            "Snacks"
        })

        cmbMealTime.Items.Clear()
        cmbMealTime.Items.AddRange({"All Day", "Breakfast", "Lunch", "Dinner"})
        cmbMealTime.SelectedIndex = 0

        numericPrice.DecimalPlaces = 2
        numericPrice.Maximum = 999999

        ProductID.Text = "Auto-Generated"
        ProductID.ReadOnly = True
        ProductID.BackColor = Color.LightGray

        DateTimePicker1.Value = DateTime.Now
        DateTimePicker1.Enabled = False

        OrderCount.Text = "0"
        OrderCount.ReadOnly = True
        OrderCount.BackColor = Color.LightGray

        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.Image = Nothing

        SelectedImagePath = Nothing
    End Sub

    ' ================================
    ' VALIDATION
    ' ================================
    Private Function ValidateForm() As Boolean
        If txtProductName.Text.Trim() = "" Then Return ShowError(txtProductName, "Enter product name.")
        If cmbCategory.SelectedIndex = -1 Then Return ShowError(cmbCategory, "Select category.")
        If Description.Text.Trim() = "" Then Return ShowError(Description, "Enter description.")
        If numericPrice.Value <= 0 Then Return ShowError(numericPrice, "Price must be greater than 0.")
        If ServingSize.Text.Trim() = "" Then Return ShowError(ServingSize, "Enter serving size.")
        If PrepTime.Text.Trim() = "" Then Return ShowError(PrepTime, "Enter prep time.")

        Return True
    End Function

    Private Function ShowError(ctrl As Control, msg As String) As Boolean
        MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ctrl.Focus()
        Return False
    End Function

    ' ================================
    ' IMAGE BROWSE
    ' ================================
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"

        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                ' Load image into PictureBox
                PictureBox1.Image = Image.FromFile(ofd.FileName)

                ' Store the selected file path
                SelectedImagePath = ofd.FileName

            Catch ex As Exception
                MessageBox.Show("Error loading image: " & ex.Message)
                SelectedImagePath = Nothing
            End Try
        End If
    End Sub

    ' =======================================================
    ' SAVE IMAGE TO UPLOAD FOLDER
    ' =======================================================
    Private Function SaveImageToFolder() As String
        If SelectedImagePath Is Nothing OrElse Not File.Exists(SelectedImagePath) Then
            Return Nothing
        End If

        Try
            ' Generate unique filename
            Dim timestamp As String = DateTime.Now.ToString("yyyyMMdd_HHmmss")
            Dim extension As String = Path.GetExtension(SelectedImagePath)
            Dim newFileName As String = $"product_{timestamp}_{Guid.NewGuid().ToString().Substring(0, 8)}{extension}"

            ' Full destination path
            Dim destinationPath As String = Path.Combine(UPLOAD_FOLDER, newFileName)

            ' Copy file to uploads folder
            File.Copy(SelectedImagePath, destinationPath, True)

            ' Return the relative web path (for database storage)
            Return WEB_BASE_PATH & newFileName

        Catch ex As Exception
            MessageBox.Show("Error saving image: " & ex.Message, "Error")
            Return Nothing
        End Try
    End Function

    ' ================================
    ' ADD BUTTON
    ' ================================
    Private Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click
        If Not ValidateForm() Then Exit Sub

        Dim savedImageUrl As String = Nothing

        Try
            openConn()

            Dim sql As String = "
                INSERT INTO products 
                (ProductName, Category, Description, Price, Availability, ServingSize, 
                 DateAdded, LastUpdated, ProductCode, OrderCount, PrepTime, MealTime)
                VALUES
                (@ProductName, @Category, @Description, @Price, @Availability, @ServingSize,
                 NOW(), NOW(), @ProductCode, 0, @PrepTime, @MealTime);
                SELECT LAST_INSERT_ID();"

            Dim cmd As New MySqlCommand(sql, conn)

            cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim())
            cmd.Parameters.AddWithValue("@Category", GetDatabaseCategory(cmbCategory.Text.Trim()))
            cmd.Parameters.AddWithValue("@Description", Description.Text.Trim())
            cmd.Parameters.AddWithValue("@Price", numericPrice.Value)
            cmd.Parameters.AddWithValue("@Availability", Availability.Text)
            cmd.Parameters.AddWithValue("@ServingSize", ServingSize.Text.Trim())
            cmd.Parameters.AddWithValue("@PrepTime", PrepTime.Text.Trim())
            cmd.Parameters.AddWithValue("@MealTime", cmbMealTime.Text)

            If ProductCode.Text.Trim() = "" Then
                cmd.Parameters.AddWithValue("@ProductCode", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@ProductCode", ProductCode.Text.Trim())
            End If

            Dim insertedId As String = cmd.ExecuteScalar().ToString()
            ProductID.Text = insertedId

            ' ===================== SAVE IMAGE AS FILE PATH =====================
            Dim imagePath As String = SaveImageToFolder()

            ' ===================== IMAGE SAVE LOGIC =====================
            ' Use stored image bytes if available
            If SelectedImageBytes IsNot Nothing Then
                cmd.Parameters.Add("@Image", MySqlDbType.LongBlob).Value = SelectedImageBytes
            Else
                cmd.Parameters.AddWithValue("@Image", DBNull.Value)
            End If
            ' ===================================================================

            If savedImageUrl IsNot Nothing Then
                Dim updateSql As String = "UPDATE products SET Image = @Image WHERE ProductID = @ProductID"
                Dim updateCmd As New MySqlCommand(updateSql, conn)
                updateCmd.Parameters.AddWithValue("@Image", savedImageUrl)
                updateCmd.Parameters.AddWithValue("@ProductID", insertedId)
                updateCmd.ExecuteNonQuery()

                MessageBox.Show("Menu item added successfully!", "Success")

                MessageBox.Show("Menu item saved! ID: " & insertedId)
                ClearForm()

        Catch ex As Exception
            MessageBox.Show("Save error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' ================================
    ' LOAD IMAGE BACK INTO PICTUREBOX
    ' ================================
    Private Sub LoadProductImage(imageUrl As String)
        Try
            Using wc As New WebClient()
                Dim imgBytes = wc.DownloadData(imageUrl)

                Using ms As New MemoryStream(imgBytes)
                    PictureBox1.Image = Image.FromStream(ms)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Image load failed: " & ex.Message)
        End Try
    End Sub

    ' ================================
    ' CATEGORY MAP
    ' ================================
    Private Function GetDatabaseCategory(displayCategory As String) As String
        If displayCategory = "Bilao" Then Return "NOODLES & PASTA"
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
        ServingSize.Text = ""
        ProductCode.Text = ""
        PrepTime.Text = ""
        cmbMealTime.SelectedIndex = 0
        PictureBox1.Image = Nothing
        SelectedImagePath = Nothing
        ProductID.Text = GenerateNextProductID()
        txtProductName.Focus()
    End Sub

End Class