using AutoMapper;
using Domain.Models;
using Shared.ProductsDto;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto,Product>().ReverseMap();
                //.ForMember(dest => dest.Category.Name,opt => opt.MapFrom(src => src.CategoryName))
            CreateMap<CreateProductDto,Product>();
            CreateMap<UpdateProductDto,Product>();
        }
    }
}
