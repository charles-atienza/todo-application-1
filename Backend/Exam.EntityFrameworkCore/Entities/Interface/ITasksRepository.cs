using Exam.Database.Repositories.Interface;

namespace Exam.Database.Interface;

/// <summary>
///     Weather Summaries Entity repository
/// </summary>
public interface ITasksRepository : IRepository<Base.Task, int>
{
}