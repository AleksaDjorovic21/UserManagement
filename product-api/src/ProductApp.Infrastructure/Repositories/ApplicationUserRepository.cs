using Microsoft.AspNetCore.Identity;
using ProductApp.Core.Domain;
using ProductApp.Core.Interfaces;

namespace ProductApp.Infrastructure.Repositories;

public class ApplicationUserRepository(UserManager<ApplicationUser> userManager) 
    : IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<ApplicationUser> GetUserByIdAsync(string userId)
    {
        var applicationUser = await _userManager.FindByIdAsync(userId) 
            ?? throw new Exception($"User Account with id: {userId} not found.");

        return applicationUser;
    }

    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email)
            ?? throw new Exception($"User Account with email: {email} not found.");
        return applicationUser;
    }

    public async Task CreateUserAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new Exception("User registration failed: " + 
                string.Join(", ", result.Errors.Select(e => e.Description)));
        };
    }
}

