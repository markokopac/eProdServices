<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainForm
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
        Me.components = New System.ComponentModel.Container()
        Me.btnEProdName = New System.Windows.Forms.Button()
        Me.chkAutoStartName = New System.Windows.Forms.CheckBox()
        Me.chkAutoSendMailing = New System.Windows.Forms.CheckBox()
        Me.tmrUpdateName = New System.Windows.Forms.Timer(Me.components)
        Me.lblNextUpdateName = New System.Windows.Forms.Label()
        Me.txtLog = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PosodobiŠpicaEventeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdSendMail = New System.Windows.Forms.Button()
        Me.tmrSendMail = New System.Windows.Forms.Timer(Me.components)
        Me.lblNextUpdateMail = New System.Windows.Forms.Label()
        Me.chkAutoChangeDeliveryDate = New System.Windows.Forms.CheckBox()
        Me.cmdDeliveryDate = New System.Windows.Forms.Button()
        Me.lblNextUpdateDeliveryDate = New System.Windows.Forms.Label()
        Me.tmrDeliveryDate = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.rtbConvert = New System.Windows.Forms.RichTextBox()
        Me.chkAutoMAWIDates = New System.Windows.Forms.CheckBox()
        Me.lblNextUpdateMAWIDates = New System.Windows.Forms.Label()
        Me.cmdMAWIDates = New System.Windows.Forms.Button()
        Me.tmrMAWIDates = New System.Windows.Forms.Timer(Me.components)
        Me.lblNextUpdateCutterSendFile = New System.Windows.Forms.Label()
        Me.cmdCuttereProd = New System.Windows.Forms.Button()
        Me.chkAutoCutterSendFile = New System.Windows.Forms.CheckBox()
        Me.lblNextUpdateCutterArchive = New System.Windows.Forms.Label()
        Me.cmdCutterArchiveFiles = New System.Windows.Forms.Button()
        Me.chkAutoCheckCutterArchive = New System.Windows.Forms.CheckBox()
        Me.tmrCutterSendFile = New System.Windows.Forms.Timer(Me.components)
        Me.tmrCutterArchive = New System.Windows.Forms.Timer(Me.components)
        Me.lblNextSklic = New System.Windows.Forms.Label()
        Me.chkUpdateSklic = New System.Windows.Forms.CheckBox()
        Me.tmrSklic = New System.Windows.Forms.Timer(Me.components)
        Me.chkUpdateMonterOsn = New System.Windows.Forms.CheckBox()
        Me.lblNextMonter = New System.Windows.Forms.Label()
        Me.lblNextSlikaVrtanja = New System.Windows.Forms.Label()
        Me.chkEProdSlikaVrtanja = New System.Windows.Forms.CheckBox()
        Me.tmrMonter = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSlikaVrtanja = New System.Windows.Forms.Timer(Me.components)
        Me.lblStart = New System.Windows.Forms.Label()
        Me.chkKapaLog = New System.Windows.Forms.CheckBox()
        Me.lblNextKapaLog = New System.Windows.Forms.Label()
        Me.tmrKapaLog = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSpica = New System.Windows.Forms.Timer(Me.components)
        Me.chkSpicaEventUpdate = New System.Windows.Forms.CheckBox()
        Me.lblSpicaMinutes = New System.Windows.Forms.Label()
        Me.chkLockTechnicalOrders = New System.Windows.Forms.CheckBox()
        Me.lblLockTechnicalOrders = New System.Windows.Forms.Label()
        Me.tmrLockTechnicalOrders = New System.Windows.Forms.Timer(Me.components)
        Me.cmdFirebase = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.lblStatusNarocila = New System.Windows.Forms.Label()
        Me.chkStatusNarocila = New System.Windows.Forms.CheckBox()
        Me.tmrStatusNarocila = New System.Windows.Forms.Timer(Me.components)
        Me.cmdImportTexts = New System.Windows.Forms.Button()
        Me.cmdExportTexts = New System.Windows.Forms.Button()
        Me.btnStatusNarocila = New System.Windows.Forms.Button()
        Me.btnLockTechnicalOrders = New System.Windows.Forms.Button()
        Me.cmdUpdateSpicaEvents = New System.Windows.Forms.Button()
        Me.btnManualKapaLog = New System.Windows.Forms.Button()
        Me.cmdManualSlikaVrtanja = New System.Windows.Forms.Button()
        Me.cmdManualUpdateMonter = New System.Windows.Forms.Button()
        Me.cmdManualUpdateSklic = New System.Windows.Forms.Button()
        Me.cmdManualProcessDeleteCutterFiles = New System.Windows.Forms.Button()
        Me.cmdManualProcessCutter = New System.Windows.Forms.Button()
        Me.cmdManualProcessMAWIDates = New System.Windows.Forms.Button()
        Me.cmdManualProcesDeliveryDate = New System.Windows.Forms.Button()
        Me.cmdManualProcesSendMail = New System.Windows.Forms.Button()
        Me.cmdManualStartUpdateName = New System.Windows.Forms.Button()
        Me.cmdEvents = New System.Windows.Forms.Button()
        Me.cmdRtfEdit = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnEProdName
        '
        Me.btnEProdName.Location = New System.Drawing.Point(325, 4)
        Me.btnEProdName.Name = "btnEProdName"
        Me.btnEProdName.Size = New System.Drawing.Size(283, 32)
        Me.btnEProdName.TabIndex = 0
        Me.btnEProdName.Text = "Ročna posodobitev imena stranke"
        Me.btnEProdName.UseVisualStyleBackColor = True
        '
        'chkAutoStartName
        '
        Me.chkAutoStartName.AutoSize = True
        Me.chkAutoStartName.Location = New System.Drawing.Point(12, 16)
        Me.chkAutoStartName.Name = "chkAutoStartName"
        Me.chkAutoStartName.Size = New System.Drawing.Size(208, 24)
        Me.chkAutoStartName.TabIndex = 1
        Me.chkAutoStartName.Text = "Zamenjaj imena, datum "
        Me.chkAutoStartName.UseVisualStyleBackColor = True
        '
        'chkAutoSendMailing
        '
        Me.chkAutoSendMailing.AutoSize = True
        Me.chkAutoSendMailing.Location = New System.Drawing.Point(12, 54)
        Me.chkAutoSendMailing.Name = "chkAutoSendMailing"
        Me.chkAutoSendMailing.Size = New System.Drawing.Size(311, 24)
        Me.chkAutoSendMailing.TabIndex = 2
        Me.chkAutoSendMailing.Text = "Avtomatsko pošiljaj elektronsko pošto"
        Me.chkAutoSendMailing.UseVisualStyleBackColor = True
        '
        'tmrUpdateName
        '
        Me.tmrUpdateName.Enabled = True
        Me.tmrUpdateName.Interval = 60000
        '
        'lblNextUpdateName
        '
        Me.lblNextUpdateName.AutoSize = True
        Me.lblNextUpdateName.ForeColor = System.Drawing.Color.Red
        Me.lblNextUpdateName.Location = New System.Drawing.Point(631, 14)
        Me.lblNextUpdateName.Name = "lblNextUpdateName"
        Me.lblNextUpdateName.Size = New System.Drawing.Size(59, 20)
        Me.lblNextUpdateName.TabIndex = 3
        Me.lblNextUpdateName.Text = "Label1"
        '
        'txtLog
        '
        Me.txtLog.ContextMenuStrip = Me.ContextMenuStrip1
        Me.txtLog.Location = New System.Drawing.Point(12, 511)
        Me.txtLog.Name = "txtLog"
        Me.txtLog.Size = New System.Drawing.Size(689, 164)
        Me.txtLog.TabIndex = 5
        Me.txtLog.Text = ""
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PosodobiŠpicaEventeToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(229, 28)
        '
        'PosodobiŠpicaEventeToolStripMenuItem
        '
        Me.PosodobiŠpicaEventeToolStripMenuItem.Name = "PosodobiŠpicaEventeToolStripMenuItem"
        Me.PosodobiŠpicaEventeToolStripMenuItem.Size = New System.Drawing.Size(228, 24)
        Me.PosodobiŠpicaEventeToolStripMenuItem.Text = "Posodobi Špica evente"
        '
        'cmdSendMail
        '
        Me.cmdSendMail.Location = New System.Drawing.Point(325, 42)
        Me.cmdSendMail.Name = "cmdSendMail"
        Me.cmdSendMail.Size = New System.Drawing.Size(282, 32)
        Me.cmdSendMail.TabIndex = 6
        Me.cmdSendMail.Text = "Ročno pošiljanje sporočila"
        Me.cmdSendMail.UseVisualStyleBackColor = True
        '
        'tmrSendMail
        '
        Me.tmrSendMail.Enabled = True
        Me.tmrSendMail.Interval = 60000
        '
        'lblNextUpdateMail
        '
        Me.lblNextUpdateMail.AutoSize = True
        Me.lblNextUpdateMail.ForeColor = System.Drawing.Color.Red
        Me.lblNextUpdateMail.Location = New System.Drawing.Point(631, 50)
        Me.lblNextUpdateMail.Name = "lblNextUpdateMail"
        Me.lblNextUpdateMail.Size = New System.Drawing.Size(59, 20)
        Me.lblNextUpdateMail.TabIndex = 26
        Me.lblNextUpdateMail.Text = "Label1"
        '
        'chkAutoChangeDeliveryDate
        '
        Me.chkAutoChangeDeliveryDate.AutoSize = True
        Me.chkAutoChangeDeliveryDate.Location = New System.Drawing.Point(12, 93)
        Me.chkAutoChangeDeliveryDate.Name = "chkAutoChangeDeliveryDate"
        Me.chkAutoChangeDeliveryDate.Size = New System.Drawing.Size(313, 24)
        Me.chkAutoChangeDeliveryDate.TabIndex = 27
        Me.chkAutoChangeDeliveryDate.Text = "Avtomatsko posodobi termin končanja"
        Me.chkAutoChangeDeliveryDate.UseVisualStyleBackColor = True
        '
        'cmdDeliveryDate
        '
        Me.cmdDeliveryDate.Location = New System.Drawing.Point(325, 81)
        Me.cmdDeliveryDate.Name = "cmdDeliveryDate"
        Me.cmdDeliveryDate.Size = New System.Drawing.Size(282, 32)
        Me.cmdDeliveryDate.TabIndex = 29
        Me.cmdDeliveryDate.Text = "Ročna posodobitev termina dobave"
        Me.cmdDeliveryDate.UseVisualStyleBackColor = True
        '
        'lblNextUpdateDeliveryDate
        '
        Me.lblNextUpdateDeliveryDate.AutoSize = True
        Me.lblNextUpdateDeliveryDate.ForeColor = System.Drawing.Color.Red
        Me.lblNextUpdateDeliveryDate.Location = New System.Drawing.Point(631, 89)
        Me.lblNextUpdateDeliveryDate.Name = "lblNextUpdateDeliveryDate"
        Me.lblNextUpdateDeliveryDate.Size = New System.Drawing.Size(59, 20)
        Me.lblNextUpdateDeliveryDate.TabIndex = 30
        Me.lblNextUpdateDeliveryDate.Text = "Label1"
        '
        'tmrDeliveryDate
        '
        Me.tmrDeliveryDate.Enabled = True
        Me.tmrDeliveryDate.Interval = 60000
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'rtbConvert
        '
        Me.rtbConvert.Location = New System.Drawing.Point(351, 684)
        Me.rtbConvert.Name = "rtbConvert"
        Me.rtbConvert.Size = New System.Drawing.Size(102, 39)
        Me.rtbConvert.TabIndex = 31
        Me.rtbConvert.Text = ""
        Me.rtbConvert.Visible = False
        '
        'chkAutoMAWIDates
        '
        Me.chkAutoMAWIDates.AutoSize = True
        Me.chkAutoMAWIDates.Location = New System.Drawing.Point(12, 131)
        Me.chkAutoMAWIDates.Name = "chkAutoMAWIDates"
        Me.chkAutoMAWIDates.Size = New System.Drawing.Size(300, 24)
        Me.chkAutoMAWIDates.TabIndex = 32
        Me.chkAutoMAWIDates.Text = "Avtomatsko posodobi MAWI datume"
        Me.chkAutoMAWIDates.UseVisualStyleBackColor = True
        '
        'lblNextUpdateMAWIDates
        '
        Me.lblNextUpdateMAWIDates.AutoSize = True
        Me.lblNextUpdateMAWIDates.ForeColor = System.Drawing.Color.Red
        Me.lblNextUpdateMAWIDates.Location = New System.Drawing.Point(631, 127)
        Me.lblNextUpdateMAWIDates.Name = "lblNextUpdateMAWIDates"
        Me.lblNextUpdateMAWIDates.Size = New System.Drawing.Size(59, 20)
        Me.lblNextUpdateMAWIDates.TabIndex = 35
        Me.lblNextUpdateMAWIDates.Text = "Label1"
        '
        'cmdMAWIDates
        '
        Me.cmdMAWIDates.Location = New System.Drawing.Point(325, 119)
        Me.cmdMAWIDates.Name = "cmdMAWIDates"
        Me.cmdMAWIDates.Size = New System.Drawing.Size(282, 32)
        Me.cmdMAWIDates.TabIndex = 34
        Me.cmdMAWIDates.Text = "Ročna posodobitev MAWI datumov"
        Me.cmdMAWIDates.UseVisualStyleBackColor = True
        '
        'tmrMAWIDates
        '
        Me.tmrMAWIDates.Enabled = True
        Me.tmrMAWIDates.Interval = 60000
        '
        'lblNextUpdateCutterSendFile
        '
        Me.lblNextUpdateCutterSendFile.AutoSize = True
        Me.lblNextUpdateCutterSendFile.ForeColor = System.Drawing.Color.Red
        Me.lblNextUpdateCutterSendFile.Location = New System.Drawing.Point(632, 165)
        Me.lblNextUpdateCutterSendFile.Name = "lblNextUpdateCutterSendFile"
        Me.lblNextUpdateCutterSendFile.Size = New System.Drawing.Size(59, 20)
        Me.lblNextUpdateCutterSendFile.TabIndex = 39
        Me.lblNextUpdateCutterSendFile.Text = "Label1"
        '
        'cmdCuttereProd
        '
        Me.cmdCuttereProd.Location = New System.Drawing.Point(326, 157)
        Me.cmdCuttereProd.Name = "cmdCuttereProd"
        Me.cmdCuttereProd.Size = New System.Drawing.Size(282, 32)
        Me.cmdCuttereProd.TabIndex = 38
        Me.cmdCuttereProd.Text = "Pošiljanje datotek na rezalnik"
        Me.cmdCuttereProd.UseVisualStyleBackColor = True
        '
        'chkAutoCutterSendFile
        '
        Me.chkAutoCutterSendFile.AutoSize = True
        Me.chkAutoCutterSendFile.Location = New System.Drawing.Point(13, 169)
        Me.chkAutoCutterSendFile.Name = "chkAutoCutterSendFile"
        Me.chkAutoCutterSendFile.Size = New System.Drawing.Size(226, 24)
        Me.chkAutoCutterSendFile.TabIndex = 36
        Me.chkAutoCutterSendFile.Text = "Pošlji datoteke na rezalnik"
        Me.chkAutoCutterSendFile.UseVisualStyleBackColor = True
        '
        'lblNextUpdateCutterArchive
        '
        Me.lblNextUpdateCutterArchive.AutoSize = True
        Me.lblNextUpdateCutterArchive.ForeColor = System.Drawing.Color.Red
        Me.lblNextUpdateCutterArchive.Location = New System.Drawing.Point(632, 203)
        Me.lblNextUpdateCutterArchive.Name = "lblNextUpdateCutterArchive"
        Me.lblNextUpdateCutterArchive.Size = New System.Drawing.Size(59, 20)
        Me.lblNextUpdateCutterArchive.TabIndex = 43
        Me.lblNextUpdateCutterArchive.Text = "Label1"
        '
        'cmdCutterArchiveFiles
        '
        Me.cmdCutterArchiveFiles.Location = New System.Drawing.Point(326, 195)
        Me.cmdCutterArchiveFiles.Name = "cmdCutterArchiveFiles"
        Me.cmdCutterArchiveFiles.Size = New System.Drawing.Size(282, 32)
        Me.cmdCutterArchiveFiles.TabIndex = 42
        Me.cmdCutterArchiveFiles.Text = "Arhiviranje datotek na rezalniku"
        Me.cmdCutterArchiveFiles.UseVisualStyleBackColor = True
        '
        'chkAutoCheckCutterArchive
        '
        Me.chkAutoCheckCutterArchive.AutoSize = True
        Me.chkAutoCheckCutterArchive.Location = New System.Drawing.Point(13, 207)
        Me.chkAutoCheckCutterArchive.Name = "chkAutoCheckCutterArchive"
        Me.chkAutoCheckCutterArchive.Size = New System.Drawing.Size(246, 24)
        Me.chkAutoCheckCutterArchive.TabIndex = 40
        Me.chkAutoCheckCutterArchive.Text = "Arhiviraj datoteke za rezalnik"
        Me.chkAutoCheckCutterArchive.UseVisualStyleBackColor = True
        '
        'tmrCutterSendFile
        '
        Me.tmrCutterSendFile.Enabled = True
        Me.tmrCutterSendFile.Interval = 60000
        '
        'tmrCutterArchive
        '
        Me.tmrCutterArchive.Enabled = True
        Me.tmrCutterArchive.Interval = 60000
        '
        'lblNextSklic
        '
        Me.lblNextSklic.AutoSize = True
        Me.lblNextSklic.ForeColor = System.Drawing.Color.Red
        Me.lblNextSklic.Location = New System.Drawing.Point(631, 242)
        Me.lblNextSklic.Name = "lblNextSklic"
        Me.lblNextSklic.Size = New System.Drawing.Size(59, 20)
        Me.lblNextSklic.TabIndex = 47
        Me.lblNextSklic.Text = "Label1"
        '
        'chkUpdateSklic
        '
        Me.chkUpdateSklic.AutoSize = True
        Me.chkUpdateSklic.Location = New System.Drawing.Point(12, 246)
        Me.chkUpdateSklic.Name = "chkUpdateSklic"
        Me.chkUpdateSklic.Size = New System.Drawing.Size(128, 24)
        Me.chkUpdateSklic.TabIndex = 44
        Me.chkUpdateSklic.Text = "Ažuriraj sklic"
        Me.chkUpdateSklic.UseVisualStyleBackColor = True
        '
        'tmrSklic
        '
        Me.tmrSklic.Enabled = True
        Me.tmrSklic.Interval = 60000
        '
        'chkUpdateMonterOsn
        '
        Me.chkUpdateMonterOsn.AutoSize = True
        Me.chkUpdateMonterOsn.Location = New System.Drawing.Point(12, 281)
        Me.chkUpdateMonterOsn.Name = "chkUpdateMonterOsn"
        Me.chkUpdateMonterOsn.Size = New System.Drawing.Size(261, 24)
        Me.chkUpdateMonterOsn.TabIndex = 48
        Me.chkUpdateMonterOsn.Text = "Monter osn. naloga, R končano"
        Me.chkUpdateMonterOsn.UseVisualStyleBackColor = True
        '
        'lblNextMonter
        '
        Me.lblNextMonter.AutoSize = True
        Me.lblNextMonter.ForeColor = System.Drawing.Color.Red
        Me.lblNextMonter.Location = New System.Drawing.Point(631, 281)
        Me.lblNextMonter.Name = "lblNextMonter"
        Me.lblNextMonter.Size = New System.Drawing.Size(59, 20)
        Me.lblNextMonter.TabIndex = 50
        Me.lblNextMonter.Text = "Label1"
        '
        'lblNextSlikaVrtanja
        '
        Me.lblNextSlikaVrtanja.AutoSize = True
        Me.lblNextSlikaVrtanja.ForeColor = System.Drawing.Color.Red
        Me.lblNextSlikaVrtanja.Location = New System.Drawing.Point(631, 320)
        Me.lblNextSlikaVrtanja.Name = "lblNextSlikaVrtanja"
        Me.lblNextSlikaVrtanja.Size = New System.Drawing.Size(59, 20)
        Me.lblNextSlikaVrtanja.TabIndex = 53
        Me.lblNextSlikaVrtanja.Text = "Label1"
        '
        'chkEProdSlikaVrtanja
        '
        Me.chkEProdSlikaVrtanja.AutoSize = True
        Me.chkEProdSlikaVrtanja.Location = New System.Drawing.Point(12, 320)
        Me.chkEProdSlikaVrtanja.Name = "chkEProdSlikaVrtanja"
        Me.chkEProdSlikaVrtanja.Size = New System.Drawing.Size(230, 24)
        Me.chkEProdSlikaVrtanja.TabIndex = 51
        Me.chkEProdSlikaVrtanja.Text = "Slika vrtanja, profil izvedba"
        Me.chkEProdSlikaVrtanja.UseVisualStyleBackColor = True
        '
        'tmrMonter
        '
        Me.tmrMonter.Enabled = True
        Me.tmrMonter.Interval = 60000
        '
        'tmrSlikaVrtanja
        '
        Me.tmrSlikaVrtanja.Enabled = True
        Me.tmrSlikaVrtanja.Interval = 60000
        '
        'lblStart
        '
        Me.lblStart.AutoSize = True
        Me.lblStart.Location = New System.Drawing.Point(495, 688)
        Me.lblStart.Name = "lblStart"
        Me.lblStart.Size = New System.Drawing.Size(0, 20)
        Me.lblStart.TabIndex = 54
        '
        'chkKapaLog
        '
        Me.chkKapaLog.AutoSize = True
        Me.chkKapaLog.Location = New System.Drawing.Point(12, 357)
        Me.chkKapaLog.Name = "chkKapaLog"
        Me.chkKapaLog.Size = New System.Drawing.Size(251, 24)
        Me.chkKapaLog.TabIndex = 55
        Me.chkKapaLog.Text = "Kapa logiranje, uvoz naslovov"
        Me.chkKapaLog.UseVisualStyleBackColor = True
        '
        'lblNextKapaLog
        '
        Me.lblNextKapaLog.AutoSize = True
        Me.lblNextKapaLog.ForeColor = System.Drawing.Color.Red
        Me.lblNextKapaLog.Location = New System.Drawing.Point(631, 358)
        Me.lblNextKapaLog.Name = "lblNextKapaLog"
        Me.lblNextKapaLog.Size = New System.Drawing.Size(59, 20)
        Me.lblNextKapaLog.TabIndex = 57
        Me.lblNextKapaLog.Text = "Label1"
        '
        'tmrKapaLog
        '
        Me.tmrKapaLog.Enabled = True
        Me.tmrKapaLog.Interval = 60000
        '
        'tmrSpica
        '
        Me.tmrSpica.Enabled = True
        Me.tmrSpica.Interval = 60000
        '
        'chkSpicaEventUpdate
        '
        Me.chkSpicaEventUpdate.AutoSize = True
        Me.chkSpicaEventUpdate.Location = New System.Drawing.Point(12, 396)
        Me.chkSpicaEventUpdate.Name = "chkSpicaEventUpdate"
        Me.chkSpicaEventUpdate.Size = New System.Drawing.Size(201, 24)
        Me.chkSpicaEventUpdate.TabIndex = 59
        Me.chkSpicaEventUpdate.Text = "Posodobi Špica evente"
        Me.chkSpicaEventUpdate.UseVisualStyleBackColor = True
        '
        'lblSpicaMinutes
        '
        Me.lblSpicaMinutes.AutoSize = True
        Me.lblSpicaMinutes.ForeColor = System.Drawing.Color.Red
        Me.lblSpicaMinutes.Location = New System.Drawing.Point(631, 391)
        Me.lblSpicaMinutes.Name = "lblSpicaMinutes"
        Me.lblSpicaMinutes.Size = New System.Drawing.Size(59, 20)
        Me.lblSpicaMinutes.TabIndex = 61
        Me.lblSpicaMinutes.Text = "Label1"
        '
        'chkLockTechnicalOrders
        '
        Me.chkLockTechnicalOrders.AutoSize = True
        Me.chkLockTechnicalOrders.Location = New System.Drawing.Point(12, 434)
        Me.chkLockTechnicalOrders.Name = "chkLockTechnicalOrders"
        Me.chkLockTechnicalOrders.Size = New System.Drawing.Size(277, 24)
        Me.chkLockTechnicalOrders.TabIndex = 62
        Me.chkLockTechnicalOrders.Text = "Zakleni tehnično obdelane naloge"
        Me.chkLockTechnicalOrders.UseVisualStyleBackColor = True
        '
        'lblLockTechnicalOrders
        '
        Me.lblLockTechnicalOrders.AutoSize = True
        Me.lblLockTechnicalOrders.ForeColor = System.Drawing.Color.Red
        Me.lblLockTechnicalOrders.Location = New System.Drawing.Point(631, 429)
        Me.lblLockTechnicalOrders.Name = "lblLockTechnicalOrders"
        Me.lblLockTechnicalOrders.Size = New System.Drawing.Size(59, 20)
        Me.lblLockTechnicalOrders.TabIndex = 64
        Me.lblLockTechnicalOrders.Text = "Label1"
        '
        'tmrLockTechnicalOrders
        '
        Me.tmrLockTechnicalOrders.Enabled = True
        Me.tmrLockTechnicalOrders.Interval = 60000
        '
        'cmdFirebase
        '
        Me.cmdFirebase.Location = New System.Drawing.Point(571, 444)
        Me.cmdFirebase.Name = "cmdFirebase"
        Me.cmdFirebase.Size = New System.Drawing.Size(53, 46)
        Me.cmdFirebase.TabIndex = 65
        Me.cmdFirebase.Text = "Test"
        Me.cmdFirebase.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(487, 705)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(87, 26)
        Me.TextBox1.TabIndex = 66
        '
        'lblStatusNarocila
        '
        Me.lblStatusNarocila.AutoSize = True
        Me.lblStatusNarocila.ForeColor = System.Drawing.Color.Red
        Me.lblStatusNarocila.Location = New System.Drawing.Point(630, 464)
        Me.lblStatusNarocila.Name = "lblStatusNarocila"
        Me.lblStatusNarocila.Size = New System.Drawing.Size(59, 20)
        Me.lblStatusNarocila.TabIndex = 69
        Me.lblStatusNarocila.Text = "Label1"
        '
        'chkStatusNarocila
        '
        Me.chkStatusNarocila.AutoSize = True
        Me.chkStatusNarocila.Location = New System.Drawing.Point(11, 469)
        Me.chkStatusNarocila.Name = "chkStatusNarocila"
        Me.chkStatusNarocila.Size = New System.Drawing.Size(281, 24)
        Me.chkStatusNarocila.TabIndex = 67
        Me.chkStatusNarocila.Text = "Posodobi MSORA status naročila"
        Me.chkStatusNarocila.UseVisualStyleBackColor = True
        '
        'tmrStatusNarocila
        '
        Me.tmrStatusNarocila.Enabled = True
        Me.tmrStatusNarocila.Interval = 60000
        '
        'cmdImportTexts
        '
        Me.cmdImportTexts.Image = Global.eProdService.My.Resources.Resources.import1
        Me.cmdImportTexts.Location = New System.Drawing.Point(398, 438)
        Me.cmdImportTexts.Name = "cmdImportTexts"
        Me.cmdImportTexts.Size = New System.Drawing.Size(55, 59)
        Me.cmdImportTexts.TabIndex = 71
        Me.cmdImportTexts.UseVisualStyleBackColor = True
        '
        'cmdExportTexts
        '
        Me.cmdExportTexts.Image = Global.eProdService.My.Resources.Resources.export
        Me.cmdExportTexts.Location = New System.Drawing.Point(332, 438)
        Me.cmdExportTexts.Name = "cmdExportTexts"
        Me.cmdExportTexts.Size = New System.Drawing.Size(55, 59)
        Me.cmdExportTexts.TabIndex = 70
        Me.cmdExportTexts.UseVisualStyleBackColor = True
        '
        'btnStatusNarocila
        '
        Me.btnStatusNarocila.Image = Global.eProdService.My.Resources.Resources.yes
        Me.btnStatusNarocila.Location = New System.Drawing.Point(288, 464)
        Me.btnStatusNarocila.Name = "btnStatusNarocila"
        Me.btnStatusNarocila.Size = New System.Drawing.Size(37, 33)
        Me.btnStatusNarocila.TabIndex = 68
        Me.btnStatusNarocila.UseVisualStyleBackColor = True
        '
        'btnLockTechnicalOrders
        '
        Me.btnLockTechnicalOrders.Image = Global.eProdService.My.Resources.Resources.yes
        Me.btnLockTechnicalOrders.Location = New System.Drawing.Point(289, 429)
        Me.btnLockTechnicalOrders.Name = "btnLockTechnicalOrders"
        Me.btnLockTechnicalOrders.Size = New System.Drawing.Size(37, 33)
        Me.btnLockTechnicalOrders.TabIndex = 63
        Me.btnLockTechnicalOrders.UseVisualStyleBackColor = True
        '
        'cmdUpdateSpicaEvents
        '
        Me.cmdUpdateSpicaEvents.Image = Global.eProdService.My.Resources.Resources.yes
        Me.cmdUpdateSpicaEvents.Location = New System.Drawing.Point(289, 391)
        Me.cmdUpdateSpicaEvents.Name = "cmdUpdateSpicaEvents"
        Me.cmdUpdateSpicaEvents.Size = New System.Drawing.Size(37, 33)
        Me.cmdUpdateSpicaEvents.TabIndex = 60
        Me.cmdUpdateSpicaEvents.UseVisualStyleBackColor = True
        '
        'btnManualKapaLog
        '
        Me.btnManualKapaLog.Image = Global.eProdService.My.Resources.Resources.yes
        Me.btnManualKapaLog.Location = New System.Drawing.Point(289, 352)
        Me.btnManualKapaLog.Name = "btnManualKapaLog"
        Me.btnManualKapaLog.Size = New System.Drawing.Size(37, 33)
        Me.btnManualKapaLog.TabIndex = 56
        Me.btnManualKapaLog.UseVisualStyleBackColor = True
        '
        'cmdManualSlikaVrtanja
        '
        Me.cmdManualSlikaVrtanja.Image = Global.eProdService.My.Resources.Resources.yes
        Me.cmdManualSlikaVrtanja.Location = New System.Drawing.Point(289, 311)
        Me.cmdManualSlikaVrtanja.Name = "cmdManualSlikaVrtanja"
        Me.cmdManualSlikaVrtanja.Size = New System.Drawing.Size(37, 33)
        Me.cmdManualSlikaVrtanja.TabIndex = 52
        Me.cmdManualSlikaVrtanja.UseVisualStyleBackColor = True
        '
        'cmdManualUpdateMonter
        '
        Me.cmdManualUpdateMonter.Image = Global.eProdService.My.Resources.Resources.yes
        Me.cmdManualUpdateMonter.Location = New System.Drawing.Point(289, 272)
        Me.cmdManualUpdateMonter.Name = "cmdManualUpdateMonter"
        Me.cmdManualUpdateMonter.Size = New System.Drawing.Size(37, 33)
        Me.cmdManualUpdateMonter.TabIndex = 49
        Me.cmdManualUpdateMonter.UseVisualStyleBackColor = True
        '
        'cmdManualUpdateSklic
        '
        Me.cmdManualUpdateSklic.Image = Global.eProdService.My.Resources.Resources.yes
        Me.cmdManualUpdateSklic.Location = New System.Drawing.Point(288, 233)
        Me.cmdManualUpdateSklic.Name = "cmdManualUpdateSklic"
        Me.cmdManualUpdateSklic.Size = New System.Drawing.Size(37, 33)
        Me.cmdManualUpdateSklic.TabIndex = 45
        Me.cmdManualUpdateSklic.UseVisualStyleBackColor = True
        '
        'cmdManualProcessDeleteCutterFiles
        '
        Me.cmdManualProcessDeleteCutterFiles.Image = Global.eProdService.My.Resources.Resources.yes
        Me.cmdManualProcessDeleteCutterFiles.Location = New System.Drawing.Point(289, 194)
        Me.cmdManualProcessDeleteCutterFiles.Name = "cmdManualProcessDeleteCutterFiles"
        Me.cmdManualProcessDeleteCutterFiles.Size = New System.Drawing.Size(37, 33)
        Me.cmdManualProcessDeleteCutterFiles.TabIndex = 41
        Me.cmdManualProcessDeleteCutterFiles.UseVisualStyleBackColor = True
        '
        'cmdManualProcessCutter
        '
        Me.cmdManualProcessCutter.Image = Global.eProdService.My.Resources.Resources.yes
        Me.cmdManualProcessCutter.Location = New System.Drawing.Point(289, 156)
        Me.cmdManualProcessCutter.Name = "cmdManualProcessCutter"
        Me.cmdManualProcessCutter.Size = New System.Drawing.Size(37, 33)
        Me.cmdManualProcessCutter.TabIndex = 37
        Me.cmdManualProcessCutter.UseVisualStyleBackColor = True
        '
        'cmdManualProcessMAWIDates
        '
        Me.cmdManualProcessMAWIDates.Image = Global.eProdService.My.Resources.Resources.yes
        Me.cmdManualProcessMAWIDates.Location = New System.Drawing.Point(288, 118)
        Me.cmdManualProcessMAWIDates.Name = "cmdManualProcessMAWIDates"
        Me.cmdManualProcessMAWIDates.Size = New System.Drawing.Size(37, 33)
        Me.cmdManualProcessMAWIDates.TabIndex = 33
        Me.cmdManualProcessMAWIDates.UseVisualStyleBackColor = True
        '
        'cmdManualProcesDeliveryDate
        '
        Me.cmdManualProcesDeliveryDate.Image = Global.eProdService.My.Resources.Resources.yes
        Me.cmdManualProcesDeliveryDate.Location = New System.Drawing.Point(288, 80)
        Me.cmdManualProcesDeliveryDate.Name = "cmdManualProcesDeliveryDate"
        Me.cmdManualProcesDeliveryDate.Size = New System.Drawing.Size(37, 33)
        Me.cmdManualProcesDeliveryDate.TabIndex = 28
        Me.cmdManualProcesDeliveryDate.UseVisualStyleBackColor = True
        '
        'cmdManualProcesSendMail
        '
        Me.cmdManualProcesSendMail.Image = Global.eProdService.My.Resources.Resources.yes
        Me.cmdManualProcesSendMail.Location = New System.Drawing.Point(288, 42)
        Me.cmdManualProcesSendMail.Name = "cmdManualProcesSendMail"
        Me.cmdManualProcesSendMail.Size = New System.Drawing.Size(37, 32)
        Me.cmdManualProcesSendMail.TabIndex = 25
        Me.cmdManualProcesSendMail.UseVisualStyleBackColor = True
        '
        'cmdManualStartUpdateName
        '
        Me.cmdManualStartUpdateName.Image = Global.eProdService.My.Resources.Resources.yes
        Me.cmdManualStartUpdateName.Location = New System.Drawing.Point(288, 4)
        Me.cmdManualStartUpdateName.Name = "cmdManualStartUpdateName"
        Me.cmdManualStartUpdateName.Size = New System.Drawing.Size(37, 32)
        Me.cmdManualStartUpdateName.TabIndex = 24
        Me.cmdManualStartUpdateName.UseVisualStyleBackColor = True
        '
        'cmdEvents
        '
        Me.cmdEvents.Image = Global.eProdService.My.Resources.Resources.events
        Me.cmdEvents.Location = New System.Drawing.Point(82, 672)
        Me.cmdEvents.Name = "cmdEvents"
        Me.cmdEvents.Size = New System.Drawing.Size(64, 60)
        Me.cmdEvents.TabIndex = 23
        Me.cmdEvents.UseVisualStyleBackColor = True
        '
        'cmdRtfEdit
        '
        Me.cmdRtfEdit.Image = Global.eProdService.My.Resources.Resources.rtf
        Me.cmdRtfEdit.Location = New System.Drawing.Point(12, 672)
        Me.cmdRtfEdit.Name = "cmdRtfEdit"
        Me.cmdRtfEdit.Size = New System.Drawing.Size(64, 60)
        Me.cmdRtfEdit.TabIndex = 22
        Me.cmdRtfEdit.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(465, 437)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(94, 59)
        Me.Button1.TabIndex = 72
        Me.Button1.Text = "Refresh Tracking"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 740)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdImportTexts)
        Me.Controls.Add(Me.cmdExportTexts)
        Me.Controls.Add(Me.lblStatusNarocila)
        Me.Controls.Add(Me.btnStatusNarocila)
        Me.Controls.Add(Me.chkStatusNarocila)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.cmdFirebase)
        Me.Controls.Add(Me.lblLockTechnicalOrders)
        Me.Controls.Add(Me.btnLockTechnicalOrders)
        Me.Controls.Add(Me.chkLockTechnicalOrders)
        Me.Controls.Add(Me.lblSpicaMinutes)
        Me.Controls.Add(Me.cmdUpdateSpicaEvents)
        Me.Controls.Add(Me.chkSpicaEventUpdate)
        Me.Controls.Add(Me.lblNextKapaLog)
        Me.Controls.Add(Me.btnManualKapaLog)
        Me.Controls.Add(Me.chkKapaLog)
        Me.Controls.Add(Me.lblStart)
        Me.Controls.Add(Me.lblNextSlikaVrtanja)
        Me.Controls.Add(Me.cmdManualSlikaVrtanja)
        Me.Controls.Add(Me.chkEProdSlikaVrtanja)
        Me.Controls.Add(Me.lblNextMonter)
        Me.Controls.Add(Me.cmdManualUpdateMonter)
        Me.Controls.Add(Me.chkUpdateMonterOsn)
        Me.Controls.Add(Me.lblNextSklic)
        Me.Controls.Add(Me.cmdManualUpdateSklic)
        Me.Controls.Add(Me.chkUpdateSklic)
        Me.Controls.Add(Me.lblNextUpdateCutterArchive)
        Me.Controls.Add(Me.cmdCutterArchiveFiles)
        Me.Controls.Add(Me.cmdManualProcessDeleteCutterFiles)
        Me.Controls.Add(Me.chkAutoCheckCutterArchive)
        Me.Controls.Add(Me.lblNextUpdateCutterSendFile)
        Me.Controls.Add(Me.cmdCuttereProd)
        Me.Controls.Add(Me.cmdManualProcessCutter)
        Me.Controls.Add(Me.chkAutoCutterSendFile)
        Me.Controls.Add(Me.lblNextUpdateMAWIDates)
        Me.Controls.Add(Me.cmdMAWIDates)
        Me.Controls.Add(Me.cmdManualProcessMAWIDates)
        Me.Controls.Add(Me.chkAutoMAWIDates)
        Me.Controls.Add(Me.rtbConvert)
        Me.Controls.Add(Me.lblNextUpdateDeliveryDate)
        Me.Controls.Add(Me.cmdDeliveryDate)
        Me.Controls.Add(Me.cmdManualProcesDeliveryDate)
        Me.Controls.Add(Me.chkAutoChangeDeliveryDate)
        Me.Controls.Add(Me.lblNextUpdateMail)
        Me.Controls.Add(Me.cmdManualProcesSendMail)
        Me.Controls.Add(Me.cmdManualStartUpdateName)
        Me.Controls.Add(Me.cmdEvents)
        Me.Controls.Add(Me.cmdRtfEdit)
        Me.Controls.Add(Me.cmdSendMail)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.lblNextUpdateName)
        Me.Controls.Add(Me.chkAutoSendMailing)
        Me.Controls.Add(Me.chkAutoStartName)
        Me.Controls.Add(Me.btnEProdName)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmMainForm"
        Me.Text = "eProd Servis"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnEProdName As System.Windows.Forms.Button
    Friend WithEvents chkAutoStartName As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutoSendMailing As System.Windows.Forms.CheckBox
    Friend WithEvents tmrUpdateName As System.Windows.Forms.Timer
    Friend WithEvents lblNextUpdateName As System.Windows.Forms.Label
    Friend WithEvents txtLog As System.Windows.Forms.RichTextBox
    Friend WithEvents cmdSendMail As System.Windows.Forms.Button
    Friend WithEvents cmdRtfEdit As System.Windows.Forms.Button
    Friend WithEvents cmdEvents As System.Windows.Forms.Button
    Friend WithEvents cmdManualStartUpdateName As System.Windows.Forms.Button
    Friend WithEvents cmdManualProcesSendMail As System.Windows.Forms.Button
    Friend WithEvents tmrSendMail As System.Windows.Forms.Timer
    Friend WithEvents lblNextUpdateMail As System.Windows.Forms.Label
    Friend WithEvents chkAutoChangeDeliveryDate As System.Windows.Forms.CheckBox
    Friend WithEvents cmdManualProcesDeliveryDate As System.Windows.Forms.Button
    Friend WithEvents cmdDeliveryDate As System.Windows.Forms.Button
    Friend WithEvents lblNextUpdateDeliveryDate As System.Windows.Forms.Label
    Friend WithEvents tmrDeliveryDate As System.Windows.Forms.Timer
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents rtbConvert As System.Windows.Forms.RichTextBox
    Friend WithEvents chkAutoMAWIDates As System.Windows.Forms.CheckBox
    Friend WithEvents lblNextUpdateMAWIDates As System.Windows.Forms.Label
    Friend WithEvents cmdMAWIDates As System.Windows.Forms.Button
    Friend WithEvents cmdManualProcessMAWIDates As System.Windows.Forms.Button
    Friend WithEvents tmrMAWIDates As System.Windows.Forms.Timer
    Friend WithEvents lblNextUpdateCutterSendFile As System.Windows.Forms.Label
    Friend WithEvents cmdCuttereProd As System.Windows.Forms.Button
    Friend WithEvents cmdManualProcessCutter As System.Windows.Forms.Button
    Friend WithEvents chkAutoCutterSendFile As System.Windows.Forms.CheckBox
    Friend WithEvents lblNextUpdateCutterArchive As System.Windows.Forms.Label
    Friend WithEvents cmdCutterArchiveFiles As System.Windows.Forms.Button
    Friend WithEvents cmdManualProcessDeleteCutterFiles As System.Windows.Forms.Button
    Friend WithEvents chkAutoCheckCutterArchive As System.Windows.Forms.CheckBox
    Friend WithEvents tmrCutterSendFile As System.Windows.Forms.Timer
    Friend WithEvents tmrCutterArchive As System.Windows.Forms.Timer
    Friend WithEvents lblNextSklic As System.Windows.Forms.Label
    Friend WithEvents cmdManualUpdateSklic As System.Windows.Forms.Button
    Friend WithEvents chkUpdateSklic As System.Windows.Forms.CheckBox
    Friend WithEvents tmrSklic As System.Windows.Forms.Timer
    Friend WithEvents chkUpdateMonterOsn As System.Windows.Forms.CheckBox
    Friend WithEvents cmdManualUpdateMonter As System.Windows.Forms.Button
    Friend WithEvents lblNextMonter As System.Windows.Forms.Label
    Friend WithEvents lblNextSlikaVrtanja As System.Windows.Forms.Label
    Friend WithEvents cmdManualSlikaVrtanja As System.Windows.Forms.Button
    Friend WithEvents chkEProdSlikaVrtanja As System.Windows.Forms.CheckBox
    Friend WithEvents tmrMonter As System.Windows.Forms.Timer
    Friend WithEvents tmrSlikaVrtanja As System.Windows.Forms.Timer
    Friend WithEvents lblStart As System.Windows.Forms.Label
    Friend WithEvents chkKapaLog As System.Windows.Forms.CheckBox
    Friend WithEvents btnManualKapaLog As System.Windows.Forms.Button
    Friend WithEvents lblNextKapaLog As System.Windows.Forms.Label
    Friend WithEvents tmrKapaLog As System.Windows.Forms.Timer
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PosodobiŠpicaEventeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrSpica As System.Windows.Forms.Timer
    Friend WithEvents chkSpicaEventUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents cmdUpdateSpicaEvents As System.Windows.Forms.Button
    Friend WithEvents lblSpicaMinutes As System.Windows.Forms.Label
    Friend WithEvents chkLockTechnicalOrders As System.Windows.Forms.CheckBox
    Friend WithEvents btnLockTechnicalOrders As System.Windows.Forms.Button
    Friend WithEvents lblLockTechnicalOrders As System.Windows.Forms.Label
    Friend WithEvents tmrLockTechnicalOrders As System.Windows.Forms.Timer
    Friend WithEvents cmdFirebase As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents lblStatusNarocila As Label
    Friend WithEvents btnStatusNarocila As Button
    Friend WithEvents chkStatusNarocila As CheckBox
    Friend WithEvents tmrStatusNarocila As Timer
    Friend WithEvents cmdExportTexts As Button
    Friend WithEvents cmdImportTexts As Button
    Friend WithEvents Button1 As Button
End Class
