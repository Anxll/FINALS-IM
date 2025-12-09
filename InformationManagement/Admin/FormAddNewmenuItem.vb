Imports MySqlConnector
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Net

Public Class FormAddNewmenuItem

    ' ================================
    ' FIELDS
    ' ================================
    Private SelectedImageBytes As Byte() = Nothing
    Private SelectedImagePath As String = Nothing

    ' FOLDER + WEB PATH
    Private Const UPLOAD_DIR As String = "C:\xampp\htdocs\TrialWeb\TrialWorkIM\Tabeya\uploads\products\"
    Private Const WEB_URL As String = "http://localhost/TrialWeb/TrialWorkIM/Tabeya/uploads/products/"

    Private Sub FormAddNewmenuItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeForm()
        EnsureUploadDirectoryExists()
        SetAutoIncrementIfNeeded()
    End Sub

    ' ================================
    ' AUTO INCREMENT
    ' ================================
    Private Sub SetAutoIncrementIfNeeded()
        Try
            openConn()

            Dim maxIdSql As String = "SELECT IFNULL(MAX(ProductID), 0) FROM products"
            Dim maxIdCmd As New MySqlCommand(maxIdSql, conn)
            Dim maxId As Integer = Convert.ToInt32(maxIdCmd.ExecuteScalar())

            Dim targetAutoInc As Integer = Math.Max(maxId + 1, 98)

            Dim sql As String = $"ALTER TABLE products AUTO_INCREMENT = {targetAutoInc}"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Console.WriteLine("Auto increment setup: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' ================================
    ' DIRECTORY CREATION
    ' ================================
    Private Sub EnsureUploadDirectoryExists()
        Try
            If Not Directory.Exists(UPLOAD_DIR) Then
                Directory.CreateDirectory(UPLOAD_DIR)
            End If
        Catch ex As Exception
            MessageBox.Show("Error creating upload directory: " & ex.Message)
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

        SelectedImageBytes = Nothing
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
                SelectedImagePath = ofd.FileName
                SelectedImageBytes = File.ReadAllBytes(ofd.FileName)

                Using ms As New MemoryStream(SelectedImageBytes)
                    PictureBox1.Image = Image.FromStream(ms)
                End Using

            Catch ex As Exception
                MessageBox.Show("Image load error: " & ex.Message)
            End Try
        End If
    End Sub

    ' ================================
    ' SAVE IMAGE TO XAMPP
    ' ================================
    Private Function SaveImageToFileSystem(productId As String) As String
        If SelectedImageBytes Is Nothing Then Return Nothing

        Try
            Dim timestamp As String = DateTime.Now.ToString("yyyyMMddHHmmss")
            Dim ext As String = Path.GetExtension(SelectedImagePath)

            Dim filename As String = $"product_{productId}_{timestamp}{ext}"
            Dim fullPath As String = Path.Combine(UPLOAD_DIR, filename)

            File.WriteAllBytes(fullPath, SelectedImageBytes)

            Return WEB_URL & filename

        Catch ex As Exception
            MessageBox.Show("Image save error: " & ex.Message)
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

            If SelectedImageBytes IsNot Nothing Then
                savedImageUrl = SaveImageToFileSystem(insertedId)

                If savedImageUrl IsNot Nothing Then
                    Dim updateSql As String = "UPDATE products SET Image = @Image WHERE ProductID = @ProductID"
                    Dim updateCmd As New MySqlCommand(updateSql, conn)
                    updateCmd.Parameters.AddWithValue("@Image", savedImageUrl)
                    updateCmd.Parameters.AddWithValue("@ProductID", insertedId)
                    updateCmd.ExecuteNonQuery()

                    ' Load back into PictureBox
                    LoadProductImage(savedImageUrl)
                End If
            End If

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
        SelectedImageBytes = Nothing
        SelectedImagePath = Nothing
        ProductID.Text = "Auto-Generated"
        txtProductName.Focus()
    End Sub

End Class