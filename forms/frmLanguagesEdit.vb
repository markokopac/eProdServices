Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports eProdService.cls.msora.DB_MSora


Public Class frmLanguagesEdit

    Public mstrCode As String
    Public mintEditMode As clsGlobal.EditMode


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If SaveRecord() Then

            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function SaveRecord() As Boolean
        Dim strSQL As String = ""
        Dim strLanguage As String = ""

        If Me.txtID.Text = "" Then
            Me.txtID.Focus()
            Return False
        End If

        If Me.txtLanguage.Text = "" Then
            Me.txtLanguage.Focus()
            Return False
        End If

        strLanguage = Me.txtID.Text
        Using conn As MySqlConnection = GetMyConnection("msora")
            Select Case mintEditMode
                Case clsGlobal.EditMode.record_insert
                    'preverim, če koda že obstaja
                    strSQL = "SELECT * FROM language WHERE language = @strLanguage"

                    Dim cmd As New MySqlCommand(strSQL, conn)
                    cmd.Parameters.AddWithValue("@strLanguage", strLanguage)

                    Dim dt As New DataTable
                    dt = GetMyData(cmd)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Language code already exists", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Error")
                        Return False
                    End If

                    'vstavim kodo
                    strSQL = "INSERT INTO language (language, description) VALUES (@language, @description)"
                    cmd = New MySqlCommand(strSQL, conn)
                    cmd.Parameters.AddWithValue("@language", strLanguage)
                    cmd.Parameters.AddWithValue("@description", Me.txtLanguage.Text)

                    Call ExecuteMyNonQuery(cmd)

                    Return True

                Case clsGlobal.EditMode.record_edit
                    If strLanguage <> mstrCode Then
                        'zamenjam jezik v vseh tabelah
                        'country
                        strSQL = "UPDATE country SET language = @strLanguage WHERE language = @mstrCode"
                       

                        Dim cmd As New MySqlCommand(strSQL, conn)
                        cmd.Parameters.AddWithValue("@strLanguage", strLanguage)
                        cmd.Parameters.AddWithValue("@mstrCode", mstrCode)

                        Call ExecuteMyNonQuery(cmd)
                        'orders
                        strSQL = "UPDATE orders SET language = @strLanguage WHERE language = @mstrCode"

                        cmd = New MySqlCommand(strSQL, conn)
                        cmd.Parameters.AddWithValue("@strLanguage", strLanguage)
                        cmd.Parameters.AddWithValue("@mstrCode", mstrCode)

                        Call ExecuteMyNonQuery(cmd)
                        'naredim update
                        strSQL = "UPDATE language SET language = @strLanguage, description = @description WHERE language = @mstrCode"
                        cmd = New MySqlCommand(strSQL, conn)
                        cmd.Parameters.AddWithValue("@strLanguage", strLanguage)
                        cmd.Parameters.AddWithValue("@description", Me.txtLanguage.Text)
                        cmd.Parameters.AddWithValue("@mstrCode", mstrCode)

                        Call ExecuteMyNonQuery(cmd)
                    Else
                        'naredim update
                        
                        strSQL = "UPDATE language SET description = @description WHERE language = @mstrCode"

                        Dim cmd As New MySqlCommand(strSQL, conn)

                        cmd = New MySqlCommand(strSQL, conn)
                        cmd.Parameters.AddWithValue("@description", Me.txtLanguage.Text)
                        cmd.Parameters.AddWithValue("@mstrCode", mstrCode)


                        Call ExecuteMyNonQuery(cmd)

                    End If
            End Select
        End Using
    End Function

    Private Sub frmLanguagesEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtID.Text = mstrCode
        Select Case mintEditMode
            Case clsGlobal.EditMode.record_edit
                Using conn As MySqlConnection = GetMyConnection("msora")
                    Dim strSQL As String = ""
                    strSQL = "SELECT description FROM language WHERE language = @strLanguage"
                    

                    Dim cmd As New MySqlCommand(strSQL, conn)
                    cmd.Parameters.AddWithValue("@strLanguage", Me.mstrCode)

                    Dim dt As New DataTable
                    dt = GetMyData(cmd)
                    If dt.Rows.Count > 0 Then
                        Me.txtLanguage.Text = dt(0)("description")
                    Else
                        Me.Close()
                    End If
                End Using
            Case clsGlobal.EditMode.record_insert
                Me.txtLanguage.Text = ""
        End Select
    End Sub
End Class
