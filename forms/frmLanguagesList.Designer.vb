<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLanguagesList
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.dgLanguages = New System.Windows.Forms.DataGridView
        Me.language = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgLanguages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgLanguages)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdAdd)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmdEdit)
        Me.SplitContainer1.Size = New System.Drawing.Size(325, 526)
        Me.SplitContainer1.SplitterDistance = 478
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 1
        '
        'dgLanguages
        '
        Me.dgLanguages.AllowUserToAddRows = False
        Me.dgLanguages.AllowUserToDeleteRows = False
        Me.dgLanguages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgLanguages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgLanguages.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.language, Me.description})
        Me.dgLanguages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgLanguages.Location = New System.Drawing.Point(0, 0)
        Me.dgLanguages.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgLanguages.Name = "dgLanguages"
        Me.dgLanguages.Size = New System.Drawing.Size(325, 478)
        Me.dgLanguages.TabIndex = 1
        '
        'language
        '
        Me.language.DataPropertyName = "language"
        Me.language.HeaderText = "Code"
        Me.language.Name = "language"
        Me.language.ReadOnly = True
        Me.language.Width = 66
        '
        'description
        '
        Me.description.DataPropertyName = "description"
        Me.description.HeaderText = "Description"
        Me.description.Name = "description"
        Me.description.Width = 101
        '
        'cmdDelete
        '
        Me.cmdDelete.Image = Global.eProdService.My.Resources.Resources._erase
        Me.cmdDelete.Location = New System.Drawing.Point(255, 5)
        Me.cmdDelete.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(43, 33)
        Me.cmdDelete.TabIndex = 22
        Me.cmdDelete.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = Global.eProdService.My.Resources.Resources.add
        Me.cmdAdd.Location = New System.Drawing.Point(17, 5)
        Me.cmdAdd.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(44, 33)
        Me.cmdAdd.TabIndex = 21
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdEdit
        '
        Me.cmdEdit.Image = Global.eProdService.My.Resources.Resources.edit
        Me.cmdEdit.Location = New System.Drawing.Point(140, 6)
        Me.cmdEdit.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(41, 32)
        Me.cmdEdit.TabIndex = 20
        Me.cmdEdit.UseVisualStyleBackColor = True
        '
        'frmLanguagesList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(325, 526)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmLanguagesList"
        Me.Text = "Languages"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgLanguages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents dgLanguages As System.Windows.Forms.DataGridView
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents language As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents description As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
