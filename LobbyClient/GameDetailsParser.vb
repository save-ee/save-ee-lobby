Public Class GameDetailsParser
    Public Enum GameType
        EEC
        AOC
        UNKNOWN
    End Enum
    Public Shared Function GetStartingResources(ByVal startingResourcesNumber As Integer, ByVal game As GameType) As String
       Select startingResourcesNumber
            Case 0
                Return Language.GamesList.Tournament_Low & " (TL)"
            Case 1
                Return Language.GamesList.Tournament_Defensive & " (TD)"
            Case 2
                Return Language.GamesList.Standard_Low & " (SL)"
            Case 3
                Return Language.GamesList.Standard_High & " (SH)"
            Case 4
                Return Language.GamesList.Deathmatch & " (DM)"
            Case Else
                Return Language.Main.Unknown
        End Select
    End Function
    Public Shared Function GetMapType(ByVal mapTypeNumber As Integer, ByVal game As GameType) As String
        If game = GameType.EEC Then
            Select Case mapTypeNumber
                Case 0
                    Return Language.GamesList.Continental
                Case 1
                    Return Language.GamesList.Highlands
                Case 2
                    Return Language.GamesList.Large_Islands
                Case 3
                    Return Language.GamesList.Mediterranean
                Case 4
                    Return Language.GamesList.Plains
                Case 5
                    Return Language.GamesList.Small_Islands
                Case 6
                    Return "Tortured Rivers"
                Case 7
                    Return Language.GamesList.Tournament_Islands
                Case 8
                    Return "Tunisia Oasis"
                Case 9
                    Return "Tweek My CA Micro"
                Case 10
                    Return "Twisted For Grens"
                Case 11
                    Return "Uniquely Random"
                Case 12
                    Return "Vicious Isthmi"
                Case 13
                    Return "X"
                Case 14
                    Return "Zeus"
                Case 15
                    Return "zBG_Death Gulch"
                Case 16
                    Return "zContients-DrOrange"
                Case 17
                    Return "zRandom Islands"
                Case 18
                    Return "zRandom Land"
                Case Else
                    Return Language.Main.Unknown
            End Select
        ElseIf game = GameType.AOC Then
            Select Case mapTypeNumber
                Case 0
                    Return Language.GamesList.Continental
                Case 1
                    Return Language.GamesList.Highlands
                Case 2
                    Return Language.GamesList.Large_Islands
                Case 3
                    Return Language.GamesList.Mediterranean
                Case 4
                    Return Language.GamesList.Plains
                Case 5
                    Return Language.GamesList.Planets_Earth
                Case 6
                    Return Language.GamesList.Planets_Large
                Case 7
                    Return Language.GamesList.Planets_Mars
                Case 8
                    Return Language.GamesList.Planets_Satellite
                Case 9
                    Return Language.GamesList.Planets_Small
                Case 10
                    Return Language.GamesList.Small_Islands
                Case 11
                    Return "Tortured Rivers"
                Case 12
                    Return Language.GamesList.Tournament_Islands
                Case 13
                    Return "Tunisia Oasis"
                Case 14
                    Return "Tweek My CA Micro"
                Case 15
                    Return "Twisted For Grens"
                Case 16
                    Return "Uniquely Random"
                Case 17
                    Return "Vicious Isthmi"
                Case 18
                    Return "X"
                Case 19
                    Return "Zeus"
                Case 20
                    Return "zBG_Death Gulch"
                Case 21
                    Return "zContinental - Space"
                Case 22
                    Return "zContients-DrOrange"
                Case 23
                    Return "zMediterranean - Space"
                Case 24
                    Return "zRandom Islands"
                Case 25
                    Return "zRandom Land"
                Case 26
                    Return "zRandom Space Islands"
                Case Else
                    Return Language.Main.Unknown
            End Select
        Else
            Return Language.Main.Unknown
        End If
    End Function
    Public Shared Function GetMapSize(ByVal mapSizeNumber As String, ByVal game As GameType) As String
        mapSizeNumber = Trim(mapSizeNumber)
        Select Case Integer.Parse(mapSizeNumber)
            Case 0
                Return Language.GamesList.Tiny
            Case 1
                Return Language.GamesList.Small
            Case 2
                Return Language.GamesList.Medium
            Case 3
                Return Language.GamesList.Large
            Case 4
                Return Language.GamesList.Huge
            Case 5
                Return Language.GamesList.Gigantic
            Case Else
                Return Language.Main.Unknown
        End Select
    End Function
    Public Shared Function GetEpochName(ByVal epochNumber As Integer, ByVal game As GameType) As String
        Select Case epochNumber
            Case 0
                Return Language.GamesList.Prehistoric_Age
            Case 1
                Return Language.GamesList.Stone_Age
            Case 2
                Return Language.GamesList.Copper_Age
            Case 3
                Return Language.GamesList.Bronze_Age
            Case 4
                Return Language.GamesList.Dark_Age
            Case 5
                Return Language.GamesList.Middle_Ages
            Case 6
                Return Language.GamesList.Renaissance
            Case 7
                Return Language.GamesList.Imperial_Age
            Case 8
                Return Language.GamesList.Industrial_Age
            Case 9
                Return Language.GamesList.Atomic_Age_WW1
            Case 10
                Return Language.GamesList.Atomic_Age_WW2
            Case 11
                Return Language.GamesList.Atomic_Age_Modern
            Case 12
                Return Language.GamesList.Digital_Age
            Case 13
                Return Language.GamesList.Nano_Age
            Case 14
                If game = GameType.EEC Then
                    Return Language.GamesList.Random_Epoch
                Else
                    Return Language.GamesList.Space_Age
                End If
            Case 15
                Return Language.GamesList.Random_Epoch
            Case Else
                Return Language.Main.Unknown
        End Select
    End Function
    ''' <summary>
    ''' Determines the epoch number given the name of the epoch in any of the supported languages.  Random Epoch is always returned as 16.
    ''' </summary>
    ''' <param name="epochName">The epoch name as a string in any supported language.</param>
    ''' <returns>The true epoch number as an integer.  Zero if unknown.</returns>
    ''' <remarks></remarks>
    Public Shared Function GetEpochNumber(ByVal epochName As String) As Integer
        Dim ret As Integer = 0
        For Each lang As Integer In [Enum].GetValues(GetType(Language.Main.Languages))
            Language.Main.SetLanguage(lang)
            Select Case epochName
                Case Language.GamesList.Prehistoric_Age
                    ret = 1
                Case Language.GamesList.Stone_Age
                    ret = 2
                Case Language.GamesList.Copper_Age
                    ret = 3
                Case Language.GamesList.Bronze_Age
                    ret = 4
                Case Language.GamesList.Dark_Age
                    ret = 5
                Case Language.GamesList.Middle_Ages
                    ret = 6
                Case Language.GamesList.Renaissance
                    ret = 7
                Case Language.GamesList.Imperial_Age
                    ret = 8
                Case Language.GamesList.Industrial_Age
                    ret = 9
                Case Language.GamesList.Atomic_Age_WW1
                    ret = 10
                Case Language.GamesList.Atomic_Age_WW2
                    ret = 11
                Case Language.GamesList.Atomic_Age_Modern
                    ret = 12
                Case Language.GamesList.Digital_Age
                    ret = 13
                Case Language.GamesList.Nano_Age
                    ret = 14
                Case Language.GamesList.Space_Age
                    ret = 15
                Case Language.GamesList.Random_Epoch
                    ret = 16
                Case Else
                    ' Try next language
            End Select
            If ret <> 0 Then Exit For
        Next
        Language.Main.SetLanguage(FormLobby.LanguageComboBox.SelectedIndex)
        Return ret
    End Function
End Class