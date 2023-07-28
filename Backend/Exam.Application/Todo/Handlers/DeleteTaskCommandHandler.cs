using Exam.Database.Interface;
using Exam.Todo.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exam.Todo.Handlers;

/// <summary>
///     Handler for GetWeatherForecastQuery
/// </summary>
public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly ITasksRepository _tasksRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tasksMapper"></param>
    /// <param name="tasksRepository"></param>
    public DeleteTaskCommandHandler(
        ITasksRepository tasksRepository
    )
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var toDelete = await _tasksRepository.GetAll()
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (toDelete == null)
        {
            throw new ArgumentException("Cannot find task to delete.");
        }

        await _tasksRepository.DeleteAsync(toDelete);
        await _tasksRepository.SaveChangesAsync();
        return true;
    }
}