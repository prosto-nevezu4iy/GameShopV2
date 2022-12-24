using Moq;
using OrderProject.Contracts.Entities;
using OrderProject.Specifications;
using OrderProject.Tests.Entities;

namespace OrderProject.Tests.Specifications
{
    [TestFixture]
    public class OrderWithItemsByIdSpecificationTests
    {
        private readonly Guid _buyerId = Guid.NewGuid();

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void ReturnsOrderWithOrderedItem(int orderId, int count)
        {
            var spec = new OrderWithItemsByIdSpecification(orderId);

            var result = GetTestCollection()
                          .AsQueryable()
                          .FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.NotNull(result.OrderItems);
            Assert.AreEqual(count, result.OrderItems.Count);
            Assert.NotNull(result.OrderItems.FirstOrDefault()?.ProductOrdered);
        }

        private List<Order> GetTestCollection()
        {
            var shipToAddress = new AddressBuilder().Build();

            var mockOrder1 = new Mock<Order>(_buyerId, shipToAddress, new List<OrderItem>
                {
                    new OrderItem(new ProductOrdered(1, "Product1", "testurl"), 10.50m, 1)
                });
            mockOrder1.SetupGet(s => s.Id).Returns(1);

            var mockOrder2 = new Mock<Order>(_buyerId, shipToAddress, new List<OrderItem>
                {
                    new OrderItem(new ProductOrdered(2, "Product2", "testurl"), 15.50m, 2),
                    new OrderItem(new ProductOrdered(2, "Product3", "testurl"), 20.50m, 1)
                });

            mockOrder2.SetupGet(s => s.Id).Returns(2);

            var ordersList = new List<Order>();

            ordersList.Add(mockOrder1.Object);
            ordersList.Add(mockOrder2.Object);

            return ordersList;
        }
    }
}
