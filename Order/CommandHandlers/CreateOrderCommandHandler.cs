using BasketProject.Contracts.Exceptions;
using BasketProject.Contracts.Queries;
using Catalog.Contracts.Queries;
using Core.Extensions;
using MediatR;
using OrderProject.Contracts.Abstracts;
using OrderProject.Contracts.Commands;
using OrderProject.Contracts.Entities;

namespace OrderProject.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IMediator mediator, IOrderRepository orderRepository)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var query = new GetBasketByIdQuery
            {
                BasketId = request.BasketId
            };

            var basket = await _mediator.Send(query);

            basket.AssertNotNull(nameof(basket));

            if (!basket.Items.Any())
            {
                throw new EmptyBasketOnCheckoutException();
            }

            var getProductsQuery = new GetProductsQuery
            {
                BasketItems = basket.Items
            };

            var basketItems = await _mediator.Send(getProductsQuery);

            var items = basketItems.Select(basketItem =>
            {
                var productOrdered = new ProductOrdered(basketItem.ProductId, basketItem.ProductName, basketItem.PictureUrl);
                var orderItem = new OrderItem(productOrdered, basketItem.UnitPrice, basketItem.Quantity);
                return orderItem;
            }).ToList();

            var order = new Order(basket.BuyerId, request.ShippingAddress, items);

            await _orderRepository.AddAsync(order);

            return Unit.Value;
        }
    }
}
