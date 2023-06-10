using Exam.DataTransferObjects.WeatherSummariesModule.WeatherSummariesSubModule;
using Exam.WeatherSummariesPage.Handlers;
using Exam.WeatherSummariesPage.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers;

/// <summary>
///     Controller For Weather Forecast Api
/// </summary>
public class WeatherForecastController : ExamControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get summary
    /// </summary>
    /// <returns></returns>
    /// <see cref="GetWeatherSummaryByIdQueryHandler" />
    [HttpGet]
    public async Task<WeatherForecastDto> GetWeatherForecast([FromQuery] GetWeatherSummaryByIdQuery query)
    {
        return await _mediator.Send(query);
    }
}