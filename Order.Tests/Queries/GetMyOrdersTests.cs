using MediatR;
using Moq;
using OrderProject.CommandHandlers;
using OrderProject.Contracts.Abstracts;
using OrderProject.Contracts.Commands;
using OrderProject.Contracts.Entities;
using OrderProject.Contracts.Queries;
using OrderProject.QueryHandlers;
using OrderProject.Specifications;
using OrderProject.Tests.Entities;

namespace OrderProject.Tests.Queries
{
    [TestFixture]
    public class GetMyOrdersTests
    {
        private Mock<IOrderRepository> _mockOrderRepository;

        [SetUp]
        public void Init()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
        }

        [Test]
        public async Task InvokesOrderRepositoryListAsyncOnce()
        {
            var orders = new List<Order> { new OrderBuilder().WithDefaultValues() };

            var request = new GetMyOrdersQuery
            {
                UserId = Guid.NewGuid()
            };

            _mockOrderRepository.Setup(x => x.ListAsync(It.IsAny<OrdersWithItemsSpecification>(), default)).ReturnsAsync(orders);

            var handler = new GetMyOrdersQueryHandler(_mockOrderRepository.Object);

            await handler.Handle(request, CancellationToken.None);

            _mockOrderRepository.Verify(x => x.ListAsync(It.IsAny<OrdersWithItemsSpecification>(), default), Times.Once);
        }
    }
}
