using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Service.Specifications;
using ServiceAbstraction;
using Shared.ProductsDto;
using Shared.TransactionDto;

namespace Service
{
    class TransactionsService(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        UserManager<AppUser> _userManager,
        ILowStockService _lowStockService) : ITransactionService
    {
        public async Task<IReadOnlyList<TransactionDto>> GetAllTransaction()
        {
            var specs = new TransactionSpecification();
            var transactions = await _unitOfWork.GetRepository<Transaction,int>().GetAllAsync(specs);
            var mappedTransactions = _mapper.Map<IReadOnlyList<Transaction>,IReadOnlyList<TransactionDto>>(transactions);
            return mappedTransactions;
        }

        public async Task<TransactionDto?> CreateTransaction(CreateTransactionDto dto)
        {
            var product = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(dto.ProductId);
            if ( product is null )
                return null;
            var user = await _userManager.FindByIdAsync(dto.AppUserId);
            if ( user is null )
                return null;

            switch ( dto.Type.ToLower() )
            {
                case "sale":
                    if ( product.QuantityInStock == 0 )
                        throw new Exception("Quantity in stock is zero");
                    product.QuantityInStock -= dto.Quantity;
                    break;
                case "purchase":
                    product.QuantityInStock += dto.Quantity;
                    break;
                default:
                    throw new Exception("Invalid transaction type: {dto.Type}");
            }
            dto.TotalAmount = product.Price * dto.Quantity;

            var mappedTransaction = _mapper.Map<CreateTransactionDto,Transaction>(dto);
            await _unitOfWork.GetRepository<Transaction,int>().AddAsync(mappedTransaction);
            var result = await _unitOfWork.SaveChangesAsync();

            if ( result == 0 )
                throw new BadRequestException(["Failed to add transaction"]);

            var transactionToReturn = _mapper.Map<Transaction,TransactionDto>(mappedTransaction);

            if ( dto.Type == "Sale" )
            {
                var mappedProduct = _mapper.Map<Product,ProductDto>(product);
                await _lowStockService.CheckAndCreateAlertAsync(mappedProduct);
            }

            return transactionToReturn;
        }


    }
}
