using BasketProject.CommandHandlers;
using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using BasketProject.Contracts.Entities;
using Core.Specifications;
using Moq;

namespace BasketProject.Tests.Commands
{
    [TestFixture]
    public class SetQuantitiesTests
    {
        private Mock<IBasketRepository> _mockBasketRepository;

        private readonly int _basketId = 1;
        private readonly Guid _buyerId = Guid.NewGuid();

        [SetUp]
        public void Init()
        {
            _mockBasketRepository = new Mock<IBasketRepository>();
        }

        [Test]
        public async Task InvokesBasketRepositoryUpdateAsyncOnce()
        {
            var request = new SetQuantitiesCommand
            {
                Id = _basketId,
                Items = new Dictionary<string, byte>
                {
                    { "1", 1 },
                    { "2", 1 }
                }
            };

            var basket = new Basket(_buyerId);
            basket.AddItem(1, It.IsAny<decimal>(), It.IsAny<byte>());
            basket.AddItem(2, It.IsAny<decimal>(), It.IsAny<byte>());

            _mockBasketRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<BaseSpecification<Basket>>(), default)).ReturnsAsync(basket);

            var handler = new SetQuantitiesCommandHandler(_mockBasketRepository.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.UpdateAsync(basket, default), Times.Once);
        }

        [Test]
        public async Task DoesNotInvokeBasketRepositoryUpdateAsyncIfBasketIsEmpty()
        {
            var request = new SetQuantitiesCommand
            {
                Id = _basketId
            };

            var basket = null as Basket;

            _mockBasketRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<BaseSpecification<Basket>>(), default)).ReturnsAsync(basket);

            var handler = new SetQuantitiesCommandHandler(_mockBasketRepository.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.UpdateAsync(It.IsAny<Basket>(), default), Times.Never);
        }
    }
}
