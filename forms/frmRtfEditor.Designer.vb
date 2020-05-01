<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRtfEditor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.rtbText = New RicherTextBox.RicherTextBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rtbText
        '
        Me.rtbText.AlignCenterVisible = True
        Me.rtbText.AlignLeftVisible = True
        Me.rtbText.AlignRightVisible = True
        Me.rtbText.BoldVisible = True
        Me.rtbText.BulletsVisible = True
        Me.rtbText.ChooseFontVisible = True
        Me.rtbText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbText.FindReplaceVisible = True
        Me.rtbText.FontColorVisible = True
        Me.rtbText.FontFamilyVisible = True
        Me.rtbText.FontSizeVisible = True
        Me.rtbText.GroupAlignmentVisible = True
        Me.rtbText.GroupBoldUnderlineItalicVisible = True
        Me.rtbText.GroupFontColorVisible = True
        Me.rtbText.GroupFontNameAndSizeVisible = True
        Me.rtbText.GroupIndentationAndBulletsVisible = True
        Me.rtbText.GroupInsertVisible = False
        Me.rtbText.GroupSaveAndLoadVisible = False
        Me.rtbText.GroupZoomVisible = True
        Me.rtbText.IndentVisible = True
        Me.rtbText.InsertPictureVisible = False
        Me.rtbText.ItalicVisible = True
        Me.rtbText.LoadVisible = True
        Me.rtbText.Location = New System.Drawing.Point(0, 0)
        Me.rtbText.Name = "rtbText"
        Me.rtbText.OutdentVisible = True
        Me.rtbText.Rtf = "{\rtf1\ansi\ansicpg1250\deff0\deflang1060{\fonttbl{\f0\fnil\fcharset238 Microsoft" & _
            " Sans Serif;}}" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "\viewkind4\uc1\pard\f0\fs18\par" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "}" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.rtbText.SaveVisible = False
        Me.rtbText.SeparatorAlignVisible = True
        Me.rtbText.SeparatorBoldUnderlineItalicVisible = True
        Me.rtbText.SeparatorFontColorVisible = True
        Me.rtbText.SeparatorFontVisible = True
        Me.rtbText.SeparatorIndentAndBulletsVisible = True
        Me.rtbText.SeparatorInsertVisible = True
        Me.rtbText.SeparatorSaveLoadVisible = True
        Me.rtbText.Size = New System.Drawing.Size(780, 517)
        Me.rtbText.TabIndex = 0
        Me.rtbText.ToolStripVisible = True
        Me.rtbText.UnderlineVisible = True
        Me.rtbText.WordWrapVisible = True
        Me.rtbText.ZoomFactorTextVisible = True
        Me.rtbText.ZoomInVisible = True
        Me.rtbText.ZoomOutVisible = True
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.Image = Global.eProdService.My.Resources.Resources.save1
        Me.cmdSave.Location = New System.Drawing.Point(708, 0)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(72, 58)
        Me.cmdSave.TabIndex = 1
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'frmRtfEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 517)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.rtbText)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRtfEditor"
        Me.Text = "Urejanje"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbText As RicherTextBox.RicherTextBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
End Class
