using Catalog.Contracts.Abstracts;
using Catalog.Contracts.Queries;
using Catalog.Contracts.QueryModels;
using Catalog.Specifications;
using MediatR;

namespace Catalog.QueryHandlers
{
    public class GetCatalogItemsQueryHandler : IRequestHandler<GetCatalogItemsQuery, CatalogItemsDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IGenreRepository _genreRepository;

        public GetCatalogItemsQueryHandler(
            IProductRepository productRepository, 
            IGenreRepository genreRepository)
        {
            _productRepository = productRepository;
            _genreRepository = genreRepository;
        }

        public async Task<CatalogItemsDto> Handle(GetCatalogItemsQuery request, CancellationToken cancellationToken)
        {
            var filterSpecification = new CatalogFilterSpecification(request.GenreId);
            var filterPaginatedSpecification =
                new CatalogFilterPaginatedSpecification(request.ItemsPage * request.PageIndex, request.ItemsPage, request.GenreId);

            return new CatalogItemsDto
            {
                Products = await _productRepository.ListAsync(filterPaginatedSpecification),
                Genres = await _genreRepository.ListAsync(),
                TotalItems = await _productRepository.CountAsync(filterSpecification)
            };
        }
    }
}
