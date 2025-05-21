using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WashWise.Models
{
    public class ActivityLog : BaseEntity
    {
        [Required] 
        public string UserId { get; set; } = null!;
        public virtual IdentityUser? User { get; set; }

        [Required] 
        public string Action { get; set; } = null!;

        [Required]
        public Guid ReportId { get; set; }
        public Report? Report { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}