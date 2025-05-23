using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;
using WashWise.Services.Contracts;

namespace WashWise.Services
{
    public class ReportService : IReportService
    {
        private readonly WashWiseDbContext _dbContext;

        public ReportService(WashWiseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Report>> GetUserReports(string userId)
        {
            return await _dbContext.Reports
                .Where(r => r.AuthorId == userId)
                .ToListAsync();
        }

        public async Task DeleteUserReports(string userId)
        {
            var userReports = await GetUserReports(userId);

            if (!userReports.Any())
            {
                return;
            }

            _dbContext.Reports.RemoveRange(userReports);
            await _dbContext.SaveChangesAsync();
        }
    }
}