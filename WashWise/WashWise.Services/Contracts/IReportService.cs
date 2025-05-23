using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetUserReports(string userId);
        Task DeleteUserReports(string userId);
    }
}