using BasketProject.CommandHandlers;
using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using BasketProject.Contracts.Entities;
using BasketProject.Specifications;
using Core.Specifications;
using Moq;
using NUnit.Framework;

namespace BasketProject.Tests.Commands
{
    [TestFixture]
    public class TransferBasketTests
    {
        private Mock<IBasketRepository> _mockBasketRepository;

        private readonly Guid _anonymousId = Guid.NewGuid();
        private readonly Guid _buyerId = Guid.NewGuid();

        [SetUp]
        public void Init()
        {
            _mockBasketRepository = new Mock<IBasketRepository>();
        }

        [Test]
        public async Task InvokesBasketRepositoryFirstOrDefaultAsyncOnceIfAnonymousBasketNotExists()
        {
            var request = new TransferBasketCommand
            {
                AnonymousId = _anonymousId.ToString(),
                UserId = _buyerId.ToString()
            };

            var anonymousBasket = null as Basket;
            var userBasket = new Basket(_buyerId);

            _mockBasketRepository.SetupSequence(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default))
                .ReturnsAsync(anonymousBasket)
                .ReturnsAsync(userBasket);

            var handler = new TransferBasketCommandHandler(_mockBasketRepository.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default), Times.Once);
        }

        [Test]
        public async Task TransferAnonymousBasketItemsWhilePreservingExistingUserBasketItems()
        {
            var request = new TransferBasketCommand
            {
                AnonymousId = _anonymousId.ToString(),
                UserId = _buyerId.ToString()
            };

            var anonymousBasket = new Basket(_anonymousId);
            anonymousBasket.AddItem(1, 10, 1);
            anonymousBasket.AddItem(3, 55, 7);
            var userBasket = new Basket(_buyerId);
            userBasket.AddItem(1, 10, 4);
            userBasket.AddItem(2, 99, 3);

            _mockBasketRepository.SetupSequence(x => x.FirstOrDefaultAsync(It.IsAny<BasketWithItemsSpecification>(), default))
                .ReturnsAsync(anonymousBasket)
                .ReturnsAsync(userBasket);

            var handler = new TransferBasketCommandHandler(_mockBasketRepository.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.UpdateAsync(userBasket, default), Times.Once);
            Assert.AreEqual(3, userBasket.Items.Count);
            Assert.That(userBasket.Items.Count(x => x.ProductId == 1 && x.UnitPrice == 10 && x.Quantity == 5), Is.EqualTo(1));
            Assert.That(userBasket.Items.Count(x => x.ProductId == 2 && x.UnitPrice == 99 && x.Quantity == 3), Is.EqualTo(1));
            Assert.That(userBasket.Items.Count(x => x.ProductId == 3 && x.UnitPrice == 55 && x.Quantity == 7), Is.EqualTo(1));
        }

        [Test]
        public async Task RemovesAnonymousBasketAfterUpdatingUserBasket()
        {
            var request = new TransferBasketCommand
            {
                AnonymousId = _anonymousId.ToString(),
                UserId = _buyerId.ToString()
            };

            var anonymousBasket = new Basket(_anonymousId);
            var userBasket = new Basket(_buyerId);

            _mockBasketRepository
                .SetupSequence(x => x.FirstOrDefaultAsync(It.IsAny<BaseSpecification<Basket>>(), default))
                    .ReturnsAsync(anonymousBasket)
                    .ReturnsAsync(userBasket);
            
            var handler = new TransferBasketCommandHandler(_mockBasketRepository.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.UpdateAsync(userBasket, default), Times.Once);
            _mockBasketRepository.Verify(x => x.DeleteAsync(anonymousBasket, default), Times.Once);
        }

        [Test]
        public async Task CreatesNewUserBasketIfNotExists()
        {
            var request = new TransferBasketCommand
            {
                AnonymousId = _anonymousId.ToString(),
                UserId = _buyerId.ToString()
            };

            var anonymousBasket = new Basket(_anonymousId);
            var userBasket = null as Basket;

            _mockBasketRepository
               .SetupSequence(x => x.FirstOrDefaultAsync(It.IsAny<BaseSpecification<Basket>>(), default))
                   .ReturnsAsync(anonymousBasket)
                   .ReturnsAsync(userBasket);

            var handler = new TransferBasketCommandHandler(_mockBasketRepository.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockBasketRepository.Verify(x => x.AddAsync(It.Is<Basket>(x => x.BuyerId == _buyerId), default), Times.Once);
        }
    }
}
