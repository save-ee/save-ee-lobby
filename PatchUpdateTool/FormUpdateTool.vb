Public Class FormUpdateTool
    Dim inputDir As String

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

        Dim uf As New System.IO.StreamWriter(txtoutputdir.Text & "\patchinfo.dat", False)
        Dim del As String = "[::]"
        For Each f As String In lstFiles.Items
            uf.Write("http://www.save-ee.com/lobby/patch/" & Replace(f, inputDir & "\", "").Replace("\", "/") & "." & txtfilever.Text & del)
            uf.Write(Replace(f, inputDir & "\", "") & "." & txtfilever.Text & del)
            uf.Write(Replace(f, inputDir & "\", "") & del)
            uf.Write(My.Computer.FileSystem.GetFileInfo(f).Length & del)
            uf.Write(GetFileHash(f) & vbCrLf)
            My.Computer.FileSystem.CopyFile(f, txtoutputdir.Text & "\" & Replace(f, inputDir & "\", "") & "." & txtfilever.Text, True)
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
        inputDir = "D:\Programming\Lobby\Misc\patch\input"
        AddDirectory(inputDir)

        txtoutputdir.Text = "D:\Programming\Lobby\Misc\patch\output" ' & LobbyShared.Globals.ClientVerNum
        'txtfilever.Text = LobbyShared.Globals.ClientVerNum
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim s As String = ""
        For Each f As String In lstFiles.Items
            s = s & f & vbCrLf
        Next
        Clipboard.SetText(s)

    End Sub
    Sub AddDirectory(ByVal SourcePath As String)
        Dim SourceDir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(SourcePath)

        ' the source directory must exist, otherwise throw an exception
        If SourceDir.Exists Then
            ' copy all the files of the current directory
            Dim ChildFile As System.IO.FileInfo
            For Each ChildFile In SourceDir.GetFiles()
                lstFiles.Items.Add(System.IO.Path.Combine(SourceDir.FullName, ChildFile.Name))
            Next

            ' add all the sub-directories by recursively calling this same routine
            Dim SubDir As System.IO.DirectoryInfo
            For Each SubDir In SourceDir.GetDirectories()
                AddDirectory(SubDir.FullName)
            Next
        Else
            Throw New System.IO.DirectoryNotFoundException("Source directory does not exist: " & SourceDir.FullName)
        End If
    End Sub
End Class
