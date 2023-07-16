using Exam.DbContexts;
using Exam.DbContexts.Interface;
using Exam.Entities.Base;
using Exam.Entities.Interface;
using Exam.Repositories.Base;

namespace Exam.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity repository
/// </summary>
public class TasksRepository : ExamRepositoryBase<Tasks>, ITasksRepository
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