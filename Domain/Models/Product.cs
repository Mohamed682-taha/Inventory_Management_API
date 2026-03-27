namespace Domain.Models
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<Transaction> Transactions { get; set; } = [];
        public ICollection<LowStockAlert> LowStockAlerts { get; set; } = [];

    }
}
