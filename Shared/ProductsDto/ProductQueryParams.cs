namespace Shared.ProductsDto
{
    public class ProductQueryParams
    {
        private const int MaxPageSize = 15;

        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;
        public string? SearchName { get; set; }
        public int? CategoryId { get; set; }
        public string? Supplier { get; set; }
        public int? MinStock { get; set; }
        public int? MaxStock { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }
    }
}
