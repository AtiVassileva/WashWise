using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;
using WashWise.Services.Contracts;

namespace WashWise.Services
{
    public class ConditionService : IConditionService
    {
        private readonly WashWiseDbContext _dbContext;

        public ConditionService(WashWiseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Condition?> GetByIdAsync(Guid id)
            => await _dbContext.Conditions.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Condition?> GetByNameAsync(string name)
            => await _dbContext.Conditions.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());

        public async Task<List<Condition>> GetAllAsync() 
            => await _dbContext.Conditions.ToListAsync();
    }
}