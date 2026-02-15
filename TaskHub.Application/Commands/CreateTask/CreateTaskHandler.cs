using TaskHub.Application.Abstractions;
using TaskHub.Domain.Entities;
using TaskHub.Infrastructure.Persistence;
using TaskHub.Infrastructure.ReadModels;

namespace TaskHub.Application.Commands.CreateTask;

public sealed class CreateTaskHandler : ICommandHandler<CreateTaskCommand, Guid>
{
    private readonly AppDbContext _db;
    public CreateTaskHandler(AppDbContext db) => _db = db;

    public async Task<Guid> HandleAsync(CreateTaskCommand command, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.Title))
            throw new InvalidOperationException("Title is required.");

        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = command.Title.Trim(),
            IsDone = false,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _db.TaskItems.Add(task);

        _db.TaskListRead.Add(new TaskListRead
        {
            TaskId = task.Id,
            Title = task.Title,
            IsDone = task.IsDone,
            CreatedAt = task.CreatedAt
        });

        await _db.SaveChangesAsync(ct);
        return task.Id;
    }
}
