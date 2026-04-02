using Domain.Models;
using Shared.ProductsDto;

namespace Service.Specifications
{
    class ProductSpecifications : BaseSpecifications<Product,int>
    {
        public ProductSpecifications(ProductQueryParams queryParams) : base(p =>
        (
            ( !queryParams.CategoryId.HasValue || p.CategoryId == queryParams.CategoryId ) &&
            ( !queryParams.MaxStock.HasValue || p.QuantityInStock <= queryParams.MaxStock ) &&
            ( !queryParams.MinStock.HasValue || p.QuantityInStock >= queryParams.MinStock ) &&
            ( string.IsNullOrWhiteSpace(queryParams.Supplier) || p.Supplier.ToLower().Contains(queryParams.Supplier.ToLower()) ) &&
            ( string.IsNullOrWhiteSpace(queryParams.SearchName) || p.Name.ToLower().Contains(queryParams.SearchName!.ToLower()) )
        )
        )

        {
            AddIncludes(p => p.Category);
            switch ( queryParams.SortingOptions )
            {
                case ProductSortingOptions.NameAsc:
                    OrderByAscending(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    OrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    OrderByAscending(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    OrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
            AddPagination(queryParams.PageSize,queryParams.PageIndex);
        }

        public ProductSpecifications(int Id) : base(p => p.Id == Id)
        {
            AddIncludes(p => p.Category);
        }
    }
}
