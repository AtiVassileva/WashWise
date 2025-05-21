using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Web.Models;

namespace WashWise.Web.Controllers
{
    public class WashingMachinesController : Controller
    {
        private readonly WashWiseDbContext _context;

        public WashingMachinesController(WashWiseDbContext context)
        {
            _context = context;
        }
        
        public IActionResult SelectBuilding()
        {
            var viewModel = new BuildingSelectionViewModel
            {
                Buildings = _context.Buildings
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

        public IActionResult Status(Guid buildingId)
        {
            var machines = _context.WashingMachines
                .Include(m => m.Condition)
                .Where(m => m.BuildingId == buildingId)
                .Select(m => new WashingMachineAvailabilityViewModel
                {
                    WashingMachineId = m.Id,
                    Model = m.Model,
                    Condition = m.Condition!.Name,
                    OccupiedUntil = _context.Reservations
                        .Where(r => r.WashingMachineId == m.Id && r.EndTime > DateTime.UtcNow)
                        .OrderByDescending(r => r.EndTime)
                        .Select(r => (DateTime?)r.EndTime)
                        .FirstOrDefault()
                })
                .ToList();

            return View(machines);
        }
    }
}
