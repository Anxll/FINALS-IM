Imports MySqlConnector

Public Class UsersAccounts
    ' Pagination variables
    Private currentPage As Integer = 1
    Private pageSize As Integer = 20
    Private totalRecords As Integer = 0
    Private totalPages As Integer = 0
    Private allStaffData As DataTable
    Private searchText As String = ""
    Private initialLoadComplete As Boolean = False

    Private Sub UsersAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDataGridView()
        RoundPaginationButtons()
        LoadStaffData()
        initialLoadComplete = True
        AdjustControlsToScreen()
    End Sub

    Private Sub UsersAccounts_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If initialLoadComplete Then
            AdjustControlsToScreen()
        End If
    End Sub

    Private Sub AdjustControlsToScreen()
        Try
            ' Get the form's client area dimensions
            Dim formWidth As Integer = Me.ClientSize.Width
            Dim formHeight As Integer = Me.ClientSize.Height

            ' Set margins
            Dim leftMargin As Integer = 30
            Dim rightMargin As Integer = 30
            Dim topMargin As Integer = 30

            ' Title Label
            Label1.Location = New Point(leftMargin, topMargin)

            ' Stats Card
            RoundedPane22.Location = New Point(leftMargin, Label1.Bottom + 20)
            Dim statsCardWidth As Integer = Math.Min(300, (formWidth - leftMargin - rightMargin) \ 3)
            RoundedPane22.Width = statsCardWidth

            ' DataGridView - Calculate available space
            Dim gridTop As Integer = RoundedPane22.Bottom + 20
            Dim gridWidth As Integer = formWidth - leftMargin - rightMargin
            Dim paginationHeight As Integer = 60
            Dim gridHeight As Integer = formHeight - gridTop - paginationHeight - 20

            UsersAccountData.Location = New Point(leftMargin, gridTop)
            UsersAccountData.Size = New Size(gridWidth, gridHeight)

            ' Adjust DataGridView column widths proportionally
            AdjustColumnWidths()

            ' Pagination Panel
            PaginationPanel.Location = New Point(leftMargin, UsersAccountData.Bottom + 10)
            PaginationPanel.Width = gridWidth

            ' Center pagination controls
            CenterPaginationControls()

        Catch ex As Exception
            ' Silently handle resize errors to prevent crashes
            Debug.WriteLine("Resize error: " & ex.Message)
        End Try
    End Sub

    Private Sub AdjustColumnWidths()
        Try
            If UsersAccountData.Columns.Count = 0 Then Return

            Dim totalWidth As Integer = UsersAccountData.Width - 20 ' Account for scrollbar

            ' Set column widths proportionally based on Designer columns
            UsersAccountData.Columns("txtName").Width = CInt(totalWidth * 0.2) ' 20% - Name
            UsersAccountData.Columns("colRole").Width = CInt(totalWidth * 0.15) ' 15% - Role
            UsersAccountData.Columns("colStatus").Width = CInt(totalWidth * 0.15) ' 15% - Status
            UsersAccountData.Columns("colJoinDate").Width = CInt(totalWidth * 0.2) ' 20% - Join Date
            UsersAccountData.Columns("colEdit").Width = 80 ' Fixed width
            UsersAccountData.Columns("colDelete").Width = 80 ' Fixed width

        Catch ex As Exception
            Debug.WriteLine("Column width adjustment error: " & ex.Message)
        End Try
    End Sub

    Private Sub CenterPaginationControls()
        Try
            Dim panelWidth As Integer = PaginationPanel.Width
            Dim totalButtonWidth As Integer = btnFirstPage.Width + btnPreviousPage.Width +
                                              btnNextPage.Width + btnLastPage.Width
            Dim spacing As Integer = 10
            Dim labelWidth As Integer = 100

            Dim totalWidth As Integer = totalButtonWidth + (spacing * 3) + labelWidth
            Dim startX As Integer = (panelWidth - totalWidth) \ 2

            btnFirstPage.Location = New Point(startX, btnFirstPage.Top)
            btnPreviousPage.Location = New Point(btnFirstPage.Right + spacing, btnPreviousPage.Top)
            lblPageInfo.Location = New Point(btnPreviousPage.Right + spacing, lblPageInfo.Top)
            lblPageInfo.Width = labelWidth
            btnNextPage.Location = New Point(lblPageInfo.Right + spacing, btnNextPage.Top)
            btnLastPage.Location = New Point(btnNextPage.Right + spacing, btnLastPage.Top)

        Catch ex As Exception
            Debug.WriteLine("Pagination centering error: " & ex.Message)
        End Try
    End Sub

    Private Sub InitializeDataGridView()
        ' Enable double buffering for smoother rendering
        UsersAccountData.DoubleBuffered(True)
        UsersAccountData.SuspendLayout()
        UsersAccountData.Rows.Clear()
        UsersAccountData.ResumeLayout()

        ' Set alternating row colors for better readability
        UsersAccountData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 252, 255)

        ' Remove selection highlighting on focus
        UsersAccountData.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 244, 250)
        UsersAccountData.DefaultCellStyle.SelectionForeColor = Color.Black

        ' Hide the Edit column
        If UsersAccountData.Columns.Contains("colEdit") Then
            UsersAccountData.Columns("colEdit").Visible = False
        End If
    End Sub

    Private Sub LoadStaffData()
        Try
            openConn()

            ' FIXED QUERY: Load only staff members with exact Position match
            ' Added LIMIT and proper index hints for performance
            Dim query As String = "
                SELECT 
                    e.EmployeeID as ID,
                    e.FirstName,
                    e.LastName,
                    e.HireDate as DateCreated
                FROM employee e
                WHERE e.Position = 'Staff'
                ORDER BY e.HireDate DESC
                LIMIT 1000"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.CommandTimeout = 30

            Dim adapter As New MySqlDataAdapter(cmd)
            allStaffData = New DataTable()
            adapter.Fill(allStaffData)

            totalRecords = allStaffData.Rows.Count
            totalPages = If(totalRecords > 0, Math.Ceiling(totalRecords / pageSize), 1)

            ' Update staff count
            lblStaffs.Text = allStaffData.Rows.Count.ToString()

            ' Apply search and load first page
            ApplySearchFilter()

        Catch ex As MySqlException
            MessageBox.Show("Database error: " & ex.Message & vbCrLf & vbCrLf &
                          "Make sure the 'Position' column contains 'Staff' entries.",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error loading staff data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub ApplySearchFilter()
        If allStaffData Is Nothing Then Return

        Dim filteredData As DataTable

        If String.IsNullOrWhiteSpace(searchText) Then
            filteredData = allStaffData
        Else
            filteredData = allStaffData.Clone()
            For Each row As DataRow In allStaffData.Rows
                Dim firstName As String = If(row("FirstName") IsNot DBNull.Value, row("FirstName").ToString().ToLower(), "")
                Dim lastName As String = If(row("LastName") IsNot DBNull.Value, row("LastName").ToString().ToLower(), "")

                If firstName.Contains(searchText.ToLower()) OrElse
                   lastName.Contains(searchText.ToLower()) Then
                    filteredData.ImportRow(row)
                End If
            Next
        End If

        totalRecords = filteredData.Rows.Count
        totalPages = If(totalRecords > 0, Math.Ceiling(totalRecords / pageSize), 1)

        ' Load first page of filtered data
        LoadPage(1, filteredData)
    End Sub

    Private Sub LoadPage(pageNumber As Integer, Optional dataSource As DataTable = Nothing)
        If dataSource Is Nothing Then dataSource = allStaffData
        If dataSource Is Nothing OrElse dataSource.Rows.Count = 0 Then
            UsersAccountData.Rows.Clear()
            UpdatePaginationControls()
            lblStaffs.Text = "0"
            Return
        End If

        ' Validate page number
        If pageNumber < 1 Then pageNumber = 1
        If pageNumber > totalPages Then pageNumber = totalPages

        currentPage = pageNumber
        Dim startIndex As Integer = (currentPage - 1) * pageSize
        Dim endIndex As Integer = Math.Min(startIndex + pageSize, dataSource.Rows.Count)

        ' Suspend layout for smoother loading
        UsersAccountData.SuspendLayout()
        UsersAccountData.Rows.Clear()

        Try
            ' Use bulk operation for better performance
            For i As Integer = startIndex To endIndex - 1
                Dim row As DataRow = dataSource.Rows(i)

                ' Get first name
                Dim firstName As String = If(row("FirstName") IsNot DBNull.Value, row("FirstName").ToString().Trim(), "N/A")

                ' Get last name
                Dim lastName As String = If(row("LastName") IsNot DBNull.Value, row("LastName").ToString().Trim(), "N/A")

                ' Format hire date
                Dim hireDate As String = "N/A"
                If row("DateCreated") IsNot DBNull.Value Then
                    Try
                        hireDate = Convert.ToDateTime(row("DateCreated")).ToString("MMMM dd, yyyy")
                    Catch
                        hireDate = row("DateCreated").ToString()
                    End Try
                End If

                ' Add row to DataGridView
                Dim rowIndex As Integer = UsersAccountData.Rows.Add()
                Dim newRow As DataGridViewRow = UsersAccountData.Rows(rowIndex)

                ' Set cell values (matching Designer column names)
                newRow.Cells("txtName").Value = firstName & " " & lastName ' Combined name
                newRow.Cells("colUsername").Value = "N/A" ' Not in database query
                newRow.Cells("colRole").Value = "Staff" ' All are staff
                newRow.Cells("colStatus").Value = "Active" ' Default status
                newRow.Cells("colJoinDate").Value = hireDate

                ' Store ID for delete operations
                newRow.Tag = If(row("ID") IsNot DBNull.Value, Convert.ToInt32(row("ID")), 0)
            Next

        Catch ex As Exception
            MessageBox.Show("Error displaying data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            UsersAccountData.ResumeLayout()
            UpdatePaginationControls()
        End Try
    End Sub

    Private Sub UpdatePaginationControls()
        ' Update page info label
        lblPageInfo.Text = $"Page {currentPage} of {totalPages}"

        ' Enable/disable buttons based on current page
        btnFirstPage.Enabled = (currentPage > 1)
        btnPreviousPage.Enabled = (currentPage > 1)
        btnNextPage.Enabled = (currentPage < totalPages)
        btnLastPage.Enabled = (currentPage < totalPages)

        ' Visual feedback for disabled buttons
        btnFirstPage.BackColor = If(btnFirstPage.Enabled, Color.FromArgb(240, 244, 250), Color.FromArgb(230, 230, 230))
        btnPreviousPage.BackColor = If(btnPreviousPage.Enabled, Color.FromArgb(240, 244, 250), Color.FromArgb(230, 230, 230))
        btnNextPage.BackColor = If(btnNextPage.Enabled, Color.FromArgb(240, 244, 250), Color.FromArgb(230, 230, 230))
        btnLastPage.BackColor = If(btnLastPage.Enabled, Color.FromArgb(240, 244, 250), Color.FromArgb(230, 230, 230))
    End Sub

    Private Sub UsersAccountData_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles UsersAccountData.CellClick
        If e.RowIndex < 0 Then Return
        If e.ColumnIndex < 0 Then Return

        Dim selectedRow As DataGridViewRow = UsersAccountData.Rows(e.RowIndex)

        ' Get the full name from the combined Name column
        Dim fullName As String = If(selectedRow.Cells("txtName").Value IsNot Nothing,
                                    selectedRow.Cells("txtName").Value.ToString().Trim(), "Unknown")

        Dim userID As Integer = If(selectedRow.Tag IsNot Nothing, CInt(selectedRow.Tag), 0)

        If userID = 0 Then
            MessageBox.Show("Invalid user ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' DELETE BUTTON
        If e.ColumnIndex = UsersAccountData.Columns("colDelete").Index Then
            Dim result As DialogResult = MessageBox.Show(
                $"Are you sure you want to delete {fullName}?{vbNewLine}{vbNewLine}This action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            )

            If result = DialogResult.Yes Then
                DeleteStaffMember(userID, fullName)
            End If
        End If

        ' EDIT BUTTON (Optional - add functionality if needed)
        If e.ColumnIndex = UsersAccountData.Columns("colEdit").Index Then
            MessageBox.Show($"Edit functionality for {fullName} coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub DeleteStaffMember(userID As Integer, username As String)
        Try
            openConn()
            Dim query As String = "DELETE FROM employee WHERE EmployeeID = @id AND Position = 'Staff'"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", userID)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show($"{username} has been deleted successfully.",
                              "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadStaffData() ' Reload data
            Else
                MessageBox.Show("No records were deleted. Staff member may not exist.",
                              "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As MySqlException
            MessageBox.Show($"Database error while deleting staff member: {ex.Message}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"Error deleting staff member: {ex.Message}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    ' Pagination button handlers
    Private Sub btnFirstPage_Click(sender As Object, e As EventArgs) Handles btnFirstPage.Click
        If currentPage > 1 Then
            ApplySearchFilter() ' Reload from page 1
        End If
    End Sub

    Private Sub btnPreviousPage_Click(sender As Object, e As EventArgs) Handles btnPreviousPage.Click
        If currentPage > 1 Then
            Dim filteredData As DataTable = GetFilteredData()
            LoadPage(currentPage - 1, filteredData)
        End If
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        If currentPage < totalPages Then
            Dim filteredData As DataTable = GetFilteredData()
            LoadPage(currentPage + 1, filteredData)
        End If
    End Sub

    Private Sub btnLastPage_Click(sender As Object, e As EventArgs) Handles btnLastPage.Click
        If currentPage < totalPages Then
            Dim filteredData As DataTable = GetFilteredData()
            LoadPage(totalPages, filteredData)
        End If
    End Sub

    Private Function GetFilteredData() As DataTable
        If allStaffData Is Nothing Then Return Nothing

        If String.IsNullOrWhiteSpace(searchText) Then
            Return allStaffData
        Else
            Dim filteredData As DataTable = allStaffData.Clone()
            For Each row As DataRow In allStaffData.Rows
                Dim firstName As String = If(row("FirstName") IsNot DBNull.Value, row("FirstName").ToString().ToLower(), "")
                Dim lastName As String = If(row("LastName") IsNot DBNull.Value, row("LastName").ToString().ToLower(), "")

                If firstName.Contains(searchText.ToLower()) OrElse
                   lastName.Contains(searchText.ToLower()) Then
                    filteredData.ImportRow(row)
                End If
            Next
            Return filteredData
        End If
    End Function

    ' Search functionality
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        searchText = txtSearch.Text.Trim()
        ApplySearchFilter()
    End Sub

    ' UI Helper Methods
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

    Private Sub RoundPaginationButtons()
        RoundButton(btnFirstPage)
        RoundButton(btnPreviousPage)
        RoundButton(btnNextPage)
        RoundButton(btnLastPage)
    End Sub

    ' Public methods for external refresh
    Public Sub RefreshData()
        LoadStaffData()
    End Sub

    Public Sub LoadUsers()
        LoadStaffData()
    End Sub

    Private Sub lblStaffs_Click(sender As Object, e As EventArgs) Handles lblStaffs.Click

    End Sub


End Class

' Extension module for DataGridView double buffering
Module DataGridViewExtensions
    <System.Runtime.CompilerServices.Extension()>
    Public Sub DoubleBuffered(ByVal dgv As DataGridView, ByVal setting As Boolean)
        Dim dgvType As Type = dgv.GetType()
        Dim pi As Reflection.PropertyInfo = dgvType.GetProperty("DoubleBuffered",
            Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
        If pi IsNot Nothing Then
            pi.SetValue(dgv, setting, Nothing)
        End If
    End Sub
End Module