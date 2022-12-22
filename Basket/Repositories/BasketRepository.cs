using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Entities;
using Core.Interfaces;
using Core.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BasketProject.Repositories
{
    public class BasketRepository : SpecificationEvaluator<Basket>, IBasketRepository
    {
        private readonly BasketDbContext _dbContext;

        public BasketRepository(BasketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Basket entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Baskets.Add(entity);

            await SaveChangesAsync(cancellationToken);
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

        public async Task<IList<Basket>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Baskets.ToListAsync(cancellationToken);
        }

        public async Task<IList<Basket>> ListAsync(ISpecification<Basket> specification, CancellationToken cancellationToken = default)
        {
            return await GetQuery(_dbContext.Baskets.AsQueryable(), specification).ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<Basket> specification, CancellationToken cancellationToken = default)
        {
            return await GetQuery(_dbContext.Baskets.AsQueryable(), specification).CountAsync(cancellationToken);
        }

        public async Task<Basket?> FirstOrDefaultAsync(ISpecification<Basket> specification, CancellationToken cancellationToken = default)
        {
            return await GetQuery(_dbContext.Baskets.AsQueryable(), specification).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> GetBasketCountAsync(Guid userId)
        {
            return await _dbContext.Baskets
               .Where(basket => basket.BuyerId == userId)
               .SelectMany(item => item.Items)
               .CountAsync();
        }
    }
}
