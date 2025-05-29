using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WashWise.Web.Models
{
    public class ReferenceFilterViewModel
    {
        [Display(Name = "Начална дата")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Крайна дата")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Блок")]
        public Guid? BuildingId { get; set; }
        [Display(Name = "Състояние")]
        public Guid? ConditionId { get; set; }

        public List<SelectListItem> Buildings { get; set; } = new();
        public List<SelectListItem> Conditions { get; set; } = new();

        public List<WashingMachineReference> Results { get; set; } = new();
    }
}