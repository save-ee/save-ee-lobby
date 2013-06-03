Public Class FileHash
    Public Shared Function GetFileHash(ByVal p As String) As String
        Dim objReader As System.IO.Stream
        Dim objMD5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim arrHash() As Byte

        ' open file (as read-only)
        objReader = New System.IO.FileStream(p, System.IO.FileMode.Open, System.IO.FileAccess.Read)

        ' hash contents of this stream
        arrHash = objMD5.ComputeHash(objReader)
        Dim txthash As String = ""
        For Each b As Byte In arrHash
            txthash = txthash & ":" & Hex(b)
        Next
        txthash = Mid(txthash, 2)
        ' thanks objects
        objReader.Close()
        objReader.Dispose()
        objReader = Nothing
        objMD5 = Nothing
        Return txthash
    End Function
End Class