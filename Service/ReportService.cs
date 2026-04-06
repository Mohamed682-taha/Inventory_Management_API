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
            // Calculate total stock value 
            var products = await _unitOfWork.GetRepository<Product,int>().GetAllAsync();
            var totalStockValue = products.Sum(p => p.Price * p.QuantityInStock);

            // Calculate ProductPerformance per product
            List<ProductPerformanceDto> productPerformanceDto = [];

            foreach ( var product in products )
            {
                // need to find transaction of type "sale" with "product Id"
                var performanceSpec = new TransactionSpecification(product.Id);
                // all transaction of productId=17 && type = sale
                var performanceTransaction = await _unitOfWork.GetRepository<Transaction,int>().GetAllAsync(performanceSpec);
                var performanceTotalRevenue = performanceTransaction.Sum(t => t.TotalAmount);
                var performanceTotalUnitsSold = performanceTransaction.Sum(t => t.Quantity);

                var productPerformance = new ProductPerformanceDto
                {
                    Id = product.Id,
                    ProductName = product.Name,
                    CategoryName = product.Category.Name,
                    Price = product.Price,
                    TotalRevenue = performanceTotalRevenue,
                    TotalUnitsSold = performanceTotalUnitsSold
                };

                productPerformanceDto.Add(productPerformance);
            }

            // Calculate TopSellingProducts
            List<TopSellingProductDto> topSellingProductDto = [];
            foreach ( var product in products )
            {
                var topSellingSpecs = new TransactionSpecification(product.Id);
                var topSellingTransaction = await _unitOfWork.GetRepository<Transaction,int>().GetAllAsync(topSellingSpecs);
                var topSellingTotalRevenue = topSellingTransaction.Sum(t => t.TotalAmount);
                var topSellingTotalUnitsSold = topSellingTransaction.Sum(t => t.Quantity);

                var topSellingProduct = new TopSellingProductDto
                {
                    ProductId = product.Id,
                    CategoryName = product.Category.Name,
                    ProductName = product.Name,
                    TotalRevenue = topSellingTotalRevenue,
                    TotalUnitsSold = topSellingTotalUnitsSold
                };
                topSellingProductDto.Add(topSellingProduct);
            }
            var topProducts = topSellingProductDto.OrderByDescending(p => p.TotalUnitsSold).Take(5).ToList();

            // Calculate total revenue
            // all transactions with type sale
            var saleSpecs = new TransactionSaleSpecification();
            var SaleTransaction = await _unitOfWork.GetRepository<Transaction,int>().GetAllAsync(saleSpecs);
            var totalRevenue = SaleTransaction.Sum(t => t.TotalAmount);

            // Calculate total purcahse count
            // all transaction with type purchase
            var purchaseSpecs = new TransactionPurchaseSpecification();
            var purchaseTransaction = await _unitOfWork.GetRepository<Transaction,int>().GetAllAsync(purchaseSpecs);
            var totalPurchaseCount = purchaseTransaction.Count;

            //Calculate total sales count
            //all transaction with type sale
            var totalSaleCount = SaleTransaction.Count;

            var reportDto = new ReportDto
            {
                ProductPerformance = productPerformanceDto,
                TopSellingProducts = topProducts,
                TotalPurchasesCount = totalPurchaseCount,
                TotalRevenue = totalRevenue,
                TotalSalesCount = totalSaleCount,
                TotalStockValue = totalStockValue

            };
            return reportDto;
        }
    }
}
