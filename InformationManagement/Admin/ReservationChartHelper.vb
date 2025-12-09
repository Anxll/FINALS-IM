Imports System.Windows.Forms.DataVisualization.Charting
Imports MySqlConnector

' ============================================
' SHARED UTILITY CLASS: Reservation Chart Helper
' Place this in a new file: ReservationChartHelper.vb
' Both Dashboard and FormReservationStatus can use this
' ============================================

Public Class ReservationChartHelper

    ' Color constants - define once, use everywhere
    Public Shared ReadOnly PendingColor As Color = Color.FromArgb(255, 165, 0)      ' Orange
    Public Shared ReadOnly ConfirmedColor As Color = Color.FromArgb(34, 197, 94)    ' Green
    Public Shared ReadOnly CancelledColor As Color = Color.FromArgb(239, 68, 68)    ' Red
    Public Shared ReadOnly CompletedColor As Color = Color.FromArgb(59, 130, 246)   ' Blue

    ' ============================================
    ' GET RESERVATION DATA
    ' ============================================
    Public Shared Function GetReservationData(conn As MySqlConnection, period As String) As Dictionary(Of String, Integer)
        Dim data As New Dictionary(Of String, Integer) From {
            {"Pending", 0},
            {"Confirmed", 0},
            {"Cancelled", 0},
            {"Completed", 0}
        }

        Try
            Dim dateFilter As String = GetDateFilter(period)

            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If

            Dim sql As String = $"
                SELECT 
                    ReservationStatus,
                    COUNT(*) AS StatusCount
                FROM reservations
                WHERE {dateFilter}
                GROUP BY ReservationStatus"

            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim status As String = reader("ReservationStatus").ToString()
                        Dim count As Integer = Convert.ToInt32(reader("StatusCount"))
                        If data.ContainsKey(status) Then
                            data(status) = count
                        End If
                    End While
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception($"Error fetching reservation data: {ex.Message}", ex)
        End Try

        Return data
    End Function

    ' ============================================
    ' RENDER CHART
    ' ============================================
    Public Shared Sub RenderChart(chart As Chart, data As Dictionary(Of String, Integer))
        Try
            ' Clear existing data
            chart.Series("ReservationStatus").Points.Clear()
            chart.Annotations.Clear()

            Dim totalCount As Integer = data.Values.Sum()

            If totalCount > 0 Then
                ' Add Pending
                If data("Pending") > 0 Then
                    AddChartPoint(chart, "Pending", data("Pending"), PendingColor)
                End If

                ' Add Confirmed
                If data("Confirmed") > 0 Then
                    AddChartPoint(chart, "Confirmed", data("Confirmed"), ConfirmedColor)
                End If

                ' Add Cancelled
                If data("Cancelled") > 0 Then
                    AddChartPoint(chart, "Cancelled", data("Cancelled"), CancelledColor)
                End If

                ' Add Completed
                If data("Completed") > 0 Then
                    AddChartPoint(chart, "Completed", data("Completed"), CompletedColor)
                End If

                ' Configure legend
                chart.Legends(0).Enabled = True
                chart.Legends(0).Docking = Docking.Right
                chart.Legends(0).Font = New Font("Segoe UI", 9)
                chart.Legends(0).BackColor = Color.Transparent
            Else
                ' Show "No Data" message
                ShowNoDataMessage(chart)
            End If

            ' Apply 3D styling
            Apply3DStyle(chart)

        Catch ex As Exception
            Throw New Exception($"Error rendering chart: {ex.Message}", ex)
        End Try
    End Sub

    ' ============================================
    ' ADD CHART POINT (Helper)
    ' ============================================
    Private Shared Sub AddChartPoint(chart As Chart, label As String, value As Integer, color As Color)
        Dim point As New DataPoint()
        point.SetValueXY(label, value)
        point.Color = color
        point.BorderColor = Color.White
        point.BorderWidth = 2
        point.Label = value.ToString()
        point.LegendText = $"{label} ({value})"
        point.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        point.LabelForeColor = Color.White
        chart.Series("ReservationStatus").Points.Add(point)
    End Sub

    ' ============================================
    ' SHOW NO DATA MESSAGE
    ' ============================================
    Private Shared Sub ShowNoDataMessage(chart As Chart)
        Dim annotation As New TextAnnotation()
        annotation.Text = "No Reservation Data"
        annotation.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        annotation.ForeColor = Color.Gray
        annotation.X = 50
        annotation.Y = 50
        annotation.Alignment = ContentAlignment.MiddleCenter
        chart.Annotations.Add(annotation)
        chart.Legends(0).Enabled = False
    End Sub

    ' ============================================
    ' APPLY 3D STYLE
    ' ============================================
    Private Shared Sub Apply3DStyle(chart As Chart)
        chart.ChartAreas(0).Area3DStyle.Enable3D = True
        chart.ChartAreas(0).Area3DStyle.Inclination = 15
        chart.ChartAreas(0).Area3DStyle.Rotation = 10
        chart.Series("ReservationStatus")("PieLabelStyle") = "Inside"
        chart.Series("ReservationStatus").IsValueShownAsLabel = True
    End Sub

    ' ============================================
    ' GET DATE FILTER
    ' ============================================
    Private Shared Function GetDateFilter(period As String) As String
        Select Case period.ToUpper()
            Case "DAILY"
                Return "DATE(ReservationDate) = CURDATE()"
            Case "WEEKLY"
                Return "YEARWEEK(ReservationDate, 1) = YEARWEEK(CURDATE(), 1)"
            Case "MONTHLY"
                Return "MONTH(ReservationDate) = MONTH(CURDATE()) AND YEAR(ReservationDate) = YEAR(CURDATE())"
            Case "YEARLY"
                Return "YEAR(ReservationDate) = YEAR(CURDATE())"
            Case Else
                Return "MONTH(ReservationDate) = MONTH(CURDATE()) AND YEAR(ReservationDate) = YEAR(CURDATE())"
        End Select
    End Function

    ' ============================================
    ' CALCULATE STATISTICS
    ' ============================================
    Public Shared Function CalculateStatistics(data As Dictionary(Of String, Integer)) As Dictionary(Of String, Object)
        Dim stats As New Dictionary(Of String, Object)
        Dim total As Integer = data.Values.Sum()

        stats("Total") = total
        stats("Pending") = data("Pending")
        stats("Confirmed") = data("Confirmed")
        stats("Cancelled") = data("Cancelled")
        stats("Completed") = data("Completed")

        ' Calculate percentages
        If total > 0 Then
            stats("PendingPercent") = (data("Pending") / total) * 100
            stats("ConfirmedPercent") = (data("Confirmed") / total) * 100
            stats("CancelledPercent") = (data("Cancelled") / total) * 100
            stats("CompletedPercent") = (data("Completed") / total) * 100
            stats("ConversionRate") = (data("Confirmed") / total) * 100
            stats("CancellationRate") = (data("Cancelled") / total) * 100
        Else
            stats("PendingPercent") = 0
            stats("ConfirmedPercent") = 0
            stats("CancelledPercent") = 0
            stats("CompletedPercent") = 0
            stats("ConversionRate") = 0
            stats("CancellationRate") = 0
        End If

        Return stats
    End Function

End Class