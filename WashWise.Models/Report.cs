using Microsoft.AspNetCore.Identity;
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

        [Required]
        public Guid WashingMachineId { get; set; }
        public virtual WashingMachine WashingMachine { get; set; } = null!;

        [Required] 
        public string AuthorId { get; set; } = null!;

        public virtual IdentityUser? Author { get; set; }
        
        public bool IsResolved { get; set; } = false;
    }
}