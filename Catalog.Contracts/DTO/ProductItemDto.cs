namespace Catalog.Contracts.QueryModels
{
    public class ProductItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }

        public int GenreId { get; set; }
    }
}
