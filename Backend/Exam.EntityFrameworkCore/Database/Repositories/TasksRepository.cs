using Exam.Database.DbContexts;
using Exam.Database.DbContexts.Interface;
using Exam.Database.Base;
using Exam.Database.Interface;
using Exam.Database.Repositories.Abstract;

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