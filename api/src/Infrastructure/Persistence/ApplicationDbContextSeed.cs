using System.Collections.Generic;
using Volue.Domain.Entities;
using Volue.Domain.ValueObjects;
using Volue.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Jwk;
using Volue.Application.DataPoints;

namespace Volue.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            var example1 = GenerateDataSeries("example1", 13515551, 1000, 1f, 0.1f);
            var example2 = GenerateDataSeries("example2", 13515551, 1000, 1f, 0.1f);

            // Seed, if necessary
            if (!context.DataPoints.Any())
            {
                context.DataPoints.AddRange(example1);
                context.DataPoints.AddRange(example2);
                context.DataPoints.AddRange(new List<DataPoint>
                {
                    new DataPoint
                    {
                        Name = "test",
                        TimeStamp = 1,
                        Value = 1f, 
                    },
                    new DataPoint
                    {
                        Name = "test",
                        TimeStamp = 2,
                        Value = 2f, 
                    },
                    new DataPoint
                    {
                        Name = "test",
                        TimeStamp = 3,
                        Value = 3f, 
                    },
                    new DataPoint
                    {
                        Name = "test",
                        TimeStamp = 4,
                        Value = 4f, 
                    },
                    new DataPoint
                    {
                        Name = "test",
                        TimeStamp = 5,
                        Value = 5f, 
                    },
                });

                await context.SaveChangesAsync();
            }
        }
        
        private static List<DataPoint> GenerateDataSeries(
            string name, 
            int timeStampStart, 
            int count, 
            float valueInit, 
            float valueStep) => 
                Enumerable.Range(timeStampStart, count).Select(ts =>
                {
                    valueInit = ts == timeStampStart ? valueInit : valueInit + valueStep;
                    return new DataPoint
                    {
                        Name = name,
                        TimeStamp = ts,
                        Value = valueInit
                    };
                }).ToList();
    }
}
