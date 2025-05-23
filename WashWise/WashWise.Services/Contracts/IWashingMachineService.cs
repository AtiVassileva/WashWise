﻿using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IWashingMachineService
    {
        Task<List<WashingMachine>> GetAllAsync();
        Task<WashingMachine?> GetByIdAsync(Guid id);
        Task<IEnumerable<WashingMachine>> GetWashingMachinesByBuildingId(Guid buildingId);
        Task CreateAsync(WashingMachine machine);
        Task<bool> UpdateAsync(WashingMachine machine);
        Task<bool> DeleteAsync(Guid id);
    }
}