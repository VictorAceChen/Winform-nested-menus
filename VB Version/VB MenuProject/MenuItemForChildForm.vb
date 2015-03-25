Namespace MenuProject
     
    Class MenuItemForChildForm
        Inherits ToolStripMenuItem

        Shadows Parent As mainForm
        Private _childForm As Type

        Public Sub New(ByVal inputMainForm As mainForm, ByVal inputChildForm As Type)
            Parent = inputMainForm
            _childForm = inputChildForm

            'set the menuItem properties
            Me.Name = inputChildForm.Name
            Me.Text = inputChildForm.Name
            AddHandler Me.Click, AddressOf childFormToolStripMenuItem_Click
        End Sub

        Public ReadOnly Property childForm() As Type
            Get
                Return _childForm
            End Get
        End Property

        Private Sub childFormToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim menuItem As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)

            Dim child = Activator.CreateInstance(Me._childForm)
            child.MdiParent = Parent
            child.Show()
        End Sub
    End Class

End Namespace


