<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormVote
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormVote))
        Me.SubmitVoteButton = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.RadioButton1A = New System.Windows.Forms.RadioButton
        Me.RadioButton1C = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RadioButton1B = New System.Windows.Forms.RadioButton
        Me.RadioButton1D = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.AlreadyVotedButton = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.RadioButton2B = New System.Windows.Forms.RadioButton
        Me.RadioButton2D = New System.Windows.Forms.RadioButton
        Me.RadioButton2A = New System.Windows.Forms.RadioButton
        Me.RadioButton2C = New System.Windows.Forms.RadioButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SubmitVoteButton
        '
        Me.SubmitVoteButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SubmitVoteButton.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubmitVoteButton.Location = New System.Drawing.Point(12, 476)
        Me.SubmitVoteButton.Name = "SubmitVoteButton"
        Me.SubmitVoteButton.Size = New System.Drawing.Size(730, 47)
        Me.SubmitVoteButton.TabIndex = 0
        Me.SubmitVoteButton.Text = "Submit Vote"
        Me.SubmitVoteButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(730, 42)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Please take a moment to answer a few questions regarding the anti-cheat patches."
        '
        'RadioButton1A
        '
        Me.RadioButton1A.AutoSize = True
        Me.RadioButton1A.Location = New System.Drawing.Point(22, 20)
        Me.RadioButton1A.Name = "RadioButton1A"
        Me.RadioButton1A.Size = New System.Drawing.Size(220, 30)
        Me.RadioButton1A.TabIndex = 2
        Me.RadioButton1A.TabStop = True
        Me.RadioButton1A.Text = "Make no tribute screen patch permanent" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Prevent all resource glitching)"
        Me.RadioButton1A.UseVisualStyleBackColor = True
        '
        'RadioButton1C
        '
        Me.RadioButton1C.AutoSize = True
        Me.RadioButton1C.Location = New System.Drawing.Point(22, 92)
        Me.RadioButton1C.Name = "RadioButton1C"
        Me.RadioButton1C.Size = New System.Drawing.Size(224, 30)
        Me.RadioButton1C.TabIndex = 6
        Me.RadioButton1C.TabStop = True
        Me.RadioButton1C.Text = "Revert to the last patch" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Only makes resource glitch harder to do)"
        Me.RadioButton1C.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RadioButton1B)
        Me.GroupBox1.Controls.Add(Me.RadioButton1D)
        Me.GroupBox1.Controls.Add(Me.RadioButton1A)
        Me.GroupBox1.Controls.Add(Me.RadioButton1C)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 307)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(450, 163)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "1.  What is your current stance on the test patch?"
        '
        'RadioButton1B
        '
        Me.RadioButton1B.AutoSize = True
        Me.RadioButton1B.Location = New System.Drawing.Point(22, 56)
        Me.RadioButton1B.Name = "RadioButton1B"
        Me.RadioButton1B.Size = New System.Drawing.Size(409, 30)
        Me.RadioButton1B.TabIndex = 8
        Me.RadioButton1B.TabStop = True
        Me.RadioButton1B.Text = "I'm willing to give up tributes, but I still want to be able to see the tribute s" & _
            "creen" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Does not prevent glitching buildings, may result in another test)"
        Me.RadioButton1B.UseVisualStyleBackColor = True
        '
        'RadioButton1D
        '
        Me.RadioButton1D.AutoSize = True
        Me.RadioButton1D.Location = New System.Drawing.Point(22, 128)
        Me.RadioButton1D.Name = "RadioButton1D"
        Me.RadioButton1D.Size = New System.Drawing.Size(286, 30)
        Me.RadioButton1D.TabIndex = 7
        Me.RadioButton1D.TabStop = True
        Me.RadioButton1D.Text = "Revert to vanilla, no patches at all, make things simple" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Like before Save-EE Lo" & _
            "bby, no anti-cheat measures)"
        Me.RadioButton1D.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(730, 81)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Location = New System.Drawing.Point(12, 200)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(730, 82)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = resources.GetString("Label3.Text")
        '
        'AlreadyVotedButton
        '
        Me.AlreadyVotedButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AlreadyVotedButton.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AlreadyVotedButton.Location = New System.Drawing.Point(12, 148)
        Me.AlreadyVotedButton.Name = "AlreadyVotedButton"
        Me.AlreadyVotedButton.Size = New System.Drawing.Size(730, 47)
        Me.AlreadyVotedButton.TabIndex = 10
        Me.AlreadyVotedButton.Text = "Click Here If You Have Already Voted"
        Me.AlreadyVotedButton.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(730, 22)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Game titles in the lobby advertising your opinion will be removed."
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.RadioButton2B)
        Me.GroupBox2.Controls.Add(Me.RadioButton2D)
        Me.GroupBox2.Controls.Add(Me.RadioButton2A)
        Me.GroupBox2.Controls.Add(Me.RadioButton2C)
        Me.GroupBox2.Location = New System.Drawing.Point(468, 307)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(274, 163)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "2.  What is your second preference?"
        '
        'RadioButton2B
        '
        Me.RadioButton2B.AutoSize = True
        Me.RadioButton2B.Location = New System.Drawing.Point(22, 56)
        Me.RadioButton2B.Name = "RadioButton2B"
        Me.RadioButton2B.Size = New System.Drawing.Size(188, 17)
        Me.RadioButton2B.TabIndex = 8
        Me.RadioButton2B.TabStop = True
        Me.RadioButton2B.Text = "Remove tributes, keep the screen"
        Me.RadioButton2B.UseVisualStyleBackColor = True
        '
        'RadioButton2D
        '
        Me.RadioButton2D.AutoSize = True
        Me.RadioButton2D.Location = New System.Drawing.Point(22, 128)
        Me.RadioButton2D.Name = "RadioButton2D"
        Me.RadioButton2D.Size = New System.Drawing.Size(104, 17)
        Me.RadioButton2D.TabIndex = 7
        Me.RadioButton2D.TabStop = True
        Me.RadioButton2D.Text = "Revert to vanilla"
        Me.RadioButton2D.UseVisualStyleBackColor = True
        '
        'RadioButton2A
        '
        Me.RadioButton2A.AutoSize = True
        Me.RadioButton2A.Location = New System.Drawing.Point(22, 20)
        Me.RadioButton2A.Name = "RadioButton2A"
        Me.RadioButton2A.Size = New System.Drawing.Size(220, 17)
        Me.RadioButton2A.TabIndex = 2
        Me.RadioButton2A.TabStop = True
        Me.RadioButton2A.Text = "Make no tribute screen patch permanent"
        Me.RadioButton2A.UseVisualStyleBackColor = True
        '
        'RadioButton2C
        '
        Me.RadioButton2C.AutoSize = True
        Me.RadioButton2C.Location = New System.Drawing.Point(22, 92)
        Me.RadioButton2C.Name = "RadioButton2C"
        Me.RadioButton2C.Size = New System.Drawing.Size(140, 17)
        Me.RadioButton2C.TabIndex = 6
        Me.RadioButton2C.TabStop = True
        Me.RadioButton2C.Text = "Revert to the last patch"
        Me.RadioButton2C.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 269)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(730, 22)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "This is the final vote that will take place for this patch release."
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'FormVote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(754, 535)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.AlreadyVotedButton)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SubmitVoteButton)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormVote"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Anti-Cheat Poll #3"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SubmitVoteButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RadioButton1A As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1C As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AlreadyVotedButton As System.Windows.Forms.Button
    Friend WithEvents RadioButton1D As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1B As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton2B As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2D As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2A As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2C As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
