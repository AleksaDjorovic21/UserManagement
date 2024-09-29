using ProductApp.Core.Domain;

namespace ProductApp.Core.Interfaces;

public interface IAdminRepository
{
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    Task<bool> AssignAdminRoleAsync(ApplicationUser user);
    Task<ApplicationUser> GetUserByIdAsync(string userId);
}

