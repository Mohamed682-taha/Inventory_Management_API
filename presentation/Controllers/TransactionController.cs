using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Errors;
using ServiceAbstraction;
using Shared.TransactionDto;

namespace Presentation.Controllers
{
    public class TransactionsController(IServiceManager _serviceManager) : ApiBaseController
    {
        // GET : BaseUrl/api/Transactions
        // Get all transaction history
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<IReadOnlyList<TransactionDto>>> GetAllTransactions()
        {
            var transactions = await _serviceManager.TransactionService.GetAllTransaction();
            return Ok(transactions);
        }

        // POST : BaseUrl/api/Transactions
        // Create transaction (sale / purchase)
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TransactionDto>> CreateTransaction(CreateTransactionDto dto)
        {
            var transaction = await _serviceManager.TransactionService.CreateTransaction(dto);
            if ( transaction is null )
                return NotFound(new ApiResponse(404,"Check ProductId / Check AppUserId"));
            return Ok(transaction);
        }

    }
}
