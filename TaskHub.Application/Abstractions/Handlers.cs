namespace TaskHub.Application.Abstractions;

public interface Handlers<TCommand>
{
    Task HandleAsync(TCommand command, CancellationToken ct);
}

public interface ICommandHandler<TCommand, TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken ct);
}
public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken ct);
}