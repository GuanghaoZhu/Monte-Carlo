''' <summary>
''' 需要修正
''' </summary>
''' <remarks></remarks>
Public Class normRandGenerator
    Inherits RandGenerator

    Private mu, sigma As Double

    Public Sub New(ByVal in_mu As Double, ByVal in_sigma As Double)
        Me.mu = in_mu
        Me.sigma = in_sigma
    End Sub

    Public Overrides Function GetNext() As Double
        Dim result As Double
        result = Me.mu + Me.sigma * Math.Sqrt(-2 * Math.Log(MyBase.GetNext())) * Math.Cos(2 * Math.PI * MyBase.GetNext())
        Return result
    End Function
End Class
