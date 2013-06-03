Public Class FormKeyList
    Public Action As String = ""
    Public OldDateTime As Date = Date.UtcNow.AddDays(1)
    Public p As New LobbyShared.NetworkMessage
    Public lvs As New ListViewHelpers.ListViewSorter(KeyListView)

    ' Load it up baby
    Private Sub FormKeyList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lvs.lv = KeyListView

        '
        ' Language support
        '
        Me.Text = Language.Windows.Key_Management
        TopLabel.Text = Language.Windows.Please_select_hardware_IDs_to
        ' ColumnHeaders
        KeyColumnHeader.Text = Language.Windows.Key
        ValueColumnHeader.Text = Language.Windows.Value
        StartTimeColumnHeader.Text = Language.Windows.Start_Time
        EndTimeColumnHeader.Text = Language.Windows.End_Time
        ReasonColumnHeader.Text = Language.Windows.Reason
        CommentsColumnHeader.Text = Language.Windows.Comments
        ' Labels
        ReasonLabel.Text = Language.Windows.Reason & ":"
        CommentsLabel.Text = Language.Windows.Comments & ":"
        RemoveTimeLabel.Text = Language.Windows.Remove_Time & ":"
        ManualRemoveCheckBox.Text = Language.Windows.Must_be_manually_removed
        DurationLabel.Text = Language.Windows.Duration & ":"
        TimezoneNotesLabel.Text = Language.Windows.Timezone_notes
        ' Some buttons
        CancelButton0.Text = Language.Windows.Cancel
        SaveButton.Text = Language.Windows.Save

        ' Autosize the columns
        Dim cnt As Integer = -1
        For Each c As ColumnHeader In KeyListView.Columns
            cnt += 1
            c.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
            If cnt <> 0 Then If c.Width < 60 Then c.Width = 60
        Next

        ' Set initial remove date
        RemoveDateTimePicker.Value = Date.UtcNow.AddDays(1)

        ' Extend key and value columns and remove the other ones
        If Action = "BAN" OrElse Action = "MUTE" Then
            KeyColumnHeader.Width = 300
            ValueColumnHeader.Width = 275
            KeyListView.Columns.Remove(ByColumnHeader)
            KeyListView.Columns.Remove(StartTimeColumnHeader)
            KeyListView.Columns.Remove(EndTimeColumnHeader)
            KeyListView.Columns.Remove(ReasonColumnHeader)
            KeyListView.Columns.Remove(CommentsColumnHeader)
        Else
            lvs.SortByColumn(3)
        End If

        If Action = "BAN" Then
            TopLabel.Text &= " " & Language.Windows.Ban.ToLower & "."
            ActionButton.Text = Language.Windows.Ban
            SaveButton.Enabled = False
            p.MessageType = LobbyShared.NetworkMessage.MsgTypes.BanPlayer
        ElseIf Action = "UNBAN" Then
            TopLabel.Text &= " " & Language.Windows.Unban.ToLower & "."
            ByColumnHeader.Text = Language.Windows.Banned_By
            ActionButton.Text = Language.Windows.Unban
            p.MessageType = LobbyShared.NetworkMessage.MsgTypes.UnbanPlayer
        ElseIf Action = "MUTE" Then
            TopLabel.Text &= " " & Language.Windows.Mute.ToLower & "."
            ActionButton.Text = Language.Windows.Mute
            SaveButton.Enabled = False
            p.MessageType = LobbyShared.NetworkMessage.MsgTypes.MutePlayer
        ElseIf Action = "UNMUTE" Then
            TopLabel.Text &= " " & Language.Windows.Unmute.ToLower & "."
            ByColumnHeader.Text = Language.Windows.Muted_By
            ActionButton.Text = Language.Windows.Unmute
            p.MessageType = LobbyShared.NetworkMessage.MsgTypes.UnmutePlayer
        Else
            TopLabel.Text = Language.Windows.Player_Details
            KeyColumnHeader.Text = ""
            KeyColumnHeader.Width = 585
            KeyListView.Columns.Remove(ValueColumnHeader)
            KeyListView.Columns.Remove(ByColumnHeader)
            KeyListView.Columns.Remove(StartTimeColumnHeader)
            KeyListView.Columns.Remove(EndTimeColumnHeader)
            KeyListView.Columns.Remove(ReasonColumnHeader)
            KeyListView.Columns.Remove(CommentsColumnHeader)
            KeyListView.Height += 100
            ReasonLabel.Visible = False
            ReasonTextBox.Visible = False
            CommentsLabel.Visible = False
            CommentsTextBox.Visible = False
            RemoveTimeLabel.Visible = False
            RemoveDateTimePicker.Visible = False
            ManualRemoveCheckBox.Visible = False
            TimeFormatLabel.Visible = False
            TimezoneNotesLabel.Visible = False
            DurationLabel.Visible = False
            DurationTextBox.Visible = False
            ActionButton.Visible = False
            SaveButton.Visible = False
            CancelButton0.Location = New Point(545, 316)
        End If
    End Sub
    ' Makes sure the time is later than the current
    Private Sub RemoveDateTimePicker_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveDateTimePicker.ValueChanged
        If RemoveDateTimePicker.Value < Date.UtcNow Then
            RemoveDateTimePicker.Value = OldDateTime
            MsgBox(Language.Windows.Selected_time_must_be_after_the_current_UTC_time, MsgBoxStyle.Exclamation, Language.Windows.Key_Management)
        Else
            OldDateTime = RemoveDateTimePicker.Value
        End If
    End Sub
    ' Toggles permanent mute/ban status
    Private Sub ManualRemoveCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualRemoveCheckBox.CheckedChanged
        If RemoveDateTimePicker.Enabled = True Then
            DurationRefreshTimer.Enabled = False
            RemoveDateTimePicker.Enabled = False
            DurationTextBox.Text = Language.Windows.Permanent
        Else
            DurationRefreshTimer.Enabled = True
            RemoveDateTimePicker.Enabled = True
        End If
    End Sub
    ' Cancel... simple enough
    Private Sub CancelButton0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton0.Click
        Me.Close()
    End Sub
    ' Used for saving changes to keys
    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        Dim ret As Integer = MsgBox(Language.Windows.Are_you_sure_SAVE, MsgBoxStyle.Question + MsgBoxStyle.YesNo, Language.Windows.Key_Management)
        If ret = vbYes Then
            ' Only for unmute or unban
            If Action = "UNBAN" OrElse Action = "UNMUTE" Then
                If Action = "UNBAN" Then p.MessageType = LobbyShared.NetworkMessage.MsgTypes.SaveBan
                If Action = "UNMUTE" Then p.MessageType = LobbyShared.NetworkMessage.MsgTypes.SaveMute
                ' Check to see if reason field is empty
                If ReasonTextBox.Text.Trim.Length = 0 Then
                    MsgBox(Language.Windows.Please_enter_a_reason, MsgBoxStyle.Exclamation, Language.Windows.Key_Management)
                    Exit Sub
                End If
                ' Check to see if comments field is empty
                If CommentsTextBox.Text.Trim.Length = 0 Then
                    MsgBox(Language.Windows.Please_leave_a_comment_when_editing_a_key, MsgBoxStyle.Exclamation, Language.Windows.Key_Management)
                    Exit Sub
                End If
                ' Set the end time
                Dim endTime As String = ""
                If ManualRemoveCheckBox.Checked = True Then
                    endTime = "permanent"
                Else
                    endTime = RemoveDateTimePicker.Value.ToString("yyyy.MM.dd.HH.mm.ss")
                End If
                ' Add HW to packet
                For Each lvi As ListViewItem In KeyListView.SelectedItems
                    ' key is listcollection item key, everything else is in list
                    p.ListCollection(lvi.Text) = New List(Of String)(New String() _
                        {lvi.SubItems(1).Text, _
                         lvi.SubItems(2).Text, _
                         lvi.SubItems(3).Text, _
                         endTime, _
                         ReasonTextBox.Text, _
                         CommentsTextBox.Text})
                Next
            End If
            Globals.ClientEngine.SendPacket(p)
            Me.Close()
        End If
    End Sub
    ' Mute/Ban or Unmute/Unban
    Private Sub ActionButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActionButton.Click
        ' For new mutes and bans
        If Action = "BAN" OrElse Action = "MUTE" Then
            ' Check to see if reason field is empty
            If ReasonTextBox.Text.Trim.Length = 0 Then
                MsgBox(Language.Windows.Please_enter_a_reason, MsgBoxStyle.Exclamation, Language.Windows.Key_Management)
                Exit Sub
            End If
            ' Set comments field if it's empty
            If CommentsTextBox.Text.Trim.Length = 0 Then
                CommentsTextBox.Text = "none"
            End If
            ' Set the end time
            Dim endTime As String = ""
            If ManualRemoveCheckBox.Checked = True Then
                endTime = "permanent"
            Else
                endTime = RemoveDateTimePicker.Value.ToString("yyyy.MM.dd.HH.mm.ss")
            End If
            ' Add HW
            For Each lvi As ListViewItem In KeyListView.SelectedItems
                ' Look to see if value is already in list
                Dim inList As Boolean = False
                For Each v As List(Of String) In p.ListCollection.Values
                    If v(0) = lvi.SubItems(1).Text Then
                        inList = True
                        Exit For
                    End If
                Next
                ' If it's not...
                If Not inList Then
                    ' key is listcollection item key, everything else is in list
                    p.ListCollection(lvi.Text) = New List(Of String)(New String() _
                        {lvi.SubItems(1).Text, _
                         Globals.CurrentUser.Username, _
                         Date.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss"), _
                         endTime, _
                         ReasonTextBox.Text, _
                         CommentsTextBox.Text})
                End If
            Next
            p.StringCollection("FROM") = Globals.CurrentUser.Username
            p.StringCollection("REASON") = ReasonTextBox.Text
            p.StringCollection("UNTIL") = endTime
        Else
            ' For unmutes and unbans, only key and value are required
            ' so we use StringCollection
            For Each lvi As ListViewItem In KeyListView.SelectedItems
                If p.StringCollection.ContainsValue(lvi.SubItems(1).Text) = False Then
                    p.StringCollection(lvi.Text) = lvi.SubItems(1).Text
                End If
            Next
        End If
        Globals.ClientEngine.SendPacket(p)
        Me.Close()
    End Sub
    ' Adds ability to copy with CTRL+C
    Private Sub KeyListView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles KeyListView.KeyDown
        If e.Control = True AndAlso e.KeyCode = Keys.C Then
            Dim a As String = "Results for "
            For Each s As ListViewItem In KeyListView.SelectedItems
                a = a & vbCrLf & s.Text
            Next
            Try
                Clipboard.SetText(a)
            Catch ex As Exception
            End Try
        End If
    End Sub
    ' Calculates and shows the duration
    Private Sub DurationRefreshTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DurationRefreshTimer.Tick
        If ManualRemoveCheckBox.Checked = False Then
            Dim str As String = RemoveDateTimePicker.Value.Subtract(Date.UtcNow).ToString
            DurationTextBox.Text = str.Substring(0, str.LastIndexOf(":") + 3)
        End If
    End Sub
    ' When selected items are changed, apply their details to the controls below the KeyListView
    Private Sub KeyListView_ItemSelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeyListView.ItemSelectionChanged
        If Action = "UNMUTE" OrElse Action = "UNBAN" Then
            Dim reason As String = Chr(2)
            Dim comments As String = Chr(2)
            Dim removeTime As String = Chr(2)
            ' Allows us to assign controls when multiple items are selected and have common subitems
            For Each lvi As ListViewItem In KeyListView.SelectedItems
                ' Set the reason
                If reason = Chr(2) Then
                    reason = lvi.SubItems(5).Text
                ElseIf reason <> lvi.SubItems(5).Text Then
                    reason = ""
                End If
                ' Set comments
                If comments = Chr(2) Then
                    comments = lvi.SubItems(6).Text
                ElseIf comments <> lvi.SubItems(6).Text Then
                    comments = ""
                End If
                ' Set remove date
                If removeTime = Chr(2) Then
                    removeTime = lvi.SubItems(4).Text
                ElseIf removeTime <> lvi.SubItems(4).Text Then
                    removeTime = ""
                End If
            Next
            ' Apply to controls
            ' Reason
            If reason = "" OrElse reason = Chr(2) Then
                ReasonTextBox.Text = ""
            Else
                ReasonTextBox.Text = reason
            End If
            ' Comments
            If comments = "" OrElse comments = Chr(2) OrElse comments = "none" Then
                CommentsTextBox.Text = ""
            Else
                CommentsTextBox.Text = comments
            End If
            ' Remove time, manual remove checkbox
            If removeTime = "" OrElse removeTime = Chr(2) Then
                RemoveDateTimePicker.Value = Date.UtcNow.AddDays(1)
            ElseIf removeTime = "permanent" Then
                ManualRemoveCheckBox.Checked = True
            Else
                RemoveDateTimePicker.Value = Date.ParseExact(removeTime, "yyyy.MM.dd.HH.mm.ss", System.Globalization.CultureInfo.InvariantCulture)
                ManualRemoveCheckBox.Checked = False
            End If
        End If
    End Sub
    Private Sub KeyListView_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles KeyListView.ColumnClick
        lvs.SortByColumn(e.Column)
    End Sub
End Class