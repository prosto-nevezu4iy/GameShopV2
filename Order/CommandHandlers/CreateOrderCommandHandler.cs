using BasketProject.Contracts.Exceptions;
using BasketProject.Contracts.Queries;
using Catalog.Contracts.Queries;
using Core.Extensions;
using MediatR;
using Order.Contracts.Abstracts;
using Order.Contracts.Commands;
using Order.Contracts.Entities;
using OrderEntity = Order.Contracts.Entities.Order;

namespace Order.CommandHandlers
{
    public class CreateOrderCommandHandler : AsyncRequestHandler<CreateOrderCommand>
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IMediator mediator, IOrderRepository orderRepository)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
        }

        protected override async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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

            var order = new OrderEntity(basket.BuyerId, request.ShippingAddress, items);

            await _orderRepository.AddAsync(order);
        }
    }
}
