<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateName
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
        Me.chkPrikazSamoRazlicne = New System.Windows.Forms.CheckBox()
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
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.dgOrders = New System.Windows.Forms.DataGridView()
        Me.auftragsnummer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.kommissionsnummer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.datumeintrag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.old_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.new_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.dgOrders, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Size = New System.Drawing.Size(1171, 738)
        Me.SplitContainer1.SplitterDistance = 199
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkPrikazSamoRazlicne)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtNalog)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtNarocilo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmdSearch)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpTo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dtpFrom)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer2.Size = New System.Drawing.Size(199, 738)
        Me.SplitContainer2.SplitterDistance = 359
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 0
        '
        'chkPrikazSamoRazlicne
        '
        Me.chkPrikazSamoRazlicne.AutoSize = True
        Me.chkPrikazSamoRazlicne.Location = New System.Drawing.Point(12, 226)
        Me.chkPrikazSamoRazlicne.Name = "chkPrikazSamoRazlicne"
        Me.chkPrikazSamoRazlicne.Size = New System.Drawing.Size(153, 20)
        Me.chkPrikazSamoRazlicne.TabIndex = 12
        Me.chkPrikazSamoRazlicne.Text = "Prikaži samo različne"
        Me.chkPrikazSamoRazlicne.UseVisualStyleBackColor = True
        '
        'txtNalog
        '
        Me.txtNalog.Location = New System.Drawing.Point(80, 141)
        Me.txtNalog.Name = "txtNalog"
        Me.txtNalog.Size = New System.Drawing.Size(99, 22)
        Me.txtNalog.TabIndex = 10
        '
        'txtNarocilo
        '
        Me.txtNarocilo.Location = New System.Drawing.Point(80, 107)
        Me.txtNarocilo.Name = "txtNarocilo"
        Me.txtNarocilo.Size = New System.Drawing.Size(99, 22)
        Me.txtNarocilo.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 147)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 16)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Nalog:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Naročilo:"
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(35, 300)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(119, 44)
        Me.cmdSearch.TabIndex = 5
        Me.cmdSearch.Text = "Išči"
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'dtpTo
        '
        Me.dtpTo.Checked = False
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(72, 70)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(107, 22)
        Me.dtpTo.TabIndex = 4
        '
        'dtpFrom
        '
        Me.dtpFrom.Checked = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(72, 42)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(107, 22)
        Me.dtpFrom.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Do"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Od"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(169, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Datum kreiranja naloga"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.dgOrders)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.Button2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmdUpdate)
        Me.SplitContainer3.Size = New System.Drawing.Size(967, 738)
        Me.SplitContainer3.SplitterDistance = 802
        Me.SplitContainer3.TabIndex = 0
        '
        'dgOrders
        '
        Me.dgOrders.AllowUserToAddRows = False
        Me.dgOrders.AllowUserToDeleteRows = False
        Me.dgOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOrders.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.auftragsnummer, Me.kommissionsnummer, Me.datumeintrag, Me.old_name, Me.new_name})
        Me.dgOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgOrders.Location = New System.Drawing.Point(0, 0)
        Me.dgOrders.Name = "dgOrders"
        Me.dgOrders.Size = New System.Drawing.Size(802, 738)
        Me.dgOrders.TabIndex = 3
        '
        'auftragsnummer
        '
        Me.auftragsnummer.DataPropertyName = "auftragsnummer"
        Me.auftragsnummer.HeaderText = "Naročilo"
        Me.auftragsnummer.Name = "auftragsnummer"
        Me.auftragsnummer.ReadOnly = True
        Me.auftragsnummer.Width = 84
        '
        'kommissionsnummer
        '
        Me.kommissionsnummer.DataPropertyName = "kommissionsnummer"
        Me.kommissionsnummer.HeaderText = "Nalog"
        Me.kommissionsnummer.Name = "kommissionsnummer"
        Me.kommissionsnummer.ReadOnly = True
        Me.kommissionsnummer.Width = 70
        '
        'datumeintrag
        '
        Me.datumeintrag.DataPropertyName = "datumeintrag"
        Me.datumeintrag.HeaderText = "Datum vnosa"
        Me.datumeintrag.Name = "datumeintrag"
        Me.datumeintrag.ReadOnly = True
        Me.datumeintrag.Width = 112
        '
        'old_name
        '
        Me.old_name.DataPropertyName = "old_name"
        Me.old_name.HeaderText = "Staro ime"
        Me.old_name.Name = "old_name"
        Me.old_name.ReadOnly = True
        Me.old_name.Width = 90
        '
        'new_name
        '
        Me.new_name.DataPropertyName = "new_name"
        Me.new_name.HeaderText = "Novo ime"
        Me.new_name.Name = "new_name"
        Me.new_name.ReadOnly = True
        Me.new_name.Width = 91
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(8, 139)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(141, 57)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Posodobi tekst proizvodnje"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(8, 76)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(141, 57)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Posodobi reklamacije"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Location = New System.Drawing.Point(8, 15)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(141, 55)
        Me.cmdUpdate.TabIndex = 0
        Me.cmdUpdate.Text = "Posodobi imena"
        Me.cmdUpdate.UseVisualStyleBackColor = True
        '
        'frmUpdateName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1171, 738)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmUpdateName"
        Me.Text = "Ažuriranje naslova"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.dgOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgOrders As System.Windows.Forms.DataGridView
    Friend WithEvents auftragsnummer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents kommissionsnummer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents datumeintrag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents old_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents new_name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNalog As System.Windows.Forms.TextBox
    Friend WithEvents txtNarocilo As System.Windows.Forms.TextBox
    Friend WithEvents chkPrikazSamoRazlicne As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
