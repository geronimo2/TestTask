using System.Collections.Generic;
using Volue.Domain.Entities;
using Volue.Domain.ValueObjects;
using Volue.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

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
            // Seed, if necessary
            if (!context.DataPoints.Any())
            {
                context.DataPoints.AddRange(new List<DataPoint>
                {
                    new DataPoint() { Name = "example1", TimeStamp = 13515551, Value = 1.1f },
                    new DataPoint() { Name = "example1", TimeStamp = 13515552, Value = 2.4f },
                    new DataPoint() { Name = "example1", TimeStamp = 13515553, Value = 3.5f },
                    new DataPoint() { Name = "example2", TimeStamp = 13515554, Value = 1.5f },
                    new DataPoint() { Name = "example2", TimeStamp = 13515555, Value = 2.5f },
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
