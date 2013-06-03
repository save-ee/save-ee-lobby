Public Class WebClient
    Public Shared Function DownloadString(ByVal address As String) As String
        Dim wc As New System.Net.WebClient
        wc.Proxy = Nothing
        Return wc.DownloadString(address)
        wc.Dispose()
    End Function
End Class