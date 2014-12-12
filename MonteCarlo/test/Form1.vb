Imports DataControl
Public Class Form1
    Private rand As RandGenerator
    Private data As DBData
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        rand = RandFactory.create(Distribution.Uniform)
        txtRand.Text = rand.GetNext().ToString

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        rand = RandFactory.create(Distribution.Exponential, 0.00001)
        txtRand.Text = rand.GetNext().ToString

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        rand = RandFactory.create(Distribution.Gaussian, 10, 3)
        txtRand.Text = rand.GetNext().ToString


    End Sub

    Private Sub btnGetFilePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetFilePath.Click
        With dlgOpenFile
            .Filter = "Access数据库 (*.mdb)|*.mdb"
            .FilterIndex = 1
            .Title = "打开Access数据库"
        End With
        If dlgOpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Dim strFileName As String = dlgOpenFile.FileName
                txtPath.Text = strFileName
            Catch ex As Exception
                MessageBox.Show(ex.Message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        data = New DBData(txtPath.Text)
    End Sub

    Private Sub btnGetTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTable.Click
        For i As Integer = 0 To data.DBTableName.Count - 1 Step 1
            lstTableName.Items.Add(data.DBTableName.Item(i).ToString)
        Next

        '获取指定表的行、列数
        Dim rows, cols As Integer
        Dim index As Integer = data.DBIndexOf("product")
        rows = data.DBRowsCount(index)
        cols = data.DBColsCount(index)


        flexData.Rows.Count = rows
        flexData.Cols.Count = cols

        For i As Integer = 0 To rows - 1 Step 1
            For j As Integer = 0 To cols - 1 Step 1
                flexData(i, j) = data.DBDataTable(index).Rows(i).Item(j).ToString
            Next
        Next

        
        My.Computer.Clipboard.

    End Sub

    
End Class
