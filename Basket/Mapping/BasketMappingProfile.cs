using AutoMapper;
using BasketProject.Contracts.DTO;
using BasketProject.Contracts.Entities;

namespace BasketProject.Mapping
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<Basket, BasketDto>();
            CreateMap<BasketItem, BasketItemDto>();
        }
    }
}
