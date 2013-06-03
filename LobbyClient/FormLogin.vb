Public Class FormLogin
    Public ConnectAction As String = ""
    Public FormUpdate As New FormUpdate
    Public HackerMode As Boolean = False
    Public WithEvents wc As New System.Net.WebClient

#Region "Load"
    Private Sub FormLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim x As New System.Net.NetworkInformation.Ping
        'Dim p As System.Net.NetworkInformation.PingReply = x.Send("www.save-ee.com")
        'MsgBox(p.Address.ToString)

        Me.Text = LobbyShared.Globals.ClientVersion & " - Login"
        Me.Show()
        Me.Refresh()
        HookEvents()
        LoadPrefs()
        LoadUsers()
        Try
            GetPublicIP()
            GetServerIP()
            GetAnnounce()
            GetSmilies()
            GetBadWords()
        Catch ex As Exception
            MsgBox(Language.Login.Please_check_your_internet_connection, MsgBoxStyle.Exclamation, Language.Login.Startup_Error)
            Application.Exit()
            Exit Sub
        End Try
        GenerateHardwareList()
        LoginGroupBox.Enabled = True

        StatusLabel.Text = Language.Login.Checking_for_updates
        Me.Refresh()

        ' Don't need this shit anymore
        Try
            System.IO.File.Delete(Globals.ProcessFolder & "updateinfo.dat")
            System.IO.File.Delete(Globals.ProcessFolder & "LobbyUpdate.exe")
            System.IO.File.Delete(Globals.ProcessFolder & "PatchUpdate.exe")
            System.IO.File.Delete(Globals.ProcessFolder & "CustomPanels.dll")
            System.IO.File.Delete(Globals.ProcessFolder & "Language.dll")
            System.IO.File.Delete(Globals.ProcessFolder & "LobbyShared.dll")
            System.IO.File.Delete(Globals.ProcessFolder & "Network.dll")
        Catch
        End Try

        CheckForUpdates()

        If Globals.PatchEngine.LocateEEDir(True) Then
            Dim patchinfo As String = Network.WebClient.DownloadString("http://www.save-ee.com/lobby/patch/patchinfo.dat")

            ' Moving this to AppData...
            If System.IO.File.Exists(Globals.ProcessFolder & "patch\patchinfo.dat") Then
                Try
                    ' Move
                    System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Patch")
                    Globals.PatchEngine.CopyDirectory(Globals.ProcessFolder & "patch", Globals.AppDataEnvVar & "\Save-EE\Patch")
                    System.IO.Directory.Delete(Globals.ProcessFolder & "patch", True)
                Catch
                End Try
            End If
            If System.IO.Directory.Exists(Globals.ProcessFolder & "update") Then
                Try
                    System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Update")
                    Globals.PatchEngine.CopyDirectory(Globals.ProcessFolder & "update", Globals.AppDataEnvVar & "\Save-EE\Update")
                    System.IO.Directory.Delete(Globals.ProcessFolder & "update", True)
                Catch
                End Try
            End If

            ' Now read it..
            Dim patchinfoold As String = LobbyShared.FileIO.ReadFile(Globals.AppDataEnvVar & "\Save-EE\Patch\patchinfo.dat")
            If patchinfo <> patchinfoold Then
                '  If the instance still exists... (ie. it's Not Nothing)
                If Not IsNothing(Globals.PatcherForm) Then
                    '  and if it hasn't been disposed yet
                    If Not Globals.PatcherForm.IsDisposed Then
                        '  then it must already be instantiated - maybe it's
                        '  minimized or hidden behind other forms ?
                        Globals.PatcherForm.BringToFront()  '  Optional
                    Else
                        '  else it has already been disposed, so you can
                        '  instantiate a new form and show it
                        Globals.PatcherForm = New FormPatch() With {.UpdateArg = True}
                        Globals.PatcherForm.Show()
                    End If
                Else
                    '  else the form = nothing, so you can safely
                    '  instantiate a new form and show it
                    Globals.PatcherForm = New FormPatch() With {.UpdateArg = True}
                    Globals.PatcherForm.Show()
                End If
            End If
        End If

        StatusLabel.Text = Language.Login.Initialization_complete
        Me.Refresh()

        If My.Settings.AutoLogin <> "" AndAlso FormUpdate.Visible = False Then
            ' Decode autologin data
            Dim UserBytes() As Byte = LobbyShared.Base64.DecodeToBytes(My.Settings.AutoLogin)
            LobbyShared.Crypt.Cryptic(UserBytes, New Byte() {1, 2, 3, 4, 5})
            Dim str As String = System.Text.Encoding.Unicode.GetString(UserBytes)

            Dim user As String = ""
            Dim pass As String = ""
            Try
                user = Split(str, "[-:+:-]")(0)
                pass = Split(str, "[-:+:-]")(1)
            Catch ex As Exception
            End Try

            My.Settings.AutoLogin = ""
            If user <> "" AndAlso pass <> "" Then
                UsernameComboBox.Text = user
                PasswordTextBox.Text = pass
                LoginButton_Click(Nothing, Nothing)
            End If
        End If
        'should take this out could be dangerous
        'If HackerMode = True Then
        '   UsernameComboBox.Text = "HACKER MODE " & System.System.Guid.NewSystem.Guid.ToString
        '   PasswordTextBox.Text = System.System.Guid.NewSystem.Guid.ToString
        '   CreateNewUserButton_Click(Nothing, Nothing)
        'End If
    End Sub
    Private Sub FormLogin_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        UnHookEvents()
    End Sub
    Private Sub UpdateLanguage()
        UsernameLabel.Text = Language.Login.Username & ":"
        PasswordLabel.Text = Language.Login.Password & ":"
        CreateNewUserButton.Text = Language.Login.Create_New_User
        LoginButton.Text = Language.Login.Login
    End Sub
