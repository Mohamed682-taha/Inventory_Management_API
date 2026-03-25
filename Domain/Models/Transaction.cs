namespace Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int ProductId { get; set; }
        public int AppUserId { get; set; }
        public Product Product { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;

    }
}
