using BasketProject.Contracts.DTO;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        BasketViewModel ConvertToViewModel(BasketDto basket);
    }
}
