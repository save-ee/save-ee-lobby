Public Class Server
    Public Port As Integer
    Public Adapter As String
    Public MaxConnections As Integer = 500
    Public CurrentConnection As Integer = 0

    Private WithEvents ListeningSocket As New Listener

    Public Connections As New Dictionary(Of String, Session)
    Public Connections_Lock As New Object

    Public BannedHardware As New Dictionary(Of String, List(Of String))
    Public BannedHardware_Lock As New Object

    Public MutedHardware As New Dictionary(Of String, List(Of String))
    Public MutedHardware_Lock As New Object

    Public CurrentGames As New Dictionary(Of String, LobbyShared.NetworkMessage.MsgTable)
    Public CurrentGames_Lock As New Object

    Public NumOfRecentRegisters As Integer = 0
    Public NumOfRecentRegisters_Lock As New Object

    Public Sub [Start]()
        ListeningSocket.Port = Port
        ListeningSocket.Adapter = Adapter
        ListeningSocket.Start()
    End Sub
    Public Sub [Stop]()
        ListeningSocket.Stop()
        SyncLock Globals.ServerEngine.Connections_Lock
            For Each s As Session In Connections.Values
                s.Close()
            Next
        End SyncLock
    End Sub

    Private Sub ListeningSocket_ConnectionRequest(ByVal s As System.Net.Sockets.Socket) Handles ListeningSocket.ConnectionRequest
        SyncLock Globals.ServerEngine.Connections_Lock
            If CurrentConnection >= MaxConnections Then
                OnDenyConnection(s)
            Else
                Dim userIp As String = Net.IPAddress.Parse(DirectCast(s.RemoteEndPoint, Net.IPEndPoint).Address.ToString).ToString
                Dim isBanned As Boolean = False
                SyncLock Globals.ServerEngine.BannedHardware_Lock
                    For Each k As String In Globals.ServerEngine.BannedHardware.Keys
                        Dim bannedIp As String = Globals.ServerEngine.BannedHardware(k)(0)
                        If Globals.ServerEngine.BannedHardware(k)(0) = userIp Then
                            isBanned = True
                            Exit For
                        End If
                        Try
                            If bannedIp.EndsWith("*") Then
                                Dim bannedIpSub1 As String = bannedIp.Substring(0, bannedIp.Length - 2)
                                If userIp.StartsWith(bannedIpSub1) Then
                                    isBanned = True
                                    Exit For
                                End If
                                If bannedIpSub1.EndsWith("*") Then
                                    Dim bannedIpSub2 As String = bannedIpSub1.Substring(0, bannedIpSub1.Length - 2)
                                    If userIp.StartsWith(bannedIpSub2) Then
                                        isBanned = True
                                        Exit For
                                    End If
                                End If
                            End If
                        Catch
                        End Try
                    Next
                End SyncLock
                SyncLock Globals.ServerEngine.Connections_Lock
                    Dim connectionsForIp As Integer = 0
                    For Each sess As Session In Connections.Values
                        If sess.PublicIP = userIp Then
                            connectionsForIp = connectionsForIp + 1
                            If connectionsForIp > 20 Then
                                isBanned = True
                                Exit For
                            End If
                        End If
                    Next
                End SyncLock
                If isBanned Then
                    OnDenyConnection(s)
                Else
                    OnAcceptConnection(s, userIp)
                End If
            End If
        End SyncLock
    End Sub

    Private Sub OnAcceptConnection(ByVal s As Net.Sockets.Socket, ByVal ip As String)
        Dim sess As New Session(s, ip)
        Try
            AddHandler sess.ConnectionLost, AddressOf OnDropConnection
            AddHandler sess.BroadCast, AddressOf OnBroadCast
            SyncLock Globals.ServerEngine.Connections_Lock
                Connections(sess.Guid) = sess
                CurrentConnection = Connections.Count
            End SyncLock

            Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.NetworkInit)
            p.StringCollection("K1") = System.Guid.NewGuid.ToString
            p.StringCollection("K2") = System.Text.Encoding.ASCII.GetString(sess.NetworkKey)
            p.StringCollection("K3") = System.Guid.NewGuid.ToString
            Dim tp As String = p.SerializeForTransport(LobbyShared.Globals.ServerSalt)
            sess.Send(tp & vbCrLf)
        Catch ex As Exception
            SyncLock Globals.ServerEngine.Connections_Lock
                If Connections.ContainsKey(sess.Guid) = True Then
                    Connections.Remove(sess.Guid)
                    CurrentConnection = Connections.Count
                End If
            End SyncLock
        End Try
    End Sub
    Private Sub OnDenyConnection(ByVal s As Net.Sockets.Socket)
        Try
            s.Shutdown(Net.Sockets.SocketShutdown.Both)
        Catch ex As Exception
        End Try
        Try
            s.Close()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub OnDropConnection(ByVal s As Session)
        RemoveHandler s.ConnectionLost, AddressOf OnDropConnection
        SyncLock Globals.ServerEngine.Connections_Lock
            Try
                s.Close()
            Catch ex As Exception
            End Try
            Try
                Connections.Remove(s.Guid)
                CurrentConnection = Connections.Count
            Catch ex As Exception
            End Try
        End SyncLock
        Dim WasHosting As Boolean = False
        SyncLock Globals.ServerEngine.CurrentGames_Lock
            If Globals.ServerEngine.CurrentGames.ContainsKey(s.PublicIP) = True Then
                WasHosting = True
                Globals.ServerEngine.CurrentGames.Remove(s.PublicIP)
            End If
        End SyncLock
        If s.IsAuth = True Then
            Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.UserRemove)
            p.StringCollection("user") = s.Guid
            OnBroadCast(p)
            Globals.WriteLog("logs\locate", s.Username & " has left the server.")
        End If
        If WasHosting = True Then
            Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.GameRemove)
            p.StringCollection("IP") = s.PublicIP
            OnBroadCast(p)
        End If
    End Sub
    Private Sub OnBroadCast(ByVal m As LobbyShared.NetworkMessage)
        SyncLock Globals.ServerEngine.Connections_Lock
            For Each ses As Session In Connections.Values
                If ses.IsAuth = True Then
                    If m.MessageType = LobbyShared.NetworkMessage.MsgTypes.LobbyChat Then
                        SyncLock ses.IgnoreList_Lock
                            If ses.IgnoreList.Contains(m.StringCollection("FROM")) = False Or m.StringCollection.ContainsKey("STAFF") Then
                                ses.SendPacket(m)
                            End If
                        End SyncLock
                    Else
                        ses.SendPacket(m)
                    End If
                End If
            Next
        End SyncLock
    End Sub
End Class