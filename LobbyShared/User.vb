Public Class User
    Enum SecurityGroups
        User
        Donator
        Moderator
        SuperModerator
        Administrator
    End Enum

    Public Property Username As String = ""
    Public Property Password As String = ""

    Public Property Security As SecurityGroups = SecurityGroups.User

    Public Property PublicIP As String = ""

    Public Property InGameEE As Boolean = False
    Public Property InGameAoC As Boolean = False
    Public Property AFK As Boolean = False

    Public Function GetStatus() As String
        If AFK Then
            If Security = SecurityGroups.Donator Then
                Return "AFK_add.png"
            Else
                Return "AFK.png"
            End If
        ElseIf InGameEE AndAlso InGameAoC Then
            If Security = SecurityGroups.Donator Then
                Return "eeboth_add.png"
            Else
                Return "eeboth.png"
            End If
        ElseIf InGameEE Then
            If Security = SecurityGroups.Donator Then
                Return "ee_add.png"
            Else
                Return "ee.png"
            End If
        ElseIf InGameAoC Then
            If Security = SecurityGroups.Donator Then
                Return "eeaoc_add.png"
            Else
                Return "eeaoc.png"
            End If
        Else
            If Security = SecurityGroups.Administrator Then
                Return "Admin.png"
            ElseIf Security = SecurityGroups.SuperModerator Then
                Return "Moderator.png"
            ElseIf Security = SecurityGroups.Moderator Then
                Return "Moderator.png"
            ElseIf Security = SecurityGroups.Donator Then
                Return "donator.png"
            Else
                Return "user.png"
            End If
        End If
    End Function
    Public Function GetGroup() As String
        Return If(Security = SecurityGroups.SuperModerator, "MODERATOR", Security.ToString.ToUpper)
    End Function
End Class
