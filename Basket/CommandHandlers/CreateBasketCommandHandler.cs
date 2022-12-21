using AutoMapper;
using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using BasketProject.Contracts.DTO;
using BasketProject.Contracts.Entities;
using BasketProject.Specifications;
using Catalog.Contracts.Queries;
using MediatR;

namespace BasketProject.CommandHandlers
{
    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, BasketDto>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateBasketCommandHandler(IBasketRepository basketRepository, IMapper mapper, IMediator mediator)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<BasketDto> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basketSpec = new BasketWithItemsSpecification(request.UserId);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);

            if (basket == null)
            {
                basket = new Basket(request.UserId);
                await _basketRepository.AddAsync(basket);
            }

            basket.AddItem(request.ProductId, request.Price, request.Quantity);

            await _basketRepository.UpdateAsync(basket);

            var basketDto = _mapper.Map<BasketDto>(basket);

            var query = new GetProductsQuery
            {
                BasketItems = basket.Items
            };

            basketDto.Items = await _mediator.Send(query);

            return basketDto;
        }
    }
}
