Public Class ClientEngine
    Private WithEvents s As New Network.Sckt
    Private Buffer As New System.Text.StringBuilder
    Private Write_Lock As New Object

    Public NetworkKey() As Byte = LobbyShared.Globals.ServerSalt
    Public Event Connecting()
    Public Event ConnectFailed()
    Public Event Connected()
    Public Event ConnectionLost()
    Public Event NetworkInit()

    Public Event VerifyKeyPass()
    Public Event VerifyKeyFail()

    Public Event LoginFail()
    Public Event LoginPass()
    Public Event LoginLocked()
    Public Event LoginAccountExists()
    Public Event LoginDoesntExist()
    Public Event LoginComplete()
    Public Event LoginAlreadyOn()

    Public Event UserAdd(ByVal m As LobbyShared.NetworkMessage)
    Public Event UserRemove(ByVal m As LobbyShared.NetworkMessage)

    Public Event FriendAdd(ByVal m As LobbyShared.NetworkMessage)
    Public Event FriendRemove(ByVal m As LobbyShared.NetworkMessage)
    Public Event FriendList(ByVal m As LobbyShared.NetworkMessage)

    Public Event CheaterAdd(ByVal m As LobbyShared.NetworkMessage)
    Public Event CheaterRemove(ByVal m As LobbyShared.NetworkMessage)
    Public Event CheaterList(ByVal m As LobbyShared.NetworkMessage)

    Public Event IgnoreAdd(ByVal m As LobbyShared.NetworkMessage)
    Public Event IgnoreRemove(ByVal m As LobbyShared.NetworkMessage)
    Public Event IgnoreList(ByVal m As LobbyShared.NetworkMessage)

    Public Event OnLobbyChat(ByVal m As LobbyShared.NetworkMessage)
    Public Event OnLobbyWhisper(ByVal m As LobbyShared.NetworkMessage)
    Public Event OnLobbyWarn(ByVal m As LobbyShared.NetworkMessage)
    Public Event OnLobbyAlert(ByVal m As LobbyShared.NetworkMessage)

    Public Event OnGetKeyList(ByVal m As LobbyShared.NetworkMessage)

    Public Event OnChangeUserRights(ByVal m As LobbyShared.NetworkMessage)
    Public Event OnChangeStatus(ByVal m As LobbyShared.NetworkMessage)

    Public Event OnGameAdd(ByVal m As LobbyShared.NetworkMessage)
    Public Event OnGameRemove(ByVal m As LobbyShared.NetworkMessage)
    Public Event OnLocate(ByVal m As LobbyShared.NetworkMessage)
    Public Event MiscMsgReturn(ByVal m As LobbyShared.NetworkMessage)

#Region "Socket conn"
    Public Sub Connect()
        s.Connect(Globals.ServerIP, LobbyShared.Globals.ServerPort)
    End Sub
    Public Sub Disconnect()
        s.Close()
    End Sub
#End Region
#Region "SocketEvents"
    Private Sub s_Connected() Handles s.Connected
        RaiseEvent Connected()
    End Sub
    Private Sub s_ConnectFailed() Handles s.ConnectFailed
        RaiseEvent ConnectFailed()
    End Sub
    Private Sub s_Connecting() Handles s.Connecting
        RaiseEvent Connecting()
    End Sub
    Private Sub s_ConnectionLost() Handles s.ConnectionLost
        RaiseEvent ConnectionLost()
    End Sub
#End Region
#Region "Data handling"
    Public Sub SendPacket(ByVal p As LobbyShared.NetworkMessage)
        Dim tp As String = p.SerializeForTransport(NetworkKey)
        Send(tp & vbCrLf)
    End Sub
    Public Sub Send(ByVal d As String)
        SyncLock Write_Lock
            s.Send(d)
        End SyncLock
    End Sub
    Private Sub s_DataArrival(ByVal b() As Byte) Handles s.DataArrival
        Try
            Buffer.Append(System.Text.Encoding.ASCII.GetString(b))
            While Buffer.ToString.Contains(vbCrLf) = True
                Dim BufferDump As String = Buffer.ToString
                Dim Msg As String = Mid(BufferDump, 1, InStr(BufferDump, vbCrLf) - 1)
                Buffer.Remove(0, InStr(BufferDump, vbCrLf) + 1)
                Dim m As New LobbyShared.NetworkMessage
                m.UnSerializeFromTransport(Msg, NetworkKey)
                ProcessMsg(m)
            End While
        Catch ex As Exception
            MsgBox(ex, MsgBoxStyle.Critical, "Data Buffer Error")
        End Try
    End Sub
