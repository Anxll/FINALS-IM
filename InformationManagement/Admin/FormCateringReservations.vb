Imports MySqlConnector
Imports System.Threading.Tasks
Imports System.Collections.Generic

Public Class FormCateringReservations
    ' Database connection string using global modDB
    Private connectionString As String = modDB.strConnection

    Private Async Sub FormCateringReservations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize ComboBox with filter options
        ComboBox1.Items.Clear()
        ComboBox1.Items.AddRange(New String() {"Daily", "Weekly", "Monthly"})
        ComboBox1.SelectedIndex = 0 ' Default to Daily

        ' Load initial data asynchronously
        Await RefreshAnalyticsAsync()
    End Sub

    Private Async Function RefreshAnalyticsAsync() As Task
        Try
            ' Run database queries concurrently
            Dim summaryTask As Task = LoadReservationSummaryAsync()
            Dim breakdownTask As Task = LoadReservationBreakdownAsync()
            
            Await Task.WhenAll(summaryTask, breakdownTask)
            
        Catch ex As Exception
            MessageBox.Show("Error refreshing catering analytics: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Function LoadReservationSummaryAsync() As Task
        Try
            Dim totalReservations As Integer = 0
            Dim totalEvents As Integer = 0
            Dim avgValue As Decimal = 0

            Await Task.Run(Sub()
                Using conn As New MySqlConnection(connectionString)
                    conn.Open()

                    ' Get Total Reservations count
                    Dim cmdTotalReservations As New MySqlCommand("SELECT COUNT(*) FROM reservations", conn)
                    totalReservations = Convert.ToInt32(cmdTotalReservations.ExecuteScalar())

                    ' Get Total Events (Confirmed)
                    Dim cmdTotalEvents As New MySqlCommand("SELECT COUNT(*) FROM reservations WHERE ReservationStatus = 'Confirmed'", conn)
                    totalEvents = Convert.ToInt32(cmdTotalEvents.ExecuteScalar())

                    ' Calculate Average Event Value
                    Dim cmdAvgValue As New MySqlCommand("
                        SELECT AVG(TotalPaid) 
                        FROM (
                            SELECT ReservationID, SUM(AmountPaid) AS TotalPaid
                            FROM reservation_payments
                            GROUP BY ReservationID
                        ) AS totals
                    ", conn)

                    Dim result As Object = cmdAvgValue.ExecuteScalar()
                    avgValue = If(result IsNot Nothing AndAlso Not IsDBNull(result), Convert.ToDecimal(result), 0D)
                End Using
            End Sub)

            ' Update UI on UI thread
            Me.Invoke(Sub()
                Label5.Text = totalReservations.ToString("N0")
                Label6.Text = totalEvents.ToString("N0")
                Label7.Text = "₱" & avgValue.ToString("N2")
            End Sub)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Async Function LoadReservationBreakdownAsync() As Task
        Try
            Dim dt As New DataTable()
            Dim selectedFilter As String = ""
            
            Me.Invoke(Sub() selectedFilter = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), "Daily"))

            Await Task.Run(Sub()
                Using conn As New MySqlConnection(connectionString)
                    conn.Open()

                    Dim query As String = ""
                    Select Case selectedFilter
                        Case "Daily"
                            query = "
                                SELECT 
                                    DATE(r.EventDate) AS Period, 
                                    COUNT(*) AS ReservationCount, 
                                    SUM(r.NumberOfGuests) AS TotalGuests, 
                                    COALESCE(SUM(p.TotalPaid), 0) AS TotalAmount 
                                FROM reservations r
                                LEFT JOIN (
                                    SELECT ReservationID, SUM(AmountPaid) AS TotalPaid
                                    FROM reservation_payments
                                    GROUP BY ReservationID
                                ) AS p ON p.ReservationID = r.ReservationID
                                GROUP BY DATE(r.EventDate)
                                ORDER BY Period DESC 
                                LIMIT 20"

                        Case "Weekly"
                            query = "
                                SELECT 
                                    CONCAT(YEAR(r.EventDate), '-W', LPAD(WEEK(r.EventDate), 2, '0')) AS Period, 
                                    COUNT(*) AS ReservationCount, 
                                    SUM(r.NumberOfGuests) AS TotalGuests, 
                                    COALESCE(SUM(p.TotalPaid), 0) AS TotalAmount
                                FROM reservations r
                                LEFT JOIN (
                                    SELECT ReservationID, SUM(AmountPaid) AS TotalPaid
                                    FROM reservation_payments
                                    GROUP BY ReservationID
                                ) AS p ON p.ReservationID = r.ReservationID
                                GROUP BY YEAR(r.EventDate), WEEK(r.EventDate)
                                ORDER BY YEAR(r.EventDate) DESC, WEEK(r.EventDate) DESC 
                                LIMIT 20"

                        Case "Monthly"
                            query = "
                                SELECT 
                                    DATE_FORMAT(r.EventDate, '%Y-%m') AS Period, 
                                    COUNT(*) AS ReservationCount, 
                                    SUM(r.NumberOfGuests) AS TotalGuests, 
                                    COALESCE(SUM(p.TotalPaid), 0) AS TotalAmount
                                FROM reservations r
                                LEFT JOIN (
                                    SELECT ReservationID, SUM(AmountPaid) AS TotalPaid
                                    FROM reservation_payments
                                    GROUP BY ReservationID
                                ) AS p ON p.ReservationID = r.ReservationID
                                GROUP BY YEAR(r.EventDate), MONTH(r.EventDate)
                                ORDER BY Period DESC 
                                LIMIT 20"
                    End Select

                    Using cmd As New MySqlCommand(query, conn)
                        Using adapter As New MySqlDataAdapter(cmd)
                            adapter.Fill(dt)
                        End Using
                    End Using
                End Using
            End Sub)

            ' Update UI on UI thread
            Me.Invoke(Sub()
                DataGridView1.Rows.Clear()
                For Each row As DataRow In dt.Rows
                    Dim period As String = row("Period").ToString()
                    Dim count As Integer = Convert.ToInt32(row("ReservationCount"))
                    Dim guests As Integer = Convert.ToInt32(row("TotalGuests"))
                    Dim amount As Decimal = Convert.ToDecimal(row("TotalAmount"))
                    
                    DataGridView1.Rows.Add(period, count, guests, "₱" & amount.ToString("N2"))
                Next
            End Sub)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Async Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Reload breakdown when filter changes
        Try
            Await LoadReservationBreakdownAsync()
        Catch ex As Exception
            ' Error handled in parent but for safety
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Export.Click
        ExportToCSV()
    End Sub

    Private Sub ExportToCSV()
        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "CSV Files (*.csv)|*.csv",
                .FileName = String.Format("Catering_Reservations_{0:yyyyMMdd_HHmmss}.csv", DateTime.Now),
                .Title = "Export Catering Reservations Report"
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

                                    ' Clean up for CSV
                                    cellValue = cellValue.Replace("₱", "").Replace(",", "").Trim()

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

                MessageBox.Show("Catering reservations report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Failed to export CSV: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class