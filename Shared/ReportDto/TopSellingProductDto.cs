namespace Shared.ReportDto
{
    public class TopSellingProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int TotalUnitsSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
