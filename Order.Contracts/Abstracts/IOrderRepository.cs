using OrderEntity = Order.Contracts.Entities.Order;

namespace Order.Contracts.Abstracts
{
    public interface IOrderRepository
    {
        Task<OrderEntity> AddAsync(OrderEntity entity, CancellationToken cancellationToken = default);
    }
}
