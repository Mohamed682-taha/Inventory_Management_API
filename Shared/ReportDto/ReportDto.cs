namespace Shared.ReportDto
{
    public class ReportDto
    {
        public decimal TotalStockValue { get; set; }
        public List<ProductPerformanceDto> ProductPerformance { get; set; } = [];
        public List<TopSellingProductDto> TopSellingProducts { get; set; } = [];
        public decimal TotalRevenue { get; set; }
        public int TotalPurchasesCount { get; set; }
        public int TotalSalesCount { get; set; }
    }
}
