using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WashWise.Services.Contracts;
using WashWise.Web.Models;
using static WashWise.Web.Common.CommonConstants;

namespace WashWise.Web.Controllers
{
    [Authorize(Roles = AdministratorRoleName)]
    public class AdminReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public AdminReservationsController(IReservationService reservationService, IMapper mapper, IStatusService statusService)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationService.GetAllAsync();
            var viewModel = _mapper.Map<IEnumerable<ReservationListViewModel>>(reservations);
            return View(viewModel);
        }
    }
}