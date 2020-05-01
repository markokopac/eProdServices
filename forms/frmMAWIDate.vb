Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common
Imports MySql.Data.MySqlClient

Public Class frmMawiDate

    Dim mblnLoaded As Boolean
    Dim dtOrders As DataTable = Nothing

    Dim connMAWI As SqlConnection = cls.msora.DB_MSora.GetConnection("MAWI")

    Private Sub frmMawiDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboMawiDateType.Items.Add("Datum vnosa naročila")
        cboMawiDateType.Items.Add("Datum vnosa potrditve naročila")
        cboMawiDateType.Items.Add("Datum dobave")

        cboMawiDateType.SelectedIndex = 0

        mblnLoaded = False
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim lngOrderId As Long = -1
        Dim dtmDeliveryDate As Date = Nothing
        Dim dtmStart As Date = Nothing
        Dim dtmEnd As Date = Nothing

        'If Me.dtpFrom.Checked Then
        dtmStart = dtpFrom.Value.Date
        'End If

        'If Me.dtpTo.Checked Then
        dtmEnd = dtpTo.Value.Date
        'End If

        dtOrders = GetMawiOrdersService(Me.cboMawiDateType.SelectedIndex, dtmStart, dtmEnd, Me.txtNarocilo.Text.Trim, connMAWI)
        dgOrders.DataSource = dtOrders

        Call RefreshArticleGrid()

        mblnLoaded = True
    End Sub
    
    Private Sub dgOrders_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgOrders.SelectionChanged
        If Not mblnLoaded Then Exit Sub
        Call RefreshArticleGrid()
    End Sub

    Private Sub RefreshArticleGrid()
        Dim lngOrderId As Long = -1
        Dim dtmExpectationDate As Date
        Dim dtmExpectationDate2 As Date
        Dim strOrderNr As String
        Dim dtmDeliveryDate As Date = Nothing
        Dim strKommissionsnummer As String = ""

        If dgOrders.Rows.Count > 0 Then

            strOrderNr = dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("order_nr").Value
            'strKommissionsnummer = dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("kommissionsnummer").Value
            lngOrderId = cls.Utils.DB2Lng(dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("id_order").Value)
            dtmExpectationDate = CDate(dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("latest_delivery_date").Value.ToString)
            If dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("datum_potrjene_dobave").Value.ToString.Trim <> "" Then
                dtmExpectationDate2 = CDate(dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("datum_potrjene_dobave").Value.ToString)
            Else
                dtmExpectationDate2 = Nothing
            End If
            If dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("datum_dobave").Value.ToString.Trim <> "" Then
                dtmDeliveryDate = CDate(dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("datum_dobave").Value.ToString)
            Else
                dtmDeliveryDate = Nothing
            End If

            Dim dt As DataTable
            dt = GetMawiOrderArticles(lngOrderId, dtmExpectationDate, dtmExpectationDate2, Me.txtArticleType.Text, connMAWI)
            Me.dgArticles.DataSource = dt

        End If
    End Sub






    Private Function IsIncludedArticleType(ByVal strArticleType As String, ByVal strIncludedTypeString As String) As Boolean
        Dim ar() As String = strIncludedTypeString.Split(",")

        If strIncludedTypeString = "" Then Return True

        For i = 0 To ar.Length - 1
            If strArticleType.Contains(ar(i)) Then
                Return True
            End If
        Next

        Return False

    End Function
    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        Dim strKommissionsNummer As String = ""
        Dim strTeilekennung As String = ""
        Dim intType As Integer = -1
        Dim dtmOrderDatum As Date = Nothing
        Dim dtmConfirmationDatum As Date = Nothing
        Dim dtmDeliveryDatum As Date = Nothing
        Me.Cursor = Cursors.WaitCursor
        For i = 0 To dgEprod.Rows.Count - 1
            strKommissionsNummer = dgEprod.Rows(i).Cells("kommissionsnummer").Value
            strTeilekennung = dgEprod.Rows(i).Cells("teilekennung").Value

            lblCounterUpdate.Text = i.ToString & "/" & (dgEprod.Rows.Count - 1).ToString
            lblCounterUpdate.Refresh()

            If Not dgEprod.Rows(i).Cells("date0").Value Is DBNull.Value Then

                intType = 0
                If strTeilekennung <> "" Then
                    dtmOrderDatum = CDate(dgEprod.Rows(i).Cells("date0").Value)
                Else
                    dtmOrderDatum = Nothing
                End If
                Call UpdateEProdFachnummer(strKommissionsNummer, strTeilekennung, dtmOrderDatum, intType)
            End If

            If Not dgEprod.Rows(i).Cells("date1").Value Is DBNull.Value Then

                intType = 1
                If strTeilekennung <> "" Then
                    dtmConfirmationDatum = CDate(dgEprod.Rows(i).Cells("date1").Value)
                Else
                    dtmConfirmationDatum = Nothing
                End If
                Call UpdateEProdFachnummer(strKommissionsNummer, strTeilekennung, dtmConfirmationDatum, intType)
            End If

            If Not dgEprod.Rows(i).Cells("date2").Value Is DBNull.Value Then

                intType = 2
                If strTeilekennung <> "" Then
                    dtmDeliveryDatum = CDate(dgEprod.Rows(i).Cells("date2").Value)
                Else
                    dtmDeliveryDatum = Nothing
                End If
                Call UpdateEProdFachnummer(strKommissionsNummer, strTeilekennung, dtmDeliveryDatum, intType)
            End If
        Next

        Me.Cursor = Cursors.Default

    End Sub



    Private Sub cmdSearchEProd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearchEProd.Click
        Dim strArticleCode As String = ""
        Dim strArticleDesc As String = ""
        Dim strArticleLongDesc As String = ""
        Dim dblQuantity As Double = 0
        Dim strOrderNr As String
        Dim dtZustand As DataTable
        Dim strSQL As String
        Dim dtm0 As DateTime = Nothing
        Dim dtm1 As DateTime = Nothing
        Dim dtm2 As DateTime = Nothing
        Dim intIdDesc As Integer = -1

        If dgOrders.Rows.Count > 0 Then
            strOrderNr = dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("order_nr").Value

            If Not dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("latest_delivery_date").Value Is DBNull.Value Then
                dtm0 = CDate(dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("latest_delivery_date").Value)
            End If
            If Not dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("datum_potrjene_dobave").Value Is DBNull.Value Then
                dtm1 = CDate(dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("datum_potrjene_dobave").Value)
            End If
            If Not dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("datum_dobave").Value Is DBNull.Value Then
                dtm2 = CDate(dgOrders.Rows(dgOrders.CurrentRow.Index).Cells("datum_dobave").Value)
            End If

            Dim dtText As DataTable = CreateTableTexts()

            dgEprod.DataSource = dtText
            strSQL = "SELECT * FROM zustand WHERE kostenstelle = 8 AND kommissionsnummer IN (SELECT kommissionsnummer FROM auftrag WHERE auftragsnummer = @strOrderNr) " _
                & " AND positionsnummer > 0"

            Using cmd As New MySqlCommand(strSQL, gConnEProd)
                cmd.Parameters.AddWithValue("@strOrderNr", strOrderNr)
                dtZustand = cls.msora.DB_MSora.GetMyData(cmd)

                If dtZustand.Rows.Count > 0 Then

                    For i = 0 To dgArticles.Rows.Count - 1

                        strArticleCode = dgArticles.Rows(i).Cells("arcode").Value.ToString
                        strArticleDesc = dgArticles.Rows(i).Cells("arname").Value.ToString
                        strArticleLongDesc = dgArticles.Rows(i).Cells("article_description").Value.ToString
                        dblQuantity = dgArticles.Rows(i).Cells("quantity").Value
                        intIdDesc = dgArticles.Rows(i).Cells("id_article_description").Value

                        Call RefreshArticles(strOrderNr, dtZustand, dtText, strArticleCode, strArticleDesc, strArticleLongDesc, dblQuantity, dtm0, dtm1, dtm2, intIdDesc)

                    Next
                    'v dtText pustim samo zapis z najvišjo compare_density

                    dtText.DefaultView.Sort = "teilekennung, compare_density DESC"

                    Dim strTeilekennung As String = ""
                    Dim dtTextFiltered As DataTable = CreateTableTexts()
                    For i = 0 To dtText.Rows.Count - 1
                        If strTeilekennung <> dtText(i)("teilekennung").ToString Then
                            Call CopyDataRecord(dtTextFiltered, dtText, i)
                        End If
                        strTeilekennung = dtText(i)("teilekennung").ToString
                    Next

                    dgEprod.DataSource = dtTextFiltered

                End If
            End Using
        End If


    End Sub



    Private Sub dgOrders_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgOrders.CellContentClick

    End Sub

    Private Sub cmdUpdateAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdateAll.Click
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

        Me.Cursor = Cursors.WaitCursor

        If dtOrders Is Nothing Then Exit Sub

        For j = 0 To dtOrders.Rows.Count - 1

            Dim dtm0 As Date = Nothing
            Dim dtm1 As Date = Nothing
            Dim dtm2 As Date = Nothing


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

                    dtArticles = GetMawiOrderArticles(lngOrderId, dtmExpectationDate, dtmExpectationDate2, Me.txtArticleType.Text, cls.msora.DB_MSora.GetConnection("MAWI"))

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
                                Call UpdateEProdFachnummer(strKommissionsNummer, strTeilekennung, dtmOrderDatum, intType)
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
                                Call UpdateEProdFachnummer(strKommissionsNummer, strTeilekennung, dtmConfirmationDatum, intType)
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
                                Call UpdateEProdFachnummer(strKommissionsNummer, strTeilekennung, dtmDeliveryDatum, intType)
                            End If
                        End If
                    Next

                End If
            End If
        Next

        Me.Cursor = Cursors.Default
    End Sub
End Class