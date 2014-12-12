Public Class SimEvent

    Implements IComparable

    Private part As Component   'Who
    Private TimeOccor As Double 'When
    Private type As EventType   'What

    Public Enum EventType As Integer
        Fail = 0
        Maintenance = 1
    End Enum





    Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
        Dim delta As Double
        delta = TimeOccor - CType(obj, SimEvent).TimeOccor
        If delta > 0 Then
            Return 1
        Else
            Return -1
        End If
    End Function
End Class
