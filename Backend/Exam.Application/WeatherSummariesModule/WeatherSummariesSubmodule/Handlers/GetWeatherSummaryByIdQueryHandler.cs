using Exam.DataTransferObjects.WeatherSummariesModule.WeatherSummariesSubModule;
using Exam.Entities.Base;
using Exam.Entities.Interface;
using Exam.Mappers.WeatherSummariesMappers;
using Exam.WeatherSummariesPage.Queries;
using MediatR;

namespace Exam.WeatherSummariesPage.Handlers;

/// <summary>
///     Handler for GetWeatherForecastQuery
/// </summary>
public class GetWeatherSummaryByIdQueryHandler : IRequestHandler<GetWeatherSummaryByIdQuery, WeatherForecastDto>
{
    private readonly IWeatherSummariesMapper _weatherSummariesMapper;
    private readonly IWeatherSummariesRepository _weatherSummariesRepository;

    /// <summary>
    ///     Base constructor
    /// </summary>
    /// <param name="weatherSummariesMapper"></param>
    /// <param name="weatherSummariesRepository"></param>
    public GetWeatherSummaryByIdQueryHandler(
        IWeatherSummariesMapper weatherSummariesMapper,
        IWeatherSummariesRepository weatherSummariesRepository
    )
    {
        _weatherSummariesMapper = weatherSummariesMapper;
        _weatherSummariesRepository = weatherSummariesRepository;
    }

    /// <summary>
    ///     Get WeatherForecast Data
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<WeatherForecastDto> Handle(
        GetWeatherSummaryByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await _weatherSummariesRepository.GetAsync(request.Id);
        var dto = _weatherSummariesMapper.ToWeatherForecastDto(result);

        return dto;
    }
}