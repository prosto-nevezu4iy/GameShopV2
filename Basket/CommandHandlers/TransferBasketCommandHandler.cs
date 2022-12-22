using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using BasketProject.Contracts.Entities;
using BasketProject.Specifications;
using MediatR;

namespace BasketProject.CommandHandlers
{
    public class TransferBasketCommandHandler : IRequestHandler<TransferBasketCommand>
    {
        private readonly IBasketRepository _basketRepository;

        public TransferBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Unit> Handle(TransferBasketCommand request, CancellationToken cancellationToken)
        {
            var anonymousBasketSpec = new BasketWithItemsSpecification(Guid.Parse(request.AnonymousId));
            var anonymousBasket = await _basketRepository.FirstOrDefaultAsync(anonymousBasketSpec);
            if (anonymousBasket == null) return Unit.Value;
            var userBasketSpec = new BasketWithItemsSpecification(Guid.Parse(request.UserId));
            var userBasket = await _basketRepository.FirstOrDefaultAsync(userBasketSpec);
            if (userBasket == null)
            {
                userBasket = new Basket(Guid.Parse(request.UserId));
                await _basketRepository.AddAsync(userBasket);
            }
            foreach (var item in anonymousBasket.Items)
            {
                userBasket.AddItem(item.ProductId, item.UnitPrice, item.Quantity);
            }
            await _basketRepository.UpdateAsync(userBasket);
            await _basketRepository.DeleteAsync(anonymousBasket);

            return Unit.Value;
        }
    }
}
