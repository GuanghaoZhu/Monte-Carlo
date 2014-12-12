'包含基本部件的所有信息。包括其抽样器，数据
Imports DataControl
Public Class Component
    Private randF, randM As RandGenerator
    Private CData As ComponentData
    Private timeF, timeM As ArrayList 'Failure Time ,and Repaired Time
    Private parent As ArrayList

    Public Enum DataStructure
        Guid = 0
        Name = 1
        Level = 2
        Row = 3
        Type = 4
        Percent = 5
        FailureDistribution = 6
        FailureArg1 = 7
        FailureArg2 = 8
        FailureArg3 = 9
        MaintenanceType = 10
        MaintenanceCost = 11
        MaintenanceDistribution = 12
        MaintenanceArg1 = 13
        MaintenanceArg2 = 14
        MaintenanceArg3 = 15
    End Enum

    Private Structure ComponentData
        Dim Guid As String
        Dim Name As String
        Dim Level As Integer
        Dim Row As Integer
        Dim Type As String
        Dim Percent As Double
        Dim FailureDistribution As String
        Dim FailureArg1 As Double
        Dim FailureArg2 As Double
        Dim FailureArg3 As Double
        Dim MaintenanceType As String
        Dim MaintenanceCost As Double
        Dim MaintenanceDistribution As String
        Dim MaintenanceArg1 As Double
        Dim MaintenanceArg2 As Double
        Dim MaintenanceArg3 As Double

    End Structure

    Public Sub New(ByRef rowData As DataRow)
        timeF = New ArrayList
        timeM = New ArrayList
        CData = New ComponentData
        parent = New ArrayList
        ReadCData(rowData)
        CreateRandGenerator()
    End Sub

    Public Function GetParent(ByRef dataSource As DBData) As ArrayList
        Dim parent As New ArrayList
        Dim parentLevel As Integer = Me.CData.Level - 1
        Dim dset = dataSource.Execute("SELECT * FROM product WHERE level<" & Me.CData.Level & " AND row<" & Me.CData.Row & " ORDER BY row DESC")
        For Each row As DataRow In dset.Tables(0).Rows
            If CInt(row.Item(CInt(DataStructure.Level))) = parentLevel Then
                Me.parent.Add(row)
                parentLevel -= 1
            Else
                Continue For
            End If
        Next
        Return parent
    End Function

    Private Sub ReadCData(ByRef data As DataRow)
        Me.CData.Guid = data.Item(0).ToString
        Me.CData.Name = data.Item(1).ToString
        Me.CData.Level = CInt(data.Item(2))
        Me.CData.Row = CInt(data.Item(3))
        Me.CData.Type = data.Item(4).ToString
        Me.CData.Percent = CDbl(data.Item(5))
        Me.CData.FailureDistribution = data.Item(6).ToString
        Me.CData.FailureArg1 = CDbl(data.Item(7))
        'Me.CData.FailureArg2 = CDbl(data.Item(8))
        'Me.CData.FailureArg3 = CDbl(data.Item(9))
        Me.CData.MaintenanceType = data.Item(10).ToString
        Me.CData.MaintenanceCost = CDbl(data.Item(11))
        Me.CData.MaintenanceDistribution = data.Item(12).ToString
        Me.CData.MaintenanceArg1 = CDbl(data.Item(13))
        'Me.CData.MaintenanceArg2 = CDbl(data.Item(14))
        'Me.CData.MaintenanceArg3 = CDbl(data.Item(15))
    End Sub

    Private Sub CreateRandGenerator()
        Select Case Me.CData.FailureDistribution
            Case "指数分布"
                randF = RandFactory.create(Distribution.Exponential, Me.CData.FailureArg1)
            Case "正态分布"
                randF = RandFactory.create(Distribution.Gaussian, Me.CData.FailureArg1, Me.CData.FailureArg2)
            Case "威布尔分布"
        End Select

        Select Case Me.CData.MaintenanceDistribution
            Case "指数分布"
                randM = RandFactory.create(Distribution.Exponential, Me.CData.MaintenanceArg1)
            Case "正态分布"
                randM = RandFactory.create(Distribution.Gaussian, Me.CData.MaintenanceArg1, Me.CData.MaintenanceArg2)
            Case "威布尔分布"
        End Select
    End Sub

    ''' <summary>
    ''' 获取该部件的发生失效和维修完成的时刻
    ''' </summary>
    ''' <param name="simTime"></param>
    ''' <remarks></remarks>
    Public Sub GenerateTimeArray(ByRef simTime As Double)
        Dim tmpTime As Double = 0

        While True
            tmpTime += randF.GetNext
            If tmpTime < simTime Then
                timeF.Add(tmpTime)
            Else
                Exit While
            End If

            tmpTime += randM.GetNext
            If tmpTime < simTime Then
                timeM.Add(tmpTime)
            Else
                Exit While
            End If
        End While
    End Sub

    Public ReadOnly Property Name As String
        Get
            Return Me.CData.Name
        End Get
    End Property

    Public ReadOnly Property GUID As String
        Get
            Return Me.CData.Guid
        End Get
    End Property

    Public ReadOnly Property Parents As ArrayList
        Get
            Return Me.parent
        End Get
    End Property

    ''' <summary>
    ''' 获取单元的失效时刻序列
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FArray As ArrayList
        Get
            Return timeF
        End Get
    End Property

    ''' <summary>
    ''' 获取单元的维修时刻序列
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MArray As ArrayList
        Get
            Return timeM
        End Get
    End Property

    'Private ReadOnly Property NextFailureTime As Double
    '    Get
    '        Return randF.GetNext()
    '    End Get
    'End Property

    'Private ReadOnly Property NextMaintenanceTime As Double
    '    Get
    '        Return randM.GetNext()
    '    End Get
    'End Property
End Class
