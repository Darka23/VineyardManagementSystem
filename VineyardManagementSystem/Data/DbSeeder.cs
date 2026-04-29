using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using VineyardManagementSystem.Enums;
using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // 1. Създаване на ролите
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Създаване на Админ
            var adminEmail = "admin@vineyard.bg";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                await userManager.CreateAsync(admin, "admin123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
        public static void Seed(ApplicationDbContext context)
        {
            if (context.GrapeVarieties.Any()) return;

            // 1. Сортове Грозде
            var merlot = new GrapeVariety { Name = "Мерло", Color = GrapeColor.Червено };
            var syrah = new GrapeVariety { Name = "Сира", Color = GrapeColor.Червено };
            var chardonnay = new GrapeVariety { Name = "Шардоне", Color = GrapeColor.Бяло };
            var sauvignon = new GrapeVariety { Name = "Совиньон Блан", Color = GrapeColor.Бяло };

            context.GrapeVarieties.AddRange(merlot, syrah, chardonnay, sauvignon);
            context.SaveChanges();

            // 2. Лозови Масиви
            var v1 = new Vineyard { Name = "Южни склонове - Мелник", Location = "с. Капатово", Size = 150, PlantingDate = new DateTime(2015, 3, 20) };
            var v2 = new Vineyard { Name = "Морски бриз", Location = "гр. Поморие", Size = 80, PlantingDate = new DateTime(2018, 4, 10) };

            context.Vineyards.AddRange(v1, v2);
            context.SaveChanges();

            // 3. Парцели
            var p1 = new Plot { InternalCode = "M-01", VineyardId = v1.Id, GrapeVarietyId = merlot.Id, AreaSize = 50 };
            var p2 = new Plot { InternalCode = "S-02", VineyardId = v1.Id, GrapeVarietyId = syrah.Id, AreaSize = 40 };
            var p3 = new Plot { InternalCode = "CH-01", VineyardId = v2.Id, GrapeVarietyId = chardonnay.Id, AreaSize = 30 };

            context.Plots.AddRange(p1, p2, p3);
            context.SaveChanges();

            // 4. Климатични данни (за последните няколко дни)
            context.ClimateLogs.AddRange(
                new ClimateLog { VineyardId = v1.Id, LogDate = DateTime.Now.AddDays(-2), Temperature = 24.5, Humidity = 60, Rainfall = 0 },
                new ClimateLog { VineyardId = v1.Id, LogDate = DateTime.Now.AddDays(-1), Temperature = 26.2, Humidity = 55, Rainfall = 2.5 },
                new ClimateLog { VineyardId = v2.Id, LogDate = DateTime.Now, Temperature = 22.8, Humidity = 75, Rainfall = 0 }
            );

            // 5. Дейности (Разходи)
            context.FieldActivities.AddRange(
                new FieldActivity { PlotId = p1.Id, ActivityType = ActivityType.Пръскане, Date = DateTime.Now.AddMonths(-1), Cost = 450.50m, Description = "Третиране срещу мана с Купроцин." },
                new FieldActivity { PlotId = p2.Id, ActivityType = ActivityType.Резитба, Date = DateTime.Now.AddMonths(-2), Cost = 1200.00m, Description = "Зимна резитба - наемане на сезонен екип." },
                new FieldActivity { PlotId = p3.Id, ActivityType = ActivityType.Торене, Date = DateTime.Now.AddMonths(-1), Cost = 320.00m, Description = "Азотно торене преди вегетация." }
            );

            // 6. Гроздобер (История)
            context.Harvests.AddRange(
                new Harvest { PlotId = p1.Id, HarvestDate = new DateTime(2025, 9, 15), QuantityKG = 4500, SugarContent = 24.5 },
                new Harvest { PlotId = p2.Id, HarvestDate = new DateTime(2025, 9, 20), QuantityKG = 3800, SugarContent = 23.2 },
                new Harvest { PlotId = p3.Id, HarvestDate = new DateTime(2025, 8, 30), QuantityKG = 2900, SugarContent = 22.0 }
            );

            context.SaveChanges();
        }
    }
}