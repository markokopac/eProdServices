Imports eProdService.cls.msora.DB_MSora
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports System.IO

Module modTools



    Public Function SetMeldeStatus(strOrderNr As String, dtmDatum As Date, connKBS As SqlConnection) As Boolean
        Dim strSQL As String = ""

        strSQL = "SELECT bsx_imeldestatus, bsx_idmeldender, bsx_meldedatum, bsx_iderfasser FROM VKBELEGSTUB WHERE bsx_belegnr = @strOrderNr"

        Using cmd As New SqlCommand(strSQL, connKBS)
            cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
            Dim dt As DataTable = GetData(cmd)

            If dt.Rows.Count > 0 Then
                Dim intMeldeStatus As Integer = DB2IntZero(dt.Rows(0)("bsx_imeldestatus"))
                Dim dtmMeldeDatum As Date
                Dim lngIdMeldender As Long

                If intMeldeStatus <> 21 Then

                    If IsDBNull(dt.Rows(0)("bsx_meldedatum")) Then
                        dtmMeldeDatum = dtmDatum
                    Else
                        dtmMeldeDatum = CDate(dt.Rows(0)("bsx_meldedatum"))
                    End If

                    lngIdMeldender = DB2Lng(dt.Rows(0)("bsx_iderfasser"))

                    strSQL = "UPDATE vkbelegstub SET bsx_imeldestatus = @intMeldeStatus, bsx_meldedatum = @dtmMeldeDatum, bsx_idmeldender = @lngIdMeldender " _
                        & " WHERE bsx_belegnr = @strOrderNrN"
                    Using cmdU As New SqlCommand(strSQL, connKBS)
                        cmdU.Parameters.AddWithValue("@intMeldeStatus", 21)
                        cmdU.Parameters.AddWithValue("@dtmMeldeDatum", dtmMeldeDatum)
                        cmdU.Parameters.AddWithValue("@lngIdMeldender", lngIdMeldender)
                        cmdU.Parameters.AddWithValue("@strOrderNrN", strOrderNr)
                        cmdU.ExecuteNonQuery()

                        Call AddToActionStatusNarocila(frmMainForm.txtLog, "*** Avtomatska prijava naročila " & strOrderNr & vbTab & dtmMeldeDatum)
                    End Using

                End If
            End If
        End Using


    End Function

    Public Function SetMeldeStatus(ByVal strDocnr As String, ByVal strNotice As String, ByVal lngMeldener As Long, dtmMeldeDate As DateTime, connTools As SqlConnection, connKBS As SqlConnection) As Boolean
        Dim intMeldeStatus As Integer = 21
        Dim dtmMeldeDatum As Date = Date.Today
        Dim strSQL As String = ""
        Dim dt As DataTable = Nothing
        Dim strDescription As String = ""

        Try

            strSQL = "SELECT bsx_imeldestatus, bsx_idmeldender, bsx_meldedatum FROM VKBELEGSTUB WHERE bsx_belegnr = @strDocNr"

            Using cmd As New SqlCommand(strSQL, connKBS)
                cmd.Parameters.AddWithValue("@strDocNr", strDocnr)
                dt = GetData(cmd)
            End Using

            If dt.Rows.Count > 0 Then

                'preverim, ce je nalog ze zaklenjen
                strSQL = "SELECT * FROM unlock_doc " _
                & " WHERE document_nr = @strDocnr AND unlock_type = 2"
                Using cmd1 As New SqlCommand(strSQL, connTools)
                    cmd1.Parameters.AddWithValue("@strDocNr", strDocnr)
                    Dim dt1 As DataTable = GetData(cmd1)
                    If dt1.Rows.Count = 0 Then
                        strSQL = "INSERT INTO UNLOCK_DOC (datum, document_nr, melde_datum_org, melde_status_org, melde_usr_id_org, usr_id, description, usr_request, database_id, unlock_type) " _
                        & " VALUES (@dtmDatum, @document_nr, @melde_datum_org, @melde_status_org, @melde_user_id_org, @usr_id, @description, @usr_request, @database_id, 2)"

                        'Using connTools As SqlConnection = GetConnection("KlaesTools")
                        Using cmd As New SqlCommand(strSQL, connTools)
                            cmd.Parameters.AddWithValue("@dtmDatum", Date.Now)
                            cmd.Parameters.AddWithValue("@document_nr", strDocnr)
                            cmd.Parameters.AddWithValue("@melde_datum_org", dtmMeldeDatum)
                            cmd.Parameters.AddWithValue("@melde_status_org", intMeldeStatus)
                            cmd.Parameters.AddWithValue("@melde_user_id_org", lngMeldener)
                            cmd.Parameters.AddWithValue("@usr_id", "SERVICE")
                            cmd.Parameters.AddWithValue("@description", strNotice)
                            cmd.Parameters.AddWithValue("@usr_request", lngMeldener)
                            cmd.Parameters.AddWithValue("@database_id", 0)

                            cmd.ExecuteNonQuery()
                        End Using
                        'End Using

                        'nastavimo status še v vkbelegstub


                    Else
                        ' kakšen update????
                        strSQL = "UPDATE unlock_doc SET return_melde_datum = NULL WHERE document_nr = @strDocNr AND unlock_type = 2"
                        Using cmd As New SqlCommand(strSQL, connTools)
                            cmd.Parameters.AddWithValue("@strDocNr", strDocnr)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    strSQL = "UPDATE vkbelegstub SET bsx_meldedatum = @dtmMeldeDate, bsx_idmeldender = @lngMeldener, bsx_imeldestatus = 21 WHERE bsx_belegnr = @strDocNr"
                    Using cmd As New SqlCommand(strSQL, connKBS)
                        cmd.Parameters.AddWithValue("@dtmMeldeDate", dtmMeldeDate)
                        cmd.Parameters.AddWithValue("@lngMeldener", lngMeldener)
                        cmd.Parameters.AddWithValue("@strDocNr", strDocnr)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using




                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try

        Return True
    End Function

    Public Function ReturnMeldeStatus(ByVal lngId As Long, connTools As SqlConnection, connKBS As SqlConnection) As Boolean

        Dim lngKlaesUserId As Long = -1
        Dim strSQL As String = ""
        Dim dt As DataTable
        Dim strOrder As String = ""

        Try

            strSQL = "SELECT TOP 1 melde_datum_org, melde_usr_id_org, document_nr, unlock_id, database_id FROM unlock_doc " _
                & " WHERE unlock_id = @strDocnr AND melde_status_org = @printstatus " _
                & " AND return_melde_datum IS NOT NULL " _
                & " ORDER BY datum DESC"

            'Using conn As SqlConnection = GetConnection("KlaesTools")
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@strDocnr", lngId)
                cmd.Parameters.AddWithValue("@printstatus", 21)
                dt = GetData(cmd)
            End Using

            If dt.Rows.Count > 0 Then


                strSQL = "UPDATE vkbelegstub SET bsx_meldedatum = @dtmDate, bsx_idmeldender = @lngKlaesUserId, bsx_imeldestatus = @status " _
                    & "WHERE bsx_belegnr = @strDocNr"

                Using cmd1 As New SqlCommand(strSQL, connKBS)
                    cmd1.Parameters.AddWithValue("dtmDate", dt(0)("melde_datum_org"))
                    cmd1.Parameters.AddWithValue("lngKlaesUserId", dt(0)("melde_usr_id_org"))
                    cmd1.Parameters.AddWithValue("status", 21)
                    cmd1.Parameters.AddWithValue("strDocNr", dt(0)("document_nr"))
                    ExecuteNonQuery(cmd1)
                End Using


                strSQL = "UPDATE UNLOCK_DOC SET return_melde_datum = @PrintDatum WHERE unlock_id = @unlockid"

                'Using connTools As SqlConnection = GetConnection("KlaesTools")
                Using cmd2 As New SqlCommand(strSQL, connTools)
                    cmd2.Parameters.AddWithValue("@PrintDatum", DBNull.Value)
                    cmd2.Parameters.AddWithValue("@unlockid", dt(0)("unlock_id"))

                    ExecuteNonQuery(cmd2)
                End Using
                'End Using

                Return True
            Else
                Return False
            End If
            'End Using
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try
    End Function


    Public Function GetKlaesUser(ByVal lngId As Long, ByVal conn As SqlConnection, Optional ByVal blnShortName As Boolean = False) As String
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Try

            strSQL = "SELECT usx_id, usx_fullname, usx_user FROM klaesuser WHERE usx_id = @lngId"

            Using cmd As New SqlCommand(strSQL, conn)
                cmd.Parameters.AddWithValue("@lngId", lngId)
                dt = GetData(cmd)
            End Using

            If dt.Rows.Count > 0 Then
                If blnShortName Then
                    Return dt(0)("usx_user").ToString
                Else
                    Return dt(0)("usx_fullname").ToString
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Return Nothing
    End Function

    Public Function GetUnlockDoc(connTools As SqlConnection) As DataTable


        Dim strSQL As String = ""
        Dim dt As DataTable
       
        Try

            strSQL = "SELECT melde_datum_org as tehnicnoobdelano, document_nr as auftragsnummer FROM unlock_doc " _
                & " WHERE unlock_type = 2 " _
                & " AND return_melde_datum IS NOT NULL " _
                & " ORDER BY return_melde_datum ASC"

            'Using conn As SqlConnection = GetConnection("KlaesTools")
            Using cmd As New SqlCommand(strSQL, connTools)
                dt = GetData(cmd)
            End Using

            Return dt
            'End Using
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function



    Public Function GetMailEvens(connTools As SqlConnection) As DataTable
        Dim dt As DataTable = Nothing
        Dim strSQL As String

        Try



            strSQL = "SELECT event_id FROM eprod_events WHERE event_enabled = 1 ORDER BY event_id"

            Using cmd As New SqlCommand(strSQL, connTools)
                dt = GetData(cmd)
                Return dt
            End Using


        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)

            Return Nothing
        End Try

    End Function


    Public Function IsMailingDissabled(ByVal strOrderNr As String, connTools As SqlConnection, connKBS As SqlConnection) As Boolean
        Dim strSQL As String = ""
        Dim strNewOrderNr As String = ""
        Dim lngPartnerNr As Long = -1
        Dim strProjectNr As String = ""



        'dokument N
        If IsNumeric(Mid(strOrderNr, 2, 2)) Then
            If Mid(strOrderNr, 1, 1) = "T" Then
                strNewOrderNr = strOrderNr.Replace("T", "N")
            End If
            If Mid(strOrderNr, 1, 1) = "C" Then
                strNewOrderNr = strOrderNr.Replace("C", "N")
            End If
            If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
        Else
            strNewOrderNr = strOrderNr
        End If

        strSQL = "SELECT * FROM service WHERE order_nr = @strOrderNr "
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
            Dim dt As DataTable = GetData(cmd)
            If dt.Rows.Count > 0 Then
                If DB2IntZero(dt(0)("mailing_type")) = -1 Then
                    Return True
                End If
            End If
        End Using

        'dokument T
        If IsNumeric(Mid(strOrderNr, 2, 2)) Then
            If Mid(strOrderNr, 1, 1) = "N" Then
                strNewOrderNr = strOrderNr.Replace("N", "T")
            End If
            If Mid(strOrderNr, 1, 1) = "C" Then
                strNewOrderNr = strOrderNr.Replace("C", "T")
            End If
            If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
        Else
            strNewOrderNr = strOrderNr
        End If

        strSQL = "SELECT * FROM service WHERE order_nr = @strOrderNr "
        Using cmd1 As New SqlCommand(strSQL, connTools)
            cmd1.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
            Dim dt1 As DataTable = GetData(cmd1)
            If dt1.Rows.Count > 0 Then
                If DB2IntZero(dt1(0)("mailing_type")) = -1 Then
                    Return True
                End If

            End If
        End Using


        'preverim še project

        Dim dtOrder As DataTable = GetOrderData(strOrderNr, connKBS)

        If dtOrder.Rows.Count > 0 Then
            strProjectNr = dtOrder(0)("bsx_vorgangnr").ToString
        End If

        strSQL = "SELECT * FROM project_lock_mailing " _
            & "WHERE project_nr = @strProjectNr"

        Using cmd1 As New SqlCommand(strSQL, connTools)
            cmd1.Parameters.AddWithValue("@strProjectNr", strProjectNr)
            Dim dt1 As DataTable = GetData(cmd1)
            If dt1.Rows.Count > 0 Then
                If DB2IntZero(dt1(0)("status")) = 1 Then
                    Return True
                End If
            End If

        End Using


        'preverim še kartoteko

        If dtOrder.Rows.Count > 0 Then
            lngPartnerNr = DB2Lng(dtOrder(0)("gpx_id"))
        End If

        strSQL = "SELECT * FROM partner_lock_mailing " _
            & "WHERE partner_id = @lngPartnerNr"

        Using cmd1 As New SqlCommand(strSQL, connTools)
            cmd1.Parameters.AddWithValue("@lngPartnerNr", lngPartnerNr)
            Dim dt1 As DataTable = GetData(cmd1)
            If dt1.Rows.Count > 0 Then
                If DB2IntZero(dt1(0)("status")) = 1 Then
                    Return True
                End If
            End If

        End Using



        Return False
    End Function

    Public Sub ProcessEvent(ByVal cEvent As cls.event.EprodEvent, Optional ByVal rtf As RichTextBox = Nothing)
        Dim dtmStart As Date
        Dim dt As DataTable = Nothing
        Dim strOrderNr As String
        Dim strKommisionNr As String
        Dim strLog As String = ""


        Dim strSMTPServer As String = cls.Config.GetMailSMTP
        Dim strSMTPPort As String = cls.Config.GetMailSMTPPort
        Dim strEmail As String = cls.Config.GetMailAddress
        Dim strUsername As String = cls.Config.GetMailUsername
        Dim strPassword As String = cls.Config.GetMailPassword
        Dim dtmTo As Date = DateAdd(DateInterval.Day, 1, Now.Date)

        dtmStart = DateAdd(DateInterval.Day, cEvent.EventDays * -1, Now.Date)

        Debug.Print(cEvent.EventId)

        Using connTools As SqlConnection = GetConnection("KLAESTOOLS")
            Using connKBS As SqlConnection = GetConnection("KLAES")
                Using connMAWI As SqlConnection = GetConnection("MAWI")
                    Using connKAPA As SqlConnection = GetConnection("KAPA")
                        Select Case cEvent.EventType
                            Case clsGlobal.EventType.event_not_defined
                                'ne naredim nič
                            Case clsGlobal.EventType.event_kommission_production_start
                                'dobim naloge, ki so bila začeta na delovni postaji
                                dt = GetStartedKommissions(dtmStart, dtmTo, cEvent.EventWorkstation, "", "")

                            Case clsGlobal.EventType.event_glass_not_ordered
                                'dobim naloge, ki so bila začeta na delovni postaji
                                dt = GetBookedOrders(dtmStart, dtmTo, cEvent.EventWorkstation, cEvent.EventParameter)

                                'pobrisem naloge, ki imajo naročeno steklo
                                For i = dt.Rows.Count - 1 To 0 Step -1
                                    Debug.Print(dt(i)("auftragsnummer").ToString)
                                    If IsGlassOrdered(dt(i)("auftragsnummer").ToString, connMAWI) Then
                                        dt.Rows.RemoveAt(i)
                                    End If
                                Next


                            Case clsGlobal.EventType.event_kommission_production_end
                                'dobim naloge, ki so bila končana na delovni postaji
                                dt = GetFinishedKommissions(dtmStart, dtmTo, cEvent.EventWorkstation, "", "")

                            Case clsGlobal.EventType.event_order_production_start
                                'dobim naročila, ki so bila začeta na delovni postaji
                                dt = GetStartedOrders(dtmStart, dtmTo, cEvent.EventWorkstation, cEvent.EventParameter)

                            Case clsGlobal.EventType.event_order_production_end
                                'dobim naročila, ki so bila končana na delovni postaji
                                dt = GetFinishedOrders(dtmStart, dtmTo, cEvent.EventWorkstation, cEvent.EventParameter)

                            Case clsGlobal.EventType.event_order_production_end_check_delivery_OK
                                'dobim naročila, ki so bila končana na delovni postaji
                                dt = GetFinishedOrdersDeliveryOK(dtmStart, dtmTo, cEvent.EventWorkstation, cEvent.EventParameter, connKBS, connTools, connMAWI)

                            Case clsGlobal.EventType.event_order_production_end_check_delivery_FAIL
                                'dobim naročila, ki so bila končana na delovni postaji
                                dt = GetFinishedOrdersDeliveryFAIL(dtmStart, dtmTo, cEvent.EventWorkstation, cEvent.EventParameter, connTools, connKBS, connMAWI)

                            Case clsGlobal.EventType.event_order_planning_start
                                'dobim naročila, ki so bila dodana v KAPA
                                dt = GetOrdersKAPAStart(dtmStart, Now.Date, connKAPA)

                            Case clsGlobal.EventType.event_order_technical_end
                                'dobim naročila, ki so bila tehnično obdelana
                                dt = GetTechicalFinnishedOrders(dtmStart, dtmTo, connKBS)

                            Case clsGlobal.EventType.event_order_already_delivered
                                'dobim naročila, ki so bila končana na delovni postaji in že dobavljena
                                dt = GetFinishedOrdersAlreadyDelivered(dtmStart, dtmTo, cEvent.EventWorkstation, cEvent.EventParameter, connTools, connKBS)

                            Case clsGlobal.EventType.event_kommission_send_to_cutter
                                dt = GetStartedKommissions(dtmStart, dtmTo, cEvent.EventWorkstation, "", "")

                            Case clsGlobal.EventType.event_kommission_cutter_move_to_archive
                                dt = GetFinnishedCutterFiles(dtmStart, dtmTo)

                            Case clsGlobal.EventType.event_delivery_note_created
                                dt = GetDeliveryNoteCreated(dtmStart, dtmTo, connKBS)

                            Case clsGlobal.EventType.event_invoice_created
                                dt = GetInvoiceCreated(dtmStart, dtmTo, cEvent.EventParameter.Trim, connKBS)

                            Case clsGlobal.EventType.event_invoice_printed
                                dtmTo = DateAdd(DateInterval.Day, -1, Now.Date)
                                dtmStart = DateAdd(DateInterval.Day, cEvent.EventDays * -1, dtmTo)
                                dt = GetInvoicePrinted(dtmStart, dtmTo, cEvent.EventParameter.Trim, connKBS)

                            Case clsGlobal.EventType.event_avans_created
                                dt = GetAvansCreated(dtmStart, dtmTo, connKBS)

                            Case clsGlobal.EventType.event_order_not_planed
                                dtmStart = Now.Date
                                dtmTo = DateAdd("d", cEvent.EventDays, dtmStart)
                                dt = GetKAPAReservedOrders(dtmStart, dtmTo, connKAPA)

                            Case clsGlobal.EventType.event_order_kapa_glass
                                'dodano v KAPA in znesek večji od...
                                dt = GetOrdersKAPAGlass(dtmStart, dtmTo, connKBS, connKAPA)

                            Case clsGlobal.EventType.event_montaza_slepci
                                dt = GetOrdersBlindsMontageFinished(dtmStart, dtmTo, connTools)

                            Case clsGlobal.EventType.event_offer_not_realized
                                Dim intDays As Integer = 60
                                If cEvent.EventParameter <> "" Then
                                    If IsNumeric(cEvent.EventParameter) Then
                                        intDays = CInt(cEvent.EventParameter)
                                    Else
                                        intDays = cls.Config.NotRealizedOffersDayDelay
                                    End If
                                Else
                                    intDays = cls.Config.NotRealizedOffersDayDelay
                                End If
                                dtmStart = DateAdd(DateInterval.Day, (intDays + cEvent.EventDays) * -1, Now.Date)
                                dtmTo = DateAdd(DateInterval.Day, (intDays) * -1, Now.Date)

                                dt = GetNotRealizedOffers(dtmStart, dtmTo, connKBS)

                            Case clsGlobal.EventType.event_task_assigned
                                dtmStart = DateAdd(DateInterval.Day, cEvent.EventDays * -1, dtmTo)
                                dt = GetNewUserTasks(dtmStart, dtmTo, cEvent.EventPartnerId.ToString, connKBS)

                            Case clsGlobal.EventType.event_MAWI_subsequent_delivery
                                'contract_nr
                                'confirmation_nr
                                'confirmation_date
                                'articles
                                dtmStart = DateAdd(DateInterval.Day, cEvent.EventDays * -1, Now.Date)
                                dt = GetConfirmedOrdersNotDelivered(dtmStart, Now.Date, True, connTools, connMAWI)
                            Case clsGlobal.EventType.event_google_order_created
                                dt = GetGoogleOrders(dtmStart, Now.Date, connTools)
                        End Select



                        If Not IsNothing(dt) Then

                            'Exit Sub


                            For i = 0 To dt.Rows.Count - 1
                                strLog = ""
                                Select Case cEvent.EventType
                                    Case clsGlobal.EventType.event_kommission_production_start, clsGlobal.EventType.event_kommission_production_end
                                        strOrderNr = dt(i)("auftragsnummer").ToString
                                        strKommisionNr = dt(i)("kommissionsnummer").ToString
                                        'If Not IsMailingDissabled(strOrderNr) Then
                                        strLog = ExecuteEventOrderMail(cEvent, strKommisionNr, strOrderNr, strSMTPServer, strSMTPPort, strEmail, strUsername, strPassword, False, connTools, connKBS, connMAWI)
                                        'Else
                                        'strLog = "Mailing za naročilo " & strOrderNr & " je onemogočeno!"
                                        'End If
                                        If strLog <> "" Then
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, strLog)
                                            Else
                                                AddToActionLogSendMail(strLog)
                                            End If
                                        Else
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, "Order Nr.: = " & strOrderNr & " Kommission = " & strKommisionNr)
                                            Else
                                                AddToActionLogSendMail("Order Nr.: = " & strOrderNr & " Kommission = " & strKommisionNr)
                                            End If
                                        End If
                                    Case clsGlobal.EventType.event_kommission_send_to_cutter
                                        strOrderNr = dt(i)("auftragsnummer").ToString
                                        strKommisionNr = dt(i)("kommissionsnummer").ToString
                                        strLog = ExecuteEventOrderNoMail(cEvent, strKommisionNr, strOrderNr, connTools, connKBS)
                                        If strLog <> "" Then
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, strLog)
                                            Else
                                                AddToActionLogSendMail(strLog)
                                            End If
                                        Else
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, "Order Nr.: = " & strOrderNr & " Kommission = " & strKommisionNr)
                                            Else
                                                AddToActionLogSendMail("Order Nr.: = " & strOrderNr & " Kommission = " & strKommisionNr)
                                            End If
                                        End If
                                    Case clsGlobal.EventType.event_kommission_cutter_move_to_archive
                                        'samo za prvi nalog
                                        If i = 0 Then
                                            strKommisionNr = ""
                                            strLog = ExecuteEventOrderNoMail(cEvent, dt)
                                        End If

                                        If strLog <> "" Then
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, strLog)
                                            Else
                                                AddToActionLogSendMail(strLog)
                                            End If
                                        End If

                                    Case clsGlobal.EventType.event_invoice_created, clsGlobal.EventType.event_invoice_printed
                                        Dim strOrderMain As String = ""
                                        Dim strOrder As String = ""

                                        strOrderNr = dt(i)("auftragsnummer").ToString
                                        strOrderMain = dt(i)("bsx_refauftragnr").ToString

                                        strKommisionNr = ""

                                        If strOrderMain = "" Then
                                            strOrder = strOrderNr
                                        Else
                                            strOrder = strOrderMain
                                        End If

                                        'If Not IsMailingDissabled(strOrder) Then
                                        strLog = ExecuteEventOrderMail(cEvent, strKommisionNr, strOrder, strSMTPServer, strSMTPPort, strEmail, strUsername, strPassword, False, connTools, connKBS, connMAWI)
                                        'Else
                                        'strLog = "Mailing za naročilo " & strOrderMain & " je onemogočeno!"
                                        'End If

                                        If strLog <> "" Then
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, strLog)
                                            Else
                                                AddToActionLogSendMail(strLog)
                                            End If
                                        Else
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, "Order Nr.: = " & strOrderNr & " / " & strOrderMain)
                                            Else
                                                AddToActionLogSendMail("Order Nr.: = " & strOrderNr & " / " & strOrderMain)
                                            End If
                                        End If
                                    Case clsGlobal.EventType.event_offer_not_realized

                                        strOrderNr = dt(i)("BSX_VORGANGNR").ToString

                                        strKommisionNr = ""

                                        'If Not IsMailingDissabled(strOrderNr) Then
                                        strLog = ExecuteEventOrderMail(cEvent, strKommisionNr, strOrderNr, strSMTPServer, strSMTPPort, strEmail, strUsername, strPassword, True, connTools, connKBS, connMAWI)
                                        'Else
                                        'strLog = "Mailing za naročilo " & strOrderNr & " je onemogočeno!"
                                        'End If

                                        If strLog <> "" Then
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, strLog)
                                            Else
                                                AddToActionLogSendMail(strLog)
                                            End If
                                        Else
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, "Project Nr.: = " & strOrderNr)
                                            Else
                                                AddToActionLogSendMail("Project Nr.: = " & strOrderNr)
                                            End If
                                        End If

                                    Case clsGlobal.EventType.event_task_assigned
                                        Dim strPartner As String = ""
                                        Dim strTask As String = ""
                                        Dim strName As String = ""

                                        strPartner = dt(i)("gpx_identnummer").ToString
                                        strTask = dt(i)("kut_betreff").ToString
                                        strName = dt(i)("fullname").ToString

                                        strKommisionNr = ""

                                        strLog = ExecuteEventPartnerMail(cEvent, strPartner, strTask, DB2Lng(dt(i)("kut_id")), dt(i)("USX_MAIL").ToString, strSMTPServer, strSMTPPort, strEmail, strUsername, strPassword, connTools, connKBS)

                                        If strLog <> "" Then
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, strLog)
                                            Else
                                                AddToActionLogSendMail(strLog)
                                            End If
                                        Else
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, "Partner/Task = " & strPartner & " / " & strTask)
                                            Else
                                                AddToActionLogSendMail("Partner/Task = " & strPartner & " / " & strTask)
                                            End If
                                        End If
                                    Case clsGlobal.EventType.event_MAWI_subsequent_delivery
                                        strLog = ExecuteEventMaterialDelivered(cEvent, dt(i)("contract_nr").ToString, dt(i)("confirmation_nr").ToString, CDate(dt(i)("confirmation_date").ToString), dt(i)("articles").ToString, strSMTPServer, strSMTPPort, strEmail, strUsername, strPassword, connTools, connKBS)
                                        If strLog <> "" Then
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, strLog)
                                            Else
                                                AddToActionLogSendMail(strLog)
                                            End If
                                        End If
                                    Case Else
                                        Dim strOrderMain As String = ""
                                        strOrderMain = dt(i)("auftragsnummer").ToString

                                        If cEvent.EventType = clsGlobal.EventType.event_montaza_slepci Then
                                            strOrderNr = dt(i)("kommissionsnummer").ToString
                                        Else
                                            strOrderNr = ""
                                        End If

                                        strKommisionNr = ""

                                        'If Not IsMailingDissabled(strOrderMain) Then
                                        strLog = ExecuteEventOrderMail(cEvent, strKommisionNr, strOrderMain, strSMTPServer, strSMTPPort, strEmail, strUsername, strPassword, False, connTools, connKBS, connMAWI)
                                        'Else
                                        'strLog = "Mailing za naročilo " & strOrderMain & " je onemogočeno!"
                                        'End If

                                        If strLog <> "" Then
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, strLog)
                                            Else
                                                AddToActionLogSendMail(strLog)
                                            End If
                                        Else
                                            If Not IsNothing(rtf) Then
                                                AddToActionLogSendMailTB(rtf, "Order Nr.: = " & strOrderNr & " / " & strOrderMain)
                                            Else
                                                AddToActionLogSendMail("Order Nr.: = " & strOrderNr & " / " & strOrderMain)
                                            End If
                                        End If
                                End Select

                            Next
                        End If
                    End Using
                End Using
            End Using
        End Using

    End Sub


    Private Function GetNewUserTasks(dtmStart As Date, dtmTo As Date, strEventParam As String, connKBS As SqlConnection) As DataTable
        Dim strSQL As String = ""
        Dim strCountry As String = ""
        Dim strTask As String = ""
        Dim strParams() As String = Nothing
        Dim strParam() As String = Nothing
        Dim strCountrySQL As String = ""
        Dim strTaskSQL As String = ""

        strParams = strEventParam.Split(";")
        For i = 0 To strParams.Length - 1
            If strParams(i) <> "" Then
                strParam = strParams(i).Split("=")
                Select Case strParam(0)
                    Case "COUNTRY"
                        strCountrySQL = " AND co.LAN_KURZBEZ = '" & strParam(1) & "'"
                    Case "TASK"
                        strTaskSQL = " AND ut.kut_betreff = '" & strParam(1) & "'"
                End Select
            End If
        Next

        strSQL = "SELECT ut.kut_id, ut.KUT_ISTATUS, ut.kut_ipriority, ut.KUT_BETREFF, ut.kut_besch, kuab.USX_USER as dodelil, gp.GPX_IDENTNUMMER, gp.gpx_name, gp.gpx_vorname, " _
            & " ltrim(rtrim(gp.gpx_anrede) + ' ' + rtrim(gp.gpx_name) + ' ' + rtrim(gp.gpx_vorname)) as fullname, " _
            & " ut.kut_creation, ut.kut_beginntam, kut_faelligam, kuto.usx_user, kuto.USX_FULLNAME, kuto.USX_MAIL, kuto.USX_PHONE, kuto.USX_MOBILE " _
            & " FROM KMCUSERTASK ut " _
            & " LEFT JOIN KLAESUSER kuab ON kuab.USX_ID = ut.KUT_absender " _
            & " LEFT JOIN GP gp ON gp.GPX_ID  = ut.KUT_IDGP " _
            & " LEFT JOIN klaesuser kuto ON kuto.USX_ID = ut.KUT_EMPFAENGER " _
            & " LEFT JOIN land co on co.LAN_ID = gp.GPX_LAND " _
            & " WHERE ut.KUT_BEGINNTAM BETWEEN @dtmStart AND @dtmTo " _
            & strTaskSQL _
            & strCountrySQL _
            & " ORDER BY ut.KUT_BEGINNTAM "



        Using cmd As New SqlCommand(strSQL, connKBS)
            cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
            cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
            Dim dt As DataTable = GetData(cmd)
            Return dt
        End Using

        Return Nothing
    End Function
    Public Function GetTechicalFinnishedOrders(ByVal dtmStart As Date, ByVal dtmEnd As Date, connKBS As SqlConnection) As DataTable

        Dim strSQL As String = ""

        strSQL = "SELECT bsx_belegnr as auftragsnummer, BSX_0_DATUM_DATUMTEHNICNOOBDELANO as tehnicnoobdelano FROM VKBELEGSTUB " _
            & " WHERE BSX_0_DATUM_DATUMTEHNICNOOBDELANO BETWEEN @dtmStart AND @dtmEnd AND bsx_belegnr LIKE 'N%'"

        Using cmd As New SqlCommand(strSQL, connKBS)
            cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
            cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)
            Dim dt As DataTable = GetData(cmd)
            Return dt
        End Using

        Return Nothing
    End Function


    Public Function GetGoogleOrders(ByVal dtmStart As Date, ByVal dtmEnd As Date, connTools As SqlConnection) As DataTable

        Dim strSQL As String = ""

        strSQL = "SELECT order_nr as auftragsnummer FROM msora_order_info WHERE convert(date, date_inserted) BETWEEN @dtmStart AND @dtmEnd"

        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@dtmStart", dtmStart.Date)
            cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd.Date)
            Dim dt As DataTable = GetData(cmd)
            Return dt
        End Using

        Return Nothing
    End Function

    Private Function GetDeliveryNoteCreated(ByVal dtmStart As Date, ByVal dtmEnd As Date, connKBS As SqlConnection) As DataTable

        Dim strSQL As String = ""

        strSQL = "SELECT bsx_belegnr as auftragsnummer FROM VKBELEGSTUB WHERE BSX_ERFDATUM BETWEEN @dtmStart AND @dtmEnd AND bsx_ibelegart = 40"

        Using cmd As New SqlCommand(strSQL, connKBS)
            cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
            cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)
            Dim dt As DataTable = GetData(cmd)
            Return dt
        End Using

        Return Nothing
    End Function

    Private Function GetAvansCreated(ByVal dtmStart As Date, ByVal dtmEnd As Date, connKBS As SqlConnection) As DataTable

        Dim strSQL As String = ""

        strSQL = "SELECT bsx_belegnr as auftragsnummer FROM VKBELEGSTUB WHERE BSX_ERFDATUM BETWEEN @dtmStart AND @dtmEnd AND bsx_ibelegart = 71"

        Using cmd As New SqlCommand(strSQL, connKBS)
            cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
            cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)
            Dim dt As DataTable = GetData(cmd)
            Return dt
        End Using

        Return Nothing
    End Function

    Private Function GetOrdersKAPAGlass(ByVal dtmStart As Date, ByVal dtmEnd As Date, connKBS As SqlConnection, connKAPA As SqlConnection) As DataTable

        Dim strSQL As String = ""
        Dim dtReturn As New DataTable
        dtReturn.Columns.Add("auftragsnummer", Type.GetType("System.String"))

        strSQL = "SELECT atx_nummer as auftragsnummer FROM kapa_auftrag WHERE atx_datum BETWEEN @dtmStart AND @dtmEnd "

        Using cmd As New SqlCommand(strSQL, connKAPA)
            cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
            cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)

            Dim dt As DataTable = GetData(cmd)
            Dim lngSumme As Long = cls.Config.SummeNetto


            For i = 0 To dt.Rows.Count - 1

                Dim strNewOrderNr As String = dt(i)("auftragsnummer").ToString.Replace("T", "N")
                'strSQL = "SELECT bsx_summematglas FROM vkbelegstub WHERE bsx_belegnr = @strNewOrderNr"
                strSQL = "SELECT bsx_summenetto as bsx_summe FROM vkbelegstub WHERE bsx_belegnr = @strNewOrderNr"

                Using cmd2 As New SqlCommand(strSQL, connKBS)
                    cmd2.Parameters.AddWithValue("@strNewOrderNr", strNewOrderNr)
                    Dim dt2 As DataTable = GetData(cmd2)
                    If dt2.Rows.Count > 0 Then
                        If DB2Dbl(dt2(0)("bsx_summe")) > lngSumme Then
                            'ga dodam 
                            Dim dr As DataRow = dtReturn.NewRow
                            dr("auftragsnummer") = strNewOrderNr
                            dtReturn.Rows.Add(dr)
                        End If
                    End If
                End Using

            Next
        End Using



        Return dtReturn

    End Function


    Private Function GetOrdersKAPAStart(ByVal dtmStart As Date, ByVal dtmEnd As Date, connKAPA As SqlConnection) As DataTable

        Dim strSQL As String = ""
        Dim dtReturn As New DataTable
        dtReturn.Columns.Add("auftragsnummer", Type.GetType("System.String"))

        strSQL = "SELECT atx_nummer as auftragsnummer FROM kapa_auftrag WHERE atx_datum BETWEEN @dtmStart AND @dtmEnd "

        Using cmd As New SqlCommand(strSQL, connKAPA)
            cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
            cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)

            Dim dt As DataTable = GetData(cmd)

            Return dt

        End Using

    End Function

    Private Function GetOrdersTechnicalCreated(ByVal dtmStart As Date, ByVal dtmEnd As Date, connKBS As SqlConnection) As DataTable

        Dim strSQL As String = ""
        Dim dtReturn As New DataTable
        dtReturn.Columns.Add("auftragsnummer", Type.GetType("System.String"))

        strSQL = "SELECT bsx_belegnr as auftragsnummer FROM VKBELEGSTUB WHERE bsx_erfdatum BETWEEN @dtmStart AND @dtmEnd AND bsx_belegnr LIKE 'T%' AND bsx_ibelegart = 20"

        Using cmd As New SqlCommand(strSQL, connKBS)
            cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
            cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)

            Dim dt As DataTable = GetData(cmd)

            Return dt

        End Using

    End Function

    Private Function GetOrdersBlindsMontageFinished(ByVal dtmStart As Date, ByVal dtmEnd As Date, connTools As SqlConnection) As DataTable

        Dim strSQL As String = ""
        Dim dtReturn As New DataTable
        Try

            dtReturn.Columns.Add("auftragsnummer", Type.GetType("System.String"))
            dtReturn.Columns.Add("kommissionsnummer", Type.GetType("System.String"))

            strSQL = "SELECT order_nr, main_order_nr FROM montage WHERE montage_date BETWEEN @dtmStart AND @dtmEnd AND status = 3 AND rtrim(order_nr) LIKE '%S'"

            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)

                Dim dt As DataTable = GetData(cmd)


                For i = 0 To dt.Rows.Count - 1

                    Dim strNewOrderNr As String = dt(i)("main_order_nr").ToString
                    Dim dr As DataRow = dtReturn.NewRow
                    dr("auftragsnummer") = strNewOrderNr
                    dr("kommissionsnummer") = dt(i)("order_nr").ToString
                    dtReturn.Rows.Add(dr)

                Next


            End Using


            Return dtReturn
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Function MontageAlreadyDone(strContract As String, connTools As SqlConnection) As Boolean

        Dim strSQL As String = ""
        Dim dtReturn As New DataTable
        Try

            dtReturn.Columns.Add("auftragsnummer", Type.GetType("System.String"))
            dtReturn.Columns.Add("kommissionsnummer", Type.GetType("System.String"))

            strSQL = "SELECT montage_date FROM montage WHERE substring(order_nr,2,7) = @strContract ORDER BY montage_date ASC"

            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@strContract", Mid(strContract, 2, 7))

                Dim dt As DataTable = GetData(cmd)

                If dt.Rows.Count > 0 Then
                    If CDate(dt(0)("montage_date")) < Now.Date Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If

            End Using

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function GetInvoiceCreated(ByVal dtmStart As Date, ByVal dtmEnd As Date, ByVal strIbelegArt As String, connKBS As SqlConnection) As DataTable

        Dim strSQL As String = ""
        Try

            If strIbelegArt.Trim = "" Then
                strIbelegArt = "70, 72, 73"
            End If

            strSQL = "SELECT bsx_belegnr as auftragsnummer, bsx_refauftragnr FROM VKBELEGSTUB WHERE BSX_ERFDATUM BETWEEN @dtmStart AND @dtmEnd AND bsx_ibelegart IN (" & strIbelegArt & ")"

            Using cmd As New SqlCommand(strSQL, connKBS)
                cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)
                Dim dt As DataTable = GetData(cmd)
                Return dt
            End Using

            Return Nothing

        Catch ex As Exception
            Call AddToErrorLog(ex.ToString)
            Return Nothing
        End Try

    End Function

    Private Function GetInvoicePrinted(ByVal dtmStart As Date, ByVal dtmEnd As Date, ByVal strIbelegArt As String, connKBS As SqlConnection) As DataTable

        Dim strSQL As String = ""

        Try

            If strIbelegArt.Trim = "" Then
                strIbelegArt = "70, 72, 73"
            End If

            strSQL = "SELECT bsx_belegnr as auftragsnummer, bsx_refauftragnr FROM VKBELEGSTUB " _
                & " WHERE BSX_DRUCKDATUM IS NOT NULL AND BSX_BELEGDATUM BETWEEN @dtmStart AND @dtmEnd AND bsx_ibelegart IN (" & strIbelegArt & ")"

            Using cmd As New SqlCommand(strSQL, connKBS)
                cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)
                Dim dt As DataTable = GetData(cmd)
                Return dt
            End Using

            Return Nothing
        Catch ex As Exception
            Call AddToErrorLog(ex.ToString)
            Return Nothing
        End Try

    End Function

    Private Function GetNotRealizedOffers(ByVal dtmStart As Date, ByVal dtmEnd As Date, connKBS As SqlConnection) As DataTable

        Dim strSQL As String = ""

        Try
            'dobim samo projekte, ki so bili nazadnje spremenjeni v obdobju in nimajo nobenega dokumenta > ponudbe
            strSQL = "SELECT DISTINCT bsx.BSX_VORGANGNR FROM vkbelegstub bsx WHERE bsx.BSX_VORGANGNR IN " _
                & " (SELECT VKV_NUMMER FROM VKVORGANG vkv WHERE vkv.VKV_MODDATE BETWEEN @dtmStart AND @dtmEnd " _
                & " AND (SELECT count(*) FROM vkbelegstub vkb WHERE vkb.BSX_VORGANGNR = vkv.VKV_NUMMER AND vkb.BSX_IBELEGART >= 20) = 0 AND ) " _
                & " ORDER BY bsx.BSX_VORGANGNR"

            strSQL = "SELECT DISTINCT bsx.BSX_VORGANGNR FROM vkbelegstub bsx WHERE bsx.BSX_VORGANGNR IN " _
                & " (SELECT VSX_VORGANGNR FROM VKVORGANGSTUB vkv WHERE vkv.VSX_ERFDATUM BETWEEN @dtmStart AND @dtmEnd " _
                & " AND (SELECT count(*) FROM vkbelegstub vkb WHERE vkb.BSX_VORGANGNR = vkv.vsx_vorgangnr AND vkb.BSX_IBELEGART >= 20) = 0 ) " _
                & " ORDER BY bsx.BSX_VORGANGNR"

            Using cmd As New SqlCommand(strSQL, connKBS)
                cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)
                Dim dt As DataTable = GetData(cmd)
                Return dt
            End Using

            Return Nothing
        Catch ex As Exception
            Call AddToErrorLog(ex.ToString)
            Return Nothing
        End Try

    End Function

    Private Function FindEventInLog(ByVal intEventId As Long, ByVal strKommisionNr As String, ByVal strOrderNr As String, ByVal intSendToType As SendToType, ByVal intEventType As Integer, intDaysToRepeat As Integer, strSubject As String, connTools As SqlConnection, Optional lngKU_ID As Long = -1) As Boolean
        Dim strSQL As String = ""
        Dim dt As DataTable

        Try


            If strKommisionNr <> "" Then
                'če je že poslano narocilo, potem ne pošiljam več 
                strSQL = "SELECT TOP 1 * FROM eprod_events_log WHERE kommision_nr = @strKommisionNr AND order_nr = @strOrderNr AND event_id = @intEventId " _
                    & " AND recipient = @intSendToType AND event_type = @intEventType "
                If strSubject <> "" Then
                    strSQL = strSQL & " AND subject_email = @strSubject"
                End If

                If lngKU_ID <> -1 Then
                    strSQL = strSQL & " AND ut_id = @lngKU_ID "
                End If

                strSQL = strSQL & " ORDER BY event_date DESC "

                Using cmd As New SqlCommand(strSQL, connTools)
                    cmd.Parameters.AddWithValue("@strKommisionNr", "")
                    cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
                    cmd.Parameters.AddWithValue("@intEventId", intEventId)
                    cmd.Parameters.AddWithValue("@intSendToType", intSendToType)
                    cmd.Parameters.AddWithValue("@intEventType", intEventType)

                    If strSubject <> "" Then
                        cmd.Parameters.AddWithValue("@strSubject", strSubject)
                    End If
                    If lngKU_ID <> -1 Then
                        cmd.Parameters.AddWithValue("@lngKU_ID", lngKU_ID)
                    End If

                    dt = GetData(cmd)

                    If dt.Rows.Count > 0 Then
                        If intDaysToRepeat = 0 Then
                            Return True
                        Else
                            If DateDiff("d", CDate(dt(0)("event_date")), Now.Date) < intDaysToRepeat Then
                                Return True
                            End If
                        End If
                    End If

                End Using
            End If

            strSQL = "SELECT TOP 1 * FROM eprod_events_log WHERE kommision_nr = @strKommisionNr AND order_nr = @strOrderNr AND event_id = @intEventId " _
                & " AND recipient = @intSendToType AND event_type = @intEventType "

            If strSubject <> "" Then
                strSQL = strSQL & " AND subject_email = @strSubject"
            End If

            If lngKU_ID <> -1 Then
                strSQL = strSQL & " AND ut_id = @lngKU_ID "
            End If

            strSQL = strSQL & " ORDER BY event_date DESC"
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@strKommisionNr", strKommisionNr)
                cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
                cmd.Parameters.AddWithValue("@intEventId", intEventId)
                cmd.Parameters.AddWithValue("@intSendToType", intSendToType)
                cmd.Parameters.AddWithValue("@intEventType", intEventType)

                If strSubject <> "" Then
                    cmd.Parameters.AddWithValue("@strSubject", strSubject)
                End If

                If lngKU_ID <> -1 Then
                    cmd.Parameters.AddWithValue("@lngKU_ID", lngKU_ID)
                End If


                dt = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    If intDaysToRepeat = 0 Then
                        Return True
                    Else
                        If DateDiff("d", CDate(dt(0)("event_date")), Now.Date) < intDaysToRepeat Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                End If
            End Using

        Catch ex As Exception
            Return True
        End Try

    End Function


    Private Function ExecuteEventOrderMail(ByVal cEvent As cls.event.EprodEvent, ByVal strKommisionNr As String, ByVal strOrderNr As String, ByVal strSMTPServer As String, ByVal strSMTPPort As String, ByVal strEmailSender As String, ByVal strUsername As String, ByVal strPassword As String, ByVal blnProject As Boolean, connTools As SqlConnection, connKBS As SqlConnection, connMAWI As SqlConnection) As String
        Dim strReturn As String = ""
        Dim streMailReceipent As String = ""
        Dim dtT As DataTable = Nothing
        Dim dtMainOrder As DataTable = Nothing
        Dim strNewOrderNr As String = ""
        Dim blnSkipOrder As Boolean = False
        Dim strRefDoc As String = ""
        Dim dtmRefDate As Date = Nothing

        Try
            'pošljem mail vsem, ki so določeni v eventu

            'T
            dtT = GetOrderData(strOrderNr, connKBS)

            blnSkipOrder = False

            If Not blnProject Then
                If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                    If Mid(strOrderNr, 1, 1) = "T" Then
                        strNewOrderNr = strOrderNr.Replace("T", "N")
                    End If
                    If Mid(strOrderNr, 1, 1) = "C" Then
                        strNewOrderNr = strOrderNr.Replace("C", "N")
                    End If
                    If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                Else
                    strNewOrderNr = strOrderNr
                End If

                dtMainOrder = GetOrderData(strNewOrderNr, connKBS)
                If Not dtMainOrder Is Nothing Then
                    If cEvent.EventExcludeReclamation Then

                        If dtMainOrder.Rows.Count > 0 Then
                            If dtMainOrder(0)("bsx_4_auswahl_reklamacija").ToString.Trim <> "" And dtMainOrder(0)("bsx_4_auswahl_reklamacija").ToString.Trim <> "Rezervacija" Then
                                blnSkipOrder = True
                                strReturn = "Order " & strOrderNr & " skipped: " & dtMainOrder(0)("bsx_4_auswahl_reklamacija").ToString.Trim
                            End If
                        End If
                    End If

                    If cEvent.EventCheckDelivery Then
                        If dtMainOrder.Rows.Count > 0 Then
                            If OrderHasDelivery(strNewOrderNr, strRefDoc, dtmRefDate, connKBS) Then
                                blnSkipOrder = True
                                strReturn = "Order " & strOrderNr & " skipped: " & "Already delivered: " & strRefDoc & "; " & dtmRefDate.ToString
                            End If
                        End If
                    End If

                    If cEvent.EventCheckMontage Then
                        If dtMainOrder.Rows.Count > 0 Then
                            Dim dtmMontageDate As Date
                            Dim strMontagePerson As String = ""
                            If OrderHasMontage(strNewOrderNr, cEvent.EventMontageDays, strMontagePerson, dtmMontageDate, connTools) Then
                                blnSkipOrder = True
                                strReturn = "Order " & strOrderNr & " skipped: " & "Montage already done or in next: " & cEvent.EventMontageDays & " days; " & dtmMontageDate.ToString & "; Montage person: " & strMontagePerson
                            End If
                        End If
                    End If
                End If

            Else
                    strNewOrderNr = strOrderNr
            End If



            If cEvent.EventPartnerId.Trim <> "" Then

                'pregledam pogoje
                'PARTNER=
                'COUNTRY=
                Dim aParam() As String

                aParam = cEvent.EventPartnerId.Trim.Split("=")

                If aParam.Length > 1 Then
                    Select Case aParam(0).ToUpper
                        Case "PARTNER"
                            If aParam(1) <> dtT(0)("gpx_identnummer").ToString.ToUpper.Trim Then
                                blnSkipOrder = True
                            End If
                        Case "COUNTRY"
                            If aParam(1) <> dtT(0)("lan_kurzbez").ToString.ToUpper.Trim Then
                                blnSkipOrder = True
                            End If
                        Case Else
                            If cEvent.EventPartnerId.ToUpper.Trim <> dtT(0)("gpx_identnummer").ToString.ToUpper.Trim Then
                                blnSkipOrder = True
                            End If
                    End Select
                Else 'samo na ime (stara verzija)
                    If cEvent.EventPartnerId.ToUpper.Trim <> dtT(0)("gpx_identnummer").ToString.ToUpper.Trim Then
                        blnSkipOrder = True
                    End If
                End If

            End If

            If Not blnSkipOrder Then


                strReturn = "Kommission/Order: " & strKommisionNr & "/" & strOrderNr & vbCrLf
                strReturn = strReturn & "Komercialist = " & GetEmail(cls.event.EprodEvent.intMailType.MailToComercialist, strOrderNr, connTools, connKBS) & vbCrLf
                strReturn = strReturn & "Tehnolog = " & GetEmail(cls.event.EprodEvent.intMailType.MailToTechnology, strOrderNr, connTools, connKBS) & vbCrLf
                strReturn = strReturn & "Partner = " & GetEmail(cls.event.EprodEvent.intMailType.MailToPartner, strOrderNr, connTools, connKBS) & vbCrLf
                strReturn = strReturn & "Extra mail = " & cEvent.EventEmailToExtra & vbCrLf
                strReturn = strReturn & "Partner ID = " & cEvent.EventPartnerId & vbCrLf


                Dim intRepeat As Integer = 0

                If Mid(cEvent.EventParameter, 1, 1) = "R" Then
                    If IsNumeric(Mid(cEvent.EventParameter, 2)) Then
                        intRepeat = CInt(Mid(cEvent.EventParameter, 2))
                    End If
                End If



                If cEvent.EventMailToCommerialist Then
                    If Not FindEventInLog(cEvent.EventId, strKommisionNr, strOrderNr, SendToType.ToComercialist, 0, intRepeat, "", connTools) Then
                        streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToComercialist, strOrderNr, connTools, connKBS)
                        If streMailReceipent.ToUpper.Trim = "X" Then
                            streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToComercialist, strOrderNr, connTools, connKBS, False)
                            strReturn = strReturn & vbTab & vbTab & " - ROČNA IZKLJUČITEV; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        Else
                            If streMailReceipent <> "" Then
                                If SendMail(SendToType.ToComercialist, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToComercialist, strKommisionNr, strOrderNr, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, dtMainOrder, connTools, connKBS) Then
                                    strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                Else
                                    strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                End If
                            Else
                                strReturn = strReturn & vbTab & vbTab & " - NI PREJEMNIKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                            End If
                        End If
                    End If
                End If

                If cEvent.EventMailToDeveloper Then
                    If Not FindEventInLog(cEvent.EventId, strKommisionNr, strOrderNr, SendToType.ToDeveloper, 0, intRepeat, "", connTools) Then
                        streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToDeveloper, strOrderNr, connTools, connKBS)
                        If streMailReceipent.ToUpper.Trim = "X" Then
                            streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToDeveloper, strOrderNr, connTools, connKBS, False)
                            strReturn = strReturn & vbTab & vbTab & " - ROČNA IZKLJUČITEV; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        Else
                            If streMailReceipent <> "" Then
                                If SendMail(SendToType.ToDeveloper, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToDeveloper, strKommisionNr, strOrderNr, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, dtMainOrder, connTools, connKBS) Then
                                    strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                Else
                                    strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                End If
                            Else
                                strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                            End If
                        End If
                    End If
                End If

                If cEvent.EventMailToDirector Then
                    If Not FindEventInLog(cEvent.EventId, strKommisionNr, strOrderNr, SendToType.ToManager, 0, intRepeat, "", connTools) Then
                        streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToDirector, strOrderNr, connTools, connKBS)
                        If streMailReceipent.ToUpper.Trim = "X" Then
                            streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToDirector, strOrderNr, connTools, connKBS, False)
                            strReturn = strReturn & vbTab & vbTab & " - ROČNA IZKLJUČITEV; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        Else
                            If streMailReceipent <> "" Then
                                If SendMail(SendToType.ToManager, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToDirector, strKommisionNr, strOrderNr, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, dtMainOrder, connTools, connKBS) Then
                                    strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                Else
                                    strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                End If
                            Else
                                strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                            End If
                        End If
                    End If
                End If

                If cEvent.EventMailToLogistic Then
                    If Not FindEventInLog(cEvent.EventId, strKommisionNr, strOrderNr, SendToType.ToLogistic, 0, intRepeat, "", connTools) Then
                        streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToLogistic, strOrderNr, connTools, connKBS)
                        If streMailReceipent.ToUpper.Trim = "X" Then
                            streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToLogistic, strOrderNr, connTools, connKBS, False)
                            strReturn = strReturn & vbTab & vbTab & " - ROČNA IZKLJUČITEV; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        Else
                            If streMailReceipent <> "" Then
                                If SendMail(SendToType.ToLogistic, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToLogistic, strKommisionNr, strOrderNr, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, dtMainOrder, connTools, connKBS) Then
                                    strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                Else
                                    strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                End If
                            Else
                                strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                            End If
                        End If
                    End If
                End If

                'stranki ne pošiljamo mailov, če je onemogočeno
                If Not IsMailingDissabled(strOrderNr, connTools, connKBS) Then
                    If cEvent.EventMailToPartner Then
                        If Not FindEventInLog(cEvent.EventId, strKommisionNr, strOrderNr, SendToType.ToPartner, 0, intRepeat, "", connTools) Then

                            streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToPartner, strOrderNr, connTools, connKBS)

                            If streMailReceipent.ToUpper.Trim = "X" Then
                                streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToPartner, strOrderNr, connTools, connKBS, False)
                                strReturn = strReturn & vbTab & vbTab & " - ROČNA IZKLJUČITEV; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                            Else
                                If streMailReceipent <> "" Then
                                    If SendMail(SendToType.ToPartner, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToPartner, strKommisionNr, strOrderNr, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, dtMainOrder, connTools, connKBS) Then
                                        strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                    Else
                                        strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                    End If
                                Else
                                    strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                End If
                            End If
                        End If
                    End If
                Else
                    strReturn = strReturn & vbTab & vbTab & " Pošiljanje pošte je onemogočeno" & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                End If

                If cEvent.EventMailToProduction Then
                    If Not FindEventInLog(cEvent.EventId, strKommisionNr, strOrderNr, SendToType.ToProduction, 0, intRepeat, "", connTools) Then
                        streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToProduction, strOrderNr, connTools, connKBS)
                        If streMailReceipent.ToUpper.Trim = "X" Then
                            streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToProduction, strOrderNr, connTools, connKBS, False)
                            strReturn = strReturn & vbTab & vbTab & " - ROČNA IZKLJUČITEV; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        Else
                            If streMailReceipent <> "" Then
                                If SendMail(SendToType.ToProduction, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToProduction, strKommisionNr, strOrderNr, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, dtMainOrder, connTools, connKBS) Then
                                    strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                Else
                                    strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                End If
                            Else
                                strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                            End If
                        End If
                    End If
                End If

                If cEvent.EventMailToTechnology Then
                    If Not FindEventInLog(cEvent.EventId, strKommisionNr, strOrderNr, SendToType.ToTechnology, 0, intRepeat, "", connTools) Then
                        streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToTechnology, strOrderNr, connTools, connKBS)
                        If streMailReceipent.ToUpper.Trim = "X" Then
                            streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToTechnology, strOrderNr, connTools, connKBS, False)
                            strReturn = strReturn & vbTab & vbTab & " - ROČNA IZKLJUČITEV; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        Else
                            If streMailReceipent <> "" Then
                                If SendMail(SendToType.ToTechnology, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToTechnology, strKommisionNr, strOrderNr, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, dtMainOrder, connTools, connKBS) Then
                                    strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                Else
                                    strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                                End If
                            Else
                                strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                            End If
                        End If
                    End If
                End If

                If cEvent.EventEmailToExtra <> "" Then
                    If Not FindEventInLog(cEvent.EventId, strKommisionNr, strOrderNr, SendToType.ToExtra, 0, intRepeat, "", connTools) Then
                        streMailReceipent = cEvent.EventEmailToExtra

                        If streMailReceipent <> "" Then
                            If SendMail(SendToType.ToExtra, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MialToExtra, strKommisionNr, strOrderNr, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, dtMainOrder, connTools, connKBS) Then
                                strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                            Else
                                strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                            End If
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        End If

                    End If
                End If

            End If 'blnskiporder

            Return strReturn


        Catch ex As Exception
            Return "Dogodek: " & cEvent.EventId & " - " & cEvent.EventDescription.Trim & " Naročilo: " & strKommisionNr & "/" & strOrderNr & " - NAPAKA" & vbCr & streMailReceipent & vbCrLf
        End Try

    End Function

    Private Function ExecuteEventPartnerMail(ByVal cEvent As cls.event.EprodEvent, ByVal strPartner As String, ByVal strTask As String, ByVal lngUT_ID As Long, ByVal strKomercialistEmail As String, ByVal strSMTPServer As String, ByVal strSMTPPort As String, ByVal strEmailSender As String, ByVal strUsername As String, ByVal strPassword As String, connTools As SqlConnection, connKBS As SqlConnection) As String
        Dim strReturn As String = ""
        Dim streMailReceipent As String = ""
        Dim strRefDoc As String = ""
        Dim dtmRefDate As Date = Nothing
        Dim strPartnerEmail As String = GetEmailPartner(strPartner, connKBS)
        Dim strSQL As String = ""
        Dim dtT As DataTable



        Try


            strSQL = "SELECT ut.kut_id, ut.KUT_ISTATUS, ut.kut_ipriority, ut.KUT_BETREFF, ut.kut_besch, kuab.USX_USER as dodelil, gp.GPX_IDENTNUMMER, gp.gpx_name, gp.gpx_vorname, gp.gpx_anrede, " _
                & " ltrim(rtrim(gp.gpx_anrede) + ' ' + rtrim(gp.gpx_name) + ' ' + rtrim(gp.gpx_vorname)) as fullname, " _
                & " ut.kut_creation, ut.kut_beginntam, kut_faelligam, kuto.usx_user, kuto.USX_FULLNAME, kuto.USX_MAIL, kuto.USX_PHONE, kuto.USX_MOBILE " _
                & " FROM KMCUSERTASK ut " _
                & " LEFT JOIN KLAESUSER kuab ON kuab.USX_ID = ut.KUT_absender " _
                & " LEFT JOIN GP gp ON gp.GPX_ID  = ut.KUT_IDGP " _
                & " LEFT JOIN klaesuser kuto ON kuto.USX_ID = ut.KUT_EMPFAENGER " _
                & " LEFT JOIN land co on co.LAN_ID = gp.GPX_LAND " _
                & " WHERE ut.KUT_BETREFF = @strTask " _
                & " AND gp.GPX_IDENTNUMMER = @strPartner " _
                & " ORDER BY ut.KUT_BEGINNTAM DESC "

            Using cmd As New SqlCommand(strSQL, connKBS)
                cmd.Parameters.AddWithValue("@strTask", strTask)
                cmd.Parameters.AddWithValue("@strPartner", strPartner)
                dtT = GetData(cmd)
            End Using


            strReturn = "Partner/Task: " & strPartner & "/" & strTask & vbCrLf
            strReturn = strReturn & "Komercialist = " & strKomercialistEmail & vbCrLf
            strReturn = strReturn & "Partner = " & strPartnerEmail & vbCrLf
            strReturn = strReturn & "Extra mail = " & cEvent.EventEmailToExtra & vbCrLf
            strReturn = strReturn & "Partner ID = " & cEvent.EventPartnerId & vbCrLf


            Dim intRepeat As Integer = 0

            If Mid(cEvent.EventParameter, 1, 1) = "R" Then
                If IsNumeric(Mid(cEvent.EventParameter, 2)) Then
                    intRepeat = CInt(Mid(cEvent.EventParameter, 2))
                End If
            End If




            If cEvent.EventMailToDeveloper Then
                If Not FindEventInLog(cEvent.EventId, strPartner, strTask, SendToType.ToDeveloper, 0, intRepeat, "", connTools, lngUT_ID) Then
                    streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToDeveloper, "", connTools, connKBS)

                    If streMailReceipent <> "" Then
                        If SendMailTask(SendToType.ToDeveloper, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToDeveloper, strPartner, strTask, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, connTools, connKBS, lngUT_ID) Then
                            strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        End If
                    Else
                        strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                    End If

                End If
            End If

            If cEvent.EventMailToDirector Then
                If Not FindEventInLog(cEvent.EventId, strPartner, strTask, SendToType.ToManager, 0, intRepeat, "", connTools, lngUT_ID) Then
                    streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToDirector, "", connTools, connKBS)

                    If streMailReceipent <> "" Then
                        If SendMailTask(SendToType.ToManager, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToDirector, strPartner, strTask, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, connTools, connKBS, lngUT_ID) Then
                            strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        End If
                    Else
                        strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                    End If

                End If
            End If

            If cEvent.EventMailToLogistic Then
                If Not FindEventInLog(cEvent.EventId, strPartner, strTask, SendToType.ToLogistic, 0, intRepeat, "", connTools, lngUT_ID) Then
                    streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToLogistic, "", connTools, connKBS)

                    If streMailReceipent <> "" Then
                        If SendMailTask(SendToType.ToLogistic, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToLogistic, strPartner, strTask, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, connTools, connKBS, lngUT_ID) Then
                            strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        End If
                    Else
                        strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                    End If

                End If
            End If

            If cEvent.EventMailToPartner Then
                If Not FindEventInLog(cEvent.EventId, strPartner, strTask, SendToType.ToPartner, 0, intRepeat, "", connTools, lngUT_ID) Then

                    streMailReceipent = strPartnerEmail


                    If streMailReceipent <> "" Then
                        If SendMailTask(SendToType.ToPartner, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToPartner, strPartner, strTask, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, connTools, connKBS, lngUT_ID) Then
                            strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        End If
                    Else
                        strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                    End If

                End If
            End If


            If cEvent.EventEmailToExtra <> "" Then
                If Not FindEventInLog(cEvent.EventId, strPartner, strTask, SendToType.ToExtra, 0, intRepeat, "", connTools, lngUT_ID) Then
                    streMailReceipent = cEvent.EventEmailToExtra

                    If streMailReceipent <> "" Then
                        If SendMailTask(SendToType.ToExtra, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MialToExtra, strPartner, strTask, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, connTools, connKBS, lngUT_ID) Then
                            strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                        End If
                    Else
                        strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                    End If

                End If
            End If

            Return strReturn


        Catch ex As Exception
            Return "Dogodek: " & cEvent.EventId & " - " & cEvent.EventDescription.Trim & " Partner/Task " & strPartner & "/" & strTask & " - NAPAKA" & vbCr & streMailReceipent & vbCrLf
        End Try

    End Function

    Private Function ExecuteEventMaterialDelivered(ByVal cEvent As cls.event.EprodEvent, strContract As String, strConfirmationNr As String, dtmConfirmationDate As Date, strArticles As String, ByVal strSMTPServer As String, ByVal strSMTPPort As String, ByVal strEmailSender As String, ByVal strUsername As String, ByVal strPassword As String, connTools As SqlConnection, connKBS As SqlConnection) As String
        Dim strReturn As String = ""
        Dim streMailReceipent As String = ""
        Dim strRefDoc As String = ""
        Dim dtmRefDate As Date = Nothing
        Dim strSQL As String = ""


        Try

            Dim intRepeat As Integer = 0

            If Mid(cEvent.EventParameter, 1, 1) = "R" Then
                If IsNumeric(Mid(cEvent.EventParameter, 2)) Then
                    intRepeat = CInt(Mid(cEvent.EventParameter, 2))
                End If
            End If



            Dim dtT As DataTable = CreateTableMAWIOrdersShort()
            Dim dr As DataRow = dtT.NewRow
            dr("contract_nr") = strContract
            dr("confirmation_nr") = strConfirmationNr
            dr("confirmation_date") = dtmConfirmationDate
            dr("articles") = strArticles
            dtT.Rows.Add(dr)

            If cEvent.EventMailToCommerialist Then
                If Not FindEventInLog(cEvent.EventId, strConfirmationNr, strContract, SendToType.ToComercialist, 0, intRepeat, "", connTools) Then

                    streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToComercialist, strContract, connTools, connKBS)


                    If streMailReceipent <> "" Then
                        If SendMailTask(SendToType.ToComercialist, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToComercialist, strConfirmationNr, strContract, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, connTools, connKBS) Then
                            strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        End If
                    Else
                        strReturn = strReturn & vbTab & vbTab & " - NI PREJEMNIKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                    End If

                End If
            End If

            If cEvent.EventMailToDeveloper Then
                If Not FindEventInLog(cEvent.EventId, strConfirmationNr, strContract, SendToType.ToDeveloper, 0, intRepeat, "", connTools) Then
                    streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToDeveloper, "", connTools, connKBS)

                    If streMailReceipent <> "" Then
                        If SendMailTask(SendToType.ToDeveloper, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToDeveloper, strConfirmationNr, strContract, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, connTools, connKBS) Then
                            strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        End If
                    Else
                        strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                    End If

                End If
            End If

            If cEvent.EventMailToDirector Then
                If Not FindEventInLog(cEvent.EventId, strConfirmationNr, strContract, SendToType.ToManager, 0, intRepeat, "", connTools) Then
                    streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToDirector, "", connTools, connKBS)

                    If streMailReceipent <> "" Then
                        If SendMailTask(SendToType.ToManager, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToDirector, strConfirmationNr, strContract, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, connTools, connKBS) Then
                            strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        End If
                    Else
                        strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                    End If

                End If
            End If

            If cEvent.EventMailToLogistic Then
                If Not FindEventInLog(cEvent.EventId, strConfirmationNr, strContract, SendToType.ToLogistic, 0, intRepeat, "", connTools) Then
                    streMailReceipent = GetEmail(cls.event.EprodEvent.intMailType.MailToLogistic, "", connTools, connKBS)

                    If streMailReceipent <> "" Then
                        If SendMailTask(SendToType.ToLogistic, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToLogistic, strConfirmationNr, strContract, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, connTools, connKBS) Then
                            strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        End If
                    Else
                        strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                    End If

                End If
            End If

            If cEvent.EventMailToPartner Then
                If Not FindEventInLog(cEvent.EventId, strConfirmationNr, strContract, SendToType.ToPartner, 0, intRepeat, "", connTools) Then

                    streMailReceipent = GetEmailPartner(GetName(cls.event.EprodEvent.intMailType.MailToComercialist, strContract, connKBS), connKBS)


                    If streMailReceipent <> "" Then
                        If SendMailTask(SendToType.ToPartner, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MailToPartner, strConfirmationNr, strContract, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, connTools, connKBS) Then
                            strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        End If
                    Else
                        strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                    End If

                End If
            End If


            If cEvent.EventEmailToExtra <> "" Then
                If Not FindEventInLog(cEvent.EventId, strConfirmationNr, strContract, SendToType.ToExtra, 0, intRepeat, "", connTools) Then
                    streMailReceipent = cEvent.EventEmailToExtra

                    If streMailReceipent <> "" Then
                        If SendMailTask(SendToType.ToExtra, streMailReceipent, cEvent, cls.event.EprodEvent.intMailType.MialToExtra, strConfirmationNr, strContract, strSMTPServer, strSMTPPort, strEmailSender, strUsername, strPassword, dtT, connTools, connKBS) Then
                            strReturn = strReturn & vbTab & vbTab & " - OK --> " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        Else
                            strReturn = strReturn & vbTab & vbTab & " - NAPAKA; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & " / Potrditev: " & strConfirmationNr & " / Naročilo: " & strContract & vbCrLf
                        End If
                    Else
                        strReturn = strReturn & vbTab & vbTab & " - NAPAKA - ni prejemnika; " & streMailReceipent & " " & cEvent.EventId & " " & cEvent.EventDescription & vbCrLf
                    End If

                End If
            End If


            Return strReturn


        Catch ex As Exception
            Return "Dogodek: " & cEvent.EventId & " - " & cEvent.EventDescription.Trim & " Partner/Task " & strContract & "/" & strConfirmationNr & " - NAPAKA" & vbCr & streMailReceipent & vbCrLf
        End Try

    End Function




    Private Function ExecuteEventOrderNoMail(ByVal cEvent As cls.event.EprodEvent, ByVal dt As DataTable) As String
        Dim strReturn As String = ""
        Dim streMailReceipent As String = ""
        Dim dtT As DataTable = Nothing
        Dim dtMainOrder As DataTable = Nothing
        Dim strNewOrderNr As String = ""
        Dim blnSkipOrder As Boolean = False
        Dim strRefDoc As String = ""
        Dim dtmRefDate As Date = Nothing

        Try



            Select Case cEvent.EventType

                Case clsGlobal.EventType.event_kommission_cutter_move_to_archive


                    strReturn = MoveCutterFilesToArchive(dt)

                Case Else

            End Select




            Return strReturn


        Catch ex As Exception
            Return "Dogodek: " & cEvent.EventId & " - " & cEvent.EventDescription.Trim & " - NAPAKA"
        End Try

    End Function

    Private Function ExecuteEventOrderNoMail(ByVal cEvent As cls.event.EprodEvent, ByVal strKommisionNr As String, ByVal strOrderNr As String, connTools As SqlConnection, connKBS As SqlConnection) As String
        Dim strReturn As String = ""
        Dim streMailReceipent As String = ""
        Dim dtT As DataTable = Nothing
        Dim dtMainOrder As DataTable = Nothing
        Dim strNewOrderNr As String = ""
        Dim blnSkipOrder As Boolean = False
        Dim strRefDoc As String = ""
        Dim dtmRefDate As Date = Nothing

        Try
            'pošljem mail vsem, ki so določeni v eventu

            'T
            dtT = GetOrderData(strOrderNr, connKBS)

            blnSkipOrder = False

            If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                If Mid(strOrderNr, 1, 1) = "T" Then
                    strNewOrderNr = strOrderNr.Replace("T", "N")
                End If
                If Mid(strOrderNr, 1, 1) = "C" Then
                    strNewOrderNr = strOrderNr.Replace("C", "N")
                End If
                If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
            Else
                strNewOrderNr = strOrderNr
            End If

            dtMainOrder = GetOrderData(strNewOrderNr, connKBS)

            If cEvent.EventExcludeReclamation Then

                If dtMainOrder.Rows.Count > 0 Then
                    If dtMainOrder(0)("bsx_4_auswahl_reklamacija").ToString.Trim <> "" Then
                        blnSkipOrder = True
                        strReturn = "Order " & strOrderNr & " skipped: " & dtMainOrder(0)("bsx_4_auswahl_reklamacija").ToString.Trim
                    End If
                End If
            End If

            If cEvent.EventCheckDelivery Then
                If dtMainOrder.Rows.Count > 0 Then
                    If OrderHasDelivery(strNewOrderNr, strRefDoc, dtmRefDate, connKBS) Then
                        blnSkipOrder = True
                        strReturn = "Order " & strOrderNr & " skipped: " & "Already delivered: " & strRefDoc & "; " & dtmRefDate.ToString
                    End If
                End If
            End If

            If Not blnSkipOrder Then


                Select Case cEvent.EventType
                    Case clsGlobal.EventType.event_kommission_send_to_cutter
                        strReturn = "Kommission/Order: " & strKommisionNr & "/" & strOrderNr & vbCrLf

                        strReturn = strReturn & vbTab & ExecuteCutterEvent(cEvent, strKommisionNr, strNewOrderNr, connTools)



                    Case Else

                End Select


            End If 'blnskiporder

            Return strReturn


        Catch ex As Exception
            Return "Dogodek: " & cEvent.EventId & " - " & cEvent.EventDescription.Trim & " Naročilo: " & strKommisionNr & "/" & strOrderNr & " - NAPAKA" & vbCr & streMailReceipent & vbCrLf
        End Try

    End Function

    Public Function MoveCutterFilesToArchive(ByVal dt As DataTable) As String
        Dim strReturn As String = "Končani razrezi: " + vbCrLf
        Dim strFileName As String = ""
        Dim strStatus As String = ""
        For i = 0 To dt.Rows.Count - 1
            strFileName = dt(i)("FileName").ToString
            strStatus = dt(i)("status").ToString
            If strStatus.Trim = "2" Then
                strReturn = strReturn & strFileName & " - Celotno Razrezan" & vbCrLf
                If Not File.Exists(Path.GetDirectoryName(strFileName) & "\Arhiv\" & Path.GetFileName(strFileName)) Then
                    File.Move(strFileName, Path.GetDirectoryName(strFileName) & "\Arhiv\" & Path.GetFileName(strFileName))
                Else
                    File.Delete(strFileName)
                End If
            ElseIf strStatus.Trim = "3" Then
                strReturn = strReturn & strFileName & " - Končan na KONTROLI" & vbCrLf
                If Not File.Exists(Path.GetDirectoryName(strFileName) & "\Arhiv\" & Path.GetFileName(strFileName)) Then
                    File.Move(strFileName, Path.GetDirectoryName(strFileName) & "\Arhiv\" & Path.GetFileName(strFileName))
                Else
                    File.Delete(strFileName)
                End If
            ElseIf strStatus.Trim = "1" Then
                strReturn = strReturn & strFileName & " - Delno razrezan" & vbCrLf
            Else
                strReturn = strReturn & strFileName & " - Nič razrezan" & vbCrLf
            End If

        Next

        Return strReturn

    End Function


    Public Function IsFileFinnished(ByVal strFile As String) As Integer
        Dim blnCompare As Boolean = False
        Dim intQuantity As Integer = 0
        Dim intFinnished As Integer = 0
        Dim objReader As New StreamReader(strFile)
        Dim sLine As String
        Dim intStatus As Integer = -1
        Try

            Do
                sLine = objReader.ReadLine()

                If Not blnCompare Then
                    If Mid(sLine, 1, 4) = "----" Then
                        blnCompare = True
                    End If
                Else
                    'primerjam 
                    Dim aLine = Split(sLine, ";")
                    If IsNumeric(aLine(0)) Then

                        intQuantity = CInt(aLine(0))

                        If IsNumeric(aLine(4)) Then
                            intFinnished = CInt(aLine(4))
                        Else
                            intFinnished = 0
                        End If

                        If intFinnished = 0 Then
                            Select Case intStatus
                                Case -1
                                    intStatus = 0
                                Case 0
                                    intStatus = 0
                                Case 1
                                    intStatus = 1
                                Case 2
                                    intStatus = 1
                            End Select
                        ElseIf intFinnished > 0 And intFinnished < intQuantity Then
                            Select Case intStatus
                                Case -1
                                    intStatus = 1
                                Case 0
                                    intStatus = 1
                                Case 1
                                    intStatus = 1
                                Case 2
                                    intStatus = 1
                            End Select
                        ElseIf intFinnished > 0 And intFinnished >= intQuantity Then
                            Select Case intStatus
                                Case -1
                                    intStatus = 2
                                Case 0
                                    intStatus = 1
                                Case 1
                                    intStatus = 1
                                Case 2
                                    intStatus = 2
                            End Select

                        End If
                    End If
                End If
                If intStatus = 1 Then
                    objReader.Close()
                    Return intStatus
                End If
            Loop Until sLine Is Nothing

            objReader.Close()
            Return intStatus
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function ExecuteCutterEvent(ByVal cEvent As cls.event.EprodEvent, ByVal strKommissionNr As String, ByVal strOrderNr As String, connTools As SqlConnection) As String
        Dim strSourcePath = cls.Config.GetCutterSourcePath
        Dim strDestPath = cls.Config.GetCutterDestPath
        Dim aFileTypes() As String = Split(cls.Config.GetCutterFileTypes, ",")
        Dim strFileType As String = ""
        Dim strSourceFile As String = ""
        Dim strDestFile As String
        Dim strSQL As String = ""
        Dim blnInsert As Boolean = False
        Dim strNewOrderNr As String = ""

        Try
            If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                If Mid(strOrderNr, 1, 1) = "N" Then
                    strNewOrderNr = strOrderNr.Replace("N", "T")
                End If
                If Mid(strOrderNr, 1, 1) = "C" Then
                    strNewOrderNr = strOrderNr.Replace("C", "T")
                End If
                If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
            Else
                strNewOrderNr = strOrderNr
            End If


            For i = 0 To aFileTypes.Length - 1

                strFileType = aFileTypes(i).ToString
                strSourceFile = strSourcePath & "\" & strKommissionNr & "." & strFileType
                strDestFile = strDestPath & "\" & Path.GetFileName(strSourceFile)
                If Not FindEventInLog(cEvent.EventId, strKommissionNr, strOrderNr, SendToType.Manual, 0, 0, strSourceFile, connTools) Then

                    If File.Exists(strSourceFile) Then
                        'ga kopiram v dest_path
                        If Not File.Exists(strDestFile) Then
                            File.Move(strSourceFile, strDestFile)
                            'File.Delete(strSourceFile)
                            blnInsert = True
                            Call AddToActionLogSendedMails("OK   " & Mid(cEvent.EventDescription, 1, 42) & " Order Nr.: = " & strOrderNr & " Kommission = " & strKommissionNr & "Cutter File:" & strSourceFile)
                        Else
                            Call AddToActionLogSendedMails("FILE EXIST " & Mid(cEvent.EventDescription, 1, 42) & " Order Nr.: = " & strOrderNr & " Kommission = " & strKommissionNr & "Cutter File:" & strSourceFile)
                        End If
                    End If

                    strSourceFile = strSourcePath & "\" & strNewOrderNr & "." & strFileType
                    strDestFile = strDestPath & "\" & Path.GetFileName(strSourceFile)
                    If File.Exists(strSourceFile) Then
                        'ga kopiram v dest_path
                        If Not File.Exists(strDestFile) Then
                            File.Move(strSourceFile, strDestFile)
                            'File.Delete(strSourceFile)
                            blnInsert = True
                            Call AddToActionLogSendedMails("OK   " & Mid(cEvent.EventDescription, 1, 42) & " Order Nr.: = " & strOrderNr & " Kommission = " & strKommissionNr & "Cutter File:" & strSourceFile)
                        Else
                            Call AddToActionLogSendedMails("FILE EXIST " & Mid(cEvent.EventDescription, 1, 42) & " Order Nr.: = " & strOrderNr & " Kommission = " & strKommissionNr & "Cutter File:" & strSourceFile)
                        End If
                    End If
                Else 'že skopiran
                    Return "Dogodek je bil že izveden!"
                End If

            Next

            If blnInsert Then
                strSQL = "INSERT INTO eprod_events_log (kommision_nr, order_nr, event_id, event_date, event_datetime, recipient, event_type, subject_email) " _
                                & " VALUES (@kommision_nr, @order_nr, @event_id, @event_date, @event_datetime, @recipient, @event_type, @subject_email)"

                Using cmd As New SqlCommand(strSQL, connTools)
                    cmd.Parameters.AddWithValue("@kommision_nr", strKommissionNr)
                    cmd.Parameters.AddWithValue("@order_nr", strOrderNr)
                    cmd.Parameters.AddWithValue("@event_id", cEvent.EventId)
                    cmd.Parameters.AddWithValue("@event_date", Date.Now.Date)
                    cmd.Parameters.AddWithValue("@event_datetime", Date.Now)
                    cmd.Parameters.AddWithValue("@recipient", SendToType.Manual)
                    cmd.Parameters.AddWithValue("@event_type", 0)
                    cmd.Parameters.AddWithValue("@subject_email", "Cutter")
                    cmd.ExecuteNonQuery()
                End Using
            End If

            Return "OK"
        Catch ex As Exception
            Call AddToActionLogSendedMails("FAIL " & Mid(cEvent.EventDescription, 1, 42) & " Order Nr.: = " & strOrderNr & " Kommission = " & strKommissionNr & "Cutter File:" & strSourceFile)
            Return "ERROR"
        End Try

    End Function


    Public Function ExecuteCutterSendFile(ByVal strKommissionNr As String, ByVal strOrderNr As String, connTools As SqlConnection, connKBS As SqlConnection) As String
        Dim strSourcePath = cls.Config.GetCutterSourcePath
        Dim strDestPath = cls.Config.GetCutterDestPath
        Dim aFileTypes() As String = Split(cls.Config.GetCutterFileTypes, ",")
        Dim strFileType As String = ""
        Dim strSourceFile As String = ""
        Dim strDestFile As String
        Dim strSQL As String = ""
        Dim blnInsertKomm As Boolean = False
        Dim blnInsertOrder As Boolean = False
        Dim blnFindKomm As Boolean = False
        Dim blnFindOrder As Boolean = False
        Dim strLogReturn As String = ""

        Dim strNewOrderNr As String = ""
        Dim strLog As String = ""

        Try
            If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                If Mid(strOrderNr, 1, 1) = "N" Then
                    strNewOrderNr = strOrderNr.Replace("N", "T")
                End If
                If Mid(strOrderNr, 1, 1) = "C" Then
                    strNewOrderNr = strOrderNr.Replace("C", "T")
                End If
                If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
            Else
                strNewOrderNr = strOrderNr
            End If

            If CheckFilter(strKommissionNr, connKBS) Then 'pogledam, če vsebuje kakšno okno s sistemom NATURA

                For i = 0 To aFileTypes.Length - 1

                    strFileType = aFileTypes(i).ToString
                    'kommission
                    strSourceFile = strSourcePath & "\" & strKommissionNr & "." & strFileType
                    strDestFile = strDestPath & "\" & Path.GetFileName(strSourceFile)
                    If Not FindEventInLog(ManualEventId.CutterSendFile, strKommissionNr, strNewOrderNr, SendToType.Manual, 0, 0, strSourceFile, connTools) Then

                        If File.Exists(strSourceFile) Then
                            'ga kopiram v dest_path
                            If Not File.Exists(strDestFile) Then
                                File.Move(strSourceFile, strDestFile)
                                'File.Delete(strSourceFile)
                                blnInsertKomm = True
                                strLog = "OK   " & Mid("Cutter - Send File", 1, 42) & " Order Nr.: = " & strNewOrderNr & " Kommission = " & strKommissionNr & " Cutter File:" & strSourceFile
                                strLogReturn = strLogReturn & vbCrLf & strLog
                                Call AddToActionCutterSendFile(strLog)
                                strLog = ""
                            Else
                                strLog = "FILE EXIST " & Mid("Cutter - Send File", 1, 42) & " Order Nr.: = " & strNewOrderNr & " Kommission = " & strKommissionNr & " Cutter File:" & strSourceFile
                                strLogReturn = strLogReturn & vbCrLf & strLog
                                Call AddToActionCutterSendFile(strLog)
                                strLog = ""
                            End If
                        End If

                        If blnInsertKomm Then
                            strSQL = "INSERT INTO eprod_events_log (kommision_nr, order_nr, event_id, event_date, event_datetime, recipient, event_type, subject_email) " _
                                            & " VALUES (@kommision_nr, @order_nr, @event_id, @event_date, @event_datetime, @recipient, @event_type, @subject_email)"

                            Using cmd As New SqlCommand(strSQL, connTools)
                                cmd.Parameters.AddWithValue("@kommision_nr", strKommissionNr)
                                cmd.Parameters.AddWithValue("@order_nr", strNewOrderNr)
                                cmd.Parameters.AddWithValue("@event_id", ManualEventId.CutterSendFile)
                                cmd.Parameters.AddWithValue("@event_date", Date.Now.Date)
                                cmd.Parameters.AddWithValue("@event_datetime", Date.Now)
                                cmd.Parameters.AddWithValue("@recipient", SendToType.Manual)
                                cmd.Parameters.AddWithValue("@event_type", 0)
                                cmd.Parameters.AddWithValue("@subject_email", strSourceFile)
                                cmd.ExecuteNonQuery()
                            End Using
                        End If
                    Else
                        blnFindKomm = True
                        strLogReturn = strLogReturn & vbCrLf & "FILE " & strSourceFile & " already in events!"
                    End If


                    'order_nr
                    strSourceFile = strSourcePath & "\" & strNewOrderNr & "." & strFileType
                    strDestFile = strDestPath & "\" & Path.GetFileName(strSourceFile)
                    If Not FindEventInLog(ManualEventId.CutterSendFile, strKommissionNr, strNewOrderNr, SendToType.Manual, 0, 0, strSourceFile, connTools) Then
                        If File.Exists(strSourceFile) Then
                            'ga kopiram v dest_path
                            If Not File.Exists(strDestFile) Then
                                File.Move(strSourceFile, strDestFile)
                                'File.Delete(strSourceFile)
                                blnInsertOrder = True
                                strLog = "OK   " & Mid("Cutter - Send File", 1, 42) & " Order Nr.: = " & strNewOrderNr & " Kommission = " & strKommissionNr & " Cutter File:" & strSourceFile
                                strLogReturn = strLogReturn & vbCrLf & strLog
                                Call AddToActionCutterSendFile(strLog)
                                strLog = ""
                            Else
                                strLog = "FILE EXIST " & Mid("Cutter - Send File", 1, 42) & " Order Nr.: = " & strNewOrderNr & " Kommission = " & strKommissionNr & " Cutter File:" & strSourceFile
                                strLogReturn = strLogReturn & vbCrLf & strLog
                                Call AddToActionCutterSendFile(strLog)
                                strLog = ""
                            End If
                        End If

                        If blnInsertOrder Then
                            strSQL = "INSERT INTO eprod_events_log (kommision_nr, order_nr, event_id, event_date, event_datetime, recipient, event_type, subject_email) " _
                                            & " VALUES (@kommision_nr, @order_nr, @event_id, @event_date, @event_datetime, @recipient, @event_type, @subject_email)"

                            Using cmd As New SqlCommand(strSQL, connTools)
                                cmd.Parameters.AddWithValue("@kommision_nr", strKommissionNr)
                                cmd.Parameters.AddWithValue("@order_nr", strNewOrderNr)
                                cmd.Parameters.AddWithValue("@event_id", ManualEventId.CutterSendFile)
                                cmd.Parameters.AddWithValue("@event_date", Date.Now.Date)
                                cmd.Parameters.AddWithValue("@event_datetime", Date.Now)
                                cmd.Parameters.AddWithValue("@recipient", SendToType.Manual)
                                cmd.Parameters.AddWithValue("@event_type", 0)
                                cmd.Parameters.AddWithValue("@subject_email", strSourceFile)
                                cmd.ExecuteNonQuery()
                            End Using
                        End If

                        If Not blnInsertKomm And Not blnInsertOrder Then
                            'nekaj je narobe... ne dobi nobenega fajla
                            strLog = "NOT FOUND   " & Mid("Cutter - Send File", 1, 42) & " Order Nr.: = " & strNewOrderNr & " Kommission = " & strKommissionNr & " Cutter File:" & strSourceFile
                            strLogReturn = strLogReturn & vbCrLf & strLog
                        End If
                    Else
                        blnFindOrder = True
                        strLogReturn = strLogReturn & vbCrLf & "FILE " & strSourceFile & " already in events!"
                    End If



                Next 'drug tip
            Else
                strLogReturn = strLogReturn & vbCrLf & strKommissionNr & " - Filter (" & cls.Config.GetCutterFilter & ") not correspond to! "
            End If 'check filter


            Return strLogReturn

        Catch ex As Exception
            Call AddToActionLogSendedMails("FAIL " & Mid("Cutter - Send File", 1, 42) & " Order Nr.: = " & strNewOrderNr & " Kommission = " & strKommissionNr & "Cutter File:" & strSourceFile)
            Return "ERROR " & strOrderNr & " / " & strKommissionNr
        End Try

    End Function

    Private Function CheckFilter(ByVal strKomissionsNr As String, connkbs As SqlConnection) As Boolean
        Dim strFilter As String

        strFilter = cls.Config.GetCutterFilter

        If strFilter <> "" Then
            Dim strSQL As String = ""
            Dim dt As DataTable
            strSQL = "SELECT FAP_POS_WARENGRP, sum(FAP_POS_STK) as suma FROM FERTIGUNGSPOSITION " _
            & " WHERE FAP_FAUFTRAGNR = @strKomissionsNr " _
            & " AND fap_postype = 'konstruktion' " _
            & " AND substring(FAP_POS_WARENGRP,2,1) <> '-' " _
            & " AND FAP_POS_WARENGRP LIKE '%" & strFilter & "%' " _
            & " GROUP BY FAP_POS_WARENGRP " _
            & " ORDER BY suma DESC "

            Try
                Using cmd As New SqlCommand(strSQL, connkbs)
                    cmd.Parameters.AddWithValue("@strKomissionsNr", strKomissionsNr)
                    dt = GetData(cmd)
                    If dt.Rows.Count > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            Catch ex As Exception
                Return False
            End Try

        Else
            Return True
        End If

    End Function




    Private Function SendMail(ByVal intSendToType As SendToType, ByVal strRecipient As String, ByVal cEvent As cls.event.EprodEvent, ByVal intMailType As cls.event.EprodEvent.intMailType, ByVal strKommisionNr As String, ByVal strOrderNr As String, ByVal strSMTPServer As String, ByVal strSMTPPort As String, ByVal strEmailSender As String, ByVal strUsername As String, ByVal strPassword As String, ByVal dt As DataTable, ByVal dtMainOrder As DataTable, connTools As SqlConnection, connKBS As SqlConnection) As Boolean
        Dim strSQL As String = ""
        Dim strBody As String = ""
        Dim strSubject As String = ""
        Dim strLanguage As String = "SL"
        Dim Recipients As New List(Of String)
        Dim strRecipients() As String
        Dim strMessage As String = ""
        Dim FileAttachments As New List(Of String)

        Try

            If strEmailSender = "" Then
                Return False
            End If

            strRecipients = strRecipient.Split(";")


            strLanguage = GetLanguage(strOrderNr, connTools, connKBS).Trim
            GetMailText(cEvent, strLanguage, strSubject, strBody, connTools)
            'subject mora biti <> ""

            Dim dtEProd As DataTable = GetOrderDataEProd(strKommisionNr)

            If strSubject <> "" Then
                strSubject = ReplaceText(strSubject, strLanguage, dt, dtMainOrder, dtEProd, strOrderNr, cEvent, connTools, connKBS)
                strBody = ReplaceText(strBody, strLanguage, dt, dtMainOrder, dtEProd, strOrderNr, cEvent, connTools, connKBS)
            End If

            For j = 0 To strRecipients.Count - 1
                Recipients.Add(strRecipients(j))
            Next



            'poiščem vse zaznamke z <$...> - posebna polja


            Dim i1 As Long = 1
            Dim iNextStart As Long = 0
            Dim iNextEnd As Long = 0
            Dim strTag As String = ""
            Dim x As Integer = 1

            Do While True
                iNextStart = InStr(i1, strBody, "<$", CompareMethod.Text)
                If iNextStart = 0 Then Exit Do
                'poiščem naslednji znak >
                iNextEnd = InStr(iNextStart, strBody, ">", CompareMethod.Text)
                If iNextEnd > iNextStart Then
                    strTag = strBody.Substring(iNextStart - 1, iNextEnd - iNextStart + 1)
                    strBody = strBody.Replace(strTag, GetTagValue(strTag, strOrderNr, strKommisionNr, dt, cEvent.EventWorkstation))
                    x = x + 1
                End If
                i1 = iNextEnd + 1
            Loop

            i1 = 1
            iNextStart = 0
            iNextEnd = 0
            strTag = ""
            x = 1
            Do While True
                iNextStart = InStr(i1, strSubject, "<$", CompareMethod.Text)
                If iNextStart = 0 Then Exit Do
                'poiščem naslednji znak >
                iNextEnd = InStr(iNextStart, strSubject, ">", CompareMethod.Text)
                If iNextEnd > iNextStart Then
                    strTag = strSubject.Substring(iNextStart - 1, iNextEnd - iNextStart + 1)
                    strSubject = strSubject.Replace(strTag, GetTagValue(strTag, strOrderNr, strKommisionNr, dt, cEvent.EventWorkstation))
                    x = x + 1
                End If
                i1 = iNextEnd + 1
            Loop


            'poiščem vse zaznamke z <#...> - slike
            i1 = 1
            iNextStart = 0
            iNextEnd = 0
            strTag = ""
            x = 1

            Do While True
                iNextStart = InStr(i1, strBody, "<#", CompareMethod.Text)
                If iNextStart = 0 Then Exit Do
                'poiščem naslednji znak >
                iNextEnd = InStr(iNextStart, strBody, ">", CompareMethod.Text)
                If iNextEnd > iNextStart Then
                    strTag = strBody.Substring(iNextStart - 1, iNextEnd - iNextStart + 1)
                    FileAttachments.Add(strTag)
                    strBody = strBody.Replace(strTag, "<img src=""cid:pic" + x.ToString.Trim + """>")
                    x = x + 1
                End If
                i1 = iNextEnd + 1
            Loop

            If SendEmailMailbee(strRecipient, strEmailSender, strSubject, strBody, strUsername, strPassword, strMessage, FileAttachments, strSMTPServer, strSMTPPort) Then
                strSQL = "INSERT INTO eprod_events_log (kommision_nr, order_nr, event_id, event_date, event_datetime, recipient, event_type, recipient_email, subject_email) " _
                    & " VALUES (@kommision_nr, @order_nr, @event_id, @event_date, @event_datetime, @recipient, @event_type, @recipient_email, @subject_email)"

                Using cmd As New SqlCommand(strSQL, connTools)
                    cmd.Parameters.AddWithValue("@kommision_nr", strKommisionNr)
                    cmd.Parameters.AddWithValue("@order_nr", strOrderNr)
                    cmd.Parameters.AddWithValue("@event_id", cEvent.EventId)
                    cmd.Parameters.AddWithValue("@event_date", Date.Now.Date)
                    cmd.Parameters.AddWithValue("@event_datetime", Date.Now)
                    cmd.Parameters.AddWithValue("@recipient", intSendToType)
                    cmd.Parameters.AddWithValue("@event_type", 0)
                    cmd.Parameters.AddWithValue("@recipient_email", strRecipient)
                    cmd.Parameters.AddWithValue("@subject_email", strSubject)
                    cmd.ExecuteNonQuery()
                End Using
                Call AddToActionLogSendedMails("OK   " & Mid(cEvent.EventDescription, 1, 42) & " Order Nr.: = " & strOrderNr & " Kommission = " & strKommisionNr & " Recipient: " & strRecipient)
                Return True
            Else
                Call modLog.AddToActionLogSendMail(strMessage)
                Call AddToActionLogSendedMails("FAIL " & Mid(cEvent.EventDescription, 1, 42) & " Order Nr.: = " & strOrderNr & " Kommission = " & strKommisionNr & " Recipient: " & strRecipient)
                Return False
            End If

        Catch ex As Exception
            Call modLog.AddToActionLogSendMailTB(frmMainForm.txtLog, ex.ToString)
            Return False
        End Try


    End Function

    Private Function SendMailTask(ByVal intSendToType As SendToType, ByVal strRecipient As String, ByVal cEvent As cls.event.EprodEvent, ByVal intMailType As cls.event.EprodEvent.intMailType, ByVal strPartner As String, ByVal strTask As String, ByVal strSMTPServer As String, ByVal strSMTPPort As String, ByVal strEmailSender As String, ByVal strUsername As String, ByVal strPassword As String, ByVal dt As DataTable, connTools As SqlConnection, connKBS As SqlConnection, Optional lngUT_ID As Long = -1) As Boolean
        Dim strSQL As String = ""
        Dim strBody As String = ""
        Dim strSubject As String = ""
        Dim strLanguage As String = "SL"
        Dim Recipients As New List(Of String)
        Dim strRecipients() As String
        Dim strMessage As String = ""
        Dim FileAttachments As New List(Of String)

        Try

            If strEmailSender = "" Then
                Return False
            End If

            strRecipients = strRecipient.Split(";")


            strLanguage = GetLanguagePartner(strPartner, connTools, connKBS)
            GetMailText(cEvent, strLanguage, strSubject, strBody, connTools)
            'subject mora biti <> ""


            If strSubject <> "" Then
                strSubject = ReplaceText(strSubject, strLanguage, dt, Nothing, Nothing, "", cEvent, connTools, connKBS)
                strBody = ReplaceText(strBody, strLanguage, dt, Nothing, Nothing, "", cEvent, connTools, connKBS)
            End If

            For j = 0 To strRecipients.Count - 1
                Recipients.Add(strRecipients(j))
            Next



            'poiščem vse zaznamke z <$...> - posebna polja


            Dim i1 As Long = 1
            Dim iNextStart As Long = 0
            Dim iNextEnd As Long = 0
            Dim strTag As String = ""
            Dim x As Integer = 1

            Do While True
                iNextStart = InStr(i1, strBody, "<$", CompareMethod.Text)
                If iNextStart = 0 Then Exit Do
                'poiščem naslednji znak >
                iNextEnd = InStr(iNextStart, strBody, ">", CompareMethod.Text)
                If iNextEnd > iNextStart Then
                    strTag = strBody.Substring(iNextStart - 1, iNextEnd - iNextStart + 1)
                    strBody = strBody.Replace(strTag, GetTagValue(strTag, strTask, strPartner, dt, cEvent.EventWorkstation))
                    x = x + 1
                End If
                i1 = iNextEnd + 1
            Loop

            i1 = 1
            iNextStart = 0
            iNextEnd = 0
            strTag = ""
            x = 1
            Do While True
                iNextStart = InStr(i1, strSubject, "<$", CompareMethod.Text)
                If iNextStart = 0 Then Exit Do
                'poiščem naslednji znak >
                iNextEnd = InStr(iNextStart, strSubject, ">", CompareMethod.Text)
                If iNextEnd > iNextStart Then
                    strTag = strSubject.Substring(iNextStart - 1, iNextEnd - iNextStart + 1)
                    strSubject = strSubject.Replace(strTag, GetTagValue(strTag, strTask, strPartner, dt, cEvent.EventWorkstation))
                    x = x + 1
                End If
                i1 = iNextEnd + 1
            Loop


            'poiščem vse zaznamke z <#...> - slike
            i1 = 1
            iNextStart = 0
            iNextEnd = 0
            strTag = ""
            x = 1

            Do While True
                iNextStart = InStr(i1, strBody, "<#", CompareMethod.Text)
                If iNextStart = 0 Then Exit Do
                'poiščem naslednji znak >
                iNextEnd = InStr(iNextStart, strBody, ">", CompareMethod.Text)
                If iNextEnd > iNextStart Then
                    strTag = strBody.Substring(iNextStart - 1, iNextEnd - iNextStart + 1)
                    FileAttachments.Add(strTag)
                    strBody = strBody.Replace(strTag, "<img src=""cid:pic" + x.ToString.Trim + """>")
                    x = x + 1
                End If
                i1 = iNextEnd + 1
            Loop

            If SendEmailMailbee(strRecipient, strEmailSender, strSubject, strBody, strUsername, strPassword, strMessage, FileAttachments, strSMTPServer, strSMTPPort) Then
                strSQL = "INSERT INTO eprod_events_log (kommision_nr, order_nr, event_id, event_date, event_datetime, recipient, event_type, recipient_email, subject_email, ut_id) " _
                    & " VALUES (@kommision_nr, @order_nr, @event_id, @event_date, @event_datetime, @recipient, @event_type, @recipient_email, @subject_email, @ut_id)"

                Using cmd As New SqlCommand(strSQL, connTools)
                    cmd.Parameters.AddWithValue("@kommision_nr", strPartner)
                    cmd.Parameters.AddWithValue("@order_nr", strTask)
                    cmd.Parameters.AddWithValue("@event_id", cEvent.EventId)
                    cmd.Parameters.AddWithValue("@event_date", Date.Now.Date)
                    cmd.Parameters.AddWithValue("@event_datetime", Date.Now)
                    cmd.Parameters.AddWithValue("@recipient", intSendToType)
                    cmd.Parameters.AddWithValue("@event_type", 0)
                    cmd.Parameters.AddWithValue("@recipient_email", strRecipient)
                    cmd.Parameters.AddWithValue("@subject_email", strSubject)
                    If lngUT_ID <> -1 Then
                        cmd.Parameters.AddWithValue("@ut_id", lngUT_ID)
                    Else
                        cmd.Parameters.AddWithValue("@ut_id", DBNull.Value)
                    End If
                    cmd.ExecuteNonQuery()
                End Using
                Call AddToActionLogSendedMails("OK   " & Mid(cEvent.EventDescription, 1, 42) & " Partner.: = " & strPartner & " Task = " & strTask & " Recipient: " & strRecipient)
                Return True
            Else
                Call modLog.AddToActionLogSendMail(strMessage)
                Call AddToActionLogSendedMails("FAIL " & Mid(cEvent.EventDescription, 1, 42) & " Partner.: = " & strPartner & " Task = " & strTask & " Recipient: " & strRecipient)
                Return False
            End If

        Catch ex As Exception
            Call modLog.AddToActionLogSendMailTB(frmMainForm.txtLog, ex.ToString)
            Return False
        End Try


    End Function



    Private Function GetTagValue(ByVal strTag As String, ByVal strOrderNr As String, ByVal strKommissionsNr As String, ByVal dt As DataTable, ByVal intPlace As Integer) As String
        Dim strType As String = ""
        Dim strOperand As String = ""
        Dim strMultiplierType As String = ""
        Dim strMultiplier As String = ""
        Dim strStartDate As String = ""
        Dim dtmDate As Date = Now.Date
        Dim dtmDateReturn As Date = Nothing
        Dim intDays As Integer = 0
        Dim strFunction As String

        strType = Mid(strTag, 3, 2)

        '<$kw_now_+d014> - yyyy/tt
        '<$ex_name> - ime
        Select Case strType.ToUpper
            Case "KW" 'izpišem teden KW v obliki YYYY/TT
                strStartDate = Mid(strTag, 6, 3)
                Select Case strStartDate.ToUpper
                    Case "NOW"
                        dtmDate = Now.Date
                End Select

                strOperand = Mid(strTag, 10, 1)
                strMultiplierType = Mid(strTag, 11, 1)
                strMultiplier = Mid(strTag, 12, 3)
                If IsNumeric(strMultiplier) Then
                    Select Case strMultiplierType.ToUpper
                        Case "D"
                            dtmDateReturn = dtmDate.AddDays(Int(strMultiplier))
                        Case "W"
                            dtmDateReturn = dtmDate.AddDays(Int(strMultiplier) * 7)
                        Case "M"
                            dtmDateReturn = dtmDate.AddMonths(Int(strMultiplier))
                    End Select
                    'pretvorim ga še v KW
                    Return dtmDateReturn.Year.ToString & "/" & GetWeek(dtmDateReturn).ToString
                Else
                    Return "Error!"
                End If
            Case "EX"
                strFunction = Mid(strTag, 6, Len(strTag) - 6)
                Dim intAll As Integer = 0
                Dim intBooked As Integer = 0
                Dim intPct As Integer
                Select Case strFunction.ToUpper
                    Case "NAME"
                        Return Trim(dt(0)("gpx_anrede").ToString.Trim & " " & dt(0)("bsx_gp_vorname").ToString.Trim & " " & dt(0)("bsx_gp_name").ToString.Trim)
                    Case "NAME1"
                        Return Trim(dt(0)("gpx_anrede").ToString.Trim & " " & dt(0)("bsx_gp_name").ToString.Trim & " " & dt(0)("bsx_gp_vorname").ToString.Trim)
                    Case "NAME2"
                        If dt(0)("gpx_anrede").ToString.Trim <> "" Then
                            Return dt(0)("gpx_anrede").ToString.Trim()
                        Else
                            Return Trim(dt(0)("bsx_gp_name").ToString.Trim & " " & dt(0)("bsx_gp_vorname").ToString.Trim)
                        End If
                    Case "PCT_FIN_ORDER"
                        intAll = GeteProdOrderDetailsAll(strOrderNr, "", intPlace)
                        intBooked = GeteProdOrderDetailsBooked(strOrderNr, "", intPlace)
                        If intAll > 0 Then
                            intPct = Int((intBooked * 100) / intAll)
                            Return intPct.ToString & " %"
                        Else
                            Return "100 %"
                        End If
                    Case "PCT_FIN_KOMMISSION"
                        intAll = GeteProdOrderDetailsAll(strOrderNr, strKommissionsNr, intPlace)
                        intBooked = GeteProdOrderDetailsBooked(strOrderNr, strKommissionsNr, intPlace)
                        If intAll > 0 Then
                            intPct = Int((intBooked * 100) / intAll)
                            Return intPct.ToString & " %"
                        Else
                            Return "100 %"
                        End If
                    Case "GOOGLE_ID"
                        'poiščemo GoogleID v msora_order_info
                        Return GetGoogleId(strOrderNr)

                End Select
            Case Else
                Return ""
        End Select

        Return ""
    End Function


    Private Function GetWeek(ByVal dtmDate As Date) As Integer
        Return DatePart("ww", dtmDate)
    End Function
    Private Function GetOrderData(ByVal strDocNr As String, connKBS As SqlConnection) As DataTable
        Dim strSQL As String = ""
        Try



            'dobim osnovne podatke o naročilu
            strSQL = "SELECT bsx.*, tr.trans_slo as doc_type, " _
                                 & " usx.usx_fullname as Kreiral, usxp.usx_fullname as Tiskal, usxp1.usx_fullname as Prijavil, " _
                                 & " la.lan_kurzbez, la.lan_langbez, gp.*" _
                                 & " FROM vkbelegstub bsx " _
                                 & " LEFT JOIN gp ON bsx.bsx_gp_id = gp.gpx_id " _
                                 & " LEFT JOIN klaesuser usxp ON usxp.usx_id = bsx.bsx_iddruckender " _
                                 & " LEFT JOIN klaesuser usxp1 ON usxp1.usx_id = bsx.bsx_idmeldender " _
                                 & " LEFT JOIN [KlaesTools].dbo.trans_code tr ON tr.trans_id = bsx.bsx_ibelegart AND trans_group = 'BSX_IBELEGART' " _
                                 & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iderfasser " _
                                 & " LEFT JOIN land la ON la.lan_id = bsx.bsx_id_gpland " _
                                 & " WHERE bsx_belegnr = @strDocNr"

            Using cmd As New SqlCommand(strSQL, connKBS)
                cmd.Parameters.AddWithValue("@strDocNr", strDocNr)
                Dim dt As DataTable = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    Return dt
                Else
                    'pogledam še projekt 
                    strSQL = "SELECT TOP 1 bsx.*, tr.trans_slo as doc_type, " _
                                 & " usx.usx_fullname as Kreiral, usxp.usx_fullname as Tiskal, usxp1.usx_fullname as Prijavil, " _
                                 & " la.lan_kurzbez, la.lan_langbez, gp.*" _
                                 & " FROM vkbelegstub bsx " _
                                 & " LEFT JOIN gp ON bsx.bsx_gp_id = gp.gpx_id " _
                                 & " LEFT JOIN klaesuser usxp ON usxp.usx_id = bsx.bsx_iddruckender " _
                                 & " LEFT JOIN klaesuser usxp1 ON usxp1.usx_id = bsx.bsx_idmeldender " _
                                 & " LEFT JOIN [KlaesTools].dbo.trans_code tr ON tr.trans_id = bsx.bsx_ibelegart AND trans_group = 'BSX_IBELEGART' " _
                                 & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iderfasser " _
                                 & " LEFT JOIN land la ON la.lan_id = bsx.bsx_id_gpland " _
                                 & " WHERE bsx.bsx_vorgangnr = @strDocNr " _
                                 & " ORDER BY bsx_belegdatum DESC "

                    Using cmd1 As New SqlCommand(strSQL, connKBS)
                        cmd1.Parameters.AddWithValue("@strDocNr", strDocNr)
                        Dim dt1 As DataTable = GetData(cmd1)
                        If dt1.Rows.Count > 0 Then
                            Return dt1
                        End If
                    End Using

                End If

                Return Nothing

            End Using

        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try

    End Function
    Public Function OrderHasDelivery(ByVal strDocNr As String, ByRef strId As String, ByRef dtmDate As Date, connKBS As SqlConnection) As Boolean
        Dim strSQL As String = ""
        Try

            'dobim osnovne podatke o naročilu
            strSQL = "SELECT bsx_belegnr, bsx_belegdatum FROM vkbelegstub " _
                                 & " WHERE bsx_refauftragnr = @strDocNr AND bsx_ibelegart = 40 "

            Using cmd As New SqlCommand(strSQL, connKBS)
                cmd.Parameters.AddWithValue("@strDocNr", strDocNr)
                Dim dt As DataTable = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    strId = dt(0)("bsx_belegnr").ToString
                    dtmDate = dt(0)("bsx_belegdatum")
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            Throw ex
            Return False
        End Try

    End Function

    Public Function OrderHasMontage(ByVal strDocNr As String, intMontageDays As Integer, ByRef strMontagePerson As String, ByRef dtmMontageDate As Date, connTools As SqlConnection) As Boolean
        Dim strSQL As String = ""
        Try

            'dobim osnovne podatke o naročilu
            strSQL = "SELECT montage_person_id, montage_date FROM montage " _
                    & " WHERE main_order_nr = @strDocNr AND montage_date <= @dtmDate ORDER BY montage_date DESC"

            If Mid(strDocNr, 1, 1) = "T" Then
                strDocNr = strDocNr.Replace("T", "N")
            End If

            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@strDocNr", strDocNr)
                cmd.Parameters.AddWithValue("@dtmDate", DateAdd(DateInterval.Day, intMontageDays, Now.Date))
                Dim dt As DataTable = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    strMontagePerson = dt(0)("montage_person_id").ToString
                    dtmMontageDate = dt(0)("montage_date")
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            Throw ex
            Return False
        End Try

    End Function
    Private Function ReplaceText(ByVal strText As String, ByVal strLang As String, ByVal dt As DataTable, ByVal dtMainOrder As DataTable, ByVal dtEProd As DataTable, ByVal strOrderNr As String, ByVal cEvent As cls.event.EprodEvent, connTools As SqlConnection, connKBS As SqlConnection) As String
        Dim blnSkip As Boolean = False
        Try


            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then


                    For i = 0 To dt.Columns.Count - 1
                        Select Case dt.Columns(i).ColumnName.Trim.ToLower
                            Case "bsx_idruckstatus"
                                strText = strText.Replace("<?" & dt.Columns(i).ColumnName.Trim.ToLower & ">", TranslateDruckStatus(DB2IntZero(dt(0)(dt.Columns(i).ColumnName)), strLang))
                            Case "bsx_ibelegart"
                                strText = strText.Replace("<?" & dt.Columns(i).ColumnName.Trim.ToLower & ">", TranslateBelegArt(DB2IntZero(dt(0)(dt.Columns(i).ColumnName)), strLang))
                            Case Else
                                strText = strText.Replace("<?" & dt.Columns(i).ColumnName.Trim.ToLower & ">", dt(0)(dt.Columns(i).ColumnName).ToString.Trim)
                        End Select

                    Next
                End If
            Else
                blnSkip = True
            End If

            If Not dtEProd Is Nothing Then
                If dtEProd.Rows.Count > 0 Then
                    For i = 0 To dtEProd.Columns.Count - 1
                        Select Case dtEProd.Columns(i).ColumnName.Trim.ToLower
                            Case "lieferzeit"
                                strText = strText.Replace("<?" & dtEProd.Columns(i).ColumnName.Trim.ToLower & ">", cls.Utils.UnixTimeToDate(dtEProd(0)(dtEProd.Columns(i).ColumnName)))
                            Case "zeitvon"
                                strText = strText.Replace("<?" & dtEProd.Columns(i).ColumnName.Trim.ToLower & ">", cls.Utils.UnixTimeToDate(dtEProd(0)(dtEProd.Columns(i).ColumnName)).Date)
                            Case "zeitbis"
                                strText = strText.Replace("<?" & dtEProd.Columns(i).ColumnName.Trim.ToLower & ">", cls.Utils.UnixTimeToDate(dtEProd(0)(dtEProd.Columns(i).ColumnName)).Date)
                            Case Else
                                strText = strText.Replace("<?" & dtEProd.Columns(i).ColumnName.Trim.ToLower & ">", dtEProd(0)(dtEProd.Columns(i).ColumnName).ToString.Trim)
                        End Select

                    Next
                End If
            End If

            'strReturn = strReturn & "Komercialist = " & GetEmail(cls.event.EprodEvent.intMailType.MailToComercialist, strOrderNr) & vbCrLf
            'strReturn = strReturn & "Partner = " & GetEmail(cls.event.EprodEvent.intMailType.MailToPartner, strOrderNr) & vbCrLf
            'strReturn = strReturn & "Tehnolog = " & GetEmail(cls.event.EprodEvent.intMailType.MailToTechnology, strOrderNr) & vbCrLf

            'zamenjam še fiksne 
            If strText.Contains("<?email_partner>") Then
                strText = strText.Replace("<?email_partner>", GetEmail(cls.event.EprodEvent.intMailType.MailToPartner, strOrderNr, connTools, connKBS))
            End If

            If strText.Contains("<?email_comercial>") Then
                strText = strText.Replace("<?email_comercial>", GetEmail(cls.event.EprodEvent.intMailType.MailToComercialist, strOrderNr, connTools, connKBS))
            End If

            If strText.Contains("<?phone_comercial>") Then
                strText = strText.Replace("<?phone_comercial>", GetPhone(cls.event.EprodEvent.intMailType.MailToComercialist, strOrderNr, connKBS))
            End If

            If strText.Contains("<?mobile_comercial>") Then
                strText = strText.Replace("<?mobile_comercial>", GetMobile(cls.event.EprodEvent.intMailType.MailToComercialist, strOrderNr, connKBS))
            End If


            If strText.Contains("<?name_comercial>") Then
                strText = strText.Replace("<?name_comercial>", GetName(cls.event.EprodEvent.intMailType.MailToComercialist, strOrderNr, connKBS))
            End If

            If strText.Contains("<?email_technolog>") Then
                strText = strText.Replace("<?email_technolog>", GetEmail(cls.event.EprodEvent.intMailType.MailToTechnology, strOrderNr, connTools, connKBS))
            End If

            If strText.Contains("<?email_logistic>") Then
                strText = strText.Replace("<?email_logistic>", GetEmail(cls.event.EprodEvent.intMailType.MailToLogistic, strOrderNr, connTools, connKBS))
            End If

            If strText.Contains("<?summe_netto>") Then

                If Not dtMainOrder Is Nothing Then
                    strText = strText.Replace("<?summe_netto>", dtMainOrder(0)("bsx_summenetto").ToString)
                Else
                    If Not blnSkip Then
                        strText = strText.Replace("<?summe_netto>", dt(0)("bsx_summenetto").ToString)
                    Else
                        strText = strText.Replace("<?summe_netto>", "*")
                    End If
                End If
            End If

            If strText.Contains("<?delivery_failed>") Then
                strText = strText.Replace("<?delivery_failed>", GetNotConfirmedOrdersText(strOrderNr, cEvent, GetConnection("MAWI")))
            End If


            If strText.Contains("<?event_id>") Then
                strText = strText.Replace("<?event_id>", cEvent.EventId)
            End If
            Return strText
        Catch ex As Exception

            Return "Error " & strText
        End Try

        Return strText
    End Function

    Private Function GetNotConfirmedOrdersText(ByVal strOrder As String, ByVal cEvent As cls.event.EprodEvent, connMAWI As SqlConnection) As String
        Dim strParameter As String
        Dim dtOrders As DataTable = GetMawiOrders(strOrder, connMAWI)
        Dim blnIzpis As Boolean = False
        Dim strReturn As String = ""
        strParameter = cEvent.EventParameter

        '    NE brišem - tiste, ki nimajo potrjene dobave
        '    NE brišem - tiste, ki imajo dobavo potrjeno za več kot X dni


        If Not dtOrders Is Nothing Then
            For j = 0 To dtOrders.Rows.Count - 1
                blnIzpis = False
                If dtOrders(j)("datum_dobave") Is DBNull.Value Then
                    If dtOrders(j)("datum_potrjene_dobave") Is DBNull.Value Then
                        strReturn = strReturn & "Ni potrjena dobava - " & dtOrders(j)("pbname").ToString & vbTab & " Datum naročila: " & Format(dtOrders(j)("order_date").ToString, "Short date") & vbTab & "       Željen datum dobave: " & Format(dtOrders(j)("latest_delivery_date").ToString, "Short date") & vbCrLf
                    Else
                        'primerjam datuma
                        Dim dtmPotrjenaDobava As Date = CDate(dtOrders(j)("datum_potrjene_dobave").ToString)
                        If DateDiff(DateInterval.Day, Now.Date, dtmPotrjenaDobava) > CInt(strParameter) Then
                            strReturn = strReturn & "Dobava potrjena - " & dtOrders(j)("pbname").ToString & vbTab & Format(dtOrders(j)("datum_potrjene_dobave").ToString, "Short date") & vbTab & "      Željen datum dobave: " & Format(dtOrders(j)("latest_delivery_date").ToString, "Short date") & vbCrLf
                        End If
                    End If
                End If
            Next
        End If

        Return strReturn
    End Function

    Private Function TranslateDruckStatus(ByVal intStatus As Integer, ByVal strLang As String) As String
        Select Case intStatus
            Case 0
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "Ni tiskan"
                    Case "HR"
                        Return "Ni tiskan"
                    Case "EN"
                        Return "Not printed"
                    Case "IT"
                        Return "Not printed"
                    Case "FR"
                        Return "Not printed"
                    Case "DE"
                        Return "Nicht gedruckt"
                End Select
            Case 1
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "Tiskan"
                    Case "HR"
                        Return "Tiskan"
                    Case "EN"
                        Return "Printed"
                    Case "IT"
                        Return "Printed"
                    Case "FR"
                        Return "Printed"
                    Case "DE"
                        Return "Gedruckt"
                End Select
        End Select
        Return ""
    End Function

    Private Function TranslateBelegArt(ByVal intStatus As Integer, ByVal strLang As String) As String
        Select Case intStatus
            Case 10
                'VKBELEGART_10_ANGEBOT = 10
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "Ponudba"
                    Case "HR"
                        Return "Ponuda"
                    Case "EN"
                        Return "Offer"
                    Case "IT"
                        Return "Offer"
                    Case "FR"
                        Return "Offer"
                    Case "DE"
                        Return "Offer"
                End Select
            Case 15
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "Glavno naročilo"
                    Case "HR"
                        Return "Glavno naročilo"
                    Case "EN"
                        Return "Main order"
                    Case "IT"
                        Return "Main order"
                    Case "FR"
                        Return "Main order"
                    Case "DE"
                        Return "Hauptauftrag"
                End Select


            Case 20
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "Naročilo"
                    Case "HR"
                        Return "Naročilo"
                    Case "EN"
                        Return "Order"
                    Case "IT"
                        Return "Order"
                    Case "FR"
                        Return "Order"
                    Case "DE"
                        Return "Auftrag"
                End Select

            Case 30
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "VKBELEGART_30_BETRIEBSAUFTRAG = 30"
                    Case "HR"
                        Return "VKBELEGART_30_BETRIEBSAUFTRAG = 30"
                    Case "EN"
                        Return "VKBELEGART_30_BETRIEBSAUFTRAG = 30"
                    Case "IT"
                        Return "VKBELEGART_30_BETRIEBSAUFTRAG = 30"
                    Case "FR"
                        Return "VKBELEGART_30_BETRIEBSAUFTRAG = 30"
                    Case "DE"
                        Return "VKBELEGART_30_BETRIEBSAUFTRAG = 30"
                End Select

            Case 40
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "VKBELEGART_40_LIEFERSCHEIN = 40"
                    Case "HR"
                        Return "VKBELEGART_40_LIEFERSCHEIN = 40"
                    Case "EN"
                        Return "VKBELEGART_40_LIEFERSCHEIN = 40"
                    Case "IT"
                        Return "VKBELEGART_40_LIEFERSCHEIN = 40"
                    Case "FR"
                        Return "VKBELEGART_40_LIEFERSCHEIN = 40"
                    Case "DE"
                        Return "VKBELEGART_40_LIEFERSCHEIN = 40"
                End Select

            Case 70
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "VKBELEGART_70_RECHNUNG = 70"
                    Case "HR"
                        Return "VKBELEGART_70_RECHNUNG = 70"
                    Case "EN"
                        Return "VKBELEGART_70_RECHNUNG = 70"
                    Case "IT"
                        Return "VKBELEGART_70_RECHNUNG = 70"
                    Case "FR"
                        Return "VKBELEGART_70_RECHNUNG = 70"
                    Case "DE"
                        Return "VKBELEGART_70_RECHNUNG = 70"
                End Select

            Case 71
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "VKBELEGART_71_ABZAHLUNG = 71"
                    Case "HR"
                        Return "VKBELEGART_71_ABZAHLUNG = 71"
                    Case "EN"
                        Return "VKBELEGART_71_ABZAHLUNG = 71"
                    Case "IT"
                        Return "VKBELEGART_71_ABZAHLUNG = 71"
                    Case "FR"
                        Return "VKBELEGART_71_ABZAHLUNG = 71"
                    Case "DE"
                        Return "VKBELEGART_71_ABZAHLUNG = 71"
                End Select

            Case 72
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "VKBELEGART_72_TEILRECHNUNG = 72"
                    Case "HR"
                        Return "VKBELEGART_72_TEILRECHNUNG = 72"
                    Case "EN"
                        Return "VKBELEGART_72_TEILRECHNUNG = 72"
                    Case "IT"
                        Return "VKBELEGART_72_TEILRECHNUNG = 72"
                    Case "FR"
                        Return "VKBELEGART_72_TEILRECHNUNG = 72"
                    Case "DE"
                        Return "VKBELEGART_72_TEILRECHNUNG = 72"
                End Select

            Case 73
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "VKBELEGART_73_SCHRECHNUNG = 73"
                    Case "HR"
                        Return "VKBELEGART_73_SCHRECHNUNG = 73"
                    Case "EN"
                        Return "VKBELEGART_73_SCHRECHNUNG = 73"
                    Case "IT"
                        Return "VKBELEGART_73_SCHRECHNUNG = 73"
                    Case "FR"
                        Return "VKBELEGART_73_SCHRECHNUNG = 73"
                    Case "DE"
                        Return "VKBELEGART_73_SCHRECHNUNG = 73"
                End Select

            Case 80
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "VKBELEGART_80_GUTSCHRIFT = 80"
                    Case "HR"
                        Return "VKBELEGART_80_GUTSCHRIFT = 80"
                    Case "EN"
                        Return "VKBELEGART_80_GUTSCHRIFT = 80"
                    Case "IT"
                        Return "VKBELEGART_80_GUTSCHRIFT = 80"
                    Case "FR"
                        Return "VKBELEGART_80_GUTSCHRIFT = 80"
                    Case "DE"
                        Return "VKBELEGART_80_GUTSCHRIFT = 80"
                End Select

            Case 90
                Select Case strLang.ToUpper
                    Case "SL"
                        Return "VKBELEGART_7X_PAYMENTS = 90"
                    Case "HR"
                        Return "VKBELEGART_7X_PAYMENTS = 90"
                    Case "EN"
                        Return "VKBELEGART_7X_PAYMENTS = 90"
                    Case "IT"
                        Return "VKBELEGART_7X_PAYMENTS = 90"
                    Case "FR"
                        Return "VKBELEGART_7X_PAYMENTS = 90"
                    Case "DE"
                        Return "VKBELEGART_7X_PAYMENTS = 90"
                End Select

        End Select
        Return ""
    End Function


    Private Sub GetMailText(ByVal cEvent As cls.event.EprodEvent, ByVal strLanguage As String, ByRef strSubject As String, ByRef strBody As String, connTools As SqlConnection)
        Dim strSQL As String = ""
        Dim strTmp As String
        Try

            If connTools Is Nothing Then
                connTools = GetConnection("TOOLS")
            End If
            strSQL = "SELECT lang_" & strLanguage.Trim & " FROM text_template " _
                & " WHERE text_id = @text_id"

            Dim dt As DataTable = Nothing
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@text_id", cEvent.EventTextIdSubject)
                dt = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    'pretvorim v navadno besedilo
                    frmMainForm.rtbConvert.Rtf = dt(0)("lang_" & strLanguage).ToString

                    strSubject = frmMainForm.rtbConvert.Text
                End If
            End Using

            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@text_id", cEvent.EventTextIdBody)
                dt = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    'pretvorim v HTML
                    frmMainForm.rtbConvert.Rtf = dt(0)("lang_" & strLanguage).ToString
                    strTmp = ConvertToHTML(frmMainForm.rtbConvert)
                    strBody = strTmp
                End If
            End Using
        Catch ex As Exception
            strBody = strBody
            strSubject = "ERROR: Language: " & strLanguage & " Subject: " & strSubject & " Event ID: " & cEvent.EventId
        End Try

    End Sub
    Public Function IsValidDate(ByVal dtmDate As DateTime) As Boolean

        Try
            If dtmDate > CDate("01/01/1900") Then
                Return True
            End If
        Catch ex As Exception

            ' if a test date cannot be created, the
            ' method will return false
            Return False

        End Try

    End Function
    Public Function GetLanguage(ByVal strOrderNr As String, connTools As SqlConnection, connKBS As SqlConnection) As String
        Dim strSQL As String = ""
        Try

            strSQL = "SELECT la.lan_kurzbez FROM vkbelegstub vkb, land la " _
                & " WHERE vkb.bsx_id_gpland = la.lan_id " _
                & " AND vkb.bsx_belegnr = @strOrderNr"

            Using cmd As New SqlCommand(strSQL, connKBS)
                cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
                Dim dt As DataTable = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    strSQL = "SELECT language FROM country WHERE country = @strCountry"
                    Using cmd1 As New SqlCommand(strSQL, connTools)
                        cmd1.Parameters.AddWithValue("@strCountry", dt(0)("lan_kurzbez"))
                        Dim dtLang As DataTable = GetData(cmd1)
                        If dtLang.Rows.Count > 0 Then
                            Return dtLang(0)("language").ToString.ToUpper.Trim
                        End If
                    End Using
                End If
            End Using

            Return cls.Config.DefaultLang.ToUpper.Trim
        Catch ex As Exception
            Return cls.Config.DefaultLang.ToUpper.Trim
        End Try

    End Function

    Private Function GetLanguagePartner(ByVal strPartnerNr As String, connTools As SqlConnection, connKBS As SqlConnection) As String
        Dim strSQL As String = ""
        Try

            strSQL = "SELECT la.lan_kurzbez FROM gp, land la " _
                & " WHERE gp.gpx_land = la.lan_id " _
                & " AND gp.gpx_identnummer = @strPartnerNr"

            Using cmd As New SqlCommand(strSQL, connKBS)
                cmd.Parameters.AddWithValue("@strPartnerNr", strPartnerNr)
                Dim dt As DataTable = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    strSQL = "SELECT language FROM country WHERE country = @strCountry"
                    Using cmd1 As New SqlCommand(strSQL, connTools)
                        cmd1.Parameters.AddWithValue("@strCountry", dt(0)("lan_kurzbez"))
                        Dim dtLang As DataTable = GetData(cmd1)
                        If dtLang.Rows.Count > 0 Then
                            Return dtLang(0)("language").ToString.ToUpper.Trim
                        End If
                    End Using
                End If
            End Using

            Return cls.Config.DefaultLang.ToUpper.Trim
        Catch ex As Exception
            Return cls.Config.DefaultLang.ToUpper
        End Try

    End Function

    Private Function GetEmail(ByVal intEmailType As cls.event.EprodEvent.intMailType, ByVal strOrderNr As String, connTools As SqlConnection, connKBS As SqlConnection, Optional ByVal blnRedirectedMail As Boolean = True) As String
        Dim strEmail As String = ""
        Dim strSQL As String
        Dim dt As DataTable
        Dim strNewOrderNr As String = ""
        Dim strEmailTrade As String = ""
        Try



            Select Case intEmailType
                Case cls.event.EprodEvent.intMailType.MailToComercialist

                    If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                        If Mid(strOrderNr, 1, 1) = "T" Then
                            strNewOrderNr = strOrderNr.Replace("T", "N")
                        End If
                        If Mid(strOrderNr, 1, 1) = "C" Then
                            strNewOrderNr = strOrderNr.Replace("C", "N")
                        End If
                        If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                    Else
                        strNewOrderNr = strOrderNr
                    End If



                    If blnRedirectedMail Then
                        strEmail = GetRedirectedMail(strOrderNr, cls.event.EprodEvent.intMailType.MailToComercialist, connTools)
                    End If

                    If strEmail = "" Then
                        strSQL = "SELECT usx.usx_mail, bsx.bsx_gp_identnummer " _
                                     & " FROM vkbelegstub bsx " _
                                     & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iderfasser " _
                                     & " WHERE (bsx.bsx_belegnr = @strOrderNr OR bsx.bsx_vorgangnr = @strOrderNr)"

                        Using cmd As New SqlCommand(strSQL, connKBS)
                            cmd.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
                            dt = GetData(cmd)
                            If dt.Rows.Count > 0 Then
                                strEmail = dt(0)("usx_mail").ToString
                            Else
                                'pogledam še 6-mestni nalog N
                                strNewOrderNr = Mid(strNewOrderNr, 1, 7)

                                strSQL = "SELECT usx.usx_mail, bsx.bsx_gp_identnummer " _
                                     & " FROM vkbelegstub bsx " _
                                     & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iderfasser " _
                                     & " WHERE (bsx.bsx_belegnr = @strOrderNr OR bsx.bsx_vorgangnr = @strOrderNr) "

                                Using cmd2 As New SqlCommand(strSQL, connKBS)
                                    cmd2.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
                                    dt = GetData(cmd2)
                                    If dt.Rows.Count > 0 Then
                                        strEmail = dt(0)("usx_mail").ToString
                                    Else
                                        strEmail = ""
                                    End If
                                End Using
                            End If

                        End Using

                    End If

                    Return strEmail


                Case cls.event.EprodEvent.intMailType.MailToDeveloper

                    If blnRedirectedMail Then
                        strEmail = GetRedirectedMail(strOrderNr, cls.event.EprodEvent.intMailType.MailToDeveloper, connTools)
                    End If

                    If strEmail = "" Then
                        strEmail = cls.Config.GetDeveloperMail
                    End If

                    Return strEmail

                Case cls.event.EprodEvent.intMailType.MailToDirector

                    If blnRedirectedMail Then
                        strEmail = GetRedirectedMail(strOrderNr, cls.event.EprodEvent.intMailType.MailToDirector, connTools)
                    End If

                    If strEmail = "" Then
                        strEmail = cls.Config.GetDirectorMail
                    End If

                    Return strEmail

                Case cls.event.EprodEvent.intMailType.MailToLogistic

                    If blnRedirectedMail Then
                        strEmail = GetRedirectedMail(strOrderNr, cls.event.EprodEvent.intMailType.MailToLogistic, connTools)
                    End If

                    If strEmail = "" Then
                        strEmail = cls.Config.GetLogisticMail
                    End If

                    Return strEmail

                Case cls.event.EprodEvent.intMailType.MailToPartner
                    If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                        If Mid(strOrderNr, 1, 1) = "T" Then
                            strNewOrderNr = strOrderNr.Replace("T", "N")
                        End If
                        If Mid(strOrderNr, 1, 1) = "C" Then
                            strNewOrderNr = strOrderNr.Replace("C", "N")
                        End If
                        If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                    Else
                        strNewOrderNr = strOrderNr
                    End If

                    If blnRedirectedMail Then
                        strEmail = GetRedirectedMail(strOrderNr, cls.event.EprodEvent.intMailType.MailToPartner, connTools)
                    End If

                    If strEmail = "" Then
                        strSQL = "SELECT it.itkontakt_wert, bsx.bsx_gp_identnummer, bsx_15_text_vezanalogtrade " _
                                     & " FROM vkbelegstub bsx, gp, " _
                                     & " itkontakt it " _
                                     & " WHERE (bsx.bsx_belegnr = @strOrderNr OR bsx.bsx_vorgangnr = @strOrderNr) AND it.itkontakt_idgp = gp.gpx_id AND bsx.bsx_gp_id = gp.gpx_id AND it.itkontakt_iverbindungsart = 4 " _
                                     & " ORDER BY it.itkontakt_rang "

                        Using cmd As New SqlCommand(strSQL, connKBS)
                            cmd.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
                            dt = GetData(cmd)
                            If dt.Rows.Count > 0 Then
                                strEmail = dt(0)("itkontakt_wert").ToString

                                If cls.Config.SendToTrade Then
                                    If dt(0)("bsx_gp_identnummer").ToString = "m sora fenster gmbh" Then
                                        strEmailTrade = GetEmailFenster(strNewOrderNr, dt(0)("bsx_15_text_vezanalogtrade").ToString.Trim)
                                        If strEmailTrade <> "" Then
                                            strEmail = strEmail & "; " & strEmailTrade
                                        End If
                                    End If

                                    If dt(0)("bsx_gp_identnummer").ToString = "m sora finestre srl" Then
                                        strEmailTrade = GetEmailFinestre(strNewOrderNr, dt(0)("bsx_15_text_vezanalogtrade").ToString.Trim)
                                        If strEmailTrade <> "" Then
                                            strEmail = strEmail & "; " & strEmailTrade
                                        End If
                                    End If

                                End If

                            Else
                                strEmail = ""
                            End If

                        End Using
                    End If

                    Return strEmail

                Case cls.event.EprodEvent.intMailType.MailToProduction

                    If blnRedirectedMail Then
                        strEmail = GetRedirectedMail(strOrderNr, cls.event.EprodEvent.intMailType.MailToProduction, connTools)
                    End If

                    If strEmail = "" Then
                        strEmail = cls.Config.GetProductionMail
                    End If

                    Return strEmail

                Case cls.event.EprodEvent.intMailType.MailToTechnology

                    If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                        If Mid(strOrderNr, 1, 1) = "T" Then
                            strNewOrderNr = strOrderNr.Replace("T", "N")
                        End If
                        If Mid(strOrderNr, 1, 1) = "C" Then
                            strNewOrderNr = strOrderNr.Replace("C", "N")
                        End If
                        If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                    Else
                        strNewOrderNr = strOrderNr
                    End If

                    If blnRedirectedMail Then
                        strEmail = GetRedirectedMail(strOrderNr, cls.event.EprodEvent.intMailType.MailToTechnology, connTools)
                    End If

                    If strEmail = "" Then
                        strSQL = "SELECT usx.usx_mail " _
                                     & " FROM vkbelegstub bsx " _
                                     & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iddruckender " _
                                     & " WHERE (bsx.bsx_belegnr = @strOrderNr OR bsx.bsx_vorgangnr = @strOrderNr)"

                        Using cmd As New SqlCommand(strSQL, connKBS)
                            cmd.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
                            dt = GetData(cmd)
                            If dt.Rows.Count > 0 Then
                                strEmail = dt(0)("usx_mail").ToString
                            Else
                                'pogledam še 6-mestni nalog N
                                strNewOrderNr = Mid(strNewOrderNr, 1, 7)

                                strSQL = "SELECT usx.usx_mail, bsx.bsx_gp_identnummer " _
                                     & " FROM vkbelegstub bsx " _
                                     & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iderfasser " _
                                     & " WHERE (bsx.bsx_belegnr = @strOrderNr OR bsx.bsx_vorgangnr = @strOrderNr"

                                Using cmd2 As New SqlCommand(strSQL, connKBS)
                                    cmd2.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
                                    dt = GetData(cmd2)
                                    If dt.Rows.Count > 0 Then
                                        strEmail = dt(0)("usx_mail").ToString
                                    Else
                                        strEmail = ""
                                    End If
                                End Using
                            End If
                        End Using

                    End If

                    Return strEmail
            End Select

            Return strEmail

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Function GetEmailPartner(ByVal strPartner As String, connKBS As SqlConnection) As String
        Dim strEmail As String = ""
        Dim strSQL As String
        Dim dt As DataTable
        Try



            strSQL = "SELECT it.itkontakt_wert " _
                             & " FROM gp, " _
                             & " itkontakt it " _
                             & " WHERE it.itkontakt_idgp = gp.gpx_id AND it.itkontakt_iverbindungsart = 4 AND gp.gpx_identnummer = @strPartner"

            Using cmd As New SqlCommand(strSQL, connKBS)
                cmd.Parameters.AddWithValue("@strPartner", strPartner)
                dt = GetData(cmd)

                If dt.Rows.Count > 0 Then
                    strEmail = dt(0)("itkontakt_wert").ToString
                Else
                    strEmail = ""
                End If

            End Using

            Return strEmail

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Function GetRedirectedMail(ByVal strOrderNr As String, ByVal intRecipient As cls.event.EprodEvent.intMailType, connTools As SqlConnection) As String
        'strordernr pretvorim v T
        Dim strNewOrderNr As String = ""
        Dim strSQL As String
        Dim strRedirectedMail As String = ""

        If IsNumeric(Mid(strOrderNr, 2, 2)) Then
            If Mid(strOrderNr, 1, 1) = "N" Then
                strNewOrderNr = strOrderNr.Replace("N", "T")
            End If
            If Mid(strOrderNr, 1, 1) = "C" Then
                strNewOrderNr = strOrderNr.Replace("C", "T")
            End If
            If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
        Else
            strNewOrderNr = strOrderNr
        End If

        'nalog T
        strSQL = "SELECT * FROM email_redirect WHERE order_nr = @strOrderNrT AND recipient = @intRecipient "
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strOrderNrT", strNewOrderNr)
            cmd.Parameters.AddWithValue("@intRecipient", intRecipient)
            Dim dt As DataTable = GetData(cmd)
            If dt.Rows.Count = 0 Then
                strRedirectedMail = ""
            Else
                strRedirectedMail = dt(0)("email").ToString.Trim
            End If

        End Using

        If strRedirectedMail = "" Then ' pogledam še za nalog  N
            If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                If Mid(strOrderNr, 1, 1) = "T" Then
                    strNewOrderNr = strOrderNr.Replace("T", "N")
                End If
                If Mid(strOrderNr, 1, 1) = "C" Then
                    strNewOrderNr = strOrderNr.Replace("C", "N")
                End If
                If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
            Else
                strNewOrderNr = strOrderNr
            End If

            strSQL = "SELECT * FROM email_redirect WHERE order_nr = @strOrderNrT AND recipient = @intRecipient "
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@strOrderNrT", strNewOrderNr)
                cmd.Parameters.AddWithValue("@intRecipient", intRecipient)
                Dim dt As DataTable = GetData(cmd)
                If dt.Rows.Count = 0 Then
                    strRedirectedMail = ""
                Else
                    strRedirectedMail = dt(0)("email").ToString.Trim
                End If

            End Using

        End If

        Return strRedirectedMail
    End Function

    Private Function GetPhone(ByVal intEmailType As cls.event.EprodEvent.intMailType, ByVal strOrderNr As String, connKBS As SqlConnection) As String
        Dim strEmail As String = ""
        Dim strSQL As String
        Dim dt As DataTable
        Dim strNewOrderNr As String = ""
        Dim strEmailTrade As String = ""
        Try

            Select Case intEmailType
                Case cls.event.EprodEvent.intMailType.MailToComercialist

                    If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                        If Mid(strOrderNr, 1, 1) = "T" Then
                            strNewOrderNr = strOrderNr.Replace("T", "N")
                        End If
                        If Mid(strOrderNr, 1, 1) = "C" Then
                            strNewOrderNr = strOrderNr.Replace("C", "N")
                        End If
                        If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                    Else
                        strNewOrderNr = strOrderNr
                    End If

                    strSQL = "SELECT usx.usx_phone, bsx.bsx_gp_identnummer " _
                                 & " FROM vkbelegstub bsx " _
                                 & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iderfasser " _
                                 & " WHERE bsx_belegnr = @strDocNr"

                    Using cmd As New SqlCommand(strSQL, connKBS)
                        cmd.Parameters.AddWithValue("@strDocNr", strNewOrderNr)
                        dt = GetData(cmd)
                        If dt.Rows.Count > 0 Then
                            strEmail = dt(0)("usx_phone").ToString
                        Else
                            strEmail = ""
                        End If


                        Return strEmail
                    End Using




                Case cls.event.EprodEvent.intMailType.MailToDeveloper
                    Return cls.Config.GetDeveloperMail
                Case cls.event.EprodEvent.intMailType.MailToDirector
                    Return cls.Config.GetDirectorMail
                Case cls.event.EprodEvent.intMailType.MailToLogistic
                    Return cls.Config.GetLogisticMail
                Case cls.event.EprodEvent.intMailType.MailToPartner
                    If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                        If Mid(strOrderNr, 1, 1) = "T" Then
                            strNewOrderNr = strOrderNr.Replace("T", "N")
                        End If
                        If Mid(strOrderNr, 1, 1) = "C" Then
                            strNewOrderNr = strOrderNr.Replace("C", "N")
                        End If
                        If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                    Else
                        strNewOrderNr = strOrderNr
                    End If


                    strSQL = "SELECT it.itkontakt_wert, bsx.bsx_gp_identnummer, bsx_15_text_vezanalogtrade " _
                                 & " FROM vkbelegstub bsx, gp, " _
                                 & " itkontakt it " _
                                 & " WHERE bsx.bsx_belegnr = @strOrderNr AND it.itkontakt_idgp = gp.gpx_id AND bsx.bsx_gp_id = gp.gpx_id AND it.itkontakt_iverbindungsart IN (1,3) "

                    Using cmd As New SqlCommand(strSQL, connKBS)
                        cmd.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
                        dt = GetData(cmd)
                        If dt.Rows.Count > 0 Then
                            strEmail = ""
                            For i = 0 To dt.Rows.Count - 1
                                strEmail = strEmail & dt(0)("itkontakt_wert").ToString & "; "
                            Next

                            If dt(0)("bsx_gp_identnummer").ToString = "m sora fenster gmbh" Then
                                strEmailTrade = GetPhoneFenster(strNewOrderNr, dt(0)("bsx_15_text_vezanalogtrade").ToString.Trim)
                                If strEmailTrade <> "" Then
                                    strEmail = strEmail & "; " & strEmailTrade
                                End If
                            End If

                            If dt(0)("bsx_gp_identnummer").ToString = "m sora finestre srl" Then
                                strEmailTrade = GetPhoneFinestre(strNewOrderNr, dt(0)("bsx_15_text_vezanalogtrade").ToString.Trim)
                                If strEmailTrade <> "" Then
                                    strEmail = strEmail & "; " & strEmailTrade
                                End If
                            End If
                        Else
                            strEmail = ""
                        End If

                        Return strEmail

                    End Using
                Case cls.event.EprodEvent.intMailType.MailToProduction
                    Return cls.Config.GetProductionMail
                Case cls.event.EprodEvent.intMailType.MailToTechnology
                    If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                        If Mid(strOrderNr, 1, 1) = "T" Then
                            strNewOrderNr = strOrderNr.Replace("T", "N")
                        End If
                        If Mid(strOrderNr, 1, 1) = "C" Then
                            strNewOrderNr = strOrderNr.Replace("C", "N")
                        End If
                        If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                    Else
                        strNewOrderNr = strOrderNr
                    End If

                    strSQL = "SELECT usx.usx_phone " _
                                 & " FROM vkbelegstub bsx " _
                                 & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iddruckender " _
                                 & " WHERE bsx_belegnr = @strOrderNr"

                    Using cmd As New SqlCommand(strSQL, connKBS)
                        cmd.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
                        dt = GetData(cmd)
                        If dt.Rows.Count > 0 Then
                            Return dt(0)("usx_phone").ToString
                        Else
                            Return ""
                        End If
                    End Using
            End Select
            Return strEmail

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Function GetMobile(ByVal intEmailType As cls.event.EprodEvent.intMailType, ByVal strOrderNr As String, connKBS As SqlConnection) As String
        Dim strEmail As String = ""
        Dim strSQL As String
        Dim dt As DataTable
        Dim strNewOrderNr As String = ""
        Dim strEmailTrade As String = ""
        Try

            Select Case intEmailType
                Case cls.event.EprodEvent.intMailType.MailToComercialist

                    If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                        If Mid(strOrderNr, 1, 1) = "T" Then
                            strNewOrderNr = strOrderNr.Replace("T", "N")
                        End If
                        If Mid(strOrderNr, 1, 1) = "C" Then
                            strNewOrderNr = strOrderNr.Replace("C", "N")
                        End If
                        If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                    Else
                        strNewOrderNr = strOrderNr
                    End If

                    strSQL = "SELECT usx.usx_mobile, bsx.bsx_gp_identnummer " _
                                 & " FROM vkbelegstub bsx " _
                                 & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iderfasser " _
                                 & " WHERE bsx_belegnr = @strDocNr"

                    Using cmd As New SqlCommand(strSQL, connKBS)
                        cmd.Parameters.AddWithValue("@strDocNr", strNewOrderNr)
                        dt = GetData(cmd)
                        If dt.Rows.Count > 0 Then
                            strEmail = dt(0)("usx_mobile").ToString
                        Else
                            strEmail = ""
                        End If


                        Return strEmail
                    End Using




                Case cls.event.EprodEvent.intMailType.MailToDeveloper
                    Return cls.Config.GetDeveloperMail
                Case cls.event.EprodEvent.intMailType.MailToDirector
                    Return cls.Config.GetDirectorMail
                Case cls.event.EprodEvent.intMailType.MailToLogistic
                    Return cls.Config.GetLogisticMail
                Case cls.event.EprodEvent.intMailType.MailToPartner
                    If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                        If Mid(strOrderNr, 1, 1) = "T" Then
                            strNewOrderNr = strOrderNr.Replace("T", "N")
                        End If
                        If Mid(strOrderNr, 1, 1) = "C" Then
                            strNewOrderNr = strOrderNr.Replace("C", "N")
                        End If
                        If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                    Else
                        strNewOrderNr = strOrderNr
                    End If


                    strSQL = "SELECT it.itkontakt_wert, bsx.bsx_gp_identnummer, bsx_15_text_vezanalogtrade " _
                                 & " FROM vkbelegstub bsx, gp, " _
                                 & " itkontakt it " _
                                 & " WHERE bsx.bsx_belegnr = @strOrderNr AND it.itkontakt_idgp = gp.gpx_id AND bsx.bsx_gp_id = gp.gpx_id AND it.itkontakt_iverbindungsart IN (1,3) "

                    Using cmd As New SqlCommand(strSQL, connKBS)
                        cmd.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
                        dt = GetData(cmd)
                        If dt.Rows.Count > 0 Then
                            strEmail = ""
                            For i = 0 To dt.Rows.Count - 1
                                strEmail = strEmail & dt(0)("itkontakt_wert").ToString & "; "
                            Next

                            If dt(0)("bsx_gp_identnummer").ToString = "m sora fenster gmbh" Then
                                strEmailTrade = GetPhoneFenster(strNewOrderNr, dt(0)("bsx_15_text_vezanalogtrade").ToString.Trim)
                                If strEmailTrade <> "" Then
                                    strEmail = strEmail & "; " & strEmailTrade
                                End If
                            End If

                            If dt(0)("bsx_gp_identnummer").ToString = "m sora finestre srl" Then
                                strEmailTrade = GetPhoneFinestre(strNewOrderNr, dt(0)("bsx_15_text_vezanalogtrade").ToString.Trim)
                                If strEmailTrade <> "" Then
                                    strEmail = strEmail & "; " & strEmailTrade
                                End If
                            End If
                        Else
                            strEmail = ""
                        End If

                        Return strEmail

                    End Using
                Case cls.event.EprodEvent.intMailType.MailToProduction
                    Return cls.Config.GetProductionMail
                Case cls.event.EprodEvent.intMailType.MailToTechnology
                    If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                        If Mid(strOrderNr, 1, 1) = "T" Then
                            strNewOrderNr = strOrderNr.Replace("T", "N")
                        End If
                        If Mid(strOrderNr, 1, 1) = "C" Then
                            strNewOrderNr = strOrderNr.Replace("C", "N")
                        End If
                        If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                    Else
                        strNewOrderNr = strOrderNr
                    End If

                    strSQL = "SELECT usx.usx_mobile " _
                                 & " FROM vkbelegstub bsx " _
                                 & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iddruckender " _
                                 & " WHERE bsx_belegnr = @strOrderNr"

                    Using cmd As New SqlCommand(strSQL, connKBS)
                        cmd.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
                        dt = GetData(cmd)
                        If dt.Rows.Count > 0 Then
                            Return dt(0)("usx_mobile").ToString
                        Else
                            Return ""
                        End If
                    End Using
            End Select
            Return strEmail

        Catch ex As Exception
            Return ""
        End Try

    End Function


    Private Function GetName(ByVal intEmailType As cls.event.EprodEvent.intMailType, ByVal strOrderNr As String, connKBS As SqlConnection) As String
        Dim strEmail As String = ""
        Dim strSQL As String
        Dim dt As DataTable
        Dim strNewOrderNr As String = ""
        Dim strEmailTrade As String = ""
        Try

            Select Case intEmailType
                Case cls.event.EprodEvent.intMailType.MailToComercialist

                    If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                        If Mid(strOrderNr, 1, 1) = "T" Then
                            strNewOrderNr = strOrderNr.Replace("T", "N")
                        End If
                        If Mid(strOrderNr, 1, 1) = "C" Then
                            strNewOrderNr = strOrderNr.Replace("C", "N")
                        End If
                        If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                    Else
                        strNewOrderNr = strOrderNr
                    End If

                    strSQL = "SELECT usx.usx_fullname, bsx.bsx_gp_identnummer " _
                                 & " FROM vkbelegstub bsx " _
                                 & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iderfasser " _
                                 & " WHERE bsx_belegnr = @strDocNr"

                    Using cmd As New SqlCommand(strSQL, connKBS)
                        cmd.Parameters.AddWithValue("@strDocNr", strNewOrderNr)
                        dt = GetData(cmd)
                        If dt.Rows.Count > 0 Then
                            strEmail = dt(0)("usx_fullname").ToString
                        Else
                            strEmail = ""
                        End If


                        Return strEmail
                    End Using

                Case cls.event.EprodEvent.intMailType.MailToTechnology
                    If IsNumeric(Mid(strOrderNr, 2, 2)) Then
                        If Mid(strOrderNr, 1, 1) = "T" Then
                            strNewOrderNr = strOrderNr.Replace("T", "N")
                        End If
                        If Mid(strOrderNr, 1, 1) = "C" Then
                            strNewOrderNr = strOrderNr.Replace("C", "N")
                        End If
                        If strNewOrderNr = "" Then strNewOrderNr = strOrderNr
                    Else
                        strNewOrderNr = strOrderNr
                    End If

                    strSQL = "SELECT usx.usx_fullname " _
                                 & " FROM vkbelegstub bsx " _
                                 & " LEFT JOIN klaesuser usx ON usx.usx_id = bsx.bsx_iddruckender " _
                                 & " WHERE bsx_belegnr = @strOrderNr"

                    Using cmd As New SqlCommand(strSQL, connKBS)
                        cmd.Parameters.AddWithValue("@strOrderNr", strNewOrderNr)
                        dt = GetData(cmd)
                        If dt.Rows.Count > 0 Then
                            Return dt(0)("usx_fullname").ToString
                        Else
                            Return ""
                        End If
                    End Using
            End Select
            Return strEmail

        Catch ex As Exception
            Return ""
        End Try

    End Function
    Private Function GetEmailFenster(ByVal strOrderNr As String, ByVal strVezaNalog As String) As String
        Dim strSQL As String = ""
        Dim strOrder As String
        Dim dt As DataTable = Nothing

        If strVezaNalog <> "" Then
            strOrder = strVezaNalog
        Else
            strOrder = strOrderNr
        End If

        If strVezaNalog <> "" Then
            strSQL = "SELECT it.itkontakt_wert, bsx.bsx_gp_identnummer " _
                                     & " FROM vkbelegstub bsx, gp, " _
                                     & " itkontakt it " _
                                     & " WHERE bsx.bsx_belegnr = @strOrder AND it.itkontakt_idgp = gp.gpx_id AND bsx.bsx_gp_id = gp.gpx_id AND it.itkontakt_iverbindungsart = 4 " _
                                     & " ORDER BY it.itkontakt_rang "

            Using cmd As New SqlCommand(strSQL, GetConnection("KLAESFENSTER"))
                cmd.Parameters.AddWithValue("@strOrder", strOrder)
                dt = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    Return dt(0)("itkontakt_wert").ToString
                Else
                    Return ""
                End If

            End Using
        End If
        Return ""
    End Function

    Private Function GetEmailFinestre(ByVal strOrderNr As String, ByVal strVezaNalog As String) As String
        Dim strSQL As String = ""
        Dim strOrder As String
        Dim dt As DataTable = Nothing

        If strVezaNalog <> "" Then
            strOrder = strVezaNalog
        Else
            strOrder = strOrderNr
        End If

        If strVezaNalog <> "" Then
            strSQL = "SELECT it.itkontakt_wert, bsx.bsx_gp_identnummer " _
                                     & " FROM vkbelegstub bsx, gp, " _
                                     & " itkontakt it " _
                                     & " WHERE bsx.bsx_belegnr = @strOrder AND it.itkontakt_idgp = gp.gpx_id AND bsx.bsx_gp_id = gp.gpx_id AND it.itkontakt_iverbindungsart = 4 " _
                                     & " ORDER BY it.itkontakt_rang "

            Using cmd As New SqlCommand(strSQL, GetConnection("KLAESFINESTRE"))
                cmd.Parameters.AddWithValue("@strOrder", strOrder)
                dt = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    Return dt(0)("itkontakt_wert").ToString
                Else
                    Return ""
                End If

            End Using
        End If
        Return ""
    End Function

    Private Function GetPhoneFenster(ByVal strOrderNr As String, ByVal strVezaNalog As String) As String
        Dim strSQL As String = ""
        Dim strOrder As String
        Dim dt As DataTable = Nothing

        If strVezaNalog <> "" Then
            strOrder = strVezaNalog
        Else
            strOrder = strOrderNr
        End If

        If strVezaNalog <> "" Then
            strSQL = "SELECT it.itkontakt_wert, bsx.bsx_gp_identnummer " _
                                     & " FROM vkbelegstub bsx, gp, " _
                                     & " itkontakt it " _
                                     & " WHERE bsx.bsx_belegnr = @strOrder AND it.itkontakt_idgp = gp.gpx_id AND bsx.bsx_gp_id = gp.gpx_id AND it.itkontakt_iverbindungsart IN (1,3) "

            Using cmd As New SqlCommand(strSQL, GetConnection("KLAESFENSTER"))
                cmd.Parameters.AddWithValue("@strOrder", strOrder)
                dt = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    Dim strReturn As String = ""
                    For i = 0 To dt.Rows.Count - 1
                        strReturn = strReturn & dt(0)("itkontakt_wert").ToString & "; "
                    Next
                    Return strReturn
                Else
                    Return ""
                End If

            End Using
        End If
        Return ""
    End Function

    Private Function GetPhoneFinestre(ByVal strOrderNr As String, ByVal strVezaNalog As String) As String
        Dim strSQL As String = ""
        Dim strOrder As String
        Dim dt As DataTable = Nothing

        If strVezaNalog <> "" Then
            strOrder = strVezaNalog
        Else
            strOrder = strOrderNr
        End If

        If strVezaNalog <> "" Then
            strSQL = "SELECT it.itkontakt_wert, bsx.bsx_gp_identnummer " _
                                     & " FROM vkbelegstub bsx, gp, " _
                                     & " itkontakt it " _
                                     & " WHERE bsx.bsx_belegnr = @strOrder AND it.itkontakt_idgp = gp.gpx_id AND bsx.bsx_gp_id = gp.gpx_id AND it.itkontakt_iverbindungsart IN (1,3) "

            Using cmd As New SqlCommand(strSQL, GetConnection("KLAESFINESTRE"))
                cmd.Parameters.AddWithValue("@strOrder", strOrder)
                dt = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    Dim strReturn As String = ""
                    For i = 0 To dt.Rows.Count - 1
                        strReturn = strReturn & dt(0)("itkontakt_wert").ToString & "; "
                    Next
                    Return strReturn
                Else
                    Return ""
                End If

            End Using
        End If
        Return ""
    End Function

    Public Function ConvertToHTML(ByVal Box As RichTextBox) _
                   As String
        ' Takes a RichTextBox control and returns a
        ' simple HTML-formatted version of its contents
        Dim strHTML As String
        Dim strColour As String
        Dim blnBold As Boolean
        Dim blnItalic As Boolean
        Dim strFont As String
        Dim shtSize As Short
        Dim lngOriginalStart As Long
        Dim lngOriginalLength As Long
        Dim intCount As Integer
        ' If nothing in the box, exit
        If Box.Text.Length = 0 Then
            Return Box.Rtf
            Exit Function
        End If

        ' Store original selections, then select first character
        lngOriginalStart = 0
        lngOriginalLength = Box.TextLength
        Box.Select(0, 1)
        ' Add HTML header
        strHTML = "<html>"
        ' Set up initial parameters
        strColour = Box.SelectionColor.ToKnownColor.ToString
        blnBold = Box.SelectionFont.Bold
        blnItalic = Box.SelectionFont.Italic
        strFont = Box.SelectionFont.FontFamily.Name
        shtSize = Box.SelectionFont.Size
        ' Include first 'style' parameters in the HTML
        strHTML += "<span style=""font-family: " & strFont & _
          "; font-size: " & shtSize & "pt; color: " _
                          & strColour & """>"
        ' Include bold tag, if required
        If blnBold = True Then
            strHTML += "<b>"
        End If
        ' Include italic tag, if required
        If blnItalic = True Then
            strHTML += "<i>"
        End If
        ' Finally, add our first character
        strHTML += Box.Text.Substring(0, 1)
        ' Loop around all remaining characters
        For intCount = 2 To Box.Text.Length
            ' Select current character
            Box.Select(intCount - 1, 1)
            ' If this is a line break, add HTML tag
            If Box.Text.Substring(intCount - 1, 1) = _
                   Convert.ToChar(10) Then
                strHTML += "<br>"
            End If
            ' Check/implement any changes in style
            If Box.SelectionColor.ToKnownColor.ToString <> _
               strColour Or Box.SelectionFont.FontFamily.Name _
               <> strFont Or Box.SelectionFont.Size <> shtSize _
               Then
                strHTML += "</span><span style=""font-family: " _
                  & Box.SelectionFont.FontFamily.Name & _
                  "; font-size: " & Box.SelectionFont.Size & _
                  "pt; color: " & _
                  Box.SelectionColor.ToKnownColor.ToString & """>"
            End If
            ' Check for bold changes
            If Box.SelectionFont.Bold <> blnBold Then
                If Box.SelectionFont.Bold = False Then
                    strHTML += "</b>"
                Else
                    strHTML += "<b>"
                End If
            End If
            ' Check for italic changes
            If Box.SelectionFont.Italic <> blnItalic Then
                If Box.SelectionFont.Italic = False Then
                    strHTML += "</i>"
                Else
                    strHTML += "<i>"
                End If
            End If
            ' Add the actual character
            strHTML += Mid(Box.Text, intCount, 1)
            ' Update variables with current style
            strColour = Box.SelectionColor.ToKnownColor.ToString
            blnBold = Box.SelectionFont.Bold
            blnItalic = Box.SelectionFont.Italic
            strFont = Box.SelectionFont.FontFamily.Name
            shtSize = Box.SelectionFont.Size
        Next
        ' Close off any open bold/italic tags
        If blnBold = True Then strHTML += ""
        If blnItalic = True Then strHTML += ""
        ' Terminate outstanding HTML tags
        strHTML += "</span></html>"
        ' Restore original RichTextBox selection
        Box.Select(lngOriginalStart, lngOriginalLength)
        ' Return HTML
        Return strHTML
    End Function

    Public Sub CopyDataRecord(ByRef dtDest As DataTable, ByVal dtSource As DataTable, ByVal intRow As Integer)
        Dim dr As DataRow = dtDest.NewRow
        Dim strName As String = ""

        For i = 0 To dtSource.Columns.Count - 1
            strName = dtSource.Columns(i).ColumnName
            dr(strName) = dtSource(intRow)(strName)
        Next
        dtDest.Rows.Add(dr)

    End Sub

    Public Function GetNewBohrShema(ByVal strShema As String, connTools As SqlConnection) As String
        Dim strSQL As String = ""
        Dim ds As DataSet = Nothing
        Dim da As SqlDataAdapter = Nothing

        If strShema.Length >= 8 Then

            'gledam samo tiste, ki so daljše od 8

            strSQL = "SELECT bohr_shema_new FROM slika_vrtanja WHERE bohr_shema_org = @strShema"
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@strShema", strShema)
                Dim dt As DataTable = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    If dt(0)("bohr_shema_new").ToString.Trim <> "" Then
                        Return dt(0)("bohr_shema_new").ToString.Trim
                    Else
                        Return strShema
                    End If
                Else
                    'vstavim v bazo
                    strSQL = "INSERT INTO slika_vrtanja (bohr_shema_org) VALUES (@strShema)"
                    Using cmdI As New SqlCommand(strSQL, connTools)
                        cmdI.Parameters.AddWithValue("@strShema", strShema)
                        cmdI.ExecuteNonQuery()
                    End Using
                    Return strShema
                End If
            End Using

        Else
            Return strShema
        End If

    End Function


    Public Function GetNewProfilIzvedba(ByVal strProfil As String, strIzvedba As String, connTools As SqlConnection) As String
        Dim strSQL As String = ""
        Dim ds As DataSet = Nothing
        Dim da As SqlDataAdapter = Nothing
        Try

            strSQL = "SELECT izvedba_new FROM eprod_profil_izvedba WHERE profil = @strProfil AND izvedba = @strIzvedba"
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@strProfil", strProfil)
                cmd.Parameters.AddWithValue("@strIzvedba", strIzvedba)
                Dim dt As DataTable = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    If dt(0)("izvedba_new").ToString.Trim <> "" Then
                        Return dt(0)("izvedba_new").ToString.Trim
                    Else
                        Return strIzvedba
                    End If
                Else
                    'vstavim v bazo

                    'dodam samo, če vsebuje
                    'alu kotnik*
                    'alu profil*
                    'alu U profil*
                    'balkon*
                    'dodatek za okenski odkapnik*
                    'GU Flatstep*
                    'GU Odkapni*
                    'GU prag*
                    'GU Thermostep*
                    'GU Timberstep*
                    'kriln*
                    'KST*
                    'odkap*
                    'okensk*
                    'pohodni profil*
                    'pvc kotnik*
                    'Steinfensterbankprofil*
                    'vodilo*
                    'WESER*
                    'GU pokrivni*
                    'GU priključek*
                    'ploščati profil*


                    If Mid(strProfil, 1, 10) = "alu kotnik" Or Mid(strProfil, 1, 10) = "alu profil" Or Mid(strProfil, 1, 12) = "alu U profil" Or Mid(strProfil, 1, 6) = "balkon" _
                        Or Mid(strProfil, 1, 27) = "dodatek za okenski odkapnik" Or Mid(strProfil, 1, 11) = "GU Flatstep" Or Mid(strProfil, 1, 10) = "GU Odkapni" Or Mid(strProfil, 1, 7) = "GU prag" _
                        Or Mid(strProfil, 1, 13) = "GU Thermostep" Or Mid(strProfil, 1, 13) = "GU Timberstep" Or Mid(strProfil, 1, 5) = "kriln" Or Mid(strProfil, 1, 3) = "KST" _
                        Or Mid(strProfil, 1, 5) = "odkap" Or Mid(strProfil, 1, 6) = "okensk" Or Mid(strProfil, 1, 14) = "pohodni profil" Or Mid(strProfil, 1, 10) = "pvc kotnik" _
                        Or Mid(strProfil, 1, 22) = "Steinfensterbankprofil" Or Mid(strProfil, 1, 6) = "vodilo" Or Mid(strProfil, 1, 5) = "WESER" Or Mid(strProfil, 1, 11) = "GU pokrivni" _
                        Or Mid(strProfil, 1, 13) = "GU priključek" Or Mid(strProfil, 1, 15) = "ploščati profil" Then


                        strSQL = "INSERT INTO eprod_profil_izvedba (profil, izvedba, izvedba_new, datum_insert) VALUES (@profil, @izvedba, @izvedba_new, @datum_insert)"
                        Using cmdI As New SqlCommand(strSQL, connTools)
                            cmdI.Parameters.AddWithValue("@profil", strProfil)
                            cmdI.Parameters.AddWithValue("@izvedba", strIzvedba)
                            cmdI.Parameters.AddWithValue("@izvedba_new", strIzvedba)
                            cmdI.Parameters.AddWithValue("@datum_insert", Now())
                            cmdI.ExecuteNonQuery()

                            Dim strError As String = ""
                            Dim FileAttachments As New List(Of String)
                            Call SendEmailMailbee(cls.Config.GeteProdMail, cls.Config.GetMailAddress, "Nova barva profila", "Profil: " & strProfil & vbCrLf & "         Izvedba: " & strIzvedba, cls.Config.GetMailUsername, _
                                                  cls.Config.GetMailPassword, strError, FileAttachments, cls.Config.GetMailSMTP, cls.Config.GetMailSMTPPort)
                        End Using
                    End If

                    Return strIzvedba

                End If
            End Using

        Catch ex As Exception
            Return strIzvedba
            'MsgBox(ex.ToString)
        End Try



    End Function

    Private Function GetConfirmedOrdersNotDelivered(dtmStart As Date, dtmEnd As Date, blnAlreadyMontiert As Boolean, connTools As SqlConnection, connMAWI As SqlConnection) As DataTable
        Dim strSQL As String
        Dim dt As DataTable = Nothing
        Dim dtShort As DataTable = CreateTableMAWIOrdersShort()
        strSQL = "SELECT c.name as cname, o.name as oname, oc.name as ocname, ocdet.expectation_date, ca.code, ca.description, sum(ocdet.package_count * dbo.fast_package_quantity(ocdet.id_package_type)) as quantity " _
                    & " FROM order_confirmation oc  " _
                    & " INNER JOIN order_confirmation_details ocdet ON ocdet.id_order_confirmation = oc.id_order_confirmation " _
                    & " INNER JOIN colored_article ca on ocdet.id_article = ca.id_article AND ocdet.id_color = ca.id_color  " _
                    & " INNER JOIN order_to_order_confirmation otoc ON otoc.id_order_confirmation = oc.id_order_confirmation AND otoc.id_order_confirmation_details = ocdet.id_order_confirmation_details  " _
                    & " AND ocdet.id_goods_group = ca.id_goods_group   " _
                    & " INNER JOIN [order] o on o.id_order = otoc.id_order  " _
                    & " INNER JOIN used u ON u.id_order = o.id_order " _
                    & " INNER JOIN contract c ON c.id_contract = u.id_contract    " _
                    & " INNER JOIN partner_branch pb ON pb.id_partner_head = o.id_partner_head AND pb.id_partner_branch = o.id_partner_branch   " _
                    & " WHERE  ocdet.expectation_date BETWEEN @dtmStart AND @dtmEnd " _
                    & " GROUP BY c.name, o.name, oc.name, ocdet.expectation_date, ca.code, ca.description"



        Using cmd As New SqlCommand(strSQL, connMAWI)
            cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
            cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)
            dt = GetData(cmd)
            If blnAlreadyMontiert Then
                If dt.Rows.Count > 0 Then
                    Dim strContract As String = ""
                    Dim strConfirmationNr As String = ""
                    Dim dtmConfirmationDate As Date
                    Dim strArticles As String = ""
                    Dim dr As DataRow
                    Dim blnMontageDone As Boolean = False
                    strContract = dt(0)("cname").ToString
                    strConfirmationNr = dt(0)("ocname").ToString
                    dtmConfirmationDate = CDate(dt(0)("expectation_date"))

                    If MontageAlreadyDone(strContract, connTools) Then

                        strArticles = dt(0)("code").ToString.Trim & " " & vbTab & Mid(dt(0)("description").ToString, 1, 40) & " " & vbTab & dt(0)("quantity").ToString
                        blnMontageDone = True
                    Else
                        blnMontageDone = False
                    End If

                    For i = 1 To dt.Rows.Count - 1

                        If strContract = dt(i)("cname").ToString And strConfirmationNr = dt(0)("ocname").ToString And blnMontageDone Then
                            strArticles = strArticles & vbCrLf & dt(i)("code").ToString.Trim & " " & vbTab & Mid(dt(i)("description").ToString, 1, 40) & " " & vbTab & dt(i)("quantity").ToString
                        Else
                            'dam v tabelo
                            If blnMontageDone Then
                                dr = dtShort.NewRow
                                dr("contract_nr") = strContract
                                dr("confirmation_nr") = strConfirmationNr
                                dr("confirmation_date") = dtmConfirmationDate
                                dr("articles") = strArticles
                                dtShort.Rows.Add(dr)
                            End If

                            strContract = dt(i)("cname").ToString
                            strConfirmationNr = dt(i)("ocname").ToString
                            dtmConfirmationDate = CDate(dt(i)("expectation_date"))

                            If MontageAlreadyDone(strContract, connTools) Then

                                strArticles = dt(i)("code").ToString.Trim & " " & vbTab & Mid(dt(i)("description").ToString, 1, 40) & " " & vbTab & dt(i)("quantity").ToString
                                blnMontageDone = True
                            Else
                                blnMontageDone = False
                            End If

                        End If

                    Next
                    If blnMontageDone Then
                        'se zadnji zapis v tabelo
                        dr = dtShort.NewRow
                        dr("contract_nr") = strContract
                        dr("confirmation_nr") = strConfirmationNr
                        dr("confirmation_date") = dtmConfirmationDate
                        dr("articles") = strArticles
                        dtShort.Rows.Add(dr)
                    End If

                    Return dtShort
                End If
            Else
                Return dtShort
            End If
        End Using


        Return dtShort
    End Function

End Module
