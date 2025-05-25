namespace WashWise.Web.Models
{
    public class WashingMachineReference
    {
        public Guid BuildingId { get; set; }
        public string MachineModel { get; set; }
        public double TotalHoursWorked { get; set; }
        public int ReservationCount { get; set; }
        public int ReportCount { get; set; }
    }
}