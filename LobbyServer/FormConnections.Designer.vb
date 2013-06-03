<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormConnections
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormConnections))
        Me.ConnectionsListView = New System.Windows.Forms.ListView()
        Me.GUID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.USERNAME = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IP = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.STARTTIME = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LOGINTIME = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'ConnectionsListView
        '
        Me.ConnectionsListView.AllowColumnReorder = True
        Me.ConnectionsListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConnectionsListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.GUID, Me.USERNAME, Me.IP, Me.STARTTIME, Me.LOGINTIME})
        Me.ConnectionsListView.FullRowSelect = True
        Me.ConnectionsListView.Location = New System.Drawing.Point(12, 12)
        Me.ConnectionsListView.Name = "ConnectionsListView"
        Me.ConnectionsListView.Size = New System.Drawing.Size(710, 410)
        Me.ConnectionsListView.TabIndex = 0
        Me.ConnectionsListView.UseCompatibleStateImageBehavior = False
        Me.ConnectionsListView.View = System.Windows.Forms.View.Details
        '
        'GUID
        '
        Me.GUID.Text = "GUID"
        Me.GUID.Width = 97
        '
        'USERNAME
        '
        Me.USERNAME.Text = "Username"
        Me.USERNAME.Width = 229
        '
        'IP
        '
        Me.IP.Text = "IP"
        Me.IP.Width = 125
        '
        'STARTTIME
        '
        Me.STARTTIME.Text = "Start Time"
        Me.STARTTIME.Width = 126
        '
        'LOGINTIME
        '
        Me.LOGINTIME.Text = "Login Time"
        Me.LOGINTIME.Width = 129
        '
        'FormConnections
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 434)
        Me.Controls.Add(Me.ConnectionsListView)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormConnections"
        Me.Text = "Current Connections"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ConnectionsListView As System.Windows.Forms.ListView
    Friend WithEvents GUID As System.Windows.Forms.ColumnHeader
    Friend WithEvents USERNAME As System.Windows.Forms.ColumnHeader
    Friend WithEvents IP As System.Windows.Forms.ColumnHeader
    Friend WithEvents STARTTIME As System.Windows.Forms.ColumnHeader
    Friend WithEvents LOGINTIME As System.Windows.Forms.ColumnHeader
End Class
