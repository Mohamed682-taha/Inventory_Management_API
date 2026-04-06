using Shared;
using Shared.ProductsDto;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
        Task<ProductDto?> GetProductByIdAsync(int Id);
        Task<int> UpdateProductAsync(int Id,UpdateProductDto dto);
        Task<bool> DeleteProductAsync(int Id);
        Task<int> AddProductAsync(CreateProductDto dto);
    }
}
