using MediatR;
using OrderProject.Contracts.Abstracts;
using OrderProject.Contracts.DTO;
using OrderProject.Contracts.Queries;
using OrderProject.Specifications;

namespace OrderProject.QueryHandlers
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderDetailsQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var spec = new OrderWithItemsByIdSpecification(request.OrderId);
            var order = await _orderRepository.FirstOrDefaultAsync(spec);

            if (order == null)
            {
                return default;
            }

            return new OrderDto
            {
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    PictureUrl = oi.ProductOrdered.PictureUri,
                    ProductId = oi.ProductOrdered.ProductId,
                    ProductName = oi.ProductOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = order.Id,
                ShippingAddress = order.ShipToAddress,
                Total = order.Total()
            };
        }
    }
}
