Imports System.Data.OleDb

Public Class DBData

    Private data As DataSet
    Private conn As OleDb.OleDbConnection
    Private adapter As OleDb.OleDbDataAdapter
    Private TableName As ArrayList
    Private TableCount As Integer
    Private Connector As MdbConnector



    Public Sub New(ByVal path As String)
        '建立数据库连接
        Connector = New MdbConnector(path)
        Me.conn = Connector.ConnectOleDb()
        Me.conn.Open()
        Try
            Me.data = New DataSet
            getTableName()
            TableCount = TableName.Count
            readData()
        Catch ex As Exception

        End Try
        Me.conn.Close()

    End Sub

    '获取表名
    Private Sub getTableName()
        Dim dtb As DataTable = Me.conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
        TableName = New ArrayList

        For Each oDataRow As DataRow In dtb.Rows
            TableName.Add(oDataRow("TABLE_NAME").ToString)
        Next
    End Sub

    '读取全部信息
    Private Sub readData()
        For i As Integer = 0 To TableCount - 1 Step 1
            Dim query As String = "SELECT * FROM " & TableName.Item(i).ToString
            Dim adapter As New OleDbDataAdapter(query, Me.conn)
            adapter.Fill(Me.data, TableName.Item(i).ToString)
        Next
        adapter.Dispose()
        adapter = Nothing
    End Sub

    '返回数据集，可在此上获取所有数据
    Public ReadOnly Property DBDataSet As DataSet
        Get
            Return Me.data
        End Get
    End Property

    Public ReadOnly Property DBDataTable(ByVal index As Integer) As DataTable
        Get
            Return Me.data.Tables(index)
        End Get
    End Property

    '获取数据表列表，包括各个数据表的名字
    Public ReadOnly Property DBTableName As ArrayList
        Get
            Return Me.TableName
        End Get
    End Property

    '获取数据表的个数
    Public ReadOnly Property DBTableCount As Integer
        Get
            Return Me.TableCount
        End Get
    End Property

    '获取指定表名的索引号
    Public ReadOnly Property DBIndexOf(ByVal name As String) As Integer
        Get
            Return TableName.IndexOf(name)
        End Get
    End Property

    Public ReadOnly Property DBRowsCount(ByVal index As Integer) As Integer
        Get
            Return Me.data.Tables(index).Rows.Count
        End Get
    End Property

    Public ReadOnly Property DBColsCount(ByVal index As Integer) As Integer
        Get
            Return Me.data.Tables(index).Columns.Count
        End Get
    End Property

    '对数据库执行用户传入的SQL语句。
    Public ReadOnly Property Execute(ByVal query As String) As DataSet
        Get
            Me.conn.Open()
            Dim adapter As New OleDbDataAdapter(query, Me.conn)
            Dim dstExe As New DataSet
            adapter.Fill(dstExe)
            Me.conn.Close()
            adapter.Dispose()
            adapter = Nothing
            Return dstExe
        End Get
    End Property



End Class
