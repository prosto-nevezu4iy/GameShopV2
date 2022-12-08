using Catalog.Contracts.DTO;

namespace Catalog.Contracts.Abstracts
{
    public interface IGenreRepository
    {
        Task<List<GenreItemDto>> ListAsync(CancellationToken cancellationToken = default);
    }
}
