using BasketProject.Contracts.DTO;
using Catalog.Contracts.Queries;
using Catalog.Specifications;
using Core.Interfaces;
using MediatR;

namespace Catalog.QueryHandlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<BasketItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUriComposer _uriComposer;

        public GetProductsQueryHandler(IProductRepository productRepository, IUriComposer uriComposer)
        {
            _productRepository = productRepository;
            _uriComposer = uriComposer;
        }

        public async Task<IEnumerable<BasketItemDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var productsSpecification = new ProductsSpecification(request.BasketItems.Select(b => b.ProductId).ToArray());
            var products = await _productRepository.ListAsync(productsSpecification);

            var items = request.BasketItems.Select(basketItem =>
            {
                var product = products.First(c => c.Id == basketItem.ProductId);

                var basketItemViewModel = new BasketItemDto
                {
                    Id = basketItem.Id,
                    UnitPrice = basketItem.UnitPrice,
                    Quantity = basketItem.Quantity,
                    ProductId = basketItem.ProductId,
                    PictureUrl = _uriComposer.ComposePicUri(product.PictureUri),
                    ProductName = product.Name
                };
                return basketItemViewModel;
            }).ToList();

            return items;
        }
    }
}
