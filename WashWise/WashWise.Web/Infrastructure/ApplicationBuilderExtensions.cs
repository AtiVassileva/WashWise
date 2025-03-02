using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WashWise.Data;
using WashWise.Models;
using static WashWise.Web.Common.CommonConstants;

namespace WashWise.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            MigrateDatabase(dbContext);

            SeedConditions(dbContext);
            SeedStatuses(dbContext);
            SeedAdministrator(serviceProvider);

            return app;
        }

        private static void MigrateDatabase(ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();
        }

        private static void SeedConditions(ApplicationDbContext dbContext)
        {
            if (dbContext.Conditions.Any())
            {
                return;
            }

            var conditions = new List<Condition>
            {
                new () { Name = "Available" },
                new () { Name = "Booked" },
                new () { Name = "Damaged" },
            };

            dbContext.Conditions.AddRange(conditions);
            dbContext.SaveChanges();
        }

        private static void SeedStatuses(ApplicationDbContext dbContext)
        {
            if (dbContext.Statuses.Any())
            {
                return;
            }

            var statuses = new List<Status>
            {
                new() { Name = "In Progress" },
                new() { Name = "Upcoming" },
                new() { Name = "Cancelled" },
                new() { Name = "Completed" },
            };

            dbContext.Statuses.AddRange(statuses);
            dbContext.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider
                .GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@abv.bg";
                    const string adminPassword = "admin123";

                    var user = new IdentityUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                    };

                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, role.Name);

                })
                .GetAwaiter()
                .GetResult();
        }
    }
}