Imports MySqlConnector
Imports System.Data

Public Class Reservations
    ' ==========================================
    ' PAGINATION VARIABLES
    ' ==========================================
    Private CurrentPage As Integer = 1
    Private RecordsPerPage As Integer = 50
    Private TotalRecords As Integer = 0
    Private CurrentCondition As String = ""

    Private Sub Reservations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize pagination dropdown
        cboRecordsPerPage.Items.Clear()
        cboRecordsPerPage.Items.AddRange(New Object() {10, 25, 50, 100})
        cboRecordsPerPage.SelectedItem = 50

        LoadReservations()
    End Sub

    ' ==========================================
    ' UPDATE STATUS BUTTON (Form Button)
    ' ==========================================
    Private Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        Try
            ' Check if a row is selected
            If Reservation.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a reservation to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get selected reservation details
            Dim selectedRow As DataGridViewRow = Reservation.SelectedRows(0)
            Dim reservationID As Integer = Convert.ToInt32(selectedRow.Cells("ReservationID").Value)
            Dim currentStatus As String = selectedRow.Cells("ReservationStatus").Value.ToString().Trim()

            ' Show dialog to select new status
            Dim statusForm As New Form()
            statusForm.Text = "Update Reservation Status"
            statusForm.Size = New Size(400, 250)
            statusForm.StartPosition = FormStartPosition.CenterParent
            statusForm.FormBorderStyle = FormBorderStyle.FixedDialog
            statusForm.MaximizeBox = False
            statusForm.MinimizeBox = False

            ' Label
            Dim lblInfo As New Label()
            lblInfo.Text = $"Reservation ID: {reservationID}" & vbCrLf &
                          $"Current Status: {currentStatus}" & vbCrLf & vbCrLf &
                          "Select new status:"
            lblInfo.Location = New Point(20, 20)
            lblInfo.Size = New Size(350, 70)
            lblInfo.Font = New Font("Segoe UI", 10)
            statusForm.Controls.Add(lblInfo)

            ' Radio buttons for status options
            Dim rbPending As New RadioButton()
            rbPending.Text = "Pending"
            rbPending.Location = New Point(30, 100)
            rbPending.Size = New Size(100, 25)
            rbPending.Font = New Font("Segoe UI", 10)
            rbPending.Checked = (currentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
            statusForm.Controls.Add(rbPending)

            Dim rbConfirmed As New RadioButton()
            rbConfirmed.Text = "Confirmed"
            rbConfirmed.Location = New Point(140, 100)
            rbConfirmed.Size = New Size(110, 25)
            rbConfirmed.Font = New Font("Segoe UI", 10)
            rbConfirmed.Checked = (currentStatus.Equals("Confirmed", StringComparison.OrdinalIgnoreCase))
            statusForm.Controls.Add(rbConfirmed)

            Dim rbCancelled As New RadioButton()
            rbCancelled.Text = "Cancelled"
            rbCancelled.Location = New Point(260, 100)
            rbCancelled.Size = New Size(110, 25)
            rbCancelled.Font = New Font("Segoe UI", 10)
            rbCancelled.Checked = (currentStatus.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
            statusForm.Controls.Add(rbCancelled)

            ' Ensure at least one is checked if none match
            If Not (rbPending.Checked Or rbConfirmed.Checked Or rbCancelled.Checked) Then
                rbPending.Checked = True
            End If

            ' Buttons
            Dim btnOK As New Button()
            btnOK.Text = "Update"
            btnOK.Location = New Point(200, 160)
            btnOK.Size = New Size(80, 35)
            btnOK.DialogResult = DialogResult.OK
            btnOK.Font = New Font("Segoe UI", 9)
            statusForm.Controls.Add(btnOK)

            Dim btnCancel As New Button()
            btnCancel.Text = "Cancel"
            btnCancel.Location = New Point(290, 160)
            btnCancel.Size = New Size(80, 35)
            btnCancel.DialogResult = DialogResult.Cancel
            btnCancel.Font = New Font("Segoe UI", 9)
            statusForm.Controls.Add(btnCancel)

            statusForm.AcceptButton = btnOK
            statusForm.CancelButton = btnCancel

            ' Show the dialog
            If statusForm.ShowDialog() = DialogResult.OK Then
                Dim newStatus As String = ""

                If rbPending.Checked Then
                    newStatus = "Pending"
                ElseIf rbConfirmed.Checked Then
                    newStatus = "Confirmed"
                ElseIf rbCancelled.Checked Then
                    newStatus = "Cancelled"
                End If

                ' Check if status is actually changing (case-insensitive comparison)
                If newStatus.Equals(currentStatus, StringComparison.OrdinalIgnoreCase) Then
                    MessageBox.Show($"Reservation status is already '{currentStatus}'.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' Update the reservation status
                UpdateReservationStatus(reservationID, newStatus)
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating reservation status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==========================================
    ' UPDATE RESERVATION STATUS
    ' ==========================================
    Private Sub UpdateReservationStatus(reservationID As Integer, newStatus As String)
        Try
            openConn()

            ' Use parameterized query to prevent SQL injection
            Dim query As String = "UPDATE reservations SET ReservationStatus = @status, UpdatedDate = @updated WHERE ReservationID = @id"

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@status", newStatus)
                cmd.Parameters.AddWithValue("@updated", DateTime.Now)
                cmd.Parameters.AddWithValue("@id", reservationID)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected = 0 Then
                    MessageBox.Show("No reservation was updated. Please check if the reservation exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    closeConn()
                    Return
                End If
            End Using

            MessageBox.Show($"Reservation #{reservationID} has been updated to '{newStatus}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As MySqlException
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error updating reservation: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConn()
            LoadReservations(CurrentCondition)
        End Try
    End Sub

    ' ==========================================
    ' LOAD RESERVATIONS WITH PAGINATION
    ' ==========================================
    Private Sub LoadReservations(Optional condition As String = "")
        Try
            CurrentCondition = condition

            ' Get total count first
            Dim countQuery As String = "SELECT COUNT(*) FROM reservations r INNER JOIN customers c ON r.CustomerID = c.CustomerID"
            If condition <> "" Then
                countQuery &= " WHERE " & condition
            End If

            openConn()
            Dim countCmd As New MySqlCommand(countQuery, conn)
            TotalRecords = Convert.ToInt32(countCmd.ExecuteScalar())
            closeConn()

            ' Build main query with pagination
            Dim query As String =
        "SELECT 
            r.ReservationID,
            r.CustomerID,
            c.FirstName,
            c.LastName,
            c.Email,
            c.ContactNumber AS CustomerContact,
            r.ContactNumber AS ReservationContact,
            r.ReservationType,
            r.EventType,
            r.EventDate,
            r.EventTime,
            r.NumberOfGuests,
            r.ProductSelection,
            r.SpecialRequests,
            r.ReservationStatus,
            r.ReservationDate,
            r.DeliveryAddress,
            r.DeliveryOption,
            r.UpdatedDate
         FROM reservations r
         INNER JOIN customers c ON r.CustomerID = c.CustomerID"

            If condition <> "" Then
                query &= " WHERE " & condition
            End If

            query &= " ORDER BY r.ReservationDate DESC"

            ' Add pagination
            Dim offset As Integer = (CurrentPage - 1) * RecordsPerPage
            query &= $" LIMIT {RecordsPerPage} OFFSET {offset}"

            ' Load results into DGV
            LoadToDGV(query, Reservation)

            ' Update pagination info
            UpdatePaginationInfo()

            ' Format the DataGridView columns
            FormatReservationColumns()

        Catch ex As Exception
            MessageBox.Show("Error loading reservations: " & ex.Message)
        End Try
    End Sub

    ' ==========================================
    ' UPDATE PAGINATION INFO
    ' ==========================================
    Private Sub UpdatePaginationInfo()
        Try
            Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)

            ' Update label with current info
            lblTotalReservations.Text = $"Total: {TotalRecords} | Page {CurrentPage} of {totalPages}"

            ' Enable/disable navigation buttons
            btnFirstPage.Enabled = (CurrentPage > 1)
            btnPrevPage.Enabled = (CurrentPage > 1)
            btnNextPage.Enabled = (CurrentPage < totalPages)
            btnLastPage.Enabled = (CurrentPage < totalPages)

        Catch ex As Exception
            ' Silently handle errors
        End Try
    End Sub

    ' ==========================================
    ' FORMAT DATAGRIDVIEW COLUMNS
    ' ==========================================
    Private Sub FormatReservationColumns()
        Try
            With Reservation
                ' Hide ID columns
                If .Columns.Contains("ReservationID") Then
                    .Columns("ReservationID").Visible = False
                End If

                If .Columns.Contains("CustomerID") Then
                    .Columns("CustomerID").Visible = False
                End If

                ' Set specific column widths - Customer Info
                If .Columns.Contains("FirstName") Then
                    .Columns("FirstName").HeaderText = "First Name"
                    .Columns("FirstName").Width = 120
                    .Columns("FirstName").DisplayIndex = 0
                End If

                If .Columns.Contains("LastName") Then
                    .Columns("LastName").HeaderText = "Last Name"
                    .Columns("LastName").Width = 120
                    .Columns("LastName").DisplayIndex = 1
                End If

                If .Columns.Contains("Email") Then
                    .Columns("Email").HeaderText = "Email"
                    .Columns("Email").Width = 180
                    .Columns("Email").DisplayIndex = 2
                End If

                If .Columns.Contains("CustomerContact") Then
                    .Columns("CustomerContact").HeaderText = "Customer Phone"
                    .Columns("CustomerContact").Width = 120
                    .Columns("CustomerContact").DisplayIndex = 3
                End If

                If .Columns.Contains("ReservationContact") Then
                    .Columns("ReservationContact").HeaderText = "Reservation Phone"
                    .Columns("ReservationContact").Width = 130
                    .Columns("ReservationContact").DisplayIndex = 4
                End If

                ' Reservation Details
                If .Columns.Contains("ReservationType") Then
                    .Columns("ReservationType").HeaderText = "Type"
                    .Columns("ReservationType").Width = 100
                    .Columns("ReservationType").DisplayIndex = 5
                End If

                If .Columns.Contains("EventType") Then
                    .Columns("EventType").HeaderText = "Event"
                    .Columns("EventType").Width = 120
                    .Columns("EventType").DisplayIndex = 6
                End If

                If .Columns.Contains("EventDate") Then
                    .Columns("EventDate").HeaderText = "Event Date"
                    .Columns("EventDate").Width = 100
                    .Columns("EventDate").DefaultCellStyle.Format = "MM/dd/yyyy"
                    .Columns("EventDate").DisplayIndex = 7
                End If

                If .Columns.Contains("EventTime") Then
                    .Columns("EventTime").HeaderText = "Time"
                    .Columns("EventTime").Width = 80
                    .Columns("EventTime").DisplayIndex = 8
                End If

                If .Columns.Contains("NumberOfGuests") Then
                    .Columns("NumberOfGuests").HeaderText = "Guests"
                    .Columns("NumberOfGuests").Width = 70
                    .Columns("NumberOfGuests").DisplayIndex = 9
                End If

                If .Columns.Contains("ReservationStatus") Then
                    .Columns("ReservationStatus").HeaderText = "Status"
                    .Columns("ReservationStatus").Width = 90
                    .Columns("ReservationStatus").DisplayIndex = 10
                End If

                If .Columns.Contains("ReservationDate") Then
                    .Columns("ReservationDate").HeaderText = "Reserved On"
                    .Columns("ReservationDate").Width = 100
                    .Columns("ReservationDate").DefaultCellStyle.Format = "MM/dd/yyyy"
                    .Columns("ReservationDate").DisplayIndex = 11
                End If

                If .Columns.Contains("ProductSelection") Then
                    .Columns("ProductSelection").HeaderText = "Products"
                    .Columns("ProductSelection").Width = 150
                    .Columns("ProductSelection").DisplayIndex = 12
                End If

                If .Columns.Contains("DeliveryOption") Then
                    .Columns("DeliveryOption").HeaderText = "Delivery"
                    .Columns("DeliveryOption").Width = 90
                    .Columns("DeliveryOption").DisplayIndex = 13
                End If

                If .Columns.Contains("DeliveryAddress") Then
                    .Columns("DeliveryAddress").HeaderText = "Address"
                    .Columns("DeliveryAddress").Width = 180
                    .Columns("DeliveryAddress").DisplayIndex = 14
                End If

                If .Columns.Contains("SpecialRequests") Then
                    .Columns("SpecialRequests").HeaderText = "Special Requests"
                    .Columns("SpecialRequests").Width = 150
                    .Columns("SpecialRequests").DisplayIndex = 15
                End If

                If .Columns.Contains("UpdatedDate") Then
                    .Columns("UpdatedDate").HeaderText = "Last Updated"
                    .Columns("UpdatedDate").Width = 100
                    .Columns("UpdatedDate").DefaultCellStyle.Format = "MM/dd/yyyy"
                    .Columns("UpdatedDate").DisplayIndex = 16
                End If

                ' Disable auto-sizing to keep fixed widths
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

                ' Optional: Allow horizontal scrolling if needed
                .ScrollBars = ScrollBars.Both

            End With

        Catch ex As Exception
            ' Silently handle formatting errors
        End Try
    End Sub

    ' ==========================================
    ' GENERIC LOAD FUNCTION
    ' ==========================================
    Private Sub LoadToDGV(query As String, dgv As DataGridView)
        Try
            openConn()

            Dim cmd As New MySqlCommand(query, conn)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()

            adapter.Fill(dt)
            dgv.DataSource = dt

            closeConn()

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
            closeConn()
        End Try
    End Sub

    ' ==========================================
    ' VIEW ALL
    ' ==========================================
    Private Sub btnViewAll_Click(sender As Object, e As EventArgs) Handles btnViewAll.Click
        CurrentPage = 1
        LoadReservations()
        lblFilter.Text = "Showing: All Reservations"
    End Sub

    ' ==========================================
    ' VIEW PENDING
    ' ==========================================
    Private Sub btnViewPending_Click(sender As Object, e As EventArgs) Handles btnViewPending.Click
        CurrentPage = 1
        LoadReservations("r.ReservationStatus = 'Pending'")
        lblFilter.Text = "Showing: Pending"
    End Sub

    ' ==========================================
    ' VIEW CONFIRMED
    ' ==========================================
    Private Sub btnViewConfirmed_Click(sender As Object, e As EventArgs) Handles btnViewConfirmed.Click
        CurrentPage = 1
        LoadReservations("r.ReservationStatus = 'Confirmed'")
        lblFilter.Text = "Showing: Confirmed"
    End Sub

    ' ==========================================
    ' VIEW CANCELLED
    ' ==========================================
    Private Sub btnViewCancelled_Click(sender As Object, e As EventArgs) Handles btnViewCancelled.Click
        CurrentPage = 1
        LoadReservations("r.ReservationStatus = 'Cancelled'")
        lblFilter.Text = "Showing: Cancelled"
    End Sub

    ' ==========================================
    ' REFRESH
    ' ==========================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        CurrentPage = 1
        LoadReservations(CurrentCondition)
        If CurrentCondition = "" Then
            lblFilter.Text = "Showing: All Reservations"
        End If
    End Sub

    ' ==========================================
    ' SEARCH BAR (FIXED - NOW USES PARAMETERS)
    ' ==========================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim keyword As String = txtSearch.Text.Trim()

        If keyword = "" Then
            CurrentPage = 1
            LoadReservations()
            Exit Sub
        End If

        ' Use parameterized search
        CurrentPage = 1
        SearchReservations(keyword)
    End Sub

    ' ==========================================
    ' SEARCH RESERVATIONS (NEW - SECURE)
    ' ==========================================
    Private Sub SearchReservations(keyword As String)
        Try
            ' Get total count for search results
            Dim countQuery As String = "SELECT COUNT(*) FROM reservations r " &
                                      "INNER JOIN customers c ON r.CustomerID = c.CustomerID " &
                                      "WHERE CAST(r.ReservationID AS CHAR) LIKE @keyword " &
                                      "OR CAST(r.CustomerID AS CHAR) LIKE @keyword " &
                                      "OR c.FirstName LIKE @keyword " &
                                      "OR c.LastName LIKE @keyword " &
                                      "OR r.EventType LIKE @keyword " &
                                      "OR r.ReservationStatus LIKE @keyword"

            openConn()
            Dim countCmd As New MySqlCommand(countQuery, conn)
            countCmd.Parameters.AddWithValue("@keyword", "%" & keyword & "%")
            TotalRecords = Convert.ToInt32(countCmd.ExecuteScalar())
            closeConn()

            ' Build main search query
            Dim query As String =
        "SELECT 
            r.ReservationID,
            r.CustomerID,
            c.FirstName,
            c.LastName,
            c.Email,
            c.ContactNumber AS CustomerContact,
            r.ContactNumber AS ReservationContact,
            r.ReservationType,
            r.EventType,
            r.EventDate,
            r.EventTime,
            r.NumberOfGuests,
            r.ProductSelection,
            r.SpecialRequests,
            r.ReservationStatus,
            r.ReservationDate,
            r.DeliveryAddress,
            r.DeliveryOption,
            r.UpdatedDate
         FROM reservations r
         INNER JOIN customers c ON r.CustomerID = c.CustomerID
         WHERE CAST(r.ReservationID AS CHAR) LIKE @keyword 
         OR CAST(r.CustomerID AS CHAR) LIKE @keyword 
         OR c.FirstName LIKE @keyword 
         OR c.LastName LIKE @keyword 
         OR r.EventType LIKE @keyword 
         OR r.ReservationStatus LIKE @keyword
         ORDER BY r.ReservationDate DESC"

            ' Add pagination
            Dim offset As Integer = (CurrentPage - 1) * RecordsPerPage
            query &= $" LIMIT {RecordsPerPage} OFFSET {offset}"

            ' Load with parameters
            openConn()
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@keyword", "%" & keyword & "%")

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Reservation.DataSource = dt
            closeConn()

            ' Update pagination info
            UpdatePaginationInfo()
            FormatReservationColumns()

            lblFilter.Text = $"Search results for: {keyword}"

        Catch ex As Exception
            MessageBox.Show("Error searching reservations: " & ex.Message)
            closeConn()
        End Try
    End Sub

    ' ==========================================
    ' DELETE RESERVATION
    ' ==========================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If Reservation.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a reservation to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim id As Integer = Reservation.SelectedRows(0).Cells("ReservationID").Value

        If MessageBox.Show("Are you sure to delete Reservation #" & id & "?",
                           "Confirm Delete",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning) = DialogResult.No Then Exit Sub

        Try
            openConn()

            Dim cmd As New MySqlCommand("DELETE FROM reservations WHERE ReservationID=@id", conn)
            cmd.Parameters.AddWithValue("@id", id)
            cmd.ExecuteNonQuery()

            closeConn()

            MessageBox.Show("Reservation deleted successfully.")
            LoadReservations(CurrentCondition)

        Catch ex As Exception
            MessageBox.Show("Error deleting reservation: " & ex.Message)
            closeConn()
        End Try

    End Sub

    ' ============================================================
    ' PAGINATION BUTTON EVENTS
    ' ============================================================
    Private Sub btnFirstPage_Click(sender As Object, e As EventArgs) Handles btnFirstPage.Click
        CurrentPage = 1

        If txtSearch.Text.Trim() <> "" Then
            SearchReservations(txtSearch.Text.Trim())
        Else
            LoadReservations(CurrentCondition)
        End If
    End Sub

    Private Sub btnPrevPage_Click(sender As Object, e As EventArgs) Handles btnPrevPage.Click
        If CurrentPage > 1 Then
            CurrentPage -= 1

            If txtSearch.Text.Trim() <> "" Then
                SearchReservations(txtSearch.Text.Trim())
            Else
                LoadReservations(CurrentCondition)
            End If
        End If
    End Sub

    Private Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click
        Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)
        If CurrentPage < totalPages Then
            CurrentPage += 1

            If txtSearch.Text.Trim() <> "" Then
                SearchReservations(txtSearch.Text.Trim())
            Else
                LoadReservations(CurrentCondition)
            End If
        End If
    End Sub

    Private Sub btnLastPage_Click(sender As Object, e As EventArgs) Handles btnLastPage.Click
        Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)
        CurrentPage = totalPages

        If txtSearch.Text.Trim() <> "" Then
            SearchReservations(txtSearch.Text.Trim())
        Else
            LoadReservations(CurrentCondition)
        End If
    End Sub

    Private Sub cboRecordsPerPage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRecordsPerPage.SelectedIndexChanged
        If cboRecordsPerPage.SelectedItem IsNot Nothing Then
            RecordsPerPage = CInt(cboRecordsPerPage.SelectedItem)
            CurrentPage = 1 ' Reset to first page when changing page size

            If txtSearch.Text.Trim() <> "" Then
                SearchReservations(txtSearch.Text.Trim())
            Else
                LoadReservations(CurrentCondition)
            End If
        End If
    End Sub

    ' ============================================================
    ' PAGE INFO LABEL CLICK (Go to specific page)
    ' ============================================================
    Private Sub lblPageInfo_Click(sender As Object, e As EventArgs) Handles lblPageInfo.Click
        Try
            Dim totalPages As Integer = If(TotalRecords > 0, Math.Ceiling(TotalRecords / RecordsPerPage), 1)

            ' Prompt user for page number
            Dim input As String = InputBox($"Enter page number (1 to {totalPages}):", "Go to Page", CurrentPage.ToString())

            If String.IsNullOrWhiteSpace(input) Then
                Return ' User cancelled
            End If

            Dim pageNumber As Integer
            If Integer.TryParse(input, pageNumber) Then
                If pageNumber >= 1 AndAlso pageNumber <= totalPages Then
                    CurrentPage = pageNumber

                    If txtSearch.Text.Trim() <> "" Then
                        SearchReservations(txtSearch.Text.Trim())
                    Else
                        LoadReservations(CurrentCondition)
                    End If
                Else
                    MessageBox.Show($"Please enter a valid page number between 1 and {totalPages}.", "Invalid Page", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                MessageBox.Show("Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error navigating to page: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class