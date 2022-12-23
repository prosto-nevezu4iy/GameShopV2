using AutoMapper;
using Catalog.Contracts.Abstracts;
using Catalog.Contracts.Queries;
using Catalog.QueryHandlers;
using Moq;

namespace Catalog.Tests.Queries
{
    [TestFixture]
    public class GetProductByIdTests
    {
        private Mock<IProductRepository> _mockProductRepository;

        [SetUp]
        public void Init()
        {
            _mockProductRepository = new Mock<IProductRepository>();
        }

        [Test]
        public async Task InvokesProductRepositoryGetByIdAsyncOnce()
        {
            var request = new GetProductByIdQuery
            {
                Id = 1
            };

            var handler = new GetProductByIdQueryHandler(_mockProductRepository.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockProductRepository.Verify(x => x.GetByIdAsync(request.Id), Times.Once);
        }
    }
}
