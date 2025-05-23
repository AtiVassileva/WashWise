using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WashWise.Models;
using WashWise.Services.Contracts;
using WashWise.Web.Models;
using static WashWise.Web.Common.CommonConstants;

namespace WashWise.Web.Controllers
{
    [Authorize(Roles = AdministratorRoleName)]
    public class AdminWashingMachinesController : Controller
    {
        private readonly IBuildingService _buildingService;
        private readonly IConditionService _conditionService;
        private readonly IWashingMachineService _washingMachineService;
        private readonly IMapper _mapper;

        public AdminWashingMachinesController(IWashingMachineService washingMachineService, IMapper mapper, IBuildingService buildingService, IConditionService conditionService)
        {
            _washingMachineService = washingMachineService;
            _mapper = mapper;
            _buildingService = buildingService;
            _conditionService = conditionService;
        }

        public async Task<IActionResult> Index()
        {
            var machines = await _washingMachineService.GetAllAsync();
            var model = _mapper.Map<List<WashingMachineViewModel>>(machines);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = new WashingMachineFormModel();
            await PopulateDropdownsAsync(model);
            return View("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WashingMachineFormModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model);
                return View("Form", model);
            }

            var entity = _mapper.Map<WashingMachine>(model);
            await _washingMachineService.CreateAsync(entity);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var machine = await _washingMachineService.GetByIdAsync(id);
            if (machine == null) return NotFound();

            var model = _mapper.Map<WashingMachineFormModel>(machine);
            await PopulateDropdownsAsync(model);

            return View("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WashingMachineFormModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(model);
                return View("Form", model);
            }

            var entity = _mapper.Map<WashingMachine>(model);
            var success = await _washingMachineService.UpdateAsync(entity);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _washingMachineService.DeleteAsync(id);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdownsAsync(WashingMachineFormModel model)
        {
            var buildings = await _buildingService.GetAllAsync();
            var conditions = await _conditionService.GetAllAsync();

            model.Buildings = buildings.Select(b => new SelectListItem(string.Concat(b.Name, " - ", b.Address), b.Id.ToString()))
                .ToList();
            model.Conditions = conditions.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
        }
    }
}