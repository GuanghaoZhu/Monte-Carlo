Public Class RandGenerator

    Inherits Random
    Implements IDisposable

    

    Protected seed As Integer
    Protected rand As Random

    Public Sub New()
        rand = New Random
    End Sub

    Public Sub New(ByVal intSeed As Integer)
        rand = New Random(intSeed)
    End Sub

    Public Overridable Function GetNext() As Double
        Return rand.NextDouble
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 检测冗余的调用

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
            End If

            ' TODO: 释放非托管资源(非托管对象)并重写下面的 Finalize()。
            ' TODO: 将大型字段设置为 null。
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: 仅当上面的 Dispose(ByVal disposing As Boolean)具有释放非托管资源的代码时重写 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 不要更改此代码。请将清理代码放入上面的 Dispose(ByVal disposing As Boolean)中。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic 添加此代码是为了正确实现可处置模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 不要更改此代码。请将清理代码放入上面的 Dispose(ByVal disposing As Boolean)中。
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
