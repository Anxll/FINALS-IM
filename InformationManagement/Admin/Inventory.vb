Imports MySqlConnector

Public Class Inventory

    Private Sub Inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


            ' Set form to maximized
            Me.WindowState = FormWindowState.Maximized

            ' Apply responsive layout
            ApplyResponsiveLayout()

            ' Load categories dropdown
            LoadCategories()

            ' Load data
            LoadInventorySummary()
            LoadInventoryStatistics()

        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message,
                          "Load Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        End Try
    End Sub

    ' Load categories into dropdown
    Private Sub LoadCategories()
        Try
            openConn()

            Dim sql As String = "SELECT CategoryName FROM ingredient_categories ORDER BY CategoryName"
            Dim cmd As New MySqlCommand(sql, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Category.Items.Clear()
            Category.Items.Add("All Categories")

            While reader.Read()
                Category.Items.Add(reader("CategoryName").ToString())
            End While

            reader.Close()
            Category.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show("Error loading categories: " & ex.Message,
                          "Database Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    ' Handle form resize
    Private Sub Inventory_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        ApplyResponsiveLayout()
    End Sub

    ' Apply responsive layout based on screen size
    Private Sub ApplyResponsiveLayout()
        Try
            Dim formWidth As Integer = Me.ClientSize.Width
            Dim formHeight As Integer = Me.ClientSize.Height

            ' Calculate margins and spacing
            Dim leftMargin As Integer = CInt(formWidth * 0.03) ' 3% margin
            Dim topMargin As Integer = 120
            Dim spacing As Integer = CInt(formWidth * 0.015) ' 1.5% spacing

            ' Position and size for header section
            If Me.Controls.Contains(Splitter1) Then
                Splitter1.Height = 105
            End If

            ' Position summary cards
            Dim cardWidth As Integer = CInt((formWidth - (leftMargin * 2) - spacing) / 2)
            Dim cardHeight As Integer = 170

            ' Total Items card
            If Me.Controls.Contains(RoundedPane21) Then
                RoundedPane21.Location = New Point(leftMargin, topMargin)
                RoundedPane21.Size = New Size(cardWidth, cardHeight)
            End If

            ' Total Value card
            If Me.Controls.Contains(RoundedPane22) Then
                RoundedPane22.Location = New Point(leftMargin + cardWidth + spacing, topMargin)
                RoundedPane22.Size = New Size(cardWidth, cardHeight)
            End If

            ' Search and filter section
            Dim searchTop As Integer = topMargin + cardHeight + 40

            ' Search label
            If Me.Controls.Contains(Label6) Then
                Label6.Location = New Point(leftMargin, searchTop)
            End If

            ' Search textbox - takes 60% width
            Dim searchWidth As Integer = CInt((formWidth - (leftMargin * 2) - spacing) * 0.6)
            If Me.Controls.Contains(TextBox1) Then
                TextBox1.Location = New Point(leftMargin, searchTop + 25)
                TextBox1.Size = New Size(searchWidth, 22)
            End If

            ' Category label
            If Me.Controls.Contains(Label7) Then
                Label7.Location = New Point(leftMargin + searchWidth + spacing, searchTop)
            End If

            ' Category dropdown
            Dim categoryWidth As Integer = formWidth - (leftMargin * 2) - searchWidth - spacing
            If Me.Controls.Contains(Category) Then
                Category.Location = New Point(leftMargin + searchWidth + spacing, searchTop + 25)
                Category.Size = New Size(categoryWidth, 24)
            End If

            ' Add Item button
            If Me.Controls.Contains(AddItem) Then
                AddItem.Location = New Point(formWidth - leftMargin - 165, 27)
                AddItem.Size = New Size(165, 56)
            End If

            ' DataGrid
            Dim gridTop As Integer = searchTop + 60
            Dim gridHeight As Integer = formHeight - gridTop - 30

            InventoryGrid.Location = New Point(leftMargin, gridTop)
            InventoryGrid.Size = New Size(formWidth - (leftMargin * 2), gridHeight)

            ' Adjust DataGrid columns
            AdjustGridColumns()

        Catch ex As Exception
            ' Silent fail during resize
        End Try
    End Sub

    ' Adjust DataGrid column widths
    Private Sub AdjustGridColumns()
        Try
            If InventoryGrid.Columns.Count > 0 AndAlso InventoryGrid.Width > 0 Then
                Dim totalWidth As Integer = InventoryGrid.Width - 40

                If InventoryGrid.Columns.Contains("Item Name") Then
                    InventoryGrid.Columns("Item Name").Width = CInt(totalWidth * 0.18)
                End If

                If InventoryGrid.Columns.Contains("Category") Then
                    InventoryGrid.Columns("Category").Width = CInt(totalWidth * 0.12)
                End If

                If InventoryGrid.Columns.Contains("Total Quantity") Then
                    InventoryGrid.Columns("Total Quantity").Width = CInt(totalWidth * 0.1)
                End If

                If InventoryGrid.Columns.Contains("Unit") Then
                    InventoryGrid.Columns("Unit").Width = CInt(totalWidth * 0.08)
                End If

                If InventoryGrid.Columns.Contains("Status") Then
                    InventoryGrid.Columns("Status").Width = CInt(totalWidth * 0.1)
                End If

                If InventoryGrid.Columns.Contains("Active Batches") Then
                    InventoryGrid.Columns("Active Batches").Width = CInt(totalWidth * 0.1)
                End If

                If InventoryGrid.Columns.Contains("Next Expiration") Then
                    InventoryGrid.Columns("Next Expiration").Width = CInt(totalWidth * 0.12)
                End If

                If InventoryGrid.Columns.Contains("Total Value") Then
                    InventoryGrid.Columns("Total Value").Width = CInt(totalWidth * 0.12)
                End If

                If InventoryGrid.Columns.Contains("ViewBatches") Then
                    InventoryGrid.Columns("ViewBatches").Width = 120
                End If
            End If
        Catch ex As Exception
            ' Silent fail
        End Try
    End Sub

    ' Load main inventory grid
    Private Sub LoadInventorySummary()
        Try
            openConn()

            Dim sql As String = "
                SELECT 
                    i.IngredientID AS 'Ingredient ID',
                    i.IngredientName AS 'Item Name',
                    COALESCE(ic.CategoryName, 'Uncategorized') AS 'Category',
                    COALESCE(SUM(ib.StockQuantity), 0) AS 'Total Quantity',
                    i.UnitType AS 'Unit',
                    CASE 
                        WHEN COALESCE(SUM(ib.StockQuantity), 0) = 0 THEN 'Out of Stock'
                        WHEN COALESCE(SUM(ib.StockQuantity), 0) < i.MinStockLevel THEN 'Low Stock'
                        WHEN COALESCE(SUM(ib.StockQuantity), 0) > i.MaxStockLevel THEN 'Overstocked'
                        ELSE 'In Stock'
                    END AS 'Status',
                    COUNT(CASE WHEN ib.BatchStatus = 'Active' THEN 1 END) AS 'Active Batches',
                    MIN(ib.ExpirationDate) AS 'Next Expiration',
                    COALESCE(SUM(ib.StockQuantity * ib.CostPerUnit), 0) AS 'Total Value',
                    i.MinStockLevel AS 'Min Level',
                    i.MaxStockLevel AS 'Max Level'
                FROM ingredients i
                LEFT JOIN ingredient_categories ic ON i.CategoryID = ic.CategoryID
                LEFT JOIN inventory_batches ib ON i.IngredientID = ib.IngredientID 
                    AND ib.BatchStatus = 'Active'
                WHERE i.IsActive = 1
                GROUP BY i.IngredientID, i.IngredientName, ic.CategoryName, 
                         i.UnitType, i.MinStockLevel, i.MaxStockLevel
                ORDER BY i.IngredientName
            "

            Dim cmd As New MySqlCommand(sql, conn)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            ' Set datasource
            InventoryGrid.DataSource = Nothing
            InventoryGrid.Columns.Clear()
            InventoryGrid.DataSource = dt

            ' Hide ID column
            If InventoryGrid.Columns.Contains("Ingredient ID") Then
                InventoryGrid.Columns("Ingredient ID").Visible = False
            End If

            ' Format grid
            FormatInventoryGrid()
            ColorCodeStatusColumn()

        Catch ex As Exception
            MessageBox.Show("Error loading inventory: " & ex.Message,
                          "Database Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    ' Format the grid
    Private Sub FormatInventoryGrid()
        Try
            With InventoryGrid
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                .RowTemplate.Height = 35
                .DefaultCellStyle.Font = New Font("Segoe UI", 9)
                .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250)
                .ReadOnly = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect

                ' Format columns
                If .Columns.Contains("Total Value") Then
                    .Columns("Total Value").DefaultCellStyle.Format = "₱#,##0.00"
                    .Columns("Total Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns("Total Value").ReadOnly = True
                End If

                If .Columns.Contains("Total Quantity") Then
                    .Columns("Total Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Total Quantity").ReadOnly = True
                End If

                If .Columns.Contains("Active Batches") Then
                    .Columns("Active Batches").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Active Batches").ReadOnly = True
                End If

                If .Columns.Contains("Next Expiration") Then
                    .Columns("Next Expiration").DefaultCellStyle.Format = "MMM dd, yyyy"
                    .Columns("Next Expiration").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Next Expiration").ReadOnly = True
                End If

                If .Columns.Contains("Status") Then
                    .Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Status").DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    .Columns("Status").ReadOnly = True
                End If

                ' Make other columns read-only
                For Each col As DataGridViewColumn In .Columns
                    If col.Name <> "ViewBatches" Then
                        col.ReadOnly = True
                    End If
                Next
            End With

            ' Add View Batches button
            If Not InventoryGrid.Columns.Contains("ViewBatches") Then
                Dim btnView As New DataGridViewButtonColumn()
                btnView.Name = "ViewBatches"
                btnView.HeaderText = "Actions"
                btnView.Text = "View Batches"
                btnView.UseColumnTextForButtonValue = True
                btnView.Width = 120
                btnView.FlatStyle = FlatStyle.Flat
                InventoryGrid.Columns.Add(btnView)
            End If

            AdjustGridColumns()

        Catch ex As Exception
            MessageBox.Show("Error formatting grid: " & ex.Message)
        End Try
    End Sub

    ' Color code status
    Private Sub ColorCodeStatusColumn()
        Try
            For Each row As DataGridViewRow In InventoryGrid.Rows
                If Not row.IsNewRow AndAlso row.Cells("Status").Value IsNot Nothing Then
                    Dim status As String = row.Cells("Status").Value.ToString()

                    Select Case status
                        Case "Out of Stock"
                            row.Cells("Status").Style.BackColor = Color.FromArgb(220, 53, 69)
                            row.Cells("Status").Style.ForeColor = Color.White
                        Case "Low Stock"
                            row.Cells("Status").Style.BackColor = Color.FromArgb(255, 193, 7)
                            row.Cells("Status").Style.ForeColor = Color.Black
                        Case "In Stock"
                            row.Cells("Status").Style.BackColor = Color.FromArgb(40, 167, 69)
                            row.Cells("Status").Style.ForeColor = Color.White
                        Case "Overstocked"
                            row.Cells("Status").Style.BackColor = Color.FromArgb(23, 162, 184)
                            row.Cells("Status").Style.ForeColor = Color.White
                    End Select

                    ' Highlight expiring items
                    If row.Cells("Next Expiration").Value IsNot Nothing AndAlso
                       Not IsDBNull(row.Cells("Next Expiration").Value) Then
                        Try
                            Dim expiryDate As Date = Convert.ToDateTime(row.Cells("Next Expiration").Value)
                            Dim daysLeft As Integer = (expiryDate - Date.Now).Days

                            If daysLeft <= 0 Then
                                row.Cells("Next Expiration").Style.BackColor = Color.FromArgb(139, 0, 0)
                                row.Cells("Next Expiration").Style.ForeColor = Color.White
                                row.Cells("Next Expiration").Style.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                            ElseIf daysLeft <= 3 Then
                                row.Cells("Next Expiration").Style.BackColor = Color.FromArgb(220, 53, 69)
                                row.Cells("Next Expiration").Style.ForeColor = Color.White
                            ElseIf daysLeft <= 7 Then
                                row.Cells("Next Expiration").Style.BackColor = Color.FromArgb(255, 193, 7)
                                row.Cells("Next Expiration").Style.ForeColor = Color.Black
                            End If
                        Catch
                            ' Skip if date conversion fails
                        End Try
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error color coding: " & ex.Message)
        End Try
    End Sub

    ' Load statistics
    Private Sub LoadInventoryStatistics()
        Try
            openConn()

            ' Total Items
            Dim sqlTotal As String = "
                SELECT COUNT(DISTINCT i.IngredientID) 
                FROM ingredients i
                WHERE i.IsActive = 1
            "
            Dim cmdTotal As New MySqlCommand(sqlTotal, conn)
            Dim totalItems As Integer = Convert.ToInt32(cmdTotal.ExecuteScalar())

            If Me.Controls.Contains(Label5) Then
                Label5.Text = totalItems.ToString("#,##0")
            End If

            ' Total Value
            Dim sqlValue As String = "
                SELECT COALESCE(SUM(ib.StockQuantity * ib.CostPerUnit), 0)
                FROM inventory_batches ib
                WHERE ib.BatchStatus = 'Active'
            "
            Dim cmdValue As New MySqlCommand(sqlValue, conn)
            Dim totalValue As Decimal = Convert.ToDecimal(cmdValue.ExecuteScalar())

            If Me.Controls.Contains(Label11) Then
                Label11.Text = "₱" & totalValue.ToString("#,##0.00")
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading statistics: " & ex.Message,
                          "Statistics Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    ' Handle View Batches button click
    Private Sub InventoryGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles InventoryGrid.CellContentClick
        Try
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 AndAlso
               InventoryGrid.Columns(e.ColumnIndex).Name = "ViewBatches" Then

                Dim ingredientID As Integer = Convert.ToInt32(InventoryGrid.Rows(e.RowIndex).Cells("Ingredient ID").Value)
                Dim ingredientName As String = InventoryGrid.Rows(e.RowIndex).Cells("Item Name").Value.ToString()

                ' Open Batch Management form
                Dim batchForm As New BatchManagement(ingredientID, ingredientName)
                batchForm.StartPosition = FormStartPosition.CenterScreen
                batchForm.ShowDialog()

                ' Refresh after closing
                LoadInventorySummary()
                LoadInventoryStatistics()
            End If
        Catch ex As Exception
            MessageBox.Show("Error opening batch details: " & ex.Message,
                          "Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        End Try
    End Sub

    ' Search functionality
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            If InventoryGrid.DataSource IsNot Nothing Then
                Dim dt As DataTable = DirectCast(InventoryGrid.DataSource, DataTable)
                Dim searchText As String = TextBox1.Text.Trim()

                If String.IsNullOrEmpty(searchText) Then
                    dt.DefaultView.RowFilter = ""
                Else
                    dt.DefaultView.RowFilter = String.Format(
                        "[Item Name] LIKE '%{0}%' OR [Category] LIKE '%{0}%'",
                        searchText.Replace("'", "''"))
                End If

                ColorCodeStatusColumn()
            End If
        Catch ex As Exception
            ' Silent fail for search
        End Try
    End Sub

    ' Category filter
    Private Sub Category_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Category.SelectedIndexChanged
        Try
            If InventoryGrid.DataSource IsNot Nothing Then
                Dim dt As DataTable = DirectCast(InventoryGrid.DataSource, DataTable)
                Dim selectedCategory As String = Category.Text

                If String.IsNullOrEmpty(selectedCategory) OrElse selectedCategory = "All Categories" Then
                    dt.DefaultView.RowFilter = ""
                Else
                    dt.DefaultView.RowFilter = String.Format(
                        "[Category] = '{0}'",
                        selectedCategory.Replace("'", "''"))
                End If

                ColorCodeStatusColumn()
            End If
        Catch ex As Exception
            ' Silent fail for filter
        End Try
    End Sub

    ' Add new batch
    Private Sub AddItem_Click(sender As Object, e As EventArgs) Handles AddItem.Click
        Try
            Dim addForm As New AddNewItems()
            addForm.StartPosition = FormStartPosition.CenterScreen

            If addForm.ShowDialog() = DialogResult.OK Then
                LoadInventorySummary()
                LoadInventoryStatistics()
            End If
        Catch ex As Exception
            MessageBox.Show("Error opening add form: " & ex.Message,
                          "Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        End Try
    End Sub

    ' Public refresh method
    Public Sub RefreshInventory()
        LoadInventorySummary()
        LoadInventoryStatistics()
    End Sub

End Class