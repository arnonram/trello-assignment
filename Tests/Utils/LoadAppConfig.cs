using Microsoft.Extensions.Configuration;

namespace Tests.Utils;

public static class LoadAppSettings
{

    public static IConfigurationSection GetAppSettings()
    {
        var isTest = Environment.GetEnvironmentVariable("ENV") ?? "dev";
        var appSettingsfileToUse = isTest == "test" ? "appsettings.test.json" : "appsettings.json";
        Console.WriteLine($"Running tests with: {appSettingsfileToUse}");
        var configBuilder = new ConfigurationBuilder().AddJsonFile(appSettingsfileToUse).Build();
        return configBuilder.GetSection("AppSettings");
    }
}