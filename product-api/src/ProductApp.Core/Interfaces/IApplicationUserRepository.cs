using ProductApp.Core.Domain;

namespace ProductApp.Core.Interfaces;

public interface IApplicationUserRepository
{
    Task<ApplicationUser> GetUserByIdAsync(string userId);
    Task<ApplicationUser> GetUserByEmailAsync(string email);
    Task CreateUserAsync(ApplicationUser user, string password);
}

