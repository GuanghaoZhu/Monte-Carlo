Imports DataControl
Public Class EventGenerator '从数据库抓取数据，生成事件队列
    Private queue As ArrayList
    Private CArray As ArrayList 'Component的列表
    Private simTime As Double

    Public Sub New(ByRef Time As Double, ByRef data As DBData)
        queue = New ArrayList
        Me.simTime = Time
        GetBasicComponent(data)
        ComponentInitialization(data)
    End Sub

    ''' <summary>
    ''' 将所有符合条件的对象从DBData放入集合CArray
    ''' </summary>
    ''' <param name="data"></param>
    ''' <remarks></remarks>
    Public Sub GetBasicComponent(ByRef data As DBData)
        CArray = New ArrayList
        Dim dset As DataSet = data.Execute("SELECT * FROM product WHERE type='失效模式' ORDER BY row")
        For Each row As DataRow In dset.Tables(0).Rows
            '将一行数据生成对应的Component对象
            Dim part As New Component(row)
            '将Component添加到临时队列
            CArray.Add(part)
        Next
    End Sub


    ''' <summary>
    ''' 初始化对象集合，按仿真时间生成各个部件的事件时刻列表
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ComponentInitialization(ByRef data As DBData)
        For Each part As Component In CArray
            part.GenerateTimeArray(Me.simTime)
            part.GetParent(data)
        Next
    End Sub

    '生成队列
    Public Function GenerateQueue() As ArrayList
        Dim tmpEvent As SimEvent

        For Each part As Component In CArray
            'Select a Component & Generate its Event
            For Each time As Double In part.FArray
                tmpEvent = New SimEvent(part, time, SimEvent.EventType.Fail)
                queue.Add(tmpEvent)
            Next

            For Each time As Double In part.MArray
                tmpEvent = New SimEvent(part, time, SimEvent.EventType.Maintenance)
                queue.Add(tmpEvent)
            Next

        Next
        queue.Sort()
        Return (queue)
    End Function





End Class
