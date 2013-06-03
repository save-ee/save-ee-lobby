<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormManualOperation
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.CancelButton0 = New System.Windows.Forms.Button
        Me.ActionButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(230, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Please enter usernames separated by [Enter]."
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 30)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(268, 202)
        Me.TextBox1.TabIndex = 1
        '
        'CancelButton0
        '
        Me.CancelButton0.Location = New System.Drawing.Point(12, 238)
        Me.CancelButton0.Name = "CancelButton0"
        Me.CancelButton0.Size = New System.Drawing.Size(75, 23)
        Me.CancelButton0.TabIndex = 2
        Me.CancelButton0.Text = "Cancel"
        Me.CancelButton0.UseVisualStyleBackColor = True
        '
        'ActionButton
        '
        Me.ActionButton.Location = New System.Drawing.Point(205, 238)
        Me.ActionButton.Name = "ActionButton"
        Me.ActionButton.Size = New System.Drawing.Size(75, 23)
        Me.ActionButton.TabIndex = 3
        Me.ActionButton.Text = "<Action>"
        Me.ActionButton.UseVisualStyleBackColor = True
        '
        'FormManualOperation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.ActionButton)
        Me.Controls.Add(Me.CancelButton0)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormManualOperation"
        Me.Text = "FormManualOperation"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents CancelButton0 As System.Windows.Forms.Button
    Friend WithEvents ActionButton As System.Windows.Forms.Button
End Class
