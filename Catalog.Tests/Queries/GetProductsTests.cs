using AutoMapper;
using BasketProject.Contracts.Entities;
using Catalog.Contracts.Abstracts;
using Catalog.Contracts.Entities;
using Catalog.Contracts.Queries;
using Catalog.QueryHandlers;
using Catalog.Specifications;
using Core.Interfaces;
using Moq;

namespace Catalog.Tests.Queries
{
    [TestFixture]
    public class GetProductsTests
    {
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IUriComposer> _mockUriComposer;

        [SetUp]
        public void Init()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockUriComposer = new Mock<IUriComposer>();
        }

        [Test]
        public async Task InvokesProductRepositoryListAsyncOnce()
        {
            var request = new GetProductsQuery
            {
               BasketItems = new List<BasketItem>
               {
                   new BasketItem(1, 1, 10)
               }
            };

            _mockProductRepository.Setup(x => x.ListAsync(It.IsAny<ProductsSpecification>(), default)).ReturnsAsync(GetTestCollection());

            var handler = new GetProductsQueryHandler(_mockProductRepository.Object, _mockUriComposer.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockProductRepository.Verify(x => x.ListAsync(It.IsAny<ProductsSpecification>(), default), Times.Once);
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
