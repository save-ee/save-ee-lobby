Imports System.Runtime
Imports System.Runtime.InteropServices
Imports System.Runtime.CompilerServices
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class AdvancedButton
    Inherits System.Windows.Forms.Control
    Public Enum ButtonStyles
        Round
        Rectangle
        RoundedRectangle
    End Enum
    Public Enum HorizontalAlignments
        Center
        Left

        Right

    End Enum
    Public Enum VerticleAlignments
        Middle
        Top

        Bottom
    End Enum

    Private _ButtonStyle As ButtonStyles = ButtonStyles.Rectangle
    Private _GradientColor1 As Color = Color.DodgerBlue
    Private _GradientColor2 As Color = Color.LightBlue
    Private _GradientColor3 As Color = Color.LightBlue
    Private _GradientColor4 As Color = Color.DodgerBlue
    Private _BorderColor As Color = Color.Black
    Private _BorderPen As New Pen(Color.Black)
    Private _ForeColorPen As New Pen(Color.Black)
    Private _TextAlignment As New StringFormat '
    Private _RoundRadius As Integer = 10
    Private _Font As New Font("Arial", 13)


    Private _MouseOver As Boolean = False

    Private _icon As Bitmap
    'Public Shadows Property Font()
    '    Get
    '        Return _Font
    '    End Get
    '    Set(ByVal value)
    '        _Font = value
    '        Invalidate()
    '    End Set
    'End Property
    Public Property Icon() As Bitmap
        Get
            Return _icon
        End Get
        Set(ByVal value As Bitmap)
            _icon = value
        End Set
    End Property

    Protected Overrides Sub OnCreateControl()
        'Me.Font = New Font("Arial", 13, FontStyle.Regular, GraphicsUnit.Pixel)
        Me.TextHorizontalAlignment = HorizontalAlignments.Center
        Me.TextVerticleAlignment = VerticleAlignments.Middle
        MyBase.OnCreateControl()
    End Sub
    <CategoryAttribute("Settings"), DefaultValueAttribute(""), _
DescriptionAttribute("Please Select Text")> _
Public Shadows Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            Invalidate()
        End Set
    End Property

    <CategoryAttribute("Settings"), DefaultValueAttribute(""), _
DescriptionAttribute("Please Select Text Verticle Alignment")> _
Public Property TextVerticleAlignment() As VerticleAlignments
        Get
            If _TextAlignment.LineAlignment = StringAlignment.Near Then
                Return VerticleAlignments.Top
            ElseIf _TextAlignment.LineAlignment = StringAlignment.Center Then
                Return VerticleAlignments.Middle
            ElseIf _TextAlignment.LineAlignment = StringAlignment.Far Then
                Return VerticleAlignments.Bottom
            End If
        End Get
        Set(ByVal value As VerticleAlignments)
            If value = VerticleAlignments.Top Then
                _TextAlignment.LineAlignment = StringAlignment.Near
            ElseIf value = VerticleAlignments.Middle Then
                _TextAlignment.LineAlignment = StringAlignment.Center
            ElseIf value = VerticleAlignments.Bottom Then
                _TextAlignment.LineAlignment = StringAlignment.Far
            End If
            Invalidate()
        End Set
    End Property

    <CategoryAttribute("Settings"), DefaultValueAttribute(""), _
DescriptionAttribute("Please Select Text Horizontal Alignment")> _
Public Property TextHorizontalAlignment() As HorizontalAlignments
        Get
            If _TextAlignment.Alignment = StringAlignment.Near Then
                Return HorizontalAlignments.Left
            ElseIf _TextAlignment.Alignment = StringAlignment.Center Then
                Return HorizontalAlignments.Center
            ElseIf _TextAlignment.Alignment = StringAlignment.Far Then
                Return HorizontalAlignments.Right
            End If
        End Get
        Set(ByVal value As HorizontalAlignments)
            If value = HorizontalAlignments.Left Then
                _TextAlignment.Alignment = StringAlignment.Near
            ElseIf value = HorizontalAlignments.Center Then
                _TextAlignment.Alignment = StringAlignment.Center
            ElseIf value = HorizontalAlignments.Right Then
                _TextAlignment.Alignment = StringAlignment.Far
            End If
            Invalidate()
        End Set
    End Property

    <CategoryAttribute("Settings"), DefaultValueAttribute(""), _
DescriptionAttribute("Please Select Button Style")> _
    Public Property ButtonStyle() As ButtonStyles
        Get
            Return _ButtonStyle
        End Get
        Set(ByVal value As ButtonStyles)
            _ButtonStyle = value
            Invalidate()
        End Set
    End Property

    <CategoryAttribute("Settings"), DefaultValueAttribute(""), _
