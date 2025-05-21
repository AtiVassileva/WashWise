using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IBuildingService
    {
        Task<IEnumerable<Building>> GetAll();
        Task<Building?> GetById(Guid id);
        Task<bool> Exists(string name, string address, string city);
        Task<bool> SaveAsync(Building building);        
        Task<bool> Edit(Building building, string name, string address, string city);
        Task Delete(Guid id);
    }
}