Imports System.Drawing.Drawing2D
Imports MySqlConnector
Imports System.Threading.Tasks
Imports System.Data

Public Class Customer
    Private connectionString As String = modDB.strConnection

    Private Async Sub Customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup search bar focus effects
        txtSearch.Text = "Search customers..."
        txtSearch.ForeColor = Color.FromArgb(148, 163, 184)
        
        Await RefreshCustomersAsync()
    End Sub

    Private Async Function RefreshCustomersAsync() As Task
        Try
            ' Load data in background
            Dim searchText As String = If(txtSearch.Text = "Search customers...", "", txtSearch.Text)
            
            Dim dt As DataTable = Await Task.Run(Function() LoadCustomerDataFromDB(searchText))
            
            ' Update UI on UI thread
            Me.Invoke(Sub()
                DataGridView1.DataSource = dt
                FormatDataGridView()
                UpdateSummaryTiles(dt)
            End Sub)
            
        Catch ex As Exception
            MessageBox.Show("Error refreshing customer data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Function LoadCustomerDataFromDB(searchText As String) As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                Dim query As String = "
                    SELECT CustomerID, FirstName, LastName, Email, ContactNumber, CustomerType,
                           FeedbackCount, TotalOrdersCount, ReservationCount, LastTransactionDate,
                           LastLoginDate, CreatedDate, AccountStatus, SatisfactionRating
                    FROM customers
                    WHERE CONCAT(FirstName, ' ', LastName, ' ', Email, ' ', ContactNumber) LIKE @search
                    ORDER BY CustomerID DESC"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Propagation to async function
            Throw ex
        End Try
        Return dt
    End Function

    Private Sub UpdateSummaryTiles(dt As DataTable)
        Try
            ' Total Customers
            Label4.Text = dt.Rows.Count.ToString("N0")

            ' Active Customers (Last 30 days - simple approximation based on LastLoginDate if available)
            ' Or just filtered count for now if no specific logic
            ' For demonstration, let's use account status if available
            Dim activeCount As Integer = dt.Select("AccountStatus = 'Active'").Length
            Label6.Text = activeCount.ToString("N0")

            ' New Customers (Joined this month)
            Dim newCount As Integer = 0
            Dim firstOfMonth As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("CreatedDate")) Then
                    Dim joinedDate As DateTime = Convert.ToDateTime(row("CreatedDate"))
                    If joinedDate >= firstOfMonth Then
                        newCount += 1
                    End If
                End If
            Next
            Label7.Text = newCount.ToString("N0")

        Catch ex As Exception
            ' Silent error for summaries
        End Try
    End Sub

    Private Sub FormatDataGridView()
        If DataGridView1.Columns.Contains("CustomerID") Then
            DataGridView1.Columns("CustomerID").Visible = False
        End If

        ' Modern headers
        Dim headers As New Dictionary(Of String, String) From {
            {"FirstName", "First Name"},
            {"LastName", "Last Name"},
            {"ContactNumber", "Contact"},
            {"CustomerType", "Type"},
            {"TotalOrdersCount", "Orders"},
            {"ReservationCount", "Reservations"},
            {"LastTransactionDate", "Last Order"},
            {"LastLoginDate", "Last Login"},
            {"CreatedDate", "Joined"},
            {"AccountStatus", "Status"},
            {"SatisfactionRating", "Rating"}
        }

        For Each col As DataGridViewColumn In DataGridView1.Columns
            If headers.ContainsKey(col.Name) Then
                col.HeaderText = headers(col.Name)
            End If
            
            ' Specific styling
            If col.Name.Contains("Date") Then
                col.DefaultCellStyle.Format = "MMM dd, yyyy"
            End If
            
            If col.Name = "SatisfactionRating" Then
                col.DefaultCellStyle.Format = "0.0"
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
        Next

        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    ' Search Box Logic
    Private Sub txtSearch_Enter(sender As Object, e As EventArgs) Handles txtSearch.Enter
        If txtSearch.Text = "Search customers..." Then
            txtSearch.Text = ""
            txtSearch.ForeColor = Color.FromArgb(15, 23, 42)
        End If
        SearchContainer.BorderColor = Color.FromArgb(99, 102, 241) ' Indigo focus
    End Sub

    Private Sub txtSearch_Leave(sender As Object, e As EventArgs) Handles txtSearch.Leave
        If String.IsNullOrWhiteSpace(txtSearch.Text) Then
            txtSearch.Text = "Search customers..."
            txtSearch.ForeColor = Color.FromArgb(148, 163, 184)
        End If
        SearchContainer.BorderColor = Color.FromArgb(226, 232, 240)
    End Sub

    Private Async Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ' Using a small delay or debouncing would be better, but for now direct async call
        Await RefreshCustomersAsync()
    End Sub

    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a customer profile to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim customerId As Integer = DataGridView1.SelectedRows(0).Cells("CustomerID").Value
        Dim name As String = DataGridView1.SelectedRows(0).Cells("FirstName").Value.ToString()

        If MessageBox.Show($"Are you sure you want to delete the profile for {name}? This action might archive their data.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Await Task.Run(Sub()
                    Using conn As New MySqlConnection(connectionString)
                        conn.Open()
                        Dim cmd As New MySqlCommand("CALL ArchiveCustomer(@id)", conn)
                        cmd.Parameters.AddWithValue("@id", customerId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Sub)

                MessageBox.Show("Customer profile has been archived and removed from active list.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Log Activity
                ActivityLogger.LogUserActivity(
                    action:="Delete",
                    actionCategory:="User Management",
                    description:=$"Deleted/Archived Customer Profile: {name} (ID: {customerId})",
                    sourceSystem:="Admin Panel",
                    referenceID:=customerId.ToString(),
                    referenceTable:="customers",
                    oldValue:="Active",
                    newValue:="Archived"
                )

                Await RefreshCustomersAsync()

            Catch ex As Exception
                MessageBox.Show("Error deleting customer: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
End Class