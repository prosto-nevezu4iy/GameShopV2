using OrderProject.Contracts.DTO;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IOrderViewModelService
    {
        IEnumerable<OrderViewModel> ConvertToViewModel(IEnumerable<OrderDto> orders);
        OrderViewModel ConvertToViewModel(OrderDto order);
    }
}
