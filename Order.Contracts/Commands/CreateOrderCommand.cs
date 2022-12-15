using MediatR;
using Order.Contracts.Entities;

namespace Order.Contracts.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public int BasketId { get; set; }
        public Address ShippingAddress { get; set; }
    }
}
