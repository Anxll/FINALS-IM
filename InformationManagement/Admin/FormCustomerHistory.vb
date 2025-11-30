Imports MySqlConnector
Imports System.Text

Public Class FormCustomerHistory
    Private ReadOnly connectionString As String = modDB.strConnection

    Private Sub FormCustomerHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomerHistory()
    End Sub

    Private Sub LoadCustomerHistory()
        Dim query As String =
            "SELECT o.OrderID,
                    o.OrderDate,
                    o.OrderType,
                    o.TotalAmount,
                    o.OrderStatus,
                    GROUP_CONCAT(CONCAT(oi.ProductName, ' (', oi.Quantity, ')') SEPARATOR ', ') AS Items
             FROM orders o
             LEFT JOIN order_items oi ON o.OrderID = oi.OrderID
             GROUP BY o.OrderID
             ORDER BY o.OrderDate DESC;"
        Try
            Using conn As New MySqlConnection(connectionString)
                Using cmd As New MySqlCommand(query, conn)
                    conn.Open()
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        DataGridView1.Rows.Clear()
                        While reader.Read()
                            ' Format date safely
                            Dim orderDate As String = ""
                            If Not reader.IsDBNull(reader.GetOrdinal("OrderDate")) Then
                                orderDate = CDate(reader("OrderDate")).ToString("yyyy-MM-dd")
                            End If
                            ' Add row to DataGridView
                            DataGridView1.Rows.Add(
                                orderDate,
                                reader("OrderID").ToString(),
                                reader("OrderType").ToString(),
                                reader("Items").ToString(),
                                Convert.ToDecimal(reader("TotalAmount")).ToString("0.00"),
                                reader("OrderStatus").ToString()
                            )
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading customer history:" & vbCrLf & ex.Message,
                            "Database Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
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