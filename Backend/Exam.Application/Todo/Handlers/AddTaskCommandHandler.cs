using Exam.Entities.Interface;
using Exam.Mappers.Interface;
using Exam.Todo.Queries;
using MediatR;

namespace Exam.Todo.Handlers;

/// <summary>
///     Handler for GetWeatherForecastQuery
/// </summary>
public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, TasksDto>
{
    private readonly ITasksMapper _tasksMapper;
    private readonly ITasksRepository _tasksRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tasksMapper"></param>
    /// <param name="tasksRepository"></param>
    public AddTaskCommandHandler(
        ITasksMapper tasksMapper,
        ITasksRepository tasksRepository
    )
    {
        _tasksMapper = tasksMapper;
        _tasksRepository = tasksRepository;
    }

    public async Task<TasksDto> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        var newTask = _tasksMapper.ToTaskEntity(request);
        var id = await _tasksRepository.InsertAndGetIdAsync(newTask);

        if (id == 0)
        {
            throw new Exception("Error while creating new task");
        }
        newTask.Id = id;

        return _tasksMapper.ToTasksDto(newTask);
    }
}