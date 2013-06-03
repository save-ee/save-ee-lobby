Public Class Globals
    Public Shared Adapter As String
    Public Shared ServerEngine As New Server
    Public Shared PublicIP As String
    Public Shared dbcon As New Databases.AccessDatabase("db.mdb")

    Public Shared Sub WriteLog(ByVal folder As String, ByVal text As String)
        Dim timestamp As String = Date.UtcNow.ToString("[HH:mm:ss]")
        Dim filename As String = folder & "\" & Date.UtcNow.ToString("yyyy-MM-dd") & ".txt"
        Try
            System.IO.Directory.CreateDirectory(folder)
        Catch
        End Try
        LobbyShared.FileIO.WriteFile(filename, timestamp & " " & text & vbCrLf, True)
    End Sub

    Public Shared Function FTPUser() As String
        Dim retVal As String = ""
        Try
            Dim fr As New System.IO.StreamReader("userpass.dat")
            retVal = fr.ReadLine()
            fr.Close()
            fr.Dispose()
        Catch ex As Exception
        End Try
        Return retVal
    End Function

    Public Shared Function FTPPass() As String
        Dim retVal As String = ""
        Try
            Dim fr As New System.IO.StreamReader("userpass.dat")
            fr.ReadLine()
            retVal = fr.ReadLine()
            fr.Close()
            fr.Dispose()
        Catch ex As Exception
        End Try
        Return retVal
    End Function
End Class