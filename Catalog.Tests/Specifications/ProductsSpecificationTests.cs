using Catalog.Contracts.Entities;
using Catalog.Specifications;
using Moq;

namespace Catalog.Tests.Specifications
{
    [TestFixture]
    public class ProductsSpecificationTests
    {
        [Test]
        public void MatchesSpecificProduct()
        {
            var catalogItemIds = new int[] { 1 };
            var spec = new ProductsSpecification(catalogItemIds);

            var result = GetTestCollection()
               .AsQueryable()
               .Where(spec.Criteria)
               .ToList();

            Assert.NotNull(result);
            Assert.That(result, Has.Exactly(1).Items);
        }

        [Test]
        public void MatchesAllProducts()
        {
            var catalogItemIds = new int[] { 1, 3 };
            var spec = new ProductsSpecification(catalogItemIds);

            var result = GetTestCollection()
                          .AsQueryable()
                          .Where(spec.Criteria)
                          .ToList();

            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        private List<Product> GetTestCollection()
        {
            var catalogItems = new List<Product>();

            var mockCatalogItem1 = new Mock<Product>(1, "Item 1", "Item 1 description", 1.5m, "Item1Uri");
            mockCatalogItem1.SetupGet(x => x.Id).Returns(1);

            var mockCatalogItem3 = new Mock<Product>(3, "Item 3", "Item 3 description", 3.5m, "Item3Uri");
            mockCatalogItem3.SetupGet(x => x.Id).Returns(3);

            catalogItems.Add(mockCatalogItem1.Object);
            catalogItems.Add(mockCatalogItem3.Object);

            return catalogItems;
        }
    }
}
