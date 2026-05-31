using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;

namespace Ecom.Api.Mapping
{
    public class RatingMapping:Profile
    {
        public RatingMapping() {

            CreateMap<ProductRating, RatingMapping>();

            CreateMap<CreateRatingDTO, ProductRating>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore());
        }

    }
}
