using Catalog.Contracts.DTO;
using Catalog.Contracts.Queries;
using Catalog.Contracts.QueryModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface ICatalogViewModelService
    {
        CatalogIndexViewModel ConvertToViewModel(GetCatalogItemsQuery query, CatalogItemsDto model);
        IEnumerable<SelectListItem> ConvertGenresToViewModel(IEnumerable<GenreItemDto> genres);
    }
}
