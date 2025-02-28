using System.ComponentModel.DataAnnotations;
using static WashWise.Models.Common.Constants;

namespace WashWise.Models
{
    public class WashingMachine : BaseEntity
    {
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Model { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        public Guid ConditionId { get; set; }
        public virtual Condition? Condition { get; set; }
    }
}