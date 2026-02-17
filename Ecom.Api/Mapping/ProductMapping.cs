using AutoMapper;
using Ecom.Core.Entities.Product;
using Ecom.Core.DTO;
namespace Ecom.Api.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();


            CreateMap<AddProductDto, Product>().ForMember(m => m.Photos, opt => opt.Ignore()).
            ReverseMap();

        }
    }
}
