using AutoMapper;
using BasketProject.CommandHandlers;
using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using BasketProject.Contracts.DTO;
using BasketProject.Contracts.Entities;
using BasketProject.Mapping;
using Core.Specifications;
using MediatR;
using Moq;

namespace BasketProject.Tests.Commands
{
    [TestFixture]
    public class CreateBasketTests
    {
        private Mock<IBasketRepository> _mockBasketRepository;
        private Mock<IMediator> _mockMediator;

        private readonly Guid _buyerId = Guid.NewGuid();
        private IMapper _mapper;

        [SetUp]
        public void Init()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BasketMappingProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _mockBasketRepository = new Mock<IBasketRepository>();
            _mockMediator = new Mock<IMediator>();
        }

        [Test]
        public async Task InvokesBasketRepositoryGetBySpecAsyncOnce()
        {
            var request = new CreateBasketCommand
            {
                UserId = _buyerId,
                ProductId = 1,
                Price = 1.50m,
                Quantity = 1
            };

            var basket = new Basket(_buyerId);
            basket.AddItem(1, It.IsAny<decimal>(), It.IsAny<byte>());

            _mockBasketRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<BaseSpecification<Basket>>(), default)).ReturnsAsync(basket);

            var handler = new CreateBasketCommandHandler(_mockBasketRepository.Object, _mapper, _mockMediator.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<BaseSpecification<Basket>>(), default), Times.Once);
        }

        [Test]
        public async Task InvokesBasketRepositoryUpdateAsyncOnce()
        {
            var request = new CreateBasketCommand
            {
                UserId = Guid.NewGuid(),
                ProductId = 1,
                Price = 1.50m,
                Quantity = 1
            };

            var basket = new Basket(_buyerId);
            basket.AddItem(1, It.IsAny<decimal>(), It.IsAny<byte>());

            _mockBasketRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<BaseSpecification<Basket>>(), default)).ReturnsAsync(basket);

            var handler = new CreateBasketCommandHandler(_mockBasketRepository.Object, _mapper, _mockMediator.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.UpdateAsync(basket, default), Times.Once);
        }
    }
}
