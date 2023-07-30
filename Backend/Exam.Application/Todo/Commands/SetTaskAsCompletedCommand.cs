using MediatR;

namespace Exam.Todo.Queries;

/// <summary>
///     Get all Task
/// </summary>
public record SetTaskAsCompletedCommand : IRequest<TasksDto>
{
    public int Id { get; init; }
}