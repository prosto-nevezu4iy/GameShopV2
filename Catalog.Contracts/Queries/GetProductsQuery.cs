using BasketProject.Contracts.DTO;
using BasketProject.Contracts.Entities;
using MediatR;

namespace Catalog.Contracts.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<BasketItemDto>>
    {
        public IEnumerable<BasketItem> BasketItems { get; set; }
    }
}
