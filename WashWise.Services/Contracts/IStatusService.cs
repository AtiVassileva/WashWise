using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IStatusService
    {
        Task<List<Status>> GetAllAsync();
        Task<Status?> GetByNameAsync(string name);
    }
}