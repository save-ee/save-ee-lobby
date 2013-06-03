Public Class Listener
    Public Adapter As String
    Public Port As Integer
    Private s As Net.Sockets.Socket = Nothing
    Public Event ConnectionRequest(ByVal s As Net.Sockets.Socket)

    Public Sub [Start]()
        If s Is Nothing Then
            s = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
        End If

        Dim listeningEndPoint As New System.Net.IPEndPoint(System.Net.IPAddress.Parse(Adapter), Port)
        s.Bind(listeningEndPoint)
        s.Listen(10)

        Try
            s.BeginAccept(New AsyncCallback(AddressOf Listen_Callback), Nothing)
            FormServer.status.Text = "Running..."
        Catch ex As Exception
            'MsgBox("Socket begin accept error." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Critical, "Start Error")
            Try
                s = Nothing
                FormServer.status.Text = "Stopped."
            Catch ex2 As Exception
                FormServer.status.Text = "Socket clear error.  Exit server application."
            End Try
        End Try
    End Sub
    Public Sub [Stop]()
        Try
            s.Shutdown(Net.Sockets.SocketShutdown.Both)
        Catch ex As Exception
            'MsgBox("Socket shutdown error." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Critical, "Stop Error")
        End Try
        Try
            s.Close()
        Catch ex As Exception
            'MsgBox("Socket close error." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Critical, "Stop Error")
        End Try
        Try
            s = Nothing
            FormServer.status.Text = "Stopped."
        Catch ex As Exception
            FormServer.status.Text = "Socket clear error.  Exit server application."
        End Try
    End Sub
    Private Sub Listen_Callback(ByVal ir As IAsyncResult)
        Dim lSocket As System.Net.Sockets.Socket = Nothing
        Try
            lSocket = s.EndAccept(ir)
        Catch ex As Exception
            'MsgBox("Socket end accept error." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Critical, "Callback Error")
        End Try
        If Not lSocket Is Nothing Then
            RaiseEvent ConnectionRequest(lSocket)
        End If
        Try
            s.BeginAccept(New AsyncCallback(AddressOf Listen_Callback), Nothing)
        Catch ex As Exception
            'MsgBox("Socket begin accept error." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Critical, "Callback Error")
        End Try
    End Sub
End Class
