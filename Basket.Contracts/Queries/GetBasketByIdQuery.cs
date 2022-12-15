using BasketProject.Contracts.Entities;
using MediatR;

namespace BasketProject.Contracts.Queries
{
    public class GetBasketByIdQuery : IRequest<Basket>
    {
        public int BasketId { get; set; }
    }
}
