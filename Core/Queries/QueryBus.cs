using Microsoft.Extensions.DependencyInjection;

namespace Core.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResponse> Query<TQuery, TResponse>(TQuery query, CancellationToken ct = default) where TQuery : notnull
        {
            var queryHandler =
                       _serviceProvider.GetService<IQueryHandler<TQuery, TResponse>>()
                       ?? throw new InvalidOperationException($"Unable to find handler for Query '{query.GetType().Name}'");

            return queryHandler.Handle(query, ct);
        }
    }
}
