using MediatR;

namespace Exam.Todo.Queries;

/// <summary>
///     Get all Task
/// </summary>
public record GetAllTasksQuery : IRequest<List<TasksDto>>
{
}