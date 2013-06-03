Public Class FormPatch
    Dim shouldShow As Boolean = False
    Private Sub FormPatch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Show()

        ' See if user has admin
        Dim extraInfo As String = ""
        If AdminCheck.RunningStandardButCanElevate Then
            extraInfo = "You may try to avoid errors by navigating to """ + Application.ExecutablePath + ","" right-clicking on PatchUpdate.exe, and then hitting ""Run as Administrator.""" + vbNewLine + vbNewLine
        End If
        If Not AdminCheck.IsRunningAsAdmin Then
            If MsgBox("You do not have Administrator privileges.  This may cause errors when using some of the update tools." + vbNewLine + vbNewLine + _
                   extraInfo + _
                   "Click OK to continue, or Cancel to close this application.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "Warning") = MsgBoxResult.Cancel Then
                Application.Exit()
            End If
        End If

        ' See if game is running
        Dim gameRunning As Boolean = False
        For Each p As Process In Process.GetProcesses
            If p.ProcessName.ToLower.Contains("empire earth") Or p.ProcessName.ToLower.Contains("ee-aoc") Then
                gameRunning = True
                Exit For
            End If
        Next
        If gameRunning Then
            If MsgBox("Your game is currently running.  It is suggested you close your game before performing any changes." + vbNewLine + vbNewLine + _
                   "Would you like to close your game now?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.Yes Then
                For Each p As Process In Process.GetProcesses
                    If p.ProcessName.ToLower.Contains("empire earth") Or p.ProcessName.ToLower.Contains("ee-aoc") Then
                        Try
                            p.Kill()
                        Catch ex As Exception
                        End Try
                    End If
                Next
            End If
        End If

        ' Read command line arguments, specifically the update run by LobbyClient
        For Each arg As String In Environment.GetCommandLineArgs()
            If arg = "-update" Then
                ' Prompt user for download
                If MsgBox("Welcome to the Save-EE Patcher Utility." + _
                       vbCrLf + vbCrLf + "Some info for new players:  Players in the Save-EE Lobby use base game versions of EE 2.0 and AoC 1.0 in addition to a set of our own patches made by Omega." + _
                       vbCrLf + vbCrLf + "If you ever need to return to this utility, just hit the Patch tab at the top of the lobby." + _
                       vbCrLf + vbCrLf + "The important stuff:  In order to ensure all players are using the same game versions, this program will attempt to automatically download and install the latest patches Save-EE Patches by Omega." + _
                       vbCrLf + vbCrLf + "Click OK to continue, or Cancel to prevent the download.", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Save-EE Patcher") = MsgBoxResult.Ok Then
                    UpdateDialog.Show()
                    If Not Globals.CheckDirs() Then
                        UpdateDialog.Close()
                        Application.Exit()
                        Exit Sub
                    End If
                    UpdateDialog.InfoTextBox.AppendText(vbCrLf + vbCrLf)
                    ' UpdatePatchFilesButton
                    Dim un As Boolean = False
                    If Globals.CheckForMSI Then
                        If MsgBox("Older installers of Omega's patches were found." & vbCrLf & vbCrLf & "Would you like to uninstall them now?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update") = MsgBoxResult.Yes Then
                            un = True
                        End If
                    End If
                    If un Then
                        Globals.UninstallOldPatches()
                        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf)
                    End If
                    AppActivate(UpdateDialog.Text)
                    Globals.UpdatePatchFiles()
                    AppActivate(UpdateDialog.Text)
                    ' Part of CopyPatchFilesButton
                    Globals.DeleteOldPatch()
                    Globals.CopyPatchToDirs()
                    UpdateDialog.OK_Button.Enabled = True
                    UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
                End If
                UpdateDialog.Close()
                Application.Exit()
            End If
        Next
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
        Globals.LocateEEDir()
        Globals.LocateAoCDir()
    End Sub
    Private Sub AutoUpdateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoUpdateButton.Click
        UpdateDialog.Show()
        ' Find Dirs
        If Not Globals.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        ' Update EE
        If Not Globals.UpdateEE() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        ' DC Patch
        Globals.DCPatch()
        ' Uninstall old patches
        AppActivate(UpdateDialog.Text)
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf)
        Globals.UninstallOldPatches()
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf)
        ' Update patch files
        Globals.UpdatePatchFiles()
        ' Delete old patch files
        'If Not Globals.patchinfoSame Then Globals.DeleteOldPatch()
        Globals.DeleteOldPatch()
        ' Copy new patch files
        'If Not Globals.patchinfoSame Then Globals.CopyPatchToDirs()
        Globals.CopyPatchToDirs()

        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub InstallEEUpdatesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstallEEUpdatesButton.Click
        UpdateDialog.Show()
        If Not Globals.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        Globals.UpdateEE()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub DirectConnectPatchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DirectConnectPatchButton.Click
        UpdateDialog.Show()
        If Not Globals.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        Globals.DCPatch()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub ReplaceWONLobbyCfgButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceWONLobbyCfgButton.Click
        UpdateDialog.Show()
        If Not Globals.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        UpdateDialog.InfoTextBox.AppendText(vbCrLf)
        Globals.ReplaceWONLobbyCfg()
        UpdateDialog.InfoTextBox.AppendText(" complete.")
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub UninstallMSIPatchesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UninstallMSIPatchesButton.Click
        UpdateDialog.Show()
        Globals.UninstallOldPatches()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub CheckForUpdatesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckForUpdatesButton.Click
        Dim un As Boolean = False
        If Globals.CheckForMSI Then
            If MsgBox("Older installers of Omega's patches were found." & vbCrLf & vbCrLf & "Would you like to uninstall them now?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update") = MsgBoxResult.Yes Then
                un = True
            End If
        End If
        UpdateDialog.Show()
        If un Then
            Globals.UninstallOldPatches()
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf)
        End If
        Globals.UpdatePatchFiles()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "To apply the patch to your game, click the ""Copy Patch Files To Install Directories"" button.")
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub ForcePatchUpdateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForcePatchUpdateButton.Click
        ' Delete patchinfo.dat
        Try
            IO.File.Delete("patch\patchinfo.dat")
        Catch
        End Try
        CheckForUpdatesButton_Click(Nothing, Nothing)
    End Sub
    Private Sub CopyPatchFilesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyPatchFilesButton.Click
        UpdateDialog.Show()
        If Not Globals.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        Globals.DeleteOldPatch()
        Globals.CopyPatchToDirs()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub DeletePatchFromDirsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeletePatchFromDirsButton.Click
        UpdateDialog.Show()
        If Not Globals.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        Globals.DeleteCurrentPatch()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub RemoveAllPatchesAndModsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveAllPatchesAndModsButton.Click
        UpdateDialog.Show()
        If Not Globals.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        Globals.ClearDBFolders()
        Globals.ClearTexturesFolders()
        Globals.ClearModelsFolders()
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub DefaultBannerButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultBannerButton.Click
        UpdateDialog.Show()
        If Not Globals.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        UpdateDialog.InfoTextBox.AppendText(vbCrLf)
        Globals.ReplaceBanner()
        UpdateDialog.InfoTextBox.AppendText(" complete.")
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub EENoCDButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EENoCDButton.Click
        UpdateDialog.Show()
        If Not Globals.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        UpdateDialog.InfoTextBox.AppendText(vbCrLf)
        Globals.GetEENoCD()
        'UpdateDialog.InfoTextBox.AppendText(" complete.")
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub AoCNoCDButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AoCNoCDButton.Click
        UpdateDialog.Show()
        If Not Globals.CheckDirs() Then
            UpdateDialog.Close()
            Exit Sub
        End If
        UpdateDialog.InfoTextBox.AppendText(vbCrLf)
        Globals.GetAoCNoCD()
        'UpdateDialog.InfoTextBox.AppendText(" complete.")
        UpdateDialog.OK_Button.Enabled = True
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Click OK to close this window.")
        AppActivate(UpdateDialog.Text)
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If LanguageDialog.Visible Or UpdateDialog.Visible Then
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
