using MediatR;
using OrderProject.Contracts.DTO;

namespace OrderProject.Contracts.Queries
{
    public class GetMyOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
        public Guid UserId { get; set; }
    }
}
