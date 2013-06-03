Public Class FormLobby
#Region "Declarations"
    ' For chat
    Private AllowNav As Boolean = True
    Private ilink As String = ""
    Private SelectedList As ListView
    Private ControlDown As Boolean = False
    Private ChatHistory As New List(Of String)
    Private ChatHistoryPosition As Integer = 0
    Private CurrentHistory As String = ""
    Private LastWhisper As String = ""
    Private FloodDetectionQueue As New List(Of Integer)

    ' For game/locate loop
    Private GameLoopAbort As Boolean = False
    Private GameLoopThread As Threading.Thread
    Private RawSocket As System.Net.Sockets.Socket
    Private buffer(1024) As Byte
    Private ClientLastLocateGameIP As String = ""
    Private ClientLastLocateUsername As String = ""
    Private HostLastLocateUserIP As String = ""
    Private HostLastLocateUsername As String = ""

    ' Misc
    Private PreviousWindowState As FormWindowState
    Private GamesListViewSorter As New ListViewHelpers.ListViewSorter(GamesListView)
    Private AFKToggleForce As Boolean = False
    Private IsUserList As Boolean = True
#End Region

#Region "Load, Close, Control Resizing"
    Private Sub FormLobby_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        UnHookEvents()
        Application.Exit()
    End Sub
    Private Sub FormLobby_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Timer1.Interval = 500
        'Timer1.Enabled = True

        GamesListViewSorter.lv = GamesListView
        AutoscrollTextBox.Text = "90000000"
        ChatWebBrowser.Navigate("about:blank")
        ChatWebBrowser.ScriptErrorsSuppressed = True
        ChatWebBrowser.AllowNavigation = True
        ChatWebBrowser.IsWebBrowserContextMenuEnabled = False
        ChatWebBrowser.Document.Write(Globals.Announce.Replace("[CHATTEXTCOLOR]", Mid(Hex(My.Settings.ChatColorChatText), 3)).Replace("[HYPERLINKCOLOR]", Mid(Hex(My.Settings.ChatColorHyperlink), 3)))

        MiscWebBrowser.Navigate("about:blank")
        MiscWebBrowser.ScriptErrorsSuppressed = True
        MiscWebBrowser.AllowNavigation = True
        MiscWebBrowser.IsWebBrowserContextMenuEnabled = True

        For Each lang As String In [Enum].GetNames(GetType(Language.Main.Languages))
            LanguageComboBox.Items.Add(lang)
        Next

        ApplyPrefs()
        HookEvents()

        SelectedList = OnlineListView
        MainTabControl.Location = New Point(-4, -22)
        ChatSplitContainer_SplitterMoved(Nothing, Nothing)
        Me.Text = LobbyShared.Globals.ClientVersion
        Me.Refresh()
        If My.Settings.ShowIconInSystemTray = True Then
            TrayNotifyIcon.Visible = True
        End If

        AddHandler ChatWebBrowser.Document.MouseOver, AddressOf DisplayHyperlinks
        AddHandler ChatWebBrowser.Document.MouseDown, AddressOf ChatWebBroswer_Click

        For Each a As String In Network.Adapters.GetList
            NetworkAdapterComboBox.Items.Add(a)
        Next

        If NetworkAdapterComboBox.Items.Contains(Globals.DefaultAdapter) = True AndAlso Globals.DefaultAdapter <> "" Then
            NetworkAdapterComboBox.SelectedItem = Globals.DefaultAdapter
        Else
            For Each s As String In NetworkAdapterComboBox.Items
                Dim regex As New System.Text.RegularExpressions.Regex("\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")
                If regex.Match(s).Length > 0 Then
                    NetworkAdapterComboBox.SelectedItem = s
                    Globals.DefaultAdapter = s
                    Exit For
                End If
            Next
        End If

        GameLoopThread = New System.Threading.Thread(AddressOf DetectGameLoop)
        GameLoopThread.IsBackground = True
        GameLoopThread.Start()


        ' Start the raw packet sniffer that we use for locate
        Try
            RawSocket = New System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Raw, System.Net.Sockets.ProtocolType.IP)
            RawSocket.Bind(New System.Net.IPEndPoint(System.Net.IPAddress.Parse(Globals.DefaultAdapter), 0))

            Dim bytrue As Byte() = {1, 0, 0, 0}
            Dim nyout As Byte() = {0, 0, 0, 0}

            RawSocket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.IP, System.Net.Sockets.SocketOptionName.HeaderIncluded, True)
            RawSocket.IOControl(System.Net.Sockets.IOControlCode.ReceiveAll, bytrue, nyout)

            ' Get the first packet
            RawSocket.BeginReceive(buffer, 0, buffer.Length, System.Net.Sockets.SocketFlags.None, AddressOf SniffCallback, Nothing)
        Catch ex As Exception
        End Try

        ' See if I should vote?
        'Dim m As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.MiscMsg)
        'm.StringCollection("VOTEQUESTION") = "1"
        'm.StringCollection("FROM") = Globals.CurrentUser.Username
        'm.StringCollection("IP") = Globals.CurrentUser.PublicIP
        'For Each s As String In Globals.HardwareList.Keys
        '   If s.StartsWith("SID_") Then
        '       m.StringCollection("SID") = Globals.HardwareList(s)
        '   End If
        'Next
        'Globals.ClientEngine.SendPacket(m)
        ' Handler in ClientEngine ProcessMsg will do vote popup
    End Sub
    Private Sub LobbyTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LobbyTimer.Tick
        ' AFK handling
        If IdleTime.IdleTime >= 300 AndAlso Globals.CurrentUser.AFK = False Then
            ' Idle for 5 mins and you aren't already AFK, then set AFK
            Globals.CurrentUser.AFK = True
            Globals.ClientEngine.ChangeStatus()
            SetAFKToolStripMenuItem.Visible = False
            ClearAFKToolStripMenuItem.Visible = True
        ElseIf IdleTime.IdleTime < 300 AndAlso AFKToggleForce = False AndAlso Globals.CurrentUser.AFK = True Then
            ' Idle for less than 5 mins, didn't manually set AFK, and are AFK, then clear AFK
            Globals.CurrentUser.AFK = False
            Globals.ClientEngine.ChangeStatus()
            SetAFKToolStripMenuItem.Visible = True
            ClearAFKToolStripMenuItem.Visible = False
        End If

        ' Flood handling
        For i = 0 To FloodDetectionQueue.Count - 1
            Try
                If FloodDetectionQueue(i) > 60 Then
                    FloodDetectionQueue.RemoveAt(i)
                    i -= 1
                Else
                    FloodDetectionQueue(i) += 1
                End If
            Catch
            End Try
        Next
    End Sub
    Private Sub FormLobby_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        If Me.WindowState = FormWindowState.Minimized Then
            If My.Settings.MinimizeToSystemTray = True Then
                Me.Hide()
                Me.ShowInTaskbar = False
                TrayNotifyIcon.Visible = True
            End If
        Else
            PreviousWindowState = Me.WindowState
        End If
    End Sub
    Private Sub TrayNotifyIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrayNotifyIcon.MouseDoubleClick
        Me.Show()
        Me.ShowInTaskbar = True
        Try
            Me.WindowState = PreviousWindowState
        Catch
        End Try
        If Not My.Settings.ShowIconInSystemTray Then
            TrayNotifyIcon.Visible = False
        End If
        AppActivate(Me.Text)
    End Sub
    Private Sub ShowSaveEELobbyClientToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowSaveEELobbyClientToolStripMenuItem.Click
        TrayNotifyIcon_MouseDoubleClick(Nothing, Nothing)
    End Sub
    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub
    Private Sub FormLobby_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        OnlineListColumnHeader.Width = OnlineListView.Width - 20
        FriendsListColumnHeader.Width = FriendsListView.Width - 20
        CheatersListColumnHeader.Width = CheatersListView.Width - 20
        IgnoreColumnHeader.Width = IgnoreListView.Width - 20
    End Sub
    Private Sub ChatSplitContainer_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles ChatSplitContainer.SplitterMoved
        OnlineListColumnHeader.Width = OnlineListView.Width - 20
        FriendsListColumnHeader.Width = FriendsListView.Width - 20
        CheatersListColumnHeader.Width = CheatersListView.Width - 20
        IgnoreColumnHeader.Width = IgnoreListView.Width - 20
    End Sub
#End Region
#Region "Language"
    Private Sub UpdateLanguage()
        Try
            LobbyAdvancedButton.Text = Language.Lobby.Lobby
            PatchAdvancedButton.Text = Language.Lobby.Patch
            OptionsAdvancedButton.Text = Language.Lobby.Options
            HelpAdvancedButton.Text = Language.Lobby.Help
            TrainingAdvancedButton.Text = Language.Lobby.Training
            SaveEEAdvancedButton.Text = Language.Lobby.Save_EE
            DonationsAdvancedButton.Text = Language.Lobby.Donations
            LadderAdvancedButton.Text = "Ladder"
            FacebookAdvancedButton.Text = "Facebook"

            GamesOutlookPanel.HeaderText = Language.Lobby.Current_Games
            If GameNameColumnHeader.Text.StartsWith("< ") OrElse GameNameColumnHeader.Text.StartsWith("> ") Then
                GameNameColumnHeader.Text = GameColumnHeader.Text.Substring(0, 2) & Language.GamesList.Game_Name
            Else
                GameNameColumnHeader.Text = Language.GamesList.Game_Name
            End If
            If EpochColumnHeader.Text.StartsWith("< ") OrElse EpochColumnHeader.Text.StartsWith("> ") Then
                EpochColumnHeader.Text = EpochColumnHeader.Text.Substring(0, 2) & Language.GamesList.Epoch
            Else
                EpochColumnHeader.Text = Language.GamesList.Epoch
            End If
            If ResourcesColumnHeader.Text.StartsWith("< ") OrElse ResourcesColumnHeader.Text.StartsWith("> ") Then
                ResourcesColumnHeader.Text = ResourcesColumnHeader.Text.Substring(0, 2) & Language.GamesList.Resources
            Else
                ResourcesColumnHeader.Text = Language.GamesList.Resources
            End If
            If MapTypeColumnHeader.Text.StartsWith("< ") OrElse MapTypeColumnHeader.Text.StartsWith("> ") Then
                MapTypeColumnHeader.Text = MapTypeColumnHeader.Text.Substring(0, 2) & Language.GamesList.Map_Type
            Else
                MapTypeColumnHeader.Text = Language.GamesList.Map_Type
            End If
            If MapSizeColumnHeader.Text.StartsWith("< ") OrElse MapSizeColumnHeader.Text.StartsWith("> ") Then
                MapSizeColumnHeader.Text = MapSizeColumnHeader.Text.Substring(0, 2) & Language.GamesList.Map_Size
            Else
                MapSizeColumnHeader.Text = Language.GamesList.Map_Size
            End If
            If HostColumnHeader.Text.StartsWith("< ") OrElse HostColumnHeader.Text.StartsWith("> ") Then
                HostColumnHeader.Text = HostColumnHeader.Text.Substring(0, 2) & Language.GamesList.Host
            Else
                HostColumnHeader.Text = Language.GamesList.Host
            End If
            If IPColumnHeader.Text.StartsWith("< ") OrElse IPColumnHeader.Text.StartsWith("> ") Then
                IPColumnHeader.Text = IPColumnHeader.Text.Substring(0, 2) & Language.GamesList.IP
            Else
                IPColumnHeader.Text = Language.GamesList.IP
            End If
            If PlayersColumnHeader.Text.StartsWith("< ") OrElse PlayersColumnHeader.Text.StartsWith("> ") Then
                PlayersColumnHeader.Text = PlayersColumnHeader.Text.Substring(0, 2) & Language.GamesList.Players
            Else
                PlayersColumnHeader.Text = Language.GamesList.Players
            End If

            ChatOutlookPanel.HeaderText = Language.Lobby.Lobby_Chat
            ChatConsoleGroupBox.Text = Language.Lobby.Chat_Console
            ChatComboBox.Items.Clear()
            ChatComboBox.Items.Add(Language.Lobby.Chat)
            ChatComboBox.Items.Add(Language.Lobby.Whisper)
            If Globals.CurrentUser.Security >= LobbyShared.User.SecurityGroups.Moderator Then ChatComboBox.Items.Add(Language.Lobby.Warn)
            If Globals.CurrentUser.Security >= LobbyShared.User.SecurityGroups.SuperModerator Then ChatComboBox.Items.Add(Language.Lobby.Alert)
            ChatComboBox.SelectedIndex = 0
            SendButton.Text = Language.Lobby.Send

            UsersOutlookPanel.HeaderText = Language.Lobby.Users_Friends
            OnlineTabPage.Text = Language.Lobby.Online
            FriendsTabPage.Text = Language.Lobby.Friends
            CheatersTabPage.Text = Language.Lobby.Cheaters
            IgnoreTabPage.Text = Language.Lobby.Ignore
            OnlineListView.Groups("ADMINISTRATOR").Header = Language.Lobby.Administrators
            OnlineListView.Groups("MODERATOR").Header = Language.Lobby.Moderators
            OnlineListView.Groups("FRIEND").Header = Language.Lobby.Friends
            OnlineListView.Groups("DONATOR").Header = Language.Lobby.Donators
            OnlineListView.Groups("USER").Header = Language.Lobby.Users
            FriendsListView.Groups("ONLINE").Header = Language.Lobby.Online
            FriendsListView.Groups("OFFLINE").Header = Language.Lobby.Offline
            CheatersListView.Groups("ONLINE").Header = Language.Lobby.Online
            CheatersListView.Groups("OFFLINE").Header = Language.Lobby.Offline
            IgnoreListView.Groups("ONLINE").Header = Language.Lobby.Online
            IgnoreListView.Groups("OFFLINE").Header = Language.Lobby.Offline
            If OnlineListView.Items.Count = 1 Then
                UsersOnlineLabel.Text = Language.Lobby.Currently_1_User_Online
            Else
                UsersOnlineLabel.Text = Replace(Language.Lobby.Currently_X_Users_Online, "X", OnlineListView.Items.Count.ToString)
            End If

            CopyIPToolStripMenuItem.Text = Language.Menus.Copy_IP

            WhisperToolStripMenuItem.Text = Language.Menus.Whisper
            CopyNameToolStripMenuItem.Text = Language.Menus.Copy_Name
            AddFriendToolStripMenuItem.Text = Language.Menus.Add_to_Friends
            RemoveFriendToolStripMenuItem.Text = Language.Menus.Remove_from_Friends
            AddCheaterToolStripMenuItem.Text = Language.Menus.Add_to_Cheaters
            RemoveCheaterToolStripMenuItem.Text = Language.Menus.Remove_from_Cheaters
            IgnoreToolStripMenuItem.Text = Language.Menus.Ignore
            UnignoreToolStripMenuItem.Text = Language.Menus.Unignore
            SetAFKToolStripMenuItem.Text = Language.Menus.Set_AFK
            ClearAFKToolStripMenuItem.Text = Language.Menus.Clear_AFK
            ManualAddToolStripMenuItem.Text = Language.Menus.Manual_Add_to & "..."
            ManualFriendToolStripMenuItem.Text = Language.Menus.Friends
            ManualCheaterToolStripMenuItem.Text = Language.Menus.Cheaters
            ManualIgnoreToolStripMenuItem.Text = Language.Menus.Ignore
            ModeratorToolStripMenuItem.Text = Language.Menus.Moderator_Functions
            WarnToolStripMenuItem.Text = Language.Menus.Warn
            KickToolStripMenuItem.Text = Language.Menus.Kick
            MuteToolStripMenuItem.Text = Language.Menus.Mute
            ManualMuteToolStripMenuItem.Text = Language.Menus.Manual_Mute
            ListMutedToolStripMenuItem.Text = Language.Menus.List_Muted_Players
            LocateToolStripMenuItem.Text = Language.Menus.Locate
            GetDetailsToolStripMenuItem.Text = Language.Menus.Get_Details
            AdminFunctionsToolStripMenuItem.Text = Language.Menus.Admin_Functions
            ForceUpdateToolStripMenuItem.Text = Language.Menus.Force_Update
            BanToolStripMenuItem.Text = Language.Menus.Ban
            ManualBanToolStripMenuItem.Text = Language.Menus.Manual_Ban
            ListBannedPlayersToolStripMenuItem.Text = Language.Menus.List_Banned_Players
            PromoteToAdminToolStripMenuItem.Text = Language.Menus.Promote_to_Admin
            PromoteToModeratorToolStripMenuItem.Text = Language.Menus.Promote_to_Moderator
            PromoteToDonatorToolStripMenuItem.Text = Language.Menus.Promote_to_Donator
            DemoteToUserToolStripMenuItem.Text = Language.Menus.Demote_to_User

            ShowSaveEELobbyClientToolStripMenuItem.Text = Language.Main.Show & " Save-EE Lobby Client"
            ExitToolStripMenuItem.Text = Language.Main.Exit_

            PatchOutlookPanel.HeaderText = Language.Lobby.Patch
            'InstallDirOutlookPanel.HeaderText = Language.Patches.Install_Directories
            'InstallDirLabel.Text = Language.Patches.Install_Dir_Label
            'AutodetectButton.Text = Language.Patches.Autodetect_Install_Directories
            'EEDirLabel.Text = Language.Patches.Empire_Earth_Install_Dir
            'AoCDirLabel.Text = Language.Patches.Art_of_Conquest_Install_Dir
            'EEDirBrowseButton.Text = Language.Patches.Browse
            'AoCDirBrowseButton.Text = Language.Patches.Browse
            'DCPatchOutlookPanel.HeaderText = Language.Patches.Direct_Connect_Patch
            'DCErrorLabel.Text = Language.Patches.Direct_Conn_Label
            'DCPatchButton.Text = Language.Patches.Apply_Direct_Connect_Patch

            OptionsOutlookPanel.HeaderText = Language.Lobby.Options
            NetworkDetailsOutlookPanel.HeaderText = Language.Options.Network_Details
            NetworkAdapterLabel.Text = Language.Options.Please_Select_Network_Adapter
            ChatOptionsOutlookPanel.HeaderText = Language.Options.Chat_Options
            AutoscrollCheckBox.Text = Language.Options.Autoscroll
            RevertToChatCheckBox.Text = Language.Options.Revert_To_Chat_After_Whisper
            EnableSoundsCheckBox.Text = Language.Options.Enable_Sounds
            ShowTimestampsCheckBox.Text = Language.Options.Show_Timestamps
            MiscOptionsOutlookPanel.HeaderText = Language.Options.Miscellaneous_Options
            AutoResizeGameColumnsCheckBox.Text = Language.Options.Automatically_Resize_Game_Columns
            MaximizeLobbyOnLoginCheckBox.Text = Language.Options.Maximize_Lobby_On_Login
            ShowIconInSystemTrayCheckBox.Text = Language.Options.Show_Icon_In_System_Tray
            MinimizeToSystemTrayCheckBox.Text = Language.Options.Minimize_To_System_Tray
            LanguageLabel.Text = Language.Options.Language
            VisualStylesOutlookPanel.HeaderText = Language.Options.Visual_Styles
            VisualStylesCheckBox.Text = Language.Options.Enable_Visual_Styles
            ButtonColorsOutlookPanel.HeaderText = Language.Options.Button_Colors
            ButtonColorTopGroupBox.Text = Language.Options.Top_Gradient
            ButtonColorBottomGroupBox.Text = Language.Options.Bottom_Gradient
            ChatColorsOutlookPanel.HeaderText = Language.Options.Chat_Colors
            BackgroundColorGroupBox.Text = Language.Options.Background
            HyperlinkGroupBox.Text = Language.Options.Hyperlink
            UserLinkGroupBox.Text = Language.Options.User_Link
            ChatTextGroupBox.Text = Language.Options.Chat_Text
            EmoteGroupBox.Text = Language.Options.Emote_Text
            WhisperTextGroupBox.Text = Language.Options.Whisper_Text
            AlertTextGroupBox.Text = Language.Options.Alert_Text
            WarningTextGroupBox.Text = Language.Options.Warning_Text
            ServerText1GroupBox.Text = Language.Options.Server_Text_1
            ServerText2GroupBox.Text = Language.Options.Server_Text_2

            MiscWBBackButton.Text = Language.Lobby.Back
            MiscWBForwardButton.Text = Language.Lobby.Forward

            Me.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region
