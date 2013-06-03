<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UpdateDialog))
        Me.OK_Button = New System.Windows.Forms.Button
        Me.DownloadProgressLabel = New System.Windows.Forms.Label
        Me.DownloadProgressBar = New System.Windows.Forms.ProgressBar
        Me.InfoTextBox = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(414, 289)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 22
        Me.OK_Button.Text = "OK"
        '
        'DownloadProgressLabel
        '
        Me.DownloadProgressLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DownloadProgressLabel.Location = New System.Drawing.Point(12, 260)
        Me.DownloadProgressLabel.Name = "DownloadProgressLabel"
        Me.DownloadProgressLabel.Size = New System.Drawing.Size(470, 23)
        Me.DownloadProgressLabel.TabIndex = 25
        Me.DownloadProgressLabel.Text = "(0 / 0)"
        Me.DownloadProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DownloadProgressBar
        '
        Me.DownloadProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DownloadProgressBar.Location = New System.Drawing.Point(12, 234)
        Me.DownloadProgressBar.Name = "DownloadProgressBar"
        Me.DownloadProgressBar.Size = New System.Drawing.Size(470, 23)
        Me.DownloadProgressBar.TabIndex = 24
        '
        'InfoTextBox
        '
        Me.InfoTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InfoTextBox.BackColor = System.Drawing.SystemColors.Window
        Me.InfoTextBox.Location = New System.Drawing.Point(12, 12)
        Me.InfoTextBox.Multiline = True
        Me.InfoTextBox.Name = "InfoTextBox"
        Me.InfoTextBox.ReadOnly = True
        Me.InfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.InfoTextBox.Size = New System.Drawing.Size(470, 216)
        Me.InfoTextBox.TabIndex = 23
        '
        'UpdateDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 325)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.DownloadProgressLabel)
        Me.Controls.Add(Me.DownloadProgressBar)
        Me.Controls.Add(Me.InfoTextBox)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UpdateDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents DownloadProgressLabel As System.Windows.Forms.Label
    Friend WithEvents DownloadProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents InfoTextBox As System.Windows.Forms.TextBox
End Class
