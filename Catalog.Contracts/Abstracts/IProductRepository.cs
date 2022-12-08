using Catalog.Contracts.Entities;
using Catalog.Contracts.QueryModels;
using Core.Interfaces;

namespace Catalog
{
    public interface IProductRepository
    {
        Task<List<ProductItemDto>> ListAsync(CancellationToken cancellationToken = default);
        Task<List<ProductItemDto>> ListAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<Product> specification, CancellationToken cancellationToken = default);
    }
}
