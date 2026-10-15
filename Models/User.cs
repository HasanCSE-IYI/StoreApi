namespace StoreApi.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public string Role { get; set; } = "Admin"; // "Admin" or "User"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}