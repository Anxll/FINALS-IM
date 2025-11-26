Imports MySqlConnector
Imports System.Data

Public Class FormDineInOrders

    Private ReadOnly connectionString As String = modDB.strConnection

    Private Sub FormDineInOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDineInOrders()
        ConfigureGrid()
    End Sub

    Private Sub LoadDineInOrders()
        Dim query As String =
            "SELECT 
                o.OrderID,
                GROUP_CONCAT(CONCAT(oi.Quantity, 'x ', oi.ProductName) SEPARATOR ', ') AS ItemsOrdered,
                IFNULL(o.TotalAmount, 0) AS TotalAmount,
                o.OrderStatus AS Status,
                CONCAT(o.OrderDate, ' ', o.OrderTime) AS OrderDateTime
            FROM orders o
            LEFT JOIN order_items oi ON oi.OrderID = o.OrderID
            WHERE o.OrderType = 'Dine-in'
            GROUP BY o.OrderID
            ORDER BY o.OrderDate DESC, o.OrderTime DESC;"

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)
                    DataGridView1.DataSource = table
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading dine-in orders: " & ex.Message, "Database Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigureGrid()
        With DataGridView1
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
        End With

        If DataGridView1.Columns.Contains("TotalAmount") Then
            DataGridView1.Columns("TotalAmount").DefaultCellStyle.Format = "₱#,##0.00"
            DataGridView1.Columns("TotalAmount").HeaderText = "Amount"
        End If

        If DataGridView1.Columns.Contains("OrderDateTime") Then
            DataGridView1.Columns("OrderDateTime").HeaderText = "Time"
        End If

        If DataGridView1.Columns.Contains("ItemsOrdered") Then
            DataGridView1.Columns("ItemsOrdered").HeaderText = "Items"
        End If

        If DataGridView1.Columns.Contains("Status") Then
            DataGridView1.Columns("Status").HeaderText = "Status"
        End If

    End Sub

End Class