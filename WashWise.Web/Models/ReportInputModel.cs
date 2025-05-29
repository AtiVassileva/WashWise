using System.ComponentModel.DataAnnotations;
using static WashWise.Models.Common.Constants;

namespace WashWise.Web.Models
{
    public class ReportInputModel
    {
        [Required]
        public Guid WashingMachineId { get; set; }

        [Required(ErrorMessage = "Описанието е задължително!")]
        [StringLength(DescriptionMaxLength, ErrorMessage = "Описанието може да е максимум 2000 символа!")]
        [Display(Name = "Описание на проблема")]
        public string Description { get; set; } = null!;

        public string? WashingMachineModel { get; set; }
        public string? BuildingAddress { get; set; }
    }
}