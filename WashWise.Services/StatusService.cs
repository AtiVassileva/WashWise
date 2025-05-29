using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;
using WashWise.Services.Contracts;

namespace WashWise.Services
{
    public class StatusService : IStatusService
    { 
        private readonly WashWiseDbContext _dbContext;

        public StatusService(WashWiseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Status?> GetByNameAsync(string name)
            => await _dbContext.Statuses.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());

        public async Task<List<Status>> GetAllAsync() => await _dbContext.Statuses.ToListAsync();
    }
}