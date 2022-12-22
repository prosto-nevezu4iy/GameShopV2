using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.DTO;
using BasketProject.Contracts.Entities;
using BasketProject.Contracts.Queries;
using BasketProject.QueryHandlers;
using BasketProject.Specifications;
using Core.Specifications;
using MediatR;
using Moq;

namespace BasketProject.Tests.Queries
{
    [TestFixture]
    public class GetBasketTests
    {
        private Mock<IBasketRepository> _mockBasketRepository;
        private Mock<IMediator> _mockMediator;

        private readonly Guid _buyerId = Guid.NewGuid();

        [SetUp]
        public void Init()
        {
            _mockBasketRepository = new Mock<IBasketRepository>();
            _mockMediator = new Mock<IMediator>();
        }

        [Test]
        public async Task InvokesBasketRepositoryFirstOrDefaultAsyncOnce()
        {
            var request = new GetBasketQuery
            {
                UserId = _buyerId
            };

            var handler = new GetBasketQueryHandler(_mockBasketRepository.Object, _mockMediator.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default), Times.Once);
        }

        [Test]
        public async Task ShouldReturnBasketDtoObject()
        {
            var request = new GetBasketQuery
            {
                UserId = _buyerId
            };

            var basket = new Basket(_buyerId);
            basket.AddItem(1, It.IsAny<decimal>(), It.IsAny<byte>());

            _mockBasketRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<BaseSpecification<Basket>>(), default)).ReturnsAsync(basket);

            var handler = new GetBasketQueryHandler(_mockBasketRepository.Object, _mockMediator.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsInstanceOf<BasketDto>(result);
        }
    }
}
