using AutoMapper;
using AutoMapper.QueryableExtensions;
using Catalog.Contracts.Abstracts;
using Catalog.Contracts.DTO;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly CatalogDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreRepository(CatalogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GenreItemDto>> ListAsync(CancellationToken cancellationToken = default)
        {
            return 
                await _dbContext.Genres
                    .AsNoTracking()
                    .ProjectTo<GenreItemDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
        }
    }
}
