using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IActivityLogService
    {
        Task<IEnumerable<ActivityLog>> GetAllAsync();
    }
}