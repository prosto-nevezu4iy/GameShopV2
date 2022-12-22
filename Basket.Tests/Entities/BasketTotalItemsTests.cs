using BasketProject.Contracts.Entities;

namespace BasketProject.Tests.Entities
{
    [TestFixture]
    public class BasketTotalItemsTests
    {
        private readonly int _testCatalogItemId = 123;
        private readonly decimal _testUnitPrice = 1.23m;
        private readonly byte _testQuantity = 2;
        private readonly Guid _buyerId = Guid.NewGuid();

        [Test]
        public void ReturnsTotalQuantityWithOneItem()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);

            var result = basket.TotalItems;

            Assert.AreEqual(_testQuantity, result);
        }

        [Test]
        public void ReturnsTotalQuantityWithMultipleItems()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testCatalogItemId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testCatalogItemId, _testUnitPrice, (byte) (_testQuantity * 2));

            var result = basket.TotalItems;

            Assert.AreEqual(_testQuantity * 3, result);
        }
    }
}
