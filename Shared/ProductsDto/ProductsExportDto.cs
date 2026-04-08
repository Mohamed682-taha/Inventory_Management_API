namespace Shared.ProductsDto
{
    public class ProductsExportDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public string CategoryName { get; set; } = null!;
    }
}
