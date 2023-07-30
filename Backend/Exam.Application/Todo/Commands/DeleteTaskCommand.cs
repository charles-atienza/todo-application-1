using MediatR;

namespace Exam.Todo.Queries;

/// <summary>
///     Get all Task
/// </summary>
public record DeleteTaskCommand : IRequest<bool>
{
    public int Id { get; init; }
}