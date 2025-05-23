using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WashWise.Services.Contracts;
using WashWise.Web.Models;
using static WashWise.Web.Common.CommonConstants;

namespace WashWise.Web.Controllers
{
    [Authorize(Roles = AdministratorRoleName)]
    public class AdminUsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminUsersController(IUserService userService, UserManager<IdentityUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();
            var models = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                models.Add(new UserRoleViewModel
                {
                    User = user,
                    IsAdmin = roles.Contains(AdministratorRoleName)
                });
            }

            return View(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeAdmin(string id)
        {
            await _userService.MakeAdminAsync(id, AdministratorRoleName);
            return RedirectToAction(nameof(Index));
        }
    }
}