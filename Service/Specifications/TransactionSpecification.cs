using Domain.Models;

namespace Service.Specifications
{
    class TransactionSpecification : BaseSpecifications<Transaction,int>
    {
        public TransactionSpecification()
        {
            AddIncludes(t => t.AppUser);
            AddIncludes(t => t.Product);
            AddIncludes(t => t.Payment);
        }
        public TransactionSpecification(int productId) : base(t => t.ProductId == productId && t.Type == TransactionType.Sale)
        {

        }

        
    }
}
