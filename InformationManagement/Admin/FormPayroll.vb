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

                ' Get total payroll (Paid records only)
                ' Calculate NetPay dynamically to be safe: (Basic + Overtime + Bonuses - Deductions)
                Dim cmdTotalPayroll As New MySqlCommand("SELECT IFNULL(SUM(BasicSalary + Overtime + Bonuses - Deductions), 0) FROM payroll WHERE Status = 'Paid'", conn)
                Dim totalPayroll As Object = cmdTotalPayroll.ExecuteScalar()
                If totalPayroll IsNot Nothing AndAlso Not IsDBNull(totalPayroll) Then
                    Label4.Text = "₱" & Convert.ToDecimal(totalPayroll).ToString("N2")
                Else
                    Label4.Text = "₱0.00"
                End If

                ' Get total hours from actual payroll records (Paid records only)
                Dim cmdTotalHours As New MySqlCommand("SELECT IFNULL(SUM(HoursWorked), 0) FROM payroll WHERE Status = 'Paid'", conn)
                Dim totalHours As Object = cmdTotalHours.ExecuteScalar()
                If totalHours IsNot Nothing AndAlso Not IsDBNull(totalHours) Then
                    Label6.Text = Convert.ToDecimal(totalHours).ToString("N2")
                Else
                    Label6.Text = "0"
                End If

                ' Get active employees count (Total workforce)
                Dim cmdActiveEmployees As New MySqlCommand("SELECT COUNT(*) FROM employee WHERE EmploymentStatus = 'Active'", conn)
                Dim activeEmployees As Object = cmdActiveEmployees.ExecuteScalar()
                If activeEmployees IsNot Nothing AndAlso Not IsDBNull(activeEmployees) Then
                    Label7.Text = activeEmployees.ToString()
                Else
                    Label7.Text = "0"
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading payroll data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadPayrollChart()
        Try
            Using conn As New MySqlConnection(modDB.strConnection)
                conn.Open()

                ' Clear existing series
                Chart1.Series.Clear()

                ' Create new series
                Dim series As New Series("Paid Payroll")
                series.ChartType = SeriesChartType.Column
                series.Color = Color.MediumSlateBlue

                ' Load actual paid amounts per employee
                Dim query As String = "SELECT CONCAT(e.FirstName, ' ', e.LastName) AS FullName, " &
                                    "SUM(p.BasicSalary + p.Overtime + p.Bonuses - p.Deductions) AS TotalPay " &
                                    "FROM payroll p " &
                                    "JOIN employee e ON p.EmployeeID = e.EmployeeID " &
                                    "WHERE p.Status = 'Paid' " &
                                    "GROUP BY p.EmployeeID, FullName " &
                                    "ORDER BY TotalPay DESC"

                Dim cmdPayrollBreakdown As New MySqlCommand(query, conn)

                Using reader As MySqlDataReader = cmdPayrollBreakdown.ExecuteReader()
                    While reader.Read()
                        Dim salaryValue As Decimal = 0D
                        If Not reader.IsDBNull(reader.GetOrdinal("TotalPay")) Then
                            salaryValue = Convert.ToDecimal(reader("TotalPay"))
                        End If
                        series.Points.AddXY(reader("FullName").ToString(), salaryValue)
                    End While
                End Using

                If series.Points.Count = 0 Then
                    series.Points.AddXY("No Paid Records", 0)
                End If

                Chart1.Series.Add(series)

                ' Configure chart appearance
                Chart1.ChartAreas(0).AxisX.MajorGrid.Enabled = False
                Chart1.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.LightGray
                Chart1.Legends(0).Enabled = False

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading chart data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Export payroll data
        Try
            Using conn As New MySqlConnection(modDB.strConnection)
                conn.Open()

                Dim query As String = "SELECT p.PayrollID, e.FirstName, e.LastName, e.Position, " &
                                    "p.PayPeriodStart, p.PayPeriodEnd, " &
                                    "p.HoursWorked, p.HourlyRate, " &
                                    "p.BasicSalary, p.Overtime, p.Bonuses, p.Deductions, " &
                                    "(p.BasicSalary + p.Overtime + p.Bonuses - p.Deductions) AS NetPay, " &
                                    "p.Status, p.CreatedDate " &
                                    "FROM payroll p " &
                                    "JOIN employee e ON p.EmployeeID = e.EmployeeID " &
                                    "WHERE p.Status = 'Paid' " &
                                    "ORDER BY p.CreatedDate DESC"

                Dim cmdExport As New MySqlCommand(query, conn)

                Dim dt As New DataTable()
                Dim adapter As New MySqlDataAdapter(cmdExport)
                adapter.Fill(dt)

                If dt.Rows.Count = 0 Then
                    MessageBox.Show("No paid payroll records to export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' Save to CSV
                Dim saveDialog As New SaveFileDialog()
                saveDialog.Filter = "CSV Files (*.csv)|*.csv"
                saveDialog.FileName = "Payroll_Report_Paid_" & DateTime.Now.ToString("yyyyMMdd") & ".csv"

                If saveDialog.ShowDialog() = DialogResult.OK Then
                    Dim csv As New System.Text.StringBuilder()

                    ' Add header
                    Dim headerLine As String = String.Join(",", dt.Columns.Cast(Of DataColumn)().Select(Function(column) column.ColumnName))
                    csv.AppendLine(headerLine)

                    ' Add rows
                    For Each row As DataRow In dt.Rows
                        Dim fields = row.ItemArray.Select(Function(field) String.Format("""{0}""", field.ToString().Replace("""", """""")))
                        csv.AppendLine(String.Join(",", fields))
                    Next

                    System.IO.File.WriteAllText(saveDialog.FileName, csv.ToString())
                    MessageBox.Show("Payroll report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("Error exporting data: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
    End Sub
End Class