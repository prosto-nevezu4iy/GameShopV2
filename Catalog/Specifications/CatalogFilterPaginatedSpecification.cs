using Catalog.Contracts.Entities;
using Core.Specifications;

namespace Catalog.Specifications
{
    public class CatalogFilterPaginatedSpecification : BaseSpecification<Product>
    {
        public CatalogFilterPaginatedSpecification(int skip, int take, int? genreId)
            : base(p => !genreId.HasValue || p.GenreId == genreId)
        {
            ApplyPaging(skip, take);
        }
    }
}