#End Region
#Region "Hook/UnHook Events"
    Public Sub HookEvents()
        AddHandler Globals.ClientEngine.ConnectionLost, AddressOf OnConnectionLost
        AddHandler Globals.ClientEngine.Connecting, AddressOf OnConnecting
        AddHandler Globals.ClientEngine.Connected, AddressOf OnConnected
        AddHandler Globals.ClientEngine.ConnectFailed, AddressOf OnConnectFailed
        AddHandler Globals.ClientEngine.NetworkInit, AddressOf OnNetworkInit
        AddHandler Globals.ClientEngine.VerifyKeyPass, AddressOf OnVerifyKeyPass
        AddHandler Globals.ClientEngine.LoginFail, AddressOf OnLoginFail
        AddHandler Globals.ClientEngine.LoginLocked, AddressOf OnLoginLocked
        AddHandler Globals.ClientEngine.LoginDoesntExist, AddressOf OnLoginDoesntExist
        AddHandler Globals.ClientEngine.LoginAccountExists, AddressOf OnLoginAccountExists
        AddHandler Globals.ClientEngine.LoginComplete, AddressOf OnLoginComplete
        AddHandler Globals.ClientEngine.LoginAlreadyOn, AddressOf OnLoginAlreadyOn
    End Sub
    Public Sub UnHookEvents()
        RemoveHandler Globals.ClientEngine.ConnectionLost, AddressOf OnConnectionLost
        RemoveHandler Globals.ClientEngine.Connecting, AddressOf OnConnecting
        RemoveHandler Globals.ClientEngine.Connected, AddressOf OnConnected
        RemoveHandler Globals.ClientEngine.ConnectFailed, AddressOf OnConnectFailed
        RemoveHandler Globals.ClientEngine.NetworkInit, AddressOf OnNetworkInit
        RemoveHandler Globals.ClientEngine.VerifyKeyPass, AddressOf OnVerifyKeyPass
        RemoveHandler Globals.ClientEngine.LoginFail, AddressOf OnLoginFail
        RemoveHandler Globals.ClientEngine.LoginLocked, AddressOf OnLoginLocked
        RemoveHandler Globals.ClientEngine.LoginDoesntExist, AddressOf OnLoginDoesntExist
        RemoveHandler Globals.ClientEngine.LoginAccountExists, AddressOf OnLoginAccountExists
        RemoveHandler Globals.ClientEngine.LoginComplete, AddressOf OnLoginComplete
        RemoveHandler Globals.ClientEngine.LoginAlreadyOn, AddressOf OnLoginAlreadyOn
    End Sub
