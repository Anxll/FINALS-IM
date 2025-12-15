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
        MyBase.New()

        ' Enable double buffering to reduce flickering
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.UserPaint Or
                    ControlStyles.AllPaintingInWmPaint Or
                    ControlStyles.OptimizedDoubleBuffer Or
                    ControlStyles.ResizeRedraw Or
                    ControlStyles.SupportsTransparentBackColor, True)
        Me.UpdateStyles()

        ' Set default background to transparent for better rendering
        Me.BackColor = Color.Transparent
    End Sub

    <Browsable(True)>
    <Category("Appearance")>
    <Description("The radius of the rounded corners")>
    <DefaultValue(15)>
    Public Property CornerRadius As Integer
        Get
            Return _cornerRadius
        End Get
        Set(value As Integer)
            If value >= 0 Then
                _cornerRadius = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <Category("Appearance")>
    <Description("The color of the border")>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set
    End Property

    <Browsable(True)>
    <Category("Appearance")>
    <Description("The thickness of the border")>
    <DefaultValue(1)>
    Public Property BorderThickness As Integer
        Get
            Return _borderThickness
        End Get
        Set(value As Integer)
            If value >= 0 Then
                _borderThickness = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <Category("Appearance")>
    <Description("The fill color of the panel")>
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

        If Me.Width <= 0 OrElse Me.Height <= 0 Then
            Return
        End If

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality

        ' Calculate rectangle with proper bounds for Any CPU compatibility
        Dim halfBorder As Integer = CInt(Math.Ceiling(_borderThickness / 2.0))
        Dim rect As New Rectangle(
            halfBorder,
            halfBorder,
            Me.Width - _borderThickness - 1,
            Me.Height - _borderThickness - 1
        )

        ' Ensure rectangle has valid dimensions
        If rect.Width <= 0 OrElse rect.Height <= 0 Then
            Return
        End If

        ' Use proper disposal pattern for Any CPU compatibility
        Using path As GraphicsPath = GetRoundedPath(rect, _cornerRadius)
            ' Fill background
            Using brush As New SolidBrush(_fillColor)
                e.Graphics.FillPath(brush, path)
            End Using

            ' Draw border
            If _borderThickness > 0 Then
                Using pen As New Pen(_borderColor, CSng(_borderThickness))
                    pen.Alignment = Drawing2D.PenAlignment.Inset
                    e.Graphics.DrawPath(pen, path)
                End Using
            End If
        End Using
    End Sub

    Private Function GetRoundedPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()

        ' Ensure radius doesn't exceed half the smallest dimension
        Dim actualRadius As Integer = Math.Min(radius, Math.Min(rect.Width, rect.Height) \ 2)
        Dim diameter As Integer = actualRadius * 2

        ' Prevent negative or zero dimensions
        If diameter <= 0 OrElse rect.Width <= 0 OrElse rect.Height <= 0 Then
            path.AddRectangle(rect)
            Return path
        End If

        Try
            path.StartFigure()

            ' Top-left corner
            If actualRadius > 0 Then
                path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90)
            Else
                path.AddLine(rect.X, rect.Y, rect.X, rect.Y)
            End If

            ' Top-right corner
            If actualRadius > 0 Then
                path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90)
            Else
                path.AddLine(rect.Right, rect.Y, rect.Right, rect.Y)
            End If

            ' Bottom-right corner
            If actualRadius > 0 Then
                path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90)
            Else
                path.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Bottom)
            End If

            ' Bottom-left corner
            If actualRadius > 0 Then
                path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90)
            Else
                path.AddLine(rect.X, rect.Bottom, rect.X, rect.Bottom)
            End If

            path.CloseFigure()

        Catch ex As Exception
            ' Fallback to rectangle if path creation fails
            path.Reset()
            path.AddRectangle(rect)
        End Try

        Return path
    End Function

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnBackColorChanged(e As EventArgs)
        MyBase.OnBackColorChanged(e)
        Me.Invalidate()
    End Sub
End Class