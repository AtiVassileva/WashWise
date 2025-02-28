using System.ComponentModel.DataAnnotations;
using static WashWise.Models.Common.Constants;

namespace WashWise.Models
{
    public class Report : BaseEntity
    {
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;
        public DateTime GeneratedAt { get; set; }
    }
}