namespace Tests.Utils;

public static class InstallPlaywrightBrowsers
{

    public static void InstallPlaywright()
    {
        var installDeps = Environment.GetEnvironmentVariable("PLAYWRIGHT_DEPS");
        if (installDeps != null)
        {
            Console.WriteLine("Installing playwright browsers");

            var exitCode = Microsoft.Playwright.Program.Main(new[] { "install", "--with-deps" });
            if (exitCode != 0)
            {
                Console.WriteLine("Failed to install playwright browsers");
                Environment.Exit(exitCode);
            }

            Console.WriteLine("Browsers playwright installed");
        }
        Console.WriteLine("Skipping browser installation");
    }
}