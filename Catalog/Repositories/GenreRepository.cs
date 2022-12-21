using Catalog.Contracts.Abstracts;
using Catalog.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly CatalogDbContext _dbContext;

        public GenreRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Genre>> ListAsync(CancellationToken cancellationToken = default)
        {
            return
                await _dbContext.Genres
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }
    }
}
