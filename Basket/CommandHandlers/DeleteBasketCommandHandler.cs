using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using Core.Extensions;
using MediatR;

namespace BasketProject.CommandHandlers
{
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Unit> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetByIdAsync(request.BasketId);

            basket.AssertNotNull(nameof(basket));

            await _basketRepository.DeleteAsync(basket);

            return Unit.Value;
        }
    }
}
