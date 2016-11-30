Public Class LoginForm

    Private _ssoid As String

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted

        Dim cookieArray As String() = WebBrowser1.Document.Cookie.Split(";")

        If WebBrowser1.Document.Cookie.Count > 0 Then

            For Each cookie In cookieArray
                Dim idx As Long = cookie.IndexOf("=")
                If Idx <> -1 Then
                    Dim cookieName As String = cookie.Substring(0, idx).Trim
                    Dim cookieValue As String = cookie.Substring(idx + 1).Trim

                    If cookieName = "ssoid" Then
                        _ssoid = cookieValue
                    End If
                End If
            Next
        End If

        If Not _ssoid = "" Then
            Form1.Print(_ssoid)
            Me.Close()
        End If

    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class