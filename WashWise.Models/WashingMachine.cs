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
        public Guid ConditionId { get; set; }
        public virtual Condition? Condition { get; set; }

        [Required]
        public Guid BuildingId { get; set; }
        public virtual Building Building { get; set; } = null!;

        public DateTime? BusyUntil { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public ICollection<Report> Reports { get; set; } = new HashSet<Report>();
    }
}