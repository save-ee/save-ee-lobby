Public Class ListViewHelpers
    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Long
    Private Const LVM_FIRST = &H1000
    Public Shared Function CheckedItemsToString(ByVal lv As ListView, Optional ByVal Delemiter As String = ",") As String
        Dim str As String = ""
        For Each lvi As ListViewItem In lv.CheckedItems
            str = str & Delemiter & lvi.Name
        Next
        If str = "" Then Return "" : Exit Function
        str = str.Substring(Len(Delemiter))
        Return str
    End Function
    Public Class ListViewComparer
        Implements IComparer

        Private m_ColumnNumber As Integer
        Private m_SortOrder As SortOrder

        Public Sub New(ByVal column_number As Integer, ByVal _
            sort_order As SortOrder)
            m_ColumnNumber = column_number
            m_SortOrder = sort_order
        End Sub
        ' Compare the items in the appropriate column
        ' for objects x and y.
        Public Function Compare(ByVal x As Object, ByVal y As _
            Object) As Integer Implements _
            System.Collections.IComparer.Compare
            Dim item_x As ListViewItem = DirectCast(x,  _
                ListViewItem)
            Dim item_y As ListViewItem = DirectCast(y,  _
                ListViewItem)

            ' Get the sub-item values.
            Dim string_x As String
            If item_x.SubItems.Count <= m_ColumnNumber Then
                string_x = ""
            Else
                string_x = item_x.SubItems(m_ColumnNumber).Text
            End If

            Dim string_y As String
            If item_y.SubItems.Count <= m_ColumnNumber Then
                string_y = ""
            Else
                string_y = item_y.SubItems(m_ColumnNumber).Text
            End If

            ' Compare them.
            If m_SortOrder = SortOrder.Ascending Then
                If IsNumeric(string_x) And IsNumeric(string_y) _
                    Then
                    Return Val(string_x).CompareTo(Val(string_y))
                Else
                    Return String.Compare(string_x, string_y)
                End If
            Else
                If IsNumeric(string_x) And IsNumeric(string_y) _
                    Then
                    Return Val(string_y).CompareTo(Val(string_x))
                Else
                    Return String.Compare(string_y, string_x)
                End If
            End If
        End Function
    End Class
    Public Class ListViewSorter
        Public lv As ListView
        Public lastcol As ColumnHeader
        Public Sub New(ByVal lv As ListView)
            Me.lv = lv
        End Sub
        Public Sub SortByColumn(ByVal i As Integer)
            ' The column currently used for sorting.


            ' Get the new sorting column.
            Dim new_sorting_column As ColumnHeader = _
                lv.Columns(i)

            ' Figure out the new sorting order.
            Dim sort_order As System.Windows.Forms.SortOrder
            If lastcol Is Nothing Then
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            Else
                ' See if this is the same column.
                If new_sorting_column.Equals(lastcol) Then
                    ' Same column. Switch the sort order.
                    If lastcol.Text.StartsWith("> ") Then
                        sort_order = SortOrder.Descending
                    Else
                        sort_order = SortOrder.Ascending
                    End If
                Else
                    ' New column. Sort ascending.
                    sort_order = SortOrder.Ascending
                End If

                ' Remove the old sort indicator.
                lastcol.Text = _
                    lastcol.Text.Substring(2)
            End If

            ' Display the new sort order.
            lastcol = new_sorting_column
            If sort_order = SortOrder.Ascending Then
                lastcol.Text = "> " & lastcol.Text
            Else
                lastcol.Text = "< " & lastcol.Text
            End If

            ' Create a comparer.
            lv.ListViewItemSorter = New  _
                ListViewComparer(i, sort_order)

            ' Sort.
            lv.Sort()
        End Sub
    End Class
End Class