using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;
using WashWise.Services.Contracts;

namespace WashWise.Services
{
    public class ReservationService : IReservationService
    {
        private readonly WashWiseDbContext _dbContext;

        public ReservationService(WashWiseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Reservation>> GetUserReservations(string userId)
        {
            return await _dbContext.Reservations
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task DeleteUserReservations(string userId)
        {
            var userReservations = await GetUserReservations(userId);

            if (!userReservations.Any())
            {
                return;
            }

            _dbContext.Reservations.RemoveRange(userReservations);
            await _dbContext.SaveChangesAsync();
        }
    }
}