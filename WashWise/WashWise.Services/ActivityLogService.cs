using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;
using WashWise.Services.Contracts;

namespace WashWise.Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly WashWiseDbContext _dbContext;

        public ActivityLogService(WashWiseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ActivityLog>> GetAllAsync() 
            => await _dbContext.ActivityLogs
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
    }
}