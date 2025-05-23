using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetUserReservations(string userId);
        Task<DateTime?> GetWashingMachineOccupiedUntilTime(Guid washingMachineId);
        Task DeleteUserReservations(string userId);
    }
}