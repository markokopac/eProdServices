Imports eProdService.cls.msora.DB_MSora
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO

Imports FireSharp.Config
Imports FireSharp.Response
Imports FireSharp.Interfaces
Imports Google.Cloud.Firestore

Module modGoogleFireStore
    Public Enum OrderStatus
        Narocilo_potrjeno = 1
        Priprava_na_tehnicno_obdelavo = 2
        Izdelava_delovnega_naloga = 3
        Narocanje_in_prejem_materiala = 4
        Proizvodnja = 5
        Sprememba_narocila = 21
        Proizvodnja_zaceta = 51
        Proizvodnja_zaceta_stiskalnica = 52
        Proizvodnja_koncana = 53
        Montaza = 6
    End Enum

    Public Enum MontageStatus

        Planirana = 1
        Planirana_dogovorjena = 2
        Koncana = 3
        Rezervirana_Skrita = 10
    End Enum
    Public Function UpdateMSoraOrderStatusDate1(strOrderNrN As String, dtmErfDatum As Date, connKBS As SqlConnection, connTools As SqlConnection) As Boolean
        Dim strSQL As String = ""
        'pogledam, če nalog N obstaja
        'če obstaja, posodobim podatke v tabeli msora_order_info
        strSQL = "SELECT TOP 1 bsx.bsx_vorgangnr, bsx.bsx_bezeichnung, bsx.bsx_druckdatum, bsx.bsx_imeldestatus, bsx.bsx_meldedatum, bsx.bsx_idmeldender, bsx.bsx_iderfasser, " _
                         & " bsx.bsx_idruckstatus, gp.gpx_anrede, gp.gpx_identnummer as bsx_gp_identnummer, gp.gpx_name, gp.gpx_vorname, " _
                         & " usx.usx_user as Kreiral, usx.usx_fullname as komercialist, usx.usx_mail, usx.usx_phone, usx.usx_mobile, " _
                         & " bsx.bsx_gp_id, zastopnik1.GPX_Identnummer as zastopnik, " _
                         & " usxp.usx_fullname as tehnolog, " _
                         & " oi.google_id, oi.order_nr " _
                         & " FROM vkbelegstub bsx " _
                         & " LEFT JOIN klaesuser usxp ON usxp.usx_id = bsx.bsx_iddruckender " _
                         & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iderfasser " _
                         & " LEFT JOIN " & cls.Config.mstr_TOOLSName & ".msora_order_info oi ON oi.order_nr " & cls.Config.GetCollation & " = bsx.bsx_belegnr " _
                         & " LEFT JOIN gp ON bsx.bsx_gp_id = gp.gpx_id " _
                         & " INNER JOIN VKVORGANGSTUB VKS ON vks.vsx_vorgangnr = bsx.bsx_vorgangnr " _
                         & " LEFT JOIN GP Zastopnik1 ON Zastopnik1.GPX_ID = SUBSTRING(VKS.VSX_BETEILIGTE_VERTRETER,4,20) And Zastopnik1.GPX_OJECTIDENTITY = 'K_VertreterRolle' " _
                         & " WHERE bsx.bsx_belegnr = @strOrderNrN "


        Using cmd As New SqlCommand(strSQL, connKBS)
            cmd.Parameters.AddWithValue("@strOrderNrN", strOrderNrN)
            Dim dt As DataTable = GetData(cmd)

            Dim strLanguage As String = GetLanguage(strOrderNrN, connTools, connKBS)

            If dt.Rows.Count > 0 Then


                If UpdateMSoraOrderInfo_Status_Date_1(connTools, strOrderNrN, dt.Rows(0)("bsx_gp_identnummer").ToString, dt.Rows(0)("gpx_vorname").ToString, dt.Rows(0)("gpx_name").ToString,
                                            dt.Rows(0)("bsx_vorgangnr").ToString, dt.Rows(0)("komercialist").ToString, dt.Rows(0)("usx_mail").ToString, dt.Rows(0)("usx_phone").ToString,
                                           dtmErfDatum, strLanguage) Then

                    Return True
                Else
                    Return False
                End If




            Else
                Return False
            End If
        End Using


    End Function

    Private Function UpdateMSoraOrderInfo_Status_Date_1(connTools As SqlConnection, strOrderNrN As String, strPartnerId As String, strPartnerLastName As String, strPartnerFirstName As String,
                                          strProjectNr As String, strContactName As String, strContactMail As String, strContactPhone As String, dtmStatus As Date, strLanguage As String) As Boolean
        Dim strSQL As String = ""
        Dim clFS As New clsOrderFB
        Dim strKey As String = ""
        Dim dtmStatusDate As Date
        Try

            strSQL = "SELECT * FROM msora_order_info WHERE order_nr = @strOrderNrN"
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@strOrderNrN", strOrderNrN)
                Dim dt As DataTable = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    strKey = dt.Rows(0)("google_id").ToString.Trim
                    If Not IsDBNull(dt.Rows(0)("status_date_1")) Then
                        dtmStatusDate = dt.Rows(0)("status_date_1")
                    Else
                        dtmStatusDate = Now.Date
                    End If

                    Call FillFS(dt, clFS)

                        clFS.status_date_1 = dtmStatus.Date

                        clFS.status_text_1 = GetStatusText(strLanguage, OrderStatus.Narocilo_potrjeno, clFS.status_date_1, Nothing)
                        If clFS.status_date_2E <> "" Then
                            clFS.status_text_21 = GetStatusText(strLanguage, OrderStatus.Sprememba_narocila, clFS.status_date_1, clFS.status_date_2E)
                        Else
                            clFS.status_text_21 = GetStatusText(strLanguage, OrderStatus.Sprememba_narocila, clFS.status_date_1, Nothing)
                        End If
                    clFS.status_text_contact = GetStatusTextContact(clFS, connTools)

                    If (dtmStatusDate <> dtmStatus) Or (clFS.status_text_1.Trim <> dt.Rows(0)("status_text_1").ToString.Trim) _
                        Or (clFS.status_text_21.Trim <> dt.Rows(0)("status_text_21").ToString.Trim) Or (clFS.status_text_contact.Trim <> dt.Rows(0)("status_text_contact").ToString.Trim) Then

                        strKey = InsertUpdateGoogleFirestoreDocument(clFS, strKey)

                        If strKey <> "" Then
                            'posodobim še msora_order_info
                            strSQL = "UPDATE msora_order_info SET status_date_1 = @dtmStatus1, status_text_1 = @strStatusText, status_text_21 = @strStatusText21, status_text_contact = @strStatusTextContact WHERE google_id = @strKey"
                            Using cmdU As New SqlCommand(strSQL, connTools)
                                cmdU.Parameters.AddWithValue("@dtmStatus1", dtmStatus.Date)
                                cmdU.Parameters.AddWithValue("@strStatusText", clFS.status_text_1)
                                cmdU.Parameters.AddWithValue("@strStatusText21", clFS.status_text_21)
                                cmdU.Parameters.AddWithValue("@strStatusTextContact", clFS.status_text_contact.Trim)

                                cmdU.Parameters.AddWithValue("@strKey", strKey.Trim)
                                cmdU.ExecuteNonQuery()
                            End Using
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Return False
                        End If
                    Else
                        '
                        clFS.order_nr = strOrderNrN
                    clFS.project_nr = strProjectNr
                    clFS.partner_id = strPartnerId
                    clFS.partner_lastname = strPartnerLastName
                    clFS.partner_name = strPartnerFirstName
                    clFS.status_date_1 = dtmStatus.Date
                    clFS.date_inserted = Now
                    clFS.contact_mail_msora = strContactMail
                    clFS.contact_msora = strContactName
                    clFS.contact_phone_msora = strContactPhone
                    clFS.date_changed = Now
                    clFS.language = strLanguage

                    clFS.status_text_1 = GetStatusText(strLanguage, OrderStatus.Narocilo_potrjeno, clFS.status_date_1, Nothing)
                    clFS.status_text_2 = GetStatusText(strLanguage, OrderStatus.Priprava_na_tehnicno_obdelavo, clFS.status_date_1, Nothing)
                    clFS.status_text_3 = GetStatusText(strLanguage, OrderStatus.Izdelava_delovnega_naloga, Nothing, Nothing)
                    clFS.status_text_4 = GetStatusText(strLanguage, OrderStatus.Narocanje_in_prejem_materiala, Nothing, Nothing)
                    clFS.status_text_5 = GetStatusText(strLanguage, OrderStatus.Proizvodnja, Nothing, Nothing)
                    clFS.status_text_6 = GetStatusText(strLanguage, OrderStatus.Montaza, Nothing, Nothing)

                    clFS.status_text_21 = GetStatusText(strLanguage, OrderStatus.Sprememba_narocila, clFS.status_date_1, Nothing)
                    clFS.status_text_51 = GetStatusText(strLanguage, OrderStatus.Proizvodnja_zaceta, Nothing, Nothing)
                    clFS.status_text_52 = GetStatusText(strLanguage, OrderStatus.Proizvodnja_zaceta_stiskalnica, Nothing, Nothing)
                    clFS.status_text_53 = GetStatusText(strLanguage, OrderStatus.Proizvodnja_koncana, Nothing, Nothing)

                    clFS.status_text_contact = GetStatusTextContact(clFS, connTools)

                    strKey = InsertUpdateGoogleFirestoreDocument(clFS, "")

                    If strKey <> "" Then
                        'posodobim še msora_order_info
                        strSQL = "INSERT INTO msora_order_info (google_id, order_nr, project_nr, partner_id, partner_lastname, partner_name, status_date_1, date_inserted, contact_msora, contact_mail_msora, contact_phone_msora, date_changed, language, " _
                            & " status_text_1, status_text_2, status_text_3, status_text_4, status_text_5, status_text_6, status_text_21, status_text_51, status_text_52, status_text_53, status_text_contact) " _
                        & " VALUES (@google_id, @order_nr, @project_nr, @partner_id, @partner_lastname, @partner_name, @status_date_1, @date_inserted, @contact_msora, @contact_mail_msora, @contact_phone_msora, @date_changed, @language, " _
                        & " @status_text_1, @status_text_2, @status_text_3, @status_text_4, @status_text_5, @status_text_6, @status_text_21, @status_text_51, @status_text_52, @status_text_53, @status_text_contact)"
                        Using cmdU As New SqlCommand(strSQL, connTools)
                            cmdU.Parameters.AddWithValue("@google_id", strKey)
                            cmdU.Parameters.AddWithValue("@order_nr", clFS.order_nr)
                            cmdU.Parameters.AddWithValue("@project_nr", clFS.project_nr)
                            cmdU.Parameters.AddWithValue("@partner_id", clFS.partner_id)
                            cmdU.Parameters.AddWithValue("@partner_lastname", clFS.partner_lastname)
                            cmdU.Parameters.AddWithValue("@partner_name", clFS.partner_name)
                            cmdU.Parameters.AddWithValue("@status_date_1", dtmStatus.Date)
                            cmdU.Parameters.AddWithValue("@date_inserted", clFS.date_inserted)
                            cmdU.Parameters.AddWithValue("@contact_msora", clFS.contact_msora)
                            cmdU.Parameters.AddWithValue("@contact_mail_msora", clFS.contact_mail_msora)
                            cmdU.Parameters.AddWithValue("@contact_phone_msora", clFS.contact_phone_msora)
                            cmdU.Parameters.AddWithValue("@date_changed", clFS.date_changed)
                            cmdU.Parameters.AddWithValue("@language", clFS.language)
                            cmdU.Parameters.AddWithValue("@status_text_1", clFS.status_text_1.Trim)
                            cmdU.Parameters.AddWithValue("@status_text_2", clFS.status_text_2.Trim)
                            cmdU.Parameters.AddWithValue("@status_text_3", clFS.status_text_3.Trim)
                            cmdU.Parameters.AddWithValue("@status_text_4", clFS.status_text_4.Trim)
                            cmdU.Parameters.AddWithValue("@status_text_5", clFS.status_text_5.Trim)
                            cmdU.Parameters.AddWithValue("@status_text_6", clFS.status_text_6.Trim)
                            cmdU.Parameters.AddWithValue("@status_text_21", clFS.status_text_21.Trim)
                            cmdU.Parameters.AddWithValue("@status_text_51", clFS.status_text_51.Trim)
                            cmdU.Parameters.AddWithValue("@status_text_52", clFS.status_text_52.Trim)
                            cmdU.Parameters.AddWithValue("@status_text_53", clFS.status_text_53.Trim)
                            cmdU.Parameters.AddWithValue("@status_text_contact", clFS.status_text_contact.Trim)

                            cmdU.ExecuteNonQuery()
                        End Using
                        Return True
                    Else
                        Return False
                    End If
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try

    End Function
    Public Function UpdateMSoraOrderInfo_Status_Date_2E(connTools As SqlConnection, connKBS As SqlConnection, strOrderNrN As String, dtmStatus As Date) As Boolean
        Dim strSQL As String = ""
        Dim clFS As New clsOrderFB
        Dim strKey As String = ""
        Dim dtmStatusDate As Date

        strSQL = "SELECT * FROM msora_order_info WHERE order_nr = @strOrderNrN"
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strOrderNrN", strOrderNrN)
            Dim dt As DataTable = GetData(cmd)
            If dt.Rows.Count > 0 Then
                strKey = dt.Rows(0)("google_id").ToString
                If Not IsDBNull(dt.Rows(0)("status_date_2E")) Then
                    dtmStatusDate = CDate(dt.Rows(0)("status_date_2E"))
                Else
                    dtmStatusDate = Nothing
                End If

                'preverim, če je nalog potrjen
                Call SetMeldeStatus(strOrderNrN, dtmStatus, connKBS)


                Call FillFS(dt, clFS)

                clFS.status_date_2E = dtmStatus
                clFS.status_text_2 = GetStatusText(clFS.language, OrderStatus.Priprava_na_tehnicno_obdelavo, clFS.status_date_1, clFS.status_date_2E)
                clFS.status_text_21 = GetStatusText(clFS.language, OrderStatus.Sprememba_narocila, clFS.status_date_1, clFS.status_date_2E)
                If clFS.status_date_3E <> "" Then
                    clFS.status_text_3 = GetStatusText(clFS.language, OrderStatus.Izdelava_delovnega_naloga, clFS.status_date_2E, clFS.status_date_3E)
                Else
                    clFS.status_text_3 = GetStatusText(clFS.language, OrderStatus.Izdelava_delovnega_naloga, clFS.status_date_2E, Nothing)
                End If
                clFS.status_text_contact = GetStatusTextContact(clFS, connTools)

                If (dtmStatusDate <> dtmStatus) Or (clFS.status_text_2.Trim <> dt.Rows(0)("status_text_2").ToString.Trim) _
                    Or (clFS.status_text_21.Trim <> dt.Rows(0)("status_text_21").ToString.Trim) Or (clFS.status_text_contact.Trim <> dt.Rows(0)("status_text_contact").ToString.Trim) Then

                    strKey = InsertUpdateGoogleFirestoreDocument(clFS, strKey)

                    If strKey <> "" Then
                        'posodobim še msora_order_info
                        strSQL = "UPDATE msora_order_info SET status_date_2E = @dtmStatus2E, status_text_2 = @strStatusText, status_text_21 = @strStatusText21, status_text_contact = @strStatusTextContact WHERE google_id = @strKey"
                        Using cmdU As New SqlCommand(strSQL, connTools)
                            cmdU.Parameters.AddWithValue("@dtmStatus2E", dtmStatus.Date)
                            cmdU.Parameters.AddWithValue("@strStatusText", clFS.status_text_2.Trim)
                            cmdU.Parameters.AddWithValue("@strStatusText21", clFS.status_text_21.Trim)
                            cmdU.Parameters.AddWithValue("@strStatusTextContact", clFS.status_text_contact.Trim)

                            cmdU.Parameters.AddWithValue("@strKey", strKey.Trim)
                            cmdU.ExecuteNonQuery()
                        End Using
                        Return True
                    Else
                        Return False
                    End If

                Else
                    'ni podatka
                    Return False
                End If
            Else
                Return False
            End If
        End Using

    End Function

    Public Function UpdateMSoraOrderInfo_Status_Date_3E(connTools As SqlConnection, strOrderNrN As String, dtmStatus As Date) As Boolean
        Dim strSQL As String = ""
        Dim clFS As New clsOrderFB
        Dim strKey As String = ""
        Dim dtmStatusDate As Date

        strSQL = "SELECT * FROM msora_order_info WHERE order_nr = @strOrderNrN"
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strOrderNrN", strOrderNrN)
            Dim dt As DataTable = GetData(cmd)
            If dt.Rows.Count > 0 Then
                strKey = dt.Rows(0)("google_id").ToString
                If Not IsDBNull(dt.Rows(0)("status_date_3E")) Then
                    dtmStatusDate = CDate(dt.Rows(0)("status_date_3E"))
                Else
                    dtmStatusDate = Nothing
                End If


                Call FillFS(dt, clFS)

                clFS.status_date_3E = dtmStatus
                If clFS.status_date_2E <> "" Then
                    clFS.status_text_3 = GetStatusText(clFS.language, OrderStatus.Izdelava_delovnega_naloga, clFS.status_date_2E, clFS.status_date_3E)
                Else
                    clFS.status_text_3 = GetStatusText(clFS.language, OrderStatus.Izdelava_delovnega_naloga, Nothing, clFS.status_date_3E)
                End If

                If clFS.status_date_4E <> "" Then
                    clFS.status_text_4 = GetStatusText(clFS.language, OrderStatus.Narocanje_in_prejem_materiala, clFS.status_date_3E, clFS.status_date_4E)
                Else
                    clFS.status_text_4 = GetStatusText(clFS.language, OrderStatus.Narocanje_in_prejem_materiala, clFS.status_date_3E, Nothing)
                End If
                clFS.status_text_contact = GetStatusTextContact(clFS, connTools)


                If (dtmStatusDate <> dtmStatus) Or (clFS.status_text_3.Trim <> dt.Rows(0)("status_text_3").ToString.Trim) _
                    Or (clFS.status_text_4.Trim <> dt.Rows(0)("status_text_4").ToString.Trim) Or (clFS.status_text_contact.Trim <> dt.Rows(0)("status_text_contact").ToString.Trim) Then

                    strKey = InsertUpdateGoogleFirestoreDocument(clFS, strKey)

                    If strKey <> "" Then
                        'posodobim še msora_order_info
                        strSQL = "UPDATE msora_order_info SET status_date_3E = @dtmStatus, status_text_3 = @strStatusText, status_text_4 = @strStatusText4, status_text_contact = @strStatusTextContact WHERE google_id = @strKey"
                        Using cmdU As New SqlCommand(strSQL, connTools)
                            cmdU.Parameters.AddWithValue("@dtmStatus", dtmStatus.Date)
                            cmdU.Parameters.AddWithValue("@strStatusText", clFS.status_text_3.Trim)
                            cmdU.Parameters.AddWithValue("@strStatusText4", clFS.status_text_4.Trim)
                            cmdU.Parameters.AddWithValue("@strStatusTextContact", clFS.status_text_contact.Trim)

                            cmdU.Parameters.AddWithValue("@strKey", strKey.Trim)
                            cmdU.ExecuteNonQuery()
                        End Using
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                    End If
                Else
                    Return False
            End If

        End Using

    End Function

    Public Function UpdateMSoraOrderInfo_Status_Date_4E(connTools As SqlConnection, strOrderNrN As String, dtmStatus As Date) As Boolean
        Dim strSQL As String = ""
        Dim clFS As New clsOrderFB
        Dim strKey As String = ""
        Dim dtmStatusDate As Date

        strSQL = "SELECT * FROM msora_order_info WHERE order_nr = @strOrderNrN"
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strOrderNrN", strOrderNrN)
            Dim dt As DataTable = GetData(cmd)
            If dt.Rows.Count > 0 Then
                strKey = dt.Rows(0)("google_id").ToString
                If Not IsDBNull(dt.Rows(0)("status_date_4E")) Then
                    dtmStatusDate = CDate(dt.Rows(0)("status_date_4E"))
                Else
                    dtmStatusDate = Nothing
                End If

                Call FillFS(dt, clFS)

                clFS.status_date_4E = dtmStatus
                If clFS.status_date_3E <> "" Then
                    clFS.status_text_4 = GetStatusText(clFS.language, OrderStatus.Narocanje_in_prejem_materiala, clFS.status_date_3E, clFS.status_date_4E)
                Else
                    clFS.status_text_4 = GetStatusText(clFS.language, OrderStatus.Narocanje_in_prejem_materiala, Nothing, clFS.status_date_4E)
                End If

                clFS.status_text_contact = GetStatusTextContact(clFS, connTools)

                If (dtmStatusDate <> dtmStatus) Or (clFS.status_text_4.Trim <> dt.Rows(0)("status_text_4").ToString.Trim) Or (clFS.status_text_contact.Trim <> dt.Rows(0)("status_text_contact").ToString.Trim) Then

                    strKey = InsertUpdateGoogleFirestoreDocument(clFS, strKey)

                    If strKey <> "" Then
                        'posodobim še msora_order_info
                        strSQL = "UPDATE msora_order_info SET status_date_4E = @dtmStatus, status_text_4 = @strStatusText, status_text_contact = @strStatusTextContact WHERE google_id = @strKey"
                        Using cmdU As New SqlCommand(strSQL, connTools)
                            cmdU.Parameters.AddWithValue("@dtmStatus", dtmStatus.Date)
                            cmdU.Parameters.AddWithValue("@strStatusText", clFS.status_text_4.Trim)
                            cmdU.Parameters.AddWithValue("@strStatusTextContact", clFS.status_text_contact.Trim)
                            cmdU.Parameters.AddWithValue("@strKey", strKey.Trim)
                            cmdU.ExecuteNonQuery()
                        End Using
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                    End If
                Else
                    Return False
            End If

        End Using

    End Function

    Public Function UpdateMSoraOrderInfo_Status_Date_51(connTools As SqlConnection, strOrderNrN As String, dtmStatus As Date) As Boolean
        Dim strSQL As String = ""
        Dim clFS As New clsOrderFB
        Dim strKey As String = ""
        Dim dtmStatusDate As Date

        strSQL = "SELECT * FROM msora_order_info WHERE order_nr = @strOrderNrN"
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strOrderNrN", strOrderNrN)
            Dim dt As DataTable = GetData(cmd)
            If dt.Rows.Count > 0 Then
                strKey = dt.Rows(0)("google_id").ToString

                If Not IsDBNull(dt.Rows(0)("status_date_51")) Then
                    dtmStatusDate = CDate(dt.Rows(0)("status_date_51"))
                Else
                    dtmStatusDate = Nothing
                End If

                Call FillFS(dt, clFS)

                clFS.status_date_51 = dtmStatus
                clFS.status_text_51 = GetStatusText(clFS.language, OrderStatus.Proizvodnja_zaceta, clFS.status_date_51, Nothing)
                clFS.status_text_contact = GetStatusTextContact(clFS, connTools)

                If (dtmStatusDate <> dtmStatus) Or (clFS.status_text_51.Trim <> dt.Rows(0)("status_text_51").ToString.Trim) _
                    Or (clFS.status_text_contact.Trim <> dt.Rows(0)("status_text_contact").ToString.Trim) Then

                    strKey = InsertUpdateGoogleFirestoreDocument(clFS, strKey)

                    If strKey <> "" Then
                        'posodobim še msora_order_info
                        strSQL = "UPDATE msora_order_info SET status_date_51 = @dtmStatus, status_text_51 = @strStatusText, status_text_contact = @strStatusTextContact WHERE google_id = @strKey"
                        Using cmdU As New SqlCommand(strSQL, connTools)
                            cmdU.Parameters.AddWithValue("@dtmStatus", dtmStatus.Date)
                            cmdU.Parameters.AddWithValue("@strStatusText", clFS.status_text_51.Trim)
                            cmdU.Parameters.AddWithValue("@strStatusTextContact", clFS.status_text_contact.Trim)

                            cmdU.Parameters.AddWithValue("@strKey", strKey.Trim)
                            cmdU.ExecuteNonQuery()
                        End Using
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If

        End Using

    End Function

    Public Function UpdateMSoraOrderInfo_Status_Date_52(connTools As SqlConnection, strOrderNrN As String, dtmStatus As Date) As Boolean
        Dim strSQL As String = ""
        Dim clFS As New clsOrderFB
        Dim strKey As String = ""
        Dim dtmStatusDate As Date

        strSQL = "SELECT * FROM msora_order_info WHERE order_nr = @strOrderNrN"
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strOrderNrN", strOrderNrN)
            Dim dt As DataTable = GetData(cmd)
            If dt.Rows.Count > 0 Then
                strKey = dt.Rows(0)("google_id").ToString

                If Not IsDBNull(dt.Rows(0)("status_date_52")) Then
                    dtmStatusDate = CDate(dt.Rows(0)("status_date_52"))
                Else
                    dtmStatusDate = Nothing
                End If

                Call FillFS(dt, clFS)

                clFS.status_date_52 = dtmStatus
                clFS.status_text_52 = GetStatusText(clFS.language, OrderStatus.Proizvodnja_zaceta_stiskalnica, clFS.status_date_52, Nothing)
                clFS.status_text_contact = GetStatusTextContact(clFS, connTools)

                If (dtmStatusDate <> dtmStatus) Or (clFS.status_text_52.Trim <> dt.Rows(0)("status_text_52").ToString.Trim) Or (clFS.status_text_contact.Trim <> dt.Rows(0)("status_text_contact").ToString.Trim) Then

                    strKey = InsertUpdateGoogleFirestoreDocument(clFS, strKey)

                    If strKey <> "" Then
                        'posodobim še msora_order_info
                        strSQL = "UPDATE msora_order_info SET status_date_52 = @dtmStatus, status_text_52 = @strStatusText, status_text_contact = @strStatusTextContact WHERE google_id = @strKey"
                        Using cmdU As New SqlCommand(strSQL, connTools)
                            cmdU.Parameters.AddWithValue("@dtmStatus", dtmStatus.Date)
                            cmdU.Parameters.AddWithValue("@strStatusText", clFS.status_text_52.Trim)
                            cmdU.Parameters.AddWithValue("@strStatusTextContact", clFS.status_text_contact.Trim)

                            cmdU.Parameters.AddWithValue("@strKey", strKey.Trim)
                            cmdU.ExecuteNonQuery()
                        End Using
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If

        End Using

    End Function

    Public Function UpdateMSoraOrderInfo_Status_Date_53(connTools As SqlConnection, strOrderNrN As String, dtmStatus As Date) As Boolean
        Dim strSQL As String = ""
        Dim clFS As New clsOrderFB
        Dim strKey As String = ""
        Dim dtmStatusDate As Date

        strSQL = "SELECT * FROM msora_order_info WHERE order_nr = @strOrderNrN"
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strOrderNrN", strOrderNrN)
            Dim dt As DataTable = GetData(cmd)
            If dt.Rows.Count > 0 Then
                strKey = dt.Rows(0)("google_id").ToString

                If Not IsDBNull(dt.Rows(0)("status_date_53")) Then
                    dtmStatusDate = CDate(dt.Rows(0)("status_date_53"))
                Else
                    dtmStatusDate = Nothing
                End If

                Call FillFS(dt, clFS)

                clFS.status_date_53 = dtmStatus
                clFS.status_text_53 = GetStatusText(clFS.language, OrderStatus.Proizvodnja_koncana, Nothing, clFS.status_date_53)
                clFS.status_text_contact = GetStatusTextContact(clFS, connTools)

                If (dtmStatusDate <> dtmStatus) Or (clFS.status_text_53.Trim <> dt.Rows(0)("status_text_53").ToString.Trim) Or (clFS.status_text_contact.Trim <> dt.Rows(0)("status_text_contact").ToString.Trim) Then

                    strKey = InsertUpdateGoogleFirestoreDocument(clFS, strKey)

                    If strKey <> "" Then
                        'posodobim še msora_order_info
                        strSQL = "UPDATE msora_order_info SET status_date_53 = @dtmStatus, status_text_53 = @strStatusText, status_text_contact = @strStatusTextContact WHERE google_id = @strKey"
                        Using cmdU As New SqlCommand(strSQL, connTools)
                            cmdU.Parameters.AddWithValue("@dtmStatus", dtmStatus.Date)
                            cmdU.Parameters.AddWithValue("@strStatusText", clFS.status_text_53.Trim)
                            cmdU.Parameters.AddWithValue("@strStatusTextContact", clFS.status_text_contact.Trim)
                            cmdU.Parameters.AddWithValue("@strKey", strKey.Trim)
                            cmdU.ExecuteNonQuery()
                        End Using
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If

        End Using

    End Function

    Public Function UpdateMSoraOrderInfo_Status_Date_6(connTools As SqlConnection, strOrderNrN As String, dtmStatus As Date, intStatus As MontageStatus) As Boolean
        Dim strSQL As String = ""
        Dim clFS As New clsOrderFB
        Dim strKey As String = ""
        Dim strKWDate As String = ""

        strSQL = "SELECT * FROM msora_order_info WHERE order_nr = @strOrderNrN"
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strOrderNrN", strOrderNrN)
            Dim dt As DataTable = GetData(cmd)

            If dt.Rows.Count > 0 Then

                strKey = dt.Rows(0)("google_id").ToString
                If Not IsDBNull(dt.Rows(0)("status_date_6")) Then
                    strKWDate = dt.Rows(0)("status_date_6").ToString.Trim
                Else
                    strKWDate = ""
                End If

                Call FillFS(dt, clFS)

                If clFS.status_date_53.Trim <> "" Then
                    If intStatus < 10 Then
                        clFS.status_date_6 = TranslateKW(clFS.language) & " " & cls.Utils.GetWeek(dtmStatus) & "/" & dtmStatus.Year
                    Else
                        clFS.status_date_6 = ""
                    End If
                Else
                    'ni še v proizvodnji
                    clFS.status_date_6 = ""
                End If
                'če ni v proizvodnji, oz tudi montaža ni določena, potem nič ne pokažem, tudi če je že planirana
                'status se spremeni šele z dnem, ko je nalog v proizvodnji
                If clFS.status_date_6 = "" Then
                    clFS.status_text_6 = GetStatusText(clFS.language, OrderStatus.Montaza, Nothing, Nothing, MontageStatus.Rezervirana_Skrita)
                Else
                    clFS.status_text_6 = GetStatusText(clFS.language, OrderStatus.Montaza, Nothing, Nothing, intStatus)
                End If
                clFS.status_text_contact = GetStatusTextContact(clFS, connTools)

                If (strKWDate.Trim <> clFS.status_date_6.Trim) Or (clFS.status_text_6.Trim <> dt.Rows(0)("status_text_6").ToString.Trim) Or (clFS.status_text_contact.Trim <> dt.Rows(0)("status_text_contact").ToString.Trim) Then

                    strKey = InsertUpdateGoogleFirestoreDocument(clFS, strKey)

                    If strKey <> "" Then
                        'posodobim še msora_order_info
                        strSQL = "UPDATE msora_order_info SET status_date_6 = @strKWDate, status_text_6 = @strStatusText, status_text_contact = @strStatusTextContact WHERE google_id = @strKey"
                        Using cmdU As New SqlCommand(strSQL, connTools)
                            cmdU.Parameters.AddWithValue("@strKWDate", clFS.status_date_6.Trim)
                            cmdU.Parameters.AddWithValue("@strStatusText", clFS.status_text_6.Trim)
                            cmdU.Parameters.AddWithValue("@strStatusTextContact", clFS.status_text_contact.Trim)
                            cmdU.Parameters.AddWithValue("@strKey", strKey.Trim)
                            cmdU.ExecuteNonQuery()
                        End Using
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                    End If
                Else
                    Return False
            End If

        End Using

    End Function


    Private Function TranslateKW(strLanguage As String) As String
        Select Case strLanguage.ToUpper.Trim
            Case "SL"
                Return "Teden"
            Case "EN"
                Return "Week"
            Case "DE"
                Return "Woche"
            Case "FR"
                Return "La semaine"
            Case "IT"
                Return "Settimana"
            Case "HR"
                Return "Tjedan"
            Case Else
                Return "Teden"
        End Select

    End Function

    Private Function GetStatusTextContact(clFB As clsOrderFB, connTools As SqlConnection) As String
        'dokler ni datuma montaže, potem dam za kontakt komercialista, ko je določena montaža pa dam za kontakt Luka
        'prevodi so v msora_order_text
        'kontakt_text
        'kontakt_text2

        Dim strSQL As String = ""
        Dim strText As String
        Try

            '        strSQL = "SELECT kontakt_text, kontakt_text2 FROM msora_order_text WHERE language = @language"
            '       Using cmd As New SqlCommand(strSQL, connTools)
            '      cmd.Parameters.AddWithValue("@language", clFB.language)
            '     Dim dt As DataTable = GetData(cmd)
            If clFB.status_date_6 <> "" Then
                'pokažem tekst kontakt_text2
                'strText = dt.Rows(0)("kontakt_text2")
                strText = "kontakt_text"
            Else
                'drugače pa kontakt_text 
                'strText = dt.Rows(0)("kontakt_text")
                strText = "kontakt_text1"
            End If

            'tekst potem še sparsam, zaenkrat fiksni podatki
            '        strText = strText.Replace("<?contact_msora>", clFB.contact_msora.Trim)
            '   strText = strText.Replace("<?contact_phone_msora>", clFB.contact_phone_msora.Trim)
            '  strText = strText.Replace("<?contact_mail_msora>", clFB.contact_mail_msora.Trim)

            Return strText
            'End Using
        Catch ex As Exception
            Return ""
        End Try

    End Function
    Private Sub FillFS(dt As DataTable, ByRef clFS As clsOrderFB)
        Try

            If dt.Rows.Count > 0 Then
                clFS.date_inserted = dt.Rows(0)("date_inserted")
                clFS.order_nr = dt.Rows(0)("order_nr").ToString
                clFS.project_nr = dt.Rows(0)("project_nr").ToString
                clFS.partner_id = dt.Rows(0)("partner_id").ToString

                clFS.partner_lastname = dt.Rows(0)("partner_lastname").ToString
                clFS.partner_name = dt.Rows(0)("partner_name").ToString

                'spremenim, da bo vedno kazalo samo datum, brez ure
                If dt.Rows(0)("status_date_1").ToString <> "" Then
                    clFS.status_date_1 = CDate(dt.Rows(0)("status_date_1")).Date
                Else
                    clFS.status_date_1 = ""
                End If

                If dt.Rows(0)("status_date_2E").ToString <> "" Then
                    clFS.status_date_2E = CDate(dt.Rows(0)("status_date_2E")).Date
                Else
                    clFS.status_date_2E = ""
                End If

                If dt.Rows(0)("status_date_3E").ToString <> "" Then
                    clFS.status_date_3E = CDate(dt.Rows(0)("status_date_3E")).Date
                Else
                    clFS.status_date_3E = ""
                End If

                If dt.Rows(0)("status_date_4E").ToString <> "" Then
                    clFS.status_date_4E = CDate(dt.Rows(0)("status_date_4E")).Date
                Else
                    clFS.status_date_4E = ""
                End If

                If dt.Rows(0)("status_date_51").ToString <> "" Then
                    clFS.status_date_51 = CDate(dt.Rows(0)("status_date_51")).Date
                Else
                    clFS.status_date_51 = ""
                End If

                If dt.Rows(0)("status_date_52").ToString <> "" Then
                    clFS.status_date_52 = CDate(dt.Rows(0)("status_date_52")).Date
                Else
                    clFS.status_date_52 = ""
                End If

                If dt.Rows(0)("status_date_53").ToString <> "" Then
                    clFS.status_date_53 = CDate(dt.Rows(0)("status_date_53")).Date
                Else
                    clFS.status_date_53 = ""
                End If

                If dt.Rows(0)("status_date_6").ToString <> "" Then
                    clFS.status_date_6 = dt.Rows(0)("status_date_6").ToString.Trim
                Else
                    clFS.status_date_6 = ""
                End If

                clFS.contact_mail_msora = dt.Rows(0)("contact_mail_msora").ToString

                clFS.contact_msora = dt.Rows(0)("contact_msora").ToString
                clFS.contact_phone_msora = dt.Rows(0)("contact_phone_msora").ToString

                clFS.remark = dt.Rows(0)("remark").ToString

                clFS.language = dt.Rows(0)("language").ToString

                clFS.status_text_1 = dt.Rows(0)("status_text_1").ToString
                clFS.status_text_2 = dt.Rows(0)("status_text_2").ToString
                clFS.status_text_3 = dt.Rows(0)("status_text_3").ToString
                clFS.status_text_4 = dt.Rows(0)("status_text_4").ToString
                clFS.status_text_5 = dt.Rows(0)("status_text_5").ToString
                clFS.status_text_6 = dt.Rows(0)("status_text_6").ToString

                clFS.status_text_21 = dt.Rows(0)("status_text_21").ToString
                clFS.status_text_51 = dt.Rows(0)("status_text_51").ToString
                clFS.status_text_52 = dt.Rows(0)("status_text_52").ToString
                clFS.status_text_53 = dt.Rows(0)("status_text_53").ToString



                clFS.date_changed = Now
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function InsertUpdateGoogleFirestoreCollection(clFS As clsOrderFB, strKeyUpdate As String) As String
        Try

            Dim path As String = AppDomain.CurrentDomain.BaseDirectory + cls.Config.FirebaseAuthSecret
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path)
            Dim db As FirestoreDb = FirestoreDb.Create(cls.Config.FirebaseBasePath)

            Dim strKey As String = ""

            Dim docRef As DocumentReference = db.Collection("orders").Document("status")
            Dim colRef As CollectionReference = docRef.Collection("info")

            Dim dict As New Dictionary(Of String, Object)

            If strKeyUpdate = "" Then
                strKey = colRef.Document.Id
            Else
                strKey = strKeyUpdate
            End If

            strKey = strKey.Trim

            dict.Add("date_inserted", clFS.date_inserted.ToString)
            dict.Add("order_nr", clFS.order_nr.ToString)
            dict.Add("project_nr", clFS.project_nr.ToString)
            dict.Add("partner_id", clFS.partner_id.ToString)
            dict.Add("partner_lastname", clFS.partner_lastname.ToString)
            dict.Add("partner_name", clFS.partner_name.ToString)
            dict.Add("contact_msora", clFS.contact_msora.ToString)
            dict.Add("contact_mail_msora", clFS.contact_mail_msora.ToString)
            dict.Add("contact_phone_msora", clFS.contact_phone_msora.ToString)
            dict.Add("status_date_1", clFS.status_date_1.ToString)
            dict.Add("status_date_2E", clFS.status_date_2E.ToString)
            dict.Add("status_date_3E", clFS.status_date_3E.ToString)
            dict.Add("status_date_4E", clFS.status_date_4E.ToString)
            dict.Add("status_date_51", clFS.status_date_51.ToString)
            dict.Add("status_date_52", clFS.status_date_52.ToString)
            dict.Add("status_date_53", clFS.status_date_53.ToString)
            dict.Add("status_date_6", clFS.status_date_6.ToString)

            dict.Add("change_date", clFS.date_changed.ToString)
            dict.Add("remark", clFS.remark.ToString)

            dict.Add("language", clFS.language.ToString)

            dict.Add("status_text_1", clFS.status_text_1.ToString)
            dict.Add("status_text_2", clFS.status_text_2.ToString)
            dict.Add("status_text_3", clFS.status_text_3.ToString)
            dict.Add("status_text_4", clFS.status_text_4.ToString)
            dict.Add("status_text_5", clFS.status_text_5.ToString)
            dict.Add("status_text_6", clFS.status_text_6.ToString)

            dict.Add("status_text_21", clFS.status_text_21.ToString)
            dict.Add("status_text_51", clFS.status_text_51.ToString)
            dict.Add("status_text_52", clFS.status_text_52.ToString)
            dict.Add("status_text_53", clFS.status_text_53.ToString)

            colRef.Document(strKey).SetAsync(dict)

            db = Nothing

            Return strKey
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return ""
        End Try
    End Function

    Private Function InsertUpdateGoogleFirestoreDocument(clFS As clsOrderFB, strKeyUpdate As String) As String
        Try

            'za testiranje
            'If clFS.order_nr.Trim <> "N200771" Then
            'Return ""
            'End If

            Dim path As String = AppDomain.CurrentDomain.BaseDirectory + cls.Config.FirebaseAuthSecret
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path)
            Dim db As FirestoreDb = FirestoreDb.Create(cls.Config.FirebaseBasePath)

            Dim strKey As String = ""

            Dim docRef As DocumentReference

            If strKeyUpdate = "" Then
                docRef = db.Collection("orders").Document
                strKey = docRef.Id.Trim
            Else
                strKey = strKeyUpdate.Trim
                docRef = db.Collection("orders").Document(strKey)
            End If


            'Dim colRef As CollectionReference = docRef.Collection("info")

            Dim dict As New Dictionary(Of String, Object)

            dict.Add("date_inserted", clFS.date_inserted.ToString.Trim)
            dict.Add("order_nr", clFS.order_nr.ToString.Trim)
            dict.Add("project_nr", clFS.project_nr.ToString.Trim)
            dict.Add("partner_id", clFS.partner_id.ToString.Trim)
            dict.Add("partner_lastname", clFS.partner_lastname.ToString.Trim)
            dict.Add("partner_name", clFS.partner_name.ToString.Trim)
            dict.Add("contact_msora", clFS.contact_msora.ToString.Trim)
            dict.Add("contact_mail_msora", clFS.contact_mail_msora.ToString.Trim)
            dict.Add("contact_phone_msora", clFS.contact_phone_msora.ToString.Trim)
            dict.Add("status_date_1", clFS.status_date_1.ToString.Trim)
            dict.Add("status_date_2E", clFS.status_date_2E.ToString.Trim)
            dict.Add("status_date_3E", clFS.status_date_3E.ToString.Trim)
            dict.Add("status_date_4E", clFS.status_date_4E.ToString.Trim)
            dict.Add("status_date_51", clFS.status_date_51.ToString.Trim)
            dict.Add("status_date_52", clFS.status_date_52.ToString.Trim)
            dict.Add("status_date_53", clFS.status_date_53.ToString.Trim)
            dict.Add("status_date_6", clFS.status_date_6.ToString.Trim)

            dict.Add("change_date", clFS.date_changed.ToString.Trim)
            dict.Add("remark", clFS.remark.ToString.Trim)

            dict.Add("language", clFS.language.ToString.Trim)

            dict.Add("status_text_1", clFS.status_text_1.ToString.Trim)
            dict.Add("status_text_2", clFS.status_text_2.ToString.Trim)
            dict.Add("status_text_3", clFS.status_text_3.ToString.Trim)
            dict.Add("status_text_4", clFS.status_text_4.ToString.Trim)
            dict.Add("status_text_5", clFS.status_text_5.ToString.Trim)
            dict.Add("status_text_6", clFS.status_text_6.ToString.Trim)
            dict.Add("status_text_21", clFS.status_text_21.ToString.Trim)
            dict.Add("status_text_51", clFS.status_text_51.ToString.Trim)
            dict.Add("status_text_52", clFS.status_text_52.ToString.Trim)
            dict.Add("status_text_53", clFS.status_text_53.ToString.Trim)
            dict.Add("status_text_contact", clFS.status_text_contact.ToString.Trim)

            If strKeyUpdate = "" Then
                strKey = docRef.Id.Trim
                docRef.SetAsync(dict)
            Else
                strKey = strKeyUpdate.Trim
                docRef.UpdateAsync(dict)
            End If
            'colRef.Document(strKey).SetAsync(dict)



            db = Nothing

            Return strKey
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return ""
        End Try
    End Function

    Private Function InsertGoogleFirestoreDocument(clFS As clsOrderFB, strKey As String) As String
        Try


            Dim path As String = AppDomain.CurrentDomain.BaseDirectory + cls.Config.FirebaseAuthSecret
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path)
            Dim db As FirestoreDb = FirestoreDb.Create(cls.Config.FirebaseBasePath)

            Dim docRef As DocumentReference = db.Collection("orders").Document(strKey)

            Dim dict As New Dictionary(Of String, Object)

            dict.Add("date_inserted", clFS.date_inserted.ToString.Trim)
            dict.Add("order_nr", clFS.order_nr.ToString.Trim)
            dict.Add("project_nr", clFS.project_nr.ToString.Trim)
            dict.Add("partner_id", clFS.partner_id.ToString.Trim)
            dict.Add("partner_lastname", clFS.partner_lastname.ToString.Trim)
            dict.Add("partner_name", clFS.partner_name.ToString.Trim)
            dict.Add("contact_msora", clFS.contact_msora.ToString.Trim)
            dict.Add("contact_mail_msora", clFS.contact_mail_msora.ToString.Trim)
            dict.Add("contact_phone_msora", clFS.contact_phone_msora.ToString.Trim)
            dict.Add("status_date_1", clFS.status_date_1.ToString.Trim)
            dict.Add("status_date_2E", clFS.status_date_2E.ToString.Trim)
            dict.Add("status_date_3E", clFS.status_date_3E.ToString.Trim)
            dict.Add("status_date_4E", clFS.status_date_4E.ToString.Trim)
            dict.Add("status_date_51", clFS.status_date_51.ToString.Trim)
            dict.Add("status_date_52", clFS.status_date_52.ToString.Trim)
            dict.Add("status_date_53", clFS.status_date_53.ToString.Trim)
            dict.Add("status_date_6", clFS.status_date_6.ToString.Trim)

            dict.Add("change_date", clFS.date_changed.ToString.Trim)
            dict.Add("remark", clFS.remark.ToString.Trim)

            dict.Add("language", clFS.language.ToString.Trim)

            dict.Add("status_text_1", clFS.status_text_1.ToString.Trim)
            dict.Add("status_text_2", clFS.status_text_2.ToString.Trim)
            dict.Add("status_text_3", clFS.status_text_3.ToString.Trim)
            dict.Add("status_text_4", clFS.status_text_4.ToString.Trim)
            dict.Add("status_text_5", clFS.status_text_5.ToString.Trim)
            dict.Add("status_text_6", clFS.status_text_6.ToString.Trim)
            dict.Add("status_text_21", clFS.status_text_21.ToString.Trim)
            dict.Add("status_text_51", clFS.status_text_51.ToString.Trim)
            dict.Add("status_text_52", clFS.status_text_52.ToString.Trim)
            dict.Add("status_text_53", clFS.status_text_53.ToString.Trim)
            dict.Add("status_text_contact", clFS.status_text_contact.ToString.Trim)


            docRef.SetAsync(dict)



            db = Nothing

            Return strKey
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return ""
        End Try
    End Function


    Private Function GetStatusText(strLanguage As String, intStatus As OrderStatus, dtmStart As Date, dtmEnd As Date, Optional intMontStatus As MontageStatus = MontageStatus.Rezervirana_Skrita) As String
        Dim strText As String = ""
        strLanguage = strLanguage.Trim
        Select Case intStatus
            Case OrderStatus.Narocilo_potrjeno
                'vedno sta oba datuma enaka
                strText = "status_text_11"
            Case OrderStatus.Priprava_na_tehnicno_obdelavo
                If Not IsValidDate(dtmStart) And Not IsValidDate(dtmEnd) Then
                    'Ni nobenega datuma
                    strText = "status_text_2"
                ElseIf IsValidDate(dtmStart) And Not IsValidDate(dtmEnd) Then
                    strText = "status_text_21"

                ElseIf IsValidDate(dtmStart) And IsValidDate(dtmEnd) Then
                    strText = "status_text_22"
                End If
            Case OrderStatus.Izdelava_delovnega_naloga
                If Not IsValidDate(dtmStart) And Not IsValidDate(dtmEnd) Then
                    'Ni nobenega datuma
                    strText = "status_text_3"
                ElseIf IsValidDate(dtmStart) And Not IsValidDate(dtmEnd) Then
                    strText = "status_text_31"
                ElseIf IsValidDate(dtmEnd) Then
                    'oba datuma OK
                    strText = "status_text_32"
                End If
            Case OrderStatus.Narocanje_in_prejem_materiala
                If Not IsValidDate(dtmStart) And Not IsValidDate(dtmEnd) Then
                    'Ni nobenega datuma
                    strText = "status_text_4"
                ElseIf IsValidDate(dtmStart) And Not IsValidDate(dtmEnd) Then
                    'samo začetni datum OK
                    strText = "status_text_41"
                ElseIf IsValidDate(dtmEnd) Then
                    'zadnji datuma OK
                    strText = "status_text_42"
                End If
            Case OrderStatus.Sprememba_narocila
                If IsValidDate(dtmEnd) Then
                    strText = "spremembe2"
                Else
                    strText = "spremembe1"
                End If
            Case OrderStatus.Proizvodnja

                strText = "status_text_5"
            Case OrderStatus.Proizvodnja_zaceta
                If IsValidDate(dtmStart) Then
                    strText = "status_text_511"
                Else
                    strText = "status_text_51"
                End If
            Case OrderStatus.Proizvodnja_zaceta_stiskalnica
                If IsValidDate(dtmStart) Then
                    strText = "status_text_521"
                Else
                    strText = "status_text_52"
                End If
            Case OrderStatus.Proizvodnja_koncana
                If IsValidDate(dtmEnd) Then
                    strText = "status_text_531"
                Else
                    strText = "status_text_53"
                End If
            Case OrderStatus.Montaza

                Select Case intMontStatus
                    Case MontageStatus.Rezervirana_Skrita
                        strText = "status_text_6"
                    Case MontageStatus.Planirana
                        strText = "status_text_61"
                    Case MontageStatus.Planirana_dogovorjena, MontageStatus.Koncana
                        strText = "status_text_62"
                    Case Else
                        strText = "status_text_6"
                End Select


        End Select

        Return strText
    End Function

    Public Function GetGoogleId(strOrderNr As String) As String
        Dim strSQL As String = ""

        Dim strKey As String = ""

        Using connTools As SqlConnection = GetConnection("TOOLS")

            strSQL = "SELECT * FROM msora_order_info WHERE order_nr = @strOrderNrN"
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@strOrderNrN", strOrderNr)
                Dim dt As DataTable = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    strKey = dt.Rows(0)("google_id").ToString.Trim
                Else
                    strKey = ""
                End If
            End Using
        End Using

        Return strKey

    End Function


    Public Sub ExportTexts()
        'najprej nafilam vse jezike v en array
        Dim strLanguage As String
        Dim languages(5) As String

        Dim path As String = AppDomain.CurrentDomain.BaseDirectory + cls.Config.FirebaseAuthSecret
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path)
        Dim db As FirestoreDb = FirestoreDb.Create(cls.Config.FirebaseBasePath)

        languages(0) = "sl"
        languages(1) = "en"
        languages(2) = "de"
        languages(3) = "fr"
        languages(4) = "it"
        languages(5) = "hr"

        Using connTools As SqlConnection = GetConnection("TOOLS")
            For i = 0 To languages.Length - 1
                strLanguage = languages(i).ToString
                'dodam dokument za jezik

                Dim strKey As String = strLanguage

                Dim docRef As DocumentReference = db.Collection("texts").Document(strLanguage)

                Dim dict As New Dictionary(Of String, Object)

                strKey = strKey.Trim

                Dim dt As DataTable = GetTexts(strKey, connTools)

                If dt.Rows.Count = 1 Then
                    dict.Add("datum", dt.Rows(0)("datum").ToString)
                    dict.Add("kontakt_text", dt.Rows(0)("kontakt_text").ToString)
                    dict.Add("kontakt_text1", dt.Rows(0)("kontakt_text1").ToString)
                    dict.Add("pomembno", dt.Rows(0)("pomembno").ToString)
                    dict.Add("spremembe1", dt.Rows(0)("spremembe1").ToString)
                    dict.Add("spremembe2", dt.Rows(0)("spremembe2").ToString)
                    dict.Add("stanje", dt.Rows(0)("stanje").ToString)
                    dict.Add("status", dt.Rows(0)("status").ToString)
                    dict.Add("status_text_1", dt.Rows(0)("status_text_1").ToString)
                    dict.Add("status_text_11", dt.Rows(0)("status_text_11").ToString)
                    dict.Add("status_text_2", dt.Rows(0)("status_text_2").ToString)
                    dict.Add("status_text_21", dt.Rows(0)("status_text_21").ToString)
                    dict.Add("status_text_22", dt.Rows(0)("status_text_22").ToString)
                    dict.Add("status_text_3", dt.Rows(0)("status_text_3").ToString)
                    dict.Add("status_text_31", dt.Rows(0)("status_text_31").ToString)
                    dict.Add("status_text_32", dt.Rows(0)("status_text_32").ToString)
                    dict.Add("status_text_4", dt.Rows(0)("status_text_4").ToString)
                    dict.Add("status_text_41", dt.Rows(0)("status_text_41").ToString)
                    dict.Add("status_text_42", dt.Rows(0)("status_text_42").ToString)
                    dict.Add("status_text_5", dt.Rows(0)("status_text_5").ToString)
                    dict.Add("status_text_51", dt.Rows(0)("status_text_51").ToString)
                    dict.Add("status_text_511", dt.Rows(0)("status_text_511").ToString)
                    dict.Add("status_text_52", dt.Rows(0)("status_text_52").ToString)
                    dict.Add("status_text_521", dt.Rows(0)("status_text_521").ToString)
                    dict.Add("status_text_53", dt.Rows(0)("status_text_53").ToString)
                    dict.Add("status_text_531", dt.Rows(0)("status_text_531").ToString)
                    dict.Add("status_text_6", dt.Rows(0)("status_text_6").ToString)
                    dict.Add("status_text_61", dt.Rows(0)("status_text_61").ToString)
                    dict.Add("status_text_62", dt.Rows(0)("status_text_62").ToString)
                    dict.Add("zahvala", dt.Rows(0)("zahvala").ToString)

                    docRef.SetAsync(dict)
                End If

            Next
        End Using

        db = Nothing
    End Sub

    Public Sub ImportTexts()
        'najprej nafilam vse jezike v en array

        Dim path As String = AppDomain.CurrentDomain.BaseDirectory + cls.Config.FirebaseAuthSecret
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path)
        Dim db As FirestoreDb = FirestoreDb.Create(cls.Config.FirebaseBasePath)

        Using connTools As SqlConnection = GetConnection("TOOLS")


            Dim q As Query = db.Collection("texts")
            Dim qSnap As Google.Cloud.Firestore.QuerySnapshot = q.GetSnapshotAsync.Result

            If Not qSnap Is Nothing Then
                Dim docSnap As Google.Cloud.Firestore.DocumentSnapshot
                For j = 0 To qSnap.Count - 1
                    Dim strKey As String = qSnap(j).Id

                    docSnap = qSnap(j)

                    Dim dict As New Dictionary(Of String, Object)

                    dict = docSnap.ToDictionary

                    Dim strSQL As String = "UPDATE msora_order_text SET datum = @datum, kontakt_text = @kontakt_text, kontakt_text1 = @kontakt_text1, pomembno = @pomembno, " _
                        & " spremembe1 = @spremembe1, spremembe2 = @spremembe2, stanje = @stanje, status = @status, status_text_1 = @status_text_1, " _
                        & " status_text_11 = @status_text_11, status_text_2 = @status_text_2, status_text_21 = @status_text_21, status_text_22 = @status_text_22, status_text_3 = @status_text_3, " _
                        & " status_text_31 = @status_text_31, status_text_32 = @status_text_32, status_text_4 = @status_text_4, status_text_41 = @status_text_41, " _
                        & " status_text_42 = @status_text_42, status_text_5 = @status_text_5, status_text_51 = @status_text_51, status_text_511 = @status_text_511, status_text_52 = @status_text_52, " _
                        & " status_text_521 = @status_text_521, status_text_53 = @status_text_531, status_text_6 = @status_text_6, status_text_61 = @status_text_61, status_text_62 = @status_text_62, " _
                        & " zahvala = @zahvala WHERE language = @strKey "

                    Using cmd As New SqlCommand(strSQL, connTools)
                        cmd.Parameters.AddWithValue("@datum", dict("datum").ToString)
                        cmd.Parameters.AddWithValue("@kontakt_text", dict("kontakt_text").ToString)
                        cmd.Parameters.AddWithValue("@kontakt_text1", dict("kontakt_text1").ToString)
                        cmd.Parameters.AddWithValue("@pomembno", dict("pomembno").ToString)
                        cmd.Parameters.AddWithValue("@spremembe1", dict("spremembe1").ToString)
                        cmd.Parameters.AddWithValue("@spremembe2", dict("spremembe2").ToString)
                        cmd.Parameters.AddWithValue("@stanje", dict("stanje").ToString)
                        cmd.Parameters.AddWithValue("@status", dict("status").ToString)
                        cmd.Parameters.AddWithValue("@status_text_1", dict("status_text_1").ToString)
                        cmd.Parameters.AddWithValue("@status_text_11", dict("status_text_11").ToString)
                        cmd.Parameters.AddWithValue("@status_text_2", dict("status_text_2").ToString)
                        cmd.Parameters.AddWithValue("@status_text_21", dict("status_text_21").ToString)
                        cmd.Parameters.AddWithValue("@status_text_22", dict("status_text_22").ToString)
                        cmd.Parameters.AddWithValue("@status_text_3", dict("status_text_3").ToString)
                        cmd.Parameters.AddWithValue("@status_text_31", dict("status_text_31").ToString)
                        cmd.Parameters.AddWithValue("@status_text_32", dict("status_text_32").ToString)
                        cmd.Parameters.AddWithValue("@status_text_4", dict("status_text_4").ToString)
                        cmd.Parameters.AddWithValue("@status_text_41", dict("status_text_41").ToString)
                        cmd.Parameters.AddWithValue("@status_text_42", dict("status_text_42").ToString)
                        cmd.Parameters.AddWithValue("@status_text_5", dict("status_text_5").ToString)
                        cmd.Parameters.AddWithValue("@status_text_51", dict("status_text_51").ToString)
                        cmd.Parameters.AddWithValue("@status_text_511", dict("status_text_511").ToString)
                        cmd.Parameters.AddWithValue("@status_text_52", dict("status_text_52").ToString)
                        cmd.Parameters.AddWithValue("@status_text_521", dict("status_text_521").ToString)
                        cmd.Parameters.AddWithValue("@status_text_53", dict("status_text_53").ToString)
                        cmd.Parameters.AddWithValue("@status_text_531", dict("status_text_531").ToString)
                        cmd.Parameters.AddWithValue("@status_text_6", dict("status_text_6").ToString)
                        cmd.Parameters.AddWithValue("@status_text_61", dict("status_text_61").ToString)
                        cmd.Parameters.AddWithValue("@status_text_62", dict("status_text_62").ToString)
                        cmd.Parameters.AddWithValue("@zahvala", dict("zahvala").ToString)
                        cmd.Parameters.AddWithValue("@strKey", strKey)
                        cmd.ExecuteNonQuery()
                    End Using




                Next
            End If


        End Using

        db = Nothing
    End Sub

    Private Function GetTexts(strLanguage As String, connTools As SqlConnection) As DataTable
        Dim strSQL As String = ""

        strSQL = "SELECT * FROM msora_order_text WHERE language = @strLanguage"
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strLanguage", strLanguage)
            Dim dt As DataTable = GetData(cmd)
            Return dt
        End Using
    End Function


    Public Function UpdateMSoraOrderInfoAll(dtmDatum As Date) As Boolean
        Dim strSQL As String = ""
        Dim clFS As New clsOrderFB
        Dim strKey As String = ""
        Using connTools As SqlConnection = GetConnection("TOOLS")

            strSQL = "SELECT * FROM msora_order_info WHERE status_date_1 >= @dtmDatum"
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@dtmDatum", dtmDatum)
                Dim dt As DataTable = GetData(cmd)
                For i = 0 To dt.Rows.Count - 1

                    strKey = dt.Rows(i)("google_id").ToString.Trim

                    strSQL = "SELECT * FROM msora_order_info WHERE google_id = @strKey"

                    Using cmd2 As New SqlCommand(strSQL, connTools)
                        cmd2.Parameters.AddWithValue("@strKey", strKey)
                        Dim dt2 As DataTable = GetData(cmd2)
                        If dt2.Rows.Count > 0 Then
                            Call FillFS(dt2, clFS)
                            If Not GoogleIdExist(clFS.order_nr.ToString.Trim) Then
                                Call InsertGoogleFirestoreDocument(clFS, strKey)
                                Call AddToActionStatusNarocila(frmMainForm.txtLog, "INSERT... " & strKey & vbTab & dt2.Rows(0)("order_nr").ToString)
                            Else
                                Call InsertUpdateGoogleFirestoreDocument(clFS, strKey)
                                Call AddToActionStatusNarocila(frmMainForm.txtLog, "UPDATE... " & strKey & vbTab & dt2.Rows(0)("order_nr").ToString)
                            End If
                        End If
                    End Using

                Next
            End Using
        End Using
    End Function

    Public Function GoogleIdExist(strOrderNr As String) As Boolean
        Dim path As String = AppDomain.CurrentDomain.BaseDirectory + cls.Config.FirebaseAuthSecret
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path)
        Dim db As FirestoreDb = FirestoreDb.Create(cls.Config.FirebaseBasePath)


        Dim docCol As CollectionReference

        docCol = db.Collection("orders")

        Dim q As Query = docCol.WhereEqualTo("order_nr", strOrderNr)

        'Dim ds As Google.Cloud.Firestore.DocumentSnapshot = q.GetSnapshotAsync

        Dim ds As Google.Cloud.Firestore.QuerySnapshot = q.GetSnapshotAsync.Result

        If Not ds Is Nothing Then
            Dim dt As Google.Cloud.Firestore.DocumentSnapshot
            For i = 0 To ds.Count - 1
                dt = ds(i)
                If dt.ContainsField("order_nr").ToString Then

                    Dim dict As New Dictionary(Of String, Object)

                    dict = dt.ToDictionary

                    If dict("order_nr").ToString <> "" Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If


            Next
        Else
            Return False
        End If

    End Function
End Module
