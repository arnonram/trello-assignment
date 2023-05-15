using Microsoft.Playwright;

namespace Tests.FrontEndTests.poms;

public class BoardPage
{
    private readonly IPage _page;

    public BoardPage(IPage page)
    {
        _page = page;
    }

    public async Task Goto(string shortUrl)
    {
        await _page.GotoAsync(shortUrl);
    }

    public async Task<ILocator> GetBoardTitle()
    {
        return _page.GetByTestId("board-name-display");
    }

    public async Task<ILocator> GetAddListButton()
    {
        return _page.GetByRole(AriaRole.Link, new() { Name = "Add another list" });
    }
}



