using OrderProject.Contracts.Entities;

namespace OrderProject.Tests.Entities
{
    [TestFixture]
    public class OrderTotalTests
    {
        private decimal _testUnitPrice = 42m;

        [Test]
        public void IsZeroForNewOrder()
        {
            var order = new OrderBuilder().WithNoItems();

            Assert.AreEqual(0, order.Total());
        }

        [Test]
        public void IsCorrectGiven1Item()
        {
            var builder = new OrderBuilder();
            var items = new List<OrderItem>
            {
                new OrderItem(builder.TestProductOrdered, _testUnitPrice, 1)
            };
            var order = new OrderBuilder().WithItems(items);
            Assert.AreEqual(_testUnitPrice, order.Total());
        }

        [Test]
        public void IsCorrectGiven3Items()
        {
            var builder = new OrderBuilder();
            var order = builder.WithDefaultValues();

            Assert.AreEqual(builder.TestUnitPrice * builder.TestUnits, order.Total());
        }
    }
}
