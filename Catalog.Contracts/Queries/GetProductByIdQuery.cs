using Catalog.Contracts.Entities;
using Catalog.Contracts.QueryModels;
using MediatR;

namespace Catalog.Contracts.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
    }
}
