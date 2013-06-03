Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Public Class OutlookPanel
    Inherits System.Windows.Forms.Panel

    Private _HeaderHeight As Integer = 25
    Public Property HeaderHeight() As Integer
        Get
            Return _HeaderHeight
        End Get
        Set(ByVal value As Integer)
            _HeaderHeight = value
            Invalidate()
        End Set
    End Property

    Private _HeaderText As String = "Outlook Panel 1.0"
    Public Property HeaderText() As String
        Get
            Return _HeaderText
        End Get
        Set(ByVal value As String)
            _HeaderText = value
            Invalidate()
        End Set
    End Property

    Private _HeaderColor1 As Color = Color.FromArgb(89, 135, 214)
    Public Property HeaderColor1() As Color
        Get
            Return _HeaderColor1
        End Get
        Set(ByVal value As Color)
            _HeaderColor1 = value
            Invalidate()
        End Set
    End Property

    Private _HeaderColor2 As Color = Color.FromArgb(3, 56, 147)
    Public Property HeaderColor2() As Color
        Get
            Return _HeaderColor2
        End Get
        Set(ByVal value As Color)
            _HeaderColor2 = value
            Invalidate()
        End Set
    End Property

    Private _HeaderFont As Font = New Font("Arial", 12.0F, System.Drawing.FontStyle.Bold)
    Public Property HeaderFont() As Font
        Get
            Return _HeaderFont
        End Get
        Set(ByVal value As Font)
            _HeaderFont = value
            Invalidate()
        End Set
    End Property

    Private _Icon As Image = Nothing
    Public Property Icon() As Image
        Get
            Return _Icon
        End Get
        Set(ByVal value As Image)
            _Icon = value
            Invalidate()
        End Set
    End Property

    Private _IconTransparentColor As Color = Color.White
    Public Property IconTransparentColor() As Color
        Get
            Return _IconTransparentColor
        End Get
        Set(ByVal value As Color)
            _IconTransparentColor = value
            Invalidate()
        End Set
    End Property
    Protected Overrides Sub OnScroll(ByVal se As System.Windows.Forms.ScrollEventArgs)
        MyBase.OnScroll(se)
        Invalidate()
    End Sub
    Public Sub New()
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint, True)
        Me.Padding = New Padding(5, _HeaderHeight + 4, 5, 4)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        'Drawing Border
        Dim p As Pen = New Pen(Color.White)
        e.Graphics.DrawRectangle(p, 0, 0, Me.Width - 1, Me.Height - 2)
        p.Dispose()
        p = Nothing

        'Drawing header
        Dim r As Rectangle = New Rectangle(1, 1, Me.Width - 2, _HeaderHeight)
        Dim b As Brush = New LinearGradientBrush(r, _HeaderColor1, _HeaderColor2, LinearGradientMode.Vertical)
        e.Graphics.FillRectangle(b, r)
        b.Dispose()
        b = Nothing

        If _HeaderText <> "" Then
            Dim s As SizeF = e.Graphics.MeasureString(_HeaderText, _HeaderFont)
            b = New SolidBrush(Color.White)
            e.Graphics.DrawString(_HeaderText, _HeaderFont, b, 5, (_HeaderHeight - s.Height) / 2)


        End If

    End Sub
End Class
