''' <summary>
''' 建立到指定的DB文件的连接，需继承实现。
''' </summary>
''' <remarks></remarks>
Public MustInherit Class Connector
    Private DBFilePath As String
    Private DBConnectString As String

    Public Sub New(ByVal path As String)
            Me.DBFilePath = path
    End Sub





    Public Property FilePath As String
        Get
            Return Me.DBFilePath
        End Get
        Set(ByVal value As String)
            Me.DBFilePath = value
        End Set
    End Property

    Public ReadOnly Property ConnectString As String
        Get
            Return Me.DBConnectString
        End Get
    End Property
End Class
