using Exam.DataTransferObjects.WeatherSummariesModule.WeatherSummariesSubModule;
using Exam.Entities.Base;
using Riok.Mapperly.Abstractions;

namespace Exam.Mappers.WeatherSummariesMappers;

[Mapper]
public partial class WeatherSummariesMapper : IWeatherSummariesMapper
{
    [MapProperty(nameof(WeatherSummaries.Name), nameof(WeatherForecastDto.Summary))]
    public partial WeatherForecastDto ToWeatherForecastDto(WeatherSummaries? entity);
}