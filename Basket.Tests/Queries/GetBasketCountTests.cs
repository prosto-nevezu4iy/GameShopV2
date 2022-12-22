using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Queries;
using BasketProject.QueryHandlers;
using Moq;

namespace BasketProject.Tests.Queries
{
    [TestFixture]
    public class GetBasketCountTests
    {
        private Mock<IBasketRepository> _mockBasketRepository;

        private readonly Guid _buyerId = Guid.NewGuid();

        [SetUp]
        public void Init()
        {
            _mockBasketRepository = new Mock<IBasketRepository>();
        }

        [Test]
        public async Task InvokesBasketRepositoryGetBasketCountAsyncOnce()
        {
            var request = new GetBasketCountQuery
            {
                UserId = _buyerId
            };

            var handler = new GetBasketCountQueryHandler(_mockBasketRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.GreaterOrEqual(0, result);
            _mockBasketRepository.Verify(x => x.GetBasketCountAsync(_buyerId), Times.Once);
        }
    }
}
