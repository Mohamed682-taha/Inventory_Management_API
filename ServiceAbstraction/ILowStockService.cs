using Shared.ProductsDto;

namespace ServiceAbstraction
{
    public interface ILowStockService
    {
        Task CheckAndCreateAlertAsync(ProductDto dto);
    }
}
