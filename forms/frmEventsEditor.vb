Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common

Public Class frmEventsEditor
    Dim blnRefresh As Boolean

    Dim connTools As SqlConnection = cls.msora.DB_MSora.GetConnection("TOOLS")

    Private Sub frmEventsEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call FillWorkstationsCombo(Me.cboWorkstation)
        Call FillEventType(cboEventType)
        Call FillTemplateCombo(cboTemplateBody, connTools)
        Call FillTemplateCombo(cboTemplateSubject, connTools)
        cboWorkstation.SelectedIndex = -1
        cboEventType.SelectedIndex = -1
        cboTemplateBody.SelectedIndex = -1
        cboTemplateSubject.SelectedIndex = -1

        Me.txtDeveloper.Text = cls.Config.GetDeveloperMail
        Me.txtDirector.Text = cls.Config.GetDirectorMail
        Me.txtLogistic.Text = cls.Config.GetLogisticMail
        Me.txtProduction.Text = cls.Config.GetProductionMail

        Call RefreshGrid(-1)
    End Sub
    Private Sub RefreshGrid(ByVal intEventId As Integer)
        Dim dt As DataTable
        Dim strSQL As String = ""
        blnRefresh = False
        Try
            strSQL = "SELECT event_id, event_desc FROM eprod_events ORDER BY event_id"
            Using cmd As New SqlCommand(strSQL, connTools)
                dt = cls.msora.DB_MSora.GetData(cmd)
                Me.dgText.DataSource = dt
            End Using

            If intEventId > -1 Then
                For i = 0 To dgText.Rows.Count - 1
                    If dgText.Rows(i).Cells("event_id").Value = intEventId Then
                        dgText.Rows(i).Selected = True
                    Else
                        dgText.Rows(i).Selected = False
                    End If
                Next

            End If

            If intEventId < 0 Then
                If Me.dgText.Rows.Count > 0 Then
                    intEventId = dgText.Rows(0).Cells("event_id").Value
                End If

            End If

            Call RefreshRecord(intEventId)

            blnRefresh = True

        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Sub
    Private Sub RefreshRecord(ByVal intEventId As Integer)
        Dim dt As DataTable
        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM eprod_events WHERE event_id = @intTextId"
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@intTextId", intEventId)
                dt = cls.msora.DB_MSora.GetData(cmd)

                If dt.Rows.Count > 0 Then
                    Me.txtID.Text = dt(0)("event_id").ToString
                    Me.txtDescription.Text = dt(0)("event_desc").ToString
                    Me.cboWorkstation.SelectedValue = cls.Utils.DB2IntZero(dt(0)("event_workstation"))
                    Me.cboEventType.SelectedValue = cls.Utils.DB2IntZero(dt(0)("event_type"))
                    Me.txtParameter.Text = dt(0)("event_parameter").ToString
                    Me.cboTemplateBody.SelectedValue = cls.Utils.DB2IntZero(dt(0)("event_text_id_body"))
                    Me.cboTemplateSubject.SelectedValue = cls.Utils.DB2IntZero(dt(0)("event_text_id_subject"))
                    Me.chkCommercialist.Checked = IIf(cls.Utils.DB2IntZero(dt(0)("event_mailto_commercial")) = 1, True, False)
                    Me.chkDeveloper.Checked = IIf(cls.Utils.DB2IntZero(dt(0)("event_mailto_developer")) = 1, True, False)
                    Me.chkDirector.Checked = IIf(cls.Utils.DB2IntZero(dt(0)("event_mailto_director")) = 1, True, False)
                    Me.chkLogistic.Checked = IIf(cls.Utils.DB2IntZero(dt(0)("event_mailto_logistic")) = 1, True, False)
                    Me.chkPartner.Checked = IIf(cls.Utils.DB2IntZero(dt(0)("event_mailto_partner")) = 1, True, False)
                    Me.chkProduction.Checked = IIf(cls.Utils.DB2IntZero(dt(0)("event_mailto_production")) = 1, True, False)
                    Me.chkTechnolog.Checked = IIf(cls.Utils.DB2IntZero(dt(0)("event_mailto_technology")) = 1, True, False)
                    Me.chkEnabled.Checked = IIf(cls.Utils.DB2IntZero(dt(0)("event_enabled")) = 1, True, False)
                    Me.nudDays.Value = cls.Utils.DB2IntZero(dt(0)("event_days"))
                    Me.chkExcludeReclamation.Checked = IIf(cls.Utils.DB2IntZero(dt(0)("event_exclude_reclamation")) = 1, True, False)
                    Me.chkCheckDelivery.Checked = IIf(cls.Utils.DB2IntZero(dt(0)("event_check_delivery")) = 1, True, False)
                    Me.chkCheckMontage.Checked = IIf(cls.Utils.DB2IntZero(dt(0)("event_check_montage")) = 1, True, False)
                    Me.txtMontageDays.Text = dt(0)("event_montage_days").ToString
                    Me.txtEmailManual.Text = dt(0)("event_emailto_extra").ToString
                    Me.txtPartnerId.Text = dt(0)("event_emailto_partner_id").ToString.Trim
                    Me.rtbZadeva.Rtf = GetText(cls.Utils.DB2IntZero(dt(0)("event_text_id_subject")), connTools)
                    Me.rtbBodySL.Rtf = GetText(cls.Utils.DB2IntZero(dt(0)("event_text_id_body")), connTools)
                Else
                    Me.txtID.Text = ""
                    Me.txtDescription.Text = ""
                    Me.cboWorkstation.SelectedIndex = -1
                    Me.cboEventType.SelectedIndex = -1
                    Me.txtParameter.Text = ""
                    Me.cboTemplateBody.SelectedIndex = -1
                    Me.cboTemplateSubject.SelectedIndex = -1
                    Me.chkCommercialist.Checked = False
                    Me.chkDeveloper.Checked = False
                    Me.chkDirector.Checked = False
                    Me.chkLogistic.Checked = False
                    Me.chkPartner.Checked = False
                    Me.chkProduction.Checked = False
                    Me.chkTechnolog.Checked = False
                    Me.chkEnabled.Checked = False
                    Me.nudDays.Value = 0
                    Me.chkExcludeReclamation.Checked = False
                    Me.chkCheckDelivery.Checked = False
                    Me.chkCheckMontage.Checked = False
                    Me.txtMontageDays.Text = 0
                    Me.txtEmailManual.Text = ""
                    Me.txtPartnerId.Text = ""
                    Me.rtbZadeva.Rtf = ""
                    Me.rtbBodySL.Rtf = ""
                End If
            End Using
        Catch ex As Exception
            modLog.AddToErrorLog(ex.Message)
        End Try

    End Sub

    Private Sub SaveRecord()
        Dim strSQL As String = ""
        Dim dt As DataTable
        Dim intEventId As Integer = -1

        If Me.txtID.Text <> "" Then
            intEventId = CInt(Me.txtID.Text)
        End If
        Try
            If intEventId > 0 Then
                strSQL = "SELECT event_id FROM eprod_events WHERE event_id = @intEventId"
                Using cmd As New SqlCommand(strSQL, connTools)
                    cmd.Parameters.AddWithValue("@intEventId", intEventId)
                    dt = cls.msora.DB_MSora.GetData(cmd)
                    If dt.Rows.Count > 0 Then
                        strSQL = "UPDATE eprod_events SET event_desc = @event_desc, " _
                            & " event_workstation = @event_workstation, " _
                            & " event_type = @event_type, " _
                            & " event_parameter = @event_parameter, " _
                            & " event_text_id_body = @event_text_id_body, " _
                            & " event_text_id_subject = @event_text_id_subject, " _
                            & " event_mailto_partner = @event_mailto_partner, " _
                            & " event_mailto_director = @event_mailto_director, " _
                            & " event_mailto_commercial = @event_mailto_commercial, " _
                            & " event_mailto_developer = @event_mailto_developer, " _
                            & " event_mailto_production = @event_mailto_production, " _
                            & " event_mailto_technology = @event_mailto_technology, " _
                            & " event_mailto_logistic = @event_mailto_logistic," _
                            & " event_enabled = @event_enabled, " _
                            & " event_days = @event_days, " _
                            & " event_exclude_reclamation = @event_exclude_reclamation,  " _
                            & " event_check_delivery = @event_check_delivery, " _
                            & " event_check_montage = @event_check_montage, " _
                            & " event_montage_days = @event_montage_days, " _
                            & " event_emailto_extra = @event_emailto_extra, " _
                            & " event_emailto_partner_id = @event_emailto_partner_id " _
                            & " WHERE event_id = @intEventId "
                    Else
                        strSQL = "INSERT INTO eprod_events (event_desc, event_workstation, event_type, event_parameter, event_text_id_body, event_text_id_subject, event_mailto_partner, " _
                        & " event_mailto_director, event_mailto_commercial, event_mailto_developer, event_mailto_production, event_mailto_technology, " _
                        & " event_mailto_logistic, event_enabled, event_days, event_exclude_reclamation, event_check_delivery, event_check_montage, event_montage_days, event_emailto_extra, event_emailto_partner_id) " _
                            & " VALUES (@event_desc, @event_workstation, @event_type, @event_parameter, @event_text_id_body, @event_text_id_subject, @event_mailto_partner, " _
                        & " @event_mailto_director, @event_mailto_commercial, @event_mailto_developer, @event_mailto_production, @event_mailto_technology, " _
                        & " @event_mailto_logistic, @event_enabled, @event_days, @event_exclude_reclamation, @event_check_delivery, @event_check_montage, @event_montage_days, @event_emailto_extra, @event_emailto_partner_id)"
                    End If

                    Using cmdUpd As New SqlCommand(strSQL, connTools)
                        cmdUpd.Parameters.AddWithValue("@event_desc", Me.txtDescription.Text)
                        cmdUpd.Parameters.AddWithValue("@event_workstation", cboWorkstation.SelectedValue)
                        cmdUpd.Parameters.AddWithValue("@event_type", cboEventType.SelectedValue)
                        cmdUpd.Parameters.AddWithValue("@event_parameter", txtParameter.Text.Trim)
                        cmdUpd.Parameters.AddWithValue("@event_text_id_body", cboTemplateBody.SelectedValue)
                        cmdUpd.Parameters.AddWithValue("@event_text_id_subject", cboTemplateSubject.SelectedValue)
                        cmdUpd.Parameters.AddWithValue("@event_mailto_partner", IIf(Me.chkPartner.Checked, 1, 0))
                        cmdUpd.Parameters.AddWithValue("@event_mailto_director", IIf(Me.chkDirector.Checked, 1, 0))
                        cmdUpd.Parameters.AddWithValue("@event_mailto_commercial", IIf(Me.chkCommercialist.Checked, 1, 0))
                        cmdUpd.Parameters.AddWithValue("@event_mailto_developer", IIf(Me.chkDeveloper.Checked, 1, 0))
                        cmdUpd.Parameters.AddWithValue("@event_mailto_production", IIf(Me.chkProduction.Checked, 1, 0))
                        cmdUpd.Parameters.AddWithValue("@event_mailto_technology", IIf(Me.chkTechnolog.Checked, 1, 0))
                        cmdUpd.Parameters.AddWithValue("@event_mailto_logistic", IIf(Me.chkLogistic.Checked, 1, 0))
                        cmdUpd.Parameters.AddWithValue("@event_enabled", IIf(Me.chkEnabled.Checked, 1, 0))
                        cmdUpd.Parameters.AddWithValue("@event_days", nudDays.Value)
                        cmdUpd.Parameters.AddWithValue("@event_exclude_reclamation", IIf(Me.chkExcludeReclamation.Checked, 1, 0))
                        cmdUpd.Parameters.AddWithValue("@event_check_delivery", IIf(Me.chkCheckDelivery.Checked, 1, 0))
                        cmdUpd.Parameters.AddWithValue("@event_check_montage", IIf(Me.chkCheckMontage.Checked, 1, 0))
                        If Me.txtMontageDays.Text <> "" Then
                            cmdUpd.Parameters.AddWithValue("@event_montage_days", CInt(Me.txtMontageDays.Text))
                        Else
                            cmdUpd.Parameters.AddWithValue("@event_montage_days", 0)
                        End If
                        cmdUpd.Parameters.AddWithValue("@event_emailto_extra", txtEmailManual.Text.Trim)
                        cmdUpd.Parameters.AddWithValue("@event_emailto_partner_id", txtPartnerId.Text.Trim)

                        cmdUpd.Parameters.AddWithValue("@intEventId", intEventId)

                        cmdUpd.ExecuteNonQuery()

                    End Using
                End Using
            Else
                strSQL = "INSERT INTO eprod_events (event_desc, event_workstation, event_type, event_parameter, event_text_id_body, event_text_id_subject, event_mailto_partner, " _
                        & " event_mailto_director, event_mailto_commercial, event_mailto_developer, event_mailto_production, event_mailto_technology, " _
                        & " event_mailto_logistic, event_enabled, event_days, event_exclude_reclamation, event_check_delivery, event_check_montage, event_montage_days, event_emailto_extra, event_emailto_partner_id) " _
                            & " VALUES (@event_desc, @event_workstation, @event_type, @event_parameter, @event_text_id_body, @event_text_id_subject, @event_mailto_partner, " _
                        & " @event_mailto_director, @event_mailto_commercial, @event_mailto_developer, @event_mailto_production, @event_mailto_technology, " _
                        & " @event_mailto_logistic, @event_enabled, @event_days, @event_exclude_reclamation, @event_check_delivery, @event_check_montage, @event_montage_days, @event_emailto_extra, @event_emailto_partner_id)"

                Using cmdUpd As New SqlCommand(strSQL, connTools)
                    cmdUpd.Parameters.AddWithValue("@event_desc", Me.txtDescription.Text)
                    cmdUpd.Parameters.AddWithValue("@event_workstation", cboWorkstation.SelectedValue)
                    cmdUpd.Parameters.AddWithValue("@event_type", cboEventType.SelectedValue)
                    cmdUpd.Parameters.AddWithValue("@event_parameter", txtParameter.Text.Trim)
                    cmdUpd.Parameters.AddWithValue("@event_text_id_body", cboTemplateBody.SelectedValue)
                    cmdUpd.Parameters.AddWithValue("@event_text_id_subject", cboTemplateSubject.SelectedValue)
                    cmdUpd.Parameters.AddWithValue("@event_mailto_partner", IIf(Me.chkPartner.Checked, 1, 0))
                    cmdUpd.Parameters.AddWithValue("@event_mailto_director", IIf(Me.chkDirector.Checked, 1, 0))
                    cmdUpd.Parameters.AddWithValue("@event_mailto_commercial", IIf(Me.chkCommercialist.Checked, 1, 0))
                    cmdUpd.Parameters.AddWithValue("@event_mailto_developer", IIf(Me.chkDeveloper.Checked, 1, 0))
                    cmdUpd.Parameters.AddWithValue("@event_mailto_production", IIf(Me.chkProduction.Checked, 1, 0))
                    cmdUpd.Parameters.AddWithValue("@event_mailto_technology", IIf(Me.chkTechnolog.Checked, 1, 0))
                    cmdUpd.Parameters.AddWithValue("@event_mailto_logistic", IIf(Me.chkLogistic.Checked, 1, 0))
                    cmdUpd.Parameters.AddWithValue("@event_enabled", IIf(Me.chkEnabled.Checked, 1, 0))
                    cmdUpd.Parameters.AddWithValue("@event_days", nudDays.Value)
                    cmdUpd.Parameters.AddWithValue("@event_exclude_reclamation", IIf(Me.chkExcludeReclamation.Checked, 1, 0))
                    cmdUpd.Parameters.AddWithValue("@event_check_delivery", IIf(Me.chkCheckDelivery.Checked, 1, 0))
                    cmdUpd.Parameters.AddWithValue("@event_check_montage", IIf(Me.chkCheckMontage.Checked, 1, 0))
                    If Me.txtMontageDays.Text <> "" Then
                        cmdUpd.Parameters.AddWithValue("@event_montage_days", CInt(Me.txtMontageDays.Text))
                    Else
                        cmdUpd.Parameters.AddWithValue("@event_montage_days", 0)
                    End If
                    cmdUpd.Parameters.AddWithValue("@event_emailto_extra", txtEmailManual.Text.Trim)
                    cmdUpd.Parameters.AddWithValue("@event_emailto_partner_id", txtPartnerId.Text.Trim)

                    cmdUpd.ExecuteNonQuery()

                End Using
            End If


        Catch ex As Exception
            modLog.AddToErrorLog(ex.Message)
        End Try
    End Sub

    Private Sub dgText_QueryContinueDrag(sender As Object, e As System.Windows.Forms.QueryContinueDragEventArgs) Handles dgText.QueryContinueDrag

    End Sub

    Private Sub dgText_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgText.SelectionChanged
        Dim intID As Integer
        If blnRefresh Then
            If dgText.Rows.Count > 0 Then
                If dgText.SelectedRows.Count > 0 Then
                    intID = dgText.Rows(dgText.CurrentRow.Index).Cells("event_id").Value
                    Call RefreshRecord(intID)
                End If
            End If
        End If
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Me.txtID.Text = ""
        Me.txtDescription.Text = ""
        Me.cboWorkstation.SelectedIndex = -1
        Me.cboEventType.SelectedIndex = -1
        Me.cboTemplateBody.SelectedIndex = -1
        Me.cboTemplateSubject.SelectedIndex = -1
        Me.chkCommercialist.Checked = False
        Me.chkDeveloper.Checked = False
        Me.chkDirector.Checked = False
        Me.chkLogistic.Checked = False
        Me.chkPartner.Checked = False
        Me.chkProduction.Checked = False
        Me.chkTechnolog.Checked = False
        Me.chkEnabled.Checked = False
        Me.nudDays.Value = 0
        Me.txtParameter.Text = ""
        Me.chkExcludeReclamation.Checked = False
        Me.rtbZadeva.Rtf = ""
        Me.rtbBodySL.Rtf = ""
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim intTxtId As Integer = -1
        Call SaveRecord()
        If Me.txtID.Text <> "" Then
            intTxtId = CInt(txtID.Text)
        End If
        Call RefreshGrid(intTxtId)
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim IntId As Integer = -1
        Dim strSQL As String = ""
        If dgText.Rows.Count > 0 Then
            IntId = dgText.Rows(dgText.CurrentRow.Index).Cells("event_id").Value
            strSQL = "DELETE FROM eprod_events WHERE event_id = @intID"
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@intID", IntId)
                cmd.ExecuteNonQuery()

                If MsgBox("Ali naj pobrišem tudi log?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    strSQL = "DELETE FROM eprod_events_log WHERE event_id = @intID"
                    Using cmd2 As New SqlCommand(strSQL, connTools)
                        cmd2.Parameters.AddWithValue("@intID", IntId)
                        cmd2.ExecuteNonQuery()
                    End Using
                End If
                Call RefreshGrid(-1)
            End Using
        End If
    End Sub

    Private Sub dgText_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgText.CellContentClick

    End Sub

    Private Sub cmdDuplicate_Click(sender As System.Object, e As System.EventArgs) Handles cmdDuplicate.Click
        Me.txtID.Text = ""
    End Sub

    Private Sub Label12_Click(sender As System.Object, e As System.EventArgs) Handles Label12.Click

    End Sub

    Private Sub cmdRunEvent_Click(sender As System.Object, e As System.EventArgs) Handles cmdRunEvent.Click
        If Me.txtID.Text <> "" Then
            Me.Cursor = Cursors.WaitCursor
            frmMainForm.AutoProcessMail(CLng(Me.txtID.Text))
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub cboTemplateSubject_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboTemplateSubject.SelectedIndexChanged
        If blnRefresh Then
            Me.rtbZadeva.Rtf = GetText(cboTemplateSubject.SelectedValue, connTools)
        End If
    End Sub

    Private Sub cboTemplateBody_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboTemplateBody.SelectedIndexChanged
        If blnRefresh Then
            Me.rtbBodySL.Rtf = GetText(cboTemplateBody.SelectedValue, connTools)
        End If
    End Sub
End Class