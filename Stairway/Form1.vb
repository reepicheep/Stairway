Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoginForm.Show()
    End Sub

    Public Sub Print(ByVal message As String)
        TextBox1.SelectionStart = TextBox1.Text.Length
        TextBox1.SelectedText = vbCrLf & message
    End Sub

End Class
