using System.Text;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Exam.Extensions;

public static class SwaggerExtensions
{
    /// <summary>
    ///     A cleaner way to add pathBase to Swagger UI.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="pathBase">base path (URL) to application API</param>
    public static void InjectBaseUrl(this SwaggerUIOptions options, string pathBase)
    {
        pathBase = pathBase.EnsureEndsWith('/');

        options.HeadContent = new StringBuilder(options.HeadContent)
            .AppendLine($"<script>'{pathBase}';</script>")
            .ToString();
    }
}