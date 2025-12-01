Imports MySqlConnector

Public Class Payroll
    Private Sub Payroll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPayroll()
    End Sub

    Public Sub LoadPayroll()
        Try
            openConn()
            
            ' Try to check if new columns exist
            Dim checkQuery As String = "SHOW COLUMNS FROM payroll LIKE 'HoursWorked'"
            Dim checkCmd As New MySqlCommand(checkQuery, conn)
            Dim hasNewColumns As Boolean = checkCmd.ExecuteScalar() IsNot Nothing
            
            Dim query As String = ""
            If hasNewColumns Then
                ' New schema with HoursWorked and HourlyRate
                query = "SELECT p.PayrollID, CONCAT(e.FirstName, ' ', e.LastName) AS EmployeeName, e.Position, 
                          p.HoursWorked, p.HourlyRate, p.BasicSalary, p.Overtime, p.NetPay, p.Status,
                          p.PayPeriodStart, p.PayPeriodEnd, 
                          IFNULL(p.Deductions, 0) as Deductions, IFNULL(p.Bonuses, 0) as Bonuses
                          FROM payroll p
                          JOIN employee e ON p.EmployeeID = e.EmployeeID
                          ORDER BY p.CreatedDate DESC"
            Else
                ' Old schema without new columns
                query = "SELECT p.PayrollID, CONCAT(e.FirstName, ' ', e.LastName) AS EmployeeName, e.Position, 
                          p.BasicSalary, p.Overtime, p.NetPay, p.Status,
                          p.PayPeriodStart, p.PayPeriodEnd
                          FROM payroll p
                          JOIN employee e ON p.EmployeeID = e.EmployeeID
                          ORDER BY p.CreatedDate DESC"
            End If
            
            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            DataGridView1.Rows.Clear()
            Dim totalGross As Decimal = 0
            Dim totalNet As Decimal = 0
            Dim empCount As Integer = dt.Rows.Count
            Dim sumHours As Decimal = 0

            For Each row As DataRow In dt.Rows
                Dim rowIndex As Integer = DataGridView1.Rows.Add()
                Dim newRow As DataGridViewRow = DataGridView1.Rows(rowIndex)
                
                newRow.Cells("Employee").Value = row("EmployeeName").ToString()
                newRow.Cells("Position").Value = row("Position").ToString()
                
                ' Display hours and rate if available
                If hasNewColumns Then
                    Dim hours As Decimal = If(row("HoursWorked") IsNot DBNull.Value, Convert.ToDecimal(row("HoursWorked")), 0)
                    Dim rate As Decimal = If(row("HourlyRate") IsNot DBNull.Value, Convert.ToDecimal(row("HourlyRate")), 0)
                    
                    newRow.Cells("Hours").Value = hours.ToString("F2")
                    newRow.Cells("HourlyRate").Value = rate.ToString("C2")
                    sumHours += hours
                Else
                    newRow.Cells("Hours").Value = "N/A"
                    newRow.Cells("HourlyRate").Value = "N/A"
                End If
                
                newRow.Cells("Overtime").Value = Convert.ToDecimal(row("Overtime")).ToString("C2")
                
                Dim gross As Decimal = Convert.ToDecimal(row("BasicSalary")) + Convert.ToDecimal(row("Overtime"))
                newRow.Cells("GrossPay").Value = gross.ToString("C2")
                newRow.Cells("NetPay").Value = Convert.ToDecimal(row("NetPay")).ToString("C2")
                newRow.Cells("Status").Value = row("Status").ToString()
                newRow.Cells("Actions").Value = "Edit"
                
                ' Store PayrollID in Tag for editing
                newRow.Tag = row("PayrollID")
                
                totalGross += gross
                totalNet += Convert.ToDecimal(row("NetPay"))
            Next
            
            lblTotalGrossPay.Text = totalGross.ToString("C2")
            lblTotalNetPay.Text = totalNet.ToString("C2")
            TotalHours.Text = If(hasNewColumns, sumHours.ToString("F2") & " hrs", "N/A")
            E.Text = empCount.ToString()
            
            If empCount = 0 Then
                ' Optional: Uncomment to debug if needed, but might be annoying on load if just empty
                ' MessageBox.Show("No payroll records found. Please add a new record.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading payroll: " & ex.Message & vbCrLf & vbCrLf & 
                          "Stack Trace: " & ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim payrollID As Integer = If(selectedRow.Tag IsNot Nothing, Convert.ToInt32(selectedRow.Tag), 0)
            Dim status As String = If(selectedRow.Cells("Status").Value IsNot Nothing, selectedRow.Cells("Status").Value.ToString(), "")
            
            ' Check if Edit button clicked (assuming you have an Edit button column)
            ' For now, we'll use row double-click for edit
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim payrollID As Integer = If(selectedRow.Tag IsNot Nothing, Convert.ToInt32(selectedRow.Tag), 0)
            Dim status As String = If(selectedRow.Cells("Status").Value IsNot Nothing, selectedRow.Cells("Status").Value.ToString(), "")
            
            If status.ToLower() = "paid" Then
                MessageBox.Show("Cannot edit a payroll record that has already been paid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            
            ' TODO: Open edit form when created
            MessageBox.Show("Edit functionality coming soon! PayrollID: " & payrollID, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Public Sub UpdatePayrollStatus(payrollID As Integer, newStatus As String)
        Try
            openConn()
            Dim query As String = "UPDATE payroll SET Status = @status, ProcessedDate = NOW() WHERE PayrollID = @id"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@status", newStatus)
            cmd.Parameters.AddWithValue("@id", payrollID)
            cmd.ExecuteNonQuery()
            closeConn()
            
            LoadPayroll()
            MessageBox.Show("Payroll status updated to " & newStatus & "!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error updating status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub AddNewPayrollRecordbtn_Click(sender As Object, e As EventArgs) Handles AddNewPayrollRecordbtn.Click
        Dim form As New FormAddNewPayrollRecord()
        form.ShowDialog()
    End Sub
End Class
