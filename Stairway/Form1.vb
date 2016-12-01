Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoginForm.Show()
    End Sub

    Public Sub Print(ByVal message As String)
        TextBox1.SelectionStart = TextBox1.Text.Length
        TextBox1.SelectedText = vbCrLf & message
    End Sub

    Public Sub Initialise()
        ListMarketCatalogue()
    End Sub

    Private Sub ListMarketCatalogue()

        Dim requestList As New List(Of MarketCatalogueRequest)
        Dim request As New MarketCatalogueRequest
        Dim params As New Params

        Dim eventTypeIds As New List(Of String)
        eventTypeIds.Add("7")
        params.filter.eventTypeIds = eventTypeIds

        Dim marketCountries As New List(Of String)
        marketCountries.Add("GB")
        params.filter.marketCountries = marketCountries

        Dim marketProjection As New List(Of String)
        marketProjection.Add("MARKET_START_TIME")
        marketProjection.Add("RUNNER_DESCRIPTION")
        marketProjection.Add("EVENT")
        params.marketProjection = marketProjection

        Dim marketTypeCodes As New List(Of String)
        marketTypeCodes.Add("WIN")
        params.filter.marketTypeCodes = marketTypeCodes

        Dim marketStartTime As New StartTime

        If Today.IsDaylightSavingTime() Then

            marketStartTime.from = Format(Date.Now, "yyyy-MM-dd") & "T" & Format(Now.AddHours(-1), "HH:mm") & "Z"

        Else

            marketStartTime.from = Format(Date.Now, "yyyy-MM-dd") & "T" & Format(Now, "HH:mm") & "Z"

        End If

        marketStartTime.to = Today.ToString("u").Replace(" ", "T").Replace("00:00", "23:00")
        params.filter.marketStartTime = marketStartTime

        request.params = params
        requestList.Add(request)

        Dim allMarkets() As MarketCatalogueResponse

        allMarkets = DeserializeMarketCatalogueResponse(SerializeMarketCatalogueRequest(requestList))

        '    For n = 0 To allMarkets(0).result.Count - 1

        '        For m = 0 To allMarkets(0).result(n).runners.Count - 1

        '            Dim course() As String
        '            course = Split(allMarkets(0).result(n).event.name)

        '            Print(Format(allMarkets(0).result(n).marketStartTime, "Short Time") & " " & course(0) & " " & allMarkets(0).result(n).marketName & " " & allMarkets(0).result(n).runners.Item(m).runnerName)
        '        Next

        '    Next

    End Sub

End Class