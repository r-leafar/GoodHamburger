using AutoMapper;
using GoodHamburger.Api.DTOs;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Mappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductResponse>();
        }
    }
}
