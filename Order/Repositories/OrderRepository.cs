using BasketProject.Contracts.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using OrderProject.Contracts.Abstracts;
using OrderProject.Contracts.Entities;

namespace OrderProject.Repositories
{
    public class OrderRepository : IOrderRepository
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

        public async Task<List<Order>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Orders.ToListAsync(cancellationToken);
        }

        public async Task<List<Order>> ListAsync(ISpecification<Order> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken);
        }

        public async Task<Order?> FirstOrDefaultAsync(ISpecification<Order> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private IQueryable<Order> ApplySpecification(ISpecification<Order> spec)
        {
            return SpecificationEvaluator<Order>.GetQuery(_dbContext.Orders.AsQueryable(), spec);
        }
    }
}
