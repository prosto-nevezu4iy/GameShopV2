using BasketProject.Contracts.DTO;
using Catalog.Contracts.Entities;
using Core.Interfaces;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IUriComposer _uriComposer;

        public BasketViewModelService(IUriComposer uriComposer)
        {
            _uriComposer = uriComposer;
        }

        public BasketViewModel ConvertToViewModel(BasketDto basket)
        {
            return new BasketViewModel()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                Items = basket.Items.Select(item =>
                {
                    return new BasketItemViewModel
                    {
                        Id = item.Id,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        PictureUrl = _uriComposer.ComposePicUri(item.PictureUrl),
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                }).ToList()
            };
        }
    }
}
