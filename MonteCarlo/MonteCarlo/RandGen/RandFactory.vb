Public Class RandFactory
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="type">随机生成器的分布类型</param>
    ''' <param name="value1">分布的第一个参数，通常对于指数分布，为Lambda；对于正态分布，为Mu；不填，为均匀分布[0,1]</param>
    ''' <param name="value2">分布的第二个参数，通常对于正态分布，为Sigma；</param>
    ''' <param name="value3">[暂未设计]分布的第三个参数，通常对于威布尔分布，为Beta；</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function create(ByVal type As Distribution, _
                                               Optional ByVal value1 As Double = 0, _
                                               Optional ByVal value2 As Double = 0, _
                                               Optional ByVal value3 As Double = 0) As RandGenerator
        Dim randGen As RandGenerator = Nothing
        Select Case type
            Case Distribution.Uniform
                randGen = New RandGenerator()
            Case Distribution.Exponential
                randGen = New expRandGenerator(value1)
            Case Distribution.Gaussian
                randGen = New normRandGenerator(value1, value2)

                'Other Cases...

        End Select
        Return randGen
    End Function
End Class
