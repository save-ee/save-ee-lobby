Public Class Session
    Inherits LobbyShared.User

    Public Property Guid As String = System.Guid.NewGuid.ToString
    Public Property StartTime As String = ""

    Public Property IsAuth As Boolean = False
    Public Property LoginTime As String = ""
    Public LocateString As String = ""

    Public IgnoreList As New List(Of String)
    Public IgnoreList_Lock As New Object

    Public HardwareList As New Dictionary(Of String, String)
    Public HardwareList_Lock As New Object

    Private WithEvents s As New Network.Sckt
    Private Buffer As New System.Text.StringBuilder
    Private Write_Lock As New Object

    Public NetworkKey() As Byte = System.Text.Encoding.ASCII.GetBytes(System.Guid.NewGuid.ToString)
    Public Event ConnectionLost(ByVal s As Session)
    Public Event BroadCast(ByVal m As LobbyShared.NetworkMessage)
#Region "Socket"
    Public Sub New()
        Me.StartTime = Date.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss")
    End Sub
    Public Sub New(ByVal ns As System.Net.Sockets.Socket, ByVal ip As String)
        s = New Network.Sckt(ns)

        Me.StartTime = Date.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss")
        Me.PublicIP = ip
    End Sub
    Public Sub Close()
        s.Close()
    End Sub
    Public Sub Send(ByVal d As String)
        SyncLock Write_Lock
            s.Send(d)
        End SyncLock
    End Sub
    Public Sub SendSync(ByVal d As String)
        SyncLock Write_Lock
            s.SendSync(d)
        End SyncLock
    End Sub
    Public Sub SendPacket(ByVal p As LobbyShared.NetworkMessage)
        Dim tp As String = p.SerializeForTransport(NetworkKey)
        Send(tp & vbCrLf)
    End Sub
    Public Sub SendPacketSync(ByVal p As LobbyShared.NetworkMessage)
        Dim tp As String = p.SerializeForTransport(NetworkKey)
        SendSync(tp & vbCrLf)
    End Sub
    Private Sub s_ConnectionLost() Handles s.ConnectionLost
        RaiseEvent ConnectionLost(Me)
    End Sub
    Private Sub s_DataArrival(ByVal b() As Byte) Handles s.DataArrival
        Try
            Buffer.Append(System.Text.Encoding.ASCII.GetString(b))
            While Buffer.ToString.Contains(vbCrLf)
                Dim BufferDump As String = Buffer.ToString
                Dim Msg As String = Mid(BufferDump, 1, InStr(BufferDump, vbCrLf) - 1)
                Buffer.Remove(0, InStr(BufferDump, vbCrLf) + 1)
                Dim m As New LobbyShared.NetworkMessage
                m.UnSerializeFromTransport(Msg, NetworkKey)
                ProcessMsg(m)
            End While

        Catch ex As Exception
            Close()
        End Try
    End Sub
