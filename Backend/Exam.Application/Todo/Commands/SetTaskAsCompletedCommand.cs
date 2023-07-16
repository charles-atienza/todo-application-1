using MediatR;

namespace Exam.Todo.Queries;

/// <summary>
///     Get all Tasks
/// </summary>
public record SetTaskAsCompletedCommand : IRequest<TasksDto>
{
    public int Id { get; init; }
}