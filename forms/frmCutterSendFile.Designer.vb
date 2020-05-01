<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCutterSendFile
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
        Me.txtNalog = New System.Windows.Forms.TextBox()
        Me.txtNarocilo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgEprod = New System.Windows.Forms.DataGridView()
        Me.kommissionsnummer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.auftragsnummer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.filename = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.optStartedKommissions = New System.Windows.Forms.RadioButton()
        Me.optFinnishedKommissions = New System.Windows.Forms.RadioButton()
        Me.optAllKommissions = New System.Windows.Forms.RadioButton()
        Me.btnMove = New System.Windows.Forms.Button()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.dgEprod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.optAllKommissions)
        Me.SplitContainer1.Panel1.Controls.Add(Me.optFinnishedKommissions)
        Me.SplitContainer1.Panel1.Controls.Add(Me.optStartedKommissions)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNalog)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtNarocilo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdSearch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1003, 790)
        Me.SplitContainer1.SplitterDistance = 270
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.dgEprod)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnMove)
        Me.SplitContainer2.Size = New System.Drawing.Size(728, 790)
        Me.SplitContainer2.SplitterDistance = 638
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 0
        '
        'txtNalog
        '
        Me.txtNalog.Location = New System.Drawing.Point(106, 179)
        Me.txtNalog.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtNalog.Name = "txtNalog"
        Me.txtNalog.Size = New System.Drawing.Size(131, 22)
        Me.txtNalog.TabIndex = 22
        '
        'txtNarocilo
        '
        Me.txtNarocilo.Location = New System.Drawing.Point(106, 138)
        Me.txtNarocilo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtNarocilo.Name = "txtNarocilo"
        Me.txtNarocilo.Size = New System.Drawing.Size(131, 22)
        Me.txtNarocilo.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 187)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 16)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Nalog:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 145)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 16)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Naročilo:"
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(52, 343)
        Me.cmdSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(159, 54)
        Me.cmdSearch.TabIndex = 18
        Me.cmdSearch.Text = "Išči"
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'dtpTo
        '
        Me.dtpTo.Checked = False
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(95, 92)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(141, 22)
        Me.dtpTo.TabIndex = 17
        '
        'dtpFrom
        '
        Me.dtpFrom.Checked = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(95, 58)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(141, 22)
        Me.dtpFrom.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 99)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 16)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Do"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 61)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 16)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Od"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 24)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(173, 16)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Datum prvega bukiranja"
        '
        'dgEprod
        '
        Me.dgEprod.AllowUserToAddRows = False
        Me.dgEprod.AllowUserToDeleteRows = False
        Me.dgEprod.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgEprod.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.kommissionsnummer, Me.auftragsnummer, Me.filename})
        Me.dgEprod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgEprod.Location = New System.Drawing.Point(0, 0)
        Me.dgEprod.Name = "dgEprod"
        Me.dgEprod.Size = New System.Drawing.Size(638, 790)
        Me.dgEprod.TabIndex = 0
        '
        'kommissionsnummer
        '
        Me.kommissionsnummer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.kommissionsnummer.DataPropertyName = "kommissionsnummer"
        Me.kommissionsnummer.HeaderText = "Nalog"
        Me.kommissionsnummer.Name = "kommissionsnummer"
        Me.kommissionsnummer.ReadOnly = True
        Me.kommissionsnummer.Width = 70
        '
        'auftragsnummer
        '
        Me.auftragsnummer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.auftragsnummer.DataPropertyName = "auftragsnummer"
        Me.auftragsnummer.HeaderText = "Naročilo"
        Me.auftragsnummer.Name = "auftragsnummer"
        Me.auftragsnummer.ReadOnly = True
        Me.auftragsnummer.Width = 84
        '
        'filename
        '
        Me.filename.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.filename.DataPropertyName = "filename"
        Me.filename.HeaderText = "Datoteka"
        Me.filename.Name = "filename"
        Me.filename.ReadOnly = True
        Me.filename.Width = 88
        '
        'optStartedKommissions
        '
        Me.optStartedKommissions.AutoSize = True
        Me.optStartedKommissions.Checked = True
        Me.optStartedKommissions.Location = New System.Drawing.Point(25, 223)
        Me.optStartedKommissions.Name = "optStartedKommissions"
        Me.optStartedKommissions.Size = New System.Drawing.Size(103, 20)
        Me.optStartedKommissions.TabIndex = 23
        Me.optStartedKommissions.TabStop = True
        Me.optStartedKommissions.Text = "Začeti nalogi"
        Me.optStartedKommissions.UseVisualStyleBackColor = True
        '
        'optFinnishedKommissions
        '
        Me.optFinnishedKommissions.AutoSize = True
        Me.optFinnishedKommissions.Location = New System.Drawing.Point(25, 249)
        Me.optFinnishedKommissions.Name = "optFinnishedKommissions"
        Me.optFinnishedKommissions.Size = New System.Drawing.Size(114, 20)
        Me.optFinnishedKommissions.TabIndex = 24
        Me.optFinnishedKommissions.Text = "Končani nalogi"
        Me.optFinnishedKommissions.UseVisualStyleBackColor = True
        '
        'optAllKommissions
        '
        Me.optAllKommissions.AutoSize = True
        Me.optAllKommissions.Location = New System.Drawing.Point(25, 275)
        Me.optAllKommissions.Name = "optAllKommissions"
        Me.optAllKommissions.Size = New System.Drawing.Size(85, 20)
        Me.optAllKommissions.TabIndex = 25
        Me.optAllKommissions.Text = "Vsi nalogi"
        Me.optAllKommissions.UseVisualStyleBackColor = True
        '
        'btnMove
        '
        Me.btnMove.Location = New System.Drawing.Point(1, 8)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(84, 69)
        Me.btnMove.TabIndex = 0
        Me.btnMove.Text = "Premakni na rezalnik"
        Me.btnMove.UseVisualStyleBackColor = True
        '
        'frmCutterSendFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1003, 790)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmCutterSendFile"
        Me.Text = "Pošiljanje datotek na rezalnik"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.dgEprod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtNalog As System.Windows.Forms.TextBox
    Friend WithEvents txtNarocilo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgEprod As System.Windows.Forms.DataGridView
    Friend WithEvents kommissionsnummer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents auftragsnummer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents filename As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents optAllKommissions As System.Windows.Forms.RadioButton
    Friend WithEvents optFinnishedKommissions As System.Windows.Forms.RadioButton
    Friend WithEvents optStartedKommissions As System.Windows.Forms.RadioButton
    Friend WithEvents btnMove As System.Windows.Forms.Button
End Class
