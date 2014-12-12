Public Class EventGenerator
    Dim queue As SimEventCollection

    '生成队列
    Public Function GenerateQueue() As SimEventCollection
        queue.Sort()
        Return queue
    End Function

    Public Sub GetBasicComponent()

    End Sub


End Class
