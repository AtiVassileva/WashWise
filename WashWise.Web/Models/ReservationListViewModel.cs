namespace WashWise.Web.Models
{
    public class ReservationListViewModel
    {
        public Guid Id { get; set; }
        public string User { get; set; } = null!;
        public string Machine { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = null!;
    }
}