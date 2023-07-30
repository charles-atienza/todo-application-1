using Exam.Database.DbContexts;
using Exam.Database.DbContexts.Interface;
using Exam.Database.Interface;
using Exam.Database.Repositories.Abstract;

namespace Exam.Database.Repositories;

/// <summary>
///     Entity repository
/// </summary>
public class TasksRepository : ExamRepositoryBase<Database.Base.Task>, ITasksRepository
{
    private readonly ExamDbContext _dbContext;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public TasksRepository(IDbContextProvider<ExamDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
        _dbContext = dbContextProvider.GetDbContext();
    }
}