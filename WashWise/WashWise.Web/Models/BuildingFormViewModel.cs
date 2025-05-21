using System.ComponentModel.DataAnnotations;

namespace WashWise.Web.Models
{
    public class BuildingFormViewModel
    {
        public Guid? Id { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string City { get; set; } = null!;

        public int? VersionNo { get; set; }
    }
}