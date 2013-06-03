Public Class Options
    Public Shared Network_Details As String = ""
    Public Shared Please_Select_Network_Adapter As String = ""

    Public Shared Chat_Options As String = ""
    Public Shared Autoscroll As String = ""
    Public Shared Revert_To_Chat_After_Whisper As String = ""
    Public Shared Enable_Sounds As String = ""
    Public Shared Show_Timestamps As String = ""

    Public Shared Miscellaneous_Options As String = ""
    Public Shared Automatically_Resize_Game_Columns As String = ""
    Public Shared Maximize_Lobby_On_Login As String = ""
    Public Shared Show_Icon_In_System_Tray As String = ""
    Public Shared Minimize_To_System_Tray As String = ""
    Public Shared Language As String = ""

    Public Shared Button_Colors As String = ""
    Public Shared Top_Gradient As String = ""
    Public Shared Bottom_Gradient As String = ""

    Public Shared Chat_Colors As String = ""
    Public Shared Background As String = ""
    Public Shared Hyperlink As String = ""
    Public Shared User_Link As String = ""
    Public Shared Whisper_Link As String = ""
    Public Shared Chat_Text As String = ""
    Public Shared Whisper_Text As String = ""
    Public Shared Emote_Text As String = ""
    Public Shared Alert_Text As String = ""
    Public Shared Warning_Text As String = ""
    Public Shared Server_Text_1 As String = ""
    Public Shared Server_Text_2 As String = ""

    Public Shared Visual_Styles As String = ""
    Public Shared Enable_Visual_Styles As String = ""

    Public Shared Sub SetLanguage(ByVal lang As Main.Languages)
        Select Case lang
            Case Main.Languages.Deutsch
                Network_Details = "Netzwerk Details"
                Please_Select_Network_Adapter = "Bitte wählen sie Einen Netzwerk Adapter"

                Chat_Options = "Chat Optionen"
                Autoscroll = "Autoscroll"
                Revert_To_Chat_After_Whisper = "Umschlagen zum Chat nach Flüstern"
                Enable_Sounds = "Geräusche Einchalten"
                Show_Timestamps = "Show Timestamps"

                Miscellaneous_Options = "Verschiedene Optionen"
                Automatically_Resize_Game_Columns = "Automatische Größenveränderung von Spielspalten"
                Maximize_Lobby_On_Login = "Maximieren der Lobby nach Einschalten"
                Show_Icon_In_System_Tray = "Show Icon In System Tray"
                Minimize_To_System_Tray = "Minimize To System Tray"
                Language = "Sprachen"

                Button_Colors = "Button Farben"
                Top_Gradient = "Top Gradient"
                Bottom_Gradient = "Bottom Gradient"

                Chat_Colors = "Chat Farben"
                Background = "Hintergrund"
                Hyperlink = "Hyperlink"
                User_Link = "User Link"
                Whisper_Link = "Flüster Link"
                Chat_Text = "Chat Text"
                Whisper_Text = "Flüster Text"
                Emote_Text = "Emote Text "
                Alert_Text = "Alarm Text"
                Warning_Text = "Verwarnung "
                Server_Text_1 = "Server Text 1"
                Server_Text_2 = "Server Text 2"

                Visual_Styles = "Visual Styles"
                Enable_Visual_Styles = "Enable Visual Styles" & vbCrLf & vbCrLf & _
                    "Entferne den Haken, wenn du folgende Fehlermeldung erhältst:" & vbCrLf & _
                    "    System.AccessViolationException: Versuch, geschützten Speicher zu lesen oder überschreiben"
            Case Main.Languages.English
                Network_Details = "Network Details"
                Please_Select_Network_Adapter = "Please Select Network Adapter"

                Chat_Options = "Chat Options"
                Autoscroll = "Autoscroll"
                Revert_To_Chat_After_Whisper = "Revert To Chat After Whisper"
                Enable_Sounds = "Enable Sounds"
                Show_Timestamps = "Show Timestamps"

                Miscellaneous_Options = "Miscellaneous Options"
                Automatically_Resize_Game_Columns = "Automatically Resize Game Columns"
                Maximize_Lobby_On_Login = "Maximize Lobby On Login"
                Show_Icon_In_System_Tray = "Show Icon In System Tray"
                Minimize_To_System_Tray = "Minimize To System Tray"
                Language = "Language"

                Button_Colors = "Button Colors"
                Top_Gradient = "Top Gradient"
                Bottom_Gradient = "Bottom Gradient"

                Chat_Colors = "Chat Colors"
                Background = "Background"
                Hyperlink = "Hyperlink"
                User_Link = "User Link"
                Whisper_Link = "Whisper Link"
                Chat_Text = "Chat Text"
                Whisper_Text = "Whisper Text"
                Emote_Text = "Emote Text"
                Alert_Text = "Alert Text"
                Warning_Text = "Warning Text"
                Server_Text_1 = "Server Text 1"
                Server_Text_2 = "Server Text 2"

                Visual_Styles = "Visual Styles"
                Enable_Visual_Styles = "Enable Visual Styles" & vbCrLf & vbCrLf & _
                    "Uncheck this box if you get the following error:" & vbCrLf & _
                    "    System.AccessViolationException: Attempted to read or write protected memory"
            Case Main.Languages.Español
                Network_Details = "Detalles de Red"
                Please_Select_Network_Adapter = "Por favor Seleccione Un Adaptador de red"

                Chat_Options = "Opciones de Charla"
                Autoscroll = "Autoscroll"
                Revert_To_Chat_After_Whisper = "Retornar a la Charla después del Susurro"
                Enable_Sounds = "Desconectar Sonidos"
                Show_Timestamps = "Show Timestamps"

                Miscellaneous_Options = "Opciones de Configuración"
                Automatically_Resize_Game_Columns = "Columnas de Configuración Automática de Tamaño del Juego"
                Maximize_Lobby_On_Login = "Maximizar Lobby al Ingresar"
                Show_Icon_In_System_Tray = "Show Icon In System Tray"
                Minimize_To_System_Tray = "Minimize To System Tray"
                Language = "Lenguaje"

                Button_Colors = "Botones Para Color"
                Top_Gradient = "Gradiente Superior"
                Bottom_Gradient = "Gradiente Inferior"

                Chat_Colors = "Colores de Charla"
                Background = "Fondo"
                Hyperlink = "Enlace de Hipertexto"
                User_Link = "Enlace de Usuario"
                Whisper_Link = "Enlace de Susurro"
                Chat_Text = "Texto Para Charla"
                Whisper_Text = "Texto de Susurro"
                Emote_Text = "Texto Emotivo"
                Alert_Text = "Alertas de Texto"
                Warning_Text = "Advertencias de Texto"
                Server_Text_1 = "Texto 1 del Servidor"
                Server_Text_2 = "Texto 2 del Servidor"

                Visual_Styles = "Estilos Visuales"
                Enable_Visual_Styles = "Habilitar estilos Visuales" & vbCrLf & vbCrLf & _
                    "No revisar este cuadrante si aparece el siguiente error:" & vbCrLf & _
                    "    System.AccessViolationException: Intento de Leer o Escribir en Memoria Protegida"
            Case Main.Languages.Français
                Network_Details = "Détails Réseau"
                Please_Select_Network_Adapter = "Veuillez sélectionner le réseau adaptée"

                Chat_Options = "Options de chat"
                Autoscroll = "Défilement Automatique"
                Revert_To_Chat_After_Whisper = "Revenir au chat lorsque qu’on chuchote"
                Enable_Sounds = "Sons du chat"
                Show_Timestamps = "Show Timestamps"

                Miscellaneous_Options = "Options divers"
                Automatically_Resize_Game_Columns = "Redimensionner les colonnes automatiquement"
                Maximize_Lobby_On_Login = "Maximiser le Hall après identification"
                Show_Icon_In_System_Tray = "Show Icon In System Tray"
                Minimize_To_System_Tray = "Minimize To System Tray"
                Language = "Langage"

                Button_Colors = "Couleurs des Boutons"
                Top_Gradient = "Gradient de Haut"
                Bottom_Gradient = "Gradient de Bas"

                Chat_Colors = "Couleurs du chat"
                Background = "Arrière plan"
                Hyperlink = "Hyperlien"
                User_Link = "Lien utilisateurs"
                Whisper_Link = "Lien chuchoter"
                Chat_Text = "Texte du Chat"
                Whisper_Text = "Texte chuchoter"
                Emote_Text = "Texte émotion /e"
                Alert_Text = "Texte d’alerte"
                Warning_Text = "Texte Avertissement"
                Server_Text_1 = "Texte 1 serveur"
                Server_Text_2 = "Texte 2 serveur"

                Visual_Styles = "Style visual"
                Enable_Visual_Styles = "Permettre style visual" & vbCrLf & vbCrLf & _
                    "Décocher cette case si vous obtenez l’erreur suivante:" & vbCrLf & _
                    "    System.AccessViolationException: Tentative de lecture ou d'écriture de mémoire protégée"
            Case Main.Languages.Nederlands
                Network_Details = "Netwerk Details"
                Please_Select_Network_Adapter = "Selecteer Netwerk Adapter"

                Chat_Options = "Chat Opties"
                Autoscroll = "Autoscroll"
                Revert_To_Chat_After_Whisper = "Terug naar Chat Na 'Fluister'"
                Enable_Sounds = "Geluid aan"
                Show_Timestamps = "Show Timestamps"

                Miscellaneous_Options = "Overige Instellingen"
                Automatically_Resize_Game_Columns = "Pas Kolommen Automatisch aan in Grootte"
                Maximize_Lobby_On_Login = "Maximaliseer Lobby Na Login"
                Show_Icon_In_System_Tray = "Toon icoon in de taakbalk"
                Minimize_To_System_Tray = "Minimaliseer naar taakbalk"
                Language = "Taal"

                Button_Colors = "Knop Kleuren"
                Top_Gradient = "Top Gradient"
                Bottom_Gradient = "Bottom Gradient"

                Chat_Colors = "Chat Kleuren"
                Background = "Achtergrond"
                Hyperlink = "Hyperlink"
                User_Link = "Gebruikersnaam"
                Whisper_Link = "Fluister-functie"
                Chat_Text = "Chat Tekst"
                Whisper_Text = "Fluister-functie Tekst"
                Emote_Text = "/e Tekst"
                Alert_Text = "Alarm Tekst"
                Warning_Text = "Waarschuwingen"
                Server_Text_1 = "Server Tekst 1"
                Server_Text_2 = "Server Tekst 2"

                Visual_Styles = "Visuele Stijl"
                Enable_Visual_Styles = "Visuele Stijlen aan" & vbCrLf & vbCrLf & _
                    "Zet deze funtie uit bij de volgende error:" & vbCrLf & _
                    "    System.AccessViolationException: Attempted to read or write protected memory"
            Case Main.Languages.Português
                Network_Details = "Detalhes de Rede"
                Please_Select_Network_Adapter = "Por favor Seleccione um Adaptador de Rede"

                Chat_Options = "Opções de Conversa"
                Autoscroll = "Autorolar"
                Revert_To_Chat_After_Whisper = "Retornar à conversa depois do susurro"
                Enable_Sounds = "Cortar sons"

                Miscellaneous_Options = "Opções de Configuração"
                Automatically_Resize_Game_Columns = "Reformulação Automática da Configuração do Tamanho do Jogo"
                Maximize_Lobby_On_Login = "Maximizar Portaria ao Entrar"
                Show_Icon_In_System_Tray = "Mostrar Ícone na Bandeja do Sistema"
                Minimize_To_System_Tray = "Minimizar para Bandeja do Sistema"
                Language = "Linguagem"

                Button_Colors = "Botões de Côr"
                Top_Gradient = "Gradiente Superior"
                Bottom_Gradient = "Gradiente Inferior"

                Chat_Colors = "Conversa em Cores"
                Background = "Fundo"
                Hyperlink = "Hiperligação"
                User_Link = "Ligação do Usuário"
                Whisper_Link = "Ligação do Susurro"
                Chat_Text = "Texto para Conversa"
                Whisper_Text = "Texto de Susurro"
                Emote_Text = "Texto Emotivo"
                Alert_Text = "Alertas de Texto"
                Warning_Text = "Advertências de Texto"
                Server_Text_1 = "Texto 1 do Servidor"
                Server_Text_2 = "Texto 2 do Servidor"
                Visual_Styles = "Estilos Visuais"
                Enable_Visual_Styles = "Cortar estilos Visuais" & vbCrLf & vbCrLf & _
                    "Não procurar este quadrante se aparecer o seguinte erro:" & vbCrLf & _
                    "    System.AccessViolationException: Intenção de Ler ou Escrever en Memória Protegida"
            Case Main.Languages.Suomi
                Network_Details = "Internet Tiedot"
                Please_Select_Network_Adapter = "Valitse Verkkokortti"

                Chat_Options = "Keskusteluasetukset"
                Autoscroll = "Automaattinen rullaus"
                Revert_To_Chat_After_Whisper = "Vaihda keskusteluun sanomisen jälkeen"
                Enable_Sounds = "Ota Äänet Käyttöön"
                Show_Timestamps = "Show Timestamps"

                Miscellaneous_Options = "Muut Asetukset"
                Automatically_Resize_Game_Columns = "Vaihda Pelikolumnien Kokoa Automaattisesti"
                Maximize_Lobby_On_Login = "Maksimoi Aulan Koko Kirjautumisen Yhteydessä"
                Show_Icon_In_System_Tray = "Show Icon In System Tray"
                Minimize_To_System_Tray = "Minimize To System Tray"
                Language = "Kieli"

                Button_Colors = "Nappien Värit"
                Top_Gradient = "Ylempi Kerros"
                Bottom_Gradient = "Alempi Kerros"

                Chat_Colors = "Keskustelun Värit"
                Background = "Tausta"
                Hyperlink = "Hyperlinkki"
                User_Link = "Käyttäjä Linkki"
                Whisper_Link = "Sanomis Linkki"
                Chat_Text = "Keskusteluteksti"
                Whisper_Text = "Sanomisteksti"
                Emote_Text = " /e Teksti"
                Alert_Text = "Hälytysteksti"
                Warning_Text = "Varoitusteksti"
                Server_Text_1 = "Palvelinteksti 1"
                Server_Text_2 = "Palvelinteksti 2"

                Visual_Styles = "Visuaaliset Tyylit"
                Enable_Visual_Styles = "Ota Visuaaliset Tyylit Käyttöön" & vbCrLf & vbCrLf & _
                    "Älä Valitse Tätä, Jos Saat Kyseisen Virheilmoituksen:" & vbCrLf & _
                    "    System.AccessViolationException: Attempted to read or write protected memory"
            Case Else
                SetLanguage(Main.Languages.English)
        End Select
    End Sub
End Class
