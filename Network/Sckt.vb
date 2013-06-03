Public Class Sckt
    Public Guid As String = System.Guid.NewGuid.ToString

    Public Event Connecting()
    Public Event Connected()
    Public Event ConnectFailed()
    Public Event ConnectionLost()
    Public Event DataArrival(ByVal b() As Byte)

    Private s As Net.Sockets.Socket
    Private buffer(4095) As Byte

    Public Enum SocketEncodings
        ASCII
        UNICODE
    End Enum
    Public SocketEncoding As SocketEncodings = SocketEncodings.ASCII
    Public Sub New(ByVal ns As Net.Sockets.Socket)
        s = ns
        s.NoDelay = True
        ReDim buffer(s.ReceiveBufferSize - 1)
        ListenForData()
    End Sub
    Public Sub New()

    End Sub
    Public Sub Connect(ByVal Address As String, ByVal Port As Integer)
        Me.Close()
        Try
            s = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
            s.NoDelay = True
            ReDim buffer(s.ReceiveBufferSize - 1)
        Catch ex As Exception
            RaiseEvent ConnectFailed()
            Exit Sub
        End Try
        RaiseEvent Connecting()
        Try
            s.BeginConnect(Net.IPAddress.Parse(Address), Port, AddressOf ConnectCall, Nothing)
        Catch ex As Exception
            RaiseEvent ConnectFailed()
        End Try
    End Sub
    Private Sub ConnectCall(ByVal ir As IAsyncResult)
        Try
            s.EndConnect(ir)
        Catch ex As Exception
            RaiseEvent ConnectFailed()
            Exit Sub
        End Try
        If s.Connected = True Then
            ListenForData()
            RaiseEvent Connected()
        Else
            RaiseEvent ConnectFailed()
        End If
    End Sub
    Public Sub Close()
        Try
            s.Shutdown(Net.Sockets.SocketShutdown.Both)
        Catch ex As Exception
        End Try
        Try
            s.Close()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub ListenForData()
        Try
            s.BeginReceive(buffer, 0, buffer.Length, Net.Sockets.SocketFlags.None, AddressOf ListenCallback, Nothing)
        Catch ex As Exception
            RaiseEvent ConnectionLost()
        End Try
    End Sub
    Public Sub ListenCallback(ByVal ir As IAsyncResult)
        Dim BytesRecvd As Integer = 0
        Try
            BytesRecvd = s.EndReceive(ir)
        Catch ex As Exception
        End Try
        If BytesRecvd = 0 Then
            RaiseEvent ConnectionLost()
            Exit Sub
        End If
        Dim b(BytesRecvd - 1) As Byte
        Array.Copy(buffer, b, b.Length)
        Array.Clear(buffer, 0, buffer.Length)
        RaiseEvent DataArrival(b)
        ListenForData()
    End Sub
    Public Sub Send(ByVal d As String)
        If SocketEncoding = SocketEncodings.ASCII Then
            Dim b() As Byte = System.Text.Encoding.ASCII.GetBytes(d)
            Send(b)
        ElseIf SocketEncoding = SocketEncodings.UNICODE Then
            Dim b() As Byte = System.Text.Encoding.Unicode.GetBytes(d)
            Send(b)
        End If
    End Sub
    Public Sub SendSync(ByVal d As String)
        If SocketEncoding = SocketEncodings.ASCII Then
            Dim b() As Byte = System.Text.Encoding.ASCII.GetBytes(d)
            SendSync(b)
        ElseIf SocketEncoding = SocketEncodings.UNICODE Then
            Dim b() As Byte = System.Text.Encoding.Unicode.GetBytes(d)
            SendSync(b)
        End If
    End Sub
    Public Sub SendSync(ByVal b() As Byte)
        Try
            s.Send(b, b.Length, Net.Sockets.SocketFlags.None)
        Catch ex As Exception
            '    RaiseEvent ConnectionLost()
        End Try
    End Sub
    Public Sub Send(ByVal b() As Byte)
        Try
            s.BeginSend(b, 0, b.Length, Net.Sockets.SocketFlags.None, AddressOf SendCall, Nothing)
        Catch ex As Exception
            '    RaiseEvent ConnectionLost()
        End Try
    End Sub
    Private Sub SendCall(ByVal ir As IAsyncResult)
        Try
            s.EndSend(ir)
        Catch ex As Exception
            '   RaiseEvent ConnectionLost()
        End Try
    End Sub
End Class
