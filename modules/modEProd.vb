
Imports eProdService.cls.msora.DB_MSora
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO

Module modEProd

    Public gConnEProd As MySqlConnection
    Public mstrCurrentOrderNr As String
    Public mdtText As DataTable

    Public Enum Stations
        Letvice = 1
        Lepljenci = 2
        NaknadnaObdelava = 3
        StrojnaObdelava = 4
        PovrsinskaObdelava = 5
        Stiskalnica = 6
        Okovje = 7
        SprejemMateriala = 8
        Steklenje = 9
        Mize = 10
        KoncnaKontrola = 11
        Odprema = 12
    End Enum

    Public Function CreateTableTexts() As DataTable
        Dim dt As New DataTable("Texts")

        Dim column As DataColumn = New DataColumn

        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "auftragsnummer"
            .Caption = "auftragsnummer"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "Kommissionsnummer"
            .Caption = "Kommissionsnummer"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "teilekennung"
            .Caption = "teilekennung"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "Zuordnung"
            .Caption = "Zuordnung"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "Positionsnummer"
            .Caption = "Positionsnummer"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "ZeileMarke"
            .Caption = "ZeileMarke"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "ProfilNummer"
            .Caption = "ProfilNummer"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "KennungMarke"
            .Caption = "KennungMarke"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "InhaltMarke"
            .Caption = "InhaltMarke"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "ShortInhaltMarke"
            .Caption = "ShortInhaltMarke"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "LongInhaltMarke"
            .Caption = "LongInhaltMarke"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Decimal")
            .ColumnName = "Quantity"
            .Caption = "Quantity"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "Unit"
            .Caption = "Unit"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.DateTime")
            .ColumnName = "Date0"
            .Caption = "Date0"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.DateTime")
            .ColumnName = "Date1"
            .Caption = "Date1"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)
        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.DateTime")
            .ColumnName = "Date2"
            .Caption = "Date2"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "compare_density"
            .Caption = "compare_density"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        Return dt
    End Function

    Public Sub ResetConnection()
        Try
            'gConnEProd.Close()
            gConnEProd = Nothing
            gConnEProd = GetMyConnection("eprod")
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)

            'poslati še mail

            SendEmailMailbee("marko.kopac@m-sora.si", "minfo@m-sora.si", "Napaka programa Servis", "NAPAKA Reset Connection", "minfo@m-sora.si", "infom2017", ex.ToString, Nothing, "vhost01.stelkom.eu", "465")

            End
        End Try

    End Sub

    Public Function CreateTableOrdersName() As DataTable
        Dim dt As New DataTable("Orders")

        Dim column As DataColumn = New DataColumn

        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "auftragsnummer"
            .Caption = "Naročilo"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "Kommissionsnummer"
            .Caption = "Nalog"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.DateTime")
            .ColumnName = "DatumEintrag"
            .Caption = "Datum"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "Old_name"
            .Caption = "Staro"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "New_name"
            .Caption = "Novo"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "reclamation"
            .Caption = "reclamation"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        Return dt
    End Function

    Public Function CreateTableCutterFiles() As DataTable
        Dim dt As New DataTable("Orders")

        Dim column As DataColumn = New DataColumn

        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "auftragsnummer"
            .Caption = "Naročilo"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "Kommissionsnummer"
            .Caption = "Nalog"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "filename"
            .Caption = "Datoteka"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)


        Return dt
    End Function
    Public Function CreateTableCutterFilesStatus() As DataTable
        Dim dt As New DataTable("Orders")

        Dim column As DataColumn = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "filename"
            .Caption = "Datoteka"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "status"
            .Caption = "Status"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)


        Return dt
    End Function

    Public Function CreateTableMAWIOrdersShort() As DataTable
        Dim dt As New DataTable("Orders")

        Dim column As DataColumn = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "contract_nr"
            .Caption = "Narocilo"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "confirmation_nr"
            .Caption = "Potrditev"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.DateTime")
            .ColumnName = "confirmation_date"
            .Caption = "Datum"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "articles"
            .Caption = "Artikli"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

       
        Return dt
    End Function


    Public Sub GetTextsDM8ForOrder(ByVal strOrderNr As String, ByVal strKommissionsNr As String, ByVal intProfilNummer As Integer, ByVal intPositionsNummer As Integer, ByVal strArticleCode As String, ByRef dtText As DataTable)


        Dim dr As DataRow = Nothing

        Dim strSQL As String = ""
        strSQL = "SELECT DISTINCT t.auftragsnummer, t.kommissionsnummer, t.zuordnung, " _
            & " t.kennungmarke, t.inhaltmarke, t.profilnummer, t.positionsnummer, t.zeilemarke " _
            & " FROM textmarken t " _
            & " WHERE t.auftragsnummer = @strOrderNr AND t.kommissionsnummer = @strKommissionsNr " _
            & " AND t.nummerliste IN (44,45,46,47,51) AND t.kennungmarke = 8001 " _
            & " AND t.profilnummer = @intProfilNummer " _
            & " AND t.inhaltmarke = @strArticleCode " _
            & " AND t.positionsnummer = @intPositionsNummer"

        Using cmd As New MySqlCommand(strSQL, gConnEProd)
            cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
            cmd.Parameters.AddWithValue("@strKommissionsNr", strKommissionsNr)
            cmd.Parameters.AddWithValue("@intProfilNummer", intProfilNummer)
            cmd.Parameters.AddWithValue("@strArticleCode", strArticleCode)
            cmd.Parameters.AddWithValue("@intPositionsNummer", intPositionsNummer)
            Dim dt As DataTable = GetMyData(cmd)

            'za vsak zapis poiščem dolg opis
            For i = 0 To dt.Rows.Count - 1
                dr = dtText.NewRow
                dr("auftragsnummer") = dt(i)("auftragsnummer")
                dr("kommissionsnummer") = dt(i)("kommissionsnummer")
                dr("zuordnung") = dt(i)("zuordnung")
                'dr("teilekennung") = dt(i)("teilekennung")
                dr("kennungmarke") = dt(i)("kennungmarke")
                dr("inhaltmarke") = dt(i)("inhaltmarke")
                dr("zeilemarke") = dt(i)("zeilemarke")
                dr("profilnummer") = dt(i)("profilnummer")
                dr("positionsnummer") = dt(i)("positionsnummer")

                dr("shortinhaltmarke") = GetInhaltMarke(dt(i)("kommissionsnummer").ToString, dt(i)("auftragsnummer").ToString, DB2Int(dt(i)("positionsnummer")), 8002, dt(i)("zuordnung").ToString)

                dr("longinhaltmarke") = GetLongDescription(dt(i)("kommissionsnummer").ToString, dt(i)("auftragsnummer").ToString, 31, DB2Int(dt(i)("positionsnummer")), 8006, dt(i)("zuordnung").ToString)

                dr("quantity") = GetInhaltMarke(dt(i)("kommissionsnummer").ToString, dt(i)("auftragsnummer").ToString, DB2Int(dt(i)("positionsnummer")), 8003, dt(i)("zuordnung").ToString)
                dr("unit") = GetInhaltMarke(dt(i)("kommissionsnummer").ToString, dt(i)("auftragsnummer").ToString, DB2Int(dt(i)("positionsnummer")), 8004, dt(i)("zuordnung").ToString)

                dtText.Rows.Add(dr)

            Next

        End Using


    End Sub

    Public Sub GetTextsDM8ForOrderNew(ByVal strOrderNr As String, ByVal strKommissionsNr As String, ByVal intProfilNummer As Integer, ByVal intPositionsNummer As Integer, ByVal strArticleCode As String, ByRef dtText As DataTable)


        Dim dr As DataRow = Nothing
        Dim dtInhaltMarke As DataTable



        Dim strSQL As String = ""
        strSQL = "SELECT DISTINCT t.auftragsnummer, t.kommissionsnummer, t.zuordnung, " _
            & " t.kennungmarke, t.inhaltmarke, t.profilnummer, t.positionsnummer, t.zeilemarke " _
            & " FROM textmarkenz t " _
            & " WHERE t.auftragsnummer = @strOrderNr AND t.kommissionsnummer = @strKommissionsNr " _
            & " AND t.nummerliste IN (41) AND t.kennungmarke = 8007" _
            & " AND t.profilnummer = @intProfilNummer " _
            & " AND t.positionsnummer = @intPositionsNummer"

        Using cmd As New MySqlCommand(strSQL, gConnEProd)
            cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
            cmd.Parameters.AddWithValue("@strKommissionsNr", strKommissionsNr)
            cmd.Parameters.AddWithValue("@intProfilNummer", intProfilNummer)
            cmd.Parameters.AddWithValue("@intPositionsNummer", intPositionsNummer)
            Dim dt As DataTable = GetMyData(cmd)

            'dobiti moram samo en zapis...
            If dt.Rows.Count > 0 Then
                dr = dtText.NewRow
                dr("auftragsnummer") = dt(0)("auftragsnummer")
                dr("kommissionsnummer") = dt(0)("kommissionsnummer")
                dr("zuordnung") = dt(0)("zuordnung")
                dr("kennungmarke") = dt(0)("kennungmarke")
                dr("inhaltmarke") = dt(0)("inhaltmarke")
                dr("zeilemarke") = dt(0)("zeilemarke")
                dr("profilnummer") = dt(0)("profilnummer")
                dr("positionsnummer") = dt(0)("positionsnummer")

                Dim strTemp As String = dt(0)("inhaltmarke").ToString.Trim
                strTemp = strTemp.Substring(1)
                strTemp = strTemp.Substring(0, strTemp.Length - 1)

                Dim blnError As Boolean = False
                dtInhaltMarke = CreateArrayInhalt(strTemp, blnError)

                If Not blnError Then
                    For i = 0 To dtInhaltMarke.Rows.Count - 1
                        '{"2001":"T161598","8004":"kos","8002":"žaluzija tip IZO MEDLE","8001":"ŽALUZIJE IZO MEDLE","8007":"1.000"}
                        Select Case dtInhaltMarke(i)("kennungmarke").ToString.Trim
                            Case "8002"
                                dr("longinhaltmarke") = dtInhaltMarke(i)("inhaltmarke").ToString.Trim
                            Case "8001"
                                dr("shortinhaltmarke") = dtInhaltMarke(i)("inhaltmarke").ToString.Trim
                            Case "2001"
                            Case "8004"
                                dr("unit") = dtInhaltMarke(i)("inhaltmarke").ToString.Trim
                            Case "8007"
                                dr("quantity") = dtInhaltMarke(i)("inhaltmarke").ToString.Trim
                        End Select
                    Next

                    dtText.Rows.Add(dr)

                End If
            End If
        End Using


    End Sub


    Private Function CreateArrayInhalt(strInhaltMarke As String, ByRef blnError As Boolean) As DataTable
        'najprej kreiram tabelo
        Dim dt As New DataTable("InhaltMarke")
        Try

            Dim column As DataColumn = New DataColumn
            With column
                .DataType = System.Type.GetType("System.String")
                .ColumnName = "Kennungmarke"
                .Caption = "Oznaka"
                .ReadOnly = False
                .Unique = False
                .DefaultValue = DBNull.Value
            End With
            dt.Columns.Add(column)

            column = New DataColumn
            With column
                .DataType = System.Type.GetType("System.String")
                .ColumnName = "Inhaltmarke"
                .Caption = "Vsebina"
                .ReadOnly = False
                .Unique = False
                .DefaultValue = DBNull.Value
            End With
            dt.Columns.Add(column)

            'grem po vrsti, ne morem dati z .split, ker so vsebovani tudi ","

            Dim strK As String = ""
            Dim strI As String = ""
            Dim strTemp As String = ""
            Dim dr As DataRow = Nothing
            Dim i As Integer
            Dim j As Integer

            strTemp = strInhaltMarke


            Do While strTemp <> ""
                i = InStr(1, strTemp, """")
                j = InStr(i + 1, strTemp, """")
                strK = Mid(strTemp, i + 1, j - 2)
                strTemp = Mid(strTemp, j + 2)
                i = InStr(1, strTemp, """")
                j = InStr(i + 1, strTemp, """")
                strI = Mid(strTemp, i + 1, j - 2)
                strTemp = Mid(strTemp, j + 2)
                If strK <> "" Then
                    dr = dt.NewRow
                    dr("Kennungmarke") = strK
                    dr("Inhaltmarke") = strI
                    dt.Rows.Add(dr)
                End If
                strK = ""
                strI = ""
            Loop

            blnError = False

            Return dt
        Catch ex As Exception
            blnError = True
            Return Nothing
        End Try

    End Function

   
    Private Function GetLongDescription(ByVal strKommissionsNummer As String, ByVal strAuftragsNummer As String, ByVal intNummerListe As Integer, ByVal intPossitionsNummer As String, ByVal intKennungMarke As Integer, ByVal strZuordnung As String) As String
        Dim strSQL As String = ""
        strSQL = "SELECT inhaltmarke FROM textmarken WHERE kommissionsnummer = @strKommissionsNummer AND auftragsnummer = @strAuftragsNummer " _
            & " AND nummerliste = @intNummerListe AND positionsnummer = @intPossitionsNummer AND kennungmarke = @intKennungMarke AND zuordnung = @strZuordnung "

        Using cmd As New MySqlCommand(strSQL, gConnEProd)
            cmd.Parameters.AddWithValue("@strKommissionsNummer", strKommissionsNummer)
            cmd.Parameters.AddWithValue("@strAuftragsNummer", strAuftragsNummer)
            cmd.Parameters.AddWithValue("@intNummerListe", intNummerListe)
            cmd.Parameters.AddWithValue("@intPossitionsNummer", intPossitionsNummer)
            cmd.Parameters.AddWithValue("@intKennungMarke", intKennungMarke)
            cmd.Parameters.AddWithValue("@strZuordnung", strZuordnung)
            Dim dt As DataTable = GetMyData(cmd)
            If dt.Rows.Count > 0 Then
                Return dt(0)("inhaltmarke").ToString
            Else
                Return ""
            End If

        End Using
    End Function

    Private Function GetInhaltMarke(ByVal strKommissionsNummer As String, ByVal strAuftragsNummer As String, ByVal intPossitionsNummer As String, ByVal intKennungMarke As Integer, ByVal strZuordnung As String) As String
        Dim strSQL As String = ""
        strSQL = "SELECT inhaltmarke FROM textmarken WHERE kommissionsnummer = @strKommissionsNummer AND auftragsnummer = @strAuftragsNummer " _
            & " AND nummerliste IN (44, 45, 46, 47, 51) AND positionsnummer = @intPossitionsNummer AND kennungmarke = @intKennungMarke AND zuordnung = @strZuordnung "

        Using cmd As New MySqlCommand(strSQL, gConnEProd)
            cmd.Parameters.AddWithValue("@strKommissionsNummer", strKommissionsNummer)
            cmd.Parameters.AddWithValue("@strAuftragsNummer", strAuftragsNummer)
            cmd.Parameters.AddWithValue("@intPossitionsNummer", intPossitionsNummer)
            cmd.Parameters.AddWithValue("@intKennungMarke", intKennungMarke)
            cmd.Parameters.AddWithValue("@strZuordnung", strZuordnung)
            Dim dt As DataTable = GetMyData(cmd)
            If dt.Rows.Count > 0 Then
                Return dt(0)("inhaltmarke").ToString
            Else
                Return ""
            End If

        End Using
    End Function




    Public Function SearchOrdersChangeName(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal strNarocilo As String, ByVal strNalog As String, ByVal strIme As String, ByVal blnShowDifferentOnly As Boolean, connKBS As SqlConnection) As DataTable
        Dim dt As DataTable = Nothing
        Dim dt1 As DataTable = Nothing
        Dim strSQL As String
        Dim dr As DataRow
        Dim strWhere As String = ""
        Dim strOldName As String = ""
        Dim strNewName As String = ""
        Dim strProductionText As String = ""
        Dim dtmModDate As DateTime = Nothing

        Try

            dt = CreateTableOrdersName()

            strWhere = " 2 > 1 "

            If strNarocilo <> "" Then
                strWhere = strWhere & " AND me.auftragsnummer LIKE '" & strNarocilo.Replace("*", "%") & "' "
            End If

            If strNalog <> "" Then
                strWhere = strWhere & " AND me.kommissionsnummer LIKE '" & strNalog.Replace("*", "%") & "' "
            End If

            strSQL = "SELECT DISTINCT me.kommissionsnummer, me.auftragsnummer, me.datumeintrag, ex.reclamation " _
                & " FROM Mengen me LEFT JOIN msora_extra ex ON me.kommissionsnummer = ex.kommisionsnummer  " _
                & " AND me.auftragsnummer = ex.auftragsnummer" _
                & " WHERE  "

            If dtmFrom.Year > 1 Then
                strWhere = strWhere & " AND me.datumeintrag >= @dtmFrom "
            End If
            If dtmTo.Year > 1 Then
                strWhere = strWhere & " AND me.datumeintrag <= @dtmTo "
            End If

            If strWhere <> "" Then
                strSQL = strSQL & strWhere & " ORDER BY me.kommissionsnummer, me.datumeintrag "

                'conn = cls.msora.DB_MSora.GetMyConnection("eProd")
                Using cmd As New MySqlCommand(strSQL, gConnEProd)
                    If dtmFrom.Year > 1 Then
                        cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                    End If
                    If dtmTo.Year > 1 Then
                        cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                    End If
                    dt1 = cls.msora.DB_MSora.GetMyData(cmd)
                End Using






                For i = 0 To dt1.Rows.Count - 1
                    strOldName = GetOrderProperties(dt1(i)("auftragsnummer").ToString, dt1(i)("kommissionsnummer").ToString, "OldName", connKBS)
                    strNewName = GetOrderProperties(dt1(i)("auftragsnummer").ToString, dt1(i)("kommissionsnummer").ToString, "Name", connKBS)
                    'strProductionText = GetOrderProperties(dt1(i)("auftragsnummer").ToString, dt1(i)("kommissionsnummer").ToString, "ProductionText", conn)
                    'dtmModDate = GetFax_ModDate(dt1(i)("kommissionsnummer").ToString)


                    If strOldName.Trim <> strNewName.Trim Or Not blnShowDifferentOnly Then
                        dr = dt.NewRow
                        dr("auftragsnummer") = dt1(i)("auftragsnummer")
                        dr("kommissionsnummer") = dt1(i)("kommissionsnummer")
                        dr("datumeintrag") = dt1(i)("datumeintrag")
                        dr("old_name") = strOldName
                        dr("new_name") = strNewName
                        'dr("production_text") = strProductionText
                        'dr("fax_moddate") = dtmModDate

                        Select Case DB2Int(dt1(i)("reclamation"))
                            Case 0
                                dr("reclamation") = ""
                            Case 1
                                dr("reclamation") = "Reklamacija"
                            Case 2
                                dr("reclamation") = "Napaka"
                        End Select

                        dt.Rows.Add(dr)
                    End If
                Next

            End If

            Return dt

        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)

            Return Nothing
        End Try

    End Function

    Public Function SearchOrdersProductionText(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal strNalog As String, ByVal strIme As String, ByVal blnShowDifferentOnly As Boolean, connKBS As SqlConnection) As DataTable
        Dim dt As DataTable = Nothing
        Dim dt1 As DataTable = Nothing
        Dim strSQL As String
        Dim strWhere As String = ""
        Dim strOldName As String = ""
        Dim strNewName As String = ""
        Dim strProductionText As String = ""

        Try

            strWhere = " 2 > 1 "


            If strNalog <> "" Then
                strWhere = strWhere & " AND me.kommissionsnummer LIKE '" & strNalog.Replace("*", "%") & "' "
            End If

            strSQL = "SELECT distinct fax_auftragnr, fax_moddate, fax_fertigungtext, fax_beleg_vorname, fax_beleg_name, fax_beleg_anrede " _
                & " FROM FERTIGUNGSAUFTRAG " _
                & " WHERE  "

            If dtmFrom.Year > 1 Then
                strWhere = strWhere & " AND fax_moddate >= @dtmFrom "
            End If
            If dtmTo.Year > 1 Then
                strWhere = strWhere & " AND fax_moddate <= @dtmTo "
            End If

            If strWhere <> "" Then
                strSQL = strSQL & strWhere & " ORDER BY fax_auftragnr, fax_moddate "

                'conn = cls.msora.DB_MSora.GetMyConnection("eProd")
                Using cmd As New SqlCommand(strSQL, connKBS)
                    If dtmFrom.Year > 1 Then
                        cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                    End If
                    If dtmTo.Year > 1 Then
                        cmd.Parameters.AddWithValue("@dtmTo", DateAdd("d", 1, dtmTo))
                    End If
                    dt1 = cls.msora.DB_MSora.GetData(cmd)
                End Using


            End If

            Return dt1

        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)

            Return Nothing
        End Try

    End Function

    Public Function GetOrderProperties(ByVal strOrder As String, ByVal strKommision As String, ByVal strType As String, ByVal conn As SqlConnection) As String

        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Dim strNewName As String

        Try


            Select Case strType.ToUpper
                Case "DESCRIPTION"
                    strSQL = "SELECT vkb.bsx_bezeichnung " _
                    & " FROM vkbelegstub vkb " _
                    & " WHERE vkb.bsx_belegnr = @strOrder"

                    Using cmd As New SqlCommand(strSQL, conn)
                        cmd.Parameters.AddWithValue("@strOrder", strOrder)
                        dt = cls.msora.DB_MSora.GetData(cmd)
                    End Using

                    If dt.Rows.Count > 0 Then
                        Return dt(0)("bsx_bezeichnung").ToString.Trim
                    Else
                        Return ""
                    End If
                Case "NAME"
                    strSQL = "SELECT vkb.bsx_bezeichnung, vkb.bsx_gp_identnummer, vkb.bsx_gp_vorname, vkb.bsx_gp_name, vkb.bsx_gp_strasse, vkb.bsx_gp_plz, vkb.bsx_gp_ort, " _
                    & " la.lan_kurzbez, la.lan_langbez, gp.gpx_anrede " _
                    & " FROM vkbelegstub vkb LEFT JOIN land la ON vkb.bsx_id_gpland = la.lan_id " _
                    & " LEFT JOIN gp ON gp.gpx_id = vkb.bsx_gp_id " _
                    & " WHERE vkb.bsx_belegnr = @strOrder"

                    Using cmd As New SqlCommand(strSQL, conn)
                        cmd.Parameters.AddWithValue("@strOrder", strOrder)
                        dt = cls.msora.DB_MSora.GetData(cmd)
                    End Using

                    If dt.Rows.Count > 0 Then
                        If dt(0)("lan_langbez").ToString = "" Then
                            strNewName = Trim((dt(0)("gpx_anrede").ToString.Trim & " " & dt(0)("bsx_gp_vorname").ToString.Trim & " " & dt(0)("bsx_gp_name").ToString.Trim & "; " & dt(0)("bsx_gp_ort")))
                        Else
                            strNewName = Trim((dt(0)("gpx_anrede").ToString.Trim & " " & dt(0)("bsx_gp_vorname").ToString.Trim & " " & dt(0)("bsx_gp_name").ToString.Trim & "; " & dt(0)("lan_kurzbez")))
                        End If

                        If dt(0)("bsx_bezeichnung").ToString.Trim <> "" Then
                            strNewName = strNewName & " {" & Mid(dt(0)("bsx_bezeichnung").ToString.Trim, 1, 20) & "}"
                        End If

                        Return strNewName
                    Else
                        Return ""
                    End If
                Case "OLDNAME"
                    strSQL = "SELECT DISTINCT tm.inhaltmarke FROM textmarken tm WHERE tm.auftragsnummer = @strOrder AND kommissionsnummer = @strKommission" _
                        & " AND tm.positionsnummer = 1 " _
                        & " AND tm.nummerliste = 81 AND tm.kennungmarke = 2006"

                    'Using myconn As MySqlConnection = GetMyConnection("EPROD")
                    Using cmd1 As New MySqlCommand(strSQL, gConnEProd)
                        cmd1.Parameters.AddWithValue("@strOrder", strOrder)
                        cmd1.Parameters.AddWithValue("@strKommission", strKommision)
                        dt = GetMyData(cmd1)
                        If dt.Rows.Count > 0 Then
                            Return dt(0)("inhaltmarke")
                        Else
                            Return ""
                        End If
                    End Using
                    'End Using
                Case "RECLAMATION"
                    strOrder = Mid(strOrder, 2)
                    strSQL = "SELECT DISTINCT vkb.bsx_4_AUSWAHL_REKLAMACIJA " _
                    & " FROM vkbelegstub vkb " _
                    & " WHERE vkb.bsx_belegnr LIKE '%" & strOrder & "' ORDER BY vkb.bsx_4_AUSWAHL_REKLAMACIJA DESC"


                    strOrder = Mid(strOrder, 2)
                    Using cmd As New SqlCommand(strSQL, conn)
                        'cmd.Parameters.AddWithValue("@strOrder", strOrder)
                        dt = cls.msora.DB_MSora.GetData(cmd)
                    End Using

                    If dt.Rows.Count > 0 Then
                        Return dt(0)("bsx_4_AUSWAHL_REKLAMACIJA").ToString.Trim
                    Else
                        Return ""
                    End If
                Case "PRODUCTIONTEXT"
                    strSQL = "SELECT fax_fertigungtext, fax_moddate " _
                    & " FROM fertigungsauftrag " _
                    & " WHERE fax_auftragnr = @strKommision"


                    Using cmd As New SqlCommand(strSQL, conn)
                        cmd.Parameters.AddWithValue("@strKommision", strKommision)
                        dt = cls.msora.DB_MSora.GetData(cmd)
                    End Using

                    If dt.Rows.Count > 0 Then
                        If dt(0)("fax_fertigungtext").ToString <> "" Then
                            strNewName = dt(0)("fax_fertigungtext").ToString.Trim
                        Else
                            strNewName = ""
                        End If
                        Return strNewName
                    Else
                        Return ""
                    End If
                Case Else
                    Return ""
            End Select
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return ""
        End Try
    End Function

    Public Function GetFax_ModDate(ByVal strKommision As String, connKBS As SqlConnection) As DateTime
        Dim dt As DataTable = Nothing
        Dim strSQL As String

        Try

            strSQL = "SELECT fax_fertigungtext, fax_moddate " _
            & " FROM fertigungsauftrag " _
            & " WHERE fax_auftragnr = @strKommision"


            Using cmd As New SqlCommand(strSQL, connKBS)
                cmd.Parameters.AddWithValue("@strKommision", strKommision)
                dt = cls.msora.DB_MSora.GetData(cmd)
                If dt.Rows.Count > 0 Then
                    Return dt(0)("fax_moddate")
                Else
                    Return Nothing
                End If
            End Using



        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return ""
        End Try
    End Function

    Public Sub UpdateReclamation(ByVal strOrder As String, ByVal strKommission As String, ByVal conn As MySqlConnection, connkbs As SqlConnection)
        'dodam še v msora_extra.reclamation
        Dim intReclamation As Integer = 0
        Dim strReklamacija As String = ""
        Dim strSQL As String = ""
        Try

            strReklamacija = modEProd.GetOrderProperties(strOrder, strKommission, "RECLAMATION", connkbs)

            Select Case strReklamacija.ToUpper
                Case "REKLAMACIJA"
                    intReclamation = 1
                Case "NAPAKA"
                    intReclamation = 2
                Case Else
                    intReclamation = 0
            End Select


            strSQL = "SELECT * FROM msora_extra WHERE kommisionsnummer = @strKommission AND auftragsnummer = @strOrder"
            Using cmd As New MySqlCommand(strSQL, conn)
                cmd.Parameters.AddWithValue("@strKommission", strKommission)
                cmd.Parameters.AddWithValue("@strOrder", strOrder)
                Dim dt As DataTable = GetMyData(cmd)
                If dt.Rows.Count > 0 Then
                    strSQL = "UPDATE msora_extra SET reclamation = @intReclamation WHERE kommisionsnummer = @strKommission AND auftragsnummer = @strOrder"
                Else
                    strSQL = "INSERT INTO msora_extra (reclamation, kommisionsnummer, auftragsnummer) VALUES (@intReclamation, @strKommission, @strOrder)"
                End If
                Using cmd1 As New MySqlCommand(strSQL, conn)
                    cmd1.Parameters.AddWithValue("@intReclamation", intReclamation)
                    cmd1.Parameters.AddWithValue("@strKommission", strKommission)
                    cmd1.Parameters.AddWithValue("@strOrder", strOrder)
                    cmd1.ExecuteNonQuery()
                End Using

            End Using


        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Sub

    Public Function GetOrderNr(ByVal strKommision As String) As String
        Dim conn As New SqlConnection

        Dim dt As DataTable = Nothing
        Dim strSQL As String

        Try

            strSQL = "SELECT DISTINCT auftragsnummer FROM auftrag WHERE kommissionsnummer = @strKommission ORDER BY auftragsnummer"

            'Using myconn As MySqlConnection = GetMyConnection("EPROD")
            Using cmd1 As New MySqlCommand(strSQL, gConnEProd)
                cmd1.Parameters.AddWithValue("@strKommission", strKommision)
                dt = GetMyData(cmd1)
                If dt.Rows.Count > 0 Then
                    Return dt(0)("auftragsnummer")
                Else
                    Return ""
                End If
            End Using
            'End Using
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return ""
        End Try
    End Function

    Public Function GetOrdersInKommission(ByVal strKommision As String) As DataTable
        Dim conn As New SqlConnection

        Dim dt As DataTable = Nothing
        Dim strSQL As String

        Try

            strSQL = "SELECT DISTINCT auftragsnummer FROM auftrag WHERE kommissionsnummer = @strKommission ORDER BY auftragsnummer"

            'Using myconn As MySqlConnection = GetMyConnection("EPROD")
            Using cmd1 As New MySqlCommand(strSQL, gConnEProd)
                cmd1.Parameters.AddWithValue("@strKommission", strKommision)
                dt = GetMyData(cmd1)

                Return dt
                
            End Using
            'End Using
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function ChangeDeliveryDate(ByVal strAuftragNr As String, ByVal dtmTerminKoncanja As Date, ByRef orgDate As String) As Boolean
        'strnalog je iz plana!
        'dobiti moram letnico!
        Dim strSQL As String = ""
        Dim dt As DataTable = Nothing
        Dim blnUpdate As Boolean = False
        Try

            'poiščem 
            strSQL = "SELECT lieferdatum as datumdobave FROM Auftrag WHERE auftragsnummer = @strAuftragNr"

            'Using conn As MySqlConnection = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@strAuftragNr", strAuftragNr)
                dt = GetMyData(cmd)

                If dt.Rows.Count > 0 Then

                    orgDate = dt(0)("datumdobave").ToString
                    'spremenim lieferdatum
                    blnUpdate = False

                    If orgDate <> "" Then
                        If IsDate(orgDate) Then
                            If CDate(orgDate) <> dtmTerminKoncanja Then
                                blnUpdate = True
                            End If
                        End If
                    Else
                        blnUpdate = True
                    End If

                    If blnUpdate Then
                        strSQL = "UPDATE Auftrag SET lieferdatum = @dtmTerminKoncanja, lieferzeit = @lieferzeit, lieferwoche = @lieferwoche " _
                            & " WHERE auftragsnummer = @strAuftragNr"

                        Using cmd2 As New MySqlCommand(strSQL, gConnEProd)
                            cmd2.Parameters.AddWithValue("@dtmTerminKoncanja", Format(dtmTerminKoncanja, "dd.MM.yyyy"))
                            cmd2.Parameters.AddWithValue("@lieferzeit", cls.Utils.DateTimeToUnixDateTimestamp(dtmTerminKoncanja).ToString + "000")
                            cmd2.Parameters.AddWithValue("@lieferwoche", cls.Utils.GetWeek(dtmTerminKoncanja) & "." & dtmTerminKoncanja.Year)
                            cmd2.Parameters.AddWithValue("@strAuftragNr", strAuftragNr)

                            cmd2.ExecuteNonQuery()

                        End Using

                        strSQL = "UPDATE textmarken SET inhaltmarke = @inhaltmarke WHERE auftragsnummer = @strAuftragNr AND nummerliste = 81 " _
                            & " AND kennungmarke IN ('2021','1007')"
                        Using cmd2 As New MySqlCommand(strSQL, gConnEProd)
                            cmd2.Parameters.AddWithValue("@inhaltmarke", cls.Utils.GetWeek(dtmTerminKoncanja) & "." & dtmTerminKoncanja.Year)
                            cmd2.Parameters.AddWithValue("@strAuftragNr", strAuftragNr)

                            cmd2.ExecuteNonQuery()

                        End Using

                        strSQL = "UPDATE textmarken SET inhaltmarke = @inhaltmarke WHERE auftragsnummer = @strAuftragNr AND nummerliste = 81 " _
                            & " AND kennungmarke IN ('1009','2020') AND zuordnung = ''"
                        Using cmd2 As New MySqlCommand(strSQL, gConnEProd)
                            cmd2.Parameters.AddWithValue("@inhaltmarke", Format(dtmTerminKoncanja, "dd.MM.yyyy"))
                            cmd2.Parameters.AddWithValue("@strAuftragNr", strAuftragNr)

                            cmd2.ExecuteNonQuery()

                            

                            Return True

                        End Using

                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End Using
            'End Using
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return False
        End Try

    End Function

    Public Sub ChangeOrderName(ByVal strNarocilo As String, ByVal strNalog As String, ByVal strOldName As String, ByVal strNewName As String, connTools As SqlConnection)

        Dim dt As DataTable = Nothing
        Dim dt2 As DataTable = Nothing
        Dim strSQL As String
        'vsako listo moram posodobiti posebej
        Dim aListe() As String
        Dim strNummerListe As String = ""
        Try

            aListe = cls.Config.GetNameNummerListe.Split(",")

            If strNewName.Trim <> "" Then
                For i = 0 To aListe.Length - 1
                    strNummerListe = aListe(i).ToString

                    strSQL = "UPDATE textmarken SET inhaltmarke = @strNewName WHERE kommissionsnummer = @strNalog AND auftragsnummer = @strNarocilo " _
                        & " AND kennungmarke = @kennungmarke AND nummerliste = @nummerliste AND kennungliste = 'F'"

                    'Using conn As MySqlConnection = GetMyConnection("eProd")
                    Using cmd As New MySqlCommand(strSQL, gConnEProd)
                        cmd.Parameters.AddWithValue("@strNewName", strNewName)
                        cmd.Parameters.AddWithValue("@strNalog", strNalog)
                        cmd.Parameters.AddWithValue("@strNarocilo", strNarocilo)
                        cmd.Parameters.AddWithValue("@kennungmarke", cls.Config.GetNameKennungMarke)
                        cmd.Parameters.AddWithValue("@nummerliste", strNummerListe)
                        cmd.ExecuteNonQuery()

                        'vstavim še v bazo eprod_change_name
                        strSQL = "SELECT * FROM eprod_change_name WHERE order_nr = @order_nr AND list_nr = @strNummerListe"

                        Using cmd2 As New SqlCommand(strSQL, connTools)
                            cmd2.Parameters.AddWithValue("@order_nr", strNalog)
                            cmd2.Parameters.AddWithValue("@strNummerListe", strNummerListe)
                            dt2 = GetData(cmd2)
                            If dt2.Rows.Count = 0 Then
                                strSQL = "INSERT INTO eprod_change_name (name_old, name_new, changed_on, user_id, order_nr, list_nr) " _
                                    & " VALUES (@name_old, @name_new, @changed_on, @user_id, @order_nr, @strNummerListe)"
                            Else
                                strSQL = "UPDATE eprod_change_name SET name_old = @name_old, name_new = @name_new, changed_on = @changed_on, user_id = @user_id " _
                                    & " WHERE order_nr = @order_nr AND list_nr =  @strNummerListe"
                            End If
                            Using cmd3 As New SqlCommand(strSQL, connTools)
                                cmd3.Parameters.AddWithValue("@name_old", strOldName)
                                cmd3.Parameters.AddWithValue("@name_new", strNewName)
                                cmd3.Parameters.AddWithValue("@changed_on", Now.Date)
                                cmd3.Parameters.AddWithValue("@user_id", "AUTO")
                                cmd3.Parameters.AddWithValue("@order_nr", strNalog)
                                cmd3.Parameters.AddWithValue("@strNummerListe", strNummerListe)
                                cmd3.ExecuteNonQuery()
                            End Using
                        End Using
                  
                    End Using


                Next

            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)

        End Try
    End Sub

    
    Public Function GetFinishedKommissions(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal strParameter As String) As DataTable

        Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Try

            strSQL = "SELECT Z.kommissionsnummer, A.auftragsnummer, A.lieferdatum, A.lieferwoche, from_unixtime(substring(Z.buchzeit,1,10)) as lastdate FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A " _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status = 100 AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo"

            'conn = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                dt = GetMyData(cmd)
            End Using

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetStartedKommissions(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal intPlace As Integer, ByVal strOrderNr As String, ByVal strKommissionNr As String) As DataTable

        Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Try

            strSQL = "SELECT ZK.kommissionsnummer, A.Auftragsnummer, " _
                & " min(KIZ.buchdatum) as startdate, " _
                & " max(KIZ.buchdatum) as lastdate " _
                & " FROM ZustandKomm ZK, Zustand Z, Auftrag A, klaes_pf_istzeiten KIZ  " _
                & " WHERE(ZK.kommissionsnummer = Z.kommissionsnummer) " _
                & " AND Z.kommissionsnummer = A.kommissionsnummer " _
                & " AND Z.kommissionsnummer = KIZ.kommissionsnummer " _
                & " AND ZK.fertig = 'N' " _
                & " AND ZK.status = 10 " _
                & " AND KIZ.buchdatum BETWEEN @dtmFrom AND @dtmTo " _
                & " AND Z.positionsnummer = 0 and z.anzahlteile > Z.nummerrueckmeldung AND KIZ.platznummerPF = @kostenstelle " _
                & " GROUP BY ZK.kommissionsnummer, A.Auftragsnummer"

            'strSQL = "SELECT ZK.kommissionsnummer, A.Auftragsnummer, from_unixtime(substring(ZK.ersterzugriff,1,10)) as startdate, from_unixtime(substring(ZK.letzterzugriff,1,10)) as lastdate FROM `fertigung`.`ZustandKomm` ZK, `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A " _
            '& " WHERE ZK.kommissionsnummer = Z.kommissionsnummer " _
            '& " AND Z.kommissionsnummer = A.kommissionsnummer " _
            '& " AND ZK.fertig = 'N' AND ZK.status = 10 AND from_unixtime(substring(ZK.ersterzugriff,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
            '& " AND Z.positionsnummer = 0 and z.anzahlteile > Z.nummerrueckmeldung AND z.kostenstelle = @kostenstelle"

            If strOrderNr <> "" Then
                strSQL = strSQL & " AND a.auftragsnummer LIKE @auftragsnummer"
            End If

            If strKommissionNr <> "" Then
                strSQL = strSQL & " AND a.kommissionsnummer LIKE @kommissionsnummer"
            End If

            'conn = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                cmd.Parameters.AddWithValue("@kostenstelle", intPlace)
                If strOrderNr <> "" Then
                    cmd.Parameters.AddWithValue("@auftragsnummer", strOrderNr.Replace("*", "%"))
                End If
                If strKommissionNr <> "" Then
                    cmd.Parameters.AddWithValue("@kommissionsnummer", strKommissionNr.Replace("*", "%"))
                End If
                dt = GetMyData(cmd)
            End Using

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetFinishedKommissions(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal intPlace As Integer, ByVal strOrderNr As String, ByVal strKommissionNr As String) As DataTable

        Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Try

            strSQL = "SELECT Z.kommissionsnummer, A.auftragsnummer, A.lieferdatum, A.lieferwoche, from_unixtime(substring(Z.buchzeit,1,10)) as lastdate " _
                & " FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A" _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status = 100 " _
                & " AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                & " AND Z.kostenstelle = @kostenstelle"

            If strOrderNr <> "" Then
                strSQL = strSQL & " AND a.auftragsnummer LIKE @auftragsnummer"
            End If

            If strKommissionNr <> "" Then
                strSQL = strSQL & " AND a.kommissionsnummer LIKE @kommissionsnummer"
            End If

            'conn = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                cmd.Parameters.AddWithValue("@kostenstelle", intPlace)
                If strOrderNr <> "" Then
                    cmd.Parameters.AddWithValue("@auftragsnummer", strOrderNr.Replace("*", "%"))
                End If
                If strKommissionNr <> "" Then
                    cmd.Parameters.AddWithValue("@kommissionsnummer", strKommissionNr.Replace("*", "%"))
                End If

                dt = GetMyData(cmd)
            End Using

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function


    Public Function GetFinnishedPositions(ByVal strOrderNr As String, ByVal intStation As Integer, ByVal conn As MySqlConnection) As Integer
        Dim strSQL As String = ""
        'Using connEprod As MySqlConnection = GetMyConnection()
        strSQL = "SELECT count(*) as fin FROM zustand z, auftrag a " _
            & " WHERE z.kommissionsnummer = a.kommissionsnummer and  a.auftragsnummer = @strOrderNr and z.positionsnummer > 0 " _
            & " AND z.kostenstelle = @intStation AND z.status = 100"


        Using cmde As New MySqlCommand(strSQL, conn)
            cmde.Parameters.AddWithValue("@strOrderNr", strOrderNr)
            cmde.Parameters.AddWithValue("@intStation", intStation)
            Dim dte As DataTable = GetMyData(cmde)
            If dte.Rows.Count > 0 Then
                Return DB2IntZero(dte.Rows(0)("fin"))
            Else
                Return 0
            End If
        End Using
        Return 0
    End Function


    Public Function GetOrderStatus(ByVal strOrderNr As String, ByVal intStation As Integer, ByVal conn As MySqlConnection) As Integer
        Dim strSQL As String = ""
        'Using connEprod As MySqlConnection = GetMyConnection()

        Try

            strSQL = "SELECT z.status FROM zustand z, auftrag a " _
                & " WHERE z.kommissionsnummer = a.kommissionsnummer and  a.auftragsnummer = @strOrderNr and z.positionsnummer = 0 " _
                & " AND z.kostenstelle = @intStation "


            Using cmde As New MySqlCommand(strSQL, conn)
                cmde.Parameters.AddWithValue("@strOrderNr", strOrderNr)
                cmde.Parameters.AddWithValue("@intStation", intStation)
                Dim dte As DataTable = GetMyData(cmde)
                If dte.Rows.Count > 0 Then
                    Return DB2IntZero(dte.Rows(0)("status"))
                End If
            End Using
            Return 0
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return -1

        End Try

    End Function

    Public Function GetOrderStatusK(ByVal strKommissionsNr As String, ByVal intStation As Integer, ByVal conn As MySqlConnection, ByRef dtmLastBooking As Date) As Integer
        Dim strSQL As String = ""

        Try

            'Using connEprod As MySqlConnection = GetMyConnection()
            strSQL = "SELECT z.status,  max(from_unixtime(Substring(pf.buchzeit,1,10))) as lastbooking FROM zustand z, auftrag a, klaes_pf_istzeiten pf " _
                & " WHERE z.kommissionsnummer = a.kommissionsnummer " _
                & " AND pf.kommissionsnummer = a.kommissionsnummer " _
                & " AND a.kommissionsnummer = @strKommissionsNr " _
                & " AND z.positionsnummer = 0 AND z.kostenstelle = @intStation "


            Using cmde As New MySqlCommand(strSQL, conn)
                cmde.Parameters.AddWithValue("@strKommissionsNr", strKommissionsNr)
                cmde.Parameters.AddWithValue("@intStation", intStation)
                Dim dte As DataTable = GetMyData(cmde)
                If dte.Rows.Count > 0 Then
                    If dte.Rows(0)("lastbooking").ToString.Trim <> "" Then
                        dtmLastBooking = CDate(dte.Rows(0)("lastbooking").ToString)
                    Else
                        dtmLastBooking = Nothing
                    End If
                    Return DB2IntZero(dte.Rows(0)("status"))
                Else
                    dtmLastBooking = Nothing
                    Return -1
                End If
            End Using
            Return 0
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return -1

        End Try

    End Function

    Public Function GetKommissions(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal intPlace As Integer, ByVal strOrderNr As String, ByVal strKommissionNr As String) As DataTable

        Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Try

            strSQL = "SELECT DISTINCT Z.kommissionsnummer, A.auftragsnummer, A.lieferdatum, A.lieferwoche, M.datumeintrag " _
                & " FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A, `fertigung`.`Mengen` M " _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 " _
                & " AND A.kommissionsnummer = M.kommissionsnummer " _
                & " AND  M.datumeintrag BETWEEN @dtmFrom AND @dtmTo " _
                & " AND Z.kostenstelle = @kostenstelle"

            If strOrderNr <> "" Then
                strSQL = strSQL & " AND a.auftragsnummer LIKE @auftragsnummer"
            End If

            If strKommissionNr <> "" Then
                strSQL = strSQL & " AND a.kommissionsnummer LIKE @kommissionsnummer"
            End If

            'conn = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom.Date)
                cmd.Parameters.AddWithValue("@dtmTo", dtmTo.Date)
                cmd.Parameters.AddWithValue("@kostenstelle", intPlace)
                If strOrderNr <> "" Then
                    cmd.Parameters.AddWithValue("@auftragsnummer", strOrderNr.Replace("*", "%"))
                End If
                If strKommissionNr <> "" Then
                    cmd.Parameters.AddWithValue("@kommissionsnummer", strKommissionNr.Replace("*", "%"))
                End If

                dt = GetMyData(cmd)
            End Using

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function


    Public Function GetStartedOrders(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal strParameter As String) As DataTable

        Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Try

            strSQL = "SELECT A.Auftragsnummer, Z.kommissionsnummer, from_unixtime(substring(Z.buchzeit,1,10)) as startdate FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A " _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status = 10 " _
                & " AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo"

            'conn = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                dt = GetMyData(cmd)
            End Using

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try

    End Function

    Public Function GetFinnishedCutterFiles(ByVal dtmFrom As Date, ByVal dtmTo As Date) As DataTable

        Dim dt As DataTable = Nothing
        Dim dr As DataRow = Nothing
        Dim dtmChanged As Date
        Dim intStatus As Integer = -1
        Dim intStatusEProd As Integer = -1
        Dim strOrder As String = ""
        Dim dtmClosed As Date = Nothing

        Try
            'najprej kreiram tabelo
            dt = CreateTableKommissionFiles()

            Dim aFiles As String() = Directory.GetFiles(cls.Config.GetCutterDestPath)

            For i = 0 To aFiles.Length - 1
                dtmChanged = File.GetLastWriteTime(aFiles(i))
                If dtmChanged >= dtmFrom And dtmChanged <= DateAdd("d", 1, dtmTo) Then
                    dr = dt.NewRow
                    dr("FileName") = aFiles(i)
                    dr("DateChanged") = dtmChanged
                    intStatus = IsFileFinnished(aFiles(i))
                    dr("Status") = intStatus

                    dt.Rows.Add(dr)
                Else

                    strOrder = Path.GetFileName(aFiles(i))
                    strOrder = Path.GetFileNameWithoutExtension(strOrder)

                    If Mid(strOrder, 1, 1) = "T" Then
                        intStatusEProd = GetOrderStatus(strOrder, 11, gConnEProd)
                    Else
                        intStatusEProd = GetOrderStatusK(strOrder, 11, gConnEProd, dtmClosed)
                    End If

                    If intStatusEProd = 100 Then
                        dr = dt.NewRow
                        dr("FileName") = aFiles(i)
                        dr("DateChanged") = dtmClosed
                        dr("Status") = 3
                        dt.Rows.Add(dr)
                    End If


                End If

            Next

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try

    End Function

    Public Function GetFinnishedCutterFiles() As DataTable

        Dim dt As DataTable = Nothing
        Dim dr As DataRow = Nothing
        Dim dtmChanged As Date
        Dim intStatus As Integer = -1

        Try
            'najprej kreiram tabelo
            dt = CreateTableKommissionFiles()

            Dim aFiles As String() = Directory.GetFiles(cls.Config.GetCutterDestPath)

            For i = 0 To aFiles.Length - 1
                dtmChanged = File.GetLastWriteTime(aFiles(i))

                dr = dt.NewRow
                dr("FileName") = aFiles(i)
                dr("DateChanged") = dtmChanged
                intStatus = IsFileFinnished(aFiles(i))
               
                dr("status") = intStatus

                dt.Rows.Add(dr)


            Next

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Function CreateTableKommissionFiles() As DataTable
        Dim dt As New DataTable("KommissionsFiles")

        Dim column As DataColumn = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "FileName"
            .Caption = "File"
            .ReadOnly = True
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.DateTime")
            .ColumnName = "DateChanged"
            .Caption = "Date"
            .ReadOnly = True
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "Status"
            .Caption = "Status"
            .ReadOnly = True
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        Return dt

    End Function

    Public Function CreateTableCutterRecord() As DataTable
        Dim dt As New DataTable("CutterRecord")

        Dim column As DataColumn = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "article_nr"
            .Caption = "Article"
            .ReadOnly = True
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "description"
            .Caption = "Opis"
            .ReadOnly = True
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "length"
            .Caption = "Length"
            .ReadOnly = True
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "quantity"
            .Caption = "Quantity"
            .ReadOnly = True
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)


        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "finished_quantity"
            .Caption = "Finished"
            .ReadOnly = True
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        Return dt

    End Function

    Public Function GetStartedOrders(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal intPlace As Integer, ByVal strParameter As String) As DataTable

        Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Try

            strSQL = "SELECT A.Auftragsnummer, max(from_unixtime(substring(Z.buchzeit,1,10))) as startdate FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A " _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status = 10 " _
                & " AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                & " AND Z.kostenstelle = @kostenstelle " _
                & " GROUP BY A.Auftragsnummer "

            'conn = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                cmd.Parameters.AddWithValue("@kostenstelle", intPlace)
                dt = GetMyData(cmd)
            End Using

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try

    End Function

    Public Function GetBookedOrders(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal intPlace As Integer, ByVal strParameter As String) As DataTable

        'Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Try

            strSQL = "SELECT max(from_unixtime(substring(Z.buchzeit,1,10),'%Y-%m-%d')) as startdate, A.Auftragsnummer " _
                & " FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A " _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status >= 10 " _
                & " AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                & " AND Z.kostenstelle = @kostenstelle " _
                & " GROUP BY A.Auftragsnummer "

            

            'conn = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                cmd.Parameters.AddWithValue("@kostenstelle", intPlace)
                dt = GetMyData(cmd)
            End Using

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try

    End Function



    Public Function GetFinishedOrders(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal strParameter As String) As DataTable

        'Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Try

            strSQL = "SELECT min(Z.status) as min_status, A.auftragsnummer, A.lieferdatum, A.lieferwoche, from_unixtime(substring(Z.buchzeit,1,10)) as lastdate FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A" _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status = 100 AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                & " AND A.auftragsnummer NOT IN (SELECT AA.auftragsnummer FROM `fertigung`.`Zustand` ZZ, `fertigung`.`Auftrag` AA " _
                & " WHERE ZZ.kommissionsnummer = AA.kommissionsnummer " _
                 & IIf(cls.Config.CheckStatusForAllPositions = 1, "", " AND ZZ.positionsnummer = 0 ") _
                & " AND ZZ.status < 100 " _
                & " AND AA.auftragsnummer = A.auftragsnummer) " _
                & " GROUP BY A.auftragsnummer, A.lieferdatum, A.lieferwoche, from_unixtime(substring(Z.buchzeit,1,10)) " _
                & " HAVING min(z.status) = 100"

            'conn = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                dt = GetMyData(cmd)
            End Using

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetFinishedOrders(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal intPlace As Integer, ByVal strParameter As String) As DataTable

        Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Try

            strParameter = strParameter.Trim

            If strParameter = "" Then
                strSQL = "SELECT min(Z.status) as min_status, max(A.lieferdatum) as lieferdatum, max(A.lieferwoche) as lieferwoche, max((substring(Z.buchzeit,1,10)) as lastdate, A.auftragsnummer " _
                & " FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A" _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status = 100 AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                & " AND Z.kostenstelle = @kostenstelle " _
                & " AND A.auftragsnummer NOT IN (SELECT AA.auftragsnummer FROM `fertigung`.`Zustand` ZZ, `fertigung`.`Auftrag` AA " _
                & " WHERE ZZ.kommissionsnummer = AA.kommissionsnummer " _
                & IIf(cls.Config.CheckStatusForAllPositions = 1, "", " AND ZZ.positionsnummer = 0 ") _
                & " AND ZZ.status < 100 And ZZ.kostenstelle = @kostenstelle " _
                & " AND AA.auftragsnummer = A.auftragsnummer) " _
                & " GROUP BY A.auftragsnummer " _
                & " HAVING min(z.status) = 100"
            Else
                'strSQL = "SELECT min(Z.status) as min_status, max(A.lieferdatum) as lieferdatum, max(A.lieferwoche) as lieferwoche, max(from_unixtime(substring(Z.buchzeit,1,10))) as lastdate, A.auftragsnummer " _
                '& " FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A" _
                '& " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                '& " AND Z.kostenstelle = @kostenstelle " _
                '& " GROUP BY A.auftragsnummer "


                strSQL = "SELECT min(Z.status) as min_status, max(A.lieferdatum) as lieferdatum, max(A.lieferwoche) as lieferwoche, max(substring(Z.buchzeit,1,10)) as lastdate, A.auftragsnummer " _
                & " FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A" _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                & " AND Z.kostenstelle = @kostenstelle " _
                & " GROUP BY A.auftragsnummer "
            End If


            'conn = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                cmd.Parameters.AddWithValue("@kostenstelle", intPlace)
                dt = GetMyData(cmd)
            End Using

            If strParameter <> "" And IsNumeric(strParameter) Then 'ugotovim če ustreza pogoju, koliko % je narejenega
                'Dim itemColumns As DataRowCollection = dt.Rows

                For i = 0 To dt.Rows.Count - 1
                    Dim strOrder As String = ""
                    Dim strKommission As String = ""
                    strOrder = dt(i)("auftragsnummer").ToString
                    Dim intAll As Integer = GeteProdOrderDetailsAll(strOrder, "", intPlace)
                    Dim intBooked As Integer = GeteProdOrderDetailsBooked(strOrder, "", intPlace)
                    If intAll > 0 Then
                        Dim intPct As Integer = Int((intBooked * 100) / intAll)

                        If intPct < CInt(strParameter) Then
                            dt.Rows(i).Delete()
                        End If
                    Else
                        dt.Rows(i).Delete()
                    End If
                Next

                dt.AcceptChanges()
            End If

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetFinishedOrdersDeliveryOK(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal intPlace As Integer, ByVal strParameter As String, connKBS As SqlConnection, connTools As SqlConnection, connMAWI As SqlConnection) As DataTable

        Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Dim blnDelete As Boolean
        Try

            strParameter = strParameter.Trim

            If strParameter <> "" And IsNumeric(strParameter) Then 'ugotovim če ustreza pogoju, vse dobavljeno čez parameter dni

                strSQL = "SELECT min(Z.status) as min_status, max(A.lieferdatum) as lieferdatum, max(A.lieferwoche) as lieferwoche, max(substring(Z.buchzeit,1,10)) as lastdate, A.auftragsnummer " _
                & " FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A" _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status = 100 AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                & " AND Z.kostenstelle = @kostenstelle " _
                & " AND A.auftragsnummer NOT IN (SELECT AA.auftragsnummer FROM `fertigung`.`Zustand` ZZ, `fertigung`.`Auftrag` AA " _
                & " WHERE ZZ.kommissionsnummer = AA.kommissionsnummer " _
                & IIf(cls.Config.CheckStatusForAllPositions = 1, "", " AND ZZ.positionsnummer = 0 ") _
                & " AND ZZ.status < 100 And ZZ.kostenstelle = @kostenstelle1 " _
                & " AND AA.auftragsnummer = A.auftragsnummer) " _
                & " GROUP BY A.auftragsnummer " _
                & " HAVING min(z.status) = 100"

                'conn = GetMyConnection("eProd")
                Using cmd As New MySqlCommand(strSQL, gConnEProd)
                    cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                    cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                    cmd.Parameters.AddWithValue("@kostenstelle", intPlace)
                    cmd.Parameters.AddWithValue("@kostenstelle1", intPlace)
                    dt = GetMyData(cmd)
                End Using

                For i = 0 To dt.Rows.Count - 1
                    Dim strOrder As String = ""
                    Dim strKommission As String = ""
                    blnDelete = False
                    strOrder = dt(i)("auftragsnummer").ToString


                    If Not IsMailingDissabled(strOrder, connTools, connKBS) Then
                        Dim dtOrders As DataTable = GetMawiOrders(strOrder, connmawi)

                        '     brišem - tiste, ki nimajo potrjene dobave
                        '     brišem - tiste, ki imajo datum dobave čez več kot X
                        If Not dtOrders Is Nothing Then
                            For j = 0 To dtOrders.Rows.Count - 1
                                If dtOrders(j)("datum_dobave") Is DBNull.Value Then
                                    If dtOrders(j)("datum_potrjene_dobave") Is DBNull.Value Then
                                        blnDelete = True
                                        Exit For
                                    Else
                                        'primerjam datuma
                                        Dim dtmPotrjenaDobava As Date = CDate(dtOrders(j)("datum_potrjene_dobave").ToString)
                                        If DateDiff(DateInterval.Day, Now.Date, dtmPotrjenaDobava) > CInt(strParameter) Then
                                            blnDelete = True
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Else
                        blnDelete = True
                    End If

                    If blnDelete Or IsMailingDissabled(strOrder, connTools, connKBS) Then
                        dt.Rows(i).Delete()
                    End If
                Next

                dt.AcceptChanges()
            Else
                Return Nothing
            End If

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function GetFinishedOrdersAlreadyDelivered(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal intPlace As Integer, ByVal strParameter As String, connTools As SqlConnection, connKBS As SqlConnection) As DataTable

        Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Dim blnDelete As Boolean
        Dim strTmp As String = ""
        Dim dtmTmp As Date = Nothing
        Try

            strParameter = strParameter.Trim

            If strParameter <> "" And IsNumeric(strParameter) Then 'ugotovim če ustreza pogoju, vse dobavljeno čez parameter dni

                strSQL = "SELECT min(Z.status) as min_status, max(A.lieferdatum) as lieferdatum, max(A.lieferwoche) as lieferwoche, max(substring(Z.buchzeit,1,10)) as lastdate, A.auftragsnummer " _
                & " FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A" _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status = 100 AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                & " AND Z.kostenstelle = @kostenstelle " _
                & " AND A.auftragsnummer NOT IN (SELECT AA.auftragsnummer FROM `fertigung`.`Zustand` ZZ, `fertigung`.`Auftrag` AA " _
                & " WHERE ZZ.kommissionsnummer = AA.kommissionsnummer " _
                & IIf(cls.Config.CheckStatusForAllPositions = 1, "", " AND ZZ.positionsnummer = 0 ") _
                & " AND ZZ.status < 100 And ZZ.kostenstelle = @kostenstelle1 " _
                & " AND AA.auftragsnummer = A.auftragsnummer) " _
                & " GROUP BY A.auftragsnummer " _
                & " HAVING min(z.status) = 100"

                'conn = GetMyConnection("eProd")
                Using cmd As New MySqlCommand(strSQL, gConnEProd)
                    cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                    cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                    cmd.Parameters.AddWithValue("@kostenstelle", intPlace)
                    cmd.Parameters.AddWithValue("@kostenstelle1", intPlace)
                    dt = GetMyData(cmd)
                End Using

                For i = 0 To dt.Rows.Count - 1
                    Dim strOrder As String = ""
                    Dim strKommission As String = ""

                    blnDelete = False
                    strOrder = dt(i)("auftragsnummer").ToString

                    If Not IsMailingDissabled(strOrder, connTools, connKBS) Then
                        'brišem tiste, ki nimajo obavnice
                        If Not OrderHasDelivery(strOrder.Replace("T", "N"), strTmp, dtmTmp, connKBS) Then
                            blnDelete = True
                        End If

                        If blnDelete Or IsMailingDissabled(strOrder, connTools, connKBS) Then
                            dt.Rows(i).Delete()
                        End If
                    End If
                Next

                dt.AcceptChanges()
            Else
                Return Nothing
            End If

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetFinishedOrdersDeliveryFAIL(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal intPlace As Integer, ByVal strParameter As String, connTools As SqlConnection, connKBS As SqlConnection, connmawi As SqlConnection) As DataTable

        Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Dim blnDelete As Boolean = True
        Try

            strParameter = strParameter.Trim
            If strParameter <> "" And IsNumeric(strParameter) Then 'ugotovim če ustreza pogoju - dobava materiala šele čez parameter dni

                strSQL = "SELECT min(Z.status) as min_status, max(A.lieferdatum) as lieferdatum, max(A.lieferwoche) as lieferwoche, max(from_unixtime(substring(Z.buchzeit,1,10))) as lastdate, A.auftragsnummer " _
                 & " FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A" _
                 & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status = 100 AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                 & " AND Z.kostenstelle = @kostenstelle " _
                 & " AND A.auftragsnummer NOT IN (SELECT AA.auftragsnummer FROM `fertigung`.`Zustand` ZZ, `fertigung`.`Auftrag` AA " _
                 & " WHERE ZZ.kommissionsnummer = AA.kommissionsnummer " _
                 & IIf(cls.Config.CheckStatusForAllPositions = 1, "", " AND ZZ.positionsnummer = 0 ") _
                 & " AND ZZ.status < 100 And ZZ.kostenstelle = @kostenstelle " _
                 & " AND AA.auftragsnummer = A.auftragsnummer) " _
                 & " GROUP BY A.auftragsnummer " _
                 & " HAVING min(z.status) = 100"

                strSQL = "SELECT min(Z.status) as min_status, max(A.lieferdatum) as lieferdatum, max(A.lieferwoche) as lieferwoche, max(substring(Z.buchzeit,1,10)) as lastdate, A.auftragsnummer " _
                 & " FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A" _
                 & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status = 100 AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                 & " AND Z.kostenstelle = @kostenstelle " _
                 & " AND A.auftragsnummer NOT IN (SELECT AA.auftragsnummer FROM `fertigung`.`Zustand` ZZ, `fertigung`.`Auftrag` AA " _
                 & " WHERE ZZ.kommissionsnummer = AA.kommissionsnummer " _
                 & IIf(cls.Config.CheckStatusForAllPositions = 1, "", " AND ZZ.positionsnummer = 0 ") _
                 & " AND ZZ.status < 100 And ZZ.kostenstelle = @kostenstelle1 " _
                 & " AND AA.auftragsnummer = A.auftragsnummer) " _
                 & " GROUP BY A.auftragsnummer " _
                 & " HAVING min(z.status) = 100"

                'conn = GetMyConnection("eProd")
                Using cmd As New MySqlCommand(strSQL, gConnEProd)
                    cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                    cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                    cmd.Parameters.AddWithValue("@kostenstelle", intPlace)
                    cmd.Parameters.AddWithValue("@kostenstelle1", intPlace)
                    dt = GetMyData(cmd)
                End Using


                For i = 0 To dt.Rows.Count - 1
                    Dim strOrder As String = ""
                    Dim strKommission As String = ""
                    blnDelete = True
                    strOrder = dt(i)("auftragsnummer").ToString

                    'If Not IsMailingAllowed(strOrder) Then

                    Dim dtOrders As DataTable = GetMawiOrders(strOrder, connmawi)


                    '    NE brišem - tiste, ki nimajo potrjene dobave
                    '    NE brišem - tiste, ki imajo dobavo potrjeno za več kot X dni
                    If Not dtOrders Is Nothing Then
                        For j = 0 To dtOrders.Rows.Count - 1
                            If dtOrders(j)("datum_dobave") Is DBNull.Value Then
                                If dtOrders(j)("datum_potrjene_dobave") Is DBNull.Value Then
                                    blnDelete = False
                                    Exit For
                                Else
                                    'primerjam datuma
                                    Dim dtmPotrjenaDobava As Date = CDate(dtOrders(j)("datum_potrjene_dobave").ToString)
                                    If DateDiff(DateInterval.Day, Now.Date, dtmPotrjenaDobava) > CInt(strParameter) Then
                                        blnDelete = False
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                    'Else
                    'dovoljeno pošiljanje pošte v vsakem primeru, torej ni FAIL
                    'blnDelete = True
                    'End If

                    If blnDelete Or IsMailingDissabled(strOrder, connTools, connKBS) Then
                        dt.Rows(i).Delete()
                    End If
                Next

                dt.AcceptChanges()
            Else
                Return Nothing
            End If

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function

    

    Public Function GetMawiOrders(ByVal strOrderNr As String, connMAWI As SqlConnection) As DataTable
        Dim strSQL As String = ""
        Dim strSQL1 As String = ""

        Dim dt As DataTable
        Dim strDobavnica As String = ""
        Dim dtmDatumDobave As Date = Nothing
        Dim dtMawi As DataTable = Nothing
        Dim lngIdComplaint As Long

        Try



            strSQL1 = "SELECT DISTINCT c.id_contract, o.id_order, o.id_order_state, pb.id_partner_head, pb.id_partner_branch, o.name as docname, o.description, o.order_date, " _
                & " o.latest_delivery_date,  os.name as osname, pb.name as pbname, oc.name as St_potrditve, ocd.id_order_confirmation, " _
                & " ocd.expectation_date as datum_potrjene_dobave, u.id_complaint " _
                & " FROM [order] o " _
                & " INNER JOIN used u ON u.id_order = o.id_order " _
                & " INNER JOIN contract c ON c.id_contract = u.id_contract " _
                & " INNER JOIN partner_branch pb ON pb.id_partner_head = o.id_partner_head AND pb.id_partner_branch = o.id_partner_branch " _
                & " INNER JOIN order_state os ON os.id_order_state = o.id_order_state " _
                & " LEFT JOIN order_confirmation oc ON oc.id_order_confirmation = u.id_order_confirmation  	AND oc.id_order_confirmation > 0 " _
                & " LEFT JOIN order_confirmation_details ocd ON ocd.id_order_confirmation = oc.id_order_confirmation  	AND ocd.id_order_confirmation > 0 " _
                    & " AND ocd.id_article = u.id_article AND ocd.id_color = u.id_color AND ocd.id_goods_group = u.id_goods_group " _
                & " WHERE c.name = @strOrderNr AND u.id_order > 0 AND u.id_item_state <> 0 "


            dtMawi = CreateMawiOrdersTable()

            Using cmd As New SqlCommand(strSQL1, connMAWI)
                cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
                dt = GetData(cmd)
                Dim dr As DataRow = Nothing
                For i = 0 To dt.Rows.Count - 1

                    If DB2Lng(dt(i)("id_order_confirmation")) > 0 Then
                        strDobavnica = ""
                        dtmDatumDobave = Nothing
                        Call GetDeliveryInfo(DB2Lng(dt(i)("id_order_confirmation")), DB2Lng(dt(i)("id_order")), CDate(dt(i)("datum_potrjene_dobave")), strDobavnica, dtmDatumDobave, connMAWI)
                        Debug.Print(dt(i)("pbname").ToString & vbTab & DB2Lng(dt(i)("id_order_confirmation")) & vbTab & DB2Lng(dt(i)("id_order")) & vbTab & CDate(dt(i)("datum_potrjene_dobave")) & vbTab & strDobavnica & vbTab & dtmDatumDobave)
                    Else
                        strDobavnica = ""
                        dtmDatumDobave = Nothing
                    End If

                    dr = dtMawi.NewRow
                    dr("id_contract") = dt(i)("id_contract")
                    dr("id_order") = dt(i)("id_order")
                    dr("id_order_state") = dt(i)("id_order_state")
                    dr("id_partner_head") = dt(i)("id_partner_head")
                    dr("id_partner_branch") = dt(i)("id_partner_branch")
                    dr("docname") = dt(i)("docname")
                    lngIdComplaint = DB2Lng(dt(i)("id_complaint"))
                    Select Case lngIdComplaint
                        Case 0
                            dr("order_type") = "Naročilo"
                        Case Is > 0
                            dr("order_type") = "Reklamacija"
                    End Select
                    dr("description") = dt(i)("description")
                    dr("order_date") = dt(i)("order_date")
                    dr("latest_delivery_date") = dt(i)("latest_delivery_date")
                    dr("osname") = dt(i)("osname")
                    dr("pbname") = dt(i)("pbname")
                    dr("St_potrditve") = dt(i)("St_potrditve")
                    dr("St_dobavnice") = strDobavnica
                    dr("datum_potrjene_dobave") = dt(i)("datum_potrjene_dobave")
                    If dtmDatumDobave = Nothing Then
                        dr("datum_dobave") = DBNull.Value
                    Else
                        dr("datum_dobave") = dtmDatumDobave
                    End If
                    dr("id_order_confirmation") = dt(i)("id_order_confirmation")




                    dtMawi.Rows.Add(dr)

                Next

                Return dtMawi
            End Using


        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try

    End Function

    Private Sub GetDeliveryInfo(ByVal lngId As Long, ByVal lngIdOrder As Long, ByVal dtmDatumPotrjeneDobave As Date, ByRef strDobavnica As String, ByRef dtmDatumDobave As Date, ByVal conn As SqlConnection)
        Dim strSQL As String
        Dim dt As DataTable
        strSQL = "SELECT de.name, max(de.date) as maxdate FROM order_confirmation_to_delivery octd, order_confirmation_details ocd, delivery de, used u " _
            & " WHERE de.id_delivery = octd.id_delivery AND octd.id_order_confirmation = @lngId " _
            & " AND u.id_order = @lngIdOrder AND u.id_item_state >= 2 " _
            & " AND ocd.id_order_confirmation_details = octd.id_order_confirmation_details " _
            & " AND ocd.id_order_confirmation = octd.id_order_confirmation AND ocd.expectation_date = @dtmDatumPotrjeneDobave " _
            & " AND u.id_article = ocd.id_article and u.id_goods_group = ocd.id_goods_group AND u.id_color = ocd.id_color " _
            & " AND u.id_delivery = de.id_delivery " _
            & " GROUP BY de.name "

        Using cmd As New SqlCommand(strSQL, conn)
            cmd.Parameters.AddWithValue("@lngId", lngId)
            cmd.Parameters.AddWithValue("@lngIdOrder", lngIdOrder)
            cmd.Parameters.AddWithValue("@dtmDatumPotrjeneDobave", dtmDatumPotrjeneDobave)
            dt = GetData(cmd)
            If dt.Rows.Count > 0 Then
                strDobavnica = dt(0)("name").ToString
                dtmDatumDobave = CDate(dt(0)("maxdate").ToString)
            Else
                strDobavnica = ""
                dtmDatumDobave = Nothing
            End If
        End Using


    End Sub

    Private Function CreateMawiOrdersTable() As DataTable

        Dim dt As New DataTable

        dt.Columns.Add("id_contract", Type.GetType("System.Int32"))
        dt.Columns.Add("id_order", Type.GetType("System.Int32"))
        dt.Columns.Add("id_order_state", Type.GetType("System.Int32"))
        dt.Columns.Add("id_partner_head", Type.GetType("System.Int32"))
        dt.Columns.Add("id_partner_branch", Type.GetType("System.Int32"))
        dt.Columns.Add("docname", Type.GetType("System.String"))
        dt.Columns.Add("order_type", Type.GetType("System.String"))
        dt.Columns.Add("description", Type.GetType("System.String"))
        dt.Columns.Add("order_date", Type.GetType("System.DateTime"))
        dt.Columns.Add("latest_delivery_date", Type.GetType("System.DateTime"))
        dt.Columns.Add("osname", Type.GetType("System.String"))
        dt.Columns.Add("pbname", Type.GetType("System.String"))
        dt.Columns.Add("St_potrditve", Type.GetType("System.String"))
        dt.Columns.Add("datum_potrjene_dobave", Type.GetType("System.DateTime"))
        dt.Columns.Add("St_dobavnice", Type.GetType("System.String"))
        dt.Columns.Add("datum_dobave", Type.GetType("System.DateTime"))
        dt.Columns.Add("id_order_confirmation", Type.GetType("System.Int32"))


        Return dt
    End Function





    Public Function GetPartialFinishedKommissions(ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal intPlace As Integer, ByVal strParameter As String) As DataTable

        Dim conn As New MySqlConnection
        Dim dt As DataTable = Nothing
        Dim strSQL As String
        Try

            strSQL = "SELECT Z.kommissionsnummer, A.auftragsnummer, A.lieferdatum, A.lieferwoche, from_unixtime(substring(Z.buchzeit,1,10)) as lastdate FROM `fertigung`.`Zustand` Z, `fertigung`.`Auftrag` A" _
                   & " WHERE Z.kommissionsnummer = A.kommissionsnummer AND Z.positionsnummer = 0 AND Z.status = 10 AND from_unixtime(substring(Z.buchzeit,1,10)) BETWEEN @dtmFrom AND @dtmTo " _
                   & " AND Z.kostenstelle = @kostenstelle"

            'conn = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                cmd.Parameters.AddWithValue("@dtmTo", dtmTo)
                cmd.Parameters.AddWithValue("@kostenstelle", intPlace)
                dt = GetMyData(cmd)
            End Using

            'conn.Close()
            'conn.Dispose()

            Return dt
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try
    End Function


    Public Sub FillWorkstationsCombo(ByRef cbo As ComboBox)
        Dim strSQL As String = ""
        Dim ds As DataSet = Nothing
        Dim da As MySqlDataAdapter = Nothing

        Dim conn As New MySqlConnection
        'conn = GetMyConnection("eProd")

        Try

            strSQL = "SELECT a.platznummer, p.slo_text FROM arbeitsplatz a LEFT JOIN prevodi p ON a.name = p.org_text AND p.tip = 'STATIONS' WHERE a.gruppe = 3 ORDER BY platznummer"

            Dim cmd As New MySqlCommand(strSQL, gConnEProd)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet
            da.Fill(ds)
            cbo.DataSource = ds.Tables(0).DefaultView
            cbo.DisplayMember = "slo_text"
            cbo.ValueMember = "platznummer"

            'conn.Close()
            'conn.Dispose()
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try
    End Sub

    Public Sub MoveOrderToMSora(ByVal strOrderNr As String, connKBS As SqlConnection, Optional ByVal intWorkingPlace As Integer = -1, Optional ByVal dtmFinnishDate As Date = Nothing)
        Dim strSQL As String
        Dim dt As DataTable
        Dim strEmail As String = ""
        Dim strPartnerId As String = ""
        Dim strCountry As String = ""
        Try

            strSQL = "SELECT order_nr, working_place, finnish_date, send_date FROM orders WHERE order_nr = @strOrderNr AND working_place = @intWorkingPlace"

            'Dim connM As New MySqlConnection
            'connM = GetMyConnection("msora")

            Dim cmd As New MySqlCommand(strSQL, gConnEProd)
            cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
            cmd.Parameters.AddWithValue("@intWorkingPlace", intWorkingPlace)
            dt = GetMyData(cmd)

            If dt.Rows.Count = 0 Then
                Call GetKlaesData(strOrderNr, strEmail, strPartnerId, strCountry, connKBS)

                strSQL = "INSERT INTO orders (order_nr, email_addresse, partner_id, working_place, finnish_date, send_date, language) " _
                    & " VALUES (@order_nr, @email_addresse, @partner_id, @working_place, @finnish_date, null, @language)"

                cmd = New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@order_nr", strOrderNr)
                cmd.Parameters.AddWithValue("@email_addresse", strEmail)
                cmd.Parameters.AddWithValue("@partner_id", strPartnerId)
                cmd.Parameters.AddWithValue("@working_place", intWorkingPlace)
                cmd.Parameters.AddWithValue("@finnish_date", IIf(dtmFinnishDate = Nothing, DBNull.Value, dtmFinnishDate))
                cmd.Parameters.AddWithValue("@language", GetLanguage(strCountry))

                cmd.ExecuteNonQuery()
            End If

            'connM.Close()
            'connM.Dispose()
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try
    End Sub
    Private Function GetLanguage(ByVal strCountry) As String
        Dim strSQL As String
        Dim dt As DataTable
        Dim strEmail As String = ""
        Dim strPartnerId As String = ""

        Try

            strSQL = "SELECT language FROM country WHERE country = @strCountry"

            'Dim connM As New MySqlConnection
            'connM = GetMyConnection("msora")

            Dim cmd As New MySqlCommand(strSQL, gConnEProd)
            cmd.Parameters.AddWithValue("@strCountry", strCountry)
            dt = GetMyData(cmd)
            If dt.Rows.Count > 0 Then
                Return dt(0)("language").ToString
            End If

            Return ""
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return ""
        End Try
    End Function

    Private Sub GetKlaesData(ByVal strOrderNr As String, ByRef strEmail As String, ByRef strPartnerId As String, ByRef strCountry As String, connKBS As SqlConnection)
        Dim strSQL As String = ""
        Dim dt As DataTable
        Dim strVorgang As String = ""

        'najprej dobim št. postopka

        'Dim conn As New MySqlConnection
        'conn = GetMyConnection("eProd")
        Try

            strSQL = "SELECT vorgang FROM auftrag WHERE kommissionsnummer = @strOrderNr"
            Dim cmd As New MySqlCommand(strSQL, gConnEProd)
            cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
            dt = GetMyData(cmd)

            If dt.Rows.Count > 0 Then
                strVorgang = dt(0)("vorgang").ToString
            End If

            If strVorgang <> "" Then

                strSQL = "SELECT IK.ITKONTAKT_WERT, GP.gpx_identnummer, land.lan_kurzbez " _
                    & " FROM VKVORGANGSTUB VVT, GP " _
                    & " LEFT JOIN ITKONTAKT IK ON IK.ITKONTAKT_IDGP = GP.gpx_id AND IK.ITKONTAKT_IVERBINDUNGSART = 4 " _
                    & " LEFT JOIN LAND ON GP.gpx_land = land.lan_id" _
                    & " WHERE GP.gpx_id = VVT.vsx_gp_id AND VVT.vsx_vorgangnr = @strVorgang "

                Dim cmdK As New SqlCommand(strSQL, connKBS)
                cmdK.Parameters.AddWithValue("@strVorgang", strVorgang)

                dt = GetData(cmdK)
                If dt.Rows.Count > 0 Then
                    strEmail = dt(0)("ITKONTAKT_WERT").ToString
                    strPartnerId = dt(0)("gpx_identnummer").ToString
                    strCountry = dt(0)("lan_kurzbez").ToString
                End If

            End If
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Sub

    Public Function TranslateWorkingPlace(ByVal intWorkingPlace As Integer, ByVal strLanguage As String) As String

        Return intWorkingPlace
    End Function


    Public Function GeteProdOrderDetailsAll(ByVal strNarocilo As String, ByVal strNalog As String, ByVal intDM As Integer) As Integer
        Dim dt As DataTable = Nothing
        Dim strSQL As String = ""
        'Dim conn As MySqlConnection

        Try
            'conn = GetMyConnection()
            strSQL = "SELECT count(*) FROM zustand Z, Auftrag A  " _
                & " WHERE Z.kommissionsnummer = A.kommissionsnummer " _
                & " AND Z.kostenstelle = @intDM AND Z.positionsnummer > 0 AND Z.teilekennung <> '' AND mid(Z.ipadresse,1,3) <> '---'"

            If strNalog <> "" Then
                strSQL = strSQL & " AND z.kommissionsnummer = @strNalog "
            End If

            If strNarocilo <> "" Then
                strSQL = strSQL & " AND a.auftragsnummer = @strNarocilo "
            End If

            'Using conn As MySqlConnection = GetMyConnection()
            Using cmd As New MySqlCommand(strSQL, gConnEProd)

                cmd.Parameters.AddWithValue("@strNalog", strNalog)
                cmd.Parameters.AddWithValue("@strNarocilo", strNarocilo)

                cmd.Parameters.AddWithValue("@intDM", intDM)

                dt = GetMyData(cmd)
                'conn.Close()
                'conn.Dispose()

                If dt.Rows.Count > 0 Then
                    Select Case intDM
                        Case 1, 3, 4
                            'Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)) / 3)
                            Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)))
                        Case 5
                            'Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)) / 2)
                            Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)))
                        Case Else
                            Return cls.Utils.DB2IntZero(dt(0)(0))

                    End Select

                Else
                    Return 0
                End If
                dt = Nothing
                dt.Dispose()
            End Using
            'End Using
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Function

    Public Function GetOrderDataEProd(ByVal strKommisionNr As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim strSQL As String = ""
        'Dim conn As MySqlConnection

        Try
            'conn = GetMyConnection()
            strSQL = "SELECT * FROM Auftrag A  " _
                & " WHERE A.kommissionsnummer = @strKommisionNr"


            'Using conn As MySqlConnection = GetMyConnection()
            Using cmd As New MySqlCommand(strSQL, gConnEProd)

                cmd.Parameters.AddWithValue("@strKommisionNr", strKommisionNr)

                dt = GetMyData(cmd)
                'conn.Close()
                'conn.Dispose()
                Return dt

            End Using
            'End Using
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Function GeteProdOrderDetailsBooked(ByVal strNarocilo As String, ByVal strNalog As String, ByVal intDM As Integer) As Integer
        Dim dt As DataTable = Nothing
        Dim strSQL As String = ""
        'Dim conn As MySqlConnection

        Try
            'conn = GetMyConnection()
            strSQL = "SELECT count(*) FROM zustand z, auftrag a " _
                & " WHERE z.kommissionsnummer = a.kommissionsnummer AND z.teilekennung <> '' " _
                & " AND z.kostenstelle = @intDM AND z.positionsnummer > 0 AND z.status = 100 AND mid(z.ipadresse,1,3) <> '---'"


            If strNalog <> "" Then
                strSQL = strSQL & " AND z.kommissionsnummer = @strNalog "
            End If

            If strNarocilo <> "" Then
                strSQL = strSQL & " AND a.auftragsnummer = @strNarocilo "
            End If

            'Using conn As MySqlConnection = GetMyConnection()
            Using cmd As New MySqlCommand(strSQL, gConnEProd)

                cmd.Parameters.AddWithValue("@strNalog", strNalog)
                cmd.Parameters.AddWithValue("@strNarocilo", strNarocilo)
                cmd.Parameters.AddWithValue("@intDM", intDM)

                dt = GetMyData(cmd)

                'conn.Close()
                'conn.Dispose()

                If dt.Rows.Count > 0 Then
                    Select Case intDM
                        Case 1, 3, 4
                            'Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)) / 3)
                            Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)))
                        Case 5
                            'Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)) / 2)
                            Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)))
                        Case Else
                            Return cls.Utils.DB2IntZero(dt(0)(0))

                    End Select
                Else
                    Return 0
                End If
                dt = Nothing
                dt.Dispose()
            End Using
            'End Using
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Function

    Public Function GeteProdOrderDetailsReturned(ByVal strNarocilo As String, ByVal strNalog As String, ByVal intDM As Integer) As Integer
        Dim dt As DataTable = Nothing
        Dim strSQL As String = ""
        'Dim conn As MySqlConnection

        Try
            'conn = GetMyConnection()
            strSQL = "SELECT count(*) FROM zustand z, auftrag a " _
                & " WHERE  z.auftragsnummer = a.auftragsnummer " _
                & " AND z.kostenstelle = @intDM AND z.positionsnummer > 0 AND z.status = 40 AND mid(z.ipadresse,1,3) <> '---'"

            If strNalog <> "" Then
                strSQL = strSQL & " AND z.kommissionsnummer = @strNalog "
            End If

            If strNarocilo <> "" Then
                strSQL = strSQL & " AND a.auftragsnummer = @strNarocilo "
            End If

            Using conn As MySqlConnection = GetMyConnection()
                Using cmd As New MySqlCommand(strSQL, conn)

                    cmd.Parameters.AddWithValue("@strNalog", strNalog)
                    cmd.Parameters.AddWithValue("@strNarocilo", strNarocilo)

                    cmd.Parameters.AddWithValue("@intDM", intDM)

                    dt = GetMyData(cmd)

                    'conn.Close()
                    'conn.Dispose()

                    If dt.Rows.Count > 0 Then
                        Select Case intDM
                            Case 1, 3, 4
                                'Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)) / 3)
                                Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)))
                            Case 5
                                'Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)) / 2)
                                Return cls.Utils.DB2IntZero(cls.Utils.DB2IntZero(dt(0)(0)))
                            Case Else
                                Return cls.Utils.DB2IntZero(dt(0)(0))

                        End Select
                    Else
                        Return 0
                    End If

                    dt = Nothing
                    dt.Dispose()

                End Using
            End Using
        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Function

    Public Sub UpdateEProdFachnummer(ByVal strKommission As String, ByVal strTeilekennung As String, ByVal dtmValue As Date, ByVal intType As Integer)
        Dim strSQL As String = ""

        If Not dtmValue = Nothing Then
            strSQL = "SELECT * FROM fachnummer WHERE kommissionsnummer = @strKommission AND teilekennung = @strTeilekennung AND wagenart = @intType AND art = 1 AND zusatz = 0"
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@strKommission", strKommission)
                cmd.Parameters.AddWithValue("@strTeilekennung", strTeilekennung & " " & strKommission)
                cmd.Parameters.AddWithValue("@intType", intType)
                Dim dt As DataTable = GetMyData(cmd)
                If dt.Rows.Count = 1 Then
                    strSQL = "UPDATE fachnummer SET fachnummer = @dtmValue WHERE kommissionsnummer = @strKommission AND teilekennung = @strTeilekennung AND wagenart = @intType AND art = 1 AND zusatz = 0"
                    Using cmd1 As New MySqlCommand(strSQL, gConnEProd)
                        cmd1.Parameters.AddWithValue("@dtmValue", Format(dtmValue, "dd-MM-yyyy"))
                        cmd1.Parameters.AddWithValue("@strKommission", strKommission)
                        cmd1.Parameters.AddWithValue("@strTeilekennung", strTeilekennung & " " & strKommission)
                        cmd1.Parameters.AddWithValue("@intType", intType)
                        cmd1.ExecuteNonQuery()
                    End Using
                Else
                    strSQL = "INSERT INTO fachnummer (kommissionsnummer, teilekennung, fachnummer, art, zusatz, wagenart, fachnummerWaagerecht, fachnummerSenkrecht, boxnummer, bemerkung1, bemerkung2, barcode) " _
                        & " VALUES (@strKommission, @strTeilekennung, @dtmValue, 1, 0, @intType, 0, 0, '','','','') "
                    Using cmd1 As New MySqlCommand(strSQL, gConnEProd)
                        cmd1.Parameters.AddWithValue("@strKommission", strKommission)
                        cmd1.Parameters.AddWithValue("@strTeilekennung", strTeilekennung & " " & strKommission)
                        cmd1.Parameters.AddWithValue("@dtmValue", Format(dtmValue, "dd-MM-yyyy"))
                        cmd1.Parameters.AddWithValue("@intType", intType)
                        cmd1.ExecuteNonQuery()
                    End Using
                End If
            End Using
        End If
    End Sub

    Public Sub GetEprodInfo(ByVal strOrderNr As String, ByVal strArcode As String, ByVal strArname As String, ByVal strExtraDesc As String, ByRef strKommissionsNr As String, ByRef strEprodTeilekennung As String, ByVal dtUsed As DataTable, ByVal intRowUsed As Integer, ByVal dblQuantity As Double)
        'dobim vse kommisione 
        Dim strSQL As String = ""
        Dim strTeilekennung As String = ""
        Dim strProfilNr As String = ""
        Dim intProfilNr As Integer = -1
        Dim intStueckPosition As Integer = -1
        Dim intPositionNummer As Integer = -1


        strSQL = "SELECT * FROM zustand WHERE kostenstelle = 8 AND kommissionsnummer IN (SELECT kommissionsnummer FROM auftrag WHERE auftragsnummer = @strOrderNr) AND positionsnummer > 0"
        Using cmd As New MySqlCommand(strSQL, gConnEProd)
            cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
            Dim dt As DataTable = GetMyData(cmd)

            For i = 0 To dt.Rows.Count - 1


                If Not FindInUsed(dtUsed, dt(i)("kommissionsnummer").ToString.Trim, dt(i)("teilekennung").ToString.Trim) Then

                    strProfilNr = Mid(dt(i)("teilekennung").ToString, 11, 6)
                    intProfilNr = -1
                    If IsNumeric(strProfilNr) Then
                        intProfilNr = CInt(strProfilNr)
                    End If


                    intStueckPosition = cls.Utils.DB2IntZero(dt(i)("StueckPosition"))
                    intPositionNummer = cls.Utils.DB2IntZero(dt(i)("positionsnummer"))

                    If intProfilNr > 0 Then

                        strSQL = "SELECT inhaltmarke FROM textmarken WHERE kommissionsnummer = @strKommissionsNr " _
                        & " AND nummerliste IN (44,47) AND inhaltmarke = @strArcode" _
                        & " AND kennungmarke = 8001 AND profilnummer = @intProfilNr " _
                        & " AND positionsnummer = @intPositionsNummer "

                        Using cmd1 As New MySqlCommand(strSQL, gConnEProd)
                            cmd1.Parameters.AddWithValue("@strKommissionsNr", dt(i)("kommissionsnummer").ToString)
                            cmd1.Parameters.AddWithValue("@strArcode", strArcode)
                            cmd1.Parameters.AddWithValue("@intProfilNr", intProfilNr)
                            cmd1.Parameters.AddWithValue("@intPositionsNummer", intPositionNummer)
                            Dim dt1 As DataTable = GetMyData(cmd1)
                            If dt1.Rows.Count > 0 Then

                                If strExtraDesc <> "" Then
                                    strSQL = "SELECT inhaltmarke FROM textmarken WHERE kommissionsnummer = @strKommissionsNr " _
                                        & " AND nummerliste IN (44,45,46,47) AND inhaltmarke = @strExtraDesc" _
                                        & " AND kennungmarke = 8002 AND profilnummer = @intProfilNr " _
                                        & " AND positionsnummer = @intPositionsNummer "
                                    Using cmd2 As New MySqlCommand(strSQL, gConnEProd)
                                        cmd2.Parameters.AddWithValue("@strKommissionsNr", dt(i)("kommissionsnummer").ToString)
                                        cmd2.Parameters.AddWithValue("@strExtraDesc", strExtraDesc)
                                        cmd2.Parameters.AddWithValue("@intProfilNr", intProfilNr)
                                        cmd2.Parameters.AddWithValue("@intPositionsNummer", intPositionNummer)
                                        Dim dt2 As DataTable = GetMyData(cmd2)
                                        If dt2.Rows.Count > 0 Then
                                            strKommissionsNr = dt(i)("kommissionsnummer").ToString.Trim
                                            strEprodTeilekennung = dt(i)("teilekennung").ToString.Trim
                                            Exit Sub
                                        End If
                                    End Using
                                Else
                                    strKommissionsNr = dt(i)("kommissionsnummer").ToString.Trim
                                    strEprodTeilekennung = dt(i)("teilekennung").ToString.Trim
                                    Exit Sub
                                End If
                            End If

                        End Using
                    End If
                End If
            Next

        End Using

    End Sub


    Private Function FindInUsed(ByVal dt As DataTable, ByVal strKommision As String, ByVal strTeilekennung As String)
        For i = 0 To dt.Rows.Count - 1
            If dt(i)("kommissionsnummer").ToString = strKommision And dt(i)("teilekennung").ToString = strTeilekennung Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub UpdateProductionText(ByVal strOrderNr As String, ByVal strKommission As String, ByVal strName As String, ByVal strProductionText As String, ByVal strOrderDescription As String, ByVal dtmModDate As Date, conntools As SqlConnection)
        'dodam še v msora_extra.reclamation

        Dim strSQL As String = ""
        Try

            'Posodobitev teksta pri seznamu nalogov
            'If strProductionText <> "" Then
            Dim strNewKundenName As String = ""
            Dim z As Integer = -1
            z = InStr(strName, ";")
            If z > 0 Then
                strNewKundenName = Mid(strName, 1, z - 1)
            Else
                strNewKundenName = strName
            End If

            If strOrderDescription <> "" Then
                z = InStr(strNewKundenName, "{")
                If z > 0 Then
                    strNewKundenName = Mid(strNewKundenName, 1, z - 1) & " {" & strOrderDescription & "}"
                Else
                    strNewKundenName = strNewKundenName & " {" & strOrderDescription & "}"
                End If
            End If

            If strProductionText <> "" Then
                z = InStr(strNewKundenName, "(")
                If z > 0 Then
                    strNewKundenName = Mid(strNewKundenName, 1, z - 1) & " (" & strProductionText & ")"
                Else
                    strNewKundenName = strNewKundenName & " (" & strProductionText & ")"
                End If
            End If



            strSQL = "UPDATE mengen SET kundenname = @strNewKundenName WHERE kommissionsnummer = @strKommission AND auftragsnummer = @strOrderNr"

            'Using conn As MySqlConnection = GetMyConnection("eProd")
            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@strNewKundenName", strNewKundenName)
                cmd.Parameters.AddWithValue("@strKommission", strKommission)
                cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
                cmd.ExecuteNonQuery()

                strSQL = "INSERT INTO eprod_change_prod_text (auftragsnummer, kommissionsnummer, production_text, date_changed) VALUES (@auftragsnummer, @kommissionsnummer, @production_text, @date_changed)"
                Using cmd1 As New SqlCommand(strSQL, conntools)
                    cmd1.Parameters.AddWithValue("@auftragsnummer", strOrderNr)
                    cmd1.Parameters.AddWithValue("@kommissionsnummer", strKommission)
                    cmd1.Parameters.AddWithValue("@production_text", strProductionText)
                    cmd1.Parameters.AddWithValue("@date_changed", dtmModDate)
                    cmd1.ExecuteNonQuery()

                End Using
            End Using
            'End If

        Catch ex As Exception
            modLog.AddToErrorLog(ex.ToString)
        End Try

    End Sub

    Public Function ProcessMawiDates(ByVal dtOrders As DataTable, connMAWI As SqlConnection, connTools As SqlConnection) As String
        Dim strArticleCode As String = ""
        Dim strArticleDesc As String = ""
        Dim strArticleLongDesc As String = ""
        Dim dblQuantity As Double = 0
        Dim strOrderNr As String = ""
        Dim dtZustand As DataTable = Nothing
        Dim strSQL As String = ""

        Dim strKommissionsNummer As String = ""
        Dim intType As Integer = -1
        Dim dtmOrderDatum As Date = Nothing
        Dim dtmConfirmationDatum As Date = Nothing
        Dim dtmDeliveryDatum As Date = Nothing
        Dim strTeilekennung As String = ""
        Dim lngOrderId As Long = -1
        Dim dtmExpectationDate As Date
        Dim dtmExpectationDate2 As Date
        Dim dtmDeliveryDate As Date = Nothing
        Dim intIdDesc As Integer = -1
        Dim strLog As String = ""

        If dtOrders Is Nothing Then Return "Empty order table"

        Try


            For j = 0 To dtOrders.Rows.Count - 1

                Dim dtm0 As Date = Nothing
                Dim dtm1 As Date = Nothing
                Dim dtm2 As Date = Nothing

                Debug.Print("Order: " & j.ToString)

                If Not dtOrders(j)("order_date") Is DBNull.Value Then
                    dtm0 = CDate(dtOrders(j)("order_date").ToString)
                End If
                If Not dtOrders(j)("datum_potrjene_dobave") Is DBNull.Value Then
                    dtm1 = CDate(dtOrders(j)("datum_potrjene_dobave").ToString)
                End If
                If Not dtOrders(j)("datum_dobave") Is DBNull.Value Then
                    dtm2 = CDate(dtOrders(j)("datum_dobave").ToString)
                End If

                If strOrderNr <> dtOrders(j)("order_nr").ToString Then
                    strOrderNr = dtOrders(j)("order_nr").ToString
                    strSQL = "SELECT * FROM zustand WHERE kostenstelle = 8 AND kommissionsnummer IN (SELECT kommissionsnummer FROM auftrag WHERE auftragsnummer = @strOrderNr) AND positionsnummer > 0"
                    Using cmd As New MySqlCommand(strSQL, gConnEProd)
                        cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
                        dtZustand = cls.msora.DB_MSora.GetMyData(cmd)
                    End Using
                End If

                If Not dtZustand Is Nothing Then
                    If dtZustand.Rows.Count > 0 Then

                        Debug.Print("Order Id: " & j.ToString)

                        lngOrderId = cls.Utils.DB2Lng(dtOrders(j)("id_order"))
                        dtmExpectationDate = CDate(dtOrders(j)("latest_delivery_date").ToString)
                        If dtOrders(j)("datum_potrjene_dobave").ToString <> "" Then
                            dtmExpectationDate2 = CDate(dtOrders(j)("datum_potrjene_dobave").ToString)
                        Else
                            dtmExpectationDate2 = Nothing
                        End If
                        If dtOrders(j)("datum_dobave").ToString <> "" Then
                            dtmDeliveryDate = CDate(dtOrders(j)("datum_dobave").ToString)
                        Else
                            dtmDeliveryDate = Nothing
                        End If

                        Dim dtArticles As DataTable
                        Dim dtText As DataTable = CreateTableTexts()

                        dtArticles = GetMawiOrderArticles(lngOrderId, dtmExpectationDate, dtmExpectationDate2, cls.Config.GetMawiArticleTypes, connMAWI)

                        For i = 0 To dtArticles.Rows.Count - 1

                            strArticleCode = dtArticles(i)("arcode").ToString
                            strArticleDesc = dtArticles(i)("arname").ToString
                            strArticleLongDesc = dtArticles(i)("article_description").ToString
                            dblQuantity = cls.Utils.DB2Dbl(dtArticles(i)("quantity"))
                            intIdDesc = cls.Utils.DB2IntZero(dtArticles(i)("quantity"))

                            Call RefreshArticles(strOrderNr, dtZustand, dtText, strArticleCode, strArticleDesc, strArticleLongDesc, dblQuantity, dtm0, dtm1, dtm2, intIdDesc)

                        Next

                        'v dtText pustim samo zapis z najvišjo compare_density

                        dtText.DefaultView.Sort = "teilekennung, compare_density DESC"

                        Dim dtTextFiltered As DataTable = CreateTableTexts()

                        For i = 0 To dtText.Rows.Count - 1
                            If strTeilekennung <> dtText(i)("teilekennung").ToString Then
                                Call CopyDataRecord(dtTextFiltered, dtText, i)
                            End If
                            strTeilekennung = dtText(i)("teilekennung").ToString
                        Next

                        'posodobim eProd
                        For i = 0 To dtTextFiltered.Rows.Count - 1
                            strKommissionsNummer = dtTextFiltered(i)("kommissionsnummer").ToString
                            strTeilekennung = dtTextFiltered(i)("teilekennung").ToString


                            If Not dtTextFiltered(i)("date0") Is DBNull.Value Then
                                If CDate(dtTextFiltered(i)("date0")) > CDate("31.12.1900") Then
                                    intType = 0
                                    If strTeilekennung <> "" Then
                                        dtmOrderDatum = CDate(dtTextFiltered(i)("date0").ToString)
                                    Else
                                        dtmOrderDatum = Nothing
                                    End If
                                    If Not FindEprodDate(strKommissionsNummer, strTeilekennung, dtmOrderDatum, intType, connTools) Then
                                        Call UpdateEProdFachnummer(strKommissionsNummer, strTeilekennung, dtmOrderDatum, intType)
                                        strLog = strLog & vbCrLf & "Komm: " & strKommissionsNummer & " " & strArticleCode & " " & strArticleDesc & " Datum prič. dobave: " & Format(dtmOrderDatum, "short date")
                                        Call InsertEprodDate(strKommissionsNummer, strTeilekennung, dtmOrderDatum, intType, connTools)
                                    End If
                                End If
                            End If

                            If Not dtTextFiltered(i)("date1") Is DBNull.Value Then
                                If CDate(dtTextFiltered(i)("date1")) > CDate("31.12.1900") Then
                                    intType = 1
                                    If strTeilekennung <> "" Then
                                        dtmConfirmationDatum = CDate(dtTextFiltered(i)("date1").ToString)
                                    Else
                                        dtmConfirmationDatum = Nothing
                                    End If
                                    If Not FindEprodDate(strKommissionsNummer, strTeilekennung, dtmOrderDatum, intType, connTools) Then
                                        Call UpdateEProdFachnummer(strKommissionsNummer, strTeilekennung, dtmConfirmationDatum, intType)
                                        strLog = strLog & vbCrLf & "Komm: " & strKommissionsNummer & " " & strArticleCode & " " & strArticleDesc & " Datum potrditve: " & Format(dtmConfirmationDatum, "short date")
                                        Call InsertEprodDate(strKommissionsNummer, strTeilekennung, dtmOrderDatum, intType, connTools)
                                    End If
                                End If
                            End If

                            If Not dtTextFiltered(i)("date2") Is DBNull.Value Then
                                If CDate(dtTextFiltered(i)("date2")) > CDate("31.12.1900") Then
                                    intType = 2
                                    If strTeilekennung <> "" Then
                                        dtmDeliveryDatum = CDate(dtTextFiltered(i)("date2").ToString)
                                    Else
                                        dtmDeliveryDatum = Nothing
                                    End If
                                    If Not FindEprodDate(strKommissionsNummer, strTeilekennung, dtmOrderDatum, intType, connTools) Then
                                        Call UpdateEProdFachnummer(strKommissionsNummer, strTeilekennung, dtmDeliveryDatum, intType)
                                        strLog = strLog & vbCrLf & "Komm: " & strKommissionsNummer & " " & strArticleCode & " " & strArticleDesc & " Datum dobave: " & Format(dtmDeliveryDatum, "short date")
                                        Call InsertEprodDate(strKommissionsNummer, strTeilekennung, dtmOrderDatum, intType, connTools)
                                    End If
                                End If
                            End If
                        Next

                    End If
                End If
            Next

            Return strLog
        Catch ex As Exception
            Return ex.ToString
        End Try

    End Function
    Private Function FindEprodDate(ByVal strKommissionsNummer As String, ByVal strTeilekennung As String, ByVal dtmOrderDatum As Date, ByVal intType As Integer, connTools As SqlConnection) As Boolean
        Dim strSQL As String
        strSQL = "SELECT * FROM eprod_dates WHERE kommissionsnummer = @strKommissionsNummer AND teilekennung = @strTeilekennung AND date_value = @dtmOrderDatum AND date_type = @intType"
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strKommissionsNummer", strKommissionsNummer)
            cmd.Parameters.AddWithValue("@strTeilekennung", strTeilekennung)
            cmd.Parameters.AddWithValue("@dtmOrderDatum", dtmOrderDatum)
            cmd.Parameters.AddWithValue("@intType", intType)
            Dim dt As DataTable = GetData(cmd)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Using
    End Function

    Private Sub InsertEprodDate(ByVal strKommissionsNummer As String, ByVal strTeilekennung As String, ByVal dtmOrderDatum As Date, ByVal intType As Integer, connTools As SqlConnection)
        Dim strSQL As String
        strSQL = "INSERT INTO eprod_dates (kommissionsnummer, teilekennung, date_value, date_type, date_inserted) " _
            & " VALUES (@strKommissionsNummer, @strTeilekennung, @dtmOrderDatum, @intType, @dtmNow) "
        Using cmd As New SqlCommand(strSQL, connTools)
            cmd.Parameters.AddWithValue("@strKommissionsNummer", strKommissionsNummer)
            cmd.Parameters.AddWithValue("@strTeilekennung", strTeilekennung)
            cmd.Parameters.AddWithValue("@dtmOrderDatum", dtmOrderDatum)
            cmd.Parameters.AddWithValue("@intType", intType)
            cmd.Parameters.AddWithValue("@dtmNow", Now)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub RefreshArticles(ByVal strOrderNr As String, ByVal dtZustand As DataTable, ByRef dtText As DataTable, ByVal strArticleCode As String, ByVal strArticleDesc As String, _
                                ByVal strArticleLongDesc As String, ByVal dblQuantity As Double, ByVal dtm0 As Date, ByVal dtm1 As Date, ByVal dtm2 As Date, ByVal intIdDesc As Integer)

        Dim strKommissionNr As String = ""
        Dim strSQL As String = ""
        Dim intProfilNummer As Integer = -1
        Dim intPositionsNummer As Integer = -1
        Dim strProfilNummer As String = ""
        Dim dtTextLocal As DataTable
        Dim intUjemanje As Integer = 0
        Dim strArticleCodeLocal As String = ""
        Dim strArticleDescLocal As String = ""
        Dim strArticleLongDescLocal As String = ""
        Dim dblQuantityLocal As Double = -1
        Dim strTeilekennung As String

        strKommissionNr = ""

        For i = 0 To dtZustand.Rows.Count - 1

            strKommissionNr = dtZustand(i)("kommissionsnummer").ToString
            strTeilekennung = dtZustand(i)("teilekennung").ToString
            strProfilNummer = Mid(strTeilekennung, 11, 6)

            If IsNumeric(strProfilNummer) Then

                intProfilNummer = strProfilNummer
                intPositionsNummer = cls.Utils.DB2IntZero(dtZustand(i)("positionsnummer"))

                dtTextLocal = CreateTableTexts()

                Call GetTextsDM8ForOrder(strOrderNr, strKommissionNr, intProfilNummer, intPositionsNummer, strArticleCode, dtTextLocal)

                If dtTextLocal.Rows.Count = 0 Then
                    Call GetTextsDM8ForOrderNew(strOrderNr, strKommissionNr, intProfilNummer, intPositionsNummer, strArticleCode, dtTextLocal)
                End If

                intUjemanje = 0
                For j = 0 To dtTextLocal.Rows.Count - 1
                    strArticleCodeLocal = dtTextLocal(j)("shortinhaltmarke").ToString
                    strArticleDescLocal = dtTextLocal(j)("shortinhaltmarke").ToString
                    strArticleLongDescLocal = dtTextLocal(j)("longinhaltmarke").ToString
                    dblQuantityLocal = cls.Utils.DB2Dbl(dtTextLocal(j)("quantity"))


                    If intIdDesc > 0 Then
                        'najprej preverim, če se vse ujema
                        If strArticleCode = strArticleCodeLocal And strArticleDesc = strArticleDescLocal And strArticleLongDesc = strArticleLongDescLocal And dblQuantity = dblQuantityLocal Then
                            If intUjemanje < 4 Then
                                intUjemanje = 4
                                Call AddToTextTable(dtText, dtTextLocal, j, strTeilekennung, dtm0, dtm1, dtm2, intUjemanje)
                            End If
                            Return
                        End If

                        If strArticleCode = strArticleCodeLocal And strArticleDesc = strArticleDescLocal And strArticleLongDesc = strArticleLongDescLocal Then
                            If intUjemanje < 3 Then
                                intUjemanje = 3
                                Call AddToTextTable(dtText, dtTextLocal, j, strTeilekennung, dtm0, dtm1, dtm2, intUjemanje)
                            End If

                        End If
                    Else
                        'najprej preverim, če se vse ujema
                        If strArticleCode = strArticleCodeLocal And strArticleDesc = strArticleDescLocal And strArticleLongDesc = strArticleLongDescLocal And dblQuantity = dblQuantityLocal Then
                            If intUjemanje < 4 Then
                                intUjemanje = 4
                                Call AddToTextTable(dtText, dtTextLocal, j, strTeilekennung, dtm0, dtm1, dtm2, intUjemanje)
                            End If
                            Return
                        End If

                        If strArticleCode = strArticleCodeLocal And strArticleDesc = strArticleDescLocal And strArticleLongDesc = strArticleLongDescLocal Then
                            If intUjemanje < 3 Then
                                intUjemanje = 3
                                Call AddToTextTable(dtText, dtTextLocal, j, strTeilekennung, dtm0, dtm1, dtm2, intUjemanje)
                            End If

                        End If
                    End If

                    If strArticleCode = strArticleCodeLocal And strArticleDesc = strArticleDescLocal And dblQuantity = dblQuantityLocal Then
                        If intUjemanje < 3 Then
                            intUjemanje = 3
                            Call AddToTextTable(dtText, dtTextLocal, j, strTeilekennung, dtm0, dtm1, dtm2, intUjemanje)
                        End If

                    End If

                    If strArticleCode = strArticleCodeLocal And strArticleDesc = strArticleDescLocal Then
                        If intUjemanje < 2 Then
                            intUjemanje = 2
                            Call AddToTextTable(dtText, dtTextLocal, j, strTeilekennung, dtm0, dtm1, dtm2, intUjemanje)
                        End If
                    End If

                    If strArticleCode = strArticleCodeLocal Then
                        If intUjemanje < 1 Then
                            intUjemanje = 1
                            Call AddToTextTable(dtText, dtTextLocal, j, strTeilekennung, dtm0, dtm1, dtm2, intUjemanje)
                        End If
                    End If

                Next
            End If
        Next

    End Sub
    Private Sub AddToTextTable(ByRef dtText As DataTable, ByVal dtTextLocal As DataTable, ByVal iRow As Integer, ByVal strTeilekennung As String, ByVal dtm0 As Date, ByVal dtm1 As Date, ByVal dtm2 As Date, ByVal intCompareDensity As Integer)

        Dim dr As DataRow = dtText.NewRow

        dr("auftragsnummer") = dtTextLocal(iRow)("auftragsnummer")
        dr("kommissionsnummer") = dtTextLocal(iRow)("kommissionsnummer")
        dr("zuordnung") = dtTextLocal(iRow)("zuordnung")
        dr("teilekennung") = strTeilekennung
        dr("kennungmarke") = dtTextLocal(iRow)("kennungmarke")
        dr("inhaltmarke") = dtTextLocal(iRow)("inhaltmarke")
        dr("zeilemarke") = dtTextLocal(iRow)("zeilemarke")
        dr("profilnummer") = dtTextLocal(iRow)("profilnummer")
        dr("positionsnummer") = dtTextLocal(iRow)("positionsnummer")

        dr("longinhaltmarke") = dtTextLocal(iRow)("longinhaltmarke")
        dr("shortinhaltmarke") = dtTextLocal(iRow)("shortinhaltmarke")
        dr("quantity") = dtTextLocal(iRow)("quantity")
        dr("unit") = dtTextLocal(iRow)("unit")

        dr("compare_density") = intCompareDensity

        dr("date0") = dtm0
        dr("date1") = dtm1
        dr("date2") = dtm2

        dtText.Rows.Add(dr)

    End Sub

    Public Function CreateTableEprodBohrshema() As DataTable
        Dim dt As New DataTable("teprod")
        'prva vrstica - izpišem vse


        Dim column As DataColumn = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "status"
            .Caption = "Status"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "kommissionsnummer"
            .Caption = "kommissionsnummer"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "nummerliste"
            .Caption = "nummerliste"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "kennungmarke"
            .Caption = "kennungmarke"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "kennungmarke1"
            .Caption = "kennungmarke1"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "kennungmarke2"
            .Caption = "kennungmarke2"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "positionsnummer"
            .Caption = "positionsnummer"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "profilnummer"
            .Caption = "profilnummer"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.Int32")
            .ColumnName = "zeilemarke"
            .Caption = "zeilemarke"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "inhaltmarke"
            .Caption = "inhaltmarke"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "bohrshema1"
            .Caption = "bohrshema1"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "bohrshema2"
            .Caption = "bohrshema2"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "newbohrshema1"
            .Caption = "newbohrshema1"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "newbohrshema2"
            .Caption = "newbohrshema2"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        column = New DataColumn
        With column
            .DataType = System.Type.GetType("System.String")
            .ColumnName = "kos_oznaka"
            .Caption = "Oznaka"
            .ReadOnly = False
            .Unique = False
            .DefaultValue = DBNull.Value
        End With
        dt.Columns.Add(column)

        Return dt
    End Function

    Public Function GetInhaltValue(strInhalt As String, strCode As String) As String
        Dim strReturn As String = ""
        Dim dtInhaltMarke As DataTable

        ''{"7002":"smr","7064":"EPR01-  1-  -Ok- 910","7001":"2702","7092":"OZ002","7091":"ON305","7129":"1000","7127":"s407","7126":"940","7125":"68","7124":"72","7046":"","7015":"1","2001":"T16EPR01","7150":"58","7075":"68.0","7044":"","7013":"1","7074":"60","7012":"1","7042":"","7011":"Okvir levo","7010":"smreka","7026":"910","7131":"72","7130":"78","7085":"KP304","7023":"","7083":"KP304","7020":"Okvir le ON305 OZ002","7149":"59","7038":"SORA 03-08","7065":"","7003":"3"}

        Dim strTemp As String = strInhalt
        Dim blnError As Boolean = False
        strTemp = strTemp.Substring(1)
        strTemp = strTemp.Substring(0, strTemp.Length - 1)

        dtInhaltMarke = CreateArrayInhalt(strTemp, blnerror)

        If Not blnError Then
            For i = 0 To dtInhaltMarke.Rows.Count - 1
                '{"2001":"T161598","8004":"kos","8002":"žaluzija tip IZO MEDLE","8001":"ŽALUZIJE IZO MEDLE","8007":"1.000"}
                If dtInhaltMarke(i)("kennungmarke").ToString.Trim = strCode Then
                    Return dtInhaltMarke(i)("inhaltmarke").ToString.Trim
                End If
            Next
        Else
            Return strCode
        End If

        Return strReturn
    End Function

End Module
