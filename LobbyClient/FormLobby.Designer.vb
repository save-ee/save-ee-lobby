<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLobby
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLobby))
        Dim ListViewGroup12 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Administrators", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup13 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Moderators", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup14 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Friends", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup15 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Donators", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup16 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Users", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Online", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Offline", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Online", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Offline", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup5 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Online", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup6 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Offline", System.Windows.Forms.HorizontalAlignment.Left)
        Me.UsersContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.WhisperToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LocateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyNameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFriendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveFriendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddCheaterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveCheaterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IgnoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnignoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SetAFKToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearAFKToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualAddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualFriendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualCheaterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualIgnoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ModeratorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WarnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KickToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.MuteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualMuteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListMutedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.GetDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdminFunctionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ForceUpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.BanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualBanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListBannedPlayersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.PromoteToAdminToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PromoteToModeratorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PromoteToDonatorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DemoteToUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IconsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.OptionsTabPage = New System.Windows.Forms.TabPage()
        Me.OptionsOutlookPanel = New CustomPanels.OutlookPanel()
        Me.VisualStylesOutlookPanel = New CustomPanels.OutlookPanel()
        Me.VisualStylesCheckBox = New System.Windows.Forms.CheckBox()
        Me.MiscOptionsOutlookPanel = New CustomPanels.OutlookPanel()
        Me.MinimizeToSystemTrayCheckBox = New System.Windows.Forms.CheckBox()
        Me.ShowIconInSystemTrayCheckBox = New System.Windows.Forms.CheckBox()
        Me.LanguageComboBox = New System.Windows.Forms.ComboBox()
        Me.LanguageLabel = New System.Windows.Forms.Label()
        Me.MaximizeLobbyOnLoginCheckBox = New System.Windows.Forms.CheckBox()
        Me.AutoResizeGameColumnsCheckBox = New System.Windows.Forms.CheckBox()
        Me.ChatOptionsOutlookPanel = New CustomPanels.OutlookPanel()
        Me.EnableSmiliesCheckBox = New System.Windows.Forms.CheckBox()
        Me.EnableBadLanguageFilterCheckBox = New System.Windows.Forms.CheckBox()
        Me.ShowTimestampsCheckBox = New System.Windows.Forms.CheckBox()
        Me.EnableSoundsCheckBox = New System.Windows.Forms.CheckBox()
        Me.RevertToChatCheckBox = New System.Windows.Forms.CheckBox()
        Me.AutoscrollTextBox = New System.Windows.Forms.TextBox()
        Me.AutoscrollCheckBox = New System.Windows.Forms.CheckBox()
        Me.ChatColorsOutlookPanel = New CustomPanels.OutlookPanel()
        Me.HyperlinkGroupBox = New System.Windows.Forms.GroupBox()
        Me.HyperlinkPanel = New System.Windows.Forms.Panel()
        Me.ServerText2GroupBox = New System.Windows.Forms.GroupBox()
        Me.ServerText2Panel = New System.Windows.Forms.Panel()
        Me.ServerText1GroupBox = New System.Windows.Forms.GroupBox()
        Me.ServerText1Panel = New System.Windows.Forms.Panel()
        Me.WarningTextGroupBox = New System.Windows.Forms.GroupBox()
        Me.WarningTextPanel = New System.Windows.Forms.Panel()
        Me.WhisperTextGroupBox = New System.Windows.Forms.GroupBox()
        Me.WhisperTextPanel = New System.Windows.Forms.Panel()
        Me.AlertTextGroupBox = New System.Windows.Forms.GroupBox()
        Me.AlertTextPanel = New System.Windows.Forms.Panel()
        Me.EmoteGroupBox = New System.Windows.Forms.GroupBox()
        Me.EmoteTextPanel = New System.Windows.Forms.Panel()
        Me.ChatTextGroupBox = New System.Windows.Forms.GroupBox()
        Me.ChatTextPanel = New System.Windows.Forms.Panel()
        Me.UserLinkGroupBox = New System.Windows.Forms.GroupBox()
        Me.UserLinkPanel = New System.Windows.Forms.Panel()
        Me.BackgroundColorGroupBox = New System.Windows.Forms.GroupBox()
        Me.BackgroundColorPanel = New System.Windows.Forms.Panel()
        Me.GameDetectionOutlookPanel = New CustomPanels.OutlookPanel()
        Me.GameDetectionLabel = New System.Windows.Forms.Label()
        Me.DisableGameDetectionButton = New System.Windows.Forms.Button()
        Me.EnableGameDetectionButton = New System.Windows.Forms.Button()
        Me.ButtonColorsOutlookPanel = New CustomPanels.OutlookPanel()
        Me.ButtonColorBottomGroupBox = New System.Windows.Forms.GroupBox()
        Me.ButtonColorBottomPanel = New System.Windows.Forms.Panel()
        Me.ButtonColorTopGroupBox = New System.Windows.Forms.GroupBox()
        Me.ButtonColorTopPanel = New System.Windows.Forms.Panel()
        Me.NetworkDetailsOutlookPanel = New CustomPanels.OutlookPanel()
        Me.NetworkAdapterComboBox = New System.Windows.Forms.ComboBox()
        Me.NetworkAdapterLabel = New System.Windows.Forms.Label()
        Me.BrowserTabPage = New System.Windows.Forms.TabPage()
        Me.MiscWBAddressTextBox = New System.Windows.Forms.TextBox()
        Me.MiscWBForwardButton = New System.Windows.Forms.Button()
        Me.MiscWBBackButton = New System.Windows.Forms.Button()
        Me.MiscWebBrowser = New System.Windows.Forms.WebBrowser()
        Me.GameContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyIPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChatSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.ChatOutlookPanel = New CustomPanels.OutlookPanel()
        Me.AdvancedButton3 = New CustomPanels.AdvancedButton()
        Me.ChatConsoleGroupBox = New System.Windows.Forms.GroupBox()
        Me.SendButton = New System.Windows.Forms.Button()
        Me.ChatComboBox = New System.Windows.Forms.ComboBox()
        Me.ChatTextBox = New System.Windows.Forms.TextBox()
        Me.ChatWebBrowser = New System.Windows.Forms.WebBrowser()
        Me.UsersOutlookPanel = New CustomPanels.OutlookPanel()
        Me.UsersOnlineLabel = New System.Windows.Forms.Label()
        Me.UsersTabControl = New System.Windows.Forms.TabControl()
        Me.OnlineTabPage = New System.Windows.Forms.TabPage()
        Me.OnlineListView = New System.Windows.Forms.ListView()
        Me.OnlineListColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FriendsTabPage = New System.Windows.Forms.TabPage()
        Me.FriendsListView = New System.Windows.Forms.ListView()
        Me.FriendsListColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CheatersTabPage = New System.Windows.Forms.TabPage()
        Me.CheatersListView = New System.Windows.Forms.ListView()
        Me.CheatersListColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IgnoreTabPage = New System.Windows.Forms.TabPage()
        Me.IgnoreListView = New System.Windows.Forms.ListView()
        Me.IgnoreColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LobbyTabPage = New System.Windows.Forms.TabPage()
        Me.MainSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.GamesOutlookPanel = New CustomPanels.OutlookPanel()
        Me.GamesListView = New System.Windows.Forms.ListView()
        Me.GameColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GameNameColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.EpochColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ResourcesColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MapTypeColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MapSizeColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.HostColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IPColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PlayersColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MainTabControl = New System.Windows.Forms.TabControl()
        Me.PatchTabPage = New System.Windows.Forms.TabPage()
        Me.PatchOutlookPanel = New CustomPanels.OutlookPanel()
        Me.TrayNotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TrayContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowSaveEELobbyClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrayToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FacebookAdvancedButton = New CustomPanels.AdvancedButton()
        Me.OptionsAdvancedButton = New CustomPanels.AdvancedButton()
        Me.PatchAdvancedButton = New CustomPanels.AdvancedButton()
        Me.LadderAdvancedButton = New CustomPanels.AdvancedButton()
        Me.HelpAdvancedButton = New CustomPanels.AdvancedButton()
        Me.DonationsAdvancedButton = New CustomPanels.AdvancedButton()
        Me.TrainingAdvancedButton = New CustomPanels.AdvancedButton()
        Me.SaveEEAdvancedButton = New CustomPanels.AdvancedButton()
        Me.LobbyAdvancedButton = New CustomPanels.AdvancedButton()
        Me.LobbyTimer = New System.Windows.Forms.Timer(Me.components)
        Me.UsersContextMenuStrip.SuspendLayout()
        Me.OptionsTabPage.SuspendLayout()
        Me.OptionsOutlookPanel.SuspendLayout()
        Me.VisualStylesOutlookPanel.SuspendLayout()
        Me.MiscOptionsOutlookPanel.SuspendLayout()
        Me.ChatOptionsOutlookPanel.SuspendLayout()
        Me.ChatColorsOutlookPanel.SuspendLayout()
        Me.HyperlinkGroupBox.SuspendLayout()
        Me.ServerText2GroupBox.SuspendLayout()
        Me.ServerText1GroupBox.SuspendLayout()
        Me.WarningTextGroupBox.SuspendLayout()
        Me.WhisperTextGroupBox.SuspendLayout()
        Me.AlertTextGroupBox.SuspendLayout()
        Me.EmoteGroupBox.SuspendLayout()
        Me.ChatTextGroupBox.SuspendLayout()
        Me.UserLinkGroupBox.SuspendLayout()
        Me.BackgroundColorGroupBox.SuspendLayout()
        Me.GameDetectionOutlookPanel.SuspendLayout()
        Me.ButtonColorsOutlookPanel.SuspendLayout()
        Me.ButtonColorBottomGroupBox.SuspendLayout()
        Me.ButtonColorTopGroupBox.SuspendLayout()
        Me.NetworkDetailsOutlookPanel.SuspendLayout()
        Me.BrowserTabPage.SuspendLayout()
        Me.GameContextMenuStrip.SuspendLayout()
        Me.ChatSplitContainer.Panel1.SuspendLayout()
        Me.ChatSplitContainer.Panel2.SuspendLayout()
        Me.ChatSplitContainer.SuspendLayout()
        Me.ChatOutlookPanel.SuspendLayout()
        Me.ChatConsoleGroupBox.SuspendLayout()
        Me.UsersOutlookPanel.SuspendLayout()
        Me.UsersTabControl.SuspendLayout()
        Me.OnlineTabPage.SuspendLayout()
        Me.FriendsTabPage.SuspendLayout()
        Me.CheatersTabPage.SuspendLayout()
        Me.IgnoreTabPage.SuspendLayout()
        Me.LobbyTabPage.SuspendLayout()
        Me.MainSplitContainer.Panel1.SuspendLayout()
        Me.MainSplitContainer.Panel2.SuspendLayout()
        Me.MainSplitContainer.SuspendLayout()
        Me.GamesOutlookPanel.SuspendLayout()
        Me.MainTabControl.SuspendLayout()
        Me.PatchTabPage.SuspendLayout()
        Me.TrayContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'UsersContextMenuStrip
        '
        Me.UsersContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WhisperToolStripMenuItem, Me.LocateToolStripMenuItem, Me.CopyNameToolStripMenuItem, Me.AddFriendToolStripMenuItem, Me.RemoveFriendToolStripMenuItem, Me.AddCheaterToolStripMenuItem, Me.RemoveCheaterToolStripMenuItem, Me.IgnoreToolStripMenuItem, Me.UnignoreToolStripMenuItem, Me.ToolStripSeparator1, Me.SetAFKToolStripMenuItem, Me.ClearAFKToolStripMenuItem, Me.ManualAddToolStripMenuItem, Me.ToolStripSeparator2, Me.ModeratorToolStripMenuItem, Me.AdminFunctionsToolStripMenuItem})
        Me.UsersContextMenuStrip.Name = "ContextMenuStrip1"
        Me.UsersContextMenuStrip.Size = New System.Drawing.Size(196, 324)
        '
        'WhisperToolStripMenuItem
        '
        Me.WhisperToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.whisper
        Me.WhisperToolStripMenuItem.Name = "WhisperToolStripMenuItem"
        Me.WhisperToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.WhisperToolStripMenuItem.Text = "Whisper"
        Me.WhisperToolStripMenuItem.Visible = False
        '
        'LocateToolStripMenuItem
        '
        Me.LocateToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.locate
        Me.LocateToolStripMenuItem.Name = "LocateToolStripMenuItem"
        Me.LocateToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.LocateToolStripMenuItem.Text = "Locate"
        Me.LocateToolStripMenuItem.Visible = False
        '
        'CopyNameToolStripMenuItem
        '
        Me.CopyNameToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.copy
        Me.CopyNameToolStripMenuItem.Name = "CopyNameToolStripMenuItem"
        Me.CopyNameToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.CopyNameToolStripMenuItem.Text = "Copy Name"
        Me.CopyNameToolStripMenuItem.Visible = False
        '
        'AddFriendToolStripMenuItem
        '
        Me.AddFriendToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.friends_add
        Me.AddFriendToolStripMenuItem.Name = "AddFriendToolStripMenuItem"
        Me.AddFriendToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.AddFriendToolStripMenuItem.Text = "Add to Friends"
        Me.AddFriendToolStripMenuItem.Visible = False
        '
        'RemoveFriendToolStripMenuItem
        '
        Me.RemoveFriendToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.friends_remove
        Me.RemoveFriendToolStripMenuItem.Name = "RemoveFriendToolStripMenuItem"
        Me.RemoveFriendToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.RemoveFriendToolStripMenuItem.Text = "Remove from Friends"
        Me.RemoveFriendToolStripMenuItem.Visible = False
        '
        'AddCheaterToolStripMenuItem
        '
        Me.AddCheaterToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.cheater_add
        Me.AddCheaterToolStripMenuItem.Name = "AddCheaterToolStripMenuItem"
        Me.AddCheaterToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.AddCheaterToolStripMenuItem.Text = "Add to Cheaters"
        Me.AddCheaterToolStripMenuItem.Visible = False
        '
        'RemoveCheaterToolStripMenuItem
        '
        Me.RemoveCheaterToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.cheater_remove
        Me.RemoveCheaterToolStripMenuItem.Name = "RemoveCheaterToolStripMenuItem"
        Me.RemoveCheaterToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.RemoveCheaterToolStripMenuItem.Text = "Remove from Cheaters"
        Me.RemoveCheaterToolStripMenuItem.Visible = False
        '
        'IgnoreToolStripMenuItem
        '
        Me.IgnoreToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.ignore
        Me.IgnoreToolStripMenuItem.Name = "IgnoreToolStripMenuItem"
        Me.IgnoreToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.IgnoreToolStripMenuItem.Text = "Ignore"
        Me.IgnoreToolStripMenuItem.Visible = False
        '
        'UnignoreToolStripMenuItem
        '
        Me.UnignoreToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.ignore
        Me.UnignoreToolStripMenuItem.Name = "UnignoreToolStripMenuItem"
        Me.UnignoreToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.UnignoreToolStripMenuItem.Text = "Unignore"
        Me.UnignoreToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(192, 6)
        Me.ToolStripSeparator1.Visible = False
        '
        'SetAFKToolStripMenuItem
        '
        Me.SetAFKToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.AFK
        Me.SetAFKToolStripMenuItem.Name = "SetAFKToolStripMenuItem"
        Me.SetAFKToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.SetAFKToolStripMenuItem.Text = "Set AFK"
        '
        'ClearAFKToolStripMenuItem
        '
        Me.ClearAFKToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.AFK
        Me.ClearAFKToolStripMenuItem.Name = "ClearAFKToolStripMenuItem"
        Me.ClearAFKToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.ClearAFKToolStripMenuItem.Text = "Clear AFK"
        Me.ClearAFKToolStripMenuItem.Visible = False
        '
        'ManualAddToolStripMenuItem
        '
        Me.ManualAddToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ManualFriendToolStripMenuItem, Me.ManualCheaterToolStripMenuItem, Me.ManualIgnoreToolStripMenuItem})
        Me.ManualAddToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.add
        Me.ManualAddToolStripMenuItem.Name = "ManualAddToolStripMenuItem"
        Me.ManualAddToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.ManualAddToolStripMenuItem.Text = "Manual Add to..."
        '
        'ManualFriendToolStripMenuItem
        '
        Me.ManualFriendToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.friends_add
        Me.ManualFriendToolStripMenuItem.Name = "ManualFriendToolStripMenuItem"
        Me.ManualFriendToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.ManualFriendToolStripMenuItem.Text = "Friends"
        '
        'ManualCheaterToolStripMenuItem
        '
        Me.ManualCheaterToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.cheater_add
        Me.ManualCheaterToolStripMenuItem.Name = "ManualCheaterToolStripMenuItem"
        Me.ManualCheaterToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.ManualCheaterToolStripMenuItem.Text = "Cheaters"
        '
        'ManualIgnoreToolStripMenuItem
        '
        Me.ManualIgnoreToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.ignore
        Me.ManualIgnoreToolStripMenuItem.Name = "ManualIgnoreToolStripMenuItem"
        Me.ManualIgnoreToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.ManualIgnoreToolStripMenuItem.Text = "Ignore"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(192, 6)
        Me.ToolStripSeparator2.Visible = False
        '
        'ModeratorToolStripMenuItem
        '
        Me.ModeratorToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WarnToolStripMenuItem, Me.KickToolStripMenuItem, Me.ToolStripSeparator3, Me.MuteToolStripMenuItem, Me.ManualMuteToolStripMenuItem, Me.ListMutedToolStripMenuItem, Me.ToolStripSeparator4, Me.GetDetailsToolStripMenuItem})
        Me.ModeratorToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Moderator
        Me.ModeratorToolStripMenuItem.Name = "ModeratorToolStripMenuItem"
        Me.ModeratorToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.ModeratorToolStripMenuItem.Text = "Moderator Functions"
        Me.ModeratorToolStripMenuItem.Visible = False
        '
        'WarnToolStripMenuItem
        '
        Me.WarnToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Moderator
        Me.WarnToolStripMenuItem.Name = "WarnToolStripMenuItem"
        Me.WarnToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.WarnToolStripMenuItem.Text = "Warn"
        '
        'KickToolStripMenuItem
        '
        Me.KickToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Moderator
        Me.KickToolStripMenuItem.Name = "KickToolStripMenuItem"
        Me.KickToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.KickToolStripMenuItem.Text = "Kick"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(167, 6)
        '
        'MuteToolStripMenuItem
        '
        Me.MuteToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Moderator
        Me.MuteToolStripMenuItem.Name = "MuteToolStripMenuItem"
        Me.MuteToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.MuteToolStripMenuItem.Text = "Mute"
        '
        'ManualMuteToolStripMenuItem
        '
        Me.ManualMuteToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Moderator
        Me.ManualMuteToolStripMenuItem.Name = "ManualMuteToolStripMenuItem"
        Me.ManualMuteToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ManualMuteToolStripMenuItem.Text = "Manual Mute"
        '
        'ListMutedToolStripMenuItem
        '
        Me.ListMutedToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Moderator
        Me.ListMutedToolStripMenuItem.Name = "ListMutedToolStripMenuItem"
        Me.ListMutedToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ListMutedToolStripMenuItem.Text = "List Muted Players"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(167, 6)
        '
        'GetDetailsToolStripMenuItem
        '
        Me.GetDetailsToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Moderator
        Me.GetDetailsToolStripMenuItem.Name = "GetDetailsToolStripMenuItem"
        Me.GetDetailsToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.GetDetailsToolStripMenuItem.Text = "Get Details"
        '
        'AdminFunctionsToolStripMenuItem
        '
        Me.AdminFunctionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ForceUpdateToolStripMenuItem, Me.ToolStripSeparator5, Me.BanToolStripMenuItem, Me.ManualBanToolStripMenuItem, Me.ListBannedPlayersToolStripMenuItem, Me.ToolStripSeparator6, Me.PromoteToAdminToolStripMenuItem, Me.PromoteToModeratorToolStripMenuItem, Me.PromoteToDonatorToolStripMenuItem, Me.DemoteToUserToolStripMenuItem})
        Me.AdminFunctionsToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Admin
        Me.AdminFunctionsToolStripMenuItem.Name = "AdminFunctionsToolStripMenuItem"
        Me.AdminFunctionsToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.AdminFunctionsToolStripMenuItem.Text = "Admin Functions"
        Me.AdminFunctionsToolStripMenuItem.Visible = False
        '
        'ForceUpdateToolStripMenuItem
        '
        Me.ForceUpdateToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Admin
        Me.ForceUpdateToolStripMenuItem.Name = "ForceUpdateToolStripMenuItem"
        Me.ForceUpdateToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ForceUpdateToolStripMenuItem.Text = "Force Update"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(190, 6)
        '
        'BanToolStripMenuItem
        '
        Me.BanToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Admin
        Me.BanToolStripMenuItem.Name = "BanToolStripMenuItem"
        Me.BanToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.BanToolStripMenuItem.Text = "Ban"
        '
        'ManualBanToolStripMenuItem
        '
        Me.ManualBanToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Admin
        Me.ManualBanToolStripMenuItem.Name = "ManualBanToolStripMenuItem"
        Me.ManualBanToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ManualBanToolStripMenuItem.Text = "Manual Ban"
        '
        'ListBannedPlayersToolStripMenuItem
        '
        Me.ListBannedPlayersToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Admin
        Me.ListBannedPlayersToolStripMenuItem.Name = "ListBannedPlayersToolStripMenuItem"
        Me.ListBannedPlayersToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ListBannedPlayersToolStripMenuItem.Text = "List Banned Players"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(190, 6)
        '
        'PromoteToAdminToolStripMenuItem
        '
        Me.PromoteToAdminToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Admin
        Me.PromoteToAdminToolStripMenuItem.Name = "PromoteToAdminToolStripMenuItem"
        Me.PromoteToAdminToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.PromoteToAdminToolStripMenuItem.Text = "Promote to Admin"
        '
        'PromoteToModeratorToolStripMenuItem
        '
        Me.PromoteToModeratorToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.Moderator
        Me.PromoteToModeratorToolStripMenuItem.Name = "PromoteToModeratorToolStripMenuItem"
        Me.PromoteToModeratorToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.PromoteToModeratorToolStripMenuItem.Text = "Promote to Moderator"
        '
        'PromoteToDonatorToolStripMenuItem
        '
        Me.PromoteToDonatorToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.donator
        Me.PromoteToDonatorToolStripMenuItem.Name = "PromoteToDonatorToolStripMenuItem"
        Me.PromoteToDonatorToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.PromoteToDonatorToolStripMenuItem.Text = "Promote to Donator"
        '
        'DemoteToUserToolStripMenuItem
        '
        Me.DemoteToUserToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.user
        Me.DemoteToUserToolStripMenuItem.Name = "DemoteToUserToolStripMenuItem"
        Me.DemoteToUserToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.DemoteToUserToolStripMenuItem.Text = "Demote to User"
        '
        'IconsImageList
        '
        Me.IconsImageList.ImageStream = CType(resources.GetObject("IconsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.IconsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.IconsImageList.Images.SetKeyName(0, "ee.png")
        Me.IconsImageList.Images.SetKeyName(1, "ee_add.png")
        Me.IconsImageList.Images.SetKeyName(2, "eeaoc.png")
        Me.IconsImageList.Images.SetKeyName(3, "eeaoc_add.png")
        Me.IconsImageList.Images.SetKeyName(4, "eeboth.png")
        Me.IconsImageList.Images.SetKeyName(5, "eeboth_add.png")
        Me.IconsImageList.Images.SetKeyName(6, "Admin.png")
        Me.IconsImageList.Images.SetKeyName(7, "AFK.png")
        Me.IconsImageList.Images.SetKeyName(8, "AFK_add.png")
        Me.IconsImageList.Images.SetKeyName(9, "cheater.png")
        Me.IconsImageList.Images.SetKeyName(10, "donator.png")
        Me.IconsImageList.Images.SetKeyName(11, "friends.png")
        Me.IconsImageList.Images.SetKeyName(12, "friends_add.png")
        Me.IconsImageList.Images.SetKeyName(13, "friends_offline.png")
        Me.IconsImageList.Images.SetKeyName(14, "ignore.png")
        Me.IconsImageList.Images.SetKeyName(15, "ignore_add.png")
        Me.IconsImageList.Images.SetKeyName(16, "Moderator.png")
        Me.IconsImageList.Images.SetKeyName(17, "user.png")
        '
        'OptionsTabPage
        '
        Me.OptionsTabPage.BackColor = System.Drawing.Color.Black
        Me.OptionsTabPage.Controls.Add(Me.OptionsOutlookPanel)
        Me.OptionsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.OptionsTabPage.Name = "OptionsTabPage"
        Me.OptionsTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.OptionsTabPage.Size = New System.Drawing.Size(991, 650)
        Me.OptionsTabPage.TabIndex = 1
        Me.OptionsTabPage.Text = "Options"
        '
        'OptionsOutlookPanel
        '
        Me.OptionsOutlookPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OptionsOutlookPanel.BackColor = System.Drawing.Color.Black
        Me.OptionsOutlookPanel.Controls.Add(Me.VisualStylesOutlookPanel)
        Me.OptionsOutlookPanel.Controls.Add(Me.MiscOptionsOutlookPanel)
        Me.OptionsOutlookPanel.Controls.Add(Me.ChatOptionsOutlookPanel)
        Me.OptionsOutlookPanel.Controls.Add(Me.ChatColorsOutlookPanel)
        Me.OptionsOutlookPanel.Controls.Add(Me.GameDetectionOutlookPanel)
        Me.OptionsOutlookPanel.Controls.Add(Me.ButtonColorsOutlookPanel)
        Me.OptionsOutlookPanel.Controls.Add(Me.NetworkDetailsOutlookPanel)
        Me.OptionsOutlookPanel.HeaderColor1 = System.Drawing.Color.SlateGray
        Me.OptionsOutlookPanel.HeaderColor2 = System.Drawing.Color.Black
        Me.OptionsOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptionsOutlookPanel.HeaderHeight = 25
        Me.OptionsOutlookPanel.HeaderText = "Options"
        Me.OptionsOutlookPanel.Icon = Nothing
        Me.OptionsOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.OptionsOutlookPanel.Location = New System.Drawing.Point(1, 46)
        Me.OptionsOutlookPanel.Name = "OptionsOutlookPanel"
        Me.OptionsOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.OptionsOutlookPanel.Size = New System.Drawing.Size(989, 602)
        Me.OptionsOutlookPanel.TabIndex = 18
        '
        'VisualStylesOutlookPanel
        '
        Me.VisualStylesOutlookPanel.BackColor = System.Drawing.Color.LightGray
        Me.VisualStylesOutlookPanel.Controls.Add(Me.VisualStylesCheckBox)
        Me.VisualStylesOutlookPanel.HeaderColor1 = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.VisualStylesOutlookPanel.HeaderColor2 = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.VisualStylesOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VisualStylesOutlookPanel.HeaderHeight = 25
        Me.VisualStylesOutlookPanel.HeaderText = "Visual Styles"
        Me.VisualStylesOutlookPanel.Icon = Nothing
        Me.VisualStylesOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.VisualStylesOutlookPanel.Location = New System.Drawing.Point(8, 480)
        Me.VisualStylesOutlookPanel.Name = "VisualStylesOutlookPanel"
        Me.VisualStylesOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.VisualStylesOutlookPanel.Size = New System.Drawing.Size(590, 90)
        Me.VisualStylesOutlookPanel.TabIndex = 24
        '
        'VisualStylesCheckBox
        '
        Me.VisualStylesCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.VisualStylesCheckBox.Location = New System.Drawing.Point(8, 32)
        Me.VisualStylesCheckBox.Name = "VisualStylesCheckBox"
        Me.VisualStylesCheckBox.Size = New System.Drawing.Size(574, 56)
        Me.VisualStylesCheckBox.TabIndex = 27
        Me.VisualStylesCheckBox.Text = "Enable Visual Styles" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Uncheck this box if you get the following error:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    Sys" & _
    "tem.AccessViolationException: Attempted to read or write protected memory" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.VisualStylesCheckBox.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.VisualStylesCheckBox.UseVisualStyleBackColor = True
        '
        'MiscOptionsOutlookPanel
        '
        Me.MiscOptionsOutlookPanel.BackColor = System.Drawing.Color.LightGray
        Me.MiscOptionsOutlookPanel.Controls.Add(Me.MinimizeToSystemTrayCheckBox)
        Me.MiscOptionsOutlookPanel.Controls.Add(Me.ShowIconInSystemTrayCheckBox)
        Me.MiscOptionsOutlookPanel.Controls.Add(Me.LanguageComboBox)
        Me.MiscOptionsOutlookPanel.Controls.Add(Me.LanguageLabel)
        Me.MiscOptionsOutlookPanel.Controls.Add(Me.MaximizeLobbyOnLoginCheckBox)
        Me.MiscOptionsOutlookPanel.Controls.Add(Me.AutoResizeGameColumnsCheckBox)
        Me.MiscOptionsOutlookPanel.HeaderColor1 = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.MiscOptionsOutlookPanel.HeaderColor2 = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.MiscOptionsOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MiscOptionsOutlookPanel.HeaderHeight = 25
        Me.MiscOptionsOutlookPanel.HeaderText = "Miscellaneous Options"
        Me.MiscOptionsOutlookPanel.Icon = Nothing
        Me.MiscOptionsOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.MiscOptionsOutlookPanel.Location = New System.Drawing.Point(8, 292)
        Me.MiscOptionsOutlookPanel.Name = "MiscOptionsOutlookPanel"
        Me.MiscOptionsOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.MiscOptionsOutlookPanel.Size = New System.Drawing.Size(262, 182)
        Me.MiscOptionsOutlookPanel.TabIndex = 23
        '
        'MinimizeToSystemTrayCheckBox
        '
        Me.MinimizeToSystemTrayCheckBox.AutoSize = True
        Me.MinimizeToSystemTrayCheckBox.Location = New System.Drawing.Point(8, 118)
        Me.MinimizeToSystemTrayCheckBox.Name = "MinimizeToSystemTrayCheckBox"
        Me.MinimizeToSystemTrayCheckBox.Size = New System.Drawing.Size(143, 17)
        Me.MinimizeToSystemTrayCheckBox.TabIndex = 30
        Me.MinimizeToSystemTrayCheckBox.Text = "Minimize To System Tray"
        Me.MinimizeToSystemTrayCheckBox.UseVisualStyleBackColor = True
        '
        'ShowIconInSystemTrayCheckBox
        '
        Me.ShowIconInSystemTrayCheckBox.AutoSize = True
        Me.ShowIconInSystemTrayCheckBox.Location = New System.Drawing.Point(8, 95)
        Me.ShowIconInSystemTrayCheckBox.Name = "ShowIconInSystemTrayCheckBox"
        Me.ShowIconInSystemTrayCheckBox.Size = New System.Drawing.Size(152, 17)
        Me.ShowIconInSystemTrayCheckBox.TabIndex = 29
        Me.ShowIconInSystemTrayCheckBox.Text = "Show Icon In System Tray"
        Me.ShowIconInSystemTrayCheckBox.UseVisualStyleBackColor = True
        '
        'LanguageComboBox
        '
        Me.LanguageComboBox.FormattingEnabled = True
        Me.LanguageComboBox.Location = New System.Drawing.Point(8, 154)
        Me.LanguageComboBox.Name = "LanguageComboBox"
        Me.LanguageComboBox.Size = New System.Drawing.Size(121, 21)
        Me.LanguageComboBox.TabIndex = 28
        '
        'LanguageLabel
        '
        Me.LanguageLabel.AutoSize = True
        Me.LanguageLabel.Location = New System.Drawing.Point(8, 138)
        Me.LanguageLabel.Name = "LanguageLabel"
        Me.LanguageLabel.Size = New System.Drawing.Size(54, 13)
        Me.LanguageLabel.TabIndex = 27
        Me.LanguageLabel.Text = "Language"
        '
        'MaximizeLobbyOnLoginCheckBox
        '
        Me.MaximizeLobbyOnLoginCheckBox.AutoSize = True
        Me.MaximizeLobbyOnLoginCheckBox.Location = New System.Drawing.Point(8, 72)
        Me.MaximizeLobbyOnLoginCheckBox.Name = "MaximizeLobbyOnLoginCheckBox"
        Me.MaximizeLobbyOnLoginCheckBox.Size = New System.Drawing.Size(146, 17)
        Me.MaximizeLobbyOnLoginCheckBox.TabIndex = 26
        Me.MaximizeLobbyOnLoginCheckBox.Text = "Maximize Lobby On Login"
        Me.MaximizeLobbyOnLoginCheckBox.UseVisualStyleBackColor = True
        '
        'AutoResizeGameColumnsCheckBox
        '
        Me.AutoResizeGameColumnsCheckBox.Location = New System.Drawing.Point(8, 32)
        Me.AutoResizeGameColumnsCheckBox.Name = "AutoResizeGameColumnsCheckBox"
        Me.AutoResizeGameColumnsCheckBox.Size = New System.Drawing.Size(197, 34)
        Me.AutoResizeGameColumnsCheckBox.TabIndex = 25
        Me.AutoResizeGameColumnsCheckBox.Text = "Automatically Resize Game Columns"
        Me.AutoResizeGameColumnsCheckBox.UseVisualStyleBackColor = True
        '
        'ChatOptionsOutlookPanel
        '
        Me.ChatOptionsOutlookPanel.BackColor = System.Drawing.Color.LightGray
        Me.ChatOptionsOutlookPanel.Controls.Add(Me.EnableSmiliesCheckBox)
        Me.ChatOptionsOutlookPanel.Controls.Add(Me.EnableBadLanguageFilterCheckBox)
        Me.ChatOptionsOutlookPanel.Controls.Add(Me.ShowTimestampsCheckBox)
        Me.ChatOptionsOutlookPanel.Controls.Add(Me.EnableSoundsCheckBox)
        Me.ChatOptionsOutlookPanel.Controls.Add(Me.RevertToChatCheckBox)
        Me.ChatOptionsOutlookPanel.Controls.Add(Me.AutoscrollTextBox)
        Me.ChatOptionsOutlookPanel.Controls.Add(Me.AutoscrollCheckBox)
        Me.ChatOptionsOutlookPanel.HeaderColor1 = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.ChatOptionsOutlookPanel.HeaderColor2 = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.ChatOptionsOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChatOptionsOutlookPanel.HeaderHeight = 25
        Me.ChatOptionsOutlookPanel.HeaderText = "Chat Options"
        Me.ChatOptionsOutlookPanel.Icon = Nothing
        Me.ChatOptionsOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.ChatOptionsOutlookPanel.Location = New System.Drawing.Point(8, 114)
        Me.ChatOptionsOutlookPanel.Name = "ChatOptionsOutlookPanel"
        Me.ChatOptionsOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.ChatOptionsOutlookPanel.Size = New System.Drawing.Size(262, 172)
        Me.ChatOptionsOutlookPanel.TabIndex = 22
        '
        'EnableSmiliesCheckBox
        '
        Me.EnableSmiliesCheckBox.AutoSize = True
        Me.EnableSmiliesCheckBox.Location = New System.Drawing.Point(8, 124)
        Me.EnableSmiliesCheckBox.Name = "EnableSmiliesCheckBox"
        Me.EnableSmiliesCheckBox.Size = New System.Drawing.Size(92, 17)
        Me.EnableSmiliesCheckBox.TabIndex = 32
        Me.EnableSmiliesCheckBox.Text = "Enable Smilies"
        Me.EnableSmiliesCheckBox.UseVisualStyleBackColor = True
        '
        'EnableBadLanguageFilterCheckBox
        '
        Me.EnableBadLanguageFilterCheckBox.AutoSize = True
        Me.EnableBadLanguageFilterCheckBox.Location = New System.Drawing.Point(8, 147)
        Me.EnableBadLanguageFilterCheckBox.Name = "EnableBadLanguageFilterCheckBox"
        Me.EnableBadLanguageFilterCheckBox.Size = New System.Drawing.Size(156, 17)
        Me.EnableBadLanguageFilterCheckBox.TabIndex = 31
        Me.EnableBadLanguageFilterCheckBox.Text = "Enable Bad Language Filter"
        Me.EnableBadLanguageFilterCheckBox.UseVisualStyleBackColor = True
        '
        'ShowTimestampsCheckBox
        '
        Me.ShowTimestampsCheckBox.AutoSize = True
        Me.ShowTimestampsCheckBox.Location = New System.Drawing.Point(8, 101)
        Me.ShowTimestampsCheckBox.Name = "ShowTimestampsCheckBox"
        Me.ShowTimestampsCheckBox.Size = New System.Drawing.Size(111, 17)
        Me.ShowTimestampsCheckBox.TabIndex = 25
        Me.ShowTimestampsCheckBox.Text = "Show Timestamps"
        Me.ShowTimestampsCheckBox.UseVisualStyleBackColor = True
        '
        'EnableSoundsCheckBox
        '
        Me.EnableSoundsCheckBox.AutoSize = True
        Me.EnableSoundsCheckBox.Location = New System.Drawing.Point(8, 78)
        Me.EnableSoundsCheckBox.Name = "EnableSoundsCheckBox"
        Me.EnableSoundsCheckBox.Size = New System.Drawing.Size(96, 17)
        Me.EnableSoundsCheckBox.TabIndex = 24
        Me.EnableSoundsCheckBox.Text = "Enable Sounds"
        Me.EnableSoundsCheckBox.UseVisualStyleBackColor = True
        '
        'RevertToChatCheckBox
        '
        Me.RevertToChatCheckBox.AutoSize = True
        Me.RevertToChatCheckBox.Location = New System.Drawing.Point(8, 55)
        Me.RevertToChatCheckBox.Name = "RevertToChatCheckBox"
        Me.RevertToChatCheckBox.Size = New System.Drawing.Size(170, 17)
        Me.RevertToChatCheckBox.TabIndex = 23
        Me.RevertToChatCheckBox.Text = "Revert To Chat After Whisper"
        Me.RevertToChatCheckBox.UseVisualStyleBackColor = True
        '
        'AutoscrollTextBox
        '
        Me.AutoscrollTextBox.Location = New System.Drawing.Point(149, 30)
        Me.AutoscrollTextBox.Name = "AutoscrollTextBox"
        Me.AutoscrollTextBox.Size = New System.Drawing.Size(105, 21)
        Me.AutoscrollTextBox.TabIndex = 22
        '
        'AutoscrollCheckBox
        '
        Me.AutoscrollCheckBox.AutoSize = True
        Me.AutoscrollCheckBox.Checked = True
        Me.AutoscrollCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AutoscrollCheckBox.Location = New System.Drawing.Point(8, 32)
        Me.AutoscrollCheckBox.Name = "AutoscrollCheckBox"
        Me.AutoscrollCheckBox.Size = New System.Drawing.Size(73, 17)
        Me.AutoscrollCheckBox.TabIndex = 21
        Me.AutoscrollCheckBox.Text = "Autoscroll"
        Me.AutoscrollCheckBox.UseVisualStyleBackColor = True
        '
        'ChatColorsOutlookPanel
        '
        Me.ChatColorsOutlookPanel.BackColor = System.Drawing.Color.LightGray
        Me.ChatColorsOutlookPanel.Controls.Add(Me.HyperlinkGroupBox)
        Me.ChatColorsOutlookPanel.Controls.Add(Me.ServerText2GroupBox)
        Me.ChatColorsOutlookPanel.Controls.Add(Me.ServerText1GroupBox)
        Me.ChatColorsOutlookPanel.Controls.Add(Me.WarningTextGroupBox)
        Me.ChatColorsOutlookPanel.Controls.Add(Me.WhisperTextGroupBox)
        Me.ChatColorsOutlookPanel.Controls.Add(Me.AlertTextGroupBox)
        Me.ChatColorsOutlookPanel.Controls.Add(Me.EmoteGroupBox)
        Me.ChatColorsOutlookPanel.Controls.Add(Me.ChatTextGroupBox)
        Me.ChatColorsOutlookPanel.Controls.Add(Me.UserLinkGroupBox)
        Me.ChatColorsOutlookPanel.Controls.Add(Me.BackgroundColorGroupBox)
        Me.ChatColorsOutlookPanel.HeaderColor1 = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.ChatColorsOutlookPanel.HeaderColor2 = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.ChatColorsOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChatColorsOutlookPanel.HeaderHeight = 25
        Me.ChatColorsOutlookPanel.HeaderText = "Chat Colors"
        Me.ChatColorsOutlookPanel.Icon = Nothing
        Me.ChatColorsOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.ChatColorsOutlookPanel.Location = New System.Drawing.Point(276, 114)
        Me.ChatColorsOutlookPanel.Name = "ChatColorsOutlookPanel"
        Me.ChatColorsOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.ChatColorsOutlookPanel.Size = New System.Drawing.Size(322, 360)
        Me.ChatColorsOutlookPanel.TabIndex = 21
        '
        'HyperlinkGroupBox
        '
        Me.HyperlinkGroupBox.Controls.Add(Me.HyperlinkPanel)
        Me.HyperlinkGroupBox.Location = New System.Drawing.Point(164, 32)
        Me.HyperlinkGroupBox.Name = "HyperlinkGroupBox"
        Me.HyperlinkGroupBox.Size = New System.Drawing.Size(150, 45)
        Me.HyperlinkGroupBox.TabIndex = 10
        Me.HyperlinkGroupBox.TabStop = False
        Me.HyperlinkGroupBox.Text = "Hyperlink"
        '
        'HyperlinkPanel
        '
        Me.HyperlinkPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HyperlinkPanel.Location = New System.Drawing.Point(6, 16)
        Me.HyperlinkPanel.Name = "HyperlinkPanel"
        Me.HyperlinkPanel.Size = New System.Drawing.Size(138, 24)
        Me.HyperlinkPanel.TabIndex = 31
        Me.HyperlinkPanel.TabStop = True
        '
        'ServerText2GroupBox
        '
        Me.ServerText2GroupBox.Controls.Add(Me.ServerText2Panel)
        Me.ServerText2GroupBox.Location = New System.Drawing.Point(164, 236)
        Me.ServerText2GroupBox.Name = "ServerText2GroupBox"
        Me.ServerText2GroupBox.Size = New System.Drawing.Size(150, 45)
        Me.ServerText2GroupBox.TabIndex = 39
        Me.ServerText2GroupBox.TabStop = False
        Me.ServerText2GroupBox.Text = "Server Text 2"
        '
        'ServerText2Panel
        '
        Me.ServerText2Panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ServerText2Panel.Location = New System.Drawing.Point(6, 16)
        Me.ServerText2Panel.Name = "ServerText2Panel"
        Me.ServerText2Panel.Size = New System.Drawing.Size(138, 24)
        Me.ServerText2Panel.TabIndex = 39
        Me.ServerText2Panel.TabStop = True
        '
        'ServerText1GroupBox
        '
        Me.ServerText1GroupBox.Controls.Add(Me.ServerText1Panel)
        Me.ServerText1GroupBox.Location = New System.Drawing.Point(8, 236)
        Me.ServerText1GroupBox.Name = "ServerText1GroupBox"
        Me.ServerText1GroupBox.Size = New System.Drawing.Size(150, 45)
        Me.ServerText1GroupBox.TabIndex = 8
        Me.ServerText1GroupBox.TabStop = False
        Me.ServerText1GroupBox.Text = "Server Text 1"
        '
        'ServerText1Panel
        '
        Me.ServerText1Panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ServerText1Panel.Location = New System.Drawing.Point(6, 16)
        Me.ServerText1Panel.Name = "ServerText1Panel"
        Me.ServerText1Panel.Size = New System.Drawing.Size(138, 24)
        Me.ServerText1Panel.TabIndex = 38
        Me.ServerText1Panel.TabStop = True
        '
        'WarningTextGroupBox
        '
        Me.WarningTextGroupBox.Controls.Add(Me.WarningTextPanel)
        Me.WarningTextGroupBox.Location = New System.Drawing.Point(164, 185)
        Me.WarningTextGroupBox.Name = "WarningTextGroupBox"
        Me.WarningTextGroupBox.Size = New System.Drawing.Size(150, 45)
        Me.WarningTextGroupBox.TabIndex = 7
        Me.WarningTextGroupBox.TabStop = False
        Me.WarningTextGroupBox.Text = "Warning Text"
        '
        'WarningTextPanel
        '
        Me.WarningTextPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.WarningTextPanel.Location = New System.Drawing.Point(6, 16)
        Me.WarningTextPanel.Name = "WarningTextPanel"
        Me.WarningTextPanel.Size = New System.Drawing.Size(138, 24)
        Me.WarningTextPanel.TabIndex = 37
        Me.WarningTextPanel.TabStop = True
        '
        'WhisperTextGroupBox
        '
        Me.WhisperTextGroupBox.Controls.Add(Me.WhisperTextPanel)
        Me.WhisperTextGroupBox.Location = New System.Drawing.Point(164, 134)
        Me.WhisperTextGroupBox.Name = "WhisperTextGroupBox"
        Me.WhisperTextGroupBox.Size = New System.Drawing.Size(150, 45)
        Me.WhisperTextGroupBox.TabIndex = 3
        Me.WhisperTextGroupBox.TabStop = False
        Me.WhisperTextGroupBox.Text = "Whisper Text"
        '
        'WhisperTextPanel
        '
        Me.WhisperTextPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.WhisperTextPanel.Location = New System.Drawing.Point(6, 16)
        Me.WhisperTextPanel.Name = "WhisperTextPanel"
        Me.WhisperTextPanel.Size = New System.Drawing.Size(138, 24)
        Me.WhisperTextPanel.TabIndex = 35
        Me.WhisperTextPanel.TabStop = True
        '
        'AlertTextGroupBox
        '
        Me.AlertTextGroupBox.Controls.Add(Me.AlertTextPanel)
        Me.AlertTextGroupBox.Location = New System.Drawing.Point(8, 185)
        Me.AlertTextGroupBox.Name = "AlertTextGroupBox"
        Me.AlertTextGroupBox.Size = New System.Drawing.Size(150, 45)
        Me.AlertTextGroupBox.TabIndex = 2
        Me.AlertTextGroupBox.TabStop = False
        Me.AlertTextGroupBox.Text = "Alert Text"
        '
        'AlertTextPanel
        '
        Me.AlertTextPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.AlertTextPanel.Location = New System.Drawing.Point(6, 16)
        Me.AlertTextPanel.Name = "AlertTextPanel"
        Me.AlertTextPanel.Size = New System.Drawing.Size(138, 24)
        Me.AlertTextPanel.TabIndex = 33
        Me.AlertTextPanel.TabStop = True
        '
        'EmoteGroupBox
        '
        Me.EmoteGroupBox.Controls.Add(Me.EmoteTextPanel)
        Me.EmoteGroupBox.Location = New System.Drawing.Point(8, 134)
        Me.EmoteGroupBox.Name = "EmoteGroupBox"
        Me.EmoteGroupBox.Size = New System.Drawing.Size(150, 45)
        Me.EmoteGroupBox.TabIndex = 6
        Me.EmoteGroupBox.TabStop = False
        Me.EmoteGroupBox.Text = "Emote Text"
        '
        'EmoteTextPanel
        '
        Me.EmoteTextPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.EmoteTextPanel.Location = New System.Drawing.Point(6, 16)
        Me.EmoteTextPanel.Name = "EmoteTextPanel"
        Me.EmoteTextPanel.Size = New System.Drawing.Size(138, 24)
        Me.EmoteTextPanel.TabIndex = 36
        Me.EmoteTextPanel.TabStop = True
        '
        'ChatTextGroupBox
        '
        Me.ChatTextGroupBox.Controls.Add(Me.ChatTextPanel)
        Me.ChatTextGroupBox.Location = New System.Drawing.Point(164, 83)
        Me.ChatTextGroupBox.Name = "ChatTextGroupBox"
        Me.ChatTextGroupBox.Size = New System.Drawing.Size(150, 45)
        Me.ChatTextGroupBox.TabIndex = 5
        Me.ChatTextGroupBox.TabStop = False
        Me.ChatTextGroupBox.Text = "Chat Text"
        '
        'ChatTextPanel
        '
        Me.ChatTextPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ChatTextPanel.Location = New System.Drawing.Point(6, 16)
        Me.ChatTextPanel.Name = "ChatTextPanel"
        Me.ChatTextPanel.Size = New System.Drawing.Size(138, 24)
        Me.ChatTextPanel.TabIndex = 34
        Me.ChatTextPanel.TabStop = True
        '
        'UserLinkGroupBox
        '
        Me.UserLinkGroupBox.Controls.Add(Me.UserLinkPanel)
        Me.UserLinkGroupBox.Location = New System.Drawing.Point(8, 83)
        Me.UserLinkGroupBox.Name = "UserLinkGroupBox"
        Me.UserLinkGroupBox.Size = New System.Drawing.Size(150, 45)
        Me.UserLinkGroupBox.TabIndex = 4
        Me.UserLinkGroupBox.TabStop = False
        Me.UserLinkGroupBox.Text = "User Link"
        '
        'UserLinkPanel
        '
        Me.UserLinkPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.UserLinkPanel.Location = New System.Drawing.Point(6, 16)
        Me.UserLinkPanel.Name = "UserLinkPanel"
        Me.UserLinkPanel.Size = New System.Drawing.Size(138, 24)
        Me.UserLinkPanel.TabIndex = 32
        Me.UserLinkPanel.TabStop = True
        '
        'BackgroundColorGroupBox
        '
        Me.BackgroundColorGroupBox.Controls.Add(Me.BackgroundColorPanel)
        Me.BackgroundColorGroupBox.Location = New System.Drawing.Point(8, 32)
        Me.BackgroundColorGroupBox.Name = "BackgroundColorGroupBox"
        Me.BackgroundColorGroupBox.Size = New System.Drawing.Size(150, 45)
        Me.BackgroundColorGroupBox.TabIndex = 3
        Me.BackgroundColorGroupBox.TabStop = False
        Me.BackgroundColorGroupBox.Text = "Background"
        '
        'BackgroundColorPanel
        '
        Me.BackgroundColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.BackgroundColorPanel.Location = New System.Drawing.Point(6, 16)
        Me.BackgroundColorPanel.Name = "BackgroundColorPanel"
        Me.BackgroundColorPanel.Size = New System.Drawing.Size(138, 24)
        Me.BackgroundColorPanel.TabIndex = 30
        Me.BackgroundColorPanel.TabStop = True
        '
        'GameDetectionOutlookPanel
        '
        Me.GameDetectionOutlookPanel.BackColor = System.Drawing.Color.LightGray
        Me.GameDetectionOutlookPanel.Controls.Add(Me.GameDetectionLabel)
        Me.GameDetectionOutlookPanel.Controls.Add(Me.DisableGameDetectionButton)
        Me.GameDetectionOutlookPanel.Controls.Add(Me.EnableGameDetectionButton)
        Me.GameDetectionOutlookPanel.HeaderColor1 = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.GameDetectionOutlookPanel.HeaderColor2 = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.GameDetectionOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GameDetectionOutlookPanel.HeaderHeight = 25
        Me.GameDetectionOutlookPanel.HeaderText = "Game Detection"
        Me.GameDetectionOutlookPanel.Icon = Nothing
        Me.GameDetectionOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.GameDetectionOutlookPanel.Location = New System.Drawing.Point(604, 32)
        Me.GameDetectionOutlookPanel.Name = "GameDetectionOutlookPanel"
        Me.GameDetectionOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.GameDetectionOutlookPanel.Size = New System.Drawing.Size(205, 76)
        Me.GameDetectionOutlookPanel.TabIndex = 20
        Me.GameDetectionOutlookPanel.Visible = False
        '
        'GameDetectionLabel
        '
        Me.GameDetectionLabel.ForeColor = System.Drawing.Color.Black
        Me.GameDetectionLabel.Location = New System.Drawing.Point(8, 29)
        Me.GameDetectionLabel.Name = "GameDetectionLabel"
        Me.GameDetectionLabel.Size = New System.Drawing.Size(189, 13)
        Me.GameDetectionLabel.TabIndex = 3
        Me.GameDetectionLabel.Text = "Game Detection Enabled"
        Me.GameDetectionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DisableGameDetectionButton
        '
        Me.DisableGameDetectionButton.Location = New System.Drawing.Point(122, 46)
        Me.DisableGameDetectionButton.Name = "DisableGameDetectionButton"
        Me.DisableGameDetectionButton.Size = New System.Drawing.Size(75, 23)
        Me.DisableGameDetectionButton.TabIndex = 41
        Me.DisableGameDetectionButton.Text = "Disable"
        Me.DisableGameDetectionButton.UseVisualStyleBackColor = True
        '
        'EnableGameDetectionButton
        '
        Me.EnableGameDetectionButton.Location = New System.Drawing.Point(8, 46)
        Me.EnableGameDetectionButton.Name = "EnableGameDetectionButton"
        Me.EnableGameDetectionButton.Size = New System.Drawing.Size(75, 23)
        Me.EnableGameDetectionButton.TabIndex = 40
        Me.EnableGameDetectionButton.Text = "Enable"
        Me.EnableGameDetectionButton.UseVisualStyleBackColor = True
        '
        'ButtonColorsOutlookPanel
        '
        Me.ButtonColorsOutlookPanel.BackColor = System.Drawing.Color.LightGray
        Me.ButtonColorsOutlookPanel.Controls.Add(Me.ButtonColorBottomGroupBox)
        Me.ButtonColorsOutlookPanel.Controls.Add(Me.ButtonColorTopGroupBox)
        Me.ButtonColorsOutlookPanel.HeaderColor1 = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.ButtonColorsOutlookPanel.HeaderColor2 = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.ButtonColorsOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonColorsOutlookPanel.HeaderHeight = 25
        Me.ButtonColorsOutlookPanel.HeaderText = "Button Colors"
        Me.ButtonColorsOutlookPanel.Icon = Nothing
        Me.ButtonColorsOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.ButtonColorsOutlookPanel.Location = New System.Drawing.Point(276, 32)
        Me.ButtonColorsOutlookPanel.Name = "ButtonColorsOutlookPanel"
        Me.ButtonColorsOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.ButtonColorsOutlookPanel.Size = New System.Drawing.Size(322, 76)
        Me.ButtonColorsOutlookPanel.TabIndex = 19
        '
        'ButtonColorBottomGroupBox
        '
        Me.ButtonColorBottomGroupBox.Controls.Add(Me.ButtonColorBottomPanel)
        Me.ButtonColorBottomGroupBox.Location = New System.Drawing.Point(164, 26)
        Me.ButtonColorBottomGroupBox.Name = "ButtonColorBottomGroupBox"
        Me.ButtonColorBottomGroupBox.Size = New System.Drawing.Size(150, 45)
        Me.ButtonColorBottomGroupBox.TabIndex = 3
        Me.ButtonColorBottomGroupBox.TabStop = False
        Me.ButtonColorBottomGroupBox.Text = "Bottom Gradient"
        '
        'ButtonColorBottomPanel
        '
        Me.ButtonColorBottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ButtonColorBottomPanel.Location = New System.Drawing.Point(6, 16)
        Me.ButtonColorBottomPanel.Name = "ButtonColorBottomPanel"
        Me.ButtonColorBottomPanel.Size = New System.Drawing.Size(138, 24)
        Me.ButtonColorBottomPanel.TabIndex = 29
        Me.ButtonColorBottomPanel.TabStop = True
        '
        'ButtonColorTopGroupBox
        '
        Me.ButtonColorTopGroupBox.Controls.Add(Me.ButtonColorTopPanel)
        Me.ButtonColorTopGroupBox.Location = New System.Drawing.Point(8, 26)
        Me.ButtonColorTopGroupBox.Name = "ButtonColorTopGroupBox"
        Me.ButtonColorTopGroupBox.Size = New System.Drawing.Size(150, 45)
        Me.ButtonColorTopGroupBox.TabIndex = 2
        Me.ButtonColorTopGroupBox.TabStop = False
        Me.ButtonColorTopGroupBox.Text = "Top Gradient"
        '
        'ButtonColorTopPanel
        '
        Me.ButtonColorTopPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ButtonColorTopPanel.Location = New System.Drawing.Point(6, 16)
        Me.ButtonColorTopPanel.Name = "ButtonColorTopPanel"
        Me.ButtonColorTopPanel.Size = New System.Drawing.Size(138, 24)
        Me.ButtonColorTopPanel.TabIndex = 28
        Me.ButtonColorTopPanel.TabStop = True
        '
        'NetworkDetailsOutlookPanel
        '
        Me.NetworkDetailsOutlookPanel.BackColor = System.Drawing.Color.LightGray
        Me.NetworkDetailsOutlookPanel.Controls.Add(Me.NetworkAdapterComboBox)
        Me.NetworkDetailsOutlookPanel.Controls.Add(Me.NetworkAdapterLabel)
        Me.NetworkDetailsOutlookPanel.HeaderColor1 = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.NetworkDetailsOutlookPanel.HeaderColor2 = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.NetworkDetailsOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NetworkDetailsOutlookPanel.HeaderHeight = 25
        Me.NetworkDetailsOutlookPanel.HeaderText = "Network Details"
        Me.NetworkDetailsOutlookPanel.Icon = Nothing
        Me.NetworkDetailsOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.NetworkDetailsOutlookPanel.Location = New System.Drawing.Point(8, 32)
        Me.NetworkDetailsOutlookPanel.Name = "NetworkDetailsOutlookPanel"
        Me.NetworkDetailsOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.NetworkDetailsOutlookPanel.Size = New System.Drawing.Size(262, 76)
        Me.NetworkDetailsOutlookPanel.TabIndex = 18
        '
        'NetworkAdapterComboBox
        '
        Me.NetworkAdapterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.NetworkAdapterComboBox.FormattingEnabled = True
        Me.NetworkAdapterComboBox.Location = New System.Drawing.Point(8, 46)
        Me.NetworkAdapterComboBox.Name = "NetworkAdapterComboBox"
        Me.NetworkAdapterComboBox.Size = New System.Drawing.Size(214, 21)
        Me.NetworkAdapterComboBox.TabIndex = 20
        '
        'NetworkAdapterLabel
        '
        Me.NetworkAdapterLabel.AutoSize = True
        Me.NetworkAdapterLabel.ForeColor = System.Drawing.Color.Black
        Me.NetworkAdapterLabel.Location = New System.Drawing.Point(8, 29)
        Me.NetworkAdapterLabel.Name = "NetworkAdapterLabel"
        Me.NetworkAdapterLabel.Size = New System.Drawing.Size(155, 13)
        Me.NetworkAdapterLabel.TabIndex = 2
        Me.NetworkAdapterLabel.Text = "Please Select Network Adapter"
        '
        'BrowserTabPage
        '
        Me.BrowserTabPage.BackColor = System.Drawing.Color.Black
        Me.BrowserTabPage.Controls.Add(Me.MiscWBAddressTextBox)
        Me.BrowserTabPage.Controls.Add(Me.MiscWBForwardButton)
        Me.BrowserTabPage.Controls.Add(Me.MiscWBBackButton)
        Me.BrowserTabPage.Controls.Add(Me.MiscWebBrowser)
        Me.BrowserTabPage.Location = New System.Drawing.Point(4, 22)
        Me.BrowserTabPage.Name = "BrowserTabPage"
        Me.BrowserTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.BrowserTabPage.Size = New System.Drawing.Size(991, 650)
        Me.BrowserTabPage.TabIndex = 2
        Me.BrowserTabPage.Text = "Browser"
        '
        'MiscWBAddressTextBox
        '
        Me.MiscWBAddressTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MiscWBAddressTextBox.Location = New System.Drawing.Point(168, 51)
        Me.MiscWBAddressTextBox.Name = "MiscWBAddressTextBox"
        Me.MiscWBAddressTextBox.Size = New System.Drawing.Size(817, 21)
        Me.MiscWBAddressTextBox.TabIndex = 45
        '
        'MiscWBForwardButton
        '
        Me.MiscWBForwardButton.BackColor = System.Drawing.SystemColors.Control
        Me.MiscWBForwardButton.Location = New System.Drawing.Point(87, 51)
        Me.MiscWBForwardButton.Name = "MiscWBForwardButton"
        Me.MiscWBForwardButton.Size = New System.Drawing.Size(75, 23)
        Me.MiscWBForwardButton.TabIndex = 44
        Me.MiscWBForwardButton.Text = "Forward"
        Me.MiscWBForwardButton.UseVisualStyleBackColor = False
        '
        'MiscWBBackButton
        '
        Me.MiscWBBackButton.BackColor = System.Drawing.SystemColors.Control
        Me.MiscWBBackButton.Location = New System.Drawing.Point(6, 51)
        Me.MiscWBBackButton.Name = "MiscWBBackButton"
        Me.MiscWBBackButton.Size = New System.Drawing.Size(75, 23)
        Me.MiscWBBackButton.TabIndex = 43
        Me.MiscWBBackButton.Text = "Back"
        Me.MiscWBBackButton.UseVisualStyleBackColor = False
        '
        'MiscWebBrowser
        '
        Me.MiscWebBrowser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MiscWebBrowser.Location = New System.Drawing.Point(6, 80)
        Me.MiscWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.MiscWebBrowser.Name = "MiscWebBrowser"
        Me.MiscWebBrowser.Size = New System.Drawing.Size(979, 561)
        Me.MiscWebBrowser.TabIndex = 42
        '
        'GameContextMenuStrip
        '
        Me.GameContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyIPToolStripMenuItem})
        Me.GameContextMenuStrip.Name = "gameContextMenuStrip"
        Me.GameContextMenuStrip.Size = New System.Drawing.Size(116, 26)
        '
        'CopyIPToolStripMenuItem
        '
        Me.CopyIPToolStripMenuItem.Image = Global.LobbyClient.My.Resources.Resources.copy
        Me.CopyIPToolStripMenuItem.Name = "CopyIPToolStripMenuItem"
        Me.CopyIPToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.CopyIPToolStripMenuItem.Text = "Copy IP"
        '
        'ChatSplitContainer
        '
        Me.ChatSplitContainer.BackColor = System.Drawing.SystemColors.Control
        Me.ChatSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChatSplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.ChatSplitContainer.Name = "ChatSplitContainer"
        '
        'ChatSplitContainer.Panel1
        '
        Me.ChatSplitContainer.Panel1.BackColor = System.Drawing.Color.Black
        Me.ChatSplitContainer.Panel1.Controls.Add(Me.ChatOutlookPanel)
        '
        'ChatSplitContainer.Panel2
        '
        Me.ChatSplitContainer.Panel2.BackColor = System.Drawing.Color.Black
        Me.ChatSplitContainer.Panel2.Controls.Add(Me.UsersOutlookPanel)
        Me.ChatSplitContainer.Size = New System.Drawing.Size(991, 416)
        Me.ChatSplitContainer.SplitterDistance = 733
        Me.ChatSplitContainer.TabIndex = 0
        '
        'ChatOutlookPanel
        '
        Me.ChatOutlookPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChatOutlookPanel.BackColor = System.Drawing.Color.Black
        Me.ChatOutlookPanel.Controls.Add(Me.AdvancedButton3)
        Me.ChatOutlookPanel.Controls.Add(Me.ChatConsoleGroupBox)
        Me.ChatOutlookPanel.Controls.Add(Me.ChatWebBrowser)
        Me.ChatOutlookPanel.HeaderColor1 = System.Drawing.Color.SlateGray
        Me.ChatOutlookPanel.HeaderColor2 = System.Drawing.Color.Black
        Me.ChatOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChatOutlookPanel.HeaderHeight = 25
        Me.ChatOutlookPanel.HeaderText = "Lobby Chat"
        Me.ChatOutlookPanel.Icon = Nothing
        Me.ChatOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.ChatOutlookPanel.Location = New System.Drawing.Point(1, 1)
        Me.ChatOutlookPanel.Name = "ChatOutlookPanel"
        Me.ChatOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.ChatOutlookPanel.Size = New System.Drawing.Size(731, 413)
        Me.ChatOutlookPanel.TabIndex = 1
        '
        'AdvancedButton3
        '
        Me.AdvancedButton3.BackColor = System.Drawing.Color.Transparent
        Me.AdvancedButton3.BorderColor = System.Drawing.Color.White
        Me.AdvancedButton3.ButtonStyle = CustomPanels.AdvancedButton.ButtonStyles.Rectangle
        Me.AdvancedButton3.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.AdvancedButton3.ForeColor = System.Drawing.Color.White
        Me.AdvancedButton3.GradientColor1 = System.Drawing.Color.SlateGray
        Me.AdvancedButton3.GradientColor2 = System.Drawing.Color.Black
        Me.AdvancedButton3.GradientColor3 = System.Drawing.Color.SlateGray
        Me.AdvancedButton3.GradientColor4 = System.Drawing.Color.Black
        Me.AdvancedButton3.Icon = Nothing
        Me.AdvancedButton3.Location = New System.Drawing.Point(992, -216)
        Me.AdvancedButton3.Name = "AdvancedButton3"
        Me.AdvancedButton3.RoundedRadius = 10
        Me.AdvancedButton3.Size = New System.Drawing.Size(110, 45)
        Me.AdvancedButton3.TabIndex = 11
        Me.AdvancedButton3.Text = "Misc"
        Me.AdvancedButton3.TextHorizontalAlignment = CustomPanels.AdvancedButton.HorizontalAlignments.Center
        Me.AdvancedButton3.TextVerticleAlignment = CustomPanels.AdvancedButton.VerticleAlignments.Middle
        '
        'ChatConsoleGroupBox
        '
        Me.ChatConsoleGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChatConsoleGroupBox.Controls.Add(Me.SendButton)
        Me.ChatConsoleGroupBox.Controls.Add(Me.ChatComboBox)
        Me.ChatConsoleGroupBox.Controls.Add(Me.ChatTextBox)
        Me.ChatConsoleGroupBox.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChatConsoleGroupBox.ForeColor = System.Drawing.Color.White
        Me.ChatConsoleGroupBox.Location = New System.Drawing.Point(5, 335)
        Me.ChatConsoleGroupBox.Name = "ChatConsoleGroupBox"
        Me.ChatConsoleGroupBox.Size = New System.Drawing.Size(721, 71)
        Me.ChatConsoleGroupBox.TabIndex = 2
        Me.ChatConsoleGroupBox.TabStop = False
        Me.ChatConsoleGroupBox.Text = "Chat Console"
        '
        'SendButton
        '
        Me.SendButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SendButton.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.SendButton.ForeColor = System.Drawing.Color.Black
        Me.SendButton.Location = New System.Drawing.Point(642, 43)
        Me.SendButton.Name = "SendButton"
        Me.SendButton.Size = New System.Drawing.Size(73, 23)
        Me.SendButton.TabIndex = 12
        Me.SendButton.Text = "Send"
        Me.SendButton.UseVisualStyleBackColor = False
        '
        'ChatComboBox
        '
        Me.ChatComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChatComboBox.FormattingEnabled = True
        Me.ChatComboBox.Location = New System.Drawing.Point(642, 16)
        Me.ChatComboBox.Name = "ChatComboBox"
        Me.ChatComboBox.Size = New System.Drawing.Size(73, 21)
        Me.ChatComboBox.TabIndex = 11
        '
        'ChatTextBox
        '
        Me.ChatTextBox.AcceptsReturn = True
        Me.ChatTextBox.AcceptsTab = True
        Me.ChatTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChatTextBox.HideSelection = False
        Me.ChatTextBox.Location = New System.Drawing.Point(6, 16)
        Me.ChatTextBox.MaxLength = 250
        Me.ChatTextBox.Multiline = True
        Me.ChatTextBox.Name = "ChatTextBox"
        Me.ChatTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ChatTextBox.Size = New System.Drawing.Size(630, 49)
        Me.ChatTextBox.TabIndex = 10
        '
        'ChatWebBrowser
        '
        Me.ChatWebBrowser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChatWebBrowser.Location = New System.Drawing.Point(5, 29)
        Me.ChatWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.ChatWebBrowser.Name = "ChatWebBrowser"
        Me.ChatWebBrowser.Size = New System.Drawing.Size(721, 303)
        Me.ChatWebBrowser.TabIndex = 9
        '
        'UsersOutlookPanel
        '
        Me.UsersOutlookPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsersOutlookPanel.BackColor = System.Drawing.Color.Black
        Me.UsersOutlookPanel.Controls.Add(Me.UsersOnlineLabel)
        Me.UsersOutlookPanel.Controls.Add(Me.UsersTabControl)
        Me.UsersOutlookPanel.HeaderColor1 = System.Drawing.Color.SlateGray
        Me.UsersOutlookPanel.HeaderColor2 = System.Drawing.Color.Black
        Me.UsersOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsersOutlookPanel.HeaderHeight = 25
        Me.UsersOutlookPanel.HeaderText = "Users & Friends"
        Me.UsersOutlookPanel.Icon = Nothing
        Me.UsersOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.UsersOutlookPanel.Location = New System.Drawing.Point(1, 1)
        Me.UsersOutlookPanel.Name = "UsersOutlookPanel"
        Me.UsersOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.UsersOutlookPanel.Size = New System.Drawing.Size(252, 413)
        Me.UsersOutlookPanel.TabIndex = 2
        '
        'UsersOnlineLabel
        '
        Me.UsersOnlineLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsersOnlineLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsersOnlineLabel.ForeColor = System.Drawing.Color.White
        Me.UsersOnlineLabel.Location = New System.Drawing.Point(2, 32)
        Me.UsersOnlineLabel.Name = "UsersOnlineLabel"
        Me.UsersOnlineLabel.Size = New System.Drawing.Size(242, 20)
        Me.UsersOnlineLabel.TabIndex = 1
        Me.UsersOnlineLabel.Text = "Currently 3 Users Online"
        Me.UsersOnlineLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'UsersTabControl
        '
        Me.UsersTabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsersTabControl.Controls.Add(Me.OnlineTabPage)
        Me.UsersTabControl.Controls.Add(Me.FriendsTabPage)
        Me.UsersTabControl.Controls.Add(Me.CheatersTabPage)
        Me.UsersTabControl.Controls.Add(Me.IgnoreTabPage)
        Me.UsersTabControl.Location = New System.Drawing.Point(5, 55)
        Me.UsersTabControl.Name = "UsersTabControl"
        Me.UsersTabControl.SelectedIndex = 0
        Me.UsersTabControl.Size = New System.Drawing.Size(242, 351)
        Me.UsersTabControl.TabIndex = 13
        '
        'OnlineTabPage
        '
        Me.OnlineTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.OnlineTabPage.Controls.Add(Me.OnlineListView)
        Me.OnlineTabPage.Location = New System.Drawing.Point(4, 22)
        Me.OnlineTabPage.Name = "OnlineTabPage"
        Me.OnlineTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.OnlineTabPage.Size = New System.Drawing.Size(234, 325)
        Me.OnlineTabPage.TabIndex = 0
        Me.OnlineTabPage.Text = "Online"
        '
        'OnlineListView
        '
        Me.OnlineListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OnlineListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.OnlineListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OnlineListColumnHeader})
        Me.OnlineListView.ContextMenuStrip = Me.UsersContextMenuStrip
        ListViewGroup12.Header = "Administrators"
        ListViewGroup12.Name = "ADMINISTRATOR"
        ListViewGroup13.Header = "Moderators"
        ListViewGroup13.Name = "MODERATOR"
        ListViewGroup14.Header = "Friends"
        ListViewGroup14.Name = "FRIEND"
        ListViewGroup15.Header = "Donators"
        ListViewGroup15.Name = "DONATOR"
        ListViewGroup16.Header = "Users"
        ListViewGroup16.Name = "USER"
        Me.OnlineListView.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup12, ListViewGroup13, ListViewGroup14, ListViewGroup15, ListViewGroup16})
        Me.OnlineListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.OnlineListView.Location = New System.Drawing.Point(0, 0)
        Me.OnlineListView.MultiSelect = False
        Me.OnlineListView.Name = "OnlineListView"
        Me.OnlineListView.Size = New System.Drawing.Size(234, 325)
        Me.OnlineListView.SmallImageList = Me.IconsImageList
        Me.OnlineListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.OnlineListView.TabIndex = 14
        Me.OnlineListView.UseCompatibleStateImageBehavior = False
        Me.OnlineListView.View = System.Windows.Forms.View.Details
        '
        'FriendsTabPage
        '
        Me.FriendsTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.FriendsTabPage.Controls.Add(Me.FriendsListView)
        Me.FriendsTabPage.Location = New System.Drawing.Point(4, 22)
        Me.FriendsTabPage.Name = "FriendsTabPage"
        Me.FriendsTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.FriendsTabPage.Size = New System.Drawing.Size(234, 325)
        Me.FriendsTabPage.TabIndex = 1
        Me.FriendsTabPage.Text = "Friends"
        '
        'FriendsListView
        '
        Me.FriendsListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FriendsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FriendsListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FriendsListColumnHeader})
        Me.FriendsListView.ContextMenuStrip = Me.UsersContextMenuStrip
        ListViewGroup1.Header = "Online"
        ListViewGroup1.Name = "ONLINE"
        ListViewGroup2.Header = "Offline"
        ListViewGroup2.Name = "OFFLINE"
        Me.FriendsListView.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.FriendsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.FriendsListView.Location = New System.Drawing.Point(0, 0)
        Me.FriendsListView.MultiSelect = False
        Me.FriendsListView.Name = "FriendsListView"
        Me.FriendsListView.Size = New System.Drawing.Size(234, 325)
        Me.FriendsListView.SmallImageList = Me.IconsImageList
        Me.FriendsListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.FriendsListView.TabIndex = 15
        Me.FriendsListView.UseCompatibleStateImageBehavior = False
        Me.FriendsListView.View = System.Windows.Forms.View.Details
        '
        'CheatersTabPage
        '
        Me.CheatersTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.CheatersTabPage.Controls.Add(Me.CheatersListView)
        Me.CheatersTabPage.Location = New System.Drawing.Point(4, 22)
        Me.CheatersTabPage.Name = "CheatersTabPage"
        Me.CheatersTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.CheatersTabPage.Size = New System.Drawing.Size(234, 325)
        Me.CheatersTabPage.TabIndex = 2
        Me.CheatersTabPage.Text = "Cheaters"
        '
        'CheatersListView
        '
        Me.CheatersListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheatersListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CheatersListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.CheatersListColumnHeader})
        Me.CheatersListView.ContextMenuStrip = Me.UsersContextMenuStrip
        ListViewGroup3.Header = "Online"
        ListViewGroup3.Name = "ONLINE"
        ListViewGroup4.Header = "Offline"
        ListViewGroup4.Name = "OFFLINE"
        Me.CheatersListView.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup3, ListViewGroup4})
        Me.CheatersListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.CheatersListView.Location = New System.Drawing.Point(0, 0)
        Me.CheatersListView.MultiSelect = False
        Me.CheatersListView.Name = "CheatersListView"
        Me.CheatersListView.Size = New System.Drawing.Size(234, 325)
        Me.CheatersListView.SmallImageList = Me.IconsImageList
        Me.CheatersListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.CheatersListView.TabIndex = 16
        Me.CheatersListView.UseCompatibleStateImageBehavior = False
        Me.CheatersListView.View = System.Windows.Forms.View.Details
        '
        'IgnoreTabPage
        '
        Me.IgnoreTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.IgnoreTabPage.Controls.Add(Me.IgnoreListView)
        Me.IgnoreTabPage.Location = New System.Drawing.Point(4, 22)
        Me.IgnoreTabPage.Name = "IgnoreTabPage"
        Me.IgnoreTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.IgnoreTabPage.Size = New System.Drawing.Size(234, 325)
        Me.IgnoreTabPage.TabIndex = 3
        Me.IgnoreTabPage.Text = "Ignore"
        '
        'IgnoreListView
        '
        Me.IgnoreListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IgnoreListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IgnoreListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.IgnoreColumnHeader})
        Me.IgnoreListView.ContextMenuStrip = Me.UsersContextMenuStrip
        ListViewGroup5.Header = "Online"
        ListViewGroup5.Name = "ONLINE"
        ListViewGroup6.Header = "Offline"
        ListViewGroup6.Name = "OFFLINE"
        Me.IgnoreListView.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup5, ListViewGroup6})
        Me.IgnoreListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.IgnoreListView.Location = New System.Drawing.Point(0, 0)
        Me.IgnoreListView.MultiSelect = False
        Me.IgnoreListView.Name = "IgnoreListView"
        Me.IgnoreListView.Size = New System.Drawing.Size(234, 325)
        Me.IgnoreListView.SmallImageList = Me.IconsImageList
        Me.IgnoreListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.IgnoreListView.TabIndex = 17
        Me.IgnoreListView.UseCompatibleStateImageBehavior = False
        Me.IgnoreListView.View = System.Windows.Forms.View.Details
        '
        'LobbyTabPage
        '
        Me.LobbyTabPage.BackColor = System.Drawing.Color.Black
        Me.LobbyTabPage.Controls.Add(Me.MainSplitContainer)
        Me.LobbyTabPage.Location = New System.Drawing.Point(4, 22)
        Me.LobbyTabPage.Name = "LobbyTabPage"
        Me.LobbyTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.LobbyTabPage.Size = New System.Drawing.Size(991, 650)
        Me.LobbyTabPage.TabIndex = 0
        Me.LobbyTabPage.Text = "Lobby"
        '
        'MainSplitContainer
        '
        Me.MainSplitContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MainSplitContainer.BackColor = System.Drawing.SystemColors.Control
        Me.MainSplitContainer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.MainSplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.MainSplitContainer.Name = "MainSplitContainer"
        Me.MainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'MainSplitContainer.Panel1
        '
        Me.MainSplitContainer.Panel1.BackColor = System.Drawing.Color.Black
        Me.MainSplitContainer.Panel1.Controls.Add(Me.GamesOutlookPanel)
        '
        'MainSplitContainer.Panel2
        '
        Me.MainSplitContainer.Panel2.BackColor = System.Drawing.Color.Black
        Me.MainSplitContainer.Panel2.Controls.Add(Me.ChatSplitContainer)
        Me.MainSplitContainer.Size = New System.Drawing.Size(991, 650)
        Me.MainSplitContainer.SplitterDistance = 230
        Me.MainSplitContainer.TabIndex = 0
        '
        'GamesOutlookPanel
        '
        Me.GamesOutlookPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GamesOutlookPanel.BackColor = System.Drawing.Color.Black
        Me.GamesOutlookPanel.Controls.Add(Me.GamesListView)
        Me.GamesOutlookPanel.HeaderColor1 = System.Drawing.Color.SlateGray
        Me.GamesOutlookPanel.HeaderColor2 = System.Drawing.Color.Black
        Me.GamesOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GamesOutlookPanel.HeaderHeight = 25
        Me.GamesOutlookPanel.HeaderText = "Current Games"
        Me.GamesOutlookPanel.Icon = Nothing
        Me.GamesOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.GamesOutlookPanel.Location = New System.Drawing.Point(1, 46)
        Me.GamesOutlookPanel.Name = "GamesOutlookPanel"
        Me.GamesOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.GamesOutlookPanel.Size = New System.Drawing.Size(989, 184)
        Me.GamesOutlookPanel.TabIndex = 0
        '
        'GamesListView
        '
        Me.GamesListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GamesListView.BackColor = System.Drawing.Color.Black
        Me.GamesListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.GameColumnHeader, Me.GameNameColumnHeader, Me.EpochColumnHeader, Me.ResourcesColumnHeader, Me.MapTypeColumnHeader, Me.MapSizeColumnHeader, Me.HostColumnHeader, Me.IPColumnHeader, Me.PlayersColumnHeader})
        Me.GamesListView.ContextMenuStrip = Me.GameContextMenuStrip
        Me.GamesListView.ForeColor = System.Drawing.Color.White
        Me.GamesListView.FullRowSelect = True
        Me.GamesListView.Location = New System.Drawing.Point(5, 27)
        Me.GamesListView.MultiSelect = False
        Me.GamesListView.Name = "GamesListView"
        Me.GamesListView.Size = New System.Drawing.Size(979, 150)
        Me.GamesListView.SmallImageList = Me.IconsImageList
        Me.GamesListView.TabIndex = 8
        Me.GamesListView.UseCompatibleStateImageBehavior = False
        Me.GamesListView.View = System.Windows.Forms.View.Details
        '
        'GameColumnHeader
        '
        Me.GameColumnHeader.Text = ""
        Me.GameColumnHeader.Width = 24
        '
        'GameNameColumnHeader
        '
        Me.GameNameColumnHeader.Text = "Game Name"
        Me.GameNameColumnHeader.Width = 76
        '
        'EpochColumnHeader
        '
        Me.EpochColumnHeader.Text = "Epoch"
        Me.EpochColumnHeader.Width = 93
        '
        'ResourcesColumnHeader
        '
        Me.ResourcesColumnHeader.Text = "Resources"
        Me.ResourcesColumnHeader.Width = 102
        '
        'MapTypeColumnHeader
        '
        Me.MapTypeColumnHeader.Text = "Map Type"
        Me.MapTypeColumnHeader.Width = 98
        '
        'MapSizeColumnHeader
        '
        Me.MapSizeColumnHeader.Text = "Map Size"
        Me.MapSizeColumnHeader.Width = 67
        '
        'HostColumnHeader
        '
        Me.HostColumnHeader.Text = "Host"
        Me.HostColumnHeader.Width = 82
        '
        'IPColumnHeader
        '
        Me.IPColumnHeader.Text = "IP"
        Me.IPColumnHeader.Width = 92
        '
        'PlayersColumnHeader
        '
        Me.PlayersColumnHeader.Text = "Players"
        Me.PlayersColumnHeader.Width = 47
        '
        'MainTabControl
        '
        Me.MainTabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MainTabControl.Controls.Add(Me.LobbyTabPage)
        Me.MainTabControl.Controls.Add(Me.PatchTabPage)
        Me.MainTabControl.Controls.Add(Me.BrowserTabPage)
        Me.MainTabControl.Controls.Add(Me.OptionsTabPage)
        Me.MainTabControl.Location = New System.Drawing.Point(-4, -22)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(999, 676)
        Me.MainTabControl.TabIndex = 0
        Me.MainTabControl.TabStop = False
        '
        'PatchTabPage
        '
        Me.PatchTabPage.BackColor = System.Drawing.Color.Black
        Me.PatchTabPage.Controls.Add(Me.PatchOutlookPanel)
        Me.PatchTabPage.Location = New System.Drawing.Point(4, 22)
        Me.PatchTabPage.Name = "PatchTabPage"
        Me.PatchTabPage.Size = New System.Drawing.Size(991, 650)
        Me.PatchTabPage.TabIndex = 3
        Me.PatchTabPage.Text = "Patch"
        '
        'PatchOutlookPanel
        '
        Me.PatchOutlookPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PatchOutlookPanel.BackColor = System.Drawing.Color.Black
        Me.PatchOutlookPanel.HeaderColor1 = System.Drawing.Color.SlateGray
        Me.PatchOutlookPanel.HeaderColor2 = System.Drawing.Color.Black
        Me.PatchOutlookPanel.HeaderFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PatchOutlookPanel.HeaderHeight = 25
        Me.PatchOutlookPanel.HeaderText = "Patch"
        Me.PatchOutlookPanel.Icon = Nothing
        Me.PatchOutlookPanel.IconTransparentColor = System.Drawing.Color.White
        Me.PatchOutlookPanel.Location = New System.Drawing.Point(1, 46)
        Me.PatchOutlookPanel.Name = "PatchOutlookPanel"
        Me.PatchOutlookPanel.Padding = New System.Windows.Forms.Padding(5, 29, 5, 4)
        Me.PatchOutlookPanel.Size = New System.Drawing.Size(989, 527)
        Me.PatchOutlookPanel.TabIndex = 13
        '
        'TrayNotifyIcon
        '
        Me.TrayNotifyIcon.ContextMenuStrip = Me.TrayContextMenuStrip
        Me.TrayNotifyIcon.Icon = CType(resources.GetObject("TrayNotifyIcon.Icon"), System.Drawing.Icon)
        Me.TrayNotifyIcon.Text = "Save-EE Lobby Client"
        '
        'TrayContextMenuStrip
        '
        Me.TrayContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowSaveEELobbyClientToolStripMenuItem, Me.TrayToolStripSeparator, Me.ExitToolStripMenuItem})
        Me.TrayContextMenuStrip.Name = "TrayContextMenuStrip"
        Me.TrayContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.TrayContextMenuStrip.ShowCheckMargin = True
        Me.TrayContextMenuStrip.ShowImageMargin = False
        Me.TrayContextMenuStrip.Size = New System.Drawing.Size(225, 54)
        '
        'ShowSaveEELobbyClientToolStripMenuItem
        '
        Me.ShowSaveEELobbyClientToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShowSaveEELobbyClientToolStripMenuItem.Name = "ShowSaveEELobbyClientToolStripMenuItem"
        Me.ShowSaveEELobbyClientToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.ShowSaveEELobbyClientToolStripMenuItem.Text = "Show Save-EE Lobby Client"
        '
        'TrayToolStripSeparator
        '
        Me.TrayToolStripSeparator.Name = "TrayToolStripSeparator"
        Me.TrayToolStripSeparator.Size = New System.Drawing.Size(221, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(224, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'FacebookAdvancedButton
        '
        Me.FacebookAdvancedButton.BackColor = System.Drawing.Color.Transparent
        Me.FacebookAdvancedButton.BorderColor = System.Drawing.Color.White
        Me.FacebookAdvancedButton.ButtonStyle = CustomPanels.AdvancedButton.ButtonStyles.Rectangle
        Me.FacebookAdvancedButton.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.FacebookAdvancedButton.ForeColor = System.Drawing.Color.White
        Me.FacebookAdvancedButton.GradientColor1 = System.Drawing.Color.SlateGray
        Me.FacebookAdvancedButton.GradientColor2 = System.Drawing.Color.Black
        Me.FacebookAdvancedButton.GradientColor3 = System.Drawing.Color.SlateGray
        Me.FacebookAdvancedButton.GradientColor4 = System.Drawing.Color.Black
        Me.FacebookAdvancedButton.Icon = Nothing
        Me.FacebookAdvancedButton.Location = New System.Drawing.Point(880, 0)
        Me.FacebookAdvancedButton.Name = "FacebookAdvancedButton"
        Me.FacebookAdvancedButton.RoundedRadius = 10
        Me.FacebookAdvancedButton.Size = New System.Drawing.Size(110, 45)
        Me.FacebookAdvancedButton.TabIndex = 10
        Me.FacebookAdvancedButton.Text = "Facebook"
        Me.FacebookAdvancedButton.TextHorizontalAlignment = CustomPanels.AdvancedButton.HorizontalAlignments.Center
        Me.FacebookAdvancedButton.TextVerticleAlignment = CustomPanels.AdvancedButton.VerticleAlignments.Middle
        '
        'OptionsAdvancedButton
        '
        Me.OptionsAdvancedButton.BackColor = System.Drawing.Color.Transparent
        Me.OptionsAdvancedButton.BorderColor = System.Drawing.Color.White
        Me.OptionsAdvancedButton.ButtonStyle = CustomPanels.AdvancedButton.ButtonStyles.Rectangle
        Me.OptionsAdvancedButton.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.OptionsAdvancedButton.ForeColor = System.Drawing.Color.White
        Me.OptionsAdvancedButton.GradientColor1 = System.Drawing.Color.SlateGray
        Me.OptionsAdvancedButton.GradientColor2 = System.Drawing.Color.Black
        Me.OptionsAdvancedButton.GradientColor3 = System.Drawing.Color.SlateGray
        Me.OptionsAdvancedButton.GradientColor4 = System.Drawing.Color.Black
        Me.OptionsAdvancedButton.Icon = Nothing
        Me.OptionsAdvancedButton.Location = New System.Drawing.Point(220, 0)
        Me.OptionsAdvancedButton.Name = "OptionsAdvancedButton"
        Me.OptionsAdvancedButton.RoundedRadius = 10
        Me.OptionsAdvancedButton.Size = New System.Drawing.Size(110, 45)
        Me.OptionsAdvancedButton.TabIndex = 3
        Me.OptionsAdvancedButton.Text = "Options"
        Me.OptionsAdvancedButton.TextHorizontalAlignment = CustomPanels.AdvancedButton.HorizontalAlignments.Center
        Me.OptionsAdvancedButton.TextVerticleAlignment = CustomPanels.AdvancedButton.VerticleAlignments.Middle
        '
        'PatchAdvancedButton
        '
        Me.PatchAdvancedButton.BackColor = System.Drawing.Color.Transparent
        Me.PatchAdvancedButton.BorderColor = System.Drawing.Color.White
        Me.PatchAdvancedButton.ButtonStyle = CustomPanels.AdvancedButton.ButtonStyles.Rectangle
        Me.PatchAdvancedButton.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.PatchAdvancedButton.ForeColor = System.Drawing.Color.White
        Me.PatchAdvancedButton.GradientColor1 = System.Drawing.Color.SlateGray
        Me.PatchAdvancedButton.GradientColor2 = System.Drawing.Color.Black
        Me.PatchAdvancedButton.GradientColor3 = System.Drawing.Color.SlateGray
        Me.PatchAdvancedButton.GradientColor4 = System.Drawing.Color.Black
        Me.PatchAdvancedButton.Icon = Nothing
        Me.PatchAdvancedButton.Location = New System.Drawing.Point(110, 0)
        Me.PatchAdvancedButton.Name = "PatchAdvancedButton"
        Me.PatchAdvancedButton.RoundedRadius = 10
        Me.PatchAdvancedButton.Size = New System.Drawing.Size(110, 45)
        Me.PatchAdvancedButton.TabIndex = 2
        Me.PatchAdvancedButton.Text = "Patch"
        Me.PatchAdvancedButton.TextHorizontalAlignment = CustomPanels.AdvancedButton.HorizontalAlignments.Center
        Me.PatchAdvancedButton.TextVerticleAlignment = CustomPanels.AdvancedButton.VerticleAlignments.Middle
        '
        'LadderAdvancedButton
        '
        Me.LadderAdvancedButton.BackColor = System.Drawing.Color.Transparent
        Me.LadderAdvancedButton.BorderColor = System.Drawing.Color.White
        Me.LadderAdvancedButton.ButtonStyle = CustomPanels.AdvancedButton.ButtonStyles.Rectangle
        Me.LadderAdvancedButton.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.LadderAdvancedButton.ForeColor = System.Drawing.Color.White
        Me.LadderAdvancedButton.GradientColor1 = System.Drawing.Color.SlateGray
        Me.LadderAdvancedButton.GradientColor2 = System.Drawing.Color.Black
        Me.LadderAdvancedButton.GradientColor3 = System.Drawing.Color.SlateGray
        Me.LadderAdvancedButton.GradientColor4 = System.Drawing.Color.Black
        Me.LadderAdvancedButton.Icon = Nothing
        Me.LadderAdvancedButton.Location = New System.Drawing.Point(770, 0)
        Me.LadderAdvancedButton.Name = "LadderAdvancedButton"
        Me.LadderAdvancedButton.RoundedRadius = 10
        Me.LadderAdvancedButton.Size = New System.Drawing.Size(110, 45)
        Me.LadderAdvancedButton.TabIndex = 6
        Me.LadderAdvancedButton.Text = "Ladder"
        Me.LadderAdvancedButton.TextHorizontalAlignment = CustomPanels.AdvancedButton.HorizontalAlignments.Center
        Me.LadderAdvancedButton.TextVerticleAlignment = CustomPanels.AdvancedButton.VerticleAlignments.Middle
        '
        'HelpAdvancedButton
        '
        Me.HelpAdvancedButton.BackColor = System.Drawing.Color.Transparent
        Me.HelpAdvancedButton.BorderColor = System.Drawing.Color.White
        Me.HelpAdvancedButton.ButtonStyle = CustomPanels.AdvancedButton.ButtonStyles.Rectangle
        Me.HelpAdvancedButton.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.HelpAdvancedButton.ForeColor = System.Drawing.Color.White
        Me.HelpAdvancedButton.GradientColor1 = System.Drawing.Color.SlateGray
        Me.HelpAdvancedButton.GradientColor2 = System.Drawing.Color.Black
        Me.HelpAdvancedButton.GradientColor3 = System.Drawing.Color.SlateGray
        Me.HelpAdvancedButton.GradientColor4 = System.Drawing.Color.Black
        Me.HelpAdvancedButton.Icon = Nothing
        Me.HelpAdvancedButton.Location = New System.Drawing.Point(330, 0)
        Me.HelpAdvancedButton.Name = "HelpAdvancedButton"
        Me.HelpAdvancedButton.RoundedRadius = 10
        Me.HelpAdvancedButton.Size = New System.Drawing.Size(110, 45)
        Me.HelpAdvancedButton.TabIndex = 4
        Me.HelpAdvancedButton.Text = "Help"
        Me.HelpAdvancedButton.TextHorizontalAlignment = CustomPanels.AdvancedButton.HorizontalAlignments.Center
        Me.HelpAdvancedButton.TextVerticleAlignment = CustomPanels.AdvancedButton.VerticleAlignments.Middle
        '
        'DonationsAdvancedButton
        '
        Me.DonationsAdvancedButton.BackColor = System.Drawing.Color.Transparent
        Me.DonationsAdvancedButton.BorderColor = System.Drawing.Color.White
        Me.DonationsAdvancedButton.ButtonStyle = CustomPanels.AdvancedButton.ButtonStyles.Rectangle
        Me.DonationsAdvancedButton.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.DonationsAdvancedButton.ForeColor = System.Drawing.Color.White
        Me.DonationsAdvancedButton.GradientColor1 = System.Drawing.Color.SlateGray
        Me.DonationsAdvancedButton.GradientColor2 = System.Drawing.Color.Black
        Me.DonationsAdvancedButton.GradientColor3 = System.Drawing.Color.SlateGray
        Me.DonationsAdvancedButton.GradientColor4 = System.Drawing.Color.Black
        Me.DonationsAdvancedButton.Icon = Nothing
        Me.DonationsAdvancedButton.Location = New System.Drawing.Point(660, 0)
        Me.DonationsAdvancedButton.Name = "DonationsAdvancedButton"
        Me.DonationsAdvancedButton.RoundedRadius = 10
        Me.DonationsAdvancedButton.Size = New System.Drawing.Size(110, 45)
        Me.DonationsAdvancedButton.TabIndex = 8
        Me.DonationsAdvancedButton.Text = "Donations"
        Me.DonationsAdvancedButton.TextHorizontalAlignment = CustomPanels.AdvancedButton.HorizontalAlignments.Center
        Me.DonationsAdvancedButton.TextVerticleAlignment = CustomPanels.AdvancedButton.VerticleAlignments.Middle
        '
        'TrainingAdvancedButton
        '
        Me.TrainingAdvancedButton.BackColor = System.Drawing.Color.Transparent
        Me.TrainingAdvancedButton.BorderColor = System.Drawing.Color.White
        Me.TrainingAdvancedButton.ButtonStyle = CustomPanels.AdvancedButton.ButtonStyles.Rectangle
        Me.TrainingAdvancedButton.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.TrainingAdvancedButton.ForeColor = System.Drawing.Color.White
        Me.TrainingAdvancedButton.GradientColor1 = System.Drawing.Color.SlateGray
        Me.TrainingAdvancedButton.GradientColor2 = System.Drawing.Color.Black
        Me.TrainingAdvancedButton.GradientColor3 = System.Drawing.Color.SlateGray
        Me.TrainingAdvancedButton.GradientColor4 = System.Drawing.Color.Black
        Me.TrainingAdvancedButton.Icon = Nothing
        Me.TrainingAdvancedButton.Location = New System.Drawing.Point(440, 0)
        Me.TrainingAdvancedButton.Name = "TrainingAdvancedButton"
        Me.TrainingAdvancedButton.RoundedRadius = 10
        Me.TrainingAdvancedButton.Size = New System.Drawing.Size(110, 45)
        Me.TrainingAdvancedButton.TabIndex = 11
        Me.TrainingAdvancedButton.Text = "Training"
        Me.TrainingAdvancedButton.TextHorizontalAlignment = CustomPanels.AdvancedButton.HorizontalAlignments.Center
        Me.TrainingAdvancedButton.TextVerticleAlignment = CustomPanels.AdvancedButton.VerticleAlignments.Middle
        '
        'SaveEEAdvancedButton
        '
        Me.SaveEEAdvancedButton.BackColor = System.Drawing.Color.Transparent
        Me.SaveEEAdvancedButton.BorderColor = System.Drawing.Color.White
        Me.SaveEEAdvancedButton.ButtonStyle = CustomPanels.AdvancedButton.ButtonStyles.Rectangle
        Me.SaveEEAdvancedButton.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.SaveEEAdvancedButton.ForeColor = System.Drawing.Color.White
        Me.SaveEEAdvancedButton.GradientColor1 = System.Drawing.Color.SlateGray
        Me.SaveEEAdvancedButton.GradientColor2 = System.Drawing.Color.Black
        Me.SaveEEAdvancedButton.GradientColor3 = System.Drawing.Color.SlateGray
        Me.SaveEEAdvancedButton.GradientColor4 = System.Drawing.Color.Black
        Me.SaveEEAdvancedButton.Icon = Nothing
        Me.SaveEEAdvancedButton.Location = New System.Drawing.Point(550, 0)
        Me.SaveEEAdvancedButton.Name = "SaveEEAdvancedButton"
        Me.SaveEEAdvancedButton.RoundedRadius = 10
        Me.SaveEEAdvancedButton.Size = New System.Drawing.Size(110, 45)
        Me.SaveEEAdvancedButton.TabIndex = 5
        Me.SaveEEAdvancedButton.Text = "Save-EE"
        Me.SaveEEAdvancedButton.TextHorizontalAlignment = CustomPanels.AdvancedButton.HorizontalAlignments.Center
        Me.SaveEEAdvancedButton.TextVerticleAlignment = CustomPanels.AdvancedButton.VerticleAlignments.Middle
        '
        'LobbyAdvancedButton
        '
        Me.LobbyAdvancedButton.BackColor = System.Drawing.Color.Transparent
        Me.LobbyAdvancedButton.BorderColor = System.Drawing.Color.White
        Me.LobbyAdvancedButton.ButtonStyle = CustomPanels.AdvancedButton.ButtonStyles.Rectangle
        Me.LobbyAdvancedButton.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.LobbyAdvancedButton.ForeColor = System.Drawing.Color.White
        Me.LobbyAdvancedButton.GradientColor1 = System.Drawing.Color.SlateGray
        Me.LobbyAdvancedButton.GradientColor2 = System.Drawing.Color.Black
        Me.LobbyAdvancedButton.GradientColor3 = System.Drawing.Color.SlateGray
        Me.LobbyAdvancedButton.GradientColor4 = System.Drawing.Color.Black
        Me.LobbyAdvancedButton.Icon = Nothing
        Me.LobbyAdvancedButton.Location = New System.Drawing.Point(0, 0)
        Me.LobbyAdvancedButton.Name = "LobbyAdvancedButton"
        Me.LobbyAdvancedButton.RoundedRadius = 10
        Me.LobbyAdvancedButton.Size = New System.Drawing.Size(110, 45)
        Me.LobbyAdvancedButton.TabIndex = 1
        Me.LobbyAdvancedButton.Text = "Lobby"
        Me.LobbyAdvancedButton.TextHorizontalAlignment = CustomPanels.AdvancedButton.HorizontalAlignments.Center
        Me.LobbyAdvancedButton.TextVerticleAlignment = CustomPanels.AdvancedButton.VerticleAlignments.Middle
        '
        'LobbyTimer
        '
        Me.LobbyTimer.Enabled = True
        Me.LobbyTimer.Interval = 10
        '
        'FormLobby
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(991, 648)
        Me.Controls.Add(Me.FacebookAdvancedButton)
        Me.Controls.Add(Me.OptionsAdvancedButton)
        Me.Controls.Add(Me.PatchAdvancedButton)
        Me.Controls.Add(Me.LadderAdvancedButton)
        Me.Controls.Add(Me.HelpAdvancedButton)
        Me.Controls.Add(Me.DonationsAdvancedButton)
        Me.Controls.Add(Me.TrainingAdvancedButton)
        Me.Controls.Add(Me.SaveEEAdvancedButton)
        Me.Controls.Add(Me.LobbyAdvancedButton)
        Me.Controls.Add(Me.MainTabControl)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormLobby"
        Me.Text = "FormLobby"
        Me.UsersContextMenuStrip.ResumeLayout(False)
        Me.OptionsTabPage.ResumeLayout(False)
        Me.OptionsOutlookPanel.ResumeLayout(False)
        Me.VisualStylesOutlookPanel.ResumeLayout(False)
        Me.MiscOptionsOutlookPanel.ResumeLayout(False)
        Me.MiscOptionsOutlookPanel.PerformLayout()
        Me.ChatOptionsOutlookPanel.ResumeLayout(False)
        Me.ChatOptionsOutlookPanel.PerformLayout()
        Me.ChatColorsOutlookPanel.ResumeLayout(False)
        Me.HyperlinkGroupBox.ResumeLayout(False)
        Me.ServerText2GroupBox.ResumeLayout(False)
        Me.ServerText1GroupBox.ResumeLayout(False)
        Me.WarningTextGroupBox.ResumeLayout(False)
        Me.WhisperTextGroupBox.ResumeLayout(False)
        Me.AlertTextGroupBox.ResumeLayout(False)
        Me.EmoteGroupBox.ResumeLayout(False)
        Me.ChatTextGroupBox.ResumeLayout(False)
        Me.UserLinkGroupBox.ResumeLayout(False)
        Me.BackgroundColorGroupBox.ResumeLayout(False)
        Me.GameDetectionOutlookPanel.ResumeLayout(False)
        Me.ButtonColorsOutlookPanel.ResumeLayout(False)
        Me.ButtonColorBottomGroupBox.ResumeLayout(False)
        Me.ButtonColorTopGroupBox.ResumeLayout(False)
        Me.NetworkDetailsOutlookPanel.ResumeLayout(False)
        Me.NetworkDetailsOutlookPanel.PerformLayout()
        Me.BrowserTabPage.ResumeLayout(False)
        Me.BrowserTabPage.PerformLayout()
        Me.GameContextMenuStrip.ResumeLayout(False)
        Me.ChatSplitContainer.Panel1.ResumeLayout(False)
        Me.ChatSplitContainer.Panel2.ResumeLayout(False)
        Me.ChatSplitContainer.ResumeLayout(False)
        Me.ChatOutlookPanel.ResumeLayout(False)
        Me.ChatConsoleGroupBox.ResumeLayout(False)
        Me.ChatConsoleGroupBox.PerformLayout()
        Me.UsersOutlookPanel.ResumeLayout(False)
        Me.UsersTabControl.ResumeLayout(False)
        Me.OnlineTabPage.ResumeLayout(False)
        Me.FriendsTabPage.ResumeLayout(False)
        Me.CheatersTabPage.ResumeLayout(False)
        Me.IgnoreTabPage.ResumeLayout(False)
        Me.LobbyTabPage.ResumeLayout(False)
        Me.MainSplitContainer.Panel1.ResumeLayout(False)
        Me.MainSplitContainer.Panel2.ResumeLayout(False)
        Me.MainSplitContainer.ResumeLayout(False)
        Me.GamesOutlookPanel.ResumeLayout(False)
        Me.MainTabControl.ResumeLayout(False)
        Me.PatchTabPage.ResumeLayout(False)
        Me.TrayContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents IgnoreListView As System.Windows.Forms.ListView
    Private WithEvents IgnoreColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents UsersContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents WhisperToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyNameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents AddFriendToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents RemoveFriendToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents AddCheaterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents RemoveCheaterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents IgnoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents UnignoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents SetAFKToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ClearAFKToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ManualAddToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ManualFriendToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ManualCheaterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ManualIgnoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ModeratorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KickToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MuteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManualMuteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListMutedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AdminFunctionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ForceUpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManualBanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListBannedPlayersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PromoteToAdminToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PromoteToModeratorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents IconsImageList As System.Windows.Forms.ImageList
    Private WithEvents OptionsTabPage As System.Windows.Forms.TabPage
    Private WithEvents IgnoreTabPage As System.Windows.Forms.TabPage
    Private WithEvents CheatersListColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents CheatersListView As System.Windows.Forms.ListView
    Private WithEvents FriendsListColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents FriendsListView As System.Windows.Forms.ListView
    Private WithEvents FriendsTabPage As System.Windows.Forms.TabPage
    Private WithEvents CheatersTabPage As System.Windows.Forms.TabPage
    Private WithEvents BrowserTabPage As System.Windows.Forms.TabPage
    Friend WithEvents MiscWebBrowser As System.Windows.Forms.WebBrowser
    Private WithEvents PlayersColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents IPColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents GameContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Private WithEvents CopyIPToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents MapTypeColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents HostColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents MapSizeColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents SendButton As System.Windows.Forms.Button
    Friend WithEvents ChatComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ChatTextBox As System.Windows.Forms.TextBox
    Private WithEvents ChatSplitContainer As System.Windows.Forms.SplitContainer
    Private WithEvents ChatOutlookPanel As CustomPanels.OutlookPanel
    Friend WithEvents ChatConsoleGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ChatWebBrowser As System.Windows.Forms.WebBrowser
    Private WithEvents UsersOutlookPanel As CustomPanels.OutlookPanel
    Private WithEvents UsersOnlineLabel As System.Windows.Forms.Label
    Private WithEvents UsersTabControl As System.Windows.Forms.TabControl
    Private WithEvents OnlineTabPage As System.Windows.Forms.TabPage
    Private WithEvents OnlineListView As System.Windows.Forms.ListView
    Private WithEvents OnlineListColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents ResourcesColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents EpochColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents GameNameColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents LobbyTabPage As System.Windows.Forms.TabPage
    Private WithEvents MainSplitContainer As System.Windows.Forms.SplitContainer
    Private WithEvents GamesOutlookPanel As CustomPanels.OutlookPanel
    Private WithEvents GamesListView As System.Windows.Forms.ListView
    Private WithEvents GameColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents SaveEEAdvancedButton As CustomPanels.AdvancedButton
    Private WithEvents LadderAdvancedButton As CustomPanels.AdvancedButton
    Private WithEvents HelpAdvancedButton As CustomPanels.AdvancedButton
    Private WithEvents OptionsAdvancedButton As CustomPanels.AdvancedButton
    Private WithEvents PatchAdvancedButton As CustomPanels.AdvancedButton
    Private WithEvents LobbyAdvancedButton As CustomPanels.AdvancedButton
    Private WithEvents MainTabControl As System.Windows.Forms.TabControl
    Friend WithEvents DemoteToUserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WarnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MiscWBAddressTextBox As System.Windows.Forms.TextBox
    Friend WithEvents MiscWBForwardButton As System.Windows.Forms.Button
    Friend WithEvents MiscWBBackButton As System.Windows.Forms.Button
    Friend WithEvents GetDetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PatchTabPage As System.Windows.Forms.TabPage
    Private WithEvents PatchOutlookPanel As CustomPanels.OutlookPanel
    Private WithEvents OptionsOutlookPanel As CustomPanels.OutlookPanel
    Friend WithEvents VisualStylesOutlookPanel As CustomPanels.OutlookPanel
    Friend WithEvents VisualStylesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents MiscOptionsOutlookPanel As CustomPanels.OutlookPanel
    Friend WithEvents LanguageComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents LanguageLabel As System.Windows.Forms.Label
    Friend WithEvents MaximizeLobbyOnLoginCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AutoResizeGameColumnsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ChatOptionsOutlookPanel As CustomPanels.OutlookPanel
    Friend WithEvents EnableSoundsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RevertToChatCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AutoscrollTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AutoscrollCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ChatColorsOutlookPanel As CustomPanels.OutlookPanel
    Friend WithEvents HyperlinkGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents HyperlinkPanel As System.Windows.Forms.Panel
    Friend WithEvents ServerText2GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ServerText2Panel As System.Windows.Forms.Panel
    Friend WithEvents ServerText1GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ServerText1Panel As System.Windows.Forms.Panel
    Friend WithEvents WarningTextGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents WarningTextPanel As System.Windows.Forms.Panel
    Friend WithEvents WhisperTextGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents WhisperTextPanel As System.Windows.Forms.Panel
    Friend WithEvents AlertTextGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents AlertTextPanel As System.Windows.Forms.Panel
    Friend WithEvents EmoteGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents EmoteTextPanel As System.Windows.Forms.Panel
    Friend WithEvents ChatTextGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ChatTextPanel As System.Windows.Forms.Panel
    Friend WithEvents UserLinkGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents UserLinkPanel As System.Windows.Forms.Panel
    Friend WithEvents BackgroundColorGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents BackgroundColorPanel As System.Windows.Forms.Panel
    Friend WithEvents GameDetectionOutlookPanel As CustomPanels.OutlookPanel
    Friend WithEvents GameDetectionLabel As System.Windows.Forms.Label
    Friend WithEvents DisableGameDetectionButton As System.Windows.Forms.Button
    Friend WithEvents EnableGameDetectionButton As System.Windows.Forms.Button
    Friend WithEvents ButtonColorsOutlookPanel As CustomPanels.OutlookPanel
    Friend WithEvents ButtonColorBottomGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonColorBottomPanel As System.Windows.Forms.Panel
    Friend WithEvents ButtonColorTopGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonColorTopPanel As System.Windows.Forms.Panel
    Friend WithEvents NetworkDetailsOutlookPanel As CustomPanels.OutlookPanel
    Friend WithEvents NetworkAdapterComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents NetworkAdapterLabel As System.Windows.Forms.Label
    Friend WithEvents TrayNotifyIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents TrayContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowSaveEELobbyClientToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrayToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowIconInSystemTrayCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents MinimizeToSystemTrayCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents PromoteToDonatorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowTimestampsCheckBox As System.Windows.Forms.CheckBox
    Private WithEvents DonationsAdvancedButton As CustomPanels.AdvancedButton
    Private WithEvents FacebookAdvancedButton As CustomPanels.AdvancedButton
    Private WithEvents AdvancedButton3 As CustomPanels.AdvancedButton
    Private WithEvents TrainingAdvancedButton As CustomPanels.AdvancedButton
    Friend WithEvents LocateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LobbyTimer As System.Windows.Forms.Timer
    Friend WithEvents EnableBadLanguageFilterCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents EnableSmiliesCheckBox As System.Windows.Forms.CheckBox

End Class
