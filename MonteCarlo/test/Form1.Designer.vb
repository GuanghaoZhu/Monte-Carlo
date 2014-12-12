<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtRand = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.btnGetFilePath = New System.Windows.Forms.Button()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.lstTableName = New System.Windows.Forms.ListBox()
        Me.btnGetTable = New System.Windows.Forms.Button()
        Me.flexData = New C1.Win.C1FlexGrid.C1FlexGrid()
        CType(Me.flexData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtRand
        '
        Me.txtRand.Location = New System.Drawing.Point(12, 12)
        Me.txtRand.Name = "txtRand"
        Me.txtRand.Size = New System.Drawing.Size(261, 20)
        Me.txtRand.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(279, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Rand"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(360, 10)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "ExpRand"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(441, 10)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "NormRand"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'btnGetFilePath
        '
        Me.btnGetFilePath.Location = New System.Drawing.Point(12, 38)
        Me.btnGetFilePath.Name = "btnGetFilePath"
        Me.btnGetFilePath.Size = New System.Drawing.Size(90, 23)
        Me.btnGetFilePath.TabIndex = 4
        Me.btnGetFilePath.Text = "Get File Path"
        Me.btnGetFilePath.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(108, 40)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(303, 20)
        Me.txtPath.TabIndex = 5
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(417, 38)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(99, 23)
        Me.btnConnect.TabIndex = 6
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'lstTableName
        '
        Me.lstTableName.FormattingEnabled = True
        Me.lstTableName.Location = New System.Drawing.Point(12, 67)
        Me.lstTableName.Name = "lstTableName"
        Me.lstTableName.Size = New System.Drawing.Size(120, 95)
        Me.lstTableName.TabIndex = 7
        '
        'btnGetTable
        '
        Me.btnGetTable.Location = New System.Drawing.Point(13, 169)
        Me.btnGetTable.Name = "btnGetTable"
        Me.btnGetTable.Size = New System.Drawing.Size(119, 23)
        Me.btnGetTable.TabIndex = 8
        Me.btnGetTable.Text = "Get Table"
        Me.btnGetTable.UseVisualStyleBackColor = True
        '
        'flexData
        '
        Me.flexData.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.flexData.Location = New System.Drawing.Point(138, 67)
        Me.flexData.Name = "flexData"
        Me.flexData.Rows.DefaultSize = 19
        Me.flexData.Size = New System.Drawing.Size(372, 125)
        Me.flexData.TabIndex = 9
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 262)
        Me.Controls.Add(Me.flexData)
        Me.Controls.Add(Me.btnGetTable)
        Me.Controls.Add(Me.lstTableName)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.btnGetFilePath)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtRand)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.flexData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtRand As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnGetFilePath As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents lstTableName As System.Windows.Forms.ListBox
    Friend WithEvents btnGetTable As System.Windows.Forms.Button
    Friend WithEvents flexData As C1.Win.C1FlexGrid.C1FlexGrid

End Class
