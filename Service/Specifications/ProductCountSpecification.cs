using Domain.Models;
using Shared.ProductsDto;

namespace Service.Specifications
{
    class ProductCountSpecification : BaseSpecifications<Product,int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) : base(p =>
        (
            ( !queryParams.CategoryId.HasValue || p.CategoryId == queryParams.CategoryId ) &&
            ( !queryParams.MaxStock.HasValue || p.QuantityInStock <= queryParams.MaxStock ) &&
            ( !queryParams.MinStock.HasValue || p.QuantityInStock >= queryParams.MinStock ) &&
            ( string.IsNullOrWhiteSpace(queryParams.Supplier) || p.Supplier.ToLower().Contains(queryParams.Supplier.ToLower()) ) &&
            ( string.IsNullOrWhiteSpace(queryParams.SearchName) || p.Name.ToLower().Contains(queryParams.SearchName!.ToLower()) )
        ))
        {

        }
    }
}
