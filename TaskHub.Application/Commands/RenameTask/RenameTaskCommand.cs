namespace TaskHub.Application.Commands.RenameTask;

public sealed record RenameTaskCommand(Guid TaskId, string NewTitle);
