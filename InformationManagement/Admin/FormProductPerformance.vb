Imports System.Globalization
Imports System.Windows.Forms.DataVisualization.Charting
Imports MySqlConnector

Public Class FormProductPerformance

    Private ReadOnly currencyCulture As CultureInfo = CultureInfo.GetCultureInfo("en-PH")
    Private summaryTiles As List(Of SummaryTile)

    Private Class SummaryTile
        Public Property NameLabel As Label
        Public Property DetailLabel As Label
    End Class

    Private Sub FormProductPerformance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeSummaryTiles()
        ConfigureChart()
        LoadProductPerformance()
    End Sub

    Private Sub InitializeSummaryTiles()
        summaryTiles = New List(Of SummaryTile) From {
            New SummaryTile With {.NameLabel = Label2, .DetailLabel = Label3},
            New SummaryTile With {.NameLabel = Label5, .DetailLabel = Label4},
            New SummaryTile With {.NameLabel = Label7, .DetailLabel = Label6},
            New SummaryTile With {.NameLabel = Label9, .DetailLabel = Label8},
            New SummaryTile With {.NameLabel = Label13, .DetailLabel = Label12},
            New SummaryTile With {.NameLabel = Label11, .DetailLabel = Label10}
        }
    End Sub

    Private Sub ConfigureChart()
        Chart1.Series.Clear()
        Chart1.Titles.Clear()
        Chart1.Legends.Clear()

        If Chart1.ChartAreas.Count = 0 Then
            Chart1.ChartAreas.Add(New ChartArea("ChartArea1"))
        End If

        Dim chartArea = Chart1.ChartAreas(0)
        With chartArea
            .AxisX.Interval = 1
            .AxisX.MajorGrid.Enabled = False
            .AxisX.LabelStyle.Font = New Font("Segoe UI", 9.0F)
            .AxisY.LabelStyle.Format = "₱#,##0"
            .AxisY.LabelStyle.Font = New Font("Segoe UI", 9.0F)
            .AxisY.MajorGrid.LineColor = Color.LightGray
            .AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot
            .BackColor = Color.White
        End With

        Dim series = Chart1.Series.Add("Revenue")
        With series
            .ChartType = SeriesChartType.Column
            .Color = Color.MediumSlateBlue
            .BorderWidth = 0
            .IsValueShownAsLabel = True
            .LabelFormat = "₱#,##0"
            .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        End With

        Chart1.Titles.Add(New Title With {
            .Text = "Revenue by Product (Completed Orders)",
            .Alignment = ContentAlignment.TopLeft,
            .Font = New Font("Segoe UI Semibold", 11.25F, FontStyle.Bold)
        })
    End Sub

    Private Sub LoadProductPerformance()
        Try
            Dim performanceData = FetchProductPerformanceData()
            UpdateChart(performanceData)
            UpdateSummaryTiles(performanceData)
        Catch ex As Exception
            MessageBox.Show($"Unable to load product performance.{Environment.NewLine}{ex.Message}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function FetchProductPerformanceData() As DataTable
        Dim query As String =
"SELECT ProductName,
        SUM(Quantity) AS TotalOrders,
        SUM(TotalPrice) AS Revenue
 FROM (
        -- Reservation items with Confirmed or Served status
        SELECT ri.ProductName,
               ri.Quantity,
               ri.TotalPrice
        FROM reservation_items ri
        INNER JOIN reservations r ON ri.ReservationID = r.ReservationID
        WHERE r.ReservationStatus IN ('Confirmed', 'Served')
        
        UNION ALL
        
        -- Order items with Served or Completed status
        SELECT oi.ProductName,
               oi.Quantity,
               (oi.Quantity * oi.UnitPrice) AS TotalPrice
        FROM order_items oi
        INNER JOIN orders o ON oi.OrderID = o.OrderID
        WHERE o.OrderStatus IN ('Served', 'Completed')
      ) AS combined
 GROUP BY ProductName
 ORDER BY Revenue DESC;"

        Dim dt As New DataTable()

        Using connection As New MySqlConnection(strConnection)
            connection.Open()
            Using command As New MySqlCommand(query, connection)
                Using reader = command.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Private Sub UpdateChart(data As DataTable)
        Dim series = Chart1.Series("Revenue")
        series.Points.Clear()

        If data.Rows.Count = 0 Then
            series.Points.AddXY("No data", 0)
            Return
        End If

        For Each row As DataRow In data.Rows
            Dim productName = row("ProductName").ToString()
            Dim revenue = If(IsDBNull(row("Revenue")), 0D, Convert.ToDecimal(row("Revenue")))
            series.Points.AddXY(productName, revenue)
        Next
    End Sub

    Private Sub UpdateSummaryTiles(data As DataTable)
        If summaryTiles Is Nothing OrElse summaryTiles.Count = 0 Then Return

        For i As Integer = 0 To summaryTiles.Count - 1
            Dim tile = summaryTiles(i)

            If i < data.Rows.Count Then
                Dim row = data.Rows(i)
                Dim productName = row("ProductName").ToString()
                Dim totalOrders = If(IsDBNull(row("TotalOrders")), 0, Convert.ToInt32(row("TotalOrders")))
                Dim revenue = If(IsDBNull(row("Revenue")), 0D, Convert.ToDecimal(row("Revenue")))
                Dim revenueText = String.Format(currencyCulture, "{0:C0}", revenue)

                tile.NameLabel.Text = productName
                tile.DetailLabel.Text = $"{totalOrders} total orders | {revenueText}"
            Else
                tile.NameLabel.Text = "N/A"
                tile.DetailLabel.Text = "No data available"
            End If
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Using dialog As New SaveFileDialog()
                dialog.Title = "Export Product Performance Chart"
                dialog.Filter = "PNG Image|*.png"
                dialog.FileName = $"ProductPerformance_{Date.Now:yyyyMMddHHmmss}.png"

                If dialog.ShowDialog() = DialogResult.OK Then
                    Dim bmp As New Bitmap(Chart1.Width, Chart1.Height)
                    Chart1.DrawToBitmap(bmp, New Rectangle(0, 0, Chart1.Width, Chart1.Height))
                    bmp.Save(dialog.FileName, Imaging.ImageFormat.Png)
                    MessageBox.Show("Chart exported successfully.", "Export Complete",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Export failed.{Environment.NewLine}{ex.Message}",
                            "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class