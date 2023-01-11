using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Context
{
    public class AppDbContextInitializer
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AppDbContextInitializer(AppDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (context.Database.IsSqlServer())
                {
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SeedDatabase()
        {
            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");
            var manufacturerRole = new IdentityRole("Manufacturer");
            var distributorRole = new IdentityRole("Distributor");

            if (roleManager.Roles.All(r => r.Name != adminRole.Name))
            {
                await roleManager.CreateAsync(adminRole);
            }

            if (roleManager.Roles.All(r => r.Name != userRole.Name))
            {
                await roleManager.CreateAsync(userRole);
            }

            if (roleManager.Roles.All(r => r.Name != manufacturerRole.Name))
            {
                await roleManager.CreateAsync(manufacturerRole);
            }

            if (roleManager.Roles.All(r => r.Name != distributorRole.Name))
            {
                await roleManager.CreateAsync(distributorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                if (!string.IsNullOrWhiteSpace(adminRole.Name))
                {
                    await userManager.AddToRolesAsync(administrator, new[] { adminRole.Name });
                }
            }
        }
    }
}
