using System.ComponentModel.DataAnnotations;

namespace WashWise.Models
{
    public class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedOn { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int VersionNo { get; set; } = 1;
    }
}