using AutoMapper;
using BasketProject.CommandHandlers;
using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using BasketProject.Contracts.Entities;
using MediatR;
using Moq;

namespace BasketProject.Tests.Commands
{
    [TestFixture]
    public class DeleteBasketTests
    {
        private Mock<IBasketRepository> _mockBasketRepository;
        private Mock<Basket> _mockBasket;

        private readonly int _basketId = 1;
        private readonly Guid _buyerId = Guid.NewGuid();

        [SetUp]
        public void Init()
        {
            _mockBasketRepository = new Mock<IBasketRepository>();
        }

        [Test]
        public async Task ShouldInvokeBasketRepositoryDeleteAsyncOnce()
        {
            var request = new DeleteBasketCommand
            {
                BasketId = _basketId,
            };

            var basket = new Basket(_buyerId);
            basket.AddItem(1, It.IsAny<decimal>(), It.IsAny<byte>());
            basket.AddItem(2, It.IsAny<decimal>(), It.IsAny<byte>());

            _mockBasketRepository.Setup(x => x.GetByIdAsync(_basketId)).ReturnsAsync(basket);

            var handler = new DeleteBasketCommandHandler(_mockBasketRepository.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.DeleteAsync(basket, default), Times.Once);
        }

        [Test]
        public async Task ShouldNotInvokeBasketRepositoryDeleteAsyncIfEmptyBasket()
        {
            var request = new DeleteBasketCommand
            {
                BasketId = _basketId,
            };

            var handler = new DeleteBasketCommandHandler(_mockBasketRepository.Object);

            Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
