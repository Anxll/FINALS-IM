Imports MySqlConnector
Imports System.IO
Imports System.Drawing.Imaging

Public Class FormEditMenu

    Public SelectedProductID As Integer
    Private SelectedImageBytes As Byte() = Nothing

    Private Sub FormEditMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeForm()
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
        cmbCategory.Items.Add("SPAGHETTI MEAL")
        cmbCategory.Items.Add("DESSERT")
        cmbCategory.Items.Add("DRINKS & BEVERAGES")
        cmbCategory.Items.Add("PLATTER")
        cmbCategory.Items.Add("RICE MEAL")
        cmbCategory.Items.Add("RICE")
        cmbCategory.Items.Add("Bilao")
        cmbCategory.Items.Add("Snacks")
        cmbCategory.SelectedIndex = -1

        cmbMealTime.Items.Clear()
        cmbMealTime.Items.Add("All Day")
        cmbMealTime.Items.Add("Breakfast")
        cmbMealTime.Items.Add("Lunch")
        cmbMealTime.Items.Add("Dinner")
        cmbMealTime.SelectedIndex = 0

        numericPrice.Value = 0
        numericPrice.DecimalPlaces = 2

        ProductID.ReadOnly = True
        OrderCount.ReadOnly = True

        PictureBox1.Image = Nothing
        SelectedImageBytes = Nothing
    End Sub

    ' =======================================================
    ' LOAD PRODUCT + IMAGE SAFELY
    ' =======================================================
    Public Sub LoadProduct(id As Integer)
        SelectedProductID = id
        Try
            openConn()
            Dim sql As String = "SELECT * FROM products WHERE ProductID = @id"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", id)
            Dim rd = cmd.ExecuteReader()

            If rd.Read() Then
                ProductID.Text = rd("ProductID").ToString()
                txtProductName.Text = rd("ProductName").ToString()
                Description.Text = rd("Description").ToString()
                numericPrice.Value = Convert.ToDecimal(rd("Price"))
                ServingSize.Text = rd("ServingSize").ToString()
                ProductCode.Text = rd("ProductCode").ToString()
                PrepTime.Text = rd("PrepTime").ToString()
                OrderCount.Text = rd("OrderCount").ToString()
                Availability.Text = rd("Availability").ToString()
                cmbCategory.Text = rd("Category").ToString()
                cmbMealTime.Text = rd("MealTime").ToString()

                ' ========== FIXED IMAGE LOADING ==========
                If Not IsDBNull(rd("Image")) Then
                    Dim imageData = rd("Image")

                    ' Check if it's actually a byte array
                    If TypeOf imageData Is Byte() Then
                        Dim bytes As Byte() = CType(imageData, Byte())

                        If bytes IsNot Nothing AndAlso bytes.Length > 100 Then
                            Try
                                Using ms As New MemoryStream(bytes)
                                    PictureBox1.Image = Image.FromStream(ms)
                                End Using
                                SelectedImageBytes = bytes
                            Catch ex As Exception
                                PictureBox1.Image = Nothing
                                SelectedImageBytes = Nothing
                            End Try
                        Else
                            PictureBox1.Image = Nothing
                            SelectedImageBytes = Nothing
                        End If
                    Else
                        ' If it's not a byte array (e.g., string path), clear the image
                        PictureBox1.Image = Nothing
                        SelectedImageBytes = Nothing
                    End If
                Else
                    PictureBox1.Image = Nothing
                    SelectedImageBytes = Nothing
                End If
            End If
            rd.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading product: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' =======================================================
    ' BROWSE IMAGE
    ' =======================================================
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"

        If ofd.ShowDialog() = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(ofd.FileName)
            SelectedImageBytes = File.ReadAllBytes(ofd.FileName)
        End If
    End Sub

    ' =======================================================
    ' UPDATE PRODUCT + IMAGE
    ' =======================================================
    Private Sub btnUpdateItem_Click(sender As Object, e As EventArgs) Handles btnUpdateItem.Click
        Try
            openConn()

            Dim sql As String =
                "UPDATE products SET 
                ProductName=@ProductName,
                Category=@Category,
                Description=@Description,
                Price=@Price,
                Availability=@Availability,
                ServingSize=@ServingSize,
                ProductCode=@ProductCode,
                PrepTime=@PrepTime,
                MealTime=@MealTime,
                LastUpdated=NOW(),
                Image=@Image
                WHERE ProductID=@id"

            Dim cmd As New MySqlCommand(sql, conn)

            cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim())
            cmd.Parameters.AddWithValue("@Category", cmbCategory.Text.Trim())
            cmd.Parameters.AddWithValue("@Description", Description.Text.Trim())
            cmd.Parameters.AddWithValue("@Price", numericPrice.Value)
            cmd.Parameters.AddWithValue("@Availability", Availability.Text)
            cmd.Parameters.AddWithValue("@ServingSize", ServingSize.Text.Trim())
            cmd.Parameters.AddWithValue("@PrepTime", PrepTime.Text.Trim())
            cmd.Parameters.AddWithValue("@MealTime", cmbMealTime.Text)
            cmd.Parameters.AddWithValue("@id", SelectedProductID)

            ' ============= FIXED IMAGE SAVING =============
            If SelectedImageBytes IsNot Nothing Then
                cmd.Parameters.Add("@Image", MySqlDbType.Blob).Value = SelectedImageBytes
            Else
                cmd.Parameters.Add("@Image", MySqlDbType.Blob).Value = DBNull.Value
            End If

            cmd.ExecuteNonQuery()

            MessageBox.Show("Menu item updated successfully!")
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error updating item: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class