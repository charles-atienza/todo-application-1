using Exam.DbContexts;
using Exam.DbContexts.Interface;
using Exam.Entities.Base;
using Exam.Entities.Interface;
using Exam.Repositories.Base;

namespace Exam.EntityFrameworkCore.Repositories;

/// <summary>
///     Allianz Entity repository
/// </summary>
public class WeatherSummariesRepository : ExamRepositoryBase<WeatherSummaries>, IWeatherSummariesRepository
{
    private readonly ExamDbContext _dbContext;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public WeatherSummariesRepository(IDbContextProvider<ExamDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
        _dbContext = dbContextProvider.GetDbContext();
    }
}