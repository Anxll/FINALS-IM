Imports MySqlConnector
Imports System.Windows.Forms.DataVisualization.Charting

Public Class FormPayroll
    ' Database connection string

    Private Sub FormPayroll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPayrollData()
        LoadPayrollChart()
    End Sub

    Private Sub LoadPayrollData()
        Try
            Using conn As New MySqlConnection(modDB.strConnection)
                conn.Open()

                ' --- 1. TOTAL PAYROLL & TRENDS ---
                ' Current Month Total
                Dim queryCurrentMonth As String = "SELECT IFNULL(SUM(BasicSalary + Overtime + Bonuses - Deductions), 0) FROM payroll " &
                                                 "WHERE Status = 'Paid' AND MONTH(PayPeriodStart) = MONTH(CURRENT_DATE()) AND YEAR(PayPeriodStart) = YEAR(CURRENT_DATE())"
                Dim cmdCurrentMonth As New MySqlCommand(queryCurrentMonth, conn)
                Dim currentPayroll As Decimal = Convert.ToDecimal(cmdCurrentMonth.ExecuteScalar())

                ' Previous Month Total
                Dim queryPrevMonth As String = "SELECT IFNULL(SUM(BasicSalary + Overtime + Bonuses - Deductions), 0) FROM payroll " &
                                              "WHERE Status = 'Paid' AND MONTH(PayPeriodStart) = MONTH(DATE_SUB(CURRENT_DATE(), INTERVAL 1 MONTH)) " &
                                              "AND YEAR(PayPeriodStart) = YEAR(DATE_SUB(CURRENT_DATE(), INTERVAL 1 MONTH))"
                Dim cmdPrevMonth As New MySqlCommand(queryPrevMonth, conn)
                Dim prevPayroll As Decimal = Convert.ToDecimal(cmdPrevMonth.ExecuteScalar())

                ' Calculate MoM Trend
                Dim momTrend As String = "0% vs last month"
                If prevPayroll > 0 Then
                    Dim diff As Decimal = ((currentPayroll - prevPayroll) / prevPayroll) * 100
                    momTrend = (If(diff >= 0, "+", "")) & diff.ToString("N1") & "% vs last month"
                End If

                Label4.Text = "₱" & currentPayroll.ToString("N2")
                Label5.Text = momTrend

                ' --- 2. TOTAL HOURS ---
                Dim cmdTotalHours As New MySqlCommand("SELECT IFNULL(SUM(HoursWorked), 0) FROM payroll WHERE Status = 'Paid' AND MONTH(PayPeriodStart) = MONTH(CURRENT_DATE())", conn)
                Dim totalHours As Object = cmdTotalHours.ExecuteScalar()
                Label6.Text = If(totalHours IsNot Nothing AndAlso Not IsDBNull(totalHours), Convert.ToDecimal(totalHours).ToString("N1") & "h", "0h")
                Label8.Text = "Total hours this month"

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

                ' --- 4. ANOMALY DETECTION (Quick Check) ---
                DetectAnomalies(conn)

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading payroll statistics: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DetectAnomalies(conn As MySqlConnection)
        ' Flag records where Overtime > 50% of Basic Salary OR Deductions > 30% of Basic Salary
        Dim query As String = "SELECT COUNT(*) FROM payroll WHERE Status = 'Paid' AND (Overtime > (BasicSalary * 0.5) OR Deductions > (BasicSalary * 0.3))"
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
                Dim series As New Series("Monthly Payroll Cost")
                series.ChartType = SeriesChartType.Column
                series.Color = Color.MediumSlateBlue
                series.IsValueShownAsLabel = True
                series.LabelFormat = "₱#,##0"

                ' Load payroll cost trends for the last 6 months
                Dim query As String = "SELECT MonthLabel, TotalNetPay FROM (" &
                                    "  SELECT DATE_FORMAT(PayPeriodStart, '%b %Y') as MonthLabel, " &
                                    "  SUM(BasicSalary + Overtime + Bonuses - Deductions) as TotalNetPay, " &
                                    "  MAX(PayPeriodStart) as MaxDate " &
                                    "  FROM payroll " &
                                    "  WHERE Status = 'Paid' " &
                                    "  GROUP BY YEAR(PayPeriodStart), MONTH(PayPeriodStart), MonthLabel " &
                                    "  ORDER BY MaxDate DESC " &
                                    "  LIMIT 6" &
                                    ") t ORDER BY MaxDate ASC"

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
            MessageBox.Show("Error loading trend chart: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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