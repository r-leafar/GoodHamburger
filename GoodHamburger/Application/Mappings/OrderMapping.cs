using GoodHamburger.Api.DTOs;
using AutoMapper;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Mappings
{
    public class OrderMapping : Profile
    {

        public OrderMapping()
        {
            CreateMap<OrderRequest, Order>()
             .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<OrderItemRequest, OrderItem>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.ItemTotal, opt => opt.Ignore()) // Will be calculated later
                .ForMember(dest => dest.Order, opt => opt.Ignore()) // Will be set when adding to Order
                .ForMember(dest => dest.Product, opt => opt.Ignore()); // 

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src=> src.Id));

            CreateMap<OrderItem, OrderItemResponse>();

            CreateMap<OrderUpdateRequest, Order>()
                .ForMember( dest => dest.Id, opt => opt.MapFrom( src => src.OrderId));

        }

    }
}
