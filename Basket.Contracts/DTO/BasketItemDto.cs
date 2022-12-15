namespace BasketProject.Contracts.DTO
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }
        public byte Quantity { get; set; }
        public int ProductId { get; set; }
        public string PictureUrl { get; set; }
        public string ProductName { get; set; }
    }
}
