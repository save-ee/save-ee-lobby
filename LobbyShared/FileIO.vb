Public Class FileIO
    Public Shared Function ReadFile(ByVal filePath As String) As String
        Dim data As String = ""
        Try
            Dim fr As New System.IO.StreamReader(filePath)
            data = fr.ReadToEnd
            fr.Close()
            fr.Dispose()
        Catch ex As Exception
        End Try
        Return data
    End Function
    Public Shared Function WriteFile(ByVal filePath As String, ByVal data As String, Optional ByVal append As Boolean = False) As Boolean
        Try
            Dim fw As New System.IO.StreamWriter(filePath, append)
            fw.Write(data)
            fw.Close()
            fw.Dispose()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
End Class