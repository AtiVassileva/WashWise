using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IConditionService
    {
        Task<List<Condition>> GetAllAsync();
        Task<Condition?> GetByIdAsync(Guid id);
        Task<Condition?> GetByNameAsync(string name);
    }
}