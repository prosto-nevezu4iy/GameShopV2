using Catalog.Contracts.Entities;
using Catalog.Specifications;
using NUnit.Framework;

namespace Catalog.Tests.Specifications
{
    [TestFixture]
    public class CatalogFilterSpecificationTests
    {
        [TestCase(null, 5)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        public void MatchesExpectedNumberOfItems(int? genreId, int expectedCount)
        {
            var spec = new CatalogFilterSpecification(genreId);

            var result = GetTestCollection()
               .AsQueryable()
               .Where(spec.Criteria)
               .ToList();

            Assert.AreEqual(expectedCount, result.Count());
        }

        public List<Product> GetTestCollection()
        {
            return new List<Product>()
            {
                new Product(1, "Name", "Description", 0, "FakePath"),
                new Product(2, "Name", "Description", 0, "FakePath"),
                new Product(3, "Name", "Description", 0, "FakePath"),
                new Product(4, "Name", "Description", 0, "FakePath"),
                new Product(4, "Name", "Description", 0, "FakePath"),
            };
        }
    }
}
