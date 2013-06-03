Public Class FormVote
    Public Shared VoteSent As Boolean = False

    Private Sub AlreadyVotedButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlreadyVotedButton.Click
        VoteSent = True
        Me.Close()
    End Sub
    Private Sub SubmitVoteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubmitVoteButton.Click
        If (RadioButton1A.Checked = False AndAlso RadioButton1B.Checked = False AndAlso RadioButton1C.Checked = False AndAlso RadioButton1D.Checked = False) OrElse (RadioButton2A.Checked = False AndAlso RadioButton2B.Checked = False AndAlso RadioButton2C.Checked = False AndAlso RadioButton2D.Checked = False) Then
            MsgBox("Poll is not complete, please answer all questions.", MsgBoxStyle.Exclamation, "Anti-Cheat Poll #2")
        Else
            Dim m As New LobbyShared.NetworkMessage(LobbyShared.NetworkMessage.MsgTypes.MiscMsg)
            m.StringCollection("VOTEANSWERS") = "1"
            m.StringCollection("FROM") = Globals.CurrentUser.Username
            m.StringCollection("IP") = Globals.CurrentUser.PublicIP
            For Each s As String In Globals.HardwareList.Keys
                If s.StartsWith("SID_") Then
                    m.StringCollection("SID") = Globals.HardwareList(s)
                End If
            Next

            If RadioButton1A.Checked Then
                m.StringCollection("Q1") = "A"
            ElseIf RadioButton1B.Checked Then
                m.StringCollection("Q1") = "B"
            ElseIf RadioButton1C.Checked Then
                m.StringCollection("Q1") = "C"
            ElseIf RadioButton1D.Checked Then
                m.StringCollection("Q1") = "D"
            End If
            If RadioButton2A.Checked Then
                m.StringCollection("Q2") = "A"
            ElseIf RadioButton2B.Checked Then
                m.StringCollection("Q2") = "B"
            ElseIf RadioButton2C.Checked Then
                m.StringCollection("Q2") = "C"
            ElseIf RadioButton2D.Checked Then
                m.StringCollection("Q2") = "D"
            End If

            Globals.ClientEngine.SendPacket(m)

            VoteSent = True
            Me.Close()
        End If
    End Sub
    Private Sub FormVote_FormClosing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            If Not VoteSent Then
                MsgBox("Please submit your vote.", MsgBoxStyle.Exclamation, "Anti-Cheat Poll #3")
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        Catch
        End Try
    End Sub

    Private Sub RadioButton1A_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1A.CheckedChanged
        If RadioButton1A.Checked Then
            RadioButton2A.Checked = False
            RadioButton2A.Enabled = False
        Else
            RadioButton2A.Enabled = True
        End If
    End Sub
    Private Sub RadioButton1B_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1B.CheckedChanged
        If RadioButton1B.Checked Then
            RadioButton2B.Checked = False
            RadioButton2B.Enabled = False
        Else
            RadioButton2B.Enabled = True
        End If
    End Sub
    Private Sub RadioButton1C_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1C.CheckedChanged
        If RadioButton1C.Checked Then
            RadioButton2C.Checked = False
            RadioButton2C.Enabled = False
        Else
            RadioButton2C.Enabled = True
        End If
    End Sub
    Private Sub RadioButton1D_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1D.CheckedChanged
        If RadioButton1D.Checked Then
            RadioButton2D.Checked = False
            RadioButton2D.Enabled = False
        Else
            RadioButton2D.Enabled = True
        End If
    End Sub
End Class