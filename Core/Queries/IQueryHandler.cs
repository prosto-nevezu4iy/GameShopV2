namespace Core.Queries
{
    public interface IQueryHandler<TQuery, TResponse>
        where TQuery : notnull
    {
        Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);
    }
}
