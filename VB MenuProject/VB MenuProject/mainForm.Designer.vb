Namespace MenuProject
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class mainForm
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
            Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
            Me.TabControl1 = New System.Windows.Forms.TabControl()
            Me.SuspendLayout()
            '
            'MenuStrip1
            '
            Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
            Me.MenuStrip1.Name = "MenuStrip1"
            Me.MenuStrip1.Size = New System.Drawing.Size(409, 24)
            Me.MenuStrip1.TabIndex = 1
            Me.MenuStrip1.Text = "MenuStrip1"
            '
            'TabControl1
            '
            Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Top
            Me.TabControl1.Location = New System.Drawing.Point(0, 24)
            Me.TabControl1.Name = "TabControl1"
            Me.TabControl1.SelectedIndex = 0
            Me.TabControl1.Size = New System.Drawing.Size(409, 23)
            Me.TabControl1.TabIndex = 2
            Me.TabControl1.Visible = False
            AddHandler TabControl1.DrawItem, AddressOf tabControl1_DrawItem
            '
            'Form1
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(409, 290)
            Me.Controls.Add(Me.TabControl1)
            Me.Controls.Add(Me.MenuStrip1)
            Me.IsMdiContainer = True
            Me.MainMenuStrip = Me.MenuStrip1
            Me.Name = "Form1"
            Me.Text = "Form1"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
        Friend WithEvents TabControl1 As System.Windows.Forms.TabControl



    End Class
End Namespace

