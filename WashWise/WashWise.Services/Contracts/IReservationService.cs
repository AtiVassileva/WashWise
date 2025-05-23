using WashWise.Models;

namespace WashWise.Services.Contracts
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetUserReservations(string userId);
        Task DeleteUserReservations(string userId);
    }
}