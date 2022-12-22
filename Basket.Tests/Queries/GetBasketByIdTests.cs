using BasketProject.CommandHandlers;
using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using BasketProject.Contracts.Entities;
using BasketProject.Contracts.Queries;
using BasketProject.QueryHandlers;
using BasketProject.Specifications;
using Moq;

namespace BasketProject.Tests.Queries
{
    [TestFixture]
    public class GetBasketByIdTests
    {
        private Mock<IBasketRepository> _mockBasketRepository;

        [SetUp]
        public void Init()
        {
            _mockBasketRepository = new Mock<IBasketRepository>();
        }

        [Test]
        public async Task InvokesBasketRepositoryFirstOrDefaultAsyncOnce()
        {
            var request = new GetBasketByIdQuery
            {
                BasketId = 1
            };

            var handler = new GetBasketByIdQueryHandler(_mockBasketRepository.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default), Times.Once);
        }
    }
}
