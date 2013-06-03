<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPatch
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPatch))
        Me.EEDirLabel = New System.Windows.Forms.Label
        Me.AoCDirLabel = New System.Windows.Forms.Label
        Me.AoCDirBrowseButton = New System.Windows.Forms.Button
        Me.AoCDirTextBox = New System.Windows.Forms.TextBox
        Me.EEDirBrowseButton = New System.Windows.Forms.Button
        Me.EEDirTextBox = New System.Windows.Forms.TextBox
        Me.AutoUpdateButton = New System.Windows.Forms.Button
        Me.InstallEEUpdatesButton = New System.Windows.Forms.Button
        Me.DirectConnectPatchButton = New System.Windows.Forms.Button
        Me.UninstallMSIPatchesButton = New System.Windows.Forms.Button
        Me.CheckForUpdatesButton = New System.Windows.Forms.Button
        Me.CopyPatchFilesButton = New System.Windows.Forms.Button
        Me.DeletePatchFromDirsButton = New System.Windows.Forms.Button
        Me.AutodetectInstallDirsButton = New System.Windows.Forms.Button
        Me.DefaultBannerButton = New System.Windows.Forms.Button
        Me.ReplaceWONLobbyCfgButton = New System.Windows.Forms.Button
        Me.ForcePatchUpdateButton = New System.Windows.Forms.Button
        Me.EENoCDButton = New System.Windows.Forms.Button
        Me.AoCNoCDButton = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.RemoveAllPatchesAndModsButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'EEDirLabel
        '
        Me.EEDirLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.EEDirLabel.AutoSize = True
        Me.EEDirLabel.Location = New System.Drawing.Point(12, 405)
        Me.EEDirLabel.Name = "EEDirLabel"
        Me.EEDirLabel.Size = New System.Drawing.Size(151, 13)
        Me.EEDirLabel.TabIndex = 11
        Me.EEDirLabel.Text = "Empire Earth Install Directory:"
        '
        'AoCDirLabel
        '
        Me.AoCDirLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AoCDirLabel.AutoSize = True
        Me.AoCDirLabel.Location = New System.Drawing.Point(12, 445)
        Me.AoCDirLabel.Name = "AoCDirLabel"
        Me.AoCDirLabel.Size = New System.Drawing.Size(167, 13)
        Me.AoCDirLabel.TabIndex = 10
        Me.AoCDirLabel.Text = "Art of Conquest Install Directory:"
        '
        'AoCDirBrowseButton
        '
        Me.AoCDirBrowseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AoCDirBrowseButton.Location = New System.Drawing.Point(405, 459)
        Me.AoCDirBrowseButton.Name = "AoCDirBrowseButton"
        Me.AoCDirBrowseButton.Size = New System.Drawing.Size(75, 23)
        Me.AoCDirBrowseButton.TabIndex = 9
        Me.AoCDirBrowseButton.Text = "Browse..."
        Me.AoCDirBrowseButton.UseVisualStyleBackColor = True
        '
        'AoCDirTextBox
        '
        Me.AoCDirTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.AoCDirTextBox.Location = New System.Drawing.Point(12, 461)
        Me.AoCDirTextBox.Name = "AoCDirTextBox"
        Me.AoCDirTextBox.Size = New System.Drawing.Size(387, 21)
        Me.AoCDirTextBox.TabIndex = 8
        '
        'EEDirBrowseButton
        '
        Me.EEDirBrowseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.EEDirBrowseButton.Location = New System.Drawing.Point(405, 419)
        Me.EEDirBrowseButton.Name = "EEDirBrowseButton"
        Me.EEDirBrowseButton.Size = New System.Drawing.Size(75, 23)
        Me.EEDirBrowseButton.TabIndex = 7
        Me.EEDirBrowseButton.Text = "Browse..."
        Me.EEDirBrowseButton.UseVisualStyleBackColor = True
        '
        'EEDirTextBox
        '
        Me.EEDirTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.EEDirTextBox.Location = New System.Drawing.Point(12, 421)
        Me.EEDirTextBox.Name = "EEDirTextBox"
        Me.EEDirTextBox.Size = New System.Drawing.Size(387, 21)
        Me.EEDirTextBox.TabIndex = 6
        '
        'AutoUpdateButton
        '
        Me.AutoUpdateButton.Location = New System.Drawing.Point(12, 60)
        Me.AutoUpdateButton.Name = "AutoUpdateButton"
        Me.AutoUpdateButton.Size = New System.Drawing.Size(468, 42)
        Me.AutoUpdateButton.TabIndex = 12
        Me.AutoUpdateButton.Text = "Auto-Update"
        Me.AutoUpdateButton.UseVisualStyleBackColor = True
        '
        'InstallEEUpdatesButton
        '
        Me.InstallEEUpdatesButton.Location = New System.Drawing.Point(12, 108)
        Me.InstallEEUpdatesButton.Name = "InstallEEUpdatesButton"
        Me.InstallEEUpdatesButton.Size = New System.Drawing.Size(231, 42)
        Me.InstallEEUpdatesButton.TabIndex = 13
        Me.InstallEEUpdatesButton.Text = "Check For, Download, and Run EE Updates" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1.0 to 1.0.4.0 to 2.0"
        Me.InstallEEUpdatesButton.UseVisualStyleBackColor = True
        '
        'DirectConnectPatchButton
        '
        Me.DirectConnectPatchButton.Location = New System.Drawing.Point(12, 156)
        Me.DirectConnectPatchButton.Name = "DirectConnectPatchButton"
        Me.DirectConnectPatchButton.Size = New System.Drawing.Size(231, 42)
        Me.DirectConnectPatchButton.TabIndex = 14
        Me.DirectConnectPatchButton.Text = "Direct Connect Patch" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(WONLobby.cfg)"
        Me.DirectConnectPatchButton.UseVisualStyleBackColor = True
        '
        'UninstallMSIPatchesButton
        '
        Me.UninstallMSIPatchesButton.Location = New System.Drawing.Point(12, 252)
        Me.UninstallMSIPatchesButton.Name = "UninstallMSIPatchesButton"
        Me.UninstallMSIPatchesButton.Size = New System.Drawing.Size(231, 42)
        Me.UninstallMSIPatchesButton.TabIndex = 15
        Me.UninstallMSIPatchesButton.Text = "Uninstall Old Omega Patches" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(2.1b/1.1c and earlier MSI Installers)"
        Me.UninstallMSIPatchesButton.UseVisualStyleBackColor = True
        '
        'CheckForUpdatesButton
        '
        Me.CheckForUpdatesButton.Location = New System.Drawing.Point(249, 108)
        Me.CheckForUpdatesButton.Name = "CheckForUpdatesButton"
        Me.CheckForUpdatesButton.Size = New System.Drawing.Size(231, 42)
        Me.CheckForUpdatesButton.TabIndex = 16
        Me.CheckForUpdatesButton.Text = "Omega's Patches:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Check For Patch Updates"
        Me.CheckForUpdatesButton.UseVisualStyleBackColor = True
        '
        'CopyPatchFilesButton
        '
        Me.CopyPatchFilesButton.Location = New System.Drawing.Point(249, 204)
        Me.CopyPatchFilesButton.Name = "CopyPatchFilesButton"
        Me.CopyPatchFilesButton.Size = New System.Drawing.Size(231, 42)
        Me.CopyPatchFilesButton.TabIndex = 17
        Me.CopyPatchFilesButton.Text = "Omega's Patches:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Copy Patch Files To Install Directories"
        Me.CopyPatchFilesButton.UseVisualStyleBackColor = True
        '
        'DeletePatchFromDirsButton
        '
        Me.DeletePatchFromDirsButton.Location = New System.Drawing.Point(249, 252)
        Me.DeletePatchFromDirsButton.Name = "DeletePatchFromDirsButton"
        Me.DeletePatchFromDirsButton.Size = New System.Drawing.Size(231, 42)
        Me.DeletePatchFromDirsButton.TabIndex = 18
        Me.DeletePatchFromDirsButton.Text = "Omega's Patches:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Delete Patch Files From Install Directories"
        Me.DeletePatchFromDirsButton.UseVisualStyleBackColor = True
        '
        'AutodetectInstallDirsButton
        '
        Me.AutodetectInstallDirsButton.Location = New System.Drawing.Point(12, 12)
        Me.AutodetectInstallDirsButton.Name = "AutodetectInstallDirsButton"
        Me.AutodetectInstallDirsButton.Size = New System.Drawing.Size(468, 42)
        Me.AutodetectInstallDirsButton.TabIndex = 19
        Me.AutodetectInstallDirsButton.Text = "Autodetect Install Directories"
        Me.AutodetectInstallDirsButton.UseVisualStyleBackColor = True
        '
        'DefaultBannerButton
        '
        Me.DefaultBannerButton.Location = New System.Drawing.Point(12, 300)
        Me.DefaultBannerButton.Name = "DefaultBannerButton"
        Me.DefaultBannerButton.Size = New System.Drawing.Size(231, 42)
        Me.DefaultBannerButton.TabIndex = 20
        Me.DefaultBannerButton.Text = "Replace DefaultBanner.jpg" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Fixes a ""FailedToDecodeImage"" Error)"
        Me.DefaultBannerButton.UseVisualStyleBackColor = True
        '
        'ReplaceWONLobbyCfgButton
        '
        Me.ReplaceWONLobbyCfgButton.Location = New System.Drawing.Point(12, 204)
        Me.ReplaceWONLobbyCfgButton.Name = "ReplaceWONLobbyCfgButton"
        Me.ReplaceWONLobbyCfgButton.Size = New System.Drawing.Size(231, 42)
        Me.ReplaceWONLobbyCfgButton.TabIndex = 21
        Me.ReplaceWONLobbyCfgButton.Text = "Entirely Replace WONLobby.cfg" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(ENGLISH GAME VERSION ONLY)"
        Me.ReplaceWONLobbyCfgButton.UseVisualStyleBackColor = True
        '
        'ForcePatchUpdateButton
        '
        Me.ForcePatchUpdateButton.Location = New System.Drawing.Point(249, 156)
        Me.ForcePatchUpdateButton.Name = "ForcePatchUpdateButton"
        Me.ForcePatchUpdateButton.Size = New System.Drawing.Size(231, 42)
        Me.ForcePatchUpdateButton.TabIndex = 22
        Me.ForcePatchUpdateButton.Text = "Omega's Patches:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Force Patch Update"
        Me.ForcePatchUpdateButton.UseVisualStyleBackColor = True
        '
        'EENoCDButton
        '
        Me.EENoCDButton.Location = New System.Drawing.Point(12, 348)
        Me.EENoCDButton.Name = "EENoCDButton"
        Me.EENoCDButton.Size = New System.Drawing.Size(231, 42)
        Me.EENoCDButton.TabIndex = 23
        Me.EENoCDButton.Text = "Get Empire Earth 2.0 No-CD Crack"
        Me.EENoCDButton.UseVisualStyleBackColor = True
        '
        'AoCNoCDButton
        '
        Me.AoCNoCDButton.Location = New System.Drawing.Point(249, 348)
        Me.AoCNoCDButton.Name = "AoCNoCDButton"
        Me.AoCNoCDButton.Size = New System.Drawing.Size(231, 42)
        Me.AoCNoCDButton.TabIndex = 24
        Me.AoCNoCDButton.Text = "Get Art of Conquest 1.0 No-CD Crack"
        Me.AoCNoCDButton.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'RemoveAllPatchesAndModsButton
        '
        Me.RemoveAllPatchesAndModsButton.Location = New System.Drawing.Point(249, 300)
        Me.RemoveAllPatchesAndModsButton.Name = "RemoveAllPatchesAndModsButton"
        Me.RemoveAllPatchesAndModsButton.Size = New System.Drawing.Size(231, 42)
        Me.RemoveAllPatchesAndModsButton.TabIndex = 25
        Me.RemoveAllPatchesAndModsButton.Text = "Remove All Patches and Mods" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Reset to an official Sierra verison)"
        Me.RemoveAllPatchesAndModsButton.UseVisualStyleBackColor = True
        '
        'FormUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 494)
        Me.Controls.Add(Me.RemoveAllPatchesAndModsButton)
        Me.Controls.Add(Me.AoCNoCDButton)
        Me.Controls.Add(Me.EENoCDButton)
        Me.Controls.Add(Me.ForcePatchUpdateButton)
        Me.Controls.Add(Me.ReplaceWONLobbyCfgButton)
        Me.Controls.Add(Me.DefaultBannerButton)
        Me.Controls.Add(Me.AutodetectInstallDirsButton)
        Me.Controls.Add(Me.DeletePatchFromDirsButton)
        Me.Controls.Add(Me.CopyPatchFilesButton)
        Me.Controls.Add(Me.CheckForUpdatesButton)
        Me.Controls.Add(Me.UninstallMSIPatchesButton)
        Me.Controls.Add(Me.DirectConnectPatchButton)
        Me.Controls.Add(Me.InstallEEUpdatesButton)
        Me.Controls.Add(Me.AutoUpdateButton)
        Me.Controls.Add(Me.EEDirLabel)
        Me.Controls.Add(Me.AoCDirLabel)
        Me.Controls.Add(Me.AoCDirBrowseButton)
        Me.Controls.Add(Me.AoCDirTextBox)
        Me.Controls.Add(Me.EEDirBrowseButton)
        Me.Controls.Add(Me.EEDirTextBox)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Save-EE Patcher 0.9.6"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents EEDirLabel As System.Windows.Forms.Label
    Friend WithEvents AoCDirLabel As System.Windows.Forms.Label
    Friend WithEvents AoCDirBrowseButton As System.Windows.Forms.Button
    Friend WithEvents AoCDirTextBox As System.Windows.Forms.TextBox
    Friend WithEvents EEDirBrowseButton As System.Windows.Forms.Button
    Friend WithEvents EEDirTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AutoUpdateButton As System.Windows.Forms.Button
    Friend WithEvents InstallEEUpdatesButton As System.Windows.Forms.Button
    Friend WithEvents DirectConnectPatchButton As System.Windows.Forms.Button
    Friend WithEvents UninstallMSIPatchesButton As System.Windows.Forms.Button
    Friend WithEvents CheckForUpdatesButton As System.Windows.Forms.Button
    Friend WithEvents CopyPatchFilesButton As System.Windows.Forms.Button
    Friend WithEvents DeletePatchFromDirsButton As System.Windows.Forms.Button
    Friend WithEvents AutodetectInstallDirsButton As System.Windows.Forms.Button
    Friend WithEvents DefaultBannerButton As System.Windows.Forms.Button
    Friend WithEvents ReplaceWONLobbyCfgButton As System.Windows.Forms.Button
    Friend WithEvents ForcePatchUpdateButton As System.Windows.Forms.Button
    Friend WithEvents EENoCDButton As System.Windows.Forms.Button
    Friend WithEvents AoCNoCDButton As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents RemoveAllPatchesAndModsButton As System.Windows.Forms.Button

End Class