#End Region
#Region "ProcessMsg"
    Private Sub ProcessMsg(ByVal m As LobbyShared.NetworkMessage)
        If m.MessageType = LobbyShared.NetworkMessage.MsgTypes.NetworkInit Then
            Try
                NetworkKey = System.Text.Encoding.ASCII.GetBytes(m.StringCollection("K2"))
                RaiseEvent NetworkInit()
                Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.VerifyKey)
                For Each k As String In Globals.HardwareList.Keys
                    p.StringCollection(k) = Globals.HardwareList(k)
                Next
                p.StringCollection("CLIENTVERSION") = LobbyShared.Globals.ClientVersion
                SendPacket(p)
            Catch ex As Exception
                Disconnect()
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.VerifyKeyPass Then
            Try
                RaiseEvent VerifyKeyPass()
            Catch ex As Exception
                MsgBox("VerifyKeyPass")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.VerifyKeyFail Then
            Try
                Process.Start("http://www.save-ee.com/viewforum.php?f=23")
                Application.Exit()
            Catch ex As Exception
                MsgBox("VerifyKeyFail")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LoginAttemptFail Then
            Try
                RaiseEvent LoginFail()
            Catch ex As Exception
                MsgBox("LoginFail")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LoginLocked Then
            Try
                RaiseEvent LoginLocked()
            Catch ex As Exception
                MsgBox("LoginLocked")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LoginDoesntExist Then
            Try
                RaiseEvent LoginDoesntExist()
            Catch ex As Exception
                MsgBox("LoginDoesntExist")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LoginAccountExist Then
            Try
                RaiseEvent LoginAccountExists()
            Catch ex As Exception
                MsgBox("LoginAccountExists")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LoginAttemptPass Then
            Try
                RaiseEvent LoginComplete()
            Catch ex As Exception
                MsgBox("LoginComplete")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LoginAttemptAlreadyLoggedIn Then
            Try
                RaiseEvent LoginAlreadyOn()
            Catch ex As Exception
                MsgBox("LoginAlreadyOn")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.UserAdd Then
            Try
                RaiseEvent UserAdd(m)
            Catch ex As Exception
                MsgBox("UserAdd")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.UserRemove Then
            Try
                RaiseEvent UserRemove(m)
            Catch ex As Exception
                MsgBox("UserRemove")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.FriendList Then
            Try
                RaiseEvent FriendList(m)
            Catch ex As Exception
                MsgBox("FriendList")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.FriendAdd Then
            Try
                RaiseEvent FriendAdd(m)
            Catch ex As Exception
                MsgBox("FriendAdd")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.FriendRemove Then
            Try
                RaiseEvent FriendRemove(m)
            Catch ex As Exception
                MsgBox("FriendRemove")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.CheaterList Then
            Try
                RaiseEvent CheaterList(m)
            Catch ex As Exception
                MsgBox("CheaterList")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.CheaterAdd Then
            Try
                RaiseEvent CheaterAdd(m)
            Catch ex As Exception
                MsgBox("CheaterAdd")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.CheaterRemove Then
            Try
                RaiseEvent CheaterRemove(m)
            Catch ex As Exception
                MsgBox("CheaterRemove")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.IgnoreList Then
            Try
                RaiseEvent IgnoreList(m)
            Catch ex As Exception
                MsgBox("IgnoreList")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.IgnoreAdd Then
            Try
                RaiseEvent IgnoreAdd(m)
            Catch ex As Exception
                MsgBox("IgnoreAdd")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.IgnoreRemove Then
            Try
                RaiseEvent IgnoreRemove(m)
            Catch ex As Exception
                MsgBox("IgnoreRemove")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LobbyChat Then
            Try
                RaiseEvent OnLobbyChat(m)
            Catch ex As Exception
                MsgBox("OnLobbyChat")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LobbyWhisper Then
            Try
                RaiseEvent OnLobbyWhisper(m)
            Catch ex As Exception
                MsgBox("OnLobbyWhisper")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LobbyWarn Then
            Try
                RaiseEvent OnLobbyWarn(m)
            Catch ex As Exception
                MsgBox("OnLobbyWarn")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LobbyAlert Then
            Try
                RaiseEvent OnLobbyAlert(m)
            Catch ex As Exception
                MsgBox("OnLobbyAlert")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.GetKeyList Then
            Try
                RaiseEvent OnGetKeyList(m)
            Catch ex As Exception
                MsgBox("OnGetKeyList")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.ChangeUserRights Then
            Try
                RaiseEvent OnChangeUserRights(m)
            Catch ex As Exception
                MsgBox("OnChangeUserRights")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.ChangeStatus Then
            Try
                RaiseEvent OnChangeStatus(m)
            Catch ex As Exception
                MsgBox("OnChangeStatus")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LocateUser Then
            Try
                RaiseEvent OnLocate(m)
            Catch ex As Exception
                MsgBox("OnLocate")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.GameAdd Then
            Try
                RaiseEvent OnGameAdd(m)
            Catch ex As Exception
                MsgBox("OnGameAdd")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.GameRemove Then
            Try
                RaiseEvent OnGameRemove(m)
            Catch ex As Exception
                MsgBox("OnGameRemove")
            End Try
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.PunishmentMessage Then
            OnPunishmentMessage(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.SendClientMessage Then
            MsgBox(m.StringCollection("TEXT"), m.StringCollection("ICON"), m.StringCollection("HEADER"))
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.MiscMsg Then
            OnMiscMsg(m)
        End If
    End Sub
    Private Sub OnPunishmentMessage(ByVal m As LobbyShared.NetworkMessage)
        If m.StringCollection("ACTION") = "KICK" Then
            MsgBox(Language.Windows.You_were_kicked_from_the_server_by & vbCrLf & m.StringCollection("FROM") & vbCrLf & vbCrLf & _
                   Language.Windows.Reason & ":" & vbCrLf & m.StringCollection("REASON"), _
                   MsgBoxStyle.Exclamation, Language.Windows.You_Were_Kicked)
        ElseIf m.StringCollection("ACTION") = "MUTE" Then
            MsgBox(Language.Windows.You_were_muted_by & vbCrLf & m.StringCollection("FROM") & vbCrLf & vbCrLf & _
                   Language.Windows.Reason & ":" & vbCrLf & m.StringCollection("REASON") & vbCrLf & vbCrLf & _
                   Language.Windows.Until & ":" & vbCrLf & m.StringCollection("UNTIL") & " (UTC)", _
                   MsgBoxStyle.Exclamation, Language.Windows.You_Were_Muted)
        ElseIf m.StringCollection("ACTION") = "BAN" Then
            MsgBox(Language.Windows.You_were_banned_from_the_server_by & vbCrLf & m.StringCollection("FROM") & vbCrLf & vbCrLf & _
                   Language.Windows.Reason & ":" & vbCrLf & m.StringCollection("REASON") & vbCrLf & vbCrLf & _
                   Language.Windows.Until & ":" & vbCrLf & m.StringCollection("UNTIL") & " (UTC)", _
                   MsgBoxStyle.Exclamation, Language.Windows.You_Were_Banned)
        End If
    End Sub
    Private Sub OnMiscMsg(ByVal m As LobbyShared.NetworkMessage)
        If m.StringCollection.ContainsKey("GETDETAILS") = True Then
            If m.StringCollection.ContainsKey("MISCMSGRETURN") Then
                RaiseEvent MiscMsgReturn(m)
            Else
                Dim f As String = m.StringCollection("FROM")
                m.StringCollection.Clear()
                Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.MiscMsg)
                Dim t As New LobbyShared.NetworkMessage.MsgTable
                t.AddRow(New String() {"username"})
                For Each s As String In Globals.UserAccounts.Keys
                    t.AddRow(New String() {s})
                Next
                m.TableCollection("USERNAMES") = t

                m.StringCollection("GETDETAILS") = True
                m.StringCollection("MISCMSGRETURN") = True
                m.StringCollection("RCPT_" & f) = "1"
                SendPacket(m)
            End If
        ElseIf m.StringCollection.ContainsKey("FORCEUPDATE") Then
            ' Encode username and password for autologin
            Dim UserData As String = Globals.CurrentUser.Username & "[-:+:-]" & Globals.CurrentUser.Password
            Dim UserBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(UserData)
            LobbyShared.Crypt.Cryptic(UserBytes, New Byte() {1, 2, 3, 4, 5})
            Dim UsersB64 As String = LobbyShared.Base64.EncodeFromBytes(UserBytes)
            My.Settings.AutoLogin = UsersB64

            My.Settings.Save()

            For Each p As Process In Process.GetProcesses()
                Try
                    If p.MainModule.FileName = Globals.ProcessPath AndAlso p.Id <> Process.GetCurrentProcess.Id Then p.Kill()
                Catch
                End Try
            Next
            Application.Restart()
        ElseIf m.StringCollection.ContainsKey("VOTEQUESTION") Then
            RaiseEvent MiscMsgReturn(m)
        End If
    End Sub
    Public Function GetEncoderInfo() As Imaging.ImageCodecInfo
        Dim i As Imaging.ImageCodecInfo = Nothing
        Dim encs() As Imaging.ImageCodecInfo = Imaging.ImageCodecInfo.GetImageEncoders
        For Each im As Imaging.ImageCodecInfo In encs
            If im.MimeType = "image/jpeg" Then
                i = im
                Exit For
            End If
        Next
        Return i
    End Function
    Public Sub Login(ByVal u As String, ByVal p As String)
        Dim m As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LoginAttempt)
        m.StringCollection("USERNAME") = u
        m.StringCollection("PASSWORD") = p
        SendPacket(m)
    End Sub
    Public Sub CreateAccount(ByVal u As String, ByVal p As String)
        Dim m As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LoginCreateUser)
        m.StringCollection("USERNAME") = u
        m.StringCollection("PASSWORD") = p
        SendPacket(m)
    End Sub
