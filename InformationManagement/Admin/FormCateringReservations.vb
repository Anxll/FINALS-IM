Imports MySqlConnector
Imports System.Threading.Tasks
Imports System.Collections.Generic

Public Class FormCateringReservations
    ' Database connection string using global modDB
    Private connectionString As String = modDB.strConnection

    ' Pagination state
    Private _currentPage As Integer = 1
    Private ReadOnly _pageSize As Integer = 10
    Private _totalRecords As Integer = 0
    Private _totalPages As Integer = 1

    Private Async Sub FormCateringReservations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load initial data asynchronously
        Await RefreshAnalyticsAsync()
    End Sub

    Public Async Sub RefreshData()
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

                                   ' Build period filter for summary
                                   Dim periodFilter As String = ""
                                   Select Case Reports.SelectedPeriod
                                       Case "Daily" : periodFilter = " WHERE DATE(EventDate) = CURDATE() "
                                       Case "Weekly" : periodFilter = " WHERE YEARWEEK(EventDate, 1) = YEARWEEK(CURDATE(), 1) "
                                       Case "Monthly" : periodFilter = " WHERE MONTH(EventDate) = MONTH(CURDATE()) AND YEAR(EventDate) = YEAR(CURDATE()) "
                                       Case "Yearly" : periodFilter = " WHERE YEAR(EventDate) = YEAR(CURDATE()) "
                                       Case Else : periodFilter = "" ' All Time
                                   End Select

                                   ' Get Total Reservations count
                                   Dim cmdTotalReservations As New MySqlCommand("SELECT COUNT(*) FROM reservations" & periodFilter, conn)
                                   totalReservations = Convert.ToInt32(cmdTotalReservations.ExecuteScalar())

                                   ' Get Total Events (Confirmed)
                                   Dim statusFilter = If(String.IsNullOrEmpty(periodFilter), " WHERE ", periodFilter & " AND ")
                                   Dim cmdTotalEvents As New MySqlCommand("SELECT COUNT(*) FROM reservations " & statusFilter & " ReservationStatus = 'Confirmed'", conn)
                                   totalEvents = Convert.ToInt32(cmdTotalEvents.ExecuteScalar())

                                   ' Calculate Average Event Value
                                   Dim cmdAvgValue As New MySqlCommand("
                        SELECT COALESCE(AVG(TotalPaid), 0)
                        FROM (
                            SELECT r.ReservationID, SUM(p.AmountPaid) AS TotalPaid
                            FROM reservations r
                            LEFT JOIN reservation_payments p ON r.ReservationID = p.ReservationID
                            " & periodFilter & "
                            GROUP BY r.ReservationID
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
            Dim selectedFilter As String = Reports.SelectedPeriod

            ' Get total count first to update pagination
            _totalRecords = Await Task.Run(Function() FetchTotalReservationCount(selectedFilter))
            _totalPages = Math.Max(1, Math.Ceiling(_totalRecords / _pageSize))

            If _currentPage > _totalPages Then _currentPage = _totalPages
            If _currentPage < 1 Then _currentPage = 1

            Dim offset As Integer = (_currentPage - 1) * _pageSize

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
                                LIMIT @limit OFFSET @offset"

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
                                LIMIT @limit OFFSET @offset"

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
                                LIMIT @limit OFFSET @offset"

                                       Case "Yearly"
                                           query = "
                                SELECT 
                                    YEAR(r.EventDate) AS Period, 
                                    COUNT(*) AS ReservationCount, 
                                    SUM(r.NumberOfGuests) AS TotalGuests, 
                                    COALESCE(SUM(p.TotalPaid), 0) AS TotalAmount
                                FROM reservations r
                                LEFT JOIN (
                                    SELECT ReservationID, SUM(AmountPaid) AS TotalPaid
                                    FROM reservation_payments
                                    GROUP BY ReservationID
                                ) AS p ON p.ReservationID = r.ReservationID
                                GROUP BY YEAR(r.EventDate)
                                ORDER BY Period DESC 
                                LIMIT @limit OFFSET @offset"
                                       Case Else ' All Time - Default to Monthly display
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
                                LIMIT @limit OFFSET @offset"
                                   End Select

                                   Using cmd As New MySqlCommand(query, conn)
                                       cmd.Parameters.AddWithValue("@limit", _pageSize)
                                       cmd.Parameters.AddWithValue("@offset", offset)
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
                          UpdatePaginationUI()
                      End Sub)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function FetchTotalReservationCount(filter As String) As Integer
        Dim groupClause As String = ""
        Select Case filter
            Case "Daily" : groupClause = "GROUP BY DATE(EventDate)"
            Case "Weekly" : groupClause = "GROUP BY YEARWEEK(EventDate, 1)"
            Case "Monthly" : groupClause = "GROUP BY YEAR(EventDate), MONTH(EventDate)"
            Case "Yearly" : groupClause = "GROUP BY YEAR(EventDate)"
            Case Else : groupClause = "GROUP BY YEAR(EventDate), MONTH(EventDate)"
        End Select

        ' We need to count the groups (periods)
        Dim query As String = $"SELECT COUNT(*) FROM (SELECT 1 FROM reservations {groupClause}) AS t"

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Using cmd As New MySqlCommand(query, conn)
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Sub UpdatePaginationUI()
        lblPageStatus.Text = $"Page {_currentPage} of {_totalPages}"
        btnPrev.Enabled = (_currentPage > 1)
        btnNext.Enabled = (_currentPage < _totalPages)
    End Sub

    Private Async Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If _currentPage > 1 Then
            _currentPage -= 1
            Await LoadReservationBreakdownAsync()
        End If
    End Sub

    Private Async Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If _currentPage < _totalPages Then
            _currentPage += 1
            Await LoadReservationBreakdownAsync()
        End If
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