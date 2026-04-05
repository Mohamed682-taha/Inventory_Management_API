namespace Shared.TransactionDto
{
    public class CreateTransactionDto
    {
        public int Quantity { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal? TotalAmount { get; set; }
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public PaymentDto? Payment { get; set; }
    }
}
