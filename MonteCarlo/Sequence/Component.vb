'包含基本部件的所有信息。包括其抽样器，数据
Imports DataControl
Public Class Component
    Private randF, randM As RandGenerator
    Private CData As ComponentData

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
        ReadCData(rowData)
        CreateRandGenerator()
    End Sub

    Private Sub ReadCData(ByRef data As DataRow)
        Me.CData.Guid = data.Item(0).ToString
        Me.CData.Name = data.Item(1).ToString
        Me.CData.Level = CInt(data.Item(2))
        Me.CData.Row = CInt(data.Item(3))
        Me.CData.Type = data.Item(4).ToString
        Me.CData.Percent = CDbl(data.Item(5))
        Me.CData.FailureDistribution = data.Item(6).ToString
        Me.CData.FailureArg1 = CDbl(data.Item(7))
        Me.CData.FailureArg2 = CDbl(data.Item(8))
        Me.CData.FailureArg3 = CDbl(data.Item(9))
        Me.CData.MaintenanceType = data.Item(10).ToString
        Me.CData.MaintenanceCost = CDbl(data.Item(11))
        Me.CData.MaintenanceDistribution = data.Item(12).ToString
        Me.CData.MaintenanceArg1 = CDbl(data.Item(13))
        Me.CData.MaintenanceArg2 = CDbl(data.Item(14))
        Me.CData.MaintenanceArg3 = CDbl(data.Item(15))
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

    Public ReadOnly Property NextFailureTime As Double
        Get
            Return randF.GetNext()
        End Get
    End Property

    Public ReadOnly Property NextMaintenanceTime As Double
        Get
            Return randM.GetNext()
        End Get
    End Property
End Class
