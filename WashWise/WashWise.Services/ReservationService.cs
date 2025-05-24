using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;
using WashWise.Services.Contracts;

namespace WashWise.Services
{
    public class ReservationService : IReservationService
    {
        private readonly WashWiseDbContext _dbContext;
        private readonly IStatusService _statusService;

        public ReservationService(WashWiseDbContext dbContext, IStatusService statusService)
        {
            _dbContext = dbContext;
            _statusService = statusService;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
            => await _dbContext.Reservations
                .Include(r => r.Status)
                .Include(r => r.WashingMachine)
                .ToListAsync();

        public async Task<IEnumerable<Reservation>> GetUserReservationsAsync(string userId) 
            =>  await _dbContext.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.WashingMachine).ThenInclude(m => m.Building)
                .Include(r => r.Status)
                .OrderByDescending(r => r.StartTime)
                .ToListAsync();

        public async Task<IEnumerable<Reservation>> GetFinishedReservationsAsync()
        {
            var reservations = await GetAllAsync();

             var finishedReservations = reservations
                 .Where(r => r.EndTime <= DateTime.Now && r.Status.Name != "Приключена" && r.Status.Name != "Канселирана")
                 .ToList();

             return finishedReservations;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsInProgressAsync()
        {
            var now = DateTime.Now;
            var reservations = await GetAllAsync();

            var reservationsInProgress = reservations
                .Where(r => r.StartTime <= now && r.EndTime > now)
                .ToList();

            return reservationsInProgress;
        }

        public async Task<List<(DateTime start, DateTime end)>> GetReservedSlotsAsync(Guid machineId, DateTime day)
        {
            var startOfDay = day.Date.AddHours(8);
            var endOfDay = day.Date.AddHours(20);

            var reservations = await _dbContext.Reservations
                .Where(r => r.WashingMachineId == machineId 
                            && r.StartTime >= startOfDay && r.StartTime < endOfDay 
                            && (r.Status.Name == "В прогрес" || r.Status.Name == "Предстояща"))
                .Select(r => new { r.StartTime, r.EndTime })
                .ToListAsync();

            return reservations
                .Select(r => (r.StartTime, r.EndTime))
                .Where(r => r.EndTime > DateTime.Now)
                .ToList();
        }

        public async Task<DateTime?> GetWashingMachineOccupiedUntilTime(Guid washingMachineId)
        {
            var now = DateTime.Now;

            var currentReservationEndTime = await _dbContext.Reservations
                .Where(r =>
                    r.WashingMachineId == washingMachineId &&
                    r.StartTime <= now &&
                    r.EndTime > now)
                .Select(r => (DateTime?)r.EndTime)
                .FirstOrDefaultAsync();

            return currentReservationEndTime;
        }

        public async Task<IEnumerable<Reservation>> GetUpcomingReservations(Guid washingMachineId) =>
            await _dbContext.Reservations
                .Where(r => r.WashingMachineId == washingMachineId && r.StartTime > DateTime.Now)
                .ToListAsync();

        public async Task<bool> IsSlotAvailableAsync(Guid machineId, DateTime startTime)
        {
            var endTime = startTime.AddHours(1);

            return !await _dbContext.Reservations
                .AnyAsync(r =>
                    r.WashingMachineId == machineId &&
                    r.StartTime < endTime &&
                    r.EndTime > startTime);
        }

        public async Task<bool> ReserveAsync(Guid washingMachineId, DateTime startTime, string userId)
        {
            if (!await IsSlotAvailableAsync(washingMachineId, startTime))
                return false;

            var upcomingStatus = await _statusService.GetByNameAsync("Предстояща");

            var reservation = new Reservation
            {
                StatusId = upcomingStatus!.Id,
                WashingMachineId = washingMachineId,
                UserId = userId,
                StartTime = startTime,
                EndTime = startTime.AddHours(1)
            };

            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelReservationAsync(Guid reservationId, string userId)
        {
            var reservation = await _dbContext.Reservations
                .Include(r => r.Status)
                .FirstOrDefaultAsync(r => r.Id == reservationId && r.UserId == userId);

            if (reservation == null || reservation.StartTime <= DateTime.Now || reservation.Status.Name == "Канселирана")
                return false;

            var cancelledStatus = await _dbContext.Statuses.FirstOrDefaultAsync(s => s.Name == "Канселирана");
            if (cancelledStatus == null)
                throw new Exception("Missing status 'Канселирана'");

            reservation.StatusId = cancelledStatus.Id;
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task DeleteUserReservations(string userId)
        {
            var userReservations = await GetUserReservationsAsync(userId);

            if (!userReservations.Any())
            {
                return;
            }

            _dbContext.Reservations.RemoveRange(userReservations);
            await _dbContext.SaveChangesAsync();
        }
    }
}