using Shared.ReportDto;

namespace ServiceAbstraction
{
    public interface IReportService
    {
        Task<ReportDto> GenerateReportAsync();
    }
}
