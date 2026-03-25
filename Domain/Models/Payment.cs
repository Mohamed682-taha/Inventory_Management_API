namespace Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; } = null!;
    }
}
