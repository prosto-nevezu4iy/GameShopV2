using BasketProject.Contracts.DTO;
using MediatR;

namespace BasketProject.Contracts.Commands
{
    public class CreateBasketCommand : IRequest<BasketDto>
    {
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public byte Quantity { get; set; } = 1;
    }
}
