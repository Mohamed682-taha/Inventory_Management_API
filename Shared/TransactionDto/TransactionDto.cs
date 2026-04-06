namespace Shared.TransactionDto
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string AppUserName { get; set; } = string.Empty;
        public PaymentDto? Payment{ get; set; } 
    }
}
