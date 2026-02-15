namespace TaskHub.Infrastructure.ReadModels;

public sealed class TaskListRead
{
    public Guid TaskId { get; set; }
    public string Title { get; set; } = "";
    public bool IsDone { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
