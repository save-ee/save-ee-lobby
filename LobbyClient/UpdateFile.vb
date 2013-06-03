Public Class UpdateFile
    Public Url As String
    Public TempFile As String
    Public RealFile As String
    Public Size As Long
    Public Hash As String
    Public Sub FromUpdateString(ByVal s As String)
        Dim c() As String = Split(s, "[::]")
        Url = c(0)
        TempFile = c(1)
        RealFile = c(2)
        Size = c(3)
        Hash = c(4)
    End Sub
End Class