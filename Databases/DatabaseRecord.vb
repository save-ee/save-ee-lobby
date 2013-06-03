Public Class DatabaseRecord
    Public Table As String
    Public PrimaryKey As String
    Public WhereClause As String
    Public Data As New Hashtable
    Public Function Clone() As DatabaseRecord
        Dim db As New DatabaseRecord
        db.Table = Me.Table
        db.PrimaryKey = Me.PrimaryKey
        db.WhereClause = Me.WhereClause
        For Each k As String In Data.Keys
            db.Data(k) = Me.Data(k)
        Next
        Return db
    End Function
End Class