#Region "Hook/UnHook Events"
    Public Sub HookEvents()
        AddHandler Globals.ClientEngine.ConnectionLost, AddressOf OnConnectionLost
        AddHandler Globals.ClientEngine.UserAdd, AddressOf OnUserAdd
        AddHandler Globals.ClientEngine.UserRemove, AddressOf OnUserRemove

        AddHandler Globals.ClientEngine.OnChangeUserRights, AddressOf OnChangeUserRights

        AddHandler Globals.ClientEngine.FriendList, AddressOf OnFriendList
        AddHandler Globals.ClientEngine.FriendAdd, AddressOf OnFriendAdd
        AddHandler Globals.ClientEngine.FriendRemove, AddressOf OnFriendRemove

        AddHandler Globals.ClientEngine.CheaterList, AddressOf OnCheaterList
        AddHandler Globals.ClientEngine.CheaterAdd, AddressOf OnCheaterAdd
        AddHandler Globals.ClientEngine.CheaterRemove, AddressOf OnCheaterRemove

        AddHandler Globals.ClientEngine.IgnoreList, AddressOf OnIgnoreList
        AddHandler Globals.ClientEngine.IgnoreAdd, AddressOf OnIgnoreAdd
        AddHandler Globals.ClientEngine.IgnoreRemove, AddressOf OnIgnoreRemove


        AddHandler Globals.ClientEngine.OnLobbyChat, AddressOf OnLobbyChat
        AddHandler Globals.ClientEngine.OnLobbyWhisper, AddressOf OnLobbyWhisper
        AddHandler Globals.ClientEngine.OnLobbyWarn, AddressOf OnLobbyWarn
        AddHandler Globals.ClientEngine.OnLobbyAlert, AddressOf OnLobbyAlert

        AddHandler Globals.ClientEngine.OnGetKeyList, AddressOf OnGetKeyList
        AddHandler Globals.ClientEngine.OnChangeStatus, AddressOf OnChangeStatus

        AddHandler Globals.ClientEngine.OnGameAdd, AddressOf OnGameAdd
        AddHandler Globals.ClientEngine.OnGameRemove, AddressOf OnGameRemove
        AddHandler Globals.ClientEngine.MiscMsgReturn, AddressOf OnMiscMsgReturn
        AddHandler Globals.ClientEngine.OnLocate, AddressOf OnLocate
    End Sub
    Public Sub UnHookEvents()
        RemoveHandler Globals.ClientEngine.ConnectionLost, AddressOf OnConnectionLost
        RemoveHandler Globals.ClientEngine.UserAdd, AddressOf OnUserAdd
        RemoveHandler Globals.ClientEngine.UserRemove, AddressOf OnUserRemove

        RemoveHandler Globals.ClientEngine.OnChangeUserRights, AddressOf OnChangeUserRights

        RemoveHandler Globals.ClientEngine.FriendList, AddressOf OnFriendList
        RemoveHandler Globals.ClientEngine.FriendAdd, AddressOf OnFriendAdd
        RemoveHandler Globals.ClientEngine.FriendRemove, AddressOf OnFriendRemove

        RemoveHandler Globals.ClientEngine.CheaterList, AddressOf OnCheaterList
        RemoveHandler Globals.ClientEngine.CheaterAdd, AddressOf OnCheaterAdd
        RemoveHandler Globals.ClientEngine.CheaterRemove, AddressOf OnCheaterRemove

        RemoveHandler Globals.ClientEngine.IgnoreList, AddressOf OnIgnoreList
        RemoveHandler Globals.ClientEngine.IgnoreAdd, AddressOf OnIgnoreAdd
        RemoveHandler Globals.ClientEngine.IgnoreRemove, AddressOf OnIgnoreRemove


        RemoveHandler Globals.ClientEngine.OnLobbyChat, AddressOf OnLobbyChat
        RemoveHandler Globals.ClientEngine.OnLobbyWhisper, AddressOf OnLobbyWhisper
        RemoveHandler Globals.ClientEngine.OnLobbyWarn, AddressOf OnLobbyWarn
        RemoveHandler Globals.ClientEngine.OnLobbyAlert, AddressOf OnLobbyAlert

        RemoveHandler Globals.ClientEngine.OnGetKeyList, AddressOf OnGetKeyList
        RemoveHandler Globals.ClientEngine.OnChangeStatus, AddressOf OnChangeStatus

        RemoveHandler Globals.ClientEngine.OnGameAdd, AddressOf OnGameAdd
        RemoveHandler Globals.ClientEngine.OnGameRemove, AddressOf OnGameRemove
        RemoveHandler Globals.ClientEngine.MiscMsgReturn, AddressOf OnMiscMsgReturn
        RemoveHandler Globals.ClientEngine.OnLocate, AddressOf OnLocate
    End Sub
    Private Sub OnConnectionLost()
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NoParams(AddressOf OnConnectionLost))
        Else
            UnHookEvents()
            'MsgBox("You Connection To The Server Was Terminated!", MsgBoxStyle.Critical, "Error, Connection Lost")
            Application.Exit()
        End If
    End Sub
#End Region
#Region "OtherWebBrowserStuff"
    Public Sub DisplayHyperlinks(ByVal sender As Object, ByVal e As System.Windows.Forms.HtmlElementEventArgs)
        Try
            Dim clink As String = e.ToElement.GetAttribute("innertext")
            ilink = clink
        Catch ex As Exception
        End Try
    End Sub
    Public Sub ChatWebBroswer_Click(ByVal sender As Object, ByVal e As HtmlElementEventArgs)
        Try
            If OnlineListView.Items.ContainsKey(ilink) Then
                UsersTabControl.SelectedTab = OnlineTabPage
                SelectedList = OnlineListView
                OnlineListView.SelectedItems.Clear()
                OnlineListView.Items(ilink).Selected = True
                UsersTabControl.SelectedTab = OnlineTabPage
                UsersContextMenuStrip.Show(Me, Cursor.Position.X - Me.Left, Cursor.Position.Y - Me.Top - 25)
                'ilink = ""
            End If
        Catch ex As Exception
            MsgBox("Chat Click Error")
        End Try
    End Sub
    Private Sub ChatWebBrowser_Navigating(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles ChatWebBrowser.Navigating
        If AllowNav Then
            AllowNav = False
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub MiscWBBackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiscWBBackButton.Click
        MiscWebBrowser.GoBack()
    End Sub
    Private Sub MiscWBForwardButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiscWBForwardButton.Click
        MiscWebBrowser.GoForward()
    End Sub
    Private Sub MiscWBAddressTextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MiscWBAddressTextBox.KeyPress
        If (e.KeyChar = Chr(13) OrElse e.KeyChar = Chr(10)) AndAlso ControlDown = False Then
            MiscWebBrowser.Navigate(MiscWBAddressTextBox.Text)
        End If
    End Sub
    Private Sub MiscWebBrowser_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles MiscWebBrowser.DocumentCompleted
        MiscWBAddressTextBox.Text = MiscWebBrowser.Url.ToString
    End Sub
#End Region
#Region "Locate handling"
    Public Sub SniffCallback(ByVal ir As IAsyncResult)
        Dim received As Integer = 0
        Dim pkt(0) As Byte

        Try
            received = RawSocket.EndReceive(ir)
            ReDim pkt(received - 1)
            Array.Copy(buffer, pkt, pkt.Length)
        Catch ex As Exception
        End Try

        ' So we have the packet, lets figure out what it is
        Try
            ' WONLobby uses TCP, host's port is 33336, when doing join game requests
            ' UDP is only used for refresh/queries

            ' Is protocol TCP?
            If pkt(9) = 6 Then
                ' Where does game data start?
                Dim ipHeaderLength As Integer = (pkt(0) And &HF) * 4
                Dim tcpHeaderLength As Integer = (pkt(ipHeaderLength + 12) And &HF0) / 4
                Dim dataStart As Integer = ipHeaderLength + tcpHeaderLength

                ' Get the source and destination IPs and Ports
                Dim sourceAddress As String = pkt(12) & "." & pkt(13) & "." & pkt(14) & "." & pkt(15)
                Dim destinationAddress As String = pkt(16) & "." & pkt(17) & "." & pkt(18) & "." & pkt(19)
                Dim sourcePort As Integer = pkt(ipHeaderLength) * 256 + pkt(ipHeaderLength + 1)
                Dim destinationPort As Integer = pkt(ipHeaderLength + 2) * 256 + pkt(ipHeaderLength + 3)

                ' Client detection (you are joining)
                If sourceAddress = Globals.DefaultAdapter AndAlso destinationPort = 33336 Then
                    ' I am sending a packet to the host

                    ' Is it the join request?
                    If pkt(dataStart + 2) = 1 Then
                        ' Get username
                        Dim temp((pkt(dataStart + 3) * 2) - 1) As Byte
                        Array.Copy(pkt, dataStart + 5, temp, 0, temp.Length)

                        ' Save for later
                        ClientLastLocateGameIP = destinationAddress
                        ClientLastLocateUsername = System.Text.Encoding.Unicode.GetString(temp)
                        'MsgBox(ClientLastLocateUsername & " | " & destinationAddress)

                        ' Ok so we have the name, don't report it yet because we need to make sure you get in
                    End If
                ElseIf destinationAddress = Globals.DefaultAdapter AndAlso sourcePort = 33336 Then
                    ' Host is sending me a packet

                    ' Is it ok to join?
                    If pkt(dataStart + 1) = 0 AndAlso pkt(dataStart + 2) = 8 AndAlso pkt(dataStart + 3) = 2 AndAlso pkt(dataStart + 4) = 0 AndAlso pkt(dataStart + 5) = 0 Then
                        ' Make sure the host's IP matches last locate attempt
                        If sourceAddress = ClientLastLocateGameIP Then
                            ' Report to server
                            'MsgBox(ClientLastLocateUsername & " | " & sourceAddress)
                            Globals.ClientEngine.JoinGame(ClientLastLocateGameIP, ClientLastLocateUsername)
                        End If
                    End If
                End If

                ' Host detection (you are hosting)
                If sourceAddress = Globals.DefaultAdapter AndAlso sourcePort = 33336 Then
                    ' You are sending to a client

                    ' Is it ok to join?
                    If pkt(dataStart + 1) = 0 AndAlso pkt(dataStart + 2) = 8 AndAlso pkt(dataStart + 3) = 2 AndAlso pkt(dataStart + 4) = 0 AndAlso pkt(dataStart + 5) = 0 Then
                        ' Make sure the host's IP matches last locate attempt
                        If destinationAddress = HostLastLocateUserIP Then
                            ' Report to server
                            'MsgBox(HostLastLocateUsername & " | " & destinationAddress)
                            Globals.ClientEngine.JoinGame2(Globals.CurrentUser.PublicIP, HostLastLocateUsername, HostLastLocateUserIP)
                        End If
                    End If
                ElseIf destinationAddress = Globals.DefaultAdapter AndAlso destinationPort = 33336 Then
                    ' Client is sending to you

                    ' Is it the join request?
                    If pkt(dataStart + 2) = 1 Then
                        ' Get username
                        Dim temp((pkt(dataStart + 3) * 2) - 1) As Byte
                        Array.Copy(pkt, dataStart + 5, temp, 0, temp.Length)

                        ' Save for later
                        HostLastLocateUserIP = sourceAddress
                        HostLastLocateUsername = System.Text.Encoding.Unicode.GetString(temp)
                        'MsgBox(HostLastLocateUsername & " | " & sourceAddress)

                        ' Ok so we have the name, don't report it yet because we need to make sure you get in
                    End If
                End If
            End If
        Catch ex As Exception
        End Try

        ' Get another packet
        RawSocket.BeginReceive(buffer, 0, buffer.Length, Net.Sockets.SocketFlags.None, AddressOf SniffCallback, Nothing)
    End Sub
    Private Sub OnLocate(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnLocate), New Object() {m})
        Else
            For Each s As String In m.StringCollection.Values
                WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & s.Replace("<", "&lt;").Replace(">", "&gt;") & "</span>")
            Next
        End If
    End Sub
