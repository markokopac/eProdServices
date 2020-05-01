Public Class frmViewReport

    Dim strDSN As System.String
    Dim strDB As System.String
    Dim strUID As System.String
    Dim strPWD As System.String
    Friend Function ViewReport(ByVal sReportName As String, Optional ByVal sSelectionFormula As String = "", Optional ByVal param As String = "", _
                               Optional ByVal strFilter As String = "", Optional ByVal strReportCaption As String = "") As Boolean

        'Declaring variablesables
        Dim intCounter As Integer
        Dim intCounter1 As Integer

        'Crystal Report's report document object
        Dim objReport As New  _
            CrystalDecisions.CrystalReports.Engine.ReportDocument

        'object of table Log on info of Crystal report
        Dim ConInfo As New CrystalDecisions.Shared.TableLogOnInfo

        'Parameter value object of crystal report 
        ' parameters used for adding the value to parameter.
        Dim paraValue As New CrystalDecisions.Shared.ParameterDiscreteValue

        'Current parameter value object(collection) of crystal report parameters.
        Dim currValue As CrystalDecisions.Shared.ParameterValues

        'Sub report object of crystal report.
        Dim mySubReportObject As  _
            CrystalDecisions.CrystalReports.Engine.SubreportObject

        'Sub report document of crystal report.
        Dim mySubRepDoc As New  _
            CrystalDecisions.CrystalReports.Engine.ReportDocument

        Dim strParValPair() As String
        Dim strVal() As String
        Dim index As Integer


        Try

            'Load the report

            'zamenjam bazo

            objReport.Load(sReportName)

            'Check if there are parameters or not in report.
            intCounter = objReport.DataDefinition.ParameterFields.Count

            Dim CRFormulaDefinitions As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions = objReport.DataDefinition.FormulaFields
            Dim CRFormulaDefinition As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinition = Nothing


            'As parameter fields collection also picks the selection 
            ' formula which is not the parameter
            ' so if total parameter count is 1 then we check whether 
            ' its a parameter or selection formula.

            If intCounter = 1 Then
                If InStr(objReport.DataDefinition.ParameterFields(0).ParameterFieldName, ".", CompareMethod.Text) > 0 Then
                    intCounter = 0
                End If
            End If

            'If there are parameters in report and 
            'user has passed them then split the 
            'parameter string and Apply the values 
            'to their concurrent parameters.

            If intCounter > 0 And Trim(param) <> "" Then
                strParValPair = param.Split("&")

                For index = 0 To UBound(strParValPair)
                    If InStr(strParValPair(index), "=") > 0 Then
                        strVal = strParValPair(index).Split("=")
                        paraValue.Value = strVal(1)
                        currValue = _
                            objReport.DataDefinition.ParameterFields(strVal(0)).CurrentValues
                        currValue.Add(paraValue)
                        objReport.DataDefinition.ParameterFields(strVal(0)).ApplyCurrentValues(currValue)
                    End If
                Next
            End If

            'Set the connection information to ConInfo 
            'object so that we can apply the 
            'connection information on each table in the report

            'ACCESS
            ConInfo.ConnectionInfo.UserID = ""
            ConInfo.ConnectionInfo.Password = "" 'MSora_Price.Decrypt(cls.Config.GetDatabasePwd)
            ConInfo.ConnectionInfo.ServerName = ""
            ConInfo.ConnectionInfo.DatabaseName = "" 'Application.StartupPath & "\" & cls.Config.DatabaseName

            'ODBC
            ConInfo.ConnectionInfo.ServerName = "eProd"
            ConInfo.ConnectionInfo.UserID = "klaesserver"
            ConInfo.ConnectionInfo.Password = "papierlos" 'MSora_Price.Decrypt(cls.Config.GetDatabasePwd)
            ConInfo.ConnectionInfo.ServerName = "192.168.173.3"
            ConInfo.ConnectionInfo.DatabaseName = "fertigung" 'Application.StartupPath & "\" & cls.Config.DatabaseName

            'Call SetupReport(objReport)


            For intCounter = 0 To objReport.Database.Tables.Count - 1
                objReport.Database.Tables(intCounter).ApplyLogOnInfo(ConInfo)
            Next

            ' Loop through each section on the report then look 
            ' through each object in the section
            ' if the object is a subreport, then apply logon info 
            ' on each table of that sub report

            For index = 0 To objReport.ReportDefinition.Sections.Count - 1
                For intCounter = 0 To _
                    objReport.ReportDefinition.Sections(index).ReportObjects.Count - 1
                    With objReport.ReportDefinition.Sections(index)
                        If .ReportObjects(intCounter).Kind = _
                        CrystalDecisions.Shared.ReportObjectKind.SubreportObject Then
                            mySubReportObject = CType(.ReportObjects(intCounter),  _
                              CrystalDecisions.CrystalReports.Engine.SubreportObject)
                            mySubRepDoc = _
                     mySubReportObject.OpenSubreport(mySubReportObject.SubreportName)
                            For intCounter1 = 0 To mySubRepDoc.Database.Tables.Count - 1
                                mySubRepDoc.Database.Tables(intCounter1).ApplyLogOnInfo(ConInfo)
                                mySubRepDoc.Database.Tables(intCounter1).ApplyLogOnInfo(ConInfo)
                            Next
                        End If
                    End With
                Next
            Next
            'If there is a selection formula passed to this function then use that
            If sSelectionFormula.Length > 0 Then
                objReport.RecordSelectionFormula = sSelectionFormula
            End If

            'nafilam se formule

            For intCounter = 0 To CRFormulaDefinitions.Count - 1
                CRFormulaDefinition = CRFormulaDefinitions(intCounter)
                If "{@VB_FIRMA}" = CRFormulaDefinition.FormulaName.Trim.ToUpper Then
                    objReport.DataDefinition.FormulaFields(intCounter).Text = "" '& cls.msora.MDB_MSora.GetCompanyInfo(MSora_Price.gconnMDB).Replace(vbCrLf, "' + CHR(10) + '") & "'"
                End If
                If "{@VB_USER}" = CRFormulaDefinition.FormulaName.Trim.ToUpper Then
                    objReport.DataDefinition.FormulaFields(intCounter).Text = "" '& cls.msora.MDB_MSora.GetUserName(MSora_Price.gstrUserId, MSora_Price.gconnMDB).Replace(vbCrLf, "' + CHR(10) + '") & "'"
                End If
                If "{@VB_REPORT_HEADER}" = CRFormulaDefinition.FormulaName.Trim.ToUpper Then
                    objReport.DataDefinition.FormulaFields(intCounter).Text = "" '& strReportCaption.Replace(vbCrLf, "' + CHR(10) + '") & "'"
                End If
                If "{@VB_FILTER}" = CRFormulaDefinition.FormulaName.Trim.ToUpper Then
                    objReport.DataDefinition.FormulaFields(intCounter).Text = "" '& strFilter.Replace(vbCrLf, "' + CHR(10) + '") & "'"
                End If

            Next


            'Re setting control 
            rptViewer.ReportSource = Nothing

            'Set the current report object to report.
            rptViewer.ReportSource = objReport

            'Show the report
            'rptViewer.Show()
            Return True
        Catch ex As System.Exception
            modLog.AddToErrorLog(ex.Message)
        End Try
    End Function

    Private Sub frmViewReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Translate.TranslateControl(Me)
    End Sub

    Private Sub rptViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rptViewer.Load
        'modLog.AddToErrorLog("Report Load")
    End Sub

    Private Sub rptViewer_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles rptViewer.Paint
        'modLog.AddToErrorLog("Report Paint")
    End Sub

    Private Function SetupReport(ByRef objCrystalReportDocument As CrystalDecisions.CrystalReports.Engine.ReportDocument) As System.Boolean
        Dim crTableLogOnInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim crDatabase As CrystalDecisions.CrystalReports.Engine.Database
        Dim crTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim aTable As CrystalDecisions.CrystalReports.Engine.Table
        Dim blnTest As System.Boolean
        Dim strLocation As System.String

        crDatabase = objCrystalReportDocument.Database
        crTables = crDatabase.Tables
        For Each aTable In crTables
            crTableLogOnInfo = aTable.LogOnInfo
            strDSN = crTableLogOnInfo.ConnectionInfo.ServerName
            strDB = crTableLogOnInfo.ConnectionInfo.DatabaseName
            strUID = crTableLogOnInfo.ConnectionInfo.UserID
            strPWD = crTableLogOnInfo.ConnectionInfo.Password
            Debug.Print("BEFORE")
            Debug.Print("TABLE NAME: " & aTable.Name)
            Debug.Print("TABLE LOC: " & aTable.Location)
            Debug.Print("SERVER: " & strDSN)
            Debug.Print("DB: " & strDB)
            Debug.Print("UID: " & strUID)
            Debug.Print("PWD: " & strPWD)
            Debug.Print("REPORT NAME: " & crTableLogOnInfo.ReportName)
            Debug.Print("Table Name: " & crTableLogOnInfo.TableName)
            aTable.ApplyLogOnInfo(crTableLogOnInfo)

            strLocation = Application.StartupPath '& "\" & cls.Config.DatabaseName 'pass new mdb name

            Debug.Print("New Location: " & strLocation)
            Try
                aTable.LogOnInfo.ConnectionInfo.Password = "" 'MSora_Price.Decrypt(cls.Config.GetDatabasePwd)

                aTable.Location = strLocation
            Catch ex As Exception
                Debug.Print("Set Location Error: " & ex.ToString)
            End Try
            Debug.Print("AFTER")
            Debug.Print("TABLE NAME: " & aTable.Name)
            Debug.Print("TABLE LOC: " & aTable.Location)
            Debug.Print("SERVER: " & strDSN)
            Debug.Print("DB: " & strDB)
            Debug.Print("UID: " & strUID)
            Debug.Print("PWD: " & strPWD)
            Debug.Print("REPORT NAME: " & crTableLogOnInfo.ReportName)
            Debug.Print("Table Name: " & crTableLogOnInfo.TableName)
            Try
                blnTest = aTable.TestConnectivity()
                Debug.Print("CONNECTED? " & blnTest.ToString())
            Catch ex As Exception
                Debug.Print("CONNECTED? NO")
                Debug.Print(ex.ToString)
            End Try
        Next aTable

    End Function
End Class