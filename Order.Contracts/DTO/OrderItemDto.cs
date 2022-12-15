using OrderProject.Contracts.Entities;

namespace OrderProject.Contracts.DTO
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
        public string? PictureUrl { get; set; }
    }
}
