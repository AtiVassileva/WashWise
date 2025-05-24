using System.ComponentModel.DataAnnotations;

namespace WashWise.Models
{
    public class ActivityLog
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required] 
        public string Action { get; set; } = null!;

        [Required]
        public string TableName { get; set; } = null!;

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}