using AutoMapper;
using Domain.Models;
using Shared.ProductsDto;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductDto>()
                .ForMember(dest => dest.CategoryName,opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<CreateProductDto,Product>();
            CreateMap<UpdateProductDto,Product>();
        }
    }
}
