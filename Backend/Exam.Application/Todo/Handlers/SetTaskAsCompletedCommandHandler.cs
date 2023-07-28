using Exam.Database.Interface;
using Exam.Mappers.Interface;
using Exam.Todo.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Todo.Handlers;

/// <summary>
///     Handler for GetWeatherForecastQuery
/// </summary>
public class SetTaskAsCompletedCommandHandler : IRequestHandler<SetTaskAsCompletedCommand, TasksDto>
{
    private readonly ITasksMapper _tasksMapper;
    private readonly ITasksRepository _tasksRepository;


    /// <summary>Base Constructor</summary>
    /// <param name="tasksMapper">The tasks mapper.</param>
    /// <param name="tasksRepository">The tasks repository.</param>
    public SetTaskAsCompletedCommandHandler(
        ITasksMapper tasksMapper,
        ITasksRepository tasksRepository
    )
    {
        _tasksMapper = tasksMapper;
        _tasksRepository = tasksRepository;
    }

    public async Task<TasksDto> Handle(SetTaskAsCompletedCommand request, CancellationToken cancellationToken)
    {
        var toUpdate = await _tasksRepository.GetAll()
                        .Where(x => x.Id == request.Id)
                        .FirstOrDefaultAsync(cancellationToken);

        if (toUpdate == null)
        {
            throw new ArgumentException("Selected task cannot be found.");
        }

        toUpdate.IsCompleted = true;
        await _tasksRepository.UpdateAsync(toUpdate);
        await _tasksRepository.SaveChangesAsync();

        return _tasksMapper.ToTasksDto(toUpdate);
    }
}