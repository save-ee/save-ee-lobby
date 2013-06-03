<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLogin))
        Me.LoginButton = New System.Windows.Forms.Button
        Me.CreateNewUserButton = New System.Windows.Forms.Button
        Me.PasswordTextBox = New System.Windows.Forms.TextBox
        Me.LoginPictureBox = New System.Windows.Forms.PictureBox
        Me.UsernameComboBox = New System.Windows.Forms.ComboBox
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.UsernameLabel = New System.Windows.Forms.Label
        Me.LoginGroupBox = New System.Windows.Forms.GroupBox
        Me.StatusLabel = New System.Windows.Forms.Label
        CType(Me.LoginPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LoginGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'LoginButton
        '
        Me.LoginButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LoginButton.Location = New System.Drawing.Point(163, 66)
        Me.LoginButton.Name = "LoginButton"
        Me.LoginButton.Size = New System.Drawing.Size(151, 32)
        Me.LoginButton.TabIndex = 5
        Me.LoginButton.Text = "Login"
        Me.LoginButton.UseVisualStyleBackColor = True
        '
        'CreateNewUserButton
        '
        Me.CreateNewUserButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CreateNewUserButton.Location = New System.Drawing.Point(6, 66)
        Me.CreateNewUserButton.Name = "CreateNewUserButton"
        Me.CreateNewUserButton.Size = New System.Drawing.Size(151, 32)
        Me.CreateNewUserButton.TabIndex = 4
        Me.CreateNewUserButton.Text = "Create New User"
        Me.CreateNewUserButton.UseVisualStyleBackColor = True
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PasswordTextBox.Location = New System.Drawing.Point(120, 40)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.Size = New System.Drawing.Size(194, 21)
        Me.PasswordTextBox.TabIndex = 3
        Me.PasswordTextBox.UseSystemPasswordChar = True
        '
        'LoginPictureBox
        '
        Me.LoginPictureBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LoginPictureBox.Image = Global.LobbyClient.My.Resources.Resources.login
        Me.LoginPictureBox.Location = New System.Drawing.Point(1, 2)
        Me.LoginPictureBox.Name = "LoginPictureBox"
        Me.LoginPictureBox.Size = New System.Drawing.Size(342, 60)
        Me.LoginPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LoginPictureBox.TabIndex = 3
        Me.LoginPictureBox.TabStop = False
        '
        'UsernameComboBox
        '
        Me.UsernameComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsernameComboBox.FormattingEnabled = True
        Me.UsernameComboBox.Location = New System.Drawing.Point(120, 13)
        Me.UsernameComboBox.Name = "UsernameComboBox"
        Me.UsernameComboBox.Size = New System.Drawing.Size(194, 21)
        Me.UsernameComboBox.TabIndex = 2
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PasswordLabel.Location = New System.Drawing.Point(8, 43)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(106, 13)
        Me.PasswordLabel.TabIndex = 1
        Me.PasswordLabel.Text = "Password:"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsernameLabel.Location = New System.Drawing.Point(6, 16)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(108, 13)
        Me.UsernameLabel.TabIndex = 0
        Me.UsernameLabel.Text = "Username:"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LoginGroupBox
        '
        Me.LoginGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LoginGroupBox.Controls.Add(Me.LoginButton)
        Me.LoginGroupBox.Controls.Add(Me.CreateNewUserButton)
        Me.LoginGroupBox.Controls.Add(Me.PasswordTextBox)
        Me.LoginGroupBox.Controls.Add(Me.UsernameComboBox)
        Me.LoginGroupBox.Controls.Add(Me.PasswordLabel)
        Me.LoginGroupBox.Controls.Add(Me.UsernameLabel)
        Me.LoginGroupBox.Enabled = False
        Me.LoginGroupBox.Location = New System.Drawing.Point(12, 97)
        Me.LoginGroupBox.Name = "LoginGroupBox"
        Me.LoginGroupBox.Size = New System.Drawing.Size(320, 104)
        Me.LoginGroupBox.TabIndex = 5
        Me.LoginGroupBox.TabStop = False
        '
        'StatusLabel
        '
        Me.StatusLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatusLabel.Location = New System.Drawing.Point(1, 68)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(342, 29)
        Me.StatusLabel.TabIndex = 4
        Me.StatusLabel.Text = "Obtaining server IP, please wait..."
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FormLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(344, 213)
        Me.Controls.Add(Me.LoginPictureBox)
        Me.Controls.Add(Me.LoginGroupBox)
        Me.Controls.Add(Me.StatusLabel)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormLogin"
        CType(Me.LoginPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LoginGroupBox.ResumeLayout(False)
        Me.LoginGroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents LoginButton As System.Windows.Forms.Button
    Private WithEvents CreateNewUserButton As System.Windows.Forms.Button
    Private WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Private WithEvents LoginPictureBox As System.Windows.Forms.PictureBox
    Private WithEvents UsernameComboBox As System.Windows.Forms.ComboBox
    Private WithEvents PasswordLabel As System.Windows.Forms.Label
    Private WithEvents UsernameLabel As System.Windows.Forms.Label
    Private WithEvents LoginGroupBox As System.Windows.Forms.GroupBox
    Private WithEvents StatusLabel As System.Windows.Forms.Label
End Class
