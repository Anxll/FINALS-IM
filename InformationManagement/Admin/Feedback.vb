Imports MySql.Data.MySqlClient
Imports System.Data

Public Class Feedback
    ' Database connection string
    Private connectionString As String = "Server=localhost;Database=tabeya_system;Uid=root;Pwd=;"
    Private conn As MySqlConnection

    ' Form Load Event
    Private Sub Feedback_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeConnection()
        LoadFeedback()
        SetupDataGridView()
    End Sub

    ' Initialize Database Connection
    Private Sub InitializeConnection()
        Try
            If conn Is Nothing Then
                conn = New MySqlConnection(connectionString)
            End If
        Catch ex As Exception
            MessageBox.Show("Connection Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Setup DataGridView Appearance
    Private Sub SetupDataGridView()
        Try
            If DataGridView1 Is Nothing Then Return

            With DataGridView1
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ReadOnly = True
                .AllowUserToAddRows = False
                .RowHeadersVisible = False

                ' Add Action Buttons if not exists
                If Not .Columns.Contains("Approve") Then
                    Dim btnApprove As New DataGridViewButtonColumn()
                    btnApprove.Name = "Approve"
                    btnApprove.HeaderText = "Approve"
                    btnApprove.Text = "Approve"
                    btnApprove.UseColumnTextForButtonValue = True
                    .Columns.Add(btnApprove)
                End If

                If Not .Columns.Contains("Reject") Then
                    Dim btnReject As New DataGridViewButtonColumn()
                    btnReject.Name = "Reject"
                    btnReject.HeaderText = "Reject"
                    btnReject.Text = "Reject"
                    btnReject.UseColumnTextForButtonValue = True
                    .Columns.Add(btnReject)
                End If
            End With
        Catch ex As Exception
            MessageBox.Show("Error setting up DataGridView: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Load All Feedback from Database
    Private Sub LoadFeedback(Optional status As String = "")
        Try
            ' Initialize connection if needed
            If conn Is Nothing Then
                InitializeConnection()
            End If

            ' Ensure connection is closed before opening
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            conn.Open()

            Dim query As String = "SELECT 
                cf.FeedbackID,
                cf.CustomerID,
                CONCAT(c.FirstName, ' ', c.LastName) AS CustomerName,
                cf.FeedbackType,
                cf.OrderID,
                cf.ReservationID,
                cf.OverallRating,
                cf.FoodTasteRating,
                cf.PortionSizeRating,
                cf.ServiceRating,
                cf.AmbienceRating,
                cf.CleanlinessRating,
                cf.ReviewMessage,
                cf.IsAnonymous,
                cf.Status,
                cf.CreatedDate,
                cf.ApprovedDate
                FROM customer_feedback cf
                INNER JOIN customers c ON cf.CustomerID = c.CustomerID"

            If status <> "" Then
                query &= " WHERE cf.Status = @status"
            End If

            query &= " ORDER BY cf.CreatedDate DESC"

            Dim adapter As New MySqlDataAdapter(query, conn)

            If status <> "" Then
                adapter.SelectCommand.Parameters.AddWithValue("@status", status)
            End If

            Dim dt As New DataTable()
            adapter.Fill(dt)

            DataGridView1.DataSource = dt

            ' Format columns
            FormatColumns()

            ' Update status label
            lblTotalReviews.Text = "Total Feedback: " & dt.Rows.Count

        Catch ex As Exception
            MessageBox.Show("Error loading feedback: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    ' Format DataGridView Columns
    Private Sub FormatColumns()
        Try
            If DataGridView1 Is Nothing Then Return

            With DataGridView1

                ' ✅ HIDE INTERNAL IDS
                If .Columns.Contains("FeedbackID") Then .Columns("FeedbackID").Visible = False
                If .Columns.Contains("CustomerID") Then .Columns("CustomerID").Visible = False
                If .Columns.Contains("OrderID") Then .Columns("OrderID").Visible = False
                If .Columns.Contains("ReservationID") Then .Columns("ReservationID").Visible = False

                ' ✅ RENAME HEADERS
                If .Columns.Contains("CustomerName") Then .Columns("CustomerName").HeaderText = "Customer Name"
                If .Columns.Contains("FeedbackType") Then .Columns("FeedbackType").HeaderText = "Type"
                If .Columns.Contains("OverallRating") Then .Columns("OverallRating").HeaderText = "Overall Rating"
                If .Columns.Contains("FoodTasteRating") Then .Columns("FoodTasteRating").HeaderText = "Food"
                If .Columns.Contains("PortionSizeRating") Then .Columns("PortionSizeRating").HeaderText = "Portion"
                If .Columns.Contains("ServiceRating") Then .Columns("ServiceRating").HeaderText = "Service"
                If .Columns.Contains("AmbienceRating") Then .Columns("AmbienceRating").HeaderText = "Ambience"
                If .Columns.Contains("CleanlinessRating") Then .Columns("CleanlinessRating").HeaderText = "Cleanliness"
                If .Columns.Contains("ReviewMessage") Then .Columns("ReviewMessage").HeaderText = "Review Message"
                If .Columns.Contains("IsAnonymous") Then .Columns("IsAnonymous").HeaderText = "Anonymous"
                If .Columns.Contains("Status") Then .Columns("Status").HeaderText = "Status"
                If .Columns.Contains("CreatedDate") Then .Columns("CreatedDate").HeaderText = "Date Created"
                If .Columns.Contains("ApprovedDate") Then .Columns("ApprovedDate").HeaderText = "Date Approved"

                ' ✅ OPTIONAL FORMATTING
                If .Columns.Contains("CreatedDate") Then .Columns("CreatedDate").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm"
                If .Columns.Contains("ApprovedDate") Then .Columns("ApprovedDate").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm"

                ' ✅ GRID BEHAVIOR
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = False
                .ReadOnly = True

            End With
        Catch ex As Exception
            ' Silently handle formatting errors
        End Try
    End Sub

    ' DataGridView Cell Click Event (for action buttons)
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            If e.RowIndex < 0 Then Return

            Dim columnName As String = DataGridView1.Columns(e.ColumnIndex).Name
            Dim feedbackId As Integer = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("FeedbackID").Value)
            Dim currentStatus As String = DataGridView1.Rows(e.RowIndex).Cells("Status").Value.ToString()

            If columnName = "Approve" Then
                If currentStatus = "Approved" Then
                    MessageBox.Show("This feedback is already approved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                If MessageBox.Show("Are you sure you want to approve this feedback?", "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    UpdateFeedbackStatus(feedbackId, "Approved")
                End If

            ElseIf columnName = "Reject" Then
                If currentStatus = "Rejected" Then
                    MessageBox.Show("This feedback is already rejected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                If MessageBox.Show("Are you sure you want to reject this feedback?", "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    UpdateFeedbackStatus(feedbackId, "Rejected")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Update Feedback Status (Approve/Reject)
    Private Sub UpdateFeedbackStatus(feedbackId As Integer, status As String)
        Try
            ' Initialize connection if needed
            If conn Is Nothing Then
                InitializeConnection()
            End If

            ' Ensure connection is closed before opening
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            conn.Open()

            Dim query As String = "UPDATE customer_feedback 
                                  SET Status = @status, 
                                      ApprovedDate = IF(@status = 'Approved', NOW(), NULL),
                                      UpdatedDate = NOW()
                                  WHERE FeedbackID = @feedbackId"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@status", status)
            cmd.Parameters.AddWithValue("@feedbackId", feedbackId)

            Dim result As Integer = cmd.ExecuteNonQuery()

            If result > 0 Then
                MessageBox.Show($"Feedback {status} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadFeedback() ' Refresh the grid
            Else
                MessageBox.Show("Failed to update feedback status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating feedback: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    ' Delete Feedback
    Private Sub DeleteFeedback(feedbackId As Integer)
        Try
            If MessageBox.Show("Are you sure you want to delete this feedback? This action cannot be undone.",
                              "Confirm Deletion",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Warning) = DialogResult.No Then
                Return
            End If

            ' Initialize connection if needed
            If conn Is Nothing Then
                InitializeConnection()
            End If

            ' Ensure connection is closed before opening
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            conn.Open()

            Dim query As String = "DELETE FROM customer_feedback WHERE FeedbackID = @feedbackId"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@feedbackId", feedbackId)

            Dim result As Integer = cmd.ExecuteNonQuery()

            If result > 0 Then
                MessageBox.Show("Feedback deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadFeedback()
            Else
                MessageBox.Show("Failed to delete feedback.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting feedback: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    ' View Feedback Details
    Private Sub ViewFeedbackDetails(feedbackId As Integer)
        Try
            ' Initialize connection if needed
            If conn Is Nothing Then
                InitializeConnection()
            End If

            ' Ensure connection is closed before opening
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            conn.Open()

            Dim query As String = "SELECT 
                cf.*,
                CONCAT(c.FirstName, ' ', c.LastName) AS CustomerName,
                c.Email
                FROM customer_feedback cf
                INNER JOIN customers c ON cf.CustomerID = c.CustomerID
                WHERE cf.FeedbackID = @feedbackId"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@feedbackId", feedbackId)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim details As String = $"Feedback Details:" & vbCrLf & vbCrLf &
                                       $"Feedback ID: {reader("FeedbackID")}" & vbCrLf &
                                       $"Customer: {reader("CustomerName")}" & vbCrLf &
                                       $"Email: {reader("Email")}" & vbCrLf &
                                       $"Type: {reader("FeedbackType")}" & vbCrLf &
                                       $"Anonymous: {If(Convert.ToBoolean(reader("IsAnonymous")), "Yes", "No")}" & vbCrLf & vbCrLf &
                                       $"Overall Rating: {reader("OverallRating")}/5" & vbCrLf &
                                       $"Food Taste: {If(IsDBNull(reader("FoodTasteRating")), "N/A", reader("FoodTasteRating").ToString())}" & vbCrLf &
                                       $"Portion Size: {If(IsDBNull(reader("PortionSizeRating")), "N/A", reader("PortionSizeRating").ToString())}" & vbCrLf &
                                       $"Service: {If(IsDBNull(reader("ServiceRating")), "N/A", reader("ServiceRating").ToString())}" & vbCrLf &
                                       $"Ambience: {If(IsDBNull(reader("AmbienceRating")), "N/A", reader("AmbienceRating").ToString())}" & vbCrLf &
                                       $"Cleanliness: {If(IsDBNull(reader("CleanlinessRating")), "N/A", reader("CleanlinessRating").ToString())}" & vbCrLf & vbCrLf &
                                       $"Comments:" & vbCrLf &
                                       $"Review Message: {If(IsDBNull(reader("ReviewMessage")), "None", reader("ReviewMessage").ToString())}" & vbCrLf &
                                       $"Status: {reader("Status")}" & vbCrLf &
                                       $"Created: {reader("CreatedDate")}" & vbCrLf &
                                       $"Approved: {If(IsDBNull(reader("ApprovedDate")), "Not yet approved", reader("ApprovedDate").ToString())}"

                MessageBox.Show(details, "Feedback Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error viewing details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    ' Search Feedback
    Private Sub SearchFeedback(searchTerm As String)
        Try
            ' Initialize connection if needed
            If conn Is Nothing Then
                InitializeConnection()
            End If

            ' Ensure connection is closed before opening
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            conn.Open()

            Dim query As String = "SELECT 
                cf.FeedbackID,
                cf.CustomerID,
                CONCAT(c.FirstName, ' ', c.LastName) AS CustomerName,
                cf.FeedbackType,
                cf.OrderID,
                cf.ReservationID,
                cf.OverallRating,
                cf.FoodTasteRating,
                cf.PortionSizeRating,
                cf.ServiceRating,
                cf.AmbienceRating,
                cf.CleanlinessRating,
                cf.ReviewMessage,
                cf.IsAnonymous,
                cf.Status,
                cf.CreatedDate,
                cf.ApprovedDate
                FROM customer_feedback cf
                INNER JOIN customers c ON cf.CustomerID = c.CustomerID
                WHERE CONCAT(c.FirstName, ' ', c.LastName) LIKE @search
                OR cf.ReviewMessage LIKE @search
                OR cf.Status LIKE @search
                OR cf.FeedbackType LIKE @search
                ORDER BY cf.CreatedDate DESC"

            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@search", "%" & searchTerm & "%")

            Dim dt As New DataTable()
            adapter.Fill(dt)

            DataGridView1.DataSource = dt
            FormatColumns()

            lblTotalReviews.Text = $"Found: {dt.Rows.Count} feedback"

        Catch ex As Exception
            MessageBox.Show("Error searching: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    ' Button Event Handlers
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadFeedback()
    End Sub

    Private Sub btnViewPending_Click(sender As Object, e As EventArgs) Handles btnViewPending.Click
        LoadFeedback("Pending")
    End Sub

    Private Sub btnViewApproved_Click(sender As Object, e As EventArgs) Handles btnViewApproved.Click
        LoadFeedback("Approved")
    End Sub

    Private Sub btnViewRejected_Click(sender As Object, e As EventArgs) Handles btnViewRejected.Click
        LoadFeedback("Rejected")
    End Sub

    Private Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        LoadFeedback()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If txtSearch.Text.Trim() <> "" AndAlso txtSearch.Text.Trim() <> "Search..." Then
                SearchFeedback(txtSearch.Text.Trim())
            Else
                LoadFeedback()
            End If
        Catch ex As Exception
            ' Ignore errors during typing
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If DataGridView1.SelectedRows.Count > 0 Then
                Dim feedbackId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("FeedbackID").Value)
                DeleteFeedback(feedbackId)
            Else
                MessageBox.Show("Please select a feedback to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnViewDetails_Click(sender As Object, e As EventArgs) Handles btnViewDetails.Click
        Try
            If DataGridView1.SelectedRows.Count > 0 Then
                Dim feedbackId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("FeedbackID").Value)
                ViewFeedbackDetails(feedbackId)
            Else
                MessageBox.Show("Please select a feedback to view details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Export to CSV (Optional)
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            ' Create a fresh DataTable from the current DataGridView
            Dim dt As New DataTable()

            If DataGridView1.DataSource IsNot Nothing Then
                dt = CType(DataGridView1.DataSource, DataTable)
            Else
                MessageBox.Show("No data to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv"
            saveFileDialog.FileName = $"Feedback_Export_{DateTime.Now:yyyyMMdd}.csv"

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Using writer As New IO.StreamWriter(saveFileDialog.FileName)
                    ' Write headers
                    For i As Integer = 0 To dt.Columns.Count - 1
                        writer.Write(dt.Columns(i).ColumnName)
                        If i < dt.Columns.Count - 1 Then writer.Write(",")
                    Next
                    writer.WriteLine()

                    ' Write data
                    For Each row As DataRow In dt.Rows
                        For i As Integer = 0 To dt.Columns.Count - 1
                            writer.Write(row(i).ToString().Replace(",", ";"))
                            If i < dt.Columns.Count - 1 Then writer.Write(",")
                        Next
                        writer.WriteLine()
                    Next
                End Using

                MessageBox.Show("Feedback exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Export error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class