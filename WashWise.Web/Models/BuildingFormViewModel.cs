using System.ComponentModel.DataAnnotations;

namespace WashWise.Web.Models
{
    public class BuildingFormViewModel
    {
        public Guid? Id { get; set; } 

        [Required(ErrorMessage = "Моля въведете име/номер на блока!")]
        [MaxLength(100, ErrorMessage = "Максималната дължина на името не трябва да превишава 100 символа!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Моля въведете адрес на блока!")]
        [MaxLength(100, ErrorMessage = "Максималната дължина на адреса не трябва да превишава 100 символа!")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Моля въведете град на блока!")]
        [MaxLength(50, ErrorMessage = "Максималната дължина на града не трябва да превишава 50 символа!")]
        public string City { get; set; } = null!;

        public int? VersionNo { get; set; }
    }
}