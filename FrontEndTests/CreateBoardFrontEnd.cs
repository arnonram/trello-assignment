using FrontEndTests.poms;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Bogus;
using ApiClient;
using Utils;
using Microsoft.Extensions.Configuration;

namespace FrontEndTests;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class CreateBoardFrontEnd : PageTest
{
    private readonly IConfigurationSection _appSettings = LoadAppSettings.GetAppSettings();

    private Faker faker = new Faker();
    private LoginPage _loginPage;
    private DashboardPage _dashboard;
    private BoardPage _boardPage;
    private string _boardNamePrefix = "Test board";

    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions()
        {
            BaseURL = "https://trello.com/",
            ViewportSize = new() { Width = 1920, Height = 1080 }
        };
    }

    [SetUp]
    public void Setup()
    {
        // This next bit here is to install the dependencies and browsers if needed
        InstallPlaywrightBrowsers.InstallPlaywright();

        _loginPage = new LoginPage(Page);
        _dashboard = new DashboardPage(Page);
        _boardPage = new BoardPage(Page);
    }

    [TearDown]
    public void TearDown()
    {
        var client = new TrelloApiClient(_appSettings["apiKey"], _appSettings["apiToken"]);

        var listOfIdToDelete = client.GetAllBoards().FindAll(x => x.Name.StartsWith(_boardNamePrefix));
        foreach (var board in listOfIdToDelete)
        {
            client.DeleteBoard(board.Id);
        }
    }


    [Test]
    public async Task FirstTest()
    {
        var boardName = $"{_boardNamePrefix} {faker.Random.AlphaNumeric(5)}";
        await _loginPage.Login(_appSettings["trelloUserName"], _appSettings["trelloPassword"]);
        await _dashboard.CreateBoard(boardName);

        await Expect(await _boardPage.GetBoardTitle()).ToHaveTextAsync(boardName);
        await Expect(await _boardPage.GetAddListButton()).ToBeVisibleAsync();
    }
}
