using BasketProject.Contracts.DTO;
using BasketProject.Contracts.Entities;
using BasketProject.Contracts.Exceptions;
using BasketProject.Contracts.Queries;
using Catalog.Contracts.Queries;
using Core.Interfaces;
using MediatR;
using Moq;
using OrderProject.CommandHandlers;
using OrderProject.Contracts.Abstracts;
using OrderProject.Contracts.Commands;
using OrderProject.Contracts.Entities;
using OrderProject.Tests.Entities;

namespace OrderProject.Tests.Commands
{
    [TestFixture]
    public class CreateOrderTests
    {
        private Mock<IOrderRepository> _mockOrderRepository;
        private Mock<IMediator> _mockMediator;

        [SetUp]
        public void Init()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockMediator = new Mock<IMediator>();
        }

        [Test]
        public async Task InvokesOrderRepositoryAddAsyncOnce()
        {
            var address = new AddressBuilder().WithDefaultValues();
            var basket = new BasketBuilder().WithOneBasketItem();
            var basketItems = basket.Items.Select(x => new BasketItemDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                ProductName = "test",
                PictureUrl = "testurl"
            });

            var request = new CreateOrderCommand
            {
                BasketId = 1,
                ShippingAddress = address
            };

            _mockMediator.Setup(x => x.Send(It.IsAny<GetBasketByIdQuery>(), default)).ReturnsAsync(basket);
            _mockMediator.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), default)).ReturnsAsync(basketItems);

            var handler = new CreateOrderCommandHandler(_mockMediator.Object, _mockOrderRepository.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockOrderRepository.Verify(x => x.AddAsync(It.IsAny<Order>(), default), Times.Once);
        }

        [Test]
        public void ThrowsArgumentNullExceptionIfBasketIsEmpty()
        {
            var address = new AddressBuilder().WithDefaultValues();
            var basket = null as Basket;

            var request = new CreateOrderCommand
            {
                BasketId = 1,
                ShippingAddress = address
            };

            _mockMediator.Setup(x => x.Send(It.IsAny<GetBasketByIdQuery>(), default)).ReturnsAsync(basket);

            var handler = new CreateOrderCommandHandler(_mockMediator.Object, _mockOrderRepository.Object);

            Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(request, CancellationToken.None));
        }

        [Test]
        public void ThrowsEmptyBasketOnCheckoutExceptionIfBasketItemsAreEmpty()
        {
            var address = new AddressBuilder().WithDefaultValues();
            var basket = new BasketBuilder().WithNoItems();

            var request = new CreateOrderCommand
            {
                BasketId = 1,
                ShippingAddress = address
            };

            _mockMediator.Setup(x => x.Send(It.IsAny<GetBasketByIdQuery>(), default)).ReturnsAsync(basket);

            var handler = new CreateOrderCommandHandler(_mockMediator.Object, _mockOrderRepository.Object);

            Assert.ThrowsAsync<EmptyBasketOnCheckoutException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
