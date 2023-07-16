using Exam.Entities.Interface;
using Exam.Mappers.Interface;
using Exam.Todo.Queries;
using MediatR;

namespace Exam.Todo.Handlers;

/// <summary>
///     Handler for GetWeatherForecastQuery
/// </summary>
public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<TasksDto>>
{
    private readonly ITasksMapper _tasksMapper;
    private readonly ITasksRepository _tasksRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tasksMapper"></param>
    /// <param name="tasksRepository"></param>
    public GetAllTasksQueryHandler(
        ITasksMapper tasksMapper,
        ITasksRepository tasksRepository
    )
    {
        _tasksMapper = tasksMapper;
        _tasksRepository = tasksRepository;
    }

    public async Task<List<TasksDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _tasksRepository.GetAllAsync();
        return _tasksMapper.ToTasksDto(tasks);
    }
}