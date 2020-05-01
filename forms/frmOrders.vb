Imports MySql.Data.MySqlClient
Imports eProdService.cls.msora.DB_MSora

Public Class frmOrders
    Dim blnLoaded As Boolean = False
    Dim strWhere As String = ""
    Private Sub frmOrders_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        blnLoaded = False

        Call FillWorkstationsCombo(cboWorkingPlace)

        cboWorkingPlace.SelectedIndex = -1

        blnLoaded = True
    End Sub


    Private Sub cboWorkingPlace_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboWorkingPlace.KeyDown
        If e.KeyCode = Keys.Delete Then
            cboWorkingPlace.SelectedIndex = -1
        End If
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Call SearchOrders()
    End Sub

    Private Sub SearchOrders()
        Dim strSQL As String = ""

        If Me.txtOrderNr.Text <> "" Then
            strWhere = strWhere & " AND order_nr LIKE '" & Me.txtOrderNr.Text.Trim.Replace("*", "%") & "'"
        End If

        If Me.txtPartnerId.Text <> "" Then
            strWhere = strWhere & " AND partner_id LIKE '" & Me.txtPartnerId.Text.Trim.Replace("*", "%") & "'"
        End If

        If Me.cboWorkingPlace.SelectedIndex > -1 Then
            strWhere = strWhere & " AND working_place = " & cboWorkingPlace.SelectedValue
        End If

        If Me.dtmSendDateFrom.Checked Then
            strWhere = strWhere & " AND send_date >= " & cls.Utils.FormatDate(dtmSendDateFrom.Value)
        End If

        If Me.dtmSendDateTo.Checked Then
            strWhere = strWhere & " AND send_date <= " & cls.Utils.FormatDate(dtmSendDateTo.Value)
        End If

        If Me.dtmFinnishDateFrom.Checked Then
            strWhere = strWhere & " AND finnish_date >= " & cls.Utils.FormatDate(dtmFinnishDateFrom.Value)
        End If

        If Me.dtmFinnishDateTo.Checked Then
            strWhere = strWhere & " AND finnish_date <= " & cls.Utils.FormatDate(dtmFinnishDateTo.Value)
        End If

        strSQL = "SELECT distinct order_nr FROM orders " _
            & " WHERE 2 > 1 " _
            & strWhere

        Using conn As MySqlConnection = GetMyConnection("msora")



            Dim cmd As New MySqlCommand(strSQL, conn)
            Dim dt As DataTable

            dt = GetMyData(cmd)

            dgOrders.DataSource = dt
        End Using
    End Sub



    Private Sub dgOrders_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgOrders.SelectionChanged
        Dim strOrderNr As String = ""
        If blnLoaded Then
            If dgOrders.Rows.Count > 0 Then
                If dgOrders.SelectedRows.Count > 0 Then
                    strOrderNr = dgOrders.Rows(dgOrders.CurrentRow.Index).Cells(0).Value
                    Call LoadData(strOrderNr)
                End If
            End If
        End If

    End Sub

    Private Sub LoadData(ByVal strOrderNr As String)
        Dim strSQL As String = ""

        strSQL = "SELECT order_nr, partner_id, email_addresse, working_place, finnish_date, send_date, mail_title, language FROM orders " _
            & " WHERE order_nr = @strOrderNr " _
            & strWhere

        Using conn As MySqlConnection = GetMyConnection("msora")

            Dim cmd As New MySqlCommand(strSQL, conn)
            cmd.Parameters.AddWithValue("@strOrdernr", strOrderNr)

            Dim dt As DataTable

            dt = GetMyData(cmd)

            dgOrdersDM.DataSource = dt
        End Using
    End Sub

    Private Sub cmdSendmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSendmail.Click
        Dim strOrderNr As String = ""
        Dim intWorkingPlace As Integer = -1
        Dim streMail As String = ""
        Dim dtmSendMail As Date
        Dim dtmFinnishDate As Date
        Dim blnAlreadySend As Boolean
        Dim strLanguage As String

        If Me.dgOrdersDM.Rows.Count > 0 Then
            For i = 0 To dgOrdersDM.SelectedRows.Count - 1
                blnAlreadySend = True
                strOrderNr = dgOrdersDM.SelectedRows(i).Cells("ordernr").Value
                streMail = dgOrdersDM.SelectedRows(i).Cells("email").Value
                If dgOrdersDM.SelectedRows(i).Cells("senddate").Value.ToString <> "" Then
                    dtmSendMail = dgOrdersDM.SelectedRows(i).Cells("senddate").Value
                    blnAlreadySend = False
                End If
                If dgOrdersDM.SelectedRows(i).Cells("finnishdate").Value.ToString <> "" Then
                    dtmFinnishDate = CDate(dgOrdersDM.SelectedRows(i).Cells("finnishdate").Value)
                End If

                intWorkingPlace = dgOrdersDM.SelectedRows(i).Cells("workingplace").Value
                strLanguage = dgOrdersDM.SelectedRows(i).Cells("language").Value

                If Not blnAlreadySend And streMail <> "" Then
                    modLog.AddToErrorLog("Send mail to: " & streMail _
                        & vbCrLf & "Working place: " & TranslateWorkingPlace(intWorkingPlace, strLanguage))
                End If


            Next


        End If
    End Sub
End Class