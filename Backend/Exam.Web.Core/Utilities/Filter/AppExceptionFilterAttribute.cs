using Microsoft.AspNetCore.Mvc.Filters;

namespace Exam.Web.Utilities.Filter;

public class AppExceptionFilterAttribute : ExceptionFilterAttribute
{
    public AppExceptionFilterAttribute(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger<AppExceptionFilterAttribute>();
    }

    private ILogger Logger { get; }

    public override void OnException(ExceptionContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var apiRoute = string.Empty;

        var request = context.HttpContext.Request;

        if (request.Path.HasValue)
        {
            var x = request.Path.Value.Split('/');
            if (x.Length > 0)
            {
                apiRoute = x[x.Length - 1].Split('?')[0];
            }
        }

        Logger.LogError(context.Exception, $"{apiRoute} - {context.Exception}");
    }
}