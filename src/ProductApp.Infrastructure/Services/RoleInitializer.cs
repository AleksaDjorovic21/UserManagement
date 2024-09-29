using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Core.Domain;

namespace ProductApp.Infrastructure.Services;

public class RoleInitializer
{
    public static async Task InitializeRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roleNames = ["Admin", "User"];

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Create the roles and seed them to the database
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create a default admin user
        var adminEmail = "Admin"; 
        var adminPassword = "Admin123"; 

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            // Ensure to set required properties
            adminUser = new ApplicationUser 
            { 
                UserName = adminEmail, 
                Email = adminEmail,
                FirstName = "Admin", 
                LastName = "User" 
            };
            await userManager.CreateAsync(adminUser, adminPassword);
        }

        // Assign the Admin role to the admin user
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