DescriptionAttribute("Please Select Radius Size")> _
    Public Property RoundedRadius() As Integer
        Get
            Return _RoundRadius
        End Get
        Set(ByVal value As Integer)
            _RoundRadius = value
            Invalidate()
        End Set
    End Property

    <CategoryAttribute("Settings"), DefaultValueAttribute(""), _
DescriptionAttribute("Please Select Border Color")> _
Public Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal value As Color)
            _BorderColor = value
            If Not _BorderPen Is Nothing Then
                _BorderPen.Dispose()
            End If
            _BorderPen = New Pen(_BorderColor)
            Invalidate()
        End Set
    End Property
    <CategoryAttribute("Settings"), DefaultValueAttribute(""), _
DescriptionAttribute("Please Select Gradient Color 1")> _
Public Property GradientColor1() As System.Drawing.Color
        Get
            Return _GradientColor1
        End Get
        Set(ByVal value As System.Drawing.Color)
            _GradientColor1 = value
            Invalidate()
        End Set
    End Property
    <CategoryAttribute("Settings"), DefaultValueAttribute(""), _
DescriptionAttribute("Please Select Gradient Color 2")> _
Public Property GradientColor2() As System.Drawing.Color
        Get
            Return _GradientColor2
        End Get
        Set(ByVal value As System.Drawing.Color)
            _GradientColor2 = value
            Invalidate()
        End Set
    End Property
    <CategoryAttribute("Settings"), DefaultValueAttribute(""), _
DescriptionAttribute("Please Select Gradient Color 3")> _
Public Property GradientColor3() As System.Drawing.Color
        Get
            Return _GradientColor3
        End Get
        Set(ByVal value As System.Drawing.Color)
            _GradientColor3 = value
            Invalidate()
        End Set
    End Property
    <CategoryAttribute("Settings"), DefaultValueAttribute(""), _
DescriptionAttribute("Please Select Gradient Color 4")> _
Public Property GradientColor4() As System.Drawing.Color
        Get
            Return _GradientColor4
        End Get
        Set(ByVal value As System.Drawing.Color)
            _GradientColor4 = value
            Invalidate()
        End Set
    End Property
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Try
            'Dim buttonsize As New AdvanceIT.Drawing.Rectangles.Rectangle(0, 0, Me.Width, Me.Height)
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim dr As New System.Drawing.Rectangle(0, 0, Me.Width, Me.Height)
            Dim _GradientBrush As System.Drawing.Drawing2D.LinearGradientBrush
            If _MouseOver = True Then
                _GradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(dr, _GradientColor3, _GradientColor4, System.Drawing.Drawing2D.LinearGradientMode.Vertical)
            Else
                _GradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(dr, _GradientColor1, _GradientColor2, System.Drawing.Drawing2D.LinearGradientMode.Vertical)
            End If

            If ButtonStyle = ButtonStyles.Rectangle Then
                e.Graphics.FillRectangle(_GradientBrush, dr)
                dr.Inflate(-1, -1)
                e.Graphics.DrawRectangle(_BorderPen, dr)
            ElseIf ButtonStyle = ButtonStyles.Round Then
                e.Graphics.FillEllipse(_GradientBrush, dr)
                dr.Inflate(-1, -1)
                e.Graphics.DrawEllipse(_BorderPen, dr)
            ElseIf ButtonStyle = ButtonStyles.RoundedRectangle Then
                '  Dim gr As System.Drawing.Drawing2D.GraphicsPath = AdvanceIT.Drawing.Rectangles.Helpers.DrawSolidRoundedRectangle(dr, 10)
                ' e.Graphics.FillPath(_GradientBrush, gr)
                'e.Graphics.DrawPath(_BorderPen, gr)

            End If
            If _icon Is Nothing Then
                e.Graphics.DrawString(MyBase.Text, Me.Font, New SolidBrush(MyBase.ForeColor), dr, _TextAlignment)
            Else
                e.Graphics.DrawImage(_icon, New Point(2, (Me.Height - _icon.Height) / 2))
                dr.X = _icon.Width + 2
                dr.Width = dr.Width - dr.X
                e.Graphics.DrawString(MyBase.Text, Me.Font, Brushes.Black, dr, _TextAlignment)
            End If
            _GradientBrush.Dispose()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        'Add your custom paint code here
    End Sub
    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        Cursor = Windows.Forms.Cursors.Hand
        _MouseOver = True
        MyBase.OnMouseEnter(e)
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        Cursor = Cursors.Default
        _MouseOver = False
        MyBase.OnMouseLeave(e)
        Invalidate()
    End Sub
    Public Sub New()

        ' This call is required by the Windows Form Designer.


        ' Add any initialization after the InitializeComponent() call
        Me.SetStyle(ControlStyles.DoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.BackColor = Color.Transparent
    End Sub
    'Overrides on
End Class
