using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using ServiceAbstraction;
using Shared.CategoryDto;

namespace Service
{
    class CategoryService(IUnitOfWork _unitOfWork,IMapper _mapper) : ICategoryService
    {
        public async Task<int> AddProductAsync(CreateCategoryDto dto)
        {
            var mappedCategory = _mapper.Map<CreateCategoryDto,Category>(dto);
            await _unitOfWork.GetRepository<Category,int>().AddAsync(mappedCategory);
            var added = await _unitOfWork.SaveChangesAsync();
            if ( added > 0 )
                return 1;
            return 0;
        }
        public async Task<int> UpdateCategoryAsync(CategoryDto dto)
        {
            var mappedCategory = _mapper.Map<CategoryDto,Category>(dto);
            _unitOfWork.GetRepository<Category,int>().Update(mappedCategory);
            var updated = await _unitOfWork.SaveChangesAsync();
            if ( updated > 0 )
                return 1;
            return 0;
        }
        public async Task<bool> DeleteCategoryAsync(int Id)
        {
            var repo = _unitOfWork.GetRepository<Category,int>();
            var category = await repo.GetByIdAsync(Id);
            if ( category is null )
                return false;
            repo.Remove(category);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.GetRepository<Category,int>().GetAllAsync();
            var mappedCategories = _mapper.Map<IReadOnlyList<Category>,IReadOnlyList<CategoryDto>>(categories);
            return mappedCategories;
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int Id)
        {
            var category = await _unitOfWork.GetRepository<Category,int>().GetByIdAsync(Id);
            if ( category is null )
                return null;
            var mappedCategory = _mapper.Map<Category,CategoryDto>(category);
            return mappedCategory;
        }

    }
}
