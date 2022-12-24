using OrderProject.Contracts.Entities;
using OrderProject.Specifications;
using OrderProject.Tests.Entities;

namespace OrderProject.Tests.Specifications
{
    [TestFixture]
    public class OrdersWithItemsSpecificationTests
    {
        private readonly Guid _buyerId = Guid.NewGuid();

        [Test]
        public void ReturnsOrderWithOrderedItem()
        {
            var spec = new OrdersWithItemsSpecification(_buyerId);

            var result = GetTestCollection()
                          .AsQueryable()
                          .FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.NotNull(result.OrderItems);
            Assert.AreEqual(1, result.OrderItems.Count);
            Assert.NotNull(result.OrderItems.FirstOrDefault()?.ProductOrdered);
        }

        [Test]
        public void ReturnsAllOrderWithAllOrderedItem()
        {
            var spec = new OrdersWithItemsSpecification(_buyerId);

            var result = GetTestCollection()
                                     .AsQueryable()
                                     .Where(spec.Criteria)
                                     .ToList();

            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].OrderItems.Count);
            Assert.NotNull(result[0].OrderItems.FirstOrDefault()?.ProductOrdered);
            Assert.AreEqual(2, result[1].OrderItems.Count);
            Assert.NotNull(result[1].OrderItems.ToList()[0].ProductOrdered);
            Assert.NotNull(result[1].OrderItems.ToList()[1].ProductOrdered);
        }

        private List<Order> GetTestCollection()
        {
            var ordersList = new List<Order>();
            var shipToAddress = new AddressBuilder().Build();

            ordersList.Add(new Order(_buyerId, shipToAddress,
                new List<OrderItem>
                {
                    new OrderItem(new ProductOrdered(1, "Product1", "testurl"), 10.50m, 1)
                }));
            ordersList.Add(new Order(_buyerId, shipToAddress,
                new List<OrderItem>
                {
                    new OrderItem(new ProductOrdered(2, "Product2", "testurl"), 15.50m, 2),
                    new OrderItem(new ProductOrdered(2, "Product3", "testurl"), 20.50m, 1)
                }));

            return ordersList;
        }
    }
}
