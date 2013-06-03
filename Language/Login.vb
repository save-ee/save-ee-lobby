Public Class Login
    Public Shared Username As String = ""
    Public Shared Password As String = ""
    Public Shared Create_New_User As String = ""
    Public Shared Login As String = ""

    ' Status label updates
    Public Shared Loading_preferences As String = ""
    Public Shared Loading_usernames As String = ""
    Public Shared Obtaining_public_IP As String = ""
    Public Shared Obtaining_server_IP As String = ""
    Public Shared Obtaining_announcement As String = ""
    Public Shared Obtaining_smilies As String = ""
    Public Shared Obtaining_bad_words As String = ""
    Public Shared Checking_for_updates As String = ""
    Public Shared Initialization_complete As String = ""
    ' Login attempt
    Public Shared Your_connection_to_the_server_was_terminated As String = ""
    Public Shared Connecting_to_server As String = ""
    Public Shared Unable_to_contact_server As String = ""
    Public Shared Connected__initializing As String = ""
    Public Shared Authenticating As String = ""
    Public Shared Verification_complete As String = ""

    ' Errors
    Public Shared Startup_Error As String = ""
    Public Shared Login_Error As String = ""
    ' Username/Password is required/contains....
    Public Shared is_required As String = ""
    Public Shared contains_invalid_chars As String = ""
    Public Shared contains_too_many_chars As String = ""
    Public Shared Please_check_your_internet_connection As String = ""
    Public Shared Error_obtaining_public_IP As String = ""
    Public Shared Error_obtaining_server_IP As String = ""
    Public Shared Error_obtaining_announcement As String = ""
    ' Login attempt
    Public Shared User_authentication_failed As String = ""
    Public Shared Account_already_in_use As String = ""
    Public Shared Account_was_locked_by_an_administrator As String = ""
    Public Shared Account_already_exists As String = ""
    Public Shared Account_not_found As String = ""

    Public Shared Sub SetLanguage(ByVal lang As Main.Languages)
        Select Case lang
            Case Main.Languages.Deutsch
                Username = "Benutzername"
                Password = "Passwort"
                Create_New_User = "Erstelle neuen Benutzer"
                Login = "Einloggen"

                ' Status label updates
                Loading_preferences = "Einstellungen werden geladen, bitte warten..."
                Loading_usernames = "Benutzername wird geladen, bitte warten..."
                Obtaining_public_IP = "Erkennen von Öffentlicher IP, bitte warten..."
                Obtaining_server_IP = "Erkennen von Server IP, bitte warten..."
                Obtaining_announcement = "Erkennen der Ankündigung, bitte warten..."
                Obtaining_smilies = "Erkennen der Smileys, bitte warten..."
                Obtaining_bad_words = "Erkennen bad words, bitte warten..."
                Checking_for_updates = "Suchen nach Updates, bitte warten..."
                Initialization_complete = "Initialisierung abgeschlossen, bitte log dich ein."
                ' Login attempt
                Your_connection_to_the_server_was_terminated = "Deine Verbindung zum Server wurde beendet."
                Connecting_to_server = "Verbinden zum Server, bitte warten..."
                Unable_to_contact_server = "Es konnte keine Verbindung zum Server hergestellt werden, bitte versuch es später nocheinmal."
                Connected__initializing = "Verbunden, Initialisieren..."
                Authenticating = "Authentisieren, bitte warten..."
                Verification_complete = "Verifizierung abgeschlossen, senden Benutzernamen und Passwort..."

                ' Errors
                Startup_Error = "Fehler beim Starten"
                Login_Error = "Fehler beim Einlogen"
                ' Username/Password is required/contains....
                is_required = "wird benötigt."
                contains_invalid_chars = "Beinhaltet falsche Buchstaben."
                contains_too_many_chars = "Beinhaltet zu viele Buchstaben."
                Please_check_your_internet_connection = "Bitte überprüfe deine Internet Verbindung."
                Error_obtaining_public_IP = "Fehler beim Erkennen der Öffentlichen IP."
                Error_obtaining_server_IP = "Fehler beim Erkennen der Server IP."
                Error_obtaining_announcement = "Fehler beim Erkennen der Ankündigung."
                ' Login attempt
                User_authentication_failed = "Benutzer Authentisierung fehlgeschlagen."
                Account_already_in_use = "Account wird gerade benutzt."
                Account_was_locked_by_an_administrator = "Account wurde von einem Administrator gesperrt."
                Account_already_exists = "Account existiert bereits."
                Account_not_found = "Account wurde nicht gefunden."
            Case Main.Languages.English
                Username = "Username"
                Password = "Password"
                Create_New_User = "Create New User"
                Login = "Login"

                ' Status label updates
                Loading_preferences = "Loading preferences, please wait..."
                Loading_usernames = "Loading usernames, please wait..."
                Obtaining_public_IP = "Obtaining public IP, please wait..."
                Obtaining_server_IP = "Obtaining server IP, please wait..."
                Obtaining_announcement = "Obtaining announcement, please wait..."
                Obtaining_smilies = "Obtaining smilies, please wait..."
                Obtaining_bad_words = "Obtaining bad words, please wait..."
                Checking_for_updates = "Checking for updates, please wait..."
                Initialization_complete = "Initialization complete, please log in."
                ' Login attempt
                Your_connection_to_the_server_was_terminated = "Your connection to the server was terminated."
                Connecting_to_server = "Connecting to server, please wait..."
                Unable_to_contact_server = "Unable to contact server, please try again later."
                Connected__initializing = "Connected, initializing..."
                Authenticating = "Authenticating, please wait..."
                Verification_complete = "Verification complete, sending username and password..."

                ' Errors
                Startup_Error = "Startup Error"
                Login_Error = "Login Error"
                ' Username/Password is required/contains....
                is_required = "is required."
                contains_invalid_chars = "contains invalid characters."
                contains_too_many_chars = "contains too many characters."
                Please_check_your_internet_connection = "Please check your internet connection."
                Error_obtaining_public_IP = "Error obtaining public IP."
                Error_obtaining_server_IP = "Error obtaining server IP."
                Error_obtaining_announcement = "Error obtaining announcement."
                ' Login attempt
                User_authentication_failed = "User authentication failed."
                Account_already_in_use = "Account already in use."
                Account_was_locked_by_an_administrator = "Account was locked by an administrator."
                Account_already_exists = "Account already exists."
                Account_not_found = "Account not found."
            Case Main.Languages.Español
                Username = "Nombre de usuarios"
                Password = "Contraseña"
                Create_New_User = "Crear Nuevo Usuario"
                Login = "Ingresar"

                ' Status label updates
                Loading_preferences = "Cargando Preferencias, por favor espera..."
                Loading_usernames = "Cargando Nombres de Usuario, por favor espera..."
                Obtaining_public_IP = "Obteniendo IP Pública, por favor espera..."
                Obtaining_server_IP = "Obteniendo Servidor de IP, por favor espera..."
                Obtaining_announcement = "Obteniendo Anuncios, por favor espera..."
                Obtaining_smilies = "Obteniendo Imágenes, por favor espera..."
                Obtaining_bad_words = "Obteniendo bad words, por favor espera..."
                Checking_for_updates = "Revisando Actualizaciones, por favor espera..."
                Initialization_complete = "Iniciación Completa, por favor ingresa."
                ' Login attempt
                Your_connection_to_the_server_was_terminated = "Su Conexión Con el Servidor fué terminada"
                Connecting_to_server = "Conectando al Servidor, por favor espera..."
                Unable_to_contact_server = "Incapaz de conectarse con el servidor, por favor intenta de nuevo más tarde."
                Connected__initializing = "Conectado, inicializando..."
                Authenticating = "Autentificando, por favor espera..."
                Verification_complete = "Verificación Completa, enviando nombre de usuario y contraseña..."

                ' Errors
                Startup_Error = "Error de Arranque"
                Login_Error = "Error al Ingresar"
                ' Username/Password is required/contains....
                is_required = "Es Requerido"
                contains_invalid_chars = "Contiene Caracteres (Letras, Números, Símbolos) Inválidos."
                contains_too_many_chars = "Contiene Muchos Caracteres (Letras, Números, Símbolos)."
                Please_check_your_internet_connection = "Por favor Revise Su Conexión a Internet."
                Error_obtaining_public_IP = "Error al Obtener la IP Pública."
                Error_obtaining_server_IP = "Error al Obtener Servidor de IP."
                Error_obtaining_announcement = "Error al Obtener Anuncios."
                ' Login attempt
                User_authentication_failed = "Autentificación de Usuario Fallida."
                Account_already_in_use = "Esta Cuenta Ya está en uso."
                Account_was_locked_by_an_administrator = "La Cuenta Fué Bloqueada Por un Administrador."
                Account_already_exists = "Esta Cuenta ya Existe."
                Account_not_found = "Cuenta No encontrada."
            Case Main.Languages.Français
                Username = "Nom d’Utilisateur"
                Password = "Mot de passe"
                Create_New_User = "Créer nouveau compte"
                Login = "Connexion"

                ' Status label updates
                Loading_preferences = "Chargement des préférences, patienter s’il vous plaît..."
                Loading_usernames = "Chargement des Utilisateurs, patienter s’il vous plaît..."
                Obtaining_public_IP = "Obtention adresse IP publique, patienter s’il vous plaît..."
                Obtaining_server_IP = "Obtention adresse IP serveur, patienter s’il vous plaît..."
                Obtaining_announcement = "Obtention de l’annonce, patienter s’il vous plaît..."
                Obtaining_smilies = "Obtention émoticônes, patienter s’il vous plaît..."
                Obtaining_bad_words = "Obtention bad words, patienter s’il vous plaît..."
                Checking_for_updates = "Vérification des mises à jour, patienter s’il vous plaît..."
                Initialization_complete = "Initialisation complète, connectez vous s’il vous plaît."
                ' Login attempt
                Your_connection_to_the_server_was_terminated = "Votre connexion au serveur a été résilié."
                Connecting_to_server = "Connexion au serveur, s’il vous plaît patientez..."
                Unable_to_contact_server = "Impossible de contacter le serveur, s'il vous plaît essayer à nouveau plus tard."
                Connected__initializing = "Connecté, initialisation..."
                Authenticating = "Authentification, patientez s’il vous plaît..."
                Verification_complete = "Vérification complète, envoie nom d’utilisateur et mot de passe..."

                ' Errors
                Startup_Error = "Erreur de démarrage"
                Login_Error = "Erreur de connexion"
                ' Username/Password is required/contains....
                is_required = "est nécessaire."
                contains_invalid_chars = "contient des caractères incorrects."
                contains_too_many_chars = "contient trop de caractères."
                Please_check_your_internet_connection = "Vérifié votre connexion internet s’il vous plaît."
                Error_obtaining_public_IP = "Erreur à l’obtention de l’IP publique."
                Error_obtaining_server_IP = "Erreur à l’obtention de l’IP du serveur."
                Error_obtaining_announcement = "Erreur à l’obtention d’annonce."
                ' Login attempt
                User_authentication_failed = "Echec de l’authentification de l’utilisateur."
                Account_already_in_use = "Ce compte est déjà en cour d’utilisation."
                Account_was_locked_by_an_administrator = "Ce compte a été verrouillé par un administrateur."
                Account_already_exists = "Ce compte existe déjà."
                Account_not_found = "Ce compte n’a pas été trouvé."
            Case Main.Languages.Nederlands
                Username = "Gebruikersnaam"
                Password = "Wachtwoord"
                Create_New_User = "Maak Account Aan"
                Login = "Login"

                ' Status label updates
                Loading_preferences = "Instellingen worden geladen, even geduld..."
                Loading_usernames = "Gebruikersnamen worden geladen, even geduld..."
                Obtaining_public_IP = "Publieke IP wordt verkregen, even geduld..."
                Obtaining_server_IP = "Server IP wordt opgevraagd, even geduld..."
                Obtaining_announcement = "Mededelingen worden opgehaald, even geduld..."
                Obtaining_smilies = "Smileys worden opgehaald, even geduld..."
                Obtaining_bad_words = "Obtaining bad words, even geduld..."
                Checking_for_updates = "Versie wordt gecontroleerd, even geduld..."
                Initialization_complete = "Initialisatie voltooid, even geduld..."
                ' Login attempt
                Your_connection_to_the_server_was_terminated = "Uw verbinding met de server is verbroken."
                Connecting_to_server = "Verbinden met de server, even geduld..."
                Unable_to_contact_server = "De server kan niet bereikt worden, probeert u het straks opnieuw."
                Connected__initializing = "Verbonden... initialisatie wordt gestart"
                Authenticating = "Verifiëren, even geduld..."
                Verification_complete = "Verificatie voltooid, gebruikersnaam en wachtwoord worden opgestuurd."

                ' Errors
                Startup_Error = "Opstart Error"
                Login_Error = "Login Error"
                ' Username/Password is required/contains....
                is_required = "is vereist."
                contains_invalid_chars = "bevat illegale tekens."
                contains_too_many_chars = "bevat te veel tekens."
                Please_check_your_internet_connection = "Controleer uw verbinding."
                Error_obtaining_public_IP = "Error bij het verkrijgen van het publieke IP."
                Error_obtaining_server_IP = "Error bij het verkrijgen van het IP van de server."
                Error_obtaining_announcement = "Error bij het verkrijgen van de mededelingen"
                ' Login attempt
                User_authentication_failed = "Gebruikers verificatie niet voltooid."
                Account_already_in_use = "Dit account wordt reeds gebruikt."
                Account_was_locked_by_an_administrator = "Deze account is gelocked door de administrator."
                Account_already_exists = "Deze gebruikersnaam bestaat al."
                Account_not_found = "Gebruikersnaam niet gevonden."
            Case Main.Languages.Português
                Username = "Nome dos usuários"
                Password = "Senha"
                Create_New_User = "Criar Novo Usuário"
                Login = "Entrar"

                ' Status label updates
                Loading_preferences = "Carregando Preferências, por favor espera..."
                Loading_usernames = "Carregando Nomes dos Usuarios, por favor espera..."
                Obtaining_public_IP = "Obtendo IP Pública, por favor espera..."
                Obtaining_server_IP = "Obtendo Servidor de IP, por favor espera..."
                Obtaining_announcement = "Obtendo Anúncios, por favor espera..."
                Obtaining_smilies = "Obtendo Imagens, por favor espera..."
                Obtaining_bad_words = "Obtendo bad words, por favor espera..."
                Checking_for_updates = "Procurando Actualizações, por favor espera..."
                Initialization_complete = "Iniciação Completa, por favor entra."
                ' Login attempt
                Your_connection_to_the_server_was_terminated = "Sua conexão com o Servidor foi terminada"
                Connecting_to_server = "Conectando ao Servidor, por favor espera..."
                Unable_to_contact_server = "Incapaz de conectar-se com o Servidor, por favor tenta de novo mais tarde."
                Connected__initializing = "Conectado, inicializando..."
                Authenticating = "Autentificando, por favor espera..."
                Verification_complete = "Verificação Completa, enviando nome do usuário e senha..."

                ' Errors
                Startup_Error = "Erro de Arranque"
                Login_Error = "Erro ao Entrar"
                ' Username/Password is required/contains....=Usuário/Senha é requerida/contém....
                is_required = "É requerido"
                contains_invalid_chars = "Contém Caracteres (Letras, Números, Símbolos) Inválidos."
                contains_too_many_chars = "Contém Muitos Caracteres (Letras, Números, Símbolos)."
                Please_check_your_internet_connection = "Por favor procure sua conexião à Internet."
                Error_obtaining_public_IP = "Erro ao obter o IP Público."
                Error_obtaining_server_IP = "Erro ao obter Servidor de IP."
                Error_obtaining_announcement = "Erro ao obter anúncios."
                ' Login attempt
                User_authentication_failed = "Falha na autentificação do Usuário"
                Account_already_in_use = "Esta conta já está en uso."
                Account_was_locked_by_an_administrator = "A conta foi bloqueada por um Administrador."
                Account_already_exists = "Esta conta já existe."
                Account_not_found = "Conta não encontrada."
            Case Main.Languages.Suomi
                Username = "Käyttäjänimi"
                Password = "Salasana"
                Create_New_User = "Luo Uusi Käyttäjä"
                Login = "Kirjaudu Sisään"

                ' Status label updates
                Loading_preferences = "Ladataan preferenssejä, odota..."
                Loading_usernames = "Ladataan käyttäjänimiä, odota..."
                Obtaining_public_IP = "Haetaan julkista IP:tä, odota..."
                Obtaining_server_IP = "Haetaan palvelimen IP:tä, odota..."
                Obtaining_announcement = "Haetaan ilmoitusta, odota..."
                Obtaining_smilies = "Haetaan hymiöitä, odota..."
                Obtaining_bad_words = "Haetaan bad words, odota..."
                Checking_for_updates = "Tarkistetaan päivityksiä, odota..."
                Initialization_complete = "Valmis, voit kirjautua sisään."
                ' Login attempt
                Your_connection_to_the_server_was_terminated = "Yhteytesi palvelimeen katkesi"
                Connecting_to_server = "Yhdistetään palvelimelle, odota..."
                Unable_to_contact_server = "Palvelimeen yhdistäminen epäonnistui, yritä myöhemmin uudelleen."
                Connected__initializing = "Yhdistetty, valmistellaan..."
                Authenticating = "Varmistetaan, odota..."
                Verification_complete = "Varmennus valmis, lähetetään käyttäjänimi ja salasana..."

                ' Errors
                Startup_Error = "Käynnistysvirhe"
                Login_Error = "Kirjautumisvirhe"
                ' Username/Password is required/contains....
                is_required = "vaaditaan."
                contains_invalid_chars = "sisältää ei tuettuja merkkejä."
                contains_too_many_chars = "sisältää liian monta merkkiä."
                Please_check_your_internet_connection = "Ole hyvä ja tarkasta internet-yhteytesi."
                Error_obtaining_public_IP = "Virhe haettaessa julkista IP:tä."
                Error_obtaining_server_IP = "Virhe haettaessa palvelimen IP:tä."
                Error_obtaining_announcement = "Virhe haettaessa ilmoitusta."
                ' Login attempt
                User_authentication_failed = "Käyttäjän varmistaminen epäonnistui."
                Account_already_in_use = "Käyttäjänimi on jo käytössä."
                Account_was_locked_by_an_administrator = "Käyttäjätili suljettu ylläpitäjän toimesta."
                Account_already_exists = "Käyttäjänimi on varattu."
                Account_not_found = "Käyttäjää ei löydetty."
            Case Else
                SetLanguage(Main.Languages.English)

        End Select
    End Sub
End Class
