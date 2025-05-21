using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WashWise.Models;
using WashWise.Services.Contracts;
using WashWise.Web.Models;

namespace WashWise.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminBuildingsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBuildingService _buildingService;

        public AdminBuildingsController(IMapper mapper, IBuildingService buildingService)
        {
            _mapper = mapper;
            _buildingService = buildingService;
        }

        public async Task<IActionResult> Index()
        {
            var buildings = await _buildingService.GetAll();
            var buildingModels = _mapper.Map<List<BuildingFormViewModel>>(buildings);

            return View(buildingModels);
        }

        public IActionResult Create() => View("Form");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BuildingFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", model);
            }

            var exists = await _buildingService.Exists(model.Name, model.Address, model.City);

            if (exists)
            {
                ViewData["Error"] = "Блокът вече съществува!";
                return View("Form", model);
            }

            var building = _mapper.Map<Building>(model);

            var isSaved = await _buildingService.SaveAsync(building);

            if (!isSaved)
            {
                ViewData["Error"] = "Възникна грешка при запазването. Моля, опитайте пак!";
                return View("Form", model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var building = await _buildingService.GetById(id);

            if (building == null) return NotFound();

            var model = _mapper.Map<BuildingFormViewModel>(building);

            return View("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BuildingFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Form", model);

            var building = await _buildingService.GetById(id);

            if (building == null)
                return NotFound();
            
            if (building.VersionNo != model.VersionNo)
            {
                ViewData["Error"] = "Данните са били променени от друг потребител. Моля, презаредете страницата и опитайте отново!";
                return View("Form", model);
            }

            var isEdited = await _buildingService.Edit(building, model.Name, model.Address, model.City);

            if (!isEdited)
            {
                ViewData["Error"] = "Възникна грешка при запазването. Моля, опитайте пак!";
                return View("Form", model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _buildingService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}