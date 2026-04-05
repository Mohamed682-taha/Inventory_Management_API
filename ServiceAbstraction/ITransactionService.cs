using Shared.TransactionDto;

namespace ServiceAbstraction
{
    public interface ITransactionService
    {
        Task<IReadOnlyList<TransactionDto>> GetAllTransaction();
        Task<TransactionDto?> CreateTransaction(CreateTransactionDto dto);
    }
}
