using Exam.Mappers.Interface;
using Exam.Todo.Queries;
using Riok.Mapperly.Abstractions;

namespace Exam.Mappers.TasksMappers;

[Mapper]
public partial class TasksMapper : ITasksMapper
{
    public partial Database.Base.Task ToTask(AddTaskCommand addTaskCommand);
    public partial TasksDto ToTasksDto(Database.Base.Task? entity);
    public partial List<TasksDto> ToTasksDto(IQueryable<Database.Base.Task>? entity);
}