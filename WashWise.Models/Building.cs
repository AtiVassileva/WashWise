using System.ComponentModel.DataAnnotations;
using static WashWise.Models.Common.Constants;

namespace WashWise.Models
{
    public class Building : NamedEntity
    {
        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string City { get; set; } = null!;

        public ICollection<WashingMachine> WashingMachines { get; set; } = new HashSet<WashingMachine>();
    }
}