namespace WashWise.Web.Models
{
    public class ReservationSlotViewModel
    {
        public Guid WashingMachineId { get; set; }
        public string MachineModel { get; set; } = null!;
        public DateTime SlotStart { get; set; }
        public DateTime SlotEnd { get; set; }
        public bool IsAvailable { get; set; }
    }
}