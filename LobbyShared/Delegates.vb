Public Class Delegates
    Public Delegate Sub SocketDelegate(ByVal s As System.Net.Sockets.Socket)
    Public Delegate Sub StringDelegate(ByVal s As String)
    Public Delegate Sub NoParams()
    Public Delegate Sub NetworkMsgDelegate(ByVal m As LobbyShared.NetworkMessage)
End Class
