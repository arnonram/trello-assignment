using Microsoft.Playwright;

namespace FrontEndTests.poms;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task Goto()
    {
        await _page.GotoAsync("login");
    }

    public async Task Login(string username, string password)
    {
        await Goto();
        await _page.FillAsync("#user", username);
        await _page.ClickAsync("#login");
        await _page.FillAsync("#password", password);
        await _page.ClickAsync("#login-submit");
        await _page.WaitForSelectorAsync("#header");
    }
}

