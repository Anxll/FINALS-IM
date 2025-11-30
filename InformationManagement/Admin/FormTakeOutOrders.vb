Imports MySqlConnector
Imports System.Data

Public Class FormTakeOutOrders
    Private ReadOnly connectionString As String = modDB.strConnection

    Private Sub FormTakeOutOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTakeoutOrders()
    End Sub

    Private Sub LoadTakeoutOrders()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String =
                    "SELECT " &
                    "OrderID, " &
                    "ItemsOrderedCount AS Items, " &
                    "TotalAmount AS Amount, " &
                    "OrderStatus AS Status, " &
                    "DATE_FORMAT(OrderTime, '%Y-%m-%d %H:%i') AS Time " &
                    "FROM orders " &
                    "WHERE OrderType = 'Takeout' " &
                    "ORDER BY OrderID DESC"
                Using cmd As New MySqlCommand(query, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        adapter.Fill(dt)
                        DataGridView1.AutoGenerateColumns = True
                        DataGridView1.DataSource = dt
                    End Using
                End Using
            End Using
            FormatGrid()
            HideOrderID()
        Catch ex As Exception
            MessageBox.Show("Error loading takeout orders: " & ex.Message)
        End Try
    End Sub

    ' =============================
    ' HIDE ORDER ID COLUMN
    ' =============================
    Private Sub HideOrderID()
        If DataGridView1.Columns.Contains("OrderID") Then
            DataGridView1.Columns("OrderID").Visible = False
        End If
    End Sub

    ' =============================
    ' FORMAT GRID
    ' =============================
    Private Sub FormatGrid()
        With DataGridView1
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .RowHeadersVisible = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .DefaultCellStyle.Font = New Font("Segoe UI", 10)
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 10)
        End With
        ' ✅ SAFE: Time is now a string, no DateTime formatting applied
        If DataGridView1.Columns.Contains("Time") Then
            DataGridView1.Columns("Time").DefaultCellStyle.Format = ""
        End If
        ' ✅ SAFE currency formatting
        If DataGridView1.Columns.Contains("Amount") Then
            DataGridView1.Columns("Amount").DefaultCellStyle.Format = "₱ #,##0.00"
        End If
    End Sub

    ' =============================
    ' PREVENT ERROR POPUPS
    ' =============================
    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) _
        Handles DataGridView1.DataError
        e.ThrowException = False
    End Sub

    Private Sub Export_Click(sender As Object, e As EventArgs) Handles Export.Click
        ExportToCSV()
    End Sub

    Private Sub ExportToCSV()
        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "CSV Files (*.csv)|*.csv",
                .FileName = String.Format("Takeout_Orders_{0:yyyyMMdd_HHmmss}.csv", DateTime.Now),
                .Title = "Export Takeout Orders Report"
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

                MessageBox.Show("Takeout orders report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Open file location
                Process.Start("explorer.exe", String.Format("/select,""{0}""", saveDialog.FileName))
            End If

        Catch ex As Exception
            MessageBox.Show("Failed to export CSV: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class