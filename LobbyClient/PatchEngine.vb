Public Class PatchEngine
    Public WithEvents wc As New System.Net.WebClient
    Public FileQueue As New Queue(Of UpdateFile)
    Public CurrentDownload As UpdateFile

    Public AoCInstalled As Boolean
    Public EEUpdateLang As String = ""
    Public miscDownloadDone As Boolean
    Public patchDownloadDone As Boolean
    Public patchinfoSame As Boolean

#Region "Utilites"
    Public Sub CopyDirectory(ByVal SourcePath As String, ByVal DestPath As String, Optional ByVal Overwrite As Boolean = False)
        Dim SourceDir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(SourcePath)
        Dim DestDir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(DestPath)

        ' the source directory must exist, otherwise throw an exception
        If SourceDir.Exists Then
            ' if destination SubDir's parent SubDir does not exist throw an exception
            If Not DestDir.Parent.Exists Then
                Throw New System.IO.DirectoryNotFoundException _
                    ("Destination directory does not exist: " & DestDir.Parent.FullName)
            End If

            If Not DestDir.Exists Then
                DestDir.Create()
            End If

            ' copy all the files of the current directory
            Dim ChildFile As System.IO.FileInfo
            For Each ChildFile In SourceDir.GetFiles()
                If Overwrite Then
                    ChildFile.CopyTo(System.IO.Path.Combine(DestDir.FullName, ChildFile.Name), True)
                Else
                    ' if Overwrite = false, copy the file only if it does not exist
                    ' this is done to avoid an IOException if a file already exists
                    ' this way the other files can be copied anyway...
                    If Not System.IO.File.Exists(System.IO.Path.Combine(DestDir.FullName, ChildFile.Name)) Then
                        ChildFile.CopyTo(System.IO.Path.Combine(DestDir.FullName, ChildFile.Name), False)
                    End If
                End If
            Next

            ' copy all the sub-directories by recursively calling this same routine
            Dim SubDir As System.IO.DirectoryInfo
            For Each SubDir In SourceDir.GetDirectories()
                CopyDirectory(SubDir.FullName, System.IO.Path.Combine(DestDir.FullName, _
                    SubDir.Name), Overwrite)
            Next
        Else
            Throw New System.IO.DirectoryNotFoundException("Source directory does not exist: " & SourceDir.FullName)
        End If
    End Sub
#End Region
#Region "Directories"
    Public Function LocateEEDir(Optional ByVal CheckOnly = False) As Boolean
        ' Function returns success as boolean
        ' Set possible keys (will return Nothing if doesn't exist)
        Dim eeHKLMapppathsx86 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Empire Earth.exe", False)
        Dim eeHKLMapppathsx64 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\App Paths\Empire Earth.exe", False)
        Dim eeHKLMx86 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Sierra OnLine\Setup\EEARTH", False)
        Dim eeHKLMx64 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Wow6432Node\Sierra OnLine\Setup\EEARTH", False)
        Dim eeHKCU As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\SSSI\Empire Earth", False)

        ' Check keys/values
        If eeHKLMapppathsx86 IsNot Nothing Then
            Dim eeDir As String = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Empire Earth.exe").GetValue("Path")
            If eeDir IsNot Nothing Then
                If Not CheckOnly Then Globals.PatcherForm.EEDirTextBox.Text = eeDir
                Return True
            End If
        ElseIf eeHKLMapppathsx64 IsNot Nothing Then
            Dim eeDir As String = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\App Paths\Empire Earth.exe").GetValue("Path")
            If eeDir IsNot Nothing Then
                If Not CheckOnly Then Globals.PatcherForm.EEDirTextBox.Text = eeDir
                Return True
            End If
        ElseIf eeHKLMx86 IsNot Nothing Then
            Dim eeDir As String = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Sierra OnLine\Setup\EEARTH").GetValue("Directory")
            If eeDir IsNot Nothing Then
                If Not CheckOnly Then Globals.PatcherForm.EEDirTextBox.Text = eeDir
                Return True
            End If
        ElseIf eeHKLMx64 IsNot Nothing Then
            Dim eeDir As String = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Wow6432Node\Sierra OnLine\Setup\EEARTH").GetValue("Directory")
            If eeDir IsNot Nothing Then
                If Not CheckOnly Then Globals.PatcherForm.EEDirTextBox.Text = eeDir
                Return True
            End If
        ElseIf eeHKCU IsNot Nothing Then
            Dim eeVol As String = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\SSSI\Empire Earth").GetValue("Installed From Volume")
            Dim eeDir As String = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\SSSI\Empire Earth").GetValue("Installed From Directory")
            If eeVol IsNot Nothing AndAlso eeDir IsNot Nothing Then
                If Not CheckOnly Then Globals.PatcherForm.EEDirTextBox.Text = eeVol & eeDir
                Return True
            End If
        End If

        ' If nothing is found
        Return False
    End Function
    Public Function LocateAoCDir() As Boolean
        ' Function returns success as boolean
        ' Set possible keys (will return Nothing if doesn't exist)
        Dim aocHKLMapppaths As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\EE-AOC.exe", False)
        Dim aocHKLMx86 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Sierra OnLine\Setup\EEAOC", False)
        Dim aocHKLMx64 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Wow6432Node\Sierra OnLine\Setup\EEAOC", False)
        Dim aocHKCU As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Mad Doc Software\EE-AOC", False)

        ' Check keys/values
        If aocHKLMapppaths IsNot Nothing Then
            Dim aocDir As String = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\EE-AOC.exe").GetValue("Path")
            If aocDir IsNot Nothing Then
                Globals.PatcherForm.AoCDirTextBox.Text = aocDir
                Return True
            End If
        ElseIf aocHKLMx86 IsNot Nothing Then
            Dim aocDir As String = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Sierra OnLine\Setup\EEAOC").GetValue("Directory")
            If aocDir IsNot Nothing Then
                Globals.PatcherForm.AoCDirTextBox.Text = aocDir
                Return True
            End If
        ElseIf aocHKLMx64 IsNot Nothing Then
            Dim aocDir As String = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Wow6432Node\Sierra OnLine\Setup\EEAOC").GetValue("Directory")
            If aocDir IsNot Nothing Then
                Globals.PatcherForm.AoCDirTextBox.Text = aocDir
                Return True
            End If
        ElseIf aocHKCU IsNot Nothing Then
            Dim aocVol As String = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Mad Doc Software\EE-AOC").GetValue("Installed From Volume")
            Dim aocDir As String = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Mad Doc Software\EE-AOC").GetValue("Installed From Directory")
            If aocVol IsNot Nothing AndAlso aocDir IsNot Nothing Then
                Globals.PatcherForm.AoCDirTextBox.Text = aocVol & aocDir
                Return True
            End If
        End If

        ' If nothing is found
        Return False
    End Function
    Public Function CheckDirs() As Boolean
        ' Autodetect directories if empty
        If Globals.PatcherForm.EEDirTextBox.Text = "" Then
            If Globals.PatchEngine.LocateEEDir() Then
                UpdateDialog.InfoTextBox.AppendText("Empire Earth installation directory automatically detected.")
            Else
                MsgBox("Unable to automatically locate the Empire Earth install directory." & vbCrLf & _
                "Please make sure Empire Earth is installed or specify a directory.", MsgBoxStyle.Exclamation, "Autodetect Results")
                Return False
            End If
        Else
            UpdateDialog.InfoTextBox.AppendText("Empire Earth installation directory specified.")
        End If
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & Globals.PatcherForm.EEDirTextBox.Text)
        AoCInstalled = True
        If Globals.PatcherForm.AoCDirTextBox.Text = "" Then
            If Globals.PatchEngine.LocateAoCDir() Then
                UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Art of Conquest installation directory automatically detected.")
            Else
                UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Unable to automatically locate the Art of Conquest install directory." & _
                vbCrLf & "Continuing without AoC.  If AoC is installed, run update again with AoC directory specificed.")
                AoCInstalled = False
            End If
        Else
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Art of Conquest installation directory specified.")
        End If
        If AoCInstalled = True Then
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & Globals.PatcherForm.AoCDirTextBox.Text)
        End If
        AppActivate(UpdateDialog.Text)
        Return True
    End Function
