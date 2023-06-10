using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers;

/// <summary>
///     Exam Controller base
/// </summary>
[ApiVersion("1.0")]
[Route("/api/v1.0/services/[controller]/[action]")]
public abstract class ExamControllerBase : Controller
{
}