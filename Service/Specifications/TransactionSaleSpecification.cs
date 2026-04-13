using Domain.Models;

namespace Service.Specifications
{
    class TransactionSaleSpecification : BaseSpecifications<Transaction,int>
    {
        public TransactionSaleSpecification() : base(t => t.Type == TransactionType.Sale)
        {

        }
    }
}
