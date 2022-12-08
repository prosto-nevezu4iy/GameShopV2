using Catalog.Contracts.DTO;

namespace Catalog.Contracts.QueryModels
{
    public class CatalogItemsDto
    {
        public ICollection<ProductItemDto> Products { get; set; } = new List<ProductItemDto>();
        public ICollection<GenreItemDto> Genres { get; set; } = new List<GenreItemDto>();
        public int TotalItems { get; set; }
    }
}
