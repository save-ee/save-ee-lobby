Imports System.Windows.Forms
Imports System.Data

Public Class AccessDatabase
    Inherits Database
#Region "New Constructor"
    Public Sub New()
    End Sub
    Public Sub New(ByVal Path As String)
        Me.DatabasePath = Path
        Me.OpenDatabase()
    End Sub
    Public Sub New(ByVal OpenConnection As Odbc.OdbcConnection)
        Connection = OpenConnection
    End Sub
    Public Sub New(ByVal OpenConnection As AccessDatabase)
        Connection = OpenConnection.Connection
    End Sub
#End Region
#Region "Open / Close Databases"
    Public Sub OpenDatabase()
        'ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DatabasePath
        'ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db.mdb;User Id=admin;Password=;"
        ConnectionString = "Driver={Microsoft Access Driver (*.mdb)};Dbq=" & Me.DatabasePath & ";Uid=Admin;Pwd=;"
        Connection = New Odbc.OdbcConnection(ConnectionString)
        Try
            Connection.Open()
        Catch ex As Exception
            'NeedsImports System.Windows.Forms
            'Clipboard.SetText(ex.Message)
        End Try
    End Sub
    Public Sub CloseDatabase()
        Try
            Connection.Close()
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class