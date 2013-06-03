Public Class FtpClient
    Public Shared Sub UploadData(ByVal remoteFile As String, ByVal username As String, ByVal password As String, ByVal data As String)
        Dim clsRequest As System.Net.FtpWebRequest = _
            DirectCast(System.Net.WebRequest.Create(remoteFile), System.Net.FtpWebRequest)
        clsRequest.KeepAlive = False
        clsRequest.Credentials = New System.Net.NetworkCredential(username, password)
        clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile

        ' read in file...
        Dim bFile() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(data)

        ' upload file...
        Dim clsStream As System.IO.Stream = _
            clsRequest.GetRequestStream()
        clsStream.Write(bFile, 0, bFile.Length)
        clsStream.Close()
        clsStream.Dispose()
    End Sub
End Class