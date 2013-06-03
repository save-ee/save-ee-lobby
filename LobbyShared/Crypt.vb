Public Class Crypt
    Public Shared Sub Cryptic(ByRef data() As Byte, ByVal key() As Byte)
        Dim keyPos As Integer = -1
        For OuterLoop As Integer = 0 To data.Length - 1
            keyPos += 1
            If keyPos = key.Length Then keyPos = 0
            data(OuterLoop) = data(OuterLoop) Xor key(keyPos)
        Next
    End Sub
End Class