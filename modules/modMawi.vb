Imports eProdService.cls.msora.DB_MSora
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient

Module modMawi

    Public Function GetMawiOrdersService(ByVal intType As Integer, ByVal dtmFrom As Date, ByVal dtmTo As Date, ByVal strOrderNr As String, connMawi As SqlConnection) As DataTable
        Dim strSQL As String = ""
        Dim strDobavnica As String
        Dim dtmDatumDobave As Date
        Dim lngIdComplaint As Long


        Try

            Select Case intType
                Case 0 'datum naročila
                    strSQL = "SELECT DISTINCT c.name as order_nr, c.id_contract, o.id_order, o.id_order_state, pb.id_partner_head, pb.id_partner_branch, o.name as docname, o.description, o.order_date, " _
                        & " o.latest_delivery_date,  os.name as osname, pb.name as pbname, oc.name as St_potrditve, ocd.id_order_confirmation, " _
                        & " ocd.expectation_date as datum_potrjene_dobave, u.id_complaint, oc.date as order_confirmation_date " _
                        & " FROM [order] o " _
                        & " INNER JOIN used u ON u.id_order = o.id_order " _
                        & " INNER JOIN contract c ON c.id_contract = u.id_contract " _
                        & " INNER JOIN partner_branch pb ON pb.id_partner_head = o.id_partner_head AND pb.id_partner_branch = o.id_partner_branch " _
                        & " INNER JOIN order_state os ON os.id_order_state = o.id_order_state " _
                        & " LEFT JOIN order_confirmation oc ON oc.id_order_confirmation = u.id_order_confirmation  	AND oc.id_order_confirmation > 0 " _
                        & " LEFT JOIN order_confirmation_details ocd ON ocd.id_order_confirmation = oc.id_order_confirmation  	AND ocd.id_order_confirmation > 0 " _
                            & " AND ocd.id_article = u.id_article AND ocd.id_color = u.id_color AND ocd.id_goods_group = u.id_goods_group "


                    If strOrderNr <> "" Then
                        strSQL = strSQL + " WHERE c.name LIKE '" & strOrderNr.Replace("*", "%") & "' AND u.id_order > 0"
                    Else
                        strSQL = strSQL + " WHERE 2 > 1 AND u.id_order > 0"
                    End If
                    If Not dtmFrom = Nothing Then
                        strSQL = strSQL + " AND o.order_date >= @dtmFrom "
                    End If
                    If Not dtmTo = Nothing Then
                        strSQL = strSQL + " AND o.order_date <= @dtmTo "
                    End If

                Case 1 'datum potrditve dobave
                    strSQL = "SELECT DISTINCT c.name as order_nr, c.id_contract, o.id_order, o.id_order_state, pb.id_partner_head, pb.id_partner_branch, o.name as docname, o.description, o.order_date, " _
                        & " o.latest_delivery_date,  os.name as osname, pb.name as pbname, oc.name as St_potrditve, ocd.id_order_confirmation, " _
                        & " ocd.expectation_date as datum_potrjene_dobave, u.id_complaint, oc.date as order_confirmation_date " _
                        & " FROM [order] o " _
                        & " INNER JOIN used u ON u.id_order = o.id_order " _
                        & " INNER JOIN contract c ON c.id_contract = u.id_contract " _
                        & " INNER JOIN partner_branch pb ON pb.id_partner_head = o.id_partner_head AND pb.id_partner_branch = o.id_partner_branch " _
                        & " INNER JOIN order_state os ON os.id_order_state = o.id_order_state " _
                        & " LEFT JOIN order_confirmation oc ON oc.id_order_confirmation = u.id_order_confirmation  	AND oc.id_order_confirmation > 0 " _
                        & " LEFT JOIN order_confirmation_details ocd ON ocd.id_order_confirmation = oc.id_order_confirmation AND ocd.id_order_confirmation > 0 " _
                            & " AND ocd.id_article = u.id_article AND ocd.id_color = u.id_color AND ocd.id_goods_group = u.id_goods_group "


                    If strOrderNr <> "" Then
                        strSQL = strSQL + " WHERE c.name LIKE '" & strOrderNr.Replace("*", "%") & "' AND u.id_order > 0"
                    Else
                        strSQL = strSQL + " WHERE 2 > 1 AND u.id_order > 0"
                    End If

                    If Not dtmFrom = Nothing Then
                        strSQL = strSQL + " AND oc.date >= @dtmFrom "
                    End If
                    If Not dtmTo = Nothing Then
                        strSQL = strSQL + "  AND oc.date <= @dtmTo "
                    End If

                Case 2 'datum dobave
                    Dim dtMawi As DataTable = CreateMawiOrdersTable()

                    'delivery
                    strSQL = "SELECT * FROM delivery WHERE 2>1  AND date >= @dtmFrom AND date <= @dtmTo "


                    Using cmd As New SqlCommand(strSQL, connmawi)

                        cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                        cmd.Parameters.AddWithValue("@dtmTo", DateAdd("d", +1, dtmTo))


                        Dim dtDelivery = cls.msora.DB_MSora.GetData(cmd)

                        For i = 0 To dtDelivery.Rows.Count - 1
                            'pridobim podatke o potrditvi dobave
                            strSQL = "SELECT DISTINCT oc.name as St_potrditve, ocd.id_order_confirmation, " _
                                & " ocd.expectation_date as datum_potrjene_dobave, oc.date as order_confirmation_date, ooc.id_order " _
                                & " FROM order_confirmation oc " _
                                & " INNER JOIN order_confirmation_details ocd ON ocd.id_order_confirmation = oc.id_order_confirmation AND ocd.id_order_confirmation > 0 " _
                                & " INNER JOIN order_to_order_confirmation ooc ON ooc.id_order_confirmation = ocd.id_order_confirmation  " _
                                & " INNER JOIN order_confirmation_to_delivery octd ON octd.id_order_confirmation = ooc.id_order_confirmation AND octd.id_order_confirmation_details = ooc.id_order_confirmation_details " _
                                & " WHERE octd.id_delivery = @id_delivery AND oc.id_order_confirmation > 0 "

                            Using cmd2 As New SqlCommand(strSQL, connmawi)
                                cmd2.Parameters.AddWithValue("@id_delivery", dtDelivery(i)("id_delivery"))
                                Dim dtConfirmation As DataTable = cls.msora.DB_MSora.GetData(cmd2)

                                For j = 0 To dtConfirmation.Rows.Count - 1
                                    strSQL = "SELECT DISTINCT c.name as order_nr, c.id_contract, o.id_order, o.id_order_state, pb.id_partner_head, pb.id_partner_branch, o.name as docname, o.description, o.order_date, " _
                                        & " o.latest_delivery_date,  os.name as osname, pb.name as pbname, u.id_complaint " _
                                        & " FROM [order] o " _
                                        & " INNER JOIN order_details od ON od.id_order = o.id_order " _
                                        & " INNER JOIN used u ON u.id_order = o.id_order " _
                                        & " INNER JOIN contract c ON c.id_contract = u.id_contract " _
                                        & " INNER JOIN partner_branch pb ON pb.id_partner_head = o.id_partner_head AND pb.id_partner_branch = o.id_partner_branch " _
                                        & " INNER JOIN order_state os ON os.id_order_state = o.id_order_state " _
                                        & " WHERE u.id_order_confirmation = @oc_id_order_confirmation " _
                                        & " AND o.id_order = @id_order "


                                    If strOrderNr <> "" Then
                                        strSQL = strSQL + " AND c.name LIKE '" & strOrderNr.Replace("*", "%") & "' AND u.id_order > 0"
                                    End If

                                    Using cmd3 As New SqlCommand(strSQL, connmawi)
                                        cmd3.Parameters.AddWithValue("@oc_id_order_confirmation", dtConfirmation(j)("id_order_confirmation"))
                                        cmd3.Parameters.AddWithValue("@id_order", dtConfirmation(j)("id_order"))
                                        Dim dtOrder As DataTable = cls.msora.DB_MSora.GetData(cmd3)

                                        If dtOrder.Rows.Count > 0 Then
                                            Dim dr As DataRow = Nothing

                                            dr = dtMawi.NewRow
                                            dr("order_nr") = dtOrder(0)("order_nr")
                                            dr("id_contract") = dtOrder(0)("id_contract")
                                            dr("id_order") = dtOrder(0)("id_order")
                                            dr("id_order_state") = dtOrder(0)("id_order_state")
                                            dr("id_partner_head") = dtOrder(0)("id_partner_head")
                                            dr("id_partner_branch") = dtOrder(0)("id_partner_branch")
                                            dr("docname") = dtOrder(0)("docname")
                                            lngIdComplaint = cls.Utils.DB2Lng(dtOrder(0)("id_complaint"))
                                            Select Case lngIdComplaint
                                                Case 0
                                                    dr("order_type") = "Naročilo"
                                                Case Is > 0
                                                    dr("order_type") = "Reklamacija"
                                            End Select
                                            dr("description") = dtOrder(0)("description")
                                            dr("order_date") = dtOrder(0)("order_date")
                                            dr("latest_delivery_date") = dtOrder(0)("latest_delivery_date")
                                            dr("osname") = dtOrder(0)("osname")
                                            dr("pbname") = dtOrder(0)("pbname")
                                            dr("St_potrditve") = dtConfirmation(j)("St_potrditve")
                                            dr("St_dobavnice") = dtDelivery(i)("name")
                                            dr("datum_potrjene_dobave") = dtConfirmation(j)("datum_potrjene_dobave")

                                            dr("datum_dobave") = dtDelivery(i)("date")
                                            dr("id_order_confirmation") = dtConfirmation(j)("id_order_confirmation")
                                            dr("order_confirmation_date") = dtConfirmation(j)("order_confirmation_date")

                                            dtMawi.Rows.Add(dr)
                                        End If
                                    End Using

                                Next
                            End Using
                        Next

                    End Using


                    Return dtMawi

            End Select

            If intType < 2 Then

                Dim dtMawi As DataTable = CreateMawiOrdersTable()


                Using cmd As New SqlCommand(strSQL, connmawi)
                    If Not dtmFrom = Nothing Then
                        cmd.Parameters.AddWithValue("@dtmFrom", dtmFrom)
                    End If
                    If Not dtmTo = Nothing Then
                        cmd.Parameters.AddWithValue("@dtmTo", DateAdd("d", +1, dtmTo))
                    End If

                    Dim dt As DataTable = cls.msora.DB_MSora.GetData(cmd)
                    'dgOrders.DataSource = dt

                    Dim dr As DataRow = Nothing
                    For i = 0 To dt.Rows.Count - 1

                        If cls.Utils.DB2Lng(dt(i)("id_order_confirmation")) > 0 Then
                            strDobavnica = ""
                            dtmDatumDobave = Nothing
                            Call GetDeliveryInfo(cls.Utils.DB2Lng(dt(i)("id_order_confirmation")), cls.Utils.DB2Lng(dt(i)("id_order")), CDate(dt(i)("datum_potrjene_dobave")), strDobavnica, dtmDatumDobave, connmawi)

                            'dtmDatumDobave = dt(i)("delivery_date")
                        Else
                            strDobavnica = ""
                            dtmDatumDobave = Nothing
                        End If

                        Dim blnAddRow As Boolean = False

                        If intType < 2 Then
                            blnAddRow = True
                        Else
                            If Not dtmFrom = Nothing And Not dtmTo = Nothing Then
                                If dtmDatumDobave >= dtmFrom And dtmDatumDobave <= dtmTo Then
                                    blnAddRow = True
                                Else
                                    blnAddRow = True
                                End If
                            Else
                                blnAddRow = True
                            End If
                        End If

                        If blnAddRow Then
                            dr = dtMawi.NewRow
                            dr("order_nr") = dt(i)("order_nr")
                            dr("id_contract") = dt(i)("id_contract")
                            dr("id_order") = dt(i)("id_order")
                            dr("id_order_state") = dt(i)("id_order_state")
                            dr("id_partner_head") = dt(i)("id_partner_head")
                            dr("id_partner_branch") = dt(i)("id_partner_branch")
                            dr("docname") = dt(i)("docname")
                            lngIdComplaint = cls.Utils.DB2Lng(dt(i)("id_complaint"))
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
                            dr("order_confirmation_date") = dt(i)("order_confirmation_date")

                            dtMawi.Rows.Add(dr)
                        End If
                    Next
                    Return dtMawi
                End Using
            End If

            Return Nothing

        Catch ex As Exception
            Return Nothing

        End Try


    End Function

    Public Function IsGlassOrdered(strOrderNr As String, connMAWI As SqlConnection) As Boolean
        Dim strSQL As String
        Dim dt As DataTable

        'najprej preverim v used, required, če je sploh kakšno steklo
        strSQL = "SELECT u.id_contract, gt.name, o.order_date, u.id_order, pb.name, o.name, count(*) " _
            & " FROM used u " _
            & " INNER JOIN required r on r.id_row_used = u.id_row_used AND r.glass = 1 AND r.id_contract = u.id_contract " _
            & " INNER JOIN contract c ON c.id_contract = u.id_contract " _
            & " INNER JOIN article a ON u.id_article = a.id_article AND a.id_goods_group = u.id_goods_group AND a.recycle_bin = 0 " _
            & " INNER JOIN colored_article ca ON ca.id_article = u.id_article AND ca.id_goods_group = u.id_goods_group AND ca.id_color = u.id_color " _
            & " INNER JOIN goods_group gg ON gg.id_goods_group = ca.id_goods_group " _
            & " INNER JOIN goods_type gt ON gt.id_goods_type = a.id_goods_type " _
            & " LEFT JOIN [order] o ON o.id_order = u.id_order " _
            & " LEFT JOIN partner_head ph ON ph.id_partner_head = o.id_partner_head " _
            & " LEFT JOIN partner_branch pb ON pb.id_partner_branch = o.id_partner_branch AND  pb.id_partner_head = o.id_partner_head " _
            & " WHERE c.name = @strOrderNr AND u.id_item_state <> 8 AND gt.name = 'STEKLO' " _
            & " GROUP BY u.id_contract, gt.name, o.order_date, u.id_order, pb.name, o.name " _
            & " ORDER BY u.id_order ASC "
        'u.id_item_state = 8 = prezrto

        Using cmd As New SqlCommand(strSQL, connMAWI)
            cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
            dt = GetData(cmd)
            'ce je prazna, ni stekle
            'ce je en zapis in id_order = 0, potem ni naročeno
            'ce je vec zapisov in prvi id_order = 0, potem je delno naročeno (false
            'ce je vec zapisov in vsi razlicni od 0 potem je vse naroceno

            Select Case dt.Rows.Count
                'ni stekla
                Case 0
                    Return True
                    'samo en zapis naročeno/ni naročeno
                Case 1
                    If DB2IntZero(dt(0)("id_order")) = 0 Then
                        Return False
                    Else
                        Return True
                    End If
                    'več zapisov delno naročeno/vse naročeno
                Case Is > 0
                    If DB2IntZero(dt(0)("id_order")) = 0 Then
                        Return False
                    Else
                        Return True
                    End If
            End Select

        End Using
    End Function


    Private Sub GetDeliveryInfo(ByVal lngId As Long, ByVal lngIdOrder As Long, ByVal dtmDatumPotrjeneDobave As Date, ByRef strDobavnica As String, ByRef dtmDatumDobave As Date, ByVal connMawi As SqlConnection)
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

        Using cmd As New SqlCommand(strSQL, connMawi)
            cmd.Parameters.AddWithValue("@lngId", lngId)
            cmd.Parameters.AddWithValue("@lngIdOrder", lngIdOrder)
            cmd.Parameters.AddWithValue("@dtmDatumPotrjeneDobave", dtmDatumPotrjeneDobave)
            dt = cls.msora.DB_MSora.GetData(cmd)
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

        dt.Columns.Add("order_nr", Type.GetType("System.String"))
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
        dt.Columns.Add("order_confirmation_date", Type.GetType("System.DateTime"))

        Return dt
    End Function

    Public Function CreateMawiArticlesTable() As DataTable

        Dim dt As New DataTable

        dt.Columns.Add("id_type_0", Type.GetType("System.Int32"))
        dt.Columns.Add("str_type_0", Type.GetType("System.String"))
        dt.Columns.Add("id_type_1", Type.GetType("System.Int32"))
        dt.Columns.Add("str_type_1", Type.GetType("System.String"))
        dt.Columns.Add("id_type_2", Type.GetType("System.Int32"))
        dt.Columns.Add("str_type_2", Type.GetType("System.String"))
        dt.Columns.Add("id_article", Type.GetType("System.Int32"))
        dt.Columns.Add("arcode", Type.GetType("System.String"))
        dt.Columns.Add("arname", Type.GetType("System.String"))
        dt.Columns.Add("opis_artikla", Type.GetType("System.String"))
        dt.Columns.Add("article_description", Type.GetType("System.String"))
        dt.Columns.Add("id_article_description", Type.GetType("System.Int32"))
        dt.Columns.Add("ident_nr", Type.GetType("System.String"))
        dt.Columns.Add("id_color", Type.GetType("System.Int32"))
        dt.Columns.Add("id_goods_group", Type.GetType("System.Int32"))
        dt.Columns.Add("kommissionsnummer", Type.GetType("System.String"))
        dt.Columns.Add("eprod_teilekennung", Type.GetType("System.String"))
        dt.Columns.Add("order_datum", Type.GetType("System.DateTime"))
        dt.Columns.Add("confirmation_datum", Type.GetType("System.DateTime"))
        dt.Columns.Add("delivery_datum", Type.GetType("System.DateTime"))

        Return dt
    End Function

    

    Public Function GetMawiOrderArticles(ByVal lngOrderId As Long, ByVal dtmExpectationDate As Date, ByVal dtmExpectationDate2 As Date, ByVal strArticleTypes As String, connMAWI As SqlConnection) As DataTable
        Dim strSQL As String
        Dim dt As DataTable
        Dim dtmTemp As Date = Nothing

        Try


            If dtmExpectationDate2 = Nothing Then
                'AND r.item > 0 AND r.pis_pos_id <> 0 "
                strSQL = "SELECT od.id_article, ar.code AS arcode, ar.name AS arname, gt.name AS opis_artikla, " _
                    & " od.expectation_date as Datum_dobave1, ex.ident_nr, ex.payment_date, u.id_color, " _
                    & " u.id_goods_group, ad.description as article_description, od.id_article_description, SUM(u.quantity) as quantity " _
                    & " FROM order_details od " _
                    & " LEFT JOIN article_description ad ON ad.id_article_description = od.id_article_description, " _
                    & " required r, used u " _
                    & " LEFT JOIN [KlaesTools].dbo.mawi_extra ex ON ex.id_order = u.id_order AND ex.id_article = u.id_article AND ex.id_color = u.id_color AND ex.id_goods_group = u.id_goods_group, " _
                    & " article ar, goods_group gg, goods_type gt, measurement_unit mu, package_type pt " _
                    & " WHERE od.id_article = ar.id_article AND od.id_goods_group = ar.id_goods_group " _
                    & " AND u.id_contract = r.id_contract AND u.id_row_used = r.id_row_required " _
                    & " AND od.id_article = u.id_article AND od.id_color = u.id_color AND od.id_goods_group = u.id_goods_group AND u.id_item_state <> 0 " _
                    & " And ar.id_goods_group = gg.id_goods_group " _
                    & " AND ar.id_goods_type = gt.id_goods_type " _
                    & " AND ar.id_measurement_unit = mu.id_measurement_unit " _
                    & " AND od.id_package_type = pt.id_package_type " _
                    & " AND od.id_order = @lngOrderId" _
                    & " AND r.id_article_description = od.id_article_description " _
                    & " AND gt.name IN ('Komarnik','Zubehör') " _
                    & " AND u.id_order = od.id_order AND u.id_article = ar.id_article " _
                    & " AND CAST(od.expectation_date as date) = @dtmExpectationDate " _
                    & " GROUP BY od.id_article, ar.code, ar.name, gt.name, " _
                    & " od.expectation_date, ex.ident_nr, ex.payment_date, u.id_color, " _
                    & " u.id_goods_group, ad.description, od.id_article_description"
                dtmTemp = Format(dtmExpectationDate, "Short date")
            Else
                strSQL = "SELECT od.id_article, ar.code AS arcode, ar.name AS arname, gt.name AS opis_artikla, " _
                    & " ocd.expectation_date as Datum_dobave1, ex.ident_nr, ex.payment_date, u.id_color, " _
                    & " u.id_goods_group, ad.description as article_description, od.id_article_description, SUM(u.quantity) as quantity " _
                    & " FROM order_details od " _
                    & " LEFT JOIN article_description ad ON ad.id_article_description = od.id_article_description, order_confirmation_details ocd, " _
                    & " required r, used u " _
                    & " LEFT JOIN [KlaesTools].dbo.mawi_extra ex ON ex.id_order = u.id_order AND ex.id_article = u.id_article AND ex.id_color = u.id_color AND ex.id_goods_group = u.id_goods_group, " _
                    & " article ar, goods_group gg, goods_type gt, measurement_unit mu, package_type pt " _
                    & " WHERE od.id_article = ar.id_article AND od.id_goods_group = ar.id_goods_group " _
                    & " AND u.id_contract = r.id_contract AND u.id_row_used = r.id_row_required " _
                    & " AND od.id_article = u.id_article AND od.id_color = u.id_color AND od.id_goods_group = u.id_goods_group AND u.id_item_state <> 0 " _
                    & " AND ar.id_goods_group = gg.id_goods_group " _
                    & " AND ar.id_goods_type = gt.id_goods_type " _
                    & " AND ar.id_measurement_unit = mu.id_measurement_unit " _
                    & " AND ocd.id_package_type = pt.id_package_type " _
                    & " AND od.id_order = @lngOrderId" _
                    & " AND ocd.id_article_description = od.id_article_description AND r.id_article_description = od.id_article_description " _
                    & " AND gt.name IN ('Komarnik','Zubehör') " _
                    & " AND u.id_order = od.id_order AND u.id_order_confirmation = ocd.id_order_confirmation AND u.id_article = ar.id_article " _
                    & " AND od.id_article = ocd.id_article AND od.id_goods_group = ocd.id_goods_group AND od.id_color = ocd.id_color " _
                    & " AND CAST(ocd.expectation_date as date) = @dtmExpectationDate " _
                    & " GROUP BY od.id_article, ar.code, ar.name, gt.name, " _
                    & " ocd.expectation_date, ex.ident_nr, ex.payment_date, u.id_color, " _
                    & " u.id_goods_group, ad.description, od.id_article_description"

                dtmTemp = Format(dtmExpectationDate2, "Short date")
            End If
            Using cmd As New SqlCommand(strSQL, connMAWI)
                cmd.Parameters.AddWithValue("@lngOrderId", lngOrderId)
                'cmd.Parameters.AddWithValue("@strArticleTypes", strArticleTypes)
                cmd.Parameters.AddWithValue("@dtmExpectationDate", dtmTemp)
                dt = cls.msora.DB_MSora.GetData(cmd)
                Return dt
            End Using
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try

    End Function

    
End Module
