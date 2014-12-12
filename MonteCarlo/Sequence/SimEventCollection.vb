Public Class SimEventCollection
    Inherits CollectionBase



    Public Sub Add(ByVal EventItem As SimEvent) '实现CollectionBase中的Add接口
        List.Add(EventItem)
    End Sub

    Public Sub Remove(ByVal Index As Integer) '实现CollectionBase中的Remove接口
        If Index >= 0 And Index < Count Then
            List.Remove(Index)
        End If
    End Sub


    Public Sub Sort()

    End Sub

    Public ReadOnly Property Item(ByVal Index As Integer) As SimEvent
        Get
            'CType : 显式转换类型
            Return CType(List.Item(Index), SimEvent)
        End Get
    End Property

    
End Class
