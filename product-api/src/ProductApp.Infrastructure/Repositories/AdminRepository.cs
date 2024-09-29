using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductApp.Core.Domain;
using ProductApp.Core.Interfaces;

namespace ProductApp.Infrastructure.Repositories;

public class AdminRepository(UserManager<ApplicationUser> userManager) 
    : IAdminRepository
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<bool> AssignAdminRoleAsync(ApplicationUser user)
    {
        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            return false; 
        }

        var result = await _userManager.AddToRoleAsync(user, "Admin");
        return result.Succeeded;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<ApplicationUser> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) 
            ?? throw new InvalidOperationException($"User with ID {userId} not found.");
            
        return user;
    }
}