Imports System.Data.OleDb
Public Class MdbConnector
    Inherits Connector

    Public Sub New(ByVal path As String)
        MyBase.New(path)
    End Sub

    Public Function ConnectOleDb() As System.Data.OleDb.OleDbConnection
        'Connect to this new mdb file.
        Dim connString As String
        connString = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                                "Data Source=" & Me.FilePath & ";" & _
                                "Persist Security Info=False"

        Dim conn As New OleDbConnection(connString)
        Return conn
    End Function
End Class
