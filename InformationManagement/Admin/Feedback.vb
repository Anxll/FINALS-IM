Imports MySqlConnector
Imports System.Data

Public Class Feedback
    ' Database connection string using global modDB
    Private connectionString As String = modDB.strConnection
    Private conn As MySqlConnection
    Private adapter As MySqlDataAdapter
    Private dt As DataTable

    ' Pagination variables
    Private currentPage As Integer = 1
    Private recordsPerPage As Integer = 20
    Private totalRecords As Integer = 0
    Private totalPages As Integer = 0
    Private currentFilter As String = ""
    Private currentSearchTerm As String = ""

    ' Form Load Event
    Private Sub Feedback_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeConnection()
        LoadFeedback()
        SetupDataGridView()
        UpdatePaginationControls()
    End Sub

    ' Initialize Database Connection
    Private Sub InitializeConnection()
        Try
            conn = New MySqlConnection(connectionString)
        Catch ex As Exception
            MessageBox.Show("Connection Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Setup DataGridView Appearance
    Private Sub SetupDataGridView()
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
    End Sub

    ' Get Total Record Count
    Private Function GetTotalRecords() As Integer
        Dim count As Integer = 0
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            conn.Open()

            Dim query As String = "SELECT COUNT(*) FROM customer_feedback cf 
                                  INNER JOIN customers c ON cf.CustomerID = c.CustomerID"

            ' Apply filters
            Dim whereClause As String = ""
            If currentFilter <> "" Then
                whereClause = " WHERE cf.Status = @status"
            End If

            If currentSearchTerm <> "" Then
                If whereClause = "" Then
                    whereClause = " WHERE "
                Else
                    whereClause &= " AND "
                End If
                whereClause &= "(CONCAT(c.FirstName, ' ', c.LastName) LIKE @search
                               OR cf.ReviewMessage LIKE @search
                               OR cf.Status LIKE @search
                               OR cf.FeedbackType LIKE @search)"
            End If

            query &= whereClause

            Dim cmd As New MySqlCommand(query, conn)

            If currentFilter <> "" Then
                cmd.Parameters.AddWithValue("@status", currentFilter)
            End If

            If currentSearchTerm <> "" Then
                cmd.Parameters.AddWithValue("@search", "%" & currentSearchTerm & "%")
            End If

            count = Convert.ToInt32(cmd.ExecuteScalar())

        Catch ex As Exception
            MessageBox.Show("Error getting record count: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

        Return count
    End Function

    ' Load Feedback with Pagination
    Private Sub LoadFeedback(Optional status As String = "")
        Try
            currentFilter = status

            ' Get total records
            totalRecords = GetTotalRecords()
            totalPages = Math.Ceiling(totalRecords / recordsPerPage)

            ' Ensure current page is valid
            If currentPage > totalPages And totalPages > 0 Then
                currentPage = totalPages
            End If
            If currentPage < 1 Then
                currentPage = 1
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
                cf.FoodTasteComment,
                cf.PortionSizeComment,
                cf.ServiceComment,
                cf.AmbienceComment,
                cf.CleanlinessComment,
                cf.ReviewMessage,
                cf.Status,
                cf.CreatedDate,
                cf.ApprovedDate
                FROM customer_feedback cf
                INNER JOIN customers c ON cf.CustomerID = c.CustomerID"

            Dim whereClause As String = ""
            If status <> "" Then
                whereClause = " WHERE cf.Status = @status"
            End If

            If currentSearchTerm <> "" Then
                If whereClause = "" Then
                    whereClause = " WHERE "
                Else
                    whereClause &= " AND "
                End If
                whereClause &= "(CONCAT(c.FirstName, ' ', c.LastName) LIKE @search
                               OR cf.ReviewMessage LIKE @search
                               OR cf.Status LIKE @search
                               OR cf.FeedbackType LIKE @search)"
            End If

            query &= whereClause
            query &= " ORDER BY cf.CreatedDate DESC"
            query &= " LIMIT @limit OFFSET @offset"

            adapter = New MySqlDataAdapter(query, conn)

            If status <> "" Then
                adapter.SelectCommand.Parameters.AddWithValue("@status", status)
            End If

            If currentSearchTerm <> "" Then
                adapter.SelectCommand.Parameters.AddWithValue("@search", "%" & currentSearchTerm & "%")
            End If

            adapter.SelectCommand.Parameters.AddWithValue("@limit", recordsPerPage)
            adapter.SelectCommand.Parameters.AddWithValue("@offset", (currentPage - 1) * recordsPerPage)

            dt = New DataTable()
            adapter.Fill(dt)

            DataGridView1.DataSource = dt

            ' Format columns
            FormatColumns()

            ' Update status label
            UpdatePaginationControls()

        Catch ex As Exception
            MessageBox.Show("Error loading feedback: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    ' Format DataGridView Columns
    Private Sub FormatColumns()
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
            If .Columns.Contains("FoodTasteComment") Then .Columns("FoodTasteComment").HeaderText = "Food Comment"
            If .Columns.Contains("PortionSizeComment") Then .Columns("PortionSizeComment").HeaderText = "Portion Comment"
            If .Columns.Contains("ServiceComment") Then .Columns("ServiceComment").HeaderText = "Service Comment"
            If .Columns.Contains("AmbienceComment") Then .Columns("AmbienceComment").HeaderText = "Ambience Comment"
            If .Columns.Contains("CleanlinessComment") Then .Columns("CleanlinessComment").HeaderText = "Cleanliness Comment"
            If .Columns.Contains("ReviewMessage") Then .Columns("ReviewMessage").HeaderText = "Review Message"
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
    End Sub

    ' Update Pagination Controls
    Private Sub UpdatePaginationControls()
        lblPageInfo.Text = $"Page {currentPage} of {totalPages} (Total: {totalRecords} records)"
        lblTotalReviews.Text = $"Total Feedback: {totalRecords}"

        ' Enable/Disable navigation buttons
        btnFirstPage.Enabled = (currentPage > 1)
        btnPrevPage.Enabled = (currentPage > 1)
        btnNextPage.Enabled = (currentPage < totalPages)
        btnLastPage.Enabled = (currentPage < totalPages)

        txtPageNumber.Text = currentPage.ToString()
    End Sub

    ' DataGridView Cell Click Event (for action buttons)
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
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
                UpdateFeedbackStatus(feedbackId, "Approved", currentStatus)
            End If

        ElseIf columnName = "Reject" Then
            If currentStatus = "Rejected" Then
                MessageBox.Show("This feedback is already rejected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If MessageBox.Show("Are you sure you want to reject this feedback?", "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                UpdateFeedbackStatus(feedbackId, "Rejected", currentStatus)
            End If
        End If
    End Sub

    ' Update Feedback Status (Approve/Reject)
    Private Sub UpdateFeedbackStatus(feedbackId As Integer, status As String, oldStatus As String)
        Try
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

                ' Log Activity
                ActivityLogger.LogUserActivity(
                    action:=status,
                    actionCategory:="System",
                    description:=$"Feedback {status} (ID: {feedbackId})",
                    sourceSystem:="Admin Panel",
                    referenceID:=feedbackId.ToString(),
                    referenceTable:="customer_feedback",
                    oldValue:=oldStatus,
                    newValue:=status
                )

                LoadFeedback(currentFilter)
            Else
                MessageBox.Show("Failed to update feedback status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating feedback: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
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

                ' Log Activity
                ActivityLogger.LogUserActivity(
                    action:="Delete",
                    actionCategory:="System",
                    description:=$"Deleted Feedback ID: {feedbackId}",
                    sourceSystem:="Admin Panel",
                    referenceID:=feedbackId.ToString(),
                    referenceTable:="customer_feedback",
                    oldValue:="Existing",
                    newValue:="Deleted"
                )

                LoadFeedback(currentFilter)
            Else
                MessageBox.Show("Failed to delete feedback.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting feedback: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    ' View Feedback Details
    Private Sub ViewFeedbackDetails(feedbackId As Integer)
        Try
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
                                       $"Type: {reader("FeedbackType")}" & vbCrLf & vbCrLf &
                                       $"Overall Rating: {reader("OverallRating")}/5" & vbCrLf & vbCrLf &
                                       $"Review Message:" & vbCrLf &
                                       $"{If(IsDBNull(reader("ReviewMessage")), "None", reader("ReviewMessage").ToString())}" & vbCrLf & vbCrLf &
                                       $"Comments:" & vbCrLf &
                                       $"Food: {If(IsDBNull(reader("FoodTasteComment")), "None", reader("FoodTasteComment").ToString())}" & vbCrLf &
                                       $"Portion: {If(IsDBNull(reader("PortionSizeComment")), "None", reader("PortionSizeComment").ToString())}" & vbCrLf &
                                       $"Service: {If(IsDBNull(reader("ServiceComment")), "None", reader("ServiceComment").ToString())}" & vbCrLf &
                                       $"Ambience: {If(IsDBNull(reader("AmbienceComment")), "None", reader("AmbienceComment").ToString())}" & vbCrLf &
                                       $"Cleanliness: {If(IsDBNull(reader("CleanlinessComment")), "None", reader("CleanlinessComment").ToString())}" & vbCrLf & vbCrLf &
                                       $"Status: {reader("Status")}" & vbCrLf &
                                       $"Created: {reader("CreatedDate")}" & vbCrLf &
                                       $"Approved: {If(IsDBNull(reader("ApprovedDate")), "Not yet approved", reader("ApprovedDate").ToString())}"

                MessageBox.Show(details, "Feedback Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error viewing details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    ' Search Feedback
    Private Sub SearchFeedback(searchTerm As String)
        currentSearchTerm = searchTerm
        currentPage = 1
        LoadFeedback(currentFilter)
    End Sub

    ' Pagination Button Event Handlers
    Private Sub btnFirstPage_Click(sender As Object, e As EventArgs) Handles btnFirstPage.Click
        currentPage = 1
        LoadFeedback(currentFilter)
    End Sub

    Private Sub btnPrevPage_Click(sender As Object, e As EventArgs) Handles btnPrevPage.Click
        If currentPage > 1 Then
            currentPage -= 1
            LoadFeedback(currentFilter)
        End If
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        If currentPage < totalPages Then
            currentPage += 1
            LoadFeedback(currentFilter)
        End If
    End Sub

    Private Sub btnLastPage_Click(sender As Object, e As EventArgs) Handles btnLastPage.Click
        currentPage = totalPages
        LoadFeedback(currentFilter)
    End Sub

    Private Sub txtPageNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPageNumber.KeyPress
        If e.KeyChar = Chr(13) Then ' Enter key
            Dim pageNum As Integer
            If Integer.TryParse(txtPageNumber.Text, pageNum) Then
                If pageNum >= 1 And pageNum <= totalPages Then
                    currentPage = pageNum
                    LoadFeedback(currentFilter)
                Else
                    MessageBox.Show($"Please enter a page number between 1 and {totalPages}", "Invalid Page", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtPageNumber.Text = currentPage.ToString()
                End If
            Else
                MessageBox.Show("Please enter a valid number", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPageNumber.Text = currentPage.ToString()
            End If
            e.Handled = True
        ElseIf Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    ' Button Event Handlers
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        currentPage = 1
        LoadFeedback(currentFilter)
    End Sub

    Private Sub btnViewPending_Click(sender As Object, e As EventArgs) Handles btnViewPending.Click
        currentPage = 1
        LoadFeedback("Pending")
    End Sub

    Private Sub btnViewApproved_Click(sender As Object, e As EventArgs) Handles btnViewApproved.Click
        currentPage = 1
        LoadFeedback("Approved")
    End Sub

    Private Sub btnViewRejected_Click(sender As Object, e As EventArgs) Handles btnViewRejected.Click
        currentPage = 1
        LoadFeedback("Rejected")
    End Sub

    Private Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        currentPage = 1
        currentFilter = ""
        LoadFeedback()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If txtSearch.Text.Trim() <> "" Then
            SearchFeedback(txtSearch.Text.Trim())
        Else
            currentSearchTerm = ""
            currentPage = 1
            LoadFeedback(currentFilter)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim feedbackId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("FeedbackID").Value)
            DeleteFeedback(feedbackId)
        Else
            MessageBox.Show("Please select a feedback to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnViewDetails_Click(sender As Object, e As EventArgs) Handles btnViewDetails.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim feedbackId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("FeedbackID").Value)
            ViewFeedbackDetails(feedbackId)
        Else
            MessageBox.Show("Please select a feedback to view details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' Export to CSV
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
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