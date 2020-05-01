<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEventsEditor
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
        Me.event_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.event_desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdDuplicate = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.rtbBodySL = New System.Windows.Forms.RichTextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtMontageDays = New System.Windows.Forms.TextBox()
        Me.chkCheckMontage = New System.Windows.Forms.CheckBox()
        Me.txtPartnerId = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtEmailManual = New System.Windows.Forms.TextBox()
        Me.chkCheckDelivery = New System.Windows.Forms.CheckBox()
        Me.chkExcludeReclamation = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.nudDays = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtDeveloper = New System.Windows.Forms.TextBox()
        Me.txtLogistic = New System.Windows.Forms.TextBox()
        Me.txtProduction = New System.Windows.Forms.TextBox()
        Me.txtDirector = New System.Windows.Forms.TextBox()
        Me.txtParameter = New System.Windows.Forms.TextBox()
        Me.cboTemplateSubject = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.cboTemplateBody = New System.Windows.Forms.ComboBox()
        Me.cboEventType = New System.Windows.Forms.ComboBox()
        Me.cboWorkstation = New System.Windows.Forms.ComboBox()
        Me.chkDeveloper = New System.Windows.Forms.CheckBox()
        Me.chkLogistic = New System.Windows.Forms.CheckBox()
        Me.chkTechnolog = New System.Windows.Forms.CheckBox()
        Me.chkProduction = New System.Windows.Forms.CheckBox()
        Me.chkDirector = New System.Windows.Forms.CheckBox()
        Me.chkCommercialist = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkPartner = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdRunEvent = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.rtbZadeva = New System.Windows.Forms.RichTextBox()
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
        CType(Me.nudDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Size = New System.Drawing.Size(1189, 681)
        Me.SplitContainer1.SplitterDistance = 340
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.dgText)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmdDuplicate)
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmdAdd)
        Me.SplitContainer2.Size = New System.Drawing.Size(340, 681)
        Me.SplitContainer2.SplitterDistance = 583
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 0
        '
        'dgText
        '
        Me.dgText.AllowUserToAddRows = False
        Me.dgText.AllowUserToDeleteRows = False
        Me.dgText.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgText.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.event_id, Me.event_desc})
        Me.dgText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgText.Location = New System.Drawing.Point(0, 0)
        Me.dgText.Margin = New System.Windows.Forms.Padding(4)
        Me.dgText.Name = "dgText"
        Me.dgText.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgText.Size = New System.Drawing.Size(338, 581)
        Me.dgText.TabIndex = 1
        '
        'event_id
        '
        Me.event_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.event_id.DataPropertyName = "event_id"
        Me.event_id.HeaderText = "ID"
        Me.event_id.Name = "event_id"
        Me.event_id.ReadOnly = True
        Me.event_id.Width = 51
        '
        'event_desc
        '
        Me.event_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.event_desc.DataPropertyName = "event_desc"
        Me.event_desc.HeaderText = "Opis"
        Me.event_desc.Name = "event_desc"
        Me.event_desc.ReadOnly = True
        Me.event_desc.Width = 69
        '
        'cmdDuplicate
        '
        Me.cmdDuplicate.Image = Global.eProdService.My.Resources.Resources.copy
        Me.cmdDuplicate.Location = New System.Drawing.Point(75, 4)
        Me.cmdDuplicate.Margin = New System.Windows.Forms.Padding(5)
        Me.cmdDuplicate.Name = "cmdDuplicate"
        Me.cmdDuplicate.Size = New System.Drawing.Size(60, 58)
        Me.cmdDuplicate.TabIndex = 23
        Me.cmdDuplicate.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = Global.eProdService.My.Resources.Resources.plus
        Me.cmdAdd.Location = New System.Drawing.Point(5, 4)
        Me.cmdAdd.Margin = New System.Windows.Forms.Padding(5)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(60, 58)
        Me.cmdAdd.TabIndex = 22
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'SplitContainer3
        '
        Me.SplitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.rtbZadeva)
        Me.SplitContainer3.Panel1.Controls.Add(Me.rtbBodySL)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label12)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtMontageDays)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkCheckMontage)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtPartnerId)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label11)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label10)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtEmailManual)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkCheckDelivery)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkExcludeReclamation)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label9)
        Me.SplitContainer3.Panel1.Controls.Add(Me.nudDays)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDeveloper)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtLogistic)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtProduction)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDirector)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtParameter)
        Me.SplitContainer3.Panel1.Controls.Add(Me.cboTemplateSubject)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkEnabled)
        Me.SplitContainer3.Panel1.Controls.Add(Me.cboTemplateBody)
        Me.SplitContainer3.Panel1.Controls.Add(Me.cboEventType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.cboWorkstation)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkDeveloper)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkLogistic)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkTechnolog)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkProduction)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkDirector)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkCommercialist)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkPartner)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtID)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmdRunEvent)
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmdDelete)
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmdClose)
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmdSave)
        Me.SplitContainer3.Size = New System.Drawing.Size(844, 681)
        Me.SplitContainer3.SplitterDistance = 586
        Me.SplitContainer3.SplitterWidth = 5
        Me.SplitContainer3.TabIndex = 0
        '
        'rtbBodySL
        '
        Me.rtbBodySL.Location = New System.Drawing.Point(494, 194)
        Me.rtbBodySL.Name = "rtbBodySL"
        Me.rtbBodySL.Size = New System.Drawing.Size(338, 242)
        Me.rtbBodySL.TabIndex = 41
        Me.rtbBodySL.Text = ""
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(318, 284)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(31, 20)
        Me.Label12.TabIndex = 39
        Me.Label12.Text = "dni"
        '
        'txtMontageDays
        '
        Me.txtMontageDays.Location = New System.Drawing.Point(263, 281)
        Me.txtMontageDays.Name = "txtMontageDays"
        Me.txtMontageDays.Size = New System.Drawing.Size(49, 26)
        Me.txtMontageDays.TabIndex = 38
        '
        'chkCheckMontage
        '
        Me.chkCheckMontage.AutoSize = True
        Me.chkCheckMontage.Location = New System.Drawing.Point(21, 287)
        Me.chkCheckMontage.Name = "chkCheckMontage"
        Me.chkCheckMontage.Size = New System.Drawing.Size(242, 24)
        Me.chkCheckMontage.TabIndex = 37
        Me.chkCheckMontage.Text = "Ne pošlji, če je montaža čez"
        Me.chkCheckMontage.UseVisualStyleBackColor = True
        '
        'txtPartnerId
        '
        Me.txtPartnerId.Location = New System.Drawing.Point(196, 325)
        Me.txtPartnerId.Name = "txtPartnerId"
        Me.txtPartnerId.Size = New System.Drawing.Size(292, 26)
        Me.txtPartnerId.TabIndex = 36
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(25, 331)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(180, 20)
        Me.Label11.TabIndex = 35
        Me.Label11.Text = "Velja samo za stranko:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(152, 552)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 20)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "Extra mail:"
        '
        'txtEmailManual
        '
        Me.txtEmailManual.Location = New System.Drawing.Point(257, 549)
        Me.txtEmailManual.Name = "txtEmailManual"
        Me.txtEmailManual.Size = New System.Drawing.Size(349, 26)
        Me.txtEmailManual.TabIndex = 33
        '
        'chkCheckDelivery
        '
        Me.chkCheckDelivery.AutoSize = True
        Me.chkCheckDelivery.Location = New System.Drawing.Point(21, 267)
        Me.chkCheckDelivery.Name = "chkCheckDelivery"
        Me.chkCheckDelivery.Size = New System.Drawing.Size(248, 24)
        Me.chkCheckDelivery.TabIndex = 32
        Me.chkCheckDelivery.Text = "Ne pošlji, če je že dobavljeno"
        Me.chkCheckDelivery.UseVisualStyleBackColor = True
        '
        'chkExcludeReclamation
        '
        Me.chkExcludeReclamation.AutoSize = True
        Me.chkExcludeReclamation.Location = New System.Drawing.Point(21, 247)
        Me.chkExcludeReclamation.Name = "chkExcludeReclamation"
        Me.chkExcludeReclamation.Size = New System.Drawing.Size(172, 24)
        Me.chkExcludeReclamation.TabIndex = 31
        Me.chkExcludeReclamation.Text = "Izključi reklamacije"
        Me.chkExcludeReclamation.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(253, 210)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 20)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "dni"
        '
        'nudDays
        '
        Me.nudDays.Location = New System.Drawing.Point(188, 204)
        Me.nudDays.Name = "nudDays"
        Me.nudDays.Size = New System.Drawing.Size(59, 26)
        Me.nudDays.TabIndex = 29
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 210)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(211, 20)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Preverjaj naloge za zadnje "
        '
        'txtDeveloper
        '
        Me.txtDeveloper.Location = New System.Drawing.Point(257, 522)
        Me.txtDeveloper.Name = "txtDeveloper"
        Me.txtDeveloper.Size = New System.Drawing.Size(349, 26)
        Me.txtDeveloper.TabIndex = 27
        '
        'txtLogistic
        '
        Me.txtLogistic.Location = New System.Drawing.Point(257, 496)
        Me.txtLogistic.Name = "txtLogistic"
        Me.txtLogistic.Size = New System.Drawing.Size(349, 26)
        Me.txtLogistic.TabIndex = 26
        '
        'txtProduction
        '
        Me.txtProduction.Location = New System.Drawing.Point(257, 468)
        Me.txtProduction.Name = "txtProduction"
        Me.txtProduction.Size = New System.Drawing.Size(349, 26)
        Me.txtProduction.TabIndex = 25
        '
        'txtDirector
        '
        Me.txtDirector.Location = New System.Drawing.Point(257, 442)
        Me.txtDirector.Name = "txtDirector"
        Me.txtDirector.Size = New System.Drawing.Size(349, 26)
        Me.txtDirector.TabIndex = 24
        '
        'txtParameter
        '
        Me.txtParameter.Location = New System.Drawing.Point(494, 108)
        Me.txtParameter.Name = "txtParameter"
        Me.txtParameter.Size = New System.Drawing.Size(102, 26)
        Me.txtParameter.TabIndex = 23
        '
        'cboTemplateSubject
        '
        Me.cboTemplateSubject.FormattingEnabled = True
        Me.cboTemplateSubject.Location = New System.Drawing.Point(130, 138)
        Me.cboTemplateSubject.Name = "cboTemplateSubject"
        Me.cboTemplateSubject.Size = New System.Drawing.Size(358, 28)
        Me.cboTemplateSubject.TabIndex = 22
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 146)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 20)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Predloga zadeve:"
        '
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(734, 549)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(98, 24)
        Me.chkEnabled.TabIndex = 20
        Me.chkEnabled.Text = "Omogoči"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'cboTemplateBody
        '
        Me.cboTemplateBody.FormattingEnabled = True
        Me.cboTemplateBody.Location = New System.Drawing.Point(129, 169)
        Me.cboTemplateBody.Name = "cboTemplateBody"
        Me.cboTemplateBody.Size = New System.Drawing.Size(359, 28)
        Me.cboTemplateBody.TabIndex = 19
        '
        'cboEventType
        '
        Me.cboEventType.FormattingEnabled = True
        Me.cboEventType.Location = New System.Drawing.Point(129, 108)
        Me.cboEventType.Name = "cboEventType"
        Me.cboEventType.Size = New System.Drawing.Size(359, 28)
        Me.cboEventType.TabIndex = 18
        '
        'cboWorkstation
        '
        Me.cboWorkstation.FormattingEnabled = True
        Me.cboWorkstation.Location = New System.Drawing.Point(129, 75)
        Me.cboWorkstation.Name = "cboWorkstation"
        Me.cboWorkstation.Size = New System.Drawing.Size(359, 28)
        Me.cboWorkstation.TabIndex = 17
        '
        'chkDeveloper
        '
        Me.chkDeveloper.AutoSize = True
        Me.chkDeveloper.Location = New System.Drawing.Point(155, 524)
        Me.chkDeveloper.Name = "chkDeveloper"
        Me.chkDeveloper.Size = New System.Drawing.Size(108, 24)
        Me.chkDeveloper.TabIndex = 16
        Me.chkDeveloper.Text = "Razvijalcu"
        Me.chkDeveloper.UseVisualStyleBackColor = True
        '
        'chkLogistic
        '
        Me.chkLogistic.AutoSize = True
        Me.chkLogistic.Location = New System.Drawing.Point(155, 498)
        Me.chkLogistic.Name = "chkLogistic"
        Me.chkLogistic.Size = New System.Drawing.Size(93, 24)
        Me.chkLogistic.TabIndex = 15
        Me.chkLogistic.Text = "Logistiki"
        Me.chkLogistic.UseVisualStyleBackColor = True
        '
        'chkTechnolog
        '
        Me.chkTechnolog.AutoSize = True
        Me.chkTechnolog.Location = New System.Drawing.Point(155, 418)
        Me.chkTechnolog.Name = "chkTechnolog"
        Me.chkTechnolog.Size = New System.Drawing.Size(108, 24)
        Me.chkTechnolog.TabIndex = 14
        Me.chkTechnolog.Text = "Tehnologu"
        Me.chkTechnolog.UseVisualStyleBackColor = True
        '
        'chkProduction
        '
        Me.chkProduction.AutoSize = True
        Me.chkProduction.Location = New System.Drawing.Point(155, 470)
        Me.chkProduction.Name = "chkProduction"
        Me.chkProduction.Size = New System.Drawing.Size(118, 24)
        Me.chkProduction.TabIndex = 13
        Me.chkProduction.Text = "Proizvodnja"
        Me.chkProduction.UseVisualStyleBackColor = True
        '
        'chkDirector
        '
        Me.chkDirector.AutoSize = True
        Me.chkDirector.Location = New System.Drawing.Point(155, 444)
        Me.chkDirector.Name = "chkDirector"
        Me.chkDirector.Size = New System.Drawing.Size(104, 24)
        Me.chkDirector.TabIndex = 12
        Me.chkDirector.Text = "Direktorju"
        Me.chkDirector.UseVisualStyleBackColor = True
        '
        'chkCommercialist
        '
        Me.chkCommercialist.AutoSize = True
        Me.chkCommercialist.Location = New System.Drawing.Point(155, 394)
        Me.chkCommercialist.Name = "chkCommercialist"
        Me.chkCommercialist.Size = New System.Drawing.Size(111, 24)
        Me.chkCommercialist.TabIndex = 11
        Me.chkCommercialist.Text = "Zastopniku"
        Me.chkCommercialist.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(23, 372)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(128, 20)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Pošlji sporočilo:"
        '
        'chkPartner
        '
        Me.chkPartner.AutoSize = True
        Me.chkPartner.Location = New System.Drawing.Point(155, 368)
        Me.chkPartner.Name = "chkPartner"
        Me.chkPartner.Size = New System.Drawing.Size(83, 24)
        Me.chkPartner.TabIndex = 9
        Me.chkPartner.Text = "Stranki"
        Me.chkPartner.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 177)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(130, 20)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Predloga teksta:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Tip:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Del. postaja:"
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(130, 42)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(466, 26)
        Me.txtDescription.TabIndex = 5
        '
        'txtID
        '
        Me.txtID.Enabled = False
        Me.txtID.Location = New System.Drawing.Point(129, 10)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(88, 26)
        Me.txtID.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Opis:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ID dogodka:"
        '
        'cmdRunEvent
        '
        Me.cmdRunEvent.Image = Global.eProdService.My.Resources.Resources.run
        Me.cmdRunEvent.Location = New System.Drawing.Point(356, 6)
        Me.cmdRunEvent.Margin = New System.Windows.Forms.Padding(5)
        Me.cmdRunEvent.Name = "cmdRunEvent"
        Me.cmdRunEvent.Size = New System.Drawing.Size(64, 58)
        Me.cmdRunEvent.TabIndex = 24
        Me.cmdRunEvent.UseVisualStyleBackColor = True
        '
        'cmdDelete
        '
        Me.cmdDelete.Image = Global.eProdService.My.Resources.Resources.delete
        Me.cmdDelete.Location = New System.Drawing.Point(239, 6)
        Me.cmdDelete.Margin = New System.Windows.Forms.Padding(5)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(64, 58)
        Me.cmdDelete.TabIndex = 23
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Image = Global.eProdService.My.Resources.Resources.gnome_application_exit
        Me.cmdClose.Location = New System.Drawing.Point(760, 3)
        Me.cmdClose.Margin = New System.Windows.Forms.Padding(5)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(63, 58)
        Me.cmdClose.TabIndex = 21
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Image = Global.eProdService.My.Resources.Resources.save1
        Me.cmdSave.Location = New System.Drawing.Point(5, 3)
        Me.cmdSave.Margin = New System.Windows.Forms.Padding(5)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(61, 58)
        Me.cmdSave.TabIndex = 19
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'rtbZadeva
        '
        Me.rtbZadeva.Location = New System.Drawing.Point(494, 138)
        Me.rtbZadeva.Name = "rtbZadeva"
        Me.rtbZadeva.Size = New System.Drawing.Size(338, 50)
        Me.rtbZadeva.TabIndex = 42
        Me.rtbZadeva.Text = ""
        '
        'frmEventsEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1189, 681)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmEventsEditor"
        Me.Text = "Urejevalec dogodkov"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.dgText, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.nudDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents dgText As System.Windows.Forms.DataGridView
    Friend WithEvents event_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents event_desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkCommercialist As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkPartner As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents chkDeveloper As System.Windows.Forms.CheckBox
    Friend WithEvents chkLogistic As System.Windows.Forms.CheckBox
    Friend WithEvents chkTechnolog As System.Windows.Forms.CheckBox
    Friend WithEvents chkProduction As System.Windows.Forms.CheckBox
    Friend WithEvents chkDirector As System.Windows.Forms.CheckBox
    Friend WithEvents cboTemplateBody As System.Windows.Forms.ComboBox
    Friend WithEvents cboEventType As System.Windows.Forms.ComboBox
    Friend WithEvents cboWorkstation As System.Windows.Forms.ComboBox
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents cboTemplateSubject As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtParameter As System.Windows.Forms.TextBox
    Friend WithEvents txtDeveloper As System.Windows.Forms.TextBox
    Friend WithEvents txtLogistic As System.Windows.Forms.TextBox
    Friend WithEvents txtProduction As System.Windows.Forms.TextBox
    Friend WithEvents txtDirector As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents nudDays As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkExcludeReclamation As System.Windows.Forms.CheckBox
    Friend WithEvents chkCheckDelivery As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtEmailManual As System.Windows.Forms.TextBox
    Friend WithEvents txtPartnerId As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmdDuplicate As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtMontageDays As System.Windows.Forms.TextBox
    Friend WithEvents chkCheckMontage As System.Windows.Forms.CheckBox
    Friend WithEvents cmdRunEvent As System.Windows.Forms.Button
    Friend WithEvents rtbBodySL As System.Windows.Forms.RichTextBox
    Friend WithEvents rtbZadeva As System.Windows.Forms.RichTextBox
End Class
