namespace TaskHub.Domain.Entities;

public sealed class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public bool IsDone { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
