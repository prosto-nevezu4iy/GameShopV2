using AutoMapper;
using OrderProject.Contracts.DTO;
using OrderProject.Contracts.Entities;

namespace OrderProject.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
