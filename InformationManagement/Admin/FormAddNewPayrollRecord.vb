Imports MySqlConnector

Public Class FormAddNewPayrollRecord
    Private cmbEmployee As New ComboBox()

    Private Sub FormAddNewPayrollRecord_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup dynamic ComboBox for Employee selection
        cmbEmployee.Parent = Me
        cmbEmployee.Location = RoundedTextBox1.Location
        cmbEmployee.Size = RoundedTextBox1.Size
        cmbEmployee.Font = New Font("Segoe UI", 12)
        cmbEmployee.DropDownStyle = ComboBoxStyle.DropDownList
        cmbEmployee.FlatStyle = FlatStyle.Flat
        cmbEmployee.BackColor = Color.WhiteSmoke
        cmbEmployee.BringToFront()

        ' Hide the original textbox
        RoundedTextBox1.Visible = False

        LoadEmployees()
        LoadPayPeriods()
    End Sub

    Private Sub LoadEmployees()
        Try
            openConn()
            ' CORRECTED: Use EmploymentStatus instead of Status
            Dim query As String = "SELECT EmployeeID, CONCAT(FirstName, ' ', LastName) AS FullName, Position FROM employee WHERE EmploymentStatus = 'Active'"
            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            cmbEmployee.DisplayMember = "FullName"
            cmbEmployee.ValueMember = "EmployeeID"
            cmbEmployee.DataSource = dt
            
            ' Auto-select position when employee changes (optional)
            AddHandler cmbEmployee.SelectedIndexChanged, AddressOf CmbEmployee_SelectedIndexChanged

        Catch ex As Exception
            MessageBox.Show("Error loading employees: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub CmbEmployee_SelectedIndexChanged(sender As Object, e As EventArgs)
        If cmbEmployee.SelectedItem IsNot Nothing Then
            Dim row As DataRowView = CType(cmbEmployee.SelectedItem, DataRowView)
            cmbPosition.Text = row("Position").ToString()
        End If
    End Sub

    Private Sub LoadPayPeriods()
        ' Generate some pay periods for selection
        Dim today As DateTime = DateTime.Now
        For i As Integer = 0 To 5
            Dim d As DateTime = today.AddMonths(-i)
            Dim period1 As String = d.ToString("MMMM") & " 1-15, " & d.Year
            Dim period2 As String = d.ToString("MMMM") & " 16-" & DateTime.DaysInMonth(d.Year, d.Month) & ", " & d.Year
            cmbPayperiod.Items.Add(period1)
            cmbPayperiod.Items.Add(period2)
        Next
        cmbPayperiod.SelectedIndex = 0
    End Sub

    Private Sub btnCreateRecord_Click(sender As Object, e As EventArgs) Handles btnCreateRecord.Click
        If cmbEmployee.SelectedIndex = -1 Then
            MessageBox.Show("Please select an employee.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If cmbPayperiod.Text = "" Then
            MessageBox.Show("Please select a pay period.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim employeeID As Integer = Convert.ToInt32(cmbEmployee.SelectedValue)
            Dim hoursWorked As Decimal = NumericUpDown1.Value
            Dim overtimeHours As Decimal = NumericUpDown2.Value
            Dim hourlyRate As Decimal = NumericUpDown3.Value
            Dim deductions As Decimal = If(Me.Controls.ContainsKey("NumericUpDown4"), CType(Me.Controls("NumericUpDown4"), NumericUpDown).Value, 0)
            Dim bonuses As Decimal = If(Me.Controls.ContainsKey("NumericUpDown5"), CType(Me.Controls("NumericUpDown5"), NumericUpDown).Value, 0)
            
            Dim basicSalary As Decimal = hoursWorked * hourlyRate
            Dim overtimePay As Decimal = overtimeHours * hourlyRate * 1.5D ' 1.5x overtime rate
            
            ' Parse Pay Period dates
            Dim periodStr As String = cmbPayperiod.Text
            Dim parts() As String = periodStr.Split(","c) ' "November 1-15", " 2023"
            Dim year As Integer = Convert.ToInt32(parts(1).Trim())
            Dim monthPart As String = parts(0).Split(" "c)(0)
            Dim dayPart As String = parts(0).Split(" "c)(1) ' "1-15"
            Dim startDay As Integer = Convert.ToInt32(dayPart.Split("-"c)(0))
            Dim endDay As Integer = Convert.ToInt32(dayPart.Split("-"c)(1))
            Dim month As Integer = DateTime.ParseExact(monthPart, "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month

            Dim startDate As New DateTime(year, month, startDay)
            Dim endDate As New DateTime(year, month, endDay)

            openConn()
            
            ' Check if new columns exist
            Dim checkQuery As String = "SHOW COLUMNS FROM payroll LIKE 'HoursWorked'"
            Dim checkCmd As New MySqlCommand(checkQuery, conn)
            Dim hasNewColumns As Boolean = checkCmd.ExecuteScalar() IsNot Nothing
            
            Dim query As String = ""
            Dim cmd As New MySqlCommand()
            cmd.Connection = conn
            
            If hasNewColumns Then
                query = "INSERT INTO payroll 
                    (EmployeeID, PayPeriodStart, PayPeriodEnd, HoursWorked, HourlyRate, BasicSalary, Overtime, Deductions, Bonuses, Status, CreatedDate) 
                    VALUES (@empID, @start, @end, @hours, @rate, @basic, @overtime, @deductions, @bonuses, 'Pending', NOW())"
                
                cmd.Parameters.AddWithValue("@hours", hoursWorked)
                cmd.Parameters.AddWithValue("@rate", hourlyRate)
                cmd.Parameters.AddWithValue("@deductions", deductions)
                cmd.Parameters.AddWithValue("@bonuses", bonuses)
            Else
                query = "INSERT INTO payroll 
                    (EmployeeID, PayPeriodStart, PayPeriodEnd, BasicSalary, Overtime, Deductions, Bonuses, Status, CreatedDate) 
                    VALUES (@empID, @start, @end, @basic, @overtime, 0, 0, 'Pending', NOW())"
            End If
            
            cmd.CommandText = query
            cmd.Parameters.AddWithValue("@empID", employeeID)
            cmd.Parameters.AddWithValue("@start", startDate)
            cmd.Parameters.AddWithValue("@end", endDate)
            cmd.Parameters.AddWithValue("@basic", basicSalary)
            cmd.Parameters.AddWithValue("@overtime", overtimePay)

            cmd.ExecuteNonQuery()
            closeConn()

            MessageBox.Show("Payroll record created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            
            ' Refresh Payroll Grid if open
            If Application.OpenForms().OfType(Of Payroll).Any() Then
                Application.OpenForms().OfType(Of Payroll)().First().LoadPayroll()
            End If

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error creating payroll record: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class
