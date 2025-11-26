<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOrders
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim DataPoint1 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 310.0R)
        Dim DataPoint2 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 300.0R)
        Dim DataPoint3 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 350.0R)
        Dim DataPoint4 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 380.0R)
        Dim DataPoint5 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 470.0R)
        Dim DataPoint6 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 450.0R)
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim DataPoint7 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 45.0R)
        Dim DataPoint8 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 25.0R)
        Dim DataPoint9 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 20.0R)
        Dim DataPoint10 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0R, 10.0R)
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormOrders))
        Me.MonthlyChartOrder = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.RoundedPane21 = New InformationManagement.RoundedPane2()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OrderCategoriesGraph = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.RoundedPane22 = New InformationManagement.RoundedPane2()
        Me.btnExportReport = New System.Windows.Forms.Button()
        CType(Me.MonthlyChartOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RoundedPane21.SuspendLayout()
        CType(Me.OrderCategoriesGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RoundedPane22.SuspendLayout()
        Me.SuspendLayout()
        '
        'MonthlyChartOrder
        '
        ChartArea1.Name = "ChartArea1"
        Me.MonthlyChartOrder.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.MonthlyChartOrder.Legends.Add(Legend1)
        Me.MonthlyChartOrder.Location = New System.Drawing.Point(32, 63)
        Me.MonthlyChartOrder.Name = "MonthlyChartOrder"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        DataPoint1.AxisLabel = "Jan"
        DataPoint2.AxisLabel = "Feb"
        DataPoint3.AxisLabel = "Mar"
        DataPoint4.AxisLabel = "Apr"
        DataPoint5.AxisLabel = "May"
        DataPoint6.AxisLabel = "Jun"
        Series1.Points.Add(DataPoint1)
        Series1.Points.Add(DataPoint2)
        Series1.Points.Add(DataPoint3)
        Series1.Points.Add(DataPoint4)
        Series1.Points.Add(DataPoint5)
        Series1.Points.Add(DataPoint6)
        Me.MonthlyChartOrder.Series.Add(Series1)
        Me.MonthlyChartOrder.Size = New System.Drawing.Size(462, 300)
        Me.MonthlyChartOrder.TabIndex = 1
        Me.MonthlyChartOrder.Text = "Chart2"
        '
        'RoundedPane21
        '
        Me.RoundedPane21.BorderColor = System.Drawing.Color.LightGray
        Me.RoundedPane21.BorderThickness = 1
        Me.RoundedPane21.Controls.Add(Me.Label1)
        Me.RoundedPane21.Controls.Add(Me.OrderCategoriesGraph)
        Me.RoundedPane21.CornerRadius = 15
        Me.RoundedPane21.FillColor = System.Drawing.Color.White
        Me.RoundedPane21.Location = New System.Drawing.Point(576, 60)
        Me.RoundedPane21.Name = "RoundedPane21"
        Me.RoundedPane21.Size = New System.Drawing.Size(509, 386)
        Me.RoundedPane21.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label1.Location = New System.Drawing.Point(30, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(123, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Popular Categories"
        '
        'OrderCategoriesGraph
        '
        ChartArea2.Name = "ChartArea1"
        Me.OrderCategoriesGraph.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.OrderCategoriesGraph.Legends.Add(Legend2)
        Me.OrderCategoriesGraph.Location = New System.Drawing.Point(33, 72)
        Me.OrderCategoriesGraph.Name = "OrderCategoriesGraph"
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
        Series2.Legend = "Legend1"
        Series2.Name = "PopularCategories"
        DataPoint7.AxisLabel = "Main Courses"
        DataPoint7.Color = System.Drawing.Color.MediumPurple
        DataPoint7.LabelForeColor = System.Drawing.Color.Transparent
        DataPoint8.AxisLabel = "Appetizers"
        DataPoint8.Color = System.Drawing.Color.LightGreen
        DataPoint8.LabelForeColor = System.Drawing.Color.Transparent
        DataPoint9.AxisLabel = "Desserts"
        DataPoint9.Color = System.Drawing.Color.Goldenrod
        DataPoint9.LabelForeColor = System.Drawing.Color.Transparent
        DataPoint10.AxisLabel = "Beverages"
        DataPoint10.Color = System.Drawing.Color.DarkOrange
        DataPoint10.LabelForeColor = System.Drawing.Color.Transparent
        Series2.Points.Add(DataPoint7)
        Series2.Points.Add(DataPoint8)
        Series2.Points.Add(DataPoint9)
        Series2.Points.Add(DataPoint10)
        Me.OrderCategoriesGraph.Series.Add(Series2)
        Me.OrderCategoriesGraph.Size = New System.Drawing.Size(450, 268)
        Me.OrderCategoriesGraph.TabIndex = 0
        Me.OrderCategoriesGraph.Text = "Chart1"
        Title1.Alignment = System.Drawing.ContentAlignment.TopLeft
        Title1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title1.Name = "Popular Categories"
        Me.OrderCategoriesGraph.Titles.Add(Title1)
        '
        'RoundedPane22
        '
        Me.RoundedPane22.BorderColor = System.Drawing.Color.LightGray
        Me.RoundedPane22.BorderThickness = 1
        Me.RoundedPane22.Controls.Add(Me.MonthlyChartOrder)
        Me.RoundedPane22.CornerRadius = 15
        Me.RoundedPane22.FillColor = System.Drawing.Color.White
        Me.RoundedPane22.Location = New System.Drawing.Point(32, 60)
        Me.RoundedPane22.Name = "RoundedPane22"
        Me.RoundedPane22.Size = New System.Drawing.Size(524, 386)
        Me.RoundedPane22.TabIndex = 2
        '
        'btnExportReport
        '
        Me.btnExportReport.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportReport.Image = CType(resources.GetObject("btnExportReport.Image"), System.Drawing.Image)
        Me.btnExportReport.Location = New System.Drawing.Point(994, 12)
        Me.btnExportReport.Name = "btnExportReport"
        Me.btnExportReport.Size = New System.Drawing.Size(104, 30)
        Me.btnExportReport.TabIndex = 8
        Me.btnExportReport.Text = "   Export"
        Me.btnExportReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExportReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExportReport.UseVisualStyleBackColor = True
        '
        'FormOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.GhostWhite
        Me.ClientSize = New System.Drawing.Size(1151, 749)
        Me.Controls.Add(Me.btnExportReport)
        Me.Controls.Add(Me.RoundedPane22)
        Me.Controls.Add(Me.RoundedPane21)
        Me.DoubleBuffered = True
        Me.Name = "FormOrders"
        Me.Text = "FormOrders"
        CType(Me.MonthlyChartOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RoundedPane21.ResumeLayout(False)
        Me.RoundedPane21.PerformLayout()
        CType(Me.OrderCategoriesGraph, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RoundedPane22.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RoundedPane21 As RoundedPane2
    Friend WithEvents OrderCategoriesGraph As DataVisualization.Charting.Chart
    Friend WithEvents Label1 As Label
    Friend WithEvents MonthlyChartOrder As DataVisualization.Charting.Chart
    Friend WithEvents RoundedPane22 As RoundedPane2
    Friend WithEvents btnExportReport As Button
End Class
