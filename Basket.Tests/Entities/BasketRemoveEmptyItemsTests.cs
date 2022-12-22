using BasketProject.Contracts.Entities;

namespace BasketProject.Tests.Entities
{
    [TestFixture]
    public class BasketRemoveEmptyItemsTests
    {
        private readonly int _testCatalogItemId = 123;
        private readonly decimal _testUnitPrice = 1.23m;
        private readonly Guid _buyerId = Guid.NewGuid();

        [Test]
        public void RemovesEmptyBasketItems()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testCatalogItemId, _testUnitPrice, 0);
            basket.RemoveEmptyItems();

            Assert.AreEqual(0, basket.Items.Count);
        }
    }
}