#End Region
#Region "User Status, Rights"
    Public Sub ChangeStatus()
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.ChangeStatus)
        p.StringCollection("INGAMEEE") = Globals.CurrentUser.InGameEE
        p.StringCollection("INGAMEAOC") = Globals.CurrentUser.InGameAoC
        p.StringCollection("AFK") = Globals.CurrentUser.AFK
        SendPacket(p)
    End Sub
    Public Sub ChangeUserRights(ByVal username As String, ByVal security As LobbyShared.User.SecurityGroups)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.ChangeUserRights)
        p.StringCollection("TO") = username
        p.StringCollection("SECURITY") = security
        SendPacket(p)
    End Sub
#End Region
#Region "Games, Locate"
    Public Sub LocateUser(ByVal u() As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LocateUser)
        For Each s As String In u
            p.StringCollection(s) = "1"
        Next
        SendPacket(p)
    End Sub
    Public Sub JoinGame(ByVal hostip As String, ByVal eeuser As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.GameJoin)
        p.StringCollection("FROM") = Globals.CurrentUser.Username
        p.StringCollection("HOSTIP") = hostip
        p.StringCollection("EEUSER") = eeuser

        SendPacket(p)
    End Sub
    Public Sub JoinGame2(ByVal hostip As String, ByVal eeuser As String, ByVal anonip As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.GameJoin2)
        p.StringCollection("FROM") = Globals.CurrentUser.Username
        p.StringCollection("HOSTIP") = hostip
        p.StringCollection("EEUSER") = eeuser
        p.StringCollection("ANONIP") = anonip

        SendPacket(p)
    End Sub
    Public Sub AddGame(ByVal gamedata As LobbyShared.NetworkMessage.MsgTable)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.GameAdd)
        p.TableCollection("GAMEDATA") = gamedata
        SendPacket(p)
    End Sub
    Public Sub RemoveGame(ByVal ip As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.GameRemove)
        p.StringCollection("IP") = ip
        SendPacket(p)
    End Sub
