Imports System.Runtime.InteropServices

Class AdminCheck

#Region "CONSTANTS"
    Const TOKEN_QUERY As UInt32 = &H8
    Const INT_SIZE As Integer = 4
#End Region
#Region "ENUMERATIONS"
    Private Enum TOKEN_ELEVATION_TYPE
        TokenElevationTypeDefault = 1
        TokenElevationTypeFull
        TokenElevationTypeLimited
    End Enum
    Private Enum TOKEN_INFO_CLASS
        TokenUser = 1
        TokenGroups
        TokenPrivileges
        TokenOwner
        TokenPrimaryGroup
        TokenDefaultDacl
        TokenSource
        TokenType
        TokenImpersonationLevel
        TokenStatistics
        TokenRestrictedSids
        TokenSessionId
        TokenGroupsAndPrivileges
        TokenSessionReference
        TokenSandBoxInert
        TokenAuditPolicy
        TokenOrigin
        TokenElevationType
        TokenLinkedToken
        TokenElevation
        TokenHasRestrictions
        TokenAccessInformation
        TokenVirtualizationAllowed
        TokenVirtualizationEnabled
        TokenIntegrityLevel
        TokenUIAccess
        TokenMandatoryPolicy
        TokenLogonSid
        MaxTokenInfoClass
        ' MaxTokenInfoClass should always be the last enum
    End Enum
#End Region
#Region "WIN API FUNCTIONS"
    <DllImport("kernel32.dll")> _
        Private Shared Function GetCurrentProcess() As IntPtr
    End Function
    <DllImport("advapi32.dll", SetLastError:=True)> _
        Private Shared Function OpenProcessToken(ByVal ProcessHandle As IntPtr, _
                                                    ByVal DesiredAccess As UInt32, _
                                                    ByRef TokenHandle As IntPtr) As Boolean
    End Function
    <DllImport("advapi32.dll", SetLastError:=True)> _
        Private Shared Function GetTokenInformation( _
                                    ByVal TokenHandle As IntPtr, _
                                    ByVal TokenInformationClass As TOKEN_INFO_CLASS, _
                                    ByVal TokenInformation As IntPtr, _
                                    ByVal TokenInformationLength As Integer, _
                                    ByRef ReturnLength As UInteger) As Boolean
    End Function
#End Region
#Region "PUBLIC METHODS"

    ''' <summary>
    ''' RETURNS TRUE WHEN THE CURRENT USER IS A MEMBER OF THE
    ''' ADMINISTRATORS GROUP AND IS ALSO RUNNING THE PROCESS
    ''' ELEVATED AS AN ADMINISTRATOR, OTHERWISE RETURNS FALSE.
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Shared Function IsRunningAsAdmin() As Boolean
        Return My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator)
    End Function

    ''' <summary>
    ''' RETURNS TRUE WHEN THE CURRENT USER CAN ELEVATE TO ADMINISTRATOR RIGHTS
    ''' OR ALREADY HAD ELEVATED TO ADMINISTRATOR RIGHTS,
    ''' OTHERWISE RETURNS FALSE
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Shared Function CanElevateToAdmin() As Boolean
        'DETERMINE IF THE CURRENT USER IS ALREADY RUNNING WITH ADMIN RIGHTS
        Dim IsAdmin = IsRunningAsAdmin()

        'EXIT OUT NOW IF THE USER IS AN ADMIN, RETURNING TRUE
        'THERE IS NO ELEVATION NEEDED IF THE USER IS ALREADY
        'RUNNING WITH ADMIN RIGHTS
        If IsAdmin Then Return True

        'IF VISTA OR HIGHER, CHECK FOR SPLIT TOKEN
        If Environment.OSVersion.Version.Major > 5 Then
            Try
                Dim myToken As IntPtr
                Dim elevationType As TOKEN_ELEVATION_TYPE
                Dim dwSize As UInteger
                Dim pElevationType As IntPtr = Marshal.AllocHGlobal(INT_SIZE)

                'GET A TOKEN REFERENCE FOR THE USER RUNNING THIS PROCESS
                OpenProcessToken(GetCurrentProcess(), TOKEN_QUERY, myToken)

                'GET THE ELEVATION INFORMATION FOR THIS TOKEN
                GetTokenInformation(myToken, _
                    TOKEN_INFO_CLASS.TokenElevationType, _
                    pElevationType, _
                    INT_SIZE, _
                    dwSize)



                'CAST THE RESULT TO ENUM TYPE
                elevationType = DirectCast( _
                    Marshal.ReadInt32(pElevationType),  _
                    TOKEN_ELEVATION_TYPE)

                'FREE ALLOCATED UNMANAGED MEMORY
                Marshal.FreeHGlobal(pElevationType)

                'DETERMINE THE RESULT OF THE ELEVATION CHECK
                '==============================================
                'TokenElevationTypeFull - User has a split token,
                'and the process is running elevated

                'TokenElevationTypeLimited - User has a split token,
                'but the process is not running elevated

                'TokenElevationTypeDefault - User is not using a split token
                '==============================================
                Return (elevationType = _
                        TOKEN_ELEVATION_TYPE.TokenElevationTypeLimited) OrElse _
                       (elevationType = _
                        TOKEN_ELEVATION_TYPE.TokenElevationTypeFull)

            Catch ex As Exception
                'LOG/HANDLE ERROR
                'RETURN FALSE IN EVENT OF ERROR
                Return False
            End Try
        Else
            'PRIOR TO VISTA, ONLY CHECK
            'NEEDED IS IF THE USER IS IN THE ADMIN GROUP
            Return IsAdmin
        End If
    End Function

    ''' <summary>
    ''' RETURNS TRUE WHEN USER IS RUNNING AS A STANDARD USER CURRENTLY
    ''' BUT CAN ELEVATE TO ADMIN RIGHTS WITH THEIR OWN CREDENTIALS
    ''' OTHERWISE RETURNS FALSE
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Shared Function RunningStandardButCanElevate() As Boolean
        Return CanElevateToAdmin() And (Not IsRunningAsAdmin())
    End Function
#End Region
End Class
