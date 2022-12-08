using AutoMapper;
using Catalog.Contracts.DTO;
using Catalog.Contracts.Entities;
using Catalog.Contracts.QueryModels;

namespace Catalog.Mapping
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<Product, ProductItemDto>();
            CreateMap<Genre, GenreItemDto>();
        }
    }
}
