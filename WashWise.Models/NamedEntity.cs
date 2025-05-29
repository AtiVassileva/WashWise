using System.ComponentModel.DataAnnotations;

namespace WashWise.Models
{
    using static Models.Common.Constants;

    public abstract class NamedEntity : BaseEntity
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}