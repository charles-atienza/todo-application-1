using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Exam.Configuration;

public static class HostingEnvironmentExtensions
{
    public static IConfigurationRoot GetAppConfiguration(this IWebHostEnvironment env)
    {
        return AppplicationConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
    }
}