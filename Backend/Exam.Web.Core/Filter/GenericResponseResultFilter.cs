using System.Text;
using System.Text.Json;
using Exam.Web.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Exam.Web.Filter;

public class GenericResponseResultFilter : IResultFilter
{
    /// <summary>
    ///     This method is called before the action method is executed.
    ///     You can modify the result here if you need to.
    /// </summary>
    /// <param name="context"></param>
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult originalResult)
        {
            var genericResponse = new GenericResponse<object>();

            if (originalResult.Value is GenericResponse<object>)
            {
                genericResponse = (GenericResponse<object>)originalResult.Value;
            }
            else
            {
                genericResponse.Success = true;
                genericResponse.Result = originalResult.Value;
            }

            if (context.Result is ObjectResult objectResult)
            {
                objectResult.Value = genericResponse;
            }

            var response = context.HttpContext.Response;
            response.ContentLength = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(genericResponse)).Length;
            response.ContentType = "application/json";
        }
    }

    /// <summary>
    ///     This method is called after the action method has executed and
    ///     the result has been produced. You can modify the result here if you need to.
    /// </summary>
    /// <param name="context"></param>
    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}