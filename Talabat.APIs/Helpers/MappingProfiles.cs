using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Address = Talabat.Core.Entities.Identity.Address;

namespace Talabat.APIs.Helpers;

public class MappingProfiles  : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(d => d.ProductBrand, O
                => O.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, O
                => O.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, O
                => O.MapFrom<ProductPictureUrlResolver>());

        CreateMap<AddressDto, Address>().ReverseMap();
        CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
        CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        
        CreateMap<AddressDto, Core.Entities.Order_Aggregate.Address>().ReverseMap();


        CreateMap<Order, OrderToReturnDto>()
            .ForMember(d => d.DeliveryMethod, O
                => O.MapFrom(S => S.DeliveryMethod.ShortName))
            .ForMember(d => d.DeliveryMethodCost, O
                => O.MapFrom(S => S.DeliveryMethod.Cost));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductId, O
                => O.MapFrom(S => S.Id))
            .ForMember(d => d.ProductNamee, O
                => O.MapFrom(S => S.Product.ProductNamee))
            .ForMember(d => d.PictureUrl, O
                => O.MapFrom(S => S.Product.PictureUrl))
            .ForMember(d => d.PictureUrl, O
                => O.MapFrom<OrderItemPictureUrlResolver>());
    }   
    
    
    
}