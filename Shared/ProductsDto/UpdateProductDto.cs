namespace Shared.ProductsDto
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryId { get; set; }
        public string Supplier { get; set; } = string.Empty;
    }
}
