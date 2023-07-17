using Exam.Todo.Handlers;
using Exam.Todo.Queries;
using Exam.Web.Controllers.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers;

/// <summary>
///     Controller For Weather Forecast Api
/// </summary>
public class TasksController : BaseController
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

    /// <see cref="GetAllTasksQueryHandler" />
    [HttpGet]
    public async Task<List<TasksDto>> GetAllTasks()
    {
        return await _mediator.Send(new GetAllTasksQuery());
    }

    /// <see cref="AddTaskCommandHandler" />
    [HttpPost]
    public async Task<TasksDto> AddTask([FromQuery] string name)
    {
        return await _mediator.Send(new AddTaskCommand() { Name = name });
    }

    /// <see cref="DeleteTaskCommandHandler" />
    [HttpDelete("{id}")]
    public async Task<bool> DeleteTask([FromRoute] int id)
    {
        return await _mediator.Send(new DeleteTaskCommand() { Id = id });
    }

    /// <see cref="SetTaskAsCompletedCommandHandler" />
    [HttpPost("{id}")]
    public async Task<TasksDto> SetTaskAsCompleted([FromRoute] int id)
    {
        return await _mediator.Send(new SetTaskAsCompletedCommand() { Id = id });
    }


}