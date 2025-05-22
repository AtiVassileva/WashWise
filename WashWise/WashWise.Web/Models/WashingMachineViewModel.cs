namespace WashWise.Web.Models
{
    public class WashingMachineViewModel
    {
        public Guid Id { get; set; }
        public string Model { get; set; } = null!;
        public string BuildingAddress { get; set; } = null!;
        public string ConditionName { get; set; } = null!;
    }
}