using BasketProject.Contracts.DTO;
using MediatR;

namespace BasketProject.Contracts.Commands
{
    public class CreateEmptyBasketCommand : IRequest<BasketDto>
    {
        public Guid UserId { get; set; }
    }
}
