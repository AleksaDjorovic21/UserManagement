using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Core.Interfaces;

namespace ProductApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")] 
public class AdminController(IAdminRepository adminRepository) 
    : ControllerBase
{
    private readonly IAdminRepository _adminRepository = adminRepository;

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _adminRepository.GetAllUsersAsync(); 
        var userSummaries = users.Select(user => new
        {
            user.Id,
            user.UserName,
            user.FirstName,
            user.LastName
        });

        return Ok(userSummaries);
    }

    [HttpPost("assign-admin-role/{userId}")]
    public async Task<IActionResult> AssignAdminRole(string userId)
    {
        var user = await _adminRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"User with ID {userId} not found.");
        }

        var result = await _adminRepository.AssignAdminRoleAsync(user);
        if (!result)
        {
            return BadRequest("User is already an Admin.");
        }

        return Ok("Admin role assigned successfully.");
    }
}

