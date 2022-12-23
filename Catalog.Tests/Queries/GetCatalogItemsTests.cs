using AutoMapper;
using BasketProject.Contracts.Abstracts;
using BasketProject.Contracts.Queries;
using Catalog.Contracts.Abstracts;
using Catalog.Contracts.Queries;
using Catalog.Mapping;
using Catalog.QueryHandlers;
using Catalog.Specifications;
using Moq;

namespace Catalog.Tests.Queries
{
    [TestFixture]
    public class GetCatalogItemsTests
    {
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IGenreRepository> _mockGenreRepository;
        private IMapper _mapper;

        [SetUp]
        public void Init()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockGenreRepository = new Mock<IGenreRepository>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CatalogMappingProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }

        [Test]
        public async Task InvokesRepositoryListAsyncAndCountAsyncOnce()
        {
            var request = new GetCatalogItemsQuery
            {
                PageIndex = 0,
                ItemsPage = 10,
                GenreId = 1
            };

            var handler = new GetCatalogItemsQueryHandler(_mockProductRepository.Object, _mockGenreRepository.Object, _mapper);

            await handler.Handle(request, CancellationToken.None);

            _mockProductRepository.Verify(x => x.ListAsync(It.IsAny<CatalogFilterPaginatedSpecification>(), default), Times.Once);
            _mockGenreRepository.Verify(x => x.ListAsync(default), Times.Once);
            _mockProductRepository.Verify(x => x.CountAsync(It.IsAny<CatalogFilterSpecification>(), default), Times.Once);
        }
    }
}
