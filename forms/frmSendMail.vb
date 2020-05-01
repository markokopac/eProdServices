Public Class frmSendMail

    Private Sub frmSendMail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtSMTPServer.Text = cls.Config.GetMailSMTP
        Me.txtSMTPPort.Text = cls.Config.GetMailSMTPPort
        Me.txtEmail.Text = cls.Config.GetMailAddress
        Me.txtUsername.Text = cls.Config.GetMailUsername
        Me.txtPassword.Text = cls.Config.GetMailPassword
    End Sub


    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click
        Dim Recipients As New List(Of String)
        Dim strRecipients() As String
        Dim strMessage As String = ""

        strRecipients = Me.txtPrejemniki.Text.ToString.Split(";")
        For i = 0 To strRecipients.Count - 1
            Recipients.Add(strRecipients(i))
        Next


        Dim FromEmailAddress As String = Me.txtEmail.Text

        Dim Subject As String = Me.txtSubject.Text
        Dim Body As String = ConvertToHTML(Me.rtfMessage)
        Dim UserName As String = Me.txtUsername.Text
        Dim Password As String = Me.txtPassword.Text
        Dim Port As Integer = CInt(Me.txtSMTPPort.Text)
        Dim Server As String = Me.txtSMTPServer.Text
        Dim FileAttachments As New List(Of String)

        If Not SendEmailMailbee(txtPrejemniki.Text, FromEmailAddress, Subject, Body, UserName, Password, strMessage, FileAttachments, Server, Port) Then
            MsgBox(strMessage)
        End If
        'MsgBox(SendEmailMailBee(Recipients, FromEmailAddress, Subject, Body, UserName, Password, Server, Port, Attachments))
        'MsgBox(SendEmailFreeSMTP(Recipients, FromEmailAddress, Subject, Body, UserName, Password, Server, Port, Attachments))

    End Sub

   
    Private Sub txtPrejemniki_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPrejemniki.TextChanged

    End Sub

    Private Sub rtfMessage_TextChanged(sender As System.Object, e As System.EventArgs) Handles rtfMessage.TextChanged

    End Sub
End Class