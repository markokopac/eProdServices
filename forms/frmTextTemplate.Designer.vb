<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTextTemplate
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.dgText = New System.Windows.Forms.DataGridView()
        Me.text_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.text_description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tabBodyLang = New System.Windows.Forms.TabControl()
        Me.tabBodySL = New System.Windows.Forms.TabPage()
        Me.rtbBodySL = New System.Windows.Forms.RichTextBox()
        Me.tabBodyEN = New System.Windows.Forms.TabPage()
        Me.rtbBodyEN = New System.Windows.Forms.RichTextBox()
        Me.tabBodyDE = New System.Windows.Forms.TabPage()
        Me.rtbBodyDE = New System.Windows.Forms.RichTextBox()
        Me.tabBodyIT = New System.Windows.Forms.TabPage()
        Me.rtbBodyIT = New System.Windows.Forms.RichTextBox()
        Me.tabBodyFR = New System.Windows.Forms.TabPage()
        Me.rtbBodyFR = New System.Windows.Forms.RichTextBox()
        Me.tabBodyHR = New System.Windows.Forms.TabPage()
        Me.rtbBodyHR = New System.Windows.Forms.RichTextBox()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdRtfEdit = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.dgText, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.tabBodyLang.SuspendLayout()
        Me.tabBodySL.SuspendLayout()
        Me.tabBodyEN.SuspendLayout()
        Me.tabBodyDE.SuspendLayout()
        Me.tabBodyIT.SuspendLayout()
        Me.tabBodyFR.SuspendLayout()
        Me.tabBodyHR.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Size = New System.Drawing.Size(1422, 630)
        Me.SplitContainer1.SplitterDistance = 499
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.dgText)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmdAdd)
        Me.SplitContainer2.Size = New System.Drawing.Size(499, 630)
        Me.SplitContainer2.SplitterDistance = 539
        Me.SplitContainer2.TabIndex = 0
        '
        'dgText
        '
        Me.dgText.AllowUserToAddRows = False
        Me.dgText.AllowUserToDeleteRows = False
        Me.dgText.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgText.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.text_id, Me.text_description})
        Me.dgText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgText.Location = New System.Drawing.Point(0, 0)
        Me.dgText.Name = "dgText"
        Me.dgText.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgText.Size = New System.Drawing.Size(497, 537)
        Me.dgText.TabIndex = 0
        '
        'text_id
        '
        Me.text_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.text_id.DataPropertyName = "text_id"
        Me.text_id.HeaderText = "ID"
        Me.text_id.Name = "text_id"
        Me.text_id.ReadOnly = True
        Me.text_id.Width = 51
        '
        'text_description
        '
        Me.text_description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.text_description.DataPropertyName = "text_description"
        Me.text_description.HeaderText = "Opis"
        Me.text_description.Name = "text_description"
        Me.text_description.ReadOnly = True
        Me.text_description.Width = 69
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = Global.eProdService.My.Resources.Resources.plus
        Me.cmdAdd.Location = New System.Drawing.Point(40, 8)
        Me.cmdAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(67, 60)
        Me.cmdAdd.TabIndex = 20
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'SplitContainer3
        '
        Me.SplitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.SplitContainer4)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmdDelete)
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmdRtfEdit)
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmdClose)
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmdSave)
        Me.SplitContainer3.Size = New System.Drawing.Size(919, 630)
        Me.SplitContainer3.SplitterDistance = 539
        Me.SplitContainer3.TabIndex = 0
        '
        'SplitContainer4
        '
        Me.SplitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer4.Panel1.Controls.Add(Me.txtID)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.tabBodyLang)
        Me.SplitContainer4.Size = New System.Drawing.Size(919, 539)
        Me.SplitContainer4.SplitterDistance = 90
        Me.SplitContainer4.TabIndex = 8
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(111, 53)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(554, 26)
        Me.txtDescription.TabIndex = 7
        '
        'txtID
        '
        Me.txtID.Enabled = False
        Me.txtID.Location = New System.Drawing.Point(111, 22)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(113, 26)
        Me.txtID.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 20)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Opis:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 20)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "ID:"
        '
        'tabBodyLang
        '
        Me.tabBodyLang.Controls.Add(Me.tabBodySL)
        Me.tabBodyLang.Controls.Add(Me.tabBodyEN)
        Me.tabBodyLang.Controls.Add(Me.tabBodyDE)
        Me.tabBodyLang.Controls.Add(Me.tabBodyIT)
        Me.tabBodyLang.Controls.Add(Me.tabBodyFR)
        Me.tabBodyLang.Controls.Add(Me.tabBodyHR)
        Me.tabBodyLang.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabBodyLang.Location = New System.Drawing.Point(0, 0)
        Me.tabBodyLang.Name = "tabBodyLang"
        Me.tabBodyLang.SelectedIndex = 0
        Me.tabBodyLang.Size = New System.Drawing.Size(917, 443)
        Me.tabBodyLang.TabIndex = 8
        '
        'tabBodySL
        '
        Me.tabBodySL.Controls.Add(Me.rtbBodySL)
        Me.tabBodySL.Location = New System.Drawing.Point(4, 29)
        Me.tabBodySL.Name = "tabBodySL"
        Me.tabBodySL.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBodySL.Size = New System.Drawing.Size(909, 410)
        Me.tabBodySL.TabIndex = 0
        Me.tabBodySL.Text = "SL"
        Me.tabBodySL.UseVisualStyleBackColor = True
        '
        'rtbBodySL
        '
        Me.rtbBodySL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbBodySL.Location = New System.Drawing.Point(3, 3)
        Me.rtbBodySL.Name = "rtbBodySL"
        Me.rtbBodySL.Size = New System.Drawing.Size(903, 404)
        Me.rtbBodySL.TabIndex = 0
        Me.rtbBodySL.Text = ""
        '
        'tabBodyEN
        '
        Me.tabBodyEN.Controls.Add(Me.rtbBodyEN)
        Me.tabBodyEN.Location = New System.Drawing.Point(4, 25)
        Me.tabBodyEN.Name = "tabBodyEN"
        Me.tabBodyEN.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBodyEN.Size = New System.Drawing.Size(911, 416)
        Me.tabBodyEN.TabIndex = 1
        Me.tabBodyEN.Text = "EN"
        Me.tabBodyEN.UseVisualStyleBackColor = True
        '
        'rtbBodyEN
        '
        Me.rtbBodyEN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbBodyEN.Location = New System.Drawing.Point(3, 3)
        Me.rtbBodyEN.Name = "rtbBodyEN"
        Me.rtbBodyEN.Size = New System.Drawing.Size(905, 410)
        Me.rtbBodyEN.TabIndex = 1
        Me.rtbBodyEN.Text = ""
        '
        'tabBodyDE
        '
        Me.tabBodyDE.Controls.Add(Me.rtbBodyDE)
        Me.tabBodyDE.Location = New System.Drawing.Point(4, 25)
        Me.tabBodyDE.Name = "tabBodyDE"
        Me.tabBodyDE.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBodyDE.Size = New System.Drawing.Size(911, 416)
        Me.tabBodyDE.TabIndex = 2
        Me.tabBodyDE.Text = "DE"
        Me.tabBodyDE.UseVisualStyleBackColor = True
        '
        'rtbBodyDE
        '
        Me.rtbBodyDE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbBodyDE.Location = New System.Drawing.Point(3, 3)
        Me.rtbBodyDE.Name = "rtbBodyDE"
        Me.rtbBodyDE.Size = New System.Drawing.Size(905, 410)
        Me.rtbBodyDE.TabIndex = 1
        Me.rtbBodyDE.Text = ""
        '
        'tabBodyIT
        '
        Me.tabBodyIT.Controls.Add(Me.rtbBodyIT)
        Me.tabBodyIT.Location = New System.Drawing.Point(4, 25)
        Me.tabBodyIT.Name = "tabBodyIT"
        Me.tabBodyIT.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBodyIT.Size = New System.Drawing.Size(911, 416)
        Me.tabBodyIT.TabIndex = 3
        Me.tabBodyIT.Text = "IT"
        Me.tabBodyIT.UseVisualStyleBackColor = True
        '
        'rtbBodyIT
        '
        Me.rtbBodyIT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbBodyIT.Location = New System.Drawing.Point(3, 3)
        Me.rtbBodyIT.Name = "rtbBodyIT"
        Me.rtbBodyIT.Size = New System.Drawing.Size(905, 410)
        Me.rtbBodyIT.TabIndex = 1
        Me.rtbBodyIT.Text = ""
        '
        'tabBodyFR
        '
        Me.tabBodyFR.Controls.Add(Me.rtbBodyFR)
        Me.tabBodyFR.Location = New System.Drawing.Point(4, 25)
        Me.tabBodyFR.Name = "tabBodyFR"
        Me.tabBodyFR.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBodyFR.Size = New System.Drawing.Size(911, 416)
        Me.tabBodyFR.TabIndex = 4
        Me.tabBodyFR.Text = "FR"
        Me.tabBodyFR.UseVisualStyleBackColor = True
        '
        'rtbBodyFR
        '
        Me.rtbBodyFR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbBodyFR.Location = New System.Drawing.Point(3, 3)
        Me.rtbBodyFR.Name = "rtbBodyFR"
        Me.rtbBodyFR.Size = New System.Drawing.Size(905, 410)
        Me.rtbBodyFR.TabIndex = 1
        Me.rtbBodyFR.Text = ""
        '
        'tabBodyHR
        '
        Me.tabBodyHR.Controls.Add(Me.rtbBodyHR)
        Me.tabBodyHR.Location = New System.Drawing.Point(4, 25)
        Me.tabBodyHR.Name = "tabBodyHR"
        Me.tabBodyHR.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBodyHR.Size = New System.Drawing.Size(911, 416)
        Me.tabBodyHR.TabIndex = 5
        Me.tabBodyHR.Text = "HR"
        Me.tabBodyHR.UseVisualStyleBackColor = True
        '
        'rtbBodyHR
        '
        Me.rtbBodyHR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbBodyHR.Location = New System.Drawing.Point(3, 3)
        Me.rtbBodyHR.Name = "rtbBodyHR"
        Me.rtbBodyHR.Size = New System.Drawing.Size(905, 410)
        Me.rtbBodyHR.TabIndex = 1
        Me.rtbBodyHR.Text = ""
        '
        'cmdDelete
        '
        Me.cmdDelete.Image = Global.eProdService.My.Resources.Resources.delete
        Me.cmdDelete.Location = New System.Drawing.Point(199, 9)
        Me.cmdDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(66, 60)
        Me.cmdDelete.TabIndex = 21
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'cmdRtfEdit
        '
        Me.cmdRtfEdit.Image = Global.eProdService.My.Resources.Resources.rtf
        Me.cmdRtfEdit.Location = New System.Drawing.Point(349, 8)
        Me.cmdRtfEdit.Name = "cmdRtfEdit"
        Me.cmdRtfEdit.Size = New System.Drawing.Size(64, 60)
        Me.cmdRtfEdit.TabIndex = 21
        Me.cmdRtfEdit.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Image = Global.eProdService.My.Resources.Resources.gnome_application_exit
        Me.cmdClose.Location = New System.Drawing.Point(754, 9)
        Me.cmdClose.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(63, 59)
        Me.cmdClose.TabIndex = 20
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Image = Global.eProdService.My.Resources.Resources.save1
        Me.cmdSave.Location = New System.Drawing.Point(4, 8)
        Me.cmdSave.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(61, 60)
        Me.cmdSave.TabIndex = 18
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'frmTextTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1422, 630)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmTextTemplate"
        Me.Text = "Tekstovne predloge"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.dgText, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        Me.tabBodyLang.ResumeLayout(False)
        Me.tabBodySL.ResumeLayout(False)
        Me.tabBodyEN.ResumeLayout(False)
        Me.tabBodyDE.ResumeLayout(False)
        Me.tabBodyIT.ResumeLayout(False)
        Me.tabBodyFR.ResumeLayout(False)
        Me.tabBodyHR.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgText As System.Windows.Forms.DataGridView
    Friend WithEvents text_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents text_description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdRtfEdit As System.Windows.Forms.Button
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tabBodyLang As System.Windows.Forms.TabControl
    Friend WithEvents tabBodySL As System.Windows.Forms.TabPage
    Friend WithEvents rtbBodySL As System.Windows.Forms.RichTextBox
    Friend WithEvents tabBodyEN As System.Windows.Forms.TabPage
    Friend WithEvents rtbBodyEN As System.Windows.Forms.RichTextBox
    Friend WithEvents tabBodyDE As System.Windows.Forms.TabPage
    Friend WithEvents rtbBodyDE As System.Windows.Forms.RichTextBox
    Friend WithEvents tabBodyIT As System.Windows.Forms.TabPage
    Friend WithEvents rtbBodyIT As System.Windows.Forms.RichTextBox
    Friend WithEvents tabBodyFR As System.Windows.Forms.TabPage
    Friend WithEvents rtbBodyFR As System.Windows.Forms.RichTextBox
    Friend WithEvents tabBodyHR As System.Windows.Forms.TabPage
    Friend WithEvents rtbBodyHR As System.Windows.Forms.RichTextBox
End Class
