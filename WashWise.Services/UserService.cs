using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WashWise.Services.Contracts;

namespace WashWise.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IReservationService _reservationService;
        private readonly IReportService _reportService;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IReservationService reservationService, IReportService reportService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _reservationService = reservationService;
            _reportService = reportService;
        }

        public async Task<List<IdentityUser>> GetAllAsync()
        {
            return await _userManager.Users.OrderBy(u => u.Email).ToListAsync();
        }

        public async Task<IdentityUser?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, roles);
            }

            await _reservationService.DeleteUserReservations(id);
            await _reportService.DeleteUserReports(id);

            await _userManager.DeleteAsync(user);

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> MakeAdminAsync(string id, string adminRoleName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            if (!await _roleManager.RoleExistsAsync(adminRoleName))
                await _roleManager.CreateAsync(new IdentityRole(adminRoleName));

            if (!await _userManager.IsInRoleAsync(user, adminRoleName))
                await _userManager.AddToRoleAsync(user, adminRoleName);

            return true;
        }
    }
}