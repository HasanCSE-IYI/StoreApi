using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Data;
using StoreApi.Models;
using StoreApi.Services;

namespace StoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AppDbContext context, ITokenService tokenService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
    {
        // ইউজারনেম আগেই নেওয়া হয়েছে কি না যাচাই
        if (await context.Users.AnyAsync(u => u.Username.ToLower() == dto.Username.ToLower()))
        {
            return BadRequest(new { message = "Username already exists." });
        }

        // BCrypt দিয়ে পাসওয়ার্ড হ্যাশ করা
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = passwordHash,
            Role = dto.Role
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        var token = tokenService.CreateToken(user);
        return Ok(new AuthResponseDto(token, user.Username, user.Role));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == dto.Username.ToLower());
        
        // ইউজার বা পাসওয়ার্ড না মিললে
        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Invalid username or password." });
        }

        var token = tokenService.CreateToken(user);
        return Ok(new AuthResponseDto(token, user.Username, user.Role));
    }
}