using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers.Abstract;

[ApiVersion("1.0")]
[Route("/api/v1.0/services/[controller]/[action]")]
public abstract class BaseController : Controller
{
}