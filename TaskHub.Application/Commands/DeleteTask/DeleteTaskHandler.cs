using Microsoft.EntityFrameworkCore;
using TaskHub.Application.Abstractions;
using TaskHub.Infrastructure.Persistence;

namespace TaskHub.Application.Commands.DeleteTask;

public sealed class DeleteTaskHandler : Handlers<DeleteTaskCommand>
{
    private readonly AppDbContext _db;
    public DeleteTaskHandler(AppDbContext db) => _db = db;

    public async Task HandleAsync(DeleteTaskCommand command, CancellationToken ct)
    {
        var task = await _db.TaskItems.FirstOrDefaultAsync(x => x.Id == command.TaskId, ct);
        if (task is null)
            throw new InvalidOperationException("Task not found.");

        var readRow = await _db.TaskListRead.FirstOrDefaultAsync(x => x.TaskId == command.TaskId, ct);

        _db.TaskItems.Remove(task);

        if (readRow is not null)
            _db.TaskListRead.Remove(readRow);

        await _db.SaveChangesAsync(ct);
    }
}
