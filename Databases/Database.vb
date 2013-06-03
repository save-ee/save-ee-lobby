Imports System.Windows.Forms
Imports System.Data

Public Class Database
    Public DatabasePath As String = ""
    Public RecordCount As Integer = 0
    Public CurrentRecord As Integer = 0
    Public IsLastRecord As Boolean = True
    Public ConnectionString As String
    Public DataRowCol As Data.DataRowCollection
    Public Connection As Odbc.OdbcConnection
    Public Command As Odbc.OdbcCommand
    Public DataSet As Data.DataSet
    Private CurretTable As String
    Public PrimaryKey As String

    Public Function GenerateRecord() As DatabaseRecord

        Dim nr As New DatabaseRecord
        nr.Table = DataSet.Tables(0).TableName
        nr.PrimaryKey = Me.PrimaryKey
        For Each d As DataColumn In DataSet.Tables(0).Columns
            nr.Data(d.ColumnName) = RecordData(d.ColumnName)
        Next
        Return nr
    End Function
    Public Function GenerateLvi(ByVal cols As String) As ListViewItem
        Dim c() As String = Split(cols, ",")

        Dim lvi As New ListViewItem
        lvi.Name = Me.RecordData(c(0))
        lvi.Text = Me.RecordData(c(1))
        If c.Length > 2 Then
            For x As Integer = 2 To c.Length - 1
                lvi.SubItems.Add(Me.RecordData(c(x)))
            Next
        End If
        Return lvi
    End Function

#Region "Record Movement"
    Public Sub MoveToNextRecord()
        CurrentRecord += 1
        If CurrentRecord > RecordCount Then
            IsLastRecord = True
        End If
    End Sub
    Public Sub MoveNext()
        CurrentRecord += 1
        If CurrentRecord > RecordCount Then
            IsLastRecord = True
        End If
    End Sub
    Public Sub MoveToRecord(ByVal RowNumber As Integer)
        CurrentRecord = RowNumber
        If CurrentRecord >= RecordCount Then
            IsLastRecord = True
        Else
            IsLastRecord = False
        End If
    End Sub
    Public Sub MoveToFirstRecord()
        CurrentRecord = 1
        IsLastRecord = False
    End Sub
    Public Sub MoveToLastRecord()
        CurrentRecord = RecordCount
        IsLastRecord = True
    End Sub
#End Region
#Region "Data Functions"
    Public Function RecordData(ByVal FieldName As String) As String
        If CurrentRecord <= RecordCount Then
            Return DataRowCol(CurrentRecord - 1)(FieldName).ToString & ""
        Else
            Return ""
        End If
    End Function
    Public Sub InsertRecord(ByVal NewRecord As DatabaseRecord)
        Dim sql As String = ""
        Dim Fields As String = ""
        Dim Values As String = ""
        For Each s As String In NewRecord.Data.Keys
            Fields = Fields & "," & s
            Values = Values & ",'" & CType(NewRecord.Data(s) & "", String).Replace("'", "''") & "'"
        Next
        Fields = Fields.Substring(1)
        Values = Values.Substring(1)
        sql = "INSERT INTO " & NewRecord.Table & " (" & Fields & ") VALUES (" & Values & ")"
        Query(sql)
    End Sub
    Public Sub UpdateRecord(ByVal NewRecord As DatabaseRecord)
        Dim sql As String = ""
        Dim FieldsValues As String = ""
        For Each s As String In NewRecord.Data.Keys
            'If s.ToLower <> NewRecord.PrimaryKey.ToLower Then FieldsValues = FieldsValues & "," & s & "=' " & NewRecord.Data(s).ToString & " '"
            FieldsValues = FieldsValues & "," & s & "='" & NewRecord.Data(s).ToString.Replace("'", "''") & "'"
        Next
        FieldsValues = FieldsValues.Substring(1)

        sql = "UPDATE " & NewRecord.Table & " SET " & FieldsValues
        If NewRecord.WhereClause = "" Then
            sql = sql & " WHERE " & NewRecord.PrimaryKey & "='" & NewRecord.Data(NewRecord.PrimaryKey).ToString & "'"
        Else
            sql = sql & " " & NewRecord.WhereClause
        End If
        Query(sql)
    End Sub
#End Region
    Public Sub Query(ByVal sql As String, ByVal primaryKey As String)
        Me.PrimaryKey = primaryKey
        Me.Query(sql)
    End Sub
    Public Sub Query(ByVal sql As String)
        If sql.ToUpper.Substring(0, 6) = "SELECT" Then
            RecordCount = 0
            CurrentRecord = 0
            IsLastRecord = False
            Dim tn As String = sql.Substring(InStr(sql, " FROM ", CompareMethod.Text) + 6 - 1)
            Dim tn2 As String = InStr(tn, " ", CompareMethod.Text)
            If tn2 > 0 Then tn = tn.Substring(0, tn2 - 1)
            DataSet = New DataSet()
            Dim oleDbDataAdapter As Odbc.OdbcDataAdapter = New Odbc.OdbcDataAdapter(sql, Connection)
            oleDbDataAdapter.SelectCommand.CommandText = sql
            Dim oleDbCommandBuilder As Odbc.OdbcCommandBuilder = New Odbc.OdbcCommandBuilder(oleDbDataAdapter)
            oleDbDataAdapter.Fill(DataSet, tn)
            DataRowCol = DataSet.Tables(tn).Rows
            RecordCount = DataRowCol.Count
            MoveToNextRecord()
        ElseIf (InStr(sql, "CREATE TABLE", CompareMethod.Text) > 0) Then
            Dim cmdDatabase As Odbc.OdbcCommand
            cmdDatabase = New Odbc.OdbcCommand(sql, Connection)
            cmdDatabase.ExecuteNonQuery()
        Else
            DataSet = New Data.DataSet()
            Dim oleDbDataAdapter As Odbc.OdbcDataAdapter = New Odbc.OdbcDataAdapter(sql, Connection)
            oleDbDataAdapter.SelectCommand.CommandText = sql
            oleDbDataAdapter.SelectCommand.ExecuteNonQuery()
        End If
    End Sub
End Class