#End Region
#Region "EE Updates"
    Public Function CheckEEVer() As Boolean
        ' Check for 1.0.4.0 readme file
        If System.IO.File.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\readme.1.0.4.0.txt") Then
            ' Check for 2.0 readme file
            If System.IO.File.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\readme_DSML_AddOn.txt") Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Function UpdateEE() As Boolean
        miscDownloadDone = False

        ' Check for 1.0.4.0 readme file
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Checking for EE 1.0.4.0 readme file...")
        If System.IO.File.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\readme.1.0.4.0.txt") Then
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "EE 1.0.4.0 update installed.")
        Else
            ' Download the 1.0.4.0 update
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "EE 1.0.4.0 update not found.  Downloading...")
            ' Ask for game language
            EEUpdateLang = "en"
            If (LanguageDialog.ShowDialog() = DialogResult.OK) Then
                EEUpdateLang = LanguageDialog.ReturnLang
            End If
            ' Set URL, download it
            Dim f As New UpdateFile
            f.Url = "http://www.save-ee.com/download/ee1040" & EEUpdateLang & ".exe"
            f.RealFile = "ee1040" & EEUpdateLang & ".exe"
            CurrentDownload = f
            Try
                System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Update")
            Catch
            End Try
            wc.Proxy = Nothing
            wc.DownloadFileAsync(New Uri(f.Url), Globals.AppDataEnvVar & "\Save-EE\Update\" & f.RealFile)
            ' wc_DownloadFileCompleted runs it and waits for completion
            Do Until miscDownloadDone
                Application.DoEvents()
            Loop
            miscDownloadDone = False

            ' Check for readme again
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Checking for EE 1.0.4.0 readme file...")
            If System.IO.File.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\readme.1.0.4.0.txt") Then
                UpdateDialog.InfoTextBox.AppendText(vbCrLf & "EE 1.0.4.0 update installed.")
            Else
                MsgBox("EE 1.0.4.0 update failed.  Stopping update.", MsgBoxStyle.Exclamation, "Update Results")
                Return False
            End If
        End If
        AppActivate(UpdateDialog.Text)

        ' Check for 2.0 readme file
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Checking for EE 2.0 readme file...")
        If System.IO.File.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\readme_DSML_AddOn.txt") Then
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "EE 2.0 update installed.")
        Else
            ' Download the 2.0 update
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "EE 2.0 update not found.  Downloading...")
            ' Ask for game language
            If EEUpdateLang = "" Then
                EEUpdateLang = "en"
                If (LanguageDialog.ShowDialog() = DialogResult.OK) Then
                    EEUpdateLang = LanguageDialog.ReturnLang
                End If
            End If
            ' Set URL, download it
            Dim f As New UpdateFile
            f.Url = "http://www.save-ee.com/download/ee2000" & EEUpdateLang & ".exe"
            f.RealFile = "ee2000" & EEUpdateLang & ".exe"
            CurrentDownload = f
            Try
                System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Update")
            Catch
            End Try
            wc.Proxy = Nothing
            wc.DownloadFileAsync(New Uri(f.Url), Globals.AppDataEnvVar & "\Save-EE\Update\" & f.RealFile)
            ' wc_DownloadFileCompleted runs it and waits for completion
            Do Until miscDownloadDone
                Application.DoEvents()
            Loop
            miscDownloadDone = False

            ' Check for readme again
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Checking for EE 2.0 readme file...")
            If System.IO.File.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\readme_DSML_AddOn.txt") Then
                UpdateDialog.InfoTextBox.AppendText(vbCrLf & "EE 2.0 update installed.")
            Else
                MsgBox("EE 2.0 update failed.  Stopping update.", MsgBoxStyle.Exclamation, "Update Results")
                Return False
            End If
        End If
        AppActivate(UpdateDialog.Text)
        Return True
    End Function
#End Region
#Region "WONLobby.cfg"
    Public Function WONLobbyCfgPatch(ByVal directory As String) As Boolean
        ' Function returns success as boolean
        Dim success As Boolean

        Dim data As String = LobbyShared.FileIO.ReadFile(directory & "\WONLobby.cfg")
        If data <> "" Then
            data = Replace(data, "CDKeyCheck: true", "CDKeyCheck: false", , , CompareMethod.Text)
            data = Replace(data, "cdkeycheck:true", "cdkeycheck:false", , , CompareMethod.Text)
            If LobbyShared.FileIO.WriteFile(directory & "\WONLobby.cfg", data) Then
                success = True
            Else
                success = False
                MsgBox("Unable to write WONLobby.cfg in specified directory.", MsgBoxStyle.Exclamation, "DC Patch Results")
            End If
        Else
            success = False
            MsgBox("Unable to read WONLobby.cfg in specified directory.", MsgBoxStyle.Exclamation, "DC Patch Results")
        End If
        Return success
    End Function
    Public Sub DCPatch()
        ' Do WONLobby.cfg patch
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Performing direct connect patch...")
        ' EE first
        If System.IO.File.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\WONLobby.cfg") Then
            If Globals.PatchEngine.WONLobbyCfgPatch(Globals.PatcherForm.EEDirTextBox.Text) Then
                UpdateDialog.InfoTextBox.AppendText(vbCrLf & "EE direct connect patch complete.")
            Else
                UpdateDialog.InfoTextBox.AppendText(vbCrLf & "EE direct connect patch failed.  Please download the replacement at http://www.save-ee.com/lobby/patch/WONLobby/EE/WONLobby.cfg and place it in" & Globals.PatcherForm.EEDirTextBox.Text & ".")
            End If
        Else
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & Globals.PatcherForm.EEDirTextBox.Text & "\WONLobby.cfg does not exist.  Make sure the 2.0 update is installed correctly.")
        End If
        ' Then AoC
        If AoCInstalled Then
            If System.IO.File.Exists(Globals.PatcherForm.AoCDirTextBox.Text & "\WONLobby.cfg") Then
                If Globals.PatchEngine.WONLobbyCfgPatch(Globals.PatcherForm.AoCDirTextBox.Text) Then
                    UpdateDialog.InfoTextBox.AppendText(vbCrLf & "AoC direct connect patch complete.")
                Else
                    UpdateDialog.InfoTextBox.AppendText(vbCrLf & "AoC direct connect patch failed.  Please download the replacement at http://www.save-ee.com/lobby/patch/WONLobby/AoC/WONLobby.cfg and place it in" & Globals.PatcherForm.AoCDirTextBox.Text & ".")
                End If
            Else
                UpdateDialog.InfoTextBox.AppendText(vbCrLf & Globals.PatcherForm.EEDirTextBox.Text & "\WONLobby.cfg does not exist.  Make sure AoC is installed correctly.")
            End If
        End If
    End Sub
    Public Sub ReplaceWONLobbyCfg()
        miscDownloadDone = False
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Replacing WONLobby.cfg...")
        ' EE first
        Dim f As New UpdateFile
        f.Url = "http://www.save-ee.com/lobby/patch/WONLobby/EE/WONLobby.cfg"
        f.RealFile = "EEWONLobby.cfg"
        CurrentDownload = f
        Try
            System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Patch")
        Catch
        End Try
        wc.DownloadFileAsync(New Uri(f.Url), Globals.AppDataEnvVar & "\Save-EE\Patch\" & f.RealFile)
        ' wc_DownloadFileCompleted returns miscDownloadDone = True
        Do Until miscDownloadDone
            Application.DoEvents()
        Loop
        miscDownloadDone = False
        ' Copy to EE dir
        If Globals.PatcherForm.EEDirTextBox.Text <> "" Then
            If Not CheckEEVer() Then
                If MsgBox("Empire Earth is not updated to version 2.0." & vbCrLf & vbCrLf & "Copy to EE directory anyway?", _
                        MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update Results") = MsgBoxResult.Yes Then
                    Try
                        System.IO.File.Copy(Globals.AppDataEnvVar & "\Save-EE\Patch\EEWONLobby.cfg", Globals.PatcherForm.EEDirTextBox.Text & "\WONLobby.cfg", True)
                    Catch
                        MsgBox("Unable to copy WONLobby.cfg to the EE directory for some reason.  The following directory probably doesn't exist, or you don't have required permissions." & vbCrLf & "Destination: " & Globals.PatcherForm.EEDirTextBox.Text)
                    End Try
                End If
            Else
                Try
                    System.IO.File.Copy(Globals.AppDataEnvVar & "\Save-EE\Patch\EEWONLobby.cfg", Globals.PatcherForm.EEDirTextBox.Text & "\WONLobby.cfg", True)
                Catch
                    MsgBox("Unable to copy WONLobby.cfg to the EE directory for some reason.  The following directory probably doesn't exist, or you don't have required permissions." & vbCrLf & "Destination: " & Globals.PatcherForm.EEDirTextBox.Text)
                End Try
            End If
        End If

        ' AoC second
        f.Url = "http://www.save-ee.com/lobby/patch/WONLobby/AoC/WONLobby.cfg"
        f.RealFile = "AoCWONLobby.cfg"
        CurrentDownload = f
        Try
            System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Patch")
        Catch
        End Try
        wc.DownloadFileAsync(New Uri(f.Url), Globals.AppDataEnvVar & "\Save-EE\Patch\" & f.RealFile)
        ' wc_DownloadFileCompleted returns miscDownloadDone = True
        Do Until miscDownloadDone
            Application.DoEvents()
        Loop
        miscDownloadDone = False
        ' Copy to AoC dir
        If AoCInstalled AndAlso Globals.PatcherForm.AoCDirTextBox.Text <> "" Then
            Try
                System.IO.File.Copy(Globals.AppDataEnvVar & "\Save-EE\Patch\AoCWONLobby.cfg", Globals.PatcherForm.AoCDirTextBox.Text & "\WONLobby.cfg", True)
            Catch
                MsgBox("Unable to copy WONLobby.cfg to the AoC directory for some reason.  The following directory probably doesn't exist, or you don't have required permissions." & vbCrLf & "Destination: " & Globals.PatcherForm.AoCDirTextBox.Text, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub
#End Region
#Region "Omega's MSIs"
    Public Function CheckForMSI() As Boolean
        ' Checking only, returns true if any exist
        If Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\3B1941A1329A5CF419200C2DE786073B", False) Is Nothing _
        OrElse Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\A019A31D6E98E43459C43B5A3864F694", False) Is Nothing _
        OrElse Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\68ADC638C846A3D4C9F21577E260064D", False) Is Nothing _
        OrElse Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\458338B7BE9B57E43B4C199D4BA1DF18", False) Is Nothing _
        OrElse Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\A5446CE9D40693A4399CD03126EF3DAA", False) Is Nothing _
        OrElse Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\F1EE210F1855DDE478131386B5E4B8B4", False) Is Nothing _
        OrElse Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\51FF10E2109834040A229A5A5EDCCC48", False) Is Nothing Then
            Return True
        End If
        Return False
    End Function
    Public Sub UninstallOldPatches()
        ' Check for old MSI installers
        UpdateDialog.InfoTextBox.AppendText("Checking for and removing old patch MSI installers...")
        Dim bReplace As Boolean = False
        ' EE 2.1
        If Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\3B1941A1329A5CF419200C2DE786073B", False) Is Nothing Then
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Uninstalling EE 2.1 patch...")
            Try
                Shell("MsiExec.exe /X{1A1491B3-A923-4FC5-9102-C0D27E6870B3}", AppWinStyle.NormalFocus, True)
            Catch
                MsgBox("Failed to run EE 2.1 patch uninstaller." & vbCrLf & "Shell: MsiExec.exe /X{1A1491B3-A923-4FC5-9102-C0D27E6870B3}")
            End Try
            bReplace = True
        End If
        ' EE 2.1a
        If Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\A019A31D6E98E43459C43B5A3864F694", False) Is Nothing Then
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Uninstalling EE 2.1a patch...")
            Try
                Shell("MsiExec.exe /X{D13A910A-89E6-434E-954C-B3A583466F49}", AppWinStyle.NormalFocus, True)
            Catch
                MsgBox("Failed to run EE 2.1a patch uninstaller." & vbCrLf & "Shell: MsiExec.exe /X{D13A910A-89E6-434E-954C-B3A583466F49}")
            End Try
            bReplace = True
        End If
        ' EE 2.1b
        If Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\68ADC638C846A3D4C9F21577E260064D", False) Is Nothing Then
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Uninstalling EE 2.1b patch...")
            Try
                Shell("MsiExec.exe /X{836CDA86-648C-4D3A-9C2F-51772E0660D4}", AppWinStyle.NormalFocus, True)
            Catch
                MsgBox("Failed to run EE 2.1b patch uninstaller." & vbCrLf & "Shell: MsiExec.exe /X{836CDA86-648C-4D3A-9C2F-51772E0660D4}")
            End Try
            bReplace = True
        End If
        ' AoC 1.1
        If Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\458338B7BE9B57E43B4C199D4BA1DF18", False) Is Nothing Then
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Uninstalling AoC 1.1 patch...")
            Try
                Shell("MsiExec.exe /X{7B833854-B9EB-4E75-B3C4-91D9B41AFD81}", AppWinStyle.NormalFocus, True)
            Catch
                MsgBox("Failed to run AoC 1.1 patch uninstaller." & vbCrLf & "Shell: MsiExec.exe /X{7B833854-B9EB-4E75-B3C4-91D9B41AFD81}")
            End Try
            bReplace = True
        End If
        ' AoC 1.1a
        If Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\A5446CE9D40693A4399CD03126EF3DAA", False) Is Nothing Then
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Uninstalling AoC 1.1a patch...")
            Try
                Shell("MsiExec.exe /X{9EC6445A-604D-4A39-93C9-0D1362FED3AA}", AppWinStyle.NormalFocus, True)
            Catch
                MsgBox("Failed to run AoC 1.1a patch uninstaller." & vbCrLf & "Shell: MsiExec.exe /X{9EC6445A-604D-4A39-93C9-0D1362FED3AA}")
            End Try
            bReplace = True
        End If
        ' AoC 1.1b
        If Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\F1EE210F1855DDE478131386B5E4B8B4", False) Is Nothing Then
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Uninstalling AoC 1.1b patch...")
            Try
                Shell("MsiExec.exe /X{F012EE1F-5581-4EDD-8731-31685B4E8B4B}", AppWinStyle.NormalFocus, True)
            Catch
                MsgBox("Failed to run AoC 1.1b patch uninstaller." & vbCrLf & "Shell: MsiExec.exe /X{F012EE1F-5581-4EDD-8731-31685B4E8B4B}")
            End Try
            bReplace = True
        End If
        ' AoC 1.1c
        If Not Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\Installer\Products\51FF10E2109834040A229A5A5EDCCC48", False) Is Nothing Then
            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Uninstalling AoC 1.1c patch...")
            Try
                Shell("MsiExec.exe /X{2E01FF15-8901-4043-A022-A9A5E5CDCC84}", AppWinStyle.NormalFocus, True)
            Catch
                MsgBox("Failed to run AoC 1.1c patch uninstaller." & vbCrLf & "Shell: MsiExec.exe /X{2E01FF15-8901-4043-A022-A9A5E5CDCC84}")
            End Try
            bReplace = True
        End If
        AppActivate(UpdateDialog.Text)

        ' Replace DefaultBanner.jpg if needed
        If bReplace Then
            ReplaceBanner()
        End If
        UpdateDialog.InfoTextBox.AppendText(" complete.")
    End Sub
    Public Sub ReplaceBanner()
        miscDownloadDone = False
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Replacing DefaultBanner.jpg...")
        Dim f As New UpdateFile
        f.Url = "http://www.save-ee.com/lobby/patch/DefaultBanner.jpg"
        f.RealFile = "DefaultBanner.jpg"
        CurrentDownload = f
        Try
            System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Patch")
        Catch
        End Try
        wc.DownloadFileAsync(New Uri(f.Url), Globals.AppDataEnvVar & "\Save-EE\Patch\" & f.RealFile)
        ' wc_DownloadFileCompleted copies file to EE and AoC dirs
        Do Until miscDownloadDone
            Application.DoEvents()
        Loop
        miscDownloadDone = False
    End Sub
#End Region
#Region "No CD Cracks"
    Public Sub GetEENoCD()
        miscDownloadDone = False
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Downloading Empire Earth.exe...")
        Dim f As New UpdateFile
        f.Url = "http://www.save-ee.com/lobby/patch/NoCD/EE/Empire Earth.exe"
        f.RealFile = "Empire Earth.exe"
        CurrentDownload = f
        Try
            System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Crack")
        Catch
        End Try
        wc.DownloadFileAsync(New Uri(f.Url), Globals.AppDataEnvVar & "\Save-EE\Crack\" & f.RealFile)
        ' wc_DownloadFileCompleted returns miscDownloadDone = True
        Do Until miscDownloadDone
            Application.DoEvents()
        Loop
        miscDownloadDone = False
        UpdateDialog.InfoTextBox.AppendText(" complete.")
        ' Copy to EE dir
        If Globals.PatcherForm.EEDirTextBox.Text <> "" Then
            If Not CheckEEVer() Then
                If MsgBox("Empire Earth is not updated to version 2.0." & vbCrLf & vbCrLf & "Copy to EE directory anyway?", _
                        MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update Results") = MsgBoxResult.Yes Then
                    ' Make backup
                    If System.IO.File.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\Empire Earth.exe") Then
                        If My.Computer.FileSystem.GetFileInfo(Globals.PatcherForm.EEDirTextBox.Text & "\Empire Earth.exe").Length() <> 6321152 Then
                            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Making Empire Earth.exe backup...")
                            Try
                                System.IO.File.Copy(Globals.PatcherForm.EEDirTextBox.Text & "\Empire Earth.exe", Globals.PatcherForm.EEDirTextBox.Text & "\Empire Earth.backup.exe", True)
                            Catch
                                MsgBox("Unable to copy Empire Earth.exe to a backup.", MsgBoxStyle.Critical, "Error")
                            End Try
                            UpdateDialog.InfoTextBox.AppendText(" complete.")
                        End If
                    End If
                    Try
                        ' Overwrite
                        UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Replacing Empire Earth.exe...")
                        System.IO.File.Copy(Globals.AppDataEnvVar & "\Save-EE\Crack\Empire Earth.exe", Globals.PatcherForm.EEDirTextBox.Text & "\Empire Earth.exe", True)
                        UpdateDialog.InfoTextBox.AppendText(" complete.")
                    Catch
                        MsgBox("Unable to copy Empire Earth.exe to the EE directory for some reason.  The following directory probably doesn't exist, or you don't have required permissions." & vbCrLf & "Destination: " & Globals.PatcherForm.EEDirTextBox.Text, MsgBoxStyle.Critical, "Error")
                    End Try
                End If
            Else
                ' Make backup
                If System.IO.File.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\Empire Earth.exe") Then
                    If My.Computer.FileSystem.GetFileInfo(Globals.PatcherForm.EEDirTextBox.Text & "\Empire Earth.exe").Length() <> 6321152 Then
                        Try
                            UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Making Empire Earth.exe backup...")
                            System.IO.File.Copy(Globals.PatcherForm.EEDirTextBox.Text & "\Empire Earth.exe", Globals.PatcherForm.EEDirTextBox.Text & "\Empire Earth.backup.exe", True)
                            UpdateDialog.InfoTextBox.AppendText(" complete.")
                        Catch
                            MsgBox("Unable to make a backup copy of Empire Earth.exe for some reason.  The following directory probably doesn't exist, or you don't have required permissions." & vbCrLf & "Destination: " & Globals.PatcherForm.EEDirTextBox.Text, MsgBoxStyle.Critical, "Error")
                        End Try
                    End If
                End If
                Try
                    ' Overwrite
                    UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Replacing Empire Earth.exe...")
                    System.IO.File.Copy(Globals.AppDataEnvVar & "\Save-EE\Crack\Empire Earth.exe", Globals.PatcherForm.EEDirTextBox.Text & "\Empire Earth.exe", True)
                    UpdateDialog.InfoTextBox.AppendText(" complete.")
                Catch
                    MsgBox("Unable to copy Empire Earth.exe to the EE directory for some reason.  The following directory probably doesn't exist, or you don't have required permissions." & vbCrLf & "Destination: " & Globals.PatcherForm.EEDirTextBox.Text, MsgBoxStyle.Critical, "Error")
                End Try
            End If
        End If
    End Sub
    Public Sub GetAoCNoCD()
        miscDownloadDone = False
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Downloading EE-AOC.exe...")
        Dim f As New UpdateFile
        f.Url = "http://www.save-ee.com/lobby/patch/NoCD/AoC/EE-AOC.exe"
        f.RealFile = "EE-AOC.exe"
        CurrentDownload = f
        Try
            System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Crack")
        Catch
        End Try
        wc.DownloadFileAsync(New Uri(f.Url), Globals.AppDataEnvVar & "\Save-EE\Crack\" & f.RealFile)
        ' wc_DownloadFileCompleted returns miscDownloadDone = True
        Do Until miscDownloadDone
            Application.DoEvents()
        Loop
        miscDownloadDone = False
        UpdateDialog.InfoTextBox.AppendText(" complete.")
        ' Copy to EE dir
        If AoCInstalled AndAlso Globals.PatcherForm.AoCDirTextBox.Text <> "" Then
            ' Make backup
            If System.IO.File.Exists(Globals.PatcherForm.AoCDirTextBox.Text & "\EE-AOC.exe") Then
                If My.Computer.FileSystem.GetFileInfo(Globals.PatcherForm.AoCDirTextBox.Text & "\EE-AOC.exe").Length() <> 6251008 Then
                    Try
                        UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Making EE-AOC.exe backup...")
                        System.IO.File.Copy(Globals.PatcherForm.AoCDirTextBox.Text & "\EE-AOC.exe", Globals.PatcherForm.AoCDirTextBox.Text & "\EE-AOC.backup.exe", True)
                        UpdateDialog.InfoTextBox.AppendText(" complete.")
                    Catch
                        MsgBox("Unable to make a backup copy of EE-AOC.exe for some reason.  The following directory probably doesn't exist, or you don't have required permissions." & vbCrLf & "Destination: " & Globals.PatcherForm.AoCDirTextBox.Text, MsgBoxStyle.Critical, "Error")
                    End Try
                End If
            End If
            Try
                ' Overwrite
                UpdateDialog.InfoTextBox.AppendText(vbCrLf & "Replacing EE-AOC.exe...")
                System.IO.File.Copy(Globals.AppDataEnvVar & "\Save-EE\Crack\EE-AOC.exe", Globals.PatcherForm.AoCDirTextBox.Text & "\EE-AOC.exe", True)
                UpdateDialog.InfoTextBox.AppendText(" complete.")
            Catch
                MsgBox("Unable to copy EE-AOC.exe to the AoC directory for some reason.  The following directory probably doesn't exist, or you don't have required permissions." & vbCrLf & "Destination: " & Globals.PatcherForm.AoCDirTextBox.Text, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub
#End Region
#Region "Omega's Patch"
    Public Sub UpdatePatchFiles()
        patchDownloadDone = False
        ' Check patchinfo.dat
        UpdateDialog.InfoTextBox.AppendText("Checking for updates...")
        Dim f As New UpdateFile
        f.Url = "http://www.save-ee.com/lobby/patch/patchinfo.dat"
        f.RealFile = "patchinfo.dat"
        CurrentDownload = f
        wc.Proxy = Nothing
        wc.DownloadStringAsync(New Uri(f.Url))
        ' Goes to wc_DownloadStringCompleted, waits until finished
        Do Until patchDownloadDone
            Application.DoEvents()
        Loop

        ' Deleting anything unnecessary from patch download folder
        'Dim patchinfoold As String = LobbyShared.FileIO.ReadFile(Globals.AppDataEnvVar & "\Save-EE\Patch\patchinfoold.dat")
        Dim patchinfo As String = LobbyShared.FileIO.ReadFile(Globals.AppDataEnvVar & "\Save-EE\Patch\patchinfo.dat")

        ' Loop thru each file in our EE patch directory and delete if not part of the patch
        For Each fileInfo As System.IO.FileInfo In New System.IO.DirectoryInfo(Globals.AppDataEnvVar & "\Save-EE\Patch\ee").GetFiles("*", System.IO.SearchOption.AllDirectories)
            ' Use the part of the full filename we care about
            Dim str As String = Mid(fileInfo.FullName, (Globals.AppDataEnvVar & "\Save-EE\Patch\").Length + 1)
            If Not patchinfo.Contains(str) Then
                ' Not in patch info, so delete it
                Try
                    System.IO.File.Delete(fileInfo.FullName)
                Catch
                End Try
            End If
        Next
        ' Now do the same thing for AoC
        For Each fileInfo As System.IO.FileInfo In New System.IO.DirectoryInfo(Globals.AppDataEnvVar & "\Save-EE\Patch\aoc").GetFiles("*", System.IO.SearchOption.AllDirectories)
            ' Use the part of the full filename we care about
            Dim str As String = Mid(fileInfo.FullName, (Globals.AppDataEnvVar & "\Save-EE\Patch\").Length + 1)
            If Not patchinfo.Contains(str) Then
                ' Not in patch info, so delete it
                Try
                    System.IO.File.Delete(fileInfo.FullName)
                Catch
                End Try
            End If
        Next

        If patchinfoSame Then UpdateDialog.InfoTextBox.AppendText(" complete.")
    End Sub
    Public Sub RemovePatchFromDirs(ByVal patchinfo As String)
        ' Count # of files
        Dim infoRows As String() = Split(patchinfo, vbCrLf)
        Dim deleteThese As New List(Of String)
        ' Go thru each uir item, set the real filename to the string
        ' Replaces ee/aoc prefixes with proper installation directories
        For Each r As String In infoRows
            If r <> "" Then
                Dim c() As String = Split(r, "[::]")
                r = c(2)
                r = Replace(r, "ee\", Globals.PatcherForm.EEDirTextBox.Text & "\")
                r = Replace(r, "aoc\", Globals.PatcherForm.AoCDirTextBox.Text & "\")
                deleteThese.Add(r)
            End If
        Next
        ' Loop thru each file, delete
        For Each r As String In deleteThese
            Try
                System.IO.File.Delete(r)
            Catch
            End Try
        Next
    End Sub
    Public Sub CopyPatchToDirs()
        ' Copy patch files to installation directories

        ' Clear db folders first so we don't get crazy retarded game versions because of unneeded files being there
        ClearDBFolders()

        ' Now copy
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Copying patch files to install directories...")
        If Globals.PatcherForm.EEDirTextBox.Text <> "" Then
            If Not CheckEEVer() Then
                If MsgBox("Empire Earth is not updated to version 2.0." & vbCrLf & vbCrLf & "Copy to EE directory anyway?", _
                        MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update Results") = MsgBoxResult.Yes Then
                    Try
                        CopyDirectory(Globals.AppDataEnvVar & "\Save-EE\Patch\ee", Globals.PatcherForm.EEDirTextBox.Text, True)
                    Catch
                        MsgBox("Unable to copy patch files to Empire Earth directory.", MsgBoxStyle.Critical, "Error")
                    End Try
                End If
            Else
                Try
                    CopyDirectory(Globals.AppDataEnvVar & "\Save-EE\Patch\ee", Globals.PatcherForm.EEDirTextBox.Text, True)
                Catch
                    MsgBox("Unable to copy patch files to Empire Earth directory.", MsgBoxStyle.Critical, "Error")
                End Try
            End If
        End If
        If Globals.PatcherForm.AoCDirTextBox.Text <> "" Then
            Try
                CopyDirectory(Globals.AppDataEnvVar & "\Save-EE\Patch\aoc", Globals.PatcherForm.AoCDirTextBox.Text, True)
            Catch ex As Exception
                MsgBox("Unable to copy patch files to Art of Conquest directory.", MsgBoxStyle.Critical, "Error")
            End Try
        End If
        UpdateDialog.InfoTextBox.AppendText(" complete.")
    End Sub
    Public Sub DeleteOldPatch()
        ' Delete old patch files
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Deleting old patch files...")

        ' Deleting from game directories
        Dim patchinfoold As String = LobbyShared.FileIO.ReadFile(Globals.AppDataEnvVar & "\Save-EE\Patch\patchinfoold.dat")
        RemovePatchFromDirs(patchinfoold)
        UpdateDialog.InfoTextBox.AppendText(" complete.")

        ' Make sure db folder is clear
        ClearDBFolders()
    End Sub
    Public Sub DeleteCurrentPatch()
        ' Delete current patch files
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Deleting current patch files...")

        ' Deleting from game directories
        ' Read file
        Dim patchinfo As String = LobbyShared.FileIO.ReadFile(Globals.AppDataEnvVar & "\Save-EE\Patch\patchinfo.dat")
        If patchinfo = "" Then
            MsgBox("Error: Cannot read " & Globals.AppDataEnvVar & "\Save-EE\Patch\patchinfo.dat" & vbCrLf & vbCrLf & "Update your patch files so I know what needs deleted...", MsgBoxStyle.Critical, "Update Results")
            UpdateDialog.InfoTextBox.AppendText(" failed.")
            Exit Sub
        End If
        RemovePatchFromDirs(patchinfo)
        UpdateDialog.InfoTextBox.AppendText(" complete.")

        ' Make sure db folder is clear
        ClearDBFolders()
    End Sub
#End Region
#Region "Cleanup"
    Public Sub ClearDBFolders()
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Clearing db folder(s)...")
        Try
            If Globals.PatcherForm.EEDirTextBox.Text <> "" Then
                If System.IO.Directory.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\Data\db") Then
                    System.IO.Directory.Delete(Globals.PatcherForm.EEDirTextBox.Text & "\Data\db", True)
                End If
            End If
            If Globals.PatcherForm.AoCDirTextBox.Text <> "" Then
                If System.IO.Directory.Exists(Globals.PatcherForm.AoCDirTextBox.Text & "\Data\db") Then
                    System.IO.Directory.Delete(Globals.PatcherForm.AoCDirTextBox.Text & "\Data\db", True)
                End If
            End If
        Catch
            MsgBox("Unable to clear db folder(s).", MsgBoxStyle.Critical, "Error")
        End Try
        UpdateDialog.InfoTextBox.AppendText(" complete.")
    End Sub
    Public Sub ClearTexturesFolders()
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Clearing textures folder(s)...")
        Try
            If Globals.PatcherForm.EEDirTextBox.Text <> "" Then
                If System.IO.Directory.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\Data\textures") Then
                    System.IO.Directory.Delete(Globals.PatcherForm.EEDirTextBox.Text & "\Data\textures", True)
                End If
            End If
            If Globals.PatcherForm.AoCDirTextBox.Text <> "" Then
                If System.IO.Directory.Exists(Globals.PatcherForm.AoCDirTextBox.Text & "\Data\textures") Then
                    System.IO.Directory.Delete(Globals.PatcherForm.AoCDirTextBox.Text & "\Data\textures", True)
                End If
            End If
        Catch
            MsgBox("Unable to clear textures folder(s).", MsgBoxStyle.Critical, "Error")
        End Try
        UpdateDialog.InfoTextBox.AppendText(" complete.")
    End Sub
    Public Sub ClearCivilizationsFolders()
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Clearing civilizations folder(s)...")
        Try
            If Globals.PatcherForm.EEDirTextBox.Text <> "" Then
                If System.IO.Directory.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\Data\civilizations") Then
                    System.IO.Directory.Delete(Globals.PatcherForm.EEDirTextBox.Text & "\Data\civilizations", True)
                End If
            End If
            If Globals.PatcherForm.AoCDirTextBox.Text <> "" Then
                If System.IO.Directory.Exists(Globals.PatcherForm.AoCDirTextBox.Text & "\Data\civilizations") Then
                    System.IO.Directory.Delete(Globals.PatcherForm.AoCDirTextBox.Text & "\Data\civilizations", True)
                End If
            End If
        Catch
            MsgBox("Unable to clear civilizations folder(s).", MsgBoxStyle.Critical, "Error")
        End Try
        UpdateDialog.InfoTextBox.AppendText(" complete.")
    End Sub
    Public Sub ClearModelsFolders()
        UpdateDialog.InfoTextBox.AppendText(vbCrLf & vbCrLf & "Clearing Models folder(s)...")
        Try
            If Globals.PatcherForm.EEDirTextBox.Text <> "" Then
                If System.IO.Directory.Exists(Globals.PatcherForm.EEDirTextBox.Text & "\Data\Models") Then
                    For Each f As System.IO.FileInfo In New System.IO.DirectoryInfo(Globals.PatcherForm.EEDirTextBox.Text & "\Data\Models").GetFiles
                        If f.Name <> "amb_rock.cem" Then
                            f.Delete()
                        End If
                    Next
                    For Each d As System.IO.DirectoryInfo In New System.IO.DirectoryInfo(Globals.PatcherForm.EEDirTextBox.Text & "\Data\Models").GetDirectories
                        d.Delete(True)
                    Next
                End If
            End If
            If Globals.PatcherForm.AoCDirTextBox.Text <> "" Then
                If System.IO.Directory.Exists(Globals.PatcherForm.AoCDirTextBox.Text & "\Data\Models") Then
                    For Each f As System.IO.FileInfo In New System.IO.DirectoryInfo(Globals.PatcherForm.AoCDirTextBox.Text & "\Data\Models").GetFiles
                        If f.Name <> "amb_rock.cem" Then
                            f.Delete()
                        End If
                    Next
                    For Each d As System.IO.DirectoryInfo In New System.IO.DirectoryInfo(Globals.PatcherForm.AoCDirTextBox.Text & "\Data\Models").GetDirectories
                        d.Delete(True)
                    Next
                End If
            End If
        Catch
            MsgBox("Unable to clear Models folder(s).", MsgBoxStyle.Critical, "Error")
        End Try
        UpdateDialog.InfoTextBox.AppendText(" complete.")
    End Sub
#End Region
#Region "Web Client"
    Public Sub wc_DownloadStringCompleted(ByVal sender As Object, ByVal e As System.Net.DownloadStringCompletedEventArgs) Handles wc.DownloadStringCompleted
        ' Compare old and new
        patchinfoSame = False
        Dim patchinfo As String = e.Result
        Dim patchinfoold As String = LobbyShared.FileIO.ReadFile(Globals.AppDataEnvVar & "\Save-EE\Patch\patchinfo.dat")

        ' If same, done and stop subroutine
        If patchinfo = patchinfoold Then
            patchDownloadDone = True
            patchinfoSame = True
            Exit Sub
        End If

        ' Write new one
        Try
            System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Patch")
        Catch
        End Try
        LobbyShared.FileIO.WriteFile(Globals.AppDataEnvVar & "\Save-EE\Patch\patchinfo.dat", patchinfo)

        ' Count # of files
        Dim infoRows As String() = Split(e.Result, vbCrLf)
        UpdateDialog.InfoTextBox.AppendText(infoRows.Length & " file(s) found, computing and comparing hashes, please wait..." & vbCrLf & vbCrLf)

        Dim cnt As Integer = -1
        For Each r As String In infoRows
            cnt += 1
            If r <> "" Then
                Dim c() As String = Split(r, "[::]")
                Dim ch As String = ""
                Try
                    ch = LobbyShared.FileHash.GetFileHash(Globals.AppDataEnvVar & "\Save-EE\Patch\" & c(2))
                    If ch <> c(4) Then
                        UpdateDialog.InfoTextBox.AppendText(c(2) & " different, queued for download..." & vbCrLf)
                        Dim f As New UpdateFile
                        f.FromUpdateString(r)
                        FileQueue.Enqueue(f)
                    Else
                        UpdateDialog.InfoTextBox.AppendText(c(2) & " file ok..." & vbCrLf)
                    End If
                Catch ex As Exception
                    UpdateDialog.InfoTextBox.AppendText(c(2) & " different, queued for download..." & vbCrLf)
                    Dim f As New UpdateFile
                    f.FromUpdateString(r)
                    FileQueue.Enqueue(f)
                End Try
            End If
        Next
        UpdateDialog.InfoTextBox.AppendText(vbCrLf)
        If FileQueue.Count = 0 Then
            UpdateDialog.InfoTextBox.AppendText("Patch files are up to date." & vbCrLf)
            patchDownloadDone = True
        Else
            UpdateDialog.InfoTextBox.AppendText(FileQueue.Count & " file(s) different, preparing to download..." & vbCrLf)
            DownloadFile()
        End If
    End Sub
    Public Sub DownloadFile()
        If FileQueue.Count > 0 Then
            CurrentDownload = FileQueue.Dequeue
            ' Check for directory
            Try
                System.IO.Directory.CreateDirectory(Globals.AppDataEnvVar & "\Save-EE\Patch\" & CurrentDownload.RealFile.Substring(0, CurrentDownload.RealFile.LastIndexOf("\")))
            Catch
            End Try
            wc.DownloadFileAsync(New Uri(CurrentDownload.Url), Globals.AppDataEnvVar & "\Save-EE\Patch\" & CurrentDownload.RealFile)
        Else
            UpdateDialog.InfoTextBox.AppendText("Patch download complete." & vbCrLf)
            patchDownloadDone = True
        End If
    End Sub
    Public Sub wc_DownloadProgressChanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles wc.DownloadProgressChanged
        Try
            UpdateDialog.DownloadProgressBar.Minimum = 0
            UpdateDialog.DownloadProgressBar.Maximum = 100
            UpdateDialog.DownloadProgressBar.Value = e.ProgressPercentage
            UpdateDialog.DownloadProgressLabel.Text = "Downloading " & CurrentDownload.RealFile.Substring(CurrentDownload.RealFile.LastIndexOf("\") + 1) & " (" & e.BytesReceived & " / " & e.TotalBytesToReceive & ")"
        Catch ex As Exception
        End Try
    End Sub
    Public Sub wc_DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.DownloadFileCompleted
        If CurrentDownload.RealFile.StartsWith("ee1040") Then
            UpdateDialog.InfoTextBox.AppendText(" complete." & vbCrLf & "Running 1.0.4.0 update.")
            Try
                Shell(Globals.AppDataEnvVar & "\Save-EE\Update\ee1040" & EEUpdateLang & ".exe", AppWinStyle.NormalFocus, True)
            Catch
                MsgBox("Failed to run 1.0.4.0 updater." & vbCrLf & "Src: " & Globals.AppDataEnvVar & "\Save-EE\Update\ee1040" & EEUpdateLang & ".exe")
            End Try
            miscDownloadDone = True
        ElseIf CurrentDownload.RealFile.StartsWith("ee2000") Then
            UpdateDialog.InfoTextBox.AppendText(" complete." & vbCrLf & "Running 2.0 update.")
            Try
                Shell(Globals.AppDataEnvVar & "\Save-EE\Update\ee2000" & EEUpdateLang & ".exe", AppWinStyle.NormalFocus, True)
            Catch
                MsgBox("Failed to run 2.0 updater." & vbCrLf & "Src: " & Globals.AppDataEnvVar & "\Save-EE\Update\ee2000" & EEUpdateLang & ".exe")
            End Try
            miscDownloadDone = True
        ElseIf CurrentDownload.RealFile = "DefaultBanner.jpg" Then
            If Globals.PatcherForm.EEDirTextBox.Text <> "" Then
                Try
                    System.IO.File.Copy(Globals.AppDataEnvVar & "\Save-EE\Patch\DefaultBanner.jpg", Globals.PatcherForm.EEDirTextBox.Text & "\Data\WONLobby Resources\Images\DefaultBanner.jpg", True)
                Catch
                    MsgBox("Unable to copy DefaultBanner.jpg to the EE directory for some reason.  The following directory probably doesn't exist." & vbCrLf & "Destination: " & Globals.PatcherForm.EEDirTextBox.Text & "\Data\WONLobby Resources\Images")
                End Try
            End If
            If AoCInstalled AndAlso Globals.PatcherForm.AoCDirTextBox.Text <> "" Then
                Try
                    System.IO.File.Copy(Globals.AppDataEnvVar & "\Save-EE\Patch\DefaultBanner.jpg", Globals.PatcherForm.AoCDirTextBox.Text & "\Data\WONLobby Resources\Images\DefaultBanner.jpg", True)
                Catch
                    MsgBox("Unable to copy DefaultBanner.jpg to the AoC directory for some reason.  The following directory probably doesn't exist." & vbCrLf & "Destination: " & Globals.PatcherForm.AoCDirTextBox.Text & "\Data\WONLobby Resources\Images")
                End Try
            End If
            miscDownloadDone = True
        ElseIf CurrentDownload.RealFile = "EEWONLobby.cfg" OrElse CurrentDownload.RealFile = "AoCWONLobby.cfg" OrElse CurrentDownload.RealFile = "Empire Earth.exe" OrElse CurrentDownload.RealFile = "EE-AOC.exe" Then
            miscDownloadDone = True
        Else
            DownloadFile()
        End If
    End Sub
#End Region
End Class