#End Region
    Private Sub ProcessMsg(ByVal m As LobbyShared.NetworkMessage)
        If m.MessageType = LobbyShared.NetworkMessage.MsgTypes.VerifyKey Then
            Process_VerifyKey(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LoginAttempt Then
            Process_LoginAttempt(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LoginCreateUser Then
            Process_LoginCreateUser(m)

        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.ChangeStatus Then
            Process_ChangeStatus(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.ChangeUserRights Then
            Process_ChangeUserRights(m)

        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LocateUser Then
            Process_LocateUser(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.GameJoin Then
            Process_GameJoin(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.GameJoin2 Then
            Process_GameJoin2(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.GameAdd Then
            Process_GameAdd(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.GameRemove Then
            Process_GameRemove(m)


        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.GetKeyList Then
            Process_GetKeyList(m)

        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.KickPlayer Then
            Process_KickPlayer(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.MutePlayer Then
            Process_MutePlayer(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.UnmutePlayer Then
            Process_UnmutePlayer(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.SaveMute Then
            Process_SaveMute(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.BanPlayer Then
            Process_BanPlayer(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.UnbanPlayer Then
            Process_UnbanPlayer(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.SaveBan Then
            Process_SaveBan(m)


        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.MiscMsg Then
            Process_MiscMsg(m)

        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LobbyChat Then
            Process_LobbyChat(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LobbyWhisper Then
            Process_LobbyWhisper(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LobbyWarn Then
            Process_LobbyWarn(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LobbyAlert Then
            Process_LobbyAlert(m)

        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.FriendAdd Then
            Process_FriendAdd(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.FriendRemove Then
            Process_FriendRemove(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.CheaterAdd Then
            Process_CheaterAdd(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.CheaterRemove Then
            Process_CheaterRemove(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.IgnoreAdd Then
            Process_IgnoreAdd(m)
        ElseIf m.MessageType = LobbyShared.NetworkMessage.MsgTypes.IgnoreRemove Then
            Process_IgnoreRemove(m)
        End If
    End Sub
#Region "Login"
    Private Sub Process_VerifyKey(ByVal m As LobbyShared.NetworkMessage)
        Dim isBanned As Boolean = False
        Dim verifyVersion As Boolean = False
        Try
            If m.StringCollection("CLIENTVERSION") = LobbyShared.Globals.ClientVersion Then
                verifyVersion = True
            Else
                verifyVersion = False
            End If
        Catch ex As Exception
        End Try

        Try
            m.StringCollection.Remove("CLIENTVERSION")
        Catch ex As Exception
        End Try

        If verifyVersion = False Then
            Dim mm As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.SendClientMessage)
            mm.StringCollection("TEXT") = "Invalid lobby version.  Please manually update."
            mm.StringCollection("HEADER") = "Error"
            mm.StringCollection("ICON") = MsgBoxStyle.Critical
            SendPacketSync(mm)
            Close()
            Exit Sub
        End If

        SyncLock Globals.ServerEngine.BannedHardware_Lock
            For Each hwid As String In m.StringCollection.Keys
                'If hwid.ToUpper.StartsWith("IP") Then
                '   Me.PublicIP = m.StringCollection(hwid)
                'End If
                HardwareList(hwid) = m.StringCollection(hwid)
                For Each k As String In Globals.ServerEngine.BannedHardware.Keys
                    If Globals.ServerEngine.BannedHardware(k)(0) = m.StringCollection(hwid) Then
                        isBanned = True
                        Exit For
                    End If
                Next
            Next
        End SyncLock
        If isBanned Then
            Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.VerifyKeyFail)
            SendPacketSync(p)
            Close()
        Else
            Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.VerifyKeyPass)
            SendPacket(p)
        End If
    End Sub
    Private Sub Process_LoginAttempt(ByVal m As LobbyShared.NetworkMessage)
        If Me.IsAuth = True Then
            Exit Sub
        End If
        If m.StringCollection.ContainsKey("USERNAME") = False OrElse m.StringCollection.ContainsKey("PASSWORD") = False Then
            Close()
        Else
            Dim d As New Databases.AccessDatabase(Globals.dbcon)

            d.Query("SELECT * FROM Users WHERE username='" & m.StringCollection("USERNAME").Replace("'", "") & "'")
            If d.RecordCount = 0 Then
                Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LoginDoesntExist)
                SendPacketSync(p)
                Close()
            Else
                If d.RecordData("username").ToUpper = m.StringCollection("USERNAME").Replace("'", "").ToUpper AndAlso d.RecordData("password") = m.StringCollection("PASSWORD").Replace("'", "") Then
                    Me.Username = m.StringCollection("USERNAME").Replace("'", "")
                    Me.Password = m.StringCollection("PASSWORD").Replace("'", "")
                    If d.RecordData("security").ToUpper = "LOCKED" Then
                        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LoginLocked)
                        SendPacketSync(p)
                        Close()
                    Else
                        Dim isBanned As Boolean = False
                        For Each k As String In Globals.ServerEngine.BannedHardware.Keys
                            ' Using substrings, get username from the key
                            Dim ss As String = k.Substring(0, k.LastIndexOf("_"))
                            ss = ss.Substring(0, ss.LastIndexOf("_"))

                            If ss = Me.Username Then
                                isBanned = True
                                Exit For
                            End If
                        Next
                        If isBanned Then
                            Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.VerifyKeyFail)
                            SendPacketSync(p)
                            Close()
                        Else
                            Process_Login()
                        End If
                    End If
                Else
                    Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LoginAttemptFail)
                    SendPacketSync(p)
                    Close()
                End If
            End If
        End If
    End Sub
    Private Sub Process_LoginCreateUser(ByVal m As LobbyShared.NetworkMessage)
        If Me.IsAuth = True Then
            Exit Sub
        End If
        SyncLock Globals.ServerEngine.NumOfRecentRegisters_Lock
            If Globals.ServerEngine.NumOfRecentRegisters > 5 Then
                Dim mm As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.SendClientMessage)
                mm.StringCollection("TEXT") = "Lobby is receiving too many registrations.  Please try again soon."
                mm.StringCollection("HEADER") = "Error"
                mm.StringCollection("ICON") = MsgBoxStyle.Exclamation
                SendPacketSync(mm)
                Close()
                Exit Sub
            End If
        End SyncLock

        If m.StringCollection.ContainsKey("USERNAME") = False OrElse m.StringCollection.ContainsKey("PASSWORD") = False Then
            Close()
            Exit Sub
        Else
            Dim d As New Databases.AccessDatabase(Globals.dbcon)

            d.Query("SELECT * FROM Users WHERE username='" & m.StringCollection("USERNAME").Replace("'", "") & "'")
            If d.RecordCount > 0 Then
                Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LoginAccountExist)
                SendPacketSync(p)
                Close()
                Exit Sub
            Else
                SyncLock Globals.ServerEngine.NumOfRecentRegisters_Lock
                    If Globals.ServerEngine.NumOfRecentRegisters > 5 Then
                        Dim mm As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.SendClientMessage)
                        mm.StringCollection("TEXT") = "Lobby is receiving too many registrations.  Please try again soon."
                        mm.StringCollection("HEADER") = "Error"
                        mm.StringCollection("ICON") = MsgBoxStyle.Exclamation
                        SendPacketSync(mm)
                        Close()
                        Exit Sub
                    Else
                        Globals.ServerEngine.NumOfRecentRegisters = Globals.ServerEngine.NumOfRecentRegisters + 1
                    End If
                End SyncLock
                d.Query("INSERT INTO Users (username, password, createdon) VALUES ('" & m.StringCollection("USERNAME").Replace("'", "") & "', '" & m.StringCollection("PASSWORD").Replace("'", "") & "', '" & Date.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss") & "')")
                Me.Username = m.StringCollection("USERNAME").Replace("'", "")
                Me.Password = m.StringCollection("PASSWORD").Replace("'", "")
                Process_Login()
            End If
        End If
    End Sub
    Private Sub Process_Login()
        Me.LocateString = Username & " has not been seen in a game."
        ' checking if user is already on
        ' adding to connections table, keys by username
        Dim isLoggedIn As Boolean = False
        SyncLock Globals.ServerEngine.Connections_Lock
            Try
                If Globals.ServerEngine.Connections.ContainsKey(Me.Username) Then
                    isLoggedIn = True
                    Me.IsAuth = False
                Else
                    Globals.ServerEngine.Connections(Me.Username) = Me
                    Globals.ServerEngine.Connections.Remove(Me.Guid)
                    Me.Guid = Me.Username
                End If
            Catch ex As Exception
                isLoggedIn = True
            End Try
        End SyncLock

        ' if user is already on, send the packet and close the connection
        If isLoggedIn Then
            Dim alp As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LoginAttemptAlreadyLoggedIn)
            SendPacketSync(alp)
            Close()
            Exit Sub
        End If

        ' check login and password and get database info for user
        Dim m As New Databases.AccessDatabase(Globals.dbcon)
        m.Query("SELECT * FROM Users WHERE username='" & Me.Username & "' AND password='" & Me.Password & "'")
        If m.RecordCount = 0 Then
            Close()
            Exit Sub
        End If

        ' Replace with actual IP and not the one client sends
        Dim ipid As String = ""
        For Each h As String In HardwareList.Keys
            If h.StartsWith("IP") Then
                ipid = h
            End If
        Next
        If ipid <> "" Then
            HardwareList(ipid) = Me.PublicIP
        End If

        ' generate new hw id list with any new hw
        Dim hwcol() As String = Split(m.RecordData("hardwareid"), vbCrLf)
        Dim tmpcol As New Dictionary(Of String, String)

        ' crash here
        Try
            For Each hw As String In hwcol
                If Trim(hw) <> "" Then
                    If tmpcol.ContainsValue(Split(hw, "[:=:]")(1)) = False Then
                        tmpcol.Add(Split(hw, "[:=:]")(0), Split(hw, "[:=:]")(1))
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
        SyncLock HardwareList_Lock
            For Each hw As String In HardwareList.Keys
                If Trim(HardwareList(hw)) <> "" Then
                    If tmpcol.ContainsValue(HardwareList(hw)) = False Then
                        tmpcol.Add(Username & "_" & hw, HardwareList(hw))
                    End If
                End If
            Next
        End SyncLock
        Dim hardwareListString As String = ""
        SyncLock HardwareList_Lock
            HardwareList.Clear()
            HardwareList = tmpcol
            For Each s As String In tmpcol.Keys
                hardwareListString &= vbCrLf & s & "[:=:]" & tmpcol(s)
                Threading.Thread.Sleep(1)
            Next
            hardwareListString = Mid(hardwareListString, 3)
        End SyncLock
        Try
            Dim m2 As New Databases.AccessDatabase(Globals.dbcon)
            m2.Query("UPDATE Users SET lastlogin='" & Date.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss") & "', ip='" & Me.PublicIP & "', hardwareid='" & hardwareListString.Replace("'", "") & "' WHERE username='" & Me.Username & "'")
        Catch ex As Exception
            ' MsgBox(ex.Message)
            'Close()
            'Exit Sub
        End Try

        ' sending login pass notification
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LoginAttemptPass)
        SendPacketSync(p)
        Threading.Thread.Sleep(100)

        ' Settings Security and Group Flags
        p = New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.ChangeUserRights)
        If m.RecordData("security").ToUpper = "DONATOR" Then
            Me.Security = LobbyShared.User.SecurityGroups.Donator
            p.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.Donator
        ElseIf m.RecordData("security").ToUpper = "MOD" Then
            Me.Security = LobbyShared.User.SecurityGroups.Moderator
            p.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.Moderator
        ElseIf m.RecordData("security").ToUpper = "SUPERMOD" Then
            Me.Security = LobbyShared.User.SecurityGroups.SuperModerator
            p.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.SuperModerator
        ElseIf m.RecordData("security").ToUpper = "ADMIN" Then
            Me.Security = LobbyShared.User.SecurityGroups.Administrator
            p.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.Administrator
        Else
            Me.Security = LobbyShared.User.SecurityGroups.User
            p.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.User
        End If
        SendPacket(p)

        ' load ignore list
        p = New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.IgnoreList)
        m.Query("SELECT * FROM IgnoreList WHERE owner='" & Username & "'")
        While m.IsLastRecord = False
            SyncLock IgnoreList_Lock
                If IgnoreList.Contains(m.RecordData("ignore")) = False Then
                    IgnoreList.Add(m.RecordData("ignore"))
                End If
            End SyncLock
            m.MoveNext()
        End While

        Me.IsAuth = True
        Me.LoginTime = Date.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss")

        ' push single new user notify to all people
        p = New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.UserAdd)
        Dim tbl As New LobbyShared.NetworkMessage.MsgTable
        tbl.AddRow(New String() {"username", "status", "group"})
        tbl.AddRow(New String() {Me.Username, Me.GetStatus(), Me.GetGroup()})
        p.TableCollection("users") = tbl
        RaiseEvent BroadCast(p)

        ' push all online users
        p = New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.UserAdd)
        tbl = New LobbyShared.NetworkMessage.MsgTable
        tbl.AddRow(New String() {"username", "status", "group"})
        SyncLock Globals.ServerEngine.Connections_Lock
            For Each sess As Session In Globals.ServerEngine.Connections.Values
                If sess.IsAuth Then tbl.AddRow(New String() {sess.Username, sess.GetStatus(), sess.GetGroup()})
            Next
        End SyncLock
        p.TableCollection("users") = tbl
        SendPacket(p)

        ' push game list
        'CurGameData("VERSION") = feedback(4)
        'CurGameData("HOST") = Globals.CurrentUser.Username
        'CurGameData("GAMENAME") = System.Text.UnicodeEncoding.Unicode.GetString(gn)
        'CurGameData("STARTEPOCH") = feedback(StartEpoch)
        'CurGameData("ENDEPOCH") = feedback(EndEpoch)
        'CurGameData("MAPTYPE") = feedback(MapType)
        'CurGameData("MAPSIZE") = feedback(MapSize)
        'CurGameData("STARTRESOURCE") = feedback(StartingResources)
        'CurGameData("IP") = Globals.CurrentUser.PublicIP
        'CurGameData("PLAYERS") = Players
        p = New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.GameAdd)
        tbl = New LobbyShared.NetworkMessage.MsgTable
        tbl.AddRow(New String() {"VERSION", "HOST", "GAMENAME", "STARTEPOCH", "ENDEPOCH", "MAPTYPE", "MAPSIZE", "STARTRESOURCE", "IP", "PLAYERS", "INPROGRESS"})
        SyncLock Globals.ServerEngine.CurrentGames_Lock
            For Each game As LobbyShared.NetworkMessage.MsgTable In Globals.ServerEngine.CurrentGames.Values
                tbl.AddRow(New String() {game.GetData(0, "VERSION"), game.GetData(0, "HOST"), game.GetData(0, "GAMENAME"), game.GetData(0, "STARTEPOCH"), game.GetData(0, "ENDEPOCH"), game.GetData(0, "MAPTYPE"), game.GetData(0, "MAPSIZE"), game.GetData(0, "STARTRESOURCE"), game.GetData(0, "IP"), game.GetData(0, "PLAYERS"), game.GetData(0, "INPROGRESS")})
            Next
        End SyncLock
        p.TableCollection("GAMEDATA") = tbl
        SendPacket(p)

        ' push friends list
        p = New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.FriendList)
        m.Query("SELECT * FROM FriendsList WHERE owner='" & Username & "'")
        While m.IsLastRecord = False
            p.StringCollection(m.RecordData("friend")) = "1"
            m.MoveNext()
        End While
        SendPacket(p)

        ' push cheaters list
        p = New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.CheaterList)
        m.Query("SELECT * FROM CheatersList WHERE owner='" & Username & "'")
        While m.IsLastRecord = False
            p.StringCollection(m.RecordData("cheater")) = "1"
            m.MoveNext()
        End While
        SendPacket(p)

        ' push ignore list
        p = New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.IgnoreList)
        m.Query("SELECT * FROM IgnoreList WHERE owner='" & Username & "'")
        While m.IsLastRecord = False
            p.StringCollection(m.RecordData("ignore")) = "1"
            m.MoveNext()
        End While
        SendPacket(p)

        Globals.WriteLog("logs\locate", Me.Username & " has joined the server.")
    End Sub
#End Region
#Region "User Status, Rights"
    Private Sub Process_ChangeStatus(ByVal m As LobbyShared.NetworkMessage)
        Me.InGameEE = m.StringCollection("INGAMEEE")
        Me.InGameAoC = m.StringCollection("INGAMEAOC")
        Me.AFK = m.StringCollection("AFK")

        m.StringCollection.Clear()

        m.StringCollection("FROM") = Me.Username
        m.StringCollection("ICON") = Me.GetStatus()
        m.StringCollection("GROUP") = Me.GetGroup()

        RaiseEvent BroadCast(m)
    End Sub
    Private Sub Process_ChangeUserRights(ByVal m As LobbyShared.NetworkMessage)
        If Me.Security <> LobbyShared.User.SecurityGroups.Administrator Then Exit Sub

        SyncLock Globals.ServerEngine.Connections_Lock
            Dim d As New Databases.AccessDatabase(Globals.dbcon)
            If Globals.ServerEngine.Connections.ContainsKey(m.StringCollection("TO")) Then
                If m.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.User Then
                    d.Query("UPDATE Users SET security=NULL WHERE username='" & m.StringCollection("TO").Replace("'", "") & "'")
                    Globals.ServerEngine.Connections(m.StringCollection("TO")).Security = LobbyShared.User.SecurityGroups.User
                    Globals.ServerEngine.Connections(m.StringCollection("TO")).SendPacket(m)
                ElseIf m.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.Donator Then
                    d.Query("UPDATE Users SET security='DONATOR' WHERE username='" & m.StringCollection("TO").Replace("'", "") & "'")
                    Globals.ServerEngine.Connections(m.StringCollection("TO")).Security = LobbyShared.User.SecurityGroups.Donator
                    Globals.ServerEngine.Connections(m.StringCollection("TO")).SendPacket(m)
                ElseIf m.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.Moderator Then
                    d.Query("UPDATE Users SET security='MOD' WHERE username='" & m.StringCollection("TO").Replace("'", "") & "'")
                    Globals.ServerEngine.Connections(m.StringCollection("TO")).Security = LobbyShared.User.SecurityGroups.Moderator
                    Globals.ServerEngine.Connections(m.StringCollection("TO")).SendPacket(m)
                ElseIf m.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.SuperModerator Then
                    d.Query("UPDATE Users SET security='SUPERMOD' WHERE username='" & m.StringCollection("TO").Replace("'", "") & "'")
                    Globals.ServerEngine.Connections(m.StringCollection("TO")).Security = LobbyShared.User.SecurityGroups.SuperModerator
                    Globals.ServerEngine.Connections(m.StringCollection("TO")).SendPacket(m)
                ElseIf m.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.Administrator Then
                    d.Query("UPDATE Users SET security='ADMIN' WHERE username='" & m.StringCollection("TO").Replace("'", "") & "'")
                    Globals.ServerEngine.Connections(m.StringCollection("TO")).Security = LobbyShared.User.SecurityGroups.Administrator
                    Globals.ServerEngine.Connections(m.StringCollection("TO")).SendPacket(m)
                End If

                Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.ChangeStatus)
                p.StringCollection("FROM") = m.StringCollection("TO")
                p.StringCollection("ICON") = Globals.ServerEngine.Connections(m.StringCollection("TO")).GetStatus()
                p.StringCollection("GROUP") = Globals.ServerEngine.Connections(m.StringCollection("TO")).GetGroup()
                RaiseEvent BroadCast(p)
            End If
        End SyncLock
    End Sub
#End Region
#Region "Games, Locate"
    Private Sub Process_LocateUser(ByVal m As LobbyShared.NetworkMessage)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LocateUser)
        For Each r As String In m.StringCollection.Keys
            SyncLock Globals.ServerEngine.Connections_Lock
                If Globals.ServerEngine.Connections.ContainsKey(r) Then
                    p.StringCollection(r) = Globals.ServerEngine.Connections(r).LocateString
                End If
            End SyncLock
        Next
        SendPacket(p)
    End Sub
    Private Sub Process_GameJoin(ByVal m As LobbyShared.NetworkMessage)
        ' Strings in collection that were passed
        ' FROM = Globals.CurrentUser.Username
        ' HOSTIP = hostip
        ' EEUSER = eeuser

        Me.LocateString = m.StringCollection("FROM") & " was last seen joining "

        SyncLock Globals.ServerEngine.CurrentGames_Lock
            If Globals.ServerEngine.CurrentGames.ContainsKey(m.StringCollection("HOSTIP")) Then
                Me.LocateString &= Globals.ServerEngine.CurrentGames(m.StringCollection("HOSTIP")).GetData(0, "HOST") & "'s game (" & m.StringCollection("HOSTIP") & ") @ " & Date.UtcNow.ToString & " UTC"
            Else
                Me.LocateString &= m.StringCollection("HOSTIP")
            End If
        End SyncLock

        Me.LocateString &= " using the name " & m.StringCollection("EEUSER")
        Globals.WriteLog("logs\locate", Me.LocateString)
    End Sub
    Private Sub Process_GameJoin2(ByVal m As LobbyShared.NetworkMessage)
        ' Strings in collection that were passed
        ' FROM = the host of the game
        ' HOSTIP = obvious
        ' EEUSER = name used by anon that is joining
        ' ANONIP = obvious

        ' See if our anon has a session
        Dim found As Boolean = False
        SyncLock Globals.ServerEngine.Connections_Lock
            For Each s As Session In Globals.ServerEngine.Connections.Values
                ' Is IP a match?
                If m.StringCollection("ANONIP") = s.PublicIP Then
                    s.LocateString = s.Username & " was last seen joining " & m.StringCollection("FROM") & "'s game (" & m.StringCollection("HOSTIP") & ") @ " & Date.UtcNow.ToString & " UTC using the name " & m.StringCollection("EEUSER")
                    Globals.WriteLog("logs\locate", s.LocateString)
                    found = True
                End If
            Next
        End SyncLock

        ' If we didn't find him then log the IP
        If Not found Then
            Dim anonString As String = "A user not logged into the lobby (" & m.StringCollection("ANONIP") & ") joined " & m.StringCollection("FROM") & "'s game (" & m.StringCollection("HOSTIP") & ") @ " & Date.UtcNow.ToString & " UTC using the name " & m.StringCollection("EEUSER")
            Globals.WriteLog("logs\locate", anonString)
        End If
    End Sub
    Private Sub Process_GameAdd(ByVal m As LobbyShared.NetworkMessage)
        SyncLock Globals.ServerEngine.CurrentGames_Lock
            Globals.ServerEngine.CurrentGames(m.TableCollection("GAMEDATA").GetData(0, "IP")) = m.TableCollection("GAMEDATA")
        End SyncLock
        Me.LocateString = Me.Username & " was last seen hosting " & m.TableCollection("GAMEDATA").GetData(0, "GAMENAME") & " (" & m.TableCollection("GAMEDATA").GetData(0, "IP") & ") @ " & Date.UtcNow.ToString & " UTC using the name " & m.TableCollection("GAMEDATA").GetData(0, "HOSTEENAME")
        Globals.WriteLog("logs\locate", Me.LocateString)
        RaiseEvent BroadCast(m)
    End Sub
    Private Sub Process_GameRemove(ByVal m As LobbyShared.NetworkMessage)
        SyncLock Globals.ServerEngine.CurrentGames_Lock
            If Globals.ServerEngine.CurrentGames.ContainsKey(m.StringCollection("IP")) Then
                Globals.ServerEngine.CurrentGames.Remove(m.StringCollection("IP"))
            End If
        End SyncLock
        ' LocateString = UserName & " has not been seen in a game."
        RaiseEvent BroadCast(m)
    End Sub
