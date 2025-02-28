using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WashWise.Models;

namespace WashWise.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<ActivityLog> ActivityLogs { get; set; } = null!;
        public DbSet<Condition> Conditions { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<WashingMachine> WashingMachines { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("21180047");
        }
    }
}