#End Region
#Region "Startup"
    Private Sub GetPublicIP()
        StatusLabel.Text = Language.Login.Obtaining_public_IP
        Me.Refresh()
        Globals.CurrentUser.PublicIP = Network.WebClient.DownloadString("http://www.save-ee.com/whatismyip.php")
        Dim x As New System.Text.RegularExpressions.Regex("[^\w\.]")
        Globals.CurrentUser.PublicIP = x.Replace(Globals.CurrentUser.PublicIP, "")
        If Globals.CurrentUser.PublicIP = "" Then
            MsgBox(Language.Login.Error_obtaining_public_IP, MsgBoxStyle.Critical, Language.Login.Startup_Error)
            Application.Exit()
            Exit Sub
        End If
    End Sub
    Public Sub GetServerIP()
        StatusLabel.Text = Language.Login.Obtaining_server_IP
        Me.Refresh()
        If LobbyShared.Globals.IsLocal = True Then
            Globals.ServerIP = LobbyShared.Globals.LocalAdapter
        Else
            Globals.ServerIP = Network.WebClient.DownloadString("http://www.save-ee.com/lobby/serverinfo.dat")
        End If
        If Globals.ServerIP = "" Then
            MsgBox(Language.Login.Error_obtaining_server_IP, MsgBoxStyle.Critical, Language.Login.Startup_Error)
            Application.Exit()
            Exit Sub
        End If
    End Sub
    Public Sub GetAnnounce()
        StatusLabel.Text = Language.Login.Obtaining_announcement
        Me.Refresh()
        Globals.Announce = Network.WebClient.DownloadString("http://www.save-ee.com/lobby/announce.dat")
        If Globals.Announce = "" Then
            MsgBox(Language.Login.Error_obtaining_announcement, MsgBoxStyle.Critical, Language.Login.Startup_Error)
            Application.Exit()
            Exit Sub
        End If
    End Sub
    Public Sub GetSmilies()
        StatusLabel.Text = Language.Login.Obtaining_smilies
        Me.Refresh()
        Dim spack() As String = Split(Network.WebClient.DownloadString("http://www.save-ee.com/images/smilies/smiles.pak"), vbCrLf)
        Dim s As String = ""
        Dim p As New List(Of String)
        For Each r As String In spack
            Dim cols() As String = Split(r, "=+:")
            If cols.Length = 3 Then
                If p.Contains(cols(0)) = False Then s = s & vbCrLf & "<tr><td>" & cols(1) & "</td><td>" & cols(2) & "</td><td><img src=""http://www.save-ee.com/images/smilies/" & cols(0) & """></td></tr>" : p.Add(cols(0))
                Globals.Smileys(cols(2)) = cols(0)
            End If
        Next
    End Sub
    Public Sub GetBadWords()
        StatusLabel.Text = Language.Login.Obtaining_bad_words
        Me.Refresh()
        Dim words() As String = Split(Network.WebClient.DownloadString("http://www.save-ee.com/lobby/badwords.dat"), vbCrLf)
        Dim s As String = ""
        For Each r As String In words
            Dim cols() As String = Split(r, "=+:")
            If cols.Length = 2 Then
                Globals.BadWords(cols(0)) = cols(1)
            End If
        Next
    End Sub
    Private Sub GenerateHardwareList()
        Dim MacIndex As Integer = 0
        For Each n As System.Net.NetworkInformation.NetworkInterface In System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces
            If n.GetPhysicalAddress.GetHashCode <> 0 Then
                MacIndex += 1
                Globals.HardwareList("MC" & MacIndex & "_" & System.Guid.NewGuid.ToString) = n.GetPhysicalAddress.ToString
            End If
        Next
        Try
            Globals.HardwareList("ID_" & System.Guid.NewGuid.ToString) = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("installdate")
        Catch ex As Exception
        End Try
        Try
            For Each s As String In Microsoft.Win32.Registry.Users.GetSubKeyNames
                If s.Length() > 30 Then
                    Globals.HardwareList("SID_" & System.Guid.NewGuid.ToString) = s.Substring(0, s.LastIndexOf("-"))
                    Exit For
                End If
            Next
        Catch ex As Exception
        End Try
        Globals.HardwareList("IP_" & System.Guid.NewGuid.ToString) = Globals.CurrentUser.PublicIP
    End Sub
#End Region
#Region "Users"
    Public Sub LoadUsers()
        StatusLabel.Text = Language.Login.Loading_usernames
        Me.Refresh()

        Dim UserData As String
        ' We are moving away from the .txt file storage
        If System.IO.File.Exists(Globals.ProcessFolder & "usernames.txt") Then
            UserData = LobbyShared.FileIO.ReadFile("usernames.txt")
            Try
                System.IO.File.Delete(Globals.ProcessFolder & "usernames.txt")
            Catch
                MsgBox("Unable to delete """ & Globals.ProcessFolder & "usernames.txt"": This file is no longer needed and will cause your usernames to stop saving correctly." & _
                       vbCrLf & vbCrLf & "Please delete this file manually.", MsgBoxStyle.Critical, Language.Login.Startup_Error)
            End Try
        Else
            UserData = My.Settings.Usernames
        End If
        Dim UserBytes() As Byte = LobbyShared.Base64.DecodeToBytes(UserData)
        If UserBytes.Length > 0 Then
            LobbyShared.Crypt.Cryptic(UserBytes, New Byte() {1, 2, 3, 4, 5})
            Dim Users As String = System.Text.Encoding.Unicode.GetString(UserBytes)
            Dim UsersA() As String = Split(Users, Chr(0) & Chr(0))

            For Each u As String In UsersA
                If u.ToLower.StartsWith("default_adapter") = False Then
                    Try
                        Dim user As String = Split(u, Chr(0))(0)
                        Dim pass As String = Split(u, Chr(0))(1)
                        Globals.UserAccounts(user) = pass
                        UsernameComboBox.Items.Add(user)
                    Catch ex As Exception

                    End Try
                ElseIf u.ToLower.StartsWith("default_adapter") = True Then
                    'Globals.DefaultAdapter = Split(u, Chr(0))(1)
                End If
            Next
        End If
    End Sub
    Public Sub SaveUsers()
        Dim UserData As String = LobbyShared.Dictionary.DictionaryToString(Globals.UserAccounts)
        Dim UserBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(UserData)
        LobbyShared.Crypt.Cryptic(UserBytes, New Byte() {1, 2, 3, 4, 5})
        Dim UsersB64 As String = LobbyShared.Base64.EncodeFromBytes(UserBytes)
        My.Settings.Usernames = UsersB64
        'LobbyShared.FileIO.WriteFile("usernames.txt", UsersB64, False)
    End Sub
    Private Sub SelectUser(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsernameComboBox.SelectedIndexChanged
        If Globals.UserAccounts.ContainsKey(UsernameComboBox.Text) = True Then
            PasswordTextBox.Text = Globals.UserAccounts(UsernameComboBox.Text)
        End If
    End Sub
    Public Sub LoadPrefs()
        StatusLabel.Text = Language.Login.Loading_preferences
        Me.Refresh()

        If My.Settings.UpgradeSettings Then
            My.Settings.Upgrade()
            My.Settings.Reload()
            My.Settings.UpgradeSettings = False
        End If

        ' We are moving away from the .txt file storage, load the first time then delete file
        If System.IO.File.Exists(Globals.ProcessFolder & "userprefs.txt") Then
            Dim UserPrefs As New Dictionary(Of String, String)
            Dim UserData As String = LobbyShared.FileIO.ReadFile(Globals.ProcessFolder & "userprefs.txt")
            Try
                System.IO.File.Delete(Globals.ProcessFolder & "userprefs.txt")
            Catch
                MsgBox("Unable to delete """ & Globals.ProcessFolder & "userprefs.txt"": This file is no longer needed and will cause your options to stop saving correctly." & _
                       vbCrLf & vbCrLf & "Please delete this file manually.", MsgBoxStyle.Critical, Language.Login.Startup_Error)
            End Try
            Dim UserBytes() As Byte = LobbyShared.Base64.DecodeToBytes(UserData)
            If UserBytes.Length > 0 Then
                LobbyShared.Crypt.Cryptic(UserBytes, New Byte() {1, 2, 3, 4, 5})
                Dim Prefs As String = System.Text.Encoding.Unicode.GetString(UserBytes)
                Dim PrefsArray() As String = Split(Prefs, Chr(0) & Chr(0))

                For Each u As String In PrefsArray
                    Try
                        Dim k As String = Split(u, Chr(0))(0)
                        Dim v As String = Split(u, Chr(0))(1)
                        UserPrefs(k) = v
                    Catch ex As Exception
                    End Try
                Next
            End If
            'Color.SlateGray.ToArgb '&HFF708090
            'Color.Black.ToArgb '&HFF000000
            'Color.SteelBlue.ToArgb '&HFF4682B4
            '&HFFE9A3E9 'Argb for e9a3e9 (pinkish purple)
            '&HFFFF9090 'Argb for ff9090 (a red)
            'Color.White.ToArgb '&HFFFFFFFF
            '&HFF90FF90 'Argb for 90ff90 (a green)
            '&HFF90FFFF 'Argb for 90ffff (a cyan)
            '&HFFFFFF90 'Argb for ffff90 (a yellow)
            '&HFFAAAAFF 'Argb for aaaaff (a light blue)
            '&HFFB0B0B0 'Argb for b0b0b0 (a grey)

            ' Update My.Settings
            My.Settings.AutoResizeGameColumns = If(UserPrefs("AUTORESIZEGAMECOLUMNS").ToLower = "true", True, False)
            'My.Settings("Autoscroll") = UserPrefs("AUTOSCROLL")
            My.Settings.EnableSounds = If(UserPrefs("ENABLESOUNDS").ToLower = "true", True, False)
            My.Settings.Language = UserPrefs("LANGUAGE")
            My.Settings.MaximizeLobbyOnLogin = If(UserPrefs("MAXIMIZELOBBY").ToLower = "true", True, False)
            My.Settings.MinimizeToSystemTray = If(UserPrefs("MINIMIZETOTRAY").ToLower = "true", True, False)
            My.Settings.ShowIconInSystemTray = If(UserPrefs("SHOWINTRAY").ToLower = "true", True, False)
            My.Settings.ShowTimestamps = If(UserPrefs("SHOWTIMESTAMPS").ToLower = "true", True, False)
            My.Settings.RevertToChatAfterWhisper = If(UserPrefs("CHATAFTERWHISP").ToLower = "true", True, False)

            My.Settings.ButtonColorTopGradient = Integer.Parse(UserPrefs("MAINBUTTONCOLOR1"))
            My.Settings.ButtonColorBottomGradient = Integer.Parse(UserPrefs("MAINBUTTONCOLOR2"))
            My.Settings.ChatColorBackground = Integer.Parse(UserPrefs("WB_BGCOLOR"))
            My.Settings.ChatColorHyperlink = Integer.Parse(UserPrefs("HYPERLINKCOLOR"))
            My.Settings.ChatColorUserLink = Integer.Parse(UserPrefs("USERLINKCOLOR"))
            My.Settings.ChatColorChatText = Integer.Parse(UserPrefs("CHATTEXTCOLOR"))
            My.Settings.ChatColorEmoteText = Integer.Parse(UserPrefs("EMOTETEXTCOLOR"))
            My.Settings.ChatColorWhisperText = Integer.Parse(UserPrefs("WHISPERTEXTCOLOR"))
            My.Settings.ChatColorAlertText = Integer.Parse(UserPrefs("ALERTTEXTCOLOR"))
            My.Settings.ChatColorWarningText = Integer.Parse(UserPrefs("WARNINGTEXTCOLOR"))
            My.Settings.ChatColorServerText1 = Integer.Parse(UserPrefs("SERVERTEXT1COLOR"))
            My.Settings.ChatColorServerText2 = Integer.Parse(UserPrefs("SERVERTEXT2COLOR"))

            My.Settings.DefaultAdapter = UserPrefs("DEFAULT_ADAPTER")
        Else
            ' Just call directly later on..
        End If

        ' We should always do this stuff:
        ' --- maybe move it to FormLogin_Load and only call this subroutine if userprefs.txt exists??
        ' --- possibly in addition to above concept
        ' Set adapter
        Globals.DefaultAdapter = My.Settings.DefaultAdapter

        ' Set language
        Try
            Language.Main.SetLanguage([Enum].Parse(GetType(Language.Main.Languages), My.Settings.Language))
        Catch
            Language.Main.SetLanguage(Language.Main.Languages.English)
            My.Settings.Language = "English"
        End Try
        UpdateLanguage()
    End Sub
#End Region
#Region "EventHandlers"
    Public Sub OnConnectionLost()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnConnectionLost))
        Else
            StatusLabel.Text = Language.Login.Your_connection_to_the_server_was_terminated
            CreateNewUserButton.Enabled = True
            LoginButton.Enabled = True
        End If
    End Sub
    Public Sub OnConnecting()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnConnecting))
        Else
            StatusLabel.Text = Language.Login.Connecting_to_server
        End If
    End Sub
    Public Sub OnConnectFailed()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnConnectFailed))
        Else
            StatusLabel.Text = Language.Login.Unable_to_contact_server
            LoginButton.Enabled = True
            CreateNewUserButton.Enabled = True
        End If
    End Sub
    Public Sub OnConnected()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnConnected))
        Else
            StatusLabel.Text = Language.Login.Connected__initializing
        End If
    End Sub
    Public Sub OnNetworkInit()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnNetworkInit))
        Else
            StatusLabel.Text = Language.Login.Authenticating
        End If
    End Sub
    Public Sub OnVerifyKeyPass()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnVerifyKeyPass))
        Else
            StatusLabel.Text = Language.Login.Verification_complete
            If Me.ConnectAction = "LOGIN" Then
                Globals.ClientEngine.Login(Globals.CurrentUser.Username, Globals.CurrentUser.Password)
            ElseIf Me.ConnectAction = "CREATE" Then
                Globals.ClientEngine.CreateAccount(Globals.CurrentUser.Username, Globals.CurrentUser.Password)
            Else
                Globals.ClientEngine.Disconnect()
            End If
        End If
    End Sub
    Public Sub OnLoginComplete()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnLoginComplete))
        Else
            Try
                Globals.UserAccounts(Globals.CurrentUser.Username) = Globals.CurrentUser.Password
                SaveUsers()
                UnHookEvents()
                Dim f As New FormLobby
                If My.Settings.MaximizeLobbyOnLogin = True Then
                    f.WindowState = FormWindowState.Maximized
                End If
                f.Show()
                Me.Close()
                Me.Dispose()
            Catch ex As Exception
                MsgBox("OnLoginComplete Error")
            End Try
        End If
    End Sub
    Public Sub OnLoginFail()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnLoginFail))
        Else
            MsgBox(Language.Login.User_authentication_failed, MsgBoxStyle.Critical, Language.Login.Login_Error)
        End If
    End Sub
    Public Sub OnLoginAlreadyOn()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnLoginAlreadyOn))
        Else
            MsgBox(Language.Login.Account_already_in_use, MsgBoxStyle.Critical, Language.Login.Login_Error)
        End If
    End Sub
    Public Sub OnLoginLocked()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnLoginLocked))
        Else
            MsgBox(Language.Login.Account_was_locked_by_an_administrator, MsgBoxStyle.Critical, Language.Login.Login_Error)
        End If
    End Sub
    Public Sub OnLoginAccountExists()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnLoginAccountExists))
        Else
            MsgBox(Language.Login.Account_already_exists, MsgBoxStyle.Critical, Language.Login.Login_Error)
        End If
    End Sub
    Public Sub OnLoginDoesntExist()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnLoginDoesntExist))
        Else
            MsgBox(Language.Login.Account_not_found, MsgBoxStyle.Critical, Language.Login.Login_Error)
        End If
    End Sub
