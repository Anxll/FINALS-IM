Imports MySqlConnector

Public Class Inventory

    Private Sub Inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set form to maximized
        Me.WindowState = FormWindowState.Maximized

        ' Apply responsive layout
        ApplyResponsiveLayout()

        ' Load data
        LoadInventorySummary()
        LoadInventoryStatistics()
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

            ' Position and size for header section (Splitter1)
            Splitter1.Height = 105

            ' Position summary cards
            Dim cardWidth As Integer = CInt((formWidth - (leftMargin * 2) - spacing) / 2)
            Dim cardHeight As Integer = 170

            ' Total Items card (RoundedPane21)
            RoundedPane21.Location = New Point(leftMargin, topMargin)
            RoundedPane21.Size = New Size(cardWidth, cardHeight)

            ' Total Value card (RoundedPane22)
            RoundedPane22.Location = New Point(leftMargin + cardWidth + spacing, topMargin)
            RoundedPane22.Size = New Size(cardWidth, cardHeight)

            ' Search and filter section
            Dim searchTop As Integer = topMargin + cardHeight + 40

            ' Search label
            Label6.Location = New Point(leftMargin, searchTop)

            ' Search textbox - takes 60% width
            Dim searchWidth As Integer = CInt((formWidth - (leftMargin * 2) - spacing) * 0.6)
            TextBox1.Location = New Point(leftMargin, searchTop + 25)
            TextBox1.Size = New Size(searchWidth, 22)

            ' Category label
            Label7.Location = New Point(leftMargin + searchWidth + spacing, searchTop)

            ' Category dropdown - takes remaining width
            Dim categoryWidth As Integer = formWidth - (leftMargin * 2) - searchWidth - spacing
            Category.Location = New Point(leftMargin + searchWidth + spacing, searchTop + 25)
            Category.Size = New Size(categoryWidth, 24)

            ' Add Item button - positioned at top right
            AddItem.Location = New Point(formWidth - leftMargin - 165, 27)
            AddItem.Size = New Size(165, 56)

            ' DataGrid - takes full width and remaining height
            Dim gridTop As Integer = searchTop + 60
            Dim gridHeight As Integer = formHeight - gridTop - 30

            InventoryGrid.Location = New Point(leftMargin, gridTop)
            InventoryGrid.Size = New Size(formWidth - (leftMargin * 2), gridHeight)

            ' Adjust DataGrid column widths proportionally
            AdjustGridColumns()

        Catch ex As Exception
            ' Silent fail to prevent errors during initialization
        End Try
    End Sub

    ' Adjust DataGrid column widths based on available space
    Private Sub AdjustGridColumns()
        Try
            If InventoryGrid.Columns.Count > 0 AndAlso InventoryGrid.Width > 0 Then
                Dim totalWidth As Integer = InventoryGrid.Width - 20 ' Account for scrollbar

                ' Set proportional widths
                If InventoryGrid.Columns.Contains("Item Name") Then
                    InventoryGrid.Columns("Item Name").Width = CInt(totalWidth * 0.15)
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

                If InventoryGrid.Columns.Contains("Min Level") Then
                    InventoryGrid.Columns("Min Level").Width = CInt(totalWidth * 0.08)
                End If

                If InventoryGrid.Columns.Contains("Max Level") Then
                    InventoryGrid.Columns("Max Level").Width = CInt(totalWidth * 0.08)
                End If

                If InventoryGrid.Columns.Contains("ViewBatches") Then
                    InventoryGrid.Columns("ViewBatches").Width = 120
                    InventoryGrid.Columns("ViewBatches").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                End If
            End If
        Catch ex As Exception
            ' Silent fail
        End Try
    End Sub

    ' Load main inventory grid with summary data
    Private Sub LoadInventorySummary()
        Try
            openConn()

            Dim sql As String = "
                SELECT 
                    i.IngredientID AS 'Ingredient ID',
                    i.IngredientName AS 'Item Name',
                    ic.CategoryName AS 'Category',
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

            ' Clear existing columns and set up new ones
            InventoryGrid.DataSource = Nothing
            InventoryGrid.Columns.Clear()
            InventoryGrid.DataSource = dt

            ' Hide Ingredient ID column
            If InventoryGrid.Columns.Contains("Ingredient ID") Then
                InventoryGrid.Columns("Ingredient ID").Visible = False
            End If

            ' Format columns
            FormatInventoryGrid()

            ' Color code status
            ColorCodeStatusColumn()

        Catch ex As Exception
            MessageBox.Show("Error loading inventory: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    ' Format the inventory grid columns
    Private Sub FormatInventoryGrid()
        Try
            With InventoryGrid
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                .RowTemplate.Height = 35
                .DefaultCellStyle.Font = New Font("Segoe UI", 9)
                .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250)

                ' Format specific columns if they exist
                If .Columns.Contains("Total Value") Then
                    .Columns("Total Value").DefaultCellStyle.Format = "₱#,##0.00"
                    .Columns("Total Value").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If

                If .Columns.Contains("Total Quantity") Then
                    .Columns("Total Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If

                If .Columns.Contains("Active Batches") Then
                    .Columns("Active Batches").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If

                If .Columns.Contains("Next Expiration") Then
                    .Columns("Next Expiration").DefaultCellStyle.Format = "MMM dd, yyyy"
                    .Columns("Next Expiration").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If

                If .Columns.Contains("Status") Then
                    .Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Status").DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                End If
            End With

            ' Add View Batches button column
            If Not InventoryGrid.Columns.Contains("ViewBatches") Then
                Dim btnViewBatches As New DataGridViewButtonColumn()
                btnViewBatches.Name = "ViewBatches"
                btnViewBatches.HeaderText = "Actions"
                btnViewBatches.Text = "View Batches"
                btnViewBatches.UseColumnTextForButtonValue = True
                btnViewBatches.Width = 120
                InventoryGrid.Columns.Add(btnViewBatches)
            End If

            ' Apply responsive column widths
            AdjustGridColumns()

        Catch ex As Exception
            MessageBox.Show("Error formatting grid: " & ex.Message)
        End Try
    End Sub

    ' Color code the status column
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
                        Dim expiryDate As Date = Convert.ToDateTime(row.Cells("Next Expiration").Value)
                        Dim daysLeft As Integer = (expiryDate - Date.Now).Days

                        If daysLeft <= 3 Then
                            row.Cells("Next Expiration").Style.BackColor = Color.FromArgb(220, 53, 69)
                            row.Cells("Next Expiration").Style.ForeColor = Color.White
                        ElseIf daysLeft <= 7 Then
                            row.Cells("Next Expiration").Style.BackColor = Color.FromArgb(255, 193, 7)
                            row.Cells("Next Expiration").Style.ForeColor = Color.Black
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error color coding: " & ex.Message)
        End Try
    End Sub

    ' Load statistics in the top panels
    Private Sub LoadInventoryStatistics()
        Try
            openConn()

            ' Total Items
            Dim sqlTotalItems As String = "
                SELECT COUNT(DISTINCT i.IngredientID) 
                FROM ingredients i
                WHERE i.IsActive = 1
            "
            Dim cmdTotal As New MySqlCommand(sqlTotalItems, conn)
            Dim totalItems As Integer = Convert.ToInt32(cmdTotal.ExecuteScalar())
            Label5.Text = totalItems.ToString()

            ' Total Value
            Dim sqlTotalValue As String = "
                SELECT COALESCE(SUM(ib.StockQuantity * ib.CostPerUnit), 0)
                FROM inventory_batches ib
                WHERE ib.BatchStatus = 'Active'
            "
            Dim cmdValue As New MySqlCommand(sqlTotalValue, conn)
            Dim totalValue As Decimal = Convert.ToDecimal(cmdValue.ExecuteScalar())
            Label11.Text = "₱" & totalValue.ToString("#,##0.00")

        Catch ex As Exception
            MessageBox.Show("Error loading statistics: " & ex.Message)
        Finally
            closeConn()
        End Try
    End Sub

    ' Handle cell click events (View Batches button)
    Private Sub InventoryGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles InventoryGrid.CellContentClick
        Try
            ' Check if it's the button column and not header
            If e.RowIndex >= 0 AndAlso e.ColumnIndex = InventoryGrid.Columns("ViewBatches").Index Then
                Dim ingredientID As Integer = Convert.ToInt32(InventoryGrid.Rows(e.RowIndex).Cells("Ingredient ID").Value)
                Dim ingredientName As String = InventoryGrid.Rows(e.RowIndex).Cells("Item Name").Value.ToString()

                ' Open Batch Management form
                Dim batchForm As New BatchManagement(ingredientID, ingredientName)
                batchForm.StartPosition = FormStartPosition.CenterScreen
                batchForm.ShowDialog()

                ' Refresh after closing batch form
                LoadInventorySummary()
                LoadInventoryStatistics()
            End If
        Catch ex As Exception
            MessageBox.Show("Error opening batch details: " & ex.Message)
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
            End If
        Catch ex As Exception
            MessageBox.Show("Error searching: " & ex.Message)
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
            End If
        Catch ex As Exception
            MessageBox.Show("Error filtering: " & ex.Message)
        End Try
    End Sub

    ' Opens the Add New Items Form (Add New Batch)
    Private Sub AddItem_Click(sender As Object, e As EventArgs) Handles AddItem.Click
        Try
            Dim addForm As New AddNewItems()
            addForm.StartPosition = FormStartPosition.CenterScreen

            If addForm.ShowDialog() = DialogResult.OK Then
                ' Refresh inventory after adding
                LoadInventorySummary()
                LoadInventoryStatistics()
            End If
        Catch ex As Exception
            MessageBox.Show("Error opening add form: " & ex.Message)
        End Try
    End Sub

    ' Refresh button
    Public Sub RefreshInventory()
        LoadInventorySummary()
        LoadInventoryStatistics()
    End Sub

End Class