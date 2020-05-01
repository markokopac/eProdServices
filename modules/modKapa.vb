Imports eProdService.cls.msora.DB_MSora
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient

Module modKapa

    Public Function UpdateKapaPCT(dtmFrom As Date, dtmTo As Date, connKAPA As SqlConnection) As String
        Dim strSQL As String = ""
        Dim strLog As String = ""
        Dim strOrderNr As String = ""
        Dim intPosFertigEprod As Integer = 0
        Dim intPosFertigKapa As Integer = 0
        Dim intPosAnzahlKapa As Integer = 0
        Dim dtmLieferTermin As Date

        Try

            strSQL = "SELECT ka.aix_kundennr, ka.aix_nachname, ka.aix_vorname, ka.aix_ptabbelegnr, ka.aix_liefertermin, ka.aix_anzposfertig, ka.aix_anzpos " _
                        & " FROM KAPA_AUFTRAG k, KAPA_AUFTRAGINFO ka " _
                        & " LEFT JOIN KAPA_EINPLANUNGSDATEN ed ON ed.ETX_ATX_ID = ka.AIX_AT_ID, " _
                        & " KAPA_PLANUNGSTEILBEREICHE kpb,  " _
                        & " KAPA_TEILAUFTRAGPTBINFO kti " _
                        & " WHERE k.atx_id = ka.aix_at_id AND kti.APX_PT_ID = kpb.PTX_ID " _
                        & " AND kti.APX_atx_id = ka.AIX_AT_ID  " _
                        & " AND ka.aix_liefertermin BETWEEN @dtmFrom AND @dtmTo " _
                        & " AND ka.aix_anzpos = ka.aix_anzposfertig "

            Dim dtKapa As DataTable = Nothing
            'Using connE As MySqlConnection = GetMyConnection("EPROD")

            Using cmd As New SqlCommand(strSQL, connKAPA)
                cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                dtKapa = GetData(cmd)
                For i = 0 To dtKapa.Rows.Count - 1
                    strOrderNr = dtKapa(i)("aix_ptabbelegnr").ToString
                    intPosAnzahlKapa = DB2IntZero(dtKapa(i)("aix_anzpos"))
                    intPosFertigKapa = DB2IntZero(dtKapa(i)("aix_anzposfertig"))
                    dtmLieferTermin = dtKapa(i)("aix_liefertermin")
                    'preverim, če je je kaj bookirano
                    If eProdService.GetOrderStatus(strOrderNr, 11, gConnEProd) < 100 Then

                        intPosFertigEprod = GetFinnishedPositions(strOrderNr, 11, gConnEProd)

                        strSQL = "UPDATE KAPA_AUFTRAGINFO SET AIX_ANZPOSFERTIG = @intPosFerig " _
                                    & " WHERE AIX_AT_ID IN (SELECT ATX_ID FROM KAPA_AUFTRAG WHERE ATX_NUMMER = @strNewOrderNr)"
                        Using cmd1 As New SqlCommand(strSQL, connKAPA)
                            cmd1.Parameters.AddWithValue("@intPosFerig", intPosFertigEprod)
                            cmd1.Parameters.AddWithValue("@strNewOrderNr", strOrderNr)
                            cmd1.ExecuteNonQuery()
                        End Using

                        strSQL = "UPDATE KAPA_TEILAUFTRAGPTBINFO SET APX_ANZPOSFERTIG = @intPosFerig " _
                                    & " WHERE APX_ATX_ID IN (SELECT ATX_ID FROM KAPA_AUFTRAG WHERE ATX_NUMMER = @strNewOrderNr)"
                        Using cmd1 As New SqlCommand(strSQL, connKAPA)
                            cmd1.Parameters.AddWithValue("@intPosFerig", intPosFertigEprod)
                            cmd1.Parameters.AddWithValue("@strNewOrderNr", strOrderNr)
                            cmd1.ExecuteNonQuery()
                        End Using
                        strLog = strLog & vbCrLf & strOrderNr & " (" & intPosAnzahlKapa.ToString & "/" & intPosFertigKapa.ToString & ") " & CDate(dtmLieferTermin.ToString) & " --> " & intPosFertigEprod.ToString & "; "
                    End If

                Next
            End Using

            'End Using
            Return strLog
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return strLog
        End Try

    End Function

    Public Function GetKAPAReservedOrders(dtmStart As Date, dtmTo As Date, connKAPA As SqlConnection) As DataTable
        Dim strSQL As String = ""

        Dim dtmProductionStart As Date = Nothing
        Dim dt As DataTable = Nothing



        strSQL = "SELECT ka.aix_ptabbelegnr as auftragsnummer, ka.aix_pisbelegnr, substring(ka.aix_vorname,1,40) as aix_vorname, substring(ka.aix_nachname,1,40) as aix_nachname, ka.aix_feineinheiten, " _
            & " kti.apx_fertigungsbeginn, kti.apx_fertigungsende, kti.apx_liefertermin, ka.aix_liefertermin, ed.ETX_FIXLIEFERTERMIN, ka.aix_anzpos, ka.aix_anzposfertig, ka.aix_kundennr " _
            & " FROM KAPA_AUFTRAGINFO ka " _
            & " LEFT JOIN KAPA_EINPLANUNGSDATEN ed ON ed.ETX_ATX_ID = ka.AIX_AT_ID, " _
            & " KAPA_PLANUNGSTEILBEREICHE kpb, " _
            & " KAPA_TEILAUFTRAGPTBINFO kti " _
            & " WHERE(kpb.PTX_STATUS = 0 And kti.APX_PT_ID = kpb.PTX_ID) " _
            & " AND kti.APX_atx_id = ka.AIX_AT_ID AND kpb.ptx_kuerzel = '00001'" _
            & " AND (kti.apx_liefertermin BETWEEN @dtmStart and @dtmTo OR ed.ETX_FIXLIEFERTERMIN BETWEEN @dtmStart and @dtmTo) " _
            & " AND kti.apx_fertigungsende IS NOT NULL AND ka.aix_pisbelegnr IS NULL"

        Using cmd As New SqlCommand(strSQL, connKAPA)
            cmd.Parameters.AddWithValue("@dtmStart", dtmStart)
            cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
            dt = GetData(cmd)
        End Using

        Return dt

    End Function

End Module
