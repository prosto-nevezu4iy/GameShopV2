using Catalog.Contracts.Entities;
using Core.Interfaces;

namespace Catalog
{
    public interface IProductRepository
    {
        Task<IList<Product>> ListAsync(CancellationToken cancellationToken = default);
        Task<IList<Product>> ListAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(int id);
    }
}
