Imports MySql.Data.MySqlClient

Public Class Customer
    Private dbConnection As MySqlConnection
    Private connectionString As String = "Server=127.0.0.1;User=root;Password=;Database=tabeya_system"

    Private Sub Customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDatabase()
        LoadCustomerData()
    End Sub

    Private Sub InitializeDatabase()
        Try
            dbConnection = New MySqlConnection(connectionString)
        Catch ex As Exception
            MessageBox.Show("Database connection error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCustomerData()
        Try
            If dbConnection.State = ConnectionState.Open Then
                dbConnection.Close()
            End If

            dbConnection.Open()

            Dim query As String = "SELECT CustomerID, FirstName, LastName, Email, ContactNumber, CustomerType, " &
                                 "FeedbackCount, TotalOrdersCount, ReservationCount, LastTransactionDate, " &
                                 "LastLoginDate, CreatedDate, AccountStatus, SatisfactionRating FROM customers ORDER BY CustomerID DESC"

            Dim adapter As New MySqlDataAdapter(query, dbConnection)
            Dim table As New DataTable()
            adapter.Fill(table)

            DataGridView1.DataSource = table

            ' Set column widths and formatting
            FormatDataGridView()

        Catch ex As Exception
            MessageBox.Show("Error loading customer data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dbConnection.State = ConnectionState.Open Then
                dbConnection.Close()
            End If
        End Try
    End Sub

    Private Sub FormatDataGridView()
        ' Format date columns
        For Each column As DataGridViewColumn In DataGridView1.Columns
            If column.Name.Contains("Date") Then
                column.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm"
            ElseIf column.Name = "SatisfactionRating" Then
                column.DefaultCellStyle.Format = "0.00"
            End If
        Next

        ' Unfreeze the CustomerID column before setting AutoSize
        DataGridView1.Columns("CustomerID").Frozen = False

        ' Auto-size columns to fill available space
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Freeze the CustomerID column after setting AutoSize
        DataGridView1.Columns("CustomerID").Frozen = True
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then Return

        Try
            Dim columnName As String = DataGridView1.Columns(e.ColumnIndex).Name
            Dim customerId As Integer = CInt(DataGridView1.Rows(e.RowIndex).Cells("CustomerID").Value)

            Select Case columnName
                Case "DeleteBtn"
                    DeleteCustomer(customerId)
                Case "SuspendBtn"
                    SuspendCustomer(customerId)
            End Select
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteCustomer(customerId As Integer)
        If MessageBox.Show("Are you sure you want to delete this customer? This action cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                If dbConnection.State = ConnectionState.Open Then
                    dbConnection.Close()
                End If

                dbConnection.Open()

                ' Use the ArchiveCustomer procedure
                Dim cmd As New MySqlCommand("CALL ArchiveCustomer(@customerId)", dbConnection)
                cmd.Parameters.AddWithValue("@customerId", customerId)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Customer archived and deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadCustomerData()

            Catch ex As Exception
                MessageBox.Show("Error deleting customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If dbConnection.State = ConnectionState.Open Then
                    dbConnection.Close()
                End If
            End Try
        End If
    End Sub

    Private Sub SuspendCustomer(customerId As Integer)
        Try
            If dbConnection.State = ConnectionState.Open Then
                dbConnection.Close()
            End If

            dbConnection.Open()

            ' Check current status
            Dim checkQuery As String = "SELECT AccountStatus FROM customers WHERE CustomerID = @id"
            Dim checkCmd As New MySqlCommand(checkQuery, dbConnection)
            checkCmd.Parameters.AddWithValue("@id", customerId)
            Dim currentStatus As String = checkCmd.ExecuteScalar().ToString()

            If currentStatus = "Suspended" Then
                MessageBox.Show("This account is already suspended.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                dbConnection.Close()
                Return
            End If

            ' Suspend the account
            Dim cmd As New MySqlCommand("CALL SuspendCustomerAccount(@customerId, @reason)", dbConnection)
            cmd.Parameters.AddWithValue("@customerId", customerId)
            cmd.Parameters.AddWithValue("@reason", "Suspended by administrator")

            cmd.ExecuteNonQuery()
            MessageBox.Show("Customer account suspended successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadCustomerData()

        Catch ex As Exception
            MessageBox.Show("Error suspending customer: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dbConnection.State = ConnectionState.Open Then
                dbConnection.Close()
            End If
        End Try
    End Sub

End Class