Imports MySqlConnector

Public Class FormEditPayroll
    Private payrollID As Integer
    Private employeeID As Integer
    Private employeeName As String
    
    Public Sub New(pID As Integer, empID As Integer, empName As String)
        InitializeComponent()
        Me.payrollID = pID
        Me.employeeID = empID
        Me.employeeName = empName
    End Sub
    
    Private Sub FormEditPayroll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblEmployeeName.Text = employeeName
        LoadPayrollData()
    End Sub
    
    Private Sub LoadPayrollData()
        Try
            openConn()
            Dim query As String = "SELECT * FROM payroll WHERE PayrollID = @id"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", payrollID)
            
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                ' Populate fields
                dtpPeriodStart.Value = Convert.ToDateTime(reader("PayPeriodStart"))
                dtpPeriodEnd.Value = Convert.ToDateTime(reader("PayPeriodEnd"))
                
                txtHours.Text = If(reader("HoursWorked") IsNot DBNull.Value, reader("HoursWorked").ToString(), "0")
                txtRate.Text = If(reader("HourlyRate") IsNot DBNull.Value, reader("HourlyRate").ToString(), "0")
                txtBasicSalary.Text = If(reader("BasicSalary") IsNot DBNull.Value, reader("BasicSalary").ToString(), "0")
                txtOvertime.Text = If(reader("Overtime") IsNot DBNull.Value, reader("Overtime").ToString(), "0")
                txtDeductions.Text = If(reader("Deductions") IsNot DBNull.Value, reader("Deductions").ToString(), "0")
                txtBonuses.Text = If(reader("Bonuses") IsNot DBNull.Value, reader("Bonuses").ToString(), "0")
                
                lblStatus.Text = reader("Status").ToString()
                
                CalculateNetPay()
            End If
            reader.Close()
            closeConn()
            
        Catch ex As Exception
            MessageBox.Show("Error loading payroll: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub
    
    Private Sub CalculateNetPay()
        Try
            Dim basic As Decimal = If(Decimal.TryParse(txtBasicSalary.Text, Nothing), Convert.ToDecimal(txtBasicSalary.Text), 0)
            Dim overtime As Decimal = If(Decimal.TryParse(txtOvertime.Text, Nothing), Convert.ToDecimal(txtOvertime.Text), 0)
            Dim deductions As Decimal = If(Decimal.TryParse(txtDeductions.Text, Nothing), Convert.ToDecimal(txtDeductions.Text), 0)
            Dim bonuses As Decimal = If(Decimal.TryParse(txtBonuses.Text, Nothing), Convert.ToDecimal(txtBonuses.Text), 0)
            
            Dim netPay As Decimal = basic + overtime + bonuses - deductions
            lblNetPay.Text = "₱" & netPay.ToString("N2")
        Catch ex As Exception
            lblNetPay.Text = "₱0.00"
        End Try
    End Sub
    
    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtBasicSalary.TextChanged, txtOvertime.TextChanged, txtDeductions.TextChanged, txtBonuses.TextChanged
        CalculateNetPay()
    End Sub
    
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            openConn()
            
            Dim query As String = "UPDATE payroll SET 
                PayPeriodStart = @start,
                PayPeriodEnd = @end,
                HoursWorked = @hours,
                HourlyRate = @rate,
                BasicSalary = @basic,
                Overtime = @overtime,
                Deductions = @deductions,
                Bonuses = @bonuses
                WHERE PayrollID = @id"
            
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@start", dtpPeriodStart.Value)
            cmd.Parameters.AddWithValue("@end", dtpPeriodEnd.Value)
            cmd.Parameters.AddWithValue("@hours", Convert.ToDecimal(txtHours.Text))
            cmd.Parameters.AddWithValue("@rate", Convert.ToDecimal(txtRate.Text))
            cmd.Parameters.AddWithValue("@basic", Convert.ToDecimal(txtBasicSalary.Text))
            cmd.Parameters.AddWithValue("@overtime", Convert.ToDecimal(txtOvertime.Text))
            cmd.Parameters.AddWithValue("@deductions", Convert.ToDecimal(txtDeductions.Text))
            cmd.Parameters.AddWithValue("@bonuses", Convert.ToDecimal(txtBonuses.Text))
            cmd.Parameters.AddWithValue("@id", payrollID)
            
            cmd.ExecuteNonQuery()
            closeConn()
            
            MessageBox.Show("Payroll updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            
            ' Refresh parent form
            If Application.OpenForms().OfType(Of Payroll).Any() Then
                Application.OpenForms().OfType(Of Payroll)().First().LoadEmployees()
            End If
            
            Me.Close()
            
        Catch ex As Exception
            MessageBox.Show("Error saving payroll: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub
    
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class
