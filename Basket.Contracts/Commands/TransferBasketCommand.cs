using MediatR;

namespace BasketProject.Contracts.Commands
{
    public class TransferBasketCommand : IRequest
    {
        public string AnonymousId { get; set; }
        public string UserId { get; set; }
    }
}