#End Region
#Region "GetKeyList"
    Private Sub Process_GetKeyList(ByVal m As LobbyShared.NetworkMessage)
        If Me.Security >= LobbyShared.User.SecurityGroups.Moderator Then
            Try
                ' Declare packet
                Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.GetKeyList)
                ' Set action type
                p.StringCollection("ACTION") = m.StringCollection("ACTION")

                If m.StringCollection("ACTION") = "BAN" OrElse m.StringCollection("ACTION") = "MUTE" Then
                    ' For each user (i.e. manual ban can have more than 1)
                    For Each r As String In m.StringCollection.Keys
                        If r.StartsWith("RCPT_") Then
                            Dim user As String = Mid(r, 6).Replace("'", "")
                            SyncLock Globals.ServerEngine.Connections_Lock
                                ' User is online, get HW from their session
                                If Globals.ServerEngine.Connections.ContainsKey(user) Then
                                    Dim us As Session = Globals.ServerEngine.Connections(user)
                                    SyncLock us.HardwareList_Lock
                                        For Each hwid As String In us.HardwareList.Keys
                                            If p.StringCollection.ContainsValue(us.HardwareList(hwid)) = False Then
                                                p.StringCollection(hwid) = us.HardwareList(hwid)
                                            End If
                                        Next
                                    End SyncLock
                                    ' User is not online, get HW from DB
                                Else
                                    Dim db As New Databases.AccessDatabase(Globals.dbcon)
                                    db.Query("SELECT * FROM Users WHERE username='" & user & "'")
                                    While db.IsLastRecord = False
                                        Dim hwcol() As String = Split(db.RecordData("hardwareid"), vbCrLf)
                                        Try
                                            For Each hw As String In hwcol
                                                If Trim(hw) <> "" Then
                                                    If p.StringCollection.ContainsValue(Split(hw, "[:=:]")(1)) = False Then
                                                        p.StringCollection.Add(Split(hw, "[:=:]")(0), Split(hw, "[:=:]")(1))
                                                    End If
                                                End If
                                            Next
                                        Catch ex As Exception
                                        End Try
                                        db.MoveNext()
                                    End While
                                End If
                            End SyncLock
                        End If
                    Next
                    ' Send HW
                    SendPacket(p)
                ElseIf m.StringCollection("ACTION") = "UNBAN" Then
                    ' Getting the current list
                    If m.StringCollection.ContainsKey("RCPT_ALL") Then
                        SyncLock Globals.ServerEngine.BannedHardware_Lock
                            ' Just send the whole damn hardware dictionary
                            ' Easy enough.
                            p.ListCollection = Globals.ServerEngine.BannedHardware
                        End SyncLock
                        SendPacket(p)
                    End If
                ElseIf m.StringCollection("ACTION") = "UNMUTE" Then
                    ' Get current list
                    If m.StringCollection.ContainsKey("RCPT_ALL") Then
                        SyncLock Globals.ServerEngine.MutedHardware_Lock
                            ' Just send the whole damn hardware dictionary
                            ' Easy enough.
                            p.ListCollection = Globals.ServerEngine.MutedHardware
                        End SyncLock
                        SendPacket(p)
                    End If
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub
#End Region
#Region "Kick, Mute, Ban"
    Private Sub Process_KickPlayer(ByVal m As LobbyShared.NetworkMessage)
        If Me.Security >= LobbyShared.User.SecurityGroups.Moderator Then
            Dim rcpt As New List(Of String)
            For Each r As String In m.StringCollection.Keys
                If Mid(r, 1, 5) = "RCPT_" Then
                    rcpt.Add(Mid(r, 6))
                End If
            Next
            SyncLock Globals.ServerEngine.Connections_Lock
                For Each r As String In rcpt
                    If Globals.ServerEngine.Connections.ContainsKey(r) Then
                        ' Send clients a MsgBox with who kicked them and why
                        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.PunishmentMessage)
                        p.StringCollection("FROM") = m.StringCollection("FROM")
                        p.StringCollection("REASON") = m.StringCollection("REASON")
                        p.StringCollection("ACTION") = "KICK"
                        Globals.ServerEngine.Connections(r).SendPacket(p)
                        ' Close their connection
                        Globals.ServerEngine.Connections(r).Close()
                    End If
                Next
            End SyncLock
        End If
    End Sub
    Private Sub Process_MutePlayer(ByVal m As LobbyShared.NetworkMessage)
        If Me.Security >= LobbyShared.User.SecurityGroups.Moderator Then
            ' Declare list of players to send mute message to
            Dim rcpt As New List(Of String)

            ' Set message data
            Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.PunishmentMessage)
            p.StringCollection("FROM") = m.StringCollection("FROM")
            p.StringCollection("REASON") = m.StringCollection("REASON")
            p.StringCollection("UNTIL") = m.StringCollection("UNTIL")
            p.StringCollection("ACTION") = "MUTE"

            ' Go thru keys
            SyncLock Globals.ServerEngine.MutedHardware_Lock
                For Each k As String In m.ListCollection.Keys
                    ' Check to see if value is already muted
                    Dim valueMuted As Boolean = False
                    For Each hwkey As String In Globals.ServerEngine.MutedHardware.Keys
                        If m.ListCollection(k)(0) = Globals.ServerEngine.MutedHardware(hwkey)(0) Then
                            valueMuted = True
                            Exit For
                        End If
                    Next
                    ' If it's not...
                    If Not valueMuted Then
                        ' Using substrings, get username from the key
                        Dim ss As String = k.Substring(0, k.LastIndexOf("_"))
                        ss = ss.Substring(0, ss.LastIndexOf("_"))
                        ' Add username to recipient list if not already on it
                        If rcpt.Contains(ss) = False Then
                            rcpt.Add(ss)
                        End If

                        ' Add to MutedHardware and to DB
                        Globals.ServerEngine.MutedHardware(k) = m.ListCollection(k)
                        Dim d As New Databases.AccessDatabase(Globals.dbcon)
                        d.Query("INSERT INTO MutedHardware (hwkey, hwvalue, addedby, starttime, endtime, reason, comments) VALUES ('" _
                                & k.Replace("'", "") & "', '" & m.ListCollection(k)(0).Replace("'", "") & "', '" & m.ListCollection(k)(1).Replace("'", "") & "', '" & m.ListCollection(k)(2).Replace("'", "") & "', '" & m.ListCollection(k)(3).Replace("'", "") & "', '" & m.ListCollection(k)(4).Replace("'", "") & "', '" & m.ListCollection(k)(5).Replace("'", "") & "')")
                    End If
                Next
            End SyncLock
            ' Send message to muted players
            SyncLock Globals.ServerEngine.Connections_Lock
                For Each r As String In rcpt
                    If Globals.ServerEngine.Connections.ContainsKey(r) Then
                        Globals.ServerEngine.Connections(r).SendPacket(p)
                    End If
                Next
            End SyncLock
        End If
    End Sub
    Private Sub Process_UnmutePlayer(ByVal m As LobbyShared.NetworkMessage)
        If Me.Security >= LobbyShared.User.SecurityGroups.Moderator Then
            SyncLock Globals.ServerEngine.MutedHardware_Lock
                For Each k As String In m.StringCollection.Keys
                    If Globals.ServerEngine.MutedHardware.ContainsKey(k) Then
                        Globals.ServerEngine.MutedHardware.Remove(k)
                        Dim d As New Databases.AccessDatabase(Globals.dbcon)
                        d.Query("DELETE FROM MutedHardware WHERE hwkey='" & k.Replace("'", "") & "'")
                    End If
                Next
            End SyncLock
        End If
    End Sub
    Private Sub Process_SaveMute(ByVal m As LobbyShared.NetworkMessage)
        If Me.Security >= LobbyShared.User.SecurityGroups.Moderator Then
            ' Go thru keys
            SyncLock Globals.ServerEngine.MutedHardware_Lock
                For Each k As String In m.ListCollection.Keys
                    If Globals.ServerEngine.MutedHardware.ContainsKey(k) Then
                        ' Update MutedHardware and DB
                        Globals.ServerEngine.MutedHardware(k) = m.ListCollection(k)
                        Dim d As New Databases.AccessDatabase(Globals.dbcon)
                        d.Query("UPDATE MutedHardware SET endtime='" & m.ListCollection(k)(3).Replace("'", "") & "', reason='" & m.ListCollection(k)(4).Replace("'", "") & "', comments='" & m.ListCollection(k)(5).Replace("'", "") & "' WHERE hwkey='" & k.Replace("'", "") & "'")
                    End If
                Next
            End SyncLock
        End If
    End Sub
    Private Sub Process_BanPlayer(ByVal m As LobbyShared.NetworkMessage)
        If Me.Security >= LobbyShared.User.SecurityGroups.SuperModerator Then
            ' Declare list of players to send mute message to
            Dim rcpt As New List(Of String)

            ' Set message data
            Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.PunishmentMessage)
            p.StringCollection("FROM") = m.StringCollection("FROM")
            p.StringCollection("REASON") = m.StringCollection("REASON")
            p.StringCollection("UNTIL") = m.StringCollection("UNTIL")
            p.StringCollection("ACTION") = "BAN"

            ' Go thru keys
            SyncLock Globals.ServerEngine.BannedHardware_Lock
                For Each k As String In m.ListCollection.Keys
                    ' Check to see if value is already banned
                    Dim valueBanned As Boolean = False
                    For Each hwkey As String In Globals.ServerEngine.BannedHardware.Keys
                        If m.ListCollection(k)(0) = Globals.ServerEngine.BannedHardware(hwkey)(0) Then
                            valueBanned = True
                            Exit For
                        End If
                    Next
                    ' If it's not...
                    If Not valueBanned Then
                        ' Using substrings, get username from the key
                        Dim ss As String = k.Substring(0, k.LastIndexOf("_"))
                        ss = ss.Substring(0, ss.LastIndexOf("_"))
                        ' Add username to recipient list if not already on it
                        If rcpt.Contains(ss) = False Then
                            rcpt.Add(ss)
                        End If

                        ' Add to BannedHardware and to DB
                        Globals.ServerEngine.BannedHardware(k) = m.ListCollection(k)
                        Dim d As New Databases.AccessDatabase(Globals.dbcon)
                        d.Query("INSERT INTO BannedHardware (hwkey, hwvalue, addedby, starttime, endtime, reason, comments) VALUES ('" _
                                & k.Replace("'", "") & "', '" & m.ListCollection(k)(0).Replace("'", "") & "', '" & m.ListCollection(k)(1).Replace("'", "") & "', '" & m.ListCollection(k)(2).Replace("'", "") & "', '" & m.ListCollection(k)(3).Replace("'", "") & "', '" & m.ListCollection(k)(4).Replace("'", "") & "', '" & m.ListCollection(k)(5).Replace("'", "") & "')")
                    End If
                Next
            End SyncLock
            ' Send message to banned players and close connection
            SyncLock Globals.ServerEngine.Connections_Lock
                For Each r As String In rcpt
                    If Globals.ServerEngine.Connections.ContainsKey(r) Then
                        Globals.ServerEngine.Connections(r).SendPacket(p)
                    End If
                Next
            End SyncLock
        End If
    End Sub
    Private Sub Process_UnbanPlayer(ByVal m As LobbyShared.NetworkMessage)
        If Me.Security >= LobbyShared.User.SecurityGroups.SuperModerator Then
            SyncLock Globals.ServerEngine.BannedHardware_Lock
                For Each k As String In m.StringCollection.Keys
                    If Globals.ServerEngine.BannedHardware.ContainsKey(k) Then
                        Globals.ServerEngine.BannedHardware.Remove(k)
                        Dim d As New Databases.AccessDatabase(Globals.dbcon)
                        d.Query("DELETE FROM BannedHardware WHERE hwkey ='" & k.Replace("'", "") & "'")
                    End If
                Next
            End SyncLock
        End If
    End Sub
    Private Sub Process_SaveBan(ByVal m As LobbyShared.NetworkMessage)
        If Me.Security >= LobbyShared.User.SecurityGroups.SuperModerator Then
            ' Go thru keys
            SyncLock Globals.ServerEngine.BannedHardware_Lock
                For Each k As String In m.ListCollection.Keys
                    If Globals.ServerEngine.BannedHardware.ContainsKey(k) Then
                        ' Update BannedHardware and DB
                        Globals.ServerEngine.BannedHardware(k) = m.ListCollection(k)
                        Dim d As New Databases.AccessDatabase(Globals.dbcon)
                        d.Query("UPDATE BannedHardware SET endtime='" & m.ListCollection(k)(3).Replace("'", "") & "', reason='" & m.ListCollection(k)(4).Replace("'", "") & "', comments='" & m.ListCollection(k)(5).Replace("'", "") & "' WHERE hwkey='" & k.Replace("'", "") & "'")
                    End If
                Next
            End SyncLock
        End If
    End Sub
