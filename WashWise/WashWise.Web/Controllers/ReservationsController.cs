using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WashWise.Services.Contracts;
using WashWise.Web.Infrastructure;
using WashWise.Web.Models;

namespace WashWise.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IWashingMachineService _washingMachineService;
        private readonly IReservationService _reservationService;
        private readonly IBuildingService _buildingService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IWashingMachineService washingMachineService, IBuildingService buildingService, IMapper mapper)
        {
            _reservationService = reservationService;
            _washingMachineService = washingMachineService;
            _buildingService = buildingService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Create(Guid machineId, DateTime? date)
        {
            var selectedDate = date?.Date ?? DateTime.Today;

            var reserved = await _reservationService.GetReservedSlotsAsync(machineId, selectedDate);

            var allSlots = new List<ReservationSlotViewModel>();
            var slotLength = TimeSpan.FromHours(1);

            for (var hour = 8; hour < 20; hour++)
            {
                var slotStart = selectedDate.AddHours(hour);
                var slotEnd = slotStart.Add(slotLength);

                var isTaken = reserved.Any(r =>
                    slotStart < r.end && slotEnd > r.start);

                allSlots.Add(new ReservationSlotViewModel
                {
                    WashingMachineId = machineId,
                    SlotStart = slotStart,
                    SlotEnd = slotEnd,
                    IsAvailable = !isTaken
                });
            }
            
            var washingMachine = await _washingMachineService.GetByIdAsync(machineId);
            var building = await _buildingService.GetByIdAsync(washingMachine != null ? washingMachine.BuildingId : Guid.Empty);
            
            var viewModel = new ReservationViewModel
            {
                MachineModel = washingMachine?.Model,
                Location = string.Concat(building?.Name, ", ", building?.Address, " - ", building?.City),
                WashingMachineId = machineId,
                Date = selectedDate,
                ReservationSlots = allSlots
            };

            return View(viewModel);
        }

        public async Task<IActionResult> GetSlots(Guid machineId, DateTime date)
        {
            var reserved = await _reservationService.GetReservedSlotsAsync(machineId, date.Date);

            var allSlots = new List<ReservationSlotViewModel>();
            var slotLength = TimeSpan.FromHours(1);

            for (var hour = 8; hour < 20; hour++)
            {
                var slotStart = date.Date.AddHours(hour);
                var slotEnd = slotStart.Add(slotLength);

                var isTaken = reserved.Any(r =>
                    slotStart < r.end && slotEnd > r.start);

                allSlots.Add(new ReservationSlotViewModel
                {
                    WashingMachineId = machineId,
                    SlotStart = slotStart,
                    SlotEnd = slotEnd,
                    IsAvailable = !isTaken
                });
            }

            return PartialView("_ReservationSlotsPartial", allSlots);
        }

        public async Task<IActionResult> Mine()
        {
            var reservations = await _reservationService.GetUserReservationsAsync(User.GetId()!);
            var viewModel = _mapper.Map<List<MyReservationViewModel>>(reservations);
            return View(viewModel);
        }
        
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _reservationService.CancelReservationAsync(id, User.GetId()!);
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(ReservationInputModel input)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Невалидни данни за резервация.";
                return RedirectToAction("Create", new { machineId = input.MachineId, date = input.StartTime.Date });
            }

            var success = await _reservationService.ReserveAsync(input.MachineId, input.StartTime, User.GetId()!);

            if (success)
            {
                var machine = await _washingMachineService.GetByIdAsync(input.MachineId);
                TempData["SuccessMessage"] =
                    $"Вашата резервация за {machine!.Model}, {machine.Building.Name} - {machine.Building.Address}, {input.StartTime:dd.MM.yyyy HH:mm} е успешна!";
            }

            return RedirectToAction("Create", new { machineId = input.MachineId, date = input.StartTime.Date });
        }
    }
}