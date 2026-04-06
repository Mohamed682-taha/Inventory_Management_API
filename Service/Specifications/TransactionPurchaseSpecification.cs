using Domain.Models;

namespace Service.Specifications
{
    class TransactionPurchaseSpecification : BaseSpecifications<Transaction,int>
    {
        public TransactionPurchaseSpecification() : base(t => t.Type == TransactionType.Purchase)
        {

        }
    }
}
