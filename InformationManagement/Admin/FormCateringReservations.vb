Imports MySql.Data.MySqlClient

Public Class FormCateringReservations
    ' Database connection string
    Private connectionString As String = "Server=localhost;Database=tabeya_system;Uid=root;Pwd=;"

    Private Sub FormCateringReservations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize ComboBox with filter options
        ComboBox1.Items.Clear()
        ComboBox1.Items.AddRange(New String() {"Daily", "Weekly", "Monthly"})
        ComboBox1.SelectedIndex = 0 ' Default to Daily

        ' Load initial data
        LoadReservationSummary()
        LoadReservationBreakdown()
    End Sub

    Private Sub LoadReservationSummary()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Get Total Reservations count
                Dim cmdTotalReservations As New MySqlCommand("SELECT COUNT(*) FROM reservations", conn)
                Dim totalReservations As Integer = Convert.ToInt32(cmdTotalReservations.ExecuteScalar())
                Label5.Text = totalReservations.ToString()

                ' Get Total Events (assuming ReservationStatus = 'Confirmed')
                Dim cmdTotalEvents As New MySqlCommand("SELECT COUNT(*) FROM reservations WHERE ReservationStatus = 'Confirmed'", conn)
                Dim totalEvents As Integer = Convert.ToInt32(cmdTotalEvents.ExecuteScalar())
                Label6.Text = totalEvents.ToString()

                ' Calculate Average Event Value from actual payments
                Dim cmdAvgValue As New MySqlCommand("
                    SELECT AVG(TotalPaid) 
                    FROM (
                        SELECT ReservationID, SUM(AmountPaid) AS TotalPaid
                        FROM reservation_payments
                        GROUP BY ReservationID
                    ) AS totals
                ", conn)

                Dim avgValue As Object = cmdAvgValue.ExecuteScalar()
                Label7.Text = If(avgValue IsNot Nothing AndAlso Not IsDBNull(avgValue),
                                 "₱" & Convert.ToDecimal(avgValue).ToString("N2"),
                                 "₱0.00")

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading reservation summary: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadReservationBreakdown()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Clear existing rows
                DataGridView1.Rows.Clear()

                Dim query As String = ""
                Dim selectedFilter As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), "Daily")

                Select Case selectedFilter
                    Case "Daily"
                        ' Group by day
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
                            LIMIT 10"

                    Case "Weekly"
                        ' Group by week
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
                            LIMIT 10"

                    Case "Monthly"
                        ' Group by month
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
                            LIMIT 10"
                End Select

                Dim cmd As New MySqlCommand(query, conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                While reader.Read()
                    Dim period As String = reader("Period").ToString()
                    Dim reservationCount As Integer = Convert.ToInt32(reader("ReservationCount"))
                    Dim totalGuests As Integer = Convert.ToInt32(reader("TotalGuests"))
                    Dim totalAmount As Decimal = If(IsDBNull(reader("TotalAmount")), 0D, Convert.ToDecimal(reader("TotalAmount")))

                    DataGridView1.Rows.Add(period, reservationCount, totalGuests, "₱" & totalAmount.ToString("N2"))
                End While

                reader.Close()

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading reservation breakdown: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Reload data when filter changes
        LoadReservationBreakdown()
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

                MessageBox.Show("Catering reservations report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Open file location
                Process.Start("explorer.exe", String.Format("/select,""{0}""", saveDialog.FileName))
            End If

        Catch ex As Exception
            MessageBox.Show("Failed to export CSV: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class