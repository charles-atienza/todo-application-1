using Exam.Utilities.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Exam.Extensions;

public static class ConfigRootExtension
{
    public static IConfigurationRoot GetAppConfiguration(this IWebHostEnvironment env)
    {
        return AppplicationConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
    }
}