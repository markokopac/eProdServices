Imports MySql.Data.MySqlClient
Imports eProdService.cls.msora.DB_MSora

Public Class frmLanguagesList

    Private Sub frmLanguages_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LoadData()

    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        frmLanguagesEdit.mstrCode = ""
        frmLanguagesEdit.mintEditMode = clsGlobal.EditMode.record_insert
        frmLanguagesEdit.ShowDialog(Me)

        If frmLanguagesEdit.DialogResult = Windows.Forms.DialogResult.OK Then
            Call LoadData()
        End If
    End Sub

    Private Sub LoadData()
        Dim strSQL As String = ""
        Dim dt As DataTable
        strSQL = "SELECT * FROM language ORDER BY language"
        Dim conn As New MySqlConnection
        conn = GetMyConnection("msora")
        Dim cmd As New MySqlCommand(strSQL, conn)


        dt = GetMyData(cmd)

        Me.dgLanguages.DataSource = dt
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        Call EditRecord()
    End Sub

    Private Sub EditRecord()
        Dim strLanguage As String = ""
        If Me.dgLanguages.Rows.Count > 0 Then
            strLanguage = dgLanguages.Rows(dgLanguages.CurrentRow.Index).Cells("language").Value

            frmLanguagesEdit.mstrCode = strLanguage
            frmLanguagesEdit.mintEditMode = clsGlobal.EditMode.record_edit
            frmLanguagesEdit.ShowDialog(Me)

            If frmLanguagesEdit.DialogResult = Windows.Forms.DialogResult.OK Then
                Call LoadData()
            End If
        End If
    End Sub

    Private Sub dgLanguages_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgLanguages.CellContentClick

    End Sub

    Private Sub dgLanguages_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgLanguages.CellDoubleClick
        Call EditRecord()
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim strLanguage As String = ""
        If dgLanguages.Rows.Count > 0 Then
            'brišem vse izbrane vrstice
            If dgLanguages.SelectedRows.Count > 0 Then

                If MsgBox("Do you realy want to delete" & " " & dgLanguages.SelectedRows.Count.ToString & " languages?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    For i = 0 To dgLanguages.SelectedRows.Count - 1
                        strLanguage = dgLanguages.SelectedRows(i).Cells("language").Value
                        Call DeleteLanguage(strLanguage)
                    Next
                End If

            End If
        End If
    End Sub

    Private Sub DeleteLanguage(ByVal strLanguage As String)
        'ne smem ga brisati, če je v orders ali country tabeli
        Dim strSQL As String = ""

        strSQL = "SELECT count(*) FROM orders WHERE language = @"
    End Sub
End Class