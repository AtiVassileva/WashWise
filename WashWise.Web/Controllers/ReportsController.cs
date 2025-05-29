using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WashWise.Models;
using WashWise.Services.Contracts;
using WashWise.Web.Infrastructure;
using WashWise.Web.Models;

namespace WashWise.Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IWashingMachineService _washingMachineService;
        private readonly IMapper _mapper;

        public ReportsController(IReportService reportService, IMapper mapper, IWashingMachineService washingMachineService)
        {
            _reportService = reportService;
            _mapper = mapper;
            _washingMachineService = washingMachineService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid machineId)
        {
            var washingMachine = await _washingMachineService.GetByIdAsync(machineId);

            if (washingMachine == null)
            {
                return BadRequest();
            }

            var model = new ReportInputModel
            {
                WashingMachineId = machineId,
                BuildingAddress = string.Concat(washingMachine.Building.Name, ", ", washingMachine.Building.Address,
                    " - ", washingMachine.Building.City),
                WashingMachineModel = washingMachine.Model
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReportInputModel input)
        {
            if (!ModelState.IsValid)
                return View(input);
            
            var reportEntity = _mapper.Map<Report>(input);
            reportEntity.AuthorId = User.GetId()!;

            await _reportService.ReportBreakdownAsync(reportEntity);
            TempData["SuccessMessage"] = "Повредата беше докладвана успешно!";

            return LocalRedirect("/Reservations/Mine");
        }
    }
}