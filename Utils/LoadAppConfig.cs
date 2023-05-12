using Microsoft.Extensions.Configuration;

namespace Utils;

public static class LoadAppSettings
{

    public static IConfigurationSection GetAppSettings()
    {
        var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        return configBuilder.GetSection("AppSettings");
    }
}