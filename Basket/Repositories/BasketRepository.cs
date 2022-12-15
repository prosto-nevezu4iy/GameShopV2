using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BasketProject.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly BasketDbContext _dbContext;

        public BasketRepository(BasketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Basket> AddAsync(Basket entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Baskets.Add(entity);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task UpdateAsync(Basket entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Baskets.Update(entity);

            await SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Basket entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Baskets.Remove(entity);

            await SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Basket?> GetByIdAsync(int id)
        {
            return await _dbContext.Baskets.FindAsync(id);
        }

        public async Task<List<Basket>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Baskets.ToListAsync(cancellationToken);
        }

        public async Task<List<Basket>> ListAsync(ISpecification<Basket> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<Basket> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).CountAsync(cancellationToken);
        }

        public async Task<Basket?> FirstOrDefaultAsync(ISpecification<Basket> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        private IQueryable<Basket> ApplySpecification(ISpecification<Basket> spec)
        {
            return SpecificationEvaluator<Basket>.GetQuery(_dbContext.Baskets.AsQueryable(), spec);
        }
    }
}
