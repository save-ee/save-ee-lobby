Public Class Main
    Public Shared Unknown As String = ""
    Public Shared Exit_ As String = ""
    Public Shared Show As String = ""

    Public Enum Languages
        Deutsch
        English
        Español
        Français
        Nederlands
        Português
        Suomi
    End Enum

    Public Shared Sub SetLanguage(ByVal lang As Main.Languages)
        ChatMsgs.SetLanguage(lang)
        GamesList.SetLanguage(lang)
        Lobby.SetLanguage(lang)
        Login.SetLanguage(lang)
        Menus.SetLanguage(lang)
        Options.SetLanguage(lang)
        Patches.SetLanguage(lang)
        Windows.SetLanguage(lang)

        Select Case lang
            Case Main.Languages.Deutsch
                Unknown = "Unbekannt"
                Exit_ = "Exit"
                Show = "Show"
            Case Main.Languages.English
                Unknown = "Unknown"
                Exit_ = "Exit"
                Show = "Show"
            Case Main.Languages.Español
                Unknown = "Desconocido"
                Exit_ = "Exit"
                Show = "Show"
            Case Main.Languages.Français
                Unknown = "Inconnu"
                Exit_ = "Exit"
                Show = "Show"
            Case Main.Languages.Nederlands
                Unknown = "Onbekend"
                Exit_ = "Sluiten"
                Show = "Toon"
            Case Main.Languages.Português
                Unknown = "Desconhecido"
                Exit_ = "Saída"
                Show = "Mostrar"
            Case Main.Languages.Suomi
                Unknown = "Tuntematon"
                Exit_ = "Exit"
                Show = "Show"
            Case Else
                SetLanguage(Main.Languages.English)
        End Select
    End Sub
End Class
