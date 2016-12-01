Imports System.Net
Imports System.Text
Imports System.IO
Imports Newtonsoft.Json

Module SportsAPI

    Public ssoid As String 'session token

    Private Function SendSportsReq(ByVal jsonString As String)

        Dim request As HttpWebRequest = WebRequest.Create("https://api.betfair.com/exchange/betting/json-rpc/v1")

        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(jsonString)

        Dim responseFromServer As String = ""

        Try
            request.Method = "POST"
            request.ContentType = "application/json"
            request.ContentLength = byteArray.Length
            request.Headers.Add("X-Application: Your AppKey Here")
            request.Headers.Add("X-Authentication: " & ssoid)
            request.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
            request.ServicePoint.Expect100Continue = False
            request.Timeout = 2000

            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)

            Dim response As WebResponse = request.GetResponse()
            dataStream = response.GetResponseStream()

            Dim reader As New StreamReader(dataStream)
            responseFromServer = reader.ReadToEnd()

            Form1.Print(responseFromServer) 'delete in chapter 5

            reader.Dispose()
            dataStream.Dispose()
            response.Dispose()

        Catch ex As WebException 'Exception
            Form1.Print("SendSportsReq Error: " & ex.Message)
        End Try

        Return responseFromServer

    End Function

    'Classes and function for MarketCatalogue request
    Public Class MarketCatalogueRequest
        Public jsonrpc As String = "2.0"
        Public method As String = "SportsAPING/v1.0/listMarketCatalogue"
        Public params As New Params
        Public id As Integer = 1
    End Class

    Public Class Params
        Public filter As New Filter
        Public sort As String = "FIRST_TO_START"
        Public maxResults As String = "200"
        Public marketProjection As New List(Of String)
    End Class

    Public Class Filter
        Public eventTypeIds As New List(Of String)
        Public marketCountries As New List(Of String)
        Public marketTypeCodes As New List(Of String)
        Public marketStartTime As New StartTime
    End Class

    Public Class StartTime
        Public from As String
        Public [to] As String
    End Class

    Function SerializeMarketCatalogueRequest(ByVal requestList As List(Of MarketCatalogueRequest))

        Dim temp As String = JsonConvert.SerializeObject(requestList)
        Form1.Print(temp)
        Form1.Print("")

        Return temp

        '        Return JsonConvert.SerializeObject(requestList)

    End Function

    'Classes and function for listMarketCatalogue response
    Public Class MarketCatalogueResponse
        Public jsonrpc As String
        Public result As List(Of MarketCatalogue)
        Public id As Integer
    End Class

    Public Class MarketCatalogue
        Public marketId As String
        Public marketName As String
        Public marketStartTime As String
        Public totalMatched As Double
        Public runners As New List(Of Runners)()
        Public [event] As New [Event]
    End Class

    Public Class Runners
        Public selectionId As Integer
        Public runnerName As String
        Public handicap As Double
        Public sortPriority As Integer
    End Class

    Public Class [Event]
        Public id As Integer
        Public name As String
        Public countryCode As String
        Public timezone As String
        Public venue As String
        Public openDate As String
    End Class

    Function DeserializeMarketCatalogueResponse(ByVal jsonResponse As String)

        Return JsonConvert.DeserializeObject(Of MarketCatalogueResponse())(SendSportsReq(jsonResponse))

    End Function

End Module
