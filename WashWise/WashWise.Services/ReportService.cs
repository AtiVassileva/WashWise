using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;
using WashWise.Services.Contracts;

namespace WashWise.Services
{
    public class ReportService : IReportService
    {
        private readonly WashWiseDbContext _dbContext;
        private readonly IWashingMachineService _washingMachineService;
        private readonly IConditionService _conditionService;
        private readonly IStatusService _statusService;
        private readonly IReservationService _reservationService;

        public ReportService(WashWiseDbContext dbContext, IWashingMachineService washingMachineService, IConditionService conditionService, IStatusService statusService, IReservationService reservationService)
        {
            _dbContext = dbContext;
            _washingMachineService = washingMachineService;
            _conditionService = conditionService;
            _statusService = statusService;
            _reservationService = reservationService;
        }

        public async Task<IEnumerable<Report>> GetUserReports(string userId)
        {
            return await _dbContext.Reports
                .Where(r => r.AuthorId == userId)
                .ToListAsync();
        }

        public async Task ReportBreakdownAsync(Report report)
        {
            report.GeneratedAt = DateTime.Now;
            report.IsResolved = false;
            _dbContext.Reports.Add(report);
            
            var machine = await _washingMachineService.GetByIdAsync(report.WashingMachineId);
            var brokenCondition = await _conditionService.GetByNameAsync("Повредена");
            machine!.ConditionId = brokenCondition!.Id;
            
            var cancelledStatus = await _statusService.GetByNameAsync("Канселирана");

            var upcomingReservations = await _reservationService.GetUpcomingReservations(report.WashingMachineId);

            upcomingReservations
                .ToList()
                .ForEach(r => r.StatusId = cancelledStatus!.Id);

            await _dbContext.SaveChangesAsync();
            
            //await _notifier.NotifyAdminsAsync(
            //    subject: $"[WashWise] Повреда на пералня {machine.Model}",
            //    message: $"Потребител {report.UserId} докладва: {report.Description}"
            //);
        }

        public async Task DeleteUserReports(string userId)
        {
            var userReports = await GetUserReports(userId);

            if (!userReports.Any())
            {
                return;
            }

            _dbContext.Reports.RemoveRange(userReports);
            await _dbContext.SaveChangesAsync();
        }
    }
}