using API.Dtos;
using API.Dtos.Inputs;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterInputDto, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<IdentityUser, UserDto>();

            CreateMap<StockInputDto, Stock>();
            CreateMap<StockInputDto, StockHistory>();
            CreateMap<Stock, StockDto>();
            CreateMap<StockHistory, StockDto>();

            CreateMap<OrderInputDto, Order>();
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Price));

        }
    }
}
