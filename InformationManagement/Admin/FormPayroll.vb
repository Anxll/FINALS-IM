Imports MySqlConnector
Imports System.Windows.Forms.DataVisualization.Charting

Public Class FormPayroll
    ' Database connection string

    Private Sub FormPayroll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshData()
    End Sub

    Public Sub RefreshData()
        LoadPayrollData()
        LoadPayrollChart()
    End Sub

    Private Sub LoadPayrollData()
        Try
            Using conn As New MySqlConnection(modDB.strConnection)
                conn.Open()

                ' Get period filter from Reports form
                Dim periodFilter As String = ""
                Dim periodLabel As String = ""
                
                Select Case Reports.SelectedPeriod
                    Case "Daily"
                        periodFilter = " AND DATE(PayPeriodStart) = CURDATE() "
                        periodLabel = "today"
                    Case "Weekly"
                        periodFilter = " AND YEARWEEK(PayPeriodStart, 1) = YEARWEEK(CURDATE(), 1) "
                        periodLabel = "this week"
                    Case "Monthly"
                        periodFilter = " AND MONTH(PayPeriodStart) = MONTH(CURDATE()) AND YEAR(PayPeriodStart) = YEAR(CURDATE()) "
                        periodLabel = "this month"
                    Case "Yearly"
                        periodFilter = " AND YEAR(PayPeriodStart) = YEAR(CURDATE()) "
                        periodLabel = "this year"
                    Case Else
                        periodFilter = "" ' All time
                        periodLabel = "all time"
                End Select

                ' --- 1. TOTAL PAYROLL ---
                Dim queryTotal As String = "SELECT IFNULL(SUM(BasicSalary + Overtime + Bonuses - Deductions), 0) FROM payroll " &
                                          "WHERE Status = 'Paid' " & periodFilter
                Dim cmdTotal As New MySqlCommand(queryTotal, conn)
                Dim currentPayroll As Decimal = Convert.ToDecimal(cmdTotal.ExecuteScalar())

                ' Previous Period Trend logic (Optional, keeping current logic for MoM if monthly, or simplified)
                Dim trendText As String = "Summary for " & periodLabel
                
                Label4.Text = "₱" & currentPayroll.ToString("N2")
                Label5.Text = trendText

                ' --- 2. TOTAL HOURS ---
                Dim queryHours As String = "SELECT IFNULL(SUM(HoursWorked), 0) FROM payroll WHERE Status = 'Paid' " & periodFilter
                Dim cmdTotalHours As New MySqlCommand(queryHours, conn)
                Dim totalHours As Object = cmdTotalHours.ExecuteScalar()
                Label6.Text = If(totalHours IsNot Nothing AndAlso Not IsDBNull(totalHours), Convert.ToDecimal(totalHours).ToString("N1") & "h", "0h")
                Label8.Text = "Total hours " & periodLabel

                ' --- 3. ACTIVE EMPLOYEES & AVG PAY ---
                Dim cmdActiveEmployees As New MySqlCommand("SELECT COUNT(*) FROM employee WHERE EmploymentStatus = 'Active'", conn)
                Dim activeCount As Integer = Convert.ToInt32(cmdActiveEmployees.ExecuteScalar())
                Label7.Text = activeCount.ToString()

                If activeCount > 0 Then
                    Dim avgPay As Decimal = currentPayroll / activeCount
                    Label9.Text = "Avg. ₱" & avgPay.ToString("N0") & " per employee"
                Else
                    Label9.Text = "No active employees"
                End If

                ' --- 4. ANOMALY DETECTION ---
                DetectAnomalies(conn, periodFilter)

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading payroll statistics: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DetectAnomalies(conn As MySqlConnection, periodFilter As String)
        ' Flag records where Overtime > 50% of Basic Salary OR Deductions > 30% of Basic Salary
        Dim query As String = "SELECT COUNT(*) FROM payroll WHERE Status = 'Paid' AND (Overtime > (BasicSalary * 0.5) OR Deductions > (BasicSalary * 0.3))" & periodFilter
        Dim cmd As New MySqlCommand(query, conn)
        Dim anomalyCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())

        If anomalyCount > 0 Then
            Label10.Text = "⚠ " & anomalyCount & " Anomalies Detected (Check Records)"
            Label10.ForeColor = Color.Crimson
        Else
            Label10.Text = "Payroll Summary (All systems normal)"
            Label10.ForeColor = Color.Black
        End If
    End Sub

    Private Sub LoadPayrollChart()
        Try
            Using conn As New MySqlConnection(modDB.strConnection)
                conn.Open()

                ' Clear existing series
                Chart1.Series.Clear()

                ' Create new series for Trends
                Dim series As New Series("Payroll Cost")
                series.ChartType = SeriesChartType.Column
                series.Color = Color.MediumSlateBlue
                series.IsValueShownAsLabel = True
                series.LabelFormat = "₱#,##0"

                ' Adjust query based on period
                Dim query As String = ""
                Select Case Reports.SelectedPeriod
                    Case "Daily"
                        ' Show last 7 days including today
                        query = "SELECT DATE_FORMAT(PayPeriodStart, '%m/%d') as MonthLabel, " &
                                "SUM(BasicSalary + Overtime + Bonuses - Deductions) as TotalNetPay " &
                                "FROM payroll WHERE Status = 'Paid' AND PayPeriodStart >= DATE_SUB(CURDATE(), INTERVAL 7 DAY) " &
                                "GROUP BY PayPeriodStart ORDER BY PayPeriodStart ASC"
                    Case "Weekly"
                        ' Show last 4 weeks
                        query = "SELECT CONCAT('Wk ', WEEK(PayPeriodStart)) as MonthLabel, " &
                                "SUM(BasicSalary + Overtime + Bonuses - Deductions) as TotalNetPay " &
                                "FROM payroll WHERE Status = 'Paid' AND PayPeriodStart >= DATE_SUB(CURDATE(), INTERVAL 4 WEEK) " &
                                "GROUP BY YEAR(PayPeriodStart), WEEK(PayPeriodStart) ORDER BY MIN(PayPeriodStart) ASC"
                    Case Else
                        ' Monthly/Yearly/All Time - Show last 6 months
                        query = "SELECT MonthLabel, TotalNetPay FROM (" &
                                "  SELECT DATE_FORMAT(PayPeriodStart, '%b %Y') as MonthLabel, " &
                                "  SUM(BasicSalary + Overtime + Bonuses - Deductions) as TotalNetPay, " &
                                "  MAX(PayPeriodStart) as MaxDate " &
                                "  FROM payroll " &
                                "  WHERE Status = 'Paid' " &
                                "  GROUP BY YEAR(PayPeriodStart), MONTH(PayPeriodStart), MonthLabel " &
                                "  ORDER BY MaxDate DESC " &
                                "  LIMIT 6" &
                                ") t ORDER BY MaxDate ASC"
                End Select

                Dim cmdTrends As New MySqlCommand(query, conn)

                Using reader As MySqlDataReader = cmdTrends.ExecuteReader()
                    While reader.Read()
                        Dim val As Decimal = Convert.ToDecimal(reader("TotalNetPay"))
                        series.Points.AddXY(reader("MonthLabel").ToString(), val)
                    End While
                End Using

                If series.Points.Count = 0 Then
                    series.Points.AddXY("No Data", 0)
                End If

                Chart1.Series.Add(series)

                ' Configure chart appearance
                Chart1.ChartAreas(0).AxisX.MajorGrid.Enabled = False
                Chart1.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.LightGray
                Chart1.ChartAreas(0).AxisY.LabelStyle.Format = "₱#,##0"
                Chart1.Legends(0).Enabled = False

            End Using
        Catch ex As Exception
            ' Silently fail chart loading or log it
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Export detailed payroll data for accounting/auditing
        Try
            Using conn As New MySqlConnection(modDB.strConnection)
                conn.Open()

                ' Detailed query for better record keeping
                Dim query As String = "SELECT p.PayrollID, CONCAT(e.FirstName, ' ', e.LastName) AS EmployeeName, e.Position, " &
                                    "p.PayPeriodStart, p.PayPeriodEnd, " &
                                    "p.HoursWorked, p.HourlyRate, " &
                                    "p.BasicSalary, p.Overtime AS OvertimePay, p.Bonuses, p.Deductions, " &
                                    "(p.BasicSalary + p.Overtime + p.Bonuses - p.Deductions) AS NetPay, " &
                                    "p.Status, p.CreatedDate as DateProcessed " &
                                    "FROM payroll p " &
                                    "JOIN employee e ON p.EmployeeID = e.EmployeeID " &
                                    "ORDER BY p.CreatedDate DESC"

                Dim cmdExport As New MySqlCommand(query, conn)
                Dim dt As New DataTable()
                Dim adapter As New MySqlDataAdapter(cmdExport)
                adapter.Fill(dt)

                If dt.Rows.Count = 0 Then
                    MessageBox.Show("No payroll records found to export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' Save to CSV
                Dim saveDialog As New SaveFileDialog()
                saveDialog.Filter = "CSV Files (*.csv)|*.csv"
                saveDialog.FileName = "Payroll_Report_" & DateTime.Now.ToString("yyyyMMdd_HHmm") & ".csv"

                If saveDialog.ShowDialog() = DialogResult.OK Then
                    Dim csv As New System.Text.StringBuilder()

                    ' Add header (Cleaned up names)
                    Dim columnHeaders As String() = {"ID", "Employee", "Position", "Start Date", "End Date", "Hours", "Rate", "Basic", "Overtime", "Bonuses", "Deductions", "Net Pay", "Status", "Date Processed"}
                    csv.AppendLine(String.Join(",", columnHeaders))

                    ' Add rows
                    For Each row As DataRow In dt.Rows
                        Dim values As New List(Of String)
                        For i As Integer = 0 To dt.Columns.Count - 1
                            Dim val As String = row(i).ToString()
                            ' Format currency and dates
                            If dt.Columns(i).ColumnName.Contains("Pay") Or dt.Columns(i).ColumnName.Contains("Salary") Or dt.Columns(i).ColumnName.Contains("Bonuses") Or dt.Columns(i).ColumnName.Contains("Deductions") Then
                                val = Convert.ToDecimal(row(i)).ToString("F2")
                            ElseIf dt.Columns(i).DataType = GetType(DateTime) Then
                                val = Convert.ToDateTime(row(i)).ToString("yyyy-MM-dd")
                            End If
                            ' Escape CSV
                            values.Add("""" & val.Replace("""", """""") & """")
                        Next
                        csv.AppendLine(String.Join(",", values))
                    Next

                    System.IO.File.WriteAllText(saveDialog.FileName, csv.ToString())
                    MessageBox.Show("Detailed payroll report exported successfully!" & vbCrLf & "File: " & System.IO.Path.GetFileName(saveDialog.FileName),
                                  "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("Error exporting payroll data: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
    End Sub
End Class