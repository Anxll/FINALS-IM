Imports MySqlConnector
Imports System.Data
Imports System.Threading.Tasks

Public Class FormDineInOrders
    Private ReadOnly connectionString As String = modDB.strConnection
    Private _isLoading As Boolean = False
    Private _baseTitle As String = ""

    Private Sub FormDineInOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureGrid()
        _baseTitle = Label2.Text
        BeginLoadDineInOrders()
    End Sub

    Private Async Sub BeginLoadDineInOrders()
        If _isLoading Then Return
        _isLoading = True
        SetLoadingState(True)

        Try
            Dim table As DataTable = Await Task.Run(Function() FetchDineInOrdersTable())

            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
            DataGridView1.DataSource = table
            ConfigureGrid()
        Catch ex As Exception
            If Not Me.IsDisposed Then
                MessageBox.Show("Error loading dine-in orders: " & ex.Message, "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            If Not Me.IsDisposed Then SetLoadingState(False)
            _isLoading = False
        End Try
    End Sub

    Private Function FetchDineInOrdersTable() As DataTable
        Dim query As String =
            "SELECT " &
            "  o.OrderID, " &
            "  GROUP_CONCAT(CONCAT(oi.Quantity, 'x ', oi.ProductName) ORDER BY oi.ProductName SEPARATOR ', ') AS ItemsOrdered, " &
            "  IFNULL(o.TotalAmount, 0) AS TotalAmount, " &
            "  o.OrderStatus AS Status, " &
            "  CONCAT(o.OrderDate, ' ', o.OrderTime) AS OrderDateTime " &
            "FROM orders o " &
            "LEFT JOIN order_items oi ON oi.OrderID = o.OrderID " &
            "WHERE o.OrderType = 'Dine-in' " &
            "GROUP BY o.OrderID, o.TotalAmount, o.OrderStatus, o.OrderDate, o.OrderTime " &
            "ORDER BY o.OrderDate DESC, o.OrderTime DESC;"

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                cmd.CommandTimeout = 60
                Using adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)
                    Return table
                End Using
            End Using
        End Using
    End Function

    Private Sub SetLoadingState(isLoading As Boolean)
        Try
            Me.UseWaitCursor = isLoading
            DataGridView1.Enabled = Not isLoading
            Export.Enabled = Not isLoading
            Label2.Text = If(isLoading, _baseTitle & " (Loading...)", _baseTitle)
        Catch
            ' Best-effort UI polish; don't fail data load due to UI state.
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

    Private Sub Export_Click(sender As Object, e As EventArgs) Handles Export.Click
        ExportToCSV()
    End Sub

    Private Sub ExportToCSV()
        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "CSV Files (*.csv)|*.csv",
                .FileName = String.Format("DineIn_Orders_{0:yyyyMMdd_HHmmss}.csv", DateTime.Now),
                .Title = "Export Dine-In Orders Report"
            }

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Using writer As New IO.StreamWriter(saveDialog.FileName)
                    ' Write headers
                    Dim headers As New List(Of String)
                    For Each column As DataGridViewColumn In DataGridView1.Columns
                        If column.Visible Then
                            headers.Add(column.HeaderText)
                        End If
                    Next
                    writer.WriteLine(String.Join(",", headers))

                    ' Write data rows
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If Not row.IsNewRow Then
                            Dim values As New List(Of String)
                            For Each column As DataGridViewColumn In DataGridView1.Columns
                                If column.Visible Then
                                    Dim cellVal As Object = row.Cells(column.Index).Value
                                    Dim cellValue As String = If(cellVal IsNot Nothing, cellVal.ToString(), "")

                                    ' Remove currency symbols and format properly
                                    cellValue = cellValue.Replace("₱", "").Trim()

                                    ' Escape commas and quotes
                                    If cellValue.Contains(",") OrElse cellValue.Contains("""") Then
                                        cellValue = """" & cellValue.Replace("""", """""") & """"
                                    End If
                                    values.Add(cellValue)
                                End If
                            Next
                            writer.WriteLine(String.Join(",", values))
                        End If
                    Next
                End Using

                MessageBox.Show("Dine-in orders report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Open file location
                Process.Start("explorer.exe", String.Format("/select,""{0}""", saveDialog.FileName))
            End If

        Catch ex As Exception
            MessageBox.Show("Failed to export CSV: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class