using AutoMapper;
using Catalog.Contracts.Abstracts;
using Catalog.Contracts.DTO;
using Catalog.Contracts.Entities;
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
        private readonly IMapper _mapper;

        public GetCatalogItemsQueryHandler(
            IProductRepository productRepository,
            IGenreRepository genreRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<CatalogItemsDto> Handle(GetCatalogItemsQuery request, CancellationToken cancellationToken)
        {
            var filterSpecification = new CatalogFilterSpecification(request.GenreId);
            var filterPaginatedSpecification =
                new CatalogFilterPaginatedSpecification(request.ItemsPage * request.PageIndex, request.ItemsPage, request.GenreId);

            var products = await _productRepository.ListAsync(filterPaginatedSpecification);
            var genres = await _genreRepository.ListAsync();

            return new CatalogItemsDto
            {
                Products = _mapper.Map<IList<ProductItemDto>>(products),
                Genres = _mapper.Map<IList<GenreItemDto>>(genres),
                TotalItems = await _productRepository.CountAsync(filterSpecification)
            };
        }
    }
}
