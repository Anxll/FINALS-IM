Imports MySqlConnector
Imports System.Text
Imports System.Data
Imports System.Threading.Tasks

Public Class FormCustomerHistory
    Private ReadOnly connectionString As String = modDB.strConnection
    Private _isLoading As Boolean = False
    Private _baseTitle As String = ""

    Private Sub FormCustomerHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _baseTitle = Label1.Text
        ConfigureGrid()
        BeginLoadCustomerHistory()
    End Sub

    Private Async Sub BeginLoadCustomerHistory()
        If _isLoading Then Return
        _isLoading = True
        SetLoadingState(True)

        Try
            Dim table As DataTable = Await Task.Run(Function() FetchCustomerHistoryTable())

            If Me.IsDisposed OrElse Not Me.IsHandleCreated Then Return
            DataGridView1.DataSource = table
        Catch ex As Exception
            If Not Me.IsDisposed Then
                MessageBox.Show("Error loading customer history:" & vbCrLf & ex.Message,
                                "Database Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
            End If
        Finally
            If Not Me.IsDisposed Then SetLoadingState(False)
            _isLoading = False
        End Try
    End Sub

    Private Function FetchCustomerHistoryTable() As DataTable
        Dim query As String =
            "SELECT " &
            "  o.OrderDate, " &
            "  o.OrderID, " &
            "  o.OrderType, " &
            "  GROUP_CONCAT(CONCAT(oi.ProductName, ' (', oi.Quantity, ')') ORDER BY oi.ProductName SEPARATOR ', ') AS Items, " &
            "  IFNULL(o.TotalAmount, 0) AS TotalAmount, " &
            "  o.OrderStatus " &
            "FROM orders o " &
            "LEFT JOIN order_items oi ON o.OrderID = oi.OrderID " &
            "WHERE 1=1 " &
            "GROUP BY o.OrderID, o.OrderDate, o.OrderType, o.TotalAmount, o.OrderStatus " &
            "ORDER BY o.OrderDate DESC, o.OrderID DESC;"

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

    Private Sub ConfigureGrid()
        ' Use the designer-defined columns; bind to a DataTable for faster loading.
        DataGridView1.AutoGenerateColumns = False

        dateid.DataPropertyName = "OrderDate"
        Orderid.DataPropertyName = "OrderID"
        Type.DataPropertyName = "OrderType"
        Items.DataPropertyName = "Items"
        Amount.DataPropertyName = "TotalAmount"
        Status.DataPropertyName = "OrderStatus"

        dateid.DefaultCellStyle.Format = "yyyy-MM-dd"
        Amount.DefaultCellStyle.Format = "₱#,##0.00"

        DataGridView1.ReadOnly = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
    End Sub

    Private Sub SetLoadingState(isLoading As Boolean)
        Try
            Me.UseWaitCursor = isLoading
            DataGridView1.Enabled = Not isLoading
            Export.Enabled = Not isLoading
            Label1.Text = If(isLoading, _baseTitle & " (Loading...)", _baseTitle)
        Catch
        End Try
    End Sub

    Private Sub Export_Click(sender As Object, e As EventArgs) Handles Export.Click
        ExportToCSV()
    End Sub

    Private Sub ExportToCSV()
        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "CSV Files (*.csv)|*.csv",
                .FileName = String.Format("Customer_History_{0:yyyyMMdd_HHmmss}.csv", DateTime.Now),
                .Title = "Export Customer History Report"
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

                MessageBox.Show("Customer history report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Open file location
                Process.Start("explorer.exe", String.Format("/select,""{0}""", saveDialog.FileName))
            End If

        Catch ex As Exception
            MessageBox.Show("Failed to export CSV: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class