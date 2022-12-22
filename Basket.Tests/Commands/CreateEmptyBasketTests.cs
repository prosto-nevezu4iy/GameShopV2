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
    public class CreateEmptyBasketTests
    {
        private Mock<IBasketRepository> _mockBasketRepository;

        private readonly Guid _buyerId = Guid.NewGuid();

        [SetUp]
        public void Init()
        {
            _mockBasketRepository = new Mock<IBasketRepository>();
        }

        [Test]
        public async Task InvokesBasketRepositoryAddAsyncOnce()
        {
            var request = new CreateEmptyBasketCommand
            {
                UserId = _buyerId,
            };

            var handler = new CreateEmptyBasketCommandHandler(_mockBasketRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.AddAsync(It.IsAny<Basket>(), default), Times.Once);
        }
    }
}