#End Region
#Region "Misc, Vote, etc"
    Private Sub Process_MiscMsg(ByVal m As LobbyShared.NetworkMessage)
        If Me.Security >= LobbyShared.User.SecurityGroups.Moderator OrElse m.StringCollection.ContainsKey("MISCMSGRETURN") Then
            m.StringCollection("FROM") = Username
            Dim rcpt As New List(Of String)
            For Each r As String In m.StringCollection.Keys
                If Mid(r, 1, 5) = "RCPT_" Then
                    rcpt.Add(Mid(r, 6))
                End If
            Next
            For Each r As String In rcpt
                SyncLock Globals.ServerEngine.Connections_Lock
                    If Globals.ServerEngine.Connections.ContainsKey(r) Then
                        Globals.ServerEngine.Connections(r).SendPacket(m)
                    End If
                End SyncLock
            Next
        End If

        If m.StringCollection.ContainsKey("VOTEQUESTION") Then
            ' Look for Username, IP, or SID
            Dim d As New Databases.AccessDatabase(Globals.dbcon)
            d.Query("SELECT * FROM Vote WHERE username='" & m.StringCollection("FROM").Replace("'", "") & "' OR ip='" & m.StringCollection("IP").Replace("'", "") & "' OR sid='" & m.StringCollection("SID").Replace("'", "") & "'")
            If d.RecordCount = 0 Then
                ' If none are in there then send back
                SendPacketSync(m)
            End If
        ElseIf m.StringCollection.ContainsKey("VOTEANSWERS") Then
            If Not m.StringCollection.ContainsKey("Q2") Then
                m.StringCollection("Q2") = ""
            End If
            If Not m.StringCollection.ContainsKey("Q3") Then
                m.StringCollection("Q3") = ""
            End If
            If Not m.StringCollection.ContainsKey("Q4") Then
                m.StringCollection("Q4") = ""
            End If
            If Not m.StringCollection.ContainsKey("Q5") Then
                m.StringCollection("Q5") = ""
            End If
            If Not m.StringCollection.ContainsKey("Q6") Then
                m.StringCollection("Q6") = ""
            End If
            ' Add to DB
            Dim d As New Databases.AccessDatabase(Globals.dbcon)
            d.Query("INSERT INTO Vote (username, ip, sid, q1, q2, q3, q4, q5, q6, submittedon) VALUES ('" & _
                    m.StringCollection("FROM").Replace("'", "") & "', '" & _
                    m.StringCollection("IP").Replace("'", "") & "', '" & _
                    m.StringCollection("SID").Replace("'", "") & "', '" & _
                    m.StringCollection("Q1").Replace("'", "") & "', '" & _
                    m.StringCollection("Q2").Replace("'", "") & "', '" & _
                    m.StringCollection("Q3").Replace("'", "") & "', '" & _
                    m.StringCollection("Q4").Replace("'", "") & "', '" & _
                    m.StringCollection("Q5").Replace("'", "") & "', '" & _
                    m.StringCollection("Q6").Replace("'", "") & "', '" & _
                    Date.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss") & "')")
        End If
    End Sub
#End Region
#Region "Chat, Whispers, Warnings, etc."
    Private Sub Process_LobbyChat(ByVal m As LobbyShared.NetworkMessage)
        Dim Muted As Boolean = False
        SyncLock Globals.ServerEngine.MutedHardware_Lock
            For Each hwid As String In HardwareList.Values
                For Each k As String In Globals.ServerEngine.MutedHardware.Keys
                    If Globals.ServerEngine.MutedHardware(k)(0) = hwid Then
                        Muted = True
                        Exit For
                    End If
                Next
            Next
        End SyncLock
        If Muted AndAlso (Me.Security < LobbyShared.User.SecurityGroups.Moderator) Then Exit Sub
        m.StringCollection("FROM") = Me.Username
        If Me.Security >= LobbyShared.User.SecurityGroups.Moderator Then
            m.StringCollection("STAFF") = "true"
        ElseIf Me.Security = LobbyShared.User.SecurityGroups.Donator Then
            If m.StringCollection("TEXT").Length > 1000 Then m.StringCollection("TEXT") = Mid(m.StringCollection("TEXT"), 1, 1000) & "..."
        Else
            m.StringCollection("TEXT") = Replace(m.StringCollection("TEXT"), "<", "&lt;")
            If m.StringCollection("TEXT").Length > 250 Then m.StringCollection("TEXT") = Mid(m.StringCollection("TEXT"), 1, 250) & "..."
        End If
        m.StringCollection("TEXT") = Replace(m.StringCollection("TEXT"), "&lt;br>", "<br>", , , CompareMethod.Text)
        Globals.WriteLog("logs\chat", Me.Username & ": " & m.StringCollection("TEXT"))
        RaiseEvent BroadCast(m)
    End Sub
    Private Sub Process_LobbyWhisper(ByVal m As LobbyShared.NetworkMessage)
        ' Check to see if user has hardware in mute list
        Dim Muted As Boolean = False
        SyncLock Globals.ServerEngine.MutedHardware_Lock
            For Each hwid As String In HardwareList.Values
                For Each k As String In Globals.ServerEngine.MutedHardware.Keys
                    If Globals.ServerEngine.MutedHardware(k)(0) = hwid Then
                        Muted = True
                        Exit For
                    End If
                Next
            Next
        End SyncLock
        ' Exit if muted
        If Muted AndAlso (Me.Security < LobbyShared.User.SecurityGroups.Donator) Then Exit Sub

        m.StringCollection("FROM") = Me.Username

        ' Add recipients
        Dim rcpt As New List(Of String)
        For Each r As String In m.StringCollection.Keys
            If Mid(r, 1, 5) = "RCPT_" Then
                rcpt.Add(Mid(r, 6))
            End If
        Next

        ' Parse text (i.e. get rid of HTML)
        If Me.Security >= LobbyShared.User.SecurityGroups.Donator Then
        Else
            m.StringCollection("TEXT") = Replace(m.StringCollection("TEXT"), "<", "&lt;")
        End If
        m.StringCollection("TEXT") = Replace(m.StringCollection("TEXT"), "&lt;br>", "<br>", , , CompareMethod.Text)

        ' fix blank TEXT
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LobbyWhisper)
        p.StringCollection("FROM") = m.StringCollection("FROM")
        Try
            p.StringCollection("TEXT") = m.StringCollection("TEXT")
        Catch ex As Exception
            Exit Sub
        End Try

        ' Send whisper
        For Each r As String In rcpt
            SyncLock Globals.ServerEngine.Connections_Lock
                If Globals.ServerEngine.Connections.ContainsKey(r) Then
                    If Me.Security >= LobbyShared.User.SecurityGroups.Moderator Then
                        Globals.ServerEngine.Connections(r).SendPacket(p)
                        Globals.WriteLog("logs\chat", Me.Username & " whispered " & r & ".")
                    Else
                        SyncLock Globals.ServerEngine.Connections(r).IgnoreList_Lock
                            If Not Globals.ServerEngine.Connections(r).IgnoreList.Contains(m.StringCollection("FROM")) Then
                                Globals.ServerEngine.Connections(r).SendPacket(p)
                                Globals.WriteLog("logs\chat", Me.Username & " whispered " & r & ".")
                            Else
                                Globals.WriteLog("logs\chat", Me.Username & " whispered " & r & ". <IGNORED>")
                            End If
                        End SyncLock
                    End If
                End If
            End SyncLock
        Next
    End Sub
    Private Sub Process_LobbyWarn(ByVal m As LobbyShared.NetworkMessage)
        m.StringCollection("FROM") = Me.Username
        If Me.Security >= LobbyShared.User.SecurityGroups.Moderator Then
            ' Add recipients
            Dim rcpt As New List(Of String)
            For Each r As String In m.StringCollection.Keys
                If Mid(r, 1, 5) = "RCPT_" Then
                    rcpt.Add(Mid(r, 6))
                End If
            Next

            ' fix blank TEXT
            Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.LobbyWarn)
            p.StringCollection("FROM") = m.StringCollection("FROM")
            Try
                p.StringCollection("TEXT") = m.StringCollection("TEXT")
            Catch ex As Exception
                Exit Sub
            End Try

            ' Send warning
            For Each r As String In rcpt
                SyncLock Globals.ServerEngine.Connections_Lock
                    If Globals.ServerEngine.Connections.ContainsKey(r) Then
                        Globals.ServerEngine.Connections(r).SendPacket(p)
                        Globals.WriteLog("logs\chat", Me.Username & " warned " & r & ".")
                    End If
                End SyncLock
            Next
        End If
    End Sub
    Private Sub Process_LobbyAlert(ByVal m As LobbyShared.NetworkMessage)
        m.StringCollection("FROM") = Me.Username
        If Me.Security >= LobbyShared.User.SecurityGroups.SuperModerator Then
            Globals.WriteLog("logs\chat", Me.Username & "alerted: " & m.StringCollection("TEXT"))
            RaiseEvent BroadCast(m)
        End If
    End Sub
