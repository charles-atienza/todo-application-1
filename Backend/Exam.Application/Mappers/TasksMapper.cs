using Exam.Entities.Base;
using Exam.Mappers.Interface;
using Exam.Todo.Queries;
using Riok.Mapperly.Abstractions;

namespace Exam.Mappers.TasksMappers;

[Mapper]
public partial class TasksMapper : ITasksMapper
{
    public partial Tasks ToTaskEntity(AddTaskCommand addTaskCommand);
    public partial TasksDto ToTasksDto(Tasks? entity);
    public partial List<TasksDto> ToTasksDto(IQueryable<Tasks>? entity);
}