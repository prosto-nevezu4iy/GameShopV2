using Catalog.Contracts.Entities;
using Core.Specifications;

namespace Catalog.Specifications
{
    public class CatalogFilterSpecification : BaseSpecification<Product>
    {
        public CatalogFilterSpecification(int? genreId)
            : base(p => !genreId.HasValue || p.GenreId == genreId)
        {
        }
    }
}
