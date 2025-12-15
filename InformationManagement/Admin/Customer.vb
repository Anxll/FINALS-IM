Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class Customer

    Private ReadOnly connectionString As String = "Server=127.0.0.1;User=root;Password=;Database=tabeya_system"

    ' ==============================
    ' FORM LOAD
    ' ==============================
    Private Sub Customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomerData()
    End Sub

    ' ==============================
    ' LOAD CUSTOMER LIST
    ' ==============================
    Private Sub LoadCustomerData(Optional searchText As String = "")
        Try
            Using dbConnection As New MySqlConnection(connectionString)
                dbConnection.Open()

                Dim query As String =
                    "SELECT CustomerID, FirstName, LastName, Email, ContactNumber, CustomerType,
                            FeedbackCount, TotalOrdersCount, ReservationCount, LastTransactionDate,
                            LastLoginDate, CreatedDate, AccountStatus, SatisfactionRating
                     FROM customers
                     WHERE CONCAT(FirstName,' ',LastName,' ',Email,' ',ContactNumber) LIKE @search
                     ORDER BY CustomerID DESC"

                Using cmd As New MySqlCommand(query, dbConnection)
                    cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")

                    Using adapter As New MySqlDataAdapter(cmd)
                        Dim table As New DataTable()
                        adapter.Fill(table)

                        DataGridView1.DataSource = table
                        FormatDataGridView()
                    End Using
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show("Database error loading customers: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error loading customers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==============================
    ' FORMAT GRID (Hide ID + Format Headers)
    ' ==============================
    Private Sub FormatDataGridView()
        Try
            ' ✅ HIDE CUSTOMER ID COLUMN
            If DataGridView1.Columns.Contains("CustomerID") Then
                DataGridView1.Columns("CustomerID").Visible = False
            End If

            ' ✅ FORMAT COLUMN HEADERS WITH SPACES
            If DataGridView1.Columns.Contains("FirstName") Then
                DataGridView1.Columns("FirstName").HeaderText = "First Name"
            End If

            If DataGridView1.Columns.Contains("LastName") Then
                DataGridView1.Columns("LastName").HeaderText = "Last Name"
            End If

            If DataGridView1.Columns.Contains("ContactNumber") Then
                DataGridView1.Columns("ContactNumber").HeaderText = "Contact Number"
            End If

            If DataGridView1.Columns.Contains("CustomerType") Then
                DataGridView1.Columns("CustomerType").HeaderText = "Customer Type"
            End If

            If DataGridView1.Columns.Contains("FeedbackCount") Then
                DataGridView1.Columns("FeedbackCount").HeaderText = "Feedback Count"
            End If

            If DataGridView1.Columns.Contains("TotalOrdersCount") Then
                DataGridView1.Columns("TotalOrdersCount").HeaderText = "Total Orders Count"
            End If

            If DataGridView1.Columns.Contains("ReservationCount") Then
                DataGridView1.Columns("ReservationCount").HeaderText = "Reservation Count"
            End If

            If DataGridView1.Columns.Contains("LastTransactionDate") Then
                DataGridView1.Columns("LastTransactionDate").HeaderText = "Last Transaction Date"
            End If

            If DataGridView1.Columns.Contains("LastLoginDate") Then
                DataGridView1.Columns("LastLoginDate").HeaderText = "Last Login Date"
            End If

            If DataGridView1.Columns.Contains("CreatedDate") Then
                DataGridView1.Columns("CreatedDate").HeaderText = "Created Date"
            End If

            If DataGridView1.Columns.Contains("AccountStatus") Then
                DataGridView1.Columns("AccountStatus").HeaderText = "Account Status"
            End If

            If DataGridView1.Columns.Contains("SatisfactionRating") Then
                DataGridView1.Columns("SatisfactionRating").HeaderText = "Satisfaction Rating"
            End If

            ' ✅ FORMAT DATES AND NUMBERS
            For Each col As DataGridViewColumn In DataGridView1.Columns
                If col.Name.Contains("Date") Then
                    col.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm"
                End If
                If col.Name = "SatisfactionRating" Then
                    col.DefaultCellStyle.Format = "0.00"
                End If
            Next

            ' ✅ AUTO-SIZE COLUMNS
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False

        Catch ex As Exception
            MessageBox.Show("Error formatting grid: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ' ==============================
    ' BUTTON: REFRESH
    ' ==============================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        txtSearch.Text = ""
        LoadCustomerData()
    End Sub

    ' ==============================
    ' BUTTON: DELETE
    ' ==============================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a customer to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim customerId As Integer
        Try
            customerId = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("CustomerID").Value)
        Catch ex As Exception
            MessageBox.Show("Error reading customer ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Dim result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then Return

        Try
            Using dbConnection As New MySqlConnection(connectionString)
                dbConnection.Open()

                Using cmd As New MySqlCommand("CALL ArchiveCustomer(@id)", dbConnection)
                    cmd.Parameters.AddWithValue("@id", customerId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Customer archived and deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadCustomerData()

        Catch ex As MySqlException
            MessageBox.Show("Database error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error deleting customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==============================
    ' SEARCH LIVE FILTER
    ' ==============================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadCustomerData(txtSearch.Text)
    End Sub

End Class