using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.TransactionDto;

namespace Presentation.Controllers
{
    public class TransactionsController(IServiceManager _serviceManager) : ApiBaseController
    {
        // GET : BaseUrl/api/Transactions
        // Get all transaction history
        [HttpGet]
        [Authorize(Roles ="Admin,Manager")]
        public async Task<ActionResult<IReadOnlyList<TransactionDto>>> GetAllTransactions()
        {
            var transactions = await _serviceManager.TransactionService.GetAllTransaction();
            return Ok(transactions);
        }


    }
}
