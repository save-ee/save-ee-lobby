<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormKeyList
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
        Me.KeyListView = New System.Windows.Forms.ListView()
        Me.KeyColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ValueColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ByColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.StartTimeColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.EndTimeColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ReasonColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CommentsColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TopLabel = New System.Windows.Forms.Label()
        Me.CancelButton0 = New System.Windows.Forms.Button()
        Me.ActionButton = New System.Windows.Forms.Button()
        Me.CommentsLabel = New System.Windows.Forms.Label()
        Me.CommentsTextBox = New System.Windows.Forms.TextBox()
        Me.RemoveDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.RemoveTimeLabel = New System.Windows.Forms.Label()
        Me.ManualRemoveCheckBox = New System.Windows.Forms.CheckBox()
        Me.DurationLabel = New System.Windows.Forms.Label()
        Me.DurationTextBox = New System.Windows.Forms.TextBox()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.ReasonLabel = New System.Windows.Forms.Label()
        Me.ReasonTextBox = New System.Windows.Forms.TextBox()
        Me.TimeFormatLabel = New System.Windows.Forms.Label()
        Me.DurationRefreshTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TimezoneNotesLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'KeyListView
        '
        Me.KeyListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.KeyListView.BackgroundImageTiled = True
        Me.KeyListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.KeyColumnHeader, Me.ValueColumnHeader, Me.ByColumnHeader, Me.StartTimeColumnHeader, Me.EndTimeColumnHeader, Me.ReasonColumnHeader, Me.CommentsColumnHeader})
        Me.KeyListView.ForeColor = System.Drawing.Color.Black
        Me.KeyListView.FullRowSelect = True
        Me.KeyListView.Location = New System.Drawing.Point(12, 30)
        Me.KeyListView.Name = "KeyListView"
        Me.KeyListView.Size = New System.Drawing.Size(608, 171)
        Me.KeyListView.TabIndex = 1
        Me.KeyListView.UseCompatibleStateImageBehavior = False
        Me.KeyListView.View = System.Windows.Forms.View.Details
        '
        'KeyColumnHeader
        '
        Me.KeyColumnHeader.Text = "Key"
        Me.KeyColumnHeader.Width = 100
        '
        'ValueColumnHeader
        '
        Me.ValueColumnHeader.Text = "Value"
        Me.ValueColumnHeader.Width = 100
        '
        'ByColumnHeader
        '
        Me.ByColumnHeader.Text = "<Action> By"
        Me.ByColumnHeader.Width = 100
        '
        'StartTimeColumnHeader
        '
        Me.StartTimeColumnHeader.Text = "Start Time"
        Me.StartTimeColumnHeader.Width = 65
        '
        'EndTimeColumnHeader
        '
        Me.EndTimeColumnHeader.Text = "End Time"
        Me.EndTimeColumnHeader.Width = 65
        '
        'ReasonColumnHeader
        '
        Me.ReasonColumnHeader.Text = "Reason"
        Me.ReasonColumnHeader.Width = 100
        '
        'CommentsColumnHeader
        '
        Me.CommentsColumnHeader.Text = "Comments"
        Me.CommentsColumnHeader.Width = 100
        '
        'TopLabel
        '
        Me.TopLabel.AutoSize = True
        Me.TopLabel.Location = New System.Drawing.Point(12, 9)
        Me.TopLabel.Name = "TopLabel"
        Me.TopLabel.Size = New System.Drawing.Size(203, 13)
        Me.TopLabel.TabIndex = 2
        Me.TopLabel.Text = "Please select hardware IDs to <Action>."
        '
        'CancelButton0
        '
        Me.CancelButton0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelButton0.Location = New System.Drawing.Point(383, 316)
        Me.CancelButton0.Name = "CancelButton0"
        Me.CancelButton0.Size = New System.Drawing.Size(75, 23)
        Me.CancelButton0.TabIndex = 3
        Me.CancelButton0.Text = "Cancel"
        Me.CancelButton0.UseVisualStyleBackColor = True
        '
        'ActionButton
        '
        Me.ActionButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ActionButton.Location = New System.Drawing.Point(545, 316)
        Me.ActionButton.Name = "ActionButton"
        Me.ActionButton.Size = New System.Drawing.Size(75, 23)
        Me.ActionButton.TabIndex = 4
        Me.ActionButton.Text = "<Action>"
        Me.ActionButton.UseVisualStyleBackColor = True
        '
        'CommentsLabel
        '
        Me.CommentsLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CommentsLabel.Location = New System.Drawing.Point(3, 237)
        Me.CommentsLabel.Name = "CommentsLabel"
        Me.CommentsLabel.Size = New System.Drawing.Size(82, 13)
        Me.CommentsLabel.TabIndex = 5
        Me.CommentsLabel.Text = "Comments:"
        Me.CommentsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'CommentsTextBox
        '
        Me.CommentsTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CommentsTextBox.Location = New System.Drawing.Point(91, 234)
        Me.CommentsTextBox.Name = "CommentsTextBox"
        Me.CommentsTextBox.Size = New System.Drawing.Size(529, 21)
        Me.CommentsTextBox.TabIndex = 6
        '
        'RemoveDateTimePicker
        '
        Me.RemoveDateTimePicker.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RemoveDateTimePicker.CustomFormat = "yyyy.MM.dd.HH:mm:ss"
        Me.RemoveDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.RemoveDateTimePicker.Location = New System.Drawing.Point(91, 261)
        Me.RemoveDateTimePicker.Name = "RemoveDateTimePicker"
        Me.RemoveDateTimePicker.Size = New System.Drawing.Size(141, 21)
        Me.RemoveDateTimePicker.TabIndex = 7
        Me.RemoveDateTimePicker.Value = New Date(9000, 1, 1, 0, 0, 0, 0)
        '
        'RemoveTimeLabel
        '
        Me.RemoveTimeLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RemoveTimeLabel.Location = New System.Drawing.Point(5, 264)
        Me.RemoveTimeLabel.Name = "RemoveTimeLabel"
        Me.RemoveTimeLabel.Size = New System.Drawing.Size(80, 13)
        Me.RemoveTimeLabel.TabIndex = 8
        Me.RemoveTimeLabel.Text = "Remove Time:"
        Me.RemoveTimeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ManualRemoveCheckBox
        '
        Me.ManualRemoveCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ManualRemoveCheckBox.AutoSize = True
        Me.ManualRemoveCheckBox.Location = New System.Drawing.Point(238, 263)
        Me.ManualRemoveCheckBox.Name = "ManualRemoveCheckBox"
        Me.ManualRemoveCheckBox.Size = New System.Drawing.Size(154, 17)
        Me.ManualRemoveCheckBox.TabIndex = 9
        Me.ManualRemoveCheckBox.Text = "Must be manually removed"
        Me.ManualRemoveCheckBox.UseVisualStyleBackColor = True
        '
        'DurationLabel
        '
        Me.DurationLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DurationLabel.Location = New System.Drawing.Point(3, 295)
        Me.DurationLabel.Name = "DurationLabel"
        Me.DurationLabel.Size = New System.Drawing.Size(82, 13)
        Me.DurationLabel.TabIndex = 10
        Me.DurationLabel.Text = "Duration:"
        Me.DurationLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DurationTextBox
        '
        Me.DurationTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DurationTextBox.Enabled = False
        Me.DurationTextBox.Location = New System.Drawing.Point(91, 292)
        Me.DurationTextBox.Name = "DurationTextBox"
        Me.DurationTextBox.Size = New System.Drawing.Size(141, 21)
        Me.DurationTextBox.TabIndex = 11
        '
        'SaveButton
        '
        Me.SaveButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveButton.Location = New System.Drawing.Point(464, 316)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveButton.TabIndex = 12
        Me.SaveButton.Text = "Save"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'ReasonLabel
        '
        Me.ReasonLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ReasonLabel.Location = New System.Drawing.Point(3, 210)
        Me.ReasonLabel.Name = "ReasonLabel"
        Me.ReasonLabel.Size = New System.Drawing.Size(82, 13)
        Me.ReasonLabel.TabIndex = 13
        Me.ReasonLabel.Text = "Reason:"
        Me.ReasonLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ReasonTextBox
        '
        Me.ReasonTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReasonTextBox.Location = New System.Drawing.Point(91, 207)
        Me.ReasonTextBox.Name = "ReasonTextBox"
        Me.ReasonTextBox.Size = New System.Drawing.Size(529, 21)
        Me.ReasonTextBox.TabIndex = 14
        '
        'TimeFormatLabel
        '
        Me.TimeFormatLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TimeFormatLabel.AutoSize = True
        Me.TimeFormatLabel.Location = New System.Drawing.Point(238, 295)
        Me.TimeFormatLabel.Name = "TimeFormatLabel"
        Me.TimeFormatLabel.Size = New System.Drawing.Size(90, 13)
        Me.TimeFormatLabel.TabIndex = 15
        Me.TimeFormatLabel.Text = "[days].HH:mm:ss"
        '
        'DurationRefreshTimer
        '
        Me.DurationRefreshTimer.Enabled = True
        '
        'TimezoneNotesLabel
        '
        Me.TimezoneNotesLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TimezoneNotesLabel.Location = New System.Drawing.Point(12, 316)
        Me.TimezoneNotesLabel.Name = "TimezoneNotesLabel"
        Me.TimezoneNotesLabel.Size = New System.Drawing.Size(365, 26)
        Me.TimezoneNotesLabel.TabIndex = 16
        Me.TimezoneNotesLabel.Text = "All times, both in the list and the selected remove time, are in the UTC timezone" & _
            "."
        '
        'FormKeyList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 351)
        Me.Controls.Add(Me.TimezoneNotesLabel)
        Me.Controls.Add(Me.TimeFormatLabel)
        Me.Controls.Add(Me.ReasonTextBox)
        Me.Controls.Add(Me.ReasonLabel)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.DurationTextBox)
        Me.Controls.Add(Me.DurationLabel)
        Me.Controls.Add(Me.ManualRemoveCheckBox)
        Me.Controls.Add(Me.RemoveTimeLabel)
        Me.Controls.Add(Me.RemoveDateTimePicker)
        Me.Controls.Add(Me.CommentsTextBox)
        Me.Controls.Add(Me.CommentsLabel)
        Me.Controls.Add(Me.ActionButton)
        Me.Controls.Add(Me.CancelButton0)
        Me.Controls.Add(Me.TopLabel)
        Me.Controls.Add(Me.KeyListView)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormKeyList"
        Me.Text = "Key Management"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents KeyListView As System.Windows.Forms.ListView
    Friend WithEvents KeyColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents ValueColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents TopLabel As System.Windows.Forms.Label
    Friend WithEvents CancelButton0 As System.Windows.Forms.Button
    Friend WithEvents ActionButton As System.Windows.Forms.Button
    Friend WithEvents CommentsLabel As System.Windows.Forms.Label
    Friend WithEvents CommentsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RemoveDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents RemoveTimeLabel As System.Windows.Forms.Label
    Friend WithEvents ManualRemoveCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents DurationLabel As System.Windows.Forms.Label
    Friend WithEvents DurationTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents ByColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents StartTimeColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents EndTimeColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents ReasonColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents CommentsColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents ReasonLabel As System.Windows.Forms.Label
    Friend WithEvents ReasonTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TimeFormatLabel As System.Windows.Forms.Label
    Friend WithEvents DurationRefreshTimer As System.Windows.Forms.Timer
    Friend WithEvents TimezoneNotesLabel As System.Windows.Forms.Label
End Class
