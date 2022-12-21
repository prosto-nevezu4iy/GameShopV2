using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using OrderProject.Contracts.Abstracts;
using OrderProject.Contracts.Entities;

namespace OrderProject.Repositories
{
    public class OrderRepository : SpecificationEvaluator<Order>, IOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Orders.Add(entity);

            await SaveChangesAsync(cancellationToken);
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task<IList<Order>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Orders.ToListAsync(cancellationToken);
        }

        public async Task<IList<Order>> ListAsync(ISpecification<Order> specification, CancellationToken cancellationToken = default)
        {
            return await GetQuery(_dbContext.Orders.AsQueryable(), specification).ToListAsync(cancellationToken);
        }

        public async Task<Order?> FirstOrDefaultAsync(ISpecification<Order> specification, CancellationToken cancellationToken = default)
        {
            return await GetQuery(_dbContext.Orders.AsQueryable(), specification).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
