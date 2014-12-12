Public Class expRandGenerator
    Inherits RandGenerator
    Private lambda As Double

    Public Sub New(ByVal in_Lambda As Double)
        Me.lambda = in_Lambda
    End Sub

    Public Overrides Function GetNext() As Double
        Dim result As Double
        result = -1 / Me.lambda * Math.Log(MyBase.GetNext())
        Return result
    End Function
End Class
