using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WashWise.Services.Contracts;
using static WashWise.Web.Common.CommonConstants;

namespace WashWise.Web.Controllers
{
    [Authorize(Roles = AdministratorRoleName)]
    public class AdminReportsController : Controller
    {
        private readonly IReportService _reportService;

        public AdminReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            var reports = await _reportService.GetAllAsync();
            return View(reports);
        }

        public async Task<IActionResult> Details(Guid reportId)
        {
            var report = await _reportService.GetByIdAsync(reportId);

            if (report == null) return NotFound();

            return View(report);
        }

        public async Task<IActionResult> ResolveReport(Guid reportId)
        {
            var isResolved = await _reportService.MarkAsResolved(reportId);

            if (!isResolved)
            {
                TempData["Error"] = "Нещо се обърка, моля опитайте пак!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}