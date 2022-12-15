using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Commands;
using Core.Extensions;
using MediatR;

namespace BasketProject.CommandHandlers
{
    public class DeleteBasketCommandHandler : AsyncRequestHandler<DeleteBasketCommand>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        protected override async Task Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetByIdAsync(request.BasketId);

            basket.AssertNotNull(nameof(basket));

            await _basketRepository.DeleteAsync(basket);
        }
    }
}
