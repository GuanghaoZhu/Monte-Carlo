Imports DataControl
Imports Sequence

''' <summary>
''' 此Dll中应当实现：
''' 1.从Structure数据库中读取可靠性模型的数据。（使用DataControl）
''' 2.分析获得的数据，将其构建成逻辑结构函数。注：该函数中的子函数在其对应的系统内容
''' 状态发生变化的时候将产生StateChange事件，向消息队列中发送该事件信息。
''' 3.输入：Structure数据表，EventQueue
''' 4.输出：
''' </summary>
''' <remarks></remarks> 



Public Class LogicStructure
    Public Structure BasicVector
        Dim GUID As String
        Dim BoolState As Boolean
        'Dim PerformState As Integer
    End Structure


    Public Sub New(ByVal path As String)
        Dim dbSource As New DBData(path)
        Dim table As DataTable = dbSource.DBDataTable(0)
        Dim tree As New FaultTreeNode(table.Rows.Item(4))
        tree.AddChild(New FaultTreeNode(table.Rows.Item(6)))
        tree.AddChild(New FaultTreeNode(table.Rows.Item(7)))

        Dim ser As New Xml.Serialization.XmlSerializer(GetType(FaultTreeNode))
        Using fs As New IO.FileStream("D:/person.xml", IO.FileMode.Create)
            ser.Serialize(fs, tree)
        End Using


    End Sub


End Class
