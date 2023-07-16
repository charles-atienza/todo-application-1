using MediatR;

#nullable disable
namespace Exam.Todo.Queries;

/// <summary>
///     Get all Tasks
/// </summary>
public record AddTaskCommand : IRequest<TasksDto>
{
    public string Name { get; init; }
}