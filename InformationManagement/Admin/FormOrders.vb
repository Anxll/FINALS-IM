Imports MySqlConnector
Imports System.Data

Public Class FormOrders

    Private ordersData As New DataTable()
    Private currentFilter As String = "All"
    Private searchText As String = ""

    ' =======================================================================
    ' FORM LOAD
    ' =======================================================================
    Private Sub FormOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Initialize controls
            InitializeDataGridView()
            InitializeFilters()
            LoadOrdersData()
            UpdateStatistics()

        Catch ex As Exception
            MessageBox.Show($"Form Load Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' INITIALIZE DATAGRIDVIEW
    ' =======================================================================
    Private Sub InitializeDataGridView()
        ' Check if DataGridView exists on your form
        ' If not, you'll need to add one in the designer
        ' This is a template - adjust based on your actual controls

        ' Example if you have a DataGridView named dgvOrders:
        ' With dgvOrders
        '     .AutoGenerateColumns = False
        '     .AllowUserToAddRows = False
        '     .AllowUserToDeleteRows = False
        '     .ReadOnly = True
        '     .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        '     .MultiSelect = False
        '     .RowHeadersVisible = False
        '     .BackgroundColor = Color.White
        '     .BorderStyle = BorderStyle.None
        '     .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(99, 102, 241)
        '     .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        '     .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        '     .DefaultCellStyle.Font = New Font("Segoe UI", 9)
        '     .RowTemplate.Height = 35
        ' End With
    End Sub

    ' =======================================================================
    ' INITIALIZE FILTERS (ComboBox, etc.)
    ' =======================================================================
    Private Sub InitializeFilters()
        ' Example if you have a ComboBox for status filter
        ' cmbStatusFilter.Items.Clear()
        ' cmbStatusFilter.Items.AddRange(New String() {"All", "Pending", "Processing", "Completed", "Cancelled"})
        ' cmbStatusFilter.SelectedIndex = 0
    End Sub

    ' =======================================================================
    ' LOAD ORDERS DATA FROM DATABASE
    ' =======================================================================
    Private Sub LoadOrdersData(Optional filterStatus As String = "All", Optional search As String = "")
        Try
            If conn Is Nothing Then
                MessageBox.Show("Database connection not initialized.", "Connection Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                LoadSampleData()
                Return
            End If

            If conn.State <> ConnectionState.Open Then
                openConn()
            End If

            ' Check if orders table exists
            If Not TableExists("orders") Then
                MessageBox.Show("Orders table not found. Loading sample data.", "Table Missing",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                LoadSampleData()
                Return
            End If

            ' Build query with filters
            Dim sql As String = BuildOrdersQuery(filterStatus, search)

            Using cmd As New MySqlCommand(sql, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    ordersData.Clear()
                    adapter.Fill(ordersData)
                End Using
            End Using

            ' Update display (adjust based on your controls)
            DisplayOrders()

        Catch ex As MySqlException
            MessageBox.Show($"Database Error: {ex.Message}{vbCrLf}Loading sample data instead.",
                          "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            LoadSampleData()
        Catch ex As Exception
            MessageBox.Show($"Error loading orders: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            LoadSampleData()
        End Try
    End Sub

    ' =======================================================================
    ' BUILD ORDERS QUERY
    ' =======================================================================
    Private Function BuildOrdersQuery(filterStatus As String, search As String) As String
        Dim sql As String = "
            SELECT 
                o.OrderID,
                o.CustomerID,
                COALESCE(c.Name, 'Guest') AS CustomerName,
                o.OrderDate,
                o.TotalAmount,
                o.OrderStatus,
                o.PaymentMethod,
                o.DeliveryAddress,
                COUNT(oi.OrderItemID) AS ItemCount
            FROM orders o
            LEFT JOIN customers c ON o.CustomerID = c.CustomerID
            LEFT JOIN order_items oi ON o.OrderID = oi.OrderID
            WHERE 1=1
        "

        ' Add status filter
        If filterStatus <> "All" AndAlso Not String.IsNullOrEmpty(filterStatus) Then
            sql &= $" AND o.OrderStatus = '{filterStatus}'"
        End If

        ' Add search filter
        If Not String.IsNullOrEmpty(search) Then
            sql &= $" AND (o.OrderID LIKE '%{search}%' OR c.Name LIKE '%{search}%' OR o.DeliveryAddress LIKE '%{search}%')"
        End If

        sql &= " GROUP BY o.OrderID ORDER BY o.OrderDate DESC LIMIT 1000"

        Return sql
    End Function

    ' =======================================================================
    ' CHECK IF TABLE EXISTS
    ' =======================================================================
    Private Function TableExists(tableName As String) As Boolean
        Try
            Dim sql As String = $"
                SELECT COUNT(*) 
                FROM information_schema.tables 
                WHERE table_schema = DATABASE()
                  AND table_name = '{tableName}'
            "

            Using cmd As New MySqlCommand(sql, conn)
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        Catch
            Return False
        End Try
    End Function

    ' =======================================================================
    ' DISPLAY ORDERS (Update your UI here)
    ' =======================================================================
    Private Sub DisplayOrders()
        ' This depends on how you want to display orders
        ' Examples:

        ' If using DataGridView:
        ' dgvOrders.DataSource = ordersData

        ' If using ListBox:
        ' lstOrders.Items.Clear()
        ' For Each row As DataRow In ordersData.Rows
        '     lstOrders.Items.Add($"Order #{row("OrderID")} - {row("CustomerName")} - ₱{Convert.ToDecimal(row("TotalAmount")):N2}")
        ' Next

        ' If using FlowLayoutPanel with custom controls:
        ' flowPanelOrders.Controls.Clear()
        ' For Each row As DataRow In ordersData.Rows
        '     Dim orderCard As New OrderCard(row)
        '     flowPanelOrders.Controls.Add(orderCard)
        ' Next
    End Sub

    ' =======================================================================
    ' LOAD SAMPLE DATA FOR TESTING
    ' =======================================================================
    Private Sub LoadSampleData()
        Try
            ordersData.Clear()

            ' Create columns
            If ordersData.Columns.Count = 0 Then
                ordersData.Columns.Add("OrderID", GetType(Integer))
                ordersData.Columns.Add("CustomerID", GetType(Integer))
                ordersData.Columns.Add("CustomerName", GetType(String))
                ordersData.Columns.Add("OrderDate", GetType(DateTime))
                ordersData.Columns.Add("TotalAmount", GetType(Decimal))
                ordersData.Columns.Add("OrderStatus", GetType(String))
                ordersData.Columns.Add("PaymentMethod", GetType(String))
                ordersData.Columns.Add("DeliveryAddress", GetType(String))
                ordersData.Columns.Add("ItemCount", GetType(Integer))
            End If

            ' Add sample rows
            ordersData.Rows.Add(1001, 1, "Juan Dela Cruz", DateTime.Now.AddDays(-1), 1250.5D, "Pending", "GCash", "123 Main St, Manila", 3)
            ordersData.Rows.Add(1002, 2, "Maria Santos", DateTime.Now.AddDays(-2), 2340D, "Processing", "COD", "456 Oak Ave, Quezon City", 5)
            ordersData.Rows.Add(1003, 3, "Jose Reyes", DateTime.Now.AddDays(-3), 890.75D, "Completed", "Credit Card", "789 Pine Rd, Makati", 2)
            ordersData.Rows.Add(1004, 4, "Ana Garcia", DateTime.Now.AddDays(-4), 1580.25D, "Completed", "GCash", "321 Elm St, Pasig", 4)
            ordersData.Rows.Add(1005, 5, "Pedro Martinez", DateTime.Now.AddDays(-5), 3200D, "Cancelled", "Bank Transfer", "654 Maple Dr, Taguig", 6)

            DisplayOrders()

        Catch ex As Exception
            MessageBox.Show($"Error loading sample data: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' UPDATE STATISTICS
    ' =======================================================================
    Private Sub UpdateStatistics()
        Try
            If ordersData.Rows.Count = 0 Then
                SetDefaultStatistics()
                Return
            End If

            Dim totalOrders As Integer = ordersData.Rows.Count
            Dim totalRevenue As Decimal = 0
            Dim pendingCount As Integer = 0
            Dim completedCount As Integer = 0
            Dim cancelledCount As Integer = 0

            For Each row As DataRow In ordersData.Rows
                totalRevenue += Convert.ToDecimal(row("TotalAmount"))

                Dim status As String = row("OrderStatus").ToString()
                Select Case status
                    Case "Pending"
                        pendingCount += 1
                    Case "Completed"
                        completedCount += 1
                    Case "Cancelled"
                        cancelledCount += 1
                End Select
            Next

            ' Update labels (adjust based on your actual label names)
            ' lblTotalOrders.Text = totalOrders.ToString()
            ' lblTotalRevenue.Text = $"₱{totalRevenue:N2}"
            ' lblPending.Text = pendingCount.ToString()
            ' lblCompleted.Text = completedCount.ToString()
            ' lblCancelled.Text = cancelledCount.ToString()

        Catch ex As Exception
            MessageBox.Show($"Error updating statistics: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' SET DEFAULT STATISTICS
    ' =======================================================================
    Private Sub SetDefaultStatistics()
        ' Set all statistics to 0 or default values
        ' lblTotalOrders.Text = "0"
        ' lblTotalRevenue.Text = "₱0.00"
        ' lblPending.Text = "0"
        ' lblCompleted.Text = "0"
        ' lblCancelled.Text = "0"
    End Sub

    ' =======================================================================
    ' GET ORDER DETAILS
    ' =======================================================================
    Public Function GetOrderDetails(orderId As Integer) As DataTable
        Dim orderDetails As New DataTable()

        Try
            If conn Is Nothing OrElse conn.State <> ConnectionState.Open Then
                openConn()
            End If

            Dim sql As String = $"
                SELECT 
                    oi.OrderItemID,
                    oi.ProductID,
                    p.ProductName,
                    oi.Quantity,
                    oi.UnitPrice,
                    (oi.Quantity * oi.UnitPrice) AS Subtotal
                FROM order_items oi
                LEFT JOIN products p ON oi.ProductID = p.ProductID
                WHERE oi.OrderID = {orderId}
            "

            Using cmd As New MySqlCommand(sql, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(orderDetails)
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error loading order details: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return orderDetails
    End Function

    ' =======================================================================
    ' UPDATE ORDER STATUS
    ' =======================================================================
    Public Function UpdateOrderStatus(orderId As Integer, newStatus As String) As Boolean
        Try
            If conn Is Nothing OrElse conn.State <> ConnectionState.Open Then
                openConn()
            End If

            Dim sql As String = $"
                UPDATE orders 
                SET OrderStatus = @status,
                    UpdatedDate = NOW()
                WHERE OrderID = @orderId
            "

            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@status", newStatus)
                cmd.Parameters.AddWithValue("@orderId", orderId)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    LoadOrdersData(currentFilter, searchText)
                    UpdateStatistics()
                    Return True
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error updating order status: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return False
    End Function

    ' =======================================================================
    ' SEARCH ORDERS
    ' =======================================================================
    Public Sub SearchOrders(searchTerm As String)
        searchText = searchTerm
        LoadOrdersData(currentFilter, searchText)
        UpdateStatistics()
    End Sub

    ' =======================================================================
    ' FILTER ORDERS BY STATUS
    ' =======================================================================
    Public Sub FilterOrdersByStatus(status As String)
        currentFilter = status
        LoadOrdersData(currentFilter, searchText)
        UpdateStatistics()
    End Sub

    ' =======================================================================
    ' REFRESH DATA
    ' =======================================================================
    Public Sub RefreshData()
        LoadOrdersData(currentFilter, searchText)
        UpdateStatistics()
    End Sub

    ' =======================================================================
    ' EXPORT ORDERS TO CSV
    ' =======================================================================
    Public Sub ExportToCSV()
        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "CSV File|*.csv",
                .Title = "Export Orders",
                .FileName = $"Orders_Export_{DateTime.Now:yyyy-MM-dd}"
            }

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Using writer As New IO.StreamWriter(saveDialog.FileName)
                    ' Write headers
                    Dim headers As New List(Of String)
                    For Each column As DataColumn In ordersData.Columns
                        headers.Add(column.ColumnName)
                    Next
                    writer.WriteLine(String.Join(",", headers))

                    ' Write data
                    For Each row As DataRow In ordersData.Rows
                        Dim values As New List(Of String)
                        For Each item In row.ItemArray
                            values.Add($"""{item}""")
                        Next
                        writer.WriteLine(String.Join(",", values))
                    Next
                End Using

                MessageBox.Show("Orders exported successfully!", "Export Complete",
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show($"Export Error: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =======================================================================
    ' GET ORDER STATISTICS
    ' =======================================================================
    Public Function GetOrderStatistics(Optional dateFrom As DateTime = Nothing,
                                       Optional dateTo As DateTime = Nothing) As Dictionary(Of String, Object)
        Dim stats As New Dictionary(Of String, Object)

        Try
            If conn Is Nothing OrElse conn.State <> ConnectionState.Open Then
                openConn()
            End If

            Dim dateFilter As String = ""
            If dateFrom <> Nothing AndAlso dateTo <> Nothing Then
                dateFilter = $"WHERE OrderDate BETWEEN '{dateFrom:yyyy-MM-dd}' AND '{dateTo:yyyy-MM-dd}'"
            End If

            Dim sql As String = $"
                SELECT 
                    COUNT(*) AS TotalOrders,
                    COALESCE(SUM(TotalAmount), 0) AS TotalRevenue,
                    COALESCE(AVG(TotalAmount), 0) AS AvgOrderValue,
                    COUNT(CASE WHEN OrderStatus = 'Pending' THEN 1 END) AS PendingOrders,
                    COUNT(CASE WHEN OrderStatus = 'Processing' THEN 1 END) AS ProcessingOrders,
                    COUNT(CASE WHEN OrderStatus = 'Completed' THEN 1 END) AS CompletedOrders,
                    COUNT(CASE WHEN OrderStatus = 'Cancelled' THEN 1 END) AS CancelledOrders
                FROM orders
                {dateFilter}
            "

            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        stats("TotalOrders") = Convert.ToInt32(reader("TotalOrders"))
                        stats("TotalRevenue") = Convert.ToDecimal(reader("TotalRevenue"))
                        stats("AvgOrderValue") = Convert.ToDecimal(reader("AvgOrderValue"))
                        stats("PendingOrders") = Convert.ToInt32(reader("PendingOrders"))
                        stats("ProcessingOrders") = Convert.ToInt32(reader("ProcessingOrders"))
                        stats("CompletedOrders") = Convert.ToInt32(reader("CompletedOrders"))
                        stats("CancelledOrders") = Convert.ToInt32(reader("CancelledOrders"))
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error getting statistics: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return stats
    End Function

    ' =======================================================================
    ' GENERATE ORDERS REPORT
    ' =======================================================================
    Public Function GenerateOrdersReport() As String
        Dim report As New Text.StringBuilder()
        Dim stats = GetOrderStatistics()

        report.AppendLine("═══════════════════════════════════════════════════════")
        report.AppendLine("                  ORDERS REPORT")
        report.AppendLine("═══════════════════════════════════════════════════════")
        report.AppendLine()

        report.AppendLine("SUMMARY:")
        report.AppendLine($"  Total Orders:       {stats("TotalOrders"):N0}")
        report.AppendLine($"  Total Revenue:      ₱{stats("TotalRevenue"):N2}")
        report.AppendLine($"  Average Order:      ₱{stats("AvgOrderValue"):N2}")
        report.AppendLine()

        report.AppendLine("ORDER STATUS BREAKDOWN:")
        report.AppendLine($"  Pending:       {stats("PendingOrders"),5}")
        report.AppendLine($"  Processing:    {stats("ProcessingOrders"),5}")
        report.AppendLine($"  Completed:     {stats("CompletedOrders"),5}")
        report.AppendLine($"  Cancelled:     {stats("CancelledOrders"),5}")
        report.AppendLine()

        report.AppendLine($"Report Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
        report.AppendLine("═══════════════════════════════════════════════════════")

        Return report.ToString()
    End Function

    ' =======================================================================
    ' CLEANUP
    ' =======================================================================
    Private Sub FormOrders_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Catch
        End Try
    End Sub

    ' =======================================================================
    ' ROUNDEDPANE21 PAINT EVENT (Keep this if needed for custom drawing)
    ' =======================================================================
    Private Sub RoundedPane21_Paint(sender As Object, e As PaintEventArgs) Handles RoundedPane21.Paint
        ' Custom painting code here if needed
    End Sub

End Class