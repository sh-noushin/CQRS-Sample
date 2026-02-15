using TaskHub.Application.Commands.CreateTask;
using TaskHub.Application.Commands.CompleteTask;
using TaskHub.Application.Commands.RenameTask;
using TaskHub.Application.Commands.DeleteTask;
using TaskHub.Application.Queries.GetTaskList;

namespace TaskHub.Api.Setup;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapTaskHubEndpoints(this IEndpointRouteBuilder app)
    {
        // COMMAND: Create
        app.MapPost("/api/tasks", async (
            CreateTaskCommand cmd,
            CreateTaskHandler handler,
            CancellationToken ct) =>
        {
            var id = await handler.HandleAsync(cmd, ct);
            return Results.Created($"/api/tasks/{id}", new { id });
        });

        // COMMAND: Complete
        app.MapPut("/api/tasks/{id:guid}/complete", async (
            Guid id,
            CompleteTaskHandler handler,
            CancellationToken ct) =>
        {
            await handler.HandleAsync(new CompleteTaskCommand(id), ct);
            return Results.NoContent();
        });

        // COMMAND: Rename
        app.MapPut("/api/tasks/{id:guid}/rename", async (
            Guid id,
            RenameTaskRequest body,
            RenameTaskHandler handler,
            CancellationToken ct) =>
        {
            await handler.HandleAsync(new RenameTaskCommand(id, body.NewTitle), ct);
            return Results.NoContent();
        });

        // COMMAND: Delete
        app.MapDelete("/api/tasks/{id:guid}", async (
            Guid id,
            DeleteTaskHandler handler,
            CancellationToken ct) =>
        {
            await handler.HandleAsync(new DeleteTaskCommand(id), ct);
            return Results.NoContent();
        });

        // QUERY: List
        app.MapGet("/api/tasks", async (
            bool? isDone,
            GetTaskListHandler handler,
            CancellationToken ct) =>
        {
            var items = await handler.HandleAsync(new GetTaskListQuery(isDone), ct);
            return Results.Ok(items);
        });

        return app;
    }

    public sealed record RenameTaskRequest(string NewTitle);
}
