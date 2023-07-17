using Exam.Web.Utilities.Common;
using FluentValidation;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System.Net;

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
        var response = new GenericResponse<string>();
        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var message = string.Join(", ", validationException.Errors.Select(x => x.ErrorMessage).ToArray());
            response = new GenericResponse<string>
            {
                Error = new GenericError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = message ?? string.Empty,
                    Details = validationException.Message,
                    ValidationErrors = validationException?.Errors?.ToList()
                },
                Success = false
            };
        }
        else
        {
            var isArgumentException = exception is ArgumentException;
            var message = "An error occurred while processing your request.";
            var statusCode =
                (int)(isArgumentException ? HttpStatusCode.BadRequest : HttpStatusCode.InternalServerError);
            var details = isArgumentException ? exception.Message : string.Empty;

            httpContext.Response.StatusCode = statusCode;
            response = new GenericResponse<string>
            {
                Error = new GenericError
                {
                    Code = statusCode,
                    Message = message,
                    Details = details
                },
                Success = false
            };
        }

        var jsonResponse = JsonConvert.SerializeObject(response);
        await httpContext.Response.WriteAsync(jsonResponse);
    }
}