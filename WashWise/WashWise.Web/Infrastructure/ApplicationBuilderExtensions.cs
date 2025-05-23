using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WashWise.Data;
using WashWise.Models;
using WashWise.Services;
using WashWise.Services.Contracts;
using static WashWise.Web.Common.CommonConstants;

namespace WashWise.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void RegisterServicesCollection(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddTransient<IBuildingService, BuildingService>()
                .AddTransient<IWashingMachineService, WashingMachineService>()
                .AddTransient<IConditionService, ConditionService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IReportService, ReportService>()
                .AddTransient<IReservationService, ReservationService>()
                .AddTransient<IStatusService, StatusService>()
                .AddHostedService<ReservationStatusUpdaterService>();
        }

        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            var dbContext = serviceProvider.GetRequiredService<WashWiseDbContext>();

            MigrateDatabase(dbContext);

            SeedConditions(dbContext);
            SeedStatuses(dbContext);
            await SeedRolesAsync(serviceProvider);

            return app;
        }

        private static void MigrateDatabase(WashWiseDbContext dbContext)
        {
            dbContext.Database.Migrate();
        }

        private static void SeedConditions(WashWiseDbContext dbContext)
        {
            if (dbContext.Conditions.Any())
            {
                return;
            }

            var conditions = new List<Condition>
            {
                new () { Name = "Свободна" },
                new () { Name = "Заета" },
                new () { Name = "Повредена" },
            };

            dbContext.Conditions.AddRange(conditions);
            dbContext.SaveChanges();
        }

        private static void SeedStatuses(WashWiseDbContext dbContext)
        {
            if (dbContext.Statuses.Any())
            {
                return;
            }

            var statuses = new List<Status>
            {
                new() { Name = "В прогрес" },
                new() { Name = "Предстояща" },
                new() { Name = "Канселирана" },
                new() { Name = "Приключена" }
            };

            dbContext.Statuses.AddRange(statuses);
            dbContext.SaveChanges();
        }

        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { UserRoleName, AdministratorRoleName };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = roleName });
                }
            }

            const string adminEmail = "admin@abv.bg";
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                const string adminPassword = "admin123";

                var admin = new IdentityUser
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };

                var result = await userManager.CreateAsync(admin, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, AdministratorRoleName);
                }
            }
        }
    }
}