
Public Class _childForm
    Inherits Form

    Private Sub childForm_Load(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        'Default settings for child forms
        Me.ControlBox = False
        Me.WindowState = FormWindowState.Maximized

    End Sub

End Class

