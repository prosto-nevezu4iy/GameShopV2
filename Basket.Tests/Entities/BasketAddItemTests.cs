using BasketProject.Contracts.Entities;

namespace BasketProject.Tests.Entities
{
    [TestFixture]
    public class BasketAddItemTests
    {
        private readonly int _testProductId = 123;
        private readonly decimal _testUnitPrice = 1.23m;
        private readonly byte _testQuantity = 2;
        private readonly Guid _buyerId = Guid.NewGuid();


        [Test]
        public void AddsBasketItemIfNotPresent()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testProductId, _testUnitPrice, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.AreEqual(_testProductId, firstItem.ProductId);
            Assert.AreEqual(_testUnitPrice, firstItem.UnitPrice);
            Assert.AreEqual(_testQuantity, firstItem.Quantity);
        }

        [Test]
        public void IncrementsQuantityOfItemIfPresent()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testProductId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testProductId, _testUnitPrice, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.AreEqual(_testQuantity * 2, firstItem.Quantity);
        }

        [Test]
        public void KeepsOriginalUnitPriceIfMoreItemsAdded()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testProductId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testProductId, _testUnitPrice * 2, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.AreEqual(_testUnitPrice, firstItem.UnitPrice);
        }

        [Test]
        public void DefaultsToQuantityOfOne()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testProductId, _testUnitPrice);

            var firstItem = basket.Items.Single();
            Assert.AreEqual(1, firstItem.Quantity);
        }
    }
}
