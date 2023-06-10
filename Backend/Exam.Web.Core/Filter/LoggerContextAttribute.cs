using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Exam.Web.Filter;

/// <summary>
///     Logger Context Attribute
/// </summary>
public class LoggerContextAttribute : ActionFilterAttribute
{
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="loggerFactory"></param>
    public LoggerContextAttribute(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger<LoggerContextAttribute>();
    }

    private ILogger Logger { get; }

    /// <summary>
    ///     Intercept the HTTP request just before entering the controller
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var paramsMap = context.ActionArguments as IDictionary<string, object>;
        var inputPayload = DeserializeInputParameters(paramsMap);
        var endPoint = GetEndpoint(context.HttpContext);

        Logger.LogDebug($"[*ACTION_START] '{endPoint}' -> Params:{inputPayload}");
    }

    /// <summary>
    ///     Intercept the HTTP request right after the controller returns the response
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var isExceptionNull = context.Exception is null;
        var endPoint = GetEndpoint(context.HttpContext);
        var message = $"[*ACTION_END  ] '{endPoint}' -> " +
                      $"{(!isExceptionNull ? "Return an error -" : "Return OK200 -")})";
        Logger.LogDebug(message);

        Debug.WriteIf(!isExceptionNull,
            $"-> [*ACTION_ERROR] at '{endPoint}' \n-> {context!.Exception!.Message} \n-> {context.Exception.InnerException?.Message ?? "No additional info given."}");
    }

    /// <summary>
    ///     Deserialize input parameters
    /// </summary>
    /// <param name="paramsMap"></param>
    /// <returns></returns>
    private static string DeserializeInputParameters(IDictionary<string, object> paramsMap)
    {
        return JsonConvert.SerializeObject(paramsMap.Values);
    }

    /// <summary>
    ///     Resolve endpoint url
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private static string GetEndpoint(HttpContext context)
    {
        return context.Request.GetDisplayUrl();
    }
}