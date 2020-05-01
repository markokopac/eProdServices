Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common
Imports eProdService.cls.msora.DB_MSora
Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Imports System.IO
Imports FireSharp


Public Class frmMainForm

    Dim blnLoaded As Boolean = False

    Dim intUpdateMinutes As Integer
    Dim intMailingMinutes As Integer
    Dim intDeliveryDateMinutes As Integer
    Dim intMAWIDateMinutes As Integer
    Dim intCutterSendFileMinutes As Integer
    Dim intCutterArchiveMinutes As Integer
    Dim intSklicMinutes As Integer
    Dim intMonterMinutes As Integer
    Dim intKapaLogMinutes As Integer
    Dim intSlikaVrtanjaMinutes As Integer
    Dim intTechnicalLockMinutes As Integer

    Dim intSpicaEventsMinutes As Integer

    Dim dtmStartTimeUpdateName As Date
    Dim dtmEndTimeUpdateName As Date

    Dim dtmStartTimeSendMail As Date
    Dim dtmEndTimeSendMail As Date

    Dim dtmStartTimeDeliveryDate As Date
    Dim dtmEndTimeDeliveryDate As Date

    Dim dtmStartTimeMawiDate As Date
    Dim dtmEndTimeMawiDate As Date

    Dim dtmStartTimeCutterSendFile As Date
    Dim dtmEndTimeCutterSendFile As Date

    Dim dtmStartTimeCutterArchive As Date
    Dim dtmEndTimeCutterArchive As Date

    Dim dtmStartTimeSklic As Date
    Dim dtmEndTimeSklic As Date


    Dim strDaysUpdateName As String
    Dim strDaysSendMail As String
    Dim strDaysDeliveryDate As String
    Dim strDaysMawiDate As String

    Dim strDaysCutterSendFile As String
    Dim strDaysCutterArchive As String
    Dim strDaysUpdateSklic As String

    Dim strDaysUpdateRKoncano As String
    Dim dtmStartTimeRKoncano As Date
    Dim dtmEndTimeRKoncano As Date

    Dim dtmStartTimeMonter As Date
    Dim dtmEndTimeMonter As Date
    Dim strDaysUpdateMonter As String

    Dim dtmStartTimeKapaLog As Date
    Dim dtmEndTimeKapaLog As Date
    Dim strDaysUpdateKapaLog As String

    Dim dtmStartTimeSlikaVrtanja As Date
    Dim dtmEndTimeSlikaVrtanja As Date
    Dim strDaysUpdateSlikaVrtanja As String




    Private Sub frmMainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If MsgBox("Ali res želite zapreti program?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
        'e.Cancel = True
        'Else
        Call AddToActionLogSendMail("Zaključek programa")
        Call AddToActionLogUpdateName("Zaključek programa")

        'End If
    End Sub

    Private Sub RunAll()
        Using connTools As SqlConnection = GetConnection("TOOLS")
            Using connKBS As SqlConnection = GetConnection("KLAESKBS")
                Call AutoProcessUpdateName(connTools, connKBS)

                Call AutoProcessUpdateProductionText(connTools, connKBS)

                Call AutoProcessMail()

                Call AutoProcessDeliveryDate()

                Call AutoProcessMAWIDates()

                Call AutoProcessCutterSendFile()

                Call AutoProcessCutterSendFile()

                Call AutoProcessUpdateSklic()

                Call AutoProcess_R_Koncano()

                Call AutoProcessUpdateMonter()

                Call AutoProcessUpdateSlikaVrtanja(True)
            End Using
        End Using
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If Environment.GetCommandLineArgs().Length > 1 Then
            cls.Config.iniFile = New cIni(Environment.GetCommandLineArgs(1))
        Else
            cls.Config.iniFile = New cIni("./eProdService.ini")
        End If

        Me.chkAutoStartName.Checked = cls.Config.AutoStartName
        Me.chkAutoSendMailing.Checked = cls.Config.AutoStartMailinig
        Me.chkAutoChangeDeliveryDate.Checked = cls.Config.AutoStartDeliveryDate
        Me.chkAutoMAWIDates.Checked = cls.Config.AutoStartMAWIDate
        Me.chkAutoCutterSendFile.Checked = cls.Config.AutoStartCutterFile
        Me.chkAutoCheckCutterArchive.Checked = cls.Config.AutoStartCutterArchive
        Me.chkUpdateSklic.Checked = cls.Config.AutoStartSklic
        Me.chkUpdateMonterOsn.Checked = cls.Config.AutoStartMonter
        Me.chkEProdSlikaVrtanja.Checked = cls.Config.AutoStartSlikaVrtanja
        Me.chkKapaLog.Checked = cls.Config.AutoStartKapaLog
        Me.chkSpicaEventUpdate.Checked = cls.Config.AutoStartKapaLog
        Me.chkLockTechnicalOrders.Checked = cls.Config.AutoStartTechnicalLock

        Me.tmrUpdateName.Interval = 60000
        Me.tmrSendMail.Interval = 60000
        Me.tmrDeliveryDate.Interval = 60000
        Me.tmrMAWIDates.Interval = 60000
        Me.tmrCutterSendFile.Interval = 60000
        Me.tmrCutterArchive.Interval = 60000
        Me.tmrSklic.Interval = 60000
        Me.tmrMonter.Interval = 60000
        Me.tmrSlikaVrtanja.Interval = 60000
        Me.tmrSpica.Interval = 60000
        Me.tmrLockTechnicalOrders.Interval = 60000


        Call AddToActionLogSendMail("Application started")
        Call AddToActionLogUpdateName("Application started")

        intUpdateMinutes = cls.Config.GetUpdateNameCheckInterval
        lblNextUpdateName.Text = intUpdateMinutes

        intMailingMinutes = cls.Config.GetSendMailCheckInterval
        lblNextUpdateMail.Text = intMailingMinutes

        intDeliveryDateMinutes = cls.Config.GetDeliveryDateCheckInterval
        lblNextUpdateDeliveryDate.Text = intDeliveryDateMinutes

        intMAWIDateMinutes = cls.Config.GetMawiDateCheckInterval
        lblNextUpdateMAWIDates.Text = intMAWIDateMinutes

        intCutterSendFileMinutes = cls.Config.GetCutterSendFileCheckInterval
        lblNextUpdateCutterSendFile.Text = intCutterSendFileMinutes

        intCutterArchiveMinutes = cls.Config.GetCutterArchiveCheckInterval
        lblNextUpdateCutterArchive.Text = intCutterArchiveMinutes

        intSklicMinutes = cls.Config.GetSklicUpdateInterval
        lblNextSklic.Text = intSklicMinutes

        intTechnicalLockMinutes = cls.Config.GetTechnicalLockUpdateInterval
        lblLockTechnicalOrders.Text = intTechnicalLockMinutes

        dtmStartTimeUpdateName = cls.Config.GetUpdateNameStartTime
        dtmEndTimeUpdateName = cls.Config.GetUpdateNameEndTime
        strDaysUpdateName = cls.Config.GetUpdateNameDays

        dtmStartTimeSendMail = cls.Config.GetSendMailStartTime
        dtmEndTimeSendMail = cls.Config.GetSendMailEndTime
        strDaysSendMail = cls.Config.GetSendMailDays

        dtmStartTimeDeliveryDate = cls.Config.GetDeliveryDateStartTime
        dtmEndTimeDeliveryDate = cls.Config.GetDeliveryDateEndTime
        strDaysDeliveryDate = cls.Config.GetDeliveryDateDays

        dtmStartTimeMawiDate = cls.Config.GetMawiDateStartTime
        dtmEndTimeMawiDate = cls.Config.GetMawiDateEndTime
        strDaysMawiDate = cls.Config.GetMawiDateDays

        dtmStartTimeCutterSendFile = cls.Config.GetCutterSendFileStartTime
        dtmEndTimeCutterSendFile = cls.Config.GetCutterSendFileEndTime
        strDaysCutterSendFile = cls.Config.GetCutterSendFileDays

        dtmStartTimeCutterArchive = cls.Config.GetCutterArchiveStartTime
        dtmEndTimeCutterArchive = cls.Config.GetCutterArchiveEndTime
        strDaysCutterArchive = cls.Config.GetCutterArchiveDays

        dtmStartTimeSklic = cls.Config.GetSklicStartTime
        dtmEndTimeSklic = cls.Config.GetSklicEndTime
        strDaysUpdateSklic = cls.Config.GetSklicDays

        dtmStartTimeRkoncano = cls.Config.GetRKoncanoStartTime
        dtmEndTimeRkoncano = cls.Config.GetRkoncanoEndTime
        strDaysUpdateRKoncano = cls.Config.GetRkoncanoDays


        dtmStartTimeMonter = cls.Config.GetMonterStartTime
        dtmEndTimeMonter = cls.Config.GetMonterEndTime
        strDaysUpdateMonter = cls.Config.GetMonterDays
        intMonterMinutes = cls.Config.GetmonterUpdateInterval
        lblNextMonter.Text = intMonterMinutes

        dtmStartTimeKapaLog = cls.Config.GetKapaLogStartTime
        dtmEndTimeKapaLog = cls.Config.GetKapaLogEndTime
        strDaysUpdateKapaLog = cls.Config.GetKapaLogDays

        intKapaLogMinutes = cls.Config.GetKapaLogUpdateInterval
        lblNextKapaLog.Text = intKapaLogMinutes

        intSpicaEventsMinutes = cls.Config.GetSpicaEventUpdateInterval
        lblSpicaMinutes.Text = intSpicaEventsMinutes

        dtmStartTimeSlikaVrtanja = cls.Config.GetSlikaVrtanjaStartTime
        dtmEndTimeSlikaVrtanja = cls.Config.GetSlikaVrtanjaEndTime
        strDaysUpdateSlikaVrtanja = cls.Config.GetSlikaVrtanjaDays
        intSlikaVrtanjaMinutes = cls.Config.GetSlikaVrtanjaUpdateInterval
        lblNextSlikaVrtanja.Text = intSlikaVrtanjaMinutes

        gConnEProd = GetMyConnection("eprod")

        Me.lblStart.Text = Now.ToString

        If cls.Config.RunAtStart Then
            Call RunAll()
        End If

    End Sub

    Public Sub AutoProcessUpdateName(connTools As SqlConnection, connKBS As SqlConnection)
        Dim dtmStart As Date
        Dim dt As DataTable
        Dim dt2 As DataTable
        Dim strOrder As String
        Dim strSQL As String
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysUpdateName.Split(",")

        Try


            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetUpdateNameCheckLastDays * -1, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek

            If dtmTime >= dtmStartTimeUpdateName And dtmTime <= dtmEndTimeUpdateName And strDaysUpdateName.Contains(intDayOfWeek.ToString) Then

                Call ResetConnection()

                Call AddToActionLogUpdateNameTB(Me.txtLog, "AutoProcessUpdateName - začetek procesiranja")

                dt = SearchOrdersChangeName(dtmStart, Now.Date, "", "", "", False, connKBS)
                
                        For i = 0 To dt.Rows.Count - 1
                            strOrder = dt(i)("kommissionsnummer").ToString
                            strSQL = "SELECT * FROM eprod_change_name WHERE order_nr = @order_nr"

                    Using cmd2 As New SqlCommand(strSQL, connTools)
                        cmd2.Parameters.AddWithValue("@order_nr", strOrder)
                        dt2 = GetData(cmd2)
                        If dt2.Rows.Count = 0 Then
                            Call modEProd.ChangeOrderName(dt(i)("auftragsnummer").ToString, dt(i)("kommissionsnummer").ToString, dt(i)("old_name").ToString, dt(i)("new_name").ToString, connTools)
                            Call AddToActionLogUpdateNameTB(Me.txtLog, "Nalog " & strOrder & " " & dt(i)("old_name").ToString & " --> " & dt(i)("new_name").ToString)
                        Else
                            If dt2(0)("name_new").ToString.Trim <> dt(i)("new_name").ToString.Trim Then
                                Call modEProd.ChangeOrderName(dt(i)("auftragsnummer").ToString, dt(i)("kommissionsnummer").ToString, dt(i)("old_name").ToString, dt(i)("new_name").ToString, connTools)
                                Call AddToActionLogUpdateNameTB(Me.txtLog, "Nalog " & strOrder & " - ponovno spremenjen " & dt2(0)("changed_on").ToString)
                            Else
                                'Call AddToActionLogTB(Me.txtLog, "Nalog " & strOrder & " - že spremenjen " & dt2(0)("changed_on").ToString)
                            End If
                        End If
                    End Using

                            'Dim conn As MySqlConnection = GetMyConnection("eprod")
                    Call UpdateReclamation(dt(i)("auftragsnummer").ToString.ToUpper, strOrder, gConnEProd, connKBS)

                            'conn = Nothing
                        Next
                    

                Call AddToActionLogUpdateNameTB(Me.txtLog, "AutoProcessUpdateName - konec procesiranja")
            Else
                Call AddToActionLogUpdateNameTB(Me.txtLog, "AutoProcessUpdateName - Ura, dan ni vključena")
            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Sub

    Public Sub AutoProcessUpdateProductionText(connTools As SqlConnection, connKBS As SqlConnection)
        Dim dtmStart As Date
        Dim dt As DataTable
        Dim strKommission As String
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysUpdateName.Split(",")
        Dim strName As String
        Dim strSQL As String = ""
        Dim dt2 As DataTable = Nothing
        Dim strOrderDesc As String = ""
        Dim strOrderNr As String

        Try

            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetUpdateNameCheckLastDays * -1, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek

            If dtmTime >= dtmStartTimeUpdateName And dtmTime <= dtmEndTimeUpdateName And strDaysUpdateName.Contains(intDayOfWeek.ToString) Then

                Call AddToActionLogProductionTextTB(Me.txtLog, "AutoProcessProductionText - začetek procesiranja")


                dt = SearchOrdersProductionText(dtmStart, Now.Date, "", "", False, connKBS)

                For i = 0 To dt.Rows.Count - 1
                    strKommission = dt(i)("fax_auftragnr").ToString

                    Dim dtOrders As DataTable = GetOrdersInKommission(strKommission)

                    If Not dtOrders Is Nothing Then
                        For j = 0 To dtOrders.Rows.Count - 1
                            strOrderNr = dtOrders(j)("auftragsnummer").ToString

                            strSQL = "SELECT * FROM eprod_change_prod_text WHERE auftragsnummer = @strOrderNr AND kommissionsnummer = @Kommissionnr AND date_changed = @dtmDataChanged"

                            Using cmd2 As New SqlCommand(strSQL, conntools)
                                cmd2.Parameters.AddWithValue("@strOrderNr", strOrderNr)
                                cmd2.Parameters.AddWithValue("@Kommissionnr", strKommission)
                                cmd2.Parameters.AddWithValue("@dtmDataChanged", dt(0)("fax_moddate"))
                                dt2 = GetData(cmd2)
                                If dt2.Rows.Count = 0 Then
                                    strName = (dt(i)("fax_beleg_anrede").ToString.Trim + " " + dt(i)("fax_beleg_vorname").ToString.Trim + " " + dt(i)("fax_beleg_name").ToString.Trim).Trim

                                    strOrderDesc = GetOrderProperties(strOrderNr, strKommission, "DESCRIPTION", connKBS)

                                    If strName = "" Then
                                        strName = GetOrderProperties(strOrderNr, strKommission, "NAME", connKBS)
                                    End If

                                    If strName <> "" Or strOrderDesc <> "" Then
                                        Call modEProd.UpdateProductionText(strOrderNr, strKommission, strName, dt(i)("fax_fertigungtext").ToString, strOrderDesc, dt(0)("fax_moddate"), conntools)

                                        Call AddToActionLogProductionTextTB(Me.txtLog, "Nalog " & strKommission & " Production Text " & dt(i)("fax_fertigungtext").ToString & " {" & strOrderDesc & "}")
                                    End If

                                End If
                            End Using


                        Next
                    End If


                Next
                    

                Call AddToActionLogProductionTextTB(Me.txtLog, "AutoProcessProductionText - konec procesiranja")
            Else
                Call AddToActionLogProductionTextTB(Me.txtLog, "AutoProcessProductionText - Ura, dan ni vključena")
            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Sub

    Public Sub AutoProcessDeliveryDate()
        Dim dtmStart As Date
        Dim dtmStartKapa As Date
        Dim dtmEndKapa As Date
        Dim dt As DataTable
        Dim strNarocilo As String
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysUpdateName.Split(",")

        Dim dtmTerminKoncanja As Date
        Dim strOldDate As String = ""
        Dim strLetnica As String = ""
        Dim intNumbers As Integer = 0
        Dim strNulls As String = ""
        Dim intLen As Integer = 0

        Try

            'se enkrat vspostavim povezavo


            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetDeliveryDateCheckLastDays, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek

            If dtmTime >= dtmStartTimeUpdateName And dtmTime <= dtmEndTimeUpdateName And strDaysUpdateName.Contains(intDayOfWeek.ToString) Then

                Call ResetConnection()

                Call AddToActionLogDeliveryDateTB(Me.txtLog, "AutoProcessDeliveryDate - začetek procesiranja")

                Using connTools As SqlConnection = GetConnection("TOOLS")
                    Using connKAPA As SqlConnection = GetConnection("KAPA")

                        dt = SearchKapa(DateAdd(DateInterval.Day, -7, Now.Date), dtmStart, connKAPA)

                        For i = 0 To dt.Rows.Count - 1

                            strNarocilo = dt(i)("AIX_PTABBELEGNR").ToString
                            dtmTerminKoncanja = dt(i)("AIX_LIEFERTERMIN")


                            If modEProd.ChangeDeliveryDate(strNarocilo, dtmTerminKoncanja, strOldDate) Then
                                Call AddToActionLogDeliveryDateTB(Me.txtLog, "Nalog " & strNarocilo & " Termin končanja: " & strOldDate & " --> " & dtmTerminKoncanja)
                                txtLog.Refresh()
                                Me.Refresh()
                            End If

                        Next


                        Call AddToActionLogDeliveryDateTB(Me.txtLog, "AutoProcessDeliveryDate - konec procesiranja")

                        'posodobim še KAPA PCT - brišem 100%
                        Dim strLog As String = ""

                        If cls.Config.UpdateKapaPCT Then

                            Call AddToActionLogDeliveryDateTB(Me.txtLog, "AutoProcessUpdateKapaPCT - začetek procesiranja")
                            dtmStartKapa = DateAdd("d", cls.Config.UpdateKapaPCTDaysBefore, Now.Date)
                            dtmEndKapa = DateAdd("d", cls.Config.UpdateKapaPCTDaysAfter, Now.Date)
                            strLog = UpdateKapaPCT(dtmStartKapa, dtmEndKapa, connKAPA)

                            Call AddToActionLogDeliveryDateTB(Me.txtLog, "Nalogi spremenjeni PCT na 0% " & vbCrLf & strLog)

                            Call AddToActionLogDeliveryDateTB(Me.txtLog, "AutoProcessUpdateKapaPCT - konec procesiranja")

                        End If

                    End Using
                End Using
            Else
                Call AddToActionLogDeliveryDateTB(Me.txtLog, "AutoProcessDeliveryDate - Ura, dan ni vključena")
            End If

            

        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try
    End Sub

    Private Sub AutoProcessMAWIDates()
        Dim strLog As String = ""
        Dim dtOrders As DataTable = Nothing
        Dim dtmStart As Date
        Dim dt As DataTable
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysMawiDate.Split(",")

        Dim strOldDate As String = ""
        Dim strLetnica As String = ""
        Dim intNumbers As Integer = 0
        Dim strNulls As String = ""
        Dim intLen As Integer = 0

        Try



            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetMAWIDateCheckLastDays, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek

            If dtmTime >= dtmStartTimeUpdateName And dtmTime <= dtmEndTimeUpdateName And strDaysMawiDate.Contains(intDayOfWeek.ToString) Then

                Call ResetConnection()

                Using connMawi As SqlConnection = GetConnection("KLAESMAWI")
                    Using connTools As SqlConnection = GetConnection("TOOLS")
                        Call AddToActionLogMAWIDateTB(Me.txtLog, "***** AutoProcessMawiDate - začetek procesiranja")

                        Call AddToActionLogMAWIDateTB(Me.txtLog, "      ***** Datum naročila ")
                        dt = GetMawiOrdersService(0, Now.Date, dtmStart, "", connMawi)
                        strLog = ProcessMawiDates(dt, connMawi, connTools)
                        Call AddToActionLogMAWIDateTB(Me.txtLog, strLog)
                        txtLog.Refresh()
                        Me.Refresh()

                        Call AddToActionLogMAWIDateTB(Me.txtLog, "      ***** Datum potrditve naročila ")
                        dt = GetMawiOrdersService(1, Now.Date, dtmStart, "", connMawi)
                        strLog = ProcessMawiDates(dt, connMawi, connTools)
                        Call AddToActionLogMAWIDateTB(Me.txtLog, strLog)
                        txtLog.Refresh()
                        Me.Refresh()

                        Call AddToActionLogMAWIDateTB(Me.txtLog, "      ***** Datum dobave ")
                        dt = GetMawiOrdersService(2, Now.Date, dtmStart, "", connMawi)
                        strLog = ProcessMawiDates(dt, connMawi, connTools)
                        Call AddToActionLogMAWIDateTB(Me.txtLog, strLog)
                        txtLog.Refresh()
                        Me.Refresh()
                    End Using
                End Using
                Call AddToActionLogMAWIDateTB(Me.txtLog, "***** AutoProcessMawiDate - konec procesiranja")

            Else
                Call AddToActionLogMAWIDateTB(Me.txtLog, "***** AutoProcessMawiDate - Ura, dan ni vključena")
            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            strLog = strLog & vbCrLf & "NAPAKA!"
            Call AddToActionLogDeliveryDateTB(Me.txtLog, strLog)
        End Try

    End Sub

    Public Sub AutoProcessCutterSendFile()
        Dim dtmStart As Date
        Dim dt As DataTable
        Dim strLog As String
        Dim strOrderNr As String
        Dim strkommissionNr As String
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysCutterSendFile.Split(",")

        Try


            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetCutterSendFileCheckLastDays * -1, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek

            If dtmTime >= dtmStartTimeCutterSendFile And dtmTime <= dtmEndTimeCutterSendFile And strDaysCutterSendFile.Contains(intDayOfWeek.ToString) Then

                Call ResetConnection()

                Call AddToActionCutterSendFileTB(Me.txtLog, "AutoProcessCutterSendFile - začetek procesiranja " & dtmStart & " ---> " & Now.Date)

                Using conn As SqlConnection = GetConnection("TOOLS")
                    Using connkbs As SqlConnection = GetConnection("KLAESKBS")
                        dt = GetStartedKommissions(dtmStart, Now.Date, cls.Config.GetCutterStation, "", "")
                        For i = 0 To dt.Rows.Count - 1
                            strLog = ""

                            strOrderNr = dt(i)("auftragsnummer").ToString
                            strkommissionNr = dt(i)("kommissionsnummer").ToString

                            strLog = ExecuteCutterSendFile(strkommissionNr, strOrderNr, conn, connkbs)

                            Debug.Print(strkommissionNr)

                            If strLog <> "" Then

                                AddToActionCutterSendFileTB(Me.txtLog, strLog)
                            Else

                                AddToActionCutterSendFileTB(Me.txtLog, "Procesed: Order Nr.: = " & strOrderNr & " Kommission = " & strkommissionNr)

                            End If

                        Next
                    End Using
                End Using

                Call AddToActionLogSendMailTB(Me.txtLog, "AutoProcessCutterSendFile - konec procesiranja")
            Else
                Call AddToActionLogSendMailTB(Me.txtLog, "AutoProcessCutterSendFile - Ura, dan ni vključena")
            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Sub

    Public Sub AutoProcessCutterArchive()
        Dim dtmStart As Date
        Dim dt As DataTable
        Dim strLog As String
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysCutterArchive.Split(",")

        Try


            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetCutterArchiveCheckLastDays * -1, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek

            If dtmTime >= dtmStartTimeCutterArchive And dtmTime <= dtmEndTimeCutterArchive And strDaysCutterArchive.Contains(intDayOfWeek.ToString) Then

                Call ResetConnection()

                Call AddToActionCutterArchiveTB(Me.txtLog, "AutoProcessCutterArchive - začetek procesiranja")

                dt = GetFinnishedCutterFiles(dtmStart, Now.Date)

                strLog = MoveCutterFilesToArchive(dt)
                Call AddToActionCutterArchiveTB(Me.txtLog, strLog)



                Call AddToActionCutterArchiveTB(Me.txtLog, "AutoProcessCutterArchive - konec procesiranja")
            Else
                Call AddToActionCutterArchiveTB(Me.txtLog, "AutoProcessCutterArchive - Ura, dan ni vključena")
            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try


    End Sub


    Public Sub AutoProcessUpdateMonter()
        Dim dtmStart As Date
        Dim strLog As String = "Update MONTER: " & vbCrLf
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysUpdateMonter.Split(",")
        Dim strSQL As String
        Dim strOsnovniNalog As String = ""


        Try

            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetMonterCheckLastDays * -1, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek

            If dtmTime >= dtmStartTimeMonter And dtmTime <= dtmEndTimeMonter And strDaysUpdateMonter.Contains(intDayOfWeek.ToString) Then

                Call AddToActionUpdateMonterTB(Me.txtLog, "AutoProcessUpdateMonter - začetek procesiranja")

                'ponudbe
                strSQL = "SELECT bsx_belegnr, bsx_vorgangnr FROM VKBELEGSTUB WHERE BSX_IBELEGART = 20 AND bsx_erfdatum BETWEEN @dtmStart AND @dtmEnd AND bsx_belegnr LIKE 'N%' " _
                    & " AND BSX_21_TEXT_M_MONTER_OSN IS NULL"
                Using conn As SqlConnection = GetConnection("KLAESKBS")

                    Using cmd As New SqlCommand(strSQL, conn)
                        cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                        cmd.Parameters.AddWithValue("@dtmEnd", Now.Date)
                        Dim dt = GetData(cmd)

                        For i = 0 To dt.Rows.Count - 1

                            'zaenkrat poiščem prvo montažo iz tega projekta
                            strSQL = "SELECT top 1 mo.montage_person_id FROM [KlaesTools].dbo.montage mo WHERE mo.main_order_nr collate Latin1_General_CI_AS_WS " _
                                & " IN (SELECT bsx_belegnr FROM vkbelegstub WHERE bsx_vorgangnr = @bsx_vorgangnr " _
                                & " AND bsx_ibelegart = 20) ORDER BY montage_date "

                            Using cmd2 As New SqlCommand(strSQL, conn)
                                cmd2.Parameters.AddWithValue("@bsx_vorgangnr", dt(i)("bsx_vorgangnr").ToString)
                                Dim dt2 As DataTable = GetData(cmd2)
                                If dt2.Rows.Count > 0 Then
                                    Dim strMonter As String = dt2(0)("montage_person_id").ToString
                                    If strMonter <> "" Then
                                        strSQL = "UPDATE VKBELEGSTUB SET BSX_21_TEXT_M_MONTER_OSN = @strMonter WHERE bsx_belegnr = @bsx_belegnr AND BSX_21_TEXT_M_MONTER_OSN IS NULL"
                                        Using cmd3 As New SqlCommand(strSQL, conn)
                                            cmd3.Parameters.AddWithValue("@strMonter", strMonter)
                                            cmd3.Parameters.AddWithValue("@bsx_belegnr", dt(i)("bsx_belegnr"))
                                            cmd3.ExecuteNonQuery()
                                        End Using

                                        strLog = strLog & "Reklamacija: " & dt(i)("bsx_belegnr").ToString & " --> Monter = " & strMonter & vbCrLf
                                    End If
                                End If
                            End Using


                        Next

                    End Using
                End Using


                Call AddToActionUpdateMonterTB(Me.txtLog, strLog)


                Call AddToActionUpdateMonterTB(Me.txtLog, "AutoProcessUpdateMonter - konec procesiranja")
            Else
                'Call AddToActionUpdateSklicTB(Me.txtLog, "AutoProcessUpdateSklic - Ura, dan ni vključena")
            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Sub
    Public Sub AutoProcessSpicaEvents()
        Dim dtmStart As Date
        Dim dtmEnd As Date = Now.Date
        Dim strLog As String = "Spica event Log: " & vbCrLf

        Try
            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetSpicaEventsCheckDaysBack * -1, Now.Date)

            Call AddToActionSpicaEvents(Me.txtLog, "AutoProcessSpicaEventslog - začetek procesiranja")

            Call UpdateSpicaEvents(dtmStart)

            Call AddToActionSpicaEvents(Me.txtLog, "AutoProcessSpicaEventslog - konec procesiranja")

        Catch ex As Exception

        End Try
    End Sub

    Public Sub AutoProcessTechnicalLocks()
        Dim dtmStart As Date
        Dim dtmEnd As Date = Now.Date
        Dim strLog As String = "Technical Locks Log: " & vbCrLf
        Dim dt As DataTable = Nothing
        Dim strOrderN As String = ""
        Dim strOrderT As String = ""
        Dim dtmTehnicnoObdelano As Date = Nothing
        Dim intMeldeStatus As Integer = 0
        Dim dtmMeldeDatum As Date = Nothing
        Dim lngMeldener As Long = cls.Config.TechnicalLockUser
        Dim strSQL As String = ""
        Try


            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetTechnicalLockCheckDaysBack * -1, Now.Date)

            Call AddToActionTechnicalLockEvents(Me.txtLog, "AutoProcessTechnicalLocks - začetek procesiranja")
            Using connKBS As SqlConnection = GetConnection("KLAES")
                If GetKlaesUser(lngMeldener, connKBS) <> "" Then
                    Using connTools As SqlConnection = GetConnection("TOOLS")
                        'zaklenjeni nalogi
                        dt = GetTechicalFinnishedOrders(dtmStart, dtmEnd, connKBS) 'dobim N-je
                        If Not dt Is Nothing Then
                            Call LockTechnicalDoc(dt, lngMeldener, connTools, connKBS)
                        End If
                        'odklenjeni nalogi
                        dt = GetUnlockDoc(connTools)
                        If Not dt Is Nothing Then
                            Call LockTechnicalDoc(dt, lngMeldener, connTools, connKBS)
                        End If

                    End Using
                Else
                    'log - ni klaes uporabnika
                    Call AddToActionTechnicalLockEvents(Me.txtLog, strOrderT & vbTab & "Klaes uporabnik ni definiran")
                End If
            End Using

            Call AddToActionTechnicalLockEvents(Me.txtLog, "AutoProcessTechnicalLocks - konec procesiranja")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LockTechnicalDoc(dt As DataTable, lngMeldener As Long, connTools As SqlConnection, connKBS As SqlConnection)
        Dim strSql As String = ""
        Dim strOrderN As String = ""
        Dim strOrderT As String = ""
        Dim dtmTehnicnoObdelano As Date
        Dim intMeldeStatus As Integer

        For i = 0 To dt.Rows.Count - 1
            strOrderN = dt(i)("auftragsnummer").ToString
            dtmTehnicnoObdelano = CDate(dt(i)("tehnicnoobdelano").ToString)

            strOrderT = "T" & Mid(strOrderN, 2, 6)

            strSql = "SELECT bsx_belegnr, bsx_imeldestatus, bsx_idmeldender, bsx_meldedatum FROM vkbelegstub WHERE bsx_belegnr LIKE '" & strOrderT & "%'"

            Using cmd As New SqlCommand(strSql, connKBS)
                Dim dt1 As DataTable = GetData(cmd)
                For j = 0 To dt1.Rows.Count - 1
                    intMeldeStatus = DB2IntZero(dt1(j)("bsx_imeldestatus"))
                    strOrderT = dt1(j)("bsx_belegnr").ToString.Trim
                    If intMeldeStatus = 0 Then
                        'potem preverim, če ga slučajno zaklenem nazaj...
                        strSql = "SELECT * FROM unlock_doc WHERE document_nr = @strordert AND return_melde_datum IS NOT NULL AND unlock_type = 2 ORDER BY datum DESC"
                        Using cmd2 As New SqlCommand(strSql, connTools)
                            cmd2.Parameters.AddWithValue("@strordert", strOrderT)
                            Dim dt2 As DataTable = GetData(cmd2)
                            If dt2.Rows.Count > 0 Then
                                If DateDiff(DateInterval.Minute, dt2(0)("return_melde_datum"), Now) >= cls.Config.GetUnlockMin Then
                                    'zaklenem nazaj
                                    If ReturnMeldeStatus(DB2Lng(dt2(0)("unlock_id")), connTools, connKBS) Then
                                        'log
                                        Call AddToActionTechnicalLockEvents(Me.txtLog, strOrderT & vbTab & "Naročilo ponovno zaklenjeno po " & DateDiff(DateInterval.Minute, Now, dt2(0)("datum")) & " minutah")
                                    Else
                                        'log
                                        'je še vedno zaklenjen
                                        'Call AddToActionTechnicalLockEvents(Me.txtLog, strOrderT & vbTab & "Nalog zaklenjen nazaj")
                                    End If
                                End If
                            Else
                                If lngMeldener <> -1 Then
                                    If SetMeldeStatus(strOrderT, "Auto set Melde status", lngMeldener, Now.Date, connTools, connKBS) Then
                                        'log
                                        Call AddToActionTechnicalLockEvents(Me.txtLog, strOrderT & vbTab & "Zaklepanje naročila")
                                    Else
                                        'log
                                        Call AddToActionTechnicalLockEvents(Me.txtLog, strOrderT & vbTab & "Napaka pri zaklepanju naročila - ne najdem naročila ali pa napaka v podatkih")
                                    End If
                                End If
                            End If
                        End Using
                    Else
                        'preverim, če je že v unlock_doc
                        If lngMeldener <> -1 Then
                            If SetMeldeStatus(strOrderT, "Auto set Melde status", lngMeldener, Now.Date, connTools, connKBS) Then
                                'log
                                Call AddToActionTechnicalLockEvents(Me.txtLog, strOrderT & vbTab & "Zaklepanje naročila")
                            Else
                                'log
                                Call AddToActionTechnicalLockEvents(Me.txtLog, strOrderT & vbTab & "Napaka pri zaklepanju naročila - ne najdem naročila ali pa napaka v podatkih")
                            End If
                        End If
                    End If
                Next
            End Using
        Next
    End Sub


    Private Sub UpdateSpicaEvents(dtmStart As Date)
        Dim strSQL As String = ""
        Dim dtmDate As Date = Nothing
        Dim intCount As Integer = 0

        Try


            Using connSpica As SqlConnection = GetConnection("SPICA")
                Using connVnosUr As SqlConnection = GetConnection("VNOSUR")
                    dtmDate = dtmStart

                    Do While dtmDate <= Now.Date
                        intCount = 0
                        strSQL = "SELECT e.no, e.USERNO, e.dt, e.EVENTID, e.TIMESTAMP " _
                                & " FROM  EVENTS E " _
                                & " INNER JOIN USERS U ON E.USERNO = U.[NO]  " _
                                & " LEFT OUTER JOIN EVNCMN ON E.[NO] = EVNCMN.ID " _
                                & " WHERE(E.USERNO Is Not NULL) " _
                                & " AND U.HOST = 1  " _
                                & " AND ((e.timestamp is not null AND e.eventid IN (18,34)) OR (e.timestamp is null AND e.eventid IN (66,67,33,18,34)))  " _
                                & " AND cast(e.DT as date) = @dtmDate " _
                                & " ORDER BY dt ASC "

                        Using cmd As New SqlCommand(strSQL, connSpica)
                            cmd.Parameters.AddWithValue("@dtmDate", dtmDate)
                            Dim dt As DataTable = GetData(cmd)
                            For i = 0 To dt.Rows.Count - 1
                                Dim dr As DataRow = dt.Rows(i)
                                If InsertSpicaEvent(dr, connVnosUr) Then
                                    intCount = intCount + 1
                                End If
                            Next
                            Call AddToActionSpicaEvents(Me.txtLog, "       Špica events" & vbTab & dtmDate.ToString & vbTab & intCount.ToString)
                            Me.txtLog.Refresh()
                        End Using
                        dtmDate = DateAdd(DateInterval.Day, 1, dtmDate)
                    Loop
                End Using
            End Using
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try
    End Sub

    Private Function InsertSpicaEvent(dr As DataRow, conn As SqlConnection) As Boolean
        Dim strSQL As String
        Try

            strSQL = "SELECT * FROM spica_events WHERE no = @no"
            Using cmd As New SqlCommand(strSQL, conn)
                cmd.Parameters.AddWithValue("@no", dr("no"))
                Dim dt As DataTable = GetData(cmd)
                If dt.Rows.Count = 0 Then
                    strSQL = "INSERT INTO spica_events (no, userno, eventid, dt, timestamp) VALUES (@no, @userno, @eventid, @dt, @timestamp)"
                    Using cmdU As New SqlCommand(strSQL, conn)
                        cmdU.Parameters.AddWithValue("@no", dr("no"))
                        cmdU.Parameters.AddWithValue("@userno", dr("userno"))
                        cmdU.Parameters.AddWithValue("@eventid", dr("eventid"))
                        cmdU.Parameters.AddWithValue("@dt", dr("dt"))
                        cmdU.Parameters.AddWithValue("@timestamp", dr("timestamp"))
                        cmdU.ExecuteNonQuery()
                        Return True
                    End Using
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Function

    Public Sub AutoProcessKapaLog()
        Dim dtmStart As Date
        Dim dtmEnd As Date
        Dim strLog As String = "Kapa Log: " & vbCrLf
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysUpdateKapaLog.Split(",")
        Dim strSQL As String
        Dim strOsnovniNalog As String = ""


        Try

            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetKapaLogCheckDaysBack * -1, Now.Date)
            dtmEnd = DateAdd(DateInterval.Day, cls.Config.GetKapaLogCheckDaysForward, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek

            If dtmTime >= dtmStartTimeKapaLog And dtmTime <= dtmEndTimeKapaLog And strDaysUpdateKapaLog.Contains(intDayOfWeek.ToString) Then

                Call AddToActionUpdateKapaLog(Me.txtLog, "AutoProcessUpdateKapalog - začetek procesiranja")

                'nalogi
                strSQL = "SELECT DISTINCT k.atx_status, k.ATX_HINWEISSTATUS, ka.aix_ptabbelegnr, ka.aix_pisbelegnr, substring(ka.aix_vorname,1,40) as aix_vorname, substring(ka.aix_nachname,1,40) as aix_nachname, ka.aix_feineinheiten, " _
                & " ka.aix_liefertermin, ka.aix_anzpos, ka.aix_anzposfertig, ka.aix_kundennr, kms.status, kms.datum " _
                & " FROM KAPA_AUFTRAG k  " _
                & " LEFT JOIN [KlaesTools].dbo.kapa_msora_status kms ON kms.order_nr collate Latin1_General_CI_AS_WS = k.ATX_NUMMER  " _
                & " INNER JOIN KAPA_AUFTRAGINFO ka ON k.ATX_ID = ka.AIX_AT_ID  " _
                & " LEFT JOIN KAPA_EINPLANUNGSDATEN ed ON ed.ETX_ATX_ID = ka.AIX_AT_ID,  " _
                & " KAPA_PLANUNGSTEILBEREICHE kpb,  " _
                & " KAPA_TEILAUFTRAGPTBINFO kti  " _
                & " WHERE kpb.PTX_STATUS = 0 AND kti.APX_PT_ID = kpb.PTX_ID AND kpb.ptx_kuerzel = '" & cls.Config.GetKapaLinija & "' " _
                & " AND kti.APX_atx_id = ka.AIX_AT_ID  " _
                & " AND (kti.apx_liefertermin BETWEEN @dtmStart and @dtmEnd OR ed.ETX_FIXLIEFERTERMIN BETWEEN @dtmStart and @dtmEnd)  " _
                & " AND kti.apx_fertigungsende IS NOT NULL  " _
                & " ORDER BY kms.datum DESC"
                Using connKAPA As SqlConnection = GetConnection("KLAESKAPA")
                    Using connTools As SqlConnection = GetConnection("TOOLS")
                        Using cmd As New SqlCommand(strSQL, connKAPA)
                            cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                            cmd.Parameters.AddWithValue("@dtmEnd", dtmEnd)
                            Dim dt = GetData(cmd)

                            For i = 0 To dt.Rows.Count - 1
                                'primerjam z atx_status, atx_hinweisstatus, kms.datum, kms.status, aix_liefertermin
                                'ce ne obstaja, potem vstavim
                                'ce je eden razlicen, potem vstavim


                                'strSQL = "SELECT * FROM kapa_log WHERE order_nr = @order_nr AND kms_status = @kms_status AND kms_datum = @kms_datum " _
                                '    & " AND atx_status = @atx_status AND atx_hinweisstatus = @atx_hinweisstatus AND aix_liefertermin = aix_liefertermin "
                                'Using cmd2 As New SqlCommand(strSQL, connTools)
                                '    cmd2.Parameters.AddWithValue("@order_nr", dt(i)("aix_ptabbelegnr"))
                                '    cmd2.Parameters.AddWithValue("@kms_status", dt(i)("status"))
                                '    cmd2.Parameters.AddWithValue("@kms_datum", dt(i)("datum"))
                                '    cmd2.Parameters.AddWithValue("@atx_status", dt(i)("atx_status"))
                                '    cmd2.Parameters.AddWithValue("@atx_hinweisstatus", dt(i)("atx_hinweisstatus"))
                                '    cmd2.Parameters.AddWithValue("@aix_liefertermin", dt(i)("aix_liefertermin"))


                                'strSQL = "SELECT * FROM kapa_log WHERE order_nr = @order_nr " _
                                '   & " AND kms_status " & IIf(IsDBNull(dt(i)("status")), " Is Null", " = " & DB2IntZero(dt(i)("status"))) _
                                '   & " AND kms_datum " & IIf(IsDBNull(dt(i)("datum")), " Is Null", " = '" & Format(dt(i)("datum"), "yyyy-MM-dd") & "'") _
                                '   & " AND atx_status = @atx_status AND atx_hinweisstatus = @atx_hinweisstatus AND aix_liefertermin = aix_liefertermin "

                                strSQL = "SELECT * FROM kapa_log WHERE order_nr = @order_nr " _
                                   & " AND kms_status " & IIf(IsDBNull(dt(i)("status")), " Is Null", " = " & DB2IntZero(dt(i)("status"))) _
                                   & " AND atx_status = @atx_status AND atx_hinweisstatus = @atx_hinweisstatus AND aix_liefertermin = @aix_liefertermin "

                                'Debug.Print(dt(i)("aix_ptabbelegnr").ToString & vbTab & IIf(IsDBNull(dt(i)("status")), " Is Null", " = " & DB2IntZero(dt(i)("status"))))

                                Using cmd2 As New SqlCommand(strSQL, connTools)
                                    cmd2.Parameters.AddWithValue("@order_nr", dt(i)("aix_ptabbelegnr"))
                                    cmd2.Parameters.AddWithValue("@atx_status", dt(i)("atx_status"))
                                    cmd2.Parameters.AddWithValue("@atx_hinweisstatus", dt(i)("atx_hinweisstatus"))
                                    cmd2.Parameters.AddWithValue("@aix_liefertermin", dt(i)("aix_liefertermin"))


                                    Dim dt2 As DataTable = GetData(cmd2)
                                    If dt2.Rows.Count = 0 Then


                                        strSQL = "INSERT INTO kapa_log (order_nr, kms_status, kms_datum, atx_status, atx_hinweisstatus, aix_liefertermin, datum) " _
                                            & " VALUES (@order_nr, @kms_status, @kms_datum, @atx_status, @atx_hinweisstatus, @aix_liefertermin, @datum)"
                                        Using cmd3 As New SqlCommand(strSQL, connTools)
                                            cmd3.Parameters.AddWithValue("@order_nr", dt(i)("aix_ptabbelegnr"))
                                            cmd3.Parameters.AddWithValue("@kms_status", dt(i)("status"))
                                            cmd3.Parameters.AddWithValue("@kms_datum", dt(i)("datum"))
                                            cmd3.Parameters.AddWithValue("@atx_status", dt(i)("atx_status"))
                                            cmd3.Parameters.AddWithValue("@atx_hinweisstatus", dt(i)("atx_hinweisstatus"))
                                            cmd3.Parameters.AddWithValue("@aix_liefertermin", dt(i)("aix_liefertermin"))
                                            cmd3.Parameters.AddWithValue("@datum", Now)
                                            cmd3.ExecuteNonQuery()
                                        End Using

                                        Call AddToActionUpdateKapaLog(Me.txtLog, dt(i)("aix_ptabbelegnr").ToString & vbTab & " KMS Status: " & dt(i)("status").ToString _
                                                                      & vbTab & " KMS datum: " & dt(i)("datum").ToString _
                                                                      & vbTab & " ATX_status: " & dt(i)("atx_status").ToString _
                                                                      & vbTab & " ATX_HinweisStatus: " & dt(i)("atx_hinweisstatus").ToString _
                                                                      & vbTab & " AIX_LieferTermin: " & dt(i)("aix_liefertermin").ToString)

                                    End If
                                End Using

                            Next

                        End Using
                    End Using
                End Using

                Call AddToActionUpdateKapaLog(Me.txtLog, strLog)


                Call AddToActionUpdateKapaLog(Me.txtLog, "AutoProcessUpdateKapaLog - konec procesiranja")
            Else
                'Call AddToActionUpdateSklicTB(Me.txtLog, "AutoProcessUpdateSklic - Ura, dan ni vključena")
            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Sub



    Public Sub AutoProcessUpdateSlikaVrtanja(blnManual As Boolean)
        Dim dtmStart As Date
        Dim strLog As String = "Update SLIKA VRTANJA, BARVA PROFILOV: " & vbCrLf
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysUpdateSlikaVrtanja.Split(",")
        Dim strSQL As String
        Dim dtBohrShema As DataTable = Nothing
        Dim strKommNr As String = ""
        Dim strBS1 As String = ""
        Dim strBS2 As String = ""
        Dim strNewBS1 As String = ""
        Dim strNewBS2 As String = ""
        Dim strOznaka As String = ""
        Dim strKennungMarke As String = ""
        Dim strKennungMarke1 As String = ""
        Dim strKennungMarke2 As String = ""
        Dim strNewInhaltMarke As String = ""

        Try


            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetSlikaVrtanjaCheckLastDays * -1, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek

            If (dtmTime >= dtmStartTimeSlikaVrtanja And dtmTime <= dtmEndTimeSlikaVrtanja And strDaysUpdateSlikaVrtanja.Contains(intDayOfWeek.ToString)) Or blnManual Then

                Call ResetConnection()

                Call AddToActionUpdateSlikaVrtanjaTB(Me.txtLog, "AutoProcessUpdate_eProdText - začetek procesiranja " & dtmStart & " - " & Now.Date)
                Application.DoEvents()
                dtBohrShema = CreateTableEprodBohrshema()
                'naredim akcije
                'pogledam naloge, ki so bili narejeni v obdobju (eProd)
                strSQL = "SELECT DISTINCT kommissionsnummer FROM mengen WHERE DatumEintrag BETWEEN @dtmStart AND @dtmEnd "
                'strSQL = strSQL & " AND kommissionsnummer = '4_HM003'"
                Using connTools As SqlConnection = GetConnection("TOOLS")
                    Using conn As MySqlConnection = GetMyConnection()
                        Using cmd As New MySqlCommand(strSQL, conn)
                            cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                            cmd.Parameters.AddWithValue("@dtmEnd", Now.Date)
                            Dim dt As DataTable = GetMyData(cmd)

                            For i = 0 To dt.Rows.Count - 1

                                'slika vrtanja
                                strKommNr = dt(i)("kommissionsnummer").ToString.Trim

                                strSQL = "SELECT t.kommissionsnummer, t.nummerliste, t.positionsnummer, t.profilnummer, t.zeilemarke, t.kennungmarke, t.inhaltmarke, m.positionsname " _
                                   & " FROM textmarkenh t, mengen m " _
                                   & " WHERE t.kommissionsnummer = m.kommissionsnummer AND t.positionsnummer = m.positionsnummer AND t.stuecknummer = 1 " _
                                   & " AND t.kommissionsnummer = @strKomm AND t.kennungmarke = @strKennungmarke " _
                                   & " AND t.nummerliste IN (25,29) " _
                                   & " ORDER BY t.positionsnummer, t.profilnummer"

                                Using cmd2 As New MySqlCommand(strSQL, conn)

                                    cmd2.Parameters.AddWithValue("@strkomm", strKommNr)
                                    cmd2.Parameters.AddWithValue("@strKennungmarke", "7150")

                                    Dim dte As DataTable = GetMyData(cmd2)
                                    For j = 0 To dte.Rows.Count - 1
                                        Dim dr As DataRow = dtBohrShema.NewRow
                                        dr("status") = 0
                                        dr("kommissionsnummer") = strKommNr
                                        dr("nummerliste") = dte(j)("nummerliste")
                                        dr("positionsnummer") = dte(j)("positionsnummer")
                                        dr("profilnummer") = dte(j)("profilnummer")
                                        dr("zeilemarke") = dte(j)("zeilemarke")
                                        dr("kennungmarke") = dte(j)("kennungmarke")
                                        dr("inhaltmarke") = dte(j)("inhaltmarke")
                                        dr("kennungmarke1") = "7149"
                                        dr("kennungmarke2") = "7150"
                                        strBS1 = GetInhaltValue(dte(j)("inhaltmarke").ToString, "7149")
                                        strBS2 = GetInhaltValue(dte(j)("inhaltmarke").ToString, "7150")
                                        strOznaka = GetInhaltValue(dte(j)("inhaltmarke").ToString, "7064")
                                        dr("bohrshema1") = strBS1
                                        dr("bohrshema2") = strBS2
                                        strNewBS1 = GetNewBohrShema(strBS1, connTools)
                                        dr("newbohrshema1") = strNewBS1

                                        strNewBS2 = GetNewBohrShema(strBS2, connTools)
                                        dr("newbohrshema2") = strNewBS2

                                        dr("kos_oznaka") = strOznaka
                                        If (strBS1 <> strNewBS1 And strNewBS1 <> "" And strBS1 <> "") Or (strBS2 <> strNewBS2 And strNewBS2 <> "" And strBS2 <> "") Then
                                            dtBohrShema.Rows.Add(dr)
                                        End If

                                    Next
                                End Using

                                'barva profilov (37, 7075)
                                strKommNr = dt(i)("kommissionsnummer").ToString.Trim

                                strSQL = "SELECT t.kommissionsnummer, t.nummerliste, t.positionsnummer, t.profilnummer, t.zeilemarke, t.kennungmarke, t.inhaltmarke, m.positionsname " _
                                   & " FROM textmarkenh t, mengen m " _
                                   & " WHERE t.kommissionsnummer = m.kommissionsnummer AND t.positionsnummer = m.positionsnummer AND t.stuecknummer = 1 " _
                                   & " AND t.kommissionsnummer = @strKomm AND t.kennungmarke = @strKennungmarke " _
                                   & " AND t.nummerliste = @strNummerListe " _
                                   & " ORDER BY t.positionsnummer, t.profilnummer"

                                Using cmd2 As New MySqlCommand(strSQL, conn)

                                    cmd2.Parameters.AddWithValue("@strkomm", strKommNr)
                                    cmd2.Parameters.AddWithValue("@strKennungmarke", "7075")
                                    cmd2.Parameters.AddWithValue("@strNummerListe", "37")

                                    Dim dte As DataTable = GetMyData(cmd2)
                                    For j = 0 To dte.Rows.Count - 1
                                        Dim dr As DataRow = dtBohrShema.NewRow
                                        dr("status") = 0
                                        dr("kommissionsnummer") = strKommNr
                                        dr("nummerliste") = dte(j)("nummerliste")
                                        dr("positionsnummer") = dte(j)("positionsnummer")
                                        dr("profilnummer") = dte(j)("profilnummer")
                                        dr("zeilemarke") = dte(j)("zeilemarke")
                                        dr("kennungmarke") = dte(j)("kennungmarke")
                                        dr("inhaltmarke") = dte(j)("inhaltmarke")
                                        dr("kennungmarke1") = "7038"
                                        dr("kennungmarke2") = ""
                                        strBS1 = GetInhaltValue(dte(j)("inhaltmarke").ToString, "7038")
                                        strOznaka = GetInhaltValue(dte(j)("inhaltmarke").ToString, "7020")
                                        dr("bohrshema1") = strBS1
                                        strNewBS1 = GetNewProfilIzvedba(strOznaka, strBS1, connTools)
                                        dr("newbohrshema1") = strNewBS1
                                        dr("kos_oznaka") = strOznaka
                                        If (strBS1 <> strNewBS1 And strNewBS1 <> "" And strBS1 <> "") Then
                                            dtBohrShema.Rows.Add(dr)
                                        End If

                                    Next
                                End Using
                                'konec barva profilov

                                'barva profilov (38, 7133)
                                strKommNr = dt(i)("kommissionsnummer").ToString.Trim

                                strSQL = "SELECT t.kommissionsnummer, t.nummerliste, t.positionsnummer, t.profilnummer, t.zeilemarke, t.kennungmarke, t.inhaltmarke, m.positionsname " _
                                   & " FROM textmarkenh t, mengen m " _
                                   & " WHERE t.kommissionsnummer = m.kommissionsnummer AND t.positionsnummer = m.positionsnummer AND t.stuecknummer = 1 " _
                                   & " AND t.kommissionsnummer = @strKomm AND t.kennungmarke = @strKennungmarke " _
                                   & " AND t.nummerliste = @strNummerListe " _
                                   & " ORDER BY t.positionsnummer, t.profilnummer"

                                Using cmd2 As New MySqlCommand(strSQL, conn)

                                    cmd2.Parameters.AddWithValue("@strkomm", strKommNr)
                                    cmd2.Parameters.AddWithValue("@strKennungmarke", "7133")
                                    cmd2.Parameters.AddWithValue("@strNummerListe", "38")

                                    Dim dte As DataTable = GetMyData(cmd2)
                                    For j = 0 To dte.Rows.Count - 1
                                        Dim dr As DataRow = dtBohrShema.NewRow
                                        dr("status") = 0
                                        dr("kommissionsnummer") = strKommNr
                                        dr("nummerliste") = dte(j)("nummerliste")
                                        dr("positionsnummer") = dte(j)("positionsnummer")
                                        dr("profilnummer") = dte(j)("profilnummer")
                                        dr("zeilemarke") = dte(j)("zeilemarke")
                                        dr("kennungmarke") = dte(j)("kennungmarke")
                                        dr("inhaltmarke") = dte(j)("inhaltmarke")
                                        dr("kennungmarke1") = "7038"
                                        dr("kennungmarke2") = ""
                                        strBS1 = GetInhaltValue(dte(j)("inhaltmarke").ToString, "7038")
                                        strOznaka = GetInhaltValue(dte(j)("inhaltmarke").ToString, "7020")
                                        dr("bohrshema1") = strBS1
                                        strNewBS1 = GetNewProfilIzvedba(strOznaka, strBS1, connTools)
                                        dr("newbohrshema1") = strNewBS1
                                        dr("kos_oznaka") = strOznaka
                                        If (strBS1 <> strNewBS1 And strNewBS1 <> "" And strBS1 <> "") Then
                                            dtBohrShema.Rows.Add(dr)
                                        End If

                                    Next
                                End Using
                                'konec barva profilov

                                'barva profilov (32, 7026)
                                strKommNr = dt(i)("kommissionsnummer").ToString.Trim

                                strSQL = "SELECT t.kommissionsnummer, t.nummerliste, t.positionsnummer, t.profilnummer, t.zeilemarke, t.kennungmarke, t.inhaltmarke, m.positionsname " _
                                   & " FROM textmarkenh t, mengen m " _
                                   & " WHERE t.kommissionsnummer = m.kommissionsnummer AND t.positionsnummer = m.positionsnummer AND t.stuecknummer = 1 " _
                                   & " AND t.kommissionsnummer = @strKomm AND t.kennungmarke = @strKennungmarke " _
                                   & " AND t.nummerliste = @strNummerListe " _
                                   & " ORDER BY t.positionsnummer, t.profilnummer"

                                Using cmd2 As New MySqlCommand(strSQL, conn)

                                    cmd2.Parameters.AddWithValue("@strkomm", strKommNr)
                                    cmd2.Parameters.AddWithValue("@strKennungmarke", "7026")
                                    cmd2.Parameters.AddWithValue("@strNummerListe", "32")

                                    Dim dte As DataTable = GetMyData(cmd2)
                                    For j = 0 To dte.Rows.Count - 1
                                        Dim dr As DataRow = dtBohrShema.NewRow
                                        dr("status") = 0
                                        dr("kommissionsnummer") = strKommNr
                                        dr("nummerliste") = dte(j)("nummerliste")
                                        dr("positionsnummer") = dte(j)("positionsnummer")
                                        dr("profilnummer") = dte(j)("profilnummer")
                                        dr("zeilemarke") = dte(j)("zeilemarke")
                                        dr("kennungmarke") = dte(j)("kennungmarke")
                                        dr("inhaltmarke") = dte(j)("inhaltmarke")
                                        dr("kennungmarke1") = "7038"
                                        dr("kennungmarke2") = ""
                                        strBS1 = GetInhaltValue(dte(j)("inhaltmarke").ToString, "7038")
                                        strOznaka = GetInhaltValue(dte(j)("inhaltmarke").ToString, "7020")
                                        dr("bohrshema1") = strBS1
                                        strNewBS1 = GetNewProfilIzvedba(strOznaka, strBS1, connTools)
                                        dr("newbohrshema1") = strNewBS1
                                        dr("kos_oznaka") = strOznaka
                                        If (strBS1 <> strNewBS1 And strNewBS1 <> "" And strBS1 <> "") Then
                                            dtBohrShema.Rows.Add(dr)
                                        End If

                                    Next
                                End Using
                                'konec barva profilov
                            Next

                            'posodobim še bazo
                            For i = 0 To dtBohrShema.Rows.Count - 1
                                strKommNr = dtBohrShema(i)("kommissionsnummer").ToString.Trim
                                strNewInhaltMarke = dtBohrShema(i)("inhaltmarke").ToString
                                strKennungMarke = dtBohrShema(i)("kennungmarke").ToString.Trim
                                strKennungMarke1 = dtBohrShema(i)("kennungmarke1").ToString.Trim
                                strKennungMarke2 = dtBohrShema(i)("kennungmarke2").ToString.Trim

                                strBS1 = dtBohrShema(i)("bohrshema1").ToString
                                strBS2 = dtBohrShema(i)("bohrshema2").ToString

                                strNewBS1 = dtBohrShema(i)("newbohrshema1").ToString
                                strNewBS2 = dtBohrShema(i)("newbohrshema2").ToString

                                If strBS1 <> strNewBS1 Then
                                    strNewInhaltMarke = strNewInhaltMarke.Replace("""" & strKennungMarke1 & """" & ":" & """" & strBS1 & """", """" & strKennungMarke1 & """" & ":" & """" & strNewBS1 & """")
                                End If
                                If strBS2 <> strNewBS2 Then
                                    strNewInhaltMarke = strNewInhaltMarke.Replace("""" & strKennungMarke2 & """" & ":" & """" & strBS2 & """", """" & strKennungMarke2 & """" & ":" & """" & strNewBS2 & """")
                                End If

                                If strNewInhaltMarke <> dtBohrShema(i)("inhaltmarke").ToString Then
                                    strSQL = "UPDATE textmarkenh SET inhaltmarke = @strNewinhaltmarke " _
                                        & " WHERE kommissionsnummer = @strKommNr " _
                                        & " AND positionsnummer = @intPositionsNr " _
                                        & " AND nummerliste = @intListe " _
                                        & " AND kennungmarke = @strKennungmarke " _
                                        & " AND zeilemarke = @intZeileMarke " _
                                        & " AND profilnummer = @intProfilNummer "
                                    Using cmdU As New MySqlCommand(strSQL, conn)
                                        cmdU.Parameters.AddWithValue("@strNewinhaltmarke", strNewInhaltMarke)
                                        cmdU.Parameters.AddWithValue("@strKommNr", strKommNr)
                                        cmdU.Parameters.AddWithValue("@intPositionsNr", dtBohrShema(i)("positionsnummer"))
                                        cmdU.Parameters.AddWithValue("@intListe", dtBohrShema(i)("nummerliste"))
                                        cmdU.Parameters.AddWithValue("@strKennungmarke", dtBohrShema(i)("kennungmarke"))
                                        cmdU.Parameters.AddWithValue("@intZeileMarke", dtBohrShema(i)("zeilemarke"))
                                        cmdU.Parameters.AddWithValue("@intProfilNummer", dtBohrShema(i)("profilnummer"))
                                        cmdU.ExecuteNonQuery()
                                        If strBS2 <> "" Then
                                            strLog = strLog & strKommNr & " Pozicija: " & dtBohrShema(i)("positionsnummer").ToString & " Oznaka: " & dtBohrShema(i)("kos_oznaka").ToString.Trim & " = " & strBS1 & "-->" & strNewBS1 & "; " & strBS2 & "-->" & strNewBS2
                                        Else
                                            strLog = strLog & strKommNr & " Pozicija: " & dtBohrShema(i)("positionsnummer").ToString & " Oznaka: " & dtBohrShema(i)("kos_oznaka").ToString.Trim & " = " & strBS1 & "-->" & strNewBS1 & "; "
                                        End If
                                        Call AddToActionUpdateSlikaVrtanjaTB(Me.txtLog, strLog)
                                        strLog = ""
                                    End Using

                                End If

                            Next



                        End Using
                    End Using

                End Using



                Call AddToActionUpdateSlikaVrtanjaTB(Me.txtLog, strLog)
                Application.DoEvents()

                Call AddToActionUpdateSlikaVrtanjaTB(Me.txtLog, "AutoProcessUpdate_eProdText - konec procesiranja")
                Application.DoEvents()
            Else
                'Call AddToActionUpdateSklicTB(Me.txtLog, "AutoProcessUpdateSklic - Ura, dan ni vključena")
            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Sub

    Public Sub AutoProcessUpdateSklic()
        Dim dtmStart As Date
        Dim strLog As String = "Update SKLIC: " & vbCrLf
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysUpdateSklic.Split(",")
        Dim strSQL As String


        Try

            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetSklicCheckLastDays * -1, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek

            If dtmTime >= dtmStartTimeSklic And dtmTime <= dtmEndTimeSklic And strDaysUpdateSklic.Contains(intDayOfWeek.ToString) Then

                Call AddToActionUpdateSklicTB(Me.txtLog, "AutoProcessUpdateSklic - začetek procesiranja")
                Application.DoEvents()
                Using connkbs As SqlConnection = GetConnection("KLAESKBS")

                    'ponudbe
                    strSQL = "SELECT count(*) as c FROM VKBELEGSTUB WHERE BSX_IBELEGART = 10 AND bsx_erfdatum BETWEEN @dtmStart AND @dtmEnd AND BSX_19_TEXT_SKLIC IS NULL"
                    Using cmd As New SqlCommand(strSQL, connkbs)
                        cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                        cmd.Parameters.AddWithValue("@dtmEnd", Now.Date)
                        Dim dt = GetData(cmd)
                        strLog = strLog & "Ponudbe: " & dt(0)("c").ToString & vbCrLf
                    End Using

                    strSQL = "UPDATE VKBELEGSTUB set BSX_19_TEXT_SKLIC = '00 ' + SUBSTRING(bsx_belegnr,2,7) WHERE BSX_IBELEGART = 10 AND bsx_erfdatum BETWEEN @dtmStart AND @dtmEnd AND BSX_19_TEXT_SKLIC IS NULL"
                    Using cmd As New SqlCommand(strSQL, connkbs)
                        cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                        cmd.Parameters.AddWithValue("@dtmEnd", Now.Date)
                        cmd.ExecuteNonQuery()
                    End Using

                    'naročilo
                    strSQL = "SELECT count(*) as c FROM VKBELEGSTUB WHERE BSX_IBELEGART = 20 AND bsx_erfdatum BETWEEN @dtmStart AND @dtmEnd AND BSX_19_TEXT_SKLIC IS NULL"
                    Using cmd As New SqlCommand(strSQL, connkbs)
                        cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                        cmd.Parameters.AddWithValue("@dtmEnd", Now.Date)
                        Dim dt = GetData(cmd)
                        strLog = strLog & "Naročila: " & dt(0)("c").ToString & vbCrLf
                    End Using

                    strSQL = "UPDATE VKBELEGSTUB set BSX_19_TEXT_SKLIC = '00 ' + SUBSTRING(bsx_belegnr,2,6) WHERE BSX_IBELEGART = 20 AND bsx_erfdatum BETWEEN @dtmStart AND @dtmEnd AND BSX_19_TEXT_SKLIC IS NULL"
                    Using cmd As New SqlCommand(strSQL, connkbs)
                        cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                        cmd.Parameters.AddWithValue("@dtmEnd", Now.Date)
                        cmd.ExecuteNonQuery()
                    End Using

                    'računi
                    strSQL = "SELECT count(*) as c FROM VKBELEGSTUB WHERE BSX_IBELEGART IN (70,73) AND bsx_erfdatum BETWEEN @dtmStart AND @dtmEnd AND BSX_19_TEXT_SKLIC IS NULL"
                    Using cmd As New SqlCommand(strSQL, connkbs)
                        cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                        cmd.Parameters.AddWithValue("@dtmEnd", Now.Date)
                        Dim dt = GetData(cmd)
                        strLog = strLog & "Računi: " & dt(0)("c").ToString & vbCrLf
                    End Using

                    strSQL = "UPDATE VKBELEGSTUB set BSX_19_TEXT_SKLIC = '00 ' + SUBSTRING(bsx_belegnr,2,8) WHERE BSX_IBELEGART IN (70,73) AND bsx_erfdatum BETWEEN @dtmStart AND @dtmEnd AND BSX_19_TEXT_SKLIC IS NULL"
                    Using cmd As New SqlCommand(strSQL, connkbs)
                        cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                        cmd.Parameters.AddWithValue("@dtmEnd", Now.Date)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                Call AddToActionUpdateSklicTB(Me.txtLog, strLog)
                Application.DoEvents()

                Call AddToActionUpdateSklicTB(Me.txtLog, "AutoProcessUpdateSklic - konec procesiranja")
                Application.DoEvents()
            Else
                'Call AddToActionUpdateSklicTB(Me.txtLog, "AutoProcessUpdateSklic - Ura, dan ni vključena")
            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try


    End Sub

    Public Sub AutoProcess_R_Koncano()
        Dim dtmStart As Date
        Dim strLog As String = "R Končano: " & vbCrLf
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysUpdateRKoncano.Split(",")
        Dim strSQL As String


        Try

            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetRKoncanoCheckLastDays * -1, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek


            If dtmTime >= dtmStartTimeRKoncano And dtmTime <= dtmEndTimeRKoncano And strDaysUpdateRKoncano.Contains(intDayOfWeek.ToString) Then

                Call AddToActionUpdateRKoncanoTB(Me.txtLog, "AutoProcess_R_Koncano - začetek procesiranja")
                Application.DoEvents()
                Using connKBS As SqlConnection = GetConnection("KLAESKBS")
                    strSQL = "SELECT vkb.bsx_belegnr, vkb.BSX_9_DATUM_R_KON_ANO, mo.main_order_nr, mo.order_nr, mo.montage_date, mo.status, mo.montage_person_id " _
                        & " FROM vkbelegstub vkb INNER JOIN [KlaesTools].dbo.montage mo ON mo.order_nr " & cls.Config.GetCollation & " = vkb.BSX_BELEGNR " _
                        & " WHERE mo.montage_date BETWEEN @dtmStart AND @dtmEnd " _
                        & " AND (mo.main_order_nr LIKE '%R%' or vkb.BSX_4_AUSWAHL_REKLAMACIJA = 'Reklamacija') " _
                        & " AND BSX_9_DATUM_R_KON_ANO is null AND mo.status = 3"
                    Using cmd As New SqlCommand(strSQL, connKBS)
                        cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
                        cmd.Parameters.AddWithValue("@dtmEnd", Now.Date)
                        Dim dt As DataTable = GetData(cmd)
                        For i = 0 To dt.Rows.Count - 1
                            strSQL = "UPDATE vkbelegstub SET BSX_9_DATUM_R_KON_ANO = @dtmKoncano, bsx_12_auswahl_r_odpravil = @monter WHERE bsx_belegnr = @bsxBelegNr"
                            Using cmdU As New SqlCommand(strSQL, connKBS)
                                cmdU.Parameters.AddWithValue("@dtmKoncano", dt(i)("montage_date"))
                                cmdU.Parameters.AddWithValue("@monter", dt(i)("montage_person_id"))
                                cmdU.Parameters.AddWithValue("@bsxBelegNr", dt(i)("bsx_belegnr"))
                                cmdU.ExecuteNonQuery()
                                strLog = strLog & "Naročilo: " & dt(i)("bsx_belegnr").ToString & " Datum končanja montaže: " & CDate(dt(i)("montage_date")) & " Monter: " & dt(i)("montage_person_id").ToString & vbCrLf
                            End Using
                        Next
                    End Using
                End Using

                Call AddToActionUpdateRKoncanoTB(Me.txtLog, strLog)
                Application.DoEvents()
                Call AddToActionUpdateRKoncanoTB(Me.txtLog, "AutoProcessUpdateRKoncano - konec procesiranja")
                Application.DoEvents()
            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try


    End Sub



    Private Function SearchKapa(ByVal dtmFrom As Date, ByVal dtmTo As Date, connKAPA As SqlConnection) As DataTable
        Dim strSQL As String = ""
        Dim dr As DataRow = Nothing
        Dim strFile As String = ""


        Try

            strSQL = "SELECT ka.AIX_AT_ID, ka.AIX_PTABBELEGNR, ka.AIX_FERTIGUNGSBEGINN, ka.AIX_FERTIGUNGSENDE, ka.AIX_LIEFERTERMIN, " _
                    & " ka.aix_feineinheiten , kti.apx_fertigungsbeginn, kti.apx_fertigungsende, kti.apx_liefertermin, ed.ETX_FIXLIEFERTERMIN " _
                    & " FROM KAPA_AUFTRAGINFO ka  " _
                    & " LEFT JOIN KAPA_EINPLANUNGSDATEN ed ON ed.ETX_ATX_ID = ka.AIX_AT_ID, " _
                    & " KAPA_PLANUNGSTEILBEREICHE kpb, " _
                    & " KAPA_TEILAUFTRAGPTBINFO kti " _
                    & " WHERE kpb.PTX_STATUS = 0 AND kti.APX_PT_ID = kpb.PTX_ID AND  ka.AIX_LIEFERTERMIN BETWEEN @dtmOd AND @dtmDo " _
                    & " AND kti.APX_atx_id = ka.AIX_AT_ID AND kpb.ptx_kuerzel = '00001'" _
                    & " ORDER BY ka.AIX_PTABBELEGNR"



            Using cmd As New SqlCommand(strSQL, connKAPA)


                cmd.Parameters.AddWithValue("@dtmOd", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmdo", dtmTo)


                Dim adapter As New SqlDataAdapter(cmd)
                Dim dt As New DataTable

                adapter.Fill(dt)


                Return dt


            End Using

        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function

    Private Function SearchPlan(ByVal dtmFrom As Date, ByVal dtmTo As Date) As DataTable
        Dim strSQL As String = ""
        Dim dr As DataRow = Nothing
        Dim strFile As String = ""
        Dim mConn As New OleDb.OleDbConnection()
        Dim strConn As String

        Try


            strConn = cls.Config.GetConnectionStringDatabasePlan

            mConn.ConnectionString = strConn

            mConn.Open()

            strSQL = "SELECT tnal.[Številka naročila], tnal.[Številka naloga], tnar.[Stranka], max(tnal.[Končni termin]) as koncni_termin " _
                & " FROM [Tabela nalogov] tnal, [Tabela naročil] tnar " _
                & " WHERE tnal.[Številka naročila] = tnar.[Številka naročila] " _
                & " AND tnal.[Končni termin] BETWEEN @dtmOd AND @dtmDo " _
                & " GROUP BY tnal.[Številka naročila], tnal.[Številka naloga], tnar.[Stranka]"


            Using cmd As New OleDb.OleDbCommand(strSQL, mConn)


                cmd.Parameters.AddWithValue("@dtmOd", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmdo", dtmTo)


                Dim adapter As New OleDb.OleDbDataAdapter(cmd)
                Dim dt As New DataTable

                adapter.Fill(dt)


                Return dt


            End Using

        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try

    End Function

    Public Sub AutoProcessMail(Optional lngEventId As Long = -1)
        Dim dtmStart As Date
        Dim dt As DataTable
        Dim strSQL As String
        Dim dtmTime As Date
        Dim intDayOfWeek As Integer = -1
        Dim Days() As String = strDaysSendMail.Split(",")

        Try


            dtmTime = Format(Now, TimeOfDay)

            dtmStart = DateAdd(DateInterval.Day, cls.Config.GetSendMailCheckLastDays * -1, Now.Date)

            intDayOfWeek = Now.Date.DayOfWeek

            If dtmTime >= dtmStartTimeSendMail And dtmTime <= dtmEndTimeSendMail And strDaysSendMail.Contains(intDayOfWeek.ToString) Then

                Call ResetConnection()

                Call AddToActionLogSendMailTB(Me.txtLog, "AutoProcessSendMail - začetek procesiranja")

                'poiščem vse evente, ki imajo status 1



                If lngEventId > -1 Then
                    ' v vsakem primeru ga zaženem (za testiranje)
                    strSQL = "SELECT event_id FROM eprod_events WHERE event_id = " & lngEventId & " ORDER BY event_id"
                Else
                    strSQL = "SELECT event_id FROM eprod_events WHERE event_enabled = 1 ORDER BY event_id"
                End If

                Using cmd As New SqlCommand(strSQL, GetConnection("KlaesTools"))
                    dt = GetData(cmd)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1

                            Dim cEvent As New cls.event.EprodEvent(DB2Lng(dt(i)("event_id")))
                            Call AddToActionLogSendMailTB(Me.txtLog, "Procesing event: " & cEvent.EventId & " - " & cEvent.EventDescription)

                            Call ProcessEvent(cEvent, Me.txtLog)

                            Application.DoEvents()

                        Next
                    End If
                End Using

                Call AddToActionLogSendMailTB(Me.txtLog, "AutoProcessSendMail - konec procesiranja")
            Else
                Call AddToActionLogSendMailTB(Me.txtLog, "AutoProcessSendMail - Ura, dan ni vključena")
            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Sub

    Private Sub btnEProdName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEProdName.Click
        frmUpdateName.ShowDialog(Me)
    End Sub

    Private Sub tmrUpdateName_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrUpdateName.Tick
        'timer se sproži vsako minuto


        intUpdateMinutes = intUpdateMinutes - 1

        Using connTools As SqlConnection = GetConnection("TOOLS")
            Using connKBS As SqlConnection = GetConnection("KLAESKBS")

                Me.lblNextUpdateName.Text = intUpdateMinutes
                If intUpdateMinutes <= 0 Then
                    If Me.chkAutoStartName.Checked Then
                        ClearLog()
                        Call AutoProcessUpdateName(connTools, connKBS)
                        Call AutoProcessUpdateProductionText(connTools, connKBS)
                    End If
                    intUpdateMinutes = cls.Config.GetUpdateNameCheckInterval
                End If
            End Using
        End Using

    End Sub
    Private Sub tmrSendMail_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSendMail.Tick
        'timer se sproži vsako minuto


        intMailingMinutes = intMailingMinutes - 1


        Me.lblNextUpdateMail.Text = intMailingMinutes
        If intMailingMinutes <= 0 Then
            If Me.chkAutoSendMailing.Checked Then
                ClearLog()
                Call AutoProcessMail()
            End If
            intMailingMinutes = cls.Config.GetUpdateNameCheckInterval
        End If

    End Sub

    Private Sub cmdSendMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSendMail.Click
        frmSendMail.ShowDialog(Me)

    End Sub

    Private Sub cmdSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdRtfEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRtfEdit.Click
        frmTextTemplate.ShowDialog(Me)
    End Sub

    Private Sub cmdEvents_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEvents.Click
        frmEventsEditor.ShowDialog(Me)
    End Sub

    Private Sub cmdManualStartUpdateName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdManualStartUpdateName.Click
        ClearLog()
        Using connTools As SqlConnection = GetConnection("TOOLS")
            Using connKBS As SqlConnection = GetConnection("KLAESKBS")
                Call AutoProcessUpdateName(connTools, connKBS)
                Call AutoProcessUpdateProductionText(connTools, connKBS)
            End Using
        End Using

    End Sub

    Private Sub cmdManualProcesSendMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdManualProcesSendMail.Click
        ClearLog()
        Call AutoProcessMail()
    End Sub

    Private Sub tmrDeliveryDate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDeliveryDate.Tick
        'timer se sproži vsako minuto


        intDeliveryDateMinutes = intDeliveryDateMinutes - 1


        Me.lblNextUpdateDeliveryDate.Text = intDeliveryDateMinutes
        If intDeliveryDateMinutes <= 0 Then
            If Me.chkAutoChangeDeliveryDate.Checked Then
                ClearLog()
                Call AutoProcessDeliveryDate()
                
            End If
            intDeliveryDateMinutes = cls.Config.GetDeliveryDateCheckInterval
        Else
            'intDeliveryDateMinutes = cls.Config.GetDeliveryDateCheckInterval
        End If

    End Sub



    Private Sub cmdManualProcesDeliveryDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdManualProcesDeliveryDate.Click
        ClearLog()
        Call AutoProcessDeliveryDate()
    End Sub

    Private Sub cmdDeliveryDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeliveryDate.Click

        Dim dtmDate1 As DateTime = Nothing
        Dim dtmDate2 As DateTime = Nothing
        Dim strTmp As String = ""
        Dim strOldDate As String = ""


        strTmp = InputBox("Začetni datum: ", "Datum končanja OD ", Now.Date)
        If strTmp <> "" Then
            If IsDate(strTmp) Then
                dtmDate1 = CDate(strTmp)
            Else
                Exit Sub
            End If
        End If

        strTmp = ""
        strTmp = InputBox("Končni datum: ", "Datum končanja DO ", DateAdd(DateInterval.Day, cls.Config.GetDeliveryDateCheckLastDays, Now.Date))
        If strTmp <> "" Then
            If IsDate(strTmp) Then
                dtmDate2 = CDate(strTmp)
            Else
                Exit Sub
            End If
        End If


        Dim dt As DataTable = SearchKapa(dtmDate1, dtmDate2, GetConnection("KAPA"))
        Dim strNarocilo As String

        Dim dtmTerminKoncanja As DateTime
        For i = 0 To dt.Rows.Count - 1
            strNarocilo = dt(i)("AIX_PTABBELEGNR").ToString
            dtmTerminKoncanja = dt(i)("AIX_LIEFERTERMIN")

            
            If modEProd.ChangeDeliveryDate(strNarocilo, dtmTerminKoncanja, strOldDate) Then
                Call AddToActionLogDeliveryDateTB(Me.txtLog, "Naročilo " & strNarocilo & " Termin končanja: " & strOldDate & " --> " & dtmTerminKoncanja)
                txtLog.Refresh()
                Me.Refresh()
            End If

        Next



    End Sub

    Private Sub ClearLog()
        If Me.txtLog.Text.Length > 20000 Then
            txtLog.Clear()
            Call AddTextLog(Me.txtLog, "Brisanje loga:" & txtLog.Text.Length)
        Else
            Call AddTextLog(Me.txtLog, "Velikost loga:" & txtLog.Text.Length)

        End If
    End Sub

    Private Sub cmdMAWIDates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMAWIDates.Click
        frmMawiDate.ShowDialog(Me)
    End Sub

    Private Sub cmdManualProcessMAWIDates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdManualProcessMAWIDates.Click
        ClearLog()
        Call AutoProcessMAWIDates()
    End Sub

    Private Sub tmrMAWIDates_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrMAWIDates.Tick
        'timer se sproži vsako minuto


        intMAWIDateMinutes = intMAWIDateMinutes - 1


        Me.lblNextUpdateMAWIDates.Text = intMAWIDateMinutes
        If intMAWIDateMinutes <= 0 Then
            If Me.chkAutoMAWIDates.Checked Then
                ClearLog()
                Call AutoProcessMAWIDates()
            End If
            intMAWIDateMinutes = cls.Config.GetMawiDateCheckInterval
        
        End If
    End Sub



    Private Sub tmrCutterSendFile_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCutterSendFile.Tick
        intCutterSendFileMinutes = intCutterSendFileMinutes - 1


        Me.lblNextUpdateCutterSendFile.Text = intCutterSendFileMinutes
        If intCutterSendFileMinutes <= 0 Then
            If Me.chkAutoCutterSendFile.Checked Then
                ClearLog()
                Call AutoProcessCutterSendFile()
            End If
            intCutterSendFileMinutes = cls.Config.GetCutterSendFileCheckInterval
        
        End If
    End Sub

    Private Sub cmdManualProcessCutter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdManualProcessCutter.Click
        ClearLog()
        Call AutoProcessCutterSendFile()
    End Sub

    Private Sub cmdManualProcessDeleteCutterFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdManualProcessDeleteCutterFiles.Click
        ClearLog()
        Call AutoProcessCutterArchive()
    End Sub

    Private Sub cmdCuttereProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCuttereProd.Click
        frmCutterSendFile.ShowDialog(Me)
    End Sub

    Private Sub cmdCutterArchiveFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCutterArchiveFiles.Click
        frmCutterArchive.ShowDialog(Me)
    End Sub

    Private Sub tmrCutterArchive_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCutterArchive.Tick
        intCutterArchiveMinutes = intCutterArchiveMinutes - 1


        Me.lblNextUpdateCutterArchive.Text = intCutterArchiveMinutes
        If intCutterArchiveMinutes <= 0 Then
            If Me.chkAutoCheckCutterArchive.Checked Then
                ClearLog()
                Call AutoProcessCutterArchive()
            End If
            intCutterArchiveMinutes = cls.Config.GetCutterArchiveCheckInterval

        End If
    End Sub

    Private Sub tmrSklic_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSklic.Tick
        intSklicMinutes = intSklicMinutes - 1


        Me.lblNextSklic.Text = intSklicMinutes
        If intSklicMinutes <= 0 Then
            If Me.chkUpdateSklic.Checked Then
                ClearLog()
                Call AutoProcessUpdateSklic()
                Call AutoProcess_R_Koncano()
            End If
            intSklicMinutes = cls.Config.GetSklicUpdateInterval

        End If
    End Sub

    Private Sub cmdManualUpdateSklic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdManualUpdateSklic.Click
        ClearLog()
        Call AutoProcessUpdateSklic()

    End Sub

    Private Sub cmdManualUpdateMonter_Click(sender As System.Object, e As System.EventArgs) Handles cmdManualUpdateMonter.Click
        ClearLog()
        Call AutoProcessUpdateMonter()
        Call AutoProcess_R_Koncano()
    End Sub

    Private Sub tmrMonter_Tick(sender As System.Object, e As System.EventArgs) Handles tmrMonter.Tick
        intMonterMinutes = intMonterMinutes - 1


        Me.lblNextMonter.Text = intMonterMinutes
        If intMonterMinutes <= 0 Then
            If Me.chkUpdateMonterOsn.Checked Then
                ClearLog()
                Call AutoProcessUpdateMonter()
                Call AutoProcess_R_Koncano()
            End If
            intMonterMinutes = cls.Config.GetMonterUpdateInterval

        End If
    End Sub

    Private Sub tmrSlikaVrtanja_Tick(sender As System.Object, e As System.EventArgs) Handles tmrSlikaVrtanja.Tick
        intSlikaVrtanjaMinutes = intSlikaVrtanjaMinutes - 1


        Me.lblNextSlikaVrtanja.Text = intSlikaVrtanjaMinutes
        If intSlikaVrtanjaMinutes <= 0 Then
            If Me.chkEProdSlikaVrtanja.Checked Then
                ClearLog()
                Call AutoProcessUpdateSlikaVrtanja(False)
            End If

            intSlikaVrtanjaMinutes = cls.Config.GetSlikaVrtanjaUpdateInterval

        End If
    End Sub

    Private Sub cmdManualSlikaVrtanja_Click(sender As System.Object, e As System.EventArgs) Handles cmdManualSlikaVrtanja.Click
        ClearLog()
        Call AutoProcessUpdateSlikaVrtanja(True)
    End Sub

    Private Sub tmrKapaLog_Tick(sender As System.Object, e As System.EventArgs) Handles tmrKapaLog.Tick
        intKapaLogMinutes = intKapaLogMinutes - 1


        Me.lblNextKapaLog.Text = intKapaLogMinutes
        If intKapaLogMinutes <= 0 Then
            If Me.chkKapaLog.Checked Then
                ClearLog()
                Call AutoProcessKapaLog()
                Call ImportAddress()
            End If
            intKapaLogMinutes = cls.Config.GetKapaLogUpdateInterval
        End If
    End Sub

    Private Sub btnManualKapaLog_Click(sender As System.Object, e As System.EventArgs) Handles btnManualKapaLog.Click
        txtLog.Clear()
        Call AutoProcessKapaLog()
        Call ImportAddress()
    End Sub

    Private Sub tmrSpica_Tick(sender As System.Object, e As System.EventArgs) Handles tmrSpica.Tick
        intSpicaEventsMinutes = intSpicaEventsMinutes - 1


        Me.lblSpicaMinutes.Text = intSpicaEventsMinutes
        If intSpicaEventsMinutes <= 0 Then
            If Me.chkSpicaEventUpdate.Checked Then
                ClearLog()
                Call AutoProcessSpicaEvents()
            End If
            intSpicaEventsMinutes = cls.Config.GetSpicaEventUpdateInterval
        End If
    End Sub

    Private Sub cmdUpdateSpicaEvents_Click(sender As System.Object, e As System.EventArgs) Handles cmdUpdateSpicaEvents.Click
        Dim strDate As String
        strDate = InputBox("Vnesi datum od za posodobitev Špica eventov", "Špica eventi", Format(Now.Date, "Short Date"))
        If IsDate(strDate) Then
            Call UpdateSpicaEvents(CDate(strDate))
        End If
    End Sub


    Private Sub ImportGlass()


        Dim strPath As String = cls.Config.ImportGlassPath
        Dim strFile As String = ""
        Dim strOrderNr As String = ""
        Dim strStPoz As String = ""
        Dim strPolje As String = ""
        Dim strKolicina As String = ""
        Dim strSirina As String = ""
        Dim strVisina As String = ""
        Dim strStruktura As String = ""
        Dim strOznaka As String = ""
        Dim strLog As String = ""
        Dim strLine As String = ""
        Dim sLine As String = ""

        Call AddToActionUpdateImportGlass(Me.txtLog, "AutoProcessImportGlass - začetek procesiranja")



        Dim fileEntries As String() = Directory.GetFiles(strPath, "*." + cls.Config.ImportGlassFileExtension)
        ' Process the list of .txt files found in the directory. '


        Using connTools As SqlConnection = GetConnection("TOOLS")
            For Each strFile In fileEntries
                strOrderNr = ""
                If IO.File.Exists(strFile) Then

                    'preberem datoteko 
                    Dim objReader As New StreamReader(strFile)

                    Do
                        sLine = objReader.ReadLine()
                        If Not sLine Is Nothing Then
                            If strOrderNr = "" Then
                                If Mid(sLine, 1, 8) = "Del.nal." Then
                                    strOrderNr = Mid(sLine, 15, 10).Trim
                                End If
                            Else
                                'potem berem vrstice
                                strStPoz = Mid(sLine, 5, 4).Trim
                                strPolje = Mid(sLine, 10, 2).Trim
                                strKolicina = Mid(sLine, 16, 3).Trim
                                strSirina = Mid(sLine, 22, 4).Trim
                                strVisina = Mid(sLine, 27, 4).Trim
                                strStruktura = Mid(sLine, 37, 17).Trim
                                strOznaka = Mid(sLine, 57, 20).Trim

                                Call InsertRecordGlass(strOrderNr, CInt(strStPoz), CInt(strPolje), CInt(strKolicina), CInt(strSirina), CInt(strVisina), strStruktura, strOznaka)
                            End If

                        End If


                    Loop Until sLine Is Nothing
                    objReader.Close()



                End If
                'IO.File.Delete(strFile)
            Next
        End Using
        Call AddToActionUpdateImportAddress(Me.txtLog, "AutoProcessImportAddress - konec procesiranja")
    End Sub

    Private Sub InsertRecordGlass(strOrderNr As String, intPozicija As Integer, intPolje As Integer, intKolicina As Integer, intSirina As Integer, intVisina As Integer, strStruktura As String, strOznaka As String)

    End Sub


    Private Sub ImportAddress()

        If Not cls.Config.RunImportAddress Then Exit Sub

        Dim rCnt As Integer
        Dim strFilePath As String = cls.Config.ImportAddressFilePath
        Dim strStevilka As String = ""
        Dim strProjekt As String = ""
        Dim strTip As String = ""
        Dim strSQL As String = ""
        Dim strLog As String = ""
        
        Call AddToActionUpdateImportAddress(Me.txtLog, "AutoProcessImportAddress - začetek procesiranja")


        Dim strFile As String = ""

        'poiščem vse datoteke z imenom export*.xls

        For Each strFileFound In My.Computer.FileSystem.GetFiles(strFilePath, FileIO.SearchOption.SearchTopLevelOnly, "export*.txt")

            strFile = strFileFound

            'If strFilePath <> "" Then
            If IO.File.Exists(strFile) Then
                Call AddToActionUpdateImportAddress(Me.txtLog, "Uvoz naslovov iz datoteke " & strFile)
                Dim dtCSV As New DataTable

                dtCSV = ReadCSV(strFile)

                Using connTools As SqlConnection = GetConnection("TOOLS")
                    For rCnt = 0 To dtCSV.Rows.Count - 1

                        strTip = dtCSV(rCnt)("tip").ToString.Trim
                        strStevilka = dtCSV(rCnt)("stevilka").ToString.Trim
                        strProjekt = dtCSV(rCnt)("projekt").ToString.Trim

                        If strStevilka <> "" And strTip <> "" Then
                            strSQL = "SELECT * FROM doc_address WHERE stevilka = @strStevilka AND tip = @strTip AND projekt = @strProjekt "
                            Using cmdS As New SqlCommand(strSQL, connTools)
                                cmdS.Parameters.AddWithValue("@strStevilka", strStevilka)
                                cmdS.Parameters.AddWithValue("@strTip", strTip)
                                cmdS.Parameters.AddWithValue("@strProjekt", strProjekt)
                                cmdS.Parameters.AddWithValue("@datum", Now.Date)
                                Dim dt As DataTable = GetData(cmdS)
                                If dt.Rows.Count > 0 Then
                                    'update
                                    strSQL = "UPDATE doc_address SET nnagovor = @nnagovor, nime = @nime, npriimek = @npriimek, nulica = @nulica, nposta = @nposta, nkraj = @nkraj, ndrzava = @ndrzava, " _
                                        & " dnagovor = @dnagovor, dime = @dime, dpriimek = @dpriimek, dulica = @dulica, dposta = @dposta, dkraj = @dkraj, ddrzava = @ddrzava, " _
                                        & " rnagovor = @rnagovor, rime = @rime, rpriimek = @rpriimek, rulica = @rulica, rposta = @rposta, rkraj = @rkraj, rdrzava = @rdrzava, zapiski = @zapiski, datum = @datum " _
                                        & " WHERE stevilka = @stevilka "
                                    strLog = "UPDATE"
                                Else
                                    'insert
                                    strSQL = "INSERT INTO doc_address (nnagovor, nime, npriimek, nulica, nposta, nkraj, ndrzava, dnagovor, dime, dpriimek, dulica, dposta, dkraj, ddrzava, rnagovor, rime, rpriimek, rulica, rposta, rkraj, rdrzava, zapiski, datum, stevilka, projekt, tip) " _
                                        & " VALUES (@nnagovor, @nime, @npriimek, @nulica, @nposta, @nkraj, @ndrzava, @dnagovor, @dime, @dpriimek, @dulica, @dposta, @dkraj, @ddrzava, @rnagovor, @rime, @rpriimek, @rulica, @rposta, @rkraj, @rdrzava, @zapiski, @datum, @stevilka, @projekt, @tip)"
                                    strLog = "INSERT"
                                End If

                                Using cmdIU As New SqlCommand(strSQL, connTools)
                                    cmdIU.Parameters.AddWithValue("@nnagovor", dtCSV(rCnt)("nnagovor").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@nime", dtCSV(rCnt)("nime").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@npriimek", dtCSV(rCnt)("npriimek").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@nulica", dtCSV(rCnt)("nulica").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@nposta", dtCSV(rCnt)("nposta").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@nkraj", dtCSV(rCnt)("nkraj").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@ndrzava", dtCSV(rCnt)("ndrzava").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@dnagovor", dtCSV(rCnt)("dnagovor").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@dime", dtCSV(rCnt)("dime").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@dpriimek", dtCSV(rCnt)("dpriimek").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@dulica", dtCSV(rCnt)("dulica").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@dposta", dtCSV(rCnt)("dposta").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@dkraj", dtCSV(rCnt)("dkraj").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@ddrzava", dtCSV(rCnt)("ddrzava").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@rnagovor", dtCSV(rCnt)("rnagovor").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@rime", dtCSV(rCnt)("rime").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@rpriimek", dtCSV(rCnt)("rpriimek").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@rulica", dtCSV(rCnt)("rulica").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@rposta", dtCSV(rCnt)("rposta").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@rkraj", dtCSV(rCnt)("rkraj").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@rdrzava", dtCSV(rCnt)("rdrzava").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@zapiski", dtCSV(rCnt)("zapiski").ToString.Trim)
                                    cmdIU.Parameters.AddWithValue("@datum", Now.Date)

                                    cmdIU.Parameters.AddWithValue("@stevilka", strStevilka)
                                    cmdIU.Parameters.AddWithValue("@projekt", strProjekt)

                                    cmdIU.Parameters.AddWithValue("@tip", strTip)
                                    cmdIU.ExecuteNonQuery()

                                    strLog = strLog & " - " & strStevilka & " - " & strProjekt

                                    Call AddToActionUpdateImportAddress(Me.txtLog, strLog)
                                End Using
                            End Using

                        End If
                    Next
                End Using


                Call AddToActionUpdateImportAddress(Me.txtLog, "Brisanje datoteke " & strFile)

                IO.File.Delete(strFile)
                'End If
            End If
        Next

        Call AddToActionUpdateImportAddress(Me.txtLog, "AutoProcessImportAddress - konec procesiranja")
    End Sub

    Private Function ReadCSV(ByVal path As String) As DataTable
        Try

            Dim dt As New DataTable

            Using myReader As New FileIO.TextFieldParser(path)
                myReader.TextFieldType = FileIO.FieldType.Delimited
                myReader.SetDelimiters(",")
                myReader.HasFieldsEnclosedInQuotes = True

                dt.Columns.Add("nnagovor", Type.GetType("System.String"))
                dt.Columns.Add("nime", Type.GetType("System.String"))
                dt.Columns.Add("npriimek", Type.GetType("System.String"))
                dt.Columns.Add("nulica", Type.GetType("System.String"))
                dt.Columns.Add("nposta", Type.GetType("System.String"))
                dt.Columns.Add("nkraj", Type.GetType("System.String"))
                dt.Columns.Add("ndrzava", Type.GetType("System.String"))
                dt.Columns.Add("dnagovor", Type.GetType("System.String"))
                dt.Columns.Add("dime", Type.GetType("System.String"))
                dt.Columns.Add("dpriimek", Type.GetType("System.String"))
                dt.Columns.Add("dulica", Type.GetType("System.String"))
                dt.Columns.Add("dposta", Type.GetType("System.String"))
                dt.Columns.Add("dkraj", Type.GetType("System.String"))
                dt.Columns.Add("ddrzava", Type.GetType("System.String"))
                dt.Columns.Add("rnagovor", Type.GetType("System.String"))
                dt.Columns.Add("rime", Type.GetType("System.String"))
                dt.Columns.Add("rpriimek", Type.GetType("System.String"))
                dt.Columns.Add("rulica", Type.GetType("System.String"))
                dt.Columns.Add("rposta", Type.GetType("System.String"))
                dt.Columns.Add("rkraj", Type.GetType("System.String"))
                dt.Columns.Add("rdrzava", Type.GetType("System.String"))
                dt.Columns.Add("tip", Type.GetType("System.String"))
                dt.Columns.Add("stevilka", Type.GetType("System.String"))
                dt.Columns.Add("projekt", Type.GetType("System.String"))
                dt.Columns.Add("zapiski", Type.GetType("System.String"))

                Do While Not myReader.EndOfData
                    Dim myData() As String = myReader.ReadFields
                    Dim dr As DataRow = dt.NewRow
                    dr("tip") = myData(0).ToString
                    dr("stevilka") = myData(1).ToString
                    dr("projekt") = myData(2).ToString
                    dr("nnagovor") = myData(3).ToString
                    dr("nime") = myData(4).ToString
                    dr("npriimek") = myData(5).ToString
                    dr("nulica") = myData(6).ToString
                    dr("nposta") = myData(7).ToString
                    dr("nkraj") = myData(8).ToString
                    dr("ndrzava") = myData(9).ToString
                    dr("dnagovor") = myData(10).ToString
                    dr("dime") = myData(11).ToString
                    dr("dpriimek") = myData(12).ToString
                    dr("dulica") = myData(13).ToString
                    dr("dposta") = myData(14).ToString
                    dr("dkraj") = myData(15).ToString
                    dr("ddrzava") = myData(16).ToString
                    dr("rnagovor") = myData(17).ToString
                    dr("rime") = myData(18).ToString
                    dr("rpriimek") = myData(19).ToString
                    dr("rulica") = myData(20).ToString
                    dr("rposta") = myData(21).ToString
                    dr("rkraj") = myData(22).ToString
                    dr("rdrzava") = myData(23).ToString
                    dr("zapiski") = myData(24).ToString
                    dt.Rows.Add(dr)
                Loop
            End Using
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    
    Private Sub chkAutoSendMailing_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAutoSendMailing.CheckedChanged

    End Sub

    Private Sub tmrLockTechnicalOrders_Tick(sender As System.Object, e As System.EventArgs) Handles tmrLockTechnicalOrders.Tick
        intTechnicalLockMinutes = intTechnicalLockMinutes - 1


        Me.lblLockTechnicalOrders.Text = intTechnicalLockMinutes
        If intTechnicalLockMinutes <= 0 Then
            If Me.chkLockTechnicalOrders.Checked Then
                ClearLog()
                Call AutoProcessTechnicalLocks()
            End If
            intTechnicalLockMinutes = cls.Config.GetTechnicalLockUpdateInterval
        End If
    End Sub

    Private Sub btnLockTechnicalOrders_Click(sender As System.Object, e As System.EventArgs) Handles btnLockTechnicalOrders.Click
        txtLog.Clear()
        Call AutoProcessTechnicalLocks()
    End Sub

    Private Sub cmdFirebase_Click(sender As System.Object, e As System.EventArgs) Handles cmdFirebase.Click


    End Sub
End Class
