namespace Core.Queries;

public interface IQueryBus
{
    Task<TResponse> Send<TQuery, TResponse>(TQuery query, CancellationToken ct);
}
