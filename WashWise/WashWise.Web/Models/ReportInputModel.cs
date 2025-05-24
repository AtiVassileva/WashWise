using System.ComponentModel.DataAnnotations;

namespace WashWise.Web.Models
{
    public class ReportInputModel
    {
        [Required]
        public Guid WashingMachineId { get; set; }

        [Required(ErrorMessage = "Описанието е задължително!")]
        [StringLength(1000, ErrorMessage = "Описанието може да е максимум 1000 символа.")]
        [Display(Name = "Описание на проблема")]
        public string Description { get; set; } = null!;

        public string? WashingMachineModel { get; set; }
        public string? BuildingAddress { get; set; }
    }
}