using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using BasketProject.Contracts.DTO;
using BasketProject.Contracts.Entities;
using BasketProject.Contracts.Queries;
using BasketProject.Specifications;
using Catalog.Contracts.Queries;
using MediatR;

namespace BasketProject.QueryHandlers
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketDto>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMediator _mediator;

        public GetBasketQueryHandler(IBasketRepository basketRepository, IMediator mediator)
        {
            _basketRepository = basketRepository;
            _mediator = mediator;
        }

        public async Task<BasketDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basketSpec = new BasketWithItemsSpecification(request.UserId);
            var basket = (await _basketRepository.FirstOrDefaultAsync(basketSpec));

            if (basket == null)
            {
                var command = new CreateEmptyBasketCommand 
                { 
                    UserId = request.UserId 
                };

                return await _mediator.Send(command);
            }

            var query = new GetProductsQuery
            {
                BasketItems = basket.Items
            };

            return new BasketDto()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                Items = await _mediator.Send(query)
            };
        }
    }
}
