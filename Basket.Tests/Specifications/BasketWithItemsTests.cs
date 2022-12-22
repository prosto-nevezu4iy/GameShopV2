using BasketProject.Contracts.Entities;
using BasketProject.Specifications;
using Moq;

namespace BasketProject.Tests.Specifications
{
    [TestFixture]
    public class BasketWithItemsTests
    {
        private readonly int _testBasketId = 123;
        private readonly Guid _buyerId = Guid.NewGuid();

        [Test]
        public void MatchesBasketWithGivenBasketId()
        {
            var spec = new BasketWithItemsSpecification(_testBasketId);

            var result = GetTestBasketCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criteria);

             Assert.NotNull(result);
             Assert.AreEqual(_testBasketId, result.Id);
        }

        [Test]
        public void MatchesNoBasketsIfBasketIdNotPresent()
        {
            int badBasketId = -1;
            var spec = new BasketWithItemsSpecification(badBasketId);

            Assert.False(GetTestBasketCollection()
                .AsQueryable()
                .Any(spec.Criteria));
        }

        [Test]
        public void MatchesBasketWithGivenBuyerId()
        {
            var spec = new BasketWithItemsSpecification(_buyerId);

            var result = GetTestBasketCollection()
                 .AsQueryable()
                 .FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.AreEqual(_buyerId, result.BuyerId);
        }

        [Test]
        public void MatchesNoBasketsIfBuyerIdNotPresent()
        {
            var badBuyerId = Guid.Empty;
            var spec = new BasketWithItemsSpecification(badBuyerId);

             Assert.False(GetTestBasketCollection()
                 .AsQueryable()
                 .Any(spec.Criteria));
        }

        public List<Basket> GetTestBasketCollection()
        {
            var basket1Mock = new Mock<Basket>(_buyerId);
            basket1Mock.SetupGet(s => s.Id).Returns(1);
            var basket2Mock = new Mock<Basket>(_buyerId);
            basket2Mock.SetupGet(s => s.Id).Returns(2);
            var basket3Mock = new Mock<Basket>(_buyerId);
            basket3Mock.SetupGet(s => s.Id).Returns(_testBasketId);

            return new List<Basket>()
            {
                basket1Mock.Object,
                basket2Mock.Object,
                basket3Mock.Object
            };
        }
    }
}
