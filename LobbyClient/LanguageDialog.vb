Public Class LanguageDialog
    Public Shared ReturnLang As String
    Private Sub LanguageDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LanguageComboBox.SelectedIndex = 1
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Select Case LanguageComboBox.SelectedIndex
            Case 0
                ReturnLang = "de"
            Case 1
                ReturnLang = "en"
            Case 2
                ReturnLang = "es"
            Case 3
                ReturnLang = "fr"
            Case 4
                ReturnLang = "it"
            Case Else
                ReturnLang = "en"
        End Select
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class
