using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IBuildingService
    {
        Task<IEnumerable<Building>> GetAllAsync();
        Task<Building?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(string name, string address, string city);
        Task<bool> SaveAsync(Building building);        
        Task<bool> UpdateAsync(Building building, string name, string address, string city);
        Task DeleteAsync(Guid id);
    }
}