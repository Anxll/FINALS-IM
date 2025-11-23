Imports MySqlConnector
Imports System.Data

Public Class Reservations

    Private Sub Reservations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadReservations()
    End Sub

    ' ======================================
    ' LOAD RESERVATIONS INTO DATAGRIDVIEW
    ' ======================================
    Private Sub LoadReservations()
        Try
            Dim query As String =
                "SELECT 
                    ReservationID,
                    CustomerID,
                    AssignedStaffID,
                    ReservationType,
                    EventType,
                    EventDate,
                    EventTime,
                    NumberOfGuests,
                    ProductSelection,
                    SpecialRequests,
                    ReservationStatus,
                    ReservationDate,
                    Address,
                    DeliveryAddress,
                    ServiceType,
                    DeliveryOption,
                    ContactNumber,
                    UpdatedDate
                FROM reservations"

            LoadToDGV(query, Reservation)

        Catch ex As Exception
            MessageBox.Show("Error loading reservations: " & ex.Message)
        End Try
    End Sub


    ' ======================================
    ' BUTTON – OPEN ADD RESERVATION FORM
    ' ======================================
    Private Sub btnAddNewReservation_Click(sender As Object, e As EventArgs)
        With PanelCreateReservation
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            .BringToFront()
        End With
    End Sub

End Class