using Exam.Database.Base;
using Exam.Todo.Queries;

namespace Exam.Mappers.Interface;

public interface ITasksMapper : IMapper
{
    Database.Base.Task ToTask(AddTaskCommand addTaskCommand);
    TasksDto ToTasksDto(Database.Base.Task? entity);
    List<TasksDto> ToTasksDto(IQueryable<Database.Base.Task>? entity);
}