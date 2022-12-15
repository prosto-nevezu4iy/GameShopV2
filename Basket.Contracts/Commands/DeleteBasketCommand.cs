using MediatR;

namespace BasketProject.Contracts.Commands
{
    public class DeleteBasketCommand : IRequest
    {
        public int BasketId { get; set; }
    }
}
