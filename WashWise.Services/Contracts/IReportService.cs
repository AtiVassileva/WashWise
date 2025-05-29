using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAllAsync();
        Task<Report?> GetByIdAsync(Guid id);
        Task<IEnumerable<Report>> GetUserReports(string userId);
        Task<bool> MarkAsResolved(Guid id);
        Task ReportBreakdownAsync(Report report);
        Task DeleteUserReports(string userId);
    }
}