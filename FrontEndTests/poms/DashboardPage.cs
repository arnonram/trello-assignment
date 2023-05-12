
using Microsoft.Playwright;

namespace FrontEndTests.poms;

public class DashboardPage
{
    private readonly IPage _page;

    public DashboardPage(IPage page)
    {
        _page = page;
    }

    public async Task Goto(string username)
    {
        await _page.GotoAsync($"u/{username}/boards");
    }

    public async Task CreateBoard(string boardName)
    {
        await _page.GetByText("Create new board").ClickAsync();
        await _page.GetByTestId("create-board-title-input").FillAsync(boardName);
        await _page.GetByTestId("create-board-submit-button").ClickAsync();
    }
}



