'Imports System.Net.Mail
Imports System.IO

Imports MailBee
Imports MailBee.SmtpMail
Imports MailBee.DnsMX
Imports MailBee.Mime

Module modMailing

    Public Enum SendToType As Integer
        ToPartner = 0
        ToComercialist = 1
        ToManager = 2
        ToProduction = 3
        ToTechnology = 4
        ToLogistic = 5
        ToDeveloper = 6
        Manual = 7
        ToExtra = 8
    End Enum





    Public Function SendEmailMailbee(ByVal Recipients As String, _
                     ByVal FromAddress As String, _
                     ByVal Subject As String, _
                     ByVal Body As String, _
                     ByVal UserName As String, _
                     ByVal Password As String, _
                     ByRef errMsg As String, _
                     ByVal FileAttachments As List(Of String),
                     Optional ByVal Server As String = "smtp.gmail.com", _
                     Optional ByVal Port As Integer = 587) As Boolean

        Dim msg As New MailMessage
        Dim oMailer As Smtp
        Dim blnSuccess As Boolean = False
        Dim strFile As String = ""
        ' Set the license key
        MailBee.Global.LicenseKey = cls.Config.LicenceKey

        ' Set mail server name. If mail server is blank,
        ' MailBee will try to connect to local machine
        oMailer = New Smtp

        oMailer.DnsServers.Clear()
        oMailer.SmtpServers.Clear()
        ' If SMTP authentication is enabled, set
        ' authentication credentials

        'oMailer.SmtpServers.Add(Server, UserName, Password, AuthenticationMethods.SaslLogin Or AuthenticationMethods.SaslPlain)
        'oMailer.SmtpServers.Add(Server, UserName, Password)
        'oMailer.SmtpServers.Add("posta.m-sora.si", 587)

        Dim S As New SmtpServer

        'msora.mizarstvo@gmail.com; Msora2019!
        'Server = "smtp.gmail.com"
        'UserName = "msora.mizarstvo@gmail.com"
        'Password = "Msora2019!"


        S.Name = Server
        S.AccountName = UserName
        S.Password = Password
        S.Port = Port
        S.SslMode = Security.SslStartupMode.Manual
        S.AuthMethods = AuthenticationMethods.None



        'dodamo priponke
        oMailer.SmtpServers.Add(S)

        For i = 0 To FileAttachments.Count - 1
            strFile = Application.StartupPath & "\Pictures\" & Mid(FileAttachments(i).ToString.Trim, 3, FileAttachments(i).ToString.Trim.Length - 3)
            If File.Exists(strFile) Then
                '                oMailer.AddAttachment(strFile, , "pic" + (i + 1).ToString)
                'oMailer.AddAttachment(strFile)
                msg.Attachments.Add(strFile, (i + 1).ToString, "pic" + (i + 1).ToString)
            End If
        Next

        'oMailer.From.AsString = FromAddress
        msg.From.AsString = FromAddress

        'oMailer.To.AsString = Recipients
        msg.To.AsString = Recipients

        'oMailer.Subject = Subject
        msg.Subject = Subject

        ' Set message body
        'oMailer.Message.BodyHtmlText = Body
        msg.BodyHtmlText = Body

        oMailer.Message = msg



        ' Send it!
        If oMailer.Send Then
            blnSuccess = True
            errMsg = "Message sent successfully!"
        Else
            errMsg = DisplayError(oMailer)
            blnSuccess = False
        End If

        ' Disconnect from SMTP server if still connected
        If oMailer.IsConnected Then
            oMailer.Disconnect()
        End If
        Return blnSuccess
    End Function

    Public Function SendEmailMailbeeOld(ByVal Recipients As String, _
                     ByVal FromAddress As String, _
                     ByVal Subject As String, _
                     ByVal Body As String, _
                     ByVal UserName As String, _
                     ByVal Password As String, _
                     ByRef errMsg As String, _
                     ByVal FileAttachments As List(Of String),
                     Optional ByVal Server As String = "smtp.gmail.com", _
                     Optional ByVal Port As Integer = 587, _
                     Optional ByVal Attachments As List(Of String) = Nothing) As Boolean

        Dim oMailer As Object = CreateObject("MailBee.SMTP")
        Dim blnSuccess As Boolean = False
        Dim strFile As String = ""
        ' Set the license key
        oMailer.LicenseKey = cls.Config.LicenceKey

        ' Set mail server name. If mail server is blank,
        ' MailBee will try to connect to local machine
        oMailer.ServerName = Server


        ' If SMTP authentication is enabled, set
        ' authentication credentials
        oMailer.AuthMethod = 2
        oMailer.UserName = UserName
        oMailer.Password = Password

        'dodamo priponke

        'oMailer.Message.AddAttachment(Application.StartupPath & "\Pictures\BAU15_msora.jpg")
        'oMailer.Message.AddAttachment(Application.StartupPath & "\Pictures\Msora_logotip_sivi_slo_small.jpg")

        For i = 0 To FileAttachments.Count - 1
            strFile = Application.StartupPath & "\Pictures\" & Mid(FileAttachments(i).ToString.Trim, 3, FileAttachments(i).ToString.Trim.Length - 3)
            If File.Exists(strFile) Then
                oMailer.AddAttachment(strFile, , "pic" + (i + 1).ToString)
            End If
        Next



        ' Set message headers
        oMailer.Message.FromAddr = FromAddress
        oMailer.Message.ToAddr = Recipients
        oMailer.Message.Subject = Subject

        ' Set message body
        oMailer.Message.BodyText = Body

        ' Set message format. If 0, plain-text;
        ' if 1, HTML text.
        oMailer.Message.BodyFormat = 1

        oMailer.SSL.Enabled = True

        oMailer.PortNumber = Port
        oMailer.SSL.UseStartTLS = False

        ' Send it!
        If oMailer.Send Then
            blnSuccess = True
            errMsg = "Message sent successfully!"
        Else
            errMsg = DisplayError(oMailer)
            blnSuccess = False
        End If

        ' Disconnect from SMTP server if still connected
        If oMailer.Connected Then
            oMailer.Disconnect()
        End If
        Return blnSuccess
    End Function

    ' Displays MailBee error
    Private Function DisplayError(ByVal oMailer As Object) As String
        Dim strDetailed As String

        Select Case oMailer.ErrCode
            Case 3 : strDetailed = "No machine under this name"
            Case 4 : strDetailed = "No such IP or SMTP service is not available"
            Case 5 : strDetailed = "Timeout occurred"
            Case 6 : strDetailed = "Connection aborted by the server"
            Case 8 : strDetailed = "Unknown connection error"
            Case 111 : strDetailed = "Authentication is not supported"
            Case 112 : strDetailed = "Unsupported authentication method"
            Case 113 : strDetailed = "Unknown authentication error"
            Case 114 : strDetailed = "Wrong password or username"
            Case 115 : strDetailed = "Local machine name not resolved"
            Case 116 : strDetailed = "Unknown SMTP error"
            Case 121 : strDetailed = "At least one sender and recipient must be specified"
            Case 122 : strDetailed = "The sender specified is not allowed by this mail server"
            Case 123 : strDetailed = "One or more recipients were rejected by the mail server"
            Case Else : strDetailed = oMailer.ErrDesc
        End Select

        If oMailer.ServerResponse <> "" Then
            Return strDetailed & ", Error #" & oMailer.ErrCode & vbCrLf & _
                "Server responded: " & oMailer.ServerResponse
        Else
            Return strDetailed & ", Error #" & oMailer.ErrCode
        End If
    End Function
    

    

End Module