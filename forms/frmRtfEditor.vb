Option Explicit On

Public Class frmRtfEditor
    Public mblnSave As Boolean

    Private Sub rtbText_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtbText.Load
        mblnSave = False
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        mblnSave = True
        Me.Hide()
    End Sub

    Private Sub frmRtfEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class