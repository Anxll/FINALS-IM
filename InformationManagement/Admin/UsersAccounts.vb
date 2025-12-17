Imports MySqlConnector

Public Class UsersAccounts
    ' Pagination variables
    Private currentPage As Integer = 1
    Private pageSize As Integer = 20
    Private totalRecords As Integer = 0
    Private totalPages As Integer = 0
    Private allStaffData As DataTable
    Private initialLoadComplete As Boolean = False

    Private Class AccountCredentialsResult
        Public Property Result As DialogResult
        Public Property Username As String
        Public Property Password As String ' plain text (will be encrypted before saving)
    End Class

    Private Sub UsersAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDataGridView()
        RoundButton(Adduserbtn)
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

            ' Add User Button (aligned to right)
            Adduserbtn.Location = New Point(formWidth - rightMargin - Adduserbtn.Width, RoundedPane22.Top + 10)

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
            Dim actionColumnWidth As Integer = 90

            ' Calculate available width for data columns
            Dim availableWidth As Integer = totalWidth - (actionColumnWidth * 2) ' 2 action columns

            ' Set column widths proportionally
            UsersAccountData.Columns("txtName").Width = CInt(availableWidth * 0.3) ' 30%
            UsersAccountData.Columns("colUsername").Width = CInt(availableWidth * 0.25) ' 25%
            UsersAccountData.Columns("colRole").Width = CInt(availableWidth * 0.2) ' 20%
            UsersAccountData.Columns("colStatus").Width = CInt(availableWidth * 0.1) ' 10%
            UsersAccountData.Columns("colJoinDate").Width = CInt(availableWidth * 0.15) ' 15%
            UsersAccountData.Columns("colEdit").Width = actionColumnWidth
            UsersAccountData.Columns("colDelete").Width = actionColumnWidth

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
    End Sub

    Private Sub LoadStaffData()
        Try
            openConn()

            ' Load only staff members with improved query
            Dim query As String = "
                SELECT 
                    e.EmployeeID as ID,
                    CONCAT(e.FirstName, ' ', e.LastName) as FullName,
                    COALESCE(
                        (SELECT ua.username FROM user_accounts ua WHERE ua.employee_id = e.EmployeeID LIMIT 1),
                        (SELECT ua.username FROM user_accounts ua WHERE ua.name = CONCAT(e.FirstName, ' ', e.LastName) LIMIT 1)
                    ) as Username,
                    e.Position as Role,
                    e.EmploymentStatus as Status,
                    e.HireDate as DateCreated
                FROM employee e
                WHERE e.Position LIKE '%Staff%'
                ORDER BY e.HireDate DESC"

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            allStaffData = New DataTable()
            adapter.Fill(allStaffData)

            totalRecords = allStaffData.Rows.Count
            totalPages = If(totalRecords > 0, Math.Ceiling(totalRecords / pageSize), 1)

            ' Update staff count
            lblStaffs.Text = totalRecords.ToString()

            ' Load first page
            If totalRecords > 0 Then
                LoadPage(1)
            Else
                UsersAccountData.Rows.Clear()
                UpdatePaginationControls()
            End If

        Catch ex As MySqlException
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error loading staff data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub LoadPage(pageNumber As Integer)
        If allStaffData Is Nothing OrElse allStaffData.Rows.Count = 0 Then
            UpdatePaginationControls()
            Return
        End If

        ' Validate page number
        If pageNumber < 1 Then pageNumber = 1
        If pageNumber > totalPages Then pageNumber = totalPages

        currentPage = pageNumber
        Dim startIndex As Integer = (currentPage - 1) * pageSize
        Dim endIndex As Integer = Math.Min(startIndex + pageSize, totalRecords)

        ' Suspend layout for smoother loading
        UsersAccountData.SuspendLayout()
        UsersAccountData.Rows.Clear()

        Try
            ' Use bulk operation for better performance
            For i As Integer = startIndex To endIndex - 1
                Dim row As DataRow = allStaffData.Rows(i)

                ' Get full name
                Dim fullName As String = If(row("FullName") IsNot DBNull.Value, row("FullName").ToString().Trim(), "N/A")

                ' Get role
                Dim role As String = If(row("Role") IsNot DBNull.Value, row("Role").ToString(), "N/A")

                ' Get username (may be NULL if no account exists yet)
                Dim loginUsername As String = "N/A"
                If allStaffData.Columns.Contains("Username") AndAlso row("Username") IsNot DBNull.Value Then
                    loginUsername = row("Username").ToString().Trim()
                    If loginUsername = "" Then loginUsername = "N/A"
                End If

                ' Get status
                Dim status As String = If(row("Status") IsNot DBNull.Value, row("Status").ToString(), "N/A")

                ' Format join date
                Dim joinDate As String = "N/A"
                If row("DateCreated") IsNot DBNull.Value Then
                    Try
                        joinDate = Convert.ToDateTime(row("DateCreated")).ToString("MMMM dd, yyyy")
                    Catch
                        joinDate = row("DateCreated").ToString()
                    End Try
                End If

                ' Add row to DataGridView
                Dim rowIndex As Integer = UsersAccountData.Rows.Add()
                Dim newRow As DataGridViewRow = UsersAccountData.Rows(rowIndex)

                newRow.Cells("txtName").Value = fullName
                newRow.Cells("colUsername").Value = loginUsername
                newRow.Cells("colRole").Value = role
                newRow.Cells("colStatus").Value = status
                newRow.Cells("colJoinDate").Value = joinDate

                ' Store ID for edit/delete operations
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
        Dim username As String = If(selectedRow.Cells("txtName").Value IsNot Nothing,
                                    selectedRow.Cells("txtName").Value.ToString(), "Unknown")

        Dim userID As Integer = If(selectedRow.Tag IsNot Nothing, CInt(selectedRow.Tag), 0)

        If userID = 0 Then
            MessageBox.Show("Invalid user ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' EDIT BUTTON
        If e.ColumnIndex = UsersAccountData.Columns("colEdit").Index Then
            EditUserAccountCredentials(userID, username)

            ' DELETE BUTTON
        ElseIf e.ColumnIndex = UsersAccountData.Columns("colDelete").Index Then
            Dim result As DialogResult = MessageBox.Show(
                $"Are you sure you want to delete {username}?{vbNewLine}{vbNewLine}This action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            )

            If result = DialogResult.Yes Then
                DeleteStaffMember(userID, username)
            End If
        End If
    End Sub

    Private Sub EditUserAccountCredentials(employeeId As Integer, staffDisplayName As String)
        Dim accountId As Integer = 0
        Dim currentUsername As String = ""
        Dim foundByFallback As Boolean = False

        Try
            openConn()

            Dim hasEmployeeId As Boolean = UserAccountsHasColumn("employee_id")

            ' 1) Preferred: find account linked to this employee_id (if the column exists)
            If hasEmployeeId Then
                Dim lookupSql As String = "SELECT id, username FROM user_accounts WHERE employee_id = @eid LIMIT 1"
                Using lookupCmd As New MySqlCommand(lookupSql, conn)
                    lookupCmd.Parameters.AddWithValue("@eid", employeeId)
                    Using reader As MySqlDataReader = lookupCmd.ExecuteReader()
                        If reader.Read() Then
                            accountId = Convert.ToInt32(reader("id"))
                            currentUsername = reader("username").ToString()
                        End If
                    End Using
                End Using
            End If

            ' 2) Fallback: older records may not have employee_id set
            If accountId = 0 Then
                Dim fallbackSql As String = "SELECT id, username, employee_id FROM user_accounts " &
                                            "WHERE (name = @name OR username = @name) " &
                                            "ORDER BY (employee_id IS NULL) DESC, id DESC LIMIT 1"
                Using fallbackCmd As New MySqlCommand(fallbackSql, conn)
                    fallbackCmd.Parameters.AddWithValue("@name", staffDisplayName)
                    Using reader As MySqlDataReader = fallbackCmd.ExecuteReader()
                        If reader.Read() Then
                            accountId = Convert.ToInt32(reader("id"))
                            currentUsername = reader("username").ToString()
                            foundByFallback = True
                        End If
                    End Using
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show("Error looking up account: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            closeConn()
        End Try

        If accountId = 0 Then
            Dim createNow As DialogResult = MessageBox.Show(
                $"No account found for {staffDisplayName}.{vbNewLine}{vbNewLine}Do you want to create their login account now?",
                "No Account",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

            If createNow <> DialogResult.Yes Then Return

            Dim suggested As String = staffDisplayName.Replace(" ", "").ToLower()
            Dim newCreds = PromptCredentials("Create Account", staffDisplayName, suggestedUsername:=suggested, allowBlankPasswordToKeep:=False)
            If newCreds Is Nothing OrElse newCreds.Result <> DialogResult.OK Then Return

            Try
                openConn()

                ' Ensure employee_id column exists (older DB)
                Try
                    Dim colCheckSql As String = "SELECT COUNT(*) FROM information_schema.COLUMNS " &
                                                "WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'user_accounts' AND COLUMN_NAME = 'employee_id'"
                    Using colCheckCmd As New MySqlCommand(colCheckSql, conn)
                        Dim colCount As Integer = Convert.ToInt32(colCheckCmd.ExecuteScalar())
                        If colCount = 0 Then
                            Using alterCmd As New MySqlCommand("ALTER TABLE user_accounts ADD COLUMN employee_id INT NULL", conn)
                                alterCmd.ExecuteNonQuery()
                            End Using
                        End If
                    End Using
                Catch
                    ' ignore
                End Try

                ' Username unique
                Using existsUserCmd As New MySqlCommand("SELECT COUNT(*) FROM user_accounts WHERE username = @u", conn)
                    existsUserCmd.Parameters.AddWithValue("@u", newCreds.Username)
                    Dim cnt As Integer = Convert.ToInt32(existsUserCmd.ExecuteScalar())
                    If cnt > 0 Then
                        MessageBox.Show("Username already exists. Please choose another.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using

                Dim encPass As String = Encrypt(newCreds.Password)
                Dim insertSql As String = "INSERT INTO user_accounts (employee_id, name, username, password, type, position, created_at) " &
                                          "VALUES (@eid, @name, @username, @pass, 1, @pos, NOW())"

                Dim role As String = ""
                Try
                    If UsersAccountData.SelectedRows IsNot Nothing AndAlso UsersAccountData.SelectedRows.Count > 0 Then
                        Dim sr As DataGridViewRow = UsersAccountData.SelectedRows(0)
                        role = If(sr.Cells("colRole").Value IsNot Nothing, sr.Cells("colRole").Value.ToString(), "")
                    End If
                Catch
                    role = ""
                End Try

                Using insertCmd As New MySqlCommand(insertSql, conn)
                    insertCmd.Parameters.AddWithValue("@eid", employeeId)
                    insertCmd.Parameters.AddWithValue("@name", staffDisplayName)
                    insertCmd.Parameters.AddWithValue("@username", newCreds.Username)
                    insertCmd.Parameters.AddWithValue("@pass", encPass)
                    insertCmd.Parameters.AddWithValue("@pos", role)
                    insertCmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadStaffData()
            Catch ex As Exception
                MessageBox.Show("Error creating account: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                closeConn()
            End Try

            Return
        End If

        ' If we found an existing account by name/username match, auto-link it to this employee_id for future edits.
        If foundByFallback Then
            Try
                openConn()

                ' Safety: ensure no other account is already linked to this employee_id
                If UserAccountsHasColumn("employee_id") Then
                    Using linkedCmd As New MySqlCommand("SELECT COUNT(*) FROM user_accounts WHERE employee_id = @eid AND id <> @id", conn)
                        linkedCmd.Parameters.AddWithValue("@eid", employeeId)
                        linkedCmd.Parameters.AddWithValue("@id", accountId)
                        Dim cnt As Integer = Convert.ToInt32(linkedCmd.ExecuteScalar())
                        If cnt = 0 Then
                            Using linkCmd As New MySqlCommand("UPDATE user_accounts SET employee_id = @eid WHERE id = @id AND (employee_id IS NULL OR employee_id = 0)", conn)
                                linkCmd.Parameters.AddWithValue("@eid", employeeId)
                                linkCmd.Parameters.AddWithValue("@id", accountId)
                                linkCmd.ExecuteNonQuery()
                            End Using
                        End If
                    End Using
                End If
            Catch
                ' Best effort; ignore auto-linking failures
            Finally
                closeConn()
            End Try
        End If

        Dim creds = PromptCredentials("Edit Account", staffDisplayName, currentUsername, allowBlankPasswordToKeep:=True)
        If creds Is Nothing OrElse creds.Result <> DialogResult.OK Then Return

        Try
            openConn()

            ' Validate unique username if changed
            If Not String.Equals(creds.Username, currentUsername, StringComparison.OrdinalIgnoreCase) Then
                Dim existsSql As String = "SELECT COUNT(*) FROM user_accounts WHERE username = @u AND id <> @id"
                Using existsCmd As New MySqlCommand(existsSql, conn)
                    existsCmd.Parameters.AddWithValue("@u", creds.Username)
                    existsCmd.Parameters.AddWithValue("@id", accountId)
                    Dim cnt As Integer = Convert.ToInt32(existsCmd.ExecuteScalar())
                    If cnt > 0 Then
                        MessageBox.Show("Username already exists. Please choose another.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End Using
            End If

            If String.IsNullOrWhiteSpace(creds.Password) Then
                Using updateCmd As New MySqlCommand("UPDATE user_accounts SET username = @u WHERE id = @id", conn)
                    updateCmd.Parameters.AddWithValue("@u", creds.Username)
                    updateCmd.Parameters.AddWithValue("@id", accountId)
                    updateCmd.ExecuteNonQuery()
                End Using
            Else
                Dim enc As String = Encrypt(creds.Password)
                Using updateCmd As New MySqlCommand("UPDATE user_accounts SET username = @u, password = @p WHERE id = @id", conn)
                    updateCmd.Parameters.AddWithValue("@u", creds.Username)
                    updateCmd.Parameters.AddWithValue("@p", enc)
                    updateCmd.Parameters.AddWithValue("@id", accountId)
                    updateCmd.ExecuteNonQuery()
                End Using
            End If

            MessageBox.Show("Account updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadStaffData()
        Catch ex As Exception
            MessageBox.Show("Error updating account: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Sub DeleteStaffMember(userID As Integer, username As String)
        Try
            openConn()
            Dim query As String = "DELETE FROM employee WHERE EmployeeID = @id"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", userID)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show($"{username} has been deleted successfully.",
                              "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadStaffData() ' Reload data
            Else
                MessageBox.Show("No records were deleted.",
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
            LoadPage(1)
        End If
    End Sub

    Private Sub btnPreviousPage_Click(sender As Object, e As EventArgs) Handles btnPreviousPage.Click
        If currentPage > 1 Then
            LoadPage(currentPage - 1)
        End If
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        If currentPage < totalPages Then
            LoadPage(currentPage + 1)
        End If
    End Sub

    Private Sub btnLastPage_Click(sender As Object, e As EventArgs) Handles btnLastPage.Click
        If currentPage < totalPages Then
            LoadPage(totalPages)
        End If
    End Sub
    Private Sub Adduserbtn_Click(sender As Object, e As EventArgs) Handles Adduserbtn.Click
        ' Create a login account for the selected staff member (links employee_id in user_accounts)
        If UsersAccountData.SelectedRows Is Nothing OrElse UsersAccountData.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a staff member first, then click Add User to create their account.",
                            "Select Staff",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
            Return
        End If

        Dim selectedRow As DataGridViewRow = UsersAccountData.SelectedRows(0)
        Dim employeeId As Integer = If(selectedRow.Tag IsNot Nothing, CInt(selectedRow.Tag), 0)
        If employeeId <= 0 Then
            MessageBox.Show("Invalid EmployeeID for the selected staff member.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim staffName As String = If(selectedRow.Cells("txtName").Value IsNot Nothing, selectedRow.Cells("txtName").Value.ToString(), "Unknown")
        Dim role As String = If(selectedRow.Cells("colRole").Value IsNot Nothing, selectedRow.Cells("colRole").Value.ToString(), "")

        ' If an account already exists, go straight to edit
        Try
            openConn()
            Using existsCmd As New MySqlCommand("SELECT COUNT(*) FROM user_accounts WHERE employee_id = @eid", conn)
                existsCmd.Parameters.AddWithValue("@eid", employeeId)
                Dim cnt As Integer = Convert.ToInt32(existsCmd.ExecuteScalar())
                If cnt > 0 Then
                    closeConn()
                    EditUserAccountCredentials(employeeId, staffName)
                    Return
                End If
            End Using

            ' Fallback: if an old account exists but isn't linked yet, link it and edit
            Using fallbackCmd As New MySqlCommand("SELECT id FROM user_accounts WHERE (name = @name OR username = @name) LIMIT 1", conn)
                fallbackCmd.Parameters.AddWithValue("@name", staffName)
                Dim existingIdObj As Object = fallbackCmd.ExecuteScalar()
                If existingIdObj IsNot Nothing AndAlso existingIdObj IsNot DBNull.Value Then
                    Dim existingId As Integer = Convert.ToInt32(existingIdObj)
                    Using linkCmd As New MySqlCommand("UPDATE user_accounts SET employee_id = @eid WHERE id = @id AND (employee_id IS NULL OR employee_id = 0)", conn)
                        linkCmd.Parameters.AddWithValue("@eid", employeeId)
                        linkCmd.Parameters.AddWithValue("@id", existingId)
                        linkCmd.ExecuteNonQuery()
                    End Using

                    closeConn()
                    EditUserAccountCredentials(employeeId, staffName)
                    Return
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking existing account: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            closeConn()
        End Try

        Dim creds = PromptCredentials("Create Account", staffName, suggestedUsername:=staffName.Replace(" ", "").ToLower(), allowBlankPasswordToKeep:=False)
        If creds Is Nothing OrElse creds.Result <> DialogResult.OK Then Return

        Try
            openConn()

            ' Ensure employee_id column exists (older DB)
            Try
                Dim colCheckSql As String = "SELECT COUNT(*) FROM information_schema.COLUMNS " &
                                            "WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'user_accounts' AND COLUMN_NAME = 'employee_id'"
                Using colCheckCmd As New MySqlCommand(colCheckSql, conn)
                    Dim colCount As Integer = Convert.ToInt32(colCheckCmd.ExecuteScalar())
                    If colCount = 0 Then
                        Using alterCmd As New MySqlCommand("ALTER TABLE user_accounts ADD COLUMN employee_id INT NULL", conn)
                            alterCmd.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            Catch
                ' ignore
            End Try

            ' Username unique
            Using existsUserCmd As New MySqlCommand("SELECT COUNT(*) FROM user_accounts WHERE username = @u", conn)
                existsUserCmd.Parameters.AddWithValue("@u", creds.Username)
                Dim cnt As Integer = Convert.ToInt32(existsUserCmd.ExecuteScalar())
                If cnt > 0 Then
                    MessageBox.Show("Username already exists. Please choose another.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End Using

            Dim encPass As String = Encrypt(creds.Password)
            Dim insertSql As String = "INSERT INTO user_accounts (employee_id, name, username, password, type, position, created_at) " &
                                      "VALUES (@eid, @name, @username, @pass, 1, @pos, NOW())"
            Using insertCmd As New MySqlCommand(insertSql, conn)
                insertCmd.Parameters.AddWithValue("@eid", employeeId)
                insertCmd.Parameters.AddWithValue("@name", staffName)
                insertCmd.Parameters.AddWithValue("@username", creds.Username)
                insertCmd.Parameters.AddWithValue("@pass", encPass)
                insertCmd.Parameters.AddWithValue("@pos", role)
                insertCmd.ExecuteNonQuery()
            End Using

            MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error creating account: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
        End Try
    End Sub

    Private Function PromptCredentials(title As String, staffName As String, suggestedUsername As String, allowBlankPasswordToKeep As Boolean) As AccountCredentialsResult
        Dim result As New AccountCredentialsResult With {.Result = DialogResult.Cancel, .Username = suggestedUsername, .Password = ""}

        Using dlg As New Form()
            dlg.Text = title
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.MaximizeBox = False
            dlg.MinimizeBox = False
            dlg.ShowInTaskbar = False
            dlg.Width = 420
            dlg.Height = 280

            Dim lblInfo As New Label() With {.Left = 12, .Top = 12, .AutoSize = True, .Text = $"Staff: {staffName}"}

            Dim lblUser As New Label() With {.Left = 12, .Top = 52, .AutoSize = True, .Text = "Username"}
            Dim txtUser As New TextBox() With {.Left = 12, .Top = 72, .Width = 370, .Text = suggestedUsername}

            Dim passHint As String = If(allowBlankPasswordToKeep, "New Password (leave blank to keep current)", "Password")
            Dim lblPass As New Label() With {.Left = 12, .Top = 106, .AutoSize = True, .Text = passHint}
            Dim txtPass As New TextBox() With {.Left = 12, .Top = 126, .Width = 370, .UseSystemPasswordChar = True}

            Dim lblConfirm As New Label() With {.Left = 12, .Top = 156, .AutoSize = True, .Text = "Confirm Password"}
            Dim txtConfirm As New TextBox() With {.Left = 12, .Top = 176, .Width = 370, .UseSystemPasswordChar = True}

            Dim chkShow As New CheckBox() With {.Left = 12, .Top = 204, .AutoSize = True, .Text = "Show password"}
            AddHandler chkShow.CheckedChanged, Sub()
                                                   txtPass.UseSystemPasswordChar = Not chkShow.Checked
                                                   txtConfirm.UseSystemPasswordChar = Not chkShow.Checked
                                               End Sub

            Dim btnOk As New Button() With {.Text = "Save", .Left = 226, .Top = 228, .Width = 75, .DialogResult = DialogResult.OK}
            Dim btnCancel As New Button() With {.Text = "Cancel", .Left = 307, .Top = 228, .Width = 75, .DialogResult = DialogResult.Cancel}

            dlg.AcceptButton = btnOk
            dlg.CancelButton = btnCancel

            dlg.Controls.Add(lblInfo)
            dlg.Controls.Add(lblUser)
            dlg.Controls.Add(txtUser)
            dlg.Controls.Add(lblPass)
            dlg.Controls.Add(txtPass)
            dlg.Controls.Add(lblConfirm)
            dlg.Controls.Add(txtConfirm)
            dlg.Controls.Add(chkShow)
            dlg.Controls.Add(btnOk)
            dlg.Controls.Add(btnCancel)

            ' This form is hosted inside the dashboard panel (TopLevel=False),
            ' so passing it as an owner can prevent the dialog from showing.
            Dim dlgResult As DialogResult = dlg.ShowDialog()
            If dlgResult <> DialogResult.OK Then
                result.Result = dlgResult
                Return result
            End If

            Dim newUsername As String = txtUser.Text.Trim()
            If String.IsNullOrWhiteSpace(newUsername) Then
                MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                result.Result = DialogResult.Cancel
                Return result
            End If

            Dim pass As String = txtPass.Text
            If Not String.IsNullOrWhiteSpace(pass) Then
                If pass <> txtConfirm.Text Then
                    MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    result.Result = DialogResult.Cancel
                    Return result
                End If
            Else
                If Not allowBlankPasswordToKeep Then
                    MessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    result.Result = DialogResult.Cancel
                    Return result
                End If
            End If

            result.Result = DialogResult.OK
            result.Username = newUsername
            result.Password = pass
            Return result
        End Using
    End Function

    Private Function UserAccountsHasColumn(columnName As String) As Boolean
        Try
            Dim sql As String = "SELECT COUNT(*) FROM information_schema.COLUMNS " &
                                "WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'user_accounts' AND COLUMN_NAME = @c"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@c", columnName)
                Dim cnt As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return cnt > 0
            End Using
        Catch
            Return False
        End Try
    End Function

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