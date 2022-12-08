using Catalog.Contracts.QueryModels;
using MediatR;

namespace Catalog.Contracts.Queries
{
    public class GetCatalogItemsQuery : IRequest<CatalogItemsDto>
    {
        public int PageIndex { get; set; }
        public int ItemsPage { get; set; }
        public int? GenreId { get; set; }
    }
}
