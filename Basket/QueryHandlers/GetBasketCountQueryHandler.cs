using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Queries;
using MediatR;

namespace BasketProject.QueryHandlers
{
    public class GetBasketCountQueryHandler : IRequestHandler<GetBasketCountQuery, int>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketCountQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<int> Handle(GetBasketCountQuery request, CancellationToken cancellationToken)
        {
            return await _basketRepository.GetBasketCountAsync(request.UserId);
        }
    }
}
