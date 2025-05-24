using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetUserReports(string userId);
        Task ReportBreakdownAsync(Report report);
        Task DeleteUserReports(string userId);
    }
}