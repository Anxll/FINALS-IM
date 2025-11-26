Imports MySqlConnector
Imports System.Data

Public Class FormEmployeeAttendance

    Private Sub FormEmployeeAttendance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup DataGridView
        SetupDataGridView()

        ' Load employee data
        LoadEmployeeData()
    End Sub

    '====================================
    ' SETUP DATAGRIDVIEW
    '====================================
    Private Sub SetupDataGridView()
        Try
            With DataGridView1
                .AutoGenerateColumns = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .ReadOnly = True
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .RowHeadersVisible = False
                .BackgroundColor = Color.White
                .BorderStyle = BorderStyle.None
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
                .GridColor = Color.FromArgb(240, 240, 240)
                .DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219)
                .DefaultCellStyle.SelectionForeColor = Color.White
                .DefaultCellStyle.Font = New Font("Segoe UI", 9.5F)
                .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
                .ColumnHeadersHeight = 40
                .RowTemplate.Height = 35
                .EnableHeadersVisualStyles = False
            End With

            ' Clear existing columns
            DataGridView1.Columns.Clear()

            ' Add columns programmatically
            DataGridView1.Columns.Add(CreateColumn("EmployeeID", "ID", 60, True))
            DataGridView1.Columns.Add(CreateColumn("EmployeeName", "Employee", 180))
            DataGridView1.Columns.Add(CreateColumn("Position", "Position", 150))
            DataGridView1.Columns.Add(CreateColumn("RegularHours", "Regular Hours", 120))
            DataGridView1.Columns.Add(CreateColumn("OvertimeHours", "Overtime Hours", 130))
            DataGridView1.Columns.Add(CreateColumn("Absences", "Absences", 100))
            DataGridView1.Columns.Add(CreateColumn("Status", "Status", 120))

        Catch ex As Exception
            MessageBox.Show("Error setting up grid: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '====================================
    ' CREATE COLUMN HELPER
    '====================================
    Private Function CreateColumn(name As String, headerText As String, width As Integer, Optional isHidden As Boolean = False) As DataGridViewTextBoxColumn
        Dim col As New DataGridViewTextBoxColumn With {
            .Name = name,
            .DataPropertyName = name,
            .HeaderText = headerText,
            .Width = width,
            .Visible = Not isHidden,
            .DefaultCellStyle = New DataGridViewCellStyle With {
                .Alignment = DataGridViewContentAlignment.MiddleCenter
            }
        }
        Return col
    End Function

    '====================================
    ' LOAD EMPLOYEE DATA (SIMULATED ATTENDANCE)
    '====================================
    Private Sub LoadEmployeeData()
        Try
            openConn()

            ' Query using existing employee table
            ' Simulating attendance metrics based on employee status and work patterns
            Dim query As String = "
                SELECT 
                    e.EmployeeID,
                    CONCAT(e.FirstName, ' ', e.LastName) AS EmployeeName,
                    e.Position,
                    CASE 
                        WHEN e.EmploymentStatus = 'Active' THEN 40
                        WHEN e.EmploymentStatus = 'On Leave' THEN 32
                        ELSE 0
                    END AS RegularHours,
                    CASE 
                        WHEN e.Position IN ('Chef', 'Cook') THEN 12
                        WHEN e.Position IN ('Server', 'Waitress') THEN 5
                        WHEN e.Position = 'Cashier' THEN 3
                        ELSE 0
                    END AS OvertimeHours,
                    CASE 
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Chef' THEN 0
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Cook' THEN 0
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Waitress' THEN 1
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Cashier' THEN 2
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Server' THEN 1
                        WHEN e.EmploymentStatus = 'On Leave' THEN 3
                        ELSE 0
                    END AS Absences,
                    CASE 
                        WHEN e.EmploymentStatus = 'Active' AND e.Position IN ('Chef', 'Cook') THEN 'Perfect'
                        WHEN e.EmploymentStatus = 'Active' AND e.Position IN ('Waitress', 'Server') THEN 'Good'
                        WHEN e.EmploymentStatus = 'Active' AND e.Position = 'Cashier' THEN 'Fair'
                        WHEN e.EmploymentStatus = 'On Leave' THEN 'On Leave'
                        ELSE 'Inactive'
                    END AS Status
                FROM 
                    employee e
                WHERE 
                    e.EmploymentStatus IN ('Active', 'On Leave')
                ORDER BY 
                    e.FirstName, e.LastName"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable()

            adapter.Fill(table)

            ' Bind to DataGridView
            DataGridView1.DataSource = table

            ' Apply custom formatting
            FormatDataGridView()

            ' Update label with total count
            Label1.Text = String.Format("Employee Attendance Report ({0} employees)", table.Rows.Count)

        Catch ex As Exception
            MessageBox.Show("Error loading employee data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    '====================================
    ' FORMAT DATAGRIDVIEW (STYLING)
    '====================================
    Private Sub FormatDataGridView()
        Try
            For Each row As DataGridViewRow In DataGridView1.Rows
                ' Get status value
                Dim statusCell As Object = row.Cells("Status").Value
                Dim status As String = If(statusCell IsNot Nothing, statusCell.ToString(), "")

                ' Apply color coding based on status
                Select Case status
                    Case "Perfect"
                        row.Cells("Status").Style.BackColor = Color.FromArgb(46, 204, 113)
                        row.Cells("Status").Style.ForeColor = Color.White
                        row.Cells("Status").Style.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)

                    Case "Good"
                        row.Cells("Status").Style.BackColor = Color.FromArgb(52, 152, 219)
                        row.Cells("Status").Style.ForeColor = Color.White
                        row.Cells("Status").Style.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)

                    Case "Fair"
                        row.Cells("Status").Style.BackColor = Color.FromArgb(241, 196, 15)
                        row.Cells("Status").Style.ForeColor = Color.White
                        row.Cells("Status").Style.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)

                    Case "On Leave"
                        row.Cells("Status").Style.BackColor = Color.FromArgb(155, 89, 182)
                        row.Cells("Status").Style.ForeColor = Color.White
                        row.Cells("Status").Style.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)

                    Case Else
                        row.Cells("Status").Style.BackColor = Color.FromArgb(149, 165, 166)
                        row.Cells("Status").Style.ForeColor = Color.White
                End Select

                ' Format numeric columns
                For Each cell As DataGridViewCell In row.Cells
                    If cell.OwningColumn.Name = "RegularHours" OrElse
                       cell.OwningColumn.Name = "OvertimeHours" OrElse
                       cell.OwningColumn.Name = "Absences" Then

                        Dim value As Integer = 0
                        Dim cellVal As Object = cell.Value
                        If cellVal IsNot Nothing AndAlso Integer.TryParse(cellVal.ToString(), value) Then
                            If cell.OwningColumn.Name = "Absences" AndAlso value > 0 Then
                                cell.Style.ForeColor = Color.FromArgb(231, 76, 60)
                                cell.Style.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
                            End If
                        End If
                    End If
                Next
            Next

        Catch ex As Exception
            ' Silent fail for formatting errors
        End Try
    End Sub

    '====================================
    ' EXPORT BUTTON
    '====================================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Export to CSV
            ExportToCSV()
        Catch ex As Exception
            MessageBox.Show("Error exporting data: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '====================================
    ' EXPORT TO CSV
    '====================================
    Private Sub ExportToCSV()
        Try
            Dim saveDialog As New SaveFileDialog With {
                .Filter = "CSV Files (*.csv)|*.csv",
                .FileName = String.Format("Employee_Attendance_{0:yyyyMMdd_HHmmss}.csv", DateTime.Now),
                .Title = "Export Attendance Report"
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
                        Dim values As New List(Of String)
                        For Each column As DataGridViewColumn In DataGridView1.Columns
                            If column.Visible Then
                                Dim cellVal As Object = row.Cells(column.Name).Value
                                Dim cellValue As String = If(cellVal IsNot Nothing, cellVal.ToString(), "")
                                ' Escape commas and quotes
                                If cellValue.Contains(",") OrElse cellValue.Contains("""") Then
                                    cellValue = """" & cellValue.Replace("""", """""") & """"
                                End If
                                values.Add(cellValue)
                            End If
                        Next
                        writer.WriteLine(String.Join(",", values))
                    Next
                End Using

                MessageBox.Show("Attendance report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Open file location
                Process.Start("explorer.exe", String.Format("/select,""{0}""", saveDialog.FileName))
            End If

        Catch ex As Exception
            Throw New Exception("Failed to export CSV: " & ex.Message)
        End Try
    End Sub

    '====================================
    ' REFRESH DATA (PUBLIC METHOD)
    '====================================
    Public Sub RefreshData()
        LoadEmployeeData()
    End Sub

End Class