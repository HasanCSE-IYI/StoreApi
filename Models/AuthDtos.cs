using System.ComponentModel.DataAnnotations;

namespace StoreApi.Models;

public class RegisterDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Username { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public required string Password { get; set; }

    public string Role { get; set; } = "User";
}

public class LoginDto
{
    [Required]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; }
}

public record AuthResponseDto(string Token, string Username, string Role);