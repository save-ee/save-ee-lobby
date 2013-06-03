Public Class FormConnections
    Private ConnectionsListViewSorter As New ListViewHelpers.ListViewSorter(ConnectionsListView)

    Private Sub FormConnections_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConnectionsListViewSorter.lv = ConnectionsListView

        SyncLock Globals.ServerEngine.Connections_Lock
            For Each s As Session In Globals.ServerEngine.Connections.Values
                Dim lvi As New ListViewItem
                lvi.Name = s.Guid
                lvi.Text = s.Guid
                lvi.SubItems.Add(s.Username)
                lvi.SubItems.Add(s.PublicIP)
                lvi.SubItems.Add(s.StartTime)
                lvi.SubItems.Add(s.LoginTime)
                ConnectionsListView.Items.Add(lvi)
            Next
        End SyncLock
    End Sub

    Private Sub ConnectionsListView_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles ConnectionsListView.ColumnClick
        ConnectionsListViewSorter.SortByColumn(e.Column)
    End Sub
End Class