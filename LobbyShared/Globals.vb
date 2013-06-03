Public Class Globals
    Public Shared ServerSalt() As Byte = New Byte() {6, 9, 3, 5, 9, 1, 34, 79, 26, 94, 29, 6, 57}
    Public Shared ServerPort As Integer = 1000
    Public Shared IsLocal As Boolean = False
    Public Shared LocalAdapter As String = "127.0.0.1"
    Public Shared ClientVerNum As String = "2.6.5"
    Public Shared ClientVersion As String = "Save-EE Lobby Client " & Globals.ClientVerNum
    Public Shared Sub SerializeObjectToDisk(ByVal FilePath As String, ByVal objData As Object, Optional ByVal salt() As Byte = Nothing)
        Dim b() As Byte = SerializeObjectToByteArray(objData, salt)
        Dim objStream As System.IO.Stream = System.IO.File.Open(FilePath, System.IO.FileMode.Create)
        objStream.Write(b, 0, b.Length)
        objStream.Close()
        objStream.Dispose()
    End Sub
    Public Shared Function UnSerializeObjectFromDisk(ByVal FilePath As String, Optional ByVal salt() As Byte = Nothing) As Object
        Dim DeserializedObject As Object = Nothing
        Try
            Dim objStream As System.IO.Stream = System.IO.File.Open(FilePath, System.IO.FileMode.Open)
            Dim b(objStream.Length - 1) As Byte
            objStream.Read(b, 0, b.Length)
            objStream.Close()
            objStream.Dispose()
            DeserializedObject = UnSerializeObjectFromByteArray(b, salt)
        Catch ex As Exception
        End Try
        Return DeserializedObject
    End Function
    Public Shared Function SerializeObjectToByteArray(ByVal objData As Object, Optional ByVal salt() As Byte = Nothing) As Byte()
        Dim objStream As System.IO.MemoryStream = New System.IO.MemoryStream
        Dim objFormat As System.Runtime.Serialization.Formatters.Binary.BinaryFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        objFormat.Serialize(objStream, objData)
        objStream.Position = 0
        objStream.Close()
        Return objStream.ToArray
    End Function
    Public Shared Function UnSerializeObjectFromByteArray(ByVal bytes() As Byte, Optional ByVal salt() As Byte = Nothing) As Object
        Dim objStream As System.IO.MemoryStream = New System.IO.MemoryStream(bytes)
        Dim objFormat As System.Runtime.Serialization.Formatters.Binary.BinaryFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Return objFormat.Deserialize(objStream)
    End Function
End Class
