Public Class Menus
    ' GameContextMenuStrip
    Public Shared Copy_IP As String = ""

    ' UserContextMenuStrip
    Public Shared Whisper As String = ""
    Public Shared Copy_Name As String = ""
    Public Shared Add_to_Friends As String = ""
    Public Shared Remove_from_Friends As String = ""
    Public Shared Add_to_Cheaters As String = ""
    Public Shared Remove_from_Cheaters As String = ""
    Public Shared Ignore As String = ""
    Public Shared Unignore As String = ""
    Public Shared Set_AFK As String = ""
    Public Shared Clear_AFK As String = ""
    Public Shared Manual_Add_to As String = ""
    ' Manual Menu
    Public Shared Friends As String = ""
    Public Shared Cheaters As String = ""
    '      Ignore already declared
    Public Shared Enter_a_username As String = ""
    ' Mod Menu
    Public Shared Moderator_Functions As String = ""
    Public Shared Warn As String = ""
    Public Shared Kick As String = ""
    Public Shared Mute As String = ""
    Public Shared Manual_Mute As String = ""
    Public Shared List_Muted_Players As String = ""
    Public Shared Locate As String = ""
    Public Shared Get_Details As String = ""
    ' Admin Menu
    Public Shared Admin_Functions As String = ""
    Public Shared Force_Update As String = ""
    Public Shared Ban As String = ""
    Public Shared Manual_Ban As String = ""
    Public Shared List_Banned_Players As String = ""
    Public Shared Promote_to_Admin As String = ""
    Public Shared Promote_to_Moderator As String = ""
    Public Shared Promote_to_Donator As String = ""
    Public Shared Demote_to_User As String = ""

    Public Shared Sub SetLanguage(ByVal lang As Main.Languages)
        Select Case lang
            Case Main.Languages.Deutsch
                ' GameContextMenuStrip
                Copy_IP = "IP kopieren"

                ' UserContextMenuStrip
                Whisper = "Flüstern"
                Copy_Name = "Name kopieren"
                Add_to_Friends = "Zur Freundeliste hinzufügen"
                Remove_from_Friends = "Aus Freundeliste entfernen"
                Add_to_Cheaters = "Zur Cheaterliste hinzufügen"
                Remove_from_Cheaters = "Aus Cheaterliste entfernen"
                Ignore = "Ignorieren"
                Unignore = "Ignorieren Aufheben"
                Set_AFK = "AFK setzen"
                Clear_AFK = "AFK aufheben"
                Manual_Add_to = "Manuell hinzufügen zu"
                ' Manual Menu
                Friends = "Freunde"
                Cheaters = "Cheater"
                'Ignore = "Ignorierte" already declared
                Enter_a_username = "Benutzernamen eingeben."
                ' Mod Menu
                Moderator_Functions = "Moderations Funktionen"
                Warn = "Warnen"
                Kick = "Kicken"
                Mute = "Stumm setzen"
                Manual_Mute = "Manuelles Stumm setzen"
                List_Muted_Players = "Liste der Stummgesetzten"
                Locate = "Suchen"
                Get_Details = "Details abrufen"
                ' Admin Menu
                Admin_Functions = "Administrations-Funktionen"
                Force_Update = "Update Erzwingen"
                Ban = "Sperren"
                Manual_Ban = "Manuelles sperren"
                List_Banned_Players = "Liste der gesperrten Spieler"
                Promote_to_Admin = "Zum Admin befördern"
                Promote_to_Moderator = "Zum Moderator befördern"
                Promote_to_Donator = "Promote to Donator"
                Demote_to_User = "Zum Benutzer degradieren"
            Case Main.Languages.English
                ' GameContextMenuStrip
                Copy_IP = "Copy IP"

                ' UserContextMenuStrip
                Whisper = "Whisper"
                Copy_Name = "Copy Name"
                Add_to_Friends = "Add to Friends"
                Remove_from_Friends = "Remove from Friends"
                Add_to_Cheaters = "Add to Cheaters"
                Remove_from_Cheaters = "Remove from Cheaters"
                Ignore = "Ignore"
                Unignore = "Unignore"
                Set_AFK = "Set AFK"
                Clear_AFK = "Clear AFK"
                Manual_Add_to = "Manual Add to"
                ' Manual Menu
                Friends = "Friends"
                Cheaters = "Cheaters"
                'Ignore = "Ignore" already declared
                Enter_a_username = "Enter a username."
                ' Mod Menu
                Moderator_Functions = "Moderator Functions"
                Warn = "Warn"
                Kick = "Kick"
                Mute = "Mute"
                Manual_Mute = "Manual Mute"
                List_Muted_Players = "List Muted Players"
                Locate = "Locate"
                Get_Details = "Get Details"
                ' Admin Menu
                Admin_Functions = "Admin Functions"
                Force_Update = "Force Update"
                Ban = "Ban"
                Manual_Ban = "Manual Ban"
                List_Banned_Players = "List Banned Players"
                Promote_to_Admin = "Promote to Admin"
                Promote_to_Moderator = "Promote to Moderator"
                Promote_to_Donator = "Promote to Donator"
                Demote_to_User = "Demote to User"
            Case Main.Languages.Español
                ' GameContextMenuStrip
                Copy_IP = "Copy IP"

                ' UserContextMenuStrip
                Whisper = "Susurrar"
                Copy_Name = "Copiar Nombre"
                Add_to_Friends = "Agregar a Amigos"
                Remove_from_Friends = "Quitar de Amigos"
                Add_to_Cheaters = "Agregar a Tramposos"
                Remove_from_Cheaters = "Quitar de Tramposos"
                Ignore = "Ignorar"
                Unignore = "No Ignorar"
                Set_AFK = "Colocar AFK (Lejos del Teclado)"
                Clear_AFK = "Quitar AFK (Lejos del Teclado)"
                Manual_Add_to = "Manual Agregar a"
                ' Manual Menu
                Friends = "Amigos"
                Cheaters = "Tramposos"
                'Ignore = "Ignorar" already declared
                Enter_a_username = "Escriba un Usuario."
                ' Mod Menu
                Moderator_Functions = "Funciones del Moderador"
                Warn = "Advertir"
                Kick = "Patear (echar)"
                Mute = "Silenciar"
                Manual_Mute = "Silencio Manual"
                List_Muted_Players = "Lista de Jugadores Silenciados"
                Locate = "Ubicar, Localizar"
                Get_Details = "Obtener Detalles"
                ' Admin Menu
                Admin_Functions = "Funciones del Administrador"
                Force_Update = "Actualización sin duda"
                Ban = "Interferir"
                Manual_Ban = "Interferencia Manual"
                List_Banned_Players = "Lista de Jugadores Interferidos"
                Promote_to_Admin = "Promover al Administrador"
                Promote_to_Moderator = "Promover al Moderador"
                Promote_to_Donator = "Promote to Donator"
                Demote_to_User = "Degradar al Usuario"
            Case Main.Languages.Français
                ' GameContextMenuStrip
                Copy_IP = "Copier IP"

                ' UserContextMenuStrip
                Whisper = "Chuchoter"
                Copy_Name = "Copier Nom"
                Add_to_Friends = "Ajouter aux Amis"
                Remove_from_Friends = "Retirer des Amis"
                Add_to_Cheaters = "Ajouter aux Tricheurs"
                Remove_from_Cheaters = "Retirer des Tricheurs"
                Ignore = "Bloquer"
                Unignore = "Débloquer"
                Set_AFK = "Absent"
                Clear_AFK = "Disponible"
                Manual_Add_to = "Ajouter Manuellement aux"
                ' Manual Menu
                Friends = "Amis"
                Cheaters = "Tricheurs"
                'Ignore = "Bloqués" already declared
                Enter_a_username = "Entrer nom d’utilisateur."
                ' Mod Menu
                Moderator_Functions = "Fonctions Modérateurs"
                Warn = "Avertissement"
                Kick = "Expulser"
                Mute = "Réduire au silence"
                Manual_Mute = "Réduire au silence Manuellement"
                List_Muted_Players = "Liste des joueurs réduit au silence"
                Locate = "Localiser"
                Get_Details = "Obtenir plus de détails"
                ' Admin Menu
                Admin_Functions = "Fonctions Administrateur"
                Force_Update = "Forcer la mise à jour"
                Ban = "Bannir"
                Manual_Ban = "Bannir manuellement"
                List_Banned_Players = "Liste des joueurs bannis"
                Promote_to_Admin = "Promouvoir l’administrateur"
                Promote_to_Moderator = "Promouvoir modérateur"
                Promote_to_Donator = "Promouvoir donateur"
                Demote_to_User = "Rétrograder a l’utilisateur"
            Case Main.Languages.Nederlands
                ' GameContextMenuStrip
                Copy_IP = "Kopieer IP"

                ' UserContextMenuStrip
                Whisper = "Fluister"
                Copy_Name = "Kopieer Naam"
                Add_to_Friends = "Voeg toe aan je Vrienden"
                Remove_from_Friends = "Verwijder van je Vrienden"
                Add_to_Cheaters = "Voeg toe aan de Valsspelers"
                Remove_from_Cheaters = "Verwijder van de Valsspelers"
                Ignore = "Negeer"
                Unignore = "Stop Negeren"
                Set_AFK = "Zet AFK aan"
                Clear_AFK = "Hef AFK op"
                Manual_Add_to = "Voeg Handmatig toe aan"
                ' Manual Menu
                Friends = "Vrienden"
                Cheaters = "Valsspelers"
                'Ignore = "Ignore" already declared
                Enter_a_username = "Geef een gebruikersnaam op."
                ' Mod Menu
                Moderator_Functions = "Moderator Functies"
                Warn = "Waarschuw"
                Kick = "Schop"
                Mute = "Dempen"
                Manual_Mute = "Handmatige Mute"
                List_Muted_Players = "Lijst met Gemute Gebruikersnamen"
                Locate = "Lokaliseren"
                Get_Details = "Verkrijg Details"
                ' Admin Menu
                Admin_Functions = "Admin Functies"
                Force_Update = "Forceer Update"
                Ban = "Verban"
                Manual_Ban = "Handmatige Verbanning"
                List_Banned_Players = "Lijst met Verbande Gebruikersnamen."
                Promote_to_Admin = "Promoveer tot Admin"
                Promote_to_Moderator = "Promoveer tot Moderator"
                Promote_to_Donator = "Promoveer naar Donateur"
                Demote_to_User = "Degradeer tot Gebruiker"
            Case Main.Languages.Português
                ' GameContextMenuStrip
                Copy_IP = "Copiar_IP"

                ' UserContextMenuStrip
                Whisper = "Susurrar"
                Copy_Name = "Copiar Nome"
                Add_to_Friends = "Agregar a Amigos"
                Remove_from_Friends = "Remover dos Amigos"
                Add_to_Cheaters = "Agregar a Batoteiros"
                Remove_from_Cheaters = "Remover dos Batoteiros"
                Ignore = "Ignorar"
                Unignore = "Não Ignorar"
                Set_AFK = "Colocar AFK (Longe do Teclado)"
                Clear_AFK = "Deixar AFK (Longe do Teclado)"
                Manual_Add_to = "Manual Agregar a"
                ' Manual Menu
                Friends = "Amigos"
                Cheaters = "Batoteiros"
                'Ignore = "Ignorar"
                Enter_a_username = "Escreva um Usuário."
                ' Mod Menu
                Moderator_Functions = "Funções do Moderador"
                Warn = "Advertir"
                Kick = "Chutar"
                Mute = "Silenciar"
                Manual_Mute = "Manual_Silencio "
                List_Muted_Players = "Lista de Jogadores Silenciados"
                Locate = "Localizar"
                Get_Details = "Obter Detalhes"
                ' Admin Menu
                Admin_Functions = "Funções do Administrador"
                Force_Update = "Actualização forçada"
                Ban = "Banir"
                Manual_Ban = "Manual_Banir"
                List_Banned_Players = "Lista de Jogadores Banidos"
                Promote_to_Admin = "Promover a Administrador"
                Promote_to_Moderator = "Promover a Moderador"
                Promote_to_Donator = "Promover a Doador"
                Demote_to_User = "Despromover a Usuário"
            Case Main.Languages.Suomi
                ' GameContextMenuStrip
                Copy_IP = "Kopio IP"

                ' UserContextMenuStrip
                Whisper = "Sano"
                Copy_Name = "Kopio Nimi"
                Add_to_Friends = "Lisää Ystäviin"
                Remove_from_Friends = "Poista Ystävistä"
                Add_to_Cheaters = "Lisää Huijareihin"
                Remove_from_Cheaters = "Poista Huijareista"
                Ignore = "Torju"
                Unignore = "Älä Torju"
                Set_AFK = "Aseta AFK"
                Clear_AFK = "Poista AFK"
                Manual_Add_to = "Lisää manuaalisesti ryhmään"
                ' Manual Menu
                Friends = "Ystävät"
                Cheaters = "Huijarit"
                'Ignore = "Torju" already declared
                Enter_a_username = "Anna käyttäjänimi."
                ' Mod Menu
                Moderator_Functions = "Moderaattorin Komennot"
                Warn = "Varoita"
                Kick = "Potki"
                Mute = "Hiljennä"
                Manual_Mute = "Manuaalinen Hiljennys"
                List_Muted_Players = "Listaa Hiljennetyt Käyttäjät"
                Locate = "Paikanna"
                Get_Details = "Hanki Tiedot"
                ' Admin Menu
                Admin_Functions = "Ylläpitäjän Komennot"
                Force_Update = "Pakota Päivitys"
                Ban = "Anna Porttikielto"
                Manual_Ban = "Manuaalinen Porttikielto"
                List_Banned_Players = "Porttikiellon saaneet pelaajat"
                Promote_to_Admin = "Ylennä Ylläpitäjäksi"
                Promote_to_Moderator = "Ylennä Moderaattoriksi"
                Promote_to_Donator = "Promote to Donator"
                Demote_to_User = "Alenna Käyttäjäksi"
            Case Else
                SetLanguage(Main.Languages.English)
        End Select
    End Sub
End Class
