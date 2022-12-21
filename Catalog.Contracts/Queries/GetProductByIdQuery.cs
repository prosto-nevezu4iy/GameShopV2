using Catalog.Contracts.Entities;
using MediatR;

namespace Catalog.Contracts.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
    }
}
