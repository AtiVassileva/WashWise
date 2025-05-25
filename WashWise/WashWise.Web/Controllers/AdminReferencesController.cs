using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ClosedXML.Excel;
using WashWise.Data;
using WashWise.Web.Models;
using static WashWise.Web.Common.CommonConstants;
using Microsoft.AspNetCore.Mvc.Rendering;
using WashWise.Services.Contracts;

namespace WashWise.Web.Controllers
{
    [Authorize(Roles = AdministratorRoleName)]
    public class AdminReferencesController : Controller
    {
        private readonly WashWiseDbContext _dbContext;
        private readonly IBuildingService _buildingService;

        public AdminReferencesController(WashWiseDbContext dbContext, IBuildingService buildingService)
        {
            _dbContext = dbContext;
            _buildingService = buildingService;
        }

        public IActionResult Index()
        {
            var model = new ReferenceFilterViewModel
            {
                Buildings = _dbContext.Buildings
                    .Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() })
                    .ToList(),
                Conditions = _dbContext.Conditions
                    .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
                    .ToList()
            };

            if (model.BuildingId == Guid.Empty)
                model.BuildingId = null;

            if (model.ConditionId == Guid.Empty)
                model.ConditionId = null;


            return View(model);
        }
        
        public IActionResult GenerateReport(ReferenceFilterViewModel model)
        {
            model.Results = GetFilteredData(model.StartDate, model.EndDate, model.BuildingId, model.ConditionId);

            model.Buildings = _dbContext.Buildings
                .OrderBy(b => b.Id)
                .Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() })
                .ToList();

            model.Conditions = _dbContext.Conditions
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
                .ToList();

            return View("Index", model);
        }

        public async Task<IActionResult> ExportToCsv(ReferenceFilterViewModel model)
        {
            var data = GetFilteredData(model.StartDate, model.EndDate, model.BuildingId, model.ConditionId);

            var csv = new StringBuilder();
            csv.AppendLine("\uFEFFБлок,Номер на пералня,Общо часове,Брой резервации,Брой повреди");

            foreach (var item in data)
            {
                var building = await _buildingService.GetByIdAsync(item.BuildingId);
                var buildingName = string.Concat(building?.Name, " - ", building?.Address, " - ", building?.City);

                csv.AppendLine($"{buildingName},{item.MachineModel},{item.TotalHoursWorked},{item.ReservationCount},{item.ReportCount}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "spravka.csv");
        }

        public async Task<IActionResult> ExportToExcel(DateTime? startDate, DateTime? endDate, Guid? buildingId, Guid? conditionId)
        {
            var data = GetFilteredData(startDate, endDate, buildingId, conditionId);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Справка");
                
                worksheet.Cell(1, 1).Value = "Блок";
                worksheet.Cell(1, 2).Value = "Модел";
                worksheet.Cell(1, 3).Value = "Общо часове";
                worksheet.Cell(1, 4).Value = "Брой резервации";
                worksheet.Cell(1, 5).Value = "Брой повреди";

                for (var i = 0; i < data.Count; i++)
                {
                    var row = i + 2;

                    var building = await _buildingService.GetByIdAsync(data[i].BuildingId);
                    var buildingName = string.Concat(building?.Name, " - ", building?.Address, " - ", building?.City);

                    worksheet.Cell(row, 1).Value = buildingName ?? "Няма име";
                    worksheet.Cell(row, 2).Value = data[i].MachineModel;
                    worksheet.Cell(row, 3).Value = data[i].TotalHoursWorked;
                    worksheet.Cell(row, 4).Value = data[i].ReservationCount;
                    worksheet.Cell(row, 5).Value = data[i].ReportCount;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"spravka-{DateTime.Now:yyyyMMdd-HHmm}.xlsx");
                }
            }
        }

        private List<WashingMachineReference> GetFilteredData(DateTime? startDate, DateTime? endDate, Guid? buildingId, Guid? conditionId)
        {
            endDate = endDate?.AddHours(24);

            var machinesQuery = _dbContext.WashingMachines
                .Where(m =>
                    (buildingId == null || m.BuildingId == buildingId) &&
                    (conditionId == null  || m.ConditionId == conditionId)
                );

            var result = machinesQuery.Select(m => new WashingMachineReference
            {
                BuildingId = m.BuildingId,
                MachineModel = m.Model,
                TotalHoursWorked = _dbContext.Reservations
                    .Where(r => r.WashingMachineId == m.Id &&
                                (!startDate.HasValue || r.StartTime >= startDate) &&
                                (!endDate.HasValue || r.EndTime <= endDate))
                    .Sum(r => (double?)EF.Functions.DateDiffMinute(r.StartTime, r.EndTime)) / 60.0 ?? 0,

                ReservationCount = _dbContext.Reservations
                    .Count(r => r.WashingMachineId == m.Id &&
                                (!startDate.HasValue || r.StartTime >= startDate) &&
                                (!endDate.HasValue || r.EndTime <= endDate)),

                ReportCount = _dbContext.Reports
                    .Count(f => f.WashingMachineId == m.Id &&
                                (!startDate.HasValue || f.GeneratedAt >= startDate) &&
                                (!endDate.HasValue || f.GeneratedAt <= endDate))
            });

            return result
                .OrderBy(x => x.BuildingId)
                .ThenByDescending(x => x.TotalHoursWorked)
                .ThenByDescending(x => x.ReservationCount)
                .ThenBy(x => x.MachineModel)
                .ThenByDescending(x => x.ReportCount)
                .ToList();
        }
    }
}