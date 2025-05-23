﻿using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;
using WashWise.Services.Contracts;

namespace WashWise.Services
{
    public class WashingMachineService : IWashingMachineService
    {
        private readonly WashWiseDbContext _dbContext;
        private readonly IConditionService _conditionService;

        public WashingMachineService(WashWiseDbContext dbContext, IConditionService conditionService)
        {
            _dbContext = dbContext;
            _conditionService = conditionService;
        }

        public async Task<List<WashingMachine>> GetAllAsync()
            => await _dbContext.WashingMachines
                .Include(m => m.Building)
                .Include(m => m.Condition)
                .ToListAsync();

        public async Task<WashingMachine?> GetByIdAsync(Guid id)
            => await _dbContext.WashingMachines.Include(w => w.Building).FirstOrDefaultAsync(wm => wm.Id == id);

        public async Task<IEnumerable<WashingMachine>> GetWashingMachinesByBuildingId(Guid buildingId) 
            => await _dbContext.WashingMachines
                .Include(m => m.Condition)
                .Where(m => m.BuildingId == buildingId)
                .Where(wm => wm.Condition!.Name != "Повредена")
                .ToListAsync();

        public async Task CreateAsync(WashingMachine machine)
        {
            var condition = await _conditionService.GetByNameAsync("свободна");

            if (condition == null) return;

            machine.ConditionId = condition.Id;

            _dbContext.WashingMachines.Add(machine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(WashingMachine machine)
        {
            var exists = await _dbContext.WashingMachines.AnyAsync(m => m.Id == machine.Id);
            if (!exists) return false;

            _dbContext.WashingMachines.Update(machine);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var machine = await GetByIdAsync(id);

            if (machine == null) return false;

            _dbContext.WashingMachines.Remove(machine);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}