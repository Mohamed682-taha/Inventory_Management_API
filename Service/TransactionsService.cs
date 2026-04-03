using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared.TransactionDto;

namespace Service
{
    class TransactionsService(IUnitOfWork _unitOfWork,IMapper _mapper) : ITransactionService
    {
        public async Task<IReadOnlyList<TransactionDto>> GetAllTransaction()
        {
            var specs = new TransactionSpecification();
            var transactions = await _unitOfWork.GetRepository<Transaction,int>().GetAllAsync(specs);
            var mappedTransactions = _mapper.Map<IReadOnlyList<Transaction>,IReadOnlyList<TransactionDto>>(transactions);
            return mappedTransactions;
        }
        //TODO
        // POST /api/transactions
        // Accessible by: Admin, Manager, Staff
        // Purpose: Record a sale or purchase transaction
        // From PDF: "As a staff member, I want to record a sale
        //            when a customer buys a product"
        //           "As a staff member, I want to record a purchase
        //            when new stock is received from suppliers"
    }
}
