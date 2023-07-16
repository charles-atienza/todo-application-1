using Exam.Todo.Handlers;
using Exam.Todo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers;

/// <summary>
///     Controller For Weather Forecast Api
/// </summary>
public class TasksController : ExamControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get summary
    /// </summary>
    /// <returns></returns>
    /// <see cref="GetAllTasksQueryHandler" />
    [HttpGet]
    public async Task<List<TasksDto>> GetAllTasks()
    {
        return await _mediator.Send(new GetAllTasksQuery());
    }

    /// <summary>Adds the task.</summary>
    /// <param name="command">The command.</param>
    /// <see cref="AddTaskCommandHandler" />
    [HttpPost]
    public async Task<TasksDto> AddTask([FromQuery] string name)
    {
        return await _mediator.Send(new AddTaskCommand() { Name = name });
    }

    /// <summary>Deletes the task.</summary>
    /// <param name="id">The identifier.</param>
    /// <see cref="DeleteTaskCommandHandler" />
    [HttpGet]
    public async Task<bool> DeleteTask([FromRoute] int id)
    {
        return await _mediator.Send(new DeleteTaskCommand() { Id = id });
    }

    /// <summary>Sets the task as completed.</summary>
    /// <param name="id">The identifier.</param>
    /// <see cref="SetTaskAsCompletedCommandHandler" />
    [HttpPut]
    public async Task<TasksDto> SetTaskAsCompleted([FromRoute] int id)
    {
        return await _mediator.Send(new SetTaskAsCompletedCommand() { Id = id });
    }


}