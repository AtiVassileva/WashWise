using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation?> GetByIdAsync(Guid id);
        Task<IEnumerable<Reservation>> GetUserReservationsAsync(string userId);
        Task<IEnumerable<Reservation>> GetFinishedReservationsAsync();
        Task<IEnumerable<Reservation>> GetReservationsInProgressAsync();
        Task<List<(DateTime start, DateTime end)>> GetReservedSlotsAsync(Guid machineId, DateTime day);
        Task<DateTime?> GetWashingMachineOccupiedUntilTime(Guid washingMachineId);
        Task<IEnumerable<Reservation>> GetUpcomingReservations(Guid washingMachineId);
        Task<bool> IsSlotAvailableAsync(Guid machineId, DateTime startTime);
        Task<bool> ReserveAsync(Guid washingMachineId, DateTime startTime, string userId);
        Task<bool> CancelReservationAsync(Guid reservationId, string userId);
        Task DeleteUserReservations(string userId);
    }
}