#End Region
#Region "Status, Key, Misc handling"
    Private Sub UpdateListViewUser(ByVal username As String)
        ' After removing a name from a listview
        ' Update group and/or status

        ' User logged off?
        If Not OnlineListView.Items.ContainsKey(username) Then
            ' See if friend, change to Offline group
            If FriendsListView.Items.ContainsKey(username) Then
                FriendsListView.Items(username).Group = FriendsListView.Groups("OFFLINE")
                FriendsListView.Items(username).ImageKey = "friends_offline.png"
            End If
            If IgnoreListView.Items.ContainsKey(username) Then
                IgnoreListView.Items(username).ImageKey = "ignore.png"
            End If
        Else
            ' User is online

            ' Get true status from IgnoreListView if ignored
            Dim isIgnored As Boolean = False
            If IgnoreListView.Items.ContainsKey(username) Then
                ' ... only if not ignore icon ...
                If Not IgnoreListView.Items(username).ImageKey.StartsWith("ignore") Then
                    OnlineListView.Items(username).ImageKey = IgnoreListView.Items(username).ImageKey
                End If
                isIgnored = True
            End If
            ' then recalculate icon as normal, and apply ignore icon again later

            ' See if friend, change to Online group
            Dim isFriend As Boolean = False
            If FriendsListView.Items.ContainsKey(username) Then
                FriendsListView.Items(username).Group = FriendsListView.Groups("ONLINE")
                FriendsListView.Items(username).ImageKey = "friends.png"
                isFriend = True
            End If

            ' Find out if donator
            Dim isDonator As Boolean = False
            If OnlineListView.Items(username).ImageKey.Contains("_add.png") OrElse OnlineListView.Items(username).ImageKey = "donator.png" Then
                isDonator = True
            End If

            ' Find out if we should put it in FRIEND, DONATOR, or USER group, only if not staff member
            If Not OnlineListView.Items(username).Group.Name = "ADMINISTRATOR" _
                    AndAlso Not OnlineListView.Items(username).Group.Name = "MODERATOR" Then
                If isFriend Then
                    OnlineListView.Items(username).Group = OnlineListView.Groups("FRIEND")
                    ' status only gets changed if currently donator.png or user.png
                    If OnlineListView.Items(username).ImageKey = "donator.png" Then
                        OnlineListView.Items(username).ImageKey = "friends_add.png"
                    ElseIf OnlineListView.Items(username).ImageKey = "user.png" Then
                        OnlineListView.Items(username).ImageKey = "friends.png"
                    End If
                ElseIf isDonator Then
                    OnlineListView.Items(username).Group = OnlineListView.Groups("DONATOR")
                    If OnlineListView.Items(username).ImageKey = "friends_add.png" Then
                        OnlineListView.Items(username).ImageKey = "donator.png"
                    End If
                Else
                    OnlineListView.Items(username).Group = OnlineListView.Groups("USER")
                    If OnlineListView.Items(username).ImageKey = "friends.png" Then
                        OnlineListView.Items(username).ImageKey = "user.png"
                    End If
                End If
            End If

            ' Do more ignore stuff
            If isIgnored Then
                ' Set the ignore list image to the true status
                If Not OnlineListView.Items(username).ImageKey.StartsWith("ignore") Then
                    ' ... only if not ignore icon ...
                    IgnoreListView.Items(username).ImageKey = OnlineListView.Items(username).ImageKey
                End If
                ' then apply ignore icon to online list
                OnlineListView.Items(username).ImageKey = If(isDonator, "ignore_add.png", "ignore.png")
            Else
                ' Not ignored but icon is still stuck on ignore
                ' We probably don't know true status so just get the default group icon right at least
                ' (We technically shouldn't ever get here)
                If OnlineListView.Items(username).ImageKey.StartsWith("ignore") Then
                    If OnlineListView.Items(username).Group.Name = "ADMINISTRATOR" Then
                        OnlineListView.Items(username).ImageKey = "Admin.png"
                    ElseIf OnlineListView.Items(username).Group.Name = "MODERATOR" Then
                        OnlineListView.Items(username).ImageKey = "Moderator.png"
                    ElseIf OnlineListView.Items(username).Group.Name = "FRIEND" Then
                        OnlineListView.Items(username).ImageKey = If(isDonator, "friends_add.png", "friends.png")
                    ElseIf OnlineListView.Items(username).Group.Name = "DONATOR" Then
                        OnlineListView.Items(username).ImageKey = "donator.png"
                    ElseIf OnlineListView.Items(username).Group.Name = "USER" Then
                        OnlineListView.Items(username).ImageKey = "user.png"
                    Else
                        OnlineListView.Items(username).ImageKey = "user.png"
                    End If
                End If
            End If
        End If

        ' Sort the lists because it never does when you change groups
        OnlineListView.Sort()
        FriendsListView.Sort()
        IgnoreListView.Sort()
    End Sub
    Private Sub OnChangeStatus(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnChangeStatus), New Object() {m})
        Else
            Dim username As String = m.StringCollection("FROM")
            Dim statusIcon As String = m.StringCollection("ICON")
            Dim groupName As String = m.StringCollection("GROUP")

            If OnlineListView.Items.ContainsKey(username) Then
                ' Set icon
                If IgnoreListView.Items.ContainsKey(username) Then
                    If FriendsListView.Items.ContainsKey(username) AndAlso statusIcon = "user.png" Then
                        IgnoreListView.Items(username).ImageKey = "friends.png"
                    ElseIf FriendsListView.Items.ContainsKey(username) AndAlso statusIcon = "donator.png" Then
                        IgnoreListView.Items(username).ImageKey = "friends_add.png"
                    Else
                        IgnoreListView.Items(username).ImageKey = statusIcon
                    End If

                    If groupName = "DONATOR" Then
                        OnlineListView.Items(username).ImageKey = "ignore_add.png"
                    Else
                        OnlineListView.Items(username).ImageKey = "ignore.png"
                    End If
                Else
                    If FriendsListView.Items.ContainsKey(username) AndAlso statusIcon = "user.png" Then
                        OnlineListView.Items(username).ImageKey = "friends.png"
                    ElseIf FriendsListView.Items.ContainsKey(username) AndAlso statusIcon = "donator.png" Then
                        OnlineListView.Items(username).ImageKey = "friends_add.png"
                    Else
                        OnlineListView.Items(username).ImageKey = statusIcon
                    End If
                End If

                ' Set group
                If FriendsListView.Items.ContainsKey(username) AndAlso (groupName = "USER" OrElse groupName = "DONATOR") Then
                    OnlineListView.Items(username).Group = OnlineListView.Groups("FRIEND")
                Else
                    OnlineListView.Items(username).Group = OnlineListView.Groups(groupName)
                End If
            End If
        End If
    End Sub
    Private Sub OnChangeUserRights(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnChangeUserRights), New Object() {m})
        Else
            If m.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.User Then
                Globals.CurrentUser.Security = LobbyShared.User.SecurityGroups.User
                ToolStripSeparator2.Visible = False
                AdminFunctionsToolStripMenuItem.Visible = False
                ModeratorToolStripMenuItem.Visible = False
                ChatTextBox.MaxLength = 250
                ChatComboBox.Items.Clear()
                ChatComboBox.Items.Add(Language.Lobby.Chat)
                ChatComboBox.Items.Add(Language.Lobby.Whisper)
                ChatComboBox.SelectedIndex = 0
                OnlineListView.MultiSelect = False
                FriendsListView.MultiSelect = False
                CheatersListView.MultiSelect = False
                IgnoreListView.MultiSelect = False
            ElseIf m.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.Donator Then
                Globals.CurrentUser.Security = LobbyShared.User.SecurityGroups.Donator
                ToolStripSeparator2.Visible = False
                AdminFunctionsToolStripMenuItem.Visible = False
                ModeratorToolStripMenuItem.Visible = False
                ChatTextBox.MaxLength = 1000
                ChatComboBox.Items.Clear()
                ChatComboBox.Items.Add(Language.Lobby.Chat)
                ChatComboBox.Items.Add(Language.Lobby.Whisper)
                ChatComboBox.SelectedIndex = 0
                OnlineListView.MultiSelect = True
                FriendsListView.MultiSelect = True
                CheatersListView.MultiSelect = True
                IgnoreListView.MultiSelect = True
            ElseIf m.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.Moderator Then
                Globals.CurrentUser.Security = LobbyShared.User.SecurityGroups.Moderator
                ToolStripSeparator2.Visible = True
                AdminFunctionsToolStripMenuItem.Visible = False
                ModeratorToolStripMenuItem.Visible = True
                ChatTextBox.MaxLength = 32000
                ChatComboBox.Items.Clear()
                ChatComboBox.Items.Add(Language.Lobby.Chat)
                ChatComboBox.Items.Add(Language.Lobby.Whisper)
                ChatComboBox.Items.Add(Language.Lobby.Warn)
                ChatComboBox.SelectedIndex = 0
                OnlineListView.MultiSelect = True
                FriendsListView.MultiSelect = True
                CheatersListView.MultiSelect = True
                IgnoreListView.MultiSelect = True
                FloodDetectionQueue.Clear()
            ElseIf m.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.SuperModerator Then
                Globals.CurrentUser.Security = LobbyShared.User.SecurityGroups.SuperModerator
                ToolStripSeparator2.Visible = True
                AdminFunctionsToolStripMenuItem.Visible = False
                ModeratorToolStripMenuItem.Visible = True
                ChatTextBox.MaxLength = 32000
                ChatComboBox.Items.Clear()
                ChatComboBox.Items.Add(Language.Lobby.Chat)
                ChatComboBox.Items.Add(Language.Lobby.Whisper)
                ChatComboBox.Items.Add(Language.Lobby.Warn)
                ChatComboBox.Items.Add(Language.Lobby.Alert)
                ChatComboBox.SelectedIndex = 0
                OnlineListView.MultiSelect = True
                FriendsListView.MultiSelect = True
                CheatersListView.MultiSelect = True
                IgnoreListView.MultiSelect = True
                FloodDetectionQueue.Clear()
            ElseIf m.StringCollection("SECURITY") = LobbyShared.User.SecurityGroups.Administrator Then
                Globals.CurrentUser.Security = LobbyShared.User.SecurityGroups.Administrator
                ToolStripSeparator2.Visible = True
                AdminFunctionsToolStripMenuItem.Visible = True
                ModeratorToolStripMenuItem.Visible = True
                ChatTextBox.MaxLength = 32000
                ChatComboBox.Items.Clear()
                ChatComboBox.Items.Add(Language.Lobby.Chat)
                ChatComboBox.Items.Add(Language.Lobby.Whisper)
                ChatComboBox.Items.Add(Language.Lobby.Warn)
                ChatComboBox.Items.Add(Language.Lobby.Alert)
                ChatComboBox.SelectedIndex = 0
                OnlineListView.MultiSelect = True
                FriendsListView.MultiSelect = True
                CheatersListView.MultiSelect = True
                IgnoreListView.MultiSelect = True
                FloodDetectionQueue.Clear()
            End If
        End If
    End Sub
    Private Sub OnGetKeyList(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnGetKeyList), New Object() {m})
        Else
            Dim keyform As New FormKeyList
            keyform.Action = m.StringCollection("ACTION")
            m.StringCollection.Remove("ACTION")
            If keyform.Action = "MUTE" OrElse keyform.Action = "BAN" Then
                For Each k As String In m.StringCollection.Keys
                    Dim l As New ListViewItem
                    l.Name = k
                    l.Text = k
                    l.SubItems.Add(m.StringCollection(k))
                    keyform.KeyListView.Items.Add(l)
                Next
            Else
                For Each k As String In m.ListCollection.Keys
                    Dim l As New ListViewItem
                    l.Name = k
                    l.Text = k
                    l.SubItems.Add(m.ListCollection(k)(0))
                    l.SubItems.Add(m.ListCollection(k)(1))
                    l.SubItems.Add(m.ListCollection(k)(2))
                    l.SubItems.Add(m.ListCollection(k)(3))
                    l.SubItems.Add(m.ListCollection(k)(4))
                    l.SubItems.Add(m.ListCollection(k)(5))
                    keyform.KeyListView.Items.Add(l)
                Next
            End If
            keyform.Show()
        End If
    End Sub
    Private Sub OnMiscMsgReturn(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnMiscMsgReturn), New Object() {m})
        Else
            If m.StringCollection.ContainsKey("VOTEQUESTION") Then
                Dim v As New FormVote
                v.Show()
                Exit Sub
            End If

            If m.StringCollection.ContainsKey("IMAGE") Then
                Dim ff As New FormSnap
                Dim mm As System.IO.MemoryStream
                mm = LobbyShared.Globals.UnSerializeObjectFromByteArray(Convert.FromBase64String(m.StringCollection("IMAGE")))

                Dim b As Bitmap = Bitmap.FromStream(mm)

                ff.PictureBox1.Image = b.Clone
                ff.PictureBox1.Refresh()
                ff.Text = mm.Length
                ff.Show()
                b.Dispose()
                mm.Close()
                mm.Dispose()
                Exit Sub
            End If

            Dim x As New FormKeyList
            Dim t As LobbyShared.NetworkMessage.MsgTable = m.TableCollection("USERNAMES")
            x.KeyListView.Items.Add("---KNOWN USER NAMES---")
            For l As Integer = 0 To t.Data.Count - 1
                x.KeyListView.Items.Add(t.GetData(l, "username"))
            Next
            Try
                Dim tt As LobbyShared.NetworkMessage.MsgTable = m.TableCollection("PROC")
                x.KeyListView.Items.Add("---PROCESSES---")
                For l As Integer = 0 To tt.Data.Count - 1
                    x.KeyListView.Items.Add(tt.GetData(l, "proc"))
                Next
            Catch ex As Exception
            End Try
            x.Show()
        End If
    End Sub
