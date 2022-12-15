using BasketProject.Contracts.DTO;
using MediatR;

namespace BasketProject.Contracts.Queries
{
    public class GetBasketQuery : IRequest<BasketDto>
    {
        public Guid UserId { get; set; }
    }
}
