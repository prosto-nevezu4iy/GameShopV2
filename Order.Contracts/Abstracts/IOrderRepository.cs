using Core.Interfaces;
using OrderProject.Contracts.Entities;

namespace OrderProject.Contracts.Abstracts
{
    public interface IOrderRepository
    {
        Task AddAsync(Order entity, CancellationToken cancellationToken = default);
        Task<Order?> FirstOrDefaultAsync(ISpecification<Order> specification, CancellationToken cancellationToken = default);
        Task<List<Order>> ListAsync(CancellationToken cancellationToken = default);
        Task<List<Order>> ListAsync(ISpecification<Order> specification, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
