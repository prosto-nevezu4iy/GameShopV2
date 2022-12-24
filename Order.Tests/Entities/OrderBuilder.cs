using OrderProject.Contracts.Entities;

namespace OrderProject.Tests.Entities
{
    public class OrderBuilder
    {
        private Order _order;
        public Guid TestBuyerId => Guid.NewGuid();
        public int TestProductId => 234;
        public string TestProductName => "Test Product Name";
        public string TestPictureUri => "http://test.com/image.jpg";
        public decimal TestUnitPrice = 1.23m;
        public byte TestUnits = 3;
        public ProductOrdered TestProductOrdered { get; }

        public OrderBuilder()
        {
            TestProductOrdered = new ProductOrdered(TestProductId, TestProductName, TestPictureUri);
            _order = WithDefaultValues();
        }

        public Order Build()
        {
            return _order;
        }

        public Order WithDefaultValues()
        {
            var orderItem = new OrderItem(TestProductOrdered, TestUnitPrice, TestUnits);
            var itemList = new List<OrderItem>() { orderItem };
            _order = new Order(TestBuyerId, new AddressBuilder().WithDefaultValues(), itemList);
            return _order;
        }

        public Order WithNoItems()
        {
            _order = new Order(TestBuyerId, new AddressBuilder().WithDefaultValues(), new List<OrderItem>());
            return _order;
        }

        public Order WithItems(List<OrderItem> items)
        {
            _order = new Order(TestBuyerId, new AddressBuilder().WithDefaultValues(), items);
            return _order;
        }
    }
}