#End Region
#Region "User/Friend/Cheater/Ignore List handling and functions"
    Private Sub OnUserAdd(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnUserAdd), New Object() {m})
        Else
            Dim t As LobbyShared.NetworkMessage.MsgTable = m.TableCollection("users")

            For i As Integer = 0 To t.Data.Count - 1
                If Not OnlineListView.Items.ContainsKey(t.GetData(i, "username")) Then
                    Dim lvi As New ListViewItem
                    lvi.Name = t.GetData(i, "username")
                    lvi.Text = t.GetData(i, "username")
                    lvi.ImageKey = t.GetData(i, "status")
                    lvi.Group = OnlineListView.Groups(t.GetData(i, "group"))

                    OnlineListView.Items.Add(lvi)
                    UpdateListViewUser(lvi.Name)

                    ' Alert of joining server
                    If Not IsUserList Then
                        If FriendsListView.Items.ContainsKey(lvi.Name) Then
                            WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & (New String(lvi.Name).Replace("<", "&lt;").Replace(">", "&gt;")) & " " & Language.ChatMsgs.has_joined_the_server & ".</span>")
                            If My.Settings.EnableSounds Then My.Computer.Audio.Play(My.Resources.FriendJoined, AudioPlayMode.Background)
                        End If
                    End If
                End If
            Next

            ' Update # of online users displayed
            If OnlineListView.Items.Count = 1 Then
                UsersOnlineLabel.Text = Language.Lobby.Currently_1_User_Online
            Else
                UsersOnlineLabel.Text = Replace(Language.Lobby.Currently_X_Users_Online, "X", OnlineListView.Items.Count.ToString)
            End If
            IsUserList = False
        End If
    End Sub
    Private Sub OnUserRemove(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnUserRemove), New Object() {m})
        Else
            If OnlineListView.Items.ContainsKey(m.StringCollection("user")) Then
                OnlineListView.Items.RemoveByKey(m.StringCollection("user"))
                UpdateListViewUser(m.StringCollection("user"))

                ' Alert of leaving server
                If FriendsListView.Items.ContainsKey(m.StringCollection("user")) Then
                    WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & m.StringCollection("user").Replace("<", "&lt;").Replace(">", "&gt;") & " " & Language.ChatMsgs.has_left_the_server & ".</span>")
                    If My.Settings.EnableSounds Then My.Computer.Audio.Play(My.Resources.FriendLeft, AudioPlayMode.Background)
                End If

                ' Update # of online users displayed
                If OnlineListView.Items.Count = 1 Then
                    UsersOnlineLabel.Text = Language.Lobby.Currently_1_User_Online
                Else
                    UsersOnlineLabel.Text = Replace(Language.Lobby.Currently_X_Users_Online, "X", OnlineListView.Items.Count.ToString)
                End If
            End If
        End If
    End Sub
    Private Sub OnFriendList(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnFriendList), New Object() {m})
        Else
            For Each f As String In m.StringCollection.Keys
                If Not FriendsListView.Items.ContainsKey(f) Then
                    Dim l As New ListViewItem
                    l.Name = f
                    l.Text = f
                    FriendsListView.Items.Add(l)
                End If
                UpdateListViewUser(f)
            Next
        End If
    End Sub
    Private Sub OnFriendAdd(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnFriendAdd), New Object() {m})
        Else
            For Each s As String In m.StringCollection.Keys
                ' Set up chat output, starting with username
                Dim output As String = "<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">"
                ' ... then the result, with language support!
                If m.StringCollection(s) = "ADDED" Then
                    output &= Language.ChatMsgs.was_added_to_your_friends_list
                ElseIf m.StringCollection(s) = "ALREADY" Then
                    output &= Language.ChatMsgs.is_already_on_your_friends_list
                ElseIf m.StringCollection(s) = "NOT_FOUND" Then
                    output &= Language.ChatMsgs.was_not_found_in_the_user_database
                End If
                ' Replace [NAME]
                output = Replace(output, "[NAME]", New String(s).Replace("<", "&lt;").Replace(">", "&gt;"))
                ' ... then the period and closing HTML
                output &= ".</span>"
                ' do it!
                WriteLineToChat(output)

                ' Modify ListViews
                If m.StringCollection(s) <> "NOT_FOUND" Then
                    If FriendsListView.Items.ContainsKey(s) = False Then
                        Dim lvi As New ListViewItem
                        lvi.Name = s
                        lvi.Text = s
                        FriendsListView.Items.Add(lvi)
                    End If
                    UpdateListViewUser(s)
                End If
            Next
        End If
    End Sub
    Private Sub OnFriendRemove(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnFriendRemove), New Object() {m})
        Else
            For Each s As String In m.StringCollection.Keys
                ' Set up chat output, starting with username
                Dim output As String = "<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">"
                ' ... then the result, with language support!
                If m.StringCollection(s) = "REMOVED" Then
                    output &= Language.ChatMsgs.was_removed_from_your_friends_list
                ElseIf m.StringCollection(s) = "NOT_FOUND" Then
                    output &= Language.ChatMsgs.was_not_found_on_your_friends_list
                End If
                ' Replace [NAME]
                output = Replace(output, "[NAME]", New String(s).Replace("<", "&lt;").Replace(">", "&gt;"))
                ' ... then the period and closing HTML
                output &= ".</span>"
                ' do it!
                WriteLineToChat(output)

                ' Modify ListViews
                If FriendsListView.Items.ContainsKey(s) Then
                    FriendsListView.Items.RemoveByKey(s)
                End If
                UpdateListViewUser(s)
            Next
        End If
    End Sub
    Private Sub OnCheaterList(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnCheaterList), New Object() {m})
        Else
            For Each f As String In m.StringCollection.Keys
                If Not CheatersListView.Items.ContainsKey(f) Then
                    Dim l As New ListViewItem
                    l.Name = f
                    l.Text = f
                    l.ImageKey = "cheater.png"
                    CheatersListView.Items.Add(l)
                End If
            Next
        End If
    End Sub
    Private Sub OnCheaterAdd(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnCheaterAdd), New Object() {m})
        Else
            For Each s As String In m.StringCollection.Keys
                ' Set up chat output, starting with username
                Dim output As String = "<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">"
                ' ... then the result, with language support!
                If m.StringCollection(s) = "ADDED" Then
                    output &= Language.ChatMsgs.was_added_to_your_cheaters_list
                ElseIf m.StringCollection(s) = "ALREADY" Then
                    output &= Language.ChatMsgs.is_already_on_your_cheaters_list
                ElseIf m.StringCollection(s) = "NOT_FOUND" Then
                    output &= Language.ChatMsgs.was_not_found_in_the_user_database
                End If
                ' Replace [NAME]
                output = Replace(output, "[NAME]", New String(s).Replace("<", "&lt;").Replace(">", "&gt;"))
                ' ... then the period and closing HTML
                output &= ".</span>"
                ' do it!
                WriteLineToChat(output)

                ' Modify ListViews
                If m.StringCollection(s) <> "NOT_FOUND" Then
                    If Not CheatersListView.Items.ContainsKey(s) Then
                        Dim lvi As New ListViewItem
                        lvi.Name = s
                        lvi.Text = s
                        lvi.ImageKey = "cheater.png"
                        CheatersListView.Items.Add(lvi)
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub OnCheaterRemove(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnCheaterRemove), New Object() {m})
        Else
            For Each s As String In m.StringCollection.Keys
                ' Set up chat output, starting with username
                Dim output As String = "<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">"
                ' ... then the result, with language support!
                If m.StringCollection(s) = "REMOVED" Then
                    output &= Language.ChatMsgs.was_removed_from_your_cheaters_list
                ElseIf m.StringCollection(s) = "NOT_FOUND" Then
                    output &= Language.ChatMsgs.was_not_found_on_your_cheaters_list
                End If
                ' Replace [NAME]
                output = Replace(output, "[NAME]", New String(s).Replace("<", "&lt;").Replace(">", "&gt;"))
                ' ... then the period and closing HTML
                output &= ".</span>"
                ' do it!
                WriteLineToChat(output)

                ' Modify ListViews
                If CheatersListView.Items.ContainsKey(s) Then
                    CheatersListView.Items.RemoveByKey(s)
                End If
            Next
        End If
    End Sub
    Private Sub OnIgnoreList(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnIgnoreList), New Object() {m})
        Else
            For Each f As String In m.StringCollection.Keys
                If Not IgnoreListView.Items.ContainsKey(f) Then
                    Dim l As New ListViewItem
                    l.Name = f
                    l.Text = f
                    l.ImageKey = "ignore.png"
                    IgnoreListView.Items.Add(l)
                End If
                UpdateListViewUser(f)
            Next
        End If
    End Sub
    Private Sub OnIgnoreAdd(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnIgnoreAdd), New Object() {m})
        Else
            For Each s As String In m.StringCollection.Keys
                ' Set up chat output, starting with username
                Dim output As String = "<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">"
                ' ... then the result, with language support!
                If m.StringCollection(s) = "ADDED" Then
                    output &= Language.ChatMsgs.was_added_to_your_ignore_list
                ElseIf m.StringCollection(s) = "ALREADY" Then
                    output &= Language.ChatMsgs.is_already_on_your_ignore_list
                ElseIf m.StringCollection(s) = "NOT_FOUND" Then
                    output &= Language.ChatMsgs.was_not_found_in_the_user_database
                End If
                ' Replace [NAME]
                output = Replace(output, "[NAME]", New String(s).Replace("<", "&lt;").Replace(">", "&gt;"))
                ' ... then the period and closing HTML
                output &= ".</span>"
                ' do it!
                WriteLineToChat(output)

                ' Modify ListViews
                If m.StringCollection(s) <> "NOT_FOUND" Then
                    If Not IgnoreListView.Items.ContainsKey(s) Then
                        Dim lvi As New ListViewItem
                        lvi.Name = s
                        lvi.Text = s
                        lvi.ImageKey = "ignore.png"
                        IgnoreListView.Items.Add(lvi)
                    End If
                    UpdateListViewUser(s)
                End If
            Next
        End If
    End Sub
    Private Sub OnIgnoreRemove(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnIgnoreRemove), New Object() {m})
        Else
            For Each s As String In m.StringCollection.Keys
                ' Set up chat output, starting with username
                Dim output As String = "<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">"
                ' ... then the result, with language support!
                If m.StringCollection(s) = "REMOVED" Then
                    output &= Language.ChatMsgs.was_removed_from_your_ignore_list
                ElseIf m.StringCollection(s) = "NOT_FOUND" Then
                    output &= Language.ChatMsgs.was_not_found_on_your_ignore_list
                End If
                ' Replace [NAME]
                output = Replace(output, "[NAME]", New String(s).Replace("<", "&lt;").Replace(">", "&gt;"))
                ' ... then the period and closing HTML
                output &= ".</span>"
                ' do it!
                WriteLineToChat(output)

                ' Modify ListViews
                If OnlineListView.Items.ContainsKey(s) Then
                    OnlineListView.Items(s).ImageKey = IgnoreListView.Items(s).ImageKey
                End If
                If IgnoreListView.Items.ContainsKey(s) Then
                    IgnoreListView.Items.RemoveByKey(s)
                End If
                UpdateListViewUser(s)
            Next
        End If
    End Sub
    Private Sub UsersTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles UsersTabControl.SelectedIndexChanged
        If UsersTabControl.SelectedIndex = 0 Then
            SelectedList = OnlineListView
        ElseIf UsersTabControl.SelectedIndex = 1 Then
            SelectedList = FriendsListView
        ElseIf UsersTabControl.SelectedIndex = 2 Then
            SelectedList = CheatersListView
        ElseIf UsersTabControl.SelectedIndex = 3 Then
            SelectedList = IgnoreListView
        End If
    End Sub
