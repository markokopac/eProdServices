
Imports System.Threading

Module modLog



    Public Sub AddToErrorLog(ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ErrorLogName
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbCrLf & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        frmMainForm.txtLog.AppendText(Now.ToString & " ERROR! " & Mid(strLog, 1, 100))

        ' ce je npaka v zvezi s povezavo, potem poskusim vzpostaviti še enkrat
        If strLog.Contains("MySql") Then
            Thread.Sleep(10000)

            strLogNew = Now.ToString & vbCrLf & "***** Application restart *****" & vbCrLf

            objWriter = New System.IO.StreamWriter(strActLog, True)
            objWriter.WriteLine(strLogNew)
            objWriter.Close()

            'Dim p() As Process
            'p = Process.GetProcessesByName("eProdService")
            'MsgBox("Število procesov: " & p.Count)
            'If p.Count > 1 Then
            'System.Threading.Thread.Sleep(10000)
            'p(0).Kill()
            'Else
            System.Threading.Thread.Sleep(10000)
            Application.Restart()
            'End If



            'Try
            'gConnEProd = cls.msora.DB_MSora.GetMyConnection("eprod")
            'strLogNew = Now.ToString & vbCrLf & "Database reconnected OK!" & vbCrLf
            'frmMainForm.txtLog.AppendText(Now.ToString & " Database reconnected OK!" + vbCrLf)
            'Catch ex As Exception
            'strLogNew = Now.ToString & vbCrLf & "Database reconnected Failed!" & vbCrLf
            'frmMainForm.txtLog.AppendText(Now.ToString & " Database reconnected Failed!" + vbCrLf)
            'End
            'End Try

        End If

    End Sub

    Public Sub AddToActionLogUpdateNameTB(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogUpdateName
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddToActionCutterSendFileTB(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogCutterSendFile
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddToActionCutterSendFile(ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogCutterSendFile
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

    End Sub

    Public Sub AddToActionCutterArchiveTB(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogCutterArchive
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddToActionUpdateSklicTB(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogUpdateSklic
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddToActionUpdateRKoncanoTB(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogUpdateRKoncano

        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddToActionUpdateMonterTB(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogUpdateMonter
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddToActionUpdateKapaLog(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogKapaLog
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub
    Public Sub AddToActionUpdateImportAddress(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogImportAddressLog
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddToActionUpdateImportGlass(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ImportGlassActionLog
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub
    Public Sub AddToActionSpicaEvents(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogKapaLog
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddToActionTechnicalLockEvents(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogTechnicalLock
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddToActionUpdateSlikaVrtanjaTB(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogUpdateSlikavrtanja
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddToActionLogProductionTextTB(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogProductionText
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub
    Public Sub AddToActionLogDeliveryDateTB(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogDeliveryDate
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub
    Public Sub AddToActionLogMAWIDateTB(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogMawiDate
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddTextLog(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogDeliveryDate
      

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        rchTB.AppendText(strLogNew)


    End Sub

    Public Sub AddToActionLogUpdateName(ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogUpdateName
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()


    End Sub

    Public Sub AddToActionLogDeliveryDate(ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogDeliveryDate
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

    End Sub

    Public Sub AddToActionLogSendMailTB(ByVal rchTB As RichTextBox, ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogSendMail
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        rchTB.AppendText(strLogNew)

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

    End Sub

    Public Sub AddToActionLogSendMail(ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = cls.Config.ActionLogSendMail
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

    End Sub

    Public Sub AddToActionLogSendedMails(ByVal strLog As String)
        Dim strLogNew As String
        Dim strLogFile As String = "SendedMails.log"
        Dim strActLog As String

        strActLog = Application.StartupPath & "\Log\" & cls.Utils.RemoveFileExtension(strLogFile) & "_" & Format(Now.Date, "yyyy_MM_dd") & "." & cls.Utils.SamoFileExt(strLogFile)

        strLogNew = Now.ToString & vbTab & strLog & vbCrLf

        Dim objWriter As New System.IO.StreamWriter(strActLog, True)
        objWriter.WriteLine(strLogNew)
        objWriter.Close()

    End Sub
End Module
