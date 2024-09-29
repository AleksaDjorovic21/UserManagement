using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Core.Domain;
using ProductApp.Core.Interfaces;

namespace ProductApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationUserController(IApplicationUserRepository userRepository,
                                    SignInManager<ApplicationUser> signInManager,
                                    UserManager<ApplicationUser> userManager) 
                                    : ControllerBase
{
    private readonly IApplicationUserRepository _userRepository = userRepository;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager; 

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Register model)
    {
        var user = new ApplicationUser 
        { 
            UserName = model.Email,
            FirstName = model.FirstName, 
            LastName = model.LastName, 
            Email = model.Email
        };
        await _userRepository.CreateUserAsync(user, model.Password);
        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
        
        if (result.Succeeded)
        {
            // Optionally retrieve user details and roles
            var user = await _userRepository.GetUserByEmailAsync(model.Email);
            var roles = await _userManager.GetRolesAsync(user);

            // You can return user info along with roles
            return Ok(new 
            { 
                Message = "Login successful.", 
                Roles = roles 
            });
        }     
        return Unauthorized("Invalid login attempt.");
    }
}

