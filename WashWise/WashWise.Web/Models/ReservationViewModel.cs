namespace WashWise.Web.Models
{
    public class ReservationViewModel
    {
        public Guid WashingMachineId { get; set; }

        public DateTime? Date { get; set; }

        public List<ReservationSlotViewModel> ReservationSlots { get; set; } = new();

        public string? MachineModel { get; set; } 
        public string? Location { get; set; } 
    }
}