Imports System.IO

Public Class frmCutterSendFile

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Call SearchOrders()
    End Sub

    Private Sub SearchOrders()
        Dim dtmFrom As Date
        Dim dtmTo As Date
        Dim dtE As DataTable = Nothing
        Dim strSourceFile As String = ""

        Dim strSourcePath As String = cls.Config.GetCutterSourcePath
        Dim strFileType As String = ""
        Dim strKommissionNr As String = ""
        Dim strOrderNr As String = ""
        Dim strNewOrderNr As String = ""
        Dim aFileTypes() As String = Split(cls.Config.GetCutterFileTypes, ",")
        Dim blnExist As Boolean = False

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        dtmFrom = dtpFrom.Value
        

        dtmTo = dtpTo.Value
        

        If Me.optStartedKommissions.Checked Then
            dtE = modEProd.GetStartedKommissions(dtmFrom, dtmTo, cls.Config.GetCutterStation, Me.txtNarocilo.Text, Me.txtNalog.Text)
        ElseIf Me.optFinnishedKommissions.Checked Then
            dtE = modEProd.GetFinishedKommissions(dtmFrom, dtmTo, cls.Config.GetCutterStation, Me.txtNarocilo.Text, Me.txtNalog.Text)
        ElseIf Me.optAllKommissions.Checked Then
            dtE = modEProd.GetKommissions(dtmFrom, dtmTo, cls.Config.GetCutterStation, Me.txtNarocilo.Text, Me.txtNalog.Text)
        End If


        Dim dt As DataTable = CreateTableCutterFiles()

        For i = 0 To dtE.Rows.Count - 1
            strKommissionNr = dtE(i)("kommissionsnummer").ToString
            strOrderNr = dtE(i)("auftragsnummer").ToString
            strNewOrderNr = ""
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

            For j = 0 To aFileTypes.Length - 1
                blnExist = False
                strFileType = aFileTypes(j).ToString

                strSourceFile = strSourcePath & "\" & strKommissionNr & "." & strFileType
                If File.Exists(strSourceFile) Then
                    Dim dr As DataRow = dt.NewRow
                    dr("kommissionsnummer") = dtE(i)("kommissionsnummer")
                    dr("auftragsnummer") = dtE(i)("auftragsnummer")
                    dr("filename") = strSourceFile
                    dt.Rows.Add(dr)
                End If

                strSourceFile = strSourcePath & "\" & strNewOrderNr & "." & strFileType
                If File.Exists(strSourceFile) Then
                    Dim dr As DataRow = dt.NewRow
                    dr("kommissionsnummer") = dtE(i)("kommissionsnummer")
                    dr("auftragsnummer") = dtE(i)("auftragsnummer")
                    dr("filename") = strSourceFile
                    dt.Rows.Add(dr)
                End If


            Next
        Next

        Me.dgEprod.DataSource = dt

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub optStartedKommissions_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optStartedKommissions.CheckedChanged
        If Me.optStartedKommissions.Checked Then Label1.Text = "Datum prvega bukiranja"
    End Sub

    Private Sub optFinnishedKommissions_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFinnishedKommissions.CheckedChanged
        If Me.optFinnishedKommissions.Checked Then Label1.Text = "Datum zadnjega bukiranja"
    End Sub

    Private Sub optAllKommissions_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAllKommissions.CheckedChanged
        If Me.optAllKommissions.Checked Then Label1.Text = "Datum izdelave naloga"
    End Sub

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        Dim strSourceFile As String = ""
        Dim strDestFile As String = ""
        Dim strDestPath As String = cls.Config.GetCutterDestPath

        For i = 0 To dgEprod.SelectedRows.Count - 1

            strSourceFile = dgEprod.SelectedRows(i).Cells("filename").Value.ToString
            strDestFile = strDestPath & "\" & Path.GetFileName(strSourceFile)
            If File.Exists(strSourceFile) Then
                'ga kopiram v dest_path
                If Not File.Exists(strDestFile) Then
                    File.Move(strSourceFile, strDestFile)                    
                End If
            End If
        Next

        Call SearchOrders()
    End Sub
End Class