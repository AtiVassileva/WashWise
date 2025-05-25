using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WashWise.Web.Models
{
    public class WashingMachineFormModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете модел на пералната машина!")]
        public string MachineModel { get; set; } = null!;

        [Required]
        public Guid BuildingId { get; set; }

        [Required]
        public Guid ConditionId { get; set; }

        public List<SelectListItem> Buildings { get; set; } = new();
        public List<SelectListItem> Conditions { get; set; } = new();
    }
}