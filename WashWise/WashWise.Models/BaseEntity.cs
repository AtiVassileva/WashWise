using System.ComponentModel.DataAnnotations;
using static WashWise.Models.Common.Constants;

namespace WashWise.Models
{
    public class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}