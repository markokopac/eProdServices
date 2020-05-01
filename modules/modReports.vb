Option Explicit On
Option Strict On


Imports System.Data.OleDb

Namespace Reports

    Module modReports


        

        Public Sub FillOutputCombo(ByVal cbo As ComboBox)
            Dim dt As New DataTable

            dt.Columns.Add("id", Type.GetType("System.Int32"))
            dt.Columns.Add("desc", Type.GetType("System.String"))


            Dim workRow As DataRow = dt.NewRow()

            workRow("id") = 0
            workRow("desc") = "Preview to screen"
            dt.Rows.Add(workRow)

            workRow = dt.NewRow()
            workRow("id") = 1
            workRow("desc") = "To printer"
            dt.Rows.Add(workRow)


            workRow = dt.NewRow()
            workRow("id") = 2
            workRow("desc") = "To PDF"
            dt.Rows.Add(workRow)

            cbo.DataSource = dt.DefaultView
            cbo.DisplayMember = "desc"
            cbo.ValueMember = "id"
        End Sub

        Public Sub FillReportCombo(ByVal intCategory As Integer, ByVal connMDB As OleDbConnection, ByVal cbo As ComboBox)
            Dim strSQL As String = ""
            Dim ds As DataSet = Nothing
            Dim da As OleDbDataAdapter = Nothing


            strSQL = "SELECT rp_id, rp_description FROM reports WHERE rp_category = @rp_category ORDER BY rp_sequence, rp_description"

            Dim cmd As New OleDbCommand(strSQL, connMDB)
            cmd.Parameters.AddWithValue("@rp_category", intCategory)
            da = New OleDbDataAdapter(cmd)
            ds = New DataSet
            da.Fill(ds)
            cbo.DataSource = ds.Tables(0).DefaultView
            cbo.DisplayMember = "rp_description"
            cbo.ValueMember = "rp_id"
        End Sub

        Public Sub GetReport(ByVal intRpId As Integer, ByVal connMDB As OleDbConnection, ByRef strReportName As String, ByRef dtReportSelectionFormula As DataTable, ByRef strReportParameter As String, ByRef strReportFilter As String)
            Dim strSQL As String = ""
            Dim dt As DataTable = Nothing
            Dim da As OleDbDataAdapter = Nothing
            Dim workRow As DataRow

            dtReportSelectionFormula = New DataTable

            dtReportSelectionFormula.Columns.Add("rq_sequence", Type.GetType("System.Int32"))
            dtReportSelectionFormula.Columns.Add("rq_table", Type.GetType("System.String"))
            dtReportSelectionFormula.Columns.Add("rq_field", Type.GetType("System.String"))
            dtReportSelectionFormula.Columns.Add("rq_default1", Type.GetType("System.String"))
            dtReportSelectionFormula.Columns.Add("rq_default2", Type.GetType("System.String"))
            dtReportSelectionFormula.Columns.Add("rq_operator", Type.GetType("System.String"))
            dtReportSelectionFormula.Columns.Add("rq_visible", Type.GetType("System.Int32"))
            dtReportSelectionFormula.Columns.Add("rq_text", Type.GetType("System.String"))


            strSQL = "SELECT rp.*, rq.rq_text, rq.rq_table, rq.rq_field, rq.rq_default1, rq.rq_default2, rq.rq_visible, rq.rq_operator, rq.rq_sequence " _
                & " FROM reports rp LEFT JOIN report_query rq ON rp.rp_id = rq.rq_rp_id WHERE rp.rp_id = @rp_id "

            Dim cmd As New OleDbCommand(strSQL, connMDB)
            cmd.Parameters.AddWithValue("@rp_id", intRpId)

            da = New OleDbDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                strReportName = dt(0)("rp_file").ToString
                strReportFilter = dt(0)("rp_filter").ToString
                For i = 0 To dt.Rows.Count - 1
                    workRow = dtReportSelectionFormula.NewRow()

                    workRow("rq_sequence") = dt(i)("rq_sequence")
                    workRow("rq_table") = dt(i)("rq_table")
                    workRow("rq_field") = dt(i)("rq_field")
                    workRow("rq_default1") = dt(i)("rq_default1")
                    workRow("rq_default2") = dt(i)("rq_default2")
                    workRow("rq_operator") = dt(i)("rq_operator")
                    workRow("rq_visible") = dt(i)("rq_visible")
                    workRow("rq_text") = dt(i)("rq_text")
                    dtReportSelectionFormula.Rows.Add(workRow)
                Next

            Else
                strReportName = ""
            End If

        End Sub


    End Module
End Namespace
