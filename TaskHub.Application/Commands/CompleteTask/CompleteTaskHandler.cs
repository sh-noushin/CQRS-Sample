using TaskHub.Application.Abstractions;
using TaskHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TaskHub.Application.Commands.CompleteTask;

public sealed class CompleteTaskHandler : ICommandHandler<CompleteTaskCommand, Guid>
{
    private readonly AppDbContext _db;
    public CompleteTaskHandler(AppDbContext db) => _db = db;

    public async Task<Guid> HandleAsync(CompleteTaskCommand command, CancellationToken ct)
    {
        var task = await _db.TaskItems.FirstOrDefaultAsync(x => x.Id == command.TaskId, ct);
        if (task is null) throw new InvalidOperationException("Task not found.");

        task.IsDone = true;

        var readRow = await _db.TaskListRead.FirstOrDefaultAsync(x => x.TaskId == command.TaskId, ct);
        if (readRow is not null) readRow.IsDone = true;

        await _db.SaveChangesAsync(ct);
        return task.Id;
    }
}
