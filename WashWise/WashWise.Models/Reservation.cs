using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WashWise.Models
{
    public class Reservation : BaseEntity
    {
        [Required]
        public string UserId { get; set; } = null!;
        public virtual IdentityUser? User { get; set; }

        [Required]
        public Guid WashingMachineId { get; set; }
        public virtual WashingMachine? WashingMachine { get; set; }

        [Required] 
        public Guid StatusId { get; set; }
        public Status? Status { get; set; }

        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }
    }
}