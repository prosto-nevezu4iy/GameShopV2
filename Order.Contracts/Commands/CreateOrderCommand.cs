using MediatR;
using OrderProject.Contracts.Entities;

namespace OrderProject.Contracts.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public int BasketId { get; set; }
        public Address ShippingAddress { get; set; }
    }
}
