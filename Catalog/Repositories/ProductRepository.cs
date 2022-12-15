using AutoMapper;
using AutoMapper.QueryableExtensions;
using Catalog.Contracts.Entities;
using Catalog.Contracts.QueryModels;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(CatalogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CountAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default)
        {
            return 
                await ApplySpecification(specification)
                    .CountAsync(cancellationToken);
        }

        public async Task<List<ProductItemDto>> ListAsync(CancellationToken cancellationToken = default)
        {
            return 
                await _dbContext.Products
                    .AsNoTracking()
                    .ProjectTo<ProductItemDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
        }

        private IQueryable<Product> ApplySpecification(ISpecification<Product> spec)
        {
            return SpecificationEvaluator<Product>.GetQuery(_dbContext.Products.AsQueryable(), spec);
        }

        public async Task<List<ProductItemDto>> ListAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default)
        {
            return 
                await ApplySpecification(specification)
                    .AsNoTracking()
                    .ProjectTo<ProductItemDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }
    }
}
