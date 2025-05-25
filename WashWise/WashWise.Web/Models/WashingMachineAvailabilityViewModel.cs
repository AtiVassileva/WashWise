namespace WashWise.Web.Models
{
    public class WashingMachineAvailabilityViewModel
    {
        public Guid WashingMachineId { get; set; }
        public Guid ConditionId { get; set; }

        public string MachineModel { get; set; } = string.Empty;
    }
}