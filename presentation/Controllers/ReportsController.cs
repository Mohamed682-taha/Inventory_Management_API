using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.ReportDto;

namespace Presentation.Controllers
{
    public class ReportsController(IServiceManager _serviceManager) : ApiBaseController
    {
        // GET : BaseUrl/api/Reports
        [HttpGet]
        public async Task<ActionResult<ReportDto>> GenerateReport()
        {
            var report = await _serviceManager.ReportService.GenerateReportAsync();
            return Ok(report);
        }
    }
}
