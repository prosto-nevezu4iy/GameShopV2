using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using BasketProject.Specifications;
using MediatR;

namespace BasketProject.CommandHandlers
{
    public class SetQuantitiesCommandHandler : IRequestHandler<SetQuantitiesCommand>
    {
        private readonly IBasketRepository _basketRepository;

        public SetQuantitiesCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Unit> Handle(SetQuantitiesCommand request, CancellationToken cancellationToken)
        {
            var basketSpec = new BasketWithItemsSpecification(request.Id);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);
            if (basket == null) return Unit.Value;

            foreach (var item in basket.Items)
            {
                if (request.Items.TryGetValue(item.Id.ToString(), out var quantity))
                {
                    item.SetQuantity(quantity);
                }
            }

            basket.RemoveEmptyItems();
            await _basketRepository.UpdateAsync(basket);

            return Unit.Value;
        }
    }
}
