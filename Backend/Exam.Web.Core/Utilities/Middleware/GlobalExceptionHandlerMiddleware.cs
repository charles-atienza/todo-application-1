using Exam.Extensions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Exam.Web.Utilities.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="next"></param>
    /// <param name="loggerFactory"></param>
    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        Logger = loggerFactory.CreateLogger<GlobalExceptionHandlerMiddleware>();
    }

    private ILogger Logger { get; }

    /// <summary>
    ///     Intercept request and handle any exception
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            Logger.LogError("[*GLOBAL_ERROR_L1*] in {@Url} -> {@Exception}", httpContext.Request.GetDisplayUrl(),
                exception);
            //Generic message 
            await CreateErrorHttpResponse(httpContext, exception!);
        }
    }

    /// <summary>
    ///     Create error http response
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="exception"></param>
    /// <returns></returns>
    private static async Task CreateErrorHttpResponse(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.Clear();
        httpContext.Response.ContentType = "application/json";

        int statusCode;
        string errorMessage;

        if (exception is UserFriendlyException userFriendlyException)
        {
            statusCode = userFriendlyException.StatusCode;
            errorMessage = userFriendlyException.Message;
        }
        else
        {
            statusCode = 500;
            errorMessage = "An unhandled error occurred, please wait or contact an Admin.";
        }

        httpContext.Response.StatusCode = statusCode;

        var response = new ObjectResult(errorMessage)
        {
            StatusCode = statusCode
        };

        var jsonResponse = JsonConvert.SerializeObject(response);
        await httpContext.Response.WriteAsync(jsonResponse);
    }
}