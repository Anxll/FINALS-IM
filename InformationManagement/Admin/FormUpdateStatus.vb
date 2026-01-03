Imports MySqlConnector

Public Class FormUpdateStatus
    Public Property TargetEmployeeID As Integer
    Public Property TargetUserID As Integer

    Private Sub FormUpdateStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Round buttons
        RoundButton(btnSave)
        RoundButton(btnCancel)
    End Sub

    Public Sub LoadData(userId As Integer, empId As Integer, name As String, currentStatus As String)
        TargetUserID = userId
        TargetEmployeeID = empId
        lblEmployeeName.Text = name
        
        If Not String.IsNullOrEmpty(currentStatus) AndAlso cmbStatus.Items.Contains(currentStatus.Trim()) Then
            cmbStatus.SelectedItem = currentStatus.Trim()
        Else
            cmbStatus.SelectedIndex = 0 ' Default Active
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cmbStatus.SelectedIndex = -1 Then
            MessageBox.Show("Please select a status.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim newStatus As String = cmbStatus.SelectedItem.ToString()

        Try
            openConn()
            
            ' 1. Update Employee Table
            If TargetEmployeeID > 0 Then
                Dim empQuery As String = "UPDATE employee SET EmploymentStatus = @status WHERE EmployeeID = @eid"
                Dim empCmd As New MySqlCommand(empQuery, conn)
                empCmd.Parameters.AddWithValue("@status", newStatus)
                empCmd.Parameters.AddWithValue("@eid", TargetEmployeeID)
                empCmd.ExecuteNonQuery()
            End If

            ' 2. Update User Account Table (Sync)
            If TargetUserID > 0 Then
                ' Also update status in user_accounts to ensure login restriction works
                Dim userQuery As String = "UPDATE user_accounts SET status = @status WHERE id = @uid"
                Dim userCmd As New MySqlCommand(userQuery, conn)
                userCmd.Parameters.AddWithValue("@status", newStatus)
                userCmd.Parameters.AddWithValue("@uid", TargetUserID)
                userCmd.ExecuteNonQuery()
            End If

            closeConn()
            MessageBox.Show("Status updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error updating status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub RoundButton(btn As Button)
        Dim radius As Integer = 10
        Dim path As New Drawing2D.GraphicsPath()
        path.StartFigure()
        path.AddArc(New Rectangle(0, 0, radius, radius), 180, 90)
        path.AddArc(New Rectangle(btn.Width - radius, 0, radius, radius), 270, 90)
        path.AddArc(New Rectangle(btn.Width - radius, btn.Height - radius, radius, radius), 0, 90)
        path.AddArc(New Rectangle(0, btn.Height - radius, radius, radius), 90, 90)
        path.CloseFigure()
        btn.Region = New Region(path)
    End Sub

    Private Sub cmbStatus_DrawItem(sender As Object, e As DrawItemEventArgs) Handles cmbStatus.DrawItem
        If e.Index < 0 Then Return
        Dim cmb As ComboBox = DirectCast(sender, ComboBox)
        e.DrawBackground()
        e.Graphics.DrawString(cmb.Items(e.Index).ToString(), cmb.Font, Brushes.Black, e.Bounds)
        e.DrawFocusRectangle()
    End Sub
End Class
