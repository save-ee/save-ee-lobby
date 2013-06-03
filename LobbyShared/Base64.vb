Public Class Base64
    Public Enum EncodingOptions
        ASCII
        UNICODE
    End Enum
    Public Shared Function EncodeFromString(ByVal d As String, Optional ByVal encode As EncodingOptions = EncodingOptions.ASCII) As String
        Dim b() As Byte = Nothing
        If encode = EncodingOptions.ASCII Then
            b = System.Text.Encoding.ASCII.GetBytes(d)
        ElseIf encode = EncodingOptions.UNICODE Then
            b = System.Text.Encoding.Unicode.GetBytes(d)
        End If
        Return Convert.ToBase64String(b)
    End Function
    Public Shared Function EncodeFromBytes(ByVal b() As Byte) As String
        Return Convert.ToBase64String(b)
    End Function
    Public Shared Function DecodeToString(ByVal d As String, Optional ByVal encode As EncodingOptions = EncodingOptions.ASCII) As String
        Dim b() As Byte = Convert.FromBase64String(d)
        Dim ret As String = ""
        If encode = EncodingOptions.ASCII Then
            ret = System.Text.Encoding.ASCII.GetString(b)
        ElseIf encode = EncodingOptions.UNICODE Then
            ret = System.Text.Encoding.Unicode.GetString(b)
        End If
        Return ret
    End Function
    Public Shared Function DecodeToBytes(ByVal d As String) As Byte()
        Return Convert.FromBase64String(d)
    End Function
End Class