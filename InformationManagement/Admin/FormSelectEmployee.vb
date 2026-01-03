Imports MySqlConnector

Public Class FormSelectEmployee
    Public Property SelectedEmployeeID As Integer = 0
    Public Property SelectedEmployeeName As String = ""
    Public Property SelectedEmployeeRole As String = ""
    
    
    Private employeesTable As DataTable
    Private Const PLACEHOLDER_TEXT As String = "Search by name..."

    Private Sub FormSelectEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RoundButton(btnSelect)
        RoundButton(btnCancel)
        SetupSearchPlaceholder()
        LoadEmployees()
    End Sub

    Private Sub SetupSearchPlaceholder()
        txtSearch.Text = PLACEHOLDER_TEXT
        txtSearch.ForeColor = Color.Gray
    End Sub

    Private Sub txtSearch_GotFocus(sender As Object, e As EventArgs) Handles txtSearch.GotFocus
        If txtSearch.Text = PLACEHOLDER_TEXT Then
            txtSearch.Text = ""
            txtSearch.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtSearch_LostFocus(sender As Object, e As EventArgs) Handles txtSearch.LostFocus
        If String.IsNullOrWhiteSpace(txtSearch.Text) Then
            txtSearch.Text = PLACEHOLDER_TEXT
            txtSearch.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub LoadEmployees()
        Try
            openConn()
            Dim query As String = "
                SELECT 
                    EmployeeID, 
                    CONCAT(FirstName, ' ', LastName) as FullName,
                    Position, 
                    Email 
                FROM employee 
                WHERE EmploymentStatus = 'Active' 
                AND Position = 'Employee'
                AND EmployeeID NOT IN (SELECT IFNULL(employee_id,0) FROM user_accounts)
                ORDER BY FirstName"
            
            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            employeesTable = New DataTable()
            adapter.Fill(employeesTable)
            
            dgvEmployees.Rows.Clear()
            For Each row As DataRow In employeesTable.Rows
                dgvEmployees.Rows.Add(
                    row("EmployeeID"),
                    row("FullName"),
                    row("Position"),
                    row("Email")
                )
            Next
            
            closeConn()
        Catch ex As Exception
            MessageBox.Show("Error loading employees: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            closeConn()
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If employeesTable Is Nothing Then Return
        
        Dim filter As String = txtSearch.Text.Trim().ToLower()
        
        ' Ignore if it is the placeholder text
        If txtSearch.Text = PLACEHOLDER_TEXT Then filter = ""
        
        dgvEmployees.Rows.Clear()
        
        For Each row As DataRow In employeesTable.Rows
            Dim name As String = row("FullName").ToString().ToLower()
            Dim position As String = row("Position").ToString().ToLower()
            
            If name.Contains(filter) OrElse position.Contains(filter) Then
                dgvEmployees.Rows.Add(
                    row("EmployeeID"),
                    row("FullName"),
                    row("Position"),
                    row("Email")
                )
            End If
        Next
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If dgvEmployees.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an employee.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        
        Dim row As DataGridViewRow = dgvEmployees.SelectedRows(0)
        SelectedEmployeeID = Convert.ToInt32(row.Cells("colID").Value)
        SelectedEmployeeName = row.Cells("colName").Value.ToString()
        SelectedEmployeeRole = row.Cells("colPosition").Value.ToString()
        
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub RoundButton(btn As Button)
        Dim radius As Integer = 8
        Dim path As New Drawing2D.GraphicsPath()
        path.StartFigure()
        path.AddArc(New Rectangle(0, 0, radius, radius), 180, 90)
        path.AddArc(New Rectangle(btn.Width - radius, 0, radius, radius), 270, 90)
        path.AddArc(New Rectangle(btn.Width - radius, btn.Height - radius, radius, radius), 0, 90)
        path.AddArc(New Rectangle(0, btn.Height - radius, radius, radius), 90, 90)
        path.CloseFigure()
        btn.Region = New Region(path)
    End Sub
End Class
