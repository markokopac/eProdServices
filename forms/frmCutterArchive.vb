Imports System.IO

Public Class frmCutterArchive
    Dim blnloaded As Boolean = False
    Private Sub frmCutterArchive_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call GetCutterFiles()
        blnloaded = True
    End Sub

    Private Sub GetCutterFiles()
        blnloaded = False
        Dim dt As DataTable = GetFinnishedCutterFiles()
        Me.dgFiles.DataSource = dt

        If Me.dgFiles.Rows.Count > 0 Then
            Call ReadFile(Me.dgFiles.Rows(0).Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub ReadFile(ByVal strFile As String)

        
        Dim blnCompare As Boolean = False
        Dim intQuantity As Integer = 0
        Dim intFinnished As Integer = 0
        Dim objReader As New StreamReader(strFile)
        Dim sLine As String = ""
        Dim strDesc As String = ""
        Dim strArticle As String = ""
        Dim intLength As Integer = 0
        Dim i As Integer = 0
        Dim intStatus As Integer = -1
        
        Dim dt As DataTable = CreateTableCutterRecord()

        If Not File.Exists(strFile) Then Exit Sub

        Do
            i = i + 1
            sLine = objReader.ReadLine()

            If Not blnCompare Then
                If Mid(sLine, 1, 4) = "----" Then
                    blnCompare = True
                End If
                If i = 6 Then
                    Me.lblStranka.Text = Mid(sLine, 1, 59)
                    Me.lblNalog.Text = Mid(sLine, 60, 10)
                End If
            Else

                Dim aLine = Split(sLine, ";")
                If aLine.Length = 5 Then
                    If IsNumeric(aLine(0)) Then
                        intQuantity = CInt(aLine(0))
                    End If

                    If IsNumeric(aLine(1)) Then
                        intLength = CInt(aLine(1))
                    Else
                        intLength = 0
                    End If
                    strDesc = aLine(2)
                    strArticle = aLine(3)
                    If IsNumeric(aLine(4)) Then
                        intFinnished = CInt(aLine(4))
                    Else
                        intFinnished = 0
                    End If

                    Dim dr As DataRow = dt.NewRow
                    dr("article_nr") = strArticle
                    dr("description") = strDesc
                    dr("length") = intLength
                    dr("quantity") = intQuantity
                    dr("finished_quantity") = intFinnished
                    dt.Rows.Add(dr)
                End If
                End If

        Loop Until sLine Is Nothing

            objReader.Close()
        Me.dgRecords.DataSource = dt
    End Sub

    Private Sub dgFiles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgFiles.CellContentClick

    End Sub

    Private Sub dgFiles_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles dgFiles.RowPostPaint
        'status
        'rdeče - 2
        'zeleno -1 
        'belo - 0, -1
        Dim intStatus As Integer = -1
        If e.RowIndex < Me.dgFiles.RowCount Then
            Dim dgvRow As DataGridViewRow = Me.dgFiles.Rows(e.RowIndex)

            intStatus = CInt(dgvRow.Cells("Status").Value.ToString)

            Select Case intStatus
                Case -1
                    dgvRow.DefaultCellStyle.BackColor = Color.White
                Case 0
                    dgvRow.DefaultCellStyle.BackColor = Color.White
                Case 1
                    dgvRow.DefaultCellStyle.BackColor = Color.Green
                Case 2
                    dgvRow.DefaultCellStyle.BackColor = Color.Red
                Case Else
                    dgvRow.DefaultCellStyle.BackColor = Color.White
            End Select

        End If
    End Sub

    Private Sub dgFiles_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgFiles.SelectionChanged
        Dim strFile As String = ""
        If Not blnloaded Then Exit Sub

        If dgFiles.Rows.Count > 0 Then
            strFile = dgFiles.Rows(dgFiles.CurrentRow.Index).Cells(0).Value.ToString
            Call ReadFile(strFile)
        End If
    End Sub

    Private Sub dgRecords_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgRecords.CellContentClick

    End Sub

    Private Sub dgRecords_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles dgRecords.RowPostPaint
        Dim intStatus As Integer = -1
        If e.RowIndex < Me.dgRecords.RowCount Then
            Dim dgvRow As DataGridViewRow = Me.dgRecords.Rows(e.RowIndex)

            If CInt(dgvRow.Cells("quantity").Value.ToString) > CInt(dgvRow.Cells("finished_quantity").Value.ToString) And CInt(dgvRow.Cells("finished_quantity").Value.ToString) > 0 Then

                intStatus = 1
            ElseIf CInt(dgvRow.Cells("quantity").Value.ToString) <= CInt(dgvRow.Cells("finished_quantity").Value.ToString) Then
                intStatus = 2
            ElseIf CInt(dgvRow.Cells("finished_quantity").Value.ToString) = 0 Then
                intStatus = 0
            End If

            Select Case intStatus
                Case -1
                    dgvRow.DefaultCellStyle.BackColor = Color.White
                Case 0
                    dgvRow.DefaultCellStyle.BackColor = Color.White
                Case 1
                    dgvRow.DefaultCellStyle.BackColor = Color.Green
                Case 2
                    dgvRow.DefaultCellStyle.BackColor = Color.Red
                Case Else
                    dgvRow.DefaultCellStyle.BackColor = Color.White
            End Select

        End If
    End Sub

    Private Sub cmdArchive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdArchive.Click
        Dim strSourceFile As String = ""
        Dim strDestFile As String = ""
        Dim strDestPath As String = cls.Config.GetCutterDestPath

        For i = 0 To dgFiles.SelectedRows.Count - 1

            strSourceFile = dgFiles.SelectedRows(i).Cells("filename").Value.ToString
            strDestFile = strDestPath & "\Arhiv\" & Path.GetFileName(strSourceFile)
            If File.Exists(strSourceFile) Then
                'ga kopiram v dest_path
                If Not File.Exists(strDestFile) Then
                    File.Move(strSourceFile, strDestFile)
                Else
                    File.Delete(strSourceFile)
                End If
            End If
        Next

        Call GetCutterFiles()

        blnloaded = True
    End Sub
End Class