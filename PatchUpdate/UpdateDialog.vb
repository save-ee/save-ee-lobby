Imports System.Windows.Forms

Public Class UpdateDialog
    Private Sub UpdateDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            AppActivate(Me.Text)
        Catch
        End Try
        InfoTextBox.Text = ""
        OK_Button.Enabled = False
        Me.Show()
        Me.Refresh()
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub UpdateDialog_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        FormPatch.Enabled = False
    End Sub
    ' wont work the way i want it to
    'Private Sub UpdateDialog_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    '    FormUpdate.Enabled = True
    '    Try
    '        AppActivate(FormUpdate.Text)
    '    Catch
    '    End Try
    'End Sub
End Class
