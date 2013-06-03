Public Class NetworkMessage
    Public Enum MsgTypes
        NetworkInit

        VerifyKey
        VerifyKeyPass
        VerifyKeyFail

        LoginAttempt
        LoginAttemptPass
        LoginAttemptAlreadyLoggedIn
        LoginAttemptFail
        LoginLocked
        LoginDoesntExist
        LoginAccountExist
        LoginCreateUser

        UserAdd
        UserRemove

        FriendList
        FriendAdd
        FriendRemove

        CheaterList
        CheaterAdd
        CheaterRemove

        IgnoreList
        IgnoreAdd
        IgnoreRemove

        LobbyChat
        LobbyWhisper
        LobbyWarn
        LobbyAlert

        KickPlayer

        MutePlayer
        UnmutePlayer
        SaveMute

        BanPlayer
        UnbanPlayer
        SaveBan

        GetKeyList

        ChangeUserRights
        ChangeStatus

        GameAdd
        GameRemove

        None

        PunishmentMessage
        SendClientMessage

        MiscMsg
        GameJoin
        GameJoin2
        GameLeave
        LocateUser
    End Enum

    Public Class MsgTable
        Public Data As New List(Of String())
        Public ColumnLookup As New List(Of String)

        Public Sub AddRow(ByVal rowArray As String())
            Try
                If ColumnLookup.Count = 0 Then
                    For Each c As String In rowArray
                        ColumnLookup.Add(c)
                    Next
                Else
                    Data.Add(rowArray)
                End If
            Catch ex As Exception
            End Try
        End Sub
        Public Sub DeleteRow(ByVal row As Integer)
            Try
                Data.RemoveAt(row)
            Catch ex As Exception
            End Try
        End Sub
        Public Function GetData(ByVal row As Integer, ByVal columnName As String) As String
            Dim ret As String = ""
            Try
                ret = Data(row)(ColumnLookup.IndexOf(columnName))
            Catch ex As Exception
            End Try
            Return ret
        End Function
        Public Function GetRow(ByVal row As Integer) As String()
            Dim ret As String() = Nothing
            Try
                ret = Data(row)
            Catch ex As Exception
            End Try
            Return ret
        End Function
        Public Function Serialize() As String
            Dim ret As String = ""
            For Each s As String In ColumnLookup
                ret = ret & Chr(1) & s
            Next
            ret = Mid(ret, 2)

            For Each b As String() In Data
                ret = ret & Chr(1) & Chr(1)
                Dim ret2 As String = ""
                For Each s As String In b
                    ret2 = ret2 & Chr(1) & s
                Next
                ret2 = Mid(ret2, 2)
                ret = ret & ret2
            Next
            Return ret
        End Function
        Public Sub UnSerialize(ByVal dataString As String)
            ColumnLookup.Clear()
            Data.Clear()
            Dim rows() As String = Split(dataString, Chr(1) & Chr(1))
            For Each r As String In rows
                AddRow(Split(r, Chr(1)))
            Next
        End Sub
    End Class

    Public StringCollection As New Dictionary(Of String, String)
    Public ListCollection As New Dictionary(Of String, List(Of String))
    Public ByteCollection As New Dictionary(Of String, Byte())
    Public TableCollection As New Dictionary(Of String, MsgTable)
    Public MessageType As NetworkMessage.MsgTypes
    Private rd As String = Chr(2) & Chr(2)
    Private cd As String = Chr(2)
    Public Sub New(ByVal msgType As NetworkMessage.MsgTypes)
        MessageType = msgType
    End Sub
    Public Sub New()
        MessageType = LobbyShared.NetworkMessage.MsgTypes.None
    End Sub
    Public Function Serialize() As String
        Dim ret As String = ""
        Try
            StringCollection("MSGTYPE") = MessageType
            For Each k As String In StringCollection.Keys
                Dim key As String = k
                Dim value As String = StringCollection(k)
                ret &= rd & "[STRING]" & cd & key & cd & value
            Next
            For Each k As String In ListCollection.Keys
                Dim key As String = k
                ret &= rd & "[LIST]" & cd & key
                For Each value As String In ListCollection(k)
                    ret &= cd & value
                Next
            Next
            For Each k As String In ByteCollection.Keys
                Dim key As String = k
                Dim value As String = Base64.EncodeFromBytes(ByteCollection(k))
                ret &= rd & "[BYTES]" & cd & key & cd & value
            Next
            For Each k As String In TableCollection.Keys
                Dim key As String = k
                Dim value As String = TableCollection(k).Serialize
                ret &= rd & "[TABLE]" & cd & key & cd & value
            Next
        Catch ex As Exception
        End Try
        Return Mid(ret, rd.Length + 1)
    End Function
    Public Function SerializeForTransport(ByVal encodingKey() As Byte) As String
        Dim pb() As Byte = System.Text.Encoding.Unicode.GetBytes(Me.Serialize)
        LobbyShared.Crypt.Cryptic(pb, encodingKey)
        Return LobbyShared.Base64.EncodeFromBytes(pb)
    End Function
    Public Sub UnSerializeFromTransport(ByVal data As String, ByVal encodingKey() As Byte)
        Dim pb2() As Byte = LobbyShared.Base64.DecodeToBytes(data)
        LobbyShared.Crypt.Cryptic(pb2, encodingKey)
        Dim pb3 As String = System.Text.Encoding.Unicode.GetString(pb2)
        Me.UnSerialize(pb3)
    End Sub
    Public Sub UnSerialize(ByVal data As String)
        StringCollection.Clear()
        ListCollection.Clear()
        ByteCollection.Clear()
        TableCollection.Clear()
        Try
            Dim datarows As String() = Split(data, rd)
            For Each dr As String In datarows
                Try
                    Dim c() As String = Split(dr, cd)
                    If c(0) = "[STRING]" Then
                        StringCollection(c(1)) = c(2)
                    ElseIf c(0) = "[LIST]" Then
                        ListCollection(c(1)) = New List(Of String)
                        For i As Integer = 2 To c.Length
                            ListCollection(c(1)).Add(c(i))
                        Next
                    ElseIf c(0) = "[TABLE]" Then
                        Dim t As New MsgTable
                        t.UnSerialize(c(2))
                        TableCollection(c(1)) = t
                    End If
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
        End Try
        Try
            Me.MessageType = StringCollection("MSGTYPE")
            StringCollection.Remove("MSGTYPE")
        Catch ex As Exception

        End Try
    End Sub
End Class