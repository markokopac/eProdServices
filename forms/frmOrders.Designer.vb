<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrders
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.cboWorkingPlace = New System.Windows.Forms.ComboBox
        Me.dtmFinnishDateTo = New System.Windows.Forms.DateTimePicker
        Me.dtmFinnishDateFrom = New System.Windows.Forms.DateTimePicker
        Me.dtmSendDateTo = New System.Windows.Forms.DateTimePicker
        Me.dtmSendDateFrom = New System.Windows.Forms.DateTimePicker
        Me.txtPartnerId = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtOrderNr = New System.Windows.Forms.TextBox
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.dgOrders = New System.Windows.Forms.DataGridView
        Me.Order_nr = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.dgOrdersDM = New System.Windows.Forms.DataGridView
        Me.cmdSendmail = New System.Windows.Forms.Button
        Me.OrderNr = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PartnerId = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.email = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.WorkingPlace = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FinnishDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SendDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MailTitle = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Language = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.dgOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgOrdersDM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdSearch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cboWorkingPlace)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtmFinnishDateTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtmFinnishDateFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtmSendDateTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtmSendDateFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPartnerId)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtOrderNr)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1291, 617)
        Me.SplitContainer1.SplitterDistance = 313
        Me.SplitContainer1.TabIndex = 0
        '
        'cmdSearch
        '
        Me.cmdSearch.Location = New System.Drawing.Point(66, 259)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(190, 45)
        Me.cmdSearch.TabIndex = 12
        Me.cmdSearch.Text = "Search"
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'cboWorkingPlace
        '
        Me.cboWorkingPlace.FormattingEnabled = True
        Me.cboWorkingPlace.Location = New System.Drawing.Point(120, 208)
        Me.cboWorkingPlace.Name = "cboWorkingPlace"
        Me.cboWorkingPlace.Size = New System.Drawing.Size(173, 24)
        Me.cboWorkingPlace.TabIndex = 11
        '
        'dtmFinnishDateTo
        '
        Me.dtmFinnishDateTo.Checked = False
        Me.dtmFinnishDateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtmFinnishDateTo.Location = New System.Drawing.Point(120, 173)
        Me.dtmFinnishDateTo.Name = "dtmFinnishDateTo"
        Me.dtmFinnishDateTo.ShowCheckBox = True
        Me.dtmFinnishDateTo.Size = New System.Drawing.Size(116, 22)
        Me.dtmFinnishDateTo.TabIndex = 10
        '
        'dtmFinnishDateFrom
        '
        Me.dtmFinnishDateFrom.Checked = False
        Me.dtmFinnishDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtmFinnishDateFrom.Location = New System.Drawing.Point(120, 145)
        Me.dtmFinnishDateFrom.Name = "dtmFinnishDateFrom"
        Me.dtmFinnishDateFrom.ShowCheckBox = True
        Me.dtmFinnishDateFrom.Size = New System.Drawing.Size(116, 22)
        Me.dtmFinnishDateFrom.TabIndex = 9
        '
        'dtmSendDateTo
        '
        Me.dtmSendDateTo.Checked = False
        Me.dtmSendDateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtmSendDateTo.Location = New System.Drawing.Point(117, 102)
        Me.dtmSendDateTo.Name = "dtmSendDateTo"
        Me.dtmSendDateTo.ShowCheckBox = True
        Me.dtmSendDateTo.Size = New System.Drawing.Size(116, 22)
        Me.dtmSendDateTo.TabIndex = 8
        '
        'dtmSendDateFrom
        '
        Me.dtmSendDateFrom.Checked = False
        Me.dtmSendDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtmSendDateFrom.Location = New System.Drawing.Point(117, 74)
        Me.dtmSendDateFrom.Name = "dtmSendDateFrom"
        Me.dtmSendDateFrom.ShowCheckBox = True
        Me.dtmSendDateFrom.Size = New System.Drawing.Size(116, 22)
        Me.dtmSendDateFrom.TabIndex = 7
        '
        'txtPartnerId
        '
        Me.txtPartnerId.Location = New System.Drawing.Point(120, 42)
        Me.txtPartnerId.Name = "txtPartnerId"
        Me.txtPartnerId.Size = New System.Drawing.Size(190, 22)
        Me.txtPartnerId.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 211)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 16)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Working place:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Finnish Date:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Send Date:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Partner ID:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Order Nr.:"
        '
        'txtOrderNr
        '
        Me.txtOrderNr.Location = New System.Drawing.Point(120, 9)
        Me.txtOrderNr.Name = "txtOrderNr"
        Me.txtOrderNr.Size = New System.Drawing.Size(90, 22)
        Me.txtOrderNr.TabIndex = 0
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmdSendmail)
        Me.SplitContainer2.Size = New System.Drawing.Size(974, 617)
        Me.SplitContainer2.SplitterDistance = 768
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.dgOrders)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.dgOrdersDM)
        Me.SplitContainer3.Size = New System.Drawing.Size(768, 617)
        Me.SplitContainer3.SplitterDistance = 104
        Me.SplitContainer3.TabIndex = 0
        '
        'dgOrders
        '
        Me.dgOrders.AllowUserToAddRows = False
        Me.dgOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOrders.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Order_nr})
        Me.dgOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgOrders.Location = New System.Drawing.Point(0, 0)
        Me.dgOrders.MultiSelect = False
        Me.dgOrders.Name = "dgOrders"
        Me.dgOrders.RowHeadersVisible = False
        Me.dgOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgOrders.Size = New System.Drawing.Size(104, 617)
        Me.dgOrders.TabIndex = 0
        '
        'Order_nr
        '
        Me.Order_nr.DataPropertyName = "order_nr"
        Me.Order_nr.HeaderText = "Order Nr."
        Me.Order_nr.Name = "Order_nr"
        Me.Order_nr.ReadOnly = True
        '
        'dgOrdersDM
        '
        Me.dgOrdersDM.AllowUserToAddRows = False
        Me.dgOrdersDM.AllowUserToDeleteRows = False
        Me.dgOrdersDM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgOrdersDM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOrdersDM.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OrderNr, Me.PartnerId, Me.email, Me.WorkingPlace, Me.FinnishDate, Me.SendDate, Me.MailTitle, Me.Language})
        Me.dgOrdersDM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgOrdersDM.Location = New System.Drawing.Point(0, 0)
        Me.dgOrdersDM.Name = "dgOrdersDM"
        Me.dgOrdersDM.RowHeadersWidth = 20
        Me.dgOrdersDM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgOrdersDM.Size = New System.Drawing.Size(660, 617)
        Me.dgOrdersDM.TabIndex = 0
        '
        'cmdSendmail
        '
        Me.cmdSendmail.Location = New System.Drawing.Point(22, 10)
        Me.cmdSendmail.Name = "cmdSendmail"
        Me.cmdSendmail.Size = New System.Drawing.Size(132, 38)
        Me.cmdSendmail.TabIndex = 0
        Me.cmdSendmail.Text = "Send Mail"
        Me.cmdSendmail.UseVisualStyleBackColor = True
        '
        'OrderNr
        '
        Me.OrderNr.DataPropertyName = "order_nr"
        Me.OrderNr.HeaderText = "OrderNr"
        Me.OrderNr.Name = "OrderNr"
        Me.OrderNr.ReadOnly = True
        Me.OrderNr.Visible = False
        Me.OrderNr.Width = 62
        '
        'PartnerId
        '
        Me.PartnerId.DataPropertyName = "partner_id"
        Me.PartnerId.HeaderText = "Partner ID"
        Me.PartnerId.Name = "PartnerId"
        Me.PartnerId.ReadOnly = True
        Me.PartnerId.Width = 92
        '
        'email
        '
        Me.email.DataPropertyName = "email_addresse"
        Me.email.HeaderText = "email"
        Me.email.Name = "email"
        Me.email.ReadOnly = True
        Me.email.Width = 66
        '
        'WorkingPlace
        '
        Me.WorkingPlace.DataPropertyName = "working_place"
        Me.WorkingPlace.HeaderText = "Working place"
        Me.WorkingPlace.Name = "WorkingPlace"
        Me.WorkingPlace.ReadOnly = True
        Me.WorkingPlace.Width = 120
        '
        'FinnishDate
        '
        Me.FinnishDate.DataPropertyName = "finnish_date"
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.FinnishDate.DefaultCellStyle = DataGridViewCellStyle1
        Me.FinnishDate.HeaderText = "Finnish Date"
        Me.FinnishDate.Name = "FinnishDate"
        Me.FinnishDate.ReadOnly = True
        Me.FinnishDate.Width = 107
        '
        'SendDate
        '
        Me.SendDate.DataPropertyName = "send_date"
        DataGridViewCellStyle2.Format = "d"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.SendDate.DefaultCellStyle = DataGridViewCellStyle2
        Me.SendDate.HeaderText = "SendDate"
        Me.SendDate.Name = "SendDate"
        Me.SendDate.ReadOnly = True
        Me.SendDate.Width = 94
        '
        'MailTitle
        '
        Me.MailTitle.DataPropertyName = "mail_title"
        Me.MailTitle.HeaderText = "Mail Title"
        Me.MailTitle.Name = "MailTitle"
        Me.MailTitle.ReadOnly = True
        Me.MailTitle.Width = 87
        '
        'Language
        '
        Me.Language.DataPropertyName = "language"
        Me.Language.HeaderText = "Language"
        Me.Language.Name = "Language"
        Me.Language.ReadOnly = True
        Me.Language.Width = 94
        '
        'frmOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1291, 617)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmOrders"
        Me.Text = "Orders"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.dgOrders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgOrdersDM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgOrders As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOrderNr As System.Windows.Forms.TextBox
    Friend WithEvents dgOrdersDM As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents cboWorkingPlace As System.Windows.Forms.ComboBox
    Friend WithEvents dtmFinnishDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmFinnishDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmSendDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtmSendDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPartnerId As System.Windows.Forms.TextBox
    Friend WithEvents Order_nr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdSendmail As System.Windows.Forms.Button
    Friend WithEvents OrderNr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PartnerId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents email As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WorkingPlace As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FinnishDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SendDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MailTitle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Language As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
