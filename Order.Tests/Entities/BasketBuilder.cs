using BasketProject.Contracts.Entities;
using Moq;

namespace OrderProject.Tests.Entities
{
    public class BasketBuilder
    {
        private Basket _basket;
        public Guid BasketBuyerId => Guid.NewGuid();

        public int BasketId => 1;

        public BasketBuilder()
        {
            _basket = WithNoItems();
        }

        public Basket Build()
        {
            return _basket;
        }

        public Basket WithNoItems()
        {
            var basketMock = new Mock<Basket>(BasketBuyerId);
            basketMock.SetupGet(s => s.Id).Returns(BasketId);

            _basket = basketMock.Object;
            return _basket;
        }

        public Basket WithOneBasketItem()
        {
            var basketMock = new Mock<Basket>(BasketBuyerId);
            basketMock.SetupGet(s => s.Id).Returns(BasketId);

            _basket = basketMock.Object;
            _basket.AddItem(2, 3.40m, 4);
            return _basket;
        }
    }
}
