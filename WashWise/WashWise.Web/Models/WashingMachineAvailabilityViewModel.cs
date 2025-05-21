namespace WashWise.Web.Models
{
    public class WashingMachineAvailabilityViewModel
    {
        public Guid WashingMachineId { get; set; }

        public string Model { get; set; } = string.Empty;

        public string Condition { get; set; } = string.Empty;

        public DateTime? OccupiedUntil { get; set; }

        public bool IsAvailable => Condition == "Available" && OccupiedUntil == null;
    }
}