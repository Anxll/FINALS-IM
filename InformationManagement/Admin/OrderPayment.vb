Imports MySqlConnector
Imports System.Data

Public Class OrderPayment

    Private Sub OrderPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPayments()
    End Sub

    ' ===============================
    ' LOAD PAYMENTS INTO DGV
    ' ===============================
    Private Sub LoadPayments()
        Try
            Dim query As String =
                "SELECT PaymentID, OrderID, PaymentDate, PaymentMethod, PaymentStatus,
                        AmountPaid, PaymentSource, TransactionID, Notes
                 FROM payments"

            ' Load result into your DataGridView (same system used in Orders)
            LoadToDGV(query, Order)

        Catch ex As Exception
            MessageBox.Show("Error loading payments: " & ex.Message)
        End Try
    End Sub

    ' ===============================
    ' CELL CLICK (OPTIONAL)
    ' ===============================
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Order.CellContentClick

    End Sub

End Class