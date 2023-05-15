using System.Net;
using Tests.ApiClient.Entities;
using RestSharp;

namespace Tests.ApiClient;
public class TrelloApiClient
{
    private const string BaseUrl = "https://api.trello.com/1";
    private readonly RestClient _client;

    public TrelloApiClient(string apiKey, string apiToken)
    {
        _client = new RestClient(BaseUrl);
        _client.AddDefaultParameter("key", apiKey);
        _client.AddDefaultParameter("token", apiToken);
    }

    public List<TrelloBoard> GetAllBoards()
    {
        Console.WriteLine($"Getting all boards");
        var request = new RestRequest("members/me/boards");
        var response = _client.Get<List<TrelloBoard>>(request);
        return response!;
    }

    public TrelloBoard GetBoardById(string boardId)
    {
        Console.WriteLine($"Getting board with ID: {boardId}");
        var request = new RestRequest($"boards/{boardId}");
        var response = _client.Get<TrelloBoard>(request);
        return response!;
    }

    public TrelloBoard CreateBoard(string boardName)
    {
        var request = new RestRequest($"boards?name={boardName}");
        var response = _client.Post<TrelloBoard>(request);
        Console.WriteLine($"Board: {response!.Id} created successfuly");
        return response;
    }

    public void DeleteBoard(string boardId)
    {
        var request = new RestRequest($"boards/{boardId}");
        var response = _client.Delete(request);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine($"Board: {boardId} deleted successfuly");
        }
        else
        {
            Console.WriteLine($"Failed to delete Board: {boardId}");
        }
    }
}
