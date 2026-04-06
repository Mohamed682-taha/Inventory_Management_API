using Domain.Models;

namespace Service.Specifications
{
    class LowStockSpecifications : BaseSpecifications<LowStockAlert,int>
    {
        public LowStockSpecifications(int productId) : base(l => l.ProductId == productId)
        {

        }
    }
}
