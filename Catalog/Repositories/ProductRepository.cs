using Catalog.Contracts.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories
{
    public class ProductRepository : SpecificationEvaluator<Product>, IProductRepository
    {
        private readonly CatalogDbContext _dbContext;

        public ProductRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default)
        {
            return
                await GetQuery(_dbContext.Products.AsQueryable(), specification)
                    .CountAsync(cancellationToken);
        }

        public async Task<IList<Product>> ListAsync(CancellationToken cancellationToken = default)
        {
            return 
                await _dbContext.Products
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }

        public async Task<IList<Product>> ListAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default)
        {
            return 
                await GetQuery(_dbContext.Products.AsQueryable(), specification)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }
    }
}
