Imports MySqlConnector
Imports System.Data

Public Class Orders

    Private Sub Orders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadOrders()
    End Sub

    ' ===============================
    ' LOAD ORDERS INTO DGV
    ' ===============================
    Private Sub LoadOrders()
        Try
            Dim query As String =
                "SELECT OrderID, CustomerID, EmployeeID, OrderType, OrderSource,
                        ReceiptNumber, NumberOfDiners, OrderDate, OrderTime,
                        ItemsOrderedCount, TotalAmount, OrderStatus, Remarks,
                        OrderPriority, PreparationTimeEstimate, SpecialRequestFlag,
                        CreatedDate, UpdatedDate
                 FROM orders"

            LoadToDGV(query, DataGridView2)

        Catch ex As Exception
            MessageBox.Show("Error loading orders: " & ex.Message)
        End Try
    End Sub

    ' ===============================
    ' CELL CLICK (OPTIONAL)
    ' ===============================
    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub OrderPayment_Click(sender As Object, e As EventArgs)
        With OrderPayment
            .Show()
            .BringToFront()
        End With
    End Sub

End Class