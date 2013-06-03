Public Class FormUpdateTool

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim f As New FolderBrowserDialog
        f.ShowDialog()
        If f.SelectedPath = "" Then Exit Sub
        txtoutputdir.Text = f.SelectedPath
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ofd As New OpenFileDialog
        ofd.Multiselect = True
        ofd.ShowDialog()
        If ofd.FileNames.Length = 0 Then Exit Sub

        For Each f As String In ofd.FileNames
            lstFiles.Items.Add(f)

        Next

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            System.IO.Directory.CreateDirectory(txtoutputdir.Text)
        Catch ex As Exception
        End Try

        Dim uf As New System.IO.StreamWriter(txtoutputdir.Text & "\updateinfo.dat", False)
        Dim del As String = "[::]"
        For Each f As String In lstFiles.Items


            uf.Write("http://www.save-ee.com/lobby/" & My.Computer.FileSystem.GetName(f) & del) ' "." & txtfilever.Text & del)
            uf.Write(My.Computer.FileSystem.GetName(f) & del) ' "." & txtfilever.Text & del)
            uf.Write(My.Computer.FileSystem.GetName(f) & del)
            uf.Write(My.Computer.FileSystem.GetFileInfo(f).Length & del)
            uf.Write(GetFileHash(f) & vbCrLf)
            My.Computer.FileSystem.CopyFile(f, txtoutputdir.Text & "\" & My.Computer.FileSystem.GetName(f), True) ' & "." & txtfilever.Text, True)
        Next
        uf.Close()
        uf.Dispose()
    End Sub
    Public Function GetFileHash(ByVal p As String) As String
        Dim objReader As System.IO.Stream
        Dim objMD5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim arrHash() As Byte

        ' open file (as read-only)
        objReader = New System.IO.FileStream(p, System.IO.FileMode.Open, System.IO.FileAccess.Read)

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
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ghost's path
        Dim solutionPath As String = "D:\Programming\Lobby\Save-EE Lobby 2\"
        lstFiles.Items.Add(solutionPath & "\LobbyUpdate\bin\Debug\LobbyUpdate.exe")
        'lstFiles.Items.Add(solutionPath & "\LobbyClient\bin\x86\Debug\LobbyClient.exe")
        'lstFiles.Items.Add(solutionPath & "\LobbyClient\bin\x86\Debug\CustomPanels.dll")
        'lstFiles.Items.Add(solutionPath & "\LobbyClient\bin\x86\Debug\LobbyShared.dll")
        'lstFiles.Items.Add(solutionPath & "\LobbyClient\bin\x86\Debug\Language.dll")
        'lstFiles.Items.Add(solutionPath & "\LobbyClient\bin\x86\Debug\Network.dll")
        'lstFiles.Items.Add(solutionPath & "\PatchUpdate\bin\Debug\PatchUpdate.exe")
        lstFiles.Items.Add(solutionPath & "\LobbyClient.exe")

        txtoutputdir.Text = "D:\Programming\Lobby\Misc\" & LobbyShared.Globals.ClientVerNum
        txtfilever.Text = LobbyShared.Globals.ClientVerNum
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim s As String = ""
        For Each f As String In lstFiles.Items
            s = s & f & vbCrLf
        Next
        Clipboard.SetText(s)

    End Sub
End Class
