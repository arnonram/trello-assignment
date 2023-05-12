namespace knab_assignment_cs;

using ApiClient;
using Bogus;
using Microsoft.Extensions.Configuration;
using Utils;

[TestFixture]
public class CreateBoardApiTests
{
    private readonly IConfigurationSection _appSettings = LoadAppSettings.GetAppSettings();
    private readonly string _boardName = "test board";
    private TrelloApiClient _client;
    private Faker faker;

    [OneTimeSetUp]
    public void Setup()
    {
        _client = new TrelloApiClient(_appSettings["apiKey"], _appSettings["apiToken"]);

        faker = new Faker();
    }

    [OneTimeTearDown]
    public void TeadDown()
    {
        var listOfIdToDelete = _client.GetAllBoards().FindAll(x => x.Name.StartsWith(_boardName));
        foreach (var board in listOfIdToDelete)
        {

            _client.DeleteBoard(board.Id);
        }
    }

    [Test]
    public void When_Creating_Board_Expect_to_see_it_in_list_of_boards()
    {
        var board = $"{_boardName} {faker.Random.AlphaNumeric(5)}";
        var createBoardResponse = _client.CreateBoard(board);
        var boardFromAllBoards = _client.GetAllBoards().Find(x => x.Name == board);
        createBoardResponse.Should().BeEquivalentTo(boardFromAllBoards);
    }
}