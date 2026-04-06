using Shared.CategoryDto;

namespace ServiceAbstraction
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(int Id);
        Task<int> AddProductAsync(CreateCategoryDto dto);
        Task<bool> DeleteCategoryAsync(int Id);
        Task<int> UpdateCategoryAsync(CategoryDto dto);
    }
}
