using OrderProject.Contracts.DTO;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class OrderViewModelService : IOrderViewModelService
    {
        public IEnumerable<OrderViewModel> ConvertToViewModel(IEnumerable<OrderDto> orders)
        {
            return orders.Select(o => new OrderViewModel
            {
                OrderDate = o.OrderDate,
                OrderItems = o.OrderItems.Select(oi => new OrderItemViewModel()
                {
                    PictureUrl = oi.PictureUrl,
                    ProductId = oi.ProductId,
                    ProductName = oi.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = o.OrderNumber,
                ShippingAddress = o.ShippingAddress,
                Total = o.Total
            });
        }

        public OrderViewModel ConvertToViewModel(OrderDto order)
        {
            return new OrderViewModel
            {
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel()
                {
                    PictureUrl = oi.PictureUrl,
                    ProductId = oi.ProductId,
                    ProductName = oi.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = order.OrderNumber,
                ShippingAddress = order.ShippingAddress,
                Total = order.Total
            };
        }
    }
}
