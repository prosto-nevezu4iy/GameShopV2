using MediatR;

namespace BasketProject.Contracts.Queries
{
    public class GetBasketCountQuery : IRequest<int>
    {
        public Guid UserId { get; set; }
    }
}
