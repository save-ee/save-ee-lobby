Public Class Globals
    Public Shared WithEvents PatcherForm As FormPatch
    Public Shared PatchEngine As New PatchEngine
    Public Shared ClientEngine As New ClientEngine

    Public Shared AppDataEnvVar As String = System.Environment.GetEnvironmentVariable("APPDATA")
    Public Shared ProcessPath As String = Process.GetCurrentProcess.MainModule.FileName
    Public Shared ProcessFolder As String = AppDomain.CurrentDomain.BaseDirectory

    Public Shared ServerIP As String = ""
    Public Shared Announce As String = ""

    Public Shared DefaultAdapter As String = ""
    Public Shared CurrentUser As New LobbyShared.User

    Public Shared UserAccounts As New Dictionary(Of String, String)
    Public Shared HardwareList As New Dictionary(Of String, String)
    Public Shared Smileys As New Dictionary(Of String, String)
    Public Shared BadWords As New Dictionary(Of String, String)

    Public Shared HackerMode As Boolean = False
    Public Shared DebugMode As Boolean = False
End Class