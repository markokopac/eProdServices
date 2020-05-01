Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common

Module modGlobal

    Public Enum ManualEventId As Integer
        CutterSendFile = 1001
        CutterArchive = 1002
    End Enum

    Public Sub FillEventType(ByVal cbo As ComboBox)
        Dim dt As New DataTable

        dt.Columns.Add("id", Type.GetType("System.Int32"))
        dt.Columns.Add("desc", Type.GetType("System.String"))

        Dim workRow As DataRow = dt.NewRow()

        workRow("id") = clsGlobal.EventType.event_not_defined
        workRow("desc") = "Ni definiran"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_kommission_production_start
        workRow("desc") = "Začetek naloga v proizvodnji"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_kommission_production_end
        workRow("desc") = "Nalog končan v proizvodnji"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_order_production_end_check_delivery_OK
        workRow("desc") = "Naročilo končano v proizvodnji - dobava DA"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_order_production_end_check_delivery_FAIL
        workRow("desc") = "Naročilo končano v proizvodnji - dobava NE"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_order_production_start
        workRow("desc") = "Začetek naročila v proizvodnji"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_order_production_end
        workRow("desc") = "Naročilo končano v proizvodnji"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_order_planning_start
        workRow("desc") = "Naročilo planirano"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_order_technical_end
        workRow("desc") = "Naročilo tehnično obdelano"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_order_already_delivered
        workRow("desc") = "Naročilo že dobavljeno"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_kommission_send_to_cutter
        workRow("desc") = "Rezalnik - kopiraj nalog"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_kommission_cutter_move_to_archive
        workRow("desc") = "Rezalnik - Premik v arhiv "
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_delivery_note_created
        workRow("desc") = "Kreiranje dobavnice"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_invoice_created
        workRow("desc") = "Kreiranje računa"
        dt.Rows.Add(workRow)


        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_invoice_printed
        workRow("desc") = "Tiskanje računa"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_avans_created
        workRow("desc") = "Kreiranje avansa"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_order_not_planed
        workRow("desc") = "Naročilo ni planirano"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_order_kapa_glass
        workRow("desc") = "Naročilo planirano"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_montaza_slepci
        workRow("desc") = "Končana montaža slepih podbojev"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_offer_not_realized
        workRow("desc") = "Nerealizirani projekti/ponudbe"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_task_assigned
        workRow("desc") = "Dodeljena naloga"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_glass_not_ordered
        workRow("desc") = "Steklo ni naročeno"
        dt.Rows.Add(workRow)

        workRow = dt.NewRow()
        workRow("id") = clsGlobal.EventType.event_MAWI_subsequent_delivery
        workRow("desc") = "Naknadna dobava materiala"
        dt.Rows.Add(workRow)

        cbo.DataSource = dt.DefaultView
        cbo.DisplayMember = "desc"
        cbo.ValueMember = "id"
    End Sub

    Public Sub FillTemplateCombo(ByVal cbo As ComboBox, connTools As SqlConnection)
        Dim strSQL As String = ""
        Dim ds As DataSet = Nothing
        Dim da As SqlDataAdapter = Nothing

        Dim conn As New SqlConnection


        strSQL = "SELECT text_id, text_description FROM text_template  ORDER BY text_description"

        Dim cmd As New SqlCommand(strSQL, connTools)
        da = New SqlDataAdapter(cmd)
        ds = New DataSet
        da.Fill(ds)
        cbo.DataSource = ds.Tables(0).DefaultView
        cbo.DisplayMember = "text_description"
        cbo.ValueMember = "text_id"

        conn.Close()
        conn.Dispose()
    End Sub


    Public Function GetText(ByVal intTextId As Integer, connTools As SqlConnection) As String
        Dim dt As DataTable
        Dim strSQL As String = ""
        Try

            strSQL = "SELECT lang_SL FROM text_template WHERE text_id = @intTextId"
            Using cmd As New SqlCommand(strSQL, connTools)
                cmd.Parameters.AddWithValue("@intTextId", intTextId)
                dt = cls.msora.DB_MSora.GetData(cmd)

                If dt.Rows.Count > 0 Then
                    Return dt(0)("lang_sl").ToString

                Else
                    Return ""
                End If
            End Using
        Catch ex As Exception

            Return ""
        End Try

    End Function
End Module
