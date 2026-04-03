using Shared.TransactionDto;

namespace ServiceAbstraction
{
    public interface ITransactionService
    {
        //Get all transaction history
        Task<IReadOnlyList<TransactionDto>> GetAllTransaction();
    }
}
