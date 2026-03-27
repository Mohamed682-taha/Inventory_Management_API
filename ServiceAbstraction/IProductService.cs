using Shared.ProductsDto;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int Id);
        void UpdateProduct(ProductDto dto);
        Task DeleteProduct(int Id);
        Task AddProductAsync(ProductDto dto);
    }
}
