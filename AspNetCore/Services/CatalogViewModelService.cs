using Catalog.Contracts.DTO;
using Catalog.Contracts.Entities;
using Catalog.Contracts.Queries;
using Catalog.Contracts.QueryModels;
using Catalog.QueryHandlers;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class CatalogViewModelService : ICatalogViewModelService
    {
        private readonly IUriComposer _uriComposer;
        private readonly ILogger<CatalogViewModelService> _logger;

        public CatalogViewModelService(
            IUriComposer uriComposer,
            ILogger<CatalogViewModelService> logger)
        {
            _uriComposer = uriComposer;
            _logger = logger;
        }

        public CatalogIndexViewModel ConvertToViewModel(GetCatalogItemsQuery query, CatalogItemsDto model)
        {
            var vm = new CatalogIndexViewModel()
            {
                Products = model.Products.Select(i => new ProductViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    PictureUri = _uriComposer.ComposePicUri(i.PictureUri),
                    Price = i.Price
                }).ToList(),
                Genres = ConvertGenresToViewModel(model.Genres).ToList(),
                GenresFilterApplied = query.GenreId ?? 0,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = query.PageIndex,
                    ItemsPerPage = model.Products.Count,
                    TotalItems = model.TotalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)model.TotalItems / query.ItemsPage)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "disabled" : "";

            return vm;
        }

        public IEnumerable<SelectListItem> ConvertGenresToViewModel(ICollection<GenreItemDto> genres)
        {
            var items = genres
                .Select(type => new SelectListItem() { Value = type.Id.ToString(), Text = type.Name })
                .OrderBy(t => t.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }
    }
}
