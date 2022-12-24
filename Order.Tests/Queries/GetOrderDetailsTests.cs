using Moq;
using OrderProject.Contracts.Abstracts;
using OrderProject.Contracts.DTO;
using OrderProject.Contracts.Entities;
using OrderProject.Contracts.Queries;
using OrderProject.QueryHandlers;
using OrderProject.Specifications;
using OrderProject.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Tests.Queries
{
    [TestFixture]
    public class GetOrderDetailsTests
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
            var order = new OrderBuilder().WithDefaultValues();

            var request = new GetOrderDetailsQuery
            {
                OrderId = 1
            };

            _mockOrderRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<OrderWithItemsByIdSpecification>(), default)).ReturnsAsync(order);

            var handler = new GetOrderDetailsQueryHandler(_mockOrderRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            _mockOrderRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<OrderWithItemsByIdSpecification>(), default), Times.Once);
        }

        [Test]
        public async Task ShouldReturnNullIfOrderIsNull()
        {
            var order = null as Order;

            var request = new GetOrderDetailsQuery
            {
                OrderId = 1
            };

            _mockOrderRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<OrderWithItemsByIdSpecification>(), default)).ReturnsAsync(order);

            var handler = new GetOrderDetailsQueryHandler(_mockOrderRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.Null(result);
        }

        [Test]
        public async Task ShouldReturnOrderDtoIfOrderIsNotNull()
        {
            var order = new OrderBuilder().WithDefaultValues();

            var request = new GetOrderDetailsQuery
            {
                OrderId = 1
            };

            _mockOrderRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<OrderWithItemsByIdSpecification>(), default)).ReturnsAsync(order);

            var handler = new GetOrderDetailsQueryHandler(_mockOrderRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsInstanceOf<OrderDto>(result);
        }
    }
}
