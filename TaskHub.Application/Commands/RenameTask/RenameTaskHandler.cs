using Microsoft.EntityFrameworkCore;
using TaskHub.Application.Abstractions;
using TaskHub.Infrastructure.Persistence;

namespace TaskHub.Application.Commands.RenameTask;

public sealed class RenameTaskHandler : ICommandHandler<RenameTaskCommand, Guid>
{
    private readonly AppDbContext _db;
    public RenameTaskHandler(AppDbContext db) => _db = db;

    public async Task<Guid> HandleAsync(RenameTaskCommand command, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.NewTitle))
            throw new InvalidOperationException("NewTitle is required.");

        var newTitle = command.NewTitle.Trim();

        var task = await _db.TaskItems.FirstOrDefaultAsync(x => x.Id == command.TaskId, ct);
        if (task is null)
            throw new InvalidOperationException("Task not found.");

        task.Title = newTitle;

        var readRow = await _db.TaskListRead.FirstOrDefaultAsync(x => x.TaskId == command.TaskId, ct);
        if (readRow is not null)
            readRow.Title = newTitle;

        await _db.SaveChangesAsync(ct);
        return command.TaskId;
            
    }
}