#End Region
#Region "Update Stuff"
    Public Sub CheckForUpdates()
        Dim updateinfo As String = Network.WebClient.DownloadString("http://www.save-ee.com/lobby/updateinfo.dat")
        Dim uir() As String = Split(updateinfo, vbCrLf)

        For Each r As String In uir
            If InStr(r, "LobbyUpdate.exe", CompareMethod.Text) > 0 Then
                'Dim c() As String = Split(r, "[::]")
                'Try
                '    Dim ch As String = LobbyShared.FileHash.GetFileHash(Globals.ProcessFolder & c(2))
                '    If ch <> c(4) Then
                '        FormUpdate.Text = Language.Login.Checking_for_updates
                '        FormUpdate.Show()
                '        FormUpdate.Refresh()
                '        wb.DownloadFileAsync(New System.Uri(c(0)), c(2))
                '        Exit Sub
                '    End If
                'Catch ex As Exception
                '    FormUpdate.Text = Language.Login.Checking_for_updates
                '    FormUpdate.Show()
                '    FormUpdate.Refresh()
                '    wb.DownloadFileAsync(New System.Uri(c(0)), c(2))
                'End Try
                'Exit For
            ElseIf InStr(r, "LobbyClient.exe", CompareMethod.Text) > 0 Then
                Dim c() As String = Split(r, "[::]")
                Try
                    Dim ch As String = LobbyShared.FileHash.GetFileHash(Globals.ProcessPath)
                    If ch <> c(4) Then
                        ' Not up to date
                        FormUpdate.Text = Language.Login.Checking_for_updates
                        FormUpdate.Show()
                        FormUpdate.Refresh()

                        ' See if file in AppData is up to date
                        If System.IO.File.Exists(Globals.AppDataEnvVar & "\Save-EE\Lobby Client\LobbyClient.exe") Then
                            If c(4) = LobbyShared.FileHash.GetFileHash(Globals.AppDataEnvVar & "\Save-EE\Lobby Client\LobbyClient.exe") Then
                                ' Just copy from AppData
                                wc_DownloadFileCompleted(Nothing, Nothing)
                                Exit Sub
                            End If
                        End If

                        ' Gotta download it
                        Try
                            System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Lobby Client")
                        Catch
                        End Try
                        wc.DownloadFileAsync(New System.Uri(c(0)), Globals.AppDataEnvVar & "\Save-EE\Lobby Client\LobbyClient.exe")
                        Exit Sub
                    End If
                Catch ex As Exception
                    FormUpdate.Text = Language.Login.Checking_for_updates
                    FormUpdate.Show()
                    FormUpdate.Refresh()
                    Try
                        System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Lobby Client")
                    Catch
                    End Try
                    wc.DownloadFileAsync(New System.Uri(c(0)), Globals.AppDataEnvVar & "\Save-EE\Lobby Client\LobbyClient.exe")
                End Try
                Exit For
            End If
        Next
    End Sub
    Private Sub wc_DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.DownloadFileCompleted
        Try
            FormUpdate.Close()
        Catch
        End Try
        'If System.IO.File.Exists("LobbyUpdate.exe") Then
        '   Shell("LobbyUpdate.exe")
        '   Application.Exit()
        'Else
        ' use ping to delay 3 seconds
        ' copy from download folder
        ' run
        Process.Start("cmd.exe", _
                      "/C echo Completing Save-EE Lobby Client update..." & _
                      " & ping 1.1.1.1 -n 1 -w 3000 > nul" & _
                      " & copy """ & Globals.AppDataEnvVar & "\Save-EE\Lobby Client\LobbyClient.exe""" & " """ & Globals.ProcessPath & """ > nul" & _
                      " & start """" /b """ & Globals.ProcessPath)

        For Each p As Process In Process.GetProcesses()
            Try
                If p.MainModule.FileName = Globals.ProcessPath Then p.Kill()
            Catch
            End Try
        Next
        Application.Exit()
        'End If
    End Sub
    Private Sub wc_DownloadProgressChanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles wc.DownloadProgressChanged
        FormUpdate.ProgressBar1.Value = e.ProgressPercentage
        FormUpdate.BringToFront()
    End Sub