#End Region
#Region "Games list handling and functions"
    Public Sub DetectGameLoop()
        ' cce = cheat codes enabled
        ' gip = game in progress
        ' p prefix = previous

        Dim PreviousGameSettings As New Dictionary(Of String, String)
        Dim IsHostingGame As Boolean = False
        Dim GameInProgress As Boolean = False

        Dim eeProcess As Process = Nothing
        Dim aocProcess As Process = Nothing
        Dim eeHandle As Integer = 0
        Dim aocHandle As Integer = 0
        Dim peeGIP As Boolean = False
        Dim paocGIP As Boolean = False

        Dim SendNoGame As Boolean = False

        While GameLoopAbort = False
            ' Start off with simply looking for WONLobby hosted game
            Try
                Dim s As New System.Net.Sockets.UdpClient()
                s.Client.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.ReceiveTimeout, 500)

                s.Connect(Globals.DefaultAdapter, 33336)
                Dim b() As Byte = {7, 6, 0, 3, 3}
                s.Send(b, b.Length)

                Dim feedback() As Byte = s.Receive(New System.Net.IPEndPoint(System.Net.IPAddress.Parse(Globals.DefaultAdapter), 33336))
                Dim GameNameLength As Integer = feedback(7) * 2
                Dim StartEpoch As Integer = GameNameLength + 7 + 15
                Dim EndEpoch As Integer = GameNameLength + 7 + 16
                Dim MapSize As Integer = GameNameLength + 7 + 12
                Dim MapType As Integer = GameNameLength + 7 + 13
                Dim StartingResources As Integer = GameNameLength + 7 + 14
                Dim Players As Integer = feedback(GameNameLength + 7 + 4)
                Dim HostEENameStart As Integer = GameNameLength + 7 + 29
                Dim HostEENameLength As Integer = feedback(HostEENameStart) * 2
                Dim hn((HostEENameLength) - 1) As Byte
                Array.Copy(feedback, HostEENameStart + 2, hn, 0, hn.Length)
                Dim gn((GameNameLength) - 1) As Byte
                Array.Copy(feedback, 9, gn, 0, gn.Length)

                Dim CurGameData As New Dictionary(Of String, String)
                CurGameData("VERSION") = feedback(4)
                CurGameData("HOST") = Globals.CurrentUser.Username
                CurGameData("GAMENAME") = System.Text.UnicodeEncoding.Unicode.GetString(gn)
                CurGameData("STARTEPOCH") = feedback(StartEpoch)
                CurGameData("ENDEPOCH") = feedback(EndEpoch)
                CurGameData("MAPTYPE") = feedback(MapType)
                CurGameData("MAPSIZE") = feedback(MapSize)
                CurGameData("STARTRESOURCE") = feedback(StartingResources)
                CurGameData("IP") = Globals.CurrentUser.PublicIP
                CurGameData("PLAYERS") = Players
                CurGameData("INPROGRESS") = "FALSE"
                CurGameData("HOSTEENAME") = System.Text.Encoding.Unicode.GetString(hn)

                If LobbyShared.Dictionary.CompareDictionary(CurGameData, PreviousGameSettings) = False Then
                    Dim tbl As New LobbyShared.NetworkMessage.MsgTable
                    tbl.AddRow(New String() {"VERSION", "HOST", "GAMENAME", "STARTEPOCH", "ENDEPOCH", "MAPTYPE", "MAPSIZE", "STARTRESOURCE", "IP", "PLAYERS", "INPROGRESS", "HOSTEENAME"})
                    tbl.AddRow(New String() {CurGameData("VERSION"), CurGameData("HOST"), CurGameData("GAMENAME"), CurGameData("STARTEPOCH"), CurGameData("ENDEPOCH"), CurGameData("MAPTYPE"), CurGameData("MAPSIZE"), CurGameData("STARTRESOURCE"), CurGameData("IP"), CurGameData("PLAYERS"), CurGameData("INPROGRESS"), CurGameData("HOSTEENAME")})

                    LobbyShared.Dictionary.CloneDictionary(CurGameData, PreviousGameSettings)
                    Globals.ClientEngine.AddGame(tbl)
                End If
                IsHostingGame = True
                s.Close()

            Catch ex As Exception
                ' Can no longer get a UDP response from the WONLobby hosted game
                '   so let's see if it's in progress
                Dim tcp As New System.Net.Sockets.TcpClient
                Try
                    tcp.SendTimeout = 3000
                    tcp.ReceiveTimeout = 3000
                    tcp.Connect(Globals.DefaultAdapter, 33336)
                    If tcp.Connected AndAlso IsHostingGame Then
                        Dim namebuf() As Byte = {13, 0, 1, 3, 0, 58, 0, 59, 0, 60, 0, 0, 0}
                        tcp.GetStream.Write(namebuf, 0, namebuf.Length)
                        Dim nameresp(600) As Byte
                        tcp.GetStream.Read(nameresp, 0, 601)
                        Dim verbuf() As Byte = {&H20, &H0, &H8, &H1, &H3E, &H0, &HA, &H0, &H76, &H0, &H31, &H0, &H2E, &H0, &H30, &H0, &H30, &H0, &H2E, &H0, &H32, &H0, &H36, &H0, &H35, &H0, &H37, &H0, &H61, &H4A, &H2B, &HE}
                        tcp.GetStream.Write(verbuf, 0, verbuf.Length)
                        Dim verresp(600) As Byte
                        tcp.GetStream.Read(verresp, 0, 601)
                        If verresp(1) = 0 AndAlso verresp(2) = 8 AndAlso verresp(3) = 2 AndAlso verresp(4) = 3 Then
                            If GameInProgress = False AndAlso PreviousGameSettings.Count > 0 Then
                                IsHostingGame = True
                                GameInProgress = True
                                PreviousGameSettings("INPROGRESS") = "TRUE"
                                Dim tbl As New LobbyShared.NetworkMessage.MsgTable
                                tbl.AddRow(New String() {"VERSION", "HOST", "GAMENAME", "STARTEPOCH", "ENDEPOCH", "MAPTYPE", "MAPSIZE", "STARTRESOURCE", "IP", "PLAYERS", "INPROGRESS", "HOSTEENAME"})
                                tbl.AddRow(New String() {PreviousGameSettings("VERSION"), PreviousGameSettings("HOST"), PreviousGameSettings("GAMENAME"), PreviousGameSettings("STARTEPOCH"), PreviousGameSettings("ENDEPOCH"), PreviousGameSettings("MAPTYPE"), PreviousGameSettings("MAPSIZE"), PreviousGameSettings("STARTRESOURCE"), PreviousGameSettings("IP"), PreviousGameSettings("PLAYERS"), PreviousGameSettings("INPROGRESS"), PreviousGameSettings("HOSTEENAME")})
                                Globals.ClientEngine.AddGame(tbl)
                            End If
                        End If
                    End If
                Catch ex2 As Exception
                    ' No TCP connection, so that means the game is over and we can remove it
                    tcp = Nothing
                    If IsHostingGame Then
                        Globals.ClientEngine.RemoveGame(Globals.CurrentUser.PublicIP)
                        IsHostingGame = False
                        GameInProgress = False
                        PreviousGameSettings.Clear()
                    End If
                End Try
            End Try

            '
            ' All done with game listings
            '
            ' Find out whether a player is in game by reading memory
            ' Cheat code stuff removed
            Try
                eeProcess = Process.GetProcessesByName("EMPIRE EARTH")(0)
                If eeHandle = 0 Then
                    eeHandle = WIN32MEMORY.OpenProcess(WIN32MEMORY.PROCESS_READ_WRITE_QUERY, 0, eeProcess.Id)
                End If
            Catch ex As Exception
                eeProcess = Nothing
                WIN32MEMORY.CloseHandle(eeHandle)
                eeHandle = 0
            End Try
            Try
                aocProcess = Process.GetProcessesByName("EE-AOC")(0)
                If aocHandle = 0 Then
                    aocHandle = WIN32MEMORY.OpenProcess(WIN32MEMORY.PROCESS_READ_WRITE_QUERY, 0, aocProcess.Id)
                End If
            Catch ex As Exception
                aocProcess = Nothing
                WIN32MEMORY.CloseHandle(aocHandle)
                aocHandle = 0
            End Try

            If Not eeProcess Is Nothing Then
                SendNoGame = True
                Dim GIPB1 As Byte() = {0}

                WIN32MEMORY.ReadProcessMemory(eeHandle, &H9128C8, GIPB1, 1, 1)

                If GIPB1(0) = 1 Then
                    Globals.CurrentUser.InGameEE = True
                Else
                    Globals.CurrentUser.InGameEE = False
                End If

                If Globals.CurrentUser.InGameEE <> peeGIP Then
                    Globals.ClientEngine.ChangeStatus()
                    peeGIP = Globals.CurrentUser.InGameEE
                End If
            End If
            If Not aocProcess Is Nothing Then
                SendNoGame = True
                Dim GIPB1 As Byte() = {0}

                WIN32MEMORY.ReadProcessMemory(aocHandle, &H929064, GIPB1, 1, 1)

                If GIPB1(0) = 1 Then
                    Globals.CurrentUser.InGameAoC = True
                Else
                    Globals.CurrentUser.InGameAoC = False
                End If

                If Globals.CurrentUser.InGameAoC <> paocGIP Then
                    Globals.ClientEngine.ChangeStatus()
                    paocGIP = Globals.CurrentUser.InGameAoC
                End If
            End If
            If eeProcess Is Nothing AndAlso aocProcess Is Nothing Then
                If SendNoGame = True Then
                    Globals.CurrentUser.InGameEE = False
                    Globals.CurrentUser.InGameAoC = False
                    Globals.ClientEngine.ChangeStatus()
                End If
                SendNoGame = False
            End If

            Threading.Thread.Sleep(3000)
        End While
    End Sub
    Private Sub OnGameAdd(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnGameAdd), New Object() {m})
        Else
            Try
                Dim mt As LobbyShared.NetworkMessage.MsgTable = m.TableCollection("GAMEDATA")
                For cr As Integer = 0 To mt.Data.Count - 1

                    Dim gt As GameDetailsParser.GameType
                    If Trim(mt.GetData(cr, "VERSION")) = "31" Then
                        gt = GameDetailsParser.GameType.AOC
                    ElseIf Trim(mt.GetData(cr, "VERSION")) = "121" Then
                        gt = GameDetailsParser.GameType.AOC
                    ElseIf Trim(mt.GetData(cr, "VERSION")) = "73" Then
                        gt = GameDetailsParser.GameType.EEC
                    Else
                        gt = GameDetailsParser.GameType.UNKNOWN
                    End If

                    Dim gip As String = ""
                    Try
                        gip = mt.GetData(cr, "INPROGRESS")
                    Catch ex As Exception

                    End Try

                    If GamesListView.Items.ContainsKey(mt.GetData(cr, "IP")) Then
                        Dim lvi As ListViewItem = GamesListView.Items(mt.GetData(cr, "IP"))
                        If gip = "TRUE" Then
                            lvi.ForeColor = Color.Gray
                        ElseIf gip = "FALSE" Then
                            lvi.ForeColor = Color.White
                        End If

                        If gt = GameDetailsParser.GameType.AOC Then
                            'lvi.Text = "AOC"
                            lvi.ImageKey = "eeaoc.png"
                        ElseIf gt = GameDetailsParser.GameType.EEC Then
                            'lvi.Text = "EEC"
                            lvi.ImageKey = "ee.png"
                        ElseIf gt = GameDetailsParser.GameType.UNKNOWN Then
                            lvi.Text = "?" & mt.GetData(cr, "VERSION") & "?"
                        End If

                        lvi.SubItems(1).Text = mt.GetData(cr, "GAMENAME")

                        If mt.GetData(cr, "STARTEPOCH") = mt.GetData(cr, "ENDEPOCH") Then
                            lvi.SubItems(2).Text = GameDetailsParser.GetEpochName(mt.GetData(cr, "STARTEPOCH"), gt)
                        Else
                            lvi.SubItems(2).Text = GameDetailsParser.GetEpochName(mt.GetData(cr, "STARTEPOCH"), gt) & " -> " & GameDetailsParser.GetEpochName(mt.GetData(cr, "ENDEPOCH"), gt)
                        End If

                        lvi.SubItems(3).Text = GameDetailsParser.GetStartingResources(mt.GetData(cr, "STARTRESOURCE"), gt)
                        lvi.SubItems(4).Text = GameDetailsParser.GetMapType(mt.GetData(cr, "MAPTYPE"), gt)
                        lvi.SubItems(5).Text = GameDetailsParser.GetMapSize(mt.GetData(cr, "MAPSIZE"), gt)
                        lvi.SubItems(6).Text = mt.GetData(cr, "HOST")
                        lvi.SubItems(7).Text = mt.GetData(cr, "IP")
                        lvi.SubItems(8).Text = mt.GetData(cr, "PLAYERS")
                    Else
                        Dim lvi As New ListViewItem
                        If gip = "TRUE" Then
                            lvi.ForeColor = Color.Gray
                        End If

                        lvi.Name = mt.GetData(cr, "IP")

                        If gt = GameDetailsParser.GameType.AOC Then
                            'lvi.Text = "AOC"
                            lvi.ImageKey = "eeaoc.png"
                        ElseIf gt = GameDetailsParser.GameType.EEC Then
                            'lvi.Text = "EEC"
                            lvi.ImageKey = "ee.png"
                        ElseIf gt = GameDetailsParser.GameType.UNKNOWN Then
                            lvi.Text = "?" & mt.GetData(cr, "VERSION") & "?"
                        End If


                        lvi.SubItems.Add(mt.GetData(cr, "GAMENAME"))

                        If mt.GetData(cr, "STARTEPOCH") = mt.GetData(cr, "ENDEPOCH") Then
                            lvi.SubItems.Add(GameDetailsParser.GetEpochName(mt.GetData(cr, "STARTEPOCH"), gt))
                        Else
                            lvi.SubItems.Add(GameDetailsParser.GetEpochName(mt.GetData(cr, "STARTEPOCH"), gt) & " -> " & GameDetailsParser.GetEpochName(mt.GetData(cr, "ENDEPOCH"), gt))
                        End If

                        lvi.SubItems.Add(GameDetailsParser.GetStartingResources(mt.GetData(cr, "STARTRESOURCE"), gt))
                        lvi.SubItems.Add(GameDetailsParser.GetMapType(mt.GetData(cr, "MAPTYPE"), gt))
                        lvi.SubItems.Add(GameDetailsParser.GetMapSize(mt.GetData(cr, "MAPSIZE"), gt))
                        lvi.SubItems.Add(mt.GetData(cr, "HOST"))
                        lvi.SubItems.Add(mt.GetData(cr, "IP"))
                        lvi.SubItems.Add(mt.GetData(cr, "PLAYERS"))
                        GamesListView.Items.Add(lvi)
                    End If
                Next

                If My.Settings.AutoResizeGameColumns Then
                    Dim cnt As Integer = -1
                    For Each c As ColumnHeader In GamesListView.Columns
                        cnt += 1
                        c.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
                        If cnt <> 0 Then If c.Width < 60 Then c.Width = 60
                    Next
                End If
            Catch ex As Exception
                'Clipboard.SetText(ex.Message)
            End Try
        End If
    End Sub
    Private Sub OnGameRemove(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnGameRemove), New Object() {m})
        Else
            If GamesListView.Items.ContainsKey(m.StringCollection("IP")) Then
                GamesListView.Items.RemoveByKey(m.StringCollection("IP"))
                If My.Settings.AutoResizeGameColumns Then
                    Dim cnt As Integer = -1
                    For Each c As ColumnHeader In GamesListView.Columns
                        cnt += 1
                        c.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
                        If cnt <> 0 Then If c.Width < 60 Then c.Width = 60
                    Next
                End If
            End If
        End If
    End Sub
    Private Sub GamesListView_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles GamesListView.ColumnClick
        GamesListViewSorter.SortByColumn(e.Column)
    End Sub
    Private Sub CopyIPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyIPToolStripMenuItem.Click
        Try
            Clipboard.SetText(GamesListView.SelectedItems(0).SubItems(7).Text)
        Catch ex As Exception
            MsgBox("Clipboard Error.", MsgBoxStyle.Critical, "Error!")
        End Try
    End Sub
