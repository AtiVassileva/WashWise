using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WashWise.Services.Contracts;
using WashWise.Web.Models;

namespace WashWise.Web.Controllers
{
    public class WashingMachinesController : Controller
    {
        private readonly IBuildingService _buildingService;
        private readonly IWashingMachineService _washingMachineService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public WashingMachinesController(IBuildingService buildingService, IWashingMachineService washingMachineService, IMapper mapper, IReservationService reservationService)
        {
            _buildingService = buildingService;
            _washingMachineService = washingMachineService;
            _mapper = mapper;
            _reservationService = reservationService;
        }
        
        public async Task<IActionResult> SelectBuilding()
        {
            var buildings = await _buildingService.GetAllAsync();

            var viewModel = new BuildingSelectionViewModel
            {
                Buildings = buildings
                    .Select(b => new SelectListItem
                    {
                        Value = b.Id.ToString(),
                        Text = string.Concat(b.Name, " - ", b.Address, ", ", b.City)
                    })
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ShowMachines(BuildingSelectionViewModel model)
        {
            return RedirectToAction(nameof(Status), new { buildingId = model.SelectedBuildingId });
        }

        public async Task<IActionResult> Status(Guid buildingId)
        {
            var machines = await _washingMachineService.GetWashingMachinesByBuildingId(buildingId);

            var viewModels = _mapper.Map<List<WashingMachineAvailabilityViewModel>>(machines);

            foreach (var vm in viewModels)
            {
                vm.OccupiedUntil = await _reservationService.GetWashingMachineOccupiedUntilTime(vm.WashingMachineId);
            }

            return View(viewModels);
        }
    }
}