Public Class FormPatch
    Dim shouldShow As Boolean = False
    Public Property UpdateArg As Boolean = False

    Private Sub FormUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Show()

        ' See if user has admin
        If AdminCheck.RunningStandardButCanElevate Then
            If MsgBox("You do not have Administrator privileges.  This may cause errors when using some of the update tools." & vbCrLf & vbCrLf & _
                   "Would you like to restart the lobby with Administrator privileges?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.Yes Then
                ' Encode username and password for autologin
                Dim UserData As String = Globals.CurrentUser.Username & "[-:+:-]" & Globals.CurrentUser.Password
                Dim UserBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(UserData)
                LobbyShared.Crypt.Cryptic(UserBytes, New Byte() {1, 2, 3, 4, 5})
                Dim UsersB64 As String = LobbyShared.Base64.EncodeFromBytes(UserBytes)
                My.Settings.AutoLogin = UsersB64
                My.Settings.Save()

                ' Try to restart with admin
                Dim startInfo As ProcessStartInfo = New ProcessStartInfo()
                startInfo.Verb = "runas" '<--- this makes it admin
                startInfo.FileName = Globals.ProcessPath
                startInfo.UseShellExecute = True
                Process.Start(startInfo)

                ' Close shit w/o admin..
                Application.Exit()
            End If
            'extraInfo = "You may try to avoid errors by navigating to """ & Application.ExecutablePath & ","" right-clicking on LobbyClient.exe, and then hitting ""Run as Administrator.""" & vbCrLf & vbCrLf
        ElseIf Not AdminCheck.IsRunningAsAdmin Then
            If MsgBox("You do not have Administrator privileges.  This may cause errors when using some of the update tools." & vbCrLf & vbCrLf & _
                    "Try right-clicking on the lobby and hitting ""Run as administrator""" & vbCrLf & vbCrLf & _
                    "Click OK to continue, or Cancel to close this application.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "Warning") = MsgBoxResult.Cancel Then
                Application.Exit()
            End If
        End If

        ' See if game is running
        Dim gameRunning As Boolean = False
        For Each p As Process In Process.GetProcesses
            If p.ProcessName.ToLower.Contains("empire earth") OrElse p.ProcessName.ToLower.Contains("ee-aoc") Then
                gameRunning = True
                Exit For
            End If
        Next
        If gameRunning Then
            If MsgBox("Your game is currently running.  It is suggested you close your game before performing any changes." & vbCrLf & vbCrLf & _
                   "Would you like to close your game now?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.Yes Then
                For Each p As Process In Process.GetProcesses
                    If p.ProcessName.ToLower.Contains("empire earth") OrElse p.ProcessName.ToLower.Contains("ee-aoc") Then
                        Try
                            p.Kill()
                        Catch ex As Exception
                        End Try
                    End If
                Next
            End If
        End If

        ' Read command line arguments, specifically the update run by LobbyClient
        'For Each arg As String In System.Environment.GetCommandLineArgs()
        '    If arg = "-update" Then
        If UpdateArg Then
            ' Prompt user for download
            If MsgBox("Welcome to the Save-EE Patcher Utility." & _
                   vbCrLf & vbCrLf & "Some info for new players:  Players in the Save-EE Lobby use base game versions of EE 2.0 and AoC 1.0 in addition to a set of our own patches made by Omega." & _
                   vbCrLf & vbCrLf & "If you ever need to return to this utility, just hit the Patch tab at the top of the lobby." & _
                   vbCrLf & vbCrLf & "The important stuff:  In order to ensure all players are using the same game versions, this program will attempt to automatically download and install the latest patches Save-EE Patches by Omega." & _
                   vbCrLf & vbCrLf & "Click OK to continue, or Cancel to prevent the download.", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Save-EE Patcher") = MsgBoxResult.Ok Then
                UpdateDialog.Show()
                If Not Globals.PatchEngine.CheckDirs() Then
                    UpdateDialog.Close()
                    Application.Exit()
                    Exit Sub
                End If
                UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf)
                ' UpdatePatchFilesButton
                Dim un As Boolean = False
                If Globals.PatchEngine.CheckForMSI Then
                    If MsgBox("Older installers of Omega's patches were found." & vbCrLf & vbCrLf & "Would you like to uninstall them now?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update") = MsgBoxResult.Yes Then
                        un = True
                    End If
                End If
                If un Then
                    Globals.PatchEngine.UninstallOldPatches()
                    UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf)
                End If
                AppActivate(UpdateDialog.Text)
                Globals.PatchEngine.UpdatePatchFiles()
                AppActivate(UpdateDialog.Text)
                ' Part of CopyPatchFilesButton
                Globals.PatchEngine.DeleteOldPatch()
                Globals.PatchEngine.CopyPatchToDirs()
                UpdateDialog.OK_Button.Enabled = True
                UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
            End If
            UpdateDialog.Close()
            Me.Close()
            Me.Dispose()
        End If
        'Next
    End Sub
    Private Sub EEDirBrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EEDirBrowseButton.Click
        Dim fbd As New FolderBrowserDialog
        fbd.Description = "Please locate and select your Empire Earth installation folder."
        fbd.ShowDialog()
        If fbd.SelectedPath = "" Then
            Exit Sub
        Else
            EEDirTextBox.Text = fbd.SelectedPath
        End If
    End Sub
    Private Sub AoCDirBrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AoCDirBrowseButton.Click
        Dim fbd As New FolderBrowserDialog
        fbd.Description = "Please locate and select your Art of Conquest installation folder."
        fbd.ShowDialog()
        If fbd.SelectedPath = "" Then
            Exit Sub
        Else
            AoCDirTextBox.Text = fbd.SelectedPath
        End If
    End Sub
    Private Sub AutodetectInstallDirsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutodetectInstallDirsButton.Click
        Globals.PatchEngine.LocateEEDir()
        Globals.PatchEngine.LocateAoCDir()
    End Sub
    Private Sub AutoUpdateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoUpdateButton.Click
        UpdateDialog.Show()
        ' Find Dirs
        If Not Globals.PatchEngine.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        ' Update EE
        If Not Globals.PatchEngine.UpdateEE() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        ' DC Patch
        Globals.PatchEngine.DCPatch()
        ' Uninstall old patches
        AppActivate(UpdateDialog.Text)
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf)
        Globals.PatchEngine.UninstallOldPatches()
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf)
        ' Update patch files
        Globals.PatchEngine.UpdatePatchFiles()
        ' Delete old patch files
        'If Not Globals.patchinfoSame Then Globals.DeleteOldPatch()
        Globals.PatchEngine.DeleteOldPatch()
        ' Copy new patch files
        'If Not Globals.patchinfoSame Then Globals.CopyPatchToDirs()
        Globals.PatchEngine.CopyPatchToDirs()

        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub InstallEEUpdatesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstallEEUpdatesButton.Click
        UpdateDialog.Show()
        If Not Globals.PatchEngine.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        Globals.PatchEngine.UpdateEE()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub DirectConnectPatchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DirectConnectPatchButton.Click
        UpdateDialog.Show()
        If Not Globals.PatchEngine.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        Globals.PatchEngine.DCPatch()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub ReplaceWONLobbyCfgButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceWONLobbyCfgButton.Click
        UpdateDialog.Show()
        If Not Globals.PatchEngine.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        UpdateDialog.InfoTextBox.AppendText(vbCrLf)
        Globals.PatchEngine.ReplaceWONLobbyCfg()
        UpdateDialog.InfoTextBox.AppendText(" complete.")
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub UninstallMSIPatchesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UninstallMSIPatchesButton.Click
        UpdateDialog.Show()
        Globals.PatchEngine.UninstallOldPatches()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub CheckForUpdatesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckForUpdatesButton.Click
        Dim un As Boolean = False
        If Globals.PatchEngine.CheckForMSI Then
            If MsgBox("Older installers of Omega's patches were found." & vbCrLf & vbCrLf & "Would you like to uninstall them now?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update") = MsgBoxResult.Yes Then
                un = True
            End If
        End If
        UpdateDialog.Show()
        If un Then
            Globals.PatchEngine.UninstallOldPatches()
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf)
        End If
        Globals.PatchEngine.UpdatePatchFiles()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "To apply the patch to your game, click the ""Copy Patch Files To Install Directories"" button.")
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub ForcePatchUpdateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcePatchUpdateButton.Click
        ' Delete patchinfo.dat
        Try
            System.IO.File.Delete(Globals.AppDataEnvVar & "\Save-EE\Patch\patchinfo.dat")
        Catch
        End Try
        CheckForUpdatesButton_Click(Nothing, Nothing)
    End Sub
    Private Sub CopyPatchFilesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyPatchFilesButton.Click
        UpdateDialog.Show()
        If Not Globals.PatchEngine.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        Globals.PatchEngine.DeleteOldPatch()
        Globals.PatchEngine.CopyPatchToDirs()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub DeletePatchFromDirsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeletePatchFromDirsButton.Click
        UpdateDialog.Show()
        If Not Globals.PatchEngine.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        Globals.PatchEngine.DeleteCurrentPatch()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub RemoveAllPatchesAndModsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveAllPatchesAndModsButton.Click
        UpdateDialog.Show()
        If Not Globals.PatchEngine.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        Globals.PatchEngine.ClearDBFolders()
        Globals.PatchEngine.ClearTexturesFolders()
        Globals.PatchEngine.ClearCivilizationsFolders()
        Globals.PatchEngine.ClearModelsFolders()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub DefaultBannerButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultBannerButton.Click
        UpdateDialog.Show()
        If Not Globals.PatchEngine.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        UpdateDialog.InfoTextBox.AppendText(vbCrLf)
        Globals.PatchEngine.ReplaceBanner()
        UpdateDialog.InfoTextBox.AppendText(" complete.")
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub EENoCDButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EENoCDButton.Click
        UpdateDialog.Show()
        If Not Globals.PatchEngine.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        UpdateDialog.InfoTextBox.AppendText(vbCrLf)
        Globals.PatchEngine.GetEENoCD()
        'UpdateDialog.InfoTextBox.AppendText(" complete.")
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub AoCNoCDButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AoCNoCDButton.Click
        UpdateDialog.Show()
        If Not Globals.PatchEngine.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        UpdateDialog.InfoTextBox.AppendText(vbCrLf)
        Globals.PatchEngine.GetAoCNoCD()
        'UpdateDialog.InfoTextBox.AppendText(" complete.")
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If LanguageDialog.Visible OrElse UpdateDialog.Visible Then
            shouldShow = True
        Else
            If shouldShow Then
                shouldShow = False
                Me.Enabled = True
                Try
                    AppActivate(Me.Text)
                Catch
                End Try
            End If
        End If
    End Sub
    Private Sub FormUpdate_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If LanguageDialog.Visible Then
            Try
                AppActivate(LanguageDialog.Text)
            Catch
            End Try
        ElseIf UpdateDialog.Visible Then
            Try
                AppActivate(UpdateDialog.Text)
            Catch
            End Try
        Else
            Me.Enabled = True
        End If
    End Sub
End Class
