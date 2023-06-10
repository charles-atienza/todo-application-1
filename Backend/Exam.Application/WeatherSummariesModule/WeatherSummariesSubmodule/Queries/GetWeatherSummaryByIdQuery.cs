using Exam.DataTransferObjects.WeatherSummariesModule.WeatherSummariesSubModule;
using MediatR;

namespace Exam.WeatherSummariesPage.Queries;

/// <summary>
///     Get Weather Forecast Query
/// </summary>
public record GetWeatherSummaryByIdQuery : IRequest<WeatherForecastDto>
{
    public int Id { get; init; }
}