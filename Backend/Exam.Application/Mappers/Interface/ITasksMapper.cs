using Exam.Database.Base;
using Exam.Todo.Queries;

namespace Exam.Mappers.Interface;

public interface ITasksMapper : IMapper
{
    Tasks ToTaskEntity(AddTaskCommand addTaskCommand);
    TasksDto ToTasksDto(Tasks? entity);
    List<TasksDto> ToTasksDto(IQueryable<Tasks>? entity);
}