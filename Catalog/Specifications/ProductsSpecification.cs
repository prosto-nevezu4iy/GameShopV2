using Catalog.Contracts.Entities;
using Core.Specifications;

namespace Catalog.Specifications
{
    public class ProductsSpecification : BaseSpecification<Product>
    {
        public ProductsSpecification(params int[] ids) : base(p => ids.Contains(p.Id))
        {
        }
    }
}
