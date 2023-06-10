using Exam.DataTransferObjects.WeatherSummariesModule.WeatherSummariesSubModule;
using Exam.Entities.Base;

namespace Exam.Mappers.WeatherSummariesMappers;

public interface IWeatherSummariesMapper : IMapper
{
    WeatherForecastDto ToWeatherForecastDto(WeatherSummaries? entity);
}