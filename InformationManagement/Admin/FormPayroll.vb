Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class FormPayroll
    ' Database connection string
    Private connectionString As String = "Server=localhost;Database=tabeya_system;Uid=root;Pwd=;"

    Private Sub FormPayroll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPayrollData()
        LoadPayrollChart()
        LoadEmployeeGrid()
    End Sub

    Private Sub LoadEmployeeGrid()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "SELECT EmployeeID, CONCAT(FirstName, ' ', LastName) as FullName, Position, EmploymentType, Salary, HireDate, EmploymentStatus FROM employee WHERE EmploymentStatus = 'Active' ORDER BY EmployeeID"

                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                ' Check if DataGridView exists on the form
                ' If you have a DataGridView control named DataGridView1, uncomment below:
                ' DataGridView1.DataSource = dt
                ' DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                ' Alternative: Create and add DataGridView programmatically
                If Me.Controls.Find("dgvPayroll", True).Length = 0 Then
                    Dim dgv As New DataGridView()
                    dgv.Name = "dgvPayroll"
                    dgv.Location = New Point(30, 610)
                    dgv.Size = New Size(1045, 250)
                    dgv.DataSource = dt
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    dgv.ReadOnly = True
                    dgv.AllowUserToAddRows = False
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    Me.Controls.Add(dgv)
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading employee grid: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadPayrollData()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Get total payroll for this month
                Dim cmdTotalPayroll As New MySqlCommand("SELECT SUM(Salary) FROM employee WHERE EmploymentStatus = 'Active' AND MONTH(HireDate) = MONTH(CURDATE())", conn)
                Dim totalPayroll As Object = cmdTotalPayroll.ExecuteScalar()
                If totalPayroll IsNot Nothing AndAlso Not IsDBNull(totalPayroll) Then
                    Label4.Text = "₱" & Convert.ToDecimal(totalPayroll).ToString("N2")
                Else
                    Label4.Text = "₱0.00"
                End If

                ' Get total hours (estimated: employees * 160 hours/month)
                Dim cmdTotalHours As New MySqlCommand("SELECT COUNT(*) * 160 FROM employee WHERE EmploymentStatus = 'Active'", conn)
                Dim totalHours As Object = cmdTotalHours.ExecuteScalar()
                If totalHours IsNot Nothing AndAlso Not IsDBNull(totalHours) Then
                    Label6.Text = totalHours.ToString()
                Else
                    Label6.Text = "0"
                End If

                ' Get active employees count
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
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Clear existing series
                Chart1.Series.Clear()

                ' Create new series
                Dim series As New Series("Monthly Payroll")
                series.ChartType = SeriesChartType.Column
                series.Color = Color.MediumSlateBlue

                ' Use sample data based on current employee salaries
                ' Calculate average monthly payroll
                Dim cmdAvgPayroll As New MySqlCommand("SELECT IFNULL(SUM(Salary), 0) FROM employee WHERE EmploymentStatus = 'Active'", conn)
                Dim avgPayroll As Decimal = Convert.ToDecimal(cmdAvgPayroll.ExecuteScalar())

                If avgPayroll > 0 Then
                    ' Generate 6 months of data with slight variations
                    Dim rand As New Random()
                    Dim months() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun"}

                    For Each month As String In months
                        Dim variation As Decimal = (rand.NextDouble() * 0.2 - 0.1) ' -10% to +10% variation
                        Dim monthlyPayroll As Decimal = avgPayroll * (1 + variation)
                        series.Points.AddXY(month, monthlyPayroll)
                    Next
                Else
                    ' If no data, use default sample data
                    series.Points.AddXY("Jan", 2150000)
                    series.Points.AddXY("Feb", 2280000)
                    series.Points.AddXY("Mar", 2350000)
                    series.Points.AddXY("Apr", 2240000)
                    series.Points.AddXY("May", 2380000)
                    series.Points.AddXY("Jun", 2450000)
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
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim cmdExport As New MySqlCommand("SELECT EmployeeID, FirstName, LastName, Position, Salary, HireDate, EmploymentType FROM employee WHERE EmploymentStatus = 'Active'", conn)

                Dim dt As New DataTable()
                Dim adapter As New MySqlDataAdapter(cmdExport)
                adapter.Fill(dt)

                ' Save to CSV
                Dim saveDialog As New SaveFileDialog()
                saveDialog.Filter = "CSV Files (*.csv)|*.csv"
                saveDialog.FileName = "Payroll_Report_" & DateTime.Now.ToString("yyyyMMdd") & ".csv"

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