using Shared.ProductsDto;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int Id);
        Task<int> UpdateProduct(int Id,UpdateProductDto dto);
        Task<bool> DeleteProduct(int Id);
        Task<int> AddProductAsync(CreateProductDto dto);
    }
}
