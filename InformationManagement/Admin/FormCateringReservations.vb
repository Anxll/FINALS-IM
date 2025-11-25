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

                ' Calculate Average Event Value
                ' This assumes you have a price/amount column or we can calculate based on guests
                ' For now, using a sample calculation: Total guests * average price per guest
                Dim cmdAvgValue As New MySqlCommand("SELECT AVG(NumberOfGuests * 1500) FROM reservations WHERE ReservationStatus = 'Confirmed'", conn)
                Dim avgValue As Object = cmdAvgValue.ExecuteScalar()
                If avgValue IsNot Nothing AndAlso Not IsDBNull(avgValue) Then
                    Label7.Text = "₱" & Convert.ToDecimal(avgValue).ToString("N2")
                Else
                    Label7.Text = "₱0.00"
                End If

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
                        query = "SELECT DATE(EventDate) as Period, COUNT(*) as ReservationCount, SUM(NumberOfGuests) as TotalGuests, SUM(NumberOfGuests * 1500) as TotalAmount FROM reservations GROUP BY DATE(EventDate) ORDER BY Period DESC LIMIT 10"

                    Case "Weekly"
                        ' Group by week
                        query = "SELECT CONCAT(YEAR(EventDate), '-W', WEEK(EventDate)) as Period, COUNT(*) as ReservationCount, SUM(NumberOfGuests) as TotalGuests, SUM(NumberOfGuests * 1500) as TotalAmount FROM reservations GROUP BY YEAR(EventDate), WEEK(EventDate) ORDER BY YEAR(EventDate) DESC, WEEK(EventDate) DESC LIMIT 10"

                    Case "Monthly"
                        ' Group by month
                        query = "SELECT DATE_FORMAT(EventDate, '%Y-%m') as Period, COUNT(*) as ReservationCount, SUM(NumberOfGuests) as TotalGuests, SUM(NumberOfGuests * 1500) as TotalAmount FROM reservations GROUP BY YEAR(EventDate), MONTH(EventDate) ORDER BY Period DESC LIMIT 10"
                End Select

                Dim cmd As New MySqlCommand(query, conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                While reader.Read()
                    Dim period As String = reader("Period").ToString()
                    Dim reservationCount As Integer = Convert.ToInt32(reader("ReservationCount"))
                    Dim totalGuests As Integer = Convert.ToInt32(reader("TotalGuests"))
                    Dim totalAmount As Decimal = Convert.ToDecimal(reader("TotalAmount"))

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Export to CSV
        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "CSV Files (*.csv)|*.csv"
            saveDialog.FileName = "Catering_Reservations_" & DateTime.Now.ToString("yyyyMMdd") & ".csv"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Dim csv As New System.Text.StringBuilder()

                ' Add header
                csv.AppendLine("Period,Reservations,Total Guests,Total Amount")

                ' Add data rows
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If Not row.IsNewRow Then
                        Dim period As String = If(row.Cells(0).Value IsNot Nothing, row.Cells(0).Value.ToString(), "")
                        Dim reservations As String = If(row.Cells(1).Value IsNot Nothing, row.Cells(1).Value.ToString(), "")
                        Dim totalGuests As String = If(row.Cells(2).Value IsNot Nothing, row.Cells(2).Value.ToString(), "")
                        Dim totalAmount As String = If(row.Cells(3).Value IsNot Nothing, row.Cells(3).Value.ToString().Replace("₱", "").Replace(",", ""), "")

                        csv.AppendLine(String.Format("""{0}"",""{1}"",""{2}"",""{3}""", period, reservations, totalGuests, totalAmount))
                    End If
                Next

                System.IO.File.WriteAllText(saveDialog.FileName, csv.ToString())
                MessageBox.Show("Catering reservations report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error exporting data: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class