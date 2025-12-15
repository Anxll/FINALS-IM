Imports MySqlConnector

Public Class ProductIngredientUsageHistory

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ProductIngredientUsageHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Set form size to notification panel style (not maximized)
            Me.Size = New Size(900, 600)
            Me.StartPosition = FormStartPosition.CenterScreen

            InitializeFilters()
            LoadUsageHistory()
        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message,
                          "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitializeFilters()
        Try
            ' Date filters - default to last 7 days
            dtpStartDate.Value = Date.Now.AddDays(-7)
            dtpEndDate.Value = Date.Now

            ' Source filter
            cmbSource.Items.Clear()
            cmbSource.Items.Add("All Sources")
            cmbSource.Items.Add("POS")
            cmbSource.Items.Add("WEBSITE")
            cmbSource.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("Error initializing filters: " & ex.Message,
                          "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadUsageHistory()
        Try
            openConn()

            ' Simplified query focusing on key information
            Dim sql As String = "
                SELECT 
                    iml.MovementDate AS 'Date & Time',
                    CASE 
                        WHEN iml.Source = 'POS' AND iml.OrderID IS NOT NULL THEN 
                            CONCAT('Order #', iml.OrderID)
                        WHEN iml.Source = 'WEBSITE' AND iml.ReservationID IS NOT NULL THEN 
                            CONCAT('Reservation #', iml.ReservationID)
                        ELSE CONCAT(iml.Source, ' - ', COALESCE(iml.Reason, 'Manual'))
                    END AS 'Order Details',
                    i.IngredientName AS 'Ingredient Used',
                    COALESCE(ic.CategoryName, 'Uncategorized') AS 'Category',
                    iml.Source
                FROM inventory_movement_log iml
                INNER JOIN ingredients i ON iml.IngredientID = i.IngredientID
                LEFT JOIN ingredient_categories ic ON i.CategoryID = ic.CategoryID
                WHERE iml.ChangeType = 'DEDUCT'
                AND DATE(iml.MovementDate) BETWEEN @startDate AND @endDate
            "

            ' Add source filter
            If cmbSource.SelectedIndex > 0 Then
                sql &= " AND iml.Source = @source"
            End If

            ' Add search filter
            If Not String.IsNullOrWhiteSpace(txtSearch.Text) Then
                sql &= " AND (i.IngredientName LIKE @search 
                         OR ic.CategoryName LIKE @search)"
            End If

            sql &= " ORDER BY iml.MovementDate DESC LIMIT 500"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@startDate", dtpStartDate.Value.Date)
            cmd.Parameters.AddWithValue("@endDate", dtpEndDate.Value.Date)

            If cmbSource.SelectedIndex > 0 Then
                cmd.Parameters.AddWithValue("@source", cmbSource.Text)
            End If

            If Not String.IsNullOrWhiteSpace(txtSearch.Text) Then
                cmd.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
            End If

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvUsageHistory.DataSource = dt
            FormatGrid()
            ColorCodeGrid()

            lblSubtitle.Text = "Showing " & dt.Rows.Count.ToString() & " recent ingredient usage records"

        Catch ex As Exception
            MessageBox.Show("Error loading usage history: " & ex.Message,
                          "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub FormatGrid()
        Try
            With dgvUsageHistory
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                .RowTemplate.Height = 35
                .DefaultCellStyle.Font = New Font("Segoe UI", 9)
                .DefaultCellStyle.Padding = New Padding(8, 4, 8, 4)
                .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250)
                .ReadOnly = True
                .AllowUserToAddRows = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect

                ' Set column widths for compact view
                If .Columns.Contains("Date & Time") Then
                    .Columns("Date & Time").Width = 150
                    .Columns("Date & Time").DefaultCellStyle.Format = "MMM dd, yyyy HH:mm"
                End If

                If .Columns.Contains("Order Details") Then
                    .Columns("Order Details").Width = 200
                End If

                If .Columns.Contains("Ingredient Used") Then
                    .Columns("Ingredient Used").Width = 180
                End If

                If .Columns.Contains("Category") Then
                    .Columns("Category").Width = 140
                End If

                If .Columns.Contains("Source") Then
                    .Columns("Source").Width = 100
                    .Columns("Source").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns("Source").DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                End If
            End With

        Catch ex As Exception
            ' Silent fail
        End Try
    End Sub

    Private Sub ColorCodeGrid()
        Try
            For Each row As DataGridViewRow In dgvUsageHistory.Rows
                If Not row.IsNewRow Then
                    ' Color code by source
                    If row.Cells("Source").Value IsNot Nothing Then
                        Dim source As String = row.Cells("Source").Value.ToString()

                        Select Case source
                            Case "POS"
                                row.Cells("Source").Style.BackColor = Color.FromArgb(23, 162, 184)
                                row.Cells("Source").Style.ForeColor = Color.White

                            Case "WEBSITE"
                                row.Cells("Source").Style.BackColor = Color.FromArgb(111, 66, 193)
                                row.Cells("Source").Style.ForeColor = Color.White

                            Case "ADMIN"
                                row.Cells("Source").Style.BackColor = Color.FromArgb(253, 126, 20)
                                row.Cells("Source").Style.ForeColor = Color.White
                        End Select
                    End If
                End If
            Next

        Catch ex As Exception
            ' Silent fail
        End Try
    End Sub

    Private Sub btnApplyFilters_Click(sender As Object, e As EventArgs)
        LoadUsageHistory()
    End Sub

    Private Sub btnResetFilters_Click(sender As Object, e As EventArgs)
        dtpStartDate.Value = Date.Now.AddDays(-7)
        dtpEndDate.Value = Date.Now
        cmbSource.SelectedIndex = 0
        txtSearch.Text = ""
        LoadUsageHistory()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadUsageHistory()
    End Sub

    Private Sub btnClearHistory_Click(sender As Object, e As EventArgs)
        Try
            Dim result As DialogResult = MessageBox.Show(
                "Are you sure you want to clear the ingredient usage history?" & vbCrLf & vbCrLf &
                "This will permanently delete all deduction records from the inventory movement log." & vbCrLf &
                "This action CANNOT be undone!" & vbCrLf & vbCrLf &
                "Note: This only clears the usage history log. Your current inventory levels will NOT be affected.",
                "Confirm Clear History",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If result <> DialogResult.Yes Then
                Return
            End If

            ' Ask for confirmation again
            Dim confirmResult As DialogResult = MessageBox.Show(
                "FINAL CONFIRMATION" & vbCrLf & vbCrLf &
                "This will delete ALL ingredient usage records!" & vbCrLf &
                "Are you absolutely sure?",
                "Final Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation)

            If confirmResult <> DialogResult.Yes Then
                Return
            End If

            openConn()

            ' Delete only DEDUCT entries from inventory_movement_log
            Dim sql As String = "DELETE FROM inventory_movement_log WHERE ChangeType = 'DEDUCT'"
            Dim cmd As New MySqlCommand(sql, conn)
            Dim rowsDeleted As Integer = cmd.ExecuteNonQuery()

            MessageBox.Show(
                rowsDeleted & " usage records cleared successfully!" & vbCrLf & vbCrLf &
                "Your current inventory levels remain unchanged.",
                "History Cleared",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            ' Refresh the grid
            LoadUsageHistory()

        Catch ex As Exception
            MessageBox.Show("Error clearing history: " & ex.Message,
                          "Clear Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ' Optional: Add auto-search with debouncing
    End Sub
End Class