Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Class RoundedPane2
    Inherits Panel

    Private _cornerRadius As Integer = 15
    Private _borderColor As Color = Color.LightGray
    Private _borderThickness As Integer = 1
    Private _fillColor As Color = Color.White

    ' Constructor with double buffering
    Public Sub New()
        ' Enable double buffering to reduce flickering
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.UserPaint Or
                    ControlStyles.AllPaintingInWmPaint Or
                    ControlStyles.OptimizedDoubleBuffer, True)
        Me.UpdateStyles()
    End Sub

    <Browsable(True), Category("Appearance")>
    Public Property CornerRadius As Integer
        Get
            Return _cornerRadius
        End Get
        Set(value As Integer)
            _cornerRadius = value
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Appearance")>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Appearance")>
    Public Property BorderThickness As Integer
        Get
            Return _borderThickness
        End Get
        Set(value As Integer)
            _borderThickness = value
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True), Category("Appearance")>
    Public Property FillColor As Color
        Get
            Return _fillColor
        End Get
        Set(value As Color)
            _fillColor = value
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Dim rect As Rectangle = New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)

        Using path As GraphicsPath = GetRoundedPath(rect, _cornerRadius)
            ' Fill background
            Using brush As New SolidBrush(_fillColor)
                e.Graphics.FillPath(brush, path)
            End Using

            ' Draw border
            Using pen As New Pen(_borderColor, _borderThickness)
                e.Graphics.DrawPath(pen, path)
            End Using
        End Using
    End Sub

    Private Function GetRoundedPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim diameter As Integer = radius * 2

        path.StartFigure()
        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90)
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90)
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90)
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90)
        path.CloseFigure()

        Return path
    End Function
End Class