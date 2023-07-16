using MediatR;

namespace Exam.Todo.Queries;

/// <summary>
///     Get all Tasks
/// </summary>
public record GetAllTasksQuery : IRequest<List<TasksDto>>
{
}