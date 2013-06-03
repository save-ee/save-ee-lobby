Imports System.Runtime.InteropServices

Public Class IdleTime
    Private Declare Function GetLastInputInfo Lib "User32.dll" _
      (ByRef lastInput As LASTINPUTINFO) As Boolean

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure LASTINPUTINFO
        Public cbSize As Int32
        Public dwTime As Int32
    End Structure

    Public Shared ReadOnly Property IdleTime() As Integer
        Get
            Dim lastInput As New LASTINPUTINFO
            lastInput.cbSize = Marshal.SizeOf(lastInput)

            If GetLastInputInfo(lastInput) Then
                Return (System.Environment.TickCount - lastInput.dwTime) / 1000
            End If
        End Get
    End Property
End Class