#End Region
#Region "Chat handling and functions"
    Public Sub WriteToChat(ByVal s As String)
        ''textbox1.Text = 'textbox1.Text & vbCrLf & "wb.Document.Body.ScrollTop" & ":" & wb.Document.Body.ScrollTop
        ''textbox1.Text = 'textbox1.Text & vbCrLf & "wb.Size.Height" & ":" & wb.Size.Height

        Dim x As Integer = ChatWebBrowser.Document.Body.ScrollTop + ChatWebBrowser.Size.Height

        Dim shouldscroll As Boolean = False
        If x >= ChatWebBrowser.Document.Body.ScrollRectangle.Height - 25 Then
            shouldscroll = True
        End If

        ChatWebBrowser.Document.Write(s)
        'ChatWebBrowser.DocumentStream.Flush()
        If shouldscroll AndAlso AutoscrollCheckBox.Checked = True Then ChatWebBrowser.Document.Window.ScrollTo(0, AutoscrollTextBox.Text)
    End Sub
    Public Sub WriteLineToChat(ByVal s As String)
        Dim line As String
        line = "<table width=""100%""><tr><td>"
        If My.Settings.ShowTimestamps Then
            line &= "<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText2), 3) & ";"">[" & Now.ToString("HH:mm:ss") & "]&nbsp;</span>"
        End If
        line &= s
        line &= "</td></tr></table>"
        WriteToChat(line)
    End Sub
    Private Sub ParseBadWords(ByRef input As String)
        If My.Settings.EnableBadLanguageFilter Then
            For Each word As String In Globals.BadWords.Keys
                ' Parsing is inline and does not look at words individually
                If InStr(input.ToLower, word.ToLower, CompareMethod.Text) > 0 Then
                    ' These 3 are Capitalized, CAPS, lower replacements
                    input = Replace(input, word, Globals.BadWords(word))
                    input = Replace(input, word.ToUpper, Globals.BadWords(word).ToUpper)
                    input = Replace(input, word.ToLower, Globals.BadWords(word).ToLower)
                    ' Now do case insensitive for what's remaining, replace with lowercase
                    input = Strings.Replace(input, word, Globals.BadWords(word).ToLower, 1, -1, CompareMethod.Text)
                End If
                ' If the entire message is a word this will detect words in badwords.dat that have leading and trailing spaces
                If input.ToUpper = word.ToUpper.Trim Then
                    input = Globals.BadWords(word).Trim
                    Exit For
                End If
            Next
        End If
    End Sub
    Private Sub ParseSmilies(ByRef input As String)
        If My.Settings.EnableSmilies Then
            For Each smilie As String In Globals.Smileys.Keys
                If InStr(input, smilie, CompareMethod.Text) > 0 Then
                    input = Replace(input, smilie, "<img src=""http://www.save-ee.com/images/smilies/" & Globals.Smileys(smilie) & """ title=""" & smilie & """ alt=""" & smilie & """>")
                End If
            Next
        End If
    End Sub
    Private Sub OnLobbyChat(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnLobbyChat), New Object() {m})
        Else
            ParseBadWords(m.StringCollection("TEXT"))
            ParseSmilies(m.StringCollection("TEXT"))

            Dim t As String
            t = "<a style=""color: " & Mid(Hex(My.Settings.ChatColorUserLink), 3) & ";"" href=""javascript:void(0)"">" & m.StringCollection("FROM").Replace("<", "&lt;").Replace(">", "&gt;") & "</a>"
            t &= "<span style=""color: "
            If m.StringCollection("TEXT").StartsWith("/e ") Then
                t &= Mid(Hex(My.Settings.ChatColorEmoteText), 3) & ";"">&nbsp;" & m.StringCollection("TEXT").Substring(3) & "</span>"
            Else
                t &= Mid(Hex(My.Settings.ChatColorChatText), 3) & ";"">:&nbsp;" & m.StringCollection("TEXT") & "</span>"
            End If

            WriteLineToChat(t)
        End If
    End Sub
    Private Sub OnLobbyWhisper(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnLobbyWhisper), New Object() {m})
        Else
            LastWhisper = m.StringCollection("FROM")
            ParseBadWords(m.StringCollection("TEXT"))
            ParseSmilies(m.StringCollection("TEXT"))

            Try
                If My.Settings.EnableSounds Then My.Computer.Audio.Play(My.Resources.WhisperRecv, AudioPlayMode.Background)
            Catch ex As Exception
            End Try

            Dim t As String
            t = "<a style=""color:" & Mid(Hex(My.Settings.ChatColorUserLink), 3) & ";"" href=""javascript:void(0)"">" & m.StringCollection("FROM").Replace("<", "&lt;").Replace(">", "&gt;") & "</a>"
            t &= "<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">&nbsp;" & Language.ChatMsgs.whispers_to_you & ":&nbsp;</span>"
            t &= "<span style=""color: " & Mid(Hex(My.Settings.ChatColorWhisperText), 3) & ";"">" & m.StringCollection("TEXT") & "</span>"

            WriteLineToChat(t)
        End If
    End Sub
    Private Sub OnLobbyWarn(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnLobbyWarn), New Object() {m})
        Else
            ParseBadWords(m.StringCollection("TEXT"))
            ParseSmilies(m.StringCollection("TEXT"))

            Try
                If My.Settings.EnableSounds Then My.Computer.Audio.Play(My.Resources._Error, AudioPlayMode.Background)
            Catch ex As Exception
            End Try
            Dim t As String

            t = "<span style=""color: " & Mid(Hex(My.Settings.ChatColorWarningText), 3) & ";"">" & Language.ChatMsgs.WARNING_from & "&nbsp;</span>"
            t &= "<a style="" color: " & Mid(Hex(My.Settings.ChatColorUserLink), 3) & ";"" href=""javascript:void(0)"">" & m.StringCollection("FROM").Replace("<", "&lt;").Replace(">", "&gt;") & "</a>"
            t &= "<span style=""color: " & Mid(Hex(My.Settings.ChatColorWarningText), 3) & ";"">" & ":&nbsp;" & m.StringCollection("TEXT") & "</span>"

            WriteLineToChat(t)
        End If
    End Sub
    Private Sub OnLobbyAlert(ByVal m As LobbyShared.NetworkMessage)
        If Me.InvokeRequired Then
            Me.Invoke(New LobbyShared.Delegates.NetworkMsgDelegate(AddressOf OnLobbyAlert), New Object() {m})
        Else
            ParseBadWords(m.StringCollection("TEXT"))
            ParseSmilies(m.StringCollection("TEXT"))

            Try
                If My.Settings.EnableSounds Then My.Computer.Audio.Play(My.Resources._Error, AudioPlayMode.Background)
            Catch ex As Exception
            End Try

            Dim t As String
            t = "<span style=""color: " & Mid(Hex(My.Settings.ChatColorAlertText), 3) & ";"">" & Language.ChatMsgs.ALERT_from & "&nbsp;</span>"
            t &= "<a style=""color: " & Mid(Hex(My.Settings.ChatColorUserLink), 3) & ";"" href=""javascript:void(0)"">" & m.StringCollection("FROM").Replace("<", "&lt;").Replace(">", "&gt;") & "</a>"
            t &= "<span style=""color: " & Mid(Hex(My.Settings.ChatColorAlertText), 3) & ";"">" & ":&nbsp;" & m.StringCollection("TEXT") & "</span>"

            WriteLineToChat(t)
        End If
    End Sub
    Private Sub SendButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendButton.Click
        Dim chat As String = ChatTextBox.Text
        If chat.ToUpper = "/CLEAR" OrElse chat.ToUpper = "/CLR" OrElse chat.ToUpper = "/CLS" Then
            ChatWebBrowser.Document.Body.InnerHtml = " "
            ChatTextBox.Text = ""
            Exit Sub
        ElseIf chat.ToUpper = "/CHAT" Then
            ChatComboBox.SelectedIndex = 0
            ChatTextBox.Text = ""
            Exit Sub
        ElseIf chat.ToUpper.StartsWith("/CHAT ") Then
            ' Trim off command with space
            chat = chat.Substring(chat.IndexOf(" ") + 1)
            ChatComboBox.SelectedIndex = 0
        ElseIf chat.ToUpper = "/?" OrElse chat.ToUpper = "/H" OrElse chat.ToUpper = "/HELP" Then
            WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorWarningText), 3) & ";"">" & _
                        "Chat Commands:<br><ul style=""margin: 10px;"">" & _
                        "/? (or /h or /help) : <span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText2), 3) & ";"">Displays this help.</span><br>" & _
                        "/w (or /t or /tell or /whisper) ""playername"" : <span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText2), 3) & ";"">Sends a private message to ""playername"".</span><br>" & _
                        "/warn ""playername"" : <span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText2), 3) & ";"">Sends a warning message to ""playername"" (moderators only).</span><br>" & _
                        "/r (or /reply) [&lt;optional message&gt;] : <span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText2), 3) & ";"">Replys to the last person who whispered to you.</span><br>" & _
                        "/e (or /em or /emote or /me) &lt;action&gt; : <span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText2), 3) & ";"">Emotes the &lt;action&gt; as if you had done it.</span><br>" & _
                        "/i (or /ignore) [""playername""] : <span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText2), 3) & ";"">Ignores all chat from ""playername"".  Displays the ignore list if no playername is specified.</span><br>" & _
                        "/afk (or /away) : <span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText2), 3) & ";"">Toggles your away from keyboard status.</span><br>" & _
                        "/clr (or /clear or /cls) : <span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText2), 3) & ";"">Clears the chat buffer.</span><br>" & _
                        "/chat [&lt;optional message&gt;] : <span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText2), 3) & ";"">Reverts to lobby chat if previously set to whisper.</span>" & _
                        "</ul></span>")
            ChatTextBox.Text = ""
            Exit Sub
        ElseIf chat.ToUpper = "/AFK" OrElse chat.ToUpper = "/AWAY" Then
            If Globals.CurrentUser.AFK Then
                ClearAFKToolStripMenuItem_Click(Nothing, Nothing)
            Else
                SetAFKToolStripMenuItem_Click(Nothing, Nothing)
            End If
            ChatTextBox.Text = ""
            Exit Sub
        ElseIf chat.ToUpper.StartsWith("/E ") OrElse chat.ToUpper.StartsWith("/EM ") OrElse chat.ToUpper.StartsWith("/EMOTE ") OrElse chat.ToUpper.StartsWith("/ME") Then
            ' Trim off command with space
            chat = chat.Substring(chat.IndexOf(" ") + 1)
            ' Reinsert the /e, which will be parsed when displayed in the chat (OnLobbyChat subroutine)
            chat = "/e " & chat
        ElseIf chat.ToUpper = "/I" OrElse chat.ToUpper = "/IGNORE" Then
            Dim list As String = ""
            For Each x As ListViewItem In IgnoreListView.Items
                list &= x.Name & ", "
            Next
            ' Remove last ", "
            If list.Length() > 2 Then
                list = list.Substring(0, list.Length() - 2)
            End If
            WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & Language.ChatMsgs.Ignore_List & ":&nbsp;</span><span style=""color: " & Mid(Hex(My.Settings.ChatColorChatText), 3) & ";"">" & list & "</span>")
            ChatTextBox.Text = ""
            Exit Sub
        ElseIf chat.ToUpper.StartsWith("/I """) OrElse chat.ToUpper.StartsWith("/IGNORE """) Then
            ' Trim off command with space and the following "
            chat = chat.Substring(chat.IndexOf("""") + 1)
            ' Find 2nd " that marks end of name
            Dim endOfName As String = chat.IndexOf("""")
            ' If not present, invalid command
            If endOfName = -1 Then
                WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & Language.ChatMsgs.Invalid_command & ".</span>")
                ChatTextBox.Text = ""
                Exit Sub
            End If
            ' Set name
            Dim ignoreName As String = chat.Substring(0, endOfName)
            ' If empty name, invalid command
            If ignoreName = "" Then
                WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & Language.ChatMsgs.Invalid_command & ".</span>")
                ChatTextBox.Text = ""
                Exit Sub
            End If
            ' Trim chat
            chat = Mid(chat, endOfName + 3)
            Globals.ClientEngine.AddIgnore(New String() {ignoreName})
            ChatTextBox.Text = ""
            Exit Sub
        ElseIf chat.ToUpper.StartsWith("/W """) OrElse chat.ToUpper.StartsWith("/T """) OrElse chat.ToUpper.StartsWith("/WHISPER """) OrElse chat.ToUpper.StartsWith("/TELL """) Then
            ' Trim off command with space and the following "
            chat = chat.Substring(chat.IndexOf("""") + 1)
            ' Find 2nd " that marks end of name
            Dim endOfName As String = chat.IndexOf("""")
            ' If not present, invalid command
            If endOfName = -1 Then
                WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & Language.ChatMsgs.Invalid_command & ".</span>")
                ChatTextBox.Text = ""
                Exit Sub
            End If
            ' Set name
            Dim whisperName As String = chat.Substring(0, endOfName)
            ' If empty name, invalid command
            If whisperName = "" Then
                WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & Language.ChatMsgs.Invalid_command & ".</span>")
                ChatTextBox.Text = ""
                Exit Sub
            End If
            ' Trim chat
            chat = Mid(chat, endOfName + 3)
            ' Clear old names and select new name
            If OnlineListView.Items.ContainsKey(whisperName) Then
                OnlineListView.SelectedItems.Clear()
                OnlineListView.Items(whisperName).Selected = True
                ChatComboBox.SelectedIndex = 1
            Else
                ChatTextBox.Text = ""
                Exit Sub
            End If
        ElseIf chat.ToUpper.StartsWith("/WARN """) Then
            ' If normal user, tell them they can't use warn and exit sub
            If Not Globals.CurrentUser.Security >= LobbyShared.User.SecurityGroups.Moderator Then
                WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & Language.ChatMsgs.Insufficient_rights_for_this_command & ".</span>")
                ChatTextBox.Text = ""
                Exit Sub
            End If
            ' Trim off command with space and the following "
            chat = chat.Substring(chat.IndexOf("""") + 1)
            ' Find 2nd " that marks end of name
            Dim endOfName As String = chat.IndexOf("""")
            ' If not present, invalid command
            If endOfName = -1 Then
                WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & Language.ChatMsgs.Invalid_command & ".</span>")
                ChatTextBox.Text = ""
                Exit Sub
            End If
            ' Set name
            Dim warnName As String = chat.Substring(0, endOfName)
            ' If empty name, invalid command
            If warnName = "" Then
                WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & Language.ChatMsgs.Invalid_command & ".</span>")
                ChatTextBox.Text = ""
                Exit Sub
            End If
            ' Trim chat
            chat = Mid(chat, endOfName + 3)
            ' Clear old names and select new name
            If OnlineListView.Items.ContainsKey(warnName) Then
                OnlineListView.SelectedItems.Clear()
                OnlineListView.Items(warnName).Selected = True
                ChatComboBox.SelectedIndex = 2
            Else
                ChatTextBox.Text = ""
                Exit Sub
            End If
        ElseIf chat.ToUpper = "/R" OrElse chat.ToUpper = "/REPLY" Then
            If OnlineListView.Items.ContainsKey(LastWhisper) Then
                OnlineListView.SelectedItems.Clear()
                OnlineListView.Items(LastWhisper).Selected = True
                ChatComboBox.SelectedIndex = 1
            End If
            ChatTextBox.Text = ""
            Exit Sub
        ElseIf chat.ToUpper.StartsWith("/R ") OrElse chat.ToUpper.StartsWith("/REPLY ") Then
            ' Trim off command with space
            chat = chat.Substring(chat.IndexOf(" ") + 1)
            If OnlineListView.Items.ContainsKey(LastWhisper) Then
                OnlineListView.SelectedItems.Clear()
                OnlineListView.Items(LastWhisper).Selected = True
                ChatComboBox.SelectedIndex = 1
            Else
                ChatTextBox.Text = ""
                Exit Sub
            End If
        ElseIf chat.ToUpper = "/DEBUG" Then
            'Globals.DebugMode = Not Globals.DebugMode
            'MsgBox("Debug Mode = " & Globals.DebugMode)
            ChatTextBox.Text = ""
            Exit Sub
        ElseIf chat.ToUpper.StartsWith("/") Then
            WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & Language.ChatMsgs.Invalid_command & ".</span>")
            ChatTextBox.Text = ""
            Exit Sub
        End If

        If Trim(chat) = "" Then
            ChatTextBox.Text = ""
            Exit Sub
        End If
        ChatHistory.Add(chat)
        ChatHistoryPosition = ChatHistory.Count

        ' Flood detection stuff, only mods and admins bypass it
        If Not Globals.CurrentUser.Security >= LobbyShared.User.SecurityGroups.Moderator Then
            If FloodDetectionQueue.Count >= 3 Then
                Try
                    If My.Settings.EnableSounds Then My.Computer.Audio.Play(My.Resources._Error, AudioPlayMode.Background)
                Catch ex As Exception
                End Try
                WriteLineToChat("<span style=""color: " & Mid(Hex(My.Settings.ChatColorWarningText), 3) & ";"">" & Language.ChatMsgs.WARNING_Spam_message_dropped & ".</span>")

                FloodDetectionQueue.Add(-500)
                FloodDetectionQueue.Add(-500)
                FloodDetectionQueue.Add(-500)
                If FloodDetectionQueue.Count > 6 Then FloodDetectionQueue.Add(-1000)

                ChatTextBox.Text = ""
                Exit Sub
            Else
                FloodDetectionQueue.Add(0)
            End If
        End If

        chat = Replace(chat, vbCrLf, "<br>")

        ' Parse smilies and language now only if whisper or warning so images don't get broken
        Dim parsedChat As String = chat
        If ChatComboBox.SelectedIndex = 1 OrElse ChatComboBox.SelectedIndex = 2 Then
            ParseBadWords(parsedChat)
            ParseSmilies(parsedChat)
        End If

        ' Depending on combobox selection, send the right message
        If ChatComboBox.SelectedIndex = 0 Then
            Globals.ClientEngine.LobbyChat(chat)
        ElseIf ChatComboBox.SelectedIndex = 1 Then
            Dim rlist As New List(Of String)
            For Each r As ListViewItem In SelectedList.SelectedItems
                rlist.Add(r.Text)
                Dim t As String
                t = "<span style=""color: " & Mid(Hex(My.Settings.ChatColorServerText1), 3) & ";"">" & Language.ChatMsgs.You_whisper_to & "&nbsp;"
                t &= "<a style=""color: " & Mid(Hex(My.Settings.ChatColorUserLink), 3) & ";"" href=""javascript:void(0)"">" & r.Text.Replace("<", "&lt;").Replace(">", "&gt;") & "</a>"
                t &= ":&nbsp;</span><span style=""color: " & Mid(Hex(My.Settings.ChatColorWhisperText), 3) & ";"">" & parsedChat & "</span>"
                WriteLineToChat(t)
            Next
            Globals.ClientEngine.LobbyWhisper(chat, rlist.ToArray)
        ElseIf ChatComboBox.SelectedIndex = 2 Then
            Dim rlist As New List(Of String)
            For Each r As ListViewItem In SelectedList.SelectedItems
                rlist.Add(r.Text)
                Dim t As String
                t = "<span style=""color: " & Mid(Hex(My.Settings.ChatColorWarningText), 3) & ";"">" & Language.ChatMsgs.WARNING_to & "&nbsp;</span>"
                t &= "<a style=""color: " & Mid(Hex(My.Settings.ChatColorUserLink), 3) & ";"" href=""javascript:void(0)"">" & r.Text.Replace("<", "&lt;").Replace(">", "&gt;") & "</a>"
                t &= "<span style=""color: " & Mid(Hex(My.Settings.ChatColorWarningText), 3) & ";"">:&nbsp;" & parsedChat & "</span>"
                WriteLineToChat(t)
            Next
            Globals.ClientEngine.LobbyWarn(chat, rlist.ToArray)
        ElseIf ChatComboBox.SelectedIndex = 3 Then
            Globals.ClientEngine.LobbyAlert(chat)
        End If
        ChatTextBox.Text = ""
        If My.Settings.RevertToChatAfterWhisper Then ChatComboBox.SelectedIndex = 0
    End Sub
    Private Sub ChatTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ChatTextBox.KeyPress
        If (e.KeyChar = Chr(13) OrElse e.KeyChar = Chr(10)) AndAlso ControlDown = False Then
            e.Handled = True
            SendButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
    Private Sub ChatTextBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ChatTextBox.KeyUp
        If e.Control = False Then CurrentHistory = ChatTextBox.Text
    End Sub
    Private Sub ChatTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ChatTextBox.KeyDown
        If e.Control Then
            ControlDown = True
        Else
            ControlDown = False
        End If

        If e.Control AndAlso e.KeyCode = Keys.Down Then
            ChatHistoryPosition += 1
            If ChatHistoryPosition >= ChatHistory.Count Then
                ChatTextBox.Text = CurrentHistory
                ChatTextBox.SelectionLength = 0
                ChatTextBox.SelectionStart = ChatTextBox.Text.Length
                ChatTextBox.ScrollToCaret()
                ChatHistoryPosition = ChatHistory.Count
            ElseIf ChatHistoryPosition < ChatHistory.Count Then
                ChatTextBox.Text = ChatHistory(ChatHistoryPosition)
                ChatTextBox.SelectionLength = 0
                ChatTextBox.SelectionStart = ChatTextBox.Text.Length
                ChatTextBox.ScrollToCaret()

            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.Up Then
            ChatHistoryPosition = ChatHistoryPosition - 1
            If ChatHistoryPosition < 0 Then ChatHistoryPosition = 0
            If ChatHistory.Count > ChatHistoryPosition Then
                ChatTextBox.Text = ChatHistory(ChatHistoryPosition)
                ChatTextBox.SelectionLength = 0
                ChatTextBox.SelectionStart = ChatTextBox.Text.Length
                ChatTextBox.ScrollToCaret()
            End If
        End If
    End Sub
    Private Sub OnlineListView_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlineListView.DoubleClick
        If OnlineListView.SelectedItems.Count > 0 Then
            ChatComboBox.SelectedIndex = 1
            ChatTextBox.Focus()
        End If
    End Sub
