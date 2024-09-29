using Microsoft.AspNetCore.Identity;

namespace ProductApp.Core.Domain;

public class ApplicationUser : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}