using Microsoft.EntityFrameworkCore;
using Order.Contracts.Abstracts;
using OrderEntity = Order.Contracts.Entities.Order;

namespace Order.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderEntity> AddAsync(OrderEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Orders.Add(entity);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