#End Region
#Region "AdvancedButtons"
    Private Sub LobbyAdvancedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LobbyAdvancedButton.Click
        MainTabControl.SelectedTab = LobbyTabPage
    End Sub
    Private Sub PatchAdvancedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatchAdvancedButton.Click
        'MainTabControl.SelectedTab = PatchTabPage

        '  If the instance still exists... (ie. it's Not Nothing)
        If Not IsNothing(Globals.PatcherForm) Then
            '  and if it hasn't been disposed yet
            If Not Globals.PatcherForm.IsDisposed Then
                '  then it must already be instantiated - maybe it's
                '  minimized or hidden behind other forms ?
                Globals.PatcherForm.BringToFront()  '  Option
            Else
                '  else it has already been disposed, so you can
                '  instantiate a new form and show it
                Globals.PatcherForm = New FormPatch()
                Globals.PatcherForm.Show()
            End If
        Else
            '  else the form = nothing, so you can safely
            '  instantiate a new form and show it
            Globals.PatcherForm = New FormPatch()
            Globals.PatcherForm.Show()
        End If
    End Sub
    Private Sub OptionsAdvancedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsAdvancedButton.Click
        MainTabControl.SelectedTab = OptionsTabPage
    End Sub
    Private Sub HelpAdvancedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpAdvancedButton.Click
        MainTabControl.SelectedTab = BrowserTabPage
        MiscWebBrowser.AllowNavigation = True
        MiscWebBrowser.Navigate("http://www.save-ee.com/help")
    End Sub
    Private Sub TrainingAdvancedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrainingAdvancedButton.Click
        MainTabControl.SelectedTab = BrowserTabPage
        MiscWebBrowser.AllowNavigation = True
        Try
            If MiscWebBrowser.Url.ToString.StartsWith("http://videos.eeaoctraining.com") Then
            ElseIf MiscWebBrowser.Url.ToString.StartsWith("http://www.videos.eeaoctraining.com") Then
            Else
                MiscWebBrowser.Navigate("http://videos.eeaoctraining.com")
            End If
        Catch ex As Exception
            MiscWebBrowser.Navigate("http://videos.eeaoctraining.com")
        End Try
    End Sub
    Private Sub SaveEEAdvancedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveEEAdvancedButton.Click
        MainTabControl.SelectedTab = BrowserTabPage
        MiscWebBrowser.AllowNavigation = True
        Try
            If MiscWebBrowser.Url.ToString.StartsWith("http://www.save-ee.com") = False Then
                MiscWebBrowser.Navigate("http://www.save-ee.com")
            End If
        Catch ex As Exception
            MiscWebBrowser.Navigate("http://www.save-ee.com")
        End Try
    End Sub
    Private Sub DonationsAdvancedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DonationsAdvancedButton.Click
        MainTabControl.SelectedTab = BrowserTabPage

        Dim name As String = (Globals.CurrentUser.Username)
        Dim uname As String = ""
        For Each c As Char In name
            uname = uname & "&#" & AscW(c) & ";"
        Next
        Dim b() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(uname)
        uname = Convert.ToBase64String(b)
        MiscWebBrowser.AllowNavigation = True
        MiscWebBrowser.Navigate("http://donation.eeaoctraining.com/donate.php?username=" & uname)
    End Sub
    Private Sub LadderAdvancedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LadderAdvancedButton.Click
        MainTabControl.SelectedTab = BrowserTabPage
        MiscWebBrowser.AllowNavigation = True
        Try
            If MiscWebBrowser.Url.ToString.StartsWith("http://" & Globals.ServerIP & ":8087") Then
            ElseIf MiscWebBrowser.Url.ToString.StartsWith("http://www.save-ee.com/pages.php?p=ladder") Then
            Else
                MiscWebBrowser.Navigate("http://www.save-ee.com/pages.php?p=ladder")
            End If
        Catch ex As Exception
            MiscWebBrowser.Navigate("http://www.save-ee.com/pages.php?p=ladder")
        End Try
    End Sub
    Private Sub PokerAdvancedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MainTabControl.SelectedTab = BrowserTabPage
        Try
            If MiscWebBrowser.Url.ToString.StartsWith("http://" & Globals.ServerIP & ":8087") = False Then
                MiscWebBrowser.Navigate("http://" & Globals.ServerIP & ":8087/")
            End If
        Catch ex As Exception
            MiscWebBrowser.Navigate("http://" & Globals.ServerIP & ":8087/")
        End Try
    End Sub
    Private Sub FacebookAdvancedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FacebookAdvancedButton.Click
        MainTabControl.SelectedTab = BrowserTabPage
        MiscWebBrowser.AllowNavigation = True
        Try
            If MiscWebBrowser.Url.ToString.StartsWith("http://www.save-ee.com/pages.php?p=facebook") = False Then
                MiscWebBrowser.Navigate("http://www.save-ee.com/pages.php?p=facebook")
            End If
        Catch ex As Exception
            MiscWebBrowser.Navigate("http://www.save-ee.com/pages.php?p=facebook")
        End Try
    End Sub
#End Region
#Region "UserFunctions"
    Private Sub UsersContextMenuStrip_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UsersContextMenuStrip.Opening
        If SelectedList.SelectedItems.Count > 0 Then
            WhisperToolStripMenuItem.Visible = True
            LocateToolStripMenuItem.Visible = True
            CopyNameToolStripMenuItem.Visible = True
            ToolStripSeparator1.Visible = True
            For Each lvi As ListViewItem In SelectedList.SelectedItems
                If Not FriendsListView.Items.ContainsKey(lvi.Name) Then
                    AddFriendToolStripMenuItem.Visible = True
                End If
                If FriendsListView.Items.ContainsKey(lvi.Name) Then
                    RemoveFriendToolStripMenuItem.Visible = True
                End If
                If Not CheatersListView.Items.ContainsKey(lvi.Name) Then
                    AddCheaterToolStripMenuItem.Visible = True
                End If
                If CheatersListView.Items.ContainsKey(lvi.Name) Then
                    RemoveCheaterToolStripMenuItem.Visible = True
                End If
                If Not IgnoreListView.Items.ContainsKey(lvi.Name) Then
                    IgnoreToolStripMenuItem.Visible = True
                End If
                If IgnoreListView.Items.ContainsKey(lvi.Name) Then
                    UnignoreToolStripMenuItem.Visible = True
                End If
            Next
        End If
    End Sub
    Private Sub UsersContextMenuStrip_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UsersContextMenuStrip.Closing
        WhisperToolStripMenuItem.Visible = False
        LocateToolStripMenuItem.Visible = False
        CopyNameToolStripMenuItem.Visible = False
        AddFriendToolStripMenuItem.Visible = False
        RemoveFriendToolStripMenuItem.Visible = False
        AddCheaterToolStripMenuItem.Visible = False
        RemoveCheaterToolStripMenuItem.Visible = False
        IgnoreToolStripMenuItem.Visible = False
        UnignoreToolStripMenuItem.Visible = False
        ToolStripSeparator1.Visible = False
    End Sub
    Private Sub WhisperToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WhisperToolStripMenuItem.Click
        ChatComboBox.SelectedIndex = 1
        ChatTextBox.Focus()
    End Sub
    Private Sub CopyNameToolStringMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyNameToolStripMenuItem.Click
        Dim str As String = ""
        For Each u As ListViewItem In SelectedList.SelectedItems
            str &= vbCrLf & u.Text
        Next
        str = Mid(str, 3)
        Try
            Clipboard.SetText(str)
        Catch ex As Exception
            MsgBox("Clipboard Error.", MsgBoxStyle.Critical, "Error!")
        End Try
    End Sub
    Private Sub AddFriendToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddFriendToolStripMenuItem.Click
        Dim b As New List(Of String)
        For Each lvi As ListViewItem In SelectedList.SelectedItems
            b.Add(lvi.Text)
        Next
        Globals.ClientEngine.AddFriend(b.ToArray)
    End Sub
    Private Sub ManualFriendToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualFriendToolStripMenuItem.Click
        Dim fn As String = Trim(InputBox(Language.Menus.Enter_a_username, Language.Menus.Manual_Add_to & " " & Language.Menus.Friends))
        If fn = "" Then Exit Sub
        Globals.ClientEngine.AddFriend(New String() {fn})
    End Sub
    Private Sub RemoveFriendToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveFriendToolStripMenuItem.Click
        Dim b As New List(Of String)
        For Each lvi As ListViewItem In SelectedList.SelectedItems
            b.Add(lvi.Text)
        Next
        Globals.ClientEngine.RemoveFriend(b.ToArray)
    End Sub
    Private Sub AddCheaterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddCheaterToolStripMenuItem.Click
        Dim b As New List(Of String)
        For Each lvi As ListViewItem In SelectedList.SelectedItems
            b.Add(lvi.Text)
        Next
        Globals.ClientEngine.AddCheater(b.ToArray)
    End Sub
    Private Sub ManualCheaterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualCheaterToolStripMenuItem.Click
        Dim fn As String = Trim(InputBox(Language.Menus.Enter_a_username, Language.Menus.Manual_Add_to & " " & Language.Menus.Cheaters))
        If fn = "" Then Exit Sub
        Globals.ClientEngine.AddCheater(New String() {fn})
    End Sub
    Private Sub RemoveCheaterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveCheaterToolStripMenuItem.Click
        Dim b As New List(Of String)
        For Each lvi As ListViewItem In SelectedList.SelectedItems
            b.Add(lvi.Text)
        Next
        Globals.ClientEngine.RemoveCheater(b.ToArray)
    End Sub
    Private Sub IgnoreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IgnoreToolStripMenuItem.Click
        Dim b As New List(Of String)
        For Each lvi As ListViewItem In SelectedList.SelectedItems
            b.Add(lvi.Text)
        Next
        Globals.ClientEngine.AddIgnore(b.ToArray)
    End Sub
    Private Sub ManualIgnoreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualIgnoreToolStripMenuItem.Click
        Dim fn As String = Trim(InputBox(Language.Menus.Enter_a_username, Language.Menus.Manual_Add_to & " " & Language.Menus.Ignore))
        If fn = "" Then Exit Sub
        Globals.ClientEngine.AddIgnore(New String() {fn})
    End Sub
    Private Sub UnignoreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnignoreToolStripMenuItem.Click
        Dim b As New List(Of String)
        For Each lvi As ListViewItem In SelectedList.SelectedItems
            b.Add(lvi.Text)
        Next
        Globals.ClientEngine.RemoveIgnore(b.ToArray)
    End Sub
    Private Sub SetAFKToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetAFKToolStripMenuItem.Click
        Globals.CurrentUser.AFK = True
        Globals.ClientEngine.ChangeStatus()
        AFKToggleForce = True
        SetAFKToolStripMenuItem.Visible = False
        ClearAFKToolStripMenuItem.Visible = True
    End Sub
    Private Sub ClearAFKToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearAFKToolStripMenuItem.Click
        Globals.CurrentUser.AFK = False
        Globals.ClientEngine.ChangeStatus()
        AFKToggleForce = False
        SetAFKToolStripMenuItem.Visible = True
        ClearAFKToolStripMenuItem.Visible = False
    End Sub
    Private Sub LocateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocateToolStripMenuItem.Click
        Dim rlist As New List(Of String)
        For Each r As ListViewItem In SelectedList.SelectedItems
            rlist.Add(r.Text)
        Next
        Globals.ClientEngine.LocateUser(rlist.ToArray)
    End Sub
#End Region
#Region "ModeratorFunctions"
    Private Sub WarnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WarnToolStripMenuItem.Click
        ChatComboBox.SelectedIndex = 2
        ChatTextBox.Focus()
    End Sub
    Private Sub KickToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KickToolStripMenuItem.Click
        Dim fn As String = Trim(InputBox("Enter the reason for the kick, which is shown to the player(s).", "Kick Player(s)"))
        If fn = "" Then Exit Sub
        Dim rlist As New List(Of String)
        For Each r As ListViewItem In SelectedList.SelectedItems
            rlist.Add(r.Text)
        Next
        Globals.ClientEngine.KickPlayer(rlist.ToArray, fn)
    End Sub
    Private Sub MuteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MuteToolStripMenuItem.Click
        Dim b As New List(Of String)
        For Each lvi As ListViewItem In SelectedList.SelectedItems
            b.Add(lvi.Text)
        Next
        Globals.ClientEngine.GetKeyList(b.ToArray, "MUTE")
    End Sub
    Private Sub ManualMuteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualMuteToolStripMenuItem.Click
        Dim manform As New FormManualOperation
        manform.Action = "MUTE"
        manform.Show()
    End Sub
    Private Sub ListMutedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListMutedToolStripMenuItem.Click
        Globals.ClientEngine.GetKeyList(New String() {"ALL"}, "UNMUTE")
    End Sub
    Private Sub GetDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDetailsToolStripMenuItem.Click
        Dim rlist As New List(Of String)
        For Each r As ListViewItem In SelectedList.SelectedItems
            rlist.Add(r.Text)
        Next
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.MiscMsg)
        p.StringCollection("GETDETAILS") = "true"
        Globals.ClientEngine.MiscMsg(rlist.ToArray, p)
        'using (Bitmap bmpScreenshot = 
        'new Bitmap(Screen.PrimaryScreen.Bounds.Width, 
        'Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb)) 
        '{ 
        '// Create a graphics object from the bitmap  
        'using (Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot)) 
        '{ 
        '            Try
        '{ 
        'Log("Capture screen"); 
        '// Take the screenshot from the upper left corner to the right bottom corner  
        'gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, 
        'Screen.PrimaryScreen.Bounds.Y, 
        '0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
    End Sub
