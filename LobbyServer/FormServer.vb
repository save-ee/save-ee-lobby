Public Class FormServer
    Public Sub LoadMutedHardware()
        Dim m As New Databases.AccessDatabase(Globals.dbcon)
        m.Query("SELECT * FROM MutedHardware")
        While m.IsLastRecord = False
            Globals.ServerEngine.MutedHardware(m.RecordData("hwkey")) = New List(Of String)(New String() {m.RecordData("hwvalue"), m.RecordData("addedby"), m.RecordData("starttime"), m.RecordData("endtime"), m.RecordData("reason"), m.RecordData("comments")})
            m.MoveNext()
        End While
    End Sub
    Public Sub LoadBannedHardware()
        Dim m As New Databases.AccessDatabase(Globals.dbcon)
        m.Query("SELECT * FROM BannedHardware")
        While m.IsLastRecord = False
            Globals.ServerEngine.BannedHardware(m.RecordData("hwkey")) = New List(Of String)(New String() {m.RecordData("hwvalue"), m.RecordData("addedby"), m.RecordData("starttime"), m.RecordData("endtime"), m.RecordData("reason"), m.RecordData("comments")})
            m.MoveNext()
        End While
    End Sub
    Private Sub FormServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Globals.PublicIP = Network.WebClient.DownloadString("http://www.save-ee.com/whatismyip.php")
        If LobbyShared.Globals.IsLocal Then
            Globals.Adapter = LobbyShared.Globals.LocalAdapter
        Else
            Globals.Adapter = Globals.PublicIP
            Network.FtpClient.UploadData("ftp://www.save-ee.com/lobby/serverinfo.dat", Globals.FTPUser, Globals.FTPPass, Globals.PublicIP)
        End If
        LoadBannedHardware()
        LoadMutedHardware()
        StartButton_Click(Nothing, Nothing)
    End Sub
    Private Sub FormServer_FormClosing(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing
        StopButton_Click(Nothing, Nothing)
    End Sub
    Private Sub StatusTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusTimer.Tick
        maxcon.Text = Globals.ServerEngine.MaxConnections
        curcon.Text = Globals.ServerEngine.CurrentConnection
    End Sub
    Private Sub RecentRegistrationsTimer_Tick(sender As Object, e As EventArgs) Handles RecentRegistrationsTimer.Tick
        SyncLock Globals.ServerEngine.NumOfRecentRegisters_Lock
            Globals.ServerEngine.NumOfRecentRegisters = 0
        End SyncLock
    End Sub
    Private Sub MuteBanRemoveTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MuteBanRemoveTimer.Tick
        Dim m As New Databases.AccessDatabase(Globals.dbcon)
        ' Check mutes
        SyncLock Globals.ServerEngine.MutedHardware_Lock
            For Each k As String In Globals.ServerEngine.MutedHardware.Keys
                ' If mute isn't permanent
                If Globals.ServerEngine.MutedHardware(k)(3) <> "permanent" Then
                    ' ... check to see if end time is before the current
                    If Date.ParseExact(Globals.ServerEngine.MutedHardware(k)(3), "yyyy.MM.dd.HH.mm.ss", System.Globalization.CultureInfo.InvariantCulture) < Date.UtcNow Then
                        ' Remove from HW list
                        Globals.ServerEngine.MutedHardware.Remove(k)
                        ' Delete from DB
                        m.Query("DELETE FROM MutedHardware WHERE hwkey='" & k & "'")
                        Exit For
                    End If
                End If
            Next
        End SyncLock
        ' Check bans
        SyncLock Globals.ServerEngine.BannedHardware_Lock
            For Each k As String In Globals.ServerEngine.BannedHardware.Keys
                ' If ban isn't permanent
                If Globals.ServerEngine.BannedHardware(k)(3) <> "permanent" Then
                    ' ... check to see if end time is before the current
                    If Date.ParseExact(Globals.ServerEngine.BannedHardware(k)(3), "yyyy.MM.dd.HH.mm.ss", System.Globalization.CultureInfo.InvariantCulture) < Date.UtcNow Then
                        ' Remove from HW list
                        Globals.ServerEngine.BannedHardware.Remove(k)
                        ' Delete from DB
                        m.Query("DELETE FROM BannedHardware WHERE hwkey='" & k & "'")
                        Exit For
                    End If
                End If
            Next
        End SyncLock
    End Sub
    Private Sub SessionRemovalTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SessionRemovalTimer.Tick
        Dim needsRemoved As New List(Of String)
        SyncLock Globals.ServerEngine.Connections_Lock
            ' Check for and close unneeded sessions
            For Each s As Session In Globals.ServerEngine.Connections.Values
                ' Only sessions without a login
                If s.LoginTime = "" Then
                    ' Add 5 minutes to start time.  If this is lower than the current time, then the session has been active for 5 minutes without a login.
                    If Date.ParseExact(s.StartTime, "yyyy.MM.dd.HH.mm.ss", System.Globalization.CultureInfo.InvariantCulture).AddMinutes(5) < Date.UtcNow Then
                        needsRemoved.Add(s.Guid)
                        ' Close the session
                        s.Close()
                    End If
                End If
            Next

            ' Remove these sessions from the connections dictionary
            For Each s As String In needsRemoved
                If Globals.ServerEngine.Connections.ContainsKey(s) Then
                    Globals.ServerEngine.Connections.Remove(s)
                End If
            Next
        End SyncLock
    End Sub
    Private Sub StartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartButton.Click
        Globals.ServerEngine.Adapter = Globals.Adapter
        Globals.ServerEngine.Port = LobbyShared.Globals.ServerPort
        Globals.ServerEngine.Start()
        If LobbyShared.Globals.IsLocal = False Then
            Network.FtpClient.UploadData("ftp://www.save-ee.com/lobby/status.dat", Globals.FTPUser, Globals.FTPPass, "Online")
        End If
    End Sub
    Private Sub StopButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopButton.Click
        Globals.ServerEngine.Stop()
        If LobbyShared.Globals.IsLocal = False Then
            Network.FtpClient.UploadData("ftp://www.save-ee.com/lobby/status.dat", Globals.FTPUser, Globals.FTPPass, "Offline")
        End If
    End Sub
    Private Sub TrayButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrayButton.Click
        Me.ShowInTaskbar = False
        Me.Hide()
        TrayNotifyIcon.Visible = True
    End Sub
    Private Sub TrayNotifyIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrayNotifyIcon.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
        TrayNotifyIcon.Visible = False
    End Sub
    Private Sub ShowSaveEELobbyServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowSaveEELobbyServerToolStripMenuItem.Click
        TrayNotifyIcon_MouseDoubleClick(Nothing, Nothing)
    End Sub
    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub ListConnectionsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListConnectionsButton.Click
        Dim f As New FormConnections
        f.Show()
    End Sub
End Class
