Public Class Adapters
    Public Shared Function GetList() As String()
        Dim TotalAdapters As Integer = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName).Length
        Dim Adapters(TotalAdapters - 1) As String
        Dim NextAdapter As Integer = 0
        For Each ip As System.Net.IPAddress In System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName)
            Adapters(NextAdapter) = ip.ToString
            NextAdapter += 1
        Next
        Return Adapters
    End Function
End Class