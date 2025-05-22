using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;
using WashWise.Services.Contracts;

namespace WashWise.Services
{
    public class WashingMachineService : IWashingMachineService
    {
        private readonly WashWiseDbContext _context;
        private readonly IConditionService _conditionService;

        public WashingMachineService(WashWiseDbContext context, IConditionService conditionService)
        {
            _context = context;
            _conditionService = conditionService;
        }

        public async Task<List<WashingMachine>> GetAllAsync()
            => await _context.WashingMachines
                .Include(m => m.Building)
                .Include(m => m.Condition)
                .ToListAsync();

        public async Task<WashingMachine?> GetByIdAsync(Guid id)
            => await _context.WashingMachines.FirstOrDefaultAsync(wm => wm.Id == id);

        public async Task CreateAsync(WashingMachine machine)
        {
            var condition = await _conditionService.GetByNameAsync("свободна");

            if (condition == null) return;

            machine.ConditionId = condition.Id;

            _context.WashingMachines.Add(machine);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(WashingMachine machine)
        {
            var exists = await _context.WashingMachines.AnyAsync(m => m.Id == machine.Id);
            if (!exists) return false;

            _context.WashingMachines.Update(machine);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var machine = await GetByIdAsync(id);

            if (machine == null) return false;

            _context.WashingMachines.Remove(machine);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}