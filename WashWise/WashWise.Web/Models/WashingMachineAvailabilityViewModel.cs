namespace WashWise.Web.Models
{
    public class WashingMachineAvailabilityViewModel
    {
        public Guid WashingMachineId { get; set; }

        public string MachineModel { get; set; } = string.Empty;
    }
}