#End Region
#Region "Friends, Cheaters, Ignore Add/Remove"
    Private Sub Process_FriendAdd(ByVal m As LobbyShared.NetworkMessage)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.FriendAdd)
        Dim d As New Databases.AccessDatabase(Globals.dbcon)

        For Each u As String In m.StringCollection.Keys
            d.Query("SELECT * FROM Users WHERE username='" & u.Replace("'", "") & "'")
            If d.RecordCount > 0 Then
                d.Query("SELECT * FROM FriendsList WHERE owner='" & Username & "' AND friend='" & u.Replace("'", "") & "'")
                If d.RecordCount > 0 Then
                    p.StringCollection(u) = "ALREADY"
                Else
                    d.Query("INSERT INTO FriendsList (owner, friend) VALUES ('" & Username & "','" & u.Replace("'", "") & "')")
                    p.StringCollection(u) = "ADDED"
                End If
            Else
                p.StringCollection(u) = "NOT_FOUND"
            End If
        Next
        SendPacket(p)
    End Sub
    Private Sub Process_FriendRemove(ByVal m As LobbyShared.NetworkMessage)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.FriendRemove)
        Dim d As New Databases.AccessDatabase(Globals.dbcon)

        For Each u As String In m.StringCollection.Keys
            d.Query("SELECT * FROM FriendsList WHERE owner='" & Username & "' AND friend='" & u.Replace("'", "") & "'")
            If d.RecordCount > 0 Then
                p.StringCollection(u) = "REMOVED"
                d.Query("DELETE FROM FriendsList WHERE owner='" & Username & "' AND friend='" & u.Replace("'", "") & "'")
            Else
                p.StringCollection(u) = "NOT_FOUND"
            End If
        Next
        SendPacket(p)
    End Sub
    Private Sub Process_CheaterAdd(ByVal m As LobbyShared.NetworkMessage)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.CheaterAdd)
        Dim d As New Databases.AccessDatabase(Globals.dbcon)

        For Each u As String In m.StringCollection.Keys
            d.Query("SELECT * FROM Users WHERE username='" & u.Replace("'", "") & "'")
            If d.RecordCount > 0 Then
                d.Query("SELECT * FROM CheatersList WHERE owner='" & Username & "' AND cheater='" & u.Replace("'", "") & "'")
                If d.RecordCount > 0 Then
                    p.StringCollection(u) = "ALREADY"
                Else
                    d.Query("INSERT INTO CheatersList (owner, cheater) VALUES ('" & Username & "','" & u.Replace("'", "") & "')")
                    p.StringCollection(u) = "ADDED"
                End If
            Else
                p.StringCollection(u) = "NOT_FOUND"
            End If
        Next
        SendPacket(p)
    End Sub
    Private Sub Process_CheaterRemove(ByVal m As LobbyShared.NetworkMessage)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.CheaterRemove)
        Dim d As New Databases.AccessDatabase(Globals.dbcon)

        For Each u As String In m.StringCollection.Keys
            d.Query("SELECT * FROM CheatersList WHERE owner='" & Username & "' AND cheater='" & u.Replace("'", "") & "'")
            If d.RecordCount > 0 Then
                p.StringCollection(u) = "REMOVED"
                d.Query("DELETE FROM CheatersList WHERE owner='" & Username & "' AND cheater='" & u.Replace("'", "") & "'")
            Else
                p.StringCollection(u) = "NOT_FOUND"
            End If
        Next
        SendPacket(p)
    End Sub
    Private Sub Process_IgnoreAdd(ByVal m As LobbyShared.NetworkMessage)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.IgnoreAdd)
        Dim d As New Databases.AccessDatabase(Globals.dbcon)

        For Each u As String In m.StringCollection.Keys
            d.Query("SELECT * FROM Users WHERE username='" & u.Replace("'", "") & "'")
            If d.RecordCount > 0 Then
                SyncLock IgnoreList_Lock
                    If IgnoreList.Contains(u) = False Then
                        IgnoreList.Add(u)
                    End If
                End SyncLock
                d.Query("SELECT * FROM IgnoreList WHERE owner='" & Username & "' AND ignore='" & u.Replace("'", "") & "'")
                If d.RecordCount > 0 Then
                    p.StringCollection(u) = "ALREADY"
                Else
                    d.Query("INSERT INTO IgnoreList (owner, ignore) VALUES ('" & Username & "', '" & u.Replace("'", "") & "')")
                    p.StringCollection(u) = "ADDED"
                End If
            Else
                p.StringCollection(u) = "NOT_FOUND"
            End If
        Next
        SendPacket(p)
    End Sub
    Private Sub Process_IgnoreRemove(ByVal m As LobbyShared.NetworkMessage)
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.IgnoreRemove)
        Dim d As New Databases.AccessDatabase(Globals.dbcon)

        For Each u As String In m.StringCollection.Keys
            SyncLock IgnoreList_Lock
                If IgnoreList.Contains(u) Then
                    IgnoreList.Remove(u)
                End If
            End SyncLock

            d.Query("SELECT * FROM IgnoreList WHERE owner='" & Username & "' AND ignore='" & u.Replace("'", "") & "'")
            If d.RecordCount > 0 Then
                p.StringCollection(u) = "REMOVED"
                d.Query("DELETE FROM IgnoreList WHERE owner='" & Username & "' AND ignore='" & u.Replace("'", "") & "'")
            Else
                p.StringCollection(u) = "NOT_FOUND"
            End If
        Next
        SendPacket(p)
    End Sub
#End Region
End Class