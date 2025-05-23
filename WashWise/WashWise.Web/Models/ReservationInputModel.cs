using System.ComponentModel.DataAnnotations;

namespace WashWise.Web.Models
{
    public class ReservationInputModel
    {
        public Guid MachineId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
    }
}