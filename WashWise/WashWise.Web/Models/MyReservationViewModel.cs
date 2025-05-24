namespace WashWise.Web.Models
{
    public class MyReservationViewModel
    {
        public Guid ReservationId { get; set; }
        public string? WashingMachineModel { get; set; }
        public Guid WashingMachineId { get; set; }
        public string? BuildingName { get; set; }
        public string? Address { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Status { get; set; }
        public bool CanCancel { get; set; }
    }
}