#End Region
#Region "GetKeyList"
    Public Sub GetKeyList(ByVal rcpt() As String, ByVal KeyType As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.GetKeyList)
        p.StringCollection("FROM") = Globals.CurrentUser.Username
        p.StringCollection("ACTION") = KeyType
        For Each r As String In rcpt
            p.StringCollection("RCPT_" & r) = "1"
        Next
        SendPacket(p)
    End Sub
#End Region
#Region "Kick, Mute, Ban"
    Public Sub KickPlayer(ByVal rcpt() As String, ByVal reason As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.KickPlayer)
        p.StringCollection("FROM") = Globals.CurrentUser.Username
        p.StringCollection("REASON") = reason
        For Each r As String In rcpt
            p.StringCollection("RCPT_" & r) = "1"
        Next
        SendPacket(p)
    End Sub
#End Region
#Region "Misc, Vote, etc"
    Public Sub MiscMsg(ByVal rcpt() As String, ByVal p As LobbyShared.NetworkMessage)
        p.StringCollection("FROM") = Globals.CurrentUser.Username
        For Each r As String In rcpt
            p.StringCollection("RCPT_" & r) = "1"
        Next
        SendPacket(p)
    End Sub
#End Region
#Region "Chat, Whispers, Warnings, etc"
    Public Sub LobbyChat(ByVal s As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LobbyChat)
        p.StringCollection("TEXT") = s
        SendPacket(p)
    End Sub
    Public Sub LobbyWhisper(ByVal s As String, ByVal rcpt() As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LobbyWhisper)
        p.StringCollection("FROM") = Globals.CurrentUser.Username
        p.StringCollection("TEXT") = s
        For Each r As String In rcpt
            p.StringCollection("RCPT_" & r) = "1"
        Next
        SendPacket(p)
    End Sub
    Public Sub LobbyWarn(ByVal s As String, ByVal rcpt() As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LobbyWarn)
        p.StringCollection("FROM") = Globals.CurrentUser.Username
        p.StringCollection("TEXT") = s
        For Each r As String In rcpt
            p.StringCollection("RCPT_" & r) = "1"
        Next
        SendPacket(p)
    End Sub
    Public Sub LobbyAlert(ByVal s As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LobbyAlert)
        p.StringCollection("FROM") = Globals.CurrentUser.Username
        p.StringCollection("TEXT") = s
        SendPacket(p)
    End Sub
#End Region
#Region "Friends, Cheaters, Ignore Add/Remove"
    Public Sub AddFriend(ByVal u() As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.FriendAdd)
        For Each s As String In u
            p.StringCollection(s) = "1"
        Next
        SendPacket(p)
    End Sub
    Public Sub RemoveFriend(ByVal u() As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.FriendRemove)
        For Each s As String In u
            p.StringCollection(s) = "1"
        Next
        SendPacket(p)
    End Sub
    Public Sub AddCheater(ByVal u() As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.CheaterAdd)
        For Each s As String In u
            p.StringCollection(s) = "1"
        Next
        SendPacket(p)
    End Sub
    Public Sub RemoveCheater(ByVal u() As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.CheaterRemove)
        For Each s As String In u
            p.StringCollection(s) = "1"
        Next
        SendPacket(p)
    End Sub
    Public Sub AddIgnore(ByVal u() As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.IgnoreAdd)
        For Each s As String In u
            p.StringCollection(s) = "1"
        Next
        SendPacket(p)
    End Sub
    Public Sub RemoveIgnore(ByVal u() As String)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.IgnoreRemove)
        For Each s As String In u
            p.StringCollection(s) = "1"
        Next
        SendPacket(p)
    End Sub
#End Region
End Class