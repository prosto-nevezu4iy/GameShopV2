using Catalog.Contracts.DTO;

namespace Catalog.Contracts.QueryModels
{
    public class CatalogItemsDto
    {
        public IEnumerable<ProductItemDto> Products { get; set; } = new List<ProductItemDto>();
        public IEnumerable<GenreItemDto> Genres { get; set; } = new List<GenreItemDto>();
        public int TotalItems { get; set; }
    }
}
