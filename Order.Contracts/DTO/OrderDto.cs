using OrderProject.Contracts.Entities;

namespace OrderProject.Contracts.DTO
{
    public class OrderDto
    {
        public int OrderNumber { get; set; }
        public Guid BuyerId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShippingAddress { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public decimal Total { get; set; }
    }
}
