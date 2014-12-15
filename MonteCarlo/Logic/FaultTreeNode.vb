Imports Sequence.Component
Imports DataControl

Public MustInherit Class FaultTreeNode

#Region "Variables and Structures"
    'Private Enable As Boolean

    Protected _GUID, _Name As String
    Protected _NType As NodeType
    Protected _Parent As FaultTreeNode
    Protected _Children As New ArrayList
    Protected _BoolState As Boolean
    Protected _PerformState As Integer

    ''' <summary>
    ''' 0~999 Logic Gate Type;
    ''' 999~  Event Type
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum NodeType
        'Static Logic Gate Type (0~99)
        Logic_And = 0
        Logic_Or = 1
        Logic_K_Out_Of_N = 2

        'Dynamic Logic Gate Type (100~199)
        Logic_Prior_And = 100
        Logic_Cold_Backup = 101

        'User Defined Logic Gate Type (200~999)
        Logic_UD_Gate1 = 200

        'Event Type
        Event_Root = 1000
        Event_Middle = 1001
        Event_Basic = 1002
    End Enum

    Public Enum NodeState
        Working = 0 'For Event
        Failed = 1  'For Event

        Fault_Occur = 1 'Equal to Failed
        Fault_Hide = 0  'Equal to Working

        Null = 2 'For Logic Gate
    End Enum

#End Region

#Region "Construction"
    Public Sub New()
        'Me.Enable = False
        Me._BoolState = False
        Me._PerformState = 1
    End Sub
#End Region

#Region "Functions and Properties"

#Region "Properties"

    Public Property BoolState As Boolean
        Get
            Return Me._BoolState
        End Get
        Set(ByVal value As Boolean)
            Me._BoolState = value
        End Set
    End Property

    Public Property PerformState As Integer
        Get
            Return Me._PerformState
        End Get
        Set(ByVal value As Integer)
            Me._PerformState = value
        End Set
    End Property

    Public Property GUID As String
        Get
            Return Me._GUID
        End Get
        Set(ByVal value As String)
            Me._GUID = value
        End Set
    End Property

    Public ReadOnly Property NType As NodeType
        Get
            Return Me._NType
        End Get
    End Property

    Public Property Name As String
        Get
            Return Me._Name
        End Get
        Set(ByVal value As String)
            Me._Name = value
        End Set
    End Property

    Public Property Parent As FaultTreeNode
        Get
            Return Me._Parent
        End Get
        Set(ByVal value As FaultTreeNode)
            Me._Parent = value
        End Set
    End Property

    Public ReadOnly Property Children As ArrayList
        Get
            Return Me._Children
        End Get
    End Property

#End Region

#Region "Functions"

    ''' <summary>
    ''' Add a Child to ChildList for this Node
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub AddChild(ByVal value As FaultTreeNode)
        Me._Children.Add(value)
    End Sub

    ''' <summary>
    ''' Add a Child(brother) to its Parent's ChildList
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub AddBrother(ByRef value As FaultTreeNode)
        Me._Parent.AddChild(value)
    End Sub

    ''' <summary>
    ''' Remove Specific Child by index
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Private Sub RemoveChild(ByVal value As Integer)
        Me._Children.RemoveAt(value)
    End Sub

    ''' <summary>
    ''' 计算当前节点的布尔状态
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public MustOverride Function CalBoolState(ByVal value As ArrayList) As Boolean

#End Region

#End Region

End Class


<Serializable()> Public Class LogicNode : Inherits FaultTreeNode

#Region "Variables"

    Private _indexOfSpecificChild As New ArrayList

#End Region

#Region "Constructions"

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal type As NodeType)
        Me._NType = type
        Me._GUID = System.Guid.NewGuid.ToString.ToUpper
    End Sub

#End Region

#Region "Functions and Properties"
    Public Overrides Function CalBoolState(ByVal LogicVector As System.Collections.ArrayList) As Boolean
        Dim NormalChildBoolState As New ArrayList
        'Dim SpecificChildBoolState As New ArrayList
        '遍历所有子节点,得到其真值列表
        If Me._Children.Count > 0 Then
            'Have more than one child
            For Each node As EventNode In Me._Children
                NormalChildBoolState.Add(node.CalBoolState(LogicVector))
            Next
        Else
            'Have No Child
        End If


        Select Case Me._NType
            Case NodeType.Logic_And
                For Each v As Boolean In NormalChildBoolState
                    Me._BoolState = Me._BoolState And v
                Next
            Case NodeType.Logic_Or
                For Each v As Boolean In NormalChildBoolState
                    Me._BoolState = Me._BoolState Or v
                Next
            Case NodeType.Logic_K_Out_Of_N

            Case NodeType.Logic_Cold_Backup
                'Specified For Pipeline System

                Dim WorkChild As New ArrayList
                Dim ColdChild As New ArrayList
                WorkChild = Me._Children
                For Each i As Integer In Me._indexOfSpecificChild
                    ColdChild.Add(Me._Children.Item(i))
                    WorkChild.RemoveAt(i)
                Next

                Dim WorkCount As Integer = WorkChild.Count
                For Each i As EventNode In WorkChild
                    If i.CalBoolState(LogicVector) = True Then
                        WorkCount -= 1
                        For Each j As EventNode In ColdChild
                            If j.CalBoolState(LogicVector) = False Then
                                'Swap
                                Dim tmp As EventNode = i
                                i = j
                                j = tmp
                                WorkCount += 1
                                Exit For
                            Else
                                Continue For
                            End If
                        Next
                    End If
                Next

                If WorkCount >= WorkChild.Count Then
                    Me._BoolState = False
                Else
                    Me._BoolState = True
                End If








            Case NodeType.Logic_Prior_And

        End Select
        Return Me._BoolState
    End Function

    'Public Property GetColdBackupChildren As ArrayList
    '    Get
    '        Dim coldArray As New ArrayList
    '        For Each i As Integer In Me._indexOfSpecificChild
    '            coldArray.Add(Me._Children.Item(i))
    '        Next

    '        Return coldArray
    '    End Get
    '    Set(ByVal value As ArrayList)

    '    End Set
    'End Property
#End Region

End Class

<Serializable()> Public Class EventNode : Inherits FaultTreeNode

#Region "Variables"
    Private _Type, _MType As String
    Private _Percent, _FArg1, _FArg2, _FArg3, _MCost, _MArg1, _MArg2, _MArg3 As Double
    Private _Level, _Row As Integer
    Private _FDistribution, _MDistribution As String

#End Region

#Region "Constructions"
    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByRef data As DataRow)
        MyBase.New()
        init(data)

        'to be modified
        
    End Sub
#End Region

#Region "Functions"

    Public Overrides Function CalBoolState(ByVal LogicVector As System.Collections.ArrayList) As Boolean   

        Select Case Me._NType
            Case NodeType.Event_Root
                'Specific for Pipeline System?
                Me._BoolState = CType(Me._Children.Item(0), LogicNode).CalBoolState(LogicVector)
            Case NodeType.Event_Middle
                Me._BoolState = CType(Me._Children.Item(0), LogicNode).CalBoolState(LogicVector)
            Case NodeType.Event_Basic
                'Refresh Basic Event Occur Time According to LogicVector
                If Me._NType = NodeType.Event_Basic Then '若是基本事件，则更新其State
                    Refresh(LogicVector)
                End If
        End Select

        Return Me._BoolState
    End Function

    Private Sub Refresh(ByVal LogicVector As System.Collections.ArrayList)
        If LogicVector.Count > 0 Then
            For Each v As LogicStructure.BasicVector In LogicVector
                If v.GUID = Me._GUID Then
                    Me._BoolState = v.BoolState
                End If
            Next
        End If
    End Sub

    Private Sub init(ByRef data As DataRow)
        Try
            Me._GUID = data.Item(DataStructure.Guid).ToString
        Catch ex As Exception
            Me._GUID = Nothing
        End Try

        Try
            Me._Name = data.Item(DataStructure.Name).ToString
        Catch ex As Exception
            Me._Name = Nothing
        End Try

        Try
            Me._Level = CInt(data.Item(DataStructure.Level))
        Catch ex As Exception
            Me._Level = Nothing
        End Try

        Try
            Me._Row = CInt(data.Item(DataStructure.Row))
        Catch ex As Exception
            Me._Row = Nothing
        End Try

        Try
            Me._Type = data.Item(DataStructure.Type).ToString
        Catch ex As Exception
            Me._Type = Nothing
        End Try

        Try
            Me._Percent = CDbl(data.Item(DataStructure.Percent))
        Catch ex As Exception
            Me._Percent = Nothing
        End Try

        Try
            Me._FDistribution = data.Item(DataStructure.FailureDistribution).ToString
        Catch ex As Exception
            Me._FDistribution = Nothing
        End Try

        Try
            Me._FArg1 = CDbl(data.Item(DataStructure.FailureArg1))
        Catch ex As Exception
            Me._FArg1 = Nothing
        End Try

        Try
            Me._FArg2 = CDbl(data.Item(DataStructure.FailureArg2))
        Catch ex As Exception
            Me._FArg2 = Nothing
        End Try

        Try
            Me._FArg3 = CDbl(data.Item(DataStructure.FailureArg3))
        Catch ex As Exception
            Me._FArg3 = Nothing
        End Try

        Try
            Me._MType = data.Item(DataStructure.MaintenanceType).ToString
        Catch ex As Exception
            Me._MType = Nothing
        End Try

        Try
            Me._MCost = CDbl(data.Item(DataStructure.MaintenanceCost))
        Catch ex As Exception
            Me._MCost = Nothing
        End Try

        Try
            Me._MDistribution = data.Item(DataStructure.MaintenanceDistribution).ToString
        Catch ex As Exception
            Me._MDistribution = Nothing
        End Try

        Try
            Me._MArg1 = CDbl(data.Item(DataStructure.MaintenanceArg1))

        Catch ex As Exception
            Me._MArg1 = Nothing
        End Try

        Try
            Me._MArg2 = CDbl(data.Item(DataStructure.MaintenanceArg2))
        Catch ex As Exception
            Me._MArg2 = Nothing
        End Try

        Try
            Me._MArg3 = CDbl(data.Item(DataStructure.MaintenanceArg3))
        Catch ex As Exception
            Me._MArg3 = Nothing
        End Try

    End Sub
#End Region

End Class