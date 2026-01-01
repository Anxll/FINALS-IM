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
                Dim sYear As Integer = Reports.SelectedYear
                Dim sMonth As Integer = Reports.SelectedMonth
                If sMonth = 0 Then sMonth = DateTime.Now.Month ' Default to current month if "All" is selected for payroll summary

                ' --- 1. TOTAL PAYROLL & TRENDS ---
                ' Selected Month Total
                Dim queryCurrentMonth As String = "SELECT IFNULL(SUM(BasicSalary + Overtime + Bonuses - Deductions), 0) FROM payroll " &
                                                 $"WHERE Status = 'Paid' AND MONTH(PayPeriodStart) = {sMonth} AND YEAR(PayPeriodStart) = {sYear}"
                Dim cmdCurrentMonth As New MySqlCommand(queryCurrentMonth, conn)
                Dim currentPayroll As Decimal = Convert.ToDecimal(cmdCurrentMonth.ExecuteScalar())

                ' Previous Month Total (calculated relative to selected month/year)
                Dim prevDate As DateTime = New DateTime(sYear, sMonth, 1).AddMonths(-1)
                Dim queryPrevMonth As String = "SELECT IFNULL(SUM(BasicSalary + Overtime + Bonuses - Deductions), 0) FROM payroll " &
                                              $"WHERE Status = 'Paid' AND MONTH(PayPeriodStart) = {prevDate.Month} " &
                                              $"AND YEAR(PayPeriodStart) = {prevDate.Year}"
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
                Dim cmdTotalHours As New MySqlCommand($"SELECT IFNULL(SUM(HoursWorked), 0) FROM payroll WHERE Status = 'Paid' AND MONTH(PayPeriodStart) = {sMonth} AND YEAR(PayPeriodStart) = {sYear}", conn)
                Dim totalHours As Object = cmdTotalHours.ExecuteScalar()
                Label6.Text = If(totalHours IsNot Nothing AndAlso Not IsDBNull(totalHours), Convert.ToDecimal(totalHours).ToString("N1") & "h", "0h")
                Label8.Text = $"Total hours for {New DateTime(sYear, sMonth, 1):MMM yyyy}"

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

                ' Load payroll cost trends for 6 months ending at the selected date
                Dim sYear As Integer = Reports.SelectedYear
                Dim sMonth As Integer = Reports.SelectedMonth
                If sMonth = 0 Then sMonth = 12 ' End of year if "All" is selected
                
                Dim endDate As String = $"{sYear}-{sMonth:D2}-28" ' Safe end date for the selected month

                Dim query As String = "SELECT MonthLabel, TotalNetPay, MaxDate FROM (" &
                                    "  SELECT DATE_FORMAT(PayPeriodStart, '%b %Y') as MonthLabel, " &
                                    "  SUM(BasicSalary + Overtime + Bonuses - Deductions) as TotalNetPay, " &
                                    "  MAX(PayPeriodStart) as MaxDate " &
                                    "  FROM payroll " &
                                    "  WHERE Status = 'Paid' " &
                                    $"  AND PayPeriodStart <= '{endDate}' " &
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

    Private Sub btnExportPdf_Click(sender As Object, e As EventArgs) Handles btnExportPdf.Click
        If Reports.Instance IsNot Nothing Then
            Reports.Instance.ExportCurrentReport()
        Else
            MessageBox.Show("Please open the Reports screen to export.", "PDF Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
    End Sub
    ' =======================================================================
    ' REFRESH DATA
    ' =======================================================================
    Public Sub RefreshData()
        LoadPayrollData()
        LoadPayrollChart()
    End Sub

End Class