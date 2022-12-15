using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Entities;
using BasketProject.Contracts.Queries;
using BasketProject.Specifications;
using MediatR;

namespace BasketProject.QueryHandlers
{
    public class GetBasketByIdQueryHandler : IRequestHandler<GetBasketByIdQuery, Basket>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketByIdQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Basket> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
        {
            var basketSpec = new BasketWithItemsSpecification(request.BasketId);
            return await _basketRepository.FirstOrDefaultAsync(basketSpec);
        }
    }
}
