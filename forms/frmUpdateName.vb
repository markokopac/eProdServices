Imports MySql.Data.MySqlClient
Imports eProdService.cls.base

Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient

Public Class frmUpdateName

    Dim connKBS As SqlConnection = cls.msora.DB_MSora.GetConnection("KLAESKBS")
    Dim connTools As SqlConnection = cls.msora.DB_MSora.GetConnection("TOOLS")

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Call SearchOrders(connKBS)
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        

        For i = 0 To dgOrders.SelectedRows.Count - 1

            Call modEProd.ChangeOrderName(dgOrders.SelectedRows(i).Cells("auftragsnummer").Value.ToString, dgOrders.SelectedRows(i).Cells("kommissionsnummer").Value.ToString, dgOrders.SelectedRows(i).Cells("old_name").Value.ToString, dgOrders.SelectedRows(i).Cells("new_name").Value.ToString, connTools)
        Next

        Call SearchOrders(connKBS)

    End Sub

    Private Sub chkPrikazSamoRazlicne_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPrikazSamoRazlicne.CheckedChanged
        Call SearchOrders(connKBS)
    End Sub

    Private Sub SearchOrders(connKBS As SqlConnection)
        Dim dtmFrom As Date
        Dim dtmTo As Date
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        dtmFrom = dtpFrom.Value
        dtmTo = dtpTo.Value
        dgOrders.DataSource = modEProd.SearchOrdersChangeName(dtmFrom, dtmTo, Me.txtNarocilo.Text, Me.txtNalog.Text, "", Me.chkPrikazSamoRazlicne.Checked, connKBS)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim conn As MySqlConnection = cls.msora.DB_MSora.GetMyConnection("eprod")
        For i = 0 To dgOrders.SelectedRows.Count - 1

            Call modEProd.UpdateReclamation(dgOrders.SelectedRows(i).Cells("auftragsnummer").Value.ToString, dgOrders.SelectedRows(i).Cells("kommissionsnummer").Value.ToString, gConnEProd, connKBS)

        Next
        'conn = Nothing
        Call SearchOrders(connKBS)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim strOrder As String
        Dim strKommission As String
        Dim strName As String
        Dim strProductionText As String
        Dim dtmModDate As DateTime
        Dim strSQL As String = ""
        Dim dt2 As DataTable = Nothing


        Dim strOrderDesc As String = ""

        For i = 0 To dgOrders.SelectedRows.Count - 1
            strOrder = dgOrders.SelectedRows(i).Cells("auftragsnummer").Value.ToString
            strKommission = dgOrders.SelectedRows(i).Cells("kommissionsnummer").Value.ToString
            strName = dgOrders.SelectedRows(i).Cells("old_name").Value.ToString
            strProductionText = GetOrderProperties(strOrder, strKommission, "ProductionText", connKBS)

            dtmModDate = GetFax_ModDate(strKommission, connKBS)

            strOrderDesc = GetOrderProperties(strOrder, strKommission, "DESCRIPTION", connKBS)
            strSQL = "SELECT * FROM eprod_change_prod_text WHERE kommissionsnummer = @order_nr AND date_changed = @dtmDataChanged"

            Using cmd2 As New SqlCommand(strSQL, connTools)
                cmd2.Parameters.AddWithValue("@order_nr", strKommission)
                cmd2.Parameters.AddWithValue("@dtmDataChanged", dtmModDate)
                dt2 = cls.msora.DB_MSora.GetData(cmd2)
                If dt2.Rows.Count = 0 Then
                    Call modEProd.UpdateProductionText(strOrder, strKommission, strName, strProductionText, strOrderDesc, dtmModDate, connTools)
                End If
            End Using

        Next
        Call SearchOrders(connKBS)


    End Sub
End Class