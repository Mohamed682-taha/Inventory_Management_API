using Domain.Interfaces;
using Domain.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared.ReportDto;

namespace Service
{
    class ReportService(IUnitOfWork _unitOfWork) : IReportService
    {
        public async Task<ReportDto> GenerateReportAsync()
        {
            var productSpecs = new ProductSpecifications();
            var products = await _unitOfWork.GetRepository<Product,int>().GetAllAsync(productSpecs);

            var transactionRepo = _unitOfWork.GetRepository<Transaction,int>();

            var saleSpecs = new TransactionSaleSpecification();
            var saleTransactions = await transactionRepo.GetAllAsync(saleSpecs);

            var purchaseSpecs = new TransactionPurchaseSpecification();
            var purchaseTransactions = await transactionRepo.GetAllAsync(purchaseSpecs);

            var salesByProduct = saleTransactions
                                .GroupBy(t => t.ProductId).ToDictionary(g => g.Key,g => g.ToList());

            var totalStockValue = products.Sum(p => p.Price * p.QuantityInStock);

            var productPerformance = products.Select(p =>
            {
                var sales = salesByProduct.ContainsKey(p.Id) ? salesByProduct[p.Id] : new List<Transaction>();
                return new ProductPerformanceDto
                {
                    Id = p.Id,
                    CategoryName = p.Category.Name,
                    Price = p.Price,
                    ProductName = p.Name,
                    TotalRevenue = sales.Sum(t => t.TotalAmount),
                    TotalUnitsSold = sales.Sum(t => t.Quantity)
                };
            }).ToList();

            var topProducts = productPerformance
                .OrderByDescending(t => t.TotalUnitsSold)
                .Take(5)
                .Select(p => new TopSellingProductDto
                {
                    ProductId = p.Id,
                    CategoryName = p.CategoryName,
                    ProductName = p.ProductName,
                    TotalUnitsSold = p.TotalUnitsSold,
                    TotalRevenue = p.TotalRevenue,
                }).ToList();

            var reportDto = new ReportDto
            {
                ProductPerformance = productPerformance,
                TopSellingProducts = topProducts,
                TotalPurchasesCount = purchaseTransactions.Count,
                TotalRevenue = saleTransactions.Sum(t => t.TotalAmount),
                TotalSalesCount = saleTransactions.Count,
                TotalStockValue = totalStockValue

            };
            return reportDto;
        }
    }
}
