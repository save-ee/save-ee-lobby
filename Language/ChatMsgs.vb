Public Class ChatMsgs
    Public Shared Invalid_command As String = ""
    Public Shared Insufficient_rights_for_this_command As String = ""
    ' Friends, Cheaters, Ignore Add/Remove
    Public Shared Friends_List As String = ""
    Public Shared Cheaters_List As String = ""
    Public Shared Ignore_List As String = ""
    ' Add/remove to list
    Public Shared is_already_on_your_friends_list As String = ""
    Public Shared was_added_to_your_friends_list As String = ""
    Public Shared was_removed_from_your_friends_list As String = ""
    Public Shared was_not_found_on_your_friends_list As String = ""
    Public Shared is_already_on_your_cheaters_list As String = ""
    Public Shared was_added_to_your_cheaters_list As String = ""
    Public Shared was_removed_from_your_cheaters_list As String = ""
    Public Shared was_not_found_on_your_cheaters_list As String = ""
    Public Shared is_already_on_your_ignore_list As String = ""
    Public Shared was_added_to_your_ignore_list As String = ""
    Public Shared was_removed_from_your_ignore_list As String = ""
    Public Shared was_not_found_on_your_ignore_list As String = ""
    Public Shared was_not_found_in_the_user_database As String = ""
    ' Friend alerts
    Public Shared has_joined_the_server As String = ""
    Public Shared has_left_the_server As String = ""
    ' Whisper, warn, alert
    Public Shared whispers_to_you As String = ""
    Public Shared You_whisper_to As String = ""
    Public Shared WARNING_from As String = ""
    Public Shared WARNING_to As String = ""
    Public Shared WARNING_Spam_message_dropped As String = ""
    Public Shared ALERT_from As String = ""
    ' Locate
    Public Shared has_not_been_seen As String = ""
    Public Shared was_last_seen_joining As String = ""
    Public Shared was_last_seen_hosting As String = ""


    Public Shared Sub SetLanguage(ByVal lang As Main.Languages)
        Select Case lang
            Case Main.Languages.Deutsch
                Invalid_command = "Ungültiger Befehl"
                Insufficient_rights_for_this_command = "Fehlende Rechte für diesen Befehl"
                ' Friends, Cheaters, Ignore Add/Remove
                Friends_List = "Freundesliste"
                Cheaters_List = "Cheaterliste"
                Ignore_List = "Ignorierenliste"
                ' Add/remove to list
                is_already_on_your_friends_list = "[NAME] ist bereits dein Freund"
                was_added_to_your_friends_list = "[NAME] wurde zu der Freundesliste hinzugefügt"
                was_removed_from_your_friends_list = "[NAME] wurde von der Freundesliste entfern "
                was_not_found_on_your_friends_list = "[NAME] wurde nicht auf der Freundesliste gefunden"
                is_already_on_your_cheaters_list = "[NAME] ist bereits auf deiner Cheaterliste"
                was_added_to_your_cheaters_list = "[NAME] wurde zu der Cheaterliste hinzugefügt"
                was_removed_from_your_cheaters_list = "[NAME] wurde von der Cheaterliste entfernt"
                was_not_found_on_your_cheaters_list = "[NAME] wurde nicht auf der Cheaterliste gefunden"
                is_already_on_your_ignore_list = "[NAME] wird bereits ignoriert"
                was_added_to_your_ignore_list = "[NAME] wurde zur Ignorierenliste hinzugefügt"
                was_removed_from_your_ignore_list = "[NAME] wurde von der Ignorierenliste entfernt"
                was_not_found_on_your_ignore_list = "[NAME] wurde nicht auf der Ignorierenliste gefunden"
                was_not_found_in_the_user_database = "[NAME] wurde nicht in der Benutzer Datenbank gefunden"
                ' Friend alerts
                has_joined_the_server = "hat den Server betreten"
                has_left_the_server = "hat den Server verlassen"
                ' Whisper, warn, alert
                whispers_to_you = "flüstert dir"
                You_whisper_to = "Du flüsterst an"
                WARNING_from = "WARNUNG von"
                WARNING_to = "WARNUNG an"
                WARNING_Spam_message_dropped = "WARNUNG: Spam message dropped"
                ALERT_from = "ALARM von"
                ' Locate
                has_not_been_seen = "X spielt zur Zeit kein Spiel"
                was_last_seen_joining = "X hat zuletzt in Y's Spiel ([IP]) @ [TIME] UTC unter dem Namen Z gespielt"
                was_last_seen_hosting = "X hat zuletzt [GAME] ([IP]) @ [TIME] UTC gehostet"
            Case Main.Languages.English
                Invalid_command = "Invalid command"
                Insufficient_rights_for_this_command = "Insufficient rights for this command"
                ' Friends, Cheaters, Ignore Add/Remove
                Friends_List = "Friends List"
                Cheaters_List = "Cheaters List"
                Ignore_List = "Ignore List"
                ' Add/remove to list
                is_already_on_your_friends_list = "[NAME] is already on your friends list"
                was_added_to_your_friends_list = "[NAME] was added to your friends list"
                was_removed_from_your_friends_list = "[NAME] was removed from your friends list"
                was_not_found_on_your_friends_list = "[NAME] was not found on your friends list"
                is_already_on_your_cheaters_list = "[NAME] is already on your cheaters list"
                was_added_to_your_cheaters_list = "[NAME] was added to your cheaters list"
                was_removed_from_your_cheaters_list = "[NAME] was removed from your cheaters list"
                was_not_found_on_your_cheaters_list = "[NAME] was not found on your cheaters list"
                is_already_on_your_ignore_list = "[NAME] is already on your ignore list"
                was_added_to_your_ignore_list = "[NAME] was added to your ignore list"
                was_removed_from_your_ignore_list = "[NAME] was removed from your ignore list"
                was_not_found_on_your_ignore_list = "[NAME] was not found on your ignore list"
                was_not_found_in_the_user_database = "[NAME] was not found in the user database"
                ' Friend alerts
                has_joined_the_server = "has joined the server"
                has_left_the_server = "has left the server"
                ' Whisper, warn, alert
                whispers_to_you = "whispers to you"
                You_whisper_to = "You whisper to"
                WARNING_from = "WARNING from"
                WARNING_to = "WARNING to"
                WARNING_Spam_message_dropped = "WARNING: Spam message dropped"
                ALERT_from = "ALERT from"
                ' Locate
                has_not_been_seen = "X has not been seen in a game"
                was_last_seen_joining = "X was last seen joining Y's game ([IP]) @ [TIME] UTC using the name Z"
                was_last_seen_hosting = "X was last seen hosting [GAME] ([IP]) @ [TIME] UTC"
            Case Main.Languages.Español
                Invalid_command = "Comando Inválido"
                Insufficient_rights_for_this_command = "Derechos insuficientes para este Comando"
                ' Friends, Cheaters, Ignore Add/Remove
                Friends_List = "Lista de Amigos"
                Cheaters_List = "Lista de Tramposos"
                Ignore_List = "Lista de Ignorados"
                ' Add/remove to list
                ' ex: Ghost is already on your friends list.
                is_already_on_your_friends_list = "[NAME] ya está en su lista de amigos"
                was_added_to_your_friends_list = "[NAME] fué agragado a su lista de amigos"
                was_removed_from_your_friends_list = "[NAME] fué removido de su lista de amigos"
                was_not_found_on_your_friends_list = "[NAME] no fué encontrado en su lista de amigos"
                is_already_on_your_cheaters_list = "[NAME] ya está en su lista de tramposos"
                was_added_to_your_cheaters_list = "[NAME] fué agragado a su lista de tramposos"
                was_removed_from_your_cheaters_list = "[NAME] fué removido de su lista de tramposos"
                was_not_found_on_your_cheaters_list = "[NAME] no fué encontrado en su lista de tramposos"
                is_already_on_your_ignore_list = "[NAME] ya está en su lista de ignorados"
                was_added_to_your_ignore_list = "[NAME] fué agragado a su lista de ignorados"
                was_removed_from_your_ignore_list = "[NAME] fué removido de su lista de ignorados"
                was_not_found_on_your_ignore_list = "[NAME] no fué encontrado en su lista de ignorados"
                was_not_found_in_the_user_database = "[NAME] no fué encontrado en la base de datos del usuario"
                ' Friend alerts
                has_joined_the_server = "se ha unido al servidor"
                has_left_the_server = "se ha retirado del servidor"
                ' Whisper, warn, alert
                whispers_to_you = "te susurra que"
                You_whisper_to = "Tú le susurras que"
                WARNING_from = "ADVERTENCIA de"
                WARNING_to = "ADVERTENCIA para"
                WARNING_Spam_message_dropped = "ADVERTENCIA: Spam message dropped"
                ALERT_from = "ALERTA de"
                ' Locate
                has_not_been_seen = "X no ha sido visto en el juego"
                was_last_seen_joining = "X fue últimamente visto al unirse al juego de Y ([IP]) al [TIME] UTC usando el nombre de Z"
                was_last_seen_hosting = "X fué ultimamente visto siendo anfitrión [GAME] ([IP]) al [TIME] UTC"
                Invalid_command = "Instruction invalide"
                Insufficient_rights_for_this_command = "Droits insuffisant pour cette instruction"
            Case Main.Languages.Français
                ' Friends, Cheaters, Ignore Add/Remove
                Friends_List = "Liste d’Amis"
                Cheaters_List = "Liste des Tricheurs"
                Ignore_List = "Liste des Bloqués"
                ' Add/remove to list
                is_already_on_your_friends_list = "[NAME] est déjà sur votre liste d'amis"
                was_added_to_your_friends_list = "[NAME] a été ajouté à votre liste d'amis"
                was_removed_from_your_friends_list = "[NAME] a été retiré de votre liste d'amis"
                was_not_found_on_your_friends_list = "[NAME] est introuvable sur votre liste d'amis"
                is_already_on_your_cheaters_list = "[NAME] est déjà sur votre liste de tricheurs"
                was_added_to_your_cheaters_list = "[NAME] a été ajouté à votre liste de tricheurs"
                was_removed_from_your_cheaters_list = "[NAME] a été retiré de votre liste de tricheurs"
                was_not_found_on_your_cheaters_list = "[NAME] est introuvable sur votre liste de tricheurs"
                is_already_on_your_ignore_list = "[NAME] est déjà sur votre liste des bloqués"
                was_added_to_your_ignore_list = "[NAME] a été ajouté sur votre liste des bloqués"
                was_removed_from_your_ignore_list = "[NAME] a été retiré de votre liste des bloqués"
                was_not_found_on_your_ignore_list = "[NAME] est introuvable dans votre liste des bloqués"
                was_not_found_in_the_user_database = "[NAME] est introuvable dans la base de données utilisateur"
                ' Friend alerts
                has_joined_the_server = "a rejoint le serveur"
                has_left_the_server = "a quitté le serveur"
                ' Whisper, warn, alert
                whispers_to_you = "vous chuchote"
                You_whisper_to = "vous chuchotez à"
                WARNING_from = "AVERTISSEMENT de"
                WARNING_to = "AVERTISSEMENT à"
                WARNING_Spam_message_dropped = "AVERTISSEMENT: Spam message dropped"
                ALERT_from = "ALERTE de"
                ' Locate
                has_not_been_seen = "X n’a pas été localisé dans une partie"
                was_last_seen_joining = "X a été localisé pour la dernière fois dans la partie Y ([IP]) @ [TIME] UTC en utilisant le nom Z"
                was_last_seen_hosting = "X a été localiser pour la dernière fois étant l’hôte de [GAME] ([IP]) @ [TIME] UTC"
            Case Main.Languages.Nederlands
                Invalid_command = "Ongeldig commando"
                Insufficient_rights_for_this_command = "Niet voldoende rechten voor deze functie"
                ' Friends, Cheaters, Ignore Add/Remove
                Friends_List = "Vriendenlijst"
                Cheaters_List = "Valsspelerslijst"
                Ignore_List = "Negeerlijst"
                ' Add/remove to list
                ' ex: Ghost is already on your friends list.
                is_already_on_your_friends_list = "[NAME] staat al op je vriendenlijst"
                was_added_to_your_friends_list = "[NAME] is toegevoegd aan je vriendenlijst"
                was_removed_from_your_friends_list = "[NAME] is verwijderd van je vriendenlijst"
                was_not_found_on_your_friends_list = "[NAME] staat niet op je vriendenlijst"
                is_already_on_your_cheaters_list = "[NAME] staat al op je valsspelerslijst"
                was_added_to_your_cheaters_list = "[NAME] is toegevoegd aan je valsspelerslijst"
                was_removed_from_your_cheaters_list = "[NAME] is verwijderd van je valsspelerslijst"
                was_not_found_on_your_cheaters_list = "[NAME] staat niet op je valsspelerslijst"
                is_already_on_your_ignore_list = "[NAME] staat al op je negeerlijst"
                was_added_to_your_ignore_list = "[NAME] is toegevoegd aan je negeerlijst"
                was_removed_from_your_ignore_list = "[NAME] is verwijderd van je negeerlijst"
                was_not_found_on_your_ignore_list = "[NAME] staat niet op je negeerlijst"
                was_not_found_in_the_user_database = "[NAME] staat niet in de gebruikers database"
                ' Friend alerts
                has_joined_the_server = "is online gekomen"
                has_left_the_server = "heeft de verbinding verbroken"
                ' Whisper, warn, alert
                whispers_to_you = "fluistert naar jou"
                You_whisper_to = "Je fluistert naar"
                WARNING_from = "WAARSCHUWING van"
                WARNING_to = "WAARSCHUW"
                WARNING_Spam_message_dropped = "WARNING: Spam message dropped"
                ALERT_from = "ALARM van"
                ' Locate
                has_not_been_seen = "X is niet in een spel"
                was_last_seen_joining = "X is het laatst gezien in Y's spel ([IP]) @ [TIME] UTC onder de naam Z"
                was_last_seen_hosting = "X is het laatst gezien terwijl hij [GAME] ([IP]) hoste @ [TIME] UTC"
            Case Main.Languages.Português
                Invalid_command = "Comando Inválido"
                Insufficient_rights_for_this_command = "Direitos insuficientes para este Comando"
                ' Friends, Cheaters, Ignore Add/Remove
                Friends_List = "Lista de Amigos"
                Cheaters_List = "Lista de Batoteiros"
                Ignore_List = "Lista de Ignorados"
                ' Add/remove to list
                ' ex: Ghost is already on your friends list.
                is_already_on_your_friends_list = "[NAME] já está en sua lista de amigos"
                was_added_to_your_friends_list = "[NAME] foi agregado à sua lista de amigos"
                was_removed_from_your_friends_list = "[NAME] foi removido da sua lista de amigos"
                was_not_found_on_your_friends_list = "[NAME] não encontrado na sua lista de amigos"
                is_already_on_your_cheaters_list = "[NAME] já está na sua lista de Batoteiros"
                was_added_to_your_cheaters_list = "[NAME] foi agregado à sua lista de Batoteiros"
                was_removed_from_your_cheaters_list = "[NAME] foi removido de sua lista de Batoteiros"
                was_not_found_on_your_cheaters_list = "[NAME] não foi encontrado na sua lista de Batoteiros"
                is_already_on_your_ignore_list = "[NAME] já está na sua lista de ignorados"
                was_added_to_your_ignore_list = "[NAME] foi agragado à sua lista de ignorados"
                was_removed_from_your_ignore_list = "[NAME] foi removido da sua lista de ignorados"
                was_not_found_on_your_ignore_list = "[NAME] não foi encontrado na sua lista de ignorados"
                was_not_found_in_the_user_database = "[NAME] não foi encontrado na base de dados do usuário"
                ' Friend alerts
                has_joined_the_server = "juntou-se ao servidor"
                has_left_the_server = "deixou o servidor"
                ' Whisper, warn, alert
                whispers_to_you = "te susurra que"
                You_whisper_to = "tu susurras que"
                WARNING_from = "ADVERTÊNCIA de"
                WARNING_to = "ADVERTÊNCIA para"
                ALERT_from = "ALERTA de"
                ' Locate
                has_not_been_seen = "X não foi visto no jogo"
                was_last_seen_joining = "X foi ultimamente visto a juntar-se ao jogo de Y ([IP]) e [TIME] UTC usando o nome de Z"
                was_last_seen_hosting = "X foi ultimamente visto sendo anfitrião [GAME] ([IP]) e [TIME] UTC"
            Case Main.Languages.Suomi
                Invalid_command = "Virheellinen komento"
                Insufficient_rights_for_this_command = "Ei tarpeeksi valtuuksia tälle komennolle"
                ' Friends, Cheaters, Ignore Add/Remove
                Friends_List = "Ystävälista"
                Cheaters_List = "Huijarilista"
                Ignore_List = "Torjutut"
                ' Add/remove to list
                ' ex: Ghost is already on your friends list.
                is_already_on_your_friends_list = "[NAME] on jo ystävälistallasi"
                was_added_to_your_friends_list = "[NAME] lisättiin ystävälistallesi"
                was_removed_from_your_friends_list = "[NAME] poistettiin ystävälistaltasi"
                was_not_found_on_your_friends_list = "[NAME] ei ole ystävälistallasi"
                is_already_on_your_cheaters_list = "[NAME] on jo huijarilistaltasi"
                was_added_to_your_cheaters_list = "[NAME] lisättiin huijarilistaltasi"
                was_removed_from_your_cheaters_list = "[NAME] poistettiin huijarilistaltasi"
                was_not_found_on_your_cheaters_list = "Käyttäjä [NAME] ei ole huijarilistallasi"
                is_already_on_your_ignore_list = "[NAME] on jo torjuttu"
                was_added_to_your_ignore_list = "Torjuit käyttäjän [NAME]"
                was_removed_from_your_ignore_list = "Lopetit käyttäjän [NAME] torjumisen"
                was_not_found_on_your_ignore_list = "Et ole torjunut käyttäjää [NAME]"
                was_not_found_in_the_user_database = "Käyttäjää [NAME] ei löytynyt käyttäjäarkistosta"
                ' Friend alerts
                has_joined_the_server = "liittyi palvelimelle"
                has_left_the_server = "poistui palvelimelta"
                ' Whisper, warn, alert
                whispers_to_you = "sanoo sinulle"
                You_whisper_to = "Sanot käyttäjälle"
                WARNING_from = "VAROITUS käyttäjältä"
                WARNING_to = "VAROITUS käyttäjälle"
                WARNING_Spam_message_dropped = "VAROITUS: Spam message dropped"
                ALERT_from = "HÄLYTYS käyttäjältä"
                ' Locate
                has_not_been_seen = "Käyttäjää X ei ole nähty pelissä"
                was_last_seen_joining = "Käyttäjä X nähtiin viimeksi Y:n pelissä ([IP]), [TIME] UTC nimellä Z"
                was_last_seen_hosting = "Käyttäjä X nähtiin viimeksi hostaamassa peliä [GAME] ([IP]), [TIME] UTC"
            Case Else
                SetLanguage(Main.Languages.English)
        End Select
    End Sub
End Class
