using Exam.Web.Helpers;

namespace Exam.Web.Startup;

public class Program
{
    public static void Main(string[] args)
    {
        CurrentDirectoryHelpers.SetCurrentDirectory();
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        return new WebHostBuilder()
            .UseKestrel(opt =>
            {
                opt.AddServerHeader = false;
                opt.Limits.MaxRequestLineSize = 16 * 1024;
            })
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureLogging((context, logging) =>
            {
                var logLevel = context.HostingEnvironment.IsDevelopment() ? LogLevel.Debug : LogLevel.Warning;
                logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", logLevel);
            })
            .UseDefaultServiceProvider(options => options.ValidateScopes = false)
            .UseIIS()
            .UseIISIntegration()
            .UseStartup<Startup>();
    }
}