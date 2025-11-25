Imports MySql.Data.MySqlClient

Public Class FormDineInOrders
    ' Database connection string
    Private connectionString As String = "Server=localhost;Database=tabeya_system;Uid=root;Pwd=;"

    Private Sub FormDineInOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureDataGridView()
        LoadDineInOrders()
    End Sub

    Private Sub ConfigureDataGridView()
        ' Configure DataGridView appearance
        DataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.ReadOnly = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub LoadDineInOrders()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Clear existing rows
                DataGridView1.Rows.Clear()

                ' Query to get only the needed data for display
                Dim query As String = "SELECT OrderID, ItemsOrdered, TotalAmount, PaymentStatus, CONCAT(OrderDate, ' ', OrderTime) as DateTime FROM orders WHERE OrderType = 'Dine-in' ORDER BY OrderDate DESC, OrderTime DESC"

                Dim cmd As New MySqlCommand(query, conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                Dim rowCount As Integer = 0
                While reader.Read()
                    rowCount += 1

                    Dim orderID As String = If(IsDBNull(reader("OrderID")), "", reader("OrderID").ToString())
                    Dim items As String = If(IsDBNull(reader("ItemsOrdered")), "No items", reader("ItemsOrdered").ToString())
                    Dim amount As String = "₱" & If(IsDBNull(reader("TotalAmount")), 0D, Convert.ToDecimal(reader("TotalAmount"))).ToString("N2")
                    Dim paymentStatus As String = If(IsDBNull(reader("PaymentStatus")), "Pending", reader("PaymentStatus").ToString())
                    Dim dateTime As String = If(IsDBNull(reader("DateTime")), "", reader("DateTime").ToString())

                    ' Add row to DataGridView
                    DataGridView1.Rows.Add(orderID, items, amount, paymentStatus, dateTime)
                End While

                reader.Close()

                ' Show message if no data
                If rowCount = 0 Then
                    MessageBox.Show("No dine-in orders found in the database yet. Orders will appear here once they are created.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading dine-in orders: " & ex.Message & vbCrLf & "Please check your database connection.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Export to CSV
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "CSV Files (*.csv)|*.csv"
            saveDialog.FileName = "DineIn_Orders_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".csv"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Dim csv As New System.Text.StringBuilder()

                ' Add header
                csv.AppendLine("Order ID,Items,Amount,Payment Status,Time")

                ' Add data rows
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If Not row.IsNewRow Then
                        Dim orderID As String = If(row.Cells(0).Value IsNot Nothing, row.Cells(0).Value.ToString(), "")
                        Dim items As String = If(row.Cells(1).Value IsNot Nothing, row.Cells(1).Value.ToString().Replace("""", """"""), "")
                        Dim amount As String = If(row.Cells(2).Value IsNot Nothing, row.Cells(2).Value.ToString().Replace("₱", "").Replace(",", ""), "0")
                        Dim paymentStatus As String = If(row.Cells(3).Value IsNot Nothing, row.Cells(3).Value.ToString(), "")
                        Dim time As String = If(row.Cells(4).Value IsNot Nothing, row.Cells(4).Value.ToString(), "")

                        csv.AppendLine(String.Format("""{0}"",""{1}"",""{2}"",""{3}"",""{4}""", orderID, items, amount, paymentStatus, time))
                    End If
                Next

                System.IO.File.WriteAllText(saveDialog.FileName, csv.ToString())
                MessageBox.Show("Dine-in orders report exported successfully to:" & vbCrLf & saveDialog.FileName, "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error exporting data: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        ' Color code payment status
        If e.ColumnIndex = 3 AndAlso e.Value IsNot Nothing Then ' PaymentStatus column
            Dim status As String = e.Value.ToString().ToLower()
            Select Case status
                Case "paid", "completed"
                    DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Green
                    DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = New Font(DataGridView1.Font, FontStyle.Bold)
                Case "pending", "processing"
                    DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Orange
                    DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = New Font(DataGridView1.Font, FontStyle.Bold)
                Case "cancelled", "refunded"
                    DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                    DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = New Font(DataGridView1.Font, FontStyle.Bold)
                Case "n/a"
                    DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Gray
            End Select
        End If
    End Sub

    ' Refresh button functionality (if you want to add a refresh feature)
    Public Sub RefreshOrders()
        LoadDineInOrders()
    End Sub
End Class