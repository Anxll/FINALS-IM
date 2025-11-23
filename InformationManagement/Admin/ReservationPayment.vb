Imports MySqlConnector
Imports System.Data

Public Class ReservationPayment

    Private Sub ReservationPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadReservationPayments()
    End Sub

    ' ====================================================
    ' LOAD RESERVATION PAYMENTS INTO DGV
    ' ====================================================
    Private Sub LoadReservationPayments()
        Try
            Dim query As String =
                "SELECT 
                    ReservationPaymentID,
                    ReservationID,
                    PaymentDate,
                    PaymentMethod,
                    PaymentStatus,
                    AmountPaid,
                    PaymentSource,
                    ProofOfPayment,
                    ReceiptFileName,
                    TransactionID,
                    Notes,
                    UpdatedDate
                FROM reservation_payments"

            LoadToDGV(query, Reservation)

        Catch ex As Exception
            MessageBox.Show("Error loading reservation payments: " & ex.Message)
        End Try
    End Sub

End Class