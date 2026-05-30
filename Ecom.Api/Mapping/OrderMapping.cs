using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Identity;
using Ecom.Core.Entities.Order;

namespace Ecom.Api.Mapping
{
    public class OrderMapping:Profile
    {
        public OrderMapping() {
            CreateMap<Order, OrderToReturnDTO>()
                ;

            CreateMap<Order, OrderToReturnDTO>()
                 .ForMember(d => d.deliveryMethod,
                 o => o.
                 MapFrom(s => s.deliveryMethod.Name))
                 .ReverseMap();

            CreateMap<OrderItems, OrderItemDto>().ReverseMap();
            CreateMap<ShippingAddress, ShipAddressDto>().ReverseMap();
            CreateMap<Address, ShipAddressDto>().ReverseMap();

        }
    }
}
