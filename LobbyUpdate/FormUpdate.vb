Public Class FormUpdate
    Dim WithEvents wc As New System.Net.WebClient
    Public FileQueue As New Queue(Of UpdateFile)
    Public CurrentDownload As UpdateFile

    Dim uir() As String

    Public Enum Actions
        DownloadUpdateInfo
    End Enum
    Public cAction As Actions = Actions.DownloadUpdateInfo

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Show()
        Me.Refresh()
        For Each p As Process In Process.GetProcesses
            If p.ProcessName.ToLower.Contains("lobbyclient") Or p.ProcessName.ToLower.Contains("patchupdate") Then
                Try
                    p.Kill()
                Catch ex As Exception
                End Try
            End If
        Next
        TextBox1.AppendText("Checking for updates, please wait..." & vbCrLf)
        wc.Proxy = Nothing
        wc.DownloadStringAsync(New Uri("http://www.save-ee.com/lobby/updateinfo.dat"))
        Me.Refresh()
    End Sub

    Private Sub wc_DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.DownloadFileCompleted
        dlf()
    End Sub

    Private Sub wc_DownloadProgressChanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles wc.DownloadProgressChanged
        Try
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = 100
            ProgressBar1.Value = e.ProgressPercentage
            Label2.Text = " Downloading " & CurrentDownload.RealFile & " (" & e.BytesReceived & " / " & e.TotalBytesToReceive & ")"
        Catch ex As Exception

        End Try

    End Sub
    Private Sub wc_DownloadStringCompleted(ByVal sender As Object, ByVal e As System.Net.DownloadStringCompletedEventArgs) Handles wc.DownloadStringCompleted
        If cAction = Actions.DownloadUpdateInfo Then
            Try
                Dim f As New IO.StreamWriter("updateinfo.dat", False)
                f.Write(e.Result)
                f.Close()
                f.Dispose()
            Catch ex As Exception

            End Try
            uir = Split(e.Result, vbCrLf)
            TextBox1.AppendText(uir.Length & " file(s) found, computing and comparing hashes, please wait..." & vbCrLf & vbCrLf)

            Dim cnt As Integer = -1
            For Each r As String In uir
                cnt = cnt + 1
                If r <> "" AndAlso InStr(r, "lobbyupdate", CompareMethod.Text) = 0 Then
                    Dim c() As String = Split(r, "[::]")
                    Dim ch As String = ""
                    Try
                        ch = FileHash.GetFileHash(c(2))
                        If ch <> c(4) Then
                            TextBox1.AppendText(c(2) & " different, queued for download..." & vbCrLf)
                            Dim f As New UpdateFile
                            f.FromUpdateString(r)
                            FileQueue.Enqueue(f)
                        Else
                            TextBox1.AppendText(c(2) & " file ok..." & vbCrLf)
                        End If
                    Catch ex As Exception
                        TextBox1.AppendText(c(2) & " different, queued for download..." & vbCrLf)
                        Dim f As New UpdateFile
                        f.FromUpdateString(r)
                        FileQueue.Enqueue(f)
                    End Try
                End If
            Next
            TextBox1.AppendText(vbCrLf)
            If FileQueue.Count = 0 Then
                ' MsgBox("You are currently running the latest version, no update required.", MsgBoxStyle.Information, "Complete")
                Try
                    Shell("LobbyClient.exe", AppWinStyle.NormalFocus)
                Catch
                    MsgBox("Unable to run LobbyClient.exe." & vbCrLf & vbCrLf & "Shell: " & Environment.CurrentDirectory & "\LobbyClient.exe", MsgBoxStyle.Exclamation)
                End Try
                Application.Exit()
            Else
                TextBox1.AppendText(FileQueue.Count & " file(s) different, preparing to download..." & vbCrLf)
                dlf()

            End If

        End If
    End Sub
    Public Sub dlf()
        If FileQueue.Count > 0 Then
            CurrentDownload = FileQueue.Dequeue
            wc.DownloadFileAsync(New Uri(CurrentDownload.Url), CurrentDownload.RealFile)
        Else
            '   MsgBox("Update Complete.", MsgBoxStyle.Information, "Complete")
            Try
                Shell("LobbyClient.exe", AppWinStyle.NormalFocus)
            Catch
                MsgBox("Unable to run LobbyClient.exe." & vbCrLf & vbCrLf & "Shell: " & Environment.CurrentDirectory & "\LobbyClient.exe", MsgBoxStyle.Exclamation)
            End Try
            Application.Exit()
        End If
    End Sub
End Class
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
Public Class FileHash
    Public Shared Function GetFileHash(ByVal p As String) As String
        Dim objReader As System.IO.Stream
        Dim objMD5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim arrHash() As Byte

        ' open file (as read-only)
        objReader = New System.IO.FileStream(p, IO.FileMode.Open, IO.FileAccess.Read)

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

