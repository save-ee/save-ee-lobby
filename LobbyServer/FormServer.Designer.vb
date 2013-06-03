<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormServer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormServer))
        Me.StopButton = New System.Windows.Forms.Button()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.curcon = New System.Windows.Forms.Label()
        Me.maxcon = New System.Windows.Forms.Label()
        Me.CurrentConnectionsLabel = New System.Windows.Forms.Label()
        Me.MaximumConnectionsLabel = New System.Windows.Forms.Label()
        Me.StatusLabel = New System.Windows.Forms.Label()
        Me.status = New System.Windows.Forms.Label()
        Me.StatusTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TrayNotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TrayContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowSaveEELobbyServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrayButton = New System.Windows.Forms.Button()
        Me.MuteBanRemoveTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ListConnectionsButton = New System.Windows.Forms.Button()
        Me.SessionRemovalTimer = New System.Windows.Forms.Timer(Me.components)
        Me.RecentRegistrationsTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TrayContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'StopButton
        '
        Me.StopButton.Location = New System.Drawing.Point(93, 88)
        Me.StopButton.Name = "StopButton"
        Me.StopButton.Size = New System.Drawing.Size(75, 23)
        Me.StopButton.TabIndex = 11
        Me.StopButton.Text = "Stop"
        Me.StopButton.UseVisualStyleBackColor = True
        '
        'StartButton
        '
        Me.StartButton.Location = New System.Drawing.Point(12, 88)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(75, 23)
        Me.StartButton.TabIndex = 10
        Me.StartButton.Text = "Start"
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'curcon
        '
        Me.curcon.AutoSize = True
        Me.curcon.Location = New System.Drawing.Point(135, 27)
        Me.curcon.Name = "curcon"
        Me.curcon.Size = New System.Drawing.Size(55, 13)
        Me.curcon.TabIndex = 9
        Me.curcon.Text = "<curcon>"
        '
        'maxcon
        '
        Me.maxcon.AutoSize = True
        Me.maxcon.Location = New System.Drawing.Point(135, 9)
        Me.maxcon.Name = "maxcon"
        Me.maxcon.Size = New System.Drawing.Size(60, 13)
        Me.maxcon.TabIndex = 8
        Me.maxcon.Text = "<maxcon>"
        '
        'CurrentConnectionsLabel
        '
        Me.CurrentConnectionsLabel.AutoSize = True
        Me.CurrentConnectionsLabel.Location = New System.Drawing.Point(19, 27)
        Me.CurrentConnectionsLabel.Name = "CurrentConnectionsLabel"
        Me.CurrentConnectionsLabel.Size = New System.Drawing.Size(110, 13)
        Me.CurrentConnectionsLabel.TabIndex = 7
        Me.CurrentConnectionsLabel.Text = "Current Connections:"
        '
        'MaximumConnectionsLabel
        '
        Me.MaximumConnectionsLabel.AutoSize = True
        Me.MaximumConnectionsLabel.Location = New System.Drawing.Point(12, 9)
        Me.MaximumConnectionsLabel.Name = "MaximumConnectionsLabel"
        Me.MaximumConnectionsLabel.Size = New System.Drawing.Size(117, 13)
        Me.MaximumConnectionsLabel.TabIndex = 6
        Me.MaximumConnectionsLabel.Text = "Maximum Connections:"
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoSize = True
        Me.StatusLabel.Location = New System.Drawing.Point(87, 45)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(42, 13)
        Me.StatusLabel.TabIndex = 12
        Me.StatusLabel.Text = "Status:"
        '
        'status
        '
        Me.status.AutoSize = True
        Me.status.Location = New System.Drawing.Point(135, 45)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(53, 13)
        Me.status.TabIndex = 13
        Me.status.Text = "<status>"
        '
        'StatusTimer
        '
        Me.StatusTimer.Enabled = True
        Me.StatusTimer.Interval = 1000
        '
        'TrayNotifyIcon
        '
        Me.TrayNotifyIcon.ContextMenuStrip = Me.TrayContextMenuStrip
        Me.TrayNotifyIcon.Icon = CType(resources.GetObject("TrayNotifyIcon.Icon"), System.Drawing.Icon)
        Me.TrayNotifyIcon.Text = "Save-EE Lobby Server"
        '
        'TrayContextMenuStrip
        '
        Me.TrayContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowSaveEELobbyServerToolStripMenuItem, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.TrayContextMenuStrip.Name = "TrayContextMenuStrip"
        Me.TrayContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.TrayContextMenuStrip.ShowImageMargin = False
        Me.TrayContextMenuStrip.Size = New System.Drawing.Size(206, 54)
        '
        'ShowSaveEELobbyServerToolStripMenuItem
        '
        Me.ShowSaveEELobbyServerToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShowSaveEELobbyServerToolStripMenuItem.Name = "ShowSaveEELobbyServerToolStripMenuItem"
        Me.ShowSaveEELobbyServerToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.ShowSaveEELobbyServerToolStripMenuItem.Text = "Show Save-EE Lobby Server"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(202, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'TrayButton
        '
        Me.TrayButton.Location = New System.Drawing.Point(205, 88)
        Me.TrayButton.Name = "TrayButton"
        Me.TrayButton.Size = New System.Drawing.Size(75, 23)
        Me.TrayButton.TabIndex = 14
        Me.TrayButton.Text = "System Tray"
        Me.TrayButton.UseVisualStyleBackColor = True
        '
        'MuteBanRemoveTimer
        '
        Me.MuteBanRemoveTimer.Enabled = True
        Me.MuteBanRemoveTimer.Interval = 1000
        '
        'ListConnectionsButton
        '
        Me.ListConnectionsButton.Location = New System.Drawing.Point(205, 12)
        Me.ListConnectionsButton.Name = "ListConnectionsButton"
        Me.ListConnectionsButton.Size = New System.Drawing.Size(75, 46)
        Me.ListConnectionsButton.TabIndex = 15
        Me.ListConnectionsButton.Text = "List Connections"
        Me.ListConnectionsButton.UseVisualStyleBackColor = True
        '
        'SessionRemovalTimer
        '
        Me.SessionRemovalTimer.Enabled = True
        Me.SessionRemovalTimer.Interval = 300000
        '
        'RecentRegistrationsTimer
        '
        Me.RecentRegistrationsTimer.Enabled = True
        Me.RecentRegistrationsTimer.Interval = 60000
        '
        'FormServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 123)
        Me.Controls.Add(Me.ListConnectionsButton)
        Me.Controls.Add(Me.TrayButton)
        Me.Controls.Add(Me.status)
        Me.Controls.Add(Me.StatusLabel)
        Me.Controls.Add(Me.StopButton)
        Me.Controls.Add(Me.StartButton)
        Me.Controls.Add(Me.curcon)
        Me.Controls.Add(Me.maxcon)
        Me.Controls.Add(Me.CurrentConnectionsLabel)
        Me.Controls.Add(Me.MaximumConnectionsLabel)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormServer"
        Me.Text = "Save-EE Lobby Server"
        Me.TrayContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents StopButton As System.Windows.Forms.Button
    Private WithEvents StartButton As System.Windows.Forms.Button
    Private WithEvents curcon As System.Windows.Forms.Label
    Private WithEvents maxcon As System.Windows.Forms.Label
    Private WithEvents CurrentConnectionsLabel As System.Windows.Forms.Label
    Private WithEvents MaximumConnectionsLabel As System.Windows.Forms.Label
    Friend WithEvents StatusLabel As System.Windows.Forms.Label
    Friend WithEvents status As System.Windows.Forms.Label
    Friend WithEvents StatusTimer As System.Windows.Forms.Timer
    Friend WithEvents TrayNotifyIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents TrayButton As System.Windows.Forms.Button
    Friend WithEvents TrayContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowSaveEELobbyServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MuteBanRemoveTimer As System.Windows.Forms.Timer
    Friend WithEvents ListConnectionsButton As System.Windows.Forms.Button
    Friend WithEvents SessionRemovalTimer As System.Windows.Forms.Timer
    Friend WithEvents RecentRegistrationsTimer As System.Windows.Forms.Timer

End Class
