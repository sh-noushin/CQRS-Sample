using Microsoft.EntityFrameworkCore;
using TaskHub.Application.Abstractions;
using TaskHub.Infrastructure.Persistence;
using TaskHub.Infrastructure.ReadModels;

namespace TaskHub.Application.Queries.GetTaskList;

public sealed class GetTaskListHandler : IQueryHandler<GetTaskListQuery, List<TaskListRead>>
{
    private readonly AppDbContext _db;

    public GetTaskListHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TaskListRead>> HandleAsync(GetTaskListQuery query, CancellationToken ct)
    {
        var q = _db.TaskListRead.AsNoTracking().AsQueryable();

        if (query.IsDone is not null)
            q = q.Where(x => x.IsDone == query.IsDone.Value);

        return await q
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(ct);
    }
}
