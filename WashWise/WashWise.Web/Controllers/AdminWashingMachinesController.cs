using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;

namespace WashWise.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminWashingMachinesController : Controller
    {
        private readonly WashWiseDbContext _context;

        public AdminWashingMachinesController(WashWiseDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var machines = await _context.WashingMachines
                .Include(m => m.Building)
                .Include(m => m.Condition)
                .ToListAsync();

            return View(machines);
        }

        public IActionResult Create()
        {
            ViewData["Buildings"] = new SelectList(_context.Buildings, "Id", "Address");
            ViewData["Conditions"] = new SelectList(_context.Conditions, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WashingMachine machine)
        {
            if (ModelState.IsValid)
            {
                _context.WashingMachines.Add(machine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Buildings"] = new SelectList(_context.Buildings, "Id", "Address", machine.BuildingId);
            ViewData["Conditions"] = new SelectList(_context.Conditions, "Id", "Name", machine.ConditionId);
            return View(machine);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var machine = await _context.WashingMachines.FindAsync(id);
            if (machine == null) return NotFound();

            ViewData["Buildings"] = new SelectList(_context.Buildings, "Id", "Address", machine.BuildingId);
            ViewData["Conditions"] = new SelectList(_context.Conditions, "Id", "Name", machine.ConditionId);
            return View(machine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WashingMachine machine)
        {
            if (id != machine.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(machine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Buildings"] = new SelectList(_context.Buildings, "Id", "Address", machine.BuildingId);
            ViewData["Conditions"] = new SelectList(_context.Conditions, "Id", "Name", machine.ConditionId);
            return View(machine);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var machine = await _context.WashingMachines
                .Include(m => m.Building)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (machine == null) return NotFound();

            return View(machine);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var machine = await _context.WashingMachines.FindAsync(id);
            if (machine != null)
            {
                _context.WashingMachines.Remove(machine);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}