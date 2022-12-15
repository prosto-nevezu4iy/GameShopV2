namespace BasketProject.Contracts.DTO
{
    public class BasketDto
    {
        public int Id { get; set; }
        public Guid BuyerId { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public int TotalItems { get; set; }
    }
}
