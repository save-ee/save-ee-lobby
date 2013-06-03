Public Class Dictionary
    Public Shared Function DictionaryToString(ByVal d As Dictionary(Of String, String)) As String
        Dim ret As String = ""
        For Each k As String In d.Keys
            ret = ret & Chr(0) & Chr(0) & k & Chr(0) & d(k)
        Next
        ret = Mid(ret, 3)
        Return ret
    End Function
    Public Shared Function StringToDictionary(ByVal s As String) As Dictionary(Of String, String)
        Dim ret As New Dictionary(Of String, String)
        Dim rows() As String = Split(Chr(0) & Chr(0))
        For Each r As String In rows
            Try
                ret(Split(r, Chr(0))(0)) = Split(r, Chr(0))(1)
            Catch ex As Exception
            End Try
        Next
        Return ret
    End Function
    Public Shared Function CompareDictionary(ByVal d1 As Dictionary(Of String, String), ByVal d2 As Dictionary(Of String, String)) As Boolean
        Try
            If d1.Count <> d2.Count Then
                Return False
            End If
            For Each k As String In d1.Keys
                If d1(k) <> d2(k) Then
                    Return False
                End If
            Next
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Shared Sub CloneDictionary(ByVal src As Dictionary(Of String, String), ByVal dest As Dictionary(Of String, String), Optional ByVal ClearDest As Boolean = False)
        If ClearDest = True Then dest.Clear()
        For Each k As String In src.Keys
            dest(k) = src(k)
        Next
    End Sub
End Class