#End Region
#Region "AdminFunctions"
    Private Sub BanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BanToolStripMenuItem.Click
        Dim b As New List(Of String)
        For Each lvi As ListViewItem In SelectedList.SelectedItems
            b.Add(lvi.Text)
        Next
        Globals.ClientEngine.GetKeyList(b.ToArray, "BAN")
    End Sub
    Private Sub ManualBanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualBanToolStripMenuItem.Click
        Dim manform As New FormManualOperation
        manform.Action = "BAN"
        manform.Show()
    End Sub
    Private Sub ListBannedPlayersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBannedPlayersToolStripMenuItem.Click
        Globals.ClientEngine.GetKeyList(New String() {"ALL"}, "UNBAN")
    End Sub
    Private Sub PromoteToAdminToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PromoteToAdminToolStripMenuItem.Click
        If SelectedList.SelectedItems.Count = 0 Then Exit Sub
        Globals.ClientEngine.ChangeUserRights(SelectedList.SelectedItems(0).Text, LobbyShared.User.SecurityGroups.Administrator)
    End Sub
    'Private Sub PromoteToSuperModeratorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PromoteToSuperModeratorToolStripMenuItem.Click
    '    If SelectedList.SelectedItems.Count = 0 Then Exit Sub
    '    Globals.ClientEngine.ChangeUserRights(SelectedList.SelectedItems(0).Text, "SUPERMOD")
    'End Sub
    Private Sub PromoteToModeratorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PromoteToModeratorToolStripMenuItem.Click
        If SelectedList.SelectedItems.Count = 0 Then Exit Sub
        Globals.ClientEngine.ChangeUserRights(SelectedList.SelectedItems(0).Text, LobbyShared.User.SecurityGroups.Moderator)
    End Sub
    Private Sub PromoteToDonatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PromoteToDonatorToolStripMenuItem.Click
        If SelectedList.SelectedItems.Count = 0 Then Exit Sub
        Globals.ClientEngine.ChangeUserRights(SelectedList.SelectedItems(0).Text, LobbyShared.User.SecurityGroups.Donator)
    End Sub
    Private Sub DemoteToUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DemoteToUserToolStripMenuItem.Click
        If SelectedList.SelectedItems.Count = 0 Then Exit Sub
        Globals.ClientEngine.ChangeUserRights(SelectedList.SelectedItems(0).Text, LobbyShared.User.SecurityGroups.User)
    End Sub
    Private Sub ForceUpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForceUpdateToolStripMenuItem.Click
        Dim rlist As New List(Of String)
        For Each r As ListViewItem In SelectedList.SelectedItems
            rlist.Add(r.Text)
        Next
        Dim p As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.MiscMsg)
        p.StringCollection("FORCEUPDATE") = "true"
        Globals.ClientEngine.MiscMsg(rlist.ToArray, p)
    End Sub
#End Region
#Region "Options"
    Public Sub ApplyPrefs()
        My.Settings.Save()

        VisualStylesCheckBox.Checked = If(System.IO.File.Exists(Globals.AppDataEnvVar & "\Save-EE\Lobby Client\disablevisuals.dat"), False, True)

        Dim c As Color

        AutoResizeGameColumnsCheckBox.Checked = My.Settings.AutoResizeGameColumns
        EnableSoundsCheckBox.Checked = My.Settings.EnableSounds
        MaximizeLobbyOnLoginCheckBox.Checked = My.Settings.MaximizeLobbyOnLogin
        MinimizeToSystemTrayCheckBox.Checked = My.Settings.MinimizeToSystemTray
        RevertToChatCheckBox.Checked = My.Settings.RevertToChatAfterWhisper
        ShowIconInSystemTrayCheckBox.Checked = My.Settings.ShowIconInSystemTray
        ShowTimestampsCheckBox.Checked = My.Settings.ShowTimestamps
        EnableSmiliesCheckBox.Checked = My.Settings.EnableSmilies
        EnableBadLanguageFilterCheckBox.Checked = My.Settings.EnableBadLanguageFilter

        Try
            LanguageComboBox.Text = My.Settings.Language
            Language.Main.SetLanguage([Enum].Parse(GetType(Language.Main.Languages), My.Settings.Language))
        Catch
            LanguageComboBox.SelectedIndex = 0
            Language.Main.SetLanguage(LanguageComboBox.SelectedIndex)
            My.Settings.Language = LanguageComboBox.Text
        End Try
        UpdateLanguage()


        c = Color.FromArgb(My.Settings.ButtonColorTopGradient)
        ButtonColorTopPanel.BackColor = c
        LobbyAdvancedButton.GradientColor1 = c
        LobbyAdvancedButton.GradientColor3 = c
        PatchAdvancedButton.GradientColor1 = c
        PatchAdvancedButton.GradientColor3 = c
        OptionsAdvancedButton.GradientColor1 = c
        OptionsAdvancedButton.GradientColor3 = c
        HelpAdvancedButton.GradientColor1 = c
        HelpAdvancedButton.GradientColor3 = c
        TrainingAdvancedButton.GradientColor1 = c
        TrainingAdvancedButton.GradientColor3 = c
        SaveEEAdvancedButton.GradientColor1 = c
        SaveEEAdvancedButton.GradientColor3 = c
        DonationsAdvancedButton.GradientColor1 = c
        DonationsAdvancedButton.GradientColor3 = c
        LadderAdvancedButton.GradientColor1 = c
        LadderAdvancedButton.GradientColor3 = c
        FacebookAdvancedButton.GradientColor1 = c
        FacebookAdvancedButton.GradientColor3 = c
        GamesOutlookPanel.HeaderColor1 = c
        ChatOutlookPanel.HeaderColor1 = c
        UsersOutlookPanel.HeaderColor1 = c
        PatchOutlookPanel.HeaderColor1 = c
        OptionsOutlookPanel.HeaderColor1 = c

        c = Color.FromArgb(My.Settings.ButtonColorBottomGradient)
        ButtonColorBottomPanel.BackColor = c
        LobbyAdvancedButton.GradientColor2 = c
        LobbyAdvancedButton.GradientColor4 = c
        PatchAdvancedButton.GradientColor2 = c
        PatchAdvancedButton.GradientColor4 = c
        OptionsAdvancedButton.GradientColor2 = c
        OptionsAdvancedButton.GradientColor4 = c
        HelpAdvancedButton.GradientColor2 = c
        HelpAdvancedButton.GradientColor4 = c
        TrainingAdvancedButton.GradientColor2 = c
        TrainingAdvancedButton.GradientColor4 = c
        SaveEEAdvancedButton.GradientColor2 = c
        SaveEEAdvancedButton.GradientColor4 = c
        DonationsAdvancedButton.GradientColor2 = c
        DonationsAdvancedButton.GradientColor4 = c
        LadderAdvancedButton.GradientColor2 = c
        LadderAdvancedButton.GradientColor4 = c
        FacebookAdvancedButton.GradientColor2 = c
        FacebookAdvancedButton.GradientColor4 = c
        GamesOutlookPanel.HeaderColor2 = c
        ChatOutlookPanel.HeaderColor2 = c
        UsersOutlookPanel.HeaderColor2 = c
        PatchOutlookPanel.HeaderColor2 = c
        OptionsOutlookPanel.HeaderColor2 = c

        ChatWebBrowser.Document.Body.SetAttribute("bgcolor", Mid(Hex(My.Settings.ChatColorBackground), 3))

        BackgroundColorPanel.BackColor = Color.FromArgb(My.Settings.ChatColorBackground)
        HyperlinkPanel.BackColor = Color.FromArgb(My.Settings.ChatColorHyperlink)
        UserLinkPanel.BackColor = Color.FromArgb(My.Settings.ChatColorUserLink)
        AlertTextPanel.BackColor = Color.FromArgb(My.Settings.ChatColorAlertText)
        ChatTextPanel.BackColor = Color.FromArgb(My.Settings.ChatColorChatText)
        WhisperTextPanel.BackColor = Color.FromArgb(My.Settings.ChatColorWhisperText)
        EmoteTextPanel.BackColor = Color.FromArgb(My.Settings.ChatColorEmoteText)
        WarningTextPanel.BackColor = Color.FromArgb(My.Settings.ChatColorWarningText)
        ServerText1Panel.BackColor = Color.FromArgb(My.Settings.ChatColorServerText1)
        ServerText2Panel.BackColor = Color.FromArgb(My.Settings.ChatColorServerText2)

        My.Settings.Save()
    End Sub
    Private Sub NetworkAdapterComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NetworkAdapterComboBox.SelectedIndexChanged
        My.Settings.DefaultAdapter = NetworkAdapterComboBox.Text
        Globals.DefaultAdapter = NetworkAdapterComboBox.Text
        ApplyPrefs()
    End Sub
    Private Sub RevertToChatCheckBox_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RevertToChatCheckBox.CheckedChanged
        My.Settings.RevertToChatAfterWhisper = RevertToChatCheckBox.Checked
        ApplyPrefs()
    End Sub
    Private Sub EnableSoundsCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableSoundsCheckBox.CheckedChanged
        My.Settings.EnableSounds = EnableSoundsCheckBox.Checked
        ApplyPrefs()
    End Sub
    Private Sub ShowTimestampsCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowTimestampsCheckBox.CheckedChanged
        My.Settings.ShowTimestamps = ShowTimestampsCheckBox.Checked
        ApplyPrefs()
    End Sub
    Private Sub EnableSmiliesCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableSmiliesCheckBox.CheckedChanged
        My.Settings.EnableSmilies = EnableSmiliesCheckBox.Checked
        ApplyPrefs()
    End Sub
    Private Sub EnableBadLanguageFilterCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableBadLanguageFilterCheckBox.CheckedChanged
        My.Settings.EnableBadLanguageFilter = EnableBadLanguageFilterCheckBox.Checked
        ApplyPrefs()
    End Sub
    Private Sub AutoResizeGameColumnsCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoResizeGameColumnsCheckBox.CheckedChanged
        My.Settings.AutoResizeGameColumns = AutoResizeGameColumnsCheckBox.Checked
        ApplyPrefs()
    End Sub
    Private Sub MaximizeLobbyOnLoginCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaximizeLobbyOnLoginCheckBox.CheckedChanged
        My.Settings.MaximizeLobbyOnLogin = MaximizeLobbyOnLoginCheckBox.Checked
        ApplyPrefs()
    End Sub
    Private Sub ShowIconInSystemTray_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowIconInSystemTrayCheckBox.CheckedChanged
        TrayNotifyIcon.Visible = ShowIconInSystemTrayCheckBox.Checked
        My.Settings.ShowIconInSystemTray = ShowIconInSystemTrayCheckBox.Checked
        ApplyPrefs()
    End Sub
    Private Sub MinimizeToSystemTrayCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinimizeToSystemTrayCheckBox.CheckedChanged
        My.Settings.MinimizeToSystemTray = MinimizeToSystemTrayCheckBox.Checked
        ApplyPrefs()
    End Sub
    Private Sub LanguageComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LanguageComboBox.SelectedIndexChanged
        Language.Main.SetLanguage(LanguageComboBox.SelectedIndex)
        My.Settings.Language = LanguageComboBox.Text
        ApplyPrefs()
    End Sub
    Private Sub VisualStylesCheckBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles VisualStylesCheckBox.Click
        If VisualStylesCheckBox.Checked Then
            Try
                System.IO.File.Delete(Globals.AppDataEnvVar & "\Save-EE\Lobby Client\disablevisuals.dat")
            Catch
                MsgBox("Unable to re-enable visual styles.  Please delete """ & Globals.AppDataEnvVar & "\Save-EE\Lobby Client\disablevisuals.dat"" manually.", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End Try
        Else
            Try
                System.IO.File.Create(Globals.AppDataEnvVar & "\Save-EE\Lobby Client\disablevisuals.dat")
            Catch
                MsgBox("Unable to disable visual styles.", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End Try
        End If

        ' Encode username and password for autologin
        Dim UserData As String = Globals.CurrentUser.Username & "[-:+:-]" & Globals.CurrentUser.Password
        Dim UserBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(UserData)
        LobbyShared.Crypt.Cryptic(UserBytes, New Byte() {1, 2, 3, 4, 5})
        Dim UsersB64 As String = LobbyShared.Base64.EncodeFromBytes(UserBytes)
        My.Settings.AutoLogin = UsersB64

        My.Settings.Save()
        Application.Restart()
    End Sub
    Private Sub ButtonTopPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ButtonColorTopPanel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ButtonColorTopGradient)
        c.ShowDialog()
        ButtonColorTopPanel.BackColor = c.Color
        My.Settings.ButtonColorTopGradient = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub ButtonBottomPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ButtonColorBottomPanel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ButtonColorBottomGradient)
        c.ShowDialog()
        ButtonColorBottomPanel.BackColor = c.Color
        My.Settings.ButtonColorBottomGradient = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub BackgroundColorPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BackgroundColorPanel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ChatColorBackground)
        c.ShowDialog()
        BackgroundColorPanel.BackColor = c.Color
        ChatWebBrowser.Document.Body.SetAttribute("bgcolor", Mid(Hex(c.Color.ToArgb), 3))
        My.Settings.ChatColorBackground = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub HyperlinkPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HyperlinkPanel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ChatColorHyperlink)
        c.ShowDialog()
        HyperlinkPanel.BackColor = c.Color
        My.Settings.ChatColorHyperlink = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub UserLinkPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles UserLinkPanel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ChatColorUserLink)
        c.ShowDialog()
        UserLinkPanel.BackColor = c.Color
        My.Settings.ChatColorUserLink = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub ChatTextPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ChatTextPanel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ChatColorChatText)
        c.ShowDialog()
        ChatTextPanel.BackColor = c.Color
        My.Settings.ChatColorChatText = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub WhisperTextPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles WhisperTextPanel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ChatColorWhisperText)
        c.ShowDialog()
        WhisperTextPanel.BackColor = c.Color
        My.Settings.ChatColorWhisperText = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub EmoteTextPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles EmoteTextPanel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ChatColorEmoteText)
        c.ShowDialog()
        EmoteTextPanel.BackColor = c.Color
        My.Settings.ChatColorEmoteText = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub AlertTextPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles AlertTextPanel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ChatColorAlertText)
        c.ShowDialog()
        AlertTextPanel.BackColor = c.Color
        My.Settings.ChatColorAlertText = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub WarningTextPanel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles WarningTextPanel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ChatColorWarningText)
        c.ShowDialog()
        WarningTextPanel.BackColor = c.Color
        My.Settings.ChatColorWarningText = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub ServerText1Panel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ServerText1Panel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ChatColorServerText1)
        c.ShowDialog()
        ServerText1Panel.BackColor = c.Color
        My.Settings.ChatColorServerText1 = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub ServerText2Panel_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ServerText2Panel.MouseClick
        Dim c As New ColorDialog
        c.Color = Color.FromArgb(My.Settings.ChatColorServerText2)
        c.ShowDialog()
        ServerText2Panel.BackColor = c.Color
        My.Settings.ChatColorServerText2 = c.Color.ToArgb
        ApplyPrefs()
    End Sub
    Private Sub EnableGameDetection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableGameDetectionButton.Click
        Try
            GameLoopAbort = True
            GameLoopThread.Abort()
        Catch ex As Exception

        End Try
        GameLoopAbort = False
        GameDetectionLabel.Text = "Game Detection Enabled"
        GameLoopThread = New System.Threading.Thread(AddressOf DetectGameLoop)
        GameLoopThread.IsBackground = True
        GameLoopThread.Start()
    End Sub
    Private Sub DisableGameDetectionButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisableGameDetectionButton.Click
        Try
            GameLoopAbort = True
            GameLoopThread.Abort()
        Catch ex As Exception

        End Try
        GameLoopAbort = False
        GameDetectionLabel.Text = "Game Detection Disabled"
    End Sub
#End Region
End Class
