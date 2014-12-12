Public Class SimEvent

    Implements IComparable

    Private part As Component   'Who
    Private TimeOccor As Double 'When
    Private type As EventType   'What

    Public Enum EventType As Integer
        Fail = 0
        Maintenance = 1
    End Enum

    Public Sub New(ByRef in_component As Component, ByRef in_time As Double, ByRef in_type As EventType)
        Me.part = in_component
        Me.TimeOccor = in_time
        Me.type = in_type
    End Sub


    Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
        Dim delta As Double
        delta = TimeOccor - CType(obj, SimEvent).TimeOccor
        If delta > 0 Then
            Return 1
        Else
            Return -1
        End If
    End Function

    ''' <summary>
    ''' 返回事件的文字描述
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Dim strType As String
        Dim strPart As String = ""
        For i As Integer = part.Parents.Count - 1 To 0 Step -1
            Dim row As DataRow = CType(part.Parents.Item(i), DataRow)
            strPart += row.Item(Component.DataStructure.Name).ToString & " | "
        Next

        If Me.type = EventType.Fail Then
            strType = "发生故障"
        Else
            strType = "修理完成"
        End If
        Dim str = strPart & Me.part.Name & " | At " & Me.TimeOccor & " : " & strType
        Return str
    End Function
End Class
