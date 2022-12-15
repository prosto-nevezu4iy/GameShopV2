using MediatR;
using OrderProject.Contracts.DTO;

namespace OrderProject.Contracts.Queries
{
    public class GetOrderDetailsQuery : IRequest<OrderDto>
    {
        public int OrderId { get; set; }
    }
}
