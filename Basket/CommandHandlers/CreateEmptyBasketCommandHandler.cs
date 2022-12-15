using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using BasketProject.Contracts.DTO;
using BasketProject.Contracts.Entities;
using MediatR;

namespace BasketProject.CommandHandlers
{
    public class CreateEmptyBasketCommandHandler : IRequestHandler<CreateEmptyBasketCommand, BasketDto>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateEmptyBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<BasketDto> Handle(CreateEmptyBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = new Basket(request.UserId);
            await _basketRepository.AddAsync(basket);

            return new BasketDto()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id
            };
        }
    }
}
