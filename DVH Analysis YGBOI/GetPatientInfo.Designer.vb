<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GetPatientInfo
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
        Me.browse = New System.Windows.Forms.Button()
        Me.patientName = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PatientDataTextBox = New System.Windows.Forms.TextBox()
        Me.DVH_Analysis_Emami = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.OpenPatientDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TxTextBox = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'browse
        '
        Me.browse.Location = New System.Drawing.Point(61, 192)
        Me.browse.Name = "browse"
        Me.browse.Size = New System.Drawing.Size(75, 23)
        Me.browse.TabIndex = 0
        Me.browse.Text = "Browse"
        Me.browse.UseVisualStyleBackColor = True
        '
        'patientName
        '
        Me.patientName.Location = New System.Drawing.Point(32, 92)
        Me.patientName.Name = "patientName"
        Me.patientName.Size = New System.Drawing.Size(144, 23)
        Me.patientName.TabIndex = 1
        Me.patientName.Text = "Enter Patient Name"
        Me.patientName.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(89, 141)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 24)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "or"
        '
        'PatientDataTextBox
        '
        Me.PatientDataTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PatientDataTextBox.BackColor = System.Drawing.SystemColors.Menu
        Me.PatientDataTextBox.Location = New System.Drawing.Point(8, 247)
        Me.PatientDataTextBox.Multiline = True
        Me.PatientDataTextBox.Name = "PatientDataTextBox"
        Me.PatientDataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.PatientDataTextBox.Size = New System.Drawing.Size(364, 503)
        Me.PatientDataTextBox.TabIndex = 3
        '
        'DVH_Analysis_Emami
        '
        Me.DVH_Analysis_Emami.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DVH_Analysis_Emami.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DVH_Analysis_Emami.Location = New System.Drawing.Point(218, 92)
        Me.DVH_Analysis_Emami.Name = "DVH_Analysis_Emami"
        Me.DVH_Analysis_Emami.Size = New System.Drawing.Size(144, 51)
        Me.DVH_Analysis_Emami.TabIndex = 4
        Me.DVH_Analysis_Emami.Text = "DVH Analysis with Emami Data"
        Me.DVH_Analysis_Emami.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(218, 178)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(144, 51)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "DVH Analysis with QUANTEC Data"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'OpenPatientDialog
        '
        Me.OpenPatientDialog.FileName = "OpenFileDialog1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TxTextBox)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DVH_Analysis_Emami)
        Me.SplitContainer1.Panel1.Controls.Add(Me.browse)
        Me.SplitContainer1.Panel1.Controls.Add(Me.patientName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.PatientDataTextBox)
        Me.SplitContainer1.Size = New System.Drawing.Size(1393, 762)
        Me.SplitContainer1.SplitterDistance = 384
        Me.SplitContainer1.TabIndex = 8
        '
        'TxTextBox
        '
        Me.TxTextBox.Location = New System.Drawing.Point(4, 53)
        Me.TxTextBox.Name = "TxTextBox"
        Me.TxTextBox.Size = New System.Drawing.Size(520, 20)
        Me.TxTextBox.TabIndex = 8
        Me.TxTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(144, 12)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(84, 35)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Back"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GetPatientInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1393, 762)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IsMdiContainer = True
        Me.MinimumSize = New System.Drawing.Size(415, 600)
        Me.Name = "GetPatientInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DVH Analysis"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents browse As System.Windows.Forms.Button
    Friend WithEvents patientName As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PatientDataTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DVH_Analysis_Emami As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents OpenPatientDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TxTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
