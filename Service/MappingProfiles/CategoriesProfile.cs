using AutoMapper;
using Domain.Models;
using Shared.CategoryDto;

namespace Service.MappingProfiles
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<CreateCategoryDto,Category>();
            CreateMap<Category,CategoryDto>().ReverseMap();
        }
    }
}
