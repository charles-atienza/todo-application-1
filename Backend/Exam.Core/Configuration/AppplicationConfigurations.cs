using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;

namespace Exam.Configuration;

public static class AppplicationConfigurations
{
    private static readonly ConcurrentDictionary<string, IConfigurationRoot> ConfigurationCache;

    static AppplicationConfigurations()
    {
        ConfigurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
    }

    public static IConfigurationRoot Get(string path, string? environmentName = null, bool addUserSecrets = false)
    {
        var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
        return ConfigurationCache.GetOrAdd(
            cacheKey,
            _ => BuildConfiguration(path, environmentName, addUserSecrets)
        );
    }

    private static IConfigurationRoot BuildConfiguration(
        string path,
        string? environmentName = null,
        bool addUserSecrets = false
    )
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", true, true);

        if (!string.IsNullOrWhiteSpace(environmentName))
        {
            builder = builder.AddJsonFile($"appsettings.{environmentName}.json", true);
        }

        builder = builder.AddEnvironmentVariables();

        if (addUserSecrets)
        {
            builder.AddUserSecrets(typeof(AppplicationConfigurations).Assembly);
        }

        return builder.Build();
    }
}