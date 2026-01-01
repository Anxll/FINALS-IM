Imports MySqlConnector
Imports System.IO

Public Class ActivityLogsForm
    Private currentPage As Integer = 1
    Private recordsPerPage As Integer = 100
    Private totalRecords As Integer = 0

    Public Sub New()
        InitializeComponent()
        Me.DoubleBuffered = True
    End Sub

    Private Sub ActivityLogsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeControls()
        LoadActivityLogs()
    End Sub

    Private Sub InitializeControls()
        ' Setup DateTimePickers
        dtpStartDate.Value = DateTime.Now.AddDays(-30) ' Default: Last 30 days
        dtpEndDate.Value = DateTime.Now

        ' Setup ComboBoxes
        cboUserType.Items.Clear()
        cboUserType.Items.AddRange(New String() {"All", "Admin", "Staff", "Customer"})
        cboUserType.SelectedIndex = 0

        cboActionCategory.Items.Clear()
        cboActionCategory.Items.AddRange(New String() {"All", "Login", "Logout", "Order", "Reservation", "Payment", "Inventory", "Product", "User Management", "Report", "System"})
        cboActionCategory.SelectedIndex = 0

        cboSourceSystem.Items.Clear()
        cboSourceSystem.Items.AddRange(New String() {"All", "POS", "Website", "Admin Panel"})
        cboSourceSystem.SelectedIndex = 0

        cboStatus.Items.Clear()
        cboStatus.Items.AddRange(New String() {"All", "Success", "Failed", "Warning"})
        cboStatus.SelectedIndex = 0

        ' Setup DataGridView
        SetupDataGridView()
    End Sub

    Private Sub SetupDataGridView()
        With dgvActivityLogs
            .AutoGenerateColumns = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .RowHeadersVisible = False
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .GridColor = Color.FromArgb(230, 230, 230)
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215)
            .DefaultCellStyle.SelectionForeColor = Color.White
            .EnableHeadersVisualStyles = False
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 38, 50)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            .ColumnHeadersHeight = 40
            .RowTemplate.Height = 35
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            .Columns.Clear()

            ' Define columns
            Dim colLogID As New DataGridViewTextBoxColumn With {
                .Name = "LogID",
                .HeaderText = "Log ID",
                .DataPropertyName = "LogID",
                .Width = 70,
                .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter}
            }

            Dim colTimestamp As New DataGridViewTextBoxColumn With {
                .Name = "Timestamp",
                .HeaderText = "Date & Time",
                .DataPropertyName = "Timestamp",
                .Width = 150,
                .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "yyyy-MM-dd HH:mm:ss"}
            }

            Dim colUserType As New DataGridViewTextBoxColumn With {
                .Name = "UserType",
                .HeaderText = "User Type",
                .DataPropertyName = "UserType",
                .Width = 90
            }

            Dim colUsername As New DataGridViewTextBoxColumn With {
                .Name = "Username",
                .HeaderText = "Username",
                .DataPropertyName = "Username",
                .Width = 120
            }

            Dim colAction As New DataGridViewTextBoxColumn With {
                .Name = "Action",
                .HeaderText = "Action",
                .DataPropertyName = "Action",
                .Width = 150
            }

            Dim colActionCategory As New DataGridViewTextBoxColumn With {
                .Name = "ActionCategory",
                .HeaderText = "Category",
                .DataPropertyName = "ActionCategory",
                .Width = 110
            }

            Dim colDescription As New DataGridViewTextBoxColumn With {
                .Name = "Description",
                .HeaderText = "Description",
                .DataPropertyName = "Description",
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            }

            Dim colSourceSystem As New DataGridViewTextBoxColumn With {
                .Name = "SourceSystem",
                .HeaderText = "Source",
                .DataPropertyName = "SourceSystem",
                .Width = 100
            }

            Dim colStatus As New DataGridViewTextBoxColumn With {
                .Name = "Status",
                .HeaderText = "Status",
                .DataPropertyName = "Status",
                .Width = 80
            }

            Dim colReferenceID As New DataGridViewTextBoxColumn With {
                .Name = "ReferenceID",
                .HeaderText = "Ref ID",
                .DataPropertyName = "ReferenceID",
                .Width = 80
            }

            .Columns.AddRange(New DataGridViewColumn() {
                colLogID, colTimestamp, colUserType, colUsername, colAction,
                colActionCategory, colDescription, colSourceSystem, colStatus, colReferenceID
            })
        End With
    End Sub

    Private Sub LoadActivityLogs()
        Try
            Cursor = Cursors.WaitCursor

            ' Build WHERE clause based on filters
            Dim whereClause As String = "WHERE 1=1"
            Dim parameters As New List(Of MySqlParameter)

            ' Date range filter
            whereClause &= " AND DATE(Timestamp) BETWEEN @StartDate AND @EndDate"
            parameters.Add(New MySqlParameter("@StartDate", dtpStartDate.Value.Date))
            parameters.Add(New MySqlParameter("@EndDate", dtpEndDate.Value.Date))

            ' User Type filter
            If cboUserType.SelectedIndex > 0 Then
                whereClause &= " AND UserType = @UserType"
                parameters.Add(New MySqlParameter("@UserType", cboUserType.SelectedItem.ToString()))
            End If

            ' Action Category filter
            If cboActionCategory.SelectedIndex > 0 Then
                whereClause &= " AND ActionCategory = @ActionCategory"
                parameters.Add(New MySqlParameter("@ActionCategory", cboActionCategory.SelectedItem.ToString()))
            End If

            ' Source System filter
            If cboSourceSystem.SelectedIndex > 0 Then
                whereClause &= " AND SourceSystem = @SourceSystem"
                parameters.Add(New MySqlParameter("@SourceSystem", cboSourceSystem.SelectedItem.ToString()))
            End If

            ' Status filter
            If cboStatus.SelectedIndex > 0 Then
                whereClause &= " AND Status = @Status"
                parameters.Add(New MySqlParameter("@Status", cboStatus.SelectedItem.ToString()))
            End If

            ' Search filter
            If Not String.IsNullOrWhiteSpace(txtSearch.Text) Then
                whereClause &= " AND (Username LIKE @Search OR Action LIKE @Search OR Description LIKE @Search OR ReferenceID LIKE @Search)"
                parameters.Add(New MySqlParameter("@Search", "%" & txtSearch.Text & "%"))
            End If

            ' Get total count
            Dim countQuery As String = $"SELECT COUNT(*) FROM activity_logs {whereClause}"

            Using conn As New MySqlConnection(modDB.strConnection)
                conn.Open()

                Using cmdCount As New MySqlCommand(countQuery, conn)
                    For Each param In parameters
                        cmdCount.Parameters.Add(New MySqlParameter(param.ParameterName, param.Value))
                    Next
                    totalRecords = Convert.ToInt32(cmdCount.ExecuteScalar())
                End Using

                ' Get paginated data
                Dim offset As Integer = (currentPage - 1) * recordsPerPage
                Dim query As String = $"SELECT LogID, UserType, UserID, Username, Action, ActionCategory, Description, 
                                              SourceSystem, ReferenceID, ReferenceTable, OldValue, NewValue, 
                                              Status, Timestamp
                                       FROM activity_logs 
                                       {whereClause}
                                       ORDER BY Timestamp DESC
                                       LIMIT @Limit OFFSET @Offset"

                Using cmd As New MySqlCommand(query, conn)
                    For Each param In parameters
                        cmd.Parameters.Add(New MySqlParameter(param.ParameterName, param.Value))
                    Next
                    cmd.Parameters.AddWithValue("@Limit", recordsPerPage)
                    cmd.Parameters.AddWithValue("@Offset", offset)

                    Dim dt As New DataTable()
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using

                    dgvActivityLogs.DataSource = dt
                End Using
            End Using

            ' Update status labels
            UpdateStatusLabels()
            ApplyRowColors()

        Catch ex As Exception
            MessageBox.Show("Error loading activity logs: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ApplyRowColors()
        For Each row As DataGridViewRow In dgvActivityLogs.Rows
            If row.Cells("Status").Value IsNot Nothing Then
                Dim status As String = row.Cells("Status").Value.ToString()
                Select Case status
                    Case "Failed"
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230)
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(180, 0, 0)
                    Case "Warning"
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 220)
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(150, 100, 0)
                    Case "Success"
                        row.DefaultCellStyle.BackColor = Color.White
                        row.DefaultCellStyle.ForeColor = Color.Black
                End Select
            End If
        Next
    End Sub

    Private Sub UpdateStatusLabels()
        Dim totalPages As Integer = Math.Ceiling(totalRecords / recordsPerPage)
        lblStatus.Text = $"Showing {dgvActivityLogs.Rows.Count} of {totalRecords} records | Page {currentPage} of {totalPages}"

        ' Enable/disable navigation buttons
        btnPrevious.Enabled = (currentPage > 1)
        btnNext.Enabled = (currentPage < totalPages)
    End Sub

    ' Filter Events
    Private Sub btnApplyFilters_Click(sender As Object, e As EventArgs) Handles btnApplyFilters.Click
        currentPage = 1
        LoadActivityLogs()
    End Sub

    Private Sub btnResetFilters_Click(sender As Object, e As EventArgs) Handles btnResetFilters.Click
        dtpStartDate.Value = DateTime.Now.AddDays(-30)
        dtpEndDate.Value = DateTime.Now
        cboUserType.SelectedIndex = 0
        cboActionCategory.SelectedIndex = 0
        cboSourceSystem.SelectedIndex = 0
        cboStatus.SelectedIndex = 0
        txtSearch.Clear()
        currentPage = 1
        LoadActivityLogs()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ' Auto-search after typing stops (optional - you can remove this if you prefer button-only search)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        currentPage = 1
        LoadActivityLogs()
    End Sub

    ' Pagination Events
    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentPage > 1 Then
            currentPage -= 1
            LoadActivityLogs()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim totalPages As Integer = Math.Ceiling(totalRecords / recordsPerPage)
        If currentPage < totalPages Then
            currentPage += 1
            LoadActivityLogs()
        End If
    End Sub

    ' Export Events
    Private Sub btnExportCSV_Click(sender As Object, e As EventArgs) Handles btnExportCSV.Click
        ExportToCSV()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadActivityLogs()
        MessageBox.Show("Activity logs refreshed successfully!", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' ✅ NEW: Clear Activity Logs Button
    Private Sub btnClearLogs_Click(sender As Object, e As EventArgs) Handles btnClearLogs.Click
        ClearActivityLogs()
    End Sub

    Private Sub ClearActivityLogs()
        Try
            ' First confirmation
            Dim result1 As DialogResult = MessageBox.Show(
                "⚠️ WARNING: This will permanently delete ALL activity logs!" & vbCrLf & vbCrLf &
                "This action cannot be undone." & vbCrLf & vbCrLf &
                "Are you sure you want to continue?",
                "Clear Activity Logs - Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            )

            If result1 = DialogResult.No Then
                Return
            End If

            ' Second confirmation (extra safety)
            Dim result2 As DialogResult = MessageBox.Show(
                "🔴 FINAL CONFIRMATION" & vbCrLf & vbCrLf &
                "You are about to delete ALL activity log records." & vbCrLf &
                "Current log count: " & totalRecords.ToString() & " records" & vbCrLf & vbCrLf &
                "This is your last chance to cancel." & vbCrLf & vbCrLf &
                "Click YES to permanently delete all logs.",
                "Final Confirmation Required",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop,
                MessageBoxDefaultButton.Button2
            )

            If result2 = DialogResult.No Then
                Return
            End If

            ' Get count before deletion for logging
            Dim recordsDeleted As Integer = totalRecords

            ' Perform deletion
            Using conn As New MySqlConnection(modDB.strConnection)
                conn.Open()

                ' Delete all activity logs
                Dim query As String = "DELETE FROM activity_logs"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                End Using

                ' Reset auto-increment (optional - starts LogID from 1 again)
                Dim resetQuery As String = "ALTER TABLE activity_logs AUTO_INCREMENT = 1"
                Using cmdReset As New MySqlCommand(resetQuery, conn)
                    cmdReset.ExecuteNonQuery()
                End Using
            End Using

            ' Log this action (will create a new log entry)
            ActivityLogger.LogUserActivity(
                "Clear All Activity Logs",
                "System",
                $"Admin cleared all activity logs. {recordsDeleted} records were deleted.",
                "Admin Panel",
                Nothing,
                "activity_logs",
                $"{recordsDeleted} records",
                "0 records",
                "Warning"
            )

            ' Refresh the grid
            LoadActivityLogs()

            ' Success message
            MessageBox.Show(
                $"✅ Successfully deleted {recordsDeleted} activity log records." & vbCrLf & vbCrLf &
                "The activity log has been cleared.",
                "Clear Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )

        Catch ex As Exception
            MessageBox.Show(
                "Error clearing activity logs: " & ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

            ' Log the failed attempt
            Try
                ActivityLogger.LogUserActivity(
                    "Clear Activity Logs Failed",
                    "System",
                    $"Failed to clear activity logs: {ex.Message}",
                    "Admin Panel",
                    Nothing, Nothing, Nothing, Nothing,
                    "Failed"
                )
            Catch
                ' Ignore logging errors
            End Try
        End Try
    End Sub

    Private Sub ExportToCSV()
        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "CSV Files (*.csv)|*.csv"
            saveDialog.FileName = $"ActivityLogs_{DateTime.Now:yyyyMMdd_HHmmss}.csv"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Using writer As New StreamWriter(saveDialog.FileName)
                    ' Write headers
                    Dim headers As New List(Of String)
                    For Each column As DataGridViewColumn In dgvActivityLogs.Columns
                        headers.Add(column.HeaderText)
                    Next
                    writer.WriteLine(String.Join(",", headers))

                    ' Write data
                    For Each row As DataGridViewRow In dgvActivityLogs.Rows
                        If Not row.IsNewRow Then
                            Dim cells As New List(Of String)
                            For Each cell As DataGridViewCell In row.Cells
                                Dim value As String = If(cell.Value IsNot Nothing, cell.Value.ToString().Replace(",", ";"), "")
                                cells.Add($"""{value}""")
                            Next
                            writer.WriteLine(String.Join(",", cells))
                        End If
                    Next
                End Using

                MessageBox.Show("Activity logs exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error exporting to CSV: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' View Details Event
    Private Sub dgvActivityLogs_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvActivityLogs.CellDoubleClick
        If e.RowIndex >= 0 Then
            ShowLogDetails(e.RowIndex)
        End If
    End Sub

    Private Sub ShowLogDetails(rowIndex As Integer)
        Try
            Dim row As DataGridViewRow = dgvActivityLogs.Rows(rowIndex)

            Dim details As String = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & vbCrLf
            details &= "ACTIVITY LOG DETAILS" & vbCrLf
            details &= "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & vbCrLf & vbCrLf
            details &= $"Log ID: {row.Cells("LogID").Value}" & vbCrLf
            details &= $"Timestamp: {row.Cells("Timestamp").Value}" & vbCrLf
            details &= $"User Type: {row.Cells("UserType").Value}" & vbCrLf
            details &= $"Username: {row.Cells("Username").Value}" & vbCrLf
            details &= $"Action: {row.Cells("Action").Value}" & vbCrLf
            details &= $"Category: {row.Cells("ActionCategory").Value}" & vbCrLf
            details &= $"Source System: {row.Cells("SourceSystem").Value}" & vbCrLf
            details &= $"Status: {row.Cells("Status").Value}" & vbCrLf
            details &= $"Reference ID: {If(row.Cells("ReferenceID").Value, "N/A")}" & vbCrLf
            details &= $"Reference Table: {If(row.Cells("ReferenceTable").Value, "N/A")}" & vbCrLf & vbCrLf
            details &= "Description:" & vbCrLf
            details &= $"{row.Cells("Description").Value}" & vbCrLf & vbCrLf

            If row.Cells("OldValue").Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(row.Cells("OldValue").Value.ToString()) Then
                details &= "Old Value:" & vbCrLf
                details &= $"{row.Cells("OldValue").Value}" & vbCrLf & vbCrLf
            End If

            If row.Cells("NewValue").Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(row.Cells("NewValue").Value.ToString()) Then
                details &= "New Value:" & vbCrLf
                details &= $"{row.Cells("NewValue").Value}" & vbCrLf
            End If

            MessageBox.Show(details, "Activity Log Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error displaying log details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Context Menu for Right-click options
    Private Sub dgvActivityLogs_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvActivityLogs.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim hit As DataGridView.HitTestInfo = dgvActivityLogs.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 Then
                dgvActivityLogs.ClearSelection()
                dgvActivityLogs.Rows(hit.RowIndex).Selected = True

                ' Show context menu
                Dim contextMenu As New ContextMenuStrip()
                contextMenu.Items.Add("View Details", Nothing, Sub() ShowLogDetails(hit.RowIndex))
                contextMenu.Items.Add("Copy Description", Nothing, Sub() CopyToClipboard(hit.RowIndex, "Description"))
                contextMenu.Items.Add("Copy Reference ID", Nothing, Sub() CopyToClipboard(hit.RowIndex, "ReferenceID"))
                contextMenu.Show(dgvActivityLogs, e.Location)
            End If
        End If
    End Sub

    Private Sub CopyToClipboard(rowIndex As Integer, columnName As String)
        Try
            Dim value As Object = dgvActivityLogs.Rows(rowIndex).Cells(columnName).Value
            If value IsNot Nothing Then
                Clipboard.SetText(value.ToString())
                MessageBox.Show($"{columnName} copied to clipboard!", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error copying to clipboard: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class