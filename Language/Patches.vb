Public Class Patches
    ' On the patch screen
    Public Shared Install_Directories As String = ""
    Public Shared Direct_Connect_Patch As String = ""

    Public Shared Install_Dir_Label As String = ""
    Public Shared Autodetect_Install_Directories As String = ""
    Public Shared Empire_Earth_Install_Dir As String = ""
    Public Shared Art_of_Conquest_Install_Dir As String = ""
    Public Shared Browse As String = ""

    Public Shared Direct_Conn_Label As String = ""
    Public Shared Apply_Direct_Connect_Patch As String = ""

    ' In result windows
    Public Shared Autodetect_Results As String = ""
    Public Shared Unable_to_locate_EE As String = ""
    Public Shared Unable_to_locate_AoC As String = ""

    Public Shared Please_locate_EE As String = ""
    Public Shared Please_locate_AoC As String = ""

    Public Shared DC_Patch_Results As String = ""
    Public Shared DC_EE_Success As String = ""
    Public Shared DC_EE_Fail As String = ""
    Public Shared DC_AoC_Success As String = ""
    Public Shared DC_AoC_Fail As String = ""


    Public Shared Sub SetLanguage(ByVal lang As Main.Languages)
        Select Case lang
            Case Main.Languages.Deutsch
                ' On the patch screen
                Install_Directories = "Verzeichnisse installieren"
                Direct_Connect_Patch = "Direktverbindungspatch"

                Install_Dir_Label = "Der Lobbyklient kann versuchen, die Installationspfade der Spiele automatisch zu finden. Wenn das fehlschlägt, muss manuell danach gesucht werden.  Installationspfade sind für jeden Patch nötig."
                Autodetect_Install_Directories = "Installationspfade automatisch finden"
                Empire_Earth_Install_Dir = "Empire Earth Installationspfad:"
                Art_of_Conquest_Install_Dir = "Art of Conquest Installationspfad:"
                Browse = "Suche..."

                Direct_Conn_Label = "Dieser Patch behebt den Fehler ""General Failure (Code: WS_ServerReq_NoServersSpecified)"" und macht es möglich, Spiele über Direktverbindung zu spielen."
                Apply_Direct_Connect_Patch = "Führe Direktverbindungspatch aus"

                ' In result windows
                Autodetect_Results = "Ergebnisse automatisch finden"
                Unable_to_locate_EE = "Nicht in der Lage, das Empire Earth Installationsverzeichnis automatisch zu finden."
                Unable_to_locate_AoC = "Nicht in der Lage, das Art of Conquest Installationsverzeichnis automatisch zu finden."

                Please_locate_EE = "Bitte finde und wähle den Empire Earth Installationsordner aus."
                Please_locate_AoC = "Bitte finde und wähle den Art of Conquest Installationsordner aus."

                DC_Patch_Results = "Direktverbindungspatch Ergebnisse"
                DC_EE_Success = "Der Empire Earth Direktverbindungspatch wurde erfolgreich ausgeführt"
                DC_EE_Fail = "Nicht in der Lage, WONLobby.cfg im angegebenen Empire Earth Ordner zu finden"
                DC_AoC_Success = "Der Art of Conquest Direktverbindungspatch wurde erfolgreich ausgeführt."
                DC_AoC_Fail = "Nicht in der Lage, WONLobby.cfg im angegebenen Art of Conquest Ordner zu finden"
            Case Main.Languages.English
                ' On the patch screen
                Install_Directories = "Install Directories"
                Direct_Connect_Patch = "Direct Connect Patch"

                Install_Dir_Label = "The lobby client can attempt to automatically detect your game install directories.  If it is unable to locate them, you may manually browse for the folders.  Install directories are required to apply any patches."
                Autodetect_Install_Directories = "Autodetect Install Directories"
                Empire_Earth_Install_Dir = "Empire Earth Install Directory:"
                Art_of_Conquest_Install_Dir = "Art of Conquest Install Directory:"
                Browse = "Browse..."

                Direct_Conn_Label = "This patch resolves the error ""General Failure (Code: WS_ServerReq_NoServersSpecified)"" and will allow you to play games via Direct Connect."
                Apply_Direct_Connect_Patch = "Apply Direct Connect Patch"

                ' In result windows
                Autodetect_Results = "Autodetect Results"
                Unable_to_locate_EE = "Unable to automatically locate the Empire Earth install directory."
                Unable_to_locate_AoC = "Unable to automatically locate the Art of Conquest install directory."

                Please_locate_EE = "Please locate and select your Empire Earth installation folder."
                Please_locate_AoC = "Please locate and select your Art of Conquest installation folder."

                DC_Patch_Results = "Direct Connect Patch Results"
                DC_EE_Success = "Empire Earth direct connect patch successful."
                DC_EE_Fail = "Unable to locate WONLobby.cfg in the specified Empire Earth directory."
                DC_AoC_Success = "Art of Conquest direct connect patch successful."
                DC_AoC_Fail = "Unable to locate WONLobby.cfg in the specified Art of Conquest directory."
            Case Main.Languages.Español
                ' On the patch screen
                Install_Directories = "Instalar Directorios"
                Direct_Connect_Patch = "Conexión Directa al Parche"

                Install_Dir_Label = "El Cliente del lobby puede intentar de manera automática detectar la opción para instalar directorios.  Si el Lobby esta desconectado para realizar esta función, usted manualmente lo podrá examinar desde sus carpetas de archivos.  Instalar los directorios es requerido para la aplicación de los diferentes parches."
                Autodetect_Install_Directories = "Auto detectar para instalar directorios"
                Empire_Earth_Install_Dir = "Instalar Directorios Empire Earth:"
                Art_of_Conquest_Install_Dir = "Instalar Directorios Art of Conquest:"
                Browse = "Examinar..."

                Direct_Conn_Label = "Este Parche genera el error ""General Failure (Code: WS_ServerReq_NoServersSpecified)"" Y te permitirá jugar partidas (juegos) vía conexión directa."
                Apply_Direct_Connect_Patch = "Aplicar Conexión Directa del Parche"

                ' In result windows
                Autodetect_Results = "Auto detectar Resultado"
                Unable_to_locate_EE = "Desconectar Automáticamente la Ubicación de la instalación del directorio de Empire Earth."
                Unable_to_locate_AoC = " Desconectar Automáticamente la Ubicación de la instalación del directorio de Art of Conquest."
                Please_locate_EE = "Por favor Ubique y Seleccione La carpeta para la instalación de Empire Earth."
                Please_locate_AoC = " Por favor Ubique y Seleccione La carpeta para la instalación de Art of Conquest."

                DC_Patch_Results = "Resultado de Conexión Directa del Parche de Empire Earth"
                DC_EE_Success = "Conexión exitosa del Parche de Empire Earth."
                DC_EE_Fail = "Desconectar la ubicación de WONLobby.cfg en el Directorio de Empire Earth Específico."
                DC_AoC_Success = " Resultado de Conexión Directa del Parche de Art of Conquest."
                DC_AoC_Fail = " Desconectar la ubicación de WONLobby.cfg en el Directorio de Art of Conquest Específico."
            Case Main.Languages.Français
                ' On the patch screen
                Install_Directories = "Installation des répertoires"
                Direct_Connect_Patch = "Patch connexion directe"

                Install_Dir_Label = "Le client Hall peut tenter de détecter automatiquement votre répertoire d'installation de jeu. S’il est incapable de le localiser, vous pouvez parcourir manuellement vos dossiers. Les dossiers d'installation sont nécessaires à l'application des patches."
                Autodetect_Install_Directories = "Lancer la détection automatique"
                Empire_Earth_Install_Dir = "Répertoire d’installation Empire Earth:"
                Art_of_Conquest_Install_Dir = "Répertoire d’installation Art of Conquest:"
                Browse = "Parcourir..."

                Direct_Conn_Label = "Ce patch corrige l'erreur ""General Failure (Code: WS_ServerReq_NoServersSpecified)"" et vous permettra de jouer via la connexion Directe."
                Apply_Direct_Connect_Patch = "Appliquer le patch connexion directe"

                ' In result windows
                Autodetect_Results = "Résultat de l’auto-détection"
                Unable_to_locate_EE = "Impossible de localiser automatiquement le répertoire d’installation de Empire Earth."
                Unable_to_locate_AoC = "Impossible de localiser automatiquement le répertoire d’installation de Art Of Conquest."

                Please_locate_EE = "Veuillez localiser et sélectionner le dossier d’installation Empire Earth."
                Please_locate_AoC = " Veuillez localiser et sélectionner le dossier d’installation Art of Conquest."

                DC_Patch_Results = "Résultat du Patch connexion directe"
                DC_EE_Success = "Le patch connexion directe de Empire Earth est opérationnel."
                DC_EE_Fail = "Impossible de localiser WONLobby.cfg dans le répertoire d’Empire Earth spécifié."
                DC_AoC_Success = "Le patch connexion directe de Art of Conquest direct est installé."
                DC_AoC_Fail = "Impossible de localiser WONLobby.cfg dans le répertoire d’Art of Conquest spécifié."
            Case Main.Languages.Nederlands
                ' On the patch screen
                Install_Directories = "Installatie map"
                Direct_Connect_Patch = "Direct Connect Patch"

                Install_Dir_Label = "Het lobby programma kan proberen om de installatiemap automatisch te vinden. Als het programma de mappen niet kan vinden kan je de map handmatig selecteren. De installatiemap is benodigd om de patch te installeren."
                Autodetect_Install_Directories = "Automatische detectie installatiemap"
                Empire_Earth_Install_Dir = "Empire Earth installatiemap:"
                Art_of_Conquest_Install_Dir = "Art of Conquest installatiemap:"
                Browse = "Browse..."

                Direct_Conn_Label = "Deze patch lost de volgende error op: ""General Failure (Code: WS_ServerReq_NoServersSpecified)"" en stelt je in staat om te spelen via Direct Connect."
                Apply_Direct_Connect_Patch = "Pas Direct Connect Patch Toe"

                ' In result windows
                Autodetect_Results = "Automatische resultaten detectie"
                Unable_to_locate_EE = "Het programma is niet in staat om de EE installatiemap automatisch te localiseren."
                Unable_to_locate_AoC = "Het programma is niet in staat om de EE-AOC installatiemap automatisch te localiseren."

                Please_locate_EE = "Selecteer de installatiemap van Empire Earth."
                Please_locate_AoC = "Selecteer de installatiemap van Empire Earth the Art of Conquest."

                DC_Patch_Results = "Direct Connect patch resultaten"
                DC_EE_Success = "De Empire Earth direct connect patch is voltooid."
                DC_EE_Fail = "Het programma heeft WONLobby.cfg niet gevonden in de gespecificeerde map."
                DC_AoC_Success = "Art of Conquest direct connect patch is voltooid."
                DC_AoC_Fail = "Het programma heeft WONLobby.cfg niet gevonden in de gespecificeerde map."
            Case Main.Languages.Português
                ' On the patch screen
                Install_Directories = "Instalar Directórios"
                Direct_Connect_Patch = "Conexão Directa ao Patch"

                Install_Dir_Label = "O Cliente da Portaria pode tentar de maneira automática detectar a opção para instalar directórios. Se a Portaria estiver inactiva para realizar esta função, pode manualmente examinar desde suas pastas de arquivos. Instalar os directórios é requerido para a aplicação dos diferentes patchs."
                Autodetect_Install_Directories = "Auto detectar para instalar directórios"
                Empire_Earth_Install_Dir = "Instalar Directórios Empire Earth:"
                Art_of_Conquest_Install_Dir = "Instalar Directórios Art of Conquest:"
                Browse = "Examinar..."

                Direct_Conn_Label = "Este Patch corrige o erro ""General Failure (Code: WS_ServerReq_NoServersSpecified)"" Y e permitirá jogar partidas (jogos) via conexão directa."
                Apply_Direct_Connect_Patch = "Aplicar Conexão Directa ao Patch"

                ' In result windows
                Autodetect_Results = "Auto detectar Resultado"
                Unable_to_locate_EE = "Desconectar Automáticamente a localização da instalação do directório de Empire Earth."
                Unable_to_locate_AoC = " Desconectar Automaticamente a localização da instalação do Art of Conquest"
                Please_locate_EE = "Por favor localize e selecione a pasta para a instalação do Empire Earth."
                Please_locate_AoC = " Por favor localize e selecione a pasta para a instalação de Art of Conquest."

                DC_Patch_Results = "Resultado da Conexão Directa do Patch do Empire Earth"
                DC_EE_Success = "Sucesso na Conexão do Patch do Empire Earth."
                DC_EE_Fail = "Falha na localização do WONLobby.cfg no Directório do Empire Earth Específico."
                DC_AoC_Success = "Sucesso na Conexão Directa do Patch do Art of Conquest."
                DC_AoC_Fail = " Falha na localização do WONLobby.cfg no Directório do Art of Conquest Específico."
            Case Main.Languages.Suomi
                ' On the patch screen
                Install_Directories = "Asennuskansio"
                Direct_Connect_Patch = "Direct Connect Päivitys"

                Install_Dir_Label = "Lobby client yrittää löytää Empire Earthin asennuskansion automaattisesti. Jos se ei löydä asennuskansiota, voit manuaalisesti etsiä sen. Asennuskansio tarvitaan, jotta päivityksen pystyy asentamaan."
                Autodetect_Install_Directories = "Etsi Asennuskansio Automaattisesti"
                Empire_Earth_Install_Dir = "Empire Earthin Asennuskansio:"
                Art_of_Conquest_Install_Dir = "Art of Conquestin Asennuskansio:"
                Browse = "Etsi..."

                Direct_Conn_Label = "Tämä päivitys korjaa virhekoodin ""General Failure (Code: WS_ServerReq_NoServersSpecified)"" ja mahdollistaa Direct Connectin kautta pelaamisen."
                Apply_Direct_Connect_Patch = "Asenna Direct Connect Päivitys"

                ' In result windows
                Autodetect_Results = "Tunnista Tulokset Automaattisesti"
                Unable_to_locate_EE = "Empire Earthin asennuskansiota ei löydetty automaattisesti."
                Unable_to_locate_AoC = "Art of Conquestin asennuskansiota ei löydetty automaattisesti."

                Please_locate_EE = "Ole hyvä ja valitse Empire Earthin asennuskansio."
                Please_locate_AoC = "Ole hyvä ja valitse Art of Conquestin asennuskansio."

                DC_Patch_Results = "Direct Connect Päivityksen Tulokset"
                DC_EE_Success = "Empire Earthin direct connect päivitys onnistui."
                DC_EE_Fail = "Ei löydetty WONLobby.cfg kyseisestä Empire Earthin kansiosta."
                DC_AoC_Success = "Art of Conquestin direct connect päivitys onnistui."
                DC_AoC_Fail = "Ei löydetty WONLobby.cfg kyseisestä Art of Conquestin kansiosta."
            Case Else
                SetLanguage(Main.Languages.English)
        End Select
    End Sub
End Class
