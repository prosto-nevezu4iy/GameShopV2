using MediatR;
using OrderProject.Contracts.Abstracts;
using OrderProject.Contracts.DTO;
using OrderProject.Contracts.Queries;
using OrderProject.Specifications;

namespace OrderProject.QueryHandlers
{
    public class GetMyOrdersQueryHandler : IRequestHandler<GetMyOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetMyOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetMyOrdersQuery request, CancellationToken cancellationToken)
        {
            var specification = new OrdersWithItemsSpecification(request.UserId);
            var orders = await _orderRepository.ListAsync(specification, cancellationToken);

            return orders.Select(o => new OrderDto
            {
                OrderDate = o.OrderDate,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto()
                {
                    PictureUrl = oi.ProductOrdered.PictureUri,
                    ProductId = oi.ProductOrdered.ProductId,
                    ProductName = oi.ProductOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = o.Id,
                ShippingAddress = o.ShipToAddress,
                Total = o.Total()
            });
        }
    }
}
