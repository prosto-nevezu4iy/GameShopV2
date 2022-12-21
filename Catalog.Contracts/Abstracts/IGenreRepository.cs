using Catalog.Contracts.Entities;

namespace Catalog.Contracts.Abstracts
{
    public interface IGenreRepository
    {
        Task<IList<Genre>> ListAsync(CancellationToken cancellationToken = default);
    }
}
