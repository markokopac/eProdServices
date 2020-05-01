Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common

Public Class frmTextTemplate

    Dim blnRefresh As Boolean
    Dim connTools As SqlConnection = cls.msora.DB_MSora.GetConnection("TOOLS")

    Private Sub EditRtf(ByVal rtf As Windows.Forms.RichTextBox)
        frmRtfEditor.rtbText.Rtf = rtf.Rtf
        frmRtfEditor.ShowDialog()
        If frmRtfEditor.mblnSave Then
            rtf.Rtf = frmRtfEditor.rtbText.Rtf
            frmRtfEditor.Close()
            frmRtfEditor.Dispose()
        End If
    End Sub

    Private Sub cmdRtfEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRtfEdit.Click
        
                Select Case tabBodyLang.SelectedIndex
                    Case 0
                        Call EditRtf(rtbBodySL)
                    Case 1
                        Call EditRtf(rtbBodyEN)
                    Case 2
                        Call EditRtf(rtbBodyDE)
                    Case 3
                        Call EditRtf(rtbBodyIT)
                    Case 4
                        Call EditRtf(rtbBodyFR)
                    Case 5
                        Call EditRtf(rtbBodyHR)

                End Select
        
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub frmTextTemplate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call RefreshGrid(-1)
    End Sub
    Private Sub RefreshGrid(ByVal intTextId As Integer)
        Dim dt As DataTable
        Dim strSQL As String = ""
        blnRefresh = False
        Try
            strSQL = "SELECT text_id, text_description FROM text_template ORDER BY text_id "
            Using cmd As New SqlCommand(strSQL, connTools)
                dt = cls.msora.DB_MSora.GetData(cmd)
                Me.dgText.DataSource = dt
            End Using

            If intTextId > -1 Then
                For i = 0 To dgText.Rows.Count - 1
                    If dgText.Rows(i).Cells("text_id").Value = intTextId Then
                        dgText.Rows(i).Selected = True
                    Else
                        dgText.Rows(i).Selected = False
                    End If
                Next

            End If

            If intTextId < 0 Then
                If Me.dgText.Rows.Count > 0 Then
                    intTextId = dgText.Rows(0).Cells("text_id").Value
                End If

            End If

            Call RefreshRecord(intTextId)

            blnRefresh = True

        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try
    End Sub
    Private Sub RefreshRecord(ByVal intTextId As Integer)
        Dim dt As DataTable
        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM text_template WHERE text_id = @intTextId"
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@intTextId", intTextId)
                dt = cls.msora.DB_MSora.GetData(cmd)

                If dt.Rows.Count > 0 Then
                    Me.txtID.Text = dt(0)("text_id").ToString
                    Me.txtDescription.Text = dt(0)("text_description").ToString
                    Me.rtbBodySL.Rtf = dt(0)("lang_sl").ToString
                    Me.rtbBodyEN.Rtf = dt(0)("lang_en").ToString
                    Me.rtbBodyDE.Rtf = dt(0)("lang_de").ToString
                    Me.rtbBodyIT.Rtf = dt(0)("lang_it").ToString
                    Me.rtbBodyFR.Rtf = dt(0)("lang_fr").ToString
                    Me.rtbBodyHR.Rtf = dt(0)("lang_hr").ToString
                Else
                    Me.txtID.Text = ""
                    Me.txtDescription.Text = ""
                    Me.rtbBodySL.Rtf = ""
                    Me.rtbBodyEN.Rtf = ""
                    Me.rtbBodyDE.Rtf = ""
                    Me.rtbBodyIT.Rtf = ""
                    Me.rtbBodyFR.Rtf = ""
                    Me.rtbBodyHR.Rtf = ""
                End If
            End Using
        Catch ex As Exception
            modLog.AddToErrorLog(ex.Message)
        End Try

    End Sub

    Private Sub SaveRecord()
        Dim strSQL As String = ""
        Dim dt As DataTable
        Dim intTextId As Integer = -1

        If Me.txtID.Text <> "" Then
            intTextId = CInt(Me.txtID.Text)
        End If
        Try
            If intTextId > 0 Then
                strSQL = "SELECT text_id FROM text_template WHERE text_id = @intTextId"
                Using cmd As New SqlCommand(strSQL, connTools)
                    cmd.Parameters.AddWithValue("@intTextId", intTextId)
                    dt = cls.msora.DB_MSora.GetData(cmd)
                    If dt.Rows.Count > 0 Then
                        strSQL = "UPDATE text_template SET text_description = @text_description, " _
                            & " lang_sl = @lang_sl, " _
                            & " lang_en = @lang_en, " _
                            & " lang_de = @lang_de, " _
                            & " lang_it = @lang_it, " _
                            & " lang_fr = @lang_fr, " _
                            & " lang_hr = @lang_hr " _
                            & " WHERE text_id = @intTextId "
                    Else
                        strSQL = "INSERT INTO text_template (text_description, lang_sl, lang_en, lang_de, lang_it, lang_fr, lang_hr) " _
                            & " VALUES (@text_description, @lang_sl, @lang_en, @lang_de, @lang_it, @lang_fr, @lang_hr)"
                    End If

                    Using cmdUpd As New SqlCommand(strSQL, connTools)
                        cmdUpd.Parameters.AddWithValue("@text_description", Me.txtDescription.Text)
                        cmdUpd.Parameters.AddWithValue("@lang_sl", rtbBodySL.Rtf)
                        cmdUpd.Parameters.AddWithValue("@lang_en", rtbBodyEN.Rtf)
                        cmdUpd.Parameters.AddWithValue("@lang_de", rtbBodyDE.Rtf)
                        cmdUpd.Parameters.AddWithValue("@lang_it", rtbBodyIT.Rtf)
                        cmdUpd.Parameters.AddWithValue("@lang_fr", rtbBodyFR.Rtf)
                        cmdUpd.Parameters.AddWithValue("@lang_hr", rtbBodyHR.Rtf)
                        cmdUpd.Parameters.AddWithValue("@intTextId", intTextId)

                        cmdUpd.ExecuteNonQuery()

                    End Using
                End Using
            Else
                strSQL = "INSERT INTO text_template (text_description, lang_sl, lang_en, lang_de, lang_it, lang_fr, lang_hr) " _
                            & " VALUES (@text_description, @lang_sl, @lang_en, @lang_de, @lang_it, @lang_fr, @lang_hr)"
                Using cmdUpd As New SqlCommand(strSQL, connTools)
                    cmdUpd.Parameters.AddWithValue("@text_description", Me.txtDescription.Text)
                    cmdUpd.Parameters.AddWithValue("@lang_sl", rtbBodySL.Rtf)
                    cmdUpd.Parameters.AddWithValue("@lang_en", rtbBodyEN.Rtf)
                    cmdUpd.Parameters.AddWithValue("@lang_de", rtbBodyDE.Rtf)
                    cmdUpd.Parameters.AddWithValue("@lang_it", rtbBodyIT.Rtf)
                    cmdUpd.Parameters.AddWithValue("@lang_fr", rtbBodyFR.Rtf)
                    cmdUpd.Parameters.AddWithValue("@lang_hr", rtbBodyHR.Rtf)
                    cmdUpd.Parameters.AddWithValue("@intTextId", intTextId)

                    cmdUpd.ExecuteNonQuery()

                End Using
            End If


        Catch ex As Exception
            modLog.AddToErrorLog(ex.Message)
        End Try
    End Sub

    Private Sub dgText_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgText.SelectionChanged
        Dim intID As Integer
        If blnRefresh Then
            If dgText.Rows.Count > 0 Then
                If dgText.SelectedRows.Count > 0 Then
                    intID = dgText.Rows(dgText.CurrentRow.Index).Cells("text_id").Value
                    Call RefreshRecord(intID)
                End If
            End If
        End If
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Me.txtID.Text = ""
        Me.txtDescription.Text = ""
        Me.rtbBodySL.Rtf = ""
        Me.rtbBodyEN.Rtf = ""
        Me.rtbBodyDE.Rtf = ""
        Me.rtbBodyIT.Rtf = ""
        Me.rtbBodyFR.Rtf = ""
        Me.rtbBodyHR.Rtf = ""
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim intTxtId As Integer = -1
        Call SaveRecord()
        If Me.txtID.Text <> "" Then
            intTxtId = CInt(txtID.Text)
        End If
        Call RefreshGrid(intTxtId)
    End Sub

    Private Sub tabMain_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim IntId As Integer = -1
        Dim strSQL As String = ""
        If dgText.Rows.Count > 0 Then
            IntId = dgText.Rows(dgText.CurrentRow.Index).Cells("text_id").Value

            strSQL = "SELECT * FROM eprod_events WHERE event_text_id_subject = @intIdSubject OR event_text_id_body = @IntIdBody"
            Using cmd3 As New SqlCommand(strSQL, connTools)
                cmd3.Parameters.AddWithValue("@intIdSubject", IntId)
                cmd3.Parameters.AddWithValue("@IntIdBody", IntId)
                Dim dt As DataTable = cls.msora.DB_MSora.GetData(cmd3)
                If dt.Rows.Count = 0 Then
                    strSQL = "DELETE FROM text_template WHERE text_id = @intID"
                    Using cmd As New SqlCommand(strSQL, connTools)
                        cmd.Parameters.AddWithValue("@intID", IntId)
                        cmd.ExecuteNonQuery()
                        Call RefreshGrid(-1)
                    End Using
                Else
                    MsgBox("Brisanje ni dovoljeno! Tekst je dodeljen enemu izmed dogodkov!")
                    Exit Sub
                End If


            End Using
        End If
    End Sub

    Private Sub dgText_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgText.CellContentClick

    End Sub
End Class