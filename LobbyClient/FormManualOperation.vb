Public Class FormManualOperation
    Public Action As String = ""
    Public p As New LobbyShared.NetworkMessage
    Private Sub FormKeyList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = Language.Windows.Please_enter_usernames_separated_by_Enter
        If Action = "BAN" Then
            ActionButton.Text = Language.Menus.Ban
            Me.Text = Language.Menus.Manual_Ban
        ElseIf Action = "MUTE" Then
            ActionButton.Text = Language.Menus.Mute
            Me.Text = Language.Menus.Manual_Mute
        Else
            ActionButton.Visible = False
        End If
    End Sub
    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton0.Click
        Me.Close()
    End Sub
    Private Sub ActionButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActionButton.Click
        Dim lst As String() = Split(TextBox1.Text, vbCrLf, , CompareMethod.Text)
        Dim b As New List(Of String)
        For Each u As String In lst
            If u <> "" Then b.Add(u)
        Next
        Globals.ClientEngine.GetKeyList(b.ToArray, Action)
        Me.Close()
    End Sub
End Class