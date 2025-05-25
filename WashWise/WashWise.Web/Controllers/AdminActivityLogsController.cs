using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WashWise.Services.Contracts;
using static WashWise.Web.Common.CommonConstants;

namespace WashWise.Web.Controllers
{
    [Authorize(Roles = AdministratorRoleName)]
    public class AdminActivityLogsController : Controller
    {
        private readonly IActivityLogService _activityLogService;

        public AdminActivityLogsController(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _activityLogService.GetAllAsync();
            return View(logs);
        }
    }
}