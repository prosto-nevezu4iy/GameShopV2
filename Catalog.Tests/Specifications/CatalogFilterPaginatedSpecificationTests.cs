using Catalog.Contracts.Entities;
using Catalog.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Tests.Specifications
{
    [TestFixture]
    public class CatalogFilterPaginatedSpecificationTests
    {
        [Test]
        public void ReturnsAllProducts()
        {
            var spec = new CatalogFilterPaginatedSpecification(0, 10, null);

            var result = GetTestCollection()
               .AsQueryable()
               .Where(spec.Criteria)
               .ToList();

            Assert.NotNull(result);
            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void Returns2ProductsWithSameGenreId()
        {
            var spec = new CatalogFilterPaginatedSpecification(0, 10, 1);

            var result = GetTestCollection()
                          .AsQueryable()
                          .Where(spec.Criteria)
                          .ToList();

            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        private List<Product> GetTestCollection()
        {
            var productList = new List<Product>();

            productList.Add(new Product(1, "Item 1", "Item 1", 1.00m, "TestUri1"));
            productList.Add(new Product(1, "Item 1.5", "Item 1.5", 1.50m, "TestUri1"));
            productList.Add(new Product(2, "Item 2", "Item 2", 2.00m, "TestUri2"));
            productList.Add(new Product(3, "Item 3", "Item 3", 3.00m, "TestUri3"));

            return productList;
        }
    }
}
