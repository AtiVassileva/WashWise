using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WashWise.Models;

namespace WashWise.Data
{
    public class WashWiseDbContext : IdentityDbContext
    {
        public WashWiseDbContext()
        {
        }
        public WashWiseDbContext(DbContextOptions<WashWiseDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<ActivityLog> ActivityLogs { get; set; } = null!;
        public DbSet<Condition> Conditions { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<WashingMachine> WashingMachines { get; set; } = null!;
        public DbSet<Building> Buildings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("21180047");

            builder.Entity<Building>()
                .HasMany(b => b.WashingMachines)
                .WithOne(wm => wm.Building)
                .HasForeignKey(wm => wm.BuildingId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<WashingMachine>()
                .HasOne(w => w.Building)
                .WithMany(b => b.WashingMachines)
                .HasForeignKey(w => w.BuildingId);

            builder.Entity<WashingMachine>()
                .HasMany(w => w.Reservations)
                .WithOne(r => r.WashingMachine)
                .HasForeignKey(r => r.WashingMachineId);

            builder.Entity<WashingMachine>()
                .HasMany(w => w.Reports)
                .WithOne(r => r.WashingMachine)
                .HasForeignKey(r => r.WashingMachineId);

            builder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Reservation>()
                .HasOne(r => r.WashingMachine)
                .WithMany(w => w.Reservations)
                .HasForeignKey(r => r.WashingMachineId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Report>()
                .HasOne(r => r.WashingMachine)
                .WithMany(w => w.Reports)
                .HasForeignKey(r => r.WashingMachineId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Report>()
                .HasOne(r => r.Author)
                .WithMany()
                .HasForeignKey(r => r.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ActivityLog>()
                .HasOne(a => a.Report)
                .WithMany()
                .HasForeignKey(a => a.ReportId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ActivityLog>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.VersionNo = 1;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.VersionNo += 1;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}