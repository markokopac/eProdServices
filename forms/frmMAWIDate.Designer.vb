<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMawiDate
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.txtNarocilo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboMawiDateType = New System.Windows.Forms.ComboBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.dgOrders = New System.Windows.Forms.DataGridView()
        Me.order_nr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_contract = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_order = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_order_state = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_partner_head = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_partner_branch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.docname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.order_type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.description1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.osname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pbname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.order_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.latest_delivery_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.order_confirmation_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.datum_potrjene_dobave = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.St_potrditve = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.datum_dobave = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ST_dobavnice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_order_confirmation = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.dgArticles = New System.Windows.Forms.DataGridView()
        Me.dgEprod = New System.Windows.Forms.DataGridView()
        Me.cmdUpdateAll = New System.Windows.Forms.Button()
        Me.cmdSearchEProd = New System.Windows.Forms.Button()
        Me.txtArticleType = New System.Windows.Forms.TextBox()
        Me.lblCounterUpdate = New System.Windows.Forms.Label()
        Me.lblCounter = New System.Windows.Forms.Label()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.dgOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        CType(Me.dgArticles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgEprod, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1272, 602)
        Me.SplitContainer1.SplitterDistance = 232
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtNarocilo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.cboMawiDateType)
        Me.SplitContainer3.Panel1.Controls.Add(Me.cmdSearch)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpTo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpFrom)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer3.Size = New System.Drawing.Size(232, 602)
        Me.SplitContainer3.SplitterDistance = 433
        Me.SplitContainer3.TabIndex = 0
        '
        'txtNarocilo
        '
        Me.txtNarocilo.Location = New System.Drawing.Point(75, 100)
        Me.txtNarocilo.Name = "txtNarocilo"
        Me.txtNarocilo.Size = New System.Drawing.Size(147, 20)
        Me.txtNarocilo.TabIndex = 23
        Me.txtNarocilo.Text = "TMK024"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Naročilo"
        '
        'cboMawiDateType
        '
        Me.cboMawiDateType.FormattingEnabled = True
        Me.cboMawiDateType.Location = New System.Drawing.Point(14, 7)
        Me.cboMawiDateType.Name = "cboMawiDateType"
        Me.cboMawiDateType.Size = New System.Drawing.Size(209, 21)
        Me.cboMawiDateType.TabIndex = 21
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(61, 299)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(119, 44)
        Me.cmdSearch.TabIndex = 16
        Me.cmdSearch.Text = "Išči"
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'dtpTo
        '
        Me.dtpTo.Checked = False
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(73, 64)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(107, 20)
        Me.dtpTo.TabIndex = 15
        '
        'dtpFrom
        '
        Me.dtpFrom.Checked = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(73, 36)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(107, 20)
        Me.dtpFrom.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Do"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Od"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer4)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmdUpdateAll)
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmdSearchEProd)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtArticleType)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblCounterUpdate)
        Me.SplitContainer2.Panel2.Controls.Add(Me.lblCounter)
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmdUpdate)
        Me.SplitContainer2.Size = New System.Drawing.Size(1036, 602)
        Me.SplitContainer2.SplitterDistance = 857
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.dgOrders)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.SplitContainer5)
        Me.SplitContainer4.Size = New System.Drawing.Size(857, 602)
        Me.SplitContainer4.SplitterDistance = 265
        Me.SplitContainer4.TabIndex = 1
        '
        'dgOrders
        '
        Me.dgOrders.AllowUserToAddRows = False
        Me.dgOrders.AllowUserToDeleteRows = False
        Me.dgOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOrders.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.order_nr, Me.id_contract, Me.id_order, Me.id_order_state, Me.id_partner_head, Me.id_partner_branch, Me.docname, Me.order_type, Me.description1, Me.osname, Me.pbname, Me.order_date, Me.latest_delivery_date, Me.order_confirmation_date, Me.datum_potrjene_dobave, Me.St_potrditve, Me.datum_dobave, Me.ST_dobavnice, Me.id_order_confirmation})
        Me.dgOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgOrders.Location = New System.Drawing.Point(0, 0)
        Me.dgOrders.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dgOrders.Name = "dgOrders"
        Me.dgOrders.RowHeadersVisible = False
        Me.dgOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgOrders.Size = New System.Drawing.Size(857, 265)
        Me.dgOrders.TabIndex = 4
        '
        'order_nr
        '
        Me.order_nr.DataPropertyName = "order_nr"
        Me.order_nr.HeaderText = "Naročilo"
        Me.order_nr.Name = "order_nr"
        Me.order_nr.ReadOnly = True
        '
        'id_contract
        '
        Me.id_contract.DataPropertyName = "id_contract"
        Me.id_contract.HeaderText = "ID Contract"
        Me.id_contract.Name = "id_contract"
        Me.id_contract.ReadOnly = True
        Me.id_contract.Visible = False
        '
        'id_order
        '
        Me.id_order.DataPropertyName = "id_order"
        Me.id_order.HeaderText = "ID"
        Me.id_order.Name = "id_order"
        Me.id_order.ReadOnly = True
        Me.id_order.Visible = False
        '
        'id_order_state
        '
        Me.id_order_state.DataPropertyName = "id_order_state"
        Me.id_order_state.HeaderText = "ID State"
        Me.id_order_state.Name = "id_order_state"
        Me.id_order_state.ReadOnly = True
        Me.id_order_state.Visible = False
        '
        'id_partner_head
        '
        Me.id_partner_head.DataPropertyName = "id_partner_head"
        Me.id_partner_head.HeaderText = "ID Partner Head"
        Me.id_partner_head.Name = "id_partner_head"
        Me.id_partner_head.ReadOnly = True
        Me.id_partner_head.Visible = False
        '
        'id_partner_branch
        '
        Me.id_partner_branch.DataPropertyName = "id_partner_branch"
        Me.id_partner_branch.HeaderText = "ID Partner Branch"
        Me.id_partner_branch.Name = "id_partner_branch"
        Me.id_partner_branch.ReadOnly = True
        Me.id_partner_branch.Visible = False
        '
        'docname
        '
        Me.docname.DataPropertyName = "docname"
        Me.docname.HeaderText = "Številka naročila"
        Me.docname.Name = "docname"
        Me.docname.ReadOnly = True
        Me.docname.Width = 80
        '
        'order_type
        '
        Me.order_type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.order_type.DataPropertyName = "order_type"
        Me.order_type.HeaderText = "Tip"
        Me.order_type.Name = "order_type"
        Me.order_type.ReadOnly = True
        Me.order_type.Width = 47
        '
        'description1
        '
        Me.description1.DataPropertyName = "description"
        Me.description1.HeaderText = "Opis"
        Me.description1.Name = "description1"
        Me.description1.ReadOnly = True
        Me.description1.Width = 120
        '
        'osname
        '
        Me.osname.DataPropertyName = "osname"
        Me.osname.HeaderText = "Status naročila"
        Me.osname.Name = "osname"
        Me.osname.ReadOnly = True
        Me.osname.Width = 70
        '
        'pbname
        '
        Me.pbname.DataPropertyName = "pbname"
        Me.pbname.HeaderText = "Dobavitelj"
        Me.pbname.Name = "pbname"
        Me.pbname.ReadOnly = True
        Me.pbname.Width = 120
        '
        'order_date
        '
        Me.order_date.DataPropertyName = "order_date"
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.order_date.DefaultCellStyle = DataGridViewCellStyle1
        Me.order_date.HeaderText = "Datum naročila"
        Me.order_date.Name = "order_date"
        Me.order_date.ReadOnly = True
        Me.order_date.Width = 70
        '
        'latest_delivery_date
        '
        Me.latest_delivery_date.DataPropertyName = "latest_delivery_date"
        DataGridViewCellStyle2.Format = "d"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.latest_delivery_date.DefaultCellStyle = DataGridViewCellStyle2
        Me.latest_delivery_date.HeaderText = "Zadnji datum"
        Me.latest_delivery_date.Name = "latest_delivery_date"
        Me.latest_delivery_date.ReadOnly = True
        Me.latest_delivery_date.Width = 70
        '
        'order_confirmation_date
        '
        Me.order_confirmation_date.DataPropertyName = "order_confirmation_date"
        Me.order_confirmation_date.HeaderText = "Datum vnosa potrditve"
        Me.order_confirmation_date.Name = "order_confirmation_date"
        Me.order_confirmation_date.ReadOnly = True
        '
        'datum_potrjene_dobave
        '
        Me.datum_potrjene_dobave.DataPropertyName = "datum_potrjene_dobave"
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.datum_potrjene_dobave.DefaultCellStyle = DataGridViewCellStyle3
        Me.datum_potrjene_dobave.HeaderText = "Potrjena dobava"
        Me.datum_potrjene_dobave.Name = "datum_potrjene_dobave"
        Me.datum_potrjene_dobave.ReadOnly = True
        Me.datum_potrjene_dobave.Width = 70
        '
        'St_potrditve
        '
        Me.St_potrditve.DataPropertyName = "St_potrditve"
        Me.St_potrditve.HeaderText = "Št. potrditve"
        Me.St_potrditve.Name = "St_potrditve"
        Me.St_potrditve.ReadOnly = True
        '
        'datum_dobave
        '
        Me.datum_dobave.DataPropertyName = "datum_dobave"
        DataGridViewCellStyle4.Format = "d"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.datum_dobave.DefaultCellStyle = DataGridViewCellStyle4
        Me.datum_dobave.HeaderText = "Datum dobave"
        Me.datum_dobave.Name = "datum_dobave"
        Me.datum_dobave.ReadOnly = True
        Me.datum_dobave.Width = 70
        '
        'ST_dobavnice
        '
        Me.ST_dobavnice.DataPropertyName = "ST_dobavnice"
        Me.ST_dobavnice.HeaderText = "Št.dobavnice"
        Me.ST_dobavnice.Name = "ST_dobavnice"
        Me.ST_dobavnice.ReadOnly = True
        '
        'id_order_confirmation
        '
        Me.id_order_confirmation.DataPropertyName = "id_order_confirmation"
        Me.id_order_confirmation.HeaderText = "id_order_confirmation"
        Me.id_order_confirmation.Name = "id_order_confirmation"
        Me.id_order_confirmation.ReadOnly = True
        Me.id_order_confirmation.Visible = False
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Margin = New System.Windows.Forms.Padding(2)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.dgArticles)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.dgEprod)
        Me.SplitContainer5.Size = New System.Drawing.Size(857, 333)
        Me.SplitContainer5.SplitterDistance = 179
        Me.SplitContainer5.SplitterWidth = 3
        Me.SplitContainer5.TabIndex = 0
        '
        'dgArticles
        '
        Me.dgArticles.AllowUserToAddRows = False
        Me.dgArticles.AllowUserToDeleteRows = False
        Me.dgArticles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgArticles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgArticles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgArticles.Location = New System.Drawing.Point(0, 0)
        Me.dgArticles.Margin = New System.Windows.Forms.Padding(2)
        Me.dgArticles.Name = "dgArticles"
        Me.dgArticles.RowTemplate.Height = 24
        Me.dgArticles.Size = New System.Drawing.Size(857, 179)
        Me.dgArticles.TabIndex = 0
        '
        'dgEprod
        '
        Me.dgEprod.AllowUserToAddRows = False
        Me.dgEprod.AllowUserToDeleteRows = False
        Me.dgEprod.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgEprod.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgEprod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgEprod.Location = New System.Drawing.Point(0, 0)
        Me.dgEprod.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dgEprod.Name = "dgEprod"
        Me.dgEprod.RowHeadersVisible = False
        Me.dgEprod.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgEprod.Size = New System.Drawing.Size(857, 151)
        Me.dgEprod.TabIndex = 3
        '
        'cmdUpdateAll
        '
        Me.cmdUpdateAll.Location = New System.Drawing.Point(15, 309)
        Me.cmdUpdateAll.Name = "cmdUpdateAll"
        Me.cmdUpdateAll.Size = New System.Drawing.Size(151, 37)
        Me.cmdUpdateAll.TabIndex = 6
        Me.cmdUpdateAll.Text = "Posodobi vse"
        Me.cmdUpdateAll.UseVisualStyleBackColor = True
        '
        'cmdSearchEProd
        '
        Me.cmdSearchEProd.Location = New System.Drawing.Point(13, 269)
        Me.cmdSearchEProd.Name = "cmdSearchEProd"
        Me.cmdSearchEProd.Size = New System.Drawing.Size(147, 34)
        Me.cmdSearchEProd.TabIndex = 5
        Me.cmdSearchEProd.Text = "Poišči vse tekste v eProd-u"
        Me.cmdSearchEProd.UseVisualStyleBackColor = True
        '
        'txtArticleType
        '
        Me.txtArticleType.Location = New System.Drawing.Point(12, 3)
        Me.txtArticleType.Name = "txtArticleType"
        Me.txtArticleType.Size = New System.Drawing.Size(151, 20)
        Me.txtArticleType.TabIndex = 4
        Me.txtArticleType.Text = "'Komarnik','Zubehör'"
        '
        'lblCounterUpdate
        '
        Me.lblCounterUpdate.Location = New System.Drawing.Point(12, 520)
        Me.lblCounterUpdate.Name = "lblCounterUpdate"
        Me.lblCounterUpdate.Size = New System.Drawing.Size(140, 30)
        Me.lblCounterUpdate.TabIndex = 3
        Me.lblCounterUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCounter
        '
        Me.lblCounter.Location = New System.Drawing.Point(20, 349)
        Me.lblCounter.Name = "lblCounter"
        Me.lblCounter.Size = New System.Drawing.Size(140, 30)
        Me.lblCounter.TabIndex = 2
        Me.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Location = New System.Drawing.Point(12, 553)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(151, 37)
        Me.cmdUpdate.TabIndex = 0
        Me.cmdUpdate.Text = "Posodobi"
        Me.cmdUpdate.UseVisualStyleBackColor = True
        '
        'frmMawiDate
        '
        Me.AcceptButton = Me.cmdSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1272, 602)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmMawiDate"
        Me.Text = "MAWI datumi"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.dgOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        CType(Me.dgArticles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgEprod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboMawiDateType As System.Windows.Forms.ComboBox
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgOrders As System.Windows.Forms.DataGridView
    Friend WithEvents order_nr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id_contract As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id_order As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id_order_state As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id_partner_head As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id_partner_branch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents docname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents order_type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents description1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents osname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pbname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents order_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents latest_delivery_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents order_confirmation_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents datum_potrjene_dobave As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents St_potrditve As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents datum_dobave As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ST_dobavnice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id_order_confirmation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents lblCounter As System.Windows.Forms.Label
    Friend WithEvents lblCounterUpdate As System.Windows.Forms.Label
    Friend WithEvents txtNarocilo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtArticleType As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer5 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgEprod As System.Windows.Forms.DataGridView
    Friend WithEvents dgArticles As System.Windows.Forms.DataGridView
    Friend WithEvents cmdSearchEProd As System.Windows.Forms.Button
    Friend WithEvents cmdUpdateAll As System.Windows.Forms.Button
End Class