#End Region
#Region "FormHandlers"
    Private Sub UsernameComboBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles UsernameComboBox.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            LoginButton_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub PasswordTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles PasswordTextBox.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            LoginButton_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub LoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        ConnectAction = "LOGIN"
        MakeConnection()
    End Sub
    Private Sub CreateNewUserButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateNewUserButton.Click
        ConnectAction = "CREATE"
        MakeConnection()
    End Sub
    Public Function ValidateUserPass(ByVal u As String, ByVal p As String) As Boolean
        If Trim(u) = "" Then
            MsgBox(Language.Login.Username & " " & Language.Login.is_required, MsgBoxStyle.Critical, Language.Login.Login_Error)
            Return False
        End If
        If u.Contains(Chr(0)) = True OrElse u.Contains(Chr(1)) = True OrElse u.Contains(vbCr) = True OrElse u.Contains(vbLf) = True Then
            MsgBox(Language.Login.Username & " " & Language.Login.contains_invalid_chars, MsgBoxStyle.Critical, Language.Login.Login_Error)
            Return False
        End If
        If u.Length > 50 Then
            MsgBox(Language.Login.Username & " " & Language.Login.contains_too_many_chars, MsgBoxStyle.Critical, Language.Login.Login_Error)
            Return False
        End If

        If Trim(p) = "" Then
            MsgBox(Language.Login.Password & " " & Language.Login.is_required, MsgBoxStyle.Critical, Language.Login.Login_Error)
            Return False
        End If
        If p.Contains(Chr(0)) = True OrElse p.Contains(Chr(1)) = True OrElse p.Contains(vbCr) = True OrElse p.Contains(vbLf) = True Then
            MsgBox(Language.Login.Password & " " & Language.Login.contains_invalid_chars, MsgBoxStyle.Critical, Language.Login.Login_Error)
            Return False
        End If
        If p.Length > 50 Then
            MsgBox(Language.Login.Password & " " & Language.Login.is_required, MsgBoxStyle.Critical, Language.Login.Login_Error)
            Return False
        End If

        Return True
    End Function
    Private Sub MakeConnection()
        Globals.CurrentUser.Username = Trim(UsernameComboBox.Text).Replace("'", "")
        Globals.CurrentUser.Password = Trim(PasswordTextBox.Text).Replace("'", "")
        If ValidateUserPass(Globals.CurrentUser.Username, Globals.CurrentUser.Password) = False Then Exit Sub
        LoginButton.Enabled = False
        CreateNewUserButton.Enabled = False
        Globals.ClientEngine.NetworkKey = LobbyShared.Globals.ServerSalt
        Globals.ClientEngine.Connect()
    End Sub
#End Region
End Class
