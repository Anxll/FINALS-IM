Imports MySqlConnector
Imports System.IO
Imports System.Net

Public Class FormCheckIngredients

    Private Const WEB_BASE_URL As String = "http://localhost/TrialWeb/TrialWorkIM/Tabeya/"

    ' Class-level controls for easy access
    Private flowPanel As FlowLayoutPanel
    Private cmbCategory As ComboBox
    Private txtSearch As TextBox
    Private isLoading As Boolean = False

    Private Sub FormCheckIngredients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeUI()
        LoadCategories()
        LoadProductCards()
    End Sub

    ' =======================================================
    ' INITIALIZE UI COMPONENTS
    ' =======================================================
    Private Sub InitializeUI()
        ' Form settings - CENTERED ON SCREEN
        Me.Text = "Product Ingredients Viewer"
        Me.Size = New Size(1400, 900)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(245, 245, 245)
        Me.WindowState = FormWindowState.Normal
        Me.DoubleBuffered = True ' Prevent flickering

        ' Main container panel
        Dim mainPanel As New Panel()
        mainPanel.Dock = DockStyle.Fill
        mainPanel.Padding = New Padding(20)
        Me.Controls.Add(mainPanel)

        ' Top panel for title and filters
        Dim topPanel As New Panel()
        topPanel.Dock = DockStyle.Top
        topPanel.Height = 120
        topPanel.BackColor = Color.White
        mainPanel.Controls.Add(topPanel)

        ' Title label
        Dim lblTitle As New Label()
        lblTitle.Text = "🍽️ Product Ingredients"
        lblTitle.Font = New Font("Segoe UI", 24, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(52, 73, 94)
        lblTitle.Location = New Point(20, 20)
        lblTitle.AutoSize = True
        topPanel.Controls.Add(lblTitle)

        ' Category filter label
        Dim lblCategory As New Label()
        lblCategory.Text = "Filter by Category:"
        lblCategory.Font = New Font("Segoe UI", 11, FontStyle.Regular)
        lblCategory.Location = New Point(20, 75)
        lblCategory.AutoSize = True
        topPanel.Controls.Add(lblCategory)

        ' Category ComboBox
        cmbCategory = New ComboBox()
        cmbCategory.Name = "cmbCategory"
        cmbCategory.Font = New Font("Segoe UI", 10)
        cmbCategory.Location = New Point(170, 72)
        cmbCategory.Size = New Size(250, 30)
        cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList
        AddHandler cmbCategory.SelectedIndexChanged, AddressOf Category_Changed
        topPanel.Controls.Add(cmbCategory)

        ' Search textbox
        txtSearch = New TextBox()
        txtSearch.Name = "txtSearch"
        txtSearch.Font = New Font("Segoe UI", 10)
        txtSearch.Location = New Point(440, 72)
        txtSearch.Size = New Size(300, 30)
        txtSearch.Text = "Search products..."
        txtSearch.ForeColor = Color.Gray
        AddHandler txtSearch.Enter, AddressOf SearchBox_Enter
        AddHandler txtSearch.Leave, AddressOf SearchBox_Leave
        AddHandler txtSearch.TextChanged, AddressOf SearchBox_Changed
        topPanel.Controls.Add(txtSearch)

        ' Refresh button (NEW)
        Dim btnRefresh As New Button()
        btnRefresh.Text = "🔄 Refresh"
        btnRefresh.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnRefresh.Location = New Point(760, 72)
        btnRefresh.Size = New Size(100, 30)
        btnRefresh.BackColor = Color.FromArgb(52, 152, 219)
        btnRefresh.ForeColor = Color.White
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.Cursor = Cursors.Hand
        AddHandler btnRefresh.Click, AddressOf RefreshButton_Click
        topPanel.Controls.Add(btnRefresh)

        ' Close button
        Dim btnClose As New Button()
        btnClose.Text = "✕ Close"
        btnClose.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnClose.Location = New Point(870, 72)
        btnClose.Size = New Size(100, 30)
        btnClose.BackColor = Color.FromArgb(231, 76, 60)
        btnClose.ForeColor = Color.White
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.Cursor = Cursors.Hand
        AddHandler btnClose.Click, Sub() Me.Close()
        topPanel.Controls.Add(btnClose)

        ' FlowLayoutPanel for product cards
        flowPanel = New FlowLayoutPanel()
        flowPanel.Name = "flowPanel"
        flowPanel.Dock = DockStyle.Fill
        flowPanel.AutoScroll = True
        flowPanel.Padding = New Padding(20, 10, 20, 10)
        flowPanel.WrapContents = True
        flowPanel.BackColor = Color.FromArgb(245, 245, 245)
        flowPanel.FlowDirection = FlowDirection.LeftToRight
        mainPanel.Controls.Add(flowPanel)

        ' Handle form resize
        AddHandler Me.Resize, AddressOf Form_Resize
    End Sub

    ' =======================================================
    ' REFRESH BUTTON CLICK
    ' =======================================================
    Private Sub RefreshButton_Click(sender As Object, e As EventArgs)
        Dim searchText As String = If(txtSearch IsNot Nothing AndAlso txtSearch.ForeColor = Color.Black, txtSearch.Text, "")
        LoadProductCards(searchText, cmbCategory.Text)
    End Sub

    ' =======================================================
    ' HANDLE FORM RESIZE FOR RESPONSIVE CARDS
    ' =======================================================
    Private Sub Form_Resize(sender As Object, e As EventArgs)
        If flowPanel IsNot Nothing AndAlso flowPanel.Width > 0 AndAlso Not isLoading Then
            Dim availableWidth As Integer = flowPanel.ClientSize.Width - 60
            Dim cardWidth As Integer = (availableWidth \ 3) - 20

            If cardWidth < 300 Then cardWidth = 300

            For Each ctrl As Control In flowPanel.Controls
                If TypeOf ctrl Is Panel Then
                    Dim card As Panel = CType(ctrl, Panel)
                    card.Width = cardWidth
                    For Each innerCtrl As Control In card.Controls
                        If TypeOf innerCtrl Is PictureBox Then
                            innerCtrl.Width = cardWidth
                        ElseIf TypeOf innerCtrl Is Label OrElse TypeOf innerCtrl Is Panel OrElse TypeOf innerCtrl Is Button Then
                            If innerCtrl.Width > 50 Then
                                innerCtrl.Width = cardWidth - 20
                            End If
                        End If
                    Next
                End If
            Next
        End If
    End Sub

    ' =======================================================
    ' LOAD CATEGORIES
    ' =======================================================
    Private Sub LoadCategories()
        If cmbCategory Is Nothing Then Return

        cmbCategory.Items.Clear()
        cmbCategory.Items.Add("All Categories")
        cmbCategory.Items.AddRange(New String() {
            "SPAGHETTI MEAL",
            "DESSERT",
            "DRINKS & BEVERAGES",
            "PLATTER",
            "RICE MEAL",
            "RICE",
            "Bilao",
            "Snacks"
        })
        cmbCategory.SelectedIndex = 0
    End Sub

    ' =======================================================
    ' LOAD PRODUCT CARDS - FIXED TO PREVENT GLITCHING
    ' =======================================================
    Private Sub LoadProductCards(Optional searchTerm As String = "", Optional categoryFilter As String = "All Categories")
        If flowPanel Is Nothing Then Return
        If isLoading Then Return ' Prevent multiple simultaneous loads

        isLoading = True
        Me.Cursor = Cursors.WaitCursor

        ' COMPLETELY HIDE THE PANEL DURING LOADING TO PREVENT GLITCHING
        flowPanel.Visible = False
        flowPanel.SuspendLayout()

        ' Clear all existing controls and dispose them properly
        For Each ctrl As Control In flowPanel.Controls.Cast(Of Control).ToList()
            flowPanel.Controls.Remove(ctrl)
            ctrl.Dispose()
        Next
        flowPanel.Controls.Clear()

        Dim query As String = "
            SELECT DISTINCT
                p.ProductID,
                p.ProductName,
                p.Category,
                p.Image
            FROM products p
            WHERE 1=1
        "

        If Not String.IsNullOrWhiteSpace(searchTerm) AndAlso searchTerm <> "Search products..." Then
            query &= " AND (p.ProductName LIKE @search OR p.ProductCode LIKE @search)"
        End If

        If categoryFilter <> "All Categories" Then
            query &= " AND p.Category = @category"
        End If

        query &= " ORDER BY p.ProductName ASC"

        Dim productList As New List(Of ProductData)

        Try
            openConn()
            Dim cmd As New MySqlCommand(query, conn)

            If Not String.IsNullOrWhiteSpace(searchTerm) AndAlso searchTerm <> "Search products..." Then
                cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
            End If

            If categoryFilter <> "All Categories" Then
                Dim actualCategory As String = If(categoryFilter = "Bilao", "NOODLES & PASTA", categoryFilter)
                cmd.Parameters.AddWithValue("@category", actualCategory)
            End If

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim productData As New ProductData()
                productData.ProductID = Convert.ToInt32(reader("ProductID"))
                productData.ProductName = reader("ProductName").ToString()
                productData.Category = reader("Category").ToString()
                productData.ImagePath = If(IsDBNull(reader("Image")), "", reader("Image").ToString())
                productList.Add(productData)
            End While

            reader.Close()
            conn.Close()

            Me.Text = $"Product Ingredients Viewer - {productList.Count} products found"

            If productList.Count = 0 Then
                Dim lblNoProducts As New Label()
                lblNoProducts.Text = "No products found. Please check your database or filters."
                lblNoProducts.Font = New Font("Segoe UI", 14, FontStyle.Bold)
                lblNoProducts.ForeColor = Color.Gray
                lblNoProducts.AutoSize = True
                lblNoProducts.Location = New Point(50, 50)
                flowPanel.Controls.Add(lblNoProducts)
            Else
                ' Calculate card width once
                Dim cardWidth As Integer = CalculateCardWidth()

                ' Create ALL cards at once WITHOUT DoEvents() to prevent glitching
                For Each productData In productList
                    Dim card As Panel = CreateProductCard(productData.ProductID, productData.ProductName, productData.Category, productData.ImagePath, cardWidth)
                    flowPanel.Controls.Add(card)
                Next
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading products: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            ' Resume layout and show panel AFTER all cards are added
            flowPanel.ResumeLayout(True)
            flowPanel.Visible = True
            Me.Cursor = Cursors.Default
            isLoading = False
        End Try
    End Sub

    ' =======================================================
    ' CALCULATE CARD WIDTH FOR 3-COLUMN LAYOUT
    ' =======================================================
    Private Function CalculateCardWidth() As Integer
        If flowPanel Is Nothing OrElse flowPanel.Width <= 0 Then
            Return 400
        End If

        Dim availableWidth As Integer = flowPanel.ClientSize.Width - 60
        Dim cardWidth As Integer = (availableWidth \ 3) - 20

        If cardWidth < 300 Then cardWidth = 300
        If cardWidth > 500 Then cardWidth = 500

        Return cardWidth
    End Function

    ' Helper class to store product data
    Private Class ProductData
        Public Property ProductID As Integer
        Public Property ProductName As String
        Public Property Category As String
        Public Property ImagePath As String
    End Class

    ' =======================================================
    ' CREATE PRODUCT CARD WITH IMAGE AND INGREDIENTS
    ' =======================================================
    Private Function CreateProductCard(productId As Integer, productName As String, category As String, imagePath As String, cardWidth As Integer) As Panel
        Dim card As New Panel()
        card.Size = New Size(cardWidth, 520)
        card.BackColor = Color.White
        card.Margin = New Padding(10)
        card.BorderStyle = BorderStyle.FixedSingle

        ' Image PictureBox
        Dim picBox As New PictureBox()
        picBox.Size = New Size(cardWidth, 200)
        picBox.Location = New Point(0, 0)
        picBox.SizeMode = PictureBoxSizeMode.Zoom
        picBox.BackColor = Color.FromArgb(240, 240, 240)

        ' Load image
        If Not String.IsNullOrEmpty(imagePath) Then
            Try
                Dim imageUrl As String = ConvertToWebUrl(imagePath)
                Dim webClient As New WebClient()
                Dim imageBytes() As Byte = webClient.DownloadData(imageUrl)
                Using ms As New MemoryStream(imageBytes)
                    picBox.Image = Image.FromStream(ms)
                End Using
            Catch ex As Exception
                picBox.BackColor = Color.FromArgb(220, 220, 220)
                Dim lblNoImg As New Label()
                lblNoImg.Text = "No Image"
                lblNoImg.AutoSize = False
                lblNoImg.Size = picBox.Size
                lblNoImg.TextAlign = ContentAlignment.MiddleCenter
                lblNoImg.ForeColor = Color.Gray
                lblNoImg.Font = New Font("Segoe UI", 12)
                picBox.Controls.Add(lblNoImg)
            End Try
        End If
        card.Controls.Add(picBox)

        ' Product name label
        Dim lblName As New Label()
        lblName.Text = productName
        lblName.Font = New Font("Segoe UI", 13, FontStyle.Bold)
        lblName.Location = New Point(10, 210)
        lblName.Size = New Size(cardWidth - 20, 50)
        lblName.ForeColor = Color.FromArgb(52, 73, 94)
        lblName.AutoEllipsis = True
        card.Controls.Add(lblName)

        ' Category label
        Dim lblCategory As New Label()
        lblCategory.Text = "📂 " & category
        lblCategory.Font = New Font("Segoe UI", 9, FontStyle.Italic)
        lblCategory.Location = New Point(10, 265)
        lblCategory.Size = New Size(cardWidth - 20, 20)
        lblCategory.ForeColor = Color.FromArgb(127, 140, 141)
        card.Controls.Add(lblCategory)

        ' Ingredients header
        Dim lblIngredientsHeader As New Label()
        lblIngredientsHeader.Text = "🥘 Ingredients:"
        lblIngredientsHeader.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        lblIngredientsHeader.Location = New Point(10, 290)
        lblIngredientsHeader.Size = New Size(cardWidth - 20, 25)
        lblIngredientsHeader.ForeColor = Color.FromArgb(41, 128, 185)
        card.Controls.Add(lblIngredientsHeader)

        ' Ingredients list (scrollable)
        Dim ingredientsPanel As New Panel()
        ingredientsPanel.Location = New Point(10, 320)
        ingredientsPanel.Size = New Size(cardWidth - 20, 145)
        ingredientsPanel.AutoScroll = True
        ingredientsPanel.BorderStyle = BorderStyle.FixedSingle
        ingredientsPanel.BackColor = Color.FromArgb(250, 250, 250)

        ' Load ingredients
        Dim ingredientsList As String = GetProductIngredients(productId)

        Dim lblIngredients As New Label()
        lblIngredients.Text = ingredientsList
        lblIngredients.Font = New Font("Segoe UI", 9)
        lblIngredients.Location = New Point(5, 5)
        lblIngredients.Size = New Size(cardWidth - 45, 1000)
        lblIngredients.AutoSize = True
        lblIngredients.ForeColor = If(ingredientsList.Contains("Error"), Color.Red, Color.FromArgb(52, 73, 94))
        ingredientsPanel.Controls.Add(lblIngredients)

        card.Controls.Add(ingredientsPanel)

        ' Edit button
        Dim btnEdit As New Button()
        btnEdit.Text = "✏️ Edit Ingredients"
        btnEdit.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btnEdit.Location = New Point(10, 475)
        btnEdit.Size = New Size(cardWidth - 20, 35)
        btnEdit.BackColor = Color.FromArgb(52, 152, 219)
        btnEdit.ForeColor = Color.White
        btnEdit.FlatStyle = FlatStyle.Flat
        btnEdit.FlatAppearance.BorderSize = 0
        btnEdit.Cursor = Cursors.Hand
        btnEdit.Tag = productId
        AddHandler btnEdit.Click, AddressOf EditIngredients_Click
        card.Controls.Add(btnEdit)

        Return card
    End Function

    ' =======================================================
    ' GET PRODUCT INGREDIENTS FROM DATABASE
    ' =======================================================
    Private Function GetProductIngredients(productId As Integer) As String
        Dim ingredientsList As New System.Text.StringBuilder()

        Try
            If conn Is Nothing Then
                Return "Error: Database connection not initialized"
            End If

            If conn.State = ConnectionState.Closed Then
                openConn()
            End If

            Dim query As String = "
                SELECT 
                    i.IngredientName,
                    pi.QuantityUsed,
                    pi.UnitType
                FROM product_ingredients pi
                INNER JOIN ingredients i ON pi.IngredientID = i.IngredientID
                WHERE pi.ProductID = @productId
                ORDER BY i.IngredientName ASC
            "

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@productId", productId)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If Not reader.HasRows Then
                ingredientsList.AppendLine("No ingredients listed yet")
            Else
                While reader.Read()
                    Dim name As String = If(IsDBNull(reader("IngredientName")), "Unknown", reader("IngredientName").ToString())
                    Dim quantity As Decimal = If(IsDBNull(reader("QuantityUsed")), 0, Convert.ToDecimal(reader("QuantityUsed")))
                    Dim unit As String = If(IsDBNull(reader("UnitType")), "", reader("UnitType").ToString())
                    ingredientsList.AppendLine($"• {name} - {quantity} {unit}")
                End While
            End If

            reader.Close()

        Catch ex As Exception
            ingredientsList.Clear()
            ingredientsList.AppendLine($"Error loading ingredients: {ex.Message}")
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

        Return ingredientsList.ToString()
    End Function

    ' =======================================================
    ' CONVERT FILE PATH TO WEB URL
    ' =======================================================
    Private Function ConvertToWebUrl(imagePath As String) As String
        If imagePath.StartsWith("http://") OrElse imagePath.StartsWith("https://") Then
            Return imagePath
        End If

        If imagePath.Contains(":\") AndAlso imagePath.ToLower().Contains("htdocs") Then
            Dim htdocsIndex As Integer = imagePath.ToLower().IndexOf("htdocs")
            If htdocsIndex > 0 Then
                Dim webPath As String = imagePath.Substring(htdocsIndex + 7)
                webPath = webPath.Replace("\", "/")
                Return "http://localhost/" & webPath
            End If
        End If

        Dim cleanPath As String = imagePath.Replace("\", "/")
        If cleanPath.StartsWith("/") Then
            cleanPath = cleanPath.Substring(1)
        End If

        Return WEB_BASE_URL & cleanPath
    End Function

    ' =======================================================
    ' EVENT HANDLERS
    ' =======================================================
    Private Sub Category_Changed(sender As Object, e As EventArgs)
        Dim searchText As String = If(txtSearch IsNot Nothing AndAlso txtSearch.ForeColor = Color.Black, txtSearch.Text, "")
        LoadProductCards(searchText, cmbCategory.Text)
    End Sub

    Private Sub SearchBox_Enter(sender As Object, e As EventArgs)
        If txtSearch.Text = "Search products..." Then
            txtSearch.Text = ""
            txtSearch.ForeColor = Color.Black
        End If
    End Sub

    Private Sub SearchBox_Leave(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(txtSearch.Text) Then
            txtSearch.Text = "Search products..."
            txtSearch.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub SearchBox_Changed(sender As Object, e As EventArgs)
        If txtSearch.ForeColor = Color.Black Then
            LoadProductCards(txtSearch.Text, cmbCategory.Text)
        End If
    End Sub

    Private Sub EditIngredients_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim productId As Integer = CInt(btn.Tag)

        Try
            Dim editForm As New FormEditIngredients()
            editForm.LoadProductData(productId)
            If editForm.ShowDialog() = DialogResult.OK Then
                Dim searchText As String = If(txtSearch IsNot Nothing AndAlso txtSearch.ForeColor = Color.Black, txtSearch.Text, "")
                LoadProductCards(searchText, cmbCategory.Text)
            End If
        Catch ex As Exception
            MessageBox.Show("